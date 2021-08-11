using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;
using Rpm.ViewModels;

namespace Forms
{
    public partial class PersoneelsForm : MetroFramework.Forms.MetroForm

    {
    private readonly bool _choose;

    public PersoneelsForm(Manager manager, bool choose)
    {
        InitializeComponent();
        ((OLVColumn) xuserlist.Columns[0]).ImageGetter = ImageGet;
        ((OLVColumn) xuserlist.Columns[0]).GroupKeyGetter = GroupGet;
        xsearchbox.ShowClearButton = true;
        SetAfdelingFilter();
        if (Manager.Opties != null)
        {
            if (Manager.Opties.ViewDataPersoneelState != null)
                xuserlist.RestoreState(Manager.Opties.ViewDataPersoneelState);
            if (!string.IsNullOrEmpty(Manager.Opties?.PersoneelAfdelingFilter))
                xafdelingfilter.SelectedItem = Manager.Opties.PersoneelAfdelingFilter;
        }

        imageList1.Images.Add(Resources.user_customer_person_13976);
        imageList1.Images.Add(
            Resources.user_customer_person_13976.CombineImage(
                Resources.business_color_progress_icon_icons_com_53437, 2));
        imageList1.Images.Add(
            Resources.user_customer_person_13976.CombineImage(Resources.play_button_icon_icons_com_60615, 2));

        //((OLVColumn) xuserlist.Columns[0]).AspectGetter = NameGetter;
        _choose = choose;
        PManager = manager;
    }

    public Manager PManager { get; set; }
    public Bewerking Bewerking { get; set; }

    private readonly List<StartProductie> _formuis = new();
    private Producties _producties;

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
            Invoke(new Action(() => ProcessUser(pers)));
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
                if (remove != null && remove.Length > 0)
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

        var xm = xuserlist.Items.Count == 1 ? "Medewerker" : "Medewerkers";
        switch (index)
        {
            case 0:
                Text = $"Personeel Beheer: {xuserlist.Items.Count} Externe {xm}";
                break;
            case 1:
                Text = $"Personeel Beheer: {xuserlist.Items.Count} Interne {xm}";
                break;
            case 2:
                Text = $"Personeel Beheer: {xuserlist.Items.Count} {xm} Zijn Bezig";
                break;
            case 3:
                Text = $"Personeel Beheer: {xuserlist.Items.Count} {xm} Op Verlof";
                break;
            default:
                if (xuserlist.Groups != null && xuserlist.Groups.Count > 0)
                {
                    var value = "";
                    foreach (var group in xuserlist.Groups.Cast<ListViewGroup>())
                        value += $"| {group.Items.Count} {group.Header.Split('[')[0]}";
                    value = value.TrimStart('|', ' ');
                    Text = $"Personeel Beheer: {value}";
                }
                else
                {
                    Text = $"Personeel Beheer: {xuserlist.Items.Count} Personeel Leden";
                }

                break;
        }

        this.Invalidate();
    }

    private void ProcessUser(Personeel pers)
    {
        if (IsDisposed || pers == null)
            return;
        try
        {
            this.BeginInvoke(new MethodInvoker(() =>
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
                    per.PersoneelLid.Actief = true;
                    per.PersoneelLid.WerktAan = pers.WerktAan;
                    per.PersoneelLid.Werkplek = pers.Werkplek;
                    per.PersoneelLid.Klusjes = pers.Klusjes;
                    per.PersoneelLid.PerUur = pers.PerUur;
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

    private void PManager_OnSettingsChanged(object instance,UserSettings settings, bool init)
    {
        if (IsDisposed || Disposing) return;
        var user = Manager.LogedInGebruiker;
        Invoke(new Action(() =>
        {
            var _enable = user != null && user.AccesLevel >= AccesType.ProductieBasis;
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

    public DialogResult ShowDialog(Bewerking bew)
    {
        if (_choose)
        {
            xformbuttonpanel.Visible = true;
            this.Text = $"Kies personeel voor productie [{bew.ArtikelNr}, {bew.ProductieNr}] {bew.Naam}";
        }

        Bewerking = bew;
        EnableFilters();
        xok.Text = "&OK";
        xok.Image = Resources.check_1582;
        xdialogbuttonpanel.Width = 250;
        //xannueer.Visible = true;
        // CollapseAllGroups();
        return base.ShowDialog();
    }

    public new DialogResult ShowDialog()
    {
        if (_choose && Bewerking != null)
        {
            xformbuttonpanel.Visible = true;
            this.Text = $"Kies personeel voor productie [{Bewerking.ArtikelNr}, {Bewerking.ProductieNr}] {Bewerking.Naam}";
        }
        
        EnableFilters();
        xok.Text = "&OK";
        xok.Image = Resources.check_1582;
        xdialogbuttonpanel.Width = 250;
        //xannueer.Visible = true;
        // CollapseAllGroups();
        return base.ShowDialog();
    }



        private void CollapseAllGroups()
    {
        foreach (var group in xuserlist.Groups.Cast<ListViewGroup>().Select(x => x.Tag as OLVGroup))
            group.Collapsed = true;
    }

    private void SetAfdelingFilter()
    {
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
        var acces1 = Manager.LogedInGebruiker != null &&
                     Manager.LogedInGebruiker.AccesLevel >= AccesType.ProductieBasis;
        var acces2 = Manager.LogedInGebruiker != null &&
                     Manager.LogedInGebruiker.AccesLevel >= AccesType.ProductieAdvance;

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
        if (xuserinfopanel.Tag != null && xuserinfopanel.Tag is PersoneelModel)
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
            if (await Manager.Database.PersoneelExist(pers.PersoneelNaam))
            {
                XMessageBox.Show($"{pers.PersoneelNaam} bestaal al!", "Bestaat Al", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                if (await Manager.Database.UpSert(pers, $"{pers.PersoneelNaam} Toegevoegd.") && IsAllowed(pers))
                {
                    var xper = new PersoneelModel(pers);
                    xuserlist.AddObject(xper);
                    xuserlist.Sort("Kracht");
                    xuserlist.SelectedObject = xper;
                    xuserlist.SelectedItem?.EnsureVisible();
                }
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
            XMessageBox.Show(ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        if (!await UpdateKlusjes(per, oldnaam)) return false;
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
        DialogResult = DialogResult.Cancel;
    }

    private void ChooseUser()
    {
        if (_choose && xuserlist.SelectedObjects?.Count > 0)
        {
            var pers = xuserlist.SelectedObjects.Cast<PersoneelModel>().Select(x => x.PersoneelLid).ToArray();
            if (pers.Any(x => x.IsVrij(DateTime.Now)))
                XMessageBox.Show("Je hebt iemand gekozen die vrij is...!\nKies iemand anders a.u.b.",
                    "Personeel Vrij",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                DialogResult = DialogResult.OK;
        }
        else if (_choose)
        {
            DialogResult = DialogResult.Cancel;
        }
        else
        {
            Close();
        }
    }

    private void xuserlist_DoubleClick(object sender, EventArgs e)
    {
        if (_choose)
            ChooseUser();
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
                    XMessageBox.Show(ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
    }

    public void ShowProductieForm(ProductieFormulier pform, Bewerking bewerking = null, Form owner = null)
    {
        if (pform == null || pform.State == ProductieState.Verwijderd)
            return;
        var productie = _formuis.FirstOrDefault(t => t.Name == pform.ProductieNr.Trim().Replace(" ", ""));
        if (productie != null && !productie.IsDisposed && _producties != null)
        {
            productie.Show(_producties.DockPanel);
        }
        else
        {
            if (productie != null)
            {
                _formuis.Remove(productie);
                //productie.Dispose();
            }

            productie = new StartProductie(pform, bewerking)
            {
                Name = pform.ProductieNr.Trim().Replace(" ", ""),
                TabText = $"[{pform.ProductieNr}, {pform.ArtikelNr}]"
            };
            productie.FormClosing += AddProduction_FormClosing;
            productie.SelectedBewerking = bewerking;
            _formuis.Add(productie);

            if (_producties == null || _producties.IsDisposed)
            {
                _producties = new Producties();
                _producties.Tag = productie;
                _producties.StartPosition = FormStartPosition.CenterScreen;
                _producties.FormClosed += _prod_FormClosed;
            }
        }

        if (!_producties.Visible)
            _producties.Show();
        if (_producties.WindowState == FormWindowState.Minimized)
            _producties.WindowState = FormWindowState.Normal;
        _producties.Focus();

        productie.Show(_producties.DockPanel);
    }

    private void _prod_FormClosed(object sender, FormClosedEventArgs e)
    {
        var x = (Producties) sender;
        if (x != null)
            _producties = null;
    }

    private void AddProduction_FormClosing(object sender, FormClosingEventArgs e)
    {
        var AddProduction = (StartProductie) sender;
        if (AddProduction != null)
            _formuis.Remove(AddProduction);
    }

    private void xpersoneelcontext_Opening(object sender, CancelEventArgs e)
    {
        EnableSelected();
        e.Cancel = !xpersoneelcontext.Items.Cast<ToolStripItem>().Any(x => !(x is ToolStripSeparator) && x.Enabled);
    }

    private void PersoneelsForm_Shown(object sender, EventArgs e)
    {
        Manager.OnSettingsChanged += PManager_OnSettingsChanged;
        Manager.OnPersoneelChanged += Manager_OnPersoneelChanged;
        Manager.OnPersoneelDeleted += Manager_OnPersoneelDeleted;
    }

    private async void EditSelectedRooster()
    {
        var bttns = new Dictionary<string, DialogResult>();
        bttns.Add("Annuleren", DialogResult.Cancel);
        bttns.Add("Standaard", DialogResult.No);
        bttns.Add("Eigen Rooster", DialogResult.Yes);
        if (xuserlist.SelectedObject is PersoneelModel pers)
            try
            {
                var dialog = XMessageBox.Show($"Wat voor rooster zou je willen gebruiken voor {pers.Naam}?\n" +
                                              $"Kies voor {pers.Naam} een standaard of eigen rooster.\n", "Rooster",
                    MessageBoxButtons.OK, MessageBoxIcon.Question, null, bttns);
                if (dialog == DialogResult.Cancel) return;

                switch (dialog)
                {
                    case DialogResult.No:
                        pers.PersoneelLid.WerkRooster = null;
                        break;
                    case DialogResult.Yes:
                        var rs = new RoosterForm(pers.PersoneelLid.WerkRooster ?? Manager.Opties.GetWerkRooster(),
                            $"Rooster voor {pers.Naam}");
                        if (rs.ShowDialog() == DialogResult.OK)
                        {
                            pers.PersoneelLid.WerkRooster = rs.WerkRooster;
                            await WijzigSelectedUser(pers);
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                XMessageBox.Show(ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        var permodel = xuserlist.SelectedObject as PersoneelModel;
        if (permodel != null && XMessageBox.Show($"Weet je zeker dat je {permodel.Naam} op non actief wilt zetten?",
            "Non Actief Zetten", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
        {
            if (await permodel.PersoneelLid.ZetOpInactief())
            {
                UpdateUserFields(permodel);
                XMessageBox.Show($"{permodel.PersoneelLid.PersoneelNaam} is op non actief gezet.", "Inactief",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XMessageBox.Show(
                    $"Het is niet gelukt om {permodel.PersoneelLid.PersoneelNaam} op non actief te zetten!", "Fout",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void OpenSelectedVaardigheden()
    {
        if (xuserlist.SelectedObject != null && xuserlist.SelectedObject is PersoneelModel)
        {
            var model = xuserlist.SelectedObject as PersoneelModel;
            if (model != null)
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


                if (await UpdateKlusjes(model.PersoneelLid))
                {
                    await Manager.Database.UpSert(model.PersoneelLid.PersoneelNaam, model.PersoneelLid, change);

                    UpdateUserFields(model);
                }
            }
        }
    }

    private async Task<bool> UpdateKlusjes(Personeel persoon, string naam = null)
    {
        try
        {
            naam ??= persoon.PersoneelNaam;
            bool flag = persoon.Klusjes.Any(x =>
                x.Status == ProductieState.Gestart || x.Status == ProductieState.Gestopt);
            var result = flag
                ? XMessageBox.Show(
                    $"Wil je alle loopende klusjes van {persoon.PersoneelNaam} ook updaten?",
                    "Personeel Klusjes Updaten", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question)
                : DialogResult.No;
            if (result == DialogResult.Cancel) return false;
            if (result == DialogResult.Yes)
            {
                foreach (var klus in persoon.Klusjes)
                {
                    if (klus.Status == ProductieState.Verwijderd ||
                        klus.Status == ProductieState.Gereed) continue;
                    var werk = klus.GetWerk();
                    if (werk == null || !werk.IsValid || werk.Bewerking == null || werk.Plek == null) continue;
                    bool changed = false;
                    klus.PersoneelNaam = persoon.PersoneelNaam.Trim();
                    klus.Tijden.WerkRooster = persoon.WerkRooster;
                    foreach (var xper in werk.Plek.Personen)
                    {
                        if (!string.Equals(naam, xper.PersoneelNaam, StringComparison.CurrentCultureIgnoreCase))
                            continue;
                        xper.PersoneelNaam = persoon.PersoneelNaam.Trim();
                        xper.WerkRooster = persoon.WerkRooster;
                        xper.VrijeDagen = persoon.VrijeDagen;
                        xper.Efficientie = persoon.Efficientie;
                        xper.IsAanwezig = persoon.IsAanwezig;
                        xper.IsUitzendKracht = persoon.IsUitzendKracht;
                        foreach (var xklus in xper.Klusjes)
                        {
                            xklus.PersoneelNaam = xper.PersoneelNaam;
                            xklus.Tijden.WerkRooster = xper.WerkRooster;
                            xklus.WerkPlek = werk.Plek.Naam;
                            xklus.Naam = werk.Bewerking.Naam;
                            xklus.ProductieNr = werk.Bewerking.ProductieNr;
                        }

                        changed = true;
                    }

                    if (changed)
                    {
                        await werk.Bewerking.UpdateBewerking(null,
                            $"{persoon.PersoneelNaam} Werkrooster aangepast op klus {klus.Path}", true);
                    }
                }
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    private void xuserlist_CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
    {
        var wp = e.Model as PersoneelModel;
        if (wp != null)
        {
            e.Title = $"{wp.Naam}";
            e.Text = "";
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

    private void PersoneelsForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (Manager.Opties != null)
            Manager.Opties.ViewDataPersoneelState = xuserlist.SaveState();
        Manager.OnSettingsChanged -= PManager_OnSettingsChanged;
        Manager.OnPersoneelChanged -= Manager_OnPersoneelChanged;
        Manager.OnPersoneelDeleted -= Manager_OnPersoneelDeleted;
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
        var tb = sender as MetroFramework.Controls.MetroTextBox;
        if (tb != null)
            if (tb.Text == "Zoeken...")
                tb.Text = "";
    }

    private void xsearch_Leave(object sender, EventArgs e)
    {
        var tb = sender as MetroFramework.Controls.MetroTextBox;
        if (tb != null)
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
    }
}