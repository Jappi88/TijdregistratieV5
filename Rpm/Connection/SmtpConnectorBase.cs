namespace Rpm.Connection
{
    internal abstract class SmtpConnectorBase
    {
        public const string EOF = "\r\n";

        protected SmtpConnectorBase(string smtpServerAddress, int port)
        {
            SmtpServerAddress = smtpServerAddress;
            Port = port;
        }

        protected string SmtpServerAddress { get; set; }
        protected int Port { get; set; }

        public abstract bool CheckResponse(int expectedCode);
        public abstract void SendData(string data);
    }
}