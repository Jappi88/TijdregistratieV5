﻿using Controls;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rpm.Misc;
using Rpm.Various;
using Various;

namespace Forms
{
    public partial class WerkplaatsIndeling : Forms.MetroBase.MetroBaseForm
    {
        public WerkplekIndeling SelectedWerkplek { get; private set; }
        public WerkplaatsIndeling()
        {
            InitializeComponent();
            productieListControl1.ValidHandler = IsAllowed;
            productieListControl1.ItemCountChanged += ProductieListControl1_ItemCountChanged;
            productieListControl1.SelectedItemChanged += ProductieListControl1_SelectedItemChanged;
           
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
                    var valid = false;
                    Invoke(new MethodInvoker(() => valid = !IsDisposed));
                    if (!valid) return;
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
                        Invoke(new MethodInvoker(() => valid = !IsDisposed));
                        if (!valid) break;
                    }
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

        private List<WerkplekIndeling> GetIndelingen()
        {
            var xreturn = new List<WerkplekIndeling>();
            try
            {
                foreach (var xgroup in xWerkplaatsIndelingPanel.Controls.Cast<GroupBox>())
                {
                    if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is WerkplekIndeling xpers)
                    {
                      if(!xpers.Werkplek.IsDefault())
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
                if (indeling == null || indeling.Werkplek.IsDefault())
                {
                    var bws = productieListControl1.GetBewerkingen(false,false);
                    var x1 = bws.Count == 1 ? "productie" : "producties";
                    var pers = GetIndelingen();

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
                            $"<li>Met <b>{pers.Count}</b> {x2} kan je op <b>{Werktijd.DatumNaTijd(DateTime.Now, TimeSpan.FromHours(xbwtijdover / pers.Count), null, null)}</b> klaar zijn</li>";
                    }

                    return GetBaseHitmlBody(xall, "bewerkingen");
                }
                else
                {
                    var pers = indeling.Werkplek;
                    var bws = productieListControl1.GetBewerkingen(false,false).Where(x => IsAllowed(x, indeling))
                        .ToList();
                    var xklusjes = pers.Value.Where(x =>
                            x.State != ProductieState.Gereed && x.State != ProductieState.Verwijderd).SelectMany(x =>
                            x.WerkPlekken.Where(w =>
                                string.Equals(pers.Key, w.Naam,
                                    StringComparison.CurrentCultureIgnoreCase)))
                        .ToList();
                    var xstarted = xklusjes.Where(x => x.Werk.State == ProductieState.Gestart && x.IsActief())
                        .ToList();
                    var x2 = xklusjes.Count == 1 ? "productie" : "producties";
                    var x3 = xstarted.Count == 1 ? "productie" : "producties";
                    var x1 = bws.Count == 1 ? "productie" : "producties";
                    xall =
                        $"<li>Totaal <b>{bws.Count}</b> {x1} <b>({Math.Round(bws.Sum(x => x.GetTijdOver()), 2)})</b> uur.</li>" +
                        $"<li>Ingezet op <b>{xklusjes.Count}</b> {x2} " +
                        $"waarvan <b>{Math.Round(xklusjes.Sum(x => x.TijdAanGewerkt()), 2)}</b> uur gewerkt.</li>" +
                        $"<li>Bezig met <b>{xstarted.Count}</b> {x3} " +
                        $"waarvan <b>{Math.Round(xstarted.Sum(x => x.TijdAanGewerkt()), 2)}</b> uur gewerkt.</li>" +
                        $"<li>Er is nog <b>{GetTijdOver()}</b> uur over tot einde van de week</li>";
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
                var xfirst = AddNewIndeling(default);
                
                productieListControl1.InitProductie(true, true, true, true, true, true);

                if (Manager.Opties?.WerkplaatsIndeling != null)
                {
                    for (int i = 0; i < Manager.Opties.WerkplaatsIndeling.Count; i++)
                    {
                        var xwp = Manager.Opties.WerkplaatsIndeling[i];
                        AddNewIndeling(xwp);
                    }
                }
                
                xfirst?.UpdateLabelText();
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
            var bws = productieListControl1.GetBewerkingen(false,true);

            var xselected = SelectedWerkplek == null || SelectedWerkplek.Werkplek.IsDefault() ? "Alle" : $"{SelectedWerkplek.Werkplek.Key}";
            var x3 = bws.Count == 1 ? "Bewerking" : "Bewerkingen";
            var x2 = $"<b>{xselected} {bws.Count}</b> {x3}<br>" +
                     $"Totaal <b>{Math.Round(bws.Sum(x=> x.TijdGewerkt),2)} / {Math.Round(bws.Sum(x => x.DoorloopTijd),2)}</b> uur gewerkt " +
                     $"waarvan nog <b>{Math.Round(bws.Sum(x=> x.GetTijdOver()),2)}</b> uur over aan productie";
            xGeselecteerdeGebruikerLabel.Text = x2;
            bws = productieListControl1.GetBewerkingen(false, false);
            var x1 = bws.Count == 1 ? "Bewerking" : "Bewerkingen";
            this.Text = $@"Werkplaats Indeling: {bws.Count} {x1}";
            var xindeling = GetIndeling(null);
            xindeling?.InitWerkplek(default);
            this.Invalidate();
        }

        private void ListProducties()
        {
            productieListControl1.UpdateProductieList(false, false);
            UpdateTitle();
        }

        private bool IsAllowed(object value, string filter)
        {
            if (value is Bewerking bew)
            {
                if (SelectedWerkplek == null || SelectedWerkplek.Werkplek.IsDefault())
                {
                    return true;
                }

                var xname = SelectedWerkplek.Werkplek.Key;
                var xreturn = bew.WerkPlekken.Any(x => string.Equals(x.Naam, xname,
                    StringComparison.CurrentCultureIgnoreCase));
                return xreturn;
            }

            return false;
        }

        private bool IsAllowed(IProductieBase productie, WerkplekIndeling indeling)
        {
            if (indeling == null)
            {
                return true;
            }

            var xreturn = productie.GetWerkPlekken().Any(x => string.Equals(x.Naam, indeling.Werkplek.Key,
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
                var pair = new KeyValuePair<string, List<Bewerking>>(indeling, new List<Bewerking>());
               
               
                xpers.InitWerkplek(pair);
                var bws = Manager.Database.GetAllBewerkingen(false,true).Result;
                bws = bws.Where(x => IsAllowed(x, xpers)).ToList();
                if (bws.Count > 0)
                    xpers.Werkplek.Value.AddRange(bws);
                xgroup.Width = xpers.Width;
                xpers.DragDrop += Xpers_DragDrop;
                xgroup.MouseDown += xpers.IndelingMouseDown;
                xgroup.MouseMove += xpers.IndelingMouseMove;
                xpers.VerwijderWerkplaats += Xpers_VerwijderIndeling;
                xpers.Dock = DockStyle.Fill;
                xpers.MouseEnter += WerkplekIndeling_MouseEnter;
                xpers.MouseLeave += WerkplekIndeling_MouseLeave;
                xpers.Click += Xpers_Click;
                xpers.DoubleClick += Xpers_DoubleClick;
                //xgroup.Dock = DockStyle.Top;
                xgroup.Controls.Add(xpers);
                xWerkplaatsIndelingPanel.Controls.Add(xgroup);
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

        private void Xpers_DragDrop(object sender, DragEventArgs e)
        {
            flowLayoutPanel1_DragDrop(xWerkplaatsIndelingPanel, e);
        }

        private void Xpers_VerwijderIndeling(object sender, EventArgs e)
        {
            try
            {
                if (sender is WerkplekIndeling indeling && !indeling.Werkplek.IsDefault())
                {
                    DeleteWerkplaats(indeling.Werkplek.Key);
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

            if (xindeling == null || xindeling.Werkplek.IsDefault())
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
            if (SelectedWerkplek != null && string.Equals(werkplek.Werkplek.Key, SelectedWerkplek.Werkplek.Key,
                    StringComparison.CurrentCultureIgnoreCase)) return;
            foreach (var xgroup in xWerkplaatsIndelingPanel.Controls.Cast<GroupBox>())
            {
                if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is WerkplekIndeling xpers)
                {
                    if (xpers.Werkplek.IsDefault() && (werkplek == null || werkplek.Werkplek.IsDefault()))
                    {
                        SetSelected(xpers, true);
                        continue;
                    }

                    if (xpers.Werkplek.IsDefault())
                    {
                        SetSelected(xpers, false);
                    }
                    else if (string.Equals(werkplek?.Werkplek.Key, xpers.Werkplek.Key, StringComparison.CurrentCultureIgnoreCase))
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
                if (indeling.Werkplek.IsDefault() && (SelectedWerkplek == null || SelectedWerkplek.Werkplek.IsDefault()))
                {
                    indeling.BackColor = Color.LightBlue;
                }
                else if (indeling.Werkplek.IsDefault())
                {
                    indeling.BackColor = color;
                }
                else
                {
                    indeling.BackColor = string.Equals(indeling.Werkplek.Key, SelectedWerkplek.Werkplek.Key,
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
                        if (xpers.Werkplek.IsDefault()) continue;
                        if (string.Equals(xpers.Werkplek.Key, plek.Werkplek.Key, StringComparison.CurrentCultureIgnoreCase))
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
                        if (xpers.Werkplek.IsDefault() && string.IsNullOrEmpty(werkplek))
                        {
                            return xpers;
                        }

                        if (xpers.Werkplek.IsDefault()) continue;
                        if (string.Equals(xpers.Werkplek.Key, werkplek, StringComparison.CurrentCultureIgnoreCase))
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
                        if (xpers.Werkplek.IsDefault()) continue;
                        if (string.Equals(xpers.Werkplek.Key, werkplek, StringComparison.CurrentCultureIgnoreCase))
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
                if (SelectedWerkplek == null || SelectedWerkplek.Werkplek.IsDefault()) return;
                DeleteWerkplaats(SelectedWerkplek.Werkplek.Key);
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
                        if (xpers.Werkplek.IsDefault()) continue;
                        if (string.Equals(xpers.Werkplek.Key, werkplek, StringComparison.CurrentCultureIgnoreCase))
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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void WerkplaatsIndeling_Shown(object sender, EventArgs e)
        {
            LoadProducties();
            productieListControl1.InitEvents();
            Manager.OnPersoneelDeleted += Manager_OnPersoneelDeleted;
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
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
                        foreach (var xgroup in xWerkplaatsIndelingPanel.Controls.Cast<GroupBox>())
                        {
                            if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is WerkplekIndeling xindeling)
                            {
                                if (xindeling.Werkplek.IsDefault()) continue;
                                if (IsAllowed(changedform, xindeling))
                                {
                                    xindeling.UpdateLabelText();
                                    if (xindeling.SelectedBewerking != null)
                                    {
                                        var xnew = changedform.Bewerkingen?.FirstOrDefault(x =>
                                            x.Equals(xindeling.SelectedBewerking));
                                        if (xnew != null)
                                            xindeling.SelectedBewerking = xnew;
                                    }

                                    var bws = changedform.Bewerkingen.Where(x =>
                                        x.WerkPlekken.Any(w => string.Equals(w.Naam, xindeling.Werkplek.Key,
                                            StringComparison.CurrentCultureIgnoreCase))).ToList();
                                    var xremove = xindeling.Werkplek.Value.Where(x =>
                                        string.Equals(x.ProductieNr, changedform.ProductieNr,
                                            StringComparison.CurrentCultureIgnoreCase) &&
                                        !x.WerkPlekken.Any(w => string.Equals(w.Naam, xindeling.Werkplek.Key,
                                            StringComparison.CurrentCultureIgnoreCase))).ToList();
                                    if (xremove.Count > 0)
                                        xremove.ForEach(r => xindeling.Werkplek.Value.Remove(r));

                                    foreach (var bw in bws)
                                    {
                                        if (!bw.WerkPlekken.Any(w => string.Equals(w.Naam, xindeling.Werkplek.Key,
                                                StringComparison.CurrentCultureIgnoreCase)))
                                            continue;
                                        var xindex = xindeling.Werkplek.Value.IndexOf(bw);
                                        if (xindex > -1)
                                            xindeling.Werkplek.Value[xindex] = bw;
                                        else xindeling.Werkplek.Value.Add(bw);
                                    }

                                }
                            }
                        }
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

        private void Manager_OnPersoneelDeleted(object sender, string id)
        {
            if (WerkplekExists(id))
                DeleteWerkplaats(id);
        }

        private void WerkplaatsIndeling_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Manager.Opties != null)
            {
                Manager.Opties.WerkplaatsIndeling = GetIndelingen().Select(x => x.Werkplek.Key).ToList();
            }

            Manager.OnPersoneelDeleted -= Manager_OnPersoneelDeleted;
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            productieListControl1.DetachEvents();
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