using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Timers;

namespace ProductieManager.Rpm.Various
{
    public class ProductieChat : IDisposable
    {
        public static string ChatPath;
        public static string ProfielPath;
        public static string BerichtenPath;
        public static string PublicLobyPath;
        public static string GroupChatImagePath { get; set; }

        public static string GebruikerPath { get; set; }
        private CustomFileWatcher _PublicberichtenWatcher;
        private CustomFileWatcher _berichtenWatcher;
        private CustomFileWatcher _gebruikerWatcher;
        public static bool LoggedIn { get; private set; }
        public static UserChat Chat { get; private set; }

        public static bool RaiseNewMessageEvent { get; set; } = true;
        public static bool RaiseUserUpdateEvent { get; set; } = true;

        public static List<UserChat> Gebruikers { get; set; } = new List<UserChat>();

        private static readonly object _locker = new object();

        public static string GetReadPath(bool checksecondary, string filename = null)
        {
            string remote = Manager.DbPath;
            string local = Manager.SecondaryAppRootPath;
            if (string.IsNullOrEmpty(local))
                local = AppDomain.CurrentDomain.BaseDirectory + "\\RPM_Data";
            else local = Path.Combine(local, "RPM_Data");
            var x = checksecondary && !string.IsNullOrEmpty(local)
                    && (((!string.IsNullOrEmpty(filename) && File.Exists(local + $"\\{filename}"))
                         || Directory.Exists(local)))
                ? local
                : remote;
            return x;
        }

        public static string[] GetWritePaths(bool onlylocal)
        {
            var xreturn = new List<string>();
            string remote = Manager.DbPath;
            string local = Manager.SecondaryAppRootPath;
            if (string.IsNullOrEmpty(local))
                local = AppDomain.CurrentDomain.BaseDirectory + "\\RPM_Data";
            else local = Path.Combine(local, "RPM_Data");

            if (!string.IsNullOrEmpty(local) &&
                !string.Equals(local, remote, StringComparison.CurrentCultureIgnoreCase) &&
                Directory.Exists(local))
            {
                xreturn.Add(local);
            }

            if (!onlylocal)
                xreturn.Insert(0, remote);
            return xreturn.ToArray();
        }

        public bool Login()
        {
            try
            {
                if (Manager.LogedInGebruiker == null) return false;
                if (LoggedIn) return true;
                
                var xpath = Path.Combine(GetReadPath(true), "Chat");
                ChatPath = xpath;
                if (!Directory.Exists(xpath))
                    Directory.CreateDirectory(xpath);
                ProfielPath = Path.Combine(xpath, Manager.LogedInGebruiker.Username);
                BerichtenPath = Path.Combine(xpath,Manager.LogedInGebruiker.Username, "Berichten");
                PublicLobyPath = Path.Combine(xpath, "Iedereen", "Berichten");
                GroupChatImagePath = Path.Combine(xpath, "GroupChatImage.png");
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
                GebruikerPath = Path.Combine(xpath, $"{Manager.LogedInGebruiker.Username}.rpm");

                try
                {
                    if (File.Exists(GebruikerPath))
                        Chat = File.ReadAllBytes(GebruikerPath).DeSerialize<UserChat>() ?? new UserChat();
                    else Chat = new UserChat();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
               
                Chat.UserName = Manager.LogedInGebruiker.Username;
                Chat.IsOnline = true;
                Chat.LastOnline = DateTime.Now;
                if (string.IsNullOrEmpty(Chat.ProfielImage) || !File.Exists(Chat.ProfielImage))
                {
                    Chat.ProfielImage = $"{ProfielPath}\\ProfielFoto.png";
                    Properties.Resources.avatardefault_92824.Save(Chat.ProfielImage, ImageFormat.Png);
                }
                Chat.Save();
                UpdateGebruikers();
               
                _PublicberichtenWatcher = new CustomFileWatcher(PublicLobyPath, "*.rpm");
                _PublicberichtenWatcher.FileChanged += _PublicberichtenWatcher_FileChanged;

                _berichtenWatcher = new CustomFileWatcher(BerichtenPath, "*.rpm");
                _berichtenWatcher.FileChanged += _berichtenWatcher_Changed;

                _gebruikerWatcher = new CustomFileWatcher(ChatPath, "*.rpm");
                _gebruikerWatcher.FileChanged += _gebruikerWatcher_FileChanged;
                LoggedIn = true;
                OnGebruikerUpdate(Chat);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private void _PublicberichtenWatcher_FileChanged(object sender, FileSystemEventArgs e)
        {
            if (IsDisposed || Chat == null || !RaiseNewMessageEvent) return;
            try
            {
                var x = e.FullPath;
                var dirname = Path.GetDirectoryName(x);
                if (dirname != null && dirname.ToLower().EndsWith("berichten"))
                {
                    if (!RaiseNewMessageEvent) return;
                    var ent = x.DeSerialize<ProductieChatEntry>();
                    if (ent != null)
                    {
                        UpdateFilePath(e);
                        OnMessageRecieved(ent);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void _berichtenWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (IsDisposed || Chat == null) return;
            try
            {
                if (Chat == null || !LoggedIn) return;
                try
                {
                    var x = e.FullPath;
                    var dirname = Path.GetDirectoryName(x);
                    if (dirname != null && dirname.ToLower().EndsWith("berichten"))
                    {
                        if (!RaiseNewMessageEvent) return;
                        var ent = x.DeSerialize<ProductieChatEntry>();
                        if (ent != null)
                        {
                            UpdateFilePath(e);
                            OnMessageRecieved(ent);
                        }
                    }
                    else
                    {
                        var ent = x.DeSerialize<UserChat>();
                        if (ent != null)
                        {
                            UpdateFilePath(e);
                            UpdateGebruikers();
                            if (RaiseUserUpdateEvent)
                                OnGebruikerUpdate(ent);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void UpdateFilePath(FileSystemEventArgs e)
        {
            var xindex = e.FullPath.IndexOf("RPM_Data", StringComparison.CurrentCultureIgnoreCase);
            if (xindex == -1) return;
            xindex += 8;
            var xpart = e.FullPath.Substring(xindex, e.FullPath.Length - xindex).TrimStart('\\');
            var xread = Path.Combine(GetReadPath(true), xpart);
            if (!string.Equals(e.FullPath, xread, StringComparison.CurrentCultureIgnoreCase))
            {
                try
                {
                    File.Copy(e.FullPath, xread, true);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }

        private void _gebruikerWatcher_FileChanged(object sender, FileSystemEventArgs e)
        {
            if (IsDisposed || Chat == null) return;
            try
            {
                var ent = e.FullPath.DeSerialize<UserChat>();
                if (ent != null)
                {
                    UpdateFilePath(e);
                    UpdateGebruikers();
                    if (RaiseUserUpdateEvent)
                        OnGebruikerUpdate(ent);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
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
                    Chat.Save();
                    Chat = null;
                }

                Gebruikers?.Clear();
                _PublicberichtenWatcher?.Dispose();
                _PublicberichtenWatcher = null;
                _berichtenWatcher?.Dispose();
                _berichtenWatcher = null;
                _gebruikerWatcher?.Dispose();
                _gebruikerWatcher = null;
                LoggedIn = false;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static ProductieChatEntry SendMessage(string message, string destination)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrEmpty(destination) || Chat == null) return null;
            var ent = new ProductieChatEntry()
            {
                Afzender = Chat,
                Bericht = message,
                Ontvanger = destination
            };
            ent.UpdateMessage();
            return ent;
            //string[] users = destination.Split(';');
            //bool xreturn = false;
            //foreach (var user in users)
            //{
            //    var ent = new ProductieChatEntry()
            //    {
            //        Afzender = Chat,
            //        Bericht = message,
            //        Ontvanger = destination
            //    };
            //    //string path = ChatPath + $"\\{user}\\Berichten\\{ent.ID}.rpm";
            //    xreturn |= ent.UpdateMessage();
            //}

            //return xreturn;
        }

        public static bool ChangeProfielImage(string img)
        {
            if (Chat == null || string.IsNullOrEmpty(img)) return false;
            try
            {
                if (!img.IsImageFile()) return false;
                Chat.ProfielImage = ProfielPath + "\\ProfielFoto.png";
                File.Copy(img, Chat.ProfielImage, true);
                FileInfo fi = new FileInfo(Chat.ProfielImage);
                fi.LastWriteTime = DateTime.Now; 
                return Chat.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private bool UpdateGebruikers()
        {
            try
            {
                Gebruikers = new List<UserChat>();
                var xpath = Path.Combine(GetReadPath(true), "Chat");
                if (Chat == null || !Directory.Exists(xpath)) return false;
                string[] files = Directory.GetFiles(xpath, "*.rpm", SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    var ent = file.DeSerialize<UserChat>();
                    if (Chat == null) break;
                    if (ent == null || string.Equals(Chat.UserName, ent.UserName,
                        StringComparison.CurrentCultureIgnoreCase)) continue;
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


        public static List<ProductieChatEntry> GetConversation(UserChat user, bool privatemessages)
        {
            var xreturn = new List<ProductieChatEntry>();
            try
            {
                if (user == null) return xreturn;

                var usermessages = string.Equals(user.UserName, "iedereen", StringComparison.CurrentCultureIgnoreCase)
                    ? new List<ProductieChatEntry>()
                    : user.GetMessagesFromAfzender(privatemessages ? Chat.UserName : null);
                var mymessages = Chat.GetMessagesFromAfzender(privatemessages ? user.UserName : null);
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

        public delegate void NewMessageHandler(ProductieChatEntry message);

        public static event NewMessageHandler MessageRecieved;

        protected virtual void OnMessageRecieved(ProductieChatEntry message)
        {
            MessageRecieved?.Invoke(message);
        }

        public delegate void GebruikerUpdateHandler(UserChat user);

        public static event GebruikerUpdateHandler GebruikerUpdate;

        protected virtual void OnGebruikerUpdate(UserChat user)
        {
            GebruikerUpdate?.Invoke(user);
        }

        public bool IsDisposed { get; set; }
        public void Dispose()
        {
            LogOut();
            IsDisposed = true;
            _PublicberichtenWatcher?.Dispose();
            _berichtenWatcher?.Dispose();
            _gebruikerWatcher?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
