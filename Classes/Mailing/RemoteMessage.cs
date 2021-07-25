namespace ProductieManager.Mailing
{
    public class RemoteMessage
    {
        public MessageAction Action { get; set; }
        public string Message { get; set; }
        public object Value { get; set; }
        public string ProductieNummer { get; set; }
        public MsgType MessageType { get; set; }
        public byte[] Content { get; set; }

        public RemoteMessage(string message, MessageAction type, MsgType msgtype, byte[] content = null, object value = null, string prodnr = null)
        {
            Action = type;
            Content = content;
            Message = message;
            Value = value;
            ProductieNummer = prodnr;
            MessageType = msgtype;
        }

        public RemoteMessage()
        {
        }
    }
}