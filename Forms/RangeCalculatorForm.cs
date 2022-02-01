using MetroFramework.Forms;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class RangeCalculatorForm : MetroForm
    {
        //private List<Bewerking> _loaded = new List<Bewerking>();
        public RangeCalculatorForm()
        {
            InitializeComponent();
            productieListControl1.IsBewerkingView = true;
            productieListControl1.ValidHandler = IsAllowed;
            productieListControl1.ItemCountChanged += ProductieListControl1_ItemCountChanged;
            if (Manager.BewerkingenLijst == null) return;
            
            var bewerkingen = Manager.BewerkingenLijst.GetAllEntries().Select(x => (object) x.Naam).ToArray();
            var afdelingen = Manager.BewerkingenLijst.GetAlleWerkplekken().Select(x => (object) x).ToArray();
            xwerkplekken.Items.AddRange(afdelingen);
            xbewerkingen.Items.AddRange(bewerkingen);
        }

        private void ProductieListControl1_ItemCountChanged(object sender, EventArgs e)
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(UpdateStatusLabel));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private int GetProductieImageIndex(Bewerking prod)
        {
            switch (prod.State)
            {
                case ProductieState.Gestopt:
                    if (prod.IsNieuw)
                        return 0;
                    if (prod.TeLaat)
                        return 1;
                    return 3;

                case ProductieState.Gestart:
                    return 2;

                case ProductieState.Gereed:
                    return 5;

                case ProductieState.Verwijderd:
                    return 4;
            }

            return 3;
        }

        private void xartikelnrcheck_CheckedChanged(object sender, EventArgs e)
        {
            xcriteria.Enabled = xcriteriacheckbox.Checked;
        }

        private void xwerkplekcheck_CheckedChanged(object sender, EventArgs e)
        {
            xwerkplekken.Enabled = xwerkplekcheck.Checked;
        }

        private void xvanafcheck_CheckedChanged(object sender, EventArgs e)
        {
            xvanafdate.Enabled = xvanafcheck.Checked;
        }

        private void xbewerkingcheck_CheckedChanged(object sender, EventArgs e)
        {
            xbewerkingen.Enabled = xbewerkingcheck.Checked;
        }

        private void xtotcheck_CheckedChanged(object sender, EventArgs e)
        {
            xtotdate.Enabled = xtotcheck.Checked;
        }

        private async void xverwerkb_Click(object sender, EventArgs e)
        {
            if (_isbussy) return;
            await Verwerk();
        }

        private bool DoCheck()
        {
            if (xwerkplekcheck.Checked && string.IsNullOrEmpty(xwerkplekken.Text.Trim()))
                XMessageBox.Show(
                    this, "Werkplekken is aangevinkt, maar het veld is leeg!\nvul in of kies werkplek en probeer het opniew.",
                    "Werkplekken", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (xbewerkingcheck.Checked && string.IsNullOrEmpty(xbewerkingen.Text.Trim()))
                XMessageBox.Show(
                    this, "Bewerking is aangevinkt, maar het veld is leeg!\nvul in of kies een bewerking en probeer het opniew.",
                    "Bewerking", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (xcriteriacheckbox.Checked && string.IsNullOrWhiteSpace(xcriteria.Text.Trim()))
                XMessageBox.Show(
                    this, "Artikelnr, productienr of een omschrijving is aangevinkt, maar het veld is leeg!\nvul in een criteria waar de productie aan moet voldoen.",
                    "Criteria", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                return true;

            return false;
        }

        public RangeFilter ShowFilter = new RangeFilter();
        public struct RangeFilter
        {
            public bool Enabled;
            public bool VanafCheck;
            public DateTime VanafTime;
            public bool TotCheck;
            public DateTime TotTime;
            public string Criteria;
            public string werkPlek;
            public string Bewerking;
        }

        private void SetFilter(RangeFilter filter)
        {
            if (!filter.Enabled) return;
            var x = filter;
            xvanafcheck.Checked = x.VanafCheck;
            xtotcheck.Checked = x.TotCheck;
            if (x.VanafCheck)
                xvanafdate.SetValue(x.VanafTime);
            if (x.TotCheck)
                xtotdate.SetValue(x.TotTime);
            if (!string.IsNullOrEmpty(x.Criteria))
            {
                xcriteria.Text = x.Criteria;
                xcriteriacheckbox.Checked = true;
                xcriteria.Select();
            }
            else xcriteriacheckbox.Checked = false;
            if (!string.IsNullOrEmpty(x.werkPlek))
            {
                xwerkplekken.SelectedItem = x.werkPlek;
                xwerkplekcheck.Checked = true;
                xwerkplekken.Select();
            }
            else xwerkplekcheck.Checked = false;
            if (!string.IsNullOrEmpty(x.Bewerking))
            {
                xbewerkingen.SelectedItem = x.Bewerking;
                xbewerkingcheck.Checked = true;
                xbewerkingen.Select();
            }
            else xbewerkingcheck.Checked = false;

        }

        private bool IsAllowed(object value, string filter)
        {
            try
            {
                if (value is not Bewerking bew) return false;
                if (!string.IsNullOrEmpty(filter) && !bew.ContainsFilter(filter)) return false;
                if (!ShowFilter.Enabled)
                    return false;

                var flag = true;

                if (ShowFilter.VanafCheck)
                {
                    switch (bew.State)
                    {
                        case ProductieState.Gestopt:
                            flag &= bew.TijdGestopt.IsDefault() ? bew.DatumToegevoegd >= ShowFilter.VanafTime : bew.TijdGestopt >= ShowFilter.VanafTime;
                            break;
                        case ProductieState.Gestart:
                            flag &= bew.GestartOp() >= ShowFilter.VanafTime;
                            break;
                        case ProductieState.Gereed:
                            flag &= bew.DatumGereed >= ShowFilter.VanafTime;
                            break;
                        case ProductieState.Verwijderd:
                            flag &= bew.DatumVerwijderd >= ShowFilter.VanafTime;
                            break;
                    }
                        
                }

                if (ShowFilter.TotCheck)
                    switch (bew.State)
                    {
                        case ProductieState.Gestopt:
                            flag &= bew.TijdGestopt.IsDefault() ? bew.DatumToegevoegd <= ShowFilter.TotTime : bew.TijdGestopt <= ShowFilter.TotTime;
                            break;
                        case ProductieState.Gestart:
                            flag &= bew.GestartOp() <= ShowFilter.TotTime;
                            break;
                        case ProductieState.Gereed:
                            flag &= bew.DatumGereed <= ShowFilter.TotTime;
                            break;
                        case ProductieState.Verwijderd:
                            flag &= bew.DatumVerwijderd <= ShowFilter.TotTime;
                            break;
                    }

                if (!flag)
                    return false;

                if (!string.IsNullOrEmpty(ShowFilter.Criteria) && !bew.ContainsFilter(ShowFilter.Criteria))
                    return false;

                if (!string.IsNullOrEmpty(ShowFilter.werkPlek) &&
                    bew.WerkPlekken.All(x =>
                        !string.Equals(x.Naam, ShowFilter.werkPlek, StringComparison.CurrentCultureIgnoreCase)))
                    return false;

                if (!string.IsNullOrEmpty(ShowFilter.Bewerking) && !string.Equals(bew.Naam.Split('[')[0],
                    ShowFilter.Bewerking, StringComparison.CurrentCultureIgnoreCase))
                    return false;

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        private void SetProgressLabelText(string text)
        {
            if (xprogresslabel.InvokeRequired)
                xprogresslabel.Invoke(new MethodInvoker(() =>
                {
                    xprogresslabel.Text = text;
                    xprogresslabel.Invalidate();
                }));
            else
            {
                xprogresslabel.Text = text;
                xprogresslabel.Invalidate();
            }

            //Application.DoEvents();
        }

        private void EnableProgressLabel(bool enable)
        {
            if (xprogresslabel.InvokeRequired)
                xprogresslabel.Invoke(new MethodInvoker(() => xprogresslabel.Visible = enable));
            else
                xprogresslabel.Visible = enable;
        }

        private bool _isbussy = false;

        public void Show(RangeFilter filter)
        {
            try
            {
                base.Show();
                SetFilter(filter);
                _=Verwerk();
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message,"Fout", MessageBoxIcon.Error);
            }
        }

        private Task Verwerk()
        {
            return Task.Run(new Action(()=>  this.Invoke(new Action(async () =>
            {
                int count = 0;
                try
                {
                    if (_isbussy) return;

                    if (DoCheck())
                    {
                        _isbussy = true;
                        ShowFilter.Enabled = true;
                        ShowFilter.VanafCheck = xvanafcheck.Checked;
                        ShowFilter.VanafTime = xvanafdate.Value;
                        ShowFilter.TotCheck = xtotcheck.Checked;
                        ShowFilter.TotTime = xtotdate.Value;
                        ShowFilter.Bewerking = xbewerkingcheck.Checked ? xbewerkingen.Text.Trim() : null;
                        ShowFilter.werkPlek = xwerkplekcheck.Checked ? xwerkplekken.Text.Trim() : null;
                        ShowFilter.Criteria = xcriteriacheckbox.Checked ? xcriteria.Text.Trim() : null;
                        EnableProgressLabel(true);
                        SetProgressLabelText("Producties Laden...");
                        var ids = await Manager.GetAllProductieIDs(true,true);
                        int cur = 0;
                        int max = ids.Count;
                        var loaded = new List<Bewerking>();
                        foreach (var id in ids)
                        {
                            if (IsDisposed || !Visible) break;
                            if (string.IsNullOrEmpty(id)) continue;
                            var x = await Manager.Database.GetProductie(id);
                            if (x == null) continue;
                            if (x.Bewerkingen is {Length: > 0})
                            {
                                var bws = x.Bewerkingen.Where(b => IsAllowed(b, null)).ToArray();
                                if (bws.Length > 0)
                                {
                                    loaded.AddRange(bws);
                                    count += bws.Length;
                                }
                            }

                            cur++;
                            double perc = (double)cur / max;
                            SetProgressLabelText($"Producties laden ({perc:0.0%})...");
                        }
                        if (IsDisposed) return;
                        productieListControl1.InitProductie(loaded, true, true, false);
                    }
                    else
                        return;
                }
                catch (Exception e)
                {
                    XMessageBox.Show(this, e.Message, "Fout", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                _isbussy = false;

                EnableProgressLabel(false);
                UpdateStatusLabel();
                return;
            }))));
        }

        private void UpdateStatusLabel()
        {
            var bws = productieListControl1.ProductieLijst.Objects?.Cast<Bewerking>().ToList();
            //var count = bws?.Count ?? 0;
            //if (count > 0 && bws != null)
            //{
            //    var x1 = count == 1 ? "resulaat" : "resultaten";
            //    var xvalue = $"{count} {x1} gevonden!";
            //    var gemaakt = bws.Sum(x => x.AantalGemaakt);
            //    var tijd = Math.Round(bws.Sum(x => x.TijdGewerkt), 2);
            //    var pu = (int)(tijd > 0 && gemaakt > 0 ? gemaakt / tijd : 0);
            //    var x2 = pu == 1 ? "stuk" : "stuks";
            //    xvalue += $"\nAantal gemaakt: {gemaakt}.";
            //    xvalue += $" in totaal {tijd} uur.";
            //    xvalue += $" Dat is gemiddeld {pu} {x2} per uur.";
            //    xoutput.Text = xvalue;
            //}
            //else
            //{
            //    xoutput.Text = "Geen resultaten gevonden!";
            //}
            if (ShowFilter.Enabled)
            {
                var xtitle = $"Resultaten voor ";
                if (!string.IsNullOrEmpty(ShowFilter.Criteria))
                    xtitle += $"criteria '{ShowFilter.Criteria}', ";
                if (!string.IsNullOrEmpty(ShowFilter.werkPlek))
                    xtitle += $"werkplek '{ShowFilter.werkPlek}', ";
                if (!string.IsNullOrEmpty(ShowFilter.Bewerking))
                    xtitle += $"bewerking '{ShowFilter.Bewerking}', ";
                xtitle = xtitle.TrimEnd(new char[] { ',', ' ' });
                if (ShowFilter.VanafCheck)
                    xtitle += $" vanaf {ShowFilter.VanafTime.ToString(CultureInfo.InvariantCulture)}";
                if (ShowFilter.TotCheck)
                    xtitle += $" t/m {ShowFilter.TotTime.ToString(CultureInfo.InvariantCulture)}";
                this.Text = xtitle;
            }
            else this.Text = "Zoek producties";
            this.Invalidate();
        }

        private void RangeCalculatorForm_Shown(object sender, EventArgs e)
        {
            //xwerkplekcheck.Checked = false;
            //xbewerkingcheck.Checked = false;
            //xvanafcheck.Checked = false;
            //xtotcheck.Checked = false;
            //xcriteriacheckbox.Checked = true;
            //xcriteria.ShowClearButton = true;
            //xcriteria.Select();
            //productieListControl1.InitProductie(true, true, true, true, false, false);
            productieListControl1.InitEvents();
            //if (Manager.Opties?._viewbewdata != null)
            //    productieListControl1.ProductieLijst.RestoreState(Manager.Opties.ViewDataBewerkingenState);
            //Manager.OnFormulierChanged += Manager_OnFormulierChanged;
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            if (changedform == null || _isbussy || IsDisposed || Disposing) return;
            this.BeginInvoke(new MethodInvoker(() =>
            {
                try
                {
                    if (changedform.Bewerkingen == null || changedform.Bewerkingen.Length == 0) return;
                    if(productieListControl1.UpdateFormulier(changedform))
                        UpdateStatusLabel();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }));
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RangeCalculatorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            productieListControl1.SaveColumns(true);
            productieListControl1.DetachEvents();
        }

        private async void xcriteria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                if (_isbussy) return;
                await Verwerk();

            }
        }
    }
}