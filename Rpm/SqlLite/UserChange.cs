using System;
using System.Collections.Generic;
using Polenter.Serialization;
using Rpm.Mailing;
using Rpm.Misc;
using Rpm.Various;

namespace Rpm.SqlLite
{
    public class UserChange
    {
         [ExcludeFromSerialization]
        public int ID { get; set; }
        public int Id => (TimeChanged.GetHashCode() ^ Change?.GetHashCode() ?? 0);
        [ExcludeFromSerialization]
        public string Reference { get; set; }

        public UserChange()
        {
            TimeChanged = DateTime.Now;
            DbIds = new Dictionary<DbType, DateTime>();
        }

        public UserChange(string change, DbType dbName) : this()
        {
            Change = change;
            DbIds[dbName] = DateTime.Now;
        }

        public List<string> ReadBy { get; set; } = new List<string>();

        public string User { get; set; }

        public string PcId { get; set; }

        public string Change { get; set; }

        public DateTime TimeChanged { get; set; }

        public Dictionary<DbType, DateTime> DbIds { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime ServerUpdated { get; set; }

        public RemoteMessage CreateMessage(DbType type)
        {
            RemoteMessage xreturn = new RemoteMessage();
            xreturn.MessageType = MsgType.Info;
            switch (type)
            {
                case DbType.Producties:
                    xreturn.Title = $"[{User}] Productie Update";
                    break;
                case DbType.Medewerkers:
                    xreturn.Title = $"[{User}] Gebruiker Update";
                    xreturn.MessageType = MsgType.Gebruiker;
                    break;
                case DbType.GereedProducties:
                    xreturn.Title = $"[{User}] Gereed Productie Update";
                    xreturn.MessageType = MsgType.Success;
                    break;
                case DbType.Opties:
                    xreturn.Title = $"[{User}] Instellingen Update";
                    break;
                case DbType.Accounts:
                    xreturn.Title = $"[{User}] Account Update";
                    break;
                case DbType.Logs:
                    xreturn.Title = $"[{User}] Log Update";
                    break;
                case DbType.Versions:
                    xreturn.Title = $"[{User}] Versie Update";
                    break;
                case DbType.Messages:
                    xreturn.Title = $"[{User}] Bericht Update";
                    xreturn.MessageType = MsgType.Gebruiker;
                    break;
                case DbType.Alles:
                    xreturn.Title = $"[{User}] Algemene Update";
                    break;
                case DbType.Geen:
                    xreturn.Title = $"[{User}] Update";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            xreturn.Action = MessageAction.AlgemeneMelding;
            xreturn.Message = Change;
            return xreturn;
        }

        public override bool Equals(object obj)
        {
            if (obj is UserChange change)
                return Id == change.Id;
            return false;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}