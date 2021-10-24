using System;
using System.Collections.Generic;
using System.IO;
using Rpm.SqlLite;

namespace Rpm.Settings
{
    public class DatabaseUpdateEntry
    {
        public string Naam { get; set; }
        private string _rootpath;

        public string UpdatePath
        {
            get => Path.Combine(_rootpath.Replace("\\RPM_Data", "").TrimEnd('\\') + "\\", "RPM_Data").Replace("//", "\\");
            set => _rootpath = value.Replace("\\RPM_Data", "");
        } 
            

        public string RootPath
        {
            get => _rootpath.Replace("\\RPM_Data", "").Replace("//", "\\").TrimEnd('\\') + "\\";
            set => _rootpath = value.Replace("\\RPM_Data", "");
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