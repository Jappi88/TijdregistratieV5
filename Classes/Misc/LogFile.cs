using System;

namespace ProductieManager.Misc
{
    [Serializable]
    public class LogFile
    {
        public DateTime Added { get; set; }
        public string Message { get; set; }
        public bool isRead { get; set; }
        public MsgType Type { get; set; }

        public LogFile()
        {
            Added = DateTime.Now;
        }

        public LogFile(string message, MsgType type)
        {
            Added = DateTime.Now;
            Type = type;
            Message = message;
        }
    }
}