using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;

namespace ProductieManager.Rpm.Misc
{
    public class CustomFileWatcher : IDisposable
    {
        public double Interval
        {
            get => _Timer?.Interval ?? -1;
            set
            {
                if (_Timer != null)
                    _Timer.Interval = value;
            }
        }
        public bool Enabled
        {
            get => _Timer?.Enabled ?? false;
            set
            {
                if (_Timer != null)
                    _Timer.Enabled = value;
            }
        }

        public string Filter { get; set; } = "*.*";
        public bool CheckForFolderChanges { get; set; }
        public string Path { get; private set; }
        public Dictionary<string, DateTime> Records = new Dictionary<string, DateTime>();


        private Timer _Timer;

        public CustomFileWatcher(string path, string filter = "*.*")
        {
            InitWatcher(path, filter, false);
        }

        public CustomFileWatcher(string path, bool checkfolders, string filter = "*.*")
        {
            InitWatcher(path, filter, checkfolders);
        }

        public CustomFileWatcher()
        {
        }

        private void _Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _Timer?.Stop();
                CheckChanges(Path);
                _Timer?.Start();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void UpdateFile(string file, bool raiseevent, bool forceupdate = false, DateTime newtime = default)
        {
            try
            {
                var fi = new FileInfo(file);
                var time = newtime.IsDefault() ? fi.LastWriteTime : newtime;
                if (Records.ContainsKey(file))
                {
                   
                    if (forceupdate || Records[file] < time)
                    {
                        lock (Records)
                        {
                            Records[file] = time;
                        }
                        if (raiseevent)
                            OnFileChanged(new FileSystemEventArgs(WatcherChangeTypes.Changed, fi.DirectoryName, fi.Name));
                    }
                }
                else
                {
                    lock (Records)
                    {
                        Records.Add(file, time);
                    }
                    if (raiseevent)
                        OnFileChanged(new FileSystemEventArgs(WatcherChangeTypes.Created, fi.DirectoryName, fi.Name));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void CheckFiles(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                    return;
                var xfiles = Directory.GetFiles(path, Filter).ToList();

                for (var i = 0; i < xfiles.Count; i++)
                {
                    UpdateFile(xfiles[i],Manager.Database.RaiseEventWhenChanged);
                }

                if (CheckForFolderChanges)
                    xfiles.AddRange(Directory.GetDirectories(path));
                var xdeletes = Records.Where(x => !xfiles.Any(f => string.Equals(f, x.Key, StringComparison.CurrentCultureIgnoreCase))).Select(x => x.Key).ToList();

                for (int i = 0; i < xdeletes.Count; i++)
                {
                    var xrec = xdeletes[i];
                    lock (Records)
                    {
                        Records.Remove(xrec);
                    }
                    if (Manager.Database.RaiseEventWhenDeleted)
                        OnFileDeleted(new FileSystemEventArgs(WatcherChangeTypes.Deleted, path,
                            System.IO.Path.GetFileName(xrec)));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void CheckFolders(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                    return;
                var xdirs = Directory.GetDirectories(path).ToList();
                lock (Records)
                {
                    for (var i = 0; i < xdirs.Count; i++)
                    {

                        var dir = xdirs[i];
                        var xinfo = new DirectoryInfo(dir);
                        if (Records.ContainsKey(dir))
                        {

                            if (Records[dir] < xinfo.LastWriteTime)
                            {
                                Records[dir] = xinfo.LastWriteTime;
                                OnFolderChanged(new FileSystemEventArgs(WatcherChangeTypes.Changed, path, xinfo.Name));
                            }
                        }
                        else
                        {
                            Records.Add(dir, xinfo.LastWriteTime);
                            OnFolderChanged(new FileSystemEventArgs(WatcherChangeTypes.Created, path, xinfo.Name));
                        }
                    }
                    xdirs.AddRange(Directory.GetFiles(path, Filter));
                    var xdeletes = Records.Where(x => !xdirs.Any(f => string.Equals(f, x.Key, StringComparison.CurrentCultureIgnoreCase))).Select(x => x.Key).ToList();

                    for (int i = 0; i < xdeletes.Count; i++)
                    {
                        var xrec = xdeletes[i];
                        Records.Remove(xrec);
                        OnFolderDeleted(new FileSystemEventArgs(WatcherChangeTypes.Deleted, path,
                            System.IO.Path.GetFileName(xrec)));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void CheckChanges(string path)
        {
            if (_disposed) return;
            CheckFiles(path);
            if (CheckForFolderChanges)
                CheckFolders(path);
        }

        public void InitWatcher(string path, string filter, bool checkforlders)
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                if (_Timer == null)
                {
                    _Timer = new Timer();
                    _Timer.Interval = 3000;
                    _Timer.Enabled = false;
                    _Timer.Elapsed += _Timer_Elapsed;
                }
                else _Timer.Stop();

                CheckForFolderChanges = checkforlders;
                Path = path;
                Filter = filter;
                if (Records == null)
                {
                    Records = new Dictionary<string, DateTime>();
                }

                lock (Records)
                {
                    Records?.Clear();
                    Records = Directory.GetFiles(path, Filter)
                        .ToDictionary(x => x, x => new FileInfo(x).LastWriteTime);

                    if (CheckForFolderChanges)
                    {
                        var xdirs = Directory.GetDirectories(path, Filter).ToList();
                        for (int i = 0; i < xdirs.Count; i++)
                        {
                            var v = xdirs[i];
                            if (Records.ContainsKey(v))
                                Records[v] = Directory.GetLastWriteTime(v);
                            else
                                Records.Add(v, Directory.GetLastWriteTime(v));
                        }
                    }
                }
                OnWatcherLoaded();
                _Timer?.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public event EventHandler WatcherLoaded;
        public event FileSystemEventHandler FileChanged;
        public event FileSystemEventHandler FileDeleted;
        public event FileSystemEventHandler FolderChanged;
        public event FileSystemEventHandler FolderDeleted;

        private bool _disposed;
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _Timer?.Stop();
                _Timer?.Dispose();
                _Timer = null;
            }
            
            lock (Records)
            {
                Records?.Clear();
            }

            _disposed = true;
        }

        protected virtual void OnFileChanged(FileSystemEventArgs e)
        {
            FileChanged?.Invoke(this, e);
        }

        protected virtual void OnFileDeleted(FileSystemEventArgs e)
        {
            FileDeleted?.Invoke(this, e);
        }

        protected virtual void OnFolderChanged(FileSystemEventArgs e)
        {
            FolderChanged?.Invoke(this, e);
        }

        protected virtual void OnFolderDeleted(FileSystemEventArgs e)
        {
            FolderDeleted?.Invoke(this, e);
        }

        protected virtual void OnWatcherLoaded()
        {
            WatcherLoaded?.Invoke(this, EventArgs.Empty);
        }
    }
}
