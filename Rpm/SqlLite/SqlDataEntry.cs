using System;

namespace Rpm.SqlLite
{
    public class SqlDataEntry
    {
        public int Id { get; set; }
        public DbType Type { get; set; }
        public string Name { get; set; }
        public DateTime LastChanged { get; set; }
        public string ChangedBy { get; set; }
        public byte[] DataObject { get; set; }
    }
}