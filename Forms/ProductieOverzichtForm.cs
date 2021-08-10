using Controls;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductieManager.Forms
{
    public partial class ProductieOverzichtForm : MetroFramework.Forms.MetroForm
    {
        public ProductieOverzichtForm()
        {
            InitializeComponent();
        }

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
                        this.BeginInvoke(new Action(() =>
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

                if (InvokeRequired)
                    this.Invoke(new Action(() => xloadinglabel.Visible = false));
                else this.Visible = false;
            });
        }

        private void StopWaitUI()
        {
            _iswaiting = false;
        }

        private List<string> GetWerkplekken()
        {
            List<string> plekken = new List<string>();
            try
            {
                if (Manager.Opties == null) return plekken;
                if (Manager.Opties.ToonAlles)
                    plekken = Manager.BewerkingenLijst.GetAlleWerkplekken();
                if (Manager.Opties.ToonAllesVanBeide || Manager.Opties.ToonVolgensAfdelingen)
                {
                    if (Manager.Opties.Afdelingen != null && Manager.Opties.Afdelingen.Length > 0)
                    {
                        foreach (var s in Manager.Opties.Afdelingen)
                        {
                            if (plekken.Any(x => string.Equals(x, s, StringComparison.CurrentCultureIgnoreCase)))
                                continue;
                            plekken.Add(s);
                        }
                    }
                }
                if (Manager.Opties.ToonAllesVanBeide || Manager.Opties.ToonVolgensBewerkingen)
                {
                    if (Manager.Opties.Bewerkingen != null && Manager.Opties.Bewerkingen.Length > 0)
                    {
                        foreach (var s in Manager.Opties.Bewerkingen)
                        {
                            var ent = Manager.BewerkingenLijst.GetEntry(s);
                            if (ent == null || ent.WerkPlekken == null || ent.WerkPlekken.Count == 0) continue;
                            foreach (var plek in ent.WerkPlekken)
                            {
                                if (plekken.Any(x => string.Equals(x, plek, StringComparison.CurrentCultureIgnoreCase)))
                                    continue;
                                plekken.Add(plek);
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return plekken;
        }

        public int IsSameProduct(string artikelnr1, string artikelnr2)
        {
            if (artikelnr1 == null && artikelnr2 == null) return 0;
            if (artikelnr1 == null || artikelnr2 == null) return 0;
            if (string.Equals(artikelnr1, artikelnr2, StringComparison.CurrentCultureIgnoreCase))
                return artikelnr2.Length;
            //kijk voor de aantal overeengekomen getallen.
            int xgelijk = 0;
            for(int i = 0; i < artikelnr2.Length; i++)
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
                    List<string> plekken = GetWerkplekken();
                    var bws = await Manager.GetBewerkingen(new ViewState[] { ViewState.Gestopt, ViewState.Gestart }, true, false);
                    var gestart = bws.Where(x => x.State == ProductieState.Gestart).ToList();
                    bws = bws.Where(x => x.State == ProductieState.Gestopt).ToList();
                    List<WerkPlekInfoUI> wpuis = new List<WerkPlekInfoUI>();
                    if (gestart.Count > 0)
                    {

                        foreach (var bw in gestart)
                        {
                            List<Bewerking> xremove = new List<Bewerking>();
                            foreach (var wp in bw.WerkPlekken)
                            {
                                try
                                {
                                    var xbws = GetBewerkingByWerkplek(bws, wp.Naam);
                                    if (xbws.Count == 0) continue;
                                    plekken.Remove(wp.Naam);
                                    Bewerking volgende = xbws.FirstOrDefault(x=> x.VerwachtDatumGereed() > x.LeverDatum ||
                                    string.Equals(x.ArtikelNr, wp.ArtikelNr, StringComparison.CurrentCultureIgnoreCase) ||
                                    IsSameProduct(x.ArtikelNr,wp.ArtikelNr) > 4);
                                    volgende ??= xbws[xbws.Count - 1];
                                    xreturn.Add(new WerkVolgorde() { Name = wp.Naam, Huidig = bw, Volgende = volgende });
                                    xremove.Add(volgende);
                                    if (!string.IsNullOrEmpty(volgende.ArtikelNr))
                                    {
                                        bws.RemoveAll(x => !string.IsNullOrEmpty(x.ArtikelNr) &&
                                        string.Equals(x.ArtikelNr, volgende.ArtikelNr, StringComparison.CurrentCultureIgnoreCase));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                        }
                    }

                    try
                    {
                        if (plekken.Count > 0 && bws.Count > 0)
                        {
                            var xtmp = new List<WerkVolgorde>();
                            foreach (var plek in plekken)
                            {
                                if (bws.Count == 0) break;
                                var xbws = GetBewerkingByWerkplek(bws, plek);
                                if (xbws.Count == 0) continue;
                                var volgende = xbws[xbws.Count - 1];
                                bws.Remove(volgende);
                                if (!string.IsNullOrEmpty(volgende.ArtikelNr))
                                {
                                    bws.RemoveAll(x => !string.IsNullOrEmpty(x.ArtikelNr) &&
                                    string.Equals(x.ArtikelNr, volgende.ArtikelNr, StringComparison.CurrentCultureIgnoreCase));
                                }
                                xtmp.Add(new WerkVolgorde() { Name = plek, Huidig = null, Volgende = volgende });
                            }
                            if(xtmp.Count > 0)
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
                return xreturn;
            });
        }

        public async void InitOverzicht()
        {

            await Task.Factory.StartNew(async () =>
            {
                try
                {
                    if (this.IsDisposed || this.Disposing || Manager.Opties == null) return;
                    StartWaitUI();
                    this.Invoke(new Action(() =>
                    {
                        xcontrolpanel.SuspendLayout();
                        xcontrolpanel.Controls.Clear();
                    }));

                    var items = await GetOverzicht();
                    if (items.Count == 0) return;
                    this.Invoke(new Action(() =>
                    {
                        foreach (var x in items)
                        {
                            var ui = new WerkPlekInfoUI();
                            ui.Dock = DockStyle.Top;
                            ui.Init(x.Name, x.Huidig, x.Volgende);
                            xcontrolpanel.Controls.Add(ui);
                            ui.BringToFront();
                        }
                        xcontrolpanel.ResumeLayout(true);
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return xreturn.OrderBy(x => x.VerwachtDatumGereed()).ToList();
        }

        private void ProductieOverzichtForm_Shown(object sender, EventArgs e)
        {
            InitOverzicht();
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
        }

        private void ProductieOverzichtForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            try
            {
                if (changedform?.Bewerkingen == null) return;
                this.BeginInvoke(new Action(() =>
               {
                   foreach (var bw in changedform.Bewerkingen)
                   {
                       foreach (var xc in xcontrolpanel.Controls)
                       {
                           if (xc is WerkPlekInfoUI wpui)
                           {
                               if (wpui.Huidig != null && wpui.Huidig.Equals(bw))
                               {
                                   wpui.Init(wpui.Werkplek, bw, wpui.Volgende);
                               }
                               if (wpui.Volgende != null && wpui.Volgende.Equals(bw))
                               {
                                   if(bw.State == ProductieState.Gestart)
                                   {
                                       wpui.Init(wpui.Werkplek, bw, null);
                                   }
                                   else
                                   {
                                       wpui.Init(wpui.Werkplek, wpui.Huidig, bw);
                                   }
                                   
                               }
                           }
                       }
                   }
                   this.Invalidate();
               }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
