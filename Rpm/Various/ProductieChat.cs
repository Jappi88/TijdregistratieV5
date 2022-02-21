﻿using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Timers;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;

namespace ProductieManager.Rpm.Various
{
    public class ProductieChat
    {
        public delegate void GebruikerUpdateHandler(UserChat user);

        public delegate void NewMessageHandler(ProductieChatEntry message);

        public static string ChatPath;
        public static string ProfielPath;
        public static string BerichtenPath;
        public static string PublicLobyPath;

        private static readonly object _locker = new();

        private readonly List<string> _changes = new();

        private readonly Timer _FileChangedNotifyTimer;
        private FileSystemWatcher _berichtenWatcher;
        private FileSystemWatcher _gebruikerWatcher;
        private FileSystemWatcher _PublicberichtenWatcher;

        public ProductieChat()
        {
            _FileChangedNotifyTimer = new Timer(1000); //500 ms vertraging
            _FileChangedNotifyTimer.Elapsed += _FileChangedNotifyTimer_Elapsed;
        }

        public static string GroupChatImagePath { get; set; }

        public static string GebruikerPath { get; set; }
        public static bool LoggedIn { get; private set; }
        public static UserChat Chat { get; private set; }

        public static bool RaiseNewMessageEvent { get; set; } = true;
        public static bool RaiseUserUpdateEvent { get; set; } = true;

        public static List<UserChat> Gebruikers { get; set; }

        private void _FileChangedNotifyTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _FileChangedNotifyTimer?.Stop();
            if (_changes == null) return;
            try
            {
                lock (_changes)
                {
                    for (var i = 0; i < _changes.Count; i++)
                        try
                        {
                            var x = _changes[i];
                            //if (x.ToLower().EndsWith("iedereen.rpm"))
                            //{
                            //    _changes.RemoveAt(i--);
                            //    continue;
                            //}
                            var dirname = Path.GetDirectoryName(x);
                            if (dirname != null && dirname.ToLower().EndsWith("berichten"))
                            {
                                var ent = x.DeSerialize<ProductieChatEntry>();
                                if (ent != null) OnMessageRecieved(ent);
                            }
                            else
                            {
                                var ent = x.DeSerialize<UserChat>();
                                if (ent != null)
                                {
                                    UpdateGebruikers();
                                    OnGebruikerUpdate(ent);
                                }
                            }

                            _changes.RemoveAt(i--);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public static string GetReadPath(bool checksecondary, string filename = null)
        {
            var remote = Manager.DbPath;
            var local = Manager.SecondaryAppRootPath;
            if (string.IsNullOrEmpty(local))
                local = AppDomain.CurrentDomain.BaseDirectory + "\\RPM_Data";
            else local = Path.Combine(local, "RPM_Data");
            var x = checksecondary && !string.IsNullOrEmpty(local)
                                   && (!string.IsNullOrEmpty(filename) && File.Exists(local + $"\\{filename}")
                                       || Directory.Exists(local))
                ? local
                : remote;
            return x;
        }

        public static string[] GetWritePaths(bool onlylocal)
        {
            var xreturn = new List<string>();
            var remote = Manager.DbPath;
            var local = Manager.SecondaryAppRootPath;
            if (string.IsNullOrEmpty(local))
                local = AppDomain.CurrentDomain.BaseDirectory + "\\RPM_Data";
            else local = Path.Combine(local, "RPM_Data");

            if (!string.IsNullOrEmpty(local) &&
                !string.Equals(local, remote, StringComparison.CurrentCultureIgnoreCase) &&
                Directory.Exists(local))
                xreturn.Add(local);

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
                BerichtenPath = Path.Combine(xpath, Manager.LogedInGebruiker.Username, "Berichten");
                PublicLobyPath = Path.Combine(xpath, "Iedereen", "Berichten");
                GroupChatImagePath = Path.Combine(xpath, "GroupChatImage.png");
                if (!File.Exists(GroupChatImagePath) || !GroupChatImagePath.IsImageFile())
                    try
                    {
                        var data = Resources.users_clients_group_16774.ToByteArray();
                        File.WriteAllBytes(GroupChatImagePath, data);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
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
                    Resources.avatardefault_92824.Save(Chat.ProfielImage, ImageFormat.Png);
                }

                Chat.Save();
                UpdateGebruikers();

                _PublicberichtenWatcher = new FileSystemWatcher(PublicLobyPath);
                _PublicberichtenWatcher.EnableRaisingEvents = true;
                _PublicberichtenWatcher.Changed += _berichtenWatcher_Changed;
                _PublicberichtenWatcher.Filter = "*.rpm";

                _berichtenWatcher = new FileSystemWatcher(BerichtenPath);
                _berichtenWatcher.EnableRaisingEvents = true;
                _berichtenWatcher.Changed += _berichtenWatcher_Changed;
                _berichtenWatcher.Filter = "*.rpm";

                _gebruikerWatcher = new FileSystemWatcher(ChatPath);
                _gebruikerWatcher.EnableRaisingEvents = true;
                _gebruikerWatcher.Filter = "*.rpm";
                _gebruikerWatcher.NotifyFilter = NotifyFilters.LastWrite;
                _gebruikerWatcher.Changed += _berichtenWatcher_Changed;
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

        public static bool SendMessage(string message, string destination)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrEmpty(destination) || Chat == null) return false;
            var ent = new ProductieChatEntry
            {
                Afzender = Chat,
                Bericht = message,
                Ontvanger = destination
            };
            return ent.UpdateMessage();
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
                var fi = new FileInfo(Chat.ProfielImage);
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
                var files = Directory.GetFiles(xpath, "*.rpm", SearchOption.TopDirectoryOnly);
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

        private void _berichtenWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            lock (_locker)
            {
                try
                {
                    if (Chat == null || !LoggedIn || !RaiseNewMessageEvent) return;
                    _FileChangedNotifyTimer?.Stop();
                    lock (_changes)
                    {
                        if (!_changes.Any(x => string.Equals(x, e.FullPath, StringComparison.CurrentCultureIgnoreCase)))
                            _changes.Add(e.FullPath);
                    }

                    _FileChangedNotifyTimer?.Start();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
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
                if (mymessages.Count > 0)
                    usermessages.AddRange(mymessages);
                xreturn = usermessages.OrderBy(x => x.Tijd).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xreturn;
        }

        public static event NewMessageHandler MessageRecieved;

        protected virtual void OnMessageRecieved(ProductieChatEntry message)
        {
            MessageRecieved?.Invoke(message);
        }

        public static event GebruikerUpdateHandler GebruikerUpdate;

        protected virtual void OnGebruikerUpdate(UserChat user)
        {
            GebruikerUpdate?.Invoke(user);
        }
    }
}