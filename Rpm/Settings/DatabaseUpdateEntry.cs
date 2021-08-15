using System;
using System.Collections.Generic;
using Rpm.SqlLite;

namespace Rpm.Settings
{
    public class DatabaseUpdateEntry
    {
        public string Naam { get; set; }
        private string _updatepath;
        public string UpdatePath { get => _updatepath; set => _updatepath = value.Replace("\\RPM_Data", ""); } 
            

        public string RootPath
        {
            get => UpdatePath?.Replace("\\RPM_Data", "");
            set => UpdatePath = value.Replace("\\RPM_Data", "") + "\\RPM_Data";
        }

        public bool UpdateMetStartup { get; set; }
        public List<DbType> UpdateDatabases { get; set; } = new();
        public bool AutoUpdate { get; set; }
        public DateTime LastUpdated { get; set; }

        public override bool Equals(object obj)
        {
            var ent = (DatabaseUpdateEntry) obj;
            if (ent == null)
                return false;
            if (!string.Equals(Naam, ent.Naam, StringComparison.CurrentCultureIgnoreCase))
                return false;
            if (!string.Equals(UpdatePath, ent.UpdatePath, StringComparison.CurrentCultureIgnoreCase))
                return false;
            if (UpdateMetStartup != ent.UpdateMetStartup)
                return false;
            if (AutoUpdate != ent.AutoUpdate)
                return false;
            if (UpdateDatabases.Count != ent.UpdateDatabases.Count)
                return false;
            for (var i = 0; i < UpdateDatabases.Count; i++)
                if (UpdateDatabases[i] != ent.UpdateDatabases[i])
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}