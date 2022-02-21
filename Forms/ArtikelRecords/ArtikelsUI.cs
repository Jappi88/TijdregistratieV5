using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Rpm.Misc;
using Rpm.Productie;

namespace Controls
{
    public partial class ArtikelsUI : UserControl
    {
        private readonly Dictionary<string, List<Bewerking>> _Values = new();

        private string _Filter;

        private bool _iswaiting;

        public ArtikelsUI()
        {
            InitializeComponent();
        }


        public string SelectedArtikelNr
        {
            get => GetSelectedArtikelNr();
            set => SelectArtikelNr(value);
        }

        public void InitUI()
        {
            productieListControl1.ValidHandler = IsValidHandler;
            productieListControl1.ItemCountChanged += ProductieListControl1_ItemCountChanged;
            ((OLVColumn) xartikelsList.Columns[0]).ImageGetter = x => 0;
            xsearchbox.ShowClearButton = true;
            LoadArtikels();
            InitEvents();
        }

        public void CloseUI()
        {
            DetachEvents();
            productieListControl1.SaveColumns(true);
        }

        public void InitEvents()
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted += Manager_OnFormulierDeleted;
        }

        public void DetachEvents()
        {
            productieListControl1.DetachEvents();
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted -= Manager_OnFormulierDeleted;
        }

        public event EventHandler StatusTextChanged;

        protected virtual void OnStatusTextChanged(string text)
        {
            StatusTextChanged?.Invoke(text, EventArgs.Empty);
        }

        private string GetSelectedArtikelNr()
        {
            if (xartikelsList.SelectedObject is KeyValuePair<string, List<Bewerking>> pair) return pair.Key;

            return null;
        }

        private void SelectArtikelNr(string artnr)
        {
            var xitems = xartikelsList.Objects?.Cast<KeyValuePair<string, List<Bewerking>>>().ToList();
            if (xitems == null || xitems.Count == 0) return;
            var xi = xitems.FirstOrDefault(x => string.Equals(x.Key, artnr, StringComparison.CurrentCultureIgnoreCase));
            xartikelsList.SelectedObject = xi;
            xartikelsList.SelectedItem?.EnsureVisible();
        }

        private void ProductieListControl1_ItemCountChanged(object sender, EventArgs e)
        {
            UpdateTitle();
        }

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
                    var xwv = "Artikelen laden";
                    //var xcurvalue = xwv;
                    var tries = 0;
                    while (_iswaiting && tries < 200)
                        try
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
                        catch (Exception e)
                        {
                            break;
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

        private async void LoadArtikels()
        {
            try
            {
                if (Manager.Database?.ProductieFormulieren == null) return;
                SetWaitUI();
                productieListControl1.InitProductie(true, true, true, true, false, false);
                _Values.Clear();
                var bws = await Manager.Database.GetAllBewerkingen(true, true);
                foreach (var bw in bws)
                {
                    if (IsDisposed || Disposing)
                    {
                        StopWait();
                        return;
                    }

                    if (_Values.ContainsKey(bw.ArtikelNr.ToLower()))
                        _Values[bw.ArtikelNr.ToLower()].Add(bw);
                    else
                        _Values.Add(bw.ArtikelNr.ToLower(), new List<Bewerking> {bw});
                }

                ListArtikels();
                UpdateTitle();
                xartikelsList.Select();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            StopWait();
        }

        private void UpdateTitle()
        {
            var x1 = _Values.Count == 1 ? "Artikel" : "Artikelen";
            var x2 = string.Empty;
            if (xartikelsList.SelectedItems.Count > 0)
            {
                var xselected = xartikelsList.SelectedItems[0].Text;
                var bws = productieListControl1.ProductieLijst.Items;
                var x3 = bws.Count == 1 ? "Bewerking" : "Bewerkingen";
                x2 = $@"[Selected: {xselected} ({bws.Count} {x3})]";
            }

            var text = $@"{_Values.Count} {x1} {x2}";
            OnStatusTextChanged(text);
        }

        private void ListArtikels()
        {
            xartikelsList.BeginUpdate();
            var values = new Dictionary<string, List<Bewerking>>();
            var filter = xsearchbox.Text.Replace("Zoeken...", "").Trim();
            foreach (var value in _Values)
            {
                if (!string.IsNullOrEmpty(filter) &&
                    !value.Key.ToLower().Contains(filter.ToLower())) continue;
                values.Add(value.Key, value.Value);
            }

            xartikelsList.SetObjects(values);
            xartikelsList.EndUpdate();
            if (xartikelsList.Items.Count > 0)
            {
                xartikelsList.Items[0].Selected = true;
                xartikelsList.Items[0].EnsureVisible();
            }

            xartikelsList_SelectedIndexChanged(this, EventArgs.Empty);
        }

        public bool IsValidHandler(object value, string filter)
        {
            if (xartikelsList.SelectedItems.Count > 0)
            {
                var xselected = xartikelsList.SelectedItems[0];
                if (value is Bewerking bew)
                    return string.Equals(xselected.Text, bew.ArtikelNr, StringComparison.CurrentCultureIgnoreCase);

                if (value is ProductieFormulier prod)
                    return string.Equals(xselected.Text, prod.ArtikelNr, StringComparison.CurrentCultureIgnoreCase);
            }

            return false;
        }

        private void xartikelsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            productieListControl1.DetachEvents();
            if (xartikelsList.SelectedItems.Count > 0)
            {
                var xselected = xartikelsList.SelectedItems[0];
                if (_Values.ContainsKey(xselected.Text.ToLower()))
                {
                    productieListControl1.InitProductie(_Values[xselected.Text.ToLower()], false, true, false);
                    productieListControl1.InitEvents();
                    return;
                }
            }

            productieListControl1.InitProductie(new List<Bewerking>(), false, true, false);
            UpdateTitle();
        }

        private void xsearchArtikel_TextChanged(object sender, EventArgs e)
        {
            var filter = xsearchbox.Text.Replace("Zoeken...", "").Trim().ToLower();
            if (string.Equals(filter, _Filter, StringComparison.CurrentCultureIgnoreCase)) return;
            ListArtikels();
            _Filter = filter;
        }

        private void Manager_OnFormulierDeleted(object sender, string id)
        {
            if (string.IsNullOrEmpty(id)) return;
            foreach (var value in _Values)
                value.Value.RemoveAll(x => string.Equals(x.ProductieNr, id, StringComparison.CurrentCultureIgnoreCase));
            Invoke(new Action(UpdateTitle));
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            try
            {
                if (changedform?.ArtikelNr != null)
                {
                    var bws = changedform.Bewerkingen?.Where(x => x.IsAllowed()).ToList();
                    if (bws == null || bws.Count == 0) return;
                    if (_Values.ContainsKey(changedform.ArtikelNr.ToLower()))
                    {
                        var xbws = _Values[changedform.ArtikelNr.ToLower()];
                        foreach (var bw in bws)
                        {
                            var index = xbws.IndexOf(bw);
                            if (index < 0)
                                xbws.Add(bw);
                            else xbws[index] = bw;
                        }
                    }
                    else
                    {
                        _Values.Add(changedform.ArtikelNr.ToLower(), bws);
                        if (InvokeRequired)
                            Invoke(new Action(UpdateTitle));
                        else UpdateTitle();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void xsearchArtikel_Enter(object sender, EventArgs e)
        {
            if (string.Equals(xsearchbox.Text.Trim(), "zoeken...", StringComparison.CurrentCultureIgnoreCase))
                xsearchbox.Text = "";
        }

        private void xsearchArtikel_Leave(object sender, EventArgs e)
        {
            if (xsearchbox.Text.Trim() == "")
                xsearchbox.Text = @"Zoeken...";
        }
    }
}