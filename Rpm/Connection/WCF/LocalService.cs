using System;
using System.Net;
using System.Threading.Tasks;
using Rpm.Misc;
using Rpm.Various;

namespace Rpm.Connection.WCF
{
    public class LocalService
    {
        #region Variables

        public static readonly IPAddress SystemIp = Functions.GetSystemIP();

        public Service Host { get; private set; }
        public Client Proxy { get; private set; }

        #endregion

        public async void OpenConnection()
        {
            try
            {
                if (!WindowService.IsRunning("NetTcpPortSharing"))
                    await WindowService.StartService("NetTcpPortSharing", 1000);
                if (Proxy == null || !Proxy.Connected)
                {
                    Proxy = new Client();
                    await Proxy.Connect();
                }

                if (Host == null || !Host.ServiceStarted)
                {
                    Host = new Service();
                    await Host.OpenConnection();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public async Task<bool> LogOut()
        {
            return await Proxy.SendRespond(new RespondMessage("Logged Out", MsgType.Info, null,
                RespondType.LoggedOut));
        }
    }
}
