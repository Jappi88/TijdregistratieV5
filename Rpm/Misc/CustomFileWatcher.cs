using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using NPOI.HSSF.Record.Chart;

namespace ProductieManager.Rpm.Misc
{
    public class CustomFileWatcher : IDisposable
    {
        public double Interval { get=> _Timer.Interval; set=> _Timer.Interval = value; }
        public bool Enabled { get => _Timer.Enabled; set => _Timer.Enabled = value; }
        public string Filter { get; set; } = "*.*";
        public string Path { get; private set; }
        private Dictionary<string, DateTime> Records = new Dictionary<string, DateTime>();
        private readonly Timer _Timer;

        public CustomFileWatcher(string path, string filter = "*.*")
        {
            _Timer = new Timer();
            _Timer.Interval = 3000;
            _Timer.Enabled = true;
            _Timer.Elapsed += _Timer_Elapsed;
            InitWatcher(path, filter);
        }

        private void _Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _Timer.Stop();
           CheckFiles();
           _Timer.Start();
        }

        private void CheckFiles()
        {
            try
            {
                if (!Directory.Exists(Path))
                    return;
                var xfiles = Directory.GetFiles(Path,Filter);
               
                foreach (var file in xfiles)
                {
                    var fi = new FileInfo(file);
                    if (Records.ContainsKey(file))
                    {
                        
                        if (Records[file] < fi.LastWriteTime)
                        {
                            Records[file] = fi.LastWriteTime;
                            OnFileChanged(new FileSystemEventArgs(WatcherChangeTypes.Changed, Path, fi.Name));
                        }
                    }
                    else
                    {
                        Records.Add(file, fi.LastWriteTime);
                        OnFileChanged(new FileSystemEventArgs(WatcherChangeTypes.Created, Path, fi.Name));
                    }
                }
                var xold = Records.Where(x =>
                    !xfiles.Any(f => string.Equals(f, x.Key, StringComparison.CurrentCultureIgnoreCase))).ToList();
                xold.ForEach(x =>
                {
                    Records.Remove(x.Key);
                    OnFileDeleted(new FileSystemEventArgs(WatcherChangeTypes.Deleted, Path, System.IO.Path.GetFileName(x.Key)));
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void InitWatcher(string path, string filter)
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                Path = path;
                Filter = filter;
                Records = Directory.GetFiles(path, Filter).ToDictionary(x => x, x => new FileInfo(x).LastWriteTime);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public event FileSystemEventHandler FileChanged;
        public event FileSystemEventHandler FileDeleted;

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
                _Timer?.Dispose();
            }
            
            Records?.Clear();

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
    }
}
