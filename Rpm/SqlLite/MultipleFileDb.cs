using Polenter.Serialization;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rpm.SqlLite
{
    public class MultipleFileDb : IDisposable
    {
        private CustomFileWatcher _pathwatcher;
        //private readonly Timer _FileChangedNotifyTimer;

        //private static readonly object _locker = new object();
        public string SecondaryDestination { get; private set; }
        public DbVersion Version { get; private set; }
        private bool _canread;
        public bool CanRead { get=> _canread && !_disposed;
            private set => _canread = value;
        }
        public SecondaryManageType[] SecondaryManagedTypes { get; private set; }
        public event FileSystemEventHandler FileChanged;
        public event FileSystemEventHandler FileDeleted;
        public event ProgressChangedHandler ProgressChanged;
        public bool MonitorCorrupted { get; set; }
        public bool RaiseChangeEvent { get; set; } = true;
        public static List<string> CorruptedFilePaths { get; private set; } = new List<string>();

        public static event EventHandler CorruptedFilesChanged;

        public MultipleFileDb(string path, bool watchdb, string version, DbType type, string filter = "*.rpm")
        {
            //_FileChangedNotifyTimer = new Timer(1000);//500 ms vertraging
            //_FileChangedNotifyTimer.Elapsed += _FileChangedNotifyTimer_Elapsed;
            Path = path;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            InitVersion(version, type);
            if (watchdb)
            {
                _pathwatcher = new CustomFileWatcher(Path, filter);
                _pathwatcher.FileChanged += _pathwatcher_Changed;
                _pathwatcher.FileDeleted += _pathwatcher_Deleted;
            }
        }

        private void InitVersion(string version, DbType type)
        {
            try
            {
                if (string.IsNullOrEmpty(Path)) return;
                var dbfile = System.IO.Path.Combine(Path, "Version.db");
                bool done = false;
                if (File.Exists(dbfile))
                {
                    try
                    {
                        Version = dbfile.DeSerialize<DbVersion>();
                        if (Version == null) throw new Exception("Unable to parse 'Version' db");
                        var xdbversion = new Version(Version.Version);
                        var xnewversion = new Version(version);
                        if (xnewversion > xdbversion)
                        {
                            Version.Version = version;
                            Version.Serialize(dbfile);
                        }
                        done = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        done = false;
                    }
                }
                if (!done)
                {
                    Version = new DbVersion() {DateChanged = DateTime.Now, DbType = type, Version = version};
                    Version.Serialize(dbfile);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _canread = Version != null &&
                       string.Equals(version, Version.Version, StringComparison.CurrentCultureIgnoreCase);
        }

        private readonly List<string> _changes = new List<string>();

        private void _pathwatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            OnFileDeleted(e);
        }

        private void _pathwatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (!_canread) return;
            if (!RaiseChangeEvent) return;
            bool valid = false;
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    using var fs = new FileStream(e.FullPath, FileMode.Open);
                    fs.Close();
                    valid = true;
                    break;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }

            if (valid)
                OnFileChanged(e);
            //_FileChangedNotifyTimer?.Stop();
            //var rpath = GetReadPath(true).ToLower();
            //if (!e.FullPath.ToLower().StartsWith(rpath)) return;
            //lock (_changes)
            //{
            //    if (!_changes.Any(x => string.Equals(x, e.FullPath, StringComparison.CurrentCultureIgnoreCase)))
            //        _changes.Add(e.FullPath);
            //}
            //_FileChangedNotifyTimer?.Start();
        }

        public void DisposeSecondayPath()
        {
            SecondaryManagedTypes = new SecondaryManageType[] { };
            SecondaryDestination = null;
        }


        public bool SetSecondaryPath(string path, SecondaryManageType[] managetypes)
        {
            try
            {
                if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                {
                    SecondaryDestination = path;
                    SecondaryManagedTypes = managetypes;
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

        public string Path { get; private set; }

        public List<T> GetAllEntries<T>(bool usesecondary, List<string> skip = default)
        {

            var xreturn = new List<T>();
            try
            {
                if (!CanRead) return xreturn;
                string path = GetReadPath(usesecondary);
                var files = Directory.GetFiles(path, "*.rpm").ToList();
                if (skip is {Count: > 0})
                {
                    files.RemoveAll(x => skip.Any(s => string.Equals(System.IO.Path.GetFileNameWithoutExtension(x), s,
                        StringComparison.CurrentCultureIgnoreCase)));
                }
                foreach (var file in files)
                {
                    var xent = GetInstanceFromFile<T>(file, MonitorCorrupted);
                    if (xent != null)
                        xreturn.Add(xent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return xreturn;
        }

        public List<string> GetAllIDs(bool checksecondary)
        {
            try
            {
                if(!CanRead) return new List<string>();
                string path = GetReadPath(checksecondary);
                return Directory.GetFiles(path, "*.rpm").Select(System.IO.Path.GetFileNameWithoutExtension).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<string>();
            }
        }

        public List<string> GetAllPaths(bool checksecondary)
        {
            try
            {
                if (!CanRead) return new List<string>();
                string path = GetReadPath(checksecondary);
                return Directory.GetFiles(path, "*.rpm").ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<string>();
            }
        }

        public List<T> GetEntries<T>(string[] ids, bool usesecondary)
        {
            //lock (_locker)
            //{
            var xreturn = new List<T>();
            try
            {
                if (!CanRead) return xreturn;
                foreach (var id in ids)
                {
                    var xent = GetEntry<T>(id, usesecondary);
                    if (xent != null)
                        xreturn.Add(xent);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return xreturn;
            //}
        }

        public List<T> GetEntries<T>(DateTime vanaf, DateTime tot, IsValidHandler validhandler, bool checksecondary)
        {
            //lock (_locker)
            //{
            var xreturn = new List<T>();
            try
            {
                if (!CanRead) return xreturn;
                string path = GetReadPath(checksecondary);
                var files = Directory.GetFiles(path, "*.rpm");
                foreach (var file in files)
                {

                    var xent = GetInstanceFromFile<T>(file,MonitorCorrupted, null, false, new TijdEntry(vanaf, tot, null));
                    if (xent != null)
                    {
                        if (validhandler != null && !validhandler.Invoke(xent, null)) continue;
                        xreturn.Add(xent);
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return xreturn;
            //}
        }

        public List<T> GetEntries<T>(IsValidHandler validhandler, bool checksecondary)
        {
            //lock (_locker)
            // {
            var xreturn = new List<T>();
            try
            {
                if (!CanRead) return xreturn;
                string path = GetReadPath(checksecondary);
                var files = Directory.GetFiles(path, "*.rpm");
                foreach (var file in files)
                {

                    var xent = GetInstanceFromFile<T>(file, MonitorCorrupted);
                    if (xent != null)
                    {
                        if (validhandler != null && !validhandler.Invoke(xent, null)) continue;
                        xreturn.Add(xent);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return xreturn;
            //}
        }

        public T GetEntry<T>(string id, bool usesecondary)
        {
            if (!CanRead) return default;
            string path = GetReadPath(usesecondary, $"{id}.rpm");
            path = $"{path}\\{id}.rpm";
            return GetInstanceFromFile<T>(path, MonitorCorrupted);
        }

        public List<T> FindEntries<T>(string criterias, bool fullmatch, bool usesecondary)
        {
            //lock (_locker)
            //{
                var xreturn = new List<T>();
                if (!CanRead) return xreturn;
                string xpath = GetReadPath(usesecondary);
                if (criterias != null)
                {
                    string[] crits = criterias.Split(';');
                    foreach (var crit in crits)
                    {
                        var path = $"{xpath}\\{crit}.rpm";
                        if (File.Exists(path))
                        {
                            var xent = GetInstanceFromFile<T>(path, MonitorCorrupted);
                            if (xent != null)
                                xreturn.Add(xent);
                            criterias = criterias.Replace(crit, "");
                        }
                    }

                    if (criterias.Replace(";", "").Trim().Length < 4)
                        return xreturn;
                }

                var ids = GetAllIDs(true);
                foreach (var id in ids)
                {
                    string path = null;
                    if (!string.IsNullOrEmpty(id))
                        path = $"{xpath}\\{id}.rpm";
                    if (path == null) continue;
                    var ent = GetInstanceFromFile<T>(path,MonitorCorrupted, criterias, fullmatch);
                    if (ent != null)
                        xreturn.Add(ent);
                }

                return xreturn;
            //}
        }

        public static RpmPacket CreatePacketFromObject(object value)
        {
            try
            {
                var rpm = new RpmPacket();
                rpm.Criterias.Clear();
                switch (value)
                {
                    case ProductieFormulier x:
                        rpm.ID = x.ProductieNr?.Trim();
                        rpm.Type = DbType.Producties;
                        rpm.Changed = x.LastChanged?.TimeChanged ?? DateTime.Now;
                        if (!string.IsNullOrEmpty(x.ArtikelNr))
                            rpm.Criterias.Add(x.ArtikelNr);
                        if (!string.IsNullOrEmpty(x.Omschrijving))
                            rpm.Criterias.Add(x.Omschrijving);
                        if(x.Bewerkingen is {Length: > 0})
                            foreach (var b in x.Bewerkingen)
                            {
                                rpm.Criterias.Add(b.Naam);
                                if(b.WerkPlekken.Count > 0)
                                    rpm.Criterias.AddRange(b.WerkPlekken.Select(w=> w.Naam));
                            }
                        break;
                    case UserSettings x:
                        rpm.ID = x.Username?.Trim();
                        rpm.Type = DbType.Opties;
                        rpm.Changed = x.LastChanged?.TimeChanged ?? DateTime.Now;
                        rpm.Criterias.Add(x.SystemID);
                        break;
                    case UserAccount x:
                        rpm.ID = x.Username?.Trim();
                        rpm.Type = DbType.Accounts;
                        break;
                    case Personeel x:
                        rpm.ID = x.PersoneelNaam?.Trim();
                        rpm.Changed = x.LastChanged?.TimeChanged ?? DateTime.Now;
                        rpm.Type = DbType.Medewerkers;
                        break;
                    case LogEntry x:
                        rpm.ID = x.Id.ToString();
                        rpm.Type = DbType.Logs;
                        rpm.Changed = x.Added;
                        if (!string.IsNullOrEmpty(x.Username))
                            rpm.Criterias.Add(x.Username.Trim());
                        if (!string.IsNullOrEmpty(x.Message))
                            rpm.Criterias.Add(x.Message.Trim());
                        break;

                }

                return rpm;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        public static RpmPacket ReadPacket(Stream input)
        {
            try
            {
                var ser = new SharpSerializer(true);
                if (ser.Deserialize(input) is RpmPacket packet)
                    return packet;
                input.Position = 0;
                return null;
            }
            catch (Exception e)
            {
                input.Position = 0;
                Console.WriteLine(e);
                return null;
            }
        }

        public static bool WriteInstanceToFile(object data, string filepath, bool raiseevent)
        {
            //lock (_locker)
            //{
            try
            {
                if (string.IsNullOrEmpty(filepath)) return false;
                //string tmp = Manager.TempPath + "\\" + System.IO.Path.GetRandomFileName();
                byte[] bytes;
                using var fs = new MemoryStream();
                {
                    RpmPacket packet = CreatePacketFromObject(data);
                    var ser = new SharpSerializer(true);
                    if (packet != null)
                        ser.Serialize(packet, fs);

                    ser.Serialize(data, fs);
                    bytes = fs.ToArray();
                    fs.Close();
                }
                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        var dir = System.IO.Path.GetDirectoryName(filepath);
                        if (!Directory.Exists(dir)) break;
                        using var xfs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                        xfs.SetLength(bytes.Length);
                        xfs.Position = 0;
                        xfs.Write(bytes, 0, bytes.Length);
                        xfs.Close();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                //OnFileChanged(new FileSystemEventArgs(WatcherChangeTypes.Changed,
                //    System.IO.Path.GetDirectoryName(filepath) ?? string.Empty, System.IO.Path.GetFileName(filepath)));
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            //}
        }

        private static List<string> _blockedfiles = new List<string>();
        public static void BlockFile(string file)
        {
            if (_blockedfiles.IndexOf(file) < 0)
                _blockedfiles.Add(file);
        }

        public static bool IsBlocked(string file)
        {
            return _blockedfiles.IndexOf(file) > -1;
        }

        public static T GetInstanceFromFile<T>(string filepath, bool monitorcorrupted = true, string criteria = null, bool fullmatch = false,
            TijdEntry bereik = null)
        {

            //lock (_locker)
            //{
            T xreturn = default;
            try
            {
                bool cor = false;
                for (int i = 0; i < 5; i++)
                {
                    bool xbreak = false;
                    try
                    {
                        if (!File.Exists(filepath)) return default;
                        using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        var packet = ReadPacket(fs);
                        var ser = new SharpSerializer(true);
                        T xent = default;
                        if (bereik != null && packet != null &&
                            !(packet.Changed >= bereik.Start && packet.Changed <= bereik.Stop))
                            return xent;
                        if (packet == null || string.IsNullOrEmpty(criteria) ||
                            packet.ContainsCriteria(criteria, fullmatch))
                        {
                            xbreak = true;
                            xreturn = (T)ser.Deserialize(fs);
                            cor = false;
                            if (monitorcorrupted)
                            {
                                //lock (CorruptedFilePaths)
                                //{
                                int index = 0;
                                if ((index = CorruptedFilePaths.IndexOf(filepath.ToLower())) > -1)
                                {
                                    CorruptedFilePaths.RemoveAt(index);
                                    OnCorruptedFilesChanged();
                                }
                                //}
                            }
                        }

                        fs.Close();
                        return xreturn;
                    }
                    catch
                    {
                        cor = true;
                        xreturn = default;
                        if (xbreak)
                            break;
                    }
                }
                if (cor && monitorcorrupted)
                {

                    //lock (CorruptedFilePaths)
                    //{
                    if (CorruptedFilePaths.IndexOf(filepath.ToLower()) < 0)
                    {
                        CorruptedFilePaths.Add(filepath.ToLower());
                        OnCorruptedFilesChanged();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return xreturn;
            //}
        }

        public bool Replace<T>(string id, T newitem, bool onlylocal)
        {
            return Upsert<T>(id, newitem,onlylocal);
        }

        public bool Exists(string id)
        {
            try
            {
                string path = GetReadPath(true, $"{id}.rpm");
                path = $"{path}\\{id}.rpm";
                return File.Exists(path);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Upsert<T>(string id, T item, bool onlylocal, bool raiseevent = true)
        {
            try
            {
                if (!CanRead) return false;
                var paths = GetWritePaths(onlylocal);
                if (paths.Length == 0) return false;
                var xfirst = paths[0];
                var path1 = $"{xfirst}\\{id.Trim()}.rpm";
                if (WriteInstanceToFile(item, path1, raiseevent))
                {
                    for (int i = 1; i < paths.Length; i++)
                    {
                        var path2 = System.IO.Path.Combine(paths[i], id + ".rpm");
                        for (int j = 0; j < 5; j++)
                        {
                            try
                            {
                                File.Copy(path1, path2, true);
                                break;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static Task<T> FromPath<T>(string path, bool monitorcorrupted)
        {
            return Task.Factory.StartNew(() =>
            {

                try
                {
                    return GetInstanceFromFile<T>(path, monitorcorrupted);
                }
                catch
                {
                    return default;
                }
            });
        }

        public static T xFromPath<T>(string path, bool monitorcorrupted)
        {
            try
            {
                return GetInstanceFromFile<T>(path, monitorcorrupted);
            }
            catch
            {
                return default;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                if (!CanRead) return false;
                var paths = GetWritePaths(false);
                bool xdel = false;
                foreach (var path in paths)
                {
                    try
                    {
                        var path1 = $"{path}\\{id.Trim()}.rpm";
                        if (File.Exists(path1))
                        {
                            File.Delete(path1);
                            xdel = true;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                return xdel;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Delete(string[] ids)
        {
            var done = 0;
            try
            {
                if (!CanRead) return 0;

                foreach (var id in ids)
                {
                    if (Delete(id))
                        done++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return done;
        }

        public int DeleteAll()
        {
            var done = 0;
            int max = 0;
            try
            {
                if (!CanRead) return 0;
                var paths = GetWritePaths(false);
                DoProgress("Entries laden...", 0, 0, null, ProgressType.WriteBussy);
                foreach (var path in paths)
                {
                    var files = Directory.GetFiles(path, "*.rpm");
                    max += files.Length;
                    foreach (var file in files)
                        try
                        {
                            DoProgress($"Verwijderen van: '{System.IO.Path.GetFileName(file)}'", done, max, file, ProgressType.WriteBussy);
                            File.Delete(file);
                            done++;
                        }
                        catch
                        {
                        }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            DoProgress($"{done} Entries verwijderd!", done, max, null, ProgressType.WriteCompleet);
            return done;
        }

        public int Count()
        {
            try
            {
                string path = GetReadPath(true);
                return Directory.GetFiles(path, "*.rpm").Length;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void DoProgress(string message, int min, int max, object value, ProgressType type)
        {
            int perc = max > min ? ((min / max) * 100) : 100;
            ProgressArg arg = new ProgressArg()
            {
                Message = message, Pogress = perc, Value = value, Type = type
            };
        }

        public string GetReadPath(bool checksecondary, string filename = null)
        {
            string xpath = SecondaryDestination;
            var x = checksecondary && !string.IsNullOrEmpty(xpath) && SecondaryManagedTypes != null &&
                   SecondaryManagedTypes.Any(x => x == SecondaryManageType.Read)
                   && (((!string.IsNullOrEmpty(filename) && File.Exists(xpath + $"\\{filename}"))
                        || Directory.Exists(xpath)))
                ? xpath
                : Path;
            return x;
        }

        public string[] GetWritePaths(bool onlylocal)
        {
            var xreturn = new List<string>();
            string xpath = SecondaryDestination;
           
            if (!string.IsNullOrEmpty(xpath) &&
                !string.Equals(xpath, Path, StringComparison.CurrentCultureIgnoreCase) &&
                SecondaryManagedTypes != null && SecondaryManagedTypes.Any(
                    x => x == SecondaryManageType.Write
                         && Directory.Exists(xpath)))
            {
                xreturn.Add(xpath);
            }

            if (!onlylocal)
                xreturn.Insert(0, Path);
            return xreturn.ToArray();
        }

        public void Close()
        {
            DisposeSecondayPath();
            if (_pathwatcher != null)
            {
                _pathwatcher.FileChanged -= _pathwatcher_Changed;
                _pathwatcher.FileDeleted -= _pathwatcher_Deleted;
                _pathwatcher?.Dispose();
                _pathwatcher = null;
            }
        }

        protected virtual void OnFileChanged(FileSystemEventArgs e)
        {
            FileChanged?.Invoke(this, e);
        }

        protected virtual void OnFileDeleted(FileSystemEventArgs e)
        {
            FileDeleted?.Invoke(this, e);
        }

        protected virtual void OnProgressChanged(ProgressArg arg)
        {
            ProgressChanged?.Invoke(this, arg);
        }

        public static void OnCorruptedFilesChanged()
        {
            CorruptedFilesChanged?.Invoke(CorruptedFilePaths, EventArgs.Empty);
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

            if (disposing)
            {
               // _FileChangedNotifyTimer?.Dispose();
                _pathwatcher?.Dispose();
            }

            _canread = false;
            Version = null;
            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.
            FileChanged = null;
            FileDeleted = null;
            ProgressChanged = null;
            SecondaryDestination = null;
            SecondaryManagedTypes = null;
            Path = null;
            _disposed = true;
        }
    }
}