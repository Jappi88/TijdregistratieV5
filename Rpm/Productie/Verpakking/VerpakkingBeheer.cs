using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rpm.Klachten;
using Rpm.Productie;
using Rpm.SqlLite;

namespace ProductieManager.Rpm.Productie.Verpakking
{
    public class VerpakkingBeheer : IDisposable
    {
        public static readonly string VerpakkingDbVersion = "1.0.0.0";
        public bool Disposed => _disposed;
        public MultipleFileDb Database { get; private set; }
        public readonly string RootPath;

        public VerpakkingBeheer(string path)
        {
            RootPath = Path.Combine(path, "Verpakking");
            if (!Directory.Exists(RootPath))
                Directory.CreateDirectory(RootPath);
            Database = new MultipleFileDb(RootPath, true, VerpakkingDbVersion, DbType.Verpakkingen);
            Database.FileChanged += Database_Changed;
            Database.FileDeleted += Database_FileDeleted;
        }


        public VerpakkingInstructie GetVerpakking(string artikelnr)
        {
            try
            {
                return Database?.GetEntry<VerpakkingInstructie>(artikelnr, false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool SaveVerpakking(VerpakkingInstructie entry)
        {
            try
            {
                return Database.Upsert(entry.ArtikelNr, entry, false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool RemoveVerpakking(VerpakkingInstructie entry)
        {
            try
            {
                if (entry == null || Database == null) return false;
                return Database.Delete(entry.ArtikelNr);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool RemoveVerpakking(string artikelnr)
        {
            try
            {
                if (string.IsNullOrEmpty(artikelnr) || Database == null) return false;
                return Database.Delete(artikelnr);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public List<VerpakkingInstructie> GetAlleVerpakkingen()
        {
            List<VerpakkingInstructie> xklachten = new List<VerpakkingInstructie>();
            try
            {
                if (Database == null) return xklachten;
                xklachten = Database.GetAllEntries<VerpakkingInstructie>(true).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return xklachten;
        }

        private void Database_FileDeleted(object sender, FileSystemEventArgs e)
        {
            OnVerpakkingDeleted(Path.GetFileNameWithoutExtension(e.FullPath));
        }

        private void Database_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (Database == null) return;
                var xk = Database.GetEntry<VerpakkingInstructie>(Path.GetFileNameWithoutExtension(e.FullPath), false);
                if (xk != null)
                    OnVerpakkingChanged(xk);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public event EventHandler VerpakkingChanged;
        public event EventHandler VerpakkingDeleted;

        protected virtual void OnVerpakkingChanged(object sender)
        {
            VerpakkingChanged?.Invoke(sender, EventArgs.Empty);
        }

        protected virtual void OnVerpakkingDeleted(object sender)
        {
            VerpakkingDeleted?.Invoke(sender, EventArgs.Empty);
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            Database?.Dispose();
            Database = null;
            _disposed = true;
        }
    }
}
