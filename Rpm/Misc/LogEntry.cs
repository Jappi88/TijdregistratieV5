using System;
using Rpm.Productie;
using Rpm.Various;

namespace Rpm.Misc
{
    [Serializable]
    public class LogEntry
    {
        public LogEntry()
        {
            Added = DateTime.Now;
            Id = Added.GetHashCode();
        }

        public LogEntry(string message, MsgType type) : this()
        {
            Type = type;
            Message = message;
            MachineId = Manager.SystemId;
            Username = Manager.Opties != null ? Manager.Opties.Username : MachineId;
        }

        public int Id { get; private set; }
        public DateTime Added { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
        public string MachineId { get; set; }
        public MsgType Type { get; set; }
    }
}