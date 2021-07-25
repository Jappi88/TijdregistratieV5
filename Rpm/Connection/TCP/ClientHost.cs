using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using Rpm.Various;

namespace Rpm.Connection.TCP
{
    public class ClientHost
    {
        #region Public Delegates
        public delegate void HostMessagesDelegate(string message);
        #endregion

        #region Variables
        private readonly Client Client;
        private readonly Thread ClientThread;

        #region Callbacks
        protected readonly HostMessagesDelegate OnHostMessages; //the connection handler logic will be performed by the consumer of this class
        #endregion
        #endregion

        #region Constructor
        public ClientHost(HostMessagesDelegate onHostMessages)
        {
            this.OnHostMessages = onHostMessages ?? throw new ArgumentNullException(nameof(onHostMessages));
            this.Client = new Client(this.OnMessage, this.ConnectionHandler); //Uses default host and port and timeouts
            this.ClientThread = new Thread(this.Client.Run);
        }
        #endregion

        #region Public Functions
        public virtual void RunClientThread()
        {
            this.ClientThread.Start();
            this.OnHostMessages.Invoke("Client started");
        }

        public virtual void WaitForClientThreadToStop()
        {
            this.Client.ExitSignal = true; //Signal that the client connection-loop should stop gracefully
            this.OnHostMessages.Invoke("Exit Signal sent to Client thread");
            this.OnHostMessages.Invoke("Joining Client thread");
            this.ClientThread.Join();
            this.OnHostMessages.Invoke("Client thread has exited gracefully");
        }
        #endregion

        #region Protected Functions
        protected virtual void ConnectionHandler(NetworkStream connectedAutoDisposedNetStream)
        {
            if (!connectedAutoDisposedNetStream.CanRead && !connectedAutoDisposedNetStream.CanWrite)
                return; //We need to be able to read and write

            var writer = new StreamWriter(connectedAutoDisposedNetStream) { AutoFlush = true };
            var reader = new StreamReader(connectedAutoDisposedNetStream);

            var StartTime = DateTime.Now;
            int i = 0;
            while (!this.Client.ExitSignal) //Tight network message-loop (optional)
            {
                try
                {
                    var send = new RespondMessage("Halloooo Server!", MsgType.Info, null, RespondType.None).ToArray();
                    //Synchronously send some JSON to the connected client
                    connectedAutoDisposedNetStream.Write(send, 0, send.Length);
                    connectedAutoDisposedNetStream.Flush();

                    //Synchronously wait for a response from the connected client
                    byte[] buffer = new byte[1024];
                    int read = 0;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        while ((read = connectedAutoDisposedNetStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }

                        buffer = ms.ToArray();
                    }

                    if (buffer.Length > 0)
                    {
                        RespondMessage rsmg = RespondMessage.FromArray(buffer);
                        if (rsmg != null)
                        {
                            Console.WriteLine(rsmg.Message);
                        }
                    }
                }
                catch (IOException ex)
                {
                    _ = ex;
                    return; //Abort on network error
                }
                i++;

                i++;
                var ElapsedTime = DateTime.Now - StartTime;
                if (ElapsedTime.TotalMilliseconds >= 1000)
                {
                    this.OnHostMessages.Invoke("Thread: " + Thread.CurrentThread.ManagedThreadId.ToString() + " Messages per second: " + i);
                    i = 0;
                    StartTime = DateTime.Now;
                }
            }
        }

        protected virtual void OnMessage(string message)
        {
            this.OnHostMessages.Invoke(message);
        }
        #endregion 
    }
}