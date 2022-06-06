using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FolderSync
{
    /// <summary>
    /// Folder synchronisatie beheerder
    /// </summary>
    public class FolderSynchronization
    {
        /// <summary>
        /// Een pauze event die de synchronisatie kan pauzeren of weer hervatten
        /// </summary>
        protected AutoResetEvent SyncIsPaused = new(false);
        /// <summary>
        /// Een pauze event die de scan kan pauzeren of weer hervatten
        /// </summary>
        protected AutoResetEvent ScanIsPaused = new(false);
        /// <summary>
        /// Een pauze event die de folder beheer kan pauzeren of weer hervatten
        /// </summary>
        protected AutoResetEvent MonitorIsPaused = new(false);
        /// <summary>
        /// De synchronisatie taken die nog uitgevoerd moeten worden
        /// </summary>
        protected Queue<FileOperation> SyncQueue = new();
        /// <summary>
        /// De Scan taken die nog uitgevoerd moeten worden
        /// </summary>
        protected Queue<FolderSynchronizationScannerItem> ScanQueue = new();
        /// <summary>
        /// De folder monitor taken die nog uitgevoerd moeten worden
        /// </summary>
        protected Queue<FolderSynchronizationScannerItem> MonitorQueue = new();
            /// <summary>
            /// Of de synchronisatie nog bezig is
            /// </summary>
        protected bool IsRunning;
            /// <summary>
            /// Of de synchronisatie gepauzeerd is
            /// </summary>
        protected bool IsPausedSync;
            /// <summary>
            /// Of de scan gepauzeerd is
            /// </summary>
        protected bool IsPausedScan;
            /// <summary>
            /// Of de folder monitor gepauzeerd is
            /// </summary>
        protected bool IsPausedMonitor;
            /// <summary>
            /// De Taak van het synchroniseren
            /// </summary>
        protected Task SyncThread;
            /// <summary>
            /// De taak van het scannen
            /// </summary>
        protected Task ScanThread;
            /// <summary>
            /// De taak van het monitoren van de folders
            /// </summary>
        protected Task MonitorThread;

            public List<FolderSynchronizationScannerItem> ScanFolders { get; set; } =
                new List<FolderSynchronizationScannerItem>();

            /// <summary>
            /// De interval waarvan de synchronisatie moet plaatsvinden in miliseconden 
            /// </summary>
        public int Interval => Manager.DefaultSettings == null || Manager.DefaultSettings.OfflineDbSyncInterval < 500
            ? 1000
            : Manager.DefaultSettings.OfflineDbSyncInterval;

        /// <summary>
        /// De status van de synchronisatie
        /// </summary>
        public string Status;

        public bool Syncing => IsRunning;

        /// <summary>
        /// De aantal synchronisatie taken die nog openstaan
        /// </summary>
        public int QueueSyncCount
        {
            get
            {

                int count = 0;
                if (SyncQueue != null)
                {
                    lock (SyncQueue)
                    {
                        count = SyncQueue.Count;
                    }
                }

                return count;
            }
        }
        /// <summary>
        /// De aantal scan taken die nog openstaan
        /// </summary>
        public int QueueScanCount
        {
            get
            {
                var xreturn = 0;
                if (ScanQueue != null)
                {
                    lock (ScanQueue)
                    {
                        xreturn = ScanQueue.Count;
                    }
                }

                return xreturn;
            }
        }
        /// <summary>
        /// De aantal monitor taken die nog openstaan
        /// </summary>
        public int QueueMonitorCount
        {
            get
            {
                var xreturn = 0;
                if (MonitorQueue != null)
                {
                    lock (MonitorQueue)
                    {
                        xreturn = MonitorQueue.Count;
                    }
                }

                return xreturn;
            }
        }
        /// <summary>
        /// Maak een  nieuwe synchronisatie aan
        /// </summary>
        public FolderSynchronization()
        {
            try
            {
                //Maak toegang aan van de huidige rechten zodat het kopieren van de bestanden uitgevoerd kunnen worden
                GeneralLib.AddAccess(AppDomain.CurrentDomain.BaseDirectory);
            }
            catch
            {
                // ignored
            }
        }

        #region Start/Stop/Pause/Resume
        /// <summary>
        /// Start de synchronisatie
        /// </summary>
        public void Start()
        {
            if (IsRunning == false)
            {
                IsRunning = true;
                StartSyncingThread();
                StartScanningThread();
               // StartMonitorThread();
            }
        }
        /// <summary>
        /// Stop de synchronisatie
        /// </summary>
        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
            }
            lock (ScanQueue)
            {
                ScanFolders?.Clear();
                ScanQueue?.Clear();
            }
            lock (SyncQueue)
            {
                SyncQueue?.Clear();
            }
            //lock (_MonitorQueue)
            //{
            //    _MonitorQueue?.Clear();
            //}
        }
        /// <summary>
        /// Pauzeer de folder monitor
        /// </summary>
        public void PauseMonitor()
        {
            IsPausedMonitor = true;
        }
        /// <summary>
        /// Pauzeer de scan naar gewijzigde bestanden
        /// </summary>
        public void PauseScan()
        {
            IsPausedScan = true;
        }
        /// <summary>
        /// Pauzeer de synchronisatie
        /// </summary>
        public void PauseSync()
        {
            IsPausedSync = true;
        }
        /// <summary>
        /// hervat de folder monitor
        /// </summary>
        public void ResumeMonitor()
        {
            if (IsRunning)
            {
                IsPausedMonitor = false;
                MonitorIsPaused.Set();
            }
        }
        /// <summary>
        /// Hervat het scannen van gewijzigde bestanden
        /// </summary>
        public void ResumeScan()
        {
            if (IsRunning)
            {
                IsPausedScan = false;
                ScanIsPaused.Set();
            }
        }
        /// <summary>
        /// Hervat de synchronisatie
        /// </summary>
        public void ResumeSync()
        {
            if (IsRunning)
            {
                IsPausedSync = false;
                SyncIsPaused.Set();
            }
        }
        #endregion
        /// <summary>
        /// Start een taak het monitoren van de folders
        /// </summary>
        protected void StartMonitorThread()
        {
            MonitorThread = Task.Factory.StartNew(StartMonitor);
        }
        /// <summary>
        /// Start een taak van het scannen naar gewijzigde bestanden
        /// </summary>
        protected void StartScanningThread()
        {
            ScanThread = Task.Factory.StartNew(StartScanning);
        }
        /// <summary>
        /// Start een synchroniseer taak
        /// </summary>
        protected void StartSyncingThread()
        {
            SyncThread = Task.Factory.StartNew(StartSyncing);
        }

        /// <summary>
        /// Start het monitoren van de folders
        /// </summary>
        protected void StartMonitor()
        {
            while (true)
            {

                try
                {
                    if (IsPausedMonitor) MonitorIsPaused.WaitOne();
                    if (IsRunning == false || Manager.Opties == null || Manager.DefaultSettings is not
                        {
                            GebruikOfflineMetSync: true
                        })
                    {
                        break;
                    }

                    while (true)
                    {
                        FolderSynchronizationScannerItem op;
                        lock (MonitorQueue)
                        {
                            if (MonitorQueue.Count == 0) break;
                            op = MonitorQueue.Dequeue();
                        }

                        if (op != null)
                        {
                            try
                            {
                                if (Directory.Exists(op.Source) && Directory.Exists(op.Destination))
                                {
                                    lock (ScanQueue)
                                    {
                                        ScanQueue.Enqueue(op);
                                    }

                                }
                                else
                                {
                                    if (op.Monitor)
                                    {
                                        lock (MonitorQueue)
                                        {
                                            MonitorQueue.Enqueue(op);
                                        }
                                    }

                                }

                                Status = string.Empty;
                            }
                            catch (Exception ex)
                            {
                                Status = ex.Message;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            Thread.Sleep(1000);
        }

        /// <summary>
        /// Starten het scannen naar gewijzigde bestanden
        /// </summary>
        protected void StartScanning()
        {
            while (true)
            {   
                //FolderSynchronizationScannerItem[] items = null;
                try
                {
                    if (IsPausedScan) ScanIsPaused.WaitOne();
                    if (IsRunning == false || Manager.Opties == null || Manager.DefaultSettings is not
                        {
                            GebruikOfflineMetSync: true
                        })
                    {
                        Stop();
                        break;
                    }

                    //if (_SyncQueue != null)
                    //{
                    //    lock (_SyncQueue)
                    //    {
                    //        if (_SyncQueue.Count > 0) continue;
                    //    }
                    //}
                 
                    if (ScanQueue != null)
                    {
                        while (true)
                        {
                            FolderSynchronizationScannerItem op;
                            lock (ScanQueue)
                            {
                                if (ScanQueue == null || ScanQueue.Count == 0) break;
                                op = ScanQueue.Dequeue();
                            }


                            if (op != null)
                            {
                                try
                                {
                                    if (Directory.Exists(op.Source))
                                    {
                                        if (!Directory.Exists(op.Destination))
                                            Directory.CreateDirectory(op.Destination);
                                        FolderSynchronizationScanner fss =
                                            new FolderSynchronizationScanner(op.Source, op.Destination, op.Option);
                                        fss.StartFolder("", -1);
                                        if (fss.SyncCollection.Operations.Count > 0)
                                        {
                                            lock (SyncQueue)
                                            {
                                                //_SyncQueue.Clear();
                                                foreach (FileOperation fo in fss.SyncCollection.Operations)
                                                {
                                                    if (!SyncQueue.Contains(fo))
                                                        SyncQueue.Enqueue(fo);
                                                }
                                            }

                                            //await Task.Delay(fss.SyncCollection.Operations.Count * 10);
                                        }
                                    }

                                    Status = string.Empty;
                                }
                                catch (Exception ex)
                                {
                                    Status = ex.Message;
                                }

                            }

                            Thread.Sleep(Interval);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                if(ScanFolders is {Count: > 0})
                    foreach (var item in ScanFolders)
                    {
                        lock (ScanQueue)
                        {
                            ScanQueue.Enqueue(item);
                        }
                    }

                Thread.Sleep(Interval);
            }
        }
        /// <summary>
        /// Start de synchronisatie
        /// </summary>
        protected void StartSyncing()
        {
            while(IsRunning)
            {
                try
                {
                    if (IsPausedSync) SyncIsPaused.WaitOne();
                    if (IsRunning == false || Manager.Opties == null || Manager.DefaultSettings is not
                        {
                            GebruikOfflineMetSync: true
                        })
                    {
                        IsRunning = false;
                        break;
                    }
                    while (IsRunning)
                    {
                        FileOperation op;
                        lock (SyncQueue)
                        {
                            if (SyncQueue.Count == 0)
                                break;
                            op = SyncQueue.Dequeue();
                        }

                        if (op != null)
                        {
                            try
                            {
                                op.DoOperation().Wait(10000);
                                Status = string.Empty;
                            }
                            catch (Exception ex)
                            {
                                Status = ex.Message;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Thread.Sleep(Interval);
            }
        }
        /// <summary>
        /// Voeg een nieuwe scan folder aan
        /// </summary>
        /// <param name="item">Een folder object dat toegevoegd moet worden</param>
        public void AddScan(FolderSynchronizationScannerItem item)
        {
            if (item == null || ScanQueue == null) return;
            lock (ScanFolders)
            {
                if (!ScanFolders.Any(x =>
                    string.Equals(x.Destination, item.Destination, StringComparison.CurrentCultureIgnoreCase)))
                    ScanFolders.Add(item);
            }
        }

        public void ClearScans()
        {
            if (ScanFolders == null) return;
            lock (ScanFolders)
            {
                ScanFolders.Clear();
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
