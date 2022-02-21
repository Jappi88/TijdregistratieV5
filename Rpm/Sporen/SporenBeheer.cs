using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Rpm.Mailing;
using Rpm.SqlLite;
using Rpm.Various;

namespace Rpm.Productie.Verpakking
{
    public class SporenBeheer : IDisposable
    {
        public static readonly string VerpakkingDbVersion = "1.0.0.0";
        public readonly string RootPath;

        public SporenBeheer(string path)
        {
            RootPath = Path.Combine(path, "Sporen");
            if (!Directory.Exists(RootPath))
                Directory.CreateDirectory(RootPath);
            Database = new MultipleFileDb(RootPath, true, VerpakkingDbVersion, DbType.Verpakkingen);
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


        public SpoorEntry GetSpoor(string artikelnr)
        {
            try
            {
                return Database?.GetEntry<SpoorEntry>(artikelnr);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool SaveSpoor(SpoorEntry entry, string change)
        {
            try
            {
                if (Database.Upsert(entry.ArtikelNr, entry, false))
                {
                    Manager.RemoteMessage(new RemoteMessage(change, MessageAction.AlgemeneMelding, MsgType.Info));
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

        public bool RemoveSpoor(SpoorEntry entry)
        {
            try
            {
                if (entry == null || Database == null) return false;
                if (Database.Delete(entry.ArtikelNr))
                {
                    var msg = $"[SPOOR][{entry.ArtikelNr}] {entry.ProductOmschrijving} " +
                              "verwijderd!";
                    Manager.RemoteMessage(new RemoteMessage(msg, MessageAction.AlgemeneMelding, MsgType.Success));
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

        public bool RemoveSpoor(string artikelnr)
        {
            try
            {
                if (string.IsNullOrEmpty(artikelnr) || Database == null) return false;
                if (Database.Delete(artikelnr))
                {
                    var msg = $"[SPOOR][{artikelnr}] " +
                              "verwijderd!";
                    Manager.RemoteMessage(new RemoteMessage(msg, MessageAction.AlgemeneMelding, MsgType.Success));
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

        public List<SpoorEntry> GetAlleSporen()
        {
            var xsporen = new List<SpoorEntry>();
            try
            {
                if (Database == null) return xsporen;
                xsporen = Database.GetAllEntries<SpoorEntry>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xsporen;
        }

        public List<string> GetAlleIDs()
        {
            var xsporen = new List<string>();
            try
            {
                if (Database == null) return xsporen;
                xsporen = Database.GetAllIDs(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xsporen;
        }

        private void Database_FileDeleted(object sender, FileSystemEventArgs e)
        {
            OnSporenDeleted(Path.GetFileNameWithoutExtension(e.FullPath));
        }

        private void Database_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (Database == null) return;
                var xk = Database.GetEntry<SpoorEntry>(Path.GetFileNameWithoutExtension(e.FullPath));
                if (xk != null)
                    OnSpoorChanged(xk);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public event EventHandler SpoorChanged;
        public event EventHandler SpoorDeleted;

        protected virtual void OnSpoorChanged(object sender)
        {
            SpoorChanged?.Invoke(sender, EventArgs.Empty);
        }

        protected virtual void OnSporenDeleted(object sender)
        {
            SpoorDeleted?.Invoke(sender, EventArgs.Empty);
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