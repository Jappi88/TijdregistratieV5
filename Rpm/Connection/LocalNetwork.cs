using System;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using SimpleTCP;

namespace Rpm.Connection
{
    public class LocalNetwork
    {
        public const int Port = 15000;

        public LocalNetwork()
        {
            //Client = new ClientHost(WriteClientMessage);
            //Server = new ServerHost(WriteServerMessage);
            BroadCaster = new Broadcaster();
            BroadCaster.OnRespondRecieved += BroadCaster_onRespondRecieved;
            //BroadCaster.Start();
        }

        public SimpleTcpClient Client { get; private set; }
        public SimpleTcpServer Server { get; private set; }
        public Broadcaster BroadCaster { get; }

        private void BroadCaster_onRespondRecieved(object sender, RespondMessage message)
        {
            try
            {
                Console.WriteLine($"[CLIENT]:{message.Message}");
                Client = new SimpleTcpClient();
                Client.Delimiter = 0x13;
                var values = message.Sender.MachineIp.Split(':');
                var adress = values[0];
                var port = Port;
                if (values.Length > 1)
                    port = int.Parse(values[1]);
                Client.Connect(adress, Port);
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
            //using (var server = new ResponseSocket("@tcp://localhost:5556")) // bind
            //using (var client = new RequestSocket(">tcp://localhost:5556")) // connect
            //{
            //    // Send a message from the client socket
            //    client.SendFrame("Hello");
            //    // client.SendFrame()
            //    // Receive the message from the server socket
            //    var m1 = server.ReceiveFrameString();
            //    Console.WriteLine("From Client: {0}", m1);

            //    // Send a response back from the server
            //    server.SendFrame("Hi Back");

            //    // Receive the response from the client socket
            //    var m2 = client.ReceiveFrameString();
            //    Console.WriteLine("From Server: {0}", m2);
            //}

            //await Task.Run(() =>
            //{
            //    try
            //    {

            //        if (Port.IsAvailiblePort())
            //        {
            //            SimpleTCP.SimpleTcpServer server = new SimpleTcpServer();
            //            server.Start(Port, AddressFamily.InterNetwork);
            //            server.Delimiter = 0x13;
            //            server.ClientConnected += Server_ClientConnected;
            //            server.DataReceived += Server_DataReceived;
            //            BroadCaster.StartBroadCasting();
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e);
            //    }
            //});
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
            var serverChannel = new TcpChannel(9090);

            if (!ChannelServices.RegisteredChannels.Any(x => x.Equals(serverChannel)))
                // Register the server channel.
                ChannelServices.RegisterChannel(serverChannel, false);

            // Show the name of the channel.
            Console.WriteLine($"The name of the channel is {serverChannel.ChannelName}.");
            // Show the priority of the channel.
            Console.WriteLine($"The priority of the channel is {serverChannel.ChannelPriority}.");

            // Show the URIs associated with the channel.
            var data = (ChannelDataStore) serverChannel.ChannelData;
            foreach (var uri in data.ChannelUris) Console.WriteLine("The channel URI is {0}.", uri);

            // Expose an object for remote calls.
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(RespondMessage), "RpmService",
                WellKnownObjectMode.Singleton);

            // Parse the channel's URI.
            var urls = serverChannel.GetUrlsForUri("RpmService");
            string objectUri;
            if (urls.Length > 0)
            {
                var objectUrl = urls[0];
                var channelUri = serverChannel.Parse(objectUrl, out objectUri);
                Console.WriteLine("The object URL is {0}.", objectUrl);
                Console.WriteLine("The object URI is {0}.", objectUri);
                Console.WriteLine("The channel URI is {0}.", channelUri);
            }

            var clientChannel = new TcpChannel();
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
            var remoteType = new WellKnownClientTypeEntry(
                typeof(RespondMessage), "tcp://localhost:9090/RpmService");
            //if (!types.Any(x => x.ObjectUrl == remoteType.ObjectUrl) && !types2.Any(x => x.ObjectUri == remoteType.ObjectUrl))
            //RemotingConfiguration.RegisterWellKnownClientType(remoteType);
            // Create a message sink.
            var messageSink =
                clientChannel.CreateMessageSink(
                    "tcp://localhost:9090/RpmService", null,
                    out objectUri);
            Console.WriteLine("The URI of the message sink is {0}.",
                objectUri);
            if (messageSink != null)
                Console.WriteLine("The type of the message sink is {0}.",
                    messageSink.GetType());

            // Create an instance of the remote object.
            var service = new RespondMessage();
            var msg = messageSink.NextSink;
            // Invoke a method on the remote object.
            Console.WriteLine("The client is invoking the remote object.");
        }

        private void Server_ClientConnected(object sender, TcpClient e)
        {
            Console.WriteLine("Connected");
        }

        private void Server_DataReceived(object sender, Message e)
        {
            Console.WriteLine(e.MessageString);
        }
    }
}