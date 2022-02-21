using System;
using System.Collections.Generic;
using System.Linq;
using Rpm.SqlLite;

namespace ProductieManager.Rpm.Misc
{
    public class RpmPacket
    {
        public RpmPacket()
        {
            Changed = DateTime.Now;
        }

        public string ID { get; set; }
        public DateTime Changed { get; set; }
        public List<string> Criterias { get; set; } = new();
        public bool IsCompressed { get; set; }
        public DbType Type { get; set; }

        public bool ContainsCriteria(string criteria, bool fullmatch)
        {
            if (Criterias == null || Criterias.Count == 0) return false;
            criteria ??= string.Empty;
            var crits = criteria.Split(';');
            foreach (var crit in crits)
            {
                if (string.Equals(crit, ID, StringComparison.CurrentCultureIgnoreCase)) return true;
                if (fullmatch)
                    if (Criterias.Any(x => string.Equals(crit, x, StringComparison.CurrentCultureIgnoreCase)))
                        return true;
                if (Criterias.Any(x => x != null && x.ToLower().Contains(crit.ToLower())))
                    return true;
            }

            return false;
        }
    }
}