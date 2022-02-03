using Controls;
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
        public Personeel SelectedPersoneel { get; private set; }
        public WerkplaatsIndeling()
        {
            InitializeComponent();
            productieListControl1.ValidHandler = IsAllowed;
            productieListControl1.ItemCountChanged += ProductieListControl1_ItemCountChanged;
            productieListControl1.SelectedItemChanged += ProductieListControl1_SelectedItemChanged;
           
        }

        private void ProductieListControl1_SelectedItemChanged(object sender, EventArgs e)
        {
            if (SelectedPersoneel != null)
            {
                var xindeling = GetIndeling(SelectedPersoneel);
                if (productieListControl1.ProductieLijst.SelectedObjects.Count == 0)
                {
                    var xnum = productieListControl1.ProductieLijst.Objects?.GetEnumerator();
                    if (xnum != null && xnum.MoveNext())
                        productieListControl1.SelectedItem = xnum.Current;
                      
                } 
                xindeling?.SetBewerking(productieListControl1.SelectedItem as Bewerking);
            }
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

        private List<Personeel> GetPersonen()
        {
            var xreturn = new List<Personeel>();
            try
            {
                foreach (var xgroup in xPersoneelIndelingPanel.Controls.Cast<GroupBox>())
                {
                    if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is PersoonIndeling xpers)
                    {
                      if(xpers.Persoon != null)
                          xreturn.Add(xpers.Persoon);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return xreturn;
        }

        private double GetTijdOver(Personeel pers)
        {
            var xweek = DateTime.Now.GetWeekNr();
            var weekrange = Functions.GetWeekRange(xweek, DateTime.Now.Year, pers.WerkRooster);
            return Math.Round(Werktijd.TijdGewerkt(new TijdEntry(DateTime.Now, weekrange.EndDate, pers.WerkRooster),
                pers.WerkRooster, null, pers.VrijeDagen.ToDictionary()).TotalHours, 2);
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

        private string IndelingFieldTextGetter(PersoonIndeling indeling)
        {

            try
            {
                string xall = null;
                if (indeling?.Persoon == null)
                {
                    var bws = productieListControl1.GetBewerkingen(false,false);
                    var x1 = bws.Count == 1 ? "productie" : "producties";
                    var pers = GetPersonen();

                    var x2 = pers.Count == 1 ? "persoon" : "personen";
                    var xtijdover = Math.Round(pers.Sum(GetTijdOver));
                    var xbwtijdover = Math.Round(bws.Sum(x => x.GetTijdOver()), 2);
                    xall =
                        $"<li>Totaal <b>{bws.Count}</b> {x1} " +
                        $"<b>({xbwtijdover})</b> uur.</li>";
                    if (pers.Count > 0)
                    {
                        xall += $"<li>Met <b>{pers.Count}</b> {x2} is dat <b>{Math.Round(xbwtijdover / pers.Count, 2)}</b> uur per persoon</li>" +
                            $"<li><b>{pers.Count}</b> {x2} met nog <b>{xtijdover}</b> uur tot einde van de week.</li>" +
                            $"<li>Met <b>{pers.Count}</b> {x2} kan je op <b>{Werktijd.DatumNaTijd(DateTime.Now, TimeSpan.FromHours(xbwtijdover / pers.Count), null, null)}</b> klaar zijn</li>";
                    }

                    return GetBaseHitmlBody(xall, "bewerkingen");
                }
                else
                {
                    var pers = indeling.Persoon;
                    var bws = productieListControl1.GetBewerkingen(false,false).Where(x => IsAllowed(x, pers))
                        .ToList();
                    var xklusjes = pers.Klusjes.Where(x =>
                        x.Status != ProductieState.Gereed && x.Status != ProductieState.Verwijderd).ToList();
                    var xstarted = xklusjes.Where(x => x.Status == ProductieState.Gestart && x.IsActief).ToList();
                    var x2 = xklusjes.Count == 1 ? "klus" : "klusjes";
                    var x3 = xstarted.Count == 1 ? "klus" : "klusjes";
                    var x1 = bws.Count == 1 ? "productie" : "producties";
                    xall =
                        $"<li>Totaal <b>{bws.Count}</b> {x1} <b>({Math.Round(bws.Sum(x => x.GetTijdOver()), 2)})</b> uur.</li>" +
                        $"<li>Ingezet op <b>{xklusjes.Count}</b> {x2} " +
                        $"waarvan <b>{Math.Round(xklusjes.Sum(x => x.TijdGewerkt(pers.VrijeDagen.ToDictionary(), pers.WerkRooster).TotalHours), 2)}</b> uur gewerkt.</li>" +
                        $"<li>Bezig met <b>{xstarted.Count}</b> {x3} " +
                        $"waarvan <b>{Math.Round(xstarted.Sum(x => x.TijdGewerkt(pers.VrijeDagen.ToDictionary(), pers.WerkRooster).TotalHours), 2)}</b> uur gewerkt.</li>" +
                        $"<li><b>{GetTijdOver(pers)}</b> uur over tot einde van de week</li>";
                    //if (indeling.SelectedBewerking != null && SelectedPersoneel != null && SelectedPersoneel.Equals(indeling.Persoon))
                    //{
                    //    var bw = indeling.SelectedBewerking;
                    //    xall +=
                    //        $"<b>{bw.Naam} van {bw.ArtikelNr} | {bw.ProductieNr}</b>";
                    //}
                    return GetBaseHitmlBody(xall, "personeel");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return string.Empty;
        }

        private async void LoadProducties()
        {
            try
            {
                SetWaitUI();
                xPersoneelIndelingPanel.SuspendLayout();
                var xfirst = AddPersoneel(null);
                
                productieListControl1.InitProductie(true, true, true, true, true, true);

                if (Manager.Opties?.PersoneelIndeling != null)
                {
                    for (int i = 0; i < Manager.Opties.PersoneelIndeling.Count; i++)
                    {
                        var xpers = Manager.Opties.PersoneelIndeling[i];
                        var xp = await Manager.Database.GetPersoneel(xpers);
                        if (xp != null)
                        {
                             AddPersoneel(xp);
                        }
                        else Manager.Opties.PersoneelIndeling.RemoveAt(i--);
                    }
                }
                
                xfirst?.UpdateLabelText();
                xPersoneelIndelingPanel.ResumeLayout(true);
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
            
            string x2 = String.Empty;
            var xselected = SelectedPersoneel == null? "Alle" : $"{SelectedPersoneel.PersoneelNaam}";
            var x3 = bws.Count == 1 ? "Bewerking" : "Bewerkingen";
            x2 = $"<b>{xselected} {bws.Count}</b> {x3}<br>" +
                 $"Totaal <b>{Math.Round(bws.Sum(x=> x.TijdGewerkt),2)} / {Math.Round(bws.Sum(x => x.DoorloopTijd),2)}</b> uur gewerkt " +
                 $"waarvan nog <b>{Math.Round(bws.Sum(x=> x.GetTijdOver()),2)}</b> uur over aan productie";
            xGeselecteerdeGebruikerLabel.Text = x2;
            bws = productieListControl1.GetBewerkingen(false, false);
            var x1 = bws.Count == 1 ? "Bewerking" : "Bewerkingen";
            this.Text = $@"Totaal {bws.Count} {x1}";
            var xindeling = GetIndeling(null);
            xindeling?.InitPersoneel((Personeel)null);
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
                if (SelectedPersoneel == null)
                {
                    return true;
                }
                var xreturn =  bew.GetPersoneel().Any(x => string.Equals(x.PersoneelNaam, SelectedPersoneel.PersoneelNaam,
                    StringComparison.CurrentCultureIgnoreCase));
                return xreturn;
            }

            return false;
        }

        private bool IsAllowed(IProductieBase productie, Personeel pers)
        {
            if (pers == null)
            {
                return true;
            }

            var xreturn = productie.Personen.Any(x => string.Equals(x.PersoneelNaam, pers.PersoneelNaam,
                StringComparison.CurrentCultureIgnoreCase));
            return xreturn;
        }

        public PersoonIndeling AddPersoneel(Personeel persoon)
        {
            try
            {
                var xgroup = new GroupBox();
                xgroup.Text = persoon == null? "Alle Bewerkingen" : persoon.PersoneelNaam;
                xgroup.Font = new Font(this.Font.FontFamily, 10, FontStyle.Bold);
                xgroup.Height = 140;
                xgroup.MouseEnter += PersoonIndeling_MouseEnter;
                xgroup.MouseLeave += PersoonIndeling_MouseLeave;
                xgroup.Click += Xpers_Click;
                xgroup.DoubleClick += Xpers_DoubleClick;
                var xpers = new PersoonIndeling();
                xpers.FieldTextGetter = IndelingFieldTextGetter;
                xpers.InitPersoneel(persoon);
                xgroup.Width = xpers.Width;
                xpers.DragDrop += Xpers_DragDrop;
                xgroup.MouseDown += xpers.IndelingMouseDown;
                xgroup.MouseMove += xpers.IndelingMouseMove;
                xpers.VerwijderPersoneel += Xpers_VerwijderPersoneel;
                xpers.Dock = DockStyle.Fill;
                xpers.MouseEnter += PersoonIndeling_MouseEnter;
                xpers.MouseLeave += PersoonIndeling_MouseLeave;
                xpers.Click += Xpers_Click;
                xpers.DoubleClick += Xpers_DoubleClick;
                //xgroup.Dock = DockStyle.Top;
                xgroup.Controls.Add(xpers);
                xPersoneelIndelingPanel.Controls.Add(xgroup);
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
            flowLayoutPanel1_DragDrop(xPersoneelIndelingPanel, e);
        }

        private void Xpers_VerwijderPersoneel(object sender, EventArgs e)
        {
            try
            {
                if (sender is PersoonIndeling indeling && indeling.Persoon != null)
                {
                    DeletePersoneel(indeling.Persoon.PersoneelNaam);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private async void Xpers_DoubleClick(object sender, EventArgs e)
        {
            PersoonIndeling xindeling = sender as PersoonIndeling;
            if (xindeling == null)
            {
                if (sender is GroupBox group && group.Controls.Count > 0 && group.Controls[0] is PersoonIndeling x)
                {
                    xindeling = x;
                }
            }

            if (xindeling?.Persoon == null)
            {
                AddNewPersoneel();
                return;
            }
            var add = new AddPersoneel(xindeling.Persoon);
            if (add.ShowDialog() == DialogResult.OK)
            {
                xindeling.Persoon = add.PersoneelLid;
                if (!await Personeel.UpdateKlusjes(this,xindeling.Persoon)) return;
                await Manager.Database.UpSert(xindeling.Persoon, $"{xindeling.Persoon.PersoneelNaam} Aangepast!");
            }
        }

        private void Xpers_Click(object sender, EventArgs e)
        {
            if (sender is GroupBox group && group.Controls.Count > 0 && group.Controls[0] is PersoonIndeling x)
            {
                SelectPersoneel(x.Persoon);
            }
            else if(sender is PersoonIndeling xpers)
            {
                SelectPersoneel(xpers.Persoon);
            }
        }

        public void SelectPersoneel(Personeel persoon)
        {
            if (SelectedPersoneel != null && SelectedPersoneel.Equals(persoon)) return;
            foreach (var xgroup in xPersoneelIndelingPanel.Controls.Cast<GroupBox>())
            {
                if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is PersoonIndeling xpers)
                {
                    if (xpers.Persoon == null && persoon == null)
                    {
                        SetSelected(xpers, true);
                        continue;
                    }

                    if (xpers.Persoon == null)
                    {
                        SetSelected(xpers, false);
                    }
                    else if (xpers.Persoon.Equals(persoon))
                    {
                        SetSelected(xpers, true);
                    }
                    else SetSelected(xpers, false);
                }
            }
        }

        private void SetSelected(PersoonIndeling indeling, bool selected)
        {
           
            if (selected)
            {
                indeling ??= GetIndeling(null);
                SelectedPersoneel = indeling?.Persoon;

                foreach (var xgroup in xPersoneelIndelingPanel.Controls.Cast<GroupBox>())
                {
                    if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is PersoonIndeling xpers)
                    {
                        SetColor(xpers, Color.White);
                        xpers.IsSelected = false;
                        xpers.UpdateGebruikerInfo();
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
            xDeletePersoneel.Enabled = SelectedPersoneel != null;
            
        }

        private void PersoonIndeling_MouseEnter(object sender, EventArgs e)
        {
            if (sender is GroupBox group && group.Controls.Count > 0 && group.Controls[0] is PersoonIndeling xpers)
            {
                SetColor(xpers, Color.AliceBlue);
            }
            else if (sender is PersoonIndeling xindeling)
            {
                SetColor(xindeling, Color.AliceBlue);
            }
        }

        private void SetColor(PersoonIndeling indeling, Color color)
        {
            if (indeling != null)
            {
                if (indeling.Persoon == null && SelectedPersoneel == null)
                {
                    indeling.BackColor = Color.LightBlue;
                }
                else if (indeling.Persoon == null)
                {
                    indeling.BackColor = color;
                }
                else
                {
                    if (indeling.Persoon.Equals(SelectedPersoneel))
                    {
                        indeling.BackColor = Color.LightBlue;
                    }
                    else
                        indeling.BackColor = color;
                }
            }
        }

        private void PersoonIndeling_MouseLeave(object sender, EventArgs e)
        {
            if (sender is GroupBox group && group.Controls.Count > 0 && group.Controls[0] is PersoonIndeling xpers)
            {
                SetColor(xpers, Color.White);
            }
            else if(sender is PersoonIndeling xindeling)
            {
                SetColor(xindeling, Color.White);
            }
        }

        private void AddNewPersoneel()
        {
            try
            {
                var xpers = new PersoneelsForm(true);
                if (xpers.ShowDialog() == DialogResult.OK)
                {
                    var chosen = xpers.SelectedPersoneel;
                    if (chosen.Length > 0)
                    {
                        PersoonIndeling xindeling = null;
                        foreach (var chose in chosen)
                        {
                            xindeling = GetIndeling(chose);
                            if (xindeling == null)
                            {
                                xindeling = AddPersoneel(chose);
                            }
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
            AddNewPersoneel();
        }

        public bool PersoneelExists(Personeel pers)
        {
            try
            {
                foreach (var xgroup in xPersoneelIndelingPanel.Controls.Cast<GroupBox>())
                {
                    if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is PersoonIndeling xpers)
                    {
                        if (xpers.Persoon == null) continue;
                        if (xpers.Persoon.Equals(pers))
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

        public PersoonIndeling GetIndeling(Personeel pers)
        {
            try
            {
                foreach (var xgroup in xPersoneelIndelingPanel.Controls.Cast<GroupBox>())
                {
                    if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is PersoonIndeling xpers)
                    {
                        if (xpers.Persoon == null && pers == null)
                        {
                            return xpers;
                        }

                        if (xpers.Persoon == null) continue;
                        if (xpers.Persoon.Equals(pers))
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

        public bool PersoneelExists(string pers)
        {
            try
            {
                foreach (var xgroup in xPersoneelIndelingPanel.Controls.Cast<GroupBox>())
                {
                    if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is PersoonIndeling xpers)
                    {
                        if (xpers.Persoon == null) continue;
                        if (string.Equals(xpers.Persoon.PersoneelNaam, pers, StringComparison.CurrentCultureIgnoreCase))
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
                if (SelectedPersoneel == null) return;
                DeletePersoneel(SelectedPersoneel.PersoneelNaam);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void DeletePersoneel(string personeelnaam)
        {
            try
            {
                GroupBox xdelete = null;
                foreach (var xgroup in xPersoneelIndelingPanel.Controls.Cast<GroupBox>())
                {
                    if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is PersoonIndeling xpers)
                    {
                        if (xpers.Persoon == null) continue;
                        if (string.Equals(xpers.Persoon.PersoneelNaam, personeelnaam, StringComparison.CurrentCultureIgnoreCase))
                        {
                            xgroup.MouseEnter -= PersoonIndeling_MouseEnter;
                            xgroup.MouseLeave -= PersoonIndeling_MouseLeave;
                            xgroup.Click -= Xpers_Click;
                            xgroup.DoubleClick -= Xpers_DoubleClick;
                            xdelete = xgroup;
                            xpers.MouseEnter -= PersoonIndeling_MouseEnter;
                            xpers.MouseLeave -= PersoonIndeling_MouseLeave;
                            xpers.Click -= Xpers_Click;
                            xpers.DoubleClick -= Xpers_DoubleClick;
                            xpers.VerwijderPersoneel -= Xpers_VerwijderPersoneel;
                            break;
                        }
                    }
                }

                if (xdelete != null)
                {
                    xPersoneelIndelingPanel.SuspendLayout();
                    xPersoneelIndelingPanel.Controls.Remove(xdelete);
                    xPersoneelIndelingPanel.ResumeLayout(true);
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
            Manager.OnPersoneelChanged += Manager_OnPersoneelChanged;
            Manager.OnPersoneelDeleted += Manager_OnPersoneelDeleted;
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            try
            {
                this.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        foreach (var xgroup in xPersoneelIndelingPanel.Controls.Cast<GroupBox>())
                        {
                            if (xgroup.Controls.Count > 0 && xgroup.Controls[0] is PersoonIndeling xindeling)
                            {
                                if (xindeling.Persoon == null) continue;
                                if (IsAllowed(changedform, xindeling.Persoon))
                                {
                                    xindeling.UpdateLabelText();
                                    if (xindeling.SelectedBewerking != null)
                                    {
                                        var xnew = changedform.Bewerkingen?.FirstOrDefault(x =>
                                            x.Equals(xindeling.SelectedBewerking));
                                        if (xnew != null)
                                            xindeling.SelectedBewerking = xnew;
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
            if (PersoneelExists(id))
                DeletePersoneel(id);
        }

        private void Manager_OnPersoneelChanged(object sender, Personeel user)
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    try
                    {
                        var indeling = GetIndeling(user);
                        indeling?.InitPersoneel(user);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void WerkplaatsIndeling_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Manager.Opties != null)
            {
                Manager.Opties.PersoneelIndeling = GetPersonen().Select(x => x.PersoneelNaam).ToList();
            }
            Manager.OnPersoneelChanged -= Manager_OnPersoneelChanged;
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
