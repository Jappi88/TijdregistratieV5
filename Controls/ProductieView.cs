﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoUpdaterDotNET;
using BrightIdeasSoftware;
using Forms;
using MetroFramework;
using ProductieManager.Forms;
using ProductieManager.Properties;
using ProductieManager.Rpm.Connection;
using ProductieManager.Rpm.Misc;
using ProductieManager.Rpm.Various;
using Rpm.Mailing;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.SqlLite;
using Rpm.Various;
using Various;

namespace Controls
{
    public partial class ProductieView : UserControl
    {
        public ProductieView()
        {
            InitializeComponent();
            metroTabControl.SelectedIndex = 0;
            _specialRoosterWatcher = new Timer();
            _specialRoosterWatcher.Interval = 60000; //1 minuut;
            _specialRoosterWatcher.Tick += (x, y) => CheckForSpecialRooster(false);
        }

        public event EventHandler OnFormLoaded;

        protected void FormLoaded()
        {
            OnFormLoaded?.Invoke(this, EventArgs.Empty);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DoPdfEnabled();
        }

        private void werkPlekkenUI1_WerkPlekClicked(object sender, EventArgs e)
        {
           // DoPdfEnabled();
        }

        #region Variables

        private readonly Timer _specialRoosterWatcher;
        public static Manager _manager;

        public int ProductieRefreshInterval { get; set; } = 10000; // 10 sec

        public bool ProductieSyncEnabled { get; set; }
        //private PersoneelsForm _persform;
        //private LogForm _logform;
        //private AlleStoringen _storingen;
        //private AlleVaardigheden _vaardigeheden;
        //private RangeCalculatorForm _rangeform;

        private static readonly List<StartProductie> _formuis = new();
        private static readonly List<ProductieLijstForm> _Productelijsten = new();
        private static Producties _producties;
        private static ProductieLijsten _productielijstdock;
        private static LogForm _logform;
        private static RangeCalculatorForm _calcform;
        private static ChatForm _chatform;
        private static OpmerkingenForm _opmerkingform;
        private static ArtikelsForm _ArtikelsForm;
        private static PersoneelsForm _PersoneelForm;
        private static ProductieOverzichtForm _ProductieOverzicht;
        private static WerkplaatsIndeling _WerkplaatsIndeling;
        public bool ShowUnreadMessage { get; set; }

        // [NonSerialized] private Opties _opties;

        //private string[] _groups = null;

        #endregion Variables

        #region Manager

        private async void InitManager(string path, bool autologin)
        {
            try
            {
                //if (_manager == null)
                _manager?.Dispose();
                if (_manager == null)
                    _manager = new Manager(false);
                DetachEvents();
                //BeginInvoke(new MethodInvoker(() => _manager.Load()));
                //BeginInvoke(new MethodInvoker(_manager.StartMonitor));
                //await _manager.Load(path, false, false, false);
                //CheckForUpdateDatabase();
                //takenManager1.InitManager();
                //xproductieListControl1.InitProductie(false, true, true, true, false, false);
                //xbewerkingListControl.InitProductie(true, true, true, true, false, false);
                //werkPlekkenUI1.InitUI(_manager);
                //recentGereedMeldingenUI1.LoadBewerkingen();
               // _manager.Dispose();
               // _manager = new Manager(false);
                _manager.InitManager();
                //xproductieListControl1.InitProductie(false, true, true, true, false, false);
                xbewerkingListControl.InitProductie(true, true, true, true, false, false);
                takenManager1.InitManager();
                werkPlekkenUI1.InitUI(_manager);
                InitEvents();
                await _manager.Load(path, autologin, true, true);
                if (Manager.Opmerkingen != null)
                    Manager.Opmerkingen.OnOpmerkingenChanged += Opmerkingen_OnOpmerkingenChanged;
                // _manager.StartMonitor();
                FormLoaded();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void LoadManager(string path, bool autologin = true)
        {
            InitManager(path, autologin);
        }

        public void InitEvents()
        {
            Manager.OnSettingsChanged += _manager_OnSettingsChanged;
            //Manager.OnProductiesLoaded += Manager_OnProductiesChanged;
            Manager.OnLoginChanged += _manager_OnLoginChanged;
            Manager.OnSettingsChanging += _manager_OnSettingsChanging;
            Manager.OnFormulierActie += Manager_OnFormulierActie;
            //Manager.DbUpdater.DbEntryUpdated += DbUpdater_DbEntryUpdated;
            //Manager.OnDbBeginUpdate += Manager_OnDbBeginUpdate;
            //Manager.OnDbEndUpdate += Manager_OnDbEndUpdate;
            Manager.OnManagerLoaded += _manager_OnManagerLoaded;
            Manager.FilterChanged += Manager_FilterChanged;
            ProductieChat.MessageRecieved += ProductieChat_MessageRecieved;
            ProductieChat.GebruikerUpdate += ProductieChat_GebruikerUpdate;
            MultipleFileDb.CorruptedFilesChanged += MultipleFileDb_CorruptedFilesChanged;

            _manager.OnShutdown += _manager_OnShutdown;
            //xproductieListControl1.InitEvents();
            xbewerkingListControl.InitEvents();
            //xproductieListControl1.ItemCountChanged += XproductieListControl1_ItemCountChanged;
            xbewerkingListControl.ItemCountChanged += XproductieListControl1_ItemCountChanged;
            recentGereedMeldingenUI1.ItemCountChanged += XproductieListControl1_ItemCountChanged;
            //xproductieListControl1.SelectedItemChanged += XproductieListControl1_SelectedItemChanged;
            xbewerkingListControl.SelectedItemChanged += XproductieListControl1_SelectedItemChanged;
            xbewerkingListControl.SelectedItemChanged += XproductieListControl1_SelectedItemChanged;
            werkPlekkenUI1.InitEvents();
            werkPlekkenUI1.OnRequestOpenWerk += WerkPlekkenUI1_OnRequestOpenWerk;
            werkPlekkenUI1.OnPlekkenChanged += WerkPlekkenUI1_OnPlekkenChanged;
        }

        private void MultipleFileDb_CorruptedFilesChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(UpdateCorruptedButtonImage));
                else UpdateCorruptedButtonImage();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void UpdateCorruptedButtonImage()
        {
            try
            {
                var xcorrupted = MultipleFileDb.CorruptedFilePaths;
                if (xcorrupted.Count > 0)
                {
                    var ximg = GraphicsExtensions.DrawUserCircle(new Size(32, 32), Brushes.White,
                        xcorrupted.Count.ToString(),
                        new Font("Ariel", 16, FontStyle.Bold), Color.DarkRed);
                    xcorruptedfilesbutton.Image = Resources.error_notification_32x32.CombineImage(ximg, 1.75);
                }
                else
                {
                    xcorruptedfilesbutton.Image = Resources.error_notification_32x32;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Opmerkingen_OnOpmerkingenChanged(object sender, EventArgs e)
        {
          UpdateUnreadOpmerkingen();
        }

        private void Manager_FilterChanged(object sender, EventArgs e)
        {
            try
            {
                BeginInvoke(new MethodInvoker(() => werkPlekkenUI1.LoadPlekken(true)));
                BeginInvoke(new MethodInvoker(recentGereedMeldingenUI1.LoadBewerkingen));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void XproductieListControl1_SelectedItemChanged(object sender, EventArgs e)
        {
            //DoPdfEnabled();
        }

        private void XproductieListControl1_ItemCountChanged(object sender, EventArgs e)
        {
            UpdateTabCounts();
        }

        private void ProductieChat_GebruikerUpdate(UserChat user)
        {
            UpdateUnreadMessages(user);
        }

        private void ProductieChat_MessageRecieved(ProductieChatEntry message)
        {
            UpdateUnreadMessages(message?.Afzender);
        }

        public void DetachEvents()
        {
            Manager.OnSettingsChanged -= _manager_OnSettingsChanged;
            Manager.OnFormulierActie -= Manager_OnFormulierActie;
            // Manager.OnProductiesLoaded -= Manager_OnProductiesChanged;
            //Manager.DbUpdater.DbEntryUpdated -= DbUpdater_DbEntryUpdated;
            Manager.OnLoginChanged -= _manager_OnLoginChanged;
            Manager.OnSettingsChanging -= _manager_OnSettingsChanging;
            Manager.FilterChanged -= Manager_FilterChanged;
            if (Manager.Opmerkingen != null)
                Manager.Opmerkingen.OnOpmerkingenChanged -= Opmerkingen_OnOpmerkingenChanged;
            ProductieChat.MessageRecieved -= ProductieChat_MessageRecieved;
            ProductieChat.GebruikerUpdate -= ProductieChat_GebruikerUpdate;
            MultipleFileDb.CorruptedFilesChanged -= MultipleFileDb_CorruptedFilesChanged;

            //Manager.OnDbBeginUpdate -= Manager_OnDbBeginUpdate;
            //Manager.OnDbEndUpdate -= Manager_OnDbEndUpdate;
            Manager.OnManagerLoaded -= _manager_OnManagerLoaded;
            _manager.OnShutdown -= _manager_OnShutdown;
            //xproductieListControl1.DetachEvents();
            xbewerkingListControl.DetachEvents();
            //productieListControl1.ItemCountChanged -= XproductieListControl1_ItemCountChanged;
            xbewerkingListControl.ItemCountChanged -= XproductieListControl1_ItemCountChanged;
            recentGereedMeldingenUI1.ItemCountChanged -= XproductieListControl1_ItemCountChanged;
            //xproductieListControl1.SelectedItemChanged -= XproductieListControl1_SelectedItemChanged;
            xbewerkingListControl.SelectedItemChanged -= XproductieListControl1_SelectedItemChanged;
            werkPlekkenUI1.DetachEvents();
            werkPlekkenUI1.OnRequestOpenWerk -= WerkPlekkenUI1_OnRequestOpenWerk;
            werkPlekkenUI1.OnPlekkenChanged -= WerkPlekkenUI1_OnPlekkenChanged;
        }

        private DialogResult _manager_OnShutdown(Manager instance, ref TimeSpan verlengtijd)
        {
            var af = new AfsluitPromp();
            var res = af.ShowDialog();
            if (res == DialogResult.OK) verlengtijd = af.VerlengTijd;
            return res;
        }

        private void _manager_OnSettingsChanging(object instance, ref UserSettings settings, ref bool cancel)
        {
            if (IsDisposed || Disposing) return;
            try
            {
                var xsettings = settings;
                if (InvokeRequired)
                    Invoke(new Action(() =>
                    {
                        try
                        {
                            //xsettings.ViewDataProductieState = xproductieListControl1.ProductieLijst.SaveState();
                            //xsettings.ViewDataBewerkingenState = xbewerkingListControl.ProductieLijst.SaveState();
                            xsettings.ViewDataWerkplekState = werkPlekkenUI1.xwerkpleklist.SaveState();
                            //xsettings.ViewDataRecentProductieState =
                            //    recentGereedMeldingenUI1.ProductieLijst.SaveState();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }));
                else
                    try
                    {
                        //xsettings.ViewDataProductieState = xproductieListControl1.ProductieLijst.SaveState();
                        //xsettings.ViewDataBewerkingenState = xbewerkingListControl.ProductieLijst.SaveState();
                        xsettings.ViewDataWerkplekState = werkPlekkenUI1.xwerkpleklist.SaveState();
                        //xsettings.ViewDataRecentProductieState = recentGereedMeldingenUI1.ProductieLijst.SaveState();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                settings = xsettings;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void _manager_OnSettingsChanged(object instance, UserSettings settings, bool init)
        {
            try
            {
                if (IsDisposed || Disposing) return;
                mainMenu1.OnSettingChanged(instance, settings, init);
                if (!init) return;
                BeginInvoke(new MethodInvoker(() =>
                {
                    if (IsDisposed || Disposing) return;
                    try
                    {
                        var name = Manager.Opties == null ? "Default" : Manager.Opties.Username;
                        Text = @$"ProductieManager [{name}]";


                        //if (Manager.Opties?._viewproddata != null)
                        //{
                        //    xproductieListControl1.ProductieLijst.RestoreState(Manager.Opties?.ViewDataProductieState);
                        //    xproductieListControl1.ProductieLijst.Columns.Remove(
                        //        xproductieListControl1.ProductieLijst.Columns["Naam"]);
                        //}

                        //if (Manager.Opties?._viewbewdata != null)
                        //    xbewerkingListControl.ProductieLijst.RestoreState(Manager.Opties.ViewDataBewerkingenState);
                        if (Manager.Opties?._viewwerkplekdata != null)
                            werkPlekkenUI1.xwerkpleklist.RestoreState(Manager.Opties.ViewDataWerkplekState);
                        //if (Manager.Opties?._viewrecentproddata != null)
                        //    recentGereedMeldingenUI1.ProductieLijst.RestoreState(Manager.Opties
                        //        .ViewDataRecentProductieState);
                        var xrooster = mainMenu1.GetButton("xroostermenubutton");
                        if (xrooster != null)
                            xrooster.Image = Manager.Opties?.TijdelijkeRooster == null
                                ? Resources.schedule_32_32
                                : Resources.schedule_32_32.CombineImage(Resources.exclamation_warning_15590, 1.75);

                        //LoadFilter();
                        _manager.SetSettings(settings);
                        werkPlekkenUI1.LoadPlekken(true);
                        CheckForSpecialRooster(false);
                        recentGereedMeldingenUI1.LoadBewerkingen();
                        this.Invalidate();
                        //Manager.Taken?.StartBeheer();
                        //if (Manager.IsLoaded)
                        //    CheckForSpecialRooster(true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public event LogInChangedHandler OnLoginChanged;

        private void _manager_OnLoginChanged(UserAccount user, object instance)
        {
            try
            {
                BeginInvoke(new Action(() =>
                {
                    if (IsDisposed || Disposing) return;
                    xloginb.Image = user != null ? Resources.Logout_37127__1_ : Resources.Login_37128__1_;
                    xcorruptedfilesbutton.Visible = user is {AccesLevel: AccesType.Manager};
                    OnLoginChanged?.Invoke(user, instance);
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void _manager_OnManagerLoaded()
        {
            if (IsDisposed || Disposing) return;
            Task.Factory.StartNew(new Action(() =>
            {
                try
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        //CheckForSyncDatabase();
                        //CheckForUpdateDatabase();
                        CheckForSpecialRooster(true);
                        LoadStartedProducties();
                        LoadProductieLogs();
                        //RunProductieRefresh();
                        CheckForPreview(false, true);
                        //UpdateAllLists();
                        _specialRoosterWatcher?.Start();
                    }));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }));
        }

        private void Manager_OnFormulierActie(object[] values, MainAktie type)
        {
            if (IsDisposed || Disposing) return;
            try
            {
                BeginInvoke(new Action(() =>
                {
                    var flag = values is {Length: > 0};
                    switch (type)
                    {
                        case MainAktie.OpenProductie:
                            if (flag)
                            {
                                var form =
                                    (ProductieFormulier) values.FirstOrDefault(x => x is ProductieFormulier);
                                var bew = (Bewerking) values.FirstOrDefault(x => x is Bewerking);
                                if (form != null)
                                    ShowProductieForm(form, true, bew);
                            }

                            break;
                        case MainAktie.OpenIndeling:
                            if (flag)
                            {
                                var form =
                                    (ProductieFormulier) values.FirstOrDefault(x => x is ProductieFormulier);
                                ShowWerkplekken(form);
                            }

                            break;
                        case MainAktie.OpenProductieWijziging:
                            if (flag)
                            {
                                var form =
                                    (ProductieFormulier) values.FirstOrDefault(x => x is ProductieFormulier);
                                ShowProductieSettings(form);
                            }

                            break;
                        case MainAktie.OpenInstellingen:
                            ShowOptieWidow();
                            break;
                        case MainAktie.OpenRangeSearcher:
                            ShowCalculatieWindow();
                            break;
                        case MainAktie.OpenPersoneel:
                            ShowPersoneelWindow();
                            break;
                        case MainAktie.OpenStoringen:
                            if (flag)
                            {
                                var bew = (Bewerking) values.FirstOrDefault(x => x is Bewerking);
                                if (bew != null)
                                    ShowBewStoringen(bew);
                            }

                            break;
                        case MainAktie.OpenAlleStoringen:
                            ShowOnderbrekeningenWidow();
                            break;
                        case MainAktie.OpenVaardigheden:
                            if (flag)
                            {
                                var per = (Personeel) values.FirstOrDefault(x => x is Personeel);
                                if (per != null)
                                    ShowPersoonVaardigheden(per);
                            }

                            break;
                        case MainAktie.OpenAlleVaardigheden:
                            ShowAlleVaardighedenWidow();
                            break;
                    }
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion Manager

        #region Main Menu Methods

        private void DoCreateExcel()
        {
            var f = new CreateExcelForm();
            f.ShowDialog();
        }

        private async void DoQuickProductie()
        {
            var xnewform = new NiewProductieForm();
            var result = xnewform.ShowDialog();
            if (result == DialogResult.Cancel) return;

            var created = xnewform.CreatedFormulier;
            if (created != null)
            {
                //await created.UpdateForm(true, false);
                if (result == DialogResult.Yes && created.Bewerkingen.Length > 0)
                    ProductieListControl.StartBewerkingen(created.Bewerkingen,this);
                else
                    await created.UpdateForm(true, false);
                SelectProductieItem(created);
            }
        }

        private async void DoOnderbreking()
        {
            var message =
                "Wat zou je willen doen?\n" +
                "Wil je een storing/onderbreking bekijken, toevoegen of wijzigen?";
            var bttns = new Dictionary<string, DialogResult>();
            bttns.Add("Annuleren", DialogResult.Cancel);
            bttns.Add("Wijzigen", DialogResult.No);
            bttns.Add("Toevoegen", DialogResult.Yes);
            bttns.Add("Bekijken", DialogResult.Ignore);


            var res = XMessageBox.Show(message, "Onderbreking", MessageBoxButtons.OK, MessageBoxIcon.Question, null,
                bttns);
            if (res == DialogResult.Cancel) return;
            var prods = await Manager.GetProducties(new[] {ViewState.Gestart, ViewState.Gestopt}, true, false,null);
            var plekken = new List<WerkPlek>();
            switch (res)
            {
                case DialogResult.Ignore:
                case DialogResult.Yes:

                    prods.ForEach(x =>
                    {
                        var xplekken = x.GetAlleWerkplekken();
                        if (res == DialogResult.Ignore)
                            xplekken = xplekken.Where(w => w.Storingen.Count > 0).ToList();
                        if (xplekken.Count > 0)
                            plekken.AddRange(xplekken);
                    });
                    if (plekken.Count == 0)
                    {
                        var xvalue = res == DialogResult.Ignore
                            ? "onderbrekeningen van te bekijken"
                            : "een onderbreking aan toe te voegen";
                        XMessageBox.Show($"Er zijn geen  aangemaakte werkplekken om {xvalue}.", "Geen Werkplekken",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    break;
                case DialogResult.No:
                    prods.ForEach(x =>
                    {
                        var xplekken = x.GetAlleWerkplekken();
                        if (xplekken.Count > 0)
                            foreach (var plek in xplekken)
                                if (plek.Storingen.Any(x => !x.IsVerholpen))
                                    plekken.Add(plek);
                    });
                    if (plekken.Count == 0)
                        XMessageBox.Show("Er zijn geen openstaande onderbrekeningen om te wijzigen.", "Onderbreking",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }

            if (plekken.Count > 0)
            {
                var selector = new WerkPlekChooser(plekken, null);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    var plek = selector.Selected;
                    if (plek == null) return;
                    NewStoringForm storingform = null;
                    switch (res)
                    {
                        case DialogResult.Yes:
                            storingform = new NewStoringForm(plek);
                            break;
                        case DialogResult.No:
                            var storingen = plek.Storingen.Where(x => !x.IsVerholpen).ToList();
                            if (storingen.Count == 0) return;
                            if (storingen.Count > 1)
                            {
                                var msg = $"Er zijn {storingen.Count} openstaande onderbrekeningen beschikbaar.\n" +
                                          "Kies een openstaande onderbreking om te wijzigen.";

                                var msgbox = new XMessageBox();
                                if (msgbox.ShowDialog(msg, "Kies Onderbreking", MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Information,
                                    storingen.Select(x => x.ToString()).ToArray()) == DialogResult.OK)
                                {
                                    var selected = msgbox.SelectedValue;
                                    if (selected != null)
                                    {
                                        var storing = storingen.FirstOrDefault(x => x.Equals(selected));
                                        if (storing != null) storingform = new NewStoringForm(plek, storing);
                                    }
                                }
                            }
                            else
                            {
                                storingform = new NewStoringForm(plek, storingen[0]);
                            }

                            break;
                    }

                    if (storingform != null)
                    {
                        if (storingform.ShowDialog() == DialogResult.OK)
                        {
                            plek.UpdateStoring(storingform.Onderbreking);
                            if (plek.Werk != null)
                            {
                                await plek.Werk?.UpdateBewerking(null,
                                    $"Onderbreking aangepast op {storingform.Onderbreking.Path}");
                                RemoteProductie.SendStoringMail(storingform.Onderbreking, plek.Werk);
                            }

                            var msg = $"Onderbreking {plek.Path}\n" +
                                      $"{storingform.Onderbreking.StoringType} is ";

                            switch (res)
                            {
                                case DialogResult.Yes:
                                    msg += "toegevoegd.";
                                    break;
                                case DialogResult.No:
                                    msg += "gewijzigd.";
                                    break;
                            }

                            XMessageBox.Show(msg, "Onderbreking", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (res == DialogResult.Ignore)
                    {
                        var sts = new StoringForm(plek);
                        sts.ShowDialog();
                    }
                }
            }
        }

        private void DoOpenProductiePdf()
        {
            switch (metroTabControl.SelectedIndex)
            {
                case 0: 
                    if (xbewerkingListControl.SelectedItem is Bewerking bew) bew.Parent?.OpenProductiePdf();
                    break;
                case 2: //werkplekken
                    var plek = werkPlekkenUI1.SelectedWerkplek;
                    plek?.Werk?.Parent?.OpenProductiePdf();
                    break;
            }
        }

        private void DoPdfEnabled()
        {
            mainMenu1.Enable("xbekijkproductiepdf", false);
            switch (metroTabControl.SelectedIndex)
            {
                case 0: //producties
                    if (xbewerkingListControl.SelectedItem is Bewerking bew)
                        mainMenu1.Enable("xbekijkproductiepdf",
                            bew.Parent != null && bew.Parent.ContainsProductiePdf());
                    break;
                case 2: //werkplekken
                    var plek = werkPlekkenUI1.SelectedWerkplek;
                    if (plek != null)
                        mainMenu1.Enable("xbekijkproductiepdf",
                            plek.Werk?.Parent != null && plek.Werk.Parent.ContainsProductiePdf());
                    break;
            }
        }

        private void SelectProductieItem(object item)
        {
            if (item is ProductieFormulier form)
            {
                //xproductieListControl1.SelectedItem = form;
                metroTabControl.SelectedIndex = 0;
            }
            else if (item is Bewerking bew)
            {
                xbewerkingListControl.SelectedItem = bew;
                metroTabControl.SelectedIndex = 1;
            }
            else if (item is WerkPlek plek)
            {
                werkPlekkenUI1.SelectedWerkplek = plek;
                metroTabControl.SelectedIndex = 2;
            }
        }

        private async void DoEigenRooster()
        {
            try
            {
                if (Manager.Opties == null)
                    throw new Exception(
                        "Opties zijn niet geladen en kan daarvoor geen rooster aanpassen.\n\nRaadpleeg Ihab a.u.b.");
                //if (Manager.Opties.TijdelijkeRooster != null)
                //{
                //    var xvalue = "\nMet een bereik:\n";
                //    if (Manager.Opties.TijdelijkeRooster.GebruiktVanaf)
                //        xvalue += $"Vanaf {Manager.Opties.TijdelijkeRooster.Vanaf} ";
                //    if (Manager.Opties.TijdelijkeRooster.GebruiktTot)
                //        xvalue += $"Tot {Manager.Opties.TijdelijkeRooster.Tot}\n";
                //    if (Manager.Opties.TijdelijkeRooster.GebruiktVanaf || Manager.Opties.TijdelijkeRooster.GebruiktTot)
                //        xrstr += xvalue;
                //}

                //var message = "Wat voor rooster zou je willen gebruiken voor alle producties?\n\n" +
                //              $"Momenteel gebruik je {xrstr}.";
                //var dialog = XMessageBox.Show(message, "Eigen Rooster",
                //    MessageBoxButtons.OK, MessageBoxIcon.Question, null, bttns);
               // if (dialog == DialogResult.Cancel) return;
                var xold = Manager.Opties?.GetWerkRooster() ?? Rooster.StandaartRooster();
                var roosterform = new RoosterForm(Manager.Opties.TijdelijkeRooster,
                           "Kies een rooster voor al je werkzaamheden");
                roosterform.ViewPeriode = false;
                if (roosterform.ShowDialog() == DialogResult.Cancel)
                    return;
                Manager.Opties.TijdelijkeRooster = roosterform.WerkRooster;
                var thesame = xold.SameTijden(Manager.Opties?.GetWerkRooster());
                if (!thesame)
                {
                    var bws = await Manager.GetBewerkingen(new ViewState[] {ViewState.Gestart}, true);

                    bws = bws.Where(x => string.Equals(Manager.Opties.Username, x.GestartDoor,
                        StringComparison.CurrentCultureIgnoreCase)).ToList();

                    if (bws.Count > 0)
                    {
                        var bwselector = new BewerkingSelectorForm(bws);
                        bwselector.Title = "Selecteer Werkplekken waarvan de rooster aangepast moet worden";
                        if (bwselector.ShowDialog() == DialogResult.OK)
                            await Manager.UpdateGestarteProductieRoosters(bwselector.SelectedWerkplekken, roosterform.WerkRooster);
                    }

                }
                var xrooster = mainMenu1.GetButton("xroostermenubutton");
                var iscustom = Manager.Opties.TijdelijkeRooster != null && Manager.Opties.TijdelijkeRooster.IsCustom();
                if (xrooster != null)
                    xrooster.Image = iscustom
                        ? Resources.schedule_32_32.CombineImage(Resources.exclamation_warning_15590, 1.75)
                        : Resources.schedule_32_32;
                var xchange = iscustom ? "aangepaste rooster" : "standaard rooster";
                await Manager.Opties.Save($"{Manager.Opties.Username} heeft een {xchange}.");
            }
            catch (Exception e)
            {
                XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private async void DoOpenStartedProducties()
        {
            try
            {
                var startedprods =
                    (await Manager.Database.GetAllProducties(false, true, null))
                    .Where(x => x.State == ProductieState.Gestart)
                    .ToArray();
                if (startedprods.Length == 0)
                {
                    XMessageBox.Show("Er zijn geen gestarte producties om te openen.", "Geen Producties",
                        MessageBoxIcon.Exclamation);
                }
                else
                {
                    var xvalue0 = startedprods.Length == 1 ? "is" : "zijn";
                    var xvalue1 = startedprods.Length == 1 ? "productie" : "producties";
                    if (XMessageBox.Show(
                        $"Er {xvalue0} {startedprods.Length} gestarte {xvalue1}.\n\nWil je ze allemaal openen?",
                        "Open Producties", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        for (var i = 0; i < startedprods.Length; i++)
                        {
                            var prod = startedprods[i];
                            var bws = prod.Bewerkingen?.Where(x => x.IsAllowed()).ToArray();
                            if (bws?.Length > 0)
                            {
                                var bw = bws.FirstOrDefault(x => x.State == ProductieState.Gestart);
                                if (bw != null)
                                    ShowProductieForm(prod, true, bw);
                            }
                        }
                }
            }
            catch (Exception e)
            {
                XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void DoUpdateFormsFromDirectory()
        {
            if (Manager.LogedInGebruiker == null ||
                Manager.LogedInGebruiker.AccesLevel <= AccesType.ProductieBasis) return;
            var ofd = new FolderBrowserDialog()
            {
                Description = "Kies een folder met productieFormulieren pdf's"

            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var xupdater = new UpdateProducties();
                xupdater.CloseWhenFinished = false;
                xupdater.ShowStop = true;
                xupdater.StartWhenShown = true;
                xupdater.UpdateMethod = ()=> Manager.UpdateFormulierenFromDirectory(ofd.SelectedPath, false,xupdater.ProgressChanged);
                xupdater.ShowDialog();
            }
        }

        private static async void DoLoadDbInstance()
        {
            try
            {
                var msgbox = new XMessageBox();
                var msg = "Kies een database dat je zou willen laden";
                var bttns = new Dictionary<string, DialogResult>();
                bttns.Add("Annuleren", DialogResult.Cancel);
                bttns.Add("Laad Database", DialogResult.OK);
                bttns.Add("Kies Folder", DialogResult.Yes);
                var dbs = Manager.DefaultSettings?.DbUpdateEntries ??
                          UserSettings.GetDefaultSettings()?.DbUpdateEntries ?? new List<DatabaseUpdateEntry>();
                var dbnames = dbs.Select(x => x.Naam).ToList();
                dbnames.Insert(0, "Eigen Database");
                var result = msgbox.ShowDialog(msg, "Laad Database", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Information, dbnames.ToArray(), bttns);
                if (result == DialogResult.Cancel) return;
                string path = null;
                string name = null;
                if (result == DialogResult.OK)
                {
                    if (msgbox.SelectedValue != null && msgbox.SelectedValue.ToLower() == "eigen database")
                    {
                        var stng = Manager.DefaultSettings ?? UserSettings.GetDefaultSettings();
                        if (Directory.Exists(stng.MainDB.RootPath))
                            path = stng.MainDB.RootPath;
                        else path = Application.StartupPath;
                    }
                    else
                    {
                        var ent = dbs.FirstOrDefault(x =>
                            string.Equals(x.Naam, msgbox.SelectedValue, StringComparison.CurrentCultureIgnoreCase));
                        path = ent?.RootPath;
                        name = ent?.Naam;
                    }
                }

                if (path == null)
                {
                    var fb = new FolderBrowserDialog {Description = "Kies een database locatie"};
                    if (fb.ShowDialog() == DialogResult.OK)
                        path = fb.SelectedPath;
                }

                if (path != null)
                {
                    if (!Directory.Exists(path))
                    {
                        XMessageBox.Show($"'{path}' bestaat niet, of is niet toegankelijk!", "Fout",
                            MessageBoxIcon.Error);
                        return;
                    }

                    //if (name != null)
                    //{
                    //    var opties = await Manager.Database.GetSetting(name);
                    //    if (opties != null)
                    //        Manager.Opties = opties;
                    //}
                    await _manager.Load(path, true, true, true);
                }
            }
            catch (Exception e)
            {
                XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void mainMenu1_OnMenuClick(object sender, EventArgs e)
        {
            if (sender is Button {Tag: MenuButton menubutton})
                try
                {
                    switch (menubutton.Name.ToLower())
                    {
                        case "xniewproductie":
                            var AddProduction = new WijzigProductie();
                            if (AddProduction.ShowDialog() == DialogResult.OK)
                                BeginInvoke(new MethodInvoker(async () =>
                                {
                                    var form = AddProduction.Formulier;
                                    var msg = await Manager.AddProductie(form,false);
                                    Manager.RemoteMessage(msg);
                                }));

                            break;
                        case "xopenproductie":

                            var ofd = new OpenFileDialog
                            {
                                Title = @"Open Productie Formulier(en)",
                                Filter = @"Pdf|*.pdf|Image|*.JPG;*.Img|Text|*.txt|Alles|*.*",
                                Multiselect = true
                            };
                            if (ofd.ShowDialog() == DialogResult.OK)
                                BeginInvoke(new MethodInvoker(async () =>
                                {
                                    var files = ofd.FileNames;
                                    await _manager.AddProductie(files,true, false, true);
                                }));
                            break;
                        case "xquickproductie":
                            DoQuickProductie();
                            break;
                        case "xsearchtekening":
                            ZoekWerkTekening();
                            break;
                        case "xwerkplaatsindeling":
                            ShowWerkplaatsIndelingWindow();
                            break;
                        case "xcreateexcel":
                            //maak een nieuwe excel aan
                            DoCreateExcel();
                            break;
                        case "xupdatedb":
                            var updater = new DbUpdater();
                            updater.ShowDialog();
                            break;
                        case "xlaaddb":
                            DoLoadDbInstance();
                            break;
                        case "xupdateforms":
                            DoUpdateFormsFromDirectory();
                            break;
                        case "xstats":
                            var chartform = new ViewChartForm();
                            chartform.ShowDialog();
                            break;
                        case "xstoringmenubutton": //storing verwerken
                            DoOnderbreking();
                            break;
                        case "xbekijkproductiepdf":
                            DoOpenProductiePdf();
                            break;
                        case "xroostermenubutton":
                            SetSpecialeRooster();
                            break;
                        case "xspecialeroosterbutton":
                          BeheerSpecialeRoosters();
                            break;
                        case "xopenproducties":
                            DoOpenStartedProducties();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    XMessageBox.Show(ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        #endregion Main Menu Methods

        #region Control Events

        private void xUpdate_Click(object sender, EventArgs e)
        {
            AutoUpdater.Start(false);
        }

        private void xsettingsb_Click(object sender, EventArgs e)
        {
            ShowOptieWidow();
        }

        private void xloginb_Click(object sender, EventArgs e)
        {
            if (Manager.LogedInGebruiker != null)
            {
                Manager.LogOut(this);
            }
            else
            {
                var xlogin = new LogIn();
                xlogin.StartPosition = FormStartPosition.CenterParent;
                xlogin.ShowDialog();
            }
        }

        private void xupdateallform_Click(object sender, EventArgs e)
        {
            var prod = new UpdateProducties();
            prod.ShowDialog();
        }

        private void xmateriaalverbruikb_Click(object sender, EventArgs e)
        {
            var xmats = new MateriaalVerbruikForm();
            xmats.ShowDialog();
        }

        private void xsendemail_Click(object sender, EventArgs e)
        {
            var emailform = new SendEmailForm();
            emailform.ShowDialog();
        }

        private void xallenotities_Click(object sender, EventArgs e)
        {
            var noteform = new AlleNotitiesForm();
            noteform.ShowDialog();
        }

        private void xchatformbutton_Click(object sender, EventArgs e)
        {
            ShowChatWindow();
        }

        private void xpersoneelb_Click(object sender, EventArgs e)
        {
            ShowPersoneelWindow();
        }

        private void xallstoringenb_Click(object sender, EventArgs e)
        {
            ShowOnderbrekeningenWidow();
        }

        private void xtoonlogsb_Click(object sender, EventArgs e)
        {
            ShowProductieLogWindow();
        }

        private void xallevaardighedenb_Click(object sender, EventArgs e)
        {
            ShowAlleVaardighedenWidow();
        }

        private void xprodinfob_Click(object sender, EventArgs e)
        {
            ShowCalculatieWindow();
        }

        private void xspeciaalroosterbutton_Click(object sender, EventArgs e)
        {
            SetSpecialeRooster();
        }

        private void xdbbewerkingen_Click(object sender, EventArgs e)
        {
            ShowBewerkingDb();
        }


        private void WerkPlekkenUI1_OnPlekkenChanged(object sender, EventArgs e)
        {
            if (sender is ObjectListView olv)
                tabPage3.Text = olv.Items.Count == 0
                    ? "Geen Actieve Werkplekken"
                    : $"Actieve Werkplekken [{olv.Items.Count}]";
        }

        private void WerkPlekkenUI1_OnRequestOpenWerk(object sender, EventArgs e)
        {
            if (!(sender is Bewerking b)) return;
            if (b.Parent != null) ShowProductieForm(b.Parent, true, b);
        }

        private void xaboutb_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }

        //private readonly Timer _prodsearch = new() {Interval = 250};
        //private readonly Timer _bwssearch = new() {Interval = 250};

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.valksolarsystems.com/nl");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.valksystemen.nl/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.valksystemen.nl/");
        }

        #endregion Control Events

        #region Methods

        public Task RunProductieRefresh()
        {
            return Task.Run(async () =>
            {
                try
                {
                    while (ProductieSyncEnabled && !IsDisposed && !Disposing)
                    {
                        await Task.Delay(ProductieRefreshInterval);
                        if (!ProductieSyncEnabled || IsDisposed || Disposing) break;
                        BeginInvoke(new MethodInvoker(xUpdateList));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }

        private void xUpdateList()
        {
            try
            {
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void CheckForSpecialRooster(bool prompchange)
        {
            if (Manager.Opties == null || Manager.LogedInGebruiker == null ||
                Manager.LogedInGebruiker.AccesLevel == AccesType.AlleenKijken)
            {
                xspeciaalroosterlabel.Visible = false;
                return;
            }
           
            var xtime = DateTime.Now;
            //eerst kijken of het weekend is.
            var culture = new CultureInfo("nl-NL");
            var day = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
            if (xtime.DayOfWeek == DayOfWeek.Saturday || xtime.DayOfWeek == DayOfWeek.Sunday)
            {
                //het is weekend, dus we zullen moeten kijken of er wel gewerkt wordt.
                var xbttntxt =
                    $"Het is {day} {xtime.TimeOfDay:hh\\:mm} uur. Vandaag is geen officiële werkdag. Click hier voor de speciale rooster";
                xspeciaalroosterbutton.Text = xbttntxt;
                var rooster = Manager.Opties.SpecialeRoosters.FirstOrDefault(x => x.Vanaf.Date == xtime.Date);
                if (rooster == null && prompchange)
                {
                    var splash = Application.OpenForms["SplashScreen"];
                    splash?.Close();
                    var xmsg =
                        $"Het is vandaag {day}, en geen officiële werkdag.\n" +
                        "Je kan een speciale rooster toevoegen als er vandaag toch wordt gewerkt.\n\n" +
                        "Wil je een rooster nu toevoegen?\nSpeciale roosters kan je achteraf ook aanpassen in de instellingen";
                    if (XMessageBox.Show(xmsg, "Speciaal Rooster", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        SetSpecialeRooster();
                }

                xspeciaalroosterlabel.Visible = true;
            }
            else
            {
                var rooster = Manager.Opties.WerkRooster;
                if (xtime.TimeOfDay < rooster.StartWerkdag || xtime.TimeOfDay > rooster.EindWerkdag)
                {
                    var xbttntxt =
                        $"Het is nu {day} {xtime.TimeOfDay:hh\\:mm} uur, dat is buiten om de gewone werkrooster van {rooster.StartWerkdag:hh\\:mm} tot {rooster.EindWerkdag:hh\\:mm} uur. Click hier voor de aangepaste rooster";
                    xspeciaalroosterbutton.Text = xbttntxt;
                    xspeciaalroosterlabel.Visible = true;
                }
                else
                {
                    xspeciaalroosterlabel.Visible = false;
                }
            }

            if (_specialRoosterWatcher != null)
            {
                xtime = DateTime.Now;
                _specialRoosterWatcher.Interval = (60 - xtime.Second) * 1000 - xtime.Millisecond;
            }
        }

        private async void SetSpecialeRooster()
        {
            if (Manager.Opties == null) return;
            var xtime = DateTime.Now;
            //eerst kijken of het weekend is.
            var culture = new CultureInfo("nl-NL");
            var day = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
            if (xtime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                var rooster = Manager.Opties?.SpecialeRoosters?.FirstOrDefault(x => x.Vanaf.Date == DateTime.Now.Date);
                var xreturn = rooster != null;
                if (rooster == null)
                {
                    rooster = Rooster.StandaartRooster();
                    rooster.Vanaf = DateTime.Now;
                    rooster.GebruiktPauze = false;
                    rooster.StartWerkdag = new TimeSpan(7, 0, 0);
                    rooster.EindWerkdag = new TimeSpan(12, 0, 0);
                }

                var roosterform = new RoosterForm(rooster, "Vul in de speciale werkdag tijden");
                roosterform.ViewPeriode = false;
                if (roosterform.ShowDialog() == DialogResult.OK)
                {
                    if (Manager.Opties.SpecialeRoosters == null)
                        Manager.Opties.SpecialeRoosters = new List<Rooster>();
                    var newrooster = roosterform.WerkRooster;
                    var dt = DateTime.Now;
                    var tijd = roosterform.WerkRooster.StartWerkdag;
                    newrooster.Vanaf = new DateTime(dt.Year, dt.Month, dt.Day, tijd.Hours, tijd.Minutes, 0);
                    Manager.Opties.SpecialeRoosters.Remove(rooster);
                    Manager.Opties.SpecialeRoosters.Add(newrooster);
                    Manager.Opties.SpecialeRoosters = Manager.Opties.SpecialeRoosters.OrderBy(x => x.Vanaf).ToList();

                    var bws = await Manager.GetBewerkingen(new ViewState[] { ViewState.Gestart }, true);
                    bws = bws.Where(x => string.Equals(Manager.Opties.Username, x.GestartDoor,
                        StringComparison.CurrentCultureIgnoreCase)).ToList();
                    if (bws.Count > 0)
                    {
                        var bwselector = new BewerkingSelectorForm(bws);
                        bwselector.Title = "Selecteer Werkplekken waarvan de rooster aangepast moet worden";
                        if (bwselector.ShowDialog() == DialogResult.OK)
                            await Manager.UpdateGestarteProductieRoosters(bwselector.SelectedWerkplekken, null);
                    }
                    await Manager.Opties.Save("Speciale roosters aangepast.");
                }
            }
            else
            {
                //var rooster = Manager.Opties.WerkRooster;
                //if (xtime.TimeOfDay < rooster.StartWerkdag || xtime.TimeOfDay > rooster.EindWerkdag)
                //{

                //}
                DoEigenRooster();
            }
        }

        private async void BeheerSpecialeRoosters()
        {
            var sproosters = new SpeciaalWerkRoostersForm(Manager.Opties.SpecialeRoosters);
            if (sproosters.ShowDialog() == DialogResult.OK)
            {
               
                var acces1 = Manager.LogedInGebruiker is { AccesLevel: >= AccesType.ProductieBasis };
                if (acces1)
                {
                    Manager.Opties.SpecialeRoosters = sproosters.Roosters;
                }
                if (acces1 && sproosters.Roosters.Count > 0)
                {
                    var bws = await Manager.GetBewerkingen(new ViewState[] { ViewState.Gestart }, true);
                    bws = bws.Where(x => string.Equals(Manager.Opties.Username, x.GestartDoor,
                        StringComparison.CurrentCultureIgnoreCase)).ToList();
                    if (bws.Count > 0)
                    {
                        var bwselector = new BewerkingSelectorForm(bws);
                        bwselector.Title = "Selecteer Werkplekken waarvan de rooster aangepast moet worden";
                        if (bwselector.ShowDialog() == DialogResult.OK)
                            await Manager.UpdateGestarteProductieRoosters(bwselector.SelectedWerkplekken, null);
                    }
                }

                if (acces1)
                    _ = Manager.Opties.Save();
            }
        }

        private void CheckForSyncDatabase()
        {
            var opties = Manager.DefaultSettings ?? UserSettings.GetDefaultSettings();
            if (opties?.TempMainDB != null && opties.MainDB != null &&
                opties.TempMainDB.LastUpdated > opties.MainDB.LastUpdated && Directory.Exists(opties.MainDB.UpdatePath))
            {
                var splash = (SplashScreen) Application.OpenForms["SplashScreen"];
                if (splash != null)
                {
                    while (splash.Visible && !splash.CanClose)
                        Application.DoEvents();
                    splash.Close();
                }

                opties.TempMainDB.LastUpdated = opties.MainDB.LastUpdated;
                var prod = new UpdateProducties(opties.TempMainDB)
                    {CloseWhenFinished = true, ShowStop = false, StartWhenShown = true};
                prod.ShowDialog();
                if (prod.IsFinished)
                {
                    opties.MainDB.LastUpdated = DateTime.Now;
                    opties.SaveAsDefault();
                }
            }
        }

        public static void CheckForUpdateDatabase()
        {
            var opties = Manager.DefaultSettings ?? UserSettings.GetDefaultSettings();
            if (opties != null && new Version(LocalDatabase.DbVersion) > new Version(opties.UpdateDatabaseVersion))
            {
                var splash = Application.OpenForms["SplashScreen"];
                splash?.Close();

                var prod = new UpdateProducties {CloseWhenFinished = true, ShowStop = false, StartWhenShown = true};
                if (prod.ShowDialog() == DialogResult.OK)
                {
                    opties.UpdateDatabaseVersion = LocalDatabase.DbVersion;
                    opties.SaveAsDefault();
                }
            }
        }

        private async void LoadStartedProducties()
        {
            if (_manager == null || Manager.Opties == null || !Manager.Opties.ToonAlleGestartProducties)
                return;
            if (Manager.LogedInGebruiker is {AccesLevel: >= AccesType.ProductieBasis})
            {
                var prs = await Manager.GetAllProductieIDs(false, true);
                foreach (var v in prs)
                {
                    var prod = await Manager.Database.GetProductie(v);
                    if (prod?.Bewerkingen == null || prod.Bewerkingen.Length == 0) continue;
                    var xs = prod.Bewerkingen.FirstOrDefault(x => x.State == ProductieState.Gestart);
                    if (xs == null) continue;
                    ShowProductieForm(prod, true, xs);
                }
            }
        }

        private void LoadProductieLogs()
        {
            if (_manager == null || Manager.Opties == null || !Manager.Opties.ToonProductieLogs)
                return;
            ShowProductieLogWindow();
        }

        private void UpdateTabCounts()
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                //tabPage1.Text = $"Producties [{xproductieListControl1.ProductieLijst.Items.Count}]";
                tabPage2.Text = $"Bewerkingen [{xbewerkingListControl.ProductieLijst.Items.Count}]";
                tabPage4.Text = $"Recente Gereedmeldingen [{recentGereedMeldingenUI1.ItemCount}]";
            }));
        }

        private XMessageBox _unreadMessages;

        public void UpdateUnreadMessages(UserChat user)
        {
            if (this.InvokeRequired)
                BeginInvoke(new Action(xUpdateUnreadMessages));
            else xUpdateUnreadMessages();
        }

        private void xUpdateUnreadMessages()
        {
            try
            {
                if (ProductieChat.Chat == null) return;
                var unread = ProductieChat.Chat.GetAllUnreadMessages();
                if (unread.Count > 0)
                {
                    var ximg = GraphicsExtensions.DrawUserCircle(new Size(32, 32), Brushes.White,
                        unread.Count.ToString(),
                        new Font("Ariel", 16, FontStyle.Bold), Color.DarkRed);
                    xchatformbutton.Image = Resources.conversation_chat_32x321.CombineImage(ximg, 1.75);
                }
                else
                {
                    xchatformbutton.Image = Resources.conversation_chat_32x321;
                }

                if (_chatform != null)
                {
                    if (_chatform.WindowState == FormWindowState.Minimized)
                        _chatform.WindowState = FormWindowState.Normal;
                    //if (user != null)
                    //    _chatform.SelectedUser(user);
                    _chatform.Show();
                    _chatform.BringToFront();
                    _chatform.Focus();
                }
                else if (unread.Count > 0 && ShowUnreadMessage)
                {
                    _unreadMessages?.Dispose();
                    var names = new List<string>();
                    foreach (var msg in unread.Where(msg => msg.Afzender != null && !names.Any(x =>
                        string.Equals(x, msg.Afzender.UserName, StringComparison.CurrentCultureIgnoreCase))))
                        names.Add(msg.Afzender.UserName);

                    if (names.Count == 0) return;
                    {
                        Application.OpenForms["SplashScreen"]?.Close();
                        var xv = names.Count == 1 ? "bericht" : "berichten";
                        var bttns = new Dictionary<string, DialogResult>();
                        bttns.Add("OK", DialogResult.OK);
                        bttns.Add("Toon Bericht", DialogResult.Yes);
                        _unreadMessages = new XMessageBox();
                        var result = _unreadMessages.ShowDialog(
                            $"Je hebt {unread.Count} ongelezen {xv} van {string.Join(", ", names)}",
                            $"{unread.Count} ongelezen berichten", MessageBoxButtons.OK, MessageBoxIcon.None, null,
                            bttns);
                        _unreadMessages?.Dispose();
                        _unreadMessages = null;
                        if (result == DialogResult.Yes)
                            ShowChatWindow(names[0]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UpdateUnreadOpmerkingen()
        {
            if (this.InvokeRequired)
                BeginInvoke(new Action(xUpdateUnreadOpmerkingen));
            else xUpdateUnreadOpmerkingen();
        }

        public void xUpdateUnreadOpmerkingen()
        {
            try
            {
                if (Manager.Opmerkingen?.OpmerkingenLijst == null) return;
                var unread = Manager.Opmerkingen.GetUnreadNotes();
                if (unread.Count > 0)
                {
                    var ximg = GraphicsExtensions.DrawUserCircle(new Size(32, 32), Brushes.White,
                        unread.Count.ToString(),
                        new Font("Ariel", 16, FontStyle.Bold), Color.DarkRed);
                    xopmerkingentoolstripbutton.Image = Resources.notes_office_page_papers_32x32.CombineImage(ximg, 1.75);
                }
                else
                {
                    xopmerkingentoolstripbutton.Image = Resources.notes_office_page_papers_32x32;
                }

                if (_opmerkingform != null)
                {
                    if (_opmerkingform.WindowState == FormWindowState.Minimized)
                        _opmerkingform.WindowState = FormWindowState.Normal;
                    //if (user != null)
                    //    _chatform.SelectedUser(user);
                    _opmerkingform.Show();
                    _opmerkingform.BringToFront();
                    _opmerkingform.Focus();
                }
                else if (unread.Count > 0 && ShowUnreadMessage)
                {
                    Application.OpenForms["SplashScreen"]?.Close();
                    var xv = unread.Count == 1 ? "opmerking" : "opmerkingen";
                    var bttns = new Dictionary<string, DialogResult>();
                    bttns.Add("OK", DialogResult.OK);
                    bttns.Add("Toon Opmerkingen", DialogResult.Yes);
                    var xmsgBox = new XMessageBox();
                    var result = xmsgBox.ShowDialog(
                        $"Je hebt {unread.Count} ongelezen {xv}",
                        $"{unread.Count} ongelezen {xv}", MessageBoxButtons.OK, MessageBoxIcon.None, null,
                        bttns);
                    xmsgBox.Dispose();
                    if (result == DialogResult.Yes)
                        ShowOpmerkingWindow();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion Methods

        #region MenuButton Methods

        public static StartProductie ShowProductieForm(ProductieFormulier pform, bool showform,
            Bewerking bewerking = null)
        {
            if (pform == null || Manager.LogedInGebruiker == null ||
                Manager.LogedInGebruiker.AccesLevel < AccesType.ProductieBasis)
                return null;
            try
            {
                var bws = pform.Bewerkingen.Where(x =>
                    x.IsAllowed() && x.State != ProductieState.Verwijderd).ToList();
                if (bws.Count == 0)
                    throw new Exception(
                        $"Kan '{pform.Omschrijving}' niet openen omdat er geen geldige bewerkingen zijn!\n\n" +
                        "Bewerkingen zijn verwijderd of gefiltered.");
                var productie = _formuis.FirstOrDefault(t => t.Name == pform.ProductieNr.Trim().Replace(" ", ""));
                if (productie is {IsDisposed: false} && _producties != null && showform)
                {
                    productie.Show(_producties.DockPanel);
                }
                else
                {
                    if (productie != null)
                        _formuis.Remove(productie);
                    //productie.Dispose();

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
                        _producties = new Producties
                        {
                            Tag = productie,
                            StartPosition = FormStartPosition.CenterScreen
                        };
                        _producties.FormClosed += (x, y) => _producties = null;
                    }

                    if (showform)
                        productie.Show(_producties.DockPanel);
                }

                if (!_producties.Visible && showform)
                    _producties.Show();
                if (_producties.WindowState == FormWindowState.Minimized)
                    _producties.WindowState = FormWindowState.Normal;

                _producties.Focus();
                return productie;
            }
            catch (Exception e)
            {
                XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
                return null;
            }
        }

        public static ProductieLijstForm ShowProductieLijstForm()
        {
            if (Manager.LogedInGebruiker == null || Manager.LogedInGebruiker.AccesLevel < AccesType.ProductieBasis)
                return null;
            try
            {
                var prodform = new ProductieLijstForm(_Productelijsten.Count);

                prodform.FormClosing += AddProduction_FormClosing;
                _Productelijsten.Add(prodform);
                if (_productielijstdock == null || _productielijstdock.IsDisposed)
                {
                    _productielijstdock = new ProductieLijsten
                    {
                        Tag = prodform,
                        StartPosition = FormStartPosition.CenterScreen
                    };
                    _productielijstdock.FormClosed += (x, y) => _producties = null;
                }

                prodform.Show(_productielijstdock.DockPanel);

                if (!_productielijstdock.Visible)
                    _productielijstdock.Show();
                if (_productielijstdock.WindowState == FormWindowState.Minimized)
                    _productielijstdock.WindowState = FormWindowState.Normal;

                _productielijstdock.Focus();
                return prodform;
            }
            catch (Exception e)
            {
                XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
                return null;
            }
        }

        public static void AddProduction_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sender is StartProductie prod)
                _formuis.Remove(prod);
            else if (sender is ProductieLijstForm form)
                _Productelijsten.Remove(form);
        }

        public static void ShowProductieLogWindow()
        {
            if (_logform == null)
            {
                _logform = new LogForm();
                _logform.FormClosed += (x, y) =>
                {
                    _logform?.Dispose();
                    _logform = null;
                };
            }

            _logform.Show();
            if (_logform.WindowState == FormWindowState.Minimized)
                _logform.WindowState = FormWindowState.Normal;
            _logform.BringToFront();
            _logform.Focus();
        }

        public static void ShowCalculatieWindow()
        {
            //var _calcform = new RangeCalculatorForm();
            //_calcform.ShowDialog();
            if (_calcform == null)
            {
                _calcform = new RangeCalculatorForm();
                _calcform.FormClosed += (x, y) =>
                {
                    _calcform?.Dispose();
                    _calcform = null;
                };
            }
            _calcform.Show();
            if (_calcform.WindowState == FormWindowState.Minimized)
                _calcform.WindowState = FormWindowState.Normal;
            _calcform.BringToFront();
            _calcform.Focus();
        }

        public static void ShowChatWindow(string username = null)
        {
            if (_chatform == null)
            {
                _chatform = new ChatForm();
                _chatform.FormClosed += (x, y) =>
                {
                    _chatform?.Dispose();
                    _chatform = null;
                };
            }

            _chatform.Show(username);
            if (_chatform.WindowState == FormWindowState.Minimized)
                _chatform.WindowState = FormWindowState.Normal;
            _chatform.BringToFront();
            _chatform.Focus();
        }

        public static void ShowOpmerkingWindow()
        {
            if (_opmerkingform == null)
            {
                _opmerkingform = new OpmerkingenForm();
                _opmerkingform.LoadOpmerkingen();
                _opmerkingform.FormClosed += (x, y) =>
                {
                    _opmerkingform?.Dispose();
                    _opmerkingform = null;
                };
            }

            _opmerkingform.Show();
            if (_opmerkingform.WindowState == FormWindowState.Minimized)
                _opmerkingform.WindowState = FormWindowState.Normal;
            _opmerkingform.BringToFront();
            _opmerkingform.Focus();
        }

        public static void ShowArtikelenWindow()
        {
            if (_ArtikelsForm == null)
            {
                _ArtikelsForm = new ArtikelsForm();
                _ArtikelsForm.FormClosed += (x, y) =>
                {
                    _ArtikelsForm?.Dispose();
                    _ArtikelsForm = null;
                };
            }

            _ArtikelsForm.Show();
            if (_ArtikelsForm.WindowState == FormWindowState.Minimized)
                _ArtikelsForm.WindowState = FormWindowState.Normal;
            _ArtikelsForm.BringToFront();
            _ArtikelsForm.Focus();
        }

        public static void ShowOnderbrekeningenWidow()
        {
            var x = new AlleStoringen();
            x.InitStoringen();
            x.ShowDialog();
        }

        public static void ShowAlleVaardighedenWidow()
        {
            new AlleVaardigheden().ShowDialog();
        }

        public static void ShowPersoneelWindow()
        {
            if (_PersoneelForm == null)
            {
                _PersoneelForm = new PersoneelsForm(false);
                _PersoneelForm.FormClosed += (x, y) =>
                {
                    _PersoneelForm?.Dispose();
                    _PersoneelForm = null;
                };
            }

            _PersoneelForm.Show();
            if (_PersoneelForm.WindowState == FormWindowState.Minimized)
                _PersoneelForm.WindowState = FormWindowState.Normal;
            _PersoneelForm.BringToFront();
            _PersoneelForm.Focus();
        }

        public static void ShowWerkplaatsIndelingWindow()
        {
            if (_WerkplaatsIndeling == null)
            {
                _WerkplaatsIndeling = new WerkplaatsIndeling();
                _WerkplaatsIndeling.FormClosed += (x, y) =>
                {
                    _WerkplaatsIndeling?.Dispose();
                    _WerkplaatsIndeling = null;
                };
            }

            _WerkplaatsIndeling.Show();
            if (_WerkplaatsIndeling.WindowState == FormWindowState.Minimized)
                _WerkplaatsIndeling.WindowState = FormWindowState.Normal;
            _WerkplaatsIndeling.BringToFront();
            _WerkplaatsIndeling.Focus();
        }

        public static void ShowPersoonVaardigheden(Personeel persoon)
        {
            if (persoon == null)
                return;
            new VaardighedenForm(persoon).ShowDialog();
        }

        public static void ShowBewStoringen(Bewerking bew)
        {
            var form = bew?.GetParent();
            if (form == null) return;
            var allst = new AlleStoringen();
            allst.InitStoringen(form);
            allst.ShowDialog();
        }

        public static void ShowWerkplekken(ProductieFormulier form)
        {
            if (form == null)
                return;
            var ind = new Indeling(form);
            ind.ShowDialog();
        }

        public static void ZoekWerkTekening()
        {
            try
            {
                var tb = new TextFieldEditor();
                tb.Style = MetroColorStyle.Yellow;
                tb.FieldImage = Resources.libreoffice_draw_icon_128x128.CombineImage(Resources.search_locate_find_6278,
                    ContentAlignment.BottomRight, 2.5);
                tb.MultiLine = false;
                tb.Title = "Zoek WerkTekening";
                if (tb.ShowDialog() == DialogResult.OK)
                {
                    var xart = tb.SelectedText;
                    var xtek = AutoDeskHelper.GetTekeningPdfAsync(xart);
                    //Process.Start(xtek); 
                    var wb = new WebBrowserForm();
                    wb.FilesToOpen = new string[] { $"{xart}_fbr.pdf" };
                    wb.FileDownloadUrl = AutoDeskHelper.DownloadUrl;
                    wb.Navigate(xtek);
                    // wb.Navigate("C:\\Users\\Gebruiker\\Dropbox\\ProductieManager\\Autodesk Vault.html");
                    wb.ShowDialog();
                }
            }
            catch (Exception e)
            {
                XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        public static void ShowProductieSettings(ProductieFormulier form)
        {
            if (form == null)
                return;
            var x = new WijzigProductie(form);
            x.ShowDialog();
        }

        public static void ShowOptieWidow()
        {
            var opties = new Opties();
            opties.ShowDialog();
            //if (_opties != null)
            //{
            //    if (!_opties.Visible)
            //        _opties.Show();
            //    if (_opties.WindowState == FormWindowState.Minimized)
            //        _opties.WindowState = FormWindowState.Normal;
            //}
            //else
            //{
            //    _opties = new Opties();
            //    _opties.FormClosed += (x, y) => { _opties = null; };
            //    _opties.Show();
            //}

            //_opties.BringToFront();
            //_opties.Select();
        }

        public static void ShowBewerkingDb()
        {
            var db = new DbBewerkingChanger();
            db.ShowDialog();
        }

        public static void ShowProductieOverzicht()
        {
            if (_ProductieOverzicht == null)
            {
                _ProductieOverzicht = new ProductieOverzichtForm();
                _ProductieOverzicht.FormClosed += (x, y) =>
                {
                    _ProductieOverzicht?.Dispose();
                    _ProductieOverzicht = null;
                };
            }

            _ProductieOverzicht.Show();
            if (_ProductieOverzicht.WindowState == FormWindowState.Minimized)
                _ProductieOverzicht.WindowState = FormWindowState.Normal;
            _ProductieOverzicht.BringToFront();
            _ProductieOverzicht.Focus();
        }

        public static void CheckForPreview(bool showall, bool onlyifnew)
        {
            try
            {
                var xprevs =
                    Functions.GetVersionPreviews(Manager.LastPreviewsUrl);
                var xvers = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                if (xprevs.Count > 0)
                {
                    UpdatePreviewForm xshowform = null;
                    if (!showall)
                    {
                        
                        if (!xprevs.ContainsKey(xvers)) return;
                        if (onlyifnew && new Version(Manager.DefaultSettings.LastPreviewVersion) >= new Version(xvers))
                            return;
                        xshowform = new UpdatePreviewForm(xprevs[xvers], onlyifnew,false);
                        xshowform.Title = $"NIEUW In {xvers}!";
                    }
                    else
                    {
                        var urls = xprevs.Select(x => x.Value).ToArray();
                        xshowform = new UpdatePreviewForm(urls);
                        xshowform.Title = $"Alle Aanpassingen In Versie: {xprevs.LastOrDefault().Key?? xvers}";
                    }

                    xshowform.ShowDialog();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion MenuButton Methods

        #region Menu Button Events

        #endregion Menu Button Events

        #region Taken Lijst

        private void takenManager1_OnTaakClicked(Taak taak)
        {
            if (taak?.Formulier != null)
            {
                if (taak.Plek != null)
                {
                    werkPlekkenUI1.xwerkpleklist.SelectedObject = taak.Plek;
                    if (werkPlekkenUI1.xwerkpleklist.SelectedItem != null)
                    {
                        metroTabControl.SelectedIndex = 1;
                        werkPlekkenUI1.xwerkpleklist.SelectedItem.EnsureVisible();
                        return;
                    }
                }

                if (taak.Bewerking != null)
                {
                    xbewerkingListControl.SelectedItem = taak.Bewerking;
                    if (xbewerkingListControl.SelectedItem != null) metroTabControl.SelectedIndex = 0;
                }
                //else
                //{
                    //xproductieListControl1.SelectedItem = taak.Formulier;
                    //if (xproductieListControl1.SelectedItem != null) metroTabControl.SelectedIndex = 0;
                //}
            }
        }

        private async void takenManager1_OnTaakUitvoeren(Taak taak)
        {
            var save = false;
            switch (taak.Type)
            {
                case AktieType.ControleCheck:
                    if (taak.Bewerking != null)
                    {
                        var xui = new AantalGemaaktUI();
                        if (xui.ShowDialog(taak.Formulier, taak.Bewerking, taak.Plek) == DialogResult.OK)
                            //taak.Update();
                            save = false;
                    }

                    break;

                case AktieType.Beginnen:
                    if (taak.Formulier != null)
                    {
                        var p = taak.Formulier;
                        if (p.State != ProductieState.Verwijderd && p.State != ProductieState.Gereed)
                            ShowProductieForm(p, true, taak.Bewerking);
                    }

                    break;

                case AktieType.GereedMelden:
                    if (taak.Formulier != null)
                    {
                        var p = taak.Formulier;
                        if (p.State != ProductieState.Verwijderd && p.State != ProductieState.Gereed)
                            ProductieListControl.MeldGereed(p);
                        //taak.Update();
                    }

                    break;

                case AktieType.BewerkingGereed:
                    if (taak.Bewerking != null)
                    {
                        var b = taak.Bewerking;
                        if (b.State != ProductieState.Verwijderd && b.State != ProductieState.Gereed)
                            ProductieListControl.MeldBewerkingGereed(b);
                        //taak.Update();
                    }

                    break;

                case AktieType.KlaarZetten:
                    if (taak.Formulier != null)
                    {
                        var p = taak.Formulier;
                        var matform = new MateriaalForm();
                        if (matform.ShowDialog(p) == DialogResult.OK)
                            save = true;
                    }

                    break;

                case AktieType.Stoppen:
                    break;

                case AktieType.PersoneelChange:
                    if (taak.Bewerking != null)
                    {
                        var ind = new Indeling(taak.Formulier, taak.Bewerking);
                        ind.StartPosition = FormStartPosition.CenterParent;
                        if (ind.ShowDialog() == DialogResult.OK)
                            //taak.Update();
                            save = false;
                    }

                    break;

                case AktieType.Telaat:
                    if (taak.Bewerking != null)
                    {
                        var dt = new DatumChanger();
                        if (dt.ShowDialog(taak.Bewerking.LeverDatum,
                                $"Wijzig leverdatum voor ({taak.Bewerking.Path}) {taak.Bewerking.Omschrijving}.") ==
                            DialogResult.OK)
                        {
                            taak.Bewerking.LeverDatum = dt.SelectedValue;
                            save = true;
                        }

                        dt.Dispose();
                    }
                    else if (taak.Formulier != null)
                    {
                        var dt = new DatumChanger();
                        if (dt.ShowDialog(taak.Formulier.LeverDatum,
                            $"Wijzig leverdatum voor {taak.Formulier.Omschrijving}.") == DialogResult.OK)
                        {
                            taak.Formulier.LeverDatum = dt.SelectedValue;
                            if (taak.Formulier.Bewerkingen.Length > 0)
                                taak.Formulier.Bewerkingen[taak.Formulier.Bewerkingen.Length - 1].LeverDatum =
                                    dt.SelectedValue;
                            save = true;
                        }

                        dt.Dispose();
                    }

                    break;

                case AktieType.PersoneelVrij:
                    if (taak.Bewerking != null)
                    {
                        var ind = new Indeling(taak.Formulier, taak.Bewerking);
                        ind.StartPosition = FormStartPosition.CenterParent;
                        if (ind.ShowDialog() == DialogResult.OK)
                            //taak.Update();
                            save = false;
                    }

                    break;
                case AktieType.Onderbreking:
                    if (taak.Plek != null)
                    {
                        var st = new StoringForm(taak.Plek);
                        st.ShowDialog();
                    }

                    break;
                case AktieType.None:
                    break;
            }

            if (save && taak.Formulier != null)
                await taak.Formulier.UpdateForm(true, false, null, $"[{taak.GetPath()}] Taak Uitgevoerd");
            else if (save)
                taak.Bewerking?.UpdateBewerking(null, $"[{taak.GetPath()}] Taak Uitgevoerd");
        }

        #endregion Taken Lijst

        private void xproductieoverzichtb_Click(object sender, EventArgs e)
        {
            ShowProductieOverzicht();
        }

        private async void xsearchprodnr_Click(object sender, EventArgs e)
        {
            try
            {
                if (Manager.Database == null)
                    throw new Exception("Database is niet geladen!");
                if (Manager.Database == null)
                    throw new Exception("Database is niet geladen!");
                var tb = new TextFieldEditor();
                tb.FieldImage = Resources.search_page_document_128x128;
                tb.MultiLine = false;
                tb.Title = "Zoek productienr";
                if (tb.ShowDialog() == DialogResult.OK)
                {
                    var xsel = tb.SelectedText.Trim();
                    var prod = await Manager.Database.GetProductie(xsel);
                    if (prod == null)
                        throw new Exception($"Er is geen productie gevonden met '{xsel}'.");
                    var bw = prod.Bewerkingen?.FirstOrDefault(x => x.IsAllowed());
                    Manager.FormulierActie(new object[] {prod, bw}, MainAktie.OpenProductie);
                }
            }
            catch (Exception ex)
            {
                XMessageBox.Show(ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)

        {
            var e = new KeyEventArgs(keyData);

            if (e.Control && e.KeyCode == Keys.F)

            {
                xsearchprodnr_Click(null, EventArgs.Empty);
                return true; // handled
            }

            if (e.Control && e.KeyCode == Keys.T)
            {
                mainMenu1.PressButton("xSearchTekening");
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void xopennewlijst_Click(object sender, EventArgs e)
        {
            ShowProductieLijstForm();
        }

        private void xtoonartikels_Click(object sender, EventArgs e)
        {
            ShowArtikelenWindow();
            //new ArtikelsForm().ShowDialog();
            //if (Manager.Opties != null)
            //    xbewerkingListControl.ProductieLijst.RestoreState(Manager.Opties.ViewDataBewerkingenState);
        }

        private void xopmerkingentoolstripbutton_Click(object sender, EventArgs e)
        {
            ShowOpmerkingWindow();
        }

        private void xShowPreview_Click(object sender, EventArgs e)
        {
            try
            {
                CheckForPreview(true, false);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                XMessageBox.Show(exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xHelpButton_Click(object sender, EventArgs e)
        {
            var xhelp = new UpdatePreviewForm(Manager.HelpDropUrl, false, true);
            xhelp.Title = "ProductieManager HelpDesk";
            if (xhelp.IsValid)
            {
                xhelp.ShowDialog();
            }
            else
            {
                XMessageBox.Show("HelpDesk is tijdelijk niet beschikbaar", "Niet Beschikbaar",
                    MessageBoxIcon.Exclamation);
            }
        }

        private void xcorruptedfilesbutton_Click(object sender, EventArgs e)
        {
            var xlist = MultipleFileDb.CorruptedFilePaths;
            var xweergave = new LijstWeergaveForm();
            xweergave.AllowAddItem = false;
            xweergave.AllowOpslaan = false;
            xweergave.ViewedData = xlist.ToArray();

            MultipleFileDb.CorruptedFilesChanged += xweergave.OnItemsChanged;

            xweergave.ItemRemoved += (x, y) =>
            {

                if (x is string[] items)
                {
                    int deleted = 0;
                    foreach (var item in items)
                    {
                        var itemname = Path.GetFileNameWithoutExtension(item);
                        var xdir = Path.GetDirectoryName(item);
                        var xdirname = Path.GetFileName(xdir);
                        var xdel = Manager.Database.RemoveFromCollection(xdirname, new string[] {itemname}).Result;
                        if(xdel > 0)
                            MultipleFileDb.CorruptedFilePaths.Remove(item);
                        deleted += xdel;
                    }

                    if (deleted > 0)
                    {
                        MultipleFileDb.OnCorruptedFilesChanged();
                    }
                }
            };
            xweergave.ShowDialog();
            MultipleFileDb.CorruptedFilesChanged -= xweergave.OnItemsChanged;
            xweergave.Dispose();
        }
    }
}