using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rpm.SqlLite;

namespace ProductieManager.Rpm.KLachten
{
    public delegate void KlachtHandler(object sender, KlachtEntry entry);

    public class KlachtBeheer
    {
        public MultipleFileDb Database { get; private set; }
        public readonly string RootPath;

        public KlachtBeheer(string path)
        {
            RootPath = path;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            Database = new MultipleFileDb(path, true);
            Database.FileChanged += Database_Changed;
            Database.FileDeleted += Database_FileDeleted;
        }

        public string GetBijlageFolder()
        {
            string xb = Path.Combine(RootPath, "Bijlages");
            if (!Directory.Exists(xb))
                Directory.CreateDirectory(xb);
            return xb;
        }

        public string[] GetBijlages(KlachtEntry entry)
        {
            var xpath = GetBijlageFolder();
            string xb = Path.Combine(xpath, entry.ID.ToString());
            if (!Directory.Exists(xb)) return new string[] { };
            return Directory.GetFiles(xb);
        }


        public KlachtEntry GetKlacht(string id)
        {
            try
            {
                return Database?.GetEntry<KlachtEntry>(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool SaveKlacht(KlachtEntry entry)
        {
            try
            {
                if (entry == null || Database == null) return false;
                return Database.Upsert<KlachtEntry>(entry.ID.ToString(), entry, false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public List<KlachtEntry> GetAlleKlachten()
        {
            List<KlachtEntry> xklachten = new List<KlachtEntry>();
            try
            {
                if (Database == null) return xklachten;
                xklachten = Database.GetAllEntries<KlachtEntry>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return xklachten;
        }

        public List<KlachtEntry> GetUnreadKlachten()
        {
            return GetAlleKlachten().Where(x => !x.IsGelezen).ToList();
        }

        private void Database_FileDeleted(object sender, FileSystemEventArgs e)
        {
            OnKlachtDeleted(this);
        }

        private void Database_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (Database == null) return;
                var xk = Database.GetEntry<KlachtEntry>(Path.GetFileNameWithoutExtension(e.FullPath));
                if (xk != null)
                    OnKlachtChanged(xk);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public event EventHandler KlachtChanged;
        public event EventHandler KlachtDeleted;

        protected virtual void OnKlachtChanged(object sender)
        {
            KlachtChanged?.Invoke(sender, EventArgs.Empty);
        }

        protected virtual void OnKlachtDeleted(object sender)
        {
            KlachtDeleted?.Invoke(sender, EventArgs.Empty);
        }
    }
}
