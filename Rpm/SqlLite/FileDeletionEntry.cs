using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rpm.Productie;

namespace Rpm.SqlLite
{
    public class FileDeletionEntry
    {
        public string DeletedBy { get; set; }
        public string Path { get; set; }
        public DateTime DeletedOn { get; set; }

        public FileDeletionEntry()
        {
            DeletedBy = Manager.Opties?.Username ?? Manager.DefaultSettings?.Username ?? "Onbekend";
            DeletedOn = DateTime.Now;
        }

        public FileDeletionEntry(string path) : this()
        {
            Path = path;
        }

        public bool IsDeleted(string path)
        {
            if (Path == null || path == null) return false;
            string pname1 = System.IO.Path.GetFileName(Path);
            string path1 = System.IO.Path.GetDirectoryName(Path);
            pname1 = System.IO.Path.GetFileName(path1) + "\\" + pname1;
            string pname2 = System.IO.Path.GetFileName(path);
            string path2 = System.IO.Path.GetDirectoryName(path);
            pname2 = System.IO.Path.GetFileName(path2) + "\\" + pname2;

            return string.Equals(pname1, pname2, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
