using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using iTextSharp.text.pdf.parser.clipper;
using NPOI.SS.Formula.Functions;
using Polenter.Serialization;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;

namespace Rpm.SqlLite
{
    public class MultipleFileDb : IDisposable
    {
        private FileSystemWatcher _pathwatcher;
        private FileSystemWatcher _secondarypathwatcher;
        private readonly Timer _FileChangedNotifyTimer;

        //private static readonly object _locker = new object();
        public string SecondaryDestination { get; private set; }
        public SecondaryManageType[] SecondaryManagedTypes { get; private set; }
        public event FileSystemEventHandler FileChanged;
        public event FileSystemEventHandler FileDeleted;
        public event FileSystemEventHandler SecondaryFileChanged;
        public event FileSystemEventHandler SecondaryFileDeleted;
        public event ProgressChangedHandler ProgressChanged;
        public static List<string> CorruptedFilePaths { get; private set; } = new List<string>();

        public static event EventHandler CorruptedFilesChanged;

        public MultipleFileDb(string path, bool watchdb)
        {
            _FileChangedNotifyTimer = new Timer(1000);//500 ms vertraging
            _FileChangedNotifyTimer.Elapsed += _FileChangedNotifyTimer_Elapsed;
            Path = path;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (watchdb)
            {
                _pathwatcher = new FileSystemWatcher(Path);
                _pathwatcher.EnableRaisingEvents = true;
                _pathwatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.FileName;
                _pathwatcher.Filter = "*.rpm";
                _pathwatcher.Changed += _pathwatcher_Changed;
                _pathwatcher.Deleted += _pathwatcher_Deleted;
            }
        }

        private readonly List<string> _changes = new List<string>();

        private void _FileChangedNotifyTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _FileChangedNotifyTimer?.Stop();
            if (_changes == null) return;
            try
            {
                lock (_changes)
                {
                    for (int i = 0; i < _changes.Count; i++)
                    {
                        var x = _changes[i];
                        OnFileChanged(new FileSystemEventArgs(WatcherChangeTypes.Changed,
                            System.IO.Path.GetDirectoryName(x) ?? x, System.IO.Path.GetFileName(x)));
                        _changes.RemoveAt(i--);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void _pathwatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            OnFileDeleted(e);
        }

        private void _pathwatcher_Changed(object sender, FileSystemEventArgs e)
        {
            _FileChangedNotifyTimer?.Stop();
            var rpath = GetReadPath(true).ToLower();
            if (!e.FullPath.ToLower().StartsWith(rpath)) return;
            lock (_changes)
            {
                if (!_changes.Any(x => string.Equals(x, e.FullPath, StringComparison.CurrentCultureIgnoreCase)))
                    _changes.Add(e.FullPath);
            }
            _FileChangedNotifyTimer?.Start();
        }

        public void DisposeSecondayPath()
        {
            if (_secondarypathwatcher != null)
            {
                _secondarypathwatcher.Deleted -= _secondarypathwatcher_Deleted;
                _secondarypathwatcher.Changed -= _secondarypathwatcher_Changed;
                //_secondarypathwatcher.Dispose();
                _secondarypathwatcher = null;
            }

            SecondaryManagedTypes = new SecondaryManageType[] { };
            SecondaryDestination = null;
        }

        private void _secondarypathwatcher_Changed(object sender, FileSystemEventArgs e)
        {
            _FileChangedNotifyTimer?.Stop();
            var rpath = GetReadPath(true).ToLower();
            if (!e.FullPath.ToLower().StartsWith(rpath)) return;
            lock (_changes)
            {
                if (!_changes.Any(x => string.Equals(x, e.FullPath, StringComparison.CurrentCultureIgnoreCase)))
                    _changes.Add(e.FullPath);
            }
            _FileChangedNotifyTimer?.Start();
        }

        private void _secondarypathwatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            OnSecondayFileDeleted(e);
        }

        public bool SetSecondaryPath(string path, SecondaryManageType[] managetypes)
        {
            try
            {
                if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                {
                    SecondaryDestination = path;
                    SecondaryManagedTypes = managetypes;
                    _secondarypathwatcher?.Dispose();
                    _secondarypathwatcher = new FileSystemWatcher(path);
                    _secondarypathwatcher.EnableRaisingEvents = true;
                    _secondarypathwatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.FileName;
                    _secondarypathwatcher.Filter = "*.rpm";
                    _secondarypathwatcher.Deleted += _secondarypathwatcher_Deleted;
                    _secondarypathwatcher.Changed += _secondarypathwatcher_Changed;
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

        public List<T> GetAllEntries<T>()
        {

            var xreturn = new List<T>();
            try
            {
                string path = GetReadPath(true);
                var files = Directory.GetFiles(path, "*.rpm");

                foreach (var file in files)
                {
                    var xent = GetInstanceFromFile<T>(file);
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
                string path = GetReadPath(checksecondary);
                return Directory.GetFiles(path, "*.rpm").ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<string>();
            }
        }

        public List<T> GetEntries<T>(string[] ids)
        {
            //lock (_locker)
            //{
                var xreturn = new List<T>();
                try
                {

                    foreach (var id in ids)
                    {
                        var xent = GetEntry<T>(id);
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

        public List<T> GetEntries<T>(DateTime vanaf, DateTime tot, IsValidHandler validhandler)
        {
           //lock (_locker)
           //{
               var xreturn = new List<T>();
               try
               {
                   string path = GetReadPath(true);
                   var files = Directory.GetFiles(path, "*.rpm");
                   foreach (var file in files)
                   {

                       var xent = GetInstanceFromFile<T>(file, null, false, new TijdEntry(vanaf, tot, null));
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

        public List<T> GetEntries<T>(IsValidHandler validhandler)
        {
            //lock (_locker)
           // {
                var xreturn = new List<T>();
                try
                {
                    string path = GetReadPath(true);
                    var files = Directory.GetFiles(path, "*.rpm");
                    foreach (var file in files)
                    {

                        var xent = GetInstanceFromFile<T>(file);
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

        public T GetEntry<T>(string id)
        {
            string path = GetReadPath(true,$"{id}.rpm");
            path = $"{path}\\{id}.rpm";
            return GetInstanceFromFile<T>(path);
        }

        public List<T> FindEntries<T>(string criterias, bool fullmatch)
        {
            //lock (_locker)
            //{
                var xreturn = new List<T>();
                string xpath = GetReadPath(false);
                if (criterias != null)
                {
                    string[] crits = criterias.Split(';');
                    foreach (var crit in crits)
                    {
                        var path = $"{xpath}\\{crit}.rpm";
                        if (File.Exists(path))
                        {
                            var xent = GetInstanceFromFile<T>(path);
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
                    var ent = GetInstanceFromFile<T>(path, criterias, fullmatch);
                    if (ent != null)
                        xreturn.Add(ent);
                }

                return xreturn;
            //}
        }

        public RpmPacket CreatePacketFromObject(object value)
        {
            try
            {
                var rpm = new RpmPacket();
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
                    case UserChange x:
                        rpm.ID = x.User?.Trim();
                        rpm.Changed = x.TimeChanged;
                        rpm.Type = DbType.Changes;
                        if (!string.IsNullOrEmpty(x.Change))
                            rpm.Criterias.Add(x.Change.Trim());
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

        public bool WriteInstanceToFile(object data, string filepath)
        {
            //lock (_locker)
            //{
                try
                {
                    if (string.IsNullOrEmpty(filepath)) return false;
                    //string tmp = Manager.TempPath + "\\" + System.IO.Path.GetRandomFileName();
                    byte[] bytes = null;
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
                            using var xs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                                FileShare.None);
                            xs.Write(bytes, 0, bytes.Length);
                            xs.Close();
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

        public static T GetInstanceFromFile<T>(string filepath, string criteria = null, bool fullmatch = false, TijdEntry bereik = null)
        {

           //lock (_locker)
           //{
               T xreturn = default;
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
                           lock (CorruptedFilePaths)
                           {
                               int index =0;
                               if ((index = CorruptedFilePaths.IndexOf(filepath.ToLower())) > -1)
                               {
                                   CorruptedFilePaths.RemoveAt(index);
                                   OnCorruptedFilesChanged();
                               }
                           }
                       }

                       fs.Close();
                       return xreturn;
                   }
                   catch (Exception e)
                   {
                       xreturn = default;
                       if (xbreak)
                       {
                           lock (CorruptedFilePaths)
                           {
                               if (CorruptedFilePaths.IndexOf(filepath.ToLower()) < 0)
                               {
                                   CorruptedFilePaths.Add(filepath.ToLower());
                                   OnCorruptedFilesChanged();
                               }
                           }
                           break;
                       }
                   }
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

        public bool Upsert<T>(string id, T item, bool onlylocal)
        {
            try
            {
                var paths = GetWritePaths(onlylocal);
                if (paths.Length == 0) return false;
                var xfirst = paths[0];
                var path1 = $"{xfirst}\\{id.Trim()}.rpm";
                if (WriteInstanceToFile(item, path1))
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

        public static Task<T> FromPath<T>(string path)
        {
            return Task.Factory.StartNew(() =>
            {

                try
                {
                    return GetInstanceFromFile<T>(path);
                }
                catch (Exception e)
                {
                    return default;
                }
            });
        }

        public bool Delete(string id)
        {
            try
            {
                var paths = GetWritePaths(false);
                foreach (var path in paths)
                {
                    try
                    {
                        var path1 = $"{path}\\{id.Trim()}.rpm";
                        if (File.Exists(path1))
                        {
                            File.Delete(path1);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                return true;
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
                _pathwatcher.Changed -= _pathwatcher_Changed;
                _pathwatcher.Deleted -= _pathwatcher_Deleted;
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

        protected virtual void OnSecondaryFileChanged(FileSystemEventArgs e)
        {
            SecondaryFileChanged?.Invoke(this, e);
        }

        protected virtual void OnSecondayFileDeleted(FileSystemEventArgs e)
        {
            SecondaryFileDeleted?.Invoke(this, e);
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
                _FileChangedNotifyTimer?.Dispose();
                _pathwatcher?.Dispose();
                _secondarypathwatcher?.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.
            FileChanged = null;
            FileDeleted = null;
            SecondaryFileChanged = null;
            SecondaryFileDeleted = null;
            ProgressChanged = null;
            SecondaryDestination = null;
            SecondaryManagedTypes = null;
            Path = null;
            _disposed = true;
        }
    }
}