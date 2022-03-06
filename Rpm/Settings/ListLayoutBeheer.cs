using Rpm.Mailing;
using Rpm.Productie;
using Rpm.SqlLite;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductieManager.Rpm.Settings
{
    public class ListLayoutBeheer : IDisposable
    {
        public static readonly string DbVersion = "1.0.0.0";
        public bool Disposed => _disposed;
        public MultipleFileDb Database { get; private set; }
        public readonly string RootPath;

        public ListLayoutBeheer(string path)
        {
            RootPath = Path.Combine(path, "LijstLayouts");
            if (!Directory.Exists(RootPath))
                Directory.CreateDirectory(RootPath);
            Database = new MultipleFileDb(RootPath, true, DbVersion, DbType.LijstLayouts);
            Database.FileChanged += Database_Changed;
            Database.FileDeleted += Database_FileDeleted;
        }


        public ExcelSettings GetLayout(string listname)
        {
            try
            {
                return Database?.GetEntry<ExcelSettings>(listname, false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool SaveLayout(ExcelSettings entry, string change, bool raiseevent)
        {
            try
            {
                if (Database == null || Disposed)
                    return false;
                bool xlast = Database.RaiseChangeEvent;
                Database.RaiseChangeEvent = raiseevent;
                if (Database.Upsert(entry.Name, entry, false))
                {
                    if (!string.IsNullOrEmpty(change))
                        Manager.RemoteMessage(new RemoteMessage(change, MessageAction.AlgemeneMelding, MsgType.Info));
                    Database.RaiseChangeEvent = xlast;
                    return true;
                }

                Database.RaiseChangeEvent = xlast;
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Exists(string listname)
        {
            try
            {
                if (Database == null || Disposed)
                    return false;
                return Database.Exists(listname);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool RemoveLayout(ExcelSettings entry, bool showmessage)
        {
            try
            {
                if (entry == null || Database == null) return false;
                if (Database.Delete(entry.Name))
                {
                    if (showmessage)
                    {
                        string msg = $"[Lijst Layout] {entry.Name} " +
                                     $"verwijderd!";
                        Manager.RemoteMessage(new RemoteMessage(msg, MessageAction.AlgemeneMelding, MsgType.Info));
                    }

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

        public bool RemoveLayout(string listname)
        {
            try
            {
                if (string.IsNullOrEmpty(listname) || Database == null) return false;
                if (Database.Delete(listname))
                {
                    string msg = $"[Lijst Layout] {listname} " +
                                 $"verwijderd!";
                    Manager.RemoteMessage(new RemoteMessage(msg, MessageAction.AlgemeneMelding, MsgType.Info));
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

        public List<ExcelSettings> GetAlleLayouts()
        {
            List<ExcelSettings> xreturn = new List<ExcelSettings>();
            try
            {
                if (Database == null) return xreturn;
                xreturn = Database.GetAllEntries<ExcelSettings>(true).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return xreturn;
        }

        public List<string> GetAlleIDs()
        {
            List<string> xreturn = new List<string>();
            try
            {
                if (Database == null) return xreturn;
                xreturn = Database.GetAllIDs(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return xreturn;
        }

        private void Database_FileDeleted(object sender, FileSystemEventArgs e)
        {
            OnLayoutDeleted(Path.GetFileNameWithoutExtension(e.FullPath));
        }

        private void Database_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (Database == null) return;
                var xk = Database.GetEntry<ExcelSettings>(Path.GetFileNameWithoutExtension(e.FullPath), false);
                if (xk != null)
                    OnLayoutChanged(xk);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public event EventHandler LayoutChanged;
        public event EventHandler LayoutDeleted;

        protected virtual void OnLayoutChanged(object sender)
        {
            LayoutChanged?.Invoke(sender, EventArgs.Empty);
        }

        protected virtual void OnLayoutDeleted(object sender)
        {
            LayoutDeleted?.Invoke(sender, EventArgs.Empty);
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
