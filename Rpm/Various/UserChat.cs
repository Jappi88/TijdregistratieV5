using Polenter.Serialization;
using System;
using System.Drawing;

namespace ProductieManager.Rpm.Various
{
    public class UserChat
    {
        public int ID { get; set; }
        public DateTime LastOnline { get; set; }
        public bool IsOnline { get; set; }
        public string UserName { get; set; }
        public string ProfielImage { get; set; }
        [ExcludeFromSerialization]
        public Bitmap Avatar { get; set; }
        public UserChat()
        {
            ID = DateTime.Now.GetHashCode();
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
