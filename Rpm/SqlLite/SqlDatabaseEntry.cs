using System;
using System.Data;
using Rpm.Misc;

namespace Rpm.SqlLite
{
    public class SqlDatabaseEntry
    {
        public SqlDatabaseEntry()
        {
        }

        public SqlDatabaseEntry(object[] values)
        {
            if (values is {Length: > 0})
                foreach (var value in values)
                    if (value is string item)
                        Name = item;
                    else if (value is short id)
                        DbId = id;
                    else if (value is DateTime created)
                        Created = created;
            IsValid = Name != null && !Created.IsDefault();
        }

        public SqlDatabaseEntry(DataRow row) : this(row?.ItemArray)
        {
        }

        public string Name { get; set; }
        public int DbId { get; set; }
        public DateTime Created { get; set; }

        public bool IsValid { get; }
    }
}