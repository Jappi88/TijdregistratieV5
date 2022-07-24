using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rpm.Verzoeken
{
    public class VerzoekBeheer
    {
        public const string Version = "1.0.0.0";
        public SqlLite.MultipleFileDb Database { get; private set; }
        public readonly string VerzoekDbPath;
        public VerzoekBeheer(string dbpath)
        {
            VerzoekDbPath = Path.Combine(dbpath, "Verzoeken");
            Database = new SqlLite.MultipleFileDb(VerzoekDbPath, true, Version, SqlLite.DbType.Verzoeken);
            Database.FileChanged += Database_FileChanged;
            Database.FileDeleted += Database_FileDeleted;
        }

        public List<VerzoekEntry> GetEntries(string personeel, string afdeling, bool unreadonly, VerzoekStatus status)
        {
            var xret = new List<VerzoekEntry>();
            try
            {
                if(Database != null && Database.CanRead)
                {
                    xret = Database.GetAllEntries<VerzoekEntry>(false);
                    if(!string.IsNullOrEmpty(personeel))
                        xret = xret.Where(x=> string.Equals(x.PersoneelNaam, personeel, StringComparison.CurrentCultureIgnoreCase)).ToList();
                    if (!string.IsNullOrEmpty(afdeling))
                    {
                        xret = xret.Where(x => string.Equals(x.Afdeling, afdeling, StringComparison.CurrentCultureIgnoreCase)).ToList();

                    }
                    if(unreadonly && Manager.Opties?.Username != null)
                        xret = xret.Where(x => !x.IsRead() || (string.IsNullOrEmpty(afdeling) || string.Equals(x.Afdeling, afdeling, StringComparison.CurrentCultureIgnoreCase) && x.Status == VerzoekStatus.InAfwachting)).ToList();
                    if(status != VerzoekStatus.Geen)
                        xret = xret.Where(x => x.Status == status).ToList();
                }
            }
            catch (Exception ex){ Console.WriteLine(ex.ToString()); }
            return xret;
        }

        public List<VerzoekEntry> GetUnreadEntries()
        {
            var xret = new List<VerzoekEntry>();
            try
            {
                bool flag = Manager.LogedInGebruiker is { AccesLevel: Various.AccesType.Manager };
                string afdeling = flag ? null : Manager.Opties?.Username;
                VerzoekStatus status = flag ? VerzoekStatus.InAfwachting : VerzoekStatus.Geen;
                var unread = flag?false:true;
                xret = GetEntries(null, afdeling, unread, status);
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return xret;
        }

        public bool UpdateVerzoek(VerzoekEntry verzoek)
        {
            try
            {
                if (verzoek == null || Database == null || !Database.CanRead) return false;
                return Database.Upsert<VerzoekEntry>(verzoek.ID.ToString(), verzoek, false);
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); return false; }
        }

        #region Events
        private void Database_FileDeleted(object sender, FileSystemEventArgs e)
        {
            if (Database == null || !Database.CanRead) return;
            var name = Path.GetFileNameWithoutExtension(e.FullPath);
            if(!Database.Exists(name))
                OnVerzoekDeleted(name,e);
        }

        private void Database_FileChanged(object sender, FileSystemEventArgs e)
        {
            if (Database == null || !Database.CanRead) return;
            var name = Path.GetFileNameWithoutExtension(e.FullPath);
            var ent = Database.GetEntry<VerzoekEntry>(name, false);
            if(ent != null)
            {
                OnVerzoekChanged(ent, e);
            }
        }

        public event FileSystemEventHandler VerzoekChanged;
        public event FileSystemEventHandler VerzoekDeleted;
        protected virtual void OnVerzoekChanged(object item, FileSystemEventArgs e)
        {
            VerzoekChanged?.Invoke(item, e);
        }

        protected virtual void OnVerzoekDeleted(object item, FileSystemEventArgs e)
        {
            VerzoekDeleted?.Invoke(item, e);
        }
        #endregion Events

        public void Close()
        {
            if (Database != null)
            {
                Database.FileChanged -= Database_FileChanged;
                Database.FileDeleted -= Database_FileDeleted;
                Database.Close();
                Database.Dispose();
                Database = null;
            }
        }
    }
}
