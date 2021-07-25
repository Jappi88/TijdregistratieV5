using LiteDB;
using ProductieManager.Misc;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace ProductieManager.Settings
{
    [Serializable]
    public class UserAccount
    {
        public Classes.SqlLite.UserChange LastChanged { get; set; }

        public string _pass { get; set; }

        [BsonIgnore]
        public string Password { get { return _pass; } set { _pass = Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(value))); } }

        public string Username { get; set; }
        public AccesType AccesLevel { get; set; }
        public bool AutoLogIn { get; set; }
        public string OsID { get; set; }

        public UserAccount()
        {
            OsID = CpuID.ProcessorId();
        }

        public bool ValidateUser(string username, string password)
        {
            if (Username.ToLower() != username.ToLower())
                throw new Exception("Ongeldige Gebruikers Name!");
            if (Password != Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(password))))
                throw new Exception("Ongeldige Wachtwoord!");
            return true;
        }

        public byte[] ToArray()
        {
            byte[] data = null;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
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
                using (MemoryStream ms = new MemoryStream(data))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    ua = bf.Deserialize(ms) as UserAccount;
                }
            }
            catch { }
            return ua;
        }
    }
}