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
        public int ID { get; set; }
        public DateTime LastOnline { get; set; }
        public bool IsOnline { get; set; }
        public string UserName { get; set; }
        public string ProfielImage { get; set; }

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
