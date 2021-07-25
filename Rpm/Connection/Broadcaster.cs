using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Rpm.Various;

namespace Rpm.Connection
{
    public class Broadcaster
    {
        private const int PORT_NUMBER = 30000;

        private readonly UdpClient udp = new(new IPEndPoint(IPAddress.Any, PORT_NUMBER));
        private IAsyncResult ar_;
        public bool IsBroadcasting { get; private set; }
        public bool IsListening { get; private set; }


        public void Start()
        {
            if (!IsListening)
            {
                IsListening = true;
                Console.WriteLine("Started listening");
                StartListening();
            }
        }

        public void Stop()
        {
            try
            {
                if (IsListening)
                {
                    IsListening = false;
                    udp.Close();
                    Console.WriteLine("Stopped listening");
                }
            }
            catch
            {
                /* don't care */
            }
        }

        private void StartListening()
        {
            if (IsListening)
                ar_ = udp.BeginReceive(Receive, new object());
        }

        private void Receive(IAsyncResult ar)
        {
            var ip = new IPEndPoint(IPAddress.Any, PORT_NUMBER);
            var bytes = udp.EndReceive(ar, ref ip);
            var message = RespondMessage.FromArray(bytes);
            if (message != null)
            {
                message.Sender.MachineIp = ip.ToString();
                Console.WriteLine("From {0} received: {1} ", ip.Address, message);
                RespondRecieved(message);
            }

            StartListening();
        }

        public void Send(RespondMessage message)
        {
            var client = new UdpClient();
            var ip = new IPEndPoint(IPAddress.Broadcast, PORT_NUMBER);
            var bytes = message.ToArray();
            client.Send(bytes, bytes.Length, ip);
            client.Close();
            Console.WriteLine("Sent: {0} ", message);
        }

        public void Send(byte[] message)
        {
            var client = new UdpClient();
            var ip = new IPEndPoint(IPAddress.Broadcast, PORT_NUMBER);
            client.Send(message, message.Length, ip);
            client.Close();
            Console.WriteLine("Sent: {0} ", message);
        }

        public async void StartBroadCasting()
        {
            if (IsBroadcasting)
                return;
            await Task.Run(() =>
            {
                IsBroadcasting = true;
                var data =
                    new RespondMessage("Server Host", MsgType.Info, null, RespondType.Bericht).ToArray();
                while (IsBroadcasting)
                {
                    Send(data);
                    Thread.Sleep(5000);
                }

                data = null;
            });
        }

        public void StopBroadcast()
        {
            IsBroadcasting = false;
        }

        public event RespondMessageHandler OnRespondRecieved;

        protected virtual void RespondRecieved(RespondMessage message)
        {
            OnRespondRecieved?.Invoke(this, message);
        }
    }
}