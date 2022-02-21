using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Rpm.Connection
{
    internal class SmtpConnectorWithoutSsl : SmtpConnectorBase
    {
        private Socket _socket;

        public SmtpConnectorWithoutSsl(string smtpServerAddress, int port) : base(smtpServerAddress, port)
        {
            var hostEntry = Dns.GetHostEntry(smtpServerAddress);
            var endPoint = new IPEndPoint(hostEntry.AddressList[0], port);
            _socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //try to connect and test the rsponse for code 220 = success
            _socket.Connect(endPoint);
        }

        ~SmtpConnectorWithoutSsl()
        {
            try
            {
                if (_socket != null)
                {
                    _socket.Close();
                    _socket.Dispose();
                    _socket = null;
                }
            }
            catch (Exception)
            {
                ;
            }
        }

        public override bool CheckResponse(int expectedCode)
        {
            while (_socket.Available == 0) Thread.Sleep(100);
            var responseArray = new byte[1024];
            _socket.Receive(responseArray, 0, _socket.Available, SocketFlags.None);
            var responseData = Encoding.UTF8.GetString(responseArray);
            var responseCode = Convert.ToInt32(responseData.Substring(0, 3));
            if (responseCode == expectedCode) return true;
            return false;
        }

        public override void SendData(string data)
        {
            var dataArray = Encoding.UTF8.GetBytes(data);
            _socket.Send(dataArray, 0, dataArray.Length, SocketFlags.None);
        }
    }
}