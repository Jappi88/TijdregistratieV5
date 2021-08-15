using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Controls
{
    public partial class RecentGereedMeldingenUI : UserControl
    {
        public bool IsLoaded { get; private set; }
        public bool IsSyncing { get; private set; }
        //TimeSpan _TijdTerug = TimeSpan.FromDays(1);
        //public TimeSpan TijdTerug
        //{
        //    get => _TijdTerug;
        //    set
        //    {
        //        _TijdTerug = value;
        //        UpdateTime();
        //    } 
        //}

        private DateTime _LastSynced;
       // public int SyncInterval { get; set; } = 300000;//5 min
        //public bool EnableSync
        //{
        //    get => _Enablesync;
        //    set
        //    {
        //        _Enablesync = value;
        //        if(value)
        //            SyncBewerkingen();
        //    }
        //}

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

        private void UpdateTijdPeriode(bool change)
        {
            if (InvokeRequired)
                this.Invoke(new MethodInvoker(()=> xUpdateTijdPeriode(change)));
            else xUpdateTijdPeriode(change);
        }

        private void xUpdateTijdPeriode(bool change)
        {
            if (Manager.Opties != null && change)
            {
                if (Manager.Opties.LastGereedStart.IsDefault())
                {
                    Manager.Opties.LastGereedStart = DateTime.Now.Subtract(TimeSpan.FromDays(1));
                }
                xvanafgereed.SetValue(Manager.Opties.LastGereedStart);
                xtotgereed.SetValue(Manager.Opties.LastGereedStop);
                xvanafgereed.Checked = Manager.Opties.UseLastGereedStart;
                xtotgereed.Checked = Manager.Opties.UseLastGereedStop;
            }
            Bereik = new TijdEntry()
            { Start = Manager.Opties.LastGereedStart, Stop = xtotgereed.Checked ? xtotgereed.Value : DateTime.Now };
        }

        public void LoadBewerkingen()
        {
            if (IsLoaded) return;
            productieListControl1.DetachEvents();
            UpdateTijdPeriode(true);
            Manager.OnSettingsChanged -= Manager_OnSettingsChanged;
            Manager.OnSettingsChanged += Manager_OnSettingsChanged; 
            Manager.FilterChanged -= Manager_FilterChanged;
            Manager.FilterChanged += Manager_FilterChanged;
            InitRecenteGereedmeldingen();
            productieListControl1.InitEvents();           
            
        }

        private void Manager_FilterChanged(object sender, EventArgs e)
        {
            UpdateGereedProducties();
        }

        private void Manager_OnSettingsChanged(object instance, Rpm.Settings.UserSettings settings, bool reinit)
        {
            UpdateGereedProducties();
        }

        private void UpdateGereedProducties()
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(() =>
                            {
                                //EnableSync = Manager.Opties.AutoGereedSync;
                                //bool changed = Bereik.Start != Manager.Opties.LastGereedStart || (!Manager.Opties.LastGereedStop.IsDefault() && Manager.Opties.LastGereedStop != Bereik.Stop);
                                UpdateTijdPeriode(true);
                                //var dt = DateTime.Now;
                                //xhourvalue.SetValue(new DateTime(dt.Year, dt.Month, dt.Day, settings.LastRecentGereedTime.Hours,
                                //    settings.LastRecentGereedTime.Minutes, 0));
                                //if (changed)
                                InitRecenteGereedmeldingen();
                            }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void StopView()
        {
            productieListControl1.DetachEvents();
            productieListControl1.InitProductie(new List<Bewerking>(),false,true,false);
            IsSyncing = false;
            IsLoaded = false;
        }

        private void SyncBewerkingen()
        {
            if (IsSyncing || Manager.Opties == null || !Manager.Opties.AutoGereedSync|| IsDisposed || !IsLoaded) return;
            IsSyncing = true;
            Task.Run(async () =>
            {
              
                while (IsSyncing && Manager.Opties != null && Manager.Opties.AutoGereedSync && !IsDisposed && IsLoaded)
                {
                    try
                    {
                        if (_LastSynced.AddMilliseconds(Manager.Opties.GereedSyncInterval) > DateTime.Now)
                        {
                            await Task.Delay(Manager.Opties.GereedSyncInterval);

                            if (!IsSyncing || !Manager.Opties.AutoGereedSync || IsDisposed || Disposing || !IsLoaded) break;

                           
                        }
                        else await InitRecenteGereedmeldingen();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            });
        }

        private void UpdateTime()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(xUpdateTime));
            else xUpdateTime();
        }

        private void xUpdateTime()
        {
            var xstop = DateTime.Now;
            if (xtotgereed.Checked)
                xstop = xtotgereed.Value;
            var xstart = DateTime.Now.Subtract(TimeSpan.FromDays(1));
            if (xvanafgereed.Checked)
                xstart = xvanafgereed.Value;
            Bereik = new TijdEntry(xstart, xstop, null);
            if (Manager.Opties != null)
            {
                Manager.Opties.UseLastGereedStart = xvanafgereed.Checked;
                Manager.Opties.UseLastGereedStop = xtotgereed.Checked;
                Manager.Opties.LastGereedStart = xstart;
                if (xtotgereed.Checked)
                    Manager.Opties.LastGereedStop = xstop;
                else Manager.Opties.LastGereedStop = new DateTime();
            }
        }

        private Task InitRecenteGereedmeldingen()
        {
            return Task.Run(async () =>
            {
                if (_iswaiting) return;
                try
                {
                    DoWait();
                    //productieListControl1.SetWaitUI();
                    var xprodids = await Manager.GetAllProductieIDs(true);
                    productieListControl1.ValidHandler = IsAllowed;
                    var xbws = new List<Bewerking>();
                    for(int i = 0; i < xprodids.Count; i++)
                    {
                        if (IsDisposed || Disposing) return;
                        var id = xprodids[i];
                        if (string.IsNullOrEmpty(id)) continue;
                        var prod = await Manager.Database.GetProductie(id);
                        if (prod?.Bewerkingen == null) continue;
                        foreach(var bw in  prod.Bewerkingen)
                        {
                            if (IsAllowed(bw, null))
                                xbws.Add(bw);
                        }
                    }
                    productieListControl1.InitProductie(xbws,true, true,false);
                    
                    IsLoaded = true;                   
                }
                catch (Exception e)
                {

                }
                if (Manager.Opties.AutoGereedSync)
                    SyncBewerkingen();
                StopWait();
                _LastSynced = DateTime.Now;
                //productieListControl1.StopWait();
            });
        }

        private bool _iswaiting;
        private async void DoWait()
        {
            if (_iswaiting) return;
            _iswaiting = true;
            var value = "Producties laden";
            int cur = 0;
            while (_iswaiting && !IsDisposed && !Disposing)
            {
                int xcur = cur++;
                xstatus.BeginInvoke(new MethodInvoker(() =>
                {
                    if (IsDisposed || Disposing) return;
                    xstatus.Text = value.PadRight(value.Length + xcur, '.');
                    xstatus.Invalidate();
                }));
                await Task.Delay(350);
                if (cur > 5)
                    cur = 0;
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
            if (IsDisposed || Disposing) return false;
            UpdateTijdPeriode(false);
            if (value is ProductieFormulier form)
            {
                if (form.Bewerkingen == null || form.Bewerkingen.Length == 0) return false;
                return form.Bewerkingen.Any(x => IsAllowed(x, filter));
            }
            if (value is Bewerking bew)
            {
                if (string.IsNullOrEmpty(bew.ProductieNr) ||
                    bew.State != ProductieState.Gereed || !bew.IsAllowed(filter)) return false;
                
                if ((bew.DatumGereed <= Bereik.Start || bew.DatumGereed >= Bereik.Stop)) return false;
                return true;
            }

            return false;
        }

        public event EventHandler ItemCountChanged;

        private void UpdateStatus()
        {
            if (IsDisposed || Disposing) return;
            this.BeginInvoke(new MethodInvoker(() =>
            {
                if (IsDisposed || Disposing) return;
                var bws = productieListControl1.Bewerkingen;
                int count = bws?.Count ?? 0;
                var x1 = count == 1 ? "Gereedmelding" : "Gereedmeldingen";
                var tijd = Bereik.Stop < Bereik.Start? new TimeSpan() : Bereik.Stop - Bereik.Start;
                var uur = tijd.Hours;
                var min = tijd.Minutes;
                var sec = tijd.Seconds;
                var dagen = (int)tijd.TotalDays;
                var weken = dagen >= 7 ? (int)(dagen / 7) : 0;
                dagen = dagen - (weken * 7);
                var weekt = weken == 1 ? "week" : "weken";
                var dagt = dagen == 1 ? "dag" : "dagen";
                var xvals = new List<string>();
                if (weken > 0)
                    xvals.Add($"{weken} {weekt}");
                if (dagen > 0)
                    xvals.Add($"{dagen} {dagt}");
                if(uur > 0)
                        xvals.Add($"{uur} uur");
                if (min > 0)
                    xvals.Add($"{min} {(min == 1? "minuut" : "minuten")}");
                if (sec > 0 || xvals.Count == 0)
                    xvals.Add($"{sec} seconden");

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
            UpdateTime();
            UpdateTijdPeriode(true);
            await InitRecenteGereedmeldingen();
        }
    }
}
