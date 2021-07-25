using System.IO;
using System.Runtime.Serialization;
using Rpm.SqlLite;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Rpm.Connection
{
    [DataContract]
    public class RespondMessage
    {
        public RespondMessage(string message, MsgType msgtype, ValueContainer values, RespondType type,
            DbType dbname = DbType.None)
        {
            Sender = new ClientInfo
            {
                Name = Manager.Opties == null ? Manager.SystemID : Manager.Opties.Username,
                MachineIp = Functions.GetSystemIP().ToString(), MachinId = Manager.SystemID
            };
            Message = message;
            MessageType = msgtype;
            Values = values;
            Type = type;
            DBName = dbname;
        }

        public RespondMessage()
        {
        }

        [DataMember] public ClientInfo Sender { get; set; }
        [DataMember] public string Message { get; set; } = "";
        [DataMember] public MsgType MessageType { get; set; }
        [DataMember] public ValueContainer Values { get; set; }
        [DataMember] public RespondType Type { get; set; }
        [DataMember] public DbType DBName { get; set; }

        public byte[] ToArray()
        {
            try
            {
                return this.Serialize().Compress();
            }
            catch
            {
                return new byte[] { };
            }
        }

        public static RespondMessage FromArray(byte[] data)
        {
            try
            {
                return data.DeCompress().DeSerialize<RespondMessage>();
            }
            catch
            {
                return null;
            }
        }

        public static RespondMessage FromStream(Stream input)
        {
            input.Position = 0;
            return input.DeCompress().DeSerialize<RespondMessage>();
        }

        //public Packet[] CreatePackets()
        //{
        //    var data = ToArray();
        //    if (data == null)
        //        return new Packet[] { };
        //    List<Packet> ps = new List<Packet> { };
        //    int parts = ((data.Length - 16) / StateObject.BUFFER_SIZE);
        //    int rest = ((data.Length - 16) % StateObject.BUFFER_SIZE);
        //    int count = parts;
        //    if (rest > 0)
        //        count++;
        //    for (int i = 0; i < count; i++)
        //    {
        //        byte[] buffer;
        //        if ((i + 1) == count && rest > 0)
        //            buffer = new byte[rest];
        //        else
        //            buffer = new byte[StateObject.BUFFER_SIZE - 16];

        //        Array.Copy(data,(i * (StateObject.BUFFER_SIZE - 16)), buffer, 0, buffer.Length);
        //        var pack = new Packet(data, data.Length, 0)
        //        {
        //            Index = i + 1, Count = count
        //        };
        //        ps.Add(pack);
        //    }

        //if (rest > 0)
        //{
        //    var pack = new Packet(data, (parts * StateObject.BUFFER_SIZE), data.Length, 0)
        //    {
        //        Index = count, Count = count
        //    };
        //    ps.Add(pack);
        //}

        //string end = "data_end:respond";
        //byte[] buffer = Encoding.UTF8.GetBytes(end);
        //ps.Add(new Packet(buffer));
        //return ps.ToArray();
        //}
        //}
    }
}