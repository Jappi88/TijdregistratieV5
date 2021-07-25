using System;
using System.ServiceModel;

namespace Rpm.Connection.WCF
{
    [ServiceContract(CallbackContract = typeof(IServiceCallback), SessionMode = SessionMode.Required)]
    public interface IService: IDisposable
    {
        [OperationContract(IsOneWay = true)]
        void SendRespond(byte[] data);

        [OperationContract(IsOneWay = true)]
        void SendRequest(byte[] data);
    }
}
