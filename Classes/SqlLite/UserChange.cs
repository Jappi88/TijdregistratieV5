using ProductieManager.Productie;
using System;
using System.Collections.Generic;

namespace ProductieManager.Classes.SqlLite
{
    public class UserChange
    {
        public string User { get; set; }
        [LiteDB.BsonId]
        public string PcId { get; set; }
        public string Change { get; set; }
        public DateTime TimeChanged { get; set; }
        public Dictionary<int,DateTime> DbIds { get; set; }

        public UserChange()
        {
            TimeChanged = DateTime.Now;
            DbIds = new Dictionary<int, DateTime> { };
        }

        public Mailing.RemoteMessage CreateMessage()
        {
            return new Mailing.RemoteMessage($"Update Door [{User}]\n" +
                                             $"{Change}", MessageAction.AlgemeneMelding, MsgType.Info);
        }
    }
}