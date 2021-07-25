using LiteDB;

namespace Rpm.SqlLite
{
    public class DbVersion
    {
        public double Version { get; set; }

        [BsonId] public string Name { get; set; }
    }
}