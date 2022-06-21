using BrightIdeasSoftware;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Mailing;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.SqlLite;
using Rpm.Various;
using Rpm.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class Opties : MetroBase.MetroBaseForm
    {
        //public readonly StickyWindow _stickyWindow;
        private List<string> _afdelingen = new();
        private List<string> _bewerkingen = new();
        public ListViewGroup[] _groups = { new("Standaart Instellingen"), new("Gebruikers") };
        public UserSettings _LoadedOpties = Manager.Opties;

        public Opties()
        {
            InitializeComponent();
            // _stickyWindow = new StickyWindow(this);
            metroTabControl1.SelectedIndex = 0;
            imageList2.Images.Add(Resources.database_21835_96x96);
            xFilterImageList.Images.Add(Resources.filter_32x32);
            ((OLVColumn)xFilterList.Columns[0]).ImageGetter = (_) => 0;
            ((OLVColumn)xoptielist.Columns[0]).GroupKeyGetter = GroupName;
            ((OLVColumn)xoptielist.Columns[0]).ImageGetter = ImageGetter;

            ((OLVColumn)xdatabaseview.Columns[0]).ImageGetter = (_) => 0;
            ((OLVColumn)xdatabaseview.Columns[0]).AspectGetter = DbNamegetter;
            xdatabaseview.CheckStateGetter = IsSelectedDatabase;

            imageList1.Images.Add(Resources.industry_setting_114090);
            imageList1.Images.Add(Resources.industry_setting_114090.CombineImage(Resources.check_1582, 2));
            xoptielist.Groups.Clear();
            if (Manager.Opties == null && !Manager.xLoadSettings(this, true))
                return;
            Manager.DefaultSettings ??= UserSettings.GetDefaultSettings();
        }

        private object DbNamegetter(object item)
        {
            if (item is string xval)
                return xval;
            return null;
        }

        private CheckState IsSelectedDatabase(object value)
        {
            try
            {
                if (_backupinfo?.ExcludeNames == null || _backupinfo.ExcludeNames.Count == 0) return CheckState.Checked;
                if (value is string xstr)
                {
                    return !_backupinfo.ExcludeNames.Any(x =>
                        string.Equals(x, xstr, StringComparison.CurrentCultureIgnoreCase)) ? CheckState.Checked : CheckState.Unchecked;
                }

                return CheckState.Checked;
            }
            catch
            {
                return CheckState.Indeterminate;
            }
        }


        private string GroupName(object item)
        {
            if (item is SettingModel) return ((SettingModel)item).GroupName;
            return "N/A";
        }

        private object ImageGetter(object item)
        {
            if (item is SettingModel) return ((SettingModel)item).IsLoaded ? 1 : 0;
            return 0;
        }

        private void _manager_OnLoginChanged(UserAccount user, object instance)
        {
            SetLoginState();
        }

        private void _manager_OnSettingsChanged(object instance, UserSettings settings, bool init)
        {
            if (this.IsDisposed || this.Disposing) return;
            this.BeginInvoke(new MethodInvoker(() =>
            {
                LoadSettings(settings, true);
                SetLoginState();
            }));
        }

        private void LoadSettings(UserSettings opties, bool setasdefault)
        {
            if (opties == null) return;
            if (setasdefault)
            {
                _LoadedOpties = opties.CreateCopy();
                Text = $"{Application.ProductName} Opties [{opties.Username}]";
                SetLoginState();
                Manager.DefaultSettings ??= UserSettings.GetDefaultSettings();
                xdblocatie.Text = Manager.DefaultSettings.MainDB.RootPath;
                xautologin.Enabled = Manager.LogedInGebruiker != null;
                xautologin.Checked = Manager.LogedInGebruiker != null && string.Equals(
                    Manager.LogedInGebruiker?.Username,
                    Manager.DefaultSettings.AutoLoginUsername, StringComparison.CurrentCultureIgnoreCase);
                xgebruikofflinemetsync.Checked = Manager.DefaultSettings.GebruikOfflineMetSync;
            }

            var x = opties;


            //rooster
            roosterUI1.SetRooster(x.WerkRooster.CreateCopy(), x.NationaleFeestdagen, x.SpecialeRoosters);
            //meldingen
            xtoonnieuwetaak.Checked = x.ToonLijstNaNieuweTaak;
            xmeldstart.Checked = x.TaakVoorStart;
            xmeldklaarzet.Checked = x.TaakVoorKlaarZet;
            xgebruiktaken.Checked = x.GebruikTaken;
            xmeldtelaatproductie.Checked = x.TaakAlsTelaat;
            xpersoneelvrij.Checked = x.TaakPersoneelVrij;
            xtaakproductiegereedcheck.Checked = x.TaakAlsGereed;
            xtaakperswijzigcheck.Checked = x.TaakVoorPersoneel;
            xonderbrekeningen.Checked = x.TaakVoorOnderbreking;
            xtaakcontrolecheck.Checked = x.TaakVoorControle;
            xminvoorstart.SetValue(x.MinVoorStart);
            xminvoorklaarzet.SetValue(x.MinVoorKlaarZet);
            xminvoorcontrole.SetValue(x.MinVoorControle);
            xsyncinterval.SetValue(x.SyncInterval);

            xmeldingOpmerking.Checked = x.ToonNieweOpmerkingMelding;
            xmeldingChat.Checked = x.ToonNieweChatBerichtMelding;
            xmeldingRecords.Checked = x.ToonArtikelRecordMeldingen;
            xmeldingBijlage.Checked = x.ToonNieweBijlageMelding;
            xmeldingtoon.Checked = x.SpeelMeldingToonAf;
            xtilesrefresh.SetValue(x.TileCountRefreshRate);

            //weergave producties
            xniewaantaluur.SetValue((decimal)x.NieuwTijd);

            xpersoneelindeling.Checked = x.ToonPersoneelIndelingNaOpstart;
            xwerkplaatsindeling.Checked = x.ToonWerkplaatsIndelingNaOpstart;

            WeergaveLijstUpdate(true);
            xFilterList.CheckedObjects = _LoadedOpties.ActieveFilters;

            //Admin Settings
            UpdateOptieList();
            UpdateEmailHostControls();
            xenablesync.Checked = x.GebruikLocalSync;
            xsyncinterval.Value = x.SyncInterval < 5000 ? 10000 : x.SyncInterval;
            xenablegreedsync.Checked = x.AutoGereedSync;
            xgereedsyncinterval.SetValue(x.GereedSyncInterval / 60000);
            xproductielijstsyncinterval.SetValue(x.ProductieLijstSyncInterval / 60000);
            xenableproductielijstsync.Checked = x.AutoProductieLijstSync;
            SetEmailFields(x);
            xlocatielist.Items.Clear();
            if (x.SyncLocaties != null)
                foreach (var s in x.SyncLocaties)
                    xlocatielist.Items.Add(s);
            xverwijderverwerkt.Checked = x.VerwijderVerwerkteBestanden;
            xstartopnaopstart.Checked = x.StartNaOpstart;
            xsluitaf.Checked = x.SluitPcAf;
            xsluitaftijd.Value = DateTime.Today.Add(x.AfsluitTijd);
            xminimizenatray.Checked = x.MinimizeToTray;
            xtoonallegestartproducties.Checked = x.ToonAlleGestartProducties;
            xtoonproductielogs.Checked = x.ToonProductieLogs;
            xweekoverzichtpath.Text = x.WeekOverzichtPath.Trim();
            xmaakoverichtenaan.Checked = x.CreateWeekOverzichten;
            xoverzichtbereikgroup.Enabled = x.CreateWeekOverzichten;
            xdocurrentweekoverzicht.Checked = x.DoCurrentWeekOverzicht;

            //var selected = x.ExcelColumns?.FirstOrDefault(s => s.IsExcelSettings && s.IsUsed("ExcelColumns"));
            //if (selected != null)
            //{
            //    xcolumnsStatusLabel.Text = $@"Opties Geselecteerd: {selected.Name}";
            //    xcolumnsStatusLabel.ForeColor = Color.DarkGreen;
            //}
            //else
            //{
            //    xcolumnsStatusLabel.Text = $@"Geen Opties Geselecteerd!";
            //    xcolumnsStatusLabel.ForeColor = Color.DarkRed;
            //}

            xtoondayli.Checked = x.ShowDaylyMessage;

            xmaakvanafweek.SetValue(x.VanafWeek);
            xmaakvanafjaar.SetValue(x.VanafJaar);
            xexcelinterval.SetValue((decimal)x.WeekOverzichtUpdateInterval / 60000);
            //load backupInfo
            xcreatebackup.Checked = x.CreateBackup;
            LoadBackupInfo();
            if (Manager.LogedInGebruiker is { AccesLevel: > AccesType.ProductieAdvance })
            {
                xbackupgroup.Visible = true;
                xrestorebackup.Visible = true;
            }
            else
            {
                xbackupgroup.Visible = false;
                xrestorebackup.Visible = false;
            }
            xtoonlognotificatie.Checked = x.ToonLogNotificatie;
            xtoonproductieNaToevoegen.Checked = x.ToonProductieNaToevoegen;
            xoffdbsyncinterval.SetValue(Manager.DefaultSettings.OfflineDbSyncInterval);
            //Zet de offline database gegevens.
            xoffprodcheckbox.Checked = Manager.DefaultSettings.OfflineDabaseTypes.IndexOf(DbType.Producties) > -1;
            xoffgereedprodcheckbox.Checked = Manager.DefaultSettings.OfflineDabaseTypes.IndexOf(DbType.GereedProducties) > -1;
            xoffperscheckbox.Checked = Manager.DefaultSettings.OfflineDabaseTypes.IndexOf(DbType.Medewerkers) > -1;
            xoffaccountcheckbox.Checked = Manager.DefaultSettings.OfflineDabaseTypes.IndexOf(DbType.Opties) > -1;
            xoffinstellingcheckbox.Checked = Manager.DefaultSettings.OfflineDabaseTypes.IndexOf(DbType.Accounts) > -1;
            xoffberichtencheckbox.Checked = Manager.DefaultSettings.OfflineDabaseTypes.IndexOf(DbType.Messages) > -1;
            xoffopmerkingcheckbox.Checked = Manager.DefaultSettings.OfflineDabaseTypes.IndexOf(DbType.Opmerkingen) > -1;
            xoffklachtencheckbox.Checked = Manager.DefaultSettings.OfflineDabaseTypes.IndexOf(DbType.Klachten) > -1;
            xartikelrecords.Checked = Manager.DefaultSettings.OfflineDabaseTypes.IndexOf(DbType.ArtikelRecords) > -1;
            xSpoorOverzichtCheckbox.Checked = Manager.DefaultSettings.OfflineDabaseTypes.IndexOf(DbType.SpoorOverzicht) > -1;
            xlijstlayout.Checked = Manager.DefaultSettings.OfflineDabaseTypes.IndexOf(DbType.LijstLayouts) > -1;
        }

        private void UpdateEmailHostControls()
        {
            if (!string.IsNullOrEmpty(_LoadedOpties?.BoundUsername))
            {
                xhostlabel.Text = $@"Host van '{_LoadedOpties.BoundUsername}' wordt gebruikt";
                xhostlabel.ForeColor = Color.ForestGreen;
                xdeletehost.Enabled = true;
                xdeletehost.Text = @$"Verwijder Host '{_LoadedOpties.BoundUsername}'";
            }
            else
            {
                xhostlabel.Text = @"Geen host om emails te verzenden!";
                xhostlabel.ForeColor = Color.Red;
                xdeletehost.Enabled = false;
                xdeletehost.Text = @$"Verwijder Host";
            }
        }

        private async void UpdateOptieList()
        {
            if (Manager.LogedInGebruiker is { AccesLevel: >= AccesType.ProductieAdvance })
                try
                {
                    var sets = await Manager.Database.GetAllSettings();
                    xoptielist.SetObjects(sets.Select(x => new SettingModel(x)
                    { IsLoaded = string.Equals(_LoadedOpties.Username, x.Username, StringComparison.CurrentCultureIgnoreCase) }).ToArray());
                }
                catch
                {
                }
            else xoptielist.Items.Clear();
        }

        string _lastcrit = null;
        private void WeergaveLijstUpdate(bool update)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(() => WeergaveLijstUpdate(update)));
                    return;
                }
                var filter = xsearchentry.Text.Trim().ToLower();

                var items = _LoadedOpties.Filters;

                if (!string.IsNullOrEmpty(_lastcrit) && string.Equals(filter, _lastcrit, StringComparison.CurrentCultureIgnoreCase) && !update)
                    return;
                _lastcrit = filter;
                if (!string.IsNullOrEmpty(filter))
                {
                    items = items.Where(x => (x.Name != null && x.Name.ToLower().Contains(filter)) ||
                                             x.CriteriaText.ToLower().Contains(filter)).ToList();
                }
                var sel = xFilterList.SelectedObjects;
                xFilterList.BeginUpdate();
                xFilterList.SetObjects(items);
                xFilterList.EndUpdate();
                xFilterList.SelectedObjects = sel;
                xFilterList.SelectedItem?.EnsureVisible();
            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        //private void WeergaveLijstUpdate()
        //{
        //    xweergavelijst.BeginUpdate();
        //    xweergavelijst.Items.Clear();
        //    xweergave.Items.Clear();
        //    var flag1 = xtoonallesvanbeide.Checked;
        //    var flag2 = xtoonvolgensafdeling.Checked;
        //    var flag3 = xtoonvolgensbewerking.Checked;
        //    // bool flag4 = xtoonalles.Checked || xdeelallesin.Checked;
        //    var bewerkingen = Manager.BewerkingenLijst.GetAllEntries().Select(x => (object) x.Naam).ToArray();
        //    var afdelingen = Manager.BewerkingenLijst.GetAlleWerkplekken(false).Select(x => (object) x).ToArray();
        //    if (flag1)
        //    {
        //        xgroupweergave.Visible = true;


        //        xweergave.Items.AddRange(bewerkingen);
        //        xweergave.Items.AddRange(afdelingen);
        //        foreach (var afd in _afdelingen)
        //            xweergavelijst.Items.Add(new ListViewItem(afd) {Tag = 0});
        //        foreach (var afd in _bewerkingen)
        //            xweergavelijst.Items.Add(new ListViewItem(afd) {Tag = 1});
        //    }
        //    else if (flag2)
        //    {
        //        xgroupweergave.Visible = true;
        //        xweergave.Items.AddRange(afdelingen);
        //        foreach (var afd in _afdelingen)
        //            xweergavelijst.Items.Add(new ListViewItem(afd) {Tag = 0});
        //    }
        //    else if (flag3)
        //    {
        //        xgroupweergave.Visible = true;
        //        xweergave.Items.AddRange(bewerkingen);
        //        foreach (var afd in _bewerkingen)
        //            xweergavelijst.Items.Add(new ListViewItem(afd) {Tag = 1});
        //    }
        //    else
        //    {
        //        xgroupweergave.Visible = false;
        //    }

        //    xweergavelijst.EndUpdate();
        //}

        public BackupInfo GetBackupInfo()
        {
            var info = BackupInfo.Load();
            info.BackupInterval = TimeSpan.FromHours((double)xcreatebackupinterval.Value).TotalMilliseconds;
            info.MaxBackupCount = (int)xmaxbackups.Value;
            info.ExcludeNames = xdatabaseview.Items.OfType<OLVListItem>().Where(x => x.CheckState != CheckState.Checked)
                .Select(x => x.RowObject as string).ToList();
            return info;
        }

        private void SaveBackupInfo()
        {
            try
            {
                var info = GetBackupInfo();
                info.Save();
                if (Manager.BackupInfo != null)
                {
                    Manager.BackupInfo.BackupInterval = info.BackupInterval;
                    Manager.BackupInfo.MaxBackupCount = info.MaxBackupCount;
                    Manager.BackupInfo.ExcludeNames = info.ExcludeNames;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private BackupInfo _backupinfo;

        private void LoadBackupInfo()
        {
            try
            {
                var info = BackupInfo.Load();
                xcreatebackupinterval.SetValue((decimal)TimeSpan.FromMilliseconds(info.BackupInterval).TotalHours);
                xmaxbackups.SetValue(info.MaxBackupCount);
                _backupinfo = info;
                xdatabaseview.SetObjects(Enum.GetNames(typeof(DbType)).Where(x => x.ToLower() != "alles" && x.ToLower() != "geen"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public UserSettings CreateInstance(string username)
        {
            if(InvokeRequired)
            {
                var ret = new UserSettings();
                this.Invoke(new MethodInvoker(()=> ret = CreateInstance(username)));
                return ret;
            }    
            var xs = new UserSettings();
            if (username != null)
                xs.Username = username;
            else if (_LoadedOpties != null)
                xs.Username = _LoadedOpties.Username;

            //werktijden
            xs.WerkRooster = roosterUI1.WerkRooster;
            xs.SpecialeRoosters = roosterUI1.SpecialeRoosters;
            xs.NationaleFeestdagen = roosterUI1.NationaleFeestdagen().ToArray();

            //taken
            xs.GebruikTaken = xgebruiktaken.Checked;
            xs.ToonLijstNaNieuweTaak = xtoonnieuwetaak.Checked;
            xs.TaakVoorStart = xmeldstart.Checked;
            xs.TaakVoorKlaarZet = xmeldklaarzet.Checked;
            xs.TaakAlsTelaat = xmeldtelaatproductie.Checked;
            xs.TaakVoorPersoneel = xtaakperswijzigcheck.Checked;
            xs.TaakVoorOnderbreking = xonderbrekeningen.Checked;
            xs.TaakPersoneelVrij = xpersoneelvrij.Checked;
            xs.TaakAlsGereed = xtaakproductiegereedcheck.Checked;
            xs.TaakVoorControle = xtaakcontrolecheck.Checked;
            xs.MinVoorStart = (int)xminvoorstart.Value;
            xs.MinVoorKlaarZet = (int)xminvoorklaarzet.Value;
            xs.MinVoorControle = (int)xminvoorcontrole.Value;
            xs.TaakSyncInterval = (int)xsyncinterval.Value;
            xs.SyncInterval = (int)xsyncinterval.Value;

            xs.TileCountRefreshRate = (int)xtilesrefresh.Value;

            xs.ShowDaylyMessage = xtoondayli.Checked;

            //weergave
            xs.NieuwTijd = (double)xniewaantaluur.Value;
            xs.ActieveFilters = xFilterList.CheckedObjects.OfType<Filter>().ToList();
            xs.Filters = xFilterList.Objects.OfType<Filter>().ToList();
            //Admin Settings
            //save settings
            if (Manager.LogedInGebruiker?.Username != null)
            {
                Manager.DefaultSettings.AutoLoginUsername = xautologin.Checked ? Manager.LogedInGebruiker.Username : null;
            }
            xs.GebruikLocalSync = xenablesync.Checked;
            xs.SyncInterval = (int)xsyncinterval.Value;
            xs.AutoGereedSync = xenablegreedsync.Checked;
            xs.GereedSyncInterval = (int)(xgereedsyncinterval.Value * 60000);
            xs.ProductieLijstSyncInterval = (int)(xproductielijstsyncinterval.Value * 60000);
            xs.AutoProductieLijstSync = xenableproductielijstsync.Checked;
            xs.VerzendAdres = GetUitgaandadreses();
            xs.InkomendMail = GetInkomendInfo();
            xs.SyncLocaties = xlocatielist.Items.Cast<ListViewItem>().Select(t => t.Text).ToArray();
            xs.VerwijderVerwerkteBestanden = xverwijderverwerkt.Checked;
            xs.StartNaOpstart = xstartopnaopstart.Checked;
            xs.SluitPcAf = xsluitaf.Checked;
            xs.AfsluitTijd = xsluitaftijd.Value.TimeOfDay;
            xs.MinimizeToTray = xminimizenatray.Checked;
            xs.ToonAlleGestartProducties = xtoonallegestartproducties.Checked;
            xs.ToonProductieLogs = xtoonproductielogs.Checked;
            xs.ToonProductieNaToevoegen = xtoonproductieNaToevoegen.Checked;

            xs.ToonPersoneelIndelingNaOpstart = xpersoneelindeling.Checked;
            xs.ToonWerkplaatsIndelingNaOpstart = xwerkplaatsindeling.Checked;

            xs.WeekOverzichtPath = xweekoverzichtpath.Text.Trim();
            xs.CreateWeekOverzichten = xmaakoverichtenaan.Checked;
            xs.DoCurrentWeekOverzicht = xdocurrentweekoverzicht.Checked;
            xs.VanafWeek = (int)xmaakvanafweek.Value;
            xs.VanafJaar = (int)xmaakvanafjaar.Value;
            xs.WeekOverzichtUpdateInterval = (int)xexcelinterval.Value * 60000;
            //backup info
            xs.CreateBackup = xcreatebackup.Checked;

            //meldingen
            xs.ToonLogNotificatie = xtoonlognotificatie.Checked;

            xs.ToonNieweOpmerkingMelding = xmeldingOpmerking.Checked;
            xs.ToonNieweChatBerichtMelding = xmeldingChat.Checked;
            xs.ToonArtikelRecordMeldingen = xmeldingRecords.Checked;
            xs.ToonNieweBijlageMelding = xmeldingBijlage.Checked;
            xs.SpeelMeldingToonAf = xmeldingtoon.Checked;

            Manager.DefaultSettings.OfflineDbSyncInterval = (int)xoffdbsyncinterval.Value;
            Manager.DefaultSettings.OfflineDabaseTypes.Clear();
            if (xoffprodcheckbox.Checked)
            {
                Manager.DefaultSettings.OfflineDabaseTypes.Add(DbType.Producties);
            }
            if (xoffgereedprodcheckbox.Checked)
            {
                Manager.DefaultSettings.OfflineDabaseTypes.Add(DbType.GereedProducties);
            }
            if (xoffperscheckbox.Checked)
            {
                Manager.DefaultSettings.OfflineDabaseTypes.Add(DbType.Medewerkers);
            }
            if (xoffaccountcheckbox.Checked)
            {
                Manager.DefaultSettings.OfflineDabaseTypes.Add(DbType.Accounts);
            }
            if (xoffinstellingcheckbox.Checked)
            {
                Manager.DefaultSettings.OfflineDabaseTypes.Add(DbType.Opties);
            }
            if (xoffberichtencheckbox.Checked)
            {
                Manager.DefaultSettings.OfflineDabaseTypes.Add(DbType.Messages);
            }
            if (xoffopmerkingcheckbox.Checked)
            {
                Manager.DefaultSettings.OfflineDabaseTypes.Add(DbType.Opmerkingen);
            }
            if (xoffklachtencheckbox.Checked)
            {
                Manager.DefaultSettings.OfflineDabaseTypes.Add(DbType.Klachten);
            }
            if (xartikelrecords.Checked)
            {
                Manager.DefaultSettings.OfflineDabaseTypes.Add(DbType.ArtikelRecords);
            }
            if (xSpoorOverzichtCheckbox.Checked)
            {
                Manager.DefaultSettings.OfflineDabaseTypes.Add(DbType.SpoorOverzicht);
            }
            if (xlijstlayout.Checked)
            {
                Manager.DefaultSettings.OfflineDabaseTypes.Add(DbType.LijstLayouts);
            }
            //default settings die we hier niet veranderen.
            if (_LoadedOpties != null)
            {
                xs.GroupEntries = _LoadedOpties.GroupEntries;

                xs.BackgroundImagePath = _LoadedOpties.BackgroundImagePath;
                xs.PreviewShown = _LoadedOpties.PreviewShown;
                xs.WelcomeShown = _LoadedOpties.WelcomeShown;
                xs.LastPreviewVersion = _LoadedOpties.LastPreviewVersion;
                xs.BoundUsername = _LoadedOpties.BoundUsername;
                xs.OntvangAdres = _LoadedOpties.OntvangAdres;

                xs.MainDB = _LoadedOpties.MainDB;
                xs.TempMainDB = _LoadedOpties.TempMainDB;
                xs.UpdateDatabaseVersion = _LoadedOpties.UpdateDatabaseVersion;

                xs.PersoneelIndeling = _LoadedOpties.PersoneelIndeling;
                xs.WerkplaatsIndelingen = _LoadedOpties.WerkplaatsIndelingen;
                xs.TileLayout = _LoadedOpties.TileLayout;
                xs.TileFlowDirection = _LoadedOpties.TileFlowDirection;
                xs.TileViewBackgroundColorRGB = _LoadedOpties.TileViewBackgroundColorRGB;
                xs.LastShownTabName = _LoadedOpties.LastShownTabName;

                xs.EmailClients = _LoadedOpties.EmailClients;
                xs.TijdelijkeRooster = _LoadedOpties.TijdelijkeRooster;
                xs.PersoneelAfdelingFilter = _LoadedOpties.PersoneelAfdelingFilter;
                xs.DbUpdateEntries = _LoadedOpties.DbUpdateEntries;
                xs.DbUpdateInterval = _LoadedOpties.DbUpdateInterval;
                xs.BewerkingWeergaveFilters = _LoadedOpties.BewerkingWeergaveFilters;
                xs.ProductieWeergaveFilters = _LoadedOpties.ProductieWeergaveFilters;

                xs.Notities = _LoadedOpties.Notities;
                xs.UseLastGereedStart = _LoadedOpties.UseLastGereedStart;
                xs.LastGereedStart = _LoadedOpties.LastGereedStart;
                xs.UseLastGereedStop = _LoadedOpties.UseLastGereedStop;
                xs.LastGereedStop = _LoadedOpties.LastGereedStop;
                //Listview View states
                xs._viewwerkplekdata = _LoadedOpties._viewwerkplekdata;
                xs._viewstoringdata = _LoadedOpties._viewstoringdata;
                xs._viewvaarddata = _LoadedOpties._viewvaarddata;
                xs._viewpersoneeldata = _LoadedOpties._viewpersoneeldata;
                xs._viewallenotitiesdata = _LoadedOpties._viewallenotitiesdata;

                xs.LastFormInfo = _LoadedOpties.LastFormInfo;
            }



            return xs;
        }

        public bool IsChanged()
        {
            if (Manager.Opties == null)
                return false;
            //bool xchanged = !string.Equals(xdblocatie.Text.Trim(), DefaultSettings.MainDB?.RootPath,
            //    StringComparison.CurrentCultureIgnoreCase);
            return !CreateInstance(Manager.Opties.Username)
                .xPublicInstancePropertiesEqual(Manager.Opties, new[] { typeof(UserChange) });
        }

        private bool SetSettings()
        {
            if (Manager.Opties == null) return false;

            if (_LoadedOpties != null && !string.Equals(Manager.Opties?.Username, _LoadedOpties.Username, StringComparison.CurrentCultureIgnoreCase))
            {
                var result = XMessageBox.Show(this,
                    $"U staat op het punt de instellingen van '{_LoadedOpties.Username}' over te nemen.\n\n" +
                    $"Weet u zeker dat u wilt doorgaan?", $"{_LoadedOpties.Username} Instellingen Overnemen",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.No) return false;
            }
            var settings = CreateInstance(Manager.Opties.Username);
            _LoadedOpties = settings;
            return true;
        }

        private bool _saving;
        private void button2_Click(object sender, EventArgs e)
        {
            if (_saving) return;
            _saving = true;
            SaveAndClose();
            _saving = false;
        }

        private bool SaveAndClose()
        {
            Manager.OnSettingsChanged -= _manager_OnSettingsChanged;
            if (!SetSettings()) return false;
            if (_locatieGewijzigd)
            {
                string db = xdblocatie.Text.Trim();
                if (!string.Equals(Manager.DefaultSettings.MainDB.RootPath, db,
                        StringComparison.CurrentCultureIgnoreCase) &&
                    Directory.Exists(db))
                {
                    var rsult = XMessageBox.Show(this, $"Database locatie is gewijzigd!\n" +
                                                 "Om de wijzigingen door te kunnen voeren dien je de Productie Manager opnieuw op te starten.\n\n" +
                                                 "Wil je de Productie Manager nu opnieuw opstarten?",
                        "Database Locatie Gewijzigd!",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (rsult == DialogResult.Cancel) return false;
                    //string xdb2 = Manager.DefaultSettings.MainDB.UpdatePath;
                    //if (Directory.Exists(xdb2))
                    //{
                    //    var rsult2 = XMessageBox.Show(this, $"Wil je de oude gegevens kopieren naar de nieuwe database?",
                    //        $"Kopieren naar {db}", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    //    if (rsult2 == DialogResult.Cancel)
                    //        return false;
                    //    if (rsult2 == DialogResult.Yes)
                    //    {
                    //        if (!xdb2.CopyDirectoryTo(db + "\\RPM_Data")) return false;

                    //    }
                    //}

                    if (rsult == DialogResult.Yes)
                    {
                        Manager.DefaultSettings.MainDB.RootPath = db;
                        Manager.DefaultSettings.SaveAsDefault();
                        var proc = Process.GetCurrentProcess();
                        var name = proc.MainModule?.FileName;
                        if (!string.IsNullOrEmpty(name) && File.Exists(name))
                        {
                            Process.Start(name);
                            proc.Kill();
                        }
                        else Application.Restart();

                        return true;
                    }
                }

                Manager.DefaultSettings.MainDB.RootPath = db;
            }

            if (Manager.LogedInGebruiker is { AccesLevel: > AccesType.ProductieAdvance })
                SaveBackupInfo();
            Manager.DefaultSettings.GebruikOfflineMetSync = xgebruikofflinemetsync.Checked;
            if (Manager.DefaultSettings.GebruikOfflineMetSync)
            {
                Manager.ProductieProvider.InitOfflineDb();
            }
            else
            {
                Manager.ProductieProvider.DisableOfflineDb();
            }

            Manager.DefaultSettings.SaveAsDefault();
            if (IsChanged())
            {
                Manager.Opties = _LoadedOpties;
                _LoadedOpties.Save("Instellingen Opgeslagen!", true, true);
            }
            this.Invoke(new Action(Close));
            return true;
            //else
            //    XMessageBox.Show(this, $"Er zijn geen  wijzigingen...\n\n", "Geen Wijzigingen", MessageBoxButtons.OK,
            //        MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void xcreateaccount_Click(object sender, EventArgs e)
        {
            var cr = new CreateAccount();
            if (cr.ShowDialog() == DialogResult.OK)
            {
                var ac = cr.Account;
                Manager.CreateAccount(ac).Wait();
                UpdateOptieList();
            }
        }

        public void SetLoginState()
        {
            if (Manager.LogedInGebruiker != null)
            {
                xlogin.Image = Resources.Logout_37127__1_;
                toolTip1.SetToolTip(xlogin, "Log Uit");
            }
            else
            {
                xlogin.Image = Resources.Login_37128__1_;
                toolTip1.SetToolTip(xlogin, "Log In");
            }
        }

        private void xlogin_Click(object sender, EventArgs e)
        {
            if (Manager.LogedInGebruiker == null)
            {
                var cr = new LogIn();
                cr.ShowDialog();
            }
            else
            {
               Manager.LogOut(this,true);
            }
        }

        private void xgebruikersb_Click(object sender, EventArgs e)
        {
            var cr = new UserAccounts();
            if (cr.ShowDialog() == DialogResult.OK) UpdateOptieList();
        }

        private void Opties_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsChanged())
            {
                var res = XMessageBox.Show(
                    this, "Er zijn wijzigingen gemaakt, wil je deze opslaan?\n\nNiet opgeslagen gegevens zullen verloren gaan!",
                    "Opslaan", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                if (res == DialogResult.Yes && !SaveAndClose())
                {
                    e.Cancel = true;
                    return;
                }
               
            }
            
            Manager.OnSettingsChanged -= _manager_OnSettingsChanged;
            //Manager.OnLoginChanged -= _manager_OnLoginChanged;
        }

        private void Opties_Shown(object sender, EventArgs e)
        {
            LoadSettings(Manager.Opties,true);
            SetLoginState();
            Manager.OnSettingsChanged += _manager_OnSettingsChanged;
            //Manager.OnLoginChanged += _manager_OnLoginChanged;
        }

        private void xkiesfolder_Click(object sender, EventArgs e)
        {
            var fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK) xfolderpath.Text = fb.SelectedPath;
        }

        private void xaddfolder_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem lv in xlocatielist.Items)
                    if (string.Equals(lv.Text, xfolderpath.Text, StringComparison.CurrentCultureIgnoreCase))
                        throw new Exception($"'{xfolderpath.Text}' is al toegevoegd!");
                xlocatielist.Items.Add(xfolderpath.Text);
            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void xlocatielist_SelectedIndexChanged(object sender, EventArgs e)
        {
            xremovefolder.Visible = xlocatielist.SelectedItems.Count > 0;
        }

        private void xremovefolder_Click(object sender, EventArgs e)
        {
            if (xlocatielist.SelectedItems.Count > 0)
                if (XMessageBox.Show(this, $"Weetje zeker dat je alle geselecteerde locaties wilt verwijderen?", "Verwijderen",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    foreach (ListViewItem lv in xlocatielist.SelectedItems)
                        lv.Remove();
        }

        private void xuitgaandemailijst_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null)
                return;
            if (!e.Label.EmailIsValid())
            {
                XMessageBox.Show(this, $"'{e.Label}' is geen geldige email adres!", "Ongeldige Email", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                e.CancelEdit = true;
            }
            else
            {
                var item = xuitgaandemailijst.Items[e.Item];
                if (item.Tag is UitgaandAdres adres)
                    adres.Adres = e.Label;
            }
        }

        //private void xinkomendemaillijst_AfterLabelEdit(object sender, LabelEditEventArgs e)
        //{
        //    if (e.Label == null)
        //        return;
        //    if (!e.Label.EmailIsValid())
        //    {
        //        XMessageBox.Show(this, $"'{e.Label}' is geen geldige email adres!", "Ongeldige Email", MessageBoxButtons.OK,
        //            MessageBoxIcon.Exclamation);
        //        e.CancelEdit = true;
        //    }
        //    else
        //    {
        //        var item = xinkomendemaillijst.Items[e.Item];
        //        if (item.Tag is InkomendAdres adres)
        //            adres.Adres = e.Label;
        //    }
        //}

        private async void xoptielist_ButtonClick(object sender, CellClickEventArgs e)
        {
            if (e.ColumnIndex > 0)
            {
                var moddel = e.ListView.Objects.Cast<SettingModel>().ToArray()[e.RowIndex];
                if (moddel == null)
                    return;
                switch (e.ColumnIndex)
                {
                    case 1: // laad instellingen
                        if (XMessageBox.Show(this, $"Weetje zeker dat je '{moddel.Settings.Username}' wilt Laden?", "Laden",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) ==
                            DialogResult.Yes) 
                            LoadSettings(Manager.Database.GetSetting(moddel.Name), true);
                        break;

                    case 2: // vervang instellingen
                        var name = moddel.Settings.Username;
                        var id = moddel.Settings.SystemID;
                        var res = XMessageBox.Show(
                            this, $"Je staat op het punt '{name}' te overschrijven met deze instellingen!\n\nWeet je zeker dat je dat wilt doen?!",
                            "Overschrijf", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (res != DialogResult.Yes)
                            return;
                        moddel.Settings = CreateInstance(name);
                        moddel.Settings.SystemID = id;
                        if (await moddel.Settings.Save())
                        {
                            if (Manager.Opties != null &&
                                string.Equals(moddel.Settings.Username, Manager.Opties.Username, StringComparison.CurrentCultureIgnoreCase))
                                Manager.Opties = moddel.Settings;
                        }
                        else
                        {
                            XMessageBox.Show(this, $"Er zijn geen  wijzigingen om op te slaan...\n\n", "Geen Wijzigingen",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                }
            }
        }

        private void xrebuilddb_Click(object sender, EventArgs e)
        {
            if (Manager.Database is {IsDisposed: false})
            {
                var prod = new UpdateProducties(){StartWhenShown = true, CloseWhenFinished = true};
                prod.ShowDialog();
            }
        }

        private void xmaakoverichtenaan_CheckedChanged(object sender, EventArgs e)
        {
            xoverzichtbereikgroup.Enabled = xmaakoverichtenaan.Checked;
        }

        private void xverzondenweekoverzichtenb_Click(object sender, EventArgs e)
        {
            if (xuitgaandemailijst.SelectedItems.Count > 0)
                if (xuitgaandemailijst.SelectedItems[0].Tag is UitgaandAdres item)
                {
                    var lf = new LijstWeergaveForm();
                    lf.ViewedData = item.VerzondenWeekOverzichten.Select(x => x.ToString()).ToArray();
                    if (lf.ShowDialog() == DialogResult.OK)
                        try
                        {
                            item.VerzondenWeekOverzichten =
                                lf.ViewedData.Select(WeekOverzicht.FromString).ToList();
                        }
                        catch (Exception ex)
                        {
                            XMessageBox.Show(this, ex.Message, "Fout",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                }
        }

        #region "Email Verkeer Settings"

        private void SetEmailFields(UserSettings settings)
        {
            var x = settings;
            RemoveCheckBoxEvents();
            //xinkomendgroup.Enabled = false;
            xuitgaandgroup.Enabled = false;
            //xinkomendemaillijst.Items.Clear();
            xuitgaandemailijst.Items.Clear();
            //if (x.OntvangAdres != null)
            //{
            //    foreach (var adres in x.OntvangAdres)
            //    {
            //        var item = new ListViewItem(adres.Adres);
            //        item.Tag = new InkomendAdres(adres.Adres, adres.Actions);
            //        xinkomendemaillijst.Items.Add(item);
            //    }

            //    if (xinkomendemaillijst.Items.Count > 0)
            //        xinkomendemaillijst.Items[0].Selected = true;
            //}

            if (x.VerzendAdres != null)
            {
                foreach (var adres in x.VerzendAdres)
                {
                    var item = new ListViewItem(adres.Adres) {Tag = adres.CreateCopy()};
                    xuitgaandemailijst.Items.Add(item);
                }

                if (xuitgaandemailijst.Items.Count > 0)
                    xuitgaandemailijst.Items[0].Selected = true;
            }
            SetInkomendInfo(settings.InkomendMail);
            Invalidate();
            AddCheckBoxEvents();
        }

        private InkomendMailSetting GetInkomendInfo()
        {
            var xset = new InkomendMailSetting();

            var actions = new List<MessageAction>();
            if(xinkprodwijz.Checked)
                actions.Add(MessageAction.ProductieWijziging);
            if (xinkniewprod.Checked)
                actions.Add(MessageAction.NieweProductie);
            if(xinkmelding.Checked)
                actions.Add(MessageAction.AlgemeneMelding);
            if(xinkbijlages.Checked)
                actions.Add(MessageAction.BijlageUpdate);
            xset.AllowedActions = actions.OrderBy(x=> (int)x).ToList();
            xset.OnlyAllowedSenders = xradiolijst.Checked;
            if(xset.OnlyAllowedSenders)
            {
                var xmails = new List<string>();
                foreach (ListViewItem item in xuitgaandemailijst.Items)
                    if (item.Tag is UitgaandAdres ink)
                        xmails.Add(ink.Adres);
                xset.AllowedSenders = xmails.OrderBy(x=> x).ToList();
            }
            return xset;
        }

        private void SetInkomendInfo(InkomendMailSetting xset)
        {
            xinkprodwijz.Checked = xset.AllowedActions != null && xset.AllowedActions.Any(x => x == MessageAction.ProductieWijziging);
            xinkniewprod.Checked = xset.AllowedActions != null && xset.AllowedActions.Any(x => x == MessageAction.NieweProductie);
            xinkmelding.Checked = xset.AllowedActions != null && xset.AllowedActions.Any(x => x == MessageAction.AlgemeneMelding);
            xinkbijlages.Checked = xset.AllowedActions != null && xset.AllowedActions.Any(x => x == MessageAction.BijlageUpdate);

            if (xset.OnlyAllowedSenders)
                xradiolijst.Checked = true;
            else xradioiedereen.Checked = true;
        }

        private List<UitgaandAdres> GetUitgaandadreses()
        {
            var xreturn = new List<UitgaandAdres>();
            foreach (ListViewItem item in xuitgaandemailijst.Items)
                if (item.Tag is UitgaandAdres)
                    xreturn.Add(item.Tag as UitgaandAdres);
            return xreturn;
        }

        //private void xinkomendemaillijst_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    xniewcheck.Checked = false;
        //    xwijzigcheck.Checked = false;
        //    xverwijdercheck.Checked = false;
        //    xmededelingcheck.Checked = false;
        //    if (xinkomendemaillijst.SelectedItems.Count > 0)
        //    {
        //        var lv = xinkomendemaillijst.SelectedItems[0];
        //        var ink = lv.Tag as InkomendAdres;

        //        if (ink != null)
        //        {
        //            if (ink.Actions != null)
        //                foreach (var action in ink.Actions)
        //                    switch (action)
        //                    {
        //                        case MessageAction.NieweProductie:
        //                            xniewcheck.Checked = true;
        //                            break;

        //                        case MessageAction.ProductieNotitie:
        //                        case MessageAction.ProductieWijziging:
        //                            xwijzigcheck.Checked = true;
        //                            break;

        //                        case MessageAction.ProductieVerwijderen:
        //                            xverwijdercheck.Checked = true;
        //                            break;

        //                        case MessageAction.AlgemeneMelding:
        //                            xmededelingcheck.Checked = true;
        //                            break;

        //                        case MessageAction.None:
        //                            break;
        //                    }

        //            xinkomendgroup.Enabled = true;
        //            xverwijderinkomendmail.Visible = true;
        //            xinkomendeemailtext.Text = ink.Adres;
        //        }
        //    }
        //    else
        //    {
        //        xinkomendgroup.Enabled = false;
        //        xverwijderinkomendmail.Visible = false;
        //    }
        //}

        private void xuitgaandemailijst_SelectedIndexChanged(object sender, EventArgs e)
        {
            xverzendstartcheck.Checked = false;
            xverzendstopcheck.Checked = false;
            xverzendverwijdercheck.Checked = false;
            xverzendgereedcheck.Checked = false;

            if (xuitgaandemailijst.SelectedItems.Count > 0)
            {
                var lv = xuitgaandemailijst.SelectedItems[0];
                if (lv.Tag is UitgaandAdres ink)
                {
                    if (ink.States != null)
                        foreach (var state in ink.States)
                            switch (state)
                            {
                                case ProductieState.Gestart:
                                    xverzendstartcheck.Checked = true;
                                    break;

                                case ProductieState.Gestopt:
                                    xverzendstopcheck.Checked = true;
                                    break;

                                case ProductieState.Verwijderd:
                                    xverzendverwijdercheck.Checked = true;
                                    break;

                                case ProductieState.Gereed:
                                    xverzendgereedcheck.Checked = true;
                                    break;
                            }

                    xuitgaandgroup.Enabled = true;
                    xverwijderuitgaanemail.Visible = true;
                    xuitgaandemailtext.Text = ink.Adres;
                    xverzendstoring.Checked = ink.SendStoringMail;
                    xsendweekoverzicht.Checked = ink.SendWeekOverzichten;
                    xvanafjaar.SetValue(ink.VanafYear);
                    ink.VanafYear = (int) xvanafjaar.Value;
                    xvanafweek.SetValue(ink.VanafWeek);
                    ink.VanafWeek = (int) xvanafweek.Value;
                }
            }
            else
            {
                xuitgaandgroup.Enabled = false;
                xverwijderuitgaanemail.Visible = false;
                xverzendstoring.Checked = false;
                xsendweekoverzicht.Checked = false;
                xvanafjaar.Value = xvanafjaar.Minimum;
                xvanafweek.Value = xvanafweek.Minimum;
            }
        }

        //private void xaddinkomendemail_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(xinkomendeemailtext.Text))
        //    {
        //        XMessageBox.Show(this, $"Vul in een geldige email adres aub");
        //    }
        //    else if (!xinkomendeemailtext.Text.EmailIsValid())
        //    {
        //        XMessageBox.Show(this, $"Ongelidge email adres!\nVul in een geldige email adres aub", "Ongeldige Email",
        //            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //    else
        //    {
        //        var contains = xinkomendemaillijst.Items.Cast<ListViewItem>().Any(x =>
        //            ((InkomendAdres) x.Tag).Adres.Trim().ToLower() == xinkomendeemailtext.Text.Trim().ToLower());
        //        if (contains)
        //        {
        //            XMessageBox.Show(this, $"Email adres bestaat al!\n Gebruik een andere email adres aub.",
        //                "Email Bestaat Al",
        //                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        }
        //        else
        //        {
        //            var item = new ListViewItem(xinkomendeemailtext.Text.Trim());
        //            var ink = new InkomendAdres {Adres = xinkomendeemailtext.Text.Trim()};
        //            item.Tag = ink;
        //            xinkomendemaillijst.Items.Add(item);
        //        }
        //    }
        //}

        //private void xverwijderinkomendmail_Click(object sender, EventArgs e)
        //{
        //    if (xinkomendemaillijst.SelectedItems.Count > 0)
        //        foreach (ListViewItem item in xinkomendemaillijst.SelectedItems)
        //            item.Remove();
        //}

        private void xverwijderuitgaanemail_Click(object sender, EventArgs e)
        {
            if (xuitgaandemailijst.SelectedItems.Count > 0)
                foreach (ListViewItem item in xuitgaandemailijst.SelectedItems)
                    item.Remove();
        }

        private void xadduitgaanemail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xuitgaandemailtext.Text))
            {
                XMessageBox.Show(this, $"Vul in een geldige email adres aub");
            }
            else if (!xuitgaandemailtext.Text.EmailIsValid())
            {
                XMessageBox.Show(this, $"Ongelidge email adres!\nVul in een geldige email adres aub", "Ongeldige Email",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                var contains = xuitgaandemailijst.Items.Cast<ListViewItem>().Any(x =>
                    ((UitgaandAdres) x.Tag).Adres.Trim().ToLower() == xuitgaandemailtext.Text.Trim().ToLower());
                if (contains)
                {
                    XMessageBox.Show(this, $"Email adres bestaat al!\n Gebruik een andere email adres aub.",
                        "Email Bestaat Al",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    var item = new ListViewItem(xuitgaandemailtext.Text.Trim());
                    var ink = new UitgaandAdres {Adres = xuitgaandemailtext.Text.Trim()};
                    item.Tag = ink;
                    xuitgaandemailijst.Items.Add(item);
                }
            }
        }

        private void AddCheckBoxEvents()
        {
            //xniewcheck.CheckedChanged += xniewcheck_CheckedChanged;
            //xverwijdercheck.CheckedChanged += xverwijdercheck_CheckedChanged;
            //xwijzigcheck.CheckedChanged += xwijzigcheck_CheckedChanged;
            //xmededelingcheck.CheckedChanged += xmededelingcheck_CheckedChanged;

            xverzendstartcheck.CheckedChanged += xverzendstartcheck_CheckedChanged;
            xverzendstopcheck.CheckedChanged += xverzendstopcheck_CheckedChanged;
            xverzendverwijdercheck.CheckedChanged += xverzendverwijdercheck_CheckedChanged;
            xverzendgereedcheck.CheckedChanged += xverzendgereedcheck_CheckedChanged;
            xverzendstoring.CheckedChanged += Xverzendstoring_CheckedChanged;
            xsendweekoverzicht.CheckedChanged += Xsendweekoverzicht_CheckedChanged;
            xvanafweek.ValueChanged += Xvanafweek_ValueChanged;
            xvanafjaar.ValueChanged += Xvanafjaar_ValueChanged;
        }

        private void Xvanafjaar_ValueChanged(object sender, EventArgs e)
        {
            if (xuitgaandemailijst.SelectedItems.Count > 0)
                if (xuitgaandemailijst.SelectedItems[0].Tag is UitgaandAdres adres)
                    adres.VanafYear = (int) xvanafjaar.Value;
        }

        private void Xvanafweek_ValueChanged(object sender, EventArgs e)
        {
            if (xuitgaandemailijst.SelectedItems.Count > 0)
                if (xuitgaandemailijst.SelectedItems[0].Tag is UitgaandAdres adres)
                    adres.VanafWeek = (int) xvanafweek.Value;
        }

        private void Xsendweekoverzicht_CheckedChanged(object sender, EventArgs e)
        {
            if (xuitgaandemailijst.SelectedItems.Count > 0)
                if (xuitgaandemailijst.SelectedItems[0].Tag is UitgaandAdres adres)
                {
                    adres.SendWeekOverzichten = xsendweekoverzicht.Checked;
                    xweekoverzichtgroup.Enabled = xsendweekoverzicht.Enabled;
                }
        }

        private void Xverzendstoring_CheckedChanged(object sender, EventArgs e)
        {
            if (xuitgaandemailijst.SelectedItems.Count > 0)
                if (xuitgaandemailijst.SelectedItems[0].Tag is UitgaandAdres adres)
                    adres.SendStoringMail = xverzendstoring.Checked;
        }

        private void RemoveCheckBoxEvents()
        {
            //xniewcheck.CheckedChanged -= xniewcheck_CheckedChanged;
            //xverwijdercheck.CheckedChanged -= xverwijdercheck_CheckedChanged;
            //xwijzigcheck.CheckedChanged -= xwijzigcheck_CheckedChanged;
            //xmededelingcheck.CheckedChanged -= xmededelingcheck_CheckedChanged;

            xverzendstartcheck.CheckedChanged -= xverzendstartcheck_CheckedChanged;
            xverzendstopcheck.CheckedChanged -= xverzendstopcheck_CheckedChanged;
            xverzendverwijdercheck.CheckedChanged -= xverzendverwijdercheck_CheckedChanged;
            xverzendgereedcheck.CheckedChanged -= xverzendgereedcheck_CheckedChanged;
            xverzendstoring.CheckedChanged -= Xverzendstoring_CheckedChanged;
            xsendweekoverzicht.CheckedChanged -= Xsendweekoverzicht_CheckedChanged;
            xvanafweek.ValueChanged -= Xvanafweek_ValueChanged;
            xvanafjaar.ValueChanged -= Xvanafjaar_ValueChanged;
        }

        //private void xniewcheck_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (xinkomendemaillijst.SelectedItems.Count > 0)
        //    {
        //        var adres = xinkomendemaillijst.SelectedItems[0].Tag as InkomendAdres;
        //        var actions = new List<MessageAction>();
        //        if (adres.Actions != null && adres.Actions.Length > 0)
        //            actions.AddRange(adres.Actions.Where(t => t != MessageAction.NieweProductie).ToArray());
        //        if (xniewcheck.Checked)
        //            actions.Add(MessageAction.NieweProductie);
        //        adres.Actions = actions.ToArray();
        //    }
        //}

        //private void xverwijdercheck_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (xinkomendemaillijst.SelectedItems.Count > 0)
        //    {
        //        var adres = xinkomendemaillijst.SelectedItems[0].Tag as InkomendAdres;
        //        var actions = new List<MessageAction>();
        //        if (adres.Actions != null && adres.Actions.Length > 0)
        //            actions.AddRange(adres.Actions.Where(t => t != MessageAction.ProductieVerwijderen).ToArray());
        //        if (xverwijdercheck.Checked)
        //            actions.Add(MessageAction.ProductieVerwijderen);
        //        adres.Actions = actions.ToArray();
        //    }
        //}

        //private void xwijzigcheck_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (xinkomendemaillijst.SelectedItems.Count > 0)
        //    {
        //        var adres = xinkomendemaillijst.SelectedItems[0].Tag as InkomendAdres;
        //        var actions = new List<MessageAction>();
        //        if (adres.Actions != null && adres.Actions.Length > 0)
        //            actions.AddRange(adres.Actions.Where(t => t != MessageAction.ProductieWijziging).ToArray());
        //        if (xwijzigcheck.Checked)
        //            actions.Add(MessageAction.ProductieWijziging);
        //        adres.Actions = actions.ToArray();
        //    }
        //}

        //private void xmededelingcheck_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (xinkomendemaillijst.SelectedItems.Count > 0)
        //    {
        //        var adres = xinkomendemaillijst.SelectedItems[0].Tag as InkomendAdres;
        //        var actions = new List<MessageAction>();
        //        if (adres.Actions != null && adres.Actions.Length > 0)
        //            actions.AddRange(adres.Actions.Where(t => t != MessageAction.AlgemeneMelding).ToArray());
        //        if (xmededelingcheck.Checked)
        //            actions.Add(MessageAction.AlgemeneMelding);
        //        adres.Actions = actions.ToArray();
        //    }
        //}

        private void xverzendstartcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (xuitgaandemailijst.SelectedItems.Count > 0)
            {
                var adres = xuitgaandemailijst.SelectedItems[0].Tag as UitgaandAdres;
                var states = new List<ProductieState>();
                if (adres.States is {Length: > 0})
                    states.AddRange(adres.States.Where(t => t != ProductieState.Gestart).ToArray());
                if (xverzendstartcheck.Checked)
                    states.Add(ProductieState.Gestart);
                adres.States = states.ToArray();
            }
        }

        private void xverzendstopcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (xuitgaandemailijst.SelectedItems.Count > 0)
            {
                var adres = xuitgaandemailijst.SelectedItems[0].Tag as UitgaandAdres;
                var states = new List<ProductieState>();
                if (adres.States is {Length: > 0})
                    states.AddRange(adres.States.Where(t => t != ProductieState.Gestopt).ToArray());
                if (xverzendstopcheck.Checked)
                    states.Add(ProductieState.Gestopt);
                adres.States = states.ToArray();
            }
        }

        private void xverzendverwijdercheck_CheckedChanged(object sender, EventArgs e)
        {
            if (xuitgaandemailijst.SelectedItems.Count > 0)
            {
                var adres = xuitgaandemailijst.SelectedItems[0].Tag as UitgaandAdres;
                var states = new List<ProductieState>();
                if (adres.States is {Length: > 0})
                    states.AddRange(adres.States.Where(t => t != ProductieState.Verwijderd).ToArray());
                if (xverzendverwijdercheck.Checked)
                    states.Add(ProductieState.Verwijderd);
                adres.States = states.ToArray();
            }
        }

        private void xverzendgereedcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (xuitgaandemailijst.SelectedItems.Count > 0)
            {
                if (xuitgaandemailijst.SelectedItems[0].Tag is UitgaandAdres adres)
                {
                    var states = new List<ProductieState>();
                    if (adres.States is {Length: > 0})
                        states.AddRange(adres.States.Where(t => t != ProductieState.Gereed).ToArray());
                    if (xverzendgereedcheck.Checked)
                        states.Add(ProductieState.Gereed);
                    adres.States = states.ToArray();
                }
            }
        }

        #endregion "Email Verkeer Settings"

        private void xgebruiktaken_CheckedChanged(object sender, EventArgs e)
        {
            xtakengroup.Enabled = xgebruiktaken.Checked;
        }

        private void xchooseoverzichtpathb_Click(object sender, EventArgs e)
        {
            var fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK) xweekoverzichtpath.Text = fb.SelectedPath;
        }


        private bool _locatieGewijzigd;
        private void xkiesdbbutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.Description = "Kies database locatie";
            fb.SelectedPath = xdblocatie.Text;
            if (fb.ShowDialog() == DialogResult.OK)
            {
                if (!string.Equals(xdblocatie.Text.Trim(), fb.SelectedPath, StringComparison.CurrentCultureIgnoreCase))
                {
                    xdblocatie.Text = fb.SelectedPath;
                }
            }
        }

        private void xcopydatabase_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog
            {
                Description = "Kies database locatie"
            };
            if (fb.ShowDialog() == DialogResult.OK)
            {
                (xdblocatie.Text + "\\RPM_Data").CopyDirectoryTo(fb.SelectedPath + "\\RPM_Data",this);
            }
        }

        private void xenablesync_CheckedChanged(object sender, EventArgs e)
        {
            xproductiesyncgroup.Enabled = xenablesync.Checked;
        }

        private void xdblocatie_TextChanged(object sender, EventArgs e)
        {
            string db = xdblocatie.Text.Trim();
            if (!string.Equals(Manager.DefaultSettings?.MainDB?.RootPath, db, StringComparison.CurrentCultureIgnoreCase) &&
                Directory.Exists(db))
            {
                _locatieGewijzigd = true;
                xdblocatie.ForeColor = Color.Green;
            }
            else
            {
                _locatieGewijzigd = false;
                xdblocatie.ForeColor = Color.Black;
            }
        }

        private void xcreatebackup_CheckedChanged(object sender, EventArgs e)
        {
            xbackupgroup.Enabled = xcreatebackup.Checked;
        }

        private void xenablegreedsync_CheckedChanged(object sender, EventArgs e)
        {
            xgereedproductiesyncgroup.Enabled = xenablegreedsync.Checked;
        }

        private void xenableproductielijstsync_CheckedChanged(object sender, EventArgs e)
        {
            xproductielijstsyncgroup.Enabled = xenableproductielijstsync.Checked;
        }

        private void xgebruikofflinemetsync_CheckedChanged(object sender, EventArgs e)
        {
            xofflinedbgroup.Enabled = xgebruikofflinemetsync.Checked;
        }

        private async void xbeheerEmailhost_Click(object sender, EventArgs e)
        {
            if (Manager.Database?.UserAccounts == null) return;
            var login = new LogIn();
            login.DisableLogin = true;
            login.ShowAutoLoginCheckbox = false;
            if (login.ShowDialog() == DialogResult.OK)
            {
                var acc = await Manager.Database.GetAccount(login.Username);
                if (acc == null)
                {
                    XMessageBox.Show(this, $"Gebruiker '{login.Username}' bestaat niet", "Gebruiker bestaat niet",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    if (acc.ValidateUser(login.Username, login.Password, false))
                    {
                        if (acc.AccesLevel < AccesType.Manager)
                            throw new Exception($"'{acc.Username}' is geen beheerder!\n\n" +
                                                $"De emailhost kan alleen door een beheerder worden gewijzigd.");
                        var mailhost = new EmailHostForm(acc.MailingHost);
                        if (mailhost.ShowDialog() == DialogResult.OK)
                        {
                            _LoadedOpties.BoundUsername = acc.Username;
                            UpdateEmailHostControls();
                            acc.MailingHost = mailhost.SelectedHost;
                            await Manager.Database.UpSert(acc);
                        }
                    }
                }
                catch (Exception exception)
                {
                    XMessageBox.Show(this, exception.Message, "Fout",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
             
            }
        }

        private async void xdeletehost_Click(object sender, EventArgs e)
        {
            if (Manager.Database?.UserAccounts == null) return;
            var login = new LogIn();
            login.DisableLogin = true;
            login.ShowAutoLoginCheckbox = false;
            if (login.ShowDialog() == DialogResult.OK)
            {
                var acc = await Manager.Database.GetAccount(login.Username);
                if (acc == null)
                {
                    XMessageBox.Show(this, $"Gebruiker '{login.Username}' bestaat niet", "Gebruiker bestaat niet",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    if (acc.ValidateUser(login.Username, login.Password, false))
                    {
                        if (acc.AccesLevel < AccesType.Manager)
                            throw new Exception($"'{acc.Username}' is geen beheerder!\n\n" +
                                                $"De emailhost kan alleen door een beheerder worden gewijzigd.");
                        if (!string.Equals(_LoadedOpties.BoundUsername, acc.Username,
                            StringComparison.CurrentCultureIgnoreCase))
                            throw new Exception(@$"Alleen '{_LoadedOpties.BoundUsername}' kan de email host verwijderen!");
                        _LoadedOpties.BoundUsername = null;
                        UpdateEmailHostControls();
                    }
                }
                catch (Exception exception)
                {
                    XMessageBox.Show(this, exception.Message, "Fout",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void xfiltertype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void xdatabaseview_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if(_backupinfo == null) { return; }

            _backupinfo.ExcludeNames ??= new List<string>();
            if (e.Item is OLVListItem item)
            {
                var index = _backupinfo.ExcludeNames.IndexOf(item.Text);
                if (!item.Checked)
                {
                    if (index > -1)
                        _backupinfo.ExcludeNames.RemoveAt(index);
                }
                else
                {
                    if (index == -1)
                        _backupinfo.ExcludeNames.Add(item.Text);
                }

                xdatabaseview.RefreshItem(item);
            }
        }

        private void xdatabaseview_DoubleClick(object sender, EventArgs e)
        {
            if (xdatabaseview.SelectedItem != null)
            {
                xdatabaseview.SelectedItem.Checked = !xdatabaseview.SelectedItem.Checked;
            }
        }

        private bool isrestoring;
        private async void xrestorebackup_Click(object sender, EventArgs e)
        {
            if (isrestoring) return;
            var result = XMessageBox.Show(this, "Weet u zeker dat u de backup wilt terugzetten?!\n\n" +
                "Alle gegevens uit de backup zullen terug gezet worden", "Backup Terugzetten", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result != DialogResult.Yes) return;
            var ofd = new OpenFileDialog();
            ofd.Filter = "Zip|*.zip";
            ofd.Title = "Kies een backup zip(.zip) bestand";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                isrestoring = true;
                var prog = new LoadingForm();
                var arg = prog.Arg;
                prog.CloseIfFinished = true;
                arg.Message = "Backup terugzetten...";
                arg.OnChanged(this);
                _ = prog.ShowDialogAsync(this);
                if (BackupInfo.IsValidBackup(ofd.FileName, null))
                {
                    await Manager.BackupInfo.UnZip(ofd.FileName, Manager.DbPath, null, true, arg.Token);
                    arg.Type = ProgressType.WriteCompleet;
                    arg.OnChanged(this);
                    XMessageBox.Show(this, "Backup succesvol terug gezet!", "Backup Terug Gezet");
                }
                else
                {
                    arg.Type = ProgressType.WriteCompleet;
                    arg.OnChanged(this);
                    XMessageBox.Show(this, $"'{Path.GetFileName(ofd.FileName)}' is geen geldige backup", "Ongeldige Backup", MessageBoxIcon.Warning);
                }
                isrestoring = false;
            }

        }

        private async void xbackupmaken_Click(object sender, EventArgs e)
        {
            if (isrestoring) return;
            var ofd = new SaveFileDialog();
            ofd.Filter = "Zip|*.zip";
            ofd.Title = "Maak een backup zip(.zip) bestand";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                isrestoring = true;
                var prog = new LoadingForm();
                var arg = prog.Arg;
                prog.CloseIfFinished = true;
                arg.Message = "Backup Maken...";
                arg.OnChanged(this);
                _= prog.ShowDialogAsync(this);
                var info = GetBackupInfo();
                info.CancellationToken = arg.Token;
                if (await BackupInfo.CreateBackup(info, ofd.FileName))
                {
                    arg.Type = ProgressType.WriteCompleet;
                    arg.OnChanged(this);
                    if (!arg.Token.IsCancellationRequested)
                        XMessageBox.Show(this, "Backup succesvol aangemaakt!", "Backup Aangemaakt");
                }
                isrestoring = false;
            }
        }

        private void xsearchentry_TextChanged(object sender, EventArgs e)
        {
            xsearchentry.ShowClearButton = true;
            xsearchentry.Invalidate();
            WeergaveLijstUpdate(false);
        }

        private void xnewEntry_Click(object sender, EventArgs e)
        {
            if (_LoadedOpties == null) return;
            string name = null;
            while (true)
            {
                var txt = new TextFieldEditor();
                txt.MultiLine = false;
                txt.MinimalTextLength = 4;
                txt.EnableSecondaryField = false;
                txt.Title = "Kies een Filternaam om aan te maken...";
                if (txt.ShowDialog() != DialogResult.OK) return;
                name = txt.SelectedText.Trim();
                bool flag = _LoadedOpties.Filters.Any(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
                if (flag)
                {
                    var res = XMessageBox.Show(this, $"Filternaam '{name}' bestaat al!\n\n" +
                        $"Kies een andere Filternaam a.u.b.", "Filternaam Bestaat Al", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (res != DialogResult.OK) return;
                    continue;
                }
                break;
            }
            if (string.IsNullOrEmpty(name)) return;
            var xcrits = new EditCriteriaForm(typeof(IProductieBase),null,true);
            xcrits.Title = $"Filter '{name}' aanmaken...";
            if (xcrits.ShowDialog() != DialogResult.OK || xcrits.SelectedFilter.Count == 0) return;
            var f = new Filter { IsTempFilter = false, Name = name };
            f.Filters.AddRange(xcrits.SelectedFilter);
            _LoadedOpties.Filters.Add(f);
            xFilterList.AddObject(f);
            xFilterList.SelectedObject = f;
            xFilterList.SelectedItem?.EnsureVisible();
            WeergaveLijstUpdate(true);
        }

        private void xFilterList_DoubleClick(object sender, EventArgs e)
        {
            if(xFilterList.SelectedItem is OLVListItem item)
            {
                item.Checked = !item.Checked;
            }
        }

        private void xFilterListMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            naamWijzigenToolStripMenuItem.Enabled = xFilterList.SelectedObject != null;
            filterWijzigenToolStripMenuItem.Enabled = xFilterList.SelectedObject != null;
            filterVerwijderenToolStripMenuItem.Enabled = xFilterList.SelectedObjects.Count > 0;
            filterDelenToolStripMenuItem.Enabled = xFilterList.SelectedObjects.Count > 0 && Manager.LogedInGebruiker is {AccesLevel: > AccesType.ProductieAdvance };
        }

        private void filterWijzigenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(xFilterList.SelectedObject is Filter filter)
            {
                var xf = new EditCriteriaForm(typeof(IProductieBase), filter.Filters, true);
                if (xf.ShowDialog() != DialogResult.OK) return;
                filter.Filters = xf.SelectedFilter;
                xFilterList.RefreshObject(filter);
            }
        }

        private void filterVerwijderenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var items = xFilterList.SelectedObjects.OfType<Filter>().ToList();
            if (_LoadedOpties?.Filters != null && items.Count > 0)
            {
                var x1 = items.Count == 1 ? $"{items[0].Name}'" : $"{items.Count} filters";
                var res = XMessageBox.Show(this, $"Weet u zeker dat u {x1} wilt verwijderen?", "Filters Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res != DialogResult.Yes) return;
                xFilterList.RemoveObjects(items);
                items.ForEach(x => _LoadedOpties.Filters.Remove(x));
            }
        }

        private void naamWijzigenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xFilterList.SelectedObject is Filter filter)
            {
                string name = null;
                while (true)
                {
                    var txt = new TextFieldEditor();
                    txt.MultiLine = false;
                    txt.MinimalTextLength = 4;
                    txt.EnableSecondaryField = false;
                    txt.Title = "Wijzig Filternaam...";
                    txt.SelectedText = filter.Name;
                    if (txt.ShowDialog() != DialogResult.OK) return;
                    name = txt.SelectedText.Trim();
                    bool flag = _LoadedOpties.Filters.Any(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
                    if (flag)
                    {
                        return;
                    }
                    break;
                }
                if (string.IsNullOrEmpty(name)) return;
                filter.Name = name;
                WeergaveLijstUpdate(true);
            }
        }

        private void filterDelenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsDisposed || Manager.Opties == null || Manager.Database.IsDisposed || Manager.Database == null) return;
                var selected = xFilterList.SelectedObjects.OfType<Filter>().ToList();
                if (selected.Count == 0) return;
                var settings = Manager.Database.xGetAllSettings().Where(x => x.Username != null && !x.Username.ToLower().Contains("default") && !string.Equals(x.Username, _LoadedOpties?.Username, StringComparison.CurrentCultureIgnoreCase)).ToList();
                var xitems = settings
                    .Select(x => x.Username.FirstCharToUpper())
                    .ToList();
                if (xitems.Count == 0) return;
                var x1 = selected.Count == 1 ? "filter" : "filters";
                var xchoose = new ChooseValuesForm();
                xchoose.Title = $"Kies gebruikers om de geselecteerde {selected.Count} {x1} mee te delen";
                xchoose.SetChooseItems(xitems);
                if (xchoose.ShowDialog() == DialogResult.OK)
                {
                    var xsel = xchoose.SelectedValues;
                    foreach(var sel in xsel)
                    {
                        var opt = settings.FirstOrDefault(x => string.Equals(x.Username, sel, StringComparison.CurrentCultureIgnoreCase));
                        if(opt != null)
                        {
                            opt.Filters ??= new List<Filter>();
                            opt.ActieveFilters ??= new List<Filter>();
                            foreach (var f in selected)
                            {
                                opt.Filters.RemoveAll(x=> string.Equals(f.Name, x.Name, StringComparison.CurrentCultureIgnoreCase));
                                opt.Filters.Add(f);
                                if (opt.ActieveFilters.RemoveAll(x => string.Equals(f.Name, x.Name, StringComparison.CurrentCultureIgnoreCase)) > 0)
                                    opt.ActieveFilters.Add(f);
                            }
                            opt.xSave($"Filter(s) van {Manager.Opties.Username} gedeeld met {sel}");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        //private void xKiesExcelColumnButton_Click(object sender, EventArgs e)
        //{
        //    if (_LoadedOpties == null) return;
        //    var xf = new ExcelOptiesForm();
        //    xf.LoadOpties(_LoadedOpties, "ExcelColumns",true);
        //    if (xf.ShowDialog() == DialogResult.OK)
        //    {
        //        Manager.UpdateExcelColumns(_LoadedOpties, xf.Settings,true);
        //        _LoadedOpties.Save("ExcelColumns Aangepast!");
        //    }
        //    var selected = _LoadedOpties.ExcelColumns?.FirstOrDefault(x => x.IsUsed("ExcelColumns") && x.IsExcelSettings);
        //    if (selected != null)
        //    {
        //        xcolumnsStatusLabel.Text = $@"Opties Geselecteerd: {selected.Name}";
        //        xcolumnsStatusLabel.ForeColor = Color.DarkGreen;
        //    }
        //    else
        //    {
        //        xcolumnsStatusLabel.Text = $@"Geen Opties Geselecteerd!";
        //        xcolumnsStatusLabel.ForeColor = Color.DarkRed;
        //    }

        //}
    }
}