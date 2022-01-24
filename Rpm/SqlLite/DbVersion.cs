using System;

namespace Rpm.SqlLite
{
    public class DbVersion
    {
        public string Version { get; set; }
        public DbType DbType { get; set; }
        public DateTime DateChanged { get; set; }
    }
}