using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.SqlLite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Timers;

namespace ProductieManager.Rpm.Various
{
    public class ProductieChat : IDisposable
    {
        public string ChatPath { get; private set; }
        public string ProfielPath { get; private set; }
        public string BerichtenPath { get; private set; }
        public string PublicLobyPath { get; private set; }
        public string GroupChatImagePath { get; set; }
        public List<UserChat> Gebruikers { get; set; } = new List<UserChat>();
        public bool LoggedIn { get; private set; }
        public bool IsLoggIn { get; private set; }
        public UserChat Chat { get; private set; }
        public MultipleFileDb ProfilesIO { get; private set; }
        public MultipleFileDb MessagesIO { get; private set; }
        public MultipleFileDb PublicMessagesIO { get; private set; }

        public ProductieChat(string path)
        {
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);
            ChatPath = path;
        }     


        public bool Login()
        {
            try
            {
                if (Manager.LogedInGebruiker == null || string.IsNullOrEmpty(ChatPath) || IsLoggIn) return false;
                if (LoggedIn) return true;
                IsLoggIn = true;
                if (!Directory.Exists(ChatPath))
                    Directory.CreateDirectory(ChatPath);
                ProfielPath = Path.Combine(ChatPath, Manager.LogedInGebruiker.Username);
                BerichtenPath = Path.Combine(ChatPath, Manager.LogedInGebruiker.Username, "Berichten");
                PublicLobyPath = Path.Combine(ChatPath, "Iedereen", "Berichten");
                GroupChatImagePath = Path.Combine(ChatPath, "GroupChatImage.png");
                if (!File.Exists(GroupChatImagePath) || !GroupChatImagePath.IsImageFile())
                {
                    try
                    {
                        var data = Properties.Resources.users_clients_group_16774.ToByteArray();
                        File.WriteAllBytes(GroupChatImagePath, data);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                  
                }
                if (!Directory.Exists(ProfielPath))
                    Directory.CreateDirectory(ProfielPath);
                if (!Directory.Exists(BerichtenPath))
                    Directory.CreateDirectory(BerichtenPath);
                if (!Directory.Exists(PublicLobyPath))
                    Directory.CreateDirectory(PublicLobyPath);
                var user = Path.Combine(ChatPath, $"{Manager.LogedInGebruiker.Username}.rpm");

                try
                {
                    if (File.Exists(user))
                        Chat = MultipleFileDb.xFromPath<UserChat>(user, false) ?? new UserChat();
                    else Chat = new UserChat();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Chat = new UserChat();
                }
               
                Chat.UserName = Manager.LogedInGebruiker.Username;
                Chat.IsOnline = true;
                Chat.LastOnline = DateTime.Now;
                if (string.IsNullOrEmpty(Chat.ProfielImage) || !File.Exists(Chat.ProfielImage))
                {
                    Chat.ProfielImage = $"{ProfielPath}\\ProfielFoto.png";
                    Resources.avatardefault_92824.Save(Chat.ProfielImage, ImageFormat.Png);
                }
                Chat.Avatar = GetProfielImage(Chat);
                SaveGebruiker();
                UpdateGebruikers();
                ProfilesIO?.Close();
                ProfilesIO = new MultipleFileDb(ChatPath, true, "1.0", DbType.Messages);
                ProfilesIO.FileChanged += ProfilesIO_FileChanged;
                ProfilesIO.FileDeleted += ProfilesIO_FileDeleted;
                MessagesIO?.Close();
                MessagesIO = new MultipleFileDb(BerichtenPath, true, "1.0", DbType.Messages);
                MessagesIO.FileChanged += MessagesIO_FileChanged;
                MessagesIO.FileDeleted += MessagesIO_FileDeleted;
                PublicMessagesIO?.Close();
                PublicMessagesIO = new MultipleFileDb(PublicLobyPath, true, "1.0", DbType.Messages);
                PublicMessagesIO.FileChanged += PublicMessagesIO_FileChanged;
                PublicMessagesIO.FileDeleted += PublicMessagesIO_FileDeleted;
                LoggedIn = true;
                IsLoggIn = false;
                return true;
            }
            catch (Exception e)
            {
                IsLoggIn = false;
                Console.WriteLine(e);
                return false;
            }
        }

        public bool LogOut()
        {
            try
            {
                if (!LoggedIn) return true;

                if (Chat != null)
                {
                    Chat.IsOnline = false;
                    Chat.LastOnline = DateTime.Now;
                    SaveGebruiker();
                    Chat = null;
                }

                Gebruikers?.Clear();
                ProfilesIO?.Close();
                ProfilesIO = null;
                MessagesIO?.Close();
                MessagesIO = null;
                PublicMessagesIO?.Close();
                PublicMessagesIO = null;
                LoggedIn = false;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        #region IO Events
        private void PublicMessagesIO_FileDeleted(object sender, FileSystemEventArgs e)
        {
            OnPublicMessageDeleted(sender, e);
        }

        private void PublicMessagesIO_FileChanged(object sender, FileSystemEventArgs e)
        {
           if(File.Exists(e.FullPath))
            {
                var ent = MultipleFileDb.xFromPath<ProductieChatEntry>(e.FullPath, false);
                if (ent != null)
                    OnPublicMessageChanged(ent, e);
            }
        }

        private void MessagesIO_FileDeleted(object sender, FileSystemEventArgs e)
        {
            OnMessageDeleted(sender, e);
        }

        private void MessagesIO_FileChanged(object sender, FileSystemEventArgs e)
        {
            if (File.Exists(e.FullPath))
            {
                var ent = MultipleFileDb.xFromPath<ProductieChatEntry>(e.FullPath, false);
                if (ent != null)
                    OnMessageChanged(ent, e);
            }
        }

        private void ProfilesIO_FileDeleted(object sender, FileSystemEventArgs e)
        {
            try
            {
                UpdateGebruikers();
                OnGebruikerDeleted(sender, e);
            }
            catch { }
        }

        private void ProfilesIO_FileChanged(object sender, FileSystemEventArgs e)
        {
            if (File.Exists(e.FullPath))
            {
                var ent = MultipleFileDb.xFromPath<UserChat>(e.FullPath, false);
                if (ent != null)
                {
                    UpdateGebruikers();
                    OnGebruikerChanged(ent, e);
                }
            }
        }
        #endregion IO Events

        public ProductieChatEntry SendMessage(string message, string destination)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrEmpty(destination) || Chat == null) return null;
            var ent = new ProductieChatEntry()
            {
                Afzender = Chat,
                Bericht = message,
                Ontvanger = destination
            };
            UpdateMessage(ent);
            return ent;
        }

        public bool UpdateMessage(ProductieChatEntry ent)
        {
            try
            {
                if (ent?.Ontvangers == null) return false;
                foreach (var ontv in ent.Ontvangers)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(ontv)) continue;
                        var xpath = Path.Combine(ChatPath, ontv, "Berichten");
                        if (!Directory.Exists(xpath))
                            Directory.CreateDirectory(xpath);
                        var xfile = Path.Combine(xpath, $"{ent.ID}.rpm");
                        MultipleFileDb.WriteInstanceToFile(ent, xfile, true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                return true;
            }
            catch { return false; }
        }

        public bool ChangeProfielImage(string img)
        {
            if (Chat == null || string.IsNullOrEmpty(img)) return false;
            try
            {
                if (!img.IsImageFile()) return false;
                Chat.ProfielImage = ProfielPath + "\\ProfielFoto.png";
                File.Copy(img, Chat.ProfielImage, true);
                FileInfo fi = new FileInfo(Chat.ProfielImage);
                fi.LastWriteTime = DateTime.Now;
                Chat.Avatar = GetProfielImage(Chat);
                return SaveGebruiker(Chat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public Bitmap GetProfielImage(UserChat chat)
        {
            try
            {
                if (Manager.LogedInGebruiker == null || chat?.UserName == null) return Resources.avatardefault_92824;
                var xfile = Path.Combine(ChatPath, chat.UserName, "ProfielFoto.png");
                if (File.Exists(xfile))
                    return Image.FromStream(new MemoryStream(File.ReadAllBytes(xfile))).ResizeImage(64, 64);
                else return Resources.avatardefault_92824;
            }
            catch (Exception exception)
            {
                return Resources.avatardefault_92824;
            }
        }

        public Bitmap GetProfielImage()
        {
            if (Chat == null) return Resources.avatardefault_92824;
            return GetProfielImage(Chat);
        }

        public bool SaveGebruiker(UserChat chat)
        {
            if (chat == null) return false;
            try
            {
                var dest = Path.Combine(ChatPath, $"{chat.UserName}.rpm");
                return MultipleFileDb.WriteInstanceToFile(chat, dest, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool SaveGebruiker()
        {
            if (Chat == null) return false;
            return SaveGebruiker(Chat);
        }

        public bool DeleteUser(UserChat chat)
        {
            try
            {
                if (string.IsNullOrEmpty(Chat?.UserName)) return false;
                var xfile = Path.Combine(ChatPath, $"{chat.UserName}.rpm");
                File.Delete(xfile);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        private bool UpdateGebruikers()
        {
            try
            {
                Gebruikers = new List<UserChat>();
                var xpath = ChatPath;
                if (Chat == null || !Directory.Exists(xpath)) return false;
                string[] files = Directory.GetFiles(xpath, "*.rpm", SearchOption.TopDirectoryOnly);
                Chat.Avatar = GetProfielImage();
                foreach (var file in files)
                {
                    var ent = MultipleFileDb.xFromPath<UserChat>(file, false);
                    if (Chat == null) break;
                    if (ent == null || string.Equals(Chat.UserName, ent.UserName,
                        StringComparison.CurrentCultureIgnoreCase) && !Gebruikers.Any(x=> string.Equals(x.UserName, ent.UserName, StringComparison.CurrentCultureIgnoreCase))) continue;
                    ent.Avatar = GetProfielImage(ent);
                    Gebruikers.Add(ent);
                }

                if (Chat == null)
                    Gebruikers.Clear();
                else Gebruikers = Gebruikers.OrderBy(x => x.UserName).ToList();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public List<ProductieChatEntry> GetMessagesFromAfzender(UserChat chat, string afzender, DateTime vanaf, bool onlyunread = false)
        {
            var xreturn = new List<ProductieChatEntry>();
            if (string.IsNullOrEmpty(chat?.UserName)) return xreturn;
            string xname = string.Equals(afzender, "iedereen", StringComparison.CurrentCultureIgnoreCase)
                ? afzender
                : chat.UserName;
            string path = Path.Combine(ChatPath, xname, "Berichten");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            List<string> files = Directory.GetFiles(path, "*.rpm", SearchOption.TopDirectoryOnly).Where(x => File.GetLastWriteTime(x) >= vanaf).ToList();
            if (string.IsNullOrEmpty(afzender))
            {
                var xpath = Path.Combine(ChatPath, "iedereen", "Berichten");
                var publicfiles = Directory.GetFiles(xpath, "*.rpm", SearchOption.TopDirectoryOnly);
                if (publicfiles.Length > 0)
                    files.AddRange(publicfiles);
            }
            try
            {
                foreach (var file in files)
                {
                    var ent = MultipleFileDb.xFromPath<ProductieChatEntry>(file, false);
                    if (ent?.Afzender == null) continue;
                    if (!vanaf.IsDefault())
                    {
                        File.SetLastWriteTime(file, ent.Tijd);
                        if (ent.Tijd < vanaf)
                            continue;
                    }
                    if (!string.IsNullOrEmpty(afzender) &&
                        !string.Equals(afzender, "iedereen", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (!string.Equals(ent.Afzender.UserName, afzender,
                                StringComparison.CurrentCultureIgnoreCase)) continue;

                    }

                    if (onlyunread && ent.IsGelezen)
                        continue;
                    if (onlyunread && string.Equals(ent.Afzender.UserName, chat.UserName,
                            StringComparison.CurrentCultureIgnoreCase))
                        continue;
                    xreturn.Add(ent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xreturn;
        }

        public List<ProductieChatEntry> GetConversation(UserChat user, bool privatemessages, DateTime vanaf)
        {
            var xreturn = new List<ProductieChatEntry>();
            try
            {
                if (user == null) return xreturn;

                var usermessages = string.Equals(user.UserName, "iedereen", StringComparison.CurrentCultureIgnoreCase)
                    ? new List<ProductieChatEntry>()
                    : GetMessagesFromAfzender(user, privatemessages ? Chat.UserName : null,vanaf);
                var mymessages = GetMessagesFromAfzender(Chat, privatemessages ? user.UserName : null,vanaf);
                if(mymessages.Count > 0)
                    usermessages.AddRange(mymessages);
                xreturn = usermessages.OrderBy(x => x.Tijd).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xreturn;
        }

        public int UnreadMessages(UserChat chat, string afzender, DateTime vanaf)
        {
            var msgs = GetMessagesFromAfzender(chat, afzender, vanaf, true);
            return msgs.Count;
        }

        public int UnreadMessages(string afzender, DateTime vanaf)
        {
            var msgs = GetMessagesFromAfzender(Chat, afzender, vanaf, true);
            return msgs.Count;
        }

        public List<ProductieChatEntry> GetAllUnreadMessages(UserChat chat)
        {
            return GetMessagesFromAfzender(chat, null, DateTime.Now.Subtract(TimeSpan.FromDays(30)), true);
        }

        public List<ProductieChatEntry> GetAllUnreadMessages()
        {
            return GetMessagesFromAfzender(Chat, null, DateTime.Now.Subtract(TimeSpan.FromDays(30)), true);
        }

        public List<ProductieChatEntry> GetAllMessages(UserChat chat)
        {
            return GetMessagesFromAfzender(chat, null, default, false);
        }

        public List<ProductieChatEntry> GetAllMessages()
        {
            return GetMessagesFromAfzender(Chat, null, default, false);
        }

        #region Events
        public event FileSystemEventHandler MessageChanged;
        public event FileSystemEventHandler MessageDeleted;
        public event FileSystemEventHandler PublicMessageChanged;
        public event FileSystemEventHandler PublicMessageDeleted;
        public event FileSystemEventHandler GebruikerChanged;
        public event FileSystemEventHandler GebruikerDeleted;

        protected virtual void OnMessageChanged(object sender, FileSystemEventArgs e)
        {
            MessageChanged?.Invoke(sender, e);
        }

        protected virtual void OnMessageDeleted(object sender, FileSystemEventArgs e)
        {
            MessageDeleted?.Invoke(sender, e);
        }

        protected virtual void OnPublicMessageChanged(object sender, FileSystemEventArgs e)
        {
            PublicMessageChanged?.Invoke(sender, e);
        }

        protected virtual void OnPublicMessageDeleted(object sender, FileSystemEventArgs e)
        {
            PublicMessageDeleted?.Invoke(sender, e);
        }

        protected virtual void OnGebruikerChanged(object sender, FileSystemEventArgs e)
        {
            GebruikerChanged?.Invoke(sender, e);
        }

        protected virtual void OnGebruikerDeleted(object sender, FileSystemEventArgs e)
        {
            GebruikerDeleted?.Invoke(sender, e);
        }
        #endregion Events
        public bool IsDisposed { get; set; }
        public void Dispose()
        {
            LogOut();
            IsDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
