﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using LiteDB;
using Polenter.Serialization;
using Rpm.SqlLite;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Rpm.Settings
{
    [Serializable]
    public class UserAccount
    {
        public UserAccount()
        {
            OsID = CpuID.ProcessorId();
        }

        public UserChange LastChanged { get; set; }

        public string _pass { get; set; }

        [BsonIgnore]
        [ExcludeFromSerialization]
        public string Password
        {
            get => _pass;
            set => _pass = Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(value)));
        }

        public string Username { get; set; }
        public AccesType AccesLevel { get; set; }
        [ExcludeFromSerialization]
        public bool AutoLogIn { get; set; }
        public string OsID { get; set; }

        public bool ValidateUser(string username, string password)
        {
            if (!String.Equals(Username, username, StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("Ongeldige Gebruikers Name!");
            if (Password != Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(password))))
                throw new Exception("Ongeldige Wachtwoord!");
            this.OsID = Manager.SystemID;
            Manager.Database.UpSert(this, $"{username} Ingelogd!");
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