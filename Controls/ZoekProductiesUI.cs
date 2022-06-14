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
using Various;

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
            InitEvents();

        }

        public void InitEvents()
        {
            productieListControl1.ItemCountChanged += ProductieListControl1_ItemCountChanged;
            productieListControl1.InitEvents();
        }

        public void DetachEvents()
        {
            productieListControl1.ItemCountChanged -= ProductieListControl1_ItemCountChanged;
            productieListControl1.CloseUI();
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
        

        private void xverwerkb_Click(object sender, EventArgs e)
        {
            if (_isbussy) return; 
            Verwerk(true);
        }



        public RangeFilter ShowFilter;

        private bool IsAllowed(object value, string filter)
        {
            try
            {
                if (value is not Bewerking bew) return false;
                if (!string.IsNullOrEmpty(filter) && !bew.ContainsFilter(filter)) return false;
                if (!ShowFilter.Enabled)
                    return false;

                var flag = true;
                if(!string.IsNullOrEmpty(ShowFilter.State))
                {
                    if(Enum.TryParse<ViewState>(ShowFilter.State, out var state))
                    {
                        if (!bew.IsValidState(state)) return false;
                    }
                }
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

                if (!string.IsNullOrEmpty(ShowFilter.MaterialenCriteria))
                {
                    var crits = ShowFilter.MaterialenCriteria.Trim().Split(';');
                    if (bew.Parent?.Materialen != null)
                    {
                        if (bew.Parent?.Materialen.Count == 0) return false;
                    }
                    else return false;
                    foreach(var crit in crits)
                    {
                        var c = crit.Trim();
                        if(!string.IsNullOrEmpty(c))
                        {
                            if (bew.Parent.Materialen.Any(x=> string.Equals(x.ArtikelNr,c, StringComparison.CurrentCultureIgnoreCase) ||
                                                              (!string.IsNullOrEmpty(x.Omschrijving) && x.Omschrijving.Trim().ToLower().Contains(c.ToLower()))))
                                return true;
                        }
                    }
                    return false;
                }
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

        public void Verwerk(bool showcrit)
        {
            if (_isbussy) return;
            if (showcrit || ShowFilter.IsDefault())
            {
                var f = new ZoekForm();
                f.SetFilter(ShowFilter);
                if (f.ShowDialog() != DialogResult.OK) return;
                ShowFilter = f.GetFilter();
            }
            if (ShowFilter.IsDefault()) return;
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
                    var ids = Manager.xGetAllProductieIDs(true, true);
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
                var xtitle = $"{productieListControl1.ProductieLijst.Items.Count} Resultaten voor ";
                if (!string.IsNullOrEmpty(ShowFilter.Criteria))
                    xtitle += $"criteria '{ShowFilter.Criteria}', ";
                if (!string.IsNullOrEmpty(ShowFilter.werkPlek))
                    xtitle += $"werkplek '{ShowFilter.werkPlek}', ";
                if (!string.IsNullOrEmpty(ShowFilter.Bewerking))
                    xtitle += $"bewerking '{ShowFilter.Bewerking}', ";
                if (!string.IsNullOrEmpty(ShowFilter.State))
                    xtitle += $"Productie Status '{ShowFilter.State}', ";
                if (!string.IsNullOrEmpty(ShowFilter.MaterialenCriteria))
                    xtitle += $"materialen '{ShowFilter.MaterialenCriteria}', ";
                xtitle = xtitle.TrimEnd(new char[] { ',', ' ' });
                if (ShowFilter.VanafCheck)
                    xtitle += $" vanaf {ShowFilter.VanafTime.ToString(CultureInfo.InvariantCulture)}";
                if (ShowFilter.TotCheck)
                    xtitle += $" t/m {ShowFilter.TotTime.ToString(CultureInfo.InvariantCulture)}";
                if (xtitle.EndsWith("Resultaten voor"))
                    xtitle += $" alle producties";
                xtext = xtitle;
            }

            OnStatusTextChanged(xtext);
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
           OnClosedClicked();
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