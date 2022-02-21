﻿using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using Polenter.Serialization;
using Rpm.Mailing;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.SqlLite;
using Rpm.Various;

namespace Rpm.Settings
{
    [Serializable]
    public class UserAccount
    {
        private byte[] _rawmaildata;

        public UserAccount()
        {
            OsID = Guid.NewGuid().ToString().Split('-').LastOrDefault();
        }

        [ExcludeFromSerialization]
        public EmailHost MailingHost
        {
            get => RawMailData?.Decrypt(Password)?.DeSerialize<EmailHost>();
            set => RawMailData = value?.Serialize()?.Encrypt(Password);
        }

        public byte[] RawMailData
        {
            get => _rawmaildata;
            set => _rawmaildata = value;
        }

        public UserChange LastChanged { get; set; }

        public string _pass { get; set; }

        [ExcludeFromSerialization]
        public string Password
        {
            get => _pass;
            set
            {
                var host = MailingHost;
                _pass = Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(value)));
                if (host != null)
                    MailingHost = host;
            }
        }

        public string Username { get; set; }
        public AccesType AccesLevel { get; set; }

        [ExcludeFromSerialization] public bool AutoLogIn { get; set; }

        public string OsID { get; set; }

        public bool ValidateUser(string username, string password, bool save)
        {
            if (!string.Equals(Username, username, StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("Ongeldige Gebruikers Name!");
            if (Password != Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(password))))
                throw new Exception("Ongeldige Wachtwoord!");
            if (save)
            {
                OsID = Manager.SystemId;
                Manager.Database.UpSert(this, $"{username} Ingelogd!");
            }

            return true;
        }

        public byte[] ToArray()
        {
            byte[] data = null;
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, this);
                data = ms.ToArray();
            }

            return data;
        }

        public static UserAccount FromArray(byte[] data)
        {
            UserAccount ua = null;
            if (data == null || data.Length == 0)
                return null;
            try
            {
                using (var ms = new MemoryStream(data))
                {
                    var bf = new BinaryFormatter();
                    ua = bf.Deserialize(ms) as UserAccount;
                }
            }
            catch
            {
            }

            return ua;
        }
    }
}