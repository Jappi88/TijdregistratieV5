using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.SqlLite;
using Rpm.Various;

namespace Rpm.Connection.WCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class Service : IService
    {
        public static List<ClientInfo> Clients { get; private set; } = new List<ClientInfo> { };

        public IServiceCallback Respons { get; private set; }
        public ServiceHost Host { get; private set; }
        public bool ServiceStarted { get; private set; }
        public readonly string ServiceAddress = "net.tcp://localhost:4345/Service";
        public readonly Binding ServiceBinding = new NetTcpBinding(SecurityMode.None)
        {
            PortSharingEnabled = true,
            MaxReceivedMessageSize = 64 * 1024 * 1024,
            
            //TransferMode = TransferMode.Buffered,
            //MaxConnections = 999
        };


        public Service()
        {
            Respons = OperationContext.Current?.GetCallbackChannel<IServiceCallback>();
        }

        public async Task<bool> OpenConnection()
        {
            return await Task<bool>.Factory.StartNew(() =>
            {


                if (Host != null && Host.State == CommunicationState.Opened)
                {
                    ServiceStarted = true;
                    return true;
                }
                try
                {
                    Host = new ServiceHost(typeof(Service), new Uri(ServiceAddress));
                    Host.Closed += Host_Closed;
                    Host.Faulted += Host_Closed;
                    Host.Opened += Host_Opened;
                    // Add service endpoint
                    Host.AddServiceEndpoint(typeof(IService), ServiceBinding, String.Empty);
                    Host.Open();
                    return true;
                }
                catch (Exception e)
                {
                    Host?.Close();
                    Console.WriteLine(e);
                    return false;
                }
            }, TaskCreationOptions.AttachedToParent);
        }

        private void Host_Opened(object sender, EventArgs e)
        {
            ServiceStarted = true;
        }

        private void Host_Closed(object sender, EventArgs e)
        {
            ServiceStarted = false;
        }

        public async void SendRespondToClients(RespondMessage respond)
        {
            if (Clients != null && Clients.Count > 0)
            {
                await Task<int>.Factory.StartNew(() =>
                {
                    int xreturn = 0;
                    for (int i = 0; i < Clients.Count; i++)
                    {
                        try
                        {
                            Clients[i].CallBack.OnRequest(respond.ToArray().Serialize().Compress());
                            xreturn++;
                        }
                        catch (Exception e)
                        {
                            Clients.RemoveAt(i--);
                        }
                    }

                    return xreturn;
                });
            }
        }

        public async void SendRequestToClients(RequestMessage request)
        {
            if (Clients != null && Clients.Count > 0)
            {
                await Task<int>.Factory.StartNew(() =>
                {
                    int xreturn = 0;
                    for (int i = 0; i < Clients.Count; i++)
                    {
                        try
                        {
                            Clients[i].CallBack.OnRequest(request.ToArray().Serialize().Compress());
                            xreturn++;
                        }
                        catch (Exception e)
                        {
                            Clients.RemoveAt(i--);
                        }
                    }

                    return xreturn;
                });
            }
        }

        public async void SendRespond(byte[] data)
        {
            if (OperationContext.Current != null && data != null && data.Length > 0)
            {
                try
                {
                    var respond = data.DeCompress().DeSerialize<RespondMessage>();
                    //if (respond == null || respond.Sender.MachineIp == LocalService.SystemIp.ToString())
                      //  return;
                    respond.Sender.CallBack = OperationContext.Current.GetCallbackChannel<IServiceCallback>();
                    var x = await Task<RespondMessage>.Factory.StartNew(() => ProcessRespondMessage(respond));
                    if (x != null)
                        respond.Sender.CallBack.OnRespond(x.ToArray());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public async void SendRequest(byte[] data)
        {
            if (OperationContext.Current != null && data != null && data.Length > 0)
            {
                try
                {
                    var request = data.DeCompress().DeSerialize<RequestMessage>();
                    //if (request == null || request.Sender.MachineIp == LocalService.SystemIp.ToString())
                       // return;
                    request.Sender.CallBack = OperationContext.Current.GetCallbackChannel<IServiceCallback>();
                    var x = await Task<RespondMessage>.Factory.StartNew(() => ProcessRequestMessage(request));
                    if(x != null)
                        request.Sender.CallBack.OnRespond(x.ToArray());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public RespondMessage ProcessRespondMessage(RespondMessage msg)
        {
            if (msg != null)
            {
                RespondRecieved(this, msg);
                switch (msg.Type)
                {
                    case RespondType.LoggedIn:
                        if (msg.MessageType == MsgType.Info)
                        {
                            SubscribeClient(msg.Sender);
                            //ConnectedClients.Add(msg.Sender);
                            ClientLoggedIn(this, msg.Sender);
                            RespondMessage res = new RespondMessage($"Welkom {msg.Sender.Name}", MsgType.Success, null,
                                RespondType.LoggedIn);
                            return res;
                        }
                        break;
                    case RespondType.LoggedOut:
                        UnsubscribeClient(msg.Sender);
                        //ConnectedClients.RemoveAll(x => Equals(x.MachineIp, msg.Sender.MachineIp));
                        ClientLoggedOut(this, msg.Sender);
                        break;
                    case RespondType.Add:
                    case RespondType.Replace:
                    case RespondType.Update:
                        if (msg.Values != null)
                        {
                            int count = Manager.Database.UpdateDbEntries(msg.Values, false);

                            if (count > 0)
                            {
                                Manager.Database.UpdateChange(msg);
                                RespondMessage xmsg = new RespondMessage($"{count} Entries zijn geupdated.",
                                    MsgType.Success, null, RespondType.Update);
                                return xmsg;
                            }
                        }
                        break;
                    case RespondType.Delete:
                        break;
                    case RespondType.Bericht:
                        break;
                    case RespondType.None:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return null;
        }

        public RespondMessage ProcessRequestMessage(RequestMessage rmessage)
        {
            if (rmessage?.Sender?.CallBack == null)
                return null;
            RequestRecieved(this, rmessage);
            UserChange change = Manager.Database.ChangeLog.FindById(rmessage.Sender.MachinId) ?? new UserChange();
            change.PcId = rmessage.Sender.MachinId;
            change.User = rmessage.Sender.Name;
            change.Change = "Last time request";
            DateTime lastrequest = DateTime.MinValue;
            if (change.DbIds.ContainsKey(rmessage.DbName))
                lastrequest = change.DbIds[rmessage.DbName];
            var xreturn = Manager.Database.GetChangedDbEntries(lastrequest, rmessage.DbName);
            if (xreturn != null)
            {
                var res = new RespondMessage("Hierbij je verzoek om de laatste db entries", MsgType.Info,
                    xreturn, RespondType.Update, rmessage.DbName);
                return res;
            }

            return null;
        }

        public bool SubscribeClient(ClientInfo info)
        {
            try
            {
                if (info?.CallBack != null && Clients.All(x => x.MachineIp != info.MachineIp))
                    Clients.Add(info);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool UnsubscribeClient(ClientInfo info)
        {
            try
            {
                return Clients?.RemoveAll(x => x.MachineIp == info.MachineIp) > 0;
            }
            catch
            {
                return false;
            }
        }

        #region Events

        public event RespondMessageHandler OnRespondRecieved;
        public event RequestMessageHandler OnRequestRecieved;
        public event ClientHandler OnClientLoggedIn;
        public event ClientHandler OnClientLoggedOut;

        public void RespondRecieved(object sender, RespondMessage message)
        {
            OnRespondRecieved?.Invoke(sender, message);
        }

        public void RequestRecieved(object sender, RequestMessage message)
        {
            OnRequestRecieved?.Invoke(sender, message);
        }

        public void ClientLoggedIn(object sender, ClientInfo client)
        {
            OnClientLoggedIn?.Invoke(sender, client);
        }

        public void ClientLoggedOut(object sender, ClientInfo client)
        {
            OnClientLoggedOut?.Invoke(sender, client);
        }

        #endregion Events

        public void Dispose()
        {
            ((IDisposable) Host)?.Dispose();
        }
    }
}
