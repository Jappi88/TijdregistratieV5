using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Rpm.SqlLite;

namespace Rpm.Klachten
{
    public class KlachtBeheer : IDisposable
    {
        public static readonly string KlachtDbVersion = "1.0.0.0";
        public readonly string RootPath;

        public KlachtBeheer(string path)
        {
            RootPath = Path.Combine(path, "Klachten");
            if (!Directory.Exists(RootPath))
                Directory.CreateDirectory(RootPath);
            Database = new MultipleFileDb(RootPath, true, KlachtDbVersion, DbType.Klachten);
            Database.FileChanged += Database_Changed;
            Database.FileDeleted += Database_FileDeleted;
        }

        public bool Disposed { get; private set; }

        public MultipleFileDb Database { get; private set; }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        public string GetBijlagesFolder()
        {
            var xb = Path.Combine(RootPath, "Bijlages");
            if (!Directory.Exists(xb))
                Directory.CreateDirectory(xb);
            return xb;
        }

        public string GetBijlageFolder(KlachtEntry entry)
        {
            var xb = Path.Combine(RootPath, "Bijlages", entry.ID.ToString());
            if (!Directory.Exists(xb))
                Directory.CreateDirectory(xb);
            return xb;
        }

        public int UpdateBijlages(KlachtEntry entry, bool rename)
        {
            if (entry?.Bijlages == null || entry.Bijlages.Count == 0) return 0;
            var xdone = 0;
            try
            {
                var xpath = GetBijlageFolder(entry);
                for (var i = 0; i < entry.Bijlages.Count; i++)
                {
                    var bijlage = entry.Bijlages[i];
                    var fn = Path.GetFileName(bijlage);
                    var fnext = Path.GetFileNameWithoutExtension(bijlage);
                    var xnewpath = Path.Combine(xpath, fn);
                    if (string.Equals(bijlage, xnewpath, StringComparison.CurrentCultureIgnoreCase))
                        continue;
                    if (rename)
                    {
                        var xnum = 0;
                        var xpp = xnewpath;
                        var xext = Path.GetExtension(xnewpath);
                        while (File.Exists(xpp))
                            xpp = Path.Combine(xpath, $"{fnext}_{xnum++}{xext}");
                        xnewpath = xpp;
                    }

                    File.Copy(bijlage, xnewpath, true);
                    entry.Bijlages[i] = xnewpath;
                }

                var files = Directory.GetFiles(xpath);
                var remove = files.Where(x =>
                    !entry.Bijlages.Any(f => string.Equals(x, f, StringComparison.CurrentCultureIgnoreCase))).ToList();
                remove.ForEach(r =>
                {
                    try
                    {
                        File.Delete(r);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return xdone;
        }

        public string[] GetBijlages(KlachtEntry entry)
        {
            var xpath = GetBijlageFolder(entry);
            if (!Directory.Exists(xpath)) return new string[] { };
            return Directory.GetFiles(xpath);
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

        public bool SaveKlacht(KlachtEntry entry, bool savebijlages)
        {
            try
            {
                if (entry == null || Database == null) return false;
                if (savebijlages && entry.Bijlages is {Count: > 0}) UpdateBijlages(entry, false);
                return Database.Upsert(entry.ID.ToString(), entry, false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool RemoveKlacht(KlachtEntry entry)
        {
            try
            {
                if (entry == null || Database == null) return false;
                if (Database.Delete(entry.ID.ToString()))
                {
                    var xbf = GetBijlageFolder(entry);
                    if (Directory.Exists(xbf))
                        Directory.Delete(xbf, true);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public List<KlachtEntry> GetAlleKlachten()
        {
            var xklachten = new List<KlachtEntry>();
            try
            {
                if (Database == null) return xklachten;
                xklachten = Database.GetAllEntries<KlachtEntry>().Where(x => x.IsValid).ToList();
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
            OnKlachtDeleted(Path.GetFileNameWithoutExtension(e.FullPath));
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

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed) return;

            Database?.Dispose();
            Database = null;
            Disposed = true;
        }
    }
}