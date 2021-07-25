using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Rpm.Connection.TCP
{
    public class ServerHost
    {
        #region Public Delegates
        public delegate void HostMessagesDelegate(string message);
        #endregion

        #region Variables
        protected readonly Server Server;
        protected readonly Thread ServerThread;

        #region Callbacks
        protected readonly HostMessagesDelegate OnHostMessages; //the connection handler logic will be performed by the consumer of this class
        #endregion
        #endregion

        #region Constructor
        public ServerHost(HostMessagesDelegate onHostMessages)
        {
            this.OnHostMessages = onHostMessages ?? throw new ArgumentNullException(nameof(onHostMessages));
            this.Server = new Server(this.OnMessage, this.ConnectionHandler,$"0.0.0.0/{Manager.InstanceID}"); //Uses default host and port and timeouts
            this.ServerThread = new Thread(this.Server.Run);
        }
        #endregion

        #region Public Functions
        public virtual void RunServerThread()
        {
            this.ServerThread.Start();
            this.OnHostMessages.Invoke("Server started");
        }

        public virtual void WaitForServerThreadToStop()
        {
            this.Server.ExitSignal = true; //Signal that the server connection-loop should stop gracefully
            this.OnHostMessages.Invoke("Exit Signal sent to server thread");
            this.OnHostMessages.Invoke("Joining server thread");
            this.ServerThread.Join();
            this.OnHostMessages.Invoke("Server thread has exited gracefully");
        }
        #endregion

        #region Protected Functions

        //Handles the client connections
        protected virtual void ConnectionHandler(NetworkStream connectedAutoDisposedNetStream)
        {
            if (!connectedAutoDisposedNetStream.CanRead && !connectedAutoDisposedNetStream.CanWrite)
                return; //We need to be able to read and write

            //var writer = new MemoryStream(connectedAutoDisposedNetStream) { AutoFlush = true };
            //var reader = new StreamReader(connectedAutoDisposedNetStream);

            var StartTime = DateTime.Now;
            int i = 0;
            while (!this.Server.ExitSignal) //Tight network message-loop (optional)
            {
                //Synchronously send some JSON to the connected client

                try
                {
                    var send = new RespondMessage("Halloooo Client!", MsgType.Info, new ValueContainer(Manager.Database.GetAllProducties(true)), RespondType.None).ToArray();
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