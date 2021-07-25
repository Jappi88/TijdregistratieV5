using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Controls
{
    public partial class RecentGereedMeldingenUI : UserControl
    {
        public bool IsLoaded { get; private set; }
        public bool IsSyncing { get; private set; }
        TimeSpan _TijdTerug = TimeSpan.FromDays(1);
        public TimeSpan TijdTerug
        {
            get => _TijdTerug;
            set
            {
                _TijdTerug = value;
                UpdateTime();
            } 
        }

        public int SyncInterval { get; set; } = 10000;//10 seconden
        private bool _Enablesync;
        public bool EnableSync
        {
            get => _Enablesync;
            set
            {
                _Enablesync = value;
                if(value)
                    SyncBewerkingen();
            }
        }

        public ObjectListView ProductieLijst => productieListControl1.ProductieLijst;

        public int ItemCount => productieListControl1.ProductieLijst.Items.Count;
        private TijdEntry Bereik { get; set; }
        public RecentGereedMeldingenUI()
        {
            InitializeComponent();
            productieListControl1.ValidHandler = IsAllowed;
            productieListControl1.ItemCountChanged += ProductieListControl1_ItemCountChanged;
            productieListControl1.RemoveCustomItemIfNotValid = true;
        }

        private void ProductieListControl1_ItemCountChanged(object sender, EventArgs e)
        {
            OnItemCountChanged();
        }

        public void LoadBewerkingen()
        {
            if (IsLoaded) return;
            productieListControl1.DetachEvents();
            if (Manager.Opties != null)
            {
                TijdTerug = Manager.Opties.LastRecentGereedTime;
                xdagenvalue.SetValue((decimal)Manager.Opties.LastRecentGereedTime.TotalDays);
            }
            Manager.OnSettingsChanged -= Manager_OnSettingsChanged;
            InitRecenteGereedmeldingen();
            productieListControl1.InitEvents();
            if (EnableSync)
                SyncBewerkingen();
            
            Manager.OnSettingsChanged += Manager_OnSettingsChanged;
        }

        private void Manager_OnSettingsChanged(object instance, Rpm.Settings.UserSettings settings, bool reinit)
        {
            if (settings != null)
            {
                try
                {
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        bool changed = TijdTerug != settings.LastRecentGereedTime;
                        _TijdTerug = settings.LastRecentGereedTime;
                        xdagenvalue.SetValue((decimal)Manager.Opties.LastRecentGereedTime.TotalDays);
                        //var dt = DateTime.Now;
                        //xhourvalue.SetValue(new DateTime(dt.Year, dt.Month, dt.Day, settings.LastRecentGereedTime.Hours,
                        //    settings.LastRecentGereedTime.Minutes, 0));
                        if (changed)
                            InitRecenteGereedmeldingen();
                    }));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void StopView()
        {
            productieListControl1.DetachEvents();
            productieListControl1.InitProductie(new List<Bewerking>(),true);
            IsSyncing = false;
            IsLoaded = false;
        }

        private void SyncBewerkingen()
        {
            if (IsSyncing || !EnableSync || IsDisposed || !IsLoaded) return;
            IsSyncing = true;
            Task.Run(() =>
            {
                this.BeginInvoke(new MethodInvoker(async () =>
                {
                    while (IsSyncing && EnableSync && !IsDisposed && IsLoaded)
                    {
                        try
                        {
                            UpdateTime();
                            //var bewerkingen = await Manager.Database.GetBewerkingen(ViewState.Gereed, true, Bereik, IsAllowed);

                            var xbws = productieListControl1.ProductieLijst.Objects?.Cast<Bewerking>().ToList();
                            if (xbws != null && xbws.Count > 0)
                            {
                                var states = productieListControl1.GetCurrentViewStates();
                                for (int i = 0; i < xbws.Count; i++)
                                {
                                    productieListControl1.UpdateBewerking(xbws[i]?.Parent, xbws, states, null);
                                    if (!IsSyncing || !EnableSync || IsDisposed || !IsLoaded) break;
                                }
                            }

                            await Task.Delay(SyncInterval);
                            if (!IsSyncing || !EnableSync || IsDisposed || !IsLoaded) break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }));
            });
        }

        private void UpdateTime()
        {
            Bereik = new TijdEntry(DateTime.Now.Subtract(TijdTerug), DateTime.Now, null);
        }

        private Task InitRecenteGereedmeldingen()
        {
            return Task.Run(async () =>
            {
                if (_iswaiting) return;
                UpdateTime();
                DoWait();
                var bewerkingen = await Manager.Database.GetBewerkingen(ViewState.Gereed, true, Bereik, IsAllowed);
                productieListControl1.InitProductie(bewerkingen, true);
                SyncBewerkingen();
                StopWait();
                IsLoaded = true;
            });
        }

        private bool _iswaiting;
        private async void DoWait()
        {
            if (_iswaiting) return;
            _iswaiting = true;
            var value = "Producties laden";
            int cur = 0;
            while (_iswaiting && !IsDisposed)
            {
                int xcur = cur++;
                xstatus.Invoke(new MethodInvoker(() =>
                {
                    xstatus.Text = value.PadRight(value.Length + xcur,'.');
                    xstatus.Invalidate();
                }));
                await Task.Delay(400);
            }

            _iswaiting = false;
        }

        private void StopWait()
        {
            _iswaiting = false;
            UpdateStatus();
        }

        private bool IsAllowed(object value, string filter)
        {
            UpdateTime();
            if (value is ProductieFormulier form)
            {
                if (form.Bewerkingen == null || form.Bewerkingen.Length == 0) return false;
                return form.Bewerkingen.Any(x => IsAllowed(x, filter));
            }
            if (value is Bewerking bew)
            {
                if (string.IsNullOrEmpty(bew.ProductieNr) ||
                    bew.State != ProductieState.Gereed || !bew.IsAllowed(filter)) return false;
                
                if ((bew.DatumGereed < Bereik.Start || bew.DatumGereed > Bereik.Stop)) return false;
                return true;
            }

            return false;
        }

        public event EventHandler ItemCountChanged;

        private void UpdateStatus()
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                var bws = productieListControl1.Bewerkingen;
                int count = bws?.Count ?? 0;
                var x1 = count == 1 ? "Gereedmelding" : "Gereedmeldingen";
                var uur = TijdTerug.Hours;
                var dagen = (int) TijdTerug.TotalDays;
                var weken = dagen >= 7 ? (int)(dagen / 7) : 0;
                dagen = dagen - (weken * 7);
                var weekt = weken == 1 ? "week" : "weken";
                var dagt = dagen == 1 ? "dag" : "dagen";
                var xvals = new List<string>();
                if (weken > 0)
                    xvals.Add($"{weken} {weekt}");
                if (dagen > 0)
                    xvals.Add($"{dagen} {dagt}");
                if(uur > 0 || xvals.Count == 0)
                        xvals.Add($"{uur} uur");
                var xmsg = $"{count} {x1} van de afgelopen ";
                for (int i = 0; i < xvals.Count; i++)
                {
                   
                    if (i > 0)
                    {
                        if (i == xvals.Count - 1)
                            xmsg += $" en {xvals[i]}";
                        else xmsg += $", {xvals[i]}";
                    }
                    else
                        xmsg += xvals[i];

                }
                xstatus.Text = xmsg + ".";
            }));
        }

        protected virtual void OnItemCountChanged()
        {
            UpdateStatus();
            ItemCountChanged?.Invoke(this, EventArgs.Empty);
        }

        private async void xupdatetijdb_Click(object sender, EventArgs e)
        {
           // var tijd = xhourvalue.Value.TimeOfDay;
           var tijd = TimeSpan.FromDays((double) xdagenvalue.Value);
            if (Manager.Opties != null)
            {
                double days = Math.Round(tijd.TotalDays, 2);
                string x1 = days <= 1.00d ? "dag" : "dagen";
                Manager.Opties.LastRecentGereedTime = tijd;
                await Manager.Opties.Save($"Recente gereedmeldingen periode gewijzigd naar {days} {x1}",false,true);
            }
            else
            {
                _TijdTerug = tijd;
                await InitRecenteGereedmeldingen();
            }
        }
    }
}
