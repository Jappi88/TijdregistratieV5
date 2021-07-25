using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Org.BouncyCastle.X509.Store;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;

namespace Forms
{
    public partial class AlleStoringen : MetroFramework.Forms.MetroForm
    {
        public readonly StickyWindow _stickyWindow;
        private TijdEntry Bereik { get; set; }
        public AlleStoringen()
        {
            InitializeComponent();
            _stickyWindow = new StickyWindow(this);
            imageList1.Images.Add(Resources.iconfinder_technology);
            imageList1.Images.Add(
                Resources.iconfinder_technology.CombineImage(Resources.exclamation_warning_15590, 2.25));
            ((OLVColumn) xwerkplekken.Columns[0]).ImageGetter = ImageGet;
            if (Manager.Opties != null)
                werkPlekStoringen1.xskillview.RestoreState(Manager.Opties.ViewDataStoringenState);
            xvanaf.Value = DateTime.Now.Subtract(TimeSpan.FromDays(365));
            UpdateBereik();
        }

        private void UpdateBereik()
        {
            Bereik = new TijdEntry(xvanaf.Value, xtot.Value, null);
        }

        public KeyValuePair<string, List<WerkPlek>> Selected { get; set; }

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

        public async void InitStoringen(ProductieFormulier productie = null)
        {
            try
            {
                _productie ??= productie;
                Collection = new Dictionary<string, List<WerkPlek>>();
                werkPlekStoringen1.InitStoringen(new KeyValuePair<string, List<WerkPlek>>());
                xstatuslabel.Text = "Onderbrekeningen laden...\nEen moment a.u.b";
                xrangepanel.Visible = productie == null;
                if (productie == null)
                {
                    await Task.Run(async () =>
                    {
                        if (Manager.Database != null && !Manager.Database.IsDisposed)
                        {
                            var items = await Manager.Database.GetAllProducties(true, true,
                                Bereik, null);
                            foreach (var prod in items)
                                UpdateStoringen(prod, Bereik);
                        }
                    });
                    if (this.Disposing || this.IsDisposed)
                        return;
                }
                else UpdateStoringen(productie, null);

                xwerkplekken.SetObjects(Collection);
                if (xwerkplekken.Items.Count > 0 && xwerkplekken.SelectedObject == null)
                {
                    var first = Collection.FirstOrDefault();
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

        public void UpdateStoringen(ProductieFormulier prod, TijdEntry bereik)
        {
            try
            {
                if (prod?.Bewerkingen != null)
                {
                    var items = xwerkplekken.Objects?.Cast<KeyValuePair<string, List<WerkPlek>>>().ToList();
                    foreach (var bew in prod.Bewerkingen)
                        if (bew.WerkPlekken != null)
                            foreach (var wp in bew.WerkPlekken)
                            {
                                wp.Werk = bew;
                                var updated = Collection.UpdateStoringCollection(wp, _productie != null,bereik);
                                var item = new KeyValuePair<string, List<WerkPlek>>();
                                if(items != null)
                                   item = items.FirstOrDefault(x =>
                                            string.Equals(x.Key, wp.Naam, StringComparison.CurrentCultureIgnoreCase));
                                bool changed = false;
                                if (!updated.IsDefault())
                                {
                                    if(bereik != null && updated.Value.Any(x=> x.Storingen.Any(source=> source.Gestart < bereik.Start || source.Gestart > bereik.Stop)))
                                    {
                                        Console.WriteLine("here");
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
            }
            catch
            {
            }
        }

        private void UpdateStatusText()
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    var text = "Geen Onderbrekeningen";
                    if (xwerkplekken.Items.Count > 0)
                    {
                        var items = xwerkplekken.Objects.Cast<KeyValuePair<string, List<WerkPlek>>>().ToArray();
                        double uren = 0;
                        for (int i = 0; i < items.Length; i++)
                        {
                            var pair = items[i];
                            if (!pair.IsDefault() && pair.Value.Count > 0)
                            {
                                for (int j = 0; j < pair.Value.Count; j++)
                                {
                                    var sts = pair.Value[j].Storingen?.CreateCopy();
                                    if (sts != null && sts.Count > 0)
                                    {
                                        uren += sts.Sum(t => t.GetTotaleTijd());
                                    }
                                }
                            }
                        }

                        if (items.Length == 1)
                            text = $"Alleen {items.First().Key} met totaal {uren} uren aan onderbrekeningen.";
                        else
                            text = $"{items.Length} werkplekken.\nTotaal {uren} uren onderbrekeningen.";
                    }

                    Text = text;
                    xstatuslabel.Text = text;
                    this.Invalidate();
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void AlleVaardigheden_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            werkPlekStoringen1.OnDisableEvents -= werkPlekStoringen1_OnDisableEvents;
            werkPlekStoringen1.OnEnableEvents -= werkPlekStoringen1_OnEnableEvents;
            if (Manager.Opties != null)
                Manager.Opties.ViewDataVaardighedenState = werkPlekStoringen1.xskillview.SaveState();
        }

        private void AlleVaardigheden_Shown(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            werkPlekStoringen1.OnDisableEvents += werkPlekStoringen1_OnDisableEvents;
            werkPlekStoringen1.OnEnableEvents += werkPlekStoringen1_OnEnableEvents;
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
                        UpdateStoringen(changedform,bereik);
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
            Close();
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
    }
}