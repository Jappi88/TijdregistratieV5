using System;
using Rpm.Productie;
using Rpm.Various;

namespace Rpm.Mailing
{
    public class RemoteMessage
    {
        public RemoteMessage(string message, MessageAction type, MsgType msgtype, byte[] content = null,
            object value = null, string prodnr = null) : 
            this(message,$"{Enum.GetName(typeof(MsgType),msgtype)} {Manager.Opties.Username}",type,msgtype,content,value,prodnr)
        {
        }

        public RemoteMessage(string message, string title, MessageAction type, MsgType msgtype, byte[] content = null,
            object value = null, string prodnr = null)
        {
            Action = type;
            Content = content;
            Message = message;
            Value = value;
            ProductieNummer = prodnr;
            MessageType = msgtype;
            Title = title;
        }

        public RemoteMessage()
        {
        }

        public MessageAction Action { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public object Value { get; set; }
        public string ProductieNummer { get; set; }
        public MsgType MessageType { get; set; }
        public byte[] Content { get; set; }
    }
}