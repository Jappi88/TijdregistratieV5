using Forms;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controls
{
    public partial class ZoekProductiesUI : UserControl
    {
        //private List<Bewerking> _loaded = new List<Bewerking>();
        public ZoekProductiesUI()
        {
            InitializeComponent();
        }

        public void InitUI()
        {
            productieListControl1.IsBewerkingView = true;
            productieListControl1.ValidHandler = IsAllowed;
            if (Manager.BewerkingenLijst == null) return;
            InitEvents();
            var bewerkingen = Manager.BewerkingenLijst.GetAllEntries().Select(x => (object)x.Naam).ToArray();
            var afdelingen = Manager.BewerkingenLijst.GetAlleWerkplekken(false).Select(x => (object)x).ToArray();
            xwerkplekken.Items.AddRange(afdelingen);
            xbewerkingen.Items.AddRange(bewerkingen);
        }

        public void InitEvents()
        {
            productieListControl1.ItemCountChanged += ProductieListControl1_ItemCountChanged;
            productieListControl1.InitEvents();
        }

        public void DetachEvents()
        {
            productieListControl1.ItemCountChanged -= ProductieListControl1_ItemCountChanged;
            productieListControl1.DetachEvents();
        }

        public void CloseUI()
        {
            DetachEvents();
            productieListControl1.SaveColumns(true);
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

        private void xverwerkb_Click(object sender, EventArgs e)
        {
            if (_isbussy) return; 
            Verwerk();
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

        public RangeFilter ShowFilter;
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

        public void SetFilter(RangeFilter filter)
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

        private bool _isbussy;

        public void Verwerk()
        {
            if (_isbussy) return;
            if (!DoCheck())
                return;
            ShowFilter.Enabled = true;
            ShowFilter.VanafCheck = xvanafcheck.Checked;
            ShowFilter.VanafTime = xvanafdate.Value;
            ShowFilter.TotCheck = xtotcheck.Checked;
            ShowFilter.TotTime = xtotdate.Value;
            ShowFilter.Bewerking = xbewerkingcheck.Checked ? xbewerkingen.Text.Trim() : null;
            ShowFilter.werkPlek = xwerkplekcheck.Checked ? xwerkplekken.Text.Trim() : null;
            ShowFilter.Criteria = xcriteriacheckbox.Checked ? xcriteria.Text.Trim() : null;
            Task.Run(() =>
            {
                try
                {
                    if (IsDisposed || Disposing)
                    {
                        _isbussy = false;
                        return;
                    }
                    _isbussy = true;
                    EnableProgressLabel(true);
                    SetProgressLabelText("Producties Laden...");
                    var ids = Manager.GetAllProductieIDs(true, true).Result;
                    int cur = 0;
                    int max = ids.Count;
                    var loaded = new List<Bewerking>();
                    foreach (var id in ids)
                    {
                        if (IsDisposed || !Visible) break;
                        if (string.IsNullOrEmpty(id)) continue;
                        var x = Manager.Database.GetProductie(id, true);
                        if (x == null) continue;
                        if (x.Bewerkingen is {Length: > 0})
                        {
                            var bws = x.Bewerkingen.Where(b => IsAllowed(b, null)).ToArray();
                            if (bws.Length > 0)
                            {
                                loaded.AddRange(bws);
                            }
                        }

                        cur++;
                        double perc = (double) cur / max;
                        SetProgressLabelText($"Producties laden ({perc:0.0%})...");
                    }

                    if (IsDisposed || Disposing)
                    {
                        _isbussy = false;
                        return;
                    }
                    productieListControl1.InitProductie(loaded, true, true, false);
                }
                catch (Exception e)
                {
                    _isbussy = false;
                    if (IsDisposed || Disposing) return;
                    this.Invoke(new MethodInvoker(() =>
                    {
                        XMessageBox.Show(this, e.Message, "Fout", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }));
                }

                _isbussy = false;
                if (IsDisposed || Disposing) return;
                EnableProgressLabel(false);
                this.Invoke(new MethodInvoker(UpdateStatusLabel));
            });
        }

        private void UpdateStatusLabel()
        {
            var xtext = "Zoek producties";
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
                if (xtitle == "Resultaten voor")
                    xtitle = "Resultaten voor alle Producties";
                xtext = xtitle;
            }

            OnStatusTextChanged(xtext);
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
           OnClosedClicked();
        }

        private void xcriteria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                if (_isbussy) return;
                Verwerk();

            }
        }

        public event EventHandler ClosedClicked;

        public event EventHandler StatusTextChanged;

        protected virtual void OnClosedClicked()
        {
            ClosedClicked?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnStatusTextChanged(string text)
        {
            StatusTextChanged?.Invoke(text, EventArgs.Empty);
        }
    }
}