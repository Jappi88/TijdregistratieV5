using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Polenter.Serialization;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;

namespace Rpm.SqlLite
{
    public class MultipleFileDb
    {
        private readonly FileSystemWatcher _pathwatcher;
        private FileSystemWatcher _secondarypathwatcher;

        private static readonly object _locker = new object();
        public string SecondaryDestination { get; private set; }
        public SecondaryManageType[] SecondaryManagedTypes { get; private set; }
        public event FileSystemEventHandler FileChanged;
        public event FileSystemEventHandler FileDeleted;
        public event FileSystemEventHandler SecondaryFileChanged;
        public event FileSystemEventHandler SecondaryFileDeleted;
        public event ProgressChangedHandler ProgressChanged;

        public MultipleFileDb(string path)
        {
            Path = path;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            _pathwatcher = new FileSystemWatcher(Path);
            _pathwatcher.EnableRaisingEvents = true;
            _pathwatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            _pathwatcher.Filter = "*.rpm";
            _pathwatcher.Changed += _pathwatcher_Changed;
            _pathwatcher.Deleted += _pathwatcher_Deleted;
        }

        private void _pathwatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            OnFileDeleted(e);
        }

        private void _pathwatcher_Changed(object sender, FileSystemEventArgs e)
        {
            OnFileChanged(e);
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
                    _secondarypathwatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
                    _secondarypathwatcher.EnableRaisingEvents = true;
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

        public void DisposeSecondayPath()
        {
            SecondaryManagedTypes = new SecondaryManageType[] { };
            SecondaryDestination = null;
            _secondarypathwatcher?.Dispose();
        }

        private void _secondarypathwatcher_Changed(object sender, FileSystemEventArgs e)
        {
            OnSecondaryFileChanged(e);
        }

        private void _secondarypathwatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            OnSecondayFileDeleted(e);
        }

        public string Path { get; }

        public List<T> GetAllEntries<T>()
        {
            lock (_locker)
            {
                var xreturn = new List<T>();
                try
                {
                    string path = GetReadPath();
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
        }

        public List<string> GetAllIDs()
        {
            try
            {
                string path = GetReadPath();
                return Directory.GetFiles(path, "*.rpm").Select(System.IO.Path.GetFileNameWithoutExtension).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<string>();
            }
        }

        public List<T> GetEntries<T>(string[] ids)
        {
            lock (_locker)
            {
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
            }
        }

        public List<T> GetEntries<T>(DateTime vanaf, DateTime tot, IsValidHandler validhandler)
        {
            lock (_locker)
            {
                var xreturn = new List<T>();
                try
                {
                    string path = GetReadPath();
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
            }
        }

        public List<T> GetEntries<T>(IsValidHandler validhandler)
        {
            lock (_locker)
            {
                var xreturn = new List<T>();
                try
                {
                    string path = GetReadPath();
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
            }
        }

        public T GetEntry<T>(string id)
        {
            string path = GetReadPath();
            path = $"{path}\\{id}.rpm";
            return GetInstanceFromFile<T>(path);
        }

        public List<T> FindEntries<T>(string criterias, bool fullmatch)
        {
            lock (_locker)
            {
                var xreturn = new List<T>();
                string xpath = GetReadPath();
                string[] crits = criterias.Split(';');
                foreach (var crit in crits)
                {
                    var path = $"{xpath}\\{crit}.rpm";
                    if (File.Exists(path))
                    {
                        var xent = GetInstanceFromFile<T>(path);
                        if (xent != null)
                            xreturn.Add(xent);
                        return xreturn;
                    }
                }

                var ids = GetAllIDs();
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
            }
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
                        if(x.Bewerkingen != null && x.Bewerkingen.Length > 0)
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

        public RpmPacket ReadPacket(Stream input)
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
            lock (_locker)
            {
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
                    if (bytes == null) return false;
                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            using (var xs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                                FileShare.Read))
                            {
                                xs.Write(bytes, 0, bytes.Length);
                                xs.Flush();
                                xs.Close();
                            }

                            //File.Copy(tmp, filepath, true);
                            //File.Delete(tmp);
                            return true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                    bytes = null;
                    //OnFileChanged(new FileSystemEventArgs(WatcherChangeTypes.Changed,
                    //    System.IO.Path.GetDirectoryName(filepath) ?? string.Empty, System.IO.Path.GetFileName(filepath)));
                    return false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }

        public T GetInstanceFromFile<T>(string filepath, string criteria = null, bool fullmatch = false, TijdEntry bereik = null)
        {

            lock (_locker)
            {
                T xreturn = default;
                for (int i = 0; i < 5; i++)
                {
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
                            xent = (T) ser.Deserialize(fs);
                        fs.Close();
                        return xent;
                    }
                    catch (Exception e)
                    {
                        xreturn = default;
                    }
                }

                return xreturn;
            }
        }

        public bool Replace<T>(string id, T newitem)
        {
            return Upsert(id, newitem);
        }

        public bool Exists(string id)
        {
            try
            {
                string path = GetReadPath();
                path = $"{path}\\{id}.rpm";
                return File.Exists(path);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Upsert<T>(string id, T item)
        {
            try
            {
                var paths = GetWritePaths();
                bool xreturn = false;
                foreach (var path in paths)
                {
                    try
                    {
                        var path1 = $"{path}\\{id.Trim()}.rpm";
                        xreturn |= WriteInstanceToFile(item, path1);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }

                return xreturn;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                var paths = GetWritePaths();
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
                var paths = GetWritePaths();
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
                string path = GetReadPath();
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

        public string GetReadPath()
        {
            return !string.IsNullOrEmpty(SecondaryDestination) && SecondaryManagedTypes != null &&
                   SecondaryManagedTypes.Any(x => x == SecondaryManageType.Read)
                   && Directory.Exists(SecondaryDestination)
                ? SecondaryDestination
                : Path;
        }

        public string[] GetWritePaths()
        {
            var xreturn = new List<string>();
            if (!string.IsNullOrEmpty(SecondaryDestination) && SecondaryManagedTypes != null && SecondaryManagedTypes.Any(
                x => x == SecondaryManageType.Write
                     && Directory.Exists(SecondaryDestination)))
            {
                xreturn.Add(SecondaryDestination);
            }

            xreturn.Add(Path);
            return xreturn.ToArray();
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
    }
}