using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Forms;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Mailing;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Controls
{
    public partial class WerkPlekStoringen : UserControl
    {
        public WerkPlekStoringen()
        {
            InitializeComponent();
            imageList1.Images.Add(Resources.onderhoud128_128);
            imageList1.Images.Add(Resources.onderhoud128_128.CombineImage(Resources.check_1582, 2));
            ((OLVColumn) xskillview.Columns[0]).ImageGetter = ImageGetter;
        }

        public KeyValuePair<string, List<WerkPlek>> Plek { get; private set; }

        public bool IsCloseAble
        {
            get => xclosepanel.Visible;
            set => xclosepanel.Visible = value;
        }

        public bool IsEditAble
        {
            get => xeditpanelcontainer.Visible;
            set => xeditpanelcontainer.Visible = value;
        }

        public object ImageGetter(object sender)
        {
            if (sender is Storing)
            {
                var st = sender as Storing;
                if (st != null) return st.IsVerholpen ? 1 : 0;
            }

            return 0;
        }

        public void InitStoringen(WerkPlek wp)
        {
            Plek = new KeyValuePair<string, List<WerkPlek>>(wp.Naam, new List<WerkPlek> {wp});
            ListItems();
        }

        public void InitStoringen(KeyValuePair<string, List<WerkPlek>> pair)
        {
            Plek = pair;
            ListItems();
        }

        public void ListItems()
        {
            if (!Plek.IsDefault() && Plek.Value != null)
            {
                var sts = new List<Storing>();
                Plek.Value.ForEach(x =>
                {
                    if (x.Storingen is {Count: > 0}) sts.AddRange(x.Storingen);
                });
                xskillview.SetObjects(sts.Where(IsAllowed));
            }
            else
            {
                xskillview.SetObjects(new Storing[] { });
            }

            UpdateHeaderLabel();
            UpdateStatusLabel();
            UpdateButtonEnable();
        }

        public void RefreshItems(WerkPlek plek)
        {
            BeginInvoke(new MethodInvoker(() => xRefreshItems(plek)));
        }

        public void RefreshItems(KeyValuePair<string, List<WerkPlek>> pair)
        {
            BeginInvoke(new MethodInvoker(() => xRefreshItems(pair)));
        }

        private void xRefreshItems(KeyValuePair<string, List<WerkPlek>> pair)
        {
            if (Plek.Key != pair.Key)
                return;
            Plek = pair;
            var sts = new List<Storing>();
            Plek.Value.ForEach(x =>
            {
                if (x.Storingen is {Count: > 0}) sts.AddRange(x.Storingen);
            });
            if (sts.Count > 0)
            {
                UpdateStoringEntries(sts.ToArray());
                UpdateHeaderLabel();
                UpdateStatusLabel();
                UpdateButtonEnable();
            }
        }

        private void UpdateStoringEntry(Storing storing)
        {
            if (storing == null)
                return;
            var allowed = IsAllowed(storing);
            Storing xst = null;
            if (xskillview.Objects != null)
                xst = xskillview.Objects.Cast<Storing>().FirstOrDefault(x => x.Equals(storing));
            if ((xskillview.Objects == null || xst == null) && allowed)
            {
                xskillview.AddObject(storing);
                xskillview.Invalidate();
            }
            else
            {
                if (xst != null && !allowed)
                    xskillview.RemoveObject(xst);
                else if (xst != null)
                    xskillview.RefreshObject(storing);
                xskillview.Invalidate();
            }
        }

        private void UpdateStoringEntries(Storing[] entries)
        {
            if (entries != null)
                foreach (var st in entries)
                    UpdateStoringEntry(st);
            //if (xskillview.Objects.Cast<Storing>().Count() > entries.Length)
            //    xskillview.RemoveObjects(xskillview.Objects.Cast<Storing>()
            //        .Where(x => !IsAllowed(x) || !entries.Any(t => t.Equals(x))).ToArray());
        }

        private void xRefreshItems(WerkPlek plek)
        {
            if (Plek.IsDefault() || Plek.Value == null ||
                Plek.Value.Count == 0 && plek != null && plek.Naam == Plek.Key && plek.Storingen.Count > 0)
            {
                Plek = new KeyValuePair<string, List<WerkPlek>>(plek.Naam, new List<WerkPlek> {plek});
                ListItems();
            }
            else if (Plek.Value != null && plek != null && Plek.Key == plek.Naam)
            {
                var xitem = Plek.Value.FirstOrDefault(x => x.Equals(plek));
                if (xitem == null && plek.Storingen.Count > 0)
                    Plek.Value.Add(plek);

                UpdateStoringEntries(plek.Storingen.ToArray());
                UpdateHeaderLabel();
                UpdateStatusLabel();
                UpdateButtonEnable();
            }
        }

        public bool IsAllowed(Storing entry)
        {
            var filter = xsearchbox.Text.ToLower();
            if (string.IsNullOrWhiteSpace(filter) || filter == "zoeken...")
                return true;
            var xreturn = false;
            if (entry.Omschrijving != null)
                xreturn |= entry.Omschrijving.ToLower().Contains(filter);
            if (entry.GemeldDoor != null)
                xreturn |= entry.GemeldDoor.ToLower().Contains(filter);
            if (entry.WerkPlek != null)
                xreturn |= entry.WerkPlek.ToLower().Contains(filter);
            if (entry.Oplossing != null)
                xreturn |= entry.Oplossing.ToLower().Contains(filter);
            if (entry.StoringType != null)
                xreturn |= entry.StoringType.ToLower().Contains(filter);
            if (entry.VerholpenDoor != null)
                xreturn |= entry.VerholpenDoor.ToLower().Contains(filter);
            return xreturn;
        }

        private void xskillview_SelectionChanged(object sender, EventArgs e)
        {
            UpdateStatusLabel();
            UpdateButtonEnable();
        }

        public void UpdateButtonEnable()
        {
            var acces1 = Manager.LogedInGebruiker is {AccesLevel: >= AccesType.ProductieBasis};
            var selected1 = xskillview.SelectedObjects.Count == 1;
            var selected2 = xskillview.SelectedObjects.Count > 0;
            xaddstoring.Enabled = acces1;
            xwijzigstoring.Enabled = acces1 && selected1;
            xverwijderstoring.Enabled = acces1 && selected2;
            wijzigToolStripMenuItem.Enabled = acces1 && selected1;
            verwijderToolStripMenuItem.Enabled = acces1 && selected2;
        }

        private void UpdateHeaderLabel()
        {
            if (!Plek.IsDefault())
            {
                if (Plek.Value != null)
                {
                    var plekken = Plek.Value;

                    IEnumerable<Storing> entries = new Storing[] { };
                    if (xskillview.Objects != null)
                        entries = xskillview.Objects.Cast<Storing>();
                    var title = $"Onderbrekeningen van {Plek.Key}.";
                    var enumerable = entries as Storing[] ?? entries.ToArray();
                    if (enumerable.Any())
                    {
                        var xarg = enumerable.Count() == 1 ? "onderbreking" : "onderbrekeningen";
                        title = $"{enumerable.Count()} {xarg} van {enumerable.Sum(x => x.GetTotaleTijd())} uur op {Plek.Key}.";
                        xomschrijving.Text = title;
                    }
                    else
                    {
                        xomschrijving.Text = $"{title}Momenteel geen onderbrekeningen.";
                    }
                }
                else
                {
                    xomschrijving.Text = $"{Plek.Key} heeft nog geen onderbrekeningen.";
                }
            }
            else
            {
                xomschrijving.Text = "Niks Geselecteerd";
            }
        }

        private void UpdateStatusLabel()
        {
            if (xskillview.SelectedObjects.Count > 0)
            {
                var selected = xskillview.SelectedObjects.Cast<Storing>().ToArray();
                if (selected.Length == 1)
                    xstatuslabel.Text = $"1 Onderbreking geselecteerd van {selected[0].GetTotaleTijd()} uur";
                else
                    xstatuslabel.Text =
                        $"{selected.Length} Onderbrekeningen geselecteerd van totaal {selected.Sum(s => s.GetTotaleTijd())} uur";
            }
            else
            {
                if (xskillview.Objects != null)
                {
                    var items = xskillview.Objects.Cast<Storing>().ToArray();
                    var selected = items;
                    if (selected.Length == 1)
                        xstatuslabel.Text = $"1 Onderbreking van {selected[0].GetTotaleTijd()} uur";
                    else
                        xstatuslabel.Text =
                            $"{selected.Length} Onderbrekeningen van totaal {selected.Sum(s => s.GetTotaleTijd())} uur";
                }
                else
                {
                    xstatuslabel.Text = "Geen Onderbrekeningen";
                }
            }
        }

        private void xsearch_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb is {Text: "Zoeken..."}) tb.Text = "";
        }

        private void xsearch_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
                if (string.IsNullOrWhiteSpace(tb.Text))
                    tb.Text = "Zoeken...";
        }

        private void xsearchbox_TextChanged(object sender, EventArgs e)
        {
            if (xsearchbox.Text.Trim().ToLower() != "zoeken...")
                ListItems();
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            CloseButtonPressed();
        }

        private async void xaddstoring_Click(object sender, EventArgs e)
        {
            if (!Plek.IsDefault() && Plek.Value != null)
            {
                var bws = Plek.Value.Where(x => x.Werk != null).Select(x => x.Werk.Path).ToArray();
                bws = bws.Union(bws).ToArray();
                WerkPlek plek = null;
                var wps = Plek.Value.Where(x => x.Werk != null).ToList();
                if (bws.Length > 1)
                {
                    var bc = new BewerkingChooser(bws);
                    if (bc.ShowDialog() != DialogResult.OK || bc.SelectedItem == null)
                        return;

                    plek = Plek.Value.FirstOrDefault(x =>
                        string.Equals(x.Werk.Path, bc.SelectedItem, StringComparison.OrdinalIgnoreCase));
                    wps = plek?.Werk.WerkPlekken;
                }
                else if (bws.Length == 1)
                {
                    wps = wps.Union(wps).ToList();
                }

                if (wps is {Count: > 0})
                {
                    if (wps.Count == 1)
                    {
                        plek = wps[0];
                    }
                    else
                    {
                        var wpc = new WerkPlekChooser(wps, null);
                        if (wpc.ShowDialog() != DialogResult.OK)
                            return;
                        plek = wpc.Selected;
                    }
                }

                if (plek == null)
                    return;
                var sf = new NewStoringForm(plek);
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    plek.Storingen.Add(sf.Onderbreking);
                    ListItems();
                    await plek.Werk.UpdateBewerking(null,
                        $"Niewe onderbreking toegevoegd door '{sf.Onderbreking.GemeldDoor} op {Plek.Value[0].Path}'");
                    RemoteProductie.SendStoringMail(sf.Onderbreking, plek.Werk);
                }
            }
        }

        private void xwijzigstoring_Click(object sender, EventArgs e)
        {
            WijzigSelected();
        }

        private void xverwijderstoring_Click(object sender, EventArgs e)
        {
            VerwijderSelected();
        }

        public void WijzigSelected()
        {
            var acces1 = Manager.LogedInGebruiker is {AccesLevel: >= AccesType.ProductieBasis};
            if (!acces1) return;
            if (!Plek.IsDefault() && xskillview.SelectedObject != null)
            {
                if (xskillview.SelectedObject is Storing st)
                {
                    var plek = Plek.Value.FirstOrDefault(x =>
                        x.Storingen != null && x.Storingen.Any(s => s.InstanceId == st.InstanceId));
                    if (plek != null)
                    {
                        var isverholpen = st.IsVerholpen;
                        var sf = new NewStoringForm(plek, st);
                        if (sf.ShowDialog() == DialogResult.OK)
                        {
                            xskillview.RefreshObject(sf.Onderbreking);
                            var xindex = plek.Storingen.IndexOf(sf.Onderbreking);
                            if (xindex > -1)
                                plek.Storingen[xindex] = sf.Onderbreking;
                            //plek.Storingen = xskillview.Objects.Cast<Storing>().Where(x=> string.Equals(x.Path, plek.Path, StringComparison.CurrentCultureIgnoreCase)).Union(plek.Storingen).ToList();
                            plek.Werk.UpdateBewerking(null,
                                $"Onderbreking aangepast door '{sf.Onderbreking.GemeldDoor} op {plek.Path}'");
                            //xskillview.SetObjects(plek.Storingen);
                            xskillview.SelectedObject = sf.Onderbreking;
                            xskillview.SelectedItem?.EnsureVisible();
                            if (!isverholpen && st.IsVerholpen)
                                RemoteProductie.SendStoringMail(sf.Onderbreking, plek.Werk);
                        }
                    }
                }
            }
        }

        public async void VerwijderSelected()
        {
            var count = 0;
            if (!Plek.IsDefault() && xskillview.SelectedObjects != null &&
                (count = xskillview.SelectedObjects.Count) > 0)
            {
                var xvalue = count == 1 ? "onderbreking" : "onderbrekeningen";
                if (XMessageBox.Show(
                        this, $"Je staat op de punt om {count} {xvalue} te verwijderen!\n\nWeet je zeker dat je dat wilt doen?\nClick op 'Nee' om te annuleren.",
                    "Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DisableEvents();
                    var removed = 0;
                    var sts = xskillview.SelectedObjects.Cast<Storing>().ToList();
                    foreach (var st in sts)
                        //var wp = Plek.Value.FirstOrDefault(x => x.Path.ToLower() == st.Path.ToLower());
                    foreach (var wp in Plek.Value)
                    {
                        count = 0;
                        if (wp.Storingen.Remove(st))
                            count++;
                        if (count > 0)
                        {
                            removed += count;
                            xvalue = count == 1 ? "onderbreking" : "onderbrekeningen";
                            var xvalue2 = count == 1 ? "is" : "zijn";
                            await wp.Werk.UpdateBewerking(null,
                                $"Er {xvalue2} {count} {xvalue} verwijderd op {wp.Path}");
                        }
                    }

                    RefreshItems(Plek);


                    if (removed > 0)
                        ListItems();
                    else
                        XMessageBox.Show(this, $"Het is niet gelukt om de geselecteerde {xvalue} te verwijderen.", "Mislukt",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    EnableEvents();
                }
            }
        }

        private void wijzigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WijzigSelected();
        }

        private void verwijderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerwijderSelected();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = !IsEditAble || !wijzigToolStripMenuItem.Enabled && !verwijderToolStripMenuItem.Enabled;
        }

        public event EventHandler OnCloseButtonPressed;
        public event EventHandler OnDisableEvents;
        public event EventHandler OnEnableEvents;

        public void CloseButtonPressed()
        {
            OnCloseButtonPressed?.Invoke(xsluiten, EventArgs.Empty);
        }

        public void EnableEvents()
        {
            OnEnableEvents?.Invoke(this, EventArgs.Empty);
        }

        public void DisableEvents()
        {
            OnDisableEvents?.Invoke(this, EventArgs.Empty);
        }

        private void xskillview_CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            if (e.Model is Storing wp)
            {
                e.Title = $"[{wp.StoringType}]{wp.Path}";
                e.Text = wp.Omschrijving;
                if (!string.IsNullOrEmpty(wp.Oplossing))
                    e.Text += $"\nOplossing:\n{wp.Oplossing}";
            }
        }

        private void xskillview_DoubleClick(object sender, EventArgs e)
        {
            WijzigSelected();
        }

        private void toonProductieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var acces1 = Manager.LogedInGebruiker is { AccesLevel: >= AccesType.ProductieBasis };
            if (!acces1) return;
            if (!Plek.IsDefault() && xskillview.SelectedObject != null)
            {
                if (xskillview.SelectedObject is Storing st)
                {
                    var plek = Plek.Value.FirstOrDefault(x =>
                        x.Storingen != null && x.Storingen.Any(s => s.InstanceId == st.InstanceId));
                    if (plek?.Werk != null)
                    {
                        Manager.FormulierActie(new object[] {plek.Werk.Parent, plek.Werk}, MainAktie.OpenProductie);
                    }
                }
            }
        }
    }
}