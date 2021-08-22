using System;
using System.IO;
using System.Timers;

namespace Rpm.SqlLite
{
    public class PathWatcher
    {

        public string Path { get; private set; }
        private int _interval = 5000; //5 sec
        private DateTime _LastChecked = DateTime.Now;
        public int SyncInterval { get => _interval; set
            {
                _interval = value;
                if (_Timer != null)
                    _Timer.Interval = value;
            }
        }

        public bool RaiseEvent { get; set; }
        public bool IsSyncing { get; private set; }
        public bool PathLost { get;  set; }
        public bool IsFile { get; private set; }
        public void WatchPath(string path, bool isfile, bool raiseevent)
        {
            if (IsSyncing && string.Equals(path, Path, StringComparison.CurrentCultureIgnoreCase))
                return;
            RaiseEvent = raiseevent;
            IsSyncing = true;
            IsFile = isfile;
            Path = path;
            _Timer?.Dispose();
            _Timer = new Timer(SyncInterval);
            _Timer.Elapsed += _Timer_Elapsed;
            _Timer.Start();
        }

        public void Start()
        {
            _Timer?.Start();
        }

        public void Stop()
        {
            _Timer?.Stop();
        }

        #region Timer
        private Timer _Timer;
        private void _Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _Timer?.Stop();
            try
            {
                if (IsFile)
                {
                    if (!File.Exists(Path))
                    {
                        if (!PathLost && RaiseEvent)
                            PathLocationLost?.Invoke(this, EventArgs.Empty);
                        PathLost = true;
                    }
                    else
                    {
                        if (PathLost && RaiseEvent)
                            PathLocationFound?.Invoke(this, EventArgs.Empty);
                        PathLost = false;
                    }
                }
                else
                {
                    if (!Directory.Exists(Path))
                    {
                        if (!PathLost && RaiseEvent)
                            PathLocationLost?.Invoke(this, EventArgs.Empty);
                        PathLost = true;
                    }
                    else
                    {
                        if (PathLost && RaiseEvent)
                            PathLocationFound?.Invoke(this, EventArgs.Empty);
                        PathLost = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            _LastChecked = DateTime.Now;
            _Timer?.Start();
        }
        #endregion

        public void Dispose()
        {

            _Timer?.Stop();
            _Timer?.Dispose();
            _Timer = null;
            IsSyncing = false;
            Path = null;
            IsFile = false;
            RaiseEvent = false;
            PathLocationFound = null;
            PathLocationLost = null;
        }

        public event EventHandler PathLocationLost;
        public event EventHandler PathLocationFound;
    }
}
