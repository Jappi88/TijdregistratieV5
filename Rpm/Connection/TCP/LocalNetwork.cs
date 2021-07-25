using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;
using Rpm.Misc;
using Rpm.Various;
using SimpleTCP;

namespace Rpm.Connection.TCP
{
    public class LocalNetwork
    {
        public const int Port = 15000;
        public SimpleTcpClient Client { get; private set; }
        public SimpleTcpServer Server { get; private set; }
        public Broadcaster BroadCaster { get; private set; }

        public LocalNetwork()
        {
            //Client = new ClientHost(WriteClientMessage);
            //Server = new ServerHost(WriteServerMessage);
            BroadCaster = new Broadcaster();
            BroadCaster.OnRespondRecieved += BroadCaster_onRespondRecieved;
            BroadCaster.Start();
        }

        private void BroadCaster_onRespondRecieved(object sender, RespondMessage message)
        {
            try
            {
                Console.WriteLine($"[CLIENT]:{message.Message}");
                Client = new SimpleTcpClient();
                Client.Connect(message.Sender.MachineIp, Port);
                Client.DataReceived += Client_DataReceived;
                Client.DelimiterDataReceived += Client_DelimiterDataReceived;
                BroadCaster.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        
        }

        public async void Start()
        {
            //Client.RunClientThread();
            //Server.RunServerThread();
            await Task.Run(() =>
            {
                try
                {
                    if (Port.IsAvailiblePort())
                    {
                        SimpleTCP.SimpleTcpServer server = new SimpleTcpServer();
                        server.Start(IPAddress.Any, Port);
                        server.Delimiter = 0x13;
                        server.ClientConnected += Server_ClientConnected;
                        server.DataReceived += Server_DataReceived;
                        BroadCaster.StartBroadCasting();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }

        private void Client_DelimiterDataReceived(object sender, Message e)
        {
            Console.WriteLine(e);
        }

        private void Client_DataReceived(object sender, Message e)
        {
            Console.WriteLine(e);
        }

        private void TestChanel()
        {
            TcpChannel serverChannel = new TcpChannel(9090);

            if (!ChannelServices.RegisteredChannels.Any(x => x.Equals(serverChannel)))
                // Register the server channel.
                ChannelServices.RegisterChannel(serverChannel, false);

            // Show the name of the channel.
            Console.WriteLine($"The name of the channel is {serverChannel.ChannelName}.");
            // Show the priority of the channel.
            Console.WriteLine($"The priority of the channel is { serverChannel.ChannelPriority}.");

            // Show the URIs associated with the channel.
            ChannelDataStore data = (ChannelDataStore)serverChannel.ChannelData;
            foreach (string uri in data.ChannelUris)
            {
                Console.WriteLine("The channel URI is {0}.", uri);
            }

            // Expose an object for remote calls.
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RespondMessage), "RpmService",
                WellKnownObjectMode.Singleton);

            // Parse the channel's URI.
            string[] urls = serverChannel.GetUrlsForUri("RpmService");
            string objectUri;
            if (urls.Length > 0)
            {
                string objectUrl = urls[0];
                string channelUri = serverChannel.Parse(objectUrl, out objectUri);
                Console.WriteLine("The object URL is {0}.", objectUrl);
                Console.WriteLine("The object URI is {0}.", objectUri);
                Console.WriteLine("The channel URI is {0}.", channelUri);
            }
            TcpChannel clientChannel = new TcpChannel();
            //int port = 9090;
            //var x = ChannelServices.RegisteredChannels;
            //if ()
            //{
            //    // Register the channel.
            //    ChannelServices.RegisterChannel(clientChannel, false);
            //}

            // Register as client for remote object.

            //var types = RemotingConfiguration.GetRegisteredWellKnownClientTypes();
            //var types2 = RemotingConfiguration.GetRegisteredWellKnownServiceTypes();
            WellKnownClientTypeEntry remoteType = new WellKnownClientTypeEntry(
                typeof(RespondMessage), "tcp://localhost:9090/RpmService");
            //if (!types.Any(x => x.ObjectUrl == remoteType.ObjectUrl) && !types2.Any(x => x.ObjectUri == remoteType.ObjectUrl))
            //RemotingConfiguration.RegisterWellKnownClientType(remoteType);
            // Create a message sink.
            System.Runtime.Remoting.Messaging.IMessageSink messageSink =
                clientChannel.CreateMessageSink(
                    "tcp://localhost:9090/RpmService", null,
                    out objectUri);
            Console.WriteLine("The URI of the message sink is {0}.",
                objectUri);
            if (messageSink != null)
            {
                Console.WriteLine("The type of the message sink is {0}.",
                    messageSink.GetType().ToString());
            }

            // Create an instance of the remote object.
            RespondMessage service = new RespondMessage();
            var msg = messageSink.NextSink;
            // Invoke a method on the remote object.
            Console.WriteLine("The client is invoking the remote object.");
        }

        private void Server_ClientConnected(object sender, System.Net.Sockets.TcpClient e)
        {
          Console.WriteLine("Connected");
        }

        private void Server_DataReceived(object sender, Message e)
        {
            Console.WriteLine(e.MessageString);
        }
    }
}
