﻿using Controls;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rpm.Misc;
using Rpm.Various;
using Various;

namespace Forms
{
    public partial class WerkplaatsIndelingUI : UserControl
    {
        internal WerkplekIndeling SelectedWerkplek { get; private set; }
        internal List<Bewerking> Bewerkingen { get; set; } = new List<Bewerking>();

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
            DetachEvents();
            productieListControl1.DetachEvents();
            productieListControl1.SaveColumns(true);
            if (Manager.Opties != null)
            {
                Manager.Opties.WerkplaatsIndeling = GetIndelingen(false).Select(x => x.Werkplek).ToList();
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
        }

        public void DetachEvents()
        {
            productieListControl1.ItemCountChanged -= ProductieListControl1_ItemCountChanged;
            productieListControl1.SelectedItemChanged -= ProductieListControl1_SelectedItemChanged;
            productieListControl1.SearchItems -= ProductieListControl1_SearchItems;
            productieListControl1.DetachEvents();
            Manager.OnFormulierDeleted -= Manager_OnFormulierDeleted;
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
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
            UpdateTitle();
        }

        private bool _iswaiting;

        /// <summary>
        ///     Toon laad scherm
        /// </summary>
        public void SetWaitUI()
        {
            if (_iswaiting) return;
            _iswaiting = true;
            Task.Run(async () =>
            {
                try
                {
                    if (Disposing || IsDisposed) return;
                    xloadinglabel.Invoke(new MethodInvoker(() => { xloadinglabel.Visible = true; }));
                    var cur = 0;
                    var xwv = "Indeling laden";
                    //var xcurvalue = xwv;
                    var tries = 0;
                    while (_iswaiting && tries < 200)
                    {
                        if (Disposing || IsDisposed) return;
                        if (cur > 5) cur = 0;
                        var curvalue = xwv.PadRight(xwv.Length + cur, '.');
                        //xcurvalue = curvalue;
                        xloadinglabel.BeginInvoke(new MethodInvoker(() =>
                        {
                            xloadinglabel.Text = curvalue;
                            xloadinglabel.Invalidate();
                        }));
                        Application.DoEvents();

                        await Task.Delay(500);
                        Application.DoEvents();
                        tries++;
                        cur++;
                    }
                    if (Disposing || IsDisposed) return;
                    xloadinglabel.Invoke(new MethodInvoker(() => { xloadinglabel.Visible = false; }));
                }
                catch (Exception e)
                {
                }


            });
        }

        /// <summary>
        ///     verberg het laad scherm
        /// </summary>
        public void StopWait()
        {
            _iswaiting = false;
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

        private string GetBaseHitmlBody(string body, string image)
        {
            string ximage = $"<td width = '64' height = '64' style = 'padding: 0px 0px 0 0' >\r\n" +
                            $"<img width='{64}' height='{64}'  src = '{image}' />\r\n" +
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
                          $"         <table border = '0' width = '100%' height = '10'>\r\n" +
                          "            <tr style = 'vertical-align: top;>" +
                          $"             {ximage}" +
                          "              <td>" +
                          "                <ul>" +
                          $"                {body}" +
                          "                </ul>" +
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
                var bws = Bewerkingen.Where(x => IsAllowed(x, indeling)).OrderBy(x=> x.ProductieNr).ToList();
               if (indeling == null || indeling.IsDefault())
                {
                   
                    var x1 = bws.Count == 1 ? "productie" : "producties";
                    var pers = GetIndelingen(false);

                    var x2 = pers.Count == 1 ? "werkplek" : "werkplaatsen";
                    var xtijdover = Math.Round(pers.Sum(x=> GetTijdOver()));
                    var xbwtijdover = Math.Round(bws.Sum(x => x.GetTijdOver()), 2);
                    xall =
                        $"<li>Totaal <b>{bws.Count}</b> {x1} " +
                        $"<b>({xbwtijdover} uur)</b>.</li>";
                    if (pers.Count > 0)
                    {
                        var x3 = pers.Count == 1
                            ? $""
                            : $"<li>Met <b>{pers.Count}</b> {x2} is dat <b>{Math.Round(xbwtijdover / pers.Count, 2)}</b> uur per werkplek</li>";
                        xall += $"{x3}" +
                            $"<li><b>{pers.Count}</b> {x2} met nog <b>{xtijdover}</b> uur tot einde van de week.</li>" +
                            $"<li>Met <b>{pers.Count}</b> {x2} kan je op <b>{Werktijd.DatumNaTijd(DateTime.Now, TimeSpan.FromHours(xbwtijdover / pers.Count), null, null):dd/MM/yy HH:mm}</b> klaar zijn</li>";
                    }

                    return GetBaseHitmlBody(xall, "bewerkingen");
                }
                else
                {
                    var pers = indeling.Werkplek;
                    var xklusjes = bws.SelectMany(x =>
                            x.WerkPlekken.Where(w =>
                                string.Equals(pers, w.Naam,
                                    StringComparison.CurrentCultureIgnoreCase)))
                        .ToList();
                    var xstarted = xklusjes.Where(x => x.Werk.State == ProductieState.Gestart && x.IsActief())
                        .ToList();
                    var xbwtijdover = Math.Round(bws.Sum(x => x.GetTijdOver()), 2);
                    var xbwtijdgewerkt = Math.Round(bws.Sum(x => x.TijdAanGewerkt()), 2);
                    var xbwtotaaltijd = Math.Round(bws.Sum(x => x.GetDoorloopTijd()), 2);
                    var x2 = xklusjes.Count == 1 ? "productie" : "producties";
                    var x3 = xstarted.Count == 1 ? "productie" : "producties";
                    var x1 = bws.Count == 1 ? "productie" : "producties";
                    xall =
                        $"<li>Totaal <b>{bws.Count}</b> {x1} <b>({xbwtijdgewerkt}/{xbwtotaaltijd})</b> uur.</li>" +
                        $"<li>Bezig met <b>{xstarted.Count}</b> {x3} " +
                        $"waarvan <b>{Math.Round(xstarted.Sum(x => x.TijdAanGewerkt()), 2)}</b> uur gewerkt.</li>" +
                        $"<li>Er is nog <b>{GetTijdOver()}</b> uur over tot einde van de week</li>" +
                        $"<li>Met Alles kan je op <b>{Werktijd.DatumNaTijd(DateTime.Now, TimeSpan.FromHours(xbwtijdover), null, null):dd/MM/yyyy HH:mm}</b> klaar zijn</li>";
                    //if (indeling.SelectedBewerking != null && SelectedPersoneel != null && SelectedPersoneel.Equals(indeling.Persoon))
                    //{
                    //    var bw = indeling.SelectedBewerking;
                    //    xall +=
                    //        $"<b>{bw.Naam} van {bw.ArtikelNr} | {bw.ProductieNr}</b>";
                    //}
                    return GetBaseHitmlBody(xall, "werkplek");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return string.Empty;
        }

        private void LoadProducties()
        {
            try
            {
                SetWaitUI();
                xWerkplaatsIndelingPanel.SuspendLayout();
                if (Manager.Database == null || Manager.Database.IsDisposed)
                    throw new Exception("Database is niet beschikbaar!");
                lock (Bewerkingen)
                {
                    Bewerkingen = Manager.Database.GetAllBewerkingen(false, true).Result.Where(x =>
                        x.State != ProductieState.Gereed && x.State != ProductieState.Verwijderd).ToList();
                }

                var xfirst = AddNewIndeling(default);
                var bws = Bewerkingen.Where(x => IsAllowed(x, xfirst)).ToList();
                productieListControl1.InitProductie(bws, true, true, true, false);

                if (Manager.Opties?.WerkplaatsIndeling != null)
                {
                    for (int i = 0; i < Manager.Opties.WerkplaatsIndeling.Count; i++)
                    {
                        var xwp = Manager.Opties.WerkplaatsIndeling[i];
                        AddNewIndeling(xwp);
                    }
                }
                
                xfirst.UpdateLabelText();
                xWerkplaatsIndelingPanel.ResumeLayout(true);
                SetSelected(null, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            StopWait();
        }

        private void UpdateTitle()
        {
            //if (SelectedWerkplek == null || SelectedWerkplek.IsDefault())
           // {
                var bws = Bewerkingen.Where(x=> IsAllowed(x, SelectedWerkplek)).ToList();

                var xselected = SelectedWerkplek == null || SelectedWerkplek.IsDefault()
                    ? "Alle"
                    : $"{SelectedWerkplek.Werkplek}";
                var x3 = bws.Count == 1 ? "Bewerking" : "Bewerkingen";
                var x2 = $"<b>{xselected} {bws.Count}</b> {x3}<br>" +
                         $"Totaal <b>{Math.Round(bws.Sum(x => x.TijdGewerkt), 2)} / {Math.Round(bws.Sum(x => x.DoorloopTijd), 2)}</b> uur gewerkt " +
                         $"waarvan nog <b>{Math.Round(bws.Sum(x => x.GetTijdOver()), 2)}</b> uur over aan productie";
                xGeselecteerdeGebruikerLabel.Text = x2;
                xGeselecteerdeGebruikerLabel.Refresh();
                var x1 = bws.Count == 1 ? "Bewerking" : "Bewerkingen";
                var xtext = $@"Werkplaats Indeling: {bws.Count} {x1}";
                var xindeling = GetIndeling(null);
                xindeling?.UpdateWerkplekInfo();
                OnStatusTextChanged(xtext);
                this.Invalidate();
           // }
        }

        private void ListProducties()
        {
            if (SelectedWerkplek != null)
            {
                //productieListControl1.InitProductie(Bewerkingen.Where(x => IsAllowed(x, SelectedWerkplek)).ToList(),
                //    false, true, true, false);
                //if (productieListControl1.DoSearch(SelectedWerkplek.Criteria))
                    productieListControl1.InitProductie(Bewerkingen.Where(x => IsAllowed(x, SelectedWerkplek)).ToList(),
                        false, true, true, false);
                    productieListControl1.DoSearch(SelectedWerkplek.Criteria);
            }

            UpdateTitle();
        }

        private bool IsAllowed(object value, string filter)
        {
            if (value is Bewerking bew)
            {
                if (SelectedWerkplek == null)
                {
                    return true;
                }

                return IsAllowed(bew, SelectedWerkplek);
            }

            return false;
        }

        private bool IsAllowed(Bewerking bew, WerkplekIndeling indeling)
        {
            if (indeling == null)
            {
                return true;
            }

            if (!bew.IsAllowed() || bew.State is ProductieState.Gereed or ProductieState.Verwijderd)
                return false;
            if (indeling.IsDefault())
            {
                var xindelingen = GetIndelingen(false);
                var xwps = Manager.BewerkingenLijst.GetWerkplekken(bew.Naam);
                if (xindelingen.Count > 0 && !xwps.Any(x =>
                        xindelingen.Any(w => string.Equals(x, w.Werkplek, StringComparison.CurrentCultureIgnoreCase))))
                    return false;
                if (indeling.ToonNietIngedeeld)
                {
                    
                        if (bew.WerkPlekken.Any(x => xindelingen.Any(w =>
                                string.Equals(w.Werkplek, x.Naam, StringComparison.CurrentCultureIgnoreCase))))
                            return false;
                }

                return true;
            }
            var xreturn = bew.WerkPlekken.Any(x => string.Equals(x.Naam, indeling.Werkplek,
                StringComparison.CurrentCultureIgnoreCase));
            return xreturn;
        }

        public WerkplekIndeling AddNewIndeling(string indeling)
        {
            try
            {
                var xgroup = new GroupBox();
                xgroup.Text = indeling ?? "Alle Bewerkingen";
                xgroup.Font = new Font(this.Font.FontFamily, 10, FontStyle.Bold);
                xgroup.Height = 140;
                xgroup.MouseEnter += WerkplekIndeling_MouseEnter;
                xgroup.MouseLeave += WerkplekIndeling_MouseLeave;
                xgroup.Click += Xpers_Click;
                xgroup.DoubleClick += Xpers_DoubleClick;
                var xpers = new WerkplekIndeling();
                xpers.FieldTextGetter = IndelingFieldTextGetter;
                

                xgroup.Width = xpers.Width;
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
                //xgroup.Dock = DockStyle.Top;
                xgroup.Controls.Add(xpers);
                xWerkplaatsIndelingPanel.Controls.Add(xgroup);
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

        private void UpdateDefault()
        {
            var xdef = GetDefaultIndeling();
            xdef?.UpdateWerkplekInfo();
            if (SelectedWerkplek != null && SelectedWerkplek.IsDefault())
            {
                ListProducties();
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
            if (SelectedWerkplek != null && string.Equals(werkplek.Werkplek, SelectedWerkplek.Werkplek,
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
                    indeling.UpdateLabelText();
                    if (indeling.Parent is GroupBox group)
                    {
                        group.Select();
                        group.Focus();
                    }
                }
                ListProducties();
            }
            else if (indeling != null)
            {
                indeling.BackColor = Color.White;
                indeling.IsSelected = false;
            }
            xDeletePersoneel.Enabled = SelectedWerkplek != null;
            
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
                var xwps = Manager.BewerkingenLijst.GetAlleWerkplekken(true).ToArray();
                var wpchooser = new WerkPlekChooser(xwps, null);
                wpchooser.Title = $"Voeg een nieuwe Werkplek toe";
                if (wpchooser.ShowDialog() == DialogResult.OK)
                {
                    var chosen = wpchooser.SelectedName;
                    if (chosen.Length > 0)
                    {
                        WerkplekIndeling xindeling = GetIndeling(chosen);
                        if (xindeling == null)
                        {
                            xindeling = AddNewIndeling(chosen);
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
            try
            {
                if (changedform?.Bewerkingen == null) return;
                this.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        if (changedform.Bewerkingen == null || changedform.Bewerkingen.Length == 0)
                        {
                            return;
                        }

                        bool xchanged = false;
                        for (int i = 0; i < changedform.Bewerkingen.Length; i++)
                        {
                            var bw = changedform.Bewerkingen[i];
                            bool valid = bw.IsAllowed() && bw.State != ProductieState.Verwijderd &&
                                         bw.State != ProductieState.Gereed;
                            bool changed = false;
                            lock (Bewerkingen)
                            {
                                var xindex = Bewerkingen.IndexOf(bw);
                               
                               
                                if (!valid && xindex > -1)
                                {
                                    Bewerkingen.RemoveAt(xindex);
                                    changed = true;
                                }
                                else if (xindex > -1)
                                {
                                    Bewerkingen[xindex] = bw;
                                    changed = true;
                                }
                                else if (valid)
                                {
                                    Bewerkingen.Add(bw);
                                    changed = true;
                                }
                            }

                            xchanged |= changed;
                            if (changed)
                            {
                                foreach (var wp in bw.WerkPlekken)
                                {
                                    var xindeling = GetIndeling(wp.Naam);
                                    if (xindeling?.SelectedBewerking != null && xindeling.SelectedBewerking.Equals(bw))
                                    {
                                        if (valid)
                                        {
                                            xindeling.SelectedBewerking = bw;
                                        }
                                        else xindeling.SelectedBewerking = null;
                                    }
                                }
                            }
                        }

                        if (xchanged)
                            UpdateIndelingen();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UpdateIndelingen()
        {
            var xindelingen = GetIndelingen(true);
            xindelingen.ForEach(x => x.UpdateWerkplekInfo());
        }

        private void Manager_OnFormulierDeleted(object sender, string id)
        {
            try
            {
                var bws = Bewerkingen
                    .Where(x => string.Equals(x.ProductieNr, id, StringComparison.CurrentCultureIgnoreCase)).ToList();
                if (bws.Count > 0)
                {
                    bool changed = false;
                    foreach (var bw in bws)
                    {
                        changed |= Bewerkingen.Remove(bw);
                    }

                    if (changed)
                    {
                        UpdateIndelingen();
                    }
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
    }
}
