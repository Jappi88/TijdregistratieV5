using Polenter.Serialization;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rpm.SqlLite
{
    public class BewerkingLijst
    {
        private static readonly object _locker = new object();
        private static readonly object _filelocker = new object();
        //public LiteDatabase Database { get; private set; }
        public List<BewerkingEntry> Entries { get; private set; }
        public FileSystemWatcher _dbWatcher;
        public BewerkingLijst()
        {
            LoadDb();
        }

        private bool LoadDb()
        {
            try
            {
                string filename = $"{Manager.DbPath}\\BewerkingLijst.rpm";
                if (!File.Exists(filename))
                {
                    if (!CreateNew()) return false;
                }

                if (_dbWatcher == null)
                {
                    _dbWatcher = new FileSystemWatcher(Manager.DbPath);
                    _dbWatcher.EnableRaisingEvents = true;
                    _dbWatcher.Changed += _dbWatcher_Changed;
                }
                //Database = new LiteDatabase(new ConnectionString()
                    //{ Connection = ConnectionType.Shared });
                    lock (_locker)
                    {
                        using var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        var ser = new SharpSerializer(true);
                        Entries = (List<BewerkingEntry>)ser.Deserialize(fs);
                        fs.Close();
                    }
                    if (Entries == null && !CreateNew())
                        throw new Exception("Ongeldige bewerkinglijst database!");
                    return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private void _dbWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (!string.Equals(e.Name, "BewerkingLijst.rpm", StringComparison.CurrentCultureIgnoreCase)) return;
            lock (_filelocker)
            {
                LoadDb();
            }
        }

        public bool DeleteEntry(string naam)
        {
            try
            {
                var bwent = Entries?.FirstOrDefault(x =>
                    string.Equals(x.Naam, naam, StringComparison.CurrentCultureIgnoreCase));
                if (bwent == null) return false;
                return WriteEntries();
            }
            catch
            {
                return false;
            }
        }

        public BewerkingEntry GetEntry(string naam)
        {
            try
            {
                if (Entries == null && !LoadDb()) return null;
                return Entries.FirstOrDefault(x =>
                    string.Equals(x.Naam, naam, StringComparison.CurrentCultureIgnoreCase));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool UpdateEntry(BewerkingEntry ent)
        {
            try
            {
                if (Entries == null && !LoadDb()) return false;
                Entries.Remove(ent);
                Entries.Add(ent);
                return WriteEntries();
            }
            catch
            {
                return false;
            }
        }

        public List<string> GetWerkplekken(string naam)
        {
            try
            {
                var entry = GetEntry(naam);
                if (entry != null)
                    return entry.WerkPlekken;
                return new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }

        //public bool Load()
        //{
        //    try
        //    {
        //        LijstPath = $"{Manager.DbPath}\\BewerkingLijst.Db";
        //        if (!IsLoaded)
        //        {
        //            Database = new LiteDatabase(new ConnectionString(LijstPath)
        //                {Connection = ConnectionType.Shared});
        //            Entries = Manager.Database.BewerkingEntries;//Database?.GetCollection<BewerkingEntry>("BewerkingEntries");
        //            IsLoaded = true;
        //        }

        //        if (Entries != null && Entries.Count() == 0)
        //            return CreateNew();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        public Task<int> UpdateDatabase()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    if (Manager.Database == null || Manager.Database.IsDisposed)
                        return -1;
                    if (Entries == null && !LoadDb()) return 0;
                    var prods = Manager.Database.xGetAllBewerkingen(true, false, true);
                    var done = 0;
                    foreach (var bw in prods)
                    {
                        var changed = false;
                        var ent = GetEntry(bw.Naam.Split('[')[0]);
                        if (ent != null)
                        {
                            if (bw.IsBemand != ent.IsBemand)
                            {
                                bw.IsBemand = ent.IsBemand;
                                changed = true;
                                done++;
                            }

                            if (ent.HasChanged)
                            {
                                bw.Naam = ent.NewName;
                                changed = true;
                                done++;
                                foreach (var wp in bw.WerkPlekken)
                                {
                                    foreach (var st in wp.Storingen)
                                    {
                                        st.Path = wp.Path;
                                    }
                                }
                            }
                        }

                        if (changed)
                            _ = bw.UpdateBewerking(null, null, true, false);
                    }

                    var entries = GetAllEntries();
                    var reload = false;
                    foreach (var ent in entries)
                        if (ent.HasChanged)
                        {
                            DeleteEntry(ent.Naam);
                            ent.Naam = ent.NewName;
                            UpdateEntry(ent);
                            reload = done > 0;
                        }

                    if (reload)
                        Manager.ProductiesChanged();
                    return done;
                }
                catch
                {
                    return -1;
                }
            });
        }

        public bool CreateNew()
        {
            try
            {
                string filename = $"{Manager.DbPath}\\BewerkingLijst.db";
                if (File.Exists(filename))
                {
                    try
                    {
                        File.Delete(filename);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                if (Entries == null || Entries.Count == 0)
                    Entries = Functions.LoadBewerkingLijst("BewerkingenV2.txt");
                return WriteEntries();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BewerkingEntry> GetAllEntries()
        {
            try
            {
                if (Entries == null && !LoadDb())
                    return Entries = new List<BewerkingEntry>();
                return Entries;
            }
            catch
            {
                return Entries = new List<BewerkingEntry>();
            }
        }

        public List<string> GetAlleWerkplekken()
        {
            try
            {
                if (Entries == null && !LoadDb())
                    return new List<string>();
                var entries = Entries;
                var values = new List<string>();
                foreach (var wp in entries.SelectMany(entry => entry.WerkPlekken.Where(wp =>
                             values.All(x => !string.Equals(x, wp, StringComparison.CurrentCultureIgnoreCase)))))
                {
                    values.Add(wp);
                }

                return values.OrderBy(x=> x).ToList();
            }
            catch
            {
                return new List<string>();
            }
        }

        public bool WriteEntries()
        {
            lock (_locker)
            {
                string filename = $"{Manager.DbPath}\\BewerkingLijst.rpm";
                try
                {
                    using var fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                    var ser = new SharpSerializer(true);
                    ser.Serialize(Entries, fs);
                    fs.Close();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }
    }
}