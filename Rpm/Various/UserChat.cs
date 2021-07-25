using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;

namespace ProductieManager.Rpm.Various
{
    public class UserChat
    {
        public int ID { get; set; }
        public DateTime LastOnline { get; set; }
        public bool IsOnline { get; set; }
        public string UserName { get; set; }
        public string ProfielImage { get; set; }

        public UserChat()
        {
            ID = DateTime.Now.GetHashCode();
        }

        public List<ProductieChatEntry> GetMessagesFromAfzender(string afzender, bool onlyunread = false)
        {
            var xreturn = new List<ProductieChatEntry>();
            if (string.IsNullOrEmpty(UserName)) return xreturn;
            string path = $"{ProductieChat.ChatPath}\\{UserName}\\Berichten";
            string[] files = Directory.GetFiles(path, "*.rpm", SearchOption.TopDirectoryOnly);
            try
            {
                foreach (var file in files)
                {
                    var ent = file.DeSerialize<ProductieChatEntry>();
                    if (ent?.Afzender == null) continue;
                    if (!string.IsNullOrEmpty(afzender) && !string.Equals(ent.Afzender.UserName, afzender,
                        StringComparison.CurrentCultureIgnoreCase)) continue;
                    if (onlyunread && ent.IsGelezen)
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

        public bool Save()
        {
            try
            {
                if (string.IsNullOrEmpty(UserName)) return false;
                return this.Serialize(ProductieChat.ChatPath+ $"\\{UserName}.rpm");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public Bitmap GetProfielImage()
        {
            if (string.IsNullOrEmpty(ProfielImage) || !File.Exists(ProfielImage))
                return null;
            return Image.FromStream(new MemoryStream(File.ReadAllBytes(ProfielImage))).ResizeImage(64,64);
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
