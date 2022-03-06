using BrightIdeasSoftware;
using Forms;
using MetroFramework.Controls;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;
using Rpm.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controls
{
    public partial class PersoneelsUI : UserControl
    {
        private bool _choose;

        public PersoneelsUI()
        {
            InitializeComponent();
        }

        public void InitUI(Bewerking bew = null, bool choose = false)
        {
            Bewerking = bew;
            ((OLVColumn) xuserlist.Columns[0]).ImageGetter = ImageGet;
            ((OLVColumn) xuserlist.Columns[0]).GroupKeyGetter = GroupGet;
            xsearchbox.ShowClearButton = true;
            Title = choose ? "Kies Personeel" : "Beheer Personeel";
            SetAfdelingFilter();
            if (Manager.Opties != null)
            {
                if (Manager.Opties.ViewDataPersoneelState != null)
                    xuserlist.RestoreState(Manager.Opties.ViewDataPersoneelState);
                if (!string.IsNullOrEmpty(Manager.Opties?.PersoneelAfdelingFilter))
                    xafdelingfilter.SelectedItem = Manager.Opties.PersoneelAfdelingFilter;
                else if (xafdelingfilter.Items.Count > 0)
                    xafdelingfilter.SelectedItem = 0;
            }

            imageList1.Images.Add(Resources.user_customer_person_13976);
            imageList1.Images.Add(
                Resources.user_customer_person_13976.CombineImage(
                    Resources.business_color_progress_icon_icons_com_53437, 2));
            imageList1.Images.Add(
                Resources.user_customer_person_13976.CombineImage(Resources.play_button_icon_icons_com_60615, 2));

            //((OLVColumn) xuserlist.Columns[0]).AspectGetter = NameGetter;
            _choose = choose;
            if (Bewerking != null && _choose)
            {
                this.Text =
                    $"Kies personeel voor productie [{Bewerking.ArtikelNr}, {Bewerking.ProductieNr}] {Bewerking.Naam}";
            }

            xformbuttonpanel.Visible = _choose;
            EnableFilters();
            xok.Text = "&OK";
            xok.Image = Resources.check_1582;
            xdialogbuttonpanel.Width = 250;
            InitEvents();
        }

        public void CloseUI()
        {
            DetachEvents();
            if (Manager.Opties != null)
                Manager.Opties.ViewDataPersoneelState = xuserlist.SaveState();
        }

        public void InitEvents()
        {
            Manager.OnSettingsChanged += PManager_OnSettingsChanged;
            Manager.OnPersoneelChanged += Manager_OnPersoneelChanged;
            Manager.OnPersoneelDeleted += Manager_OnPersoneelDeleted;
        }

        public void DetachEvents()
        {
            Manager.OnSettingsChanged -= PManager_OnSettingsChanged;
            Manager.OnPersoneelChanged -= Manager_OnPersoneelChanged;
            Manager.OnPersoneelDeleted -= Manager_OnPersoneelDeleted;
        }

        public event EventHandler StatusTextChanged;
        public event EventHandler CloseClicked;
        public event EventHandler OKClicked;

        protected virtual void OnStatusTextChanged(string text)
        {
            StatusTextChanged?.Invoke(text, EventArgs.Empty);
        }

        public Bewerking Bewerking { get; set; }
        

        public Personeel[] SelectedPersoneel =>
            xuserlist.SelectedObjects?.Cast<PersoneelModel>().Select(x => x.PersoneelLid).ToArray();

        private object NameGetter(object item)
        {
            if (item is PersoneelModel pers)
                return pers.Naam + (string.IsNullOrEmpty(pers.Afdeling) ? "" : $"[{pers.Afdeling}]");
            return "N/A";
        }

        private void Manager_OnPersoneelChanged(object sender, Personeel pers)
        {
            if (IsDisposed || Disposing) return;
            try
            {
                ProcessUser(pers);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Manager_OnPersoneelDeleted(object sender, string id)
        {
            if (IsDisposed || Disposing) return;
            this.BeginInvoke(new MethodInvoker(() =>
            {
                try
                {
                    var pers = xuserlist.Objects?.Cast<PersoneelModel>().ToList();
                    var remove = pers?.Where(x => string.Equals(x.Naam, id)).ToArray();
                    if (remove is {Length: > 0})
                    {
                        for (int i = 0; i < remove.Length; i++)
                        {
                            pers.Remove(remove[i]);
                        }

                        LoadPersoneel(pers.Select(x => x.PersoneelLid).ToList());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }));
        }

        public string Title { get; set; }

        private void UpdateStatus(int index = -1)
        {
            if (index == -1)
            {
                var filter = Currentfilter();
                if (filter != null)
                {
                    index = -1;
                    int.TryParse(filter.Tag as string, out index);
                }
            }

            var xtext = "Alle Medewerkers";
            var xm = xuserlist.Items.Count == 1 ? "Medewerker" : "Medewerkers";
            switch (index)
            {
                case 0:
                    xtext = $"{Title}: {xuserlist.Items.Count} Externe {xm}";
                    break;
                case 1:
                    xtext = $"{Title}: {xuserlist.Items.Count} Interne {xm}";
                    break;
                case 2:
                    xtext = $"{Title}: {xuserlist.Items.Count} {xm} Zijn Bezig";
                    break;
                case 3:
                    xtext = $"{Title}: {xuserlist.Items.Count} {xm} Op Verlof";
                    break;
                default:
                    if (xuserlist.Groups is {Count: > 0})
                    {
                        var value = "";
                        foreach (var group in xuserlist.Groups.Cast<ListViewGroup>())
                            value += $"| {group.Items.Count} {group.Header.Split('[')[0]}";
                        value = value.TrimStart('|', ' ');
                        xtext = $"{Title}: {value}";
                    }
                    else
                    {
                        xtext = $"{Title}: {xuserlist.Items.Count} Personeel Leden";
                    }

                    break;
            }
            OnStatusTextChanged(xtext);
        }

        private void ProcessUser(Personeel pers)
        {
            if (IsDisposed || pers == null)
                return;
            try
            {
                this.Invoke(new MethodInvoker(() =>
                {


                    var allowed = IsAllowed(pers);
                    var resort = false;
                    var per = xuserlist.Objects?.Cast<PersoneelModel>()
                        .FirstOrDefault(x => x.Naam.ToLower() == pers.PersoneelNaam.ToLower());
                    if (per != null && !allowed)
                    {
                        xuserlist.RemoveObject(per);
                        resort = true;
                    }
                    else if (per == null && allowed)
                    {
                        var xpers = new PersoneelModel(pers) {PersoneelLid = {Actief = true}};
                        xuserlist.AddObject(xpers);
                        resort = true;
                    }
                    else if (per != null)
                    {
                        resort = per.PersoneelLid.IsUitzendKracht != pers.IsUitzendKracht;
                        per.PersoneelLid = pers;
                        xuserlist.RefreshObject(per);
                    }

                    if (resort)
                    {
                        xuserlist.Sort("Kracht");
                        UpdateStatus();
                    }
                }));


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void PManager_OnSettingsChanged(object instance, UserSettings settings, bool init)
        {
            if (IsDisposed || Disposing) return;
            var user = Manager.LogedInGebruiker;
            Invoke(new Action(() =>
            {
                var _enable = user is {AccesLevel: >= AccesType.ProductieBasis};
                xuserinfopanel.Visible = _enable;
            }));
        }

        public object GroupGet(object sender)
        {
            try
            {
                var model = sender as PersoneelModel;
                if (model != null)
                    return model.Kracht;
                return "N/A";
            }
            catch
            {
                return 0;
            }
        }

        public object ImageGet(object sender)
        {
            try
            {
                var model = sender as PersoneelModel;
                if (model != null) return DefaultImageIndex(model.PersoneelLid);
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public void Show(Bewerking bew)
        {
            Bewerking = bew;
            var x = Currentfilter();
            var enable = x != null;
            xformbuttonpanel.Visible = false;
            if (EnableFilters())
            {
                xok.Text = "&Sluiten";
                //xannueer.Visible = false;
                // CollapseAllGroups();
                xok.Image = Resources.delete_1577;
                xdialogbuttonpanel.Width = 127;
                base.Show();
            }
        }
        
        private void CollapseAllGroups()
        {
            foreach (var group in xuserlist.Groups.Cast<ListViewGroup>().Select(x => x.Tag as OLVGroup))
                group.Collapsed = true;
        }

        private void SetAfdelingFilter()
        {
            if (Manager.Database?.PersoneelLijst == null) return;
            xafdelingfilter.Items.Clear();
            xafdelingfilter.Items.Add("Iedereen");
            var pers = Manager.Database.GetAllPersoneel().Result;
            if (pers?.Count > 0)
            {
                List<string> xafdelingen = new List<string>();
                foreach (var per in pers)
                {
                    if (string.IsNullOrEmpty(per.Afdeling)) continue;
                    if (!xafdelingen.Any(x =>
                            string.Equals(x, per.Afdeling.Trim(), StringComparison.CurrentCultureIgnoreCase)))
                        xafdelingen.Add(per.Afdeling.Trim());
                }

                if (xafdelingen.Count > 0)
                    xafdelingfilter.Items.AddRange(xafdelingen.Select(x => (object) x).ToArray());
            }
        }

        private void LoadPersoneel(List<Personeel> xpers)
        {
            var pers = xpers ?? Manager.Database.GetAllPersoneel().Result;
            if (xafdelingfilter.SelectedItem != null &&
                !string.IsNullOrEmpty(xafdelingfilter.SelectedItem.ToString().Trim()) &&
                xafdelingfilter.SelectedItem.ToString().ToLower() != "iedereen")
            {
                pers = pers.Where(x => !string.IsNullOrEmpty(x.Afdeling) && string.Equals(
                    xafdelingfilter.SelectedItem.ToString().Trim(), x.Afdeling.Trim(),
                    StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            xuserinfopanel.Enabled = false;
            xuserlist.BeginUpdate();
            var selected = xuserlist.SelectedObject;
            var models = GetPersoneelModels(pers);
            xuserlist.SetObjects(models);
            xuserlist.Sort("Kracht");
            xuserlist.EndUpdate();
            UpdateStatus();

            EnableSelected();
            xuserlist.SelectedObject = selected;
            xuserlist.SelectedItem?.EnsureVisible();
        }

        public List<PersoneelModel> GetPersoneelModels(List<Personeel> pers)
        {
            var models = new List<PersoneelModel>();
            foreach (var per in pers)
            {
                var x = new PersoneelModel(per);
                x.PersoneelLid.Actief = true;
                models.Add(x);
            }

            return models;
        }

        private void xuserlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xuserlist.SelectedItems.Count == 1)
                xuserinfopanel.Tag = xuserlist.SelectedObject;
            else
                xuserinfopanel.Tag = null;
            EnableSelected();
        }

        private void EnableSelected()
        {
            var acces1 = Manager.LogedInGebruiker is {AccesLevel: >= AccesType.ProductieBasis};
            var acces2 = Manager.LogedInGebruiker is {AccesLevel: >= AccesType.ProductieAdvance};

            var enable1 = xuserinfopanel.Tag != null;
            var enable2 = xuserlist.SelectedObjects.Count > 0;

            xuserinfopanel.Visible = !_choose && enable1 && acces1;

            verwijderPersoneelToolStripMenuItem.Enabled = enable2 && acces2;
            openWerkplekToolStripMenuItem.Enabled = enable1 && acces1 && !_choose && xuserlist.SelectedObjects
                .Cast<PersoneelModel>().Any(x => x.PersoneelLid.Klusjes.Any(t => t.Status == ProductieState.Gestart));
            zetOpNonActiefToolStripMenuItem.Enabled = openWerkplekToolStripMenuItem.Enabled;
            vaardighedenToolStripMenuItem.Enabled = enable1 && acces1;
            verlofToolStripMenuItem.Enabled = enable1 && acces2;
            xkiesvrijetijd.Enabled = enable1 && acces2;
            xuserinfopanel.Enabled = enable1 && acces1;
            wijzigAfdelingToolStripMenuItem.Enabled = acces1 && enable2;
            xklusjestoolstripitem.Enabled = acces1 && enable1;
            xklusjesb.Enabled = acces1 && enable1;
            werkRoosterToolStripMenuItem.Enabled = acces1 && enable1;
            xvaardigeheden.Enabled = enable1 && acces1;
            xdeletepersoneel.Enabled = enable2 && acces2;
            xverwijderpers.Enabled = enable1 && acces2;
            xwijzigpers.Enabled = acces1 && enable2;
            xisuitzendcheck.Enabled = acces2 && enable2;
            xaddpersoneel.Enabled = acces2;
            vouwAlleGroupenToolStripMenuItem.Enabled = xuserlist.Groups?.Count > 0;
            ontvouwAlleGroupenToolStripMenuItem.Enabled = xuserlist.Groups?.Count > 0;
            if (enable1)
                InitSelected();
        }

        private void InitSelected()
        {
            if (xuserinfopanel.Tag is PersoneelModel)
            {
                var model = xuserinfopanel.Tag as PersoneelModel;
                UpdateUserFields(model);
            }
        }

        private void UpdateUserFields(PersoneelModel model)
        {
            var per = model?.PersoneelLid;
            if (per != null)
            {
                xnaam.Text = per.PersoneelNaam;
                xafdeling.Text = per.Afdeling;
                xisuitzendcheck.Checked = per.IsUitzendKracht;
                //xisvrijimage.Image = per.IsVrij(DateTime.Now) ? Resources.delete_1577 : Resources.check_1582;
                //xuserimage.Image = imageList1.Images[DefaultImageIndex(per)];
            }
        }

        private async void xaddpersoneel_Click(object sender, EventArgs e)
        {
            var add = new AddPersoneel();
            if (add.ShowDialog() == DialogResult.OK)
            {
                var pers = add.PersoneelLid;
                if (await Manager.Database.UpSert(pers, $"{pers.PersoneelNaam} Toegevoegd.") && IsAllowed(pers))
                {
                    var xper = new PersoneelModel(pers);
                    // xuserlist.AddObject(xper);
                    xuserlist.Sort("Kracht");
                    xuserlist.SelectedObject = xper;
                    xuserlist.SelectedItem?.EnsureVisible();
                }
            }
        }

        private void xdeletepersoneel_Click(object sender, EventArgs e)
        {
            DeleteSelectedUsers();
        }

        private async void DeleteSelectedUsers()
        {
            if (xuserlist.SelectedObjects.Count > 0)
                if (XMessageBox.Show(
                        this,
                        $"Weetje zeker dat je '{string.Join(", ", xuserlist.SelectedObjects.Cast<PersoneelModel>().Select(x => x.Naam))}' gaat verwijderen?",
                        "Verwijder Personeel", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    var delete = xuserlist.SelectedObjects.Cast<PersoneelModel>().Select(x => x.PersoneelLid).ToArray();
                    if (await Manager.Database.Delete(delete) > 0) xuserlist.RemoveObjects(xuserlist.SelectedObjects);
                }
        }

        private async void xwijzigpers_Click(object sender, EventArgs e)
        {
            if (xuserinfopanel.Tag == null)
                return;
            try
            {
                if (string.IsNullOrEmpty(xnaam.Text) || xnaam.Text.Trim().Length < 3)
                    throw new Exception("Ongeldige personeel naam!");
                if (xuserinfopanel.Tag is PersoneelModel model) await WijzigSelectedUser(model);
            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> WijzigSelectedUser(PersoneelModel pers)
        {
            var per = pers?.PersoneelLid;
            if (per == null)
                return false;
            var oldnaam = per.PersoneelNaam;
            per.PersoneelNaam = xnaam.Text.Trim();
            per.Afdeling = xafdeling.Text.Trim();
            var resort = per.IsUitzendKracht != xisuitzendcheck.Checked && IsAllowed(per);
            per.IsUitzendKracht = xisuitzendcheck.Checked;
            if (!await Personeel.UpdateKlusjes(this, per, oldnaam)) return false;
            string change = $"Wijzigingen voor {per.PersoneelNaam} succesvol!";
            bool replace = !string.Equals(oldnaam, xnaam.Text, StringComparison.CurrentCultureIgnoreCase);
            if ((replace && await Manager.Database.Replace(oldnaam, per, change)) ||
                (!replace && await Manager.Database.UpSert(per, change)))
            {
                ProcessUser(per);
                if (resort) xuserlist.Sort("Kracht");
            }

            return true;
        }

        private int DefaultImageIndex(Personeel pers)
        {
            if (pers == null)
                return 0;
            if (pers.IsBezig)
                return 2;
            if (pers.IsVrij(DateTime.Now))
                return 1;
            return 0;
        }

        private void xverwijderpers_Click(object sender, EventArgs e)
        {
            DeleteSelectedUsers();
        }

        private void xok_Click(object sender, EventArgs e)
        {
            ChooseUser();
        }

        private void xannueer_Click(object sender, EventArgs e)
        {
            OnCloseClicked();
        }

        private bool ChooseUser()
        {
            if (_choose && xuserlist.SelectedObjects?.Count > 0)
            {
                var pers = xuserlist.SelectedObjects.Cast<PersoneelModel>().Select(x => x.PersoneelLid).ToArray();
                if (pers.Any(x => x.IsVrij(DateTime.Now)))
                {
                    XMessageBox.Show(this, $"Je hebt iemand gekozen die vrij is...!\nKies iemand anders a.u.b.",
                        "Personeel Vrij",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                OnOKClicked();
                return true;
            }
            return false;
        }

        private async void xuserlist_DoubleClick(object sender, EventArgs e)
        {
            if (_choose)
                ChooseUser();
            else if (xuserlist.SelectedObject is PersoneelModel model)
            {
                if (Manager.LogedInGebruiker == null || Manager.LogedInGebruiker.AccesLevel < AccesType.ProductieBasis)
                    return;

                var add = new AddPersoneel(model.PersoneelLid);
                if (add.ShowDialog() == DialogResult.OK)
                {
                    model.PersoneelLid = add.PersoneelLid;
                    if (await Manager.Database.UpSert(model.PersoneelLid, $"{model.Naam} Aangepast!") &&
                        IsAllowed(model.PersoneelLid))
                    {
                        xuserlist.RefreshObject(model);
                        xuserlist.SelectedObject = model;
                        xuserlist.SelectedItem?.EnsureVisible();
                        InitSelected();
                    }
                }
            }
        }

        private void xkiesvrijetijd_Click(object sender, EventArgs e)
        {
            OpenSelectedVrijeDagen();
        }

        private void verwijderPersoneelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xdeletepersoneel_Click(sender, e);
        }

        private void openWerkplekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xuserlist.SelectedObjects.Count > 0)
                foreach (var model in xuserlist.SelectedObjects.Cast<PersoneelModel>()
                             .Where(x => x.PersoneelLid.IsBezig))
                    try
                    {
                        foreach (var klus in model.PersoneelLid.Klusjes)
                        {
                            if (klus.Status != ProductieState.Gestart)
                                continue;
                            var pair = klus.GetWerk();
                            var form = pair.Formulier;
                            var werk = pair.Bewerking;
                            if (form != null && werk != null)
                            {
                                Manager.FormulierActie(new object[] {form, werk}, MainAktie.OpenProductie);
                                //ShowProductieForm(form, werk);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        XMessageBox.Show(this, ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
        }

        private void xpersoneelcontext_Opening(object sender, CancelEventArgs e)
        {
            EnableSelected();
            e.Cancel = !xpersoneelcontext.Items.Cast<ToolStripItem>().Any(x => !(x is ToolStripSeparator) && x.Enabled);
        }

        private async void EditSelectedRooster()
        {
            if (xuserlist.SelectedObject is PersoneelModel pers)
                try
                {
                    var rs = new RoosterForm(pers.PersoneelLid.WerkRooster ?? Manager.Opties.GetWerkRooster(),
                        $"Rooster voor {pers.Naam}");
                    if (rs.ShowDialog() == DialogResult.OK)
                    {
                        pers.PersoneelLid.WerkRooster = rs.WerkRooster;
                        await WijzigSelectedUser(pers);
                    }
                }
                catch (Exception ex)
                {
                    XMessageBox.Show(this, ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        public bool IsAllowed(Personeel pers)
        {
            if (pers == null)
                return false;
            var filter = Currentfilter();
            string fname = xsearchbox.Text.ToLower().Trim();
            if (!string.IsNullOrEmpty(fname) && !fname.Contains("zoeken"))
            {
                string[] xfilters = fname.Split(';').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                if (!xfilters.Any(s => pers.PersoneelNaam.ToLower().Contains(s.ToLower()) ||
                                       (pers.Afdeling != null && pers.Afdeling.ToLower().Contains(s.ToLower()))))
                    return false;

            }

            if (filter != null)
            {
                if (!int.TryParse(filter.Tag as string, out var index))
                    return false;
                switch (index)
                {
                    case 0:
                        return pers.IsUitzendKracht;
                    case 1:
                        return !pers.IsUitzendKracht;
                    case 2:
                        return pers.IsBezig;
                    case 3:
                        return pers.IsVrij(DateTime.Now);
                }

                return false;
            }

            if (xafdelingfilter.SelectedItem != null && xafdelingfilter.SelectedItem.ToString().ToLower() != "iedereen")
            {
                if (string.IsNullOrEmpty(pers.Afdeling)) return false;
                return string.Equals(pers.Afdeling.Trim(), xafdelingfilter.SelectedItem.ToString().Trim());
            }

            return true;
        }

        private bool EnableFilters(Button b = null)
        {
            try
            {
                if (Manager.Database?.PersoneelLijst == null) return false;
                var filter = b ?? Currentfilter();
                if (filter != null)
                {
                    var c = filter.BackColor;
                    xexternfilter.BackColor = Color.Transparent;
                    xinternefilter.BackColor = Color.Transparent;
                    xbezigfilter.BackColor = Color.Transparent;
                    xvrijfilter.BackColor = Color.Transparent;
                    if (c == Color.LightSkyBlue)
                        filter.BackColor = Color.Transparent;
                    else
                        filter.BackColor = Color.LightSkyBlue;
                }

                var pers = Manager.Database.GetAllPersoneel().Result;
                LoadPersoneel(pers.Where(IsAllowed).ToList());
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Button Currentfilter()
        {
            if (xexternfilter.BackColor == Color.LightSkyBlue)
                return xexternfilter;
            if (xinternefilter.BackColor == Color.LightSkyBlue)
                return xinternefilter;
            if (xbezigfilter.BackColor == Color.LightSkyBlue)
                return xbezigfilter;
            if (xvrijfilter.BackColor == Color.LightSkyBlue)
                return xvrijfilter;
            return null;
        }

        private void xfilter_Click(object sender, EventArgs e)
        {
            var x = sender as Button;
            if (x != null) EnableFilters(x);
        }

        private void vaardighedenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedVaardigheden();
        }

        private void verlofToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedVrijeDagen();
        }

        private void xvaardigeheden_Click(object sender, EventArgs e)
        {
            OpenSelectedVaardigheden();
        }

        private async void zetOpNonActiefToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xuserlist.SelectedObject is PersoneelModel permodel && XMessageBox.Show(this,
                    $"Weet je zeker dat je {permodel.Naam} op non actief wilt zetten?",
                    "Non Actief Zetten", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                if (await permodel.PersoneelLid.ZetOpInactief())
                {
                    UpdateUserFields(permodel);
                }
                else
                {
                    XMessageBox.Show(
                        this, $"Het is niet gelukt om {permodel.PersoneelLid.PersoneelNaam} op non actief te zetten!",
                        "Fout",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OpenSelectedVaardigheden()
        {
            if (xuserlist.SelectedObject is PersoneelModel)
            {
                if (xuserlist.SelectedObject is PersoneelModel model)
                {
                    var vf = new VaardighedenForm(model.PersoneelLid);
                    vf.ShowDialog();
                    UpdateUserFields(model);
                }
            }
        }

        private void xroosterb_Click(object sender, EventArgs e)
        {
            EditSelectedRooster();
        }

        private void werkRoosterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditSelectedRooster();
        }

        private async void OpenSelectedVrijeDagen()
        {
            if (xuserlist.SelectedObject is PersoneelModel model)
            {
                var vf = new VrijeTijdForm(model.PersoneelLid);
                if (vf.ShowDialog() == DialogResult.OK)
                {
                    var change = $"{model.Naam} Vrije Tijd  Aangepast\n" +
                                 $"Van: {Math.Round(model.PersoneelLid.TijdVrij().TotalHours, 2)} uur\n";

                    model.PersoneelLid.VrijeDagen = vf.VrijeTijd;

                    change += $"Naar: {Math.Round(model.PersoneelLid.TijdVrij().TotalHours, 2)} uur";


                    if (await Personeel.UpdateKlusjes(this, model.PersoneelLid))
                    {
                        await Manager.Database.UpSert(model.PersoneelLid.PersoneelNaam, model.PersoneelLid, change);

                        UpdateUserFields(model);
                    }
                }
            }
        }

        private void xuserlist_CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            if (e.Model is PersoneelModel wp)
            {
                e.Title = $"{wp.Naam}";
                e.Text = e.SubItem.Text;
                if (wp.PersoneelLid.WerktAan != null)
                    e.Text += $"Werkt aan: {wp.WerktAan}\n";
                else if (wp.PersoneelLid.Werkplek != null)
                    e.Text += $"Laats gewerkt op '{wp.PersoneelLid.Werkplek}'";
                if (wp.PersoneelLid.IsVrij(DateTime.Now))
                    e.Text += $"Let Op!! {wp.Naam} is vrij!";
                if (e.Text.Length == 0)
                    e.Text = $"{wp.Naam}";
            }
        }

        private void xklusjesb_Click(object sender, EventArgs e)
        {
            if (xuserlist.SelectedObject is PersoneelModel model)
            {
                var ak = new AlleKlusjes(model.PersoneelLid);
                ak.ShowDialog();
                xuserlist.RefreshObject(model);
                UpdateUserFields(model);
            }
        }

        private void wijzigAfdelingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xuserlist.SelectedObjects.Count > 0)
            {
                var tf = new TextFieldEditor();
                var field = "";
                var pers = xuserlist.SelectedObjects.Cast<PersoneelModel>().ToArray();
                var name = pers.Length == 1 ? pers[0].Naam + " Afdeling" : $"afdeling van {pers.Length} medewerkers";
                tf.Title = $"Wijzig {name}";
                foreach (var p in pers)
                    if (string.IsNullOrEmpty(field))
                        field = p.PersoneelLid.Afdeling;
                tf.SelectedText = field;
                if (tf.ShowDialog() == DialogResult.OK)
                {
                    foreach (var p in pers)
                    {
                        p.PersoneelLid.Afdeling = tf.SelectedText;
                        Manager.Database.UpSert(p.PersoneelLid,
                            $"{p.PersoneelLid.PersoneelNaam} afdeling aangepast naar {tf.SelectedText}.");
                        xuserlist.RefreshObject(p);
                    }

                    InitSelected();
                }
            }
        }

        private void CollapseGroups(bool collapsed)
        {
            foreach (ListViewGroup group in xuserlist.Groups)
                ((OLVGroup) @group.Tag).Collapsed = collapsed;
        }

        private void vouwAlleGroupenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CollapseGroups(true);
        }

        private void ontvouwAlleGroupenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CollapseGroups(false);
        }

        private void xafdelingfilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Manager.Opties != null && xafdelingfilter.SelectedIndex > -1)
            {
                Manager.Opties.PersoneelAfdelingFilter = xafdelingfilter.SelectedItem.ToString().Trim();
            }

            EnableFilters();
        }

        private void xsearch_Enter(object sender, EventArgs e)
        {
            if (sender is MetroTextBox {Text: "Zoeken..."} tb) tb.Text = "";
        }

        private void xsearch_Leave(object sender, EventArgs e)
        {
            if (sender is MetroTextBox tb)
                if (string.IsNullOrWhiteSpace(tb.Text))
                    tb.Text = "Zoeken...";
        }

        private void xsearchbox_TextChanged(object sender, EventArgs e)
        {
            if (xsearchbox.Text.Trim().ToLower() != "zoeken...")
            {
                EnableFilters();
            }
        }

        private void PersoneelsForm_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
            xsearchbox.Invalidate();
        }

        protected virtual void OnCloseClicked()
        {
            CloseClicked?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnOKClicked()
        {
            OKClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}