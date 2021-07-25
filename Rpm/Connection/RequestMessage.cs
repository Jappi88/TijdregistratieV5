using System.IO;
using System.Runtime.Serialization;
using Rpm.SqlLite;
using Rpm.Misc;
using Rpm.Productie;

namespace Rpm.Connection
{
    [DataContract]
    public class RequestMessage
    {
        public RequestMessage(DbType dbname) : this()
        {
            DbName = dbname;
        }

        public RequestMessage()
        {
            Sender = new ClientInfo
            {
                Name = Manager.Opties == null ? Manager.SystemID : Manager.Opties.Username,
                MachineIp = Functions.GetSystemIP().ToString(), MachinId = Manager.SystemID
            };
        }

        [DataMember] public ClientInfo Sender { get; set; }

        [DataMember] public DbType DbName { get; set; }

        public byte[] ToArray()
        {
            return this.Serialize().Compress();
        }

        public static RequestMessage FromArray(byte[] data)
        {
            return data.DeCompress().DeSerialize<RequestMessage>();
        }

        public static RequestMessage FromStream(Stream input)
        {
            input.Position = 0;
            return input.DeSerialize<RequestMessage>();
        }

        //public Packet CreatePacket()
        //{
        //    return new Packet(ToArray(), 1) {Index = 1, Count = 1};
        //}
    }
}