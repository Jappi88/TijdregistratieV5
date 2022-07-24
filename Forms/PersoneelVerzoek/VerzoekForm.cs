using BrightIdeasSoftware;
using Forms.Verzoeken;
using ProductieManager.Forms.PersoneelVerzoek;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Productie;
using Rpm.Verzoeken;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Forms.PersoneelVerzoek
{
    public partial class VerzoekForm : MetroBase.MetroBaseForm
    {
        private string _CurrentAfdeling;
        private string _CurrentPersoon;
        private Personeel _Persoon;
        private List<VerzoekEntry> Verzoeken = new List<VerzoekEntry>();

        public VerzoekForm()
        {
            InitializeComponent();
            SetPersoneelNamen();
            imageList1.Images.Add(Resources.transfer_man_32x32);
            imageList1.Images.Add(Resources.ic_thumb_up_128_28824);
            imageList1.Images.Add(Resources.refresh_arrow_1546);
            imageList1.Images.Add((Resources.cancel_stop_exit_1583));
            this.Load += VerzoekForm_Load;
            this.Shown += VerzoekForm_Shown;
            this.FormClosing += VerzoekForm_FormClosing;
            ((OLVColumn)xVerzoeklijst.Columns[0]).ImageGetter = GetImageIndex;
        }

        private object GetImageIndex(object item)
        {
            if (item is VerzoekEntry ent)
            {
                switch (ent.Status)
                {
                    case VerzoekStatus.InAfwachting:
                        return 2;
                    case VerzoekStatus.GoedGekeurd:
                        return 1;
                    case VerzoekStatus.Afgekeurd:
                        return 3;
                }
            }
            return 0;
        }

        private void VerzoekForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            DetachEvents();
        }

        private void VerzoekForm_Shown(object sender, EventArgs e)
        {
            InitEvents();
        }

        private void InitEvents()
        {
            Manager.OnSettingsChanged += Manager_OnSettingsChanged;
            Manager.OnLoginChanged += Manager_OnLoginChanged;
            Manager.VerzoekChanged += Manager_VerzoekChanged;
            Manager.VerzoekDeleted += Manager_VerzoekDeleted;
        }

        private void DetachEvents()
        {
            Manager.OnSettingsChanged -= Manager_OnSettingsChanged;
            Manager.OnLoginChanged -= Manager_OnLoginChanged;
            Manager.VerzoekChanged -= Manager_VerzoekChanged;
            Manager.VerzoekDeleted -= Manager_VerzoekDeleted;
        }

        private void Manager_VerzoekDeleted(object sender, System.IO.FileSystemEventArgs e)
        {
            try
            {
                if (sender is string val)
                {
                    if (Verzoeken.RemoveAll(x => string.Equals(val, x.ID.ToString(), StringComparison.CurrentCultureIgnoreCase)) > 0)
                    {
                        ListVerzoeken();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void Manager_VerzoekChanged(object sender, System.IO.FileSystemEventArgs e)
        {
            try
            {
                if (sender is VerzoekEntry ent)
                {
                    bool allowed = IsAllowed(ent, false);
                    var index = Verzoeken.IndexOf(ent);
                    if (index == -1)
                    {
                        if (allowed)
                        {
                            Verzoeken.Add(ent);
                            if (IsAllowed(ent, true))
                            {
                                xVerzoeklijst.AddObject(ent);
                            }
                        }
                    }
                    else
                    {
                        if (allowed)
                        {
                            Verzoeken[index] = ent;
                            if (IsAllowed(ent, true))
                            {
                                xVerzoeklijst.RefreshObject(ent);
                            }
                        }
                        else
                        {
                            Verzoeken.Remove(ent);
                            xVerzoeklijst.RemoveObject(ent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void Manager_OnLoginChanged(Rpm.Settings.UserAccount user, object instance)
        {
            try
            {
                CheckCurrent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void Manager_OnSettingsChanged(object instance, Rpm.Settings.UserSettings settings, bool reinit)
        {
            try
            {
                CheckCurrent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void CheckCurrent()
        {
            if (string.IsNullOrEmpty(Manager.Opties?.Username))
            {
                if (this.InvokeRequired)
                    this.Invoke(new Action(this.Close));
                else this.Close();
            }
            else if (!string.Equals(Manager.Opties.Username, _CurrentAfdeling, StringComparison.CurrentCultureIgnoreCase))
            {
                InitVerzoeken(_CurrentPersoon);
            }
            else
            {
                EnableButtons();
            }
        }

        private void VerzoekForm_Load(object sender, EventArgs e)
        {
            InitVerzoeken();
        }

        private void SetPersoneelNamen()
        {
            try
            {
                var namen = Manager.Database.PersoneelLijst?.GetAllIDs(false) ?? new List<string>();
                xpersoneelnamen.AutoCompleteCustomSource = new System.Windows.Forms.AutoCompleteStringCollection();
                xpersoneelnamen.AutoCompleteCustomSource.AddRange(namen.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void InitVerzoeken(string personeel = null, List<VerzoekEntry> verzoeken = null)
        {
            try
            {
                if (this.Disposing || this.IsDisposed) return;
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(() => InitVerzoeken(personeel, verzoeken)));
                else
                {
                    bool flag = Manager.LogedInGebruiker is { AccesLevel: Rpm.Various.AccesType.Manager };
                    bool flag1 = Manager.LogedInGebruiker is { AccesLevel: Rpm.Various.AccesType.ProductieAdvance };
                    xNieuweVerzoek.Enabled = flag || flag1;
                    bool unread = string.IsNullOrEmpty(personeel) && Manager.LogedInGebruiker is {AccesLevel: < Rpm.Various.AccesType.Manager };
                    VerzoekStatus status = !unread && string.IsNullOrEmpty(personeel) ? VerzoekStatus.InAfwachting : VerzoekStatus.Geen;
                    string afdeling = flag ? null : Manager.Opties.Username;
                    if (string.IsNullOrEmpty(personeel))
                    {
                        _Persoon = null;
                    }
                    else if (_Persoon?.PersoneelNaam == null || !string.Equals(personeel, _Persoon.PersoneelNaam, StringComparison.CurrentCultureIgnoreCase))
                    {
                        _Persoon = Manager.Database.xGetPersoneel(personeel);
                    }

                    Verzoeken = verzoeken ??= Manager.Verzoeken.GetEntries(personeel, xalleafdelingencheckbox.Checked ? null : afdeling, unread, status);
                    _CurrentAfdeling = afdeling;
                    _CurrentPersoon = personeel;
                    ListVerzoeken();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ListVerzoeken()
        {
            try
            {
                if (this.Disposing || this.IsDisposed) return;
                if (InvokeRequired)
                    this.Invoke(new MethodInvoker(ListVerzoeken));
                else
                {
                    var verz = Verzoeken.Where(x => IsAllowed(x, true)).ToList();
                    var sel = xVerzoeklijst.SelectedObjects;
                    xVerzoeklijst.BeginUpdate();
                    xVerzoeklijst.SetObjects(verz);
                    xVerzoeklijst.EndUpdate();
                    xVerzoeklijst.SelectedObjects = sel;
                    xVerzoeklijst.SelectedItem?.EnsureVisible();
                    EnableButtons();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private bool IsAllowed(VerzoekEntry entry, bool filter)
        {
            if (entry == null) return false;
            bool flag1 = Manager.LogedInGebruiker is { AccesLevel: Rpm.Various.AccesType.ProductieAdvance };
            bool flag2 = Manager.LogedInGebruiker is { AccesLevel: Rpm.Various.AccesType.Manager };
            if (!flag1 && !flag2) return false;
            var xret = string.Equals(entry.PersoneelNaam, _CurrentPersoon, StringComparison.CurrentCultureIgnoreCase);
            var crit = xZoekTextbox.Text.Trim();
            if (crit.ToLower().StartsWith("zoeken"))
                crit = "";
            if (filter && !string.IsNullOrEmpty(crit))
            {
                xret &= (entry.Afdeling != null && entry.Afdeling.ToLower().Contains(crit.ToLower())) ||
                    (entry.PersoneelNaam != null && entry.PersoneelNaam.ToLower().Contains(crit.ToLower())) ||
                    (entry.NaamMelder != null && entry.NaamMelder.ToLower().Contains(crit.ToLower())) ||
                    (entry.VerzoekReactie != null && entry.VerzoekReactie.ToLower().Contains(crit.ToLower())) ||
                    (entry.VerzoekMelding != null && entry.VerzoekMelding.ToLower().Contains(crit.ToLower()));
                if (!xret) return false;
            }
            if (!xret)
            {
                if (string.IsNullOrEmpty(_CurrentPersoon))
                {
                    if (!entry.IsRead() && (flag1 || flag2)) return true;
                    if (entry.Status == VerzoekStatus.InAfwachting && (flag2 || (flag1 && string.Equals(Manager.Opties?.Username, entry.Afdeling, StringComparison.CurrentCultureIgnoreCase))))
                        return true;
                }
            }
            return xret;
        }

        private void xNieuweVerzoek_Click(object sender, EventArgs e)
        {
            try
            {
                if (Manager.LogedInGebruiker is { AccesLevel: < Rpm.Various.AccesType.ProductieAdvance })
                    return;
                var verz = new NewVerzoekForm(new VerzoekEntry() { PersoneelNaam = _CurrentPersoon }) { Personeel = _Persoon };
                if (verz.ShowDialog(this) == DialogResult.OK)
                {
                    if (Manager.Verzoeken?.Database is { CanRead: true })
                    {
                        Manager.Verzoeken.UpdateVerzoek(verz.Verzoek);
                        _Persoon = verz.Personeel;
                        _CurrentPersoon = verz.Verzoek.PersoneelNaam;
                        if (!string.Equals(xpersoneelnamen.Text.Trim(), _CurrentPersoon, StringComparison.CurrentCultureIgnoreCase))
                        {
                            xpersoneelnamen.Text = _CurrentPersoon;
                            xpersoneelnamen.Invalidate();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void xKiesPersoneel_Click(object sender, EventArgs e)
        {
            try
            {
                var xpers = new PersoneelsForm(null, true);
                if (xpers.ShowDialog(this) == DialogResult.OK)
                {
                    var pers = xpers.SelectedPersoneel?.First();
                    xpersoneelnamen.Text = pers?.PersoneelNaam;
                    _Persoon = pers;
                    _CurrentPersoon = pers.PersoneelNaam;
                    xpersoneelnamen.Select();
                    xpersoneelnamen.Focus();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void xpersoneelnamen_Enter(object sender, EventArgs e)
        {
            if (xpersoneelnamen.Text.ToLower().StartsWith("personeelnaam"))
            {
                xpersoneelnamen.Text = "";
            }
        }

        private void xpersoneelnamen_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xpersoneelnamen.Text.Trim()))
            {
                xpersoneelnamen.Text = "Personeelnaam...";
            }
        }

        private void xPersoneelSluiten_Click(object sender, EventArgs e)
        {
            xpersoneelnamen.Text = "";
            _CurrentPersoon = null;
            _Persoon = null;
            xpersoneelnamen.Select();
            xpersoneelnamen.Focus();
        }

        private void xpersoneelnamen_TextChanged(object sender, EventArgs e)
        {
            if (xpersoneelnamen.Text.ToLower().StartsWith("personeelnaam"))
            {
                xpersoneelnamen.Font = new Font("Segoe UI", 10, FontStyle.Italic);
                xpersoneelnamen.ForeColor = Color.Gray;
            }
            else
            {
                xpersoneelnamen.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                xpersoneelnamen.ForeColor = Color.Black;
            }
            xPersoneelTimer.Stop();
            xPersoneelTimer.Start();
        }

        string _LastSearch = null;
        private void xPersoneelTimer_Tick(object sender, EventArgs e)
        {
            xPersoneelTimer.Stop();
            var name = xpersoneelnamen.Text.Trim().ToLower().StartsWith("personeelnaam") ? null : xpersoneelnamen.Text.Trim();
            if (string.IsNullOrEmpty(name))
                name = null;
            if (string.Equals(_LastSearch, name, StringComparison.CurrentCultureIgnoreCase)) return;
            _LastSearch = name;
            InitVerzoeken(name);
        }

        private void EnableButtons()
        {
            if (InvokeRequired)
                this.Invoke(new Action(EnableButtons));
            else
            {
                bool flag0 = Manager.LogedInGebruiker is { AccesLevel: > Rpm.Various.AccesType.ProductieAdvance };
                bool flag1 = Manager.LogedInGebruiker is { AccesLevel: > Rpm.Various.AccesType.ProductieBasis };
                bool flag2 = Manager.LogedInGebruiker is { AccesLevel: < Rpm.Various.AccesType.Manager} && xVerzoeklijst.SelectedObject is VerzoekEntry ent && string.Equals(ent.Afdeling, Manager.Opties.Username, StringComparison.CurrentCultureIgnoreCase) && ent.Status is VerzoekStatus.InAfwachting;
                xNieuweVerzoek.Enabled = flag1;
                xverwijderverzoek.Enabled = flag1 && xVerzoeklijst.SelectedObjects.Count > 0;
                xEditVerzoek.Enabled = (flag0 || flag2) && xVerzoeklijst.SelectedObjects.Count == 1;
                wijzigenToolStripMenuItem.Enabled = xEditVerzoek.Enabled;
                verwijderenToolStripMenuItem.Enabled = xverwijderverzoek.Enabled;
                xalleafdelingencheckbox.Visible = Manager.LogedInGebruiker is { AccesLevel: < Rpm.Various.AccesType.Manager };
            }
        }

        private void xVerzoeklijst_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        private void xverwijderverzoek_Click(object sender, EventArgs e)
        {
            if (Manager.Verzoeken?.Database == null || !Manager.Verzoeken.Database.CanRead) return;
            bool flag = xVerzoeklijst.SelectedObjects.Count > 0 && Manager.LogedInGebruiker is { AccesLevel: > Rpm.Various.AccesType.ProductieBasis };
            if (!flag) return;
            bool remove = Manager.LogedInGebruiker is { AccesLevel: > Rpm.Various.AccesType.ProductieAdvance };
            var items = xVerzoeklijst.SelectedObjects.Cast<VerzoekEntry>().ToList();
            var x1 = items.Count == 1 ? "Verzoek" : "Verzoeken";
            var msg = remove ? $"Weetje zeker dat je {items.Count} {x1.ToLower()} wilt verwijderen?" :
                $"Weetje zeker dat je een verzoek wilt indienen voor het verwijderen van {items.Count} {x1.ToLower()}?";
            if (XMessageBox.Show(this, msg, $"{items.Count} {x1} Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            foreach (var item in items)
            {
                if (remove)
                {
                    if (Manager.Verzoeken.Database.Delete(item.ID.ToString()))
                    {
                        xVerzoeklijst.RemoveObject(item);
                        if (item.VerzoekSoort == VerzoekType.Vrij)
                        {
                            var pers = Manager.Database.xGetPersoneel(item.PersoneelNaam);
                            if (pers != null)
                            {
                                if (pers.VrijeDagen.Remove(new TijdEntry(item.StartDatum, item.EindDatum, pers.WerkRooster)))
                                {
                                    if (pers != null)
                                    {
                                        Manager.Database.UpSert(pers, $"Verlof voor {pers.PersoneelNaam} aangepast!");
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    item.VerzoekSoort = VerzoekType.Verwijderen;
                    item.Status = VerzoekStatus.InAfwachting;
                    item.GelezenDoor = new List<string>() { Manager.Opties.Username };
                    if (Manager.Verzoeken.UpdateVerzoek(item))
                    {
                        xVerzoeklijst.RefreshObject(item);
                    }
                }
            }
        }

        private void xClearZoek_Click(object sender, EventArgs e)
        {
            xZoekTextbox.Text = "";
            xZoekTextbox.Select();
            xZoekTextbox.Focus();
        }

        private void xZoekTextbox_Enter(object sender, EventArgs e)
        {
            if (xZoekTextbox.Text.ToLower().StartsWith("zoeken"))
            {
                xZoekTextbox.Text = "";
            }
        }

        private void xZoekTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xZoekTextbox.Text.Trim()))
            {
                xZoekTextbox.Text = "Zoeken...";
            }
        }

        private string _lastcrit = "";
        private void xZoekTextbox_TextChanged(object sender, EventArgs e)
        {
            if (xZoekTextbox.Text.ToLower().StartsWith("zoeken"))
            {
                xZoekTextbox.Font = new Font("Segoe UI", 10, FontStyle.Italic);
                xZoekTextbox.ForeColor = Color.Gray;
            }
            else
            {
                xZoekTextbox.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                xZoekTextbox.ForeColor = Color.Black;
            }
            var crit = xZoekTextbox.Text.Trim();
            if (crit.ToLower().StartsWith("zoeken"))
                crit = "";
            if (!string.Equals(_lastcrit, crit))
            {
                _lastcrit = crit;
                ListVerzoeken();
            }
        }

        private void xEditVerzoek_Click(object sender, EventArgs e)
        {
            if(xVerzoeklijst.SelectedObject is VerzoekEntry entry)
            {
                try
                {
                    if (Manager.LogedInGebruiker is { AccesLevel: < Rpm.Various.AccesType.Manager } && !string.Equals(entry.Afdeling, Manager.Opties.Username, StringComparison.CurrentCultureIgnoreCase))
                        return;
                    if (Manager.LogedInGebruiker is { AccesLevel: < Rpm.Various.AccesType.Manager } && entry.Status is not VerzoekStatus.InAfwachting)
                        return;
                    var verz = new NewVerzoekForm(entry);
                    if (verz.ShowDialog(this) == DialogResult.OK)
                    {
                        if (Manager.Verzoeken?.Database is { CanRead: true })
                        {
                            xVerzoeklijst.RefreshObject(verz.Verzoek);
                            Manager.Verzoeken.UpdateVerzoek(verz.Verzoek);
                            _Persoon = verz.Personeel;
                            _CurrentPersoon = verz.Verzoek.PersoneelNaam;
                            if (!string.Equals(xpersoneelnamen.Text.Trim(), _CurrentPersoon, StringComparison.CurrentCultureIgnoreCase))
                            {
                                xpersoneelnamen.Text = _CurrentPersoon;
                                xpersoneelnamen.Invalidate();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void xVerzoeklijst_DoubleClick(object sender, EventArgs e)
        {
            if (xVerzoeklijst.SelectedObjects.Count > 0)
            {
                var xnot = new VerzoekNotificatieForm();
                var items = xVerzoeklijst.SelectedObjects.Cast<VerzoekEntry>().ToArray();
                xnot.InitVerzoeken(items);
                xnot.StartPosition = FormStartPosition.CenterParent;
                xnot.ClickAble = false;
                xnot.TopMost = false;
                xnot.ShowDialog(this);
                if (Manager.Verzoeken?.Database == null || !Manager.Verzoeken.Database.CanRead) return;
                foreach(var item in items)
                {
                    if(!item.IsRead())
                    {
                        item.SetRead(true);
                        Manager.Verzoeken.UpdateVerzoek(item);
                    }
                    xVerzoeklijst.RefreshObject(item);
                }
            }
        }

        private void xalleafdelingencheckbox_CheckedChanged(object sender, EventArgs e)
        {
            InitVerzoeken(_CurrentPersoon);
        }
    }
}
