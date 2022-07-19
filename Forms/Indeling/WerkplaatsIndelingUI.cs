using Controls;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Forms
{
    public partial class WerkplaatsIndelingUI : UserControl
    {
        private System.Timers.Timer _updatetimer;
        internal WerkplekIndeling SelectedWerkplek { get; private set; }

        private List<Bewerking> _bewerkingen = new List<Bewerking>();

        internal List<Bewerking> Bewerkingen
        {
            get
            {
                try
                {
                    _bewerkingen ??= new List<Bewerking>();
                }
                catch (Exception ex)
                {
                    
                    _bewerkingen = Manager.Database.xGetAllBewerkingen(false, true, false);
                }
                return _bewerkingen;
            }
            set => _bewerkingen = value;
        }

        public WerkplaatsIndelingUI()
        {
            InitializeComponent();
        }

        public void InitUI()
        {
            productieListControl1.ValidHandler = IsAllowed;
            LoadProducties();
            InitEvents();
        }

        public void CloseUI()
        {
            _updatetimer?.Stop();
            _updatetimer?.Dispose();
            _updatetimer = null;
            DetachEvents();
            if (Manager.Opties != null)
            {
                Manager.Opties.WerkplaatsIndelingen = GetIndelingen(false).Where(x => x.Settings?.Name != null).Select(x => x.Settings).ToList();
                // Manager.Opties.xSave("Werkplaats indeling opgeslagen",false,false,false);
            }
        }

        public void InitEvents()
        {
            productieListControl1.ItemCountChanged += ProductieListControl1_ItemCountChanged;
            productieListControl1.SelectedItemChanged += ProductieListControl1_SelectedItemChanged;
            productieListControl1.SearchItems += ProductieListControl1_SearchItems;
            productieListControl1.InitEvents();

            Manager.OnFormulierDeleted += Manager_OnFormulierDeleted;
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            Manager.OnSettingsChanged += Manager_OnSettingsChanged;
        }

        private void Manager_OnSettingsChanged(object instance, Rpm.Settings.UserSettings settings, bool reinit)
        {
            try
            {
                if (reinit)
                    UpdateIndelingLayout();
            }
            catch (Exception ex)
            {

            }
        }

        public void DetachEvents()
        {
            productieListControl1.ItemCountChanged -= ProductieListControl1_ItemCountChanged;
            productieListControl1.SelectedItemChanged -= ProductieListControl1_SelectedItemChanged;
            productieListControl1.SearchItems -= ProductieListControl1_SearchItems;
            productieListControl1.CloseUI();
            Manager.OnFormulierDeleted -= Manager_OnFormulierDeleted;
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            Manager.OnSettingsChanged -= Manager_OnSettingsChanged;
        }

        public event EventHandler StatusTextChanged;
        protected virtual void OnStatusTextChanged(string text)
        {
            StatusTextChanged?.Invoke(text, EventArgs.Empty);
        }

        private void ProductieListControl1_SearchItems(object sender, EventArgs e)
        {
            if (SelectedWerkplek == null) return;
            SelectedWerkplek.Criteria = sender as string;
        }

        private void ProductieListControl1_SelectedItemChanged(object sender, EventArgs e)
        {
            SelectedWerkplek?.SetBewerking(productieListControl1.SelectedItem as Bewerking);
        }

        private void ProductieListControl1_ItemCountChanged(object sender, EventArgs e)
        {
            UpdateTitle(null);
        }

        private List<WerkplekIndeling> GetIndelingen(bool incdefault)
        {
            var xreturn = new List<WerkplekIndeling>();
            try
            {
                foreach (var xgroup in xWerkplaatsIndelingPanel.Controls.Cast<GroupBox>())
                {
                    if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is WerkplekIndeling xpers)
                    {
                        if (xpers.IsDefault() && !incdefault)
                            continue;
                        xreturn.Add(xpers);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return xreturn;
        }

        private WerkplekIndeling GetDefaultIndeling()
        {
            try
            {
                foreach (var xgroup in xWerkplaatsIndelingPanel.Controls.Cast<GroupBox>())
                {
                    if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is WerkplekIndeling xpers)
                    {
                        if (xpers.IsDefault())
                            return xpers;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        private double GetTijdOver()
        {
            var xweek = DateTime.Now.GetWeekNr();
            var rs = Manager.Opties.GetWerkRooster() ?? Rooster.StandaartRooster();
            var weekrange = Functions.GetWeekRange(xweek, DateTime.Now.Year, rs);
            return Math.Round(
                Werktijd.TijdGewerkt(new TijdEntry(DateTime.Now, weekrange.EndDate, rs), rs,
                    Manager.Opties.SpecialeRoosters).TotalHours, 2);
        }

        private string GetBaseHitmlBody(string body, string image, Size imagesize)
        {
            string ximage = $"<td width = '{imagesize.Width}' height = '{imagesize.Height}' style = 'padding: 0px 0px 0 0' >\r\n" +
                            $"<img width='{imagesize.Width}' height='{imagesize.Height}'  src = '{image}' />\r\n" +
                            $"</td>";
            var xreturn = "<html>" +
                          "    <body>" +
                          "       <style>h1, h2, h3 { color: navy; font:11pt Tahoma; }" +
                          "                    h1 { margin-bottom: .3em}" +
                          "                    h2 { margin-bottom: .3em; font:10pt Tahoma }" +
                          "                    h3 { margin-bottom: .4em }" +
                          "                    ul { margin-top: .0em }" +
                          "                    ul li {color: navy;}" +
                          "                    body { font:10pt Tahoma;}" +
                          "		            pre  { border:solid 1px gray; background-color:#eee; padding:1em }" +
                          "                    a:link { text-decoration: none; }" +
                          "                    a:hover { text-decoration: underline; }" +
                          "                    .gray    { color:gray; }" +
                          "                    .example { background-color:#efefef; corner-radius:5px; padding:0.5em; }" +
                          "                    .whitehole { background-color:white; corner-radius:10px; padding:0px; }" +
                          "                    .caption { font-size: 1.1em }" +
                          "                    .comment { color: green; margin-bottom: 5px; margin-left: 3px; }" +
                          "                    .comment2 { color: green; }" +
                          "       </style>" +
                          $"         <table border = '0' width = '100%' height = '1'>\r\n" +
                          "            <tr style = 'vertical-align: top;>" +
                          $"             {ximage}" +
                          "              <td>" +
                          $"                {body}" +
                          "              </td>" +
                          "             </tr>" +
                          "          </table>" +
                          "    </body>" +
                          "</html>";
            return xreturn;
        }

        private string IndelingFieldTextGetter(WerkplekIndeling indeling)
        {

            try
            {
                string xall = null;
                List<IProductieBase> bws = Bewerkingen.Cast<IProductieBase>().ToList().GetRange(0,Bewerkingen.Count).Where(x => x != null && (x.State is ProductieState.Gestopt or ProductieState.Gestart) && IsAllowed(x, indeling, false)).OrderBy(x => x.ProductieNr).ToList();
                // }
                var size = indeling.IsCompact ? new Size(32, 32) : new Size(64, 64);
                if (indeling == null || indeling.IsDefault())
                {

                    var x1 = bws.Count == 1 ? "productie" : "producties";
                    var pers = GetIndelingen(false);

                    var x2 = pers.Count == 1 ? "werkplek" : "werkplaatsen";
                    var xtijdover = Math.Round(pers.Sum(x => GetTijdOver()));
                    var xbwtijdover = Math.Round(bws.Sum(x => x.GetTijdOver()), 2);
                    xall =
                        $"<li>Totaal <b>{bws.Count}</b> {x1} " +
                        $"<b>({xbwtijdover} uur)</b>.</li>";
                    if (bws.Count > 0 && !indeling.IsCompact)
                    {
                        if (pers.Count > 0)
                        {
                            indeling.GereedOp = Werktijd.DatumNaTijd(DateTime.Now, TimeSpan.FromHours(xbwtijdover / pers.Count), indeling.Settings?.WerkRooster, indeling.Settings?.SpecialeRoosters);
                            var x3 = pers.Count == 1
                                ? $""
                                : $"<li>Met <b>{pers.Count}</b> {x2} is dat <b>{Math.Round(xbwtijdover / pers.Count, 2)}</b> uur per werkplek</li>";
                            xall += $"{x3}" +
                                $"<li><b>{pers.Count}</b> {x2} met nog <b>{xtijdover}</b> uur tot einde van de week.</li>" +
                                $"<li>Gereed op <b>{indeling.GereedOp:dd/MM/yy HH:mm}</b></li>";
                        }
                        else
                        {
                            indeling.GereedOp = bws.BerekenLeverDatums(false, false, false);
                        }
                    }
                 
                    return GetBaseHitmlBody($"<ul>{xall}</ul>", "bewerkingen", size);
                }
                else
                {
                    var pers = indeling.Werkplek;
                    var xklusjes = bws.SelectMany(x =>
                            ((Bewerking)x)?.WerkPlekken.Where(w =>
                                string.Equals(pers, w.Naam,
                                    StringComparison.CurrentCultureIgnoreCase)))
                        .ToList();
                    var xstarted = xklusjes.Where(x => x.Werk.State == ProductieState.Gestart && x.IsActief())
                        .ToList();
                    var xbwtijdover = Math.Round(bws.Sum(x => x.GetTijdOver()), 2);
                    var xbwtijdgewerkt = Math.Round(bws.Sum(x => x.TijdGewerkt), 2);
                    var xbwtotaaltijd = Math.Round(bws.Sum(x => x.DoorloopTijd), 2);
                    var x2 = xklusjes.Count == 1 ? "productie" : "producties";
                    var x3 = xstarted.Count == 1 ? "productie" : "producties";
                    var x1 = bws.Count == 1 ? "productie" : "producties";
                    indeling.GereedOp = bws.BerekenLeverDatums(false, false, false);
                    if (!indeling.IsCompact)
                    {
                        xall =
                            $"<ul><li>Totaal <b>{bws.Count}</b> {x1} <b>({xbwtijdgewerkt}/{xbwtotaaltijd} uur)</b></li>" +
                            $"<li>Bezig met <b>{xstarted.Count}</b> {x3} <b>({Math.Round(xstarted.Sum(x => x.TijdAanGewerkt()), 2)}/{Math.Round(xstarted.Sum(x => x.Werk?.DoorloopTijd??0), 2)} uur)</b></li>" +
                            $"<li>Nog <b>{GetTijdOver()} uur</b> over tot einde van de week</li>" +
                            $"<li>Gereed op <b>{indeling.GereedOp:dd/MM/yyyy HH:mm}</b></li></ul>";
                    }
                    else
                    {
                        xall = $"<b>{bws.Count}</b> {x1} <b>({xbwtijdgewerkt}/{xbwtotaaltijd} uur)</b>";
                        if (bws.Count > 0)
                            xall += $"<br>Gereed op <b>{indeling.GereedOp:dd/MM/yyyy HH:mm}</b>.";
                    }
                    //if (indeling.SelectedBewerking != null && SelectedPersoneel != null && SelectedPersoneel.Equals(indeling.Persoon))
                    //{
                    //    var bw = indeling.SelectedBewerking;
                    //    xall +=
                    //        $"<b>{bw.Naam} van {bw.ArtikelNr} | {bw.ProductieNr}</b>";
                    //}
                    return GetBaseHitmlBody(xall, "werkplek", size);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return string.Empty;
        }

        public void UpdateIndelingLayout()
        {
            try
            {
                if (InvokeRequired)
                    this.Invoke(new Action(UpdateIndelingLayout));
                else
                {
                    if (Manager.Opties?.WerkplaatsIndelingen != null)
                    {
                        var curinds = GetIndelingen(false);
                        var remove = curinds.Where(x => !Manager.Opties.WerkplaatsIndelingen.Any(w => string.Equals(x.Name, x.Werkplek, StringComparison.CurrentCultureIgnoreCase))).ToList();
                        for (int i = 0; i < remove.Count; i++)
                        {
                            DeleteWerkplaats(remove[i].Name);
                        }
                        for (int i = 0; i < Manager.Opties.WerkplaatsIndelingen.Count; i++)
                        {
                            var xwp = Manager.Opties.WerkplaatsIndelingen[i];
                            var ind = GetIndeling(xwp.Name);
                            if (ind == null)
                                AddNewIndeling(xwp);
                        }
                    }
                    _updatetimer?.Stop();
                    _updatetimer.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void LoadProducties()
        {
            try
            {
                

                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(LoadProducties));
                    return;
                }
                // SetWaitUI();
                if (Manager.Database == null || Manager.Database.IsDisposed)
                    throw new Exception("Database is niet beschikbaar!");
                if(_updatetimer == null)
                {
                    _updatetimer = new System.Timers.Timer();
                    _updatetimer.Elapsed += xupdatetimer_Tick;
                    _updatetimer.Interval = 500;
                    _updatetimer.Enabled = false;
                }
                //lock (Bewerkingen)
                //{
                Bewerkingen = Manager.Database.xGetAllBewerkingen(false, true, false);
               // }
                xWerkplaatsIndelingPanel.SuspendLayout();
                xWerkplaatsIndelingPanel.Controls.Clear();
                var xfirst = AddNewIndeling(default);
                var bws = Bewerkingen.GetRange(0,Bewerkingen.Count).Where(x => IsAllowed(x, xfirst, false)).ToList();
                productieListControl1.InitProductie(bws, true, true, true, false, true);
                if (Manager.Opties?.WerkplaatsIndelingen != null)
                {
                    for (int i = 0; i < Manager.Opties.WerkplaatsIndelingen.Count; i++)
                    {
                        var xwp = Manager.Opties.WerkplaatsIndelingen[i];
                        AddNewIndeling(xwp);
                    }
                }

                xfirst.UpdateWerkplekInfo();
                xWerkplaatsIndelingPanel.ResumeLayout(true);
                SetSelected(null, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //StopWait();
        }

        private void UpdateTitle(List<Bewerking> bewerkingen)
        {
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new Action(() => UpdateTitle(bewerkingen)));
                else
                {
                    bewerkingen ??= Bewerkingen ?? new List<Bewerking>();
                    var bws = new List<Bewerking>();
                    for (int i = 0; i < bewerkingen.Count; i++)
                    {
                        var bw = bewerkingen[i];
                        if (IsAllowed(bw, SelectedWerkplek, false))
                            bws.Add(bw);
                    }
                    var xselected = SelectedWerkplek == null || SelectedWerkplek.IsDefault()
                        ? "Totaal"
                        : $"{SelectedWerkplek.Werkplek}";
                    var x3 = bws.Count == 1 ? "Productie" : "Producties";
                    var x2 = $"<span color='navy'><b>{xselected}: {bws.Count} {x3}</b></span><br>" +
                             $"Totaal <b>{Math.Round(bws.Sum(x => x.TijdGewerkt), 2)} / {Math.Round(bws.Sum(x => x.DoorloopTijd), 2)}</b> uur gewerkt " +
                             $"waarvan nog <b>{Math.Round(bws.Sum(x => x.GetTijdOver()), 2)}</b> uur over aan productie";
                    xGeselecteerdeGebruikerLabel.Text = x2;
                    xGeselecteerdeGebruikerLabel.Refresh();
                    var x1 = bws.Count == 1 ? "Bewerking" : "Bewerkingen";
                    var xtext = $@"Werkplaats Indeling: {bws.Count} {x1}";
                    OnStatusTextChanged(xtext);
                    this.Invalidate();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            //if (SelectedWerkplek == null || SelectedWerkplek.IsDefault())
            // {

            // }
        }

        private void ListProducties(bool resetview)
        {
            if (SelectedWerkplek != null)
            {
                //productieListControl1.InitProductie(Bewerkingen.Where(x => IsAllowed(x, SelectedWerkplek)).ToList(),
                //    false, true, true, false);
                //if (productieListControl1.DoSearch(SelectedWerkplek.Criteria))
                productieListControl1.WindowName = SelectedWerkplek.Werkplek;
                var sel = SelectedWerkplek.SelectedBewerking;
                if (resetview)
                    productieListControl1.SetCurrentViewStates(new ViewState[0], false);
                productieListControl1.ShowWaitUI = false;
                var bws = Bewerkingen.GetRange(0, Bewerkingen.Count).Where(x => IsAllowed(x, SelectedWerkplek, false)).ToList();
                productieListControl1.InitProductie(bws, false, true, true, false, false);
                //productieListControl1.InitProductie(true,
                //    true, true, false, true, true);
                UpdateTitle(bws);
                productieListControl1.DoSearch(SelectedWerkplek.Criteria);
                productieListControl1.SelectedItem = sel;
                productieListControl1.ShowWaitUI = true;
               
            }
        }

        private bool IsAllowed(object sender, object value, string filter, bool tempfilter)
        {
            if (value is IProductieBase bew)
            {
                var wp = SelectedWerkplek;
                //if(sender is ProductieListControl list)
                //{
                //    wp = GetIndeling(list.WindowName);
                //}
                if (wp == null)
                {
                    return true;
                }

                return IsAllowed(bew, wp, tempfilter);
            }

            return false;
        }

        private bool IsAllowed(IProductieBase bew, WerkplekIndeling indeling, bool tempfilter)
        {
            if (indeling == null)
            {
                return true;
            }

            if (!bew.IsAllowed(tempfilter))
                return false;
            if (indeling.IsDefault())
            {
                var xindelingen = GetIndelingen(false);
                var xwps = Manager.BewerkingenLijst.GetWerkplekken(bew.Naam);
                if (xindelingen.Count > 0 && !xwps.Any(x =>
                        xindelingen.Any(w => string.Equals(x.Replace(" ", ""), w.Werkplek.Replace(" ", ""), StringComparison.CurrentCultureIgnoreCase))))
                    return false;
                if (indeling.ToonNietIngedeeld)
                {

                    if (bew is Bewerking bw && bw.WerkPlekken.Any(x => xindelingen.Any(w =>
                             string.Equals(w.Werkplek.Replace(" ", ""), x.Naam.Replace(" ", ""), StringComparison.CurrentCultureIgnoreCase))))
                        return false;
                    if (bew.State != ProductieState.Gestopt) return false;
                }

                return true;
            }
            if (bew is Bewerking xbw)
                return xbw.WerkPlekken.Any(x => string.Equals(x.Naam.Replace(" ", ""), indeling.Werkplek.Replace(" ", ""),
                    StringComparison.CurrentCultureIgnoreCase));
            return false;
        }

        public WerkplekIndeling AddNewIndeling(WerkplaatsSettings indeling)
        {
            try
            {
                var xgroup = new GroupBox();
                xgroup.Padding = new Padding(3, 0, 3, 3);
                xgroup.Text = indeling?.Name ?? "Alle Bewerkingen";
                xgroup.Font = new Font(this.Font.FontFamily, 10, FontStyle.Bold);
                xgroup.MouseEnter += WerkplekIndeling_MouseEnter;
                xgroup.MouseLeave += WerkplekIndeling_MouseLeave;
                xgroup.Click += Xpers_Click;
                xgroup.DoubleClick += Xpers_DoubleClick;
                if (xIndelingPanel.Width > 50)
                    xgroup.Width = xIndelingPanel.Width - 50;
                if (indeling == null)
                    xgroup.Height -= 20;
               // xgroup.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                var xpers = new WerkplekIndeling();
                bool expanded = xIndelingPanel.Width > 26;
                if (!expanded)
                    indeling.IsCompact = true;
                xpers.ToonCompactMode = expanded;
                xpers.ToonSelectedKnoppen = expanded;
                xpers.FieldTextGetter = IndelingFieldTextGetter;
                xpers.ResetIndeling += Xpers_ResetIndeling;
                xpers.UpdateIndeling += Xpers_UpdateIndeling;
                //xgroup.Width = xpers.Width;
                xpers.DragDrop += Xpers_DragDrop;
                xgroup.MouseDown += xpers.IndelingMouseDown;
                xgroup.MouseMove += xpers.IndelingMouseMove;
                xpers.VerwijderWerkplaats += Xpers_VerwijderIndeling;
                xpers.Dock = DockStyle.Fill;
                xpers.MouseEnter += WerkplekIndeling_MouseEnter;
                xpers.MouseLeave += WerkplekIndeling_MouseLeave;
                xpers.ViewStateChanged += Xpers_ViewStateChanged;
                xpers.Click += Xpers_Click;
                xpers.DoubleClick += Xpers_DoubleClick;
                xpers.SettingsChanged += Xpers_SettingsChanged;
                //xgroup.Dock = DockStyle.Top;
                xgroup.Controls.Add(xpers);
                xWerkplaatsIndelingPanel.Controls.Add(xgroup);
                toolTip1.SetToolTip(xgroup, xgroup.Text);
                xpers.InitWerkplek(indeling);
                if (!xpers.IsDefault())
                {
                    UpdateDefault();
                }
                //xPersoneelIndelingPanel.Controls.SetChildIndex(xgroup, xPersoneelIndelingPanel.Controls.Count -1);

                return xpers;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
                return null;
            }
        }

        private void Xpers_UpdateIndeling(object sender, EventArgs e)
        {
            try
            {
                if(sender is WerkplekIndeling indeling)
                {
                    UpdateIndeling(indeling,true);
                }
            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void Xpers_SettingsChanged(object sender, EventArgs e)
        {
           try
            {
                if (sender is WerkplekIndeling indeling)
                {
                    var bws = Bewerkingen.GetRange(0,Bewerkingen.Count).Where(x => (x.State is ProductieState.Gestart or ProductieState.Gestopt) && IsAllowed(x, indeling, false)).ToList();
                    for (int i = 0; i < bws.Count; i++)
                    {
                        var bw = bws[i];
                        var wp = bw.WerkPlekken?.FirstOrDefault(x => string.Equals(indeling.Werkplek, x.Naam, StringComparison.CurrentCultureIgnoreCase));
                        if (wp?.Tijden == null) continue;
                        wp.Tijden.SpecialeRoosters ??= new List<Rooster>();
                        wp.Tijden.WerkRooster = indeling.Settings.WerkRooster.CreateCopy();
                        var xadd = wp.Tijden.SpecialeRoosters.Count > 0 ?
                            indeling.Settings.SpecialeRoosters?.Where(x => !wp.Tijden.SpecialeRoosters.Any(t => t.Vanaf.Date == x.Vanaf.Date)).ToList() :
                            indeling.Settings.SpecialeRoosters;
                        xadd ??= new List<Rooster>();
                        if (xadd.Count > 0)
                            wp.Tijden.SpecialeRoosters.AddRange(xadd);
                        bw.UpdateBewerking(null, $"[{wp.Path}] Rooster geupdate", true, false);
                    }
                    //UpdateIndeling(indeling, true);
                    //if (bws.Count > 0)
                    //{
                    //    var result = XMessageBox.Show(this, "Wilt u producties kiezen om de roosters te updaten?", "Roosters Updaten", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //    if (result != DialogResult.Yes) return;
                    //    var xbw = new BewerkingSelectorForm(bws);
                    //    if (xbw.ShowDialog() != DialogResult.OK || xbw.SelectedBewerkingen is { Count: 0 }) return;
                    //    bws = xbw.SelectedBewerkingen;

                    //}
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void Xpers_ResetIndeling(object sender, EventArgs e)
        {
            try
            {
                if (_isbussy) return;
                if (sender is WerkplekIndeling indeling)
                {
                    var bws = Bewerkingen.GetRange(0,Bewerkingen.Count).Where(x => x.IsAllowed() && x.State == ProductieState.Gestopt && IsAllowed(x,indeling, false)).ToList();
                    bws = bws.Where(x =>
                            x.WerkPlekken != null &&
                            x.WerkPlekken.Any(wp => wp.TotaalGemaakt == 0 && wp.TijdGewerkt == 0))
                        .ToList();
                    if (bws.Count > 0)
                    {
                        var x1 = bws.Count == 1 ? "productie" : "producties";
                        var x2 = bws.Count == 1 ? "is" : "zijn";
                        var res = XMessageBox.Show(this, $"Er {x2} {bws.Count} {x1} op {indeling.Werkplek} om te resetten.\n\n" +
                                                         $"Wil je doorgaan met het resetten van de ingedeelde producties?",
                            $"{indeling.Werkplek} Indeling Resetten", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes)
                        {
                            ResetIndeling(bws);
                        }
                    }
                    else
                    {
                        XMessageBox.Show(this, "Er zijn geen ingedeelde producties om te resetten", "Geen Producties",
                            MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void UpdateDefault()
        {
            var xdef = GetDefaultIndeling();
            xdef?.UpdateWerkplekInfo();
            if (SelectedWerkplek != null && SelectedWerkplek.IsDefault())
            {
                ListProducties(true);
            }
        }

        private void Xpers_ViewStateChanged(object sender, EventArgs e)
        {
            UpdateDefault();
        }

        private void Xpers_DragDrop(object sender, DragEventArgs e)
        {
            flowLayoutPanel1_DragDrop(xWerkplaatsIndelingPanel, e);
        }

        private void Xpers_VerwijderIndeling(object sender, EventArgs e)
        {
            try
            {
                if (sender is WerkplekIndeling indeling && !indeling.IsDefault())
                {
                    var result = XMessageBox.Show(this, $"Weet je zeker dat je '{indeling.Werkplek}' wilt verwijderen?\n\n" +
                        $"Het verwijderen van '{indeling.Werkplek}' heeft geen effect op de producties.\n" +
                        $"Dit is enkel voor visualisatie en kan altijd weer worden toegevoegd.", $"{indeling.Werkplek} Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result != DialogResult.Yes) return;
                    DeleteWerkplaats(indeling.Werkplek);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void Xpers_DoubleClick(object sender, EventArgs e)
        {
            WerkplekIndeling xindeling = sender as WerkplekIndeling;
            if (xindeling == null)
            {
                if (sender is GroupBox group && group.Controls.Count > 0 && group.Controls[0] is WerkplekIndeling x)
                {
                    xindeling = x;
                }
            }

            if (xindeling == null || xindeling.IsDefault())
            {
                AddNewIndeling();
            }
            else if(sender is GroupBox)
            {
                xindeling.TogleCompactMode();
            }
        }

        private void Xpers_Click(object sender, EventArgs e)
        {
            if (sender is GroupBox group && group.Controls.Count > 0 && group.Controls[0] is WerkplekIndeling x)
            {
                SelectWerkplek(x);
            }
            else if(sender is WerkplekIndeling xpers)
            {
                SelectWerkplek(xpers);
            }
        }

        public void SelectWerkplek(WerkplekIndeling werkplek)
        {
            if (string.Equals(werkplek?.Werkplek, SelectedWerkplek?.Werkplek,
                    StringComparison.CurrentCultureIgnoreCase)) return;
            foreach (var xgroup in xWerkplaatsIndelingPanel.Controls.Cast<GroupBox>())
            {
                if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is WerkplekIndeling xpers)
                {
                    if (xpers.IsDefault() && (werkplek == null || werkplek.IsDefault()))
                    {
                        SetSelected(xpers, true);
                        continue;
                    }

                    if (xpers.IsDefault())
                    {
                        SetSelected(xpers, false);
                    }
                    else if (string.Equals(werkplek?.Werkplek, xpers.Werkplek, StringComparison.CurrentCultureIgnoreCase))
                    {
                        SetSelected(xpers, true);
                    }
                    else SetSelected(xpers, false);
                }
            }
        }

        private void SetSelected(WerkplekIndeling indeling, bool selected)
        {
            xBerekenDatumTimer.Stop();
            xBerekenDatumTimer.Start();
            if (selected)
            {
                indeling ??= GetIndeling(null);
                SelectedWerkplek = indeling;

                foreach (var xgroup in xWerkplaatsIndelingPanel.Controls.Cast<GroupBox>())
                {
                    if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is WerkplekIndeling xpers)
                    {
                        SetColor(xpers, Color.White);
                        xpers.IsSelected = false;
                        xpers.UpdateWerkplekInfo();
                    }
                }

                if (indeling != null)
                {
                    indeling.IsSelected = true;
                    UpdateIndeling(indeling, true);
                }
                ListProducties(true);
            }
            else if (indeling != null)
            {
                indeling.BackColor = Color.White;
                indeling.IsSelected = false;
            }
            xDeletePersoneel.Enabled = SelectedWerkplek != null && !SelectedWerkplek.IsDefault();
        }

        private void WerkplekIndeling_MouseEnter(object sender, EventArgs e)
        {
            if (sender is GroupBox group && group.Controls.Count > 0 && group.Controls[0] is WerkplekIndeling xpers)
            {
                SetColor(xpers, Color.AliceBlue);
            }
            else if (sender is WerkplekIndeling xindeling)
            {
                SetColor(xindeling, Color.AliceBlue);
            }
        }

        private void SetColor(WerkplekIndeling indeling, Color color)
        {
            if (indeling != null)
            {
                if (indeling.IsDefault() && (SelectedWerkplek == null || SelectedWerkplek.IsDefault()))
                {
                    indeling.BackColor = Color.LightBlue;
                }
                else if (indeling.IsDefault())
                {
                    indeling.BackColor = color;
                }
                else
                {
                    indeling.BackColor = string.Equals(indeling.Werkplek, SelectedWerkplek.Werkplek,
                        StringComparison.CurrentCultureIgnoreCase)
                        ? Color.LightBlue
                        : color;
                }
            }
        }

        private void WerkplekIndeling_MouseLeave(object sender, EventArgs e)
        {
            if (sender is GroupBox group && group.Controls.Count > 0 && group.Controls[0] is WerkplekIndeling xpers)
            {
                SetColor(xpers, Color.White);
            }
            else if(sender is WerkplekIndeling xindeling)
            {
                SetColor(xindeling, Color.White);
            }
        }

        private void AddNewIndeling()
        {
            try
            {
                var xwps = Manager.BewerkingenLijst.GetAlleWerkplekken().ToArray();
                var wpchooser = new WerkPlekChooser(xwps, null);
                wpchooser.Title = $"Voeg een nieuwe Werkplek toe";
                if (wpchooser.ShowDialog(this) == DialogResult.OK)
                {
                    var chosen = wpchooser.SelectedName;
                    if (chosen.Length > 0)
                    {
                        WerkplekIndeling xindeling = GetIndeling(chosen);
                        if (xindeling == null)
                        {
                            xindeling = AddNewIndeling(new WerkplaatsSettings() { Name = chosen });
                        }

                        if (xindeling != null)
                        {
                            SetSelected(xindeling, true);
                            xindeling.Focus();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xAddPersoneel_Click(object sender, EventArgs e)
        {
            AddNewIndeling();
        }

        public bool IndelingExists(WerkplekIndeling plek)
        {
            try
            {
                foreach (var xgroup in xWerkplaatsIndelingPanel.Controls.Cast<GroupBox>())
                {
                    if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is WerkplekIndeling xpers)
                    {
                        if (xpers.IsDefault()) continue;
                        if (string.Equals(xpers.Werkplek, plek.Werkplek, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                    }
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public WerkplekIndeling GetIndeling(string werkplek)
        {
            try
            {
                foreach (var xgroup in xWerkplaatsIndelingPanel.Controls.Cast<GroupBox>())
                {
                    if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is WerkplekIndeling xpers)
                    {
                        if (xpers.IsDefault() && string.IsNullOrEmpty(werkplek))
                        {
                            return xpers;
                        }

                        if (xpers.IsDefault()) continue;
                        if (string.Equals(xpers.Werkplek, werkplek, StringComparison.CurrentCultureIgnoreCase))
                            return xpers;
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool WerkplekExists(string werkplek)
        {
            try
            {
                foreach (var xgroup in xWerkplaatsIndelingPanel.Controls.Cast<GroupBox>())
                {
                    if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is WerkplekIndeling xpers)
                    {
                        if (xpers.IsDefault()) continue;
                        if (string.Equals(xpers.Werkplek, werkplek, StringComparison.CurrentCultureIgnoreCase))
                            return true;
                    }
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void xDeletePersoneel_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedWerkplek == null || SelectedWerkplek.IsDefault()) return;
                DeleteWerkplaats(SelectedWerkplek.Werkplek);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void DeleteWerkplaats(string werkplek)
        {
            try
            {
                GroupBox xdelete = null;
                foreach (var xgroup in xWerkplaatsIndelingPanel.Controls.Cast<GroupBox>())
                {
                    if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is WerkplekIndeling xpers)
                    {
                        if (xpers.IsDefault()) continue;
                        if (string.Equals(xpers.Werkplek, werkplek, StringComparison.CurrentCultureIgnoreCase))
                        {
                            xgroup.MouseEnter -= WerkplekIndeling_MouseEnter;
                            xgroup.MouseLeave -= WerkplekIndeling_MouseLeave;
                            xgroup.Click -= Xpers_Click;
                            xgroup.DoubleClick -= Xpers_DoubleClick;
                            xdelete = xgroup;
                            xpers.MouseEnter -= WerkplekIndeling_MouseEnter;
                            xpers.MouseLeave -= WerkplekIndeling_MouseLeave;
                            xpers.Click -= Xpers_Click;
                            xpers.DoubleClick -= Xpers_DoubleClick;
                            xpers.VerwijderWerkplaats -= Xpers_VerwijderIndeling;
                            
                            break;
                        }
                    }
                }

                if (xdelete != null)
                {
                    xWerkplaatsIndelingPanel.SuspendLayout();
                    xWerkplaatsIndelingPanel.Controls.Remove(xdelete);
                    xWerkplaatsIndelingPanel.ResumeLayout(true);
                    SetSelected(null, true);
                    UpdateDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            // try
            //{
            if (changedform?.Bewerkingen == null || _isbussy) return;
            // this.BeginInvoke(new Action(() =>
            //{
            try
            {
                if (changedform.Bewerkingen == null || changedform.Bewerkingen.Length == 0)
                {
                    return;
                }
                bool changed = false;
                for (int i = 0; i < changedform.Bewerkingen.Length; i++)
                {
                    var bw = changedform.Bewerkingen[i];
                    bool valid = bw.IsAllowed();
                   
                    if (!valid)
                    {
                        //lock (Bewerkingen)
                        // {
                        Bewerkingen.Remove(bw);
                        //}
                        changed = true;
                    }
                    else
                    {
                        //lock (Bewerkingen)
                        //{
                        var xindex = Bewerkingen.IndexOf(bw);
                        if (xindex > -1 && xindex < Bewerkingen.Count)
                        {


                            //changed = Bewerkingen[xindex].WerkPlekken.Any(x => bw.WerkPlekken.All(w => !string.Equals(w.Naam, x.Naam, StringComparison.CurrentCultureIgnoreCase)));
                            //changed |= bw.WerkPlekken.Any(x => Bewerkingen[xindex].WerkPlekken.All(w => !string.Equals(w.Naam, x.Naam, StringComparison.CurrentCultureIgnoreCase)));
                            //var xbw = Bewerkingen.ElementAt(xindex);
                            //changed |= !bw.TheSameAs(xbw);
                           // bw.BerekentLeverDatum = Bewerkingen[xindex].BerekentLeverDatum;
                            Bewerkingen[xindex] = bw;
                            changed = false;
                        }
                        else
                        {
                            Bewerkingen.Add(bw);
                            changed = true;
                        }
                        //}
                    }
                }
                if (changed)
                {
                    _updatetimer?.Stop();
                    _updatetimer?.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            // }));
            //}
            // catch (Exception e)
            // {
            //    Console.WriteLine(e);
            // }
        }
        bool _updating = false;

        private void UpdateIndelingen()
        {
            try
            {
                //if (InvokeRequired)
                //    this.Invoke(new MethodInvoker(UpdateIndelingen));
                //else
                //{
                _updating = true;
                var xindelingen = GetIndelingen(true);
                for (int i = 0; i < xindelingen.Count; i++)
                {
                    var x = xindelingen[i];
                    UpdateIndeling(x, true);
                }
                _updating = false;
                // }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void UpdateIndeling(WerkplekIndeling indeling, bool updategereed)
        {
            try
            {
                if (updategereed)
                {
                    xBerekenDatumTimer.Stop();
                    BerekenLeverDatums(indeling);

                    xBerekenDatumTimer.Start();
                }
                else
                {
                    indeling.UpdateWerkplekInfo();
                    if (indeling.IsSelected)
                        UpdateTitle(null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void BerekenLeverDatums(WerkplekIndeling indeling)
        {
            try
            {
                List<Bewerking> bws = null;
                if (!indeling.IsDefault())
                {
                    bws = Bewerkingen.GetRange(0, Bewerkingen.Count).Where(bw => bw != null && (bw.State is ProductieState.Gestopt or ProductieState.Gestart) && IsAllowed(bw, indeling, false)).ToList();
                    indeling.GereedOp = bws.OfType<IProductieBase>().ToList().BerekenLeverDatums(true,true, true);
                }
                indeling.UpdateWerkplekInfo();
                if (indeling.IsSelected)
                {
                    UpdateTitle(bws);
                    if(productieListControl1.ProductieLijst.PrimarySortColumn?.AspectName != null && productieListControl1.ProductieLijst.PrimarySortColumn.AspectName.ToLower().Contains("berekentleverdatum"))
                    {
                        productieListControl1.ProductieLijst.Sort(productieListControl1.ProductieLijst.PrimarySortColumn);
                    }
                }
            }
            catch { }
        }

        private void Manager_OnFormulierDeleted(object sender, string id)
        {
            try
            {
                var bws = Bewerkingen
                    .GetRange(0,Bewerkingen.Count).Where(x => string.Equals(x.ProductieNr, id, StringComparison.CurrentCultureIgnoreCase)).ToList();
                if (bws.Count > 0)
                {
                    for (int i = 0; i < bws.Count; i++)
                    {
                        var b = bws[i];
                        Bewerkingen.Remove(b);
                    }
                    _updatetimer.Stop();
                    _updatetimer.Start();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            var data = (GroupBox)e.Data.GetData(typeof(GroupBox));
            if (data == null) return;
            var _destination = (FlowLayoutPanel)sender;
            var _source = (FlowLayoutPanel)data.Parent;

            if (_source != _destination)
            {
                // Add control to panel
                _destination.Controls.Add(data);
                data.Size = new Size(_destination.Width, 50);

                // Reorder
                var p = _destination.PointToClient(new Point(e.X, e.Y));
                var item = _destination.GetChildAtPoint(p);
                var index = _destination.Controls.GetChildIndex(item, false);

                _destination.Controls.SetChildIndex(data, index);

                // Invalidate to paint!
                _destination.Invalidate();
                _source.Invalidate();
            }
            else
            {
                // Just add the control to the new panel.
                // No need to remove from the other panel, this changes the Control.Parent property.
                var p = _destination.PointToClient(new Point(e.X, e.Y));
                var item = _destination.GetChildAtPoint(p) ?? _destination.Controls[_destination.Controls.Count - 1];
                var index = item == null ? 1 : _destination.Controls.GetChildIndex(item, false);
                if (index == 0)
                    index = 1;
                _destination.Controls.SetChildIndex(data, index);
                _destination.Invalidate();
            }
        }

        private void flowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void xWerkplaatsIndelingPanel_Click(object sender, EventArgs e)
        {
            SelectWerkplek(null);
        }

        private void xautoindeling_Click(object sender, EventArgs e)
        {
            AutoIndelen();
        }

        private void AutoIndelen()
        {
            try
            {
                if (_isbussy) return;
                var wps = GetIndelingen(false);
                if (wps == null || wps.Count == 0)
                {
                    XMessageBox.Show(this, "Er zijn geen werkplaatsen om werk voor in te delen.\n\n" +
                                           "Voeg eerst werkplaats(en) toe", "Geen Werkplaatsen",
                        MessageBoxIcon.Exclamation);
                    return;
                }
                List<Bewerking> bws = Bewerkingen.GetRange(0,Bewerkingen.Count).Where(x => x.IsAllowed() && x.State == ProductieState.Gestopt && IsAllowed(x,GetDefaultIndeling(), false)).ToList();
                bws = bws.Where(x => x.WerkPlekken == null || x.WerkPlekken.All(f =>
                    !wps.Any(w => string.Equals(w.Werkplek.Replace(" ", ""), f.Naam.Replace(" ", ""),
                        StringComparison.CurrentCultureIgnoreCase)))).OrderBy(x=> x.LeverDatum).ToList();
                if (bws.Count > 0)
                {
                    var x1 = bws.Count == 1 ? "productie" : "producties";
                    var x2 = bws.Count == 1 ? "is" : "zijn";
                    var x3 = wps.Count == 1 ? "werkplaats" : "werkplaatsen";
                    var res = XMessageBox.Show(this, $"Er {x2} {bws.Count} {x1} om in te delen.\n\n" +
                                                     $"Wil je doorgaan met het indelen van {bws.Count} {x1} verspreid over {wps.Count} {x3}?",
                        "Producties Indelen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        DeelProductiesIn(bws, wps);
                    }
                }
                else
                {
                    XMessageBox.Show(this, "Er zijn geen producties om in te delen", "Geen Producties",
                        MessageBoxIcon.Information);
                }

            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
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

        private WerkplekIndeling GetAanbevolenIndeling(Bewerking bw, List<WerkplekIndeling> wps)
        {
            try
            {
                WerkplekIndeling xind = null;
                var xwps = Manager.BewerkingenLijst.GetWerkplekken(bw.Naam);
                var inds = wps.Where(x => xwps.Any(xw => string.Equals(xw.Replace(" ", ""),
                    x.Werkplek.Replace(" ", ""), StringComparison.CurrentCultureIgnoreCase))).ToList();
                if (inds.Count == 0) return null;
                bw.WerkPlekken.RemoveAll(x =>
                    x.TotaalGemaakt == 0 && x.TijdGewerkt == 0);

                double xlasttijd = 0;
                for (int j = 0; j < inds.Count; j++)
                {
                    var ind = inds[j];
                    if (ind.Settings?.Voorwaardes != null && !ind.Settings.Voorwaardes.IsAllowed(bw, null, false))
                        continue;
                    var curbws = Bewerkingen.GetRange(0,Bewerkingen.Count).Where(x => IsAllowed(x, ind, false)).OrderBy(x => x.GetStartOp()).ToList();
                    var art = bw.ArtikelNr?.ToUpper().Replace("ZW", "");
                    if (curbws.Any(x => string.Equals(x.ArtikelNr?.ToUpper().Replace("ZW", ""), art, StringComparison.CurrentCultureIgnoreCase) &&
                            string.Equals(x.Naam, bw.Naam, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        xind = ind;
                        break;
                    }

                    var xbwtijdover = curbws.Sum(x => x.GetTijdOver());
                    var gereed = Werktijd.DatumNaTijd(DateTime.Now, TimeSpan.FromHours(xbwtijdover),
                        ind.Settings?.WerkRooster, ind.Settings?.SpecialeRoosters);
                    var startop = bw.GetStartOp();

                    if (xind == null)
                    {
                        xind = ind;
                        xlasttijd = xbwtijdover;
                        continue;
                    }
                    if (xlasttijd > xbwtijdover)
                    {
                        xlasttijd = xbwtijdover;
                        xind = ind;
                        continue;
                    }
                    if (gereed < startop && curbws.Any(x => IsSameProduct(x.ArtikelNr, bw.ArtikelNr) >= 4 &&
                           string.Equals(x.Naam, bw.Naam, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        xind = ind;
                    }
                    //if (gereed <= startop)
                    //{
                    //    xind = ind;
                    //    xlasttijd = xbwtijdover;
                    //    break;
                    //}
                }
                return xind;
            }
            catch { return null; }
        }

        private bool _isbussy;

        private void DeelProductiesIn(List<Bewerking> bws, List<WerkplekIndeling> wps)
        {
            if (_isbussy) return;
            _isbussy = true;
            xBerekenDatumTimer.Stop();
            var prog = new LoadingForm();
            var arg = prog.Arg;
            arg.Message = "Indeling maken...";
            prog.CloseIfFinished = true;
            arg.OnChanged(this);
            _= prog.ShowDialogAsync(this.ParentForm);
            try
            {
                var sections = bws.ToArtikelNrSections(null);
                foreach (var sec in sections)
                {
                    for (int i = 0; i < sec.Value.Count; i++)
                    {
                        var bw = (Bewerking)sec.Value[i];
                        arg.Current++;
                        arg.Max = bws.Count;
                        arg.Message = $"Indelen: {bw.Path}({arg.Current}/{bws.Count})...";
                        arg.Type = ProgressType.WriteBussy;
                        arg.OnChanged(this);
                        if (arg.IsCanceled) break;
                        var prod = Werk.FromPath(bw.Path);
                        if (!prod.IsValid || prod.Bewerking == null) continue;
                        arg.OnChanged(this);
                        if (arg.IsCanceled) break;
                        bw = prod.Bewerking;
                        if (bw.State != ProductieState.Gestopt) continue;
                        WerkplekIndeling xind = GetAanbevolenIndeling(bw, wps);
                        if (xind == null) continue;
                        var wp = bw.WerkPlekken.FirstOrDefault(xw => string.Equals(xw.Naam.Replace(" ", ""),
                            xind.Werkplek.Replace(" ", ""), StringComparison.CurrentCultureIgnoreCase));
                        if (wp != null)
                            continue;
                        wp = bw.GetWerkPlek(xind.Werkplek, true);
                        if (wp == null)
                            continue;
                        bw.ExcludeFromUpdate();
                        wp.Tijden.WerkRooster = xind.Settings?.WerkRooster;
                        wp.Tijden.SpecialeRoosters = xind.Settings?.SpecialeRoosters;
                        _ = bw.UpdateBewerking(null,
                            $"[{wp.Naam}] Ingedeeld op {bw.Path}", true, false);
                        var ind = Bewerkingen.IndexOf(bw);
                        if (ind > -1)
                            Bewerkingen[ind] = bw;
                        bw.RemoveExcludeFromUpdate();
                    }
                }
                arg.Type = ProgressType.WriteCompleet;
                arg.IsCanceled = true;
                arg.OnChanged(this);
                Application.DoEvents();
                UpdateIndelingen();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            _isbussy = false;
        }

        private void ResetIndeling(List<Bewerking> bws)
        {
            try
            {
                if (_isbussy) return;
                xBerekenDatumTimer.Stop();
                var prog = new LoadingForm();
                var arg = prog.Arg;
                arg.Message = "Indeling resetten...";
                arg.OnChanged(this);
                prog.CloseIfFinished = true;
                _=prog.ShowDialogAsync(this.ParentForm);
                _isbussy = true;
                Application.DoEvents();
                try
                {
                    int cur = 0;
                    foreach (var bw in bws)
                    {
                        arg.Current = cur;
                        arg.Max = bws.Count;
                        arg.Message = $"Indeling Reseten: {bw.Path}({cur}/{bws.Count})...";
                        arg.Type = ProgressType.WriteBussy;
                        arg.OnChanged(this);
                        if (arg.IsCanceled) break;
                        cur++;
                        var prod = Werk.FromPath(bw.Path);
                        if (!prod.IsValid || prod.Bewerking == null) continue;

                        int removed = prod.Bewerking.WerkPlekken.RemoveAll(x => x.TotaalGemaakt == 0 && x.TijdGewerkt == 0);
                        if (removed > 0)
                        {
                            prod.Bewerking.ExcludeFromUpdate();
                            _ = prod.Bewerking.UpdateBewerking(null,
                                $"[{bw.Path}] Indeling gereset", true, false);
                            var ind = Bewerkingen.IndexOf(prod.Bewerking);
                            if (ind > -1)
                                Bewerkingen[ind] = prod.Bewerking;
                            prod.Bewerking.RemoveExcludeFromUpdate();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                arg.Type = ProgressType.WriteCompleet;
                arg.IsCanceled = true;
                arg.OnChanged(this);
                Application.DoEvents();
                UpdateIndelingen();
                _isbussy = false;
            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xreset_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isbussy) return;
                var bws = Bewerkingen.GetRange(0,Bewerkingen.Count).Where(x => x.IsAllowed() && x.State == ProductieState.Gestopt).ToList();
                bws = bws.Where(x =>
                        x.WerkPlekken != null && x.WerkPlekken.Any(wp => wp.TotaalGemaakt == 0 && wp.TijdGewerkt == 0))
                    .ToList();
                if (bws.Count > 0)
                {
                    var x1 = bws.Count == 1 ? "productie" : "producties";
                    var x2 = bws.Count == 1 ? "is" : "zijn";
                    var res = XMessageBox.Show(this, $"Er {x2} {bws.Count} {x1} om resetten.\n\n" +
                                                     $"Wil je doorgaan met resetten van de ingedeelde producties?",
                        "Indeling Resetten", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        ResetIndeling(bws);
                    }
                }
                else
                {
                    XMessageBox.Show(this, "Er zijn geen ingedeelde producties om te resetten", "Geen Producties",
                        MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xupdatetimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_updating) return;
                _updatetimer?.Stop();
                UpdateIndelingen();
            }
            catch { }

        }

        private void xCollapseButton_Click(object sender, EventArgs e)
        {
            try
            {
                var inds = GetIndelingen(true);
                bool expanden = xIndelingPanel.Width > 26;
                expanden = !expanden;
                xIndelingPanel.Width = expanden ? 510 : 26;
                xIndelingPanel.Invalidate();
                foreach (var ind in inds)
                {
                    var g = ind.Parent as GroupBox;
                    if (g != null)
                    {
                        ind.ToonSelectedKnoppen = expanden;
                        ind.ToonCompactMode = expanden;
                        ind.SetCompactMode(expanden?ind.Settings.IsCompact : true);
                        g.Invalidate();
                    }
                }
                xCollapseButton.Image = expanden ? Resources.Navigate_left_36746 : Resources.Navigate_right_36745;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void xPreviousIndeling_Click(object sender, EventArgs e)
        {
            var sel = SelectedWerkplek;
            int index = 0;
            if(sel?.Parent != null)
            {
                index = xWerkplaatsIndelingPanel.Controls.IndexOf(sel.Parent);
                if(index == -1)
                {
                    index = 0;
                }
                else
                {
                    if (index > 0)
                        index--;
                    else index = xWerkplaatsIndelingPanel.Controls.Count - 1;
                }
                if (index > -1 && index < xWerkplaatsIndelingPanel.Controls.Count)
                {
                    var xgroup = xWerkplaatsIndelingPanel.Controls[index];
                    var ind = xgroup.Controls.OfType<WerkplekIndeling>().First();
                    if (ind != null)
                    {
                        SetSelected(ind,true);
                    }
                }
            }
        }

        private void xNextIndeling_Click(object sender, EventArgs e)
        {
            var sel = SelectedWerkplek;
            int index = 0;
            if (sel?.Parent != null)
            {
                index = xWerkplaatsIndelingPanel.Controls.IndexOf(sel.Parent);
                if (index == -1)
                {
                    index = 0;
                }
                else
                {
                    if (index < xWerkplaatsIndelingPanel.Controls.Count - 1)
                        index++;
                    else index = 0;
                }
                if (index > -1 && index < xWerkplaatsIndelingPanel.Controls.Count)
                {
                    var xgroup = xWerkplaatsIndelingPanel.Controls[index];
                    var ind = xgroup.Controls.OfType<WerkplekIndeling>().First();
                    if (ind != null)
                    {
                        SetSelected(ind, true);
                    }
                }
            }
        }

        private void xBerekenDatumTimer_Tick(object sender, EventArgs e)
        {
            if (SelectedWerkplek != null && !SelectedWerkplek.IsDefault())
            {
                xBerekenDatumTimer.Stop();
                BerekenLeverDatums(SelectedWerkplek);
                xBerekenDatumTimer.Start();
            }
        }
    }
}
