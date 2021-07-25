using System.ServiceModel;
using System.Threading.Tasks;

namespace Rpm.Connection
{
    [ServiceContract(CallbackContract = typeof(IClientCallback))]
    
    public interface IClient
    {
        [OperationContract(AsyncPattern = true)]
        Task<RespondMessage> SendRespond(RespondMessage respond, string to = null);

        [OperationContract(AsyncPattern = true)]
        Task<RespondMessage> SendRequest(RequestMessage request, string from = null);
    }
}
