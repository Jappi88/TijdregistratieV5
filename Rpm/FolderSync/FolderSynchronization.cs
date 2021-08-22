using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using Rpm.Productie;

namespace FolderSync
{
    public class FolderSynchronization
    {
        protected AutoResetEvent _SyncIsPaused = new AutoResetEvent(false);
        protected AutoResetEvent _ScanIsPaused = new AutoResetEvent(false);
        protected AutoResetEvent _MonitorIsPaused = new AutoResetEvent(false);
        protected Queue<FileOperation> _SyncQueue = new Queue<FileOperation>();
        protected Queue<FolderSynchronizationScannerItem> _ScanQueue = new Queue<FolderSynchronizationScannerItem>();
        protected Queue<FolderSynchronizationScannerItem> _MonitorQueue = new Queue<FolderSynchronizationScannerItem>();
        protected bool _IsRunning = false;
        protected bool _IsPausedSync = false;
        protected bool _IsPausedScan = false;
        protected bool _IsPausedMonitor = false;
        protected Task _SyncThread = null;
        protected Task _ScanThread = null;
        protected Task _MonitorThread = null;
        protected string _Status = string.Empty;
        public int Interval
        {
            get => Manager.DefaultSettings == null ? 1000 : Manager.DefaultSettings.OfflineDbSyncInterval;
        }
        public string Status => _Status;

        public int QueueSyncCount => _SyncQueue.Count;

        public int QueueScanCount
        {
            get
            {
                var xreturn = 0;
                if (_ScanQueue != null)
                {
                    lock (_ScanQueue)
                    {
                        xreturn = _ScanQueue.Count;
                    }
                }

                return xreturn;
            }
        }

        public int QueueMonitorCount => _MonitorQueue.Count;

        public FolderSynchronization()
        {
            try
            {
                GeneralLib.AddAccess(AppDomain.CurrentDomain.BaseDirectory);
            }
            catch { }
        }

        #region Start/Stop/Pause/Resume
        public void Start()
        {
            if (_IsRunning == false)
            {
                _IsRunning = true;
                StartSyncingThread();
                StartScanningThread();
               // StartMonitorThread();
            }
        }

        public void Stop()
        {
            if (_IsRunning)
            {
                _IsRunning = false;
            }
            lock (_ScanQueue)
            {
                _ScanQueue?.Clear();
            }
            lock (_SyncQueue)
            {
                _SyncQueue?.Clear();
            }
            //lock (_MonitorQueue)
            //{
            //    _MonitorQueue?.Clear();
            //}
        }

        public void PauseMonitor()
        {
            _IsPausedMonitor = true;
        }

        public void PauseScan()
        {
            _IsPausedScan = true;
        }

        public void PauseSync()
        {
            _IsPausedSync = true;
        }

        public void ResumeMonitor()
        {
            if (_IsRunning)
            {
                _IsPausedMonitor = false;
                _MonitorIsPaused.Set();
            }
        }

        public void ResumeScan()
        {
            if (_IsRunning)
            {
                _IsPausedScan = false;
                _ScanIsPaused.Set();
            }
        }

        public void ResumeSync()
        {
            if (_IsRunning)
            {
                _IsPausedSync = false;
                _SyncIsPaused.Set();
            }
        }
        #endregion

        protected void StartMonitorThread()
        {
            _MonitorThread = Task.Factory.StartNew(StartMonitor);
        }

        protected void StartScanningThread()
        {
            _ScanThread = Task.Factory.StartNew(StartScanning);
        }

        protected void StartSyncingThread()
        {
            _SyncThread = Task.Factory.StartNew(StartSyncing);
        }

        protected async void StartMonitor()
        {
            while (true)
            {
                
                if (_IsPausedMonitor) _MonitorIsPaused.WaitOne();
                if (_IsRunning == false || Manager.Opties == null || Manager.DefaultSettings is not
                {
                    GebruikOfflineMetSync: true
                })
                {
                    break;
                }

                while (true)
                {
                    FolderSynchronizationScannerItem op = null;
                    lock (_MonitorQueue)
                    {
                        if (_MonitorQueue.Count == 0) break;
                        op = _MonitorQueue.Dequeue();
                    }

                    if (op != null)
                    {
                        try
                        {
                            if (Directory.Exists(op.Source) && Directory.Exists(op.Destination))
                            {
                                lock (_ScanQueue)
                                {
                                    _ScanQueue.Enqueue(op);
                                }

                            }
                            else
                            {
                                if (op.Monitor)
                                {
                                    lock (_MonitorQueue)
                                    {
                                        _MonitorQueue.Enqueue(op);
                                    }
                                }

                            }

                            _Status = string.Empty;
                        }
                        catch (Exception ex)
                        {
                            _Status = ex.Message;
                        }
                    }
                }
                await Task.Delay(1000);
            }
        }

        protected async void StartScanning()
        {
            while (true)
            {
                if (_IsPausedScan) _ScanIsPaused.WaitOne();
                if (_IsRunning == false || Manager.Opties == null || Manager.DefaultSettings is not
                {
                    GebruikOfflineMetSync: true
                })
                {
                    break;
                }

                //if (_SyncQueue != null)
                //{
                //    lock (_SyncQueue)
                //    {
                //        if (_SyncQueue.Count > 0) continue;
                //    }
                //}
                FolderSynchronizationScannerItem[] items = null;
                if (_ScanQueue != null)
                {
                    lock (_ScanQueue)
                    {
                        items = _ScanQueue.ToArray();
                    }

                    while (true)
                    {
                        FolderSynchronizationScannerItem op = null;
                        lock (_ScanQueue)
                        {
                            if (_ScanQueue == null || _ScanQueue.Count == 0) break;
                            op = _ScanQueue.Dequeue();
                        }


                        if (op != null)
                        {
                            try
                            {
                                if (Directory.Exists(op.Source) && Directory.Exists(op.Destination))
                                {
                                    FolderSynchronizationScanner fss =
                                        new FolderSynchronizationScanner(op.Source, op.Destination, op.Option);
                                    await fss.Sync();
                                    if (fss.SyncCollection.Objects.Count > 0)
                                    {
                                        lock (_SyncQueue)
                                        {
                                            _SyncQueue.Clear();
                                            foreach (FileOperation fo in fss.SyncCollection.Objects)
                                            {
                                                _SyncQueue.Enqueue(fo);
                                            }
                                        }
                                        await Task.Delay(fss.SyncCollection.Objects.Count * 10);
                                    }
                                }

                                _Status = string.Empty;
                            }
                            catch (Exception ex)
                            {
                                _Status = ex.Message;
                            }
                           
                        }

                    }
                }

                if(items is {Length: > 0})
                    foreach (var item in items)
                    {
                        lock (_ScanQueue)
                        {
                            _ScanQueue.Enqueue(item);
                        }
                    }

                await Task.Delay(Interval);
            }
        }

        protected async void StartSyncing()
        {
            while(_IsRunning)
            {
                if (_IsPausedSync) _SyncIsPaused.WaitOne();
                if (_IsRunning == false || Manager.Opties == null || Manager.DefaultSettings is not
                {
                    GebruikOfflineMetSync: true
                })
                {
                    _IsRunning = false;
                    break;
                }
                while (_IsRunning)
                {
                    FileOperation op = null;
                    lock (_SyncQueue)
                    {
                        if (_SyncQueue.Count == 0)
                            break;
                        op = _SyncQueue.Dequeue();
                    }

                    if (op != null)
                    {
                        try
                        {
                            op.DoOperation();
                            _Status = string.Empty;
                        }
                        catch (Exception ex)
                        {
                            _Status = ex.Message;
                        }
                    }
                }
                await Task.Delay(Interval);
            }
        }

        public void AddScan(FolderSynchronizationScannerItem item)
        {
            if (item == null || _ScanQueue == null) return;
            lock (_ScanQueue)
            {
                if (!_ScanQueue.Any(x =>
                    string.Equals(x.Destination, item.Destination, StringComparison.CurrentCultureIgnoreCase)))
                    _ScanQueue.Enqueue(item);
            }
        }

        //public void StartFolderScanner(string source, string destination, FolderSynchorizationOption option, bool monitor)
        //{
        //    FolderSynchronizationScanner fss = new FolderSynchronizationScanner(source, destination, option);
        //    fss.Sync();
        //    if (fss.SyncCollection.Objects.Count <= 0) return;
        //    foreach (FileOperation fo in fss.SyncCollection.Objects)
        //    {
        //        AddQueue(fo);
        //    }
        //}
    }
}
