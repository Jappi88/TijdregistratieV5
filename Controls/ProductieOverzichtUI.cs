using ProductieManager.Properties;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controls
{
    public partial class ProductieOverzichtUI : UserControl
    {
        public ProductieOverzichtUI()
        {
            InitializeComponent();
            
        }

        public void InitUI()
        {
            imageList1.Images.Add(Resources.iconfinder_technology);
            LoadList();
            InitOverzicht();
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
        }

        public void CloseUI()
        {
            imageList1.Images.Clear();
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;

        }

        private readonly List<Bewerking> Bewerkingen = new();
        private readonly Dictionary<string, bool> WerkPlekken = new Dictionary<string, bool>();

        private bool _iswaiting = false;

        private void StartWaitUI()
        {
            if (_iswaiting) return;
            _iswaiting = true;
            Task.Run(async () =>
            {

                try
                {
                    bool valid = false;
                    this.Invoke(new Action(() => valid = !this.IsDisposed));
                    if (!valid) return;
                    if (InvokeRequired)
                        this.Invoke(new Action(() => xloadinglabel.Visible = true));
                    else this.Visible = true;
                    var cur = 0;
                    var xwv = "Overzicht aanmaken.";
                    //var xcurvalue = xwv;
                    var tries = 0;
                    while (_iswaiting && tries < 200)
                    {
                        if (cur > 5) cur = 0;
                        var curvalue = xwv.PadRight(xwv.Length + cur, '.');
                        //xcurvalue = curvalue;
                        this.Invoke(new Action(() =>
                        {
                            xloadinglabel.Text = curvalue;
                            xloadinglabel.Invalidate();
                        }));
                        Application.DoEvents();

                        await Task.Delay(350);
                        Application.DoEvents();
                        tries++;
                        cur++;
                        this.Invoke(new Action(() => valid = !this.IsDisposed));
                        if (!valid) break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            });
        }

        private void StopWaitUI()
        {
          _iswaiting = false;
            if (InvokeRequired)
                this.Invoke(new Action(() => xloadinglabel.Visible = false));
            else xloadinglabel.Visible = false;
        }

        private Dictionary<string, bool> GetWerkplekken()
        {
           // List<string> plekken = new List<string>();
            try
            {
                if (Manager.Opties == null) return WerkPlekken;
                if (Manager.Opties.ToonAlles)
                {
                    var plekken = Manager.BewerkingenLijst.GetAlleWerkplekken(true);
                    foreach (var plek in plekken)
                    {
                        if (!WerkPlekken.ContainsKey(plek))
                            WerkPlekken.Add(plek, true);
                    }

                    return WerkPlekken;
                }
                if (Manager.Opties.ToonAllesVanBeide || Manager.Opties.ToonVolgensAfdelingen)
                {
                    if (Manager.Opties.Afdelingen is {Length: > 0})
                    {
                        foreach (var s in Manager.Opties.Afdelingen)
                        {
                            var xpleks = Manager.BewerkingenLijst.GetWerkplekken(s);
                            foreach(var plek in xpleks)
                                if (!WerkPlekken.ContainsKey(plek))
                                    WerkPlekken.Add(plek, true);
                        }
                    }
                }

                if (Manager.Opties.ToonAllesVanBeide || Manager.Opties.ToonVolgensBewerkingen)
                {
                    if (Manager.Opties.Bewerkingen is {Length: > 0})
                    {
                        foreach (var s in Manager.Opties.Bewerkingen)
                        {
                            var ent = Manager.BewerkingenLijst.GetEntry(s);
                            if (ent == null || ent.WerkPlekken == null || ent.WerkPlekken.Count == 0) continue;
                            foreach (var plek in ent.WerkPlekken)
                            {
                                if (!WerkPlekken.ContainsKey(plek))
                                    WerkPlekken.Add(plek, true);
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return WerkPlekken;
        }

        private List<string> GetWerkplekkenFromList()
        {
            List<string> xret = new List<string>();
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(() =>
                    xret = xwerkpleklist.Items.Cast<ListViewItem>().Where(x => x.Checked).Select(x => x.Text)
                        .ToList()));
            else xret = xwerkpleklist.Items.Cast<ListViewItem>().Where(x => x.Checked).Select(x => x.Text).ToList();
            return xret;
        }

        private List<string> GetWerkplekkenFromView()
        {
            var xreturn = new List<string>();
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(() => xreturn = xGetWerkplekkenFromView()));
                else xreturn = xGetWerkplekkenFromView();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xreturn;
        }

        private List<string> xGetWerkplekkenFromView()
        {
            var xreturn = new List<string>();
            try
            {
                var xwps = xcontrolpanel.Controls.Cast<WerkPlekInfoUI>().ToList();
                xreturn = xwps.Select(x => x.Werkplek).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xreturn;
        }

        private List<string> GetEmptyWerkplekkenFromView()
        {
            var xreturn = new List<string>();
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(() => xreturn = xGetEmptyWerkplekkenFromView()));
                else xreturn = xGetEmptyWerkplekkenFromView();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xreturn;
        }

        private List<string> xGetEmptyWerkplekkenFromView()
        {
            var xreturn = new List<string>();
            try
            {
                var xwps = xcontrolpanel.Controls.Cast<WerkPlekInfoUI>().ToList();
                xreturn = xwps.Where(x=> x.Huidig == null).Select(x => x.Werkplek).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xreturn;
        }

        private List<string> GetWerkplekkenFromView(string name, string bewnaam)
        {
            var xreturn = new List<string>();
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(() => xreturn = xGetWerkplekkenFromView(name, bewnaam)));
                else xreturn = xGetWerkplekkenFromView(name, bewnaam);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xreturn;
        }

        private List<string> xGetWerkplekkenFromView(string name, string bewnaam)
        {
            var xreturn = new List<string>();
            try
            {
                var xwps = xcontrolpanel.Controls.Cast<WerkPlekInfoUI>().ToList();
                xreturn = xwps.Where(x => string.Equals(x.Werkplek, name, StringComparison.CurrentCultureIgnoreCase) && (string.Equals(x.Huidig?.Path, bewnaam, StringComparison.CurrentCultureIgnoreCase))).Select(x => x.Werkplek).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xreturn;
        }

        public int IsSameProduct(string artikelnr1, string artikelnr2)
        {
            if (artikelnr1 == null && artikelnr2 == null) return 0;
            if (artikelnr1 == null || artikelnr2 == null) return 0;
            if (string.Equals(artikelnr1, artikelnr2, StringComparison.CurrentCultureIgnoreCase))
                return artikelnr2.Length;
            //kijk voor de aantal overeengekomen getallen.
            int xgelijk = 0;
            for (int i = 0; i < artikelnr2.Length; i++)
            {
                if (i >= artikelnr1.Length) break;
                if (artikelnr1.ToLower()[i] == artikelnr2.ToLower()[i])
                    xgelijk++;
                else break;
            }

            return xgelijk;
        }

        public struct WerkVolgorde
        {
            public string Name;
            public Bewerking Huidig;
            public Bewerking Volgende;
        }

        public Task<List<WerkVolgorde>> GetOverzicht()
        {
            return Task.Run(async () =>
            {
                var xreturn = new List<WerkVolgorde>();
                try
                {
                    List<string> plekken = GetWerkplekkenFromList();
                    var bws = await Manager.GetBewerkingen(new ViewState[] {ViewState.Gestopt, ViewState.Gestart},
                        true);
                    var gestart = bws.Where(x => x.State == ProductieState.Gestart).OrderBy(x=> x.VerwachtDatumGereed()).ToList();
                    bws = bws.Where(x => x.State == ProductieState.Gestopt).Distinct(new BewerkingDistinctComparer()).ToList();
                    List<Bewerking> xremove = new List<Bewerking>();
                    if (gestart.Count > 0)
                    {
                        foreach (var bw in gestart)
                        {
                            foreach (var wp in bw.WerkPlekken)
                            {
                                try
                                {
                                    if (!plekken.Any(x =>
                                            string.Equals(x, wp.Naam, StringComparison.CurrentCultureIgnoreCase)))
                                        continue;
                                    var xbws = GetBewerkingByWerkplek(bws, wp.Naam);
                                    if (xbws.Count == 0) continue;
                                    plekken.Remove(wp.Naam);
                                    Bewerking volgende = xbws.FirstOrDefault(x =>
                                        x.VerwachtDatumGereed() > x.LeverDatum ||
                                        string.Equals(x.ArtikelNr, wp.ArtikelNr,
                                            StringComparison.CurrentCultureIgnoreCase) ||
                                        IsSameProduct(x.ArtikelNr, wp.ArtikelNr) > 5);
                                    volgende ??= xbws[0];
                                    xreturn.Add(new WerkVolgorde() {Name = wp.Naam, Huidig = bw, Volgende = volgende});
                                    xremove.Add(volgende);

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }

                            foreach (var xrem in xremove)
                            {
                                bws.RemoveAll(x =>
                                    string.IsNullOrEmpty(x.ArtikelNr) || string.IsNullOrEmpty(x.ProductieNr) ||
                                    string.Equals(x.ArtikelNr, xrem.ArtikelNr,
                                        StringComparison.CurrentCultureIgnoreCase) ||
                                    string.Equals(x.ProductieNr, xrem.ProductieNr,
                                        StringComparison.CurrentCultureIgnoreCase)
                                );
                            }
                        }
                    }

                    try
                    {
                        if (plekken.Count > 0 && bws.Count > 0)
                        {
                            var xtmp = new List<WerkVolgorde>();
                            //for (int i = 0; i < bws.Count; i++)
                            //{
                            //    var bw = bws[i];
                            //    var xaanbev = await bw.GetAanbevolenWerkplekken();
                            //    if (xaanbev is {Count: > 0})
                            //    {
                            //        foreach (var xbev in xaanbev)
                            //        {
                            //            var xpl = plekken.FirstOrDefault(x =>
                            //                string.Equals(x, xbev.Key, StringComparison.CurrentCultureIgnoreCase));
                            //            if (xpl != null)
                            //            {
                            //                xtmp.Add(new WerkVolgorde() {Name = xpl, Huidig = null, Volgende = bw});
                            //                bws.RemoveAt(i--);
                            //                plekken.Remove(xpl);
                            //                break;
                            //            }
                            //        }
                            //    }
                            //}

                            foreach (var plek in plekken)
                            {
                                if (bws.Count == 0) break;
                                var xbws = GetBewerkingByWerkplek(bws, plek);
                                if (xbws.Count == 0) continue;
                                var volgende = xbws[xbws.Count - 1];
                                bws.Remove(volgende);
                                if (!string.IsNullOrEmpty(volgende.ArtikelNr))
                                {
                                    bws.RemoveAll(x =>
                                        string.IsNullOrEmpty(x.ArtikelNr) || string.IsNullOrEmpty(x.ProductieNr) ||
                                        string.Equals(x.ArtikelNr, volgende.ArtikelNr,
                                            StringComparison.CurrentCultureIgnoreCase) ||
                                        string.Equals(x.ProductieNr, volgende.ProductieNr,
                                            StringComparison.CurrentCultureIgnoreCase));
                                }

                                xtmp.Add(new WerkVolgorde() {Name = plek, Huidig = null, Volgende = volgende});
                            }

                            if (xtmp.Count > 0)
                                xreturn.AddRange(xtmp.OrderBy(x => x.Name));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return xreturn.OrderBy(x=> x.Huidig?.VerwachtLeverDatum??x.Volgende?.VerwachtLeverDatum).ToList();
            });
        }

        private bool HasChanged(ProductieFormulier prod, bool update)
        {
            try
            {
                if (prod?.Bewerkingen == null || Bewerkingen.Count == 0) return false;
                bool xchanged = false;
                foreach (var bew in prod.Bewerkingen)
                {
                    var index = Bewerkingen.IndexOf(bew);
                    xchanged = index > -1 && Bewerkingen[index].State != bew.State;
                    if (update && index > -1)
                    {
                        Bewerkingen[index] = bew;
                    }

                    if (!xchanged)
                    {
                        //if (bew.State == ProductieState.Gestart)
                        //{

                        //}
                        foreach (var wp in bew.WerkPlekken)
                        {
                            var wps = GetWerkplekkenFromView(wp.Naam, null);
                            xchanged = wps.Count > 0 && bew.State == ProductieState.Gestart;
                            if (xchanged)
                                return true;
                            wps = GetWerkplekkenFromView(wp.Naam, bew.Path);
                            xchanged = wps.Count > 0 && bew.State == ProductieState.Gestopt;
                            if (xchanged)
                                return true;
                        }
                    }
                    else return true;
                }

                return xchanged;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async void InitOverzicht()
        {

            await Task.Run(async () =>
            {
                try
                {
                    if (this.IsDisposed || this.Disposing || Manager.Opties == null || _iswaiting) return;
                    StartWaitUI();
                    var items = await GetOverzicht();
                    this.Invoke(new Action(() =>
                    {
                        xcontrolpanel.SuspendLayout();
                        var xcurpos = xcontrolpanel.VerticalScroll.Value;
                        xcontrolpanel.Controls.Clear();
                        lock (Bewerkingen)
                        {
                            Bewerkingen.Clear();
                            foreach (var x in items)
                            {
                                var ui = new WerkPlekInfoUI();
                                ui.Dock = DockStyle.Top;
                                ui.Init(x.Name, x.Huidig, x.Volgende);
                                xcontrolpanel.Controls.Add(ui);
                                ui.BringToFront();
                                if (x.Huidig != null)
                                    Bewerkingen.Add(x.Huidig);
                                if (x.Volgende != null)
                                    Bewerkingen.Add(x.Volgende);
                            }
                        }

                        for (int i = 0; i < 4; i++)
                            xcontrolpanel.VerticalScroll.Value = xcurpos;
                        xcontrolpanel.ResumeLayout(true);
                        if (items.Count == 0)
                        {
                            xloadinglabel.Text = "Geen Producties Beschikbaar";
                            xloadinglabel.Visible = true;
                            xloadinglabel.BringToFront();
                        }
                        else
                        {
                            xloadinglabel.Visible = false;
                            xloadinglabel.Text = "Overzicht aanmaken...";
                        }

                        xloadinglabel.Invalidate();
                    }));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                StopWaitUI();
            });

        }

        private List<Bewerking> GetBewerkingByWerkplek(List<Bewerking> bewerkingen, string werkplek)
        {
            List<Bewerking> xreturn = new List<Bewerking>();
            if (string.IsNullOrEmpty(werkplek)) return xreturn;
            try
            {
                foreach (var bw in bewerkingen)
                {
                    var wps = Manager.BewerkingenLijst.GetEntry(bw.Naam);
                    if (wps?.WerkPlekken != null &&
                        wps.WerkPlekken.Any(x => string.Equals(x, werkplek, StringComparison.CurrentCultureIgnoreCase)))
                        xreturn.Add(bw);

                }

                xreturn = xreturn.OrderBy(x => x.LeverDatum).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return xreturn.OrderBy(x => x.VerwachtDatumGereed()).ToList();
        }

        private bool _loadinglist = false;
        private void LoadList()
        {
            try
            {
                var wps = GetWerkplekken();
                _loadinglist = true;
                xwerkpleklist.Items.Clear();
                xwerkpleklist.BeginUpdate();
                    foreach (var wp in wps)
                    {
                        if (!xsearchbox.Text.Trim().ToLower().StartsWith("zoeken") &&
                            !string.IsNullOrEmpty(xsearchbox.Text.Trim()))
                        {
                            var crits = xsearchbox.Text.Trim().ToLower().Split(';');
                            if (crits.Length > 0)
                            {
                                bool valid = false;
                                foreach (var crit in crits)
                                {
                                    if (!string.IsNullOrEmpty(crit) && wp.Key.Trim().ToLower().Contains(crit.Trim()))
                                    {
                                        valid = true;
                                        break;
                                    }
                                }

                                if (!valid)
                                    continue;
                            }
                        }

                        var lv = new ListViewItem(wp.Key);
                        lv.Tag = wp;
                        lv.ImageIndex = 0;
                        lv.Checked = wp.Value;
                        xwerkpleklist.Items.Add(lv);
                    }

                    xwerkpleklist.EndUpdate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _loadinglist = false;
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            try
            {
                if (changedform?.Bewerkingen == null ||_iswaiting || !HasChanged(changedform,true)) return;
                this.BeginInvoke(new Action(InitOverzicht));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitOverzicht();
        }

        private void selecteerAllesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in xwerkpleklist.Items)
            {
                if (item is ListViewItem lv)
                    lv.Checked = true;
            }
        }

        private void deSelecteerAllesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in xwerkpleklist.Items)
            {
                if (item is ListViewItem lv)
                    lv.Checked = false;
            }
        }

        private void xsearchbox_TextChanged(object sender, EventArgs e)
        {
            if (!xsearchbox.Text.Trim().ToLower().StartsWith("zoeken..."))
            {
                LoadList();
            }
        }

        private void xsearchbox_Enter(object sender, EventArgs e)
        {
            if (xsearchbox.Text.Trim().ToLower().StartsWith("zoeken..."))
                xsearchbox.Text = "";
        }

        private void xsearchbox_Leave(object sender, EventArgs e)
        {
            if (xsearchbox.Text.Trim() == "")
                xsearchbox.Text = "Zoeken...";
        }

        private void xwerkpleklist_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (_loadinglist) return;
            var xitem = e.Item.Text;
            if (WerkPlekken.ContainsKey(xitem))
                WerkPlekken[xitem] = e.Item.Checked;
        }
    }
}
