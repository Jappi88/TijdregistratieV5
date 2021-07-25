using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using Rpm.Various;

namespace Rpm.Connection.WCF
{
    public class Client: IServiceCallback
    {
        //IServiceDuplexCallback callback = null; 
        public DuplexChannelFactory<IService> PipeFactory { get; private set; }
        public IService ClientProxy { get; private set; }
        public static List<ClientInfo> Services { get; private set; } = new List<ClientInfo> { };
        public bool Connected { get; private set; }
        public readonly string ServiceAddress = "net.tcp://localhost:4345/Service";
        public readonly Binding ServiceBinding = new NetTcpBinding(SecurityMode.None)
        {
            MaxReceivedMessageSize = 64 * 1024 * 1024,
            MaxBufferSize = 64 * 1024 * 1024,
            PortSharingEnabled = true,
            //TransferMode = TransferMode.Buffered,
            //MaxConnections = 999
        };

        public Client()
        {
            
        }

        public async Task<bool> Connect()
        {
            var valid = await OpenConnection();

            if (valid)
            {
                await SendRespond(new RespondMessage("Logged In", MsgType.Info, null,
                    RespondType.LoggedIn));
                await SendRequest(new RequestMessage(DatabaseName.Alles));
            }

            return valid;
        }

        public async Task<bool> OpenConnection()
        {
            return await Task.Factory.StartNew(() =>
            {
                try
                {
                    if (ClientProxy == null || PipeFactory == null || PipeFactory.State == CommunicationState.Faulted ||
                        PipeFactory.State == CommunicationState.Closed)
                    {

                        PipeFactory =
                            new DuplexChannelFactory<IService>(
                                new InstanceContext(this),
                                ServiceBinding,
                                ServiceAddress);

                        ClientProxy = PipeFactory.CreateChannel();
                    }

                    Connected = true;
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Connected = false;
                    PipeFactory = null;
                    ClientProxy = null;
                    return false;
                }
            });
        }

        public async void Disconnect()
        {
            await Task<bool>.Factory.StartNew(() =>
            {
                try
                {
                    ClientProxy.SendRespond(new RespondMessage("Logged Out", MsgType.Info,null, RespondType.LoggedOut).ToArray());
                    ClientProxy?.Dispose();
                    Connected = false;
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Connected = false;
                    PipeFactory = null;
                    ClientProxy = null;
                    return false;
                }
            });
        }

        public async void OnRespond(byte[] data)
        {
            var xc = OperationContext.Current?.GetCallbackChannel<IService>();
            await Task.Factory.StartNew(() =>
            {
                if (data != null && data.Length > 0)
                {
                    
                    var res = RespondMessage.FromArray(data);
                    res.Sender.Service = xc;
                    res.Sender.CallBack = this;
                    ProcessRespondMessage(res);
                }
            });
        }

        public async void OnRequest(byte[] data)
        {
            var xc = OperationContext.Current?.GetCallbackChannel<IService>();
            await Task.Factory.StartNew(() =>
            {
                if (data != null && data.Length > 0)
                {
                    
                    var res = RequestMessage.FromArray(data);
                    res.Sender.Service = xc;
                    res.Sender.CallBack = this;
                   // ProcessRequestMessage(res);
                }
            });
        }

        public async Task<bool> SendRespond(RespondMessage message)
        {

            bool valid = await OpenConnection();
            return valid && await Task.Factory.StartNew(() =>
            {
                try
                {
                    ClientProxy.SendRespond(message.ToArray());
                    return true;
                }
                catch (Exception e)
                {
                    Connected = false;
                    PipeFactory = null;
                    ClientProxy = null;
                    Console.WriteLine(e);
                    return false;
                }
            });
        }

        public async Task<bool> SendRequest(RequestMessage request)
        {

            bool valid = await OpenConnection();
            return valid && await Task.Factory.StartNew(() =>
            {
                try
                {
                    ClientProxy.SendRequest(request.ToArray());
                    return true;
                }
                catch (Exception e)
                {
                    Connected = false;
                    PipeFactory = null;
                    ClientProxy = null;
                    Console.WriteLine(e);
                    return false;
                }
            });
        }

        public bool SubscribeService(ClientInfo info)
        {
            try
            {
                if (info?.Service != null && Services.All(x => x.MachineIp != info.MachineIp))
                    Services.Add(info);
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
                return Services?.RemoveAll(x => x.MachineIp == info.MachineIp) > 0;
            }
            catch
            {
                return false;
            }
        }

        public void ProcessRespondMessage(RespondMessage msg)
        {
            if (msg != null)
            {
                //RespondRecieved(this, msg);
                switch (msg.Type)
                {
                    case RespondType.LoggedIn:
                        if (msg.MessageType == MsgType.Success)
                        {
                            SubscribeService(msg.Sender);
                        }
                        break;
                    case RespondType.LoggedOut:
                        UnsubscribeClient(msg.Sender);
                        break;
                    case RespondType.Add:
                    case RespondType.Replace:
                    case RespondType.Update:
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
        }

        //public RespondMessage ProcessRequestMessage(RequestMessage rmessage)
        //{
        //    if (rmessage?.Sender?.CallBack == null)
        //        return null;
        //    Service.RequestRecieved(this, rmessage);
        //    UserChange change = Manager.Database.ChangeLog.FindById(rmessage.Sender.MachinId) ?? new UserChange();
        //    change.PcId = rmessage.Sender.MachinId;
        //    change.User = rmessage.Sender.Name;
        //    change.Change = "Last time request";
        //    DateTime lastrequest = DateTime.MinValue;
        //    if (change.DbIds.ContainsKey(rmessage.DbName))
        //        lastrequest = change.DbIds[rmessage.DbName];
        //    var xreturn = Manager.Database.GetChangedDbEntries(lastrequest, rmessage.DbName);
        //    if (xreturn != null)
        //    {
        //        var res = new RespondMessage("Hierbij je verzoek om de laatste db entries", MsgType.Info,
        //            xreturn, RespondType.Update, rmessage.DbName);
        //        return res;
        //    }

        //    return null;
        //}
    }
}
