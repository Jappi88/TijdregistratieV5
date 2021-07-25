namespace Rpm.Connection
{
    //[DataContract]
    public class ClientInfo
    {
        //[DataMember] 
        public string Name { get; set; }

        //[DataMember] 
        public string MachinId { get; set; }

        //[DataMember] 
        public string MachineIp { get; set; }
    }
}