using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;

namespace Rpm.SqlLite
{
    public class DatabaseUpdater
    {
        internal CancellationTokenSource _cancelation = new();

        private BackgroundWorker _worker;
        public UserSettings DefaultSettings { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsBussy { get; private set; }

        private void InitWorker(UserSettings settings)
        {
            if (_worker != null)
                return;
            DefaultSettings = settings ?? UserSettings.GetDefaultSettings();
            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += _worker_DoWork;
            _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsRunning = false;
        }

        private async void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            IsRunning = true;

            while (DefaultSettings?.DbUpdateEntries != null && DefaultSettings.DbUpdateEntries.Any(x => x.AutoUpdate))
            {
                if (_worker.CancellationPending)
                    break;
                if (IsBussy) continue;
                var entries = DefaultSettings.DbUpdateEntries.Where(x => x.AutoUpdate).ToList();
                foreach (var ent in entries)
                {
                    await UpdateEntryAsync(ent, _cancelation, null);
                    if (_worker.CancellationPending)
                        break;
                }

                IsRunning = true;
                Thread.Sleep(DefaultSettings.DbUpdateInterval * 60 * 1000);
            }

            IsRunning = false;
            _worker.Dispose();
        }

        public Task<int> UpdateEntryAsync(DatabaseUpdateEntry entry, CancellationTokenSource canceltoken,
            ProgressChangedHandler progres)
        {
            return Task.Run(async () =>
            {
                if (entry == null)
                    return 0;
                var xreturn = 0;
                IsBussy = true;
                xreturn = await Manager.Database.UpdateDbFromDb(entry, canceltoken, progres);
                IsBussy = false;
                if (xreturn > 0)
                    OnDbEntryUpdated(entry, xreturn);
                return xreturn;
            });
        }

        public async void UpdateStartupDbs()
        {
            if (IsBussy)
                return;
            if (DefaultSettings == null) return;
            IsBussy = true;
            foreach (var entry in DefaultSettings.DbUpdateEntries.Where(x => x.UpdateMetStartup))
            {
                var count = 0;
                count = await Manager.Database.UpdateDbFromDb(entry, new CancellationTokenSource());
                if (count > 0)
                    OnDbEntryUpdated(entry, count);
            }

            IsBussy = false;
        }

        public void Start(UserSettings settings = null)
        {
            if (IsRunning)
                return;
            InitWorker(settings);
            try
            {
                _worker?.RunWorkerAsync();
            }
            catch
            {
            }
        }

        public void Stop()
        {
            _worker.CancelAsync();
            _cancelation.Cancel();
        }

        public event DbEntryUpdatedHandler DbEntryUpdated;

        public void OnDbEntryUpdated(DatabaseUpdateEntry entry, int count)
        {
            try
            {
                entry.LastUpdated = DateTime.Now;
                DefaultSettings.SaveAsDefault();
            }
            catch
            {
            }

            DbEntryUpdated?.Invoke(entry, count);
        }
    }

    public delegate void DbEntryUpdatedHandler(DatabaseUpdateEntry entry, int count);
}