using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;

namespace ProductieManager.Rpm.Various
{
    public class UserChat
    {
        public UserChat()
        {
            ID = DateTime.Now.GetHashCode();
        }

        public int ID { get; set; }
        public DateTime LastOnline { get; set; }
        public bool IsOnline { get; set; }
        public string UserName { get; set; }
        public string ProfielImage { get; set; }

        public List<ProductieChatEntry> GetMessagesFromAfzender(string afzender, bool onlyunread = false)
        {
            var xreturn = new List<ProductieChatEntry>();
            if (string.IsNullOrEmpty(UserName)) return xreturn;
            var xname = string.Equals(afzender, "iedereen", StringComparison.CurrentCultureIgnoreCase)
                ? afzender
                : UserName;
            var path = Path.Combine(ProductieChat.GetReadPath(true), "Chat", xname, "Berichten");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var files = Directory.GetFiles(path, "*.rpm", SearchOption.TopDirectoryOnly).ToList();
            if (string.IsNullOrEmpty(afzender) && Directory.Exists(ProductieChat.PublicLobyPath))
            {
                var publicfiles =
                    Directory.GetFiles(ProductieChat.PublicLobyPath, "*.rpm", SearchOption.TopDirectoryOnly);
                if (publicfiles.Length > 0)
                    files.AddRange(publicfiles);
            }

            try
            {
                foreach (var file in files)
                {
                    var ent = file.DeSerialize<ProductieChatEntry>();
                    if (ent?.Afzender == null) continue;
                    if (!string.IsNullOrEmpty(afzender) &&
                        !string.Equals(afzender, "iedereen", StringComparison.CurrentCultureIgnoreCase))
                        if (!string.Equals(ent.Afzender.UserName, afzender,
                                StringComparison.CurrentCultureIgnoreCase))
                            continue;

                    if (onlyunread && ent.IsGelezen)
                        continue;
                    if (onlyunread && string.Equals(ent.Afzender.UserName, UserName,
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

        public int UnreadMessages(string afzender)
        {
            var msgs = GetMessagesFromAfzender(afzender, true);
            return msgs.Count;
        }

        public List<ProductieChatEntry> GetAllUnreadMessages()
        {
            return GetMessagesFromAfzender(null, true);
        }

        public List<ProductieChatEntry> GetAllMessages()
        {
            return GetMessagesFromAfzender(null);
        }

        public bool Save()
        {
            try
            {
                if (string.IsNullOrEmpty(UserName)) return false;
                var paths = ProductieChat.GetWritePaths(false);
                if (paths.Length == 0) return false;
                var xfirst = paths[0];
                var xfile = Path.Combine(xfirst, "Chat", $"{UserName}.rpm");
                if (this.Serialize(xfile))
                {
                    for (var i = 1; i < paths.Length; i++)
                    {
                        var path2 = Path.Combine(paths[i], "Chat", $"{UserName}.rpm");
                        for (var j = 0; j < 5; j++)
                            try
                            {
                                File.Copy(xfile, path2, true);
                                break;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                    }

                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }


        public Bitmap GetProfielImage()
        {
            try
            {
                if (Manager.LogedInGebruiker == null) return null;
                if (File.Exists(ProfielImage))
                    return Image.FromStream(new MemoryStream(File.ReadAllBytes(ProfielImage))).ResizeImage(64, 64);
                var xfile = Path.Combine(ProductieChat.GetReadPath(true), "Chat", UserName, "ProfielFoto.png");
                if (!File.Exists(xfile))
                    return Resources.avatardefault_92824;
                return Image.FromStream(new MemoryStream(File.ReadAllBytes(xfile))).ResizeImage(64, 64);
            }
            catch (Exception exception)
            {
                return Resources.avatardefault_92824;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is UserChat chat)
                return string.Equals(chat.UserName, UserName);
            return false;
        }

        public override int GetHashCode()
        {
            return UserName?.GetHashCode() ?? ID;
        }
    }
}