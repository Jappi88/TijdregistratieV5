using System.ServiceModel;

namespace Rpm.Connection.WCF
{
    [ServiceContract]
    public interface IServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnRespond(byte[] data);
        [OperationContract(IsOneWay = true)]
        void OnRequest(byte[] data);
    }
}
