using BrightIdeasSoftware;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductieManager.Rpm.Productie;
using Rpm.Various;

namespace Controls
{
    public partial class AlleStoringenUI : UserControl
    {
        private TijdEntry Bereik { get; set; }
        public AlleStoringenUI()
        {
            InitializeComponent();
           
        }

        public void InitUI()
        {
            if (Manager.Opties != null)
                werkPlekStoringen1.xskillview.RestoreState(Manager.Opties.ViewDataStoringenState);
            imageList1.Images.Add(Resources.iconfinder_technology);
            imageList1.Images.Add(
                Resources.iconfinder_technology.CombineImage(Resources.exclamation_warning_15590, 2.25));
            ((OLVColumn)xwerkplekken.Columns[0]).ImageGetter = ImageGet;
            xvanaf.Value = DateTime.Now.Subtract(TimeSpan.FromDays(365));
            UpdateBereik();
            InitEvents();
        }

        public void CloseUI()
        {
            DetachEvents();
            if (Manager.Opties != null)
                Manager.Opties.ViewDataVaardighedenState = werkPlekStoringen1.xskillview.SaveState();
        }

        public void InitEvents()
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            Manager.OnSettingsChanged += Manager_OnSettingsChanged;
            werkPlekStoringen1.OnDisableEvents += werkPlekStoringen1_OnDisableEvents;
            werkPlekStoringen1.OnEnableEvents += werkPlekStoringen1_OnEnableEvents;
        }

        private void Manager_OnSettingsChanged(object instance, Rpm.Settings.UserSettings settings, bool reinit)
        {
            try
            {
                UpdateBereik();
                if (this.IsDisposed || this.Disposing) return;
                if (InvokeRequired)
                    this.BeginInvoke(new MethodInvoker(()=> InitStoringen()));
                else InitStoringen();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DetachEvents()
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged; 
            Manager.OnSettingsChanged -= Manager_OnSettingsChanged;
            werkPlekStoringen1.OnDisableEvents -= werkPlekStoringen1_OnDisableEvents;
            werkPlekStoringen1.OnEnableEvents -= werkPlekStoringen1_OnEnableEvents;
        }

        private void UpdateBereik()
        {
            Bereik = new TijdEntry(xvanaf.Value, xtot.Value, null);
        }

        private KeyValuePair<string, List<WerkPlek>> Selected { get; set; }

        public Dictionary<string, List<WerkPlek>> Collection { get; private set; }

        public ProductieFormulier _productie = null;

        public object ImageGet(object sender)
        {
            var x = (KeyValuePair<string, List<WerkPlek>>) sender;
            if (!x.IsDefault())
            {
                var sec = x.Value.Any(s => s.Storingen != null && s.Storingen.Any(t => !t.IsVerholpen));
                if (sec)
                    return 1;
            }

            return 0;
        }

        private void xuserlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xwerkplekken.SelectedObject != null)
            {
                if (Selected.IsDefault() || !xwerkplekken.SelectedObject.Equals(Selected))
                    Selected = (KeyValuePair<string, List<WerkPlek>>) xwerkplekken.SelectedObject;
            }
            else
            {
                Selected = new KeyValuePair<string, List<WerkPlek>>();
            }

            werkPlekStoringen1.InitStoringen(Selected);
        }

        public async void InitStoringen(ProductieFormulier productie = null, WerkPlek selected = null)
        {
            try
            {
                if (this.Disposing || this.IsDisposed)
                    return;
                _productie ??= productie;
                Collection = new Dictionary<string, List<WerkPlek>>();
                werkPlekStoringen1.InitStoringen(new KeyValuePair<string, List<WerkPlek>>());
                xstatuslabel.Text = "Onderbrekeningen laden...\nEen moment a.u.b";
                xrangepanel.Visible = productie == null;
                if (productie == null)
                {
                    await Task.Run(() =>
                    {
                        if (Manager.Database is {IsDisposed: false})
                        {
                            var bws = Manager.ProductieProvider.GetBewerkingen(ProductieProvider.LoadedType.Alles,
                                new ViewState[]
                                {
                                    ViewState.Alles
                                }, true, null).Result;
                          
                            foreach (var bw in bws)
                                UpdateStoringen(bw, Bereik,null);
                        }
                    });
                  
                }
                else UpdateStoringen(productie, null,false);

                Collection = Collection.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                xwerkplekken.SetObjects(Collection);
                if (xwerkplekken.Items.Count > 0 && xwerkplekken.SelectedObject == null)
                {
                    var first = selected == null
                        ? Collection.FirstOrDefault()
                        : Collection.FirstOrDefault(x =>
                            string.Equals(x.Key, selected.Naam, StringComparison.CurrentCultureIgnoreCase));
                    if (first.IsDefault())
                        xwerkplekken.SelectedObject = null;
                    else xwerkplekken.SelectedObject = first;
                    xwerkplekken.SelectedItem?.EnsureVisible();
                }
                UpdateStatusText();
            }
            catch
            {
            }
        }

        public void UpdateStoringen(Bewerking bew, TijdEntry bereik, List<KeyValuePair<string, List<WerkPlek>>> items)
        {
            try
            {
                if (bew == null || !bew.IsAllowed()) return;
                if (bereik != null && !bew.HeeftGewerkt(bereik)) return;
                if (bew.WerkPlekken != null)
                    foreach (var wp in bew.WerkPlekken)
                    {
                        wp.Werk = bew;
                        var updated = Collection.UpdateStoringCollection(wp, _productie != null, bereik);
                        if (items == null) continue;
                        var item = items.FirstOrDefault(x =>
                            string.Equals(x.Key, wp.Naam, StringComparison.CurrentCultureIgnoreCase));
                        bool changed = false;
                        if (!updated.IsDefault())
                        {
                            if (bereik != null && updated.Value.Any(x => x.Storingen.Any(source => source.Gestart < bereik.Start || source.Gestart > bereik.Stop)))
                            {
                                Console.WriteLine(@"here");
                            }
                            if (item.IsDefault())
                            {
                                xwerkplekken.AddObject(updated);
                            }
                            else
                            {
                                xwerkplekken.RefreshObject(updated);
                            }
                            if (Collection.ContainsKey(updated.Key))
                                Collection[updated.Key] = updated.Value;
                            else
                            {
                                Collection.Add(updated.Key, updated.Value);
                                changed = true;
                            }
                        }
                        else
                        {
                            if (xwerkplekken.Objects != null && !item.IsDefault())
                                xwerkplekken.RemoveObject(item);
                            if (Collection.ContainsKey(wp.Naam))
                            {
                                Collection.Remove(wp.Naam);
                                changed = true;
                            }

                        }

                        if (!werkPlekStoringen1.Plek.IsDefault() &&
                            string.Equals(werkPlekStoringen1.Plek.Key, wp.Naam,
                                StringComparison.CurrentCultureIgnoreCase))
                            werkPlekStoringen1.RefreshItems(wp);
                        if (changed)
                            UpdateStatusText();
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UpdateStoringen(ProductieFormulier prod, TijdEntry bereik, bool updatelist)
        {
            try
            {
                if (prod?.Bewerkingen != null)
                {
                    var items = updatelist
                        ? xwerkplekken.Objects?.Cast<KeyValuePair<string, List<WerkPlek>>>().ToList()
                        : null;
                    foreach (var bew in prod.Bewerkingen)
                        if (bew.IsAllowed())
                            UpdateStoringen(bew, bereik, items);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void xUpdateStatusText()
        {
            var text = "Geen Onderbrekeningen";
            if (xwerkplekken.Items.Count > 0)
            {
                var items = xwerkplekken.Objects.Cast<KeyValuePair<string, List<WerkPlek>>>().ToArray();
                double uren = 0;
                int aantal = 0;
                for (int i = 0; i < items.Length; i++)
                {
                    var pair = items[i];
                    if (!pair.IsDefault() && pair.Value.Count > 0)
                    {
                        for (int j = 0; j < pair.Value.Count; j++)
                        {
                            var sts = pair.Value[j].Storingen?.CreateCopy();
                            if (sts is { Count: > 0 })
                            {
                                uren += sts.Sum(t => t.GetTotaleTijd());
                                aantal += sts.Count;
                            }
                        }
                    }
                }
                var x1 = aantal == 1? "onderbreking" : "onderbrekeningen";
                var x2 = aantal == 1 ? items.First().Key : $"{items.Length} werkplaatsen";
                text = $"{aantal} {x1} van {uren} uur op {x2}.";
            }

            OnStatusTextChanged(text);
            xstatuslabel.Text = text;
            this.Invalidate();
        }

        public void UpdateStatusText()
        {
            try
            {
                if (this.InvokeRequired)
                    this.BeginInvoke(new MethodInvoker(xUpdateStatusText));
                else xUpdateStatusText();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            if (changedform != null && !IsDisposed)
            {
                try
                {
                    if (_productie != null && !_productie.Equals(changedform))
                        return;
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        var bereik = _productie == null ? Bereik : null;
                        UpdateStoringen(changedform,bereik,true);
                    }));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }
        }

        private void werkPlekStoringen1_OnCloseButtonPressed(object sender, EventArgs e)
        {
            OnCloseClicked();
        }

        private void werkPlekStoringen1_OnDisableEvents(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
        }

        private void werkPlekStoringen1_OnEnableEvents(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
        }

        private void xupdatetijdb_Click(object sender, EventArgs e)
        {
            UpdateBereik();
            InitStoringen();
        }

        public event EventHandler CloseClicked;

        protected virtual void OnCloseClicked()
        {
            CloseClicked?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler StatusTextChanged;

        protected virtual void OnStatusTextChanged(string text)
        {
            StatusTextChanged?.Invoke(text, EventArgs.Empty);
        }
    }
}