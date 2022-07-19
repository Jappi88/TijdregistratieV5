using MetroFramework;
using Microsoft.Win32.SafeHandles;
using ProductieManager.Rpm.ExcelHelper;
using ProductieManager.Rpm.Productie;
using ProductieManager.Rpm.Productie.Verpakking;
using ProductieManager.Rpm.Settings;
using ProductieManager.Rpm.Various;
using Rpm.Klachten;
using Rpm.Mailing;
using Rpm.Misc;
using Rpm.Opmerking;
using Rpm.Productie.Verpakking;
using Rpm.Settings;
using Rpm.SqlLite;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductieManager.Rpm.Mailing;
using Rpm.MeldingCenter;
using Timer = System.Timers.Timer;

namespace Rpm.Productie
{
    /// <summary>
    /// De hoofd beheerder van de PrdouctieManager
    /// </summary>
    [Serializable]
    public class Manager : IDisposable
    {
        #region "Variables"
        private TimeSpan _afsluittijd;

        public static readonly string LastPreviewsUrl =
            "https://www.dropbox.com/s/5rtsu7ipm4d6dp9/Preview%20Links.txt?dl=1";
        public static readonly string HelpDropUrl =
            "https://www.dropbox.com/s/5xc90j20d5odya6/Help.txt?dl=1";
        // public static LocalService LocalConnection { get; private set; }
        public static Form ActiveForm { get; set; }
        public static bool AllowEvents { get; set; } = true;
        public static ArtikelRecords.ArtikelRecords ArtikelRecords { get; set; }
        public static VerpakkingBeheer Verpakkingen { get; private set; }
        public static SporenBeheer SporenBeheer { get; private set; }
        public static KlachtBeheer Klachten { get; private set; }
        public static ListLayoutBeheer ListLayouts { get; private set; }
        public static EmailServer MailServer { get; private set; }
        /// <summary>
        /// De productiechat
        /// </summary>
        public static ProductieChat ProductieChat { get; private set; }
        /// <summary>
        /// De database die de bestanden kan lezen, schrijven en zoeken
        /// </summary>
        public static LocalDatabase Database { get; private set; }
        /// <summary>
        /// De database updater die kijkt of de database is geupdate, en update.
        /// </summary>
        public static DatabaseUpdater DbUpdater { get; private set; }
        /// <summary>
        /// De database voor alle opmerkingen.
        /// </summary>
        public static Opmerkingen Opmerkingen { get; private set; }
        public static MeldingCenter.MeldingBeheer Meldingen { get; private set; }
        /// <summary>
        /// De productieprovider zorgt ervoor dat altijd je altijd de actuele infomatie krijgt.
        /// </summary>
        public static ProductieProvider ProductieProvider { get; private set; }
        /// <summary>
        /// de locatie beheerders waar wordt gekeken of er nieuwe productie PDF zijn geplaatst.
        /// </summary>
        private List<FileSystemWatcher> _fileWatchers = new();
        /// <summary>
        /// Een rij taken die uitgevoerd moeten worden vanuit de productiemanager 
        /// </summary>
        public static readonly TaskQueues TaskQueues = new();
        /// <summary>
        /// De applicatie basis lokatie
        /// </summary>
        public static string AppRootPath;
        public static string SecondaryAppRootPath;
        /// <summary>
        /// De locatie van de basis database
        /// </summary>
        public static string DbPath;
        /// <summary>
        /// De locatie waar de week overzichten gemaakt moeten worden
        /// </summary>
        public static string WeekOverzichtPath;
        /// <summary>
        /// Een locatie voor eventuele tijdelijke bestanden
        /// </summary>
        public static string TempPath;
        /// <summary>
        /// De locatie waar de backups opgeslagen kunnen worden
        /// </summary>
        public static string BackupPath;
        /// <summary>
        /// De locatie waar de productieformulieren opgeslagen kunnen worden om later te kunnen bekijken.
        /// </summary>
        public static string ProductieFormPath;
        //private static readonly string SettingsDirectory = Mainform.BootDir + "\\RPM_Settings";

        // [NonSerialized] private readonly BackgroundWorker _backemailchecker;

        // [NonSerialized] private readonly Timer _emailcheckTimer = new();

        [NonSerialized] private Timer _syncTimer;
        [NonSerialized] private Timer _overzichtSyncTimer;
        /// <summary>
        /// De geladen gebruiker opties
        /// </summary>
        public static UserSettings Opties { get; set; }
        /// <summary>
        /// Ingelogde gebruiker
        /// </summary>
        public static UserAccount LogedInGebruiker { get; set; }
        /// <summary>
        /// Huidige geladen backup informatie
        /// </summary>
        public static BackupInfo BackupInfo { get; set; }

        /// <summary>
        /// De basis instellingen van de applicatie
        /// </summary>
        public static UserSettings DefaultSettings { get; set; } = UserSettings.GetDefaultSettings();
        /// <summary>
        /// De interval in miliseconden waarvan de applicatie de producties op de achtergrond synchroniseerd
        /// </summary>
        public double SyncInterval
        {
            get => _syncTimer.Interval;
            set => _syncTimer.Interval = value;
        }

        //public int MailInterval
        //{
        //    get => _emailcheckTimer.Interval;
        //    set => _emailcheckTimer.Interval = value;
        //}
        /// <summary>
        /// De huidige bewerking lijst
        /// </summary>
        public static BewerkingLijst BewerkingenLijst;
        /// <summary>
        /// Of de Beheerder geladen is
        /// </summary>
        public static bool IsLoaded { get; private set; }
        //public static string xInstanceID { get; private set; }
        /// <summary>
        /// De unique systeem id wordt gebruikt om instellingen van een andere pc niet door elkaar te halen
        /// </summary>
        public static string SystemId { get; private set; }
        /// <summary>
        /// Als je logs wilt maken van de activiteiten
        /// </summary>
        public bool LoggerEnabled { get; private set; }

        #endregion "Variables"

        #region "Constructor"
        /// <summary>
        /// Maak een nieuwe beheerder aan
        /// </summary>
        /// <param name="dolog">True voor als je activiteiten wilt loggen</param>
        public Manager(bool dolog)
        {
            LoggerEnabled = dolog;
            // + "_" + Functions.CheckForSameInstance(); // Path.GetRandomFileName().Replace(".",""); //CpuID.ProcessorId();
            //LoadPath(rootpath);
            //_backemailchecker = new BackgroundWorker();
            //_backemailchecker.WorkerSupportsCancellation = true;
            //_backemailchecker.WorkerReportsProgress = true;
        }

        #region Manager Init
        /// <summary>
        /// Initialiseer events voor de manager als je met deze beheerder aan de slag wilt
        /// </summary>
        public void InitManager()
        {
            //_backemailchecker.DoWork += _worker_DoWork;
            //_backemailchecker.RunWorkerCompleted += _worker_RunWorkerCompleted;
            OnManagerLoaded -= Manager_OnManagerLoaded;
            OnManagerLoaded += Manager_OnManagerLoaded;
            //_emailcheckTimer.Tick += _timer_Tick;
            _syncTimer?.Dispose();
            _syncTimer = new Timer();
            _syncTimer.Elapsed += _syncTimer_Tick;
            _overzichtSyncTimer?.Dispose();
            _overzichtSyncTimer = new Timer();
            _overzichtSyncTimer.Interval = 60000;//1 min
            _overzichtSyncTimer.Elapsed += _overzichtSyncTimer_Tick;
            TaskQueues.OnRunComplete -= _tasks_OnRunComplete;
            TaskQueues.RunInstanceComplete -= _tasks_OnRunInstanceComplete;
            TaskQueues.OnRunComplete += _tasks_OnRunComplete;
            TaskQueues.RunInstanceComplete += _tasks_OnRunInstanceComplete;
        }

        private void InitDirectories()
        {
            if (!Directory.Exists(DbPath))
                Directory.CreateDirectory(DbPath);
            if (!string.IsNullOrEmpty(SecondaryAppRootPath) && !Directory.Exists(SecondaryAppRootPath))
                Directory.CreateDirectory(SecondaryAppRootPath);
            if (!Directory.Exists(TempPath))
                Directory.CreateDirectory(TempPath);
            if (!Directory.Exists(BackupPath))
                Directory.CreateDirectory(BackupPath);
            if (!Directory.Exists(WeekOverzichtPath))
                Directory.CreateDirectory(WeekOverzichtPath);
            if (!Directory.Exists(ProductieFormPath))
                Directory.CreateDirectory(ProductieFormPath);
        }

        private void LoadPath(string rootpath)
        {
            AppRootPath = rootpath;
            SecondaryAppRootPath = DefaultSettings?.TempMainDB?.RootPath;
            var xyearpath = Path.Combine(AppRootPath, DateTime.Now.Year.ToString());
            if (Directory.Exists(xyearpath))
            {
                AppRootPath = xyearpath;
                if(!string.IsNullOrEmpty(SecondaryAppRootPath))
                    SecondaryAppRootPath = Path.Combine(SecondaryAppRootPath, DateTime.Now.Year.ToString());
            }

            AppRootPath = Path.Combine(Directory.GetParent(AppRootPath)?.FullName??"", Path.GetFileName(AppRootPath));
            DbPath = Path.Combine(AppRootPath, "RPM_Data");
            TempPath = Path.Combine(AppRootPath, "Temp");
            BackupPath = Path.Combine(AppRootPath, "Backup");
            WeekOverzichtPath = Path.Combine(AppRootPath, "Week Overzichten");
            ProductieFormPath = Path.Combine(AppRootPath, "RPM_Data","Productie Formulieren");
            InitDirectories();
        }

        /// <summary>
        /// Laad de beheerder als een taak met de aangegeven argumenten
        /// </summary>
        /// <param name="path">De hoofdlocatie waarvan zich de database vind en eventueel andere folders</param>
        /// <param name="autologin">True voor als je automatisch in wilt loggen</param>
        /// <param name="loadsettings">True voor als je de instellingen wilt laden</param>
        /// <param name="raiseManagerLoadingEvents">True voor als je de event wilt roepen dat de beheerder geladen is</param>
        /// <returns>Een taak die op de achtergrond kan draaien en "True" zal aangeven als het gelukt is</returns>
        public Task<bool> Load(string path, bool autologin, bool loadsettings, bool raiseManagerLoadingEvents)
        {
            return Task.Run(() =>
            {
                try
                {
                    var cancel = false;
                    if (raiseManagerLoadingEvents)
                    {
                        ManagerLoading(ref cancel);
                        if (cancel)
                            return false;
                    }

                    Close();
                    LoadPath(path);
                    InitManager();
                    Database = new LocalDatabase(this, SystemId, DbPath)
                    {
                        LoggerEnabled = LoggerEnabled,
                        NotificationEnabled = false
                    }; 
                    Database.LoadMultiFiles();
                    ProductieChat = new ProductieChat(Path.Combine(DbPath, "Chat"));
                    ProductieProvider = new ProductieProvider();
                    ProductieProvider.InitEvents();
                    // LocalConnection = new LocalService();
                    //Server = new SqlDatabase();
                    Klachten = new KlachtBeheer(DbPath);
                    Klachten.KlachtChanged += OnKlachtChanged;
                    Klachten.KlachtDeleted += OnKlachtDeleted;

                    Verpakkingen = new VerpakkingBeheer(DbPath);
                    Verpakkingen.VerpakkingChanged += OnVerpakkingChanged;
                    Verpakkingen.VerpakkingDeleted += OnVerpakkingDeleted;

                    SporenBeheer = new SporenBeheer(DbPath);
                    SporenBeheer.SpoorChanged += OnSpoorChanged;
                    SporenBeheer.SpoorDeleted += OnSpoorDeleted;

                    ListLayouts = new ListLayoutBeheer(DbPath);
                    ListLayouts.LayoutChanged += OnLayoutChanged;
                    ListLayouts.LayoutDeleted += OnLayoutDeleted;

                    DbUpdater = new DatabaseUpdater();
                    BackupInfo = BackupInfo.Load();
                    BewerkingenLijst = new BewerkingLijst();
                    Meldingen = new MeldingBeheer(Path.Combine(DbPath, "Meldingen"));
                    if (string.IsNullOrEmpty(DefaultSettings.SystemID))
                    {
                        DefaultSettings.SystemID = Guid.NewGuid().ToByteArray().ToHexString(4);
                        DefaultSettings.SaveAsDefault();
                    }

                    SystemId = DefaultSettings.SystemID;
                    if (autologin)
                        autologin = xAutoLogin(this);
                    if (loadsettings && !autologin)
                        _=LoadSettings(this, raiseManagerLoadingEvents);

                    if (loadsettings)
                    {
                        ArtikelRecords?.Dispose();
                        ArtikelRecords = new ArtikelRecords.ArtikelRecords(DbPath);
                        Opmerkingen = new Opmerkingen();
                        Opmerkingen.OnOpmerkingenChanged += Opmerkingen_OnOpmerkingenChanged;
                        _= Opmerkingen.Load();
                        ProductieProvider?.InitOfflineDb();
                    }

                    DbUpdater.UpdateStartupDbs();
                    //while (Mainform.IsLoading)
                    //    Application.DoEvents();
                    //MailServer = new EmailServer();
                    //MailServer.Start();
                    IsLoaded = true;
                    if (raiseManagerLoadingEvents)
                        ManagerLoaded();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            });
        }

        private void Opmerkingen_OnOpmerkingenChanged(object sender, EventArgs e)
        {
            OnOpmerkingenChanged();
        }

        #endregion Manager Init


        #endregion "Constructor"

        #region Productie Formulieren

        #endregion Productie Formulieren

        #region "Public Methods"

        //public void StartMonitor()
        //{
        //    if (!_emailcheckTimer.Enabled)
        //        _emailcheckTimer.Start();
        //}

        //public void StopMonitor()
        //{
        //    if (_emailcheckTimer.Enabled)
        //        _emailcheckTimer.Stop();
        //}
        /// <summary>
        /// Maak een random text aan met 8 characters
        /// </summary>
        /// <returns>Een random aangemaakte text met 8 characters</returns>
        public string Get8CharacterRandomString()
        {
            var path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path.Substring(0, 8); // Return 8 character string
        }

        /// <summary>
        /// Een taak voor het aanmaken van een account
        /// </summary>
        /// <param name="account">De account die aangemaakt moet worden</param>
        /// <returns>Een taak dat draait op de achtergrond</returns>
        public static Task<bool> CreateAccount(UserAccount account)
        {
            return Database.UpSert(account);
        }

        public static int UpdateExcelColumns(List<ExcelSettings> settings, bool overwrite, bool removenotexisting, bool isexcel, List<string> exclude = null)
        {
            if (ListLayouts == null || ListLayouts.Disposed) return -1;
            int xreturn = 0;
            foreach (var xset in settings)
            {
                if (!overwrite)
                {
                    bool exist = ListLayouts.Exists(xset.Name);
                    int cur = 1;
                    while (exist)
                    {
                        var xlast = xset.Name.LastIndexOf('[');
                        if (xlast > 0)
                            xset.Name = xset.Name.Substring(0, xlast);
                        xset.Name += $"[{cur++}]";
                        exist = ListLayouts.Exists(xset.Name);
                    }
                }

                bool raiseevent = exclude == null || !exclude.Any(x => xset.IsUsed(x));
                if (ListLayouts.SaveLayout(xset, null, raiseevent))
                    xreturn++;
            }

            if (removenotexisting)
            {
                var xitems = ListLayouts.GetAlleLayouts().Where(x => x.IsExcelSettings == isexcel &&
                    !settings.Any(s => string.Equals(s.Name, x.Name, StringComparison.CurrentCultureIgnoreCase))).ToList();
                foreach (var xitem in xitems)
                    ListLayouts.RemoveLayout(xitem,false);
            }
            return xreturn;
        }

        /// <summary>
            /// Een taak aanmaken voor het inloggen
            /// </summary>
            /// <param name="username">De naam van de gebruiker die ingelogd wilt worden</param>
            /// <param name="password">De wachtwoord dat hoort bij de gebruikersnaam</param>
            /// <param name="autologin">Of de gebruiker voortaan automatch ingelogd moet worden</param>
            /// <param name="sender">De afzender die de inglog taak oproept</param>
            /// <returns>Een taak die draait op de achtergrond</returns>
            public static Task<bool> Login(string username, string password, bool autologin, object sender)
        {
            return Task.Run(async () =>
            {
                if (Manager.Database?.UserAccounts == null)
                    throw new Exception("Database is niet geladen!\n\n" +
                                        "Kan niet inloggen als de database niet is geladen");
                var x = await Database.GetAccount(username);
                if (x != null)
                {
                    if (x.ValidateUser(username, password,true))
                    {
                        var xdef = DefaultSettings;
                        xdef.AutoLoginUsername = autologin ? x.Username : null;
                        xdef.SaveAsDefault();
                        LogedInGebruiker = x;
                        LoginChanged(sender,true,true);
                        return true;
                    }

                    LogedInGebruiker = null;

                    LoginChanged(sender,false,false);
                    return false;
                }

                throw new Exception("Gebruiker bestaat niet!");
            });
        }

        /// <summary>
        /// Een taak aanmaken voor het inloggen
        /// </summary>
        /// <param name="username">De naam van de gebruiker die ingelogd wilt worden</param>
        /// <param name="sender">De afzender die de inglog taak oproept</param>
        /// <returns>Een taak die draait op de achtergrond</returns>
        public static Task<bool> Login(string username, object sender)
        {
            return Task.Factory.StartNew(() =>
            {
                return xLogin(username, sender);
            });
        }

        public static bool xLogin(string username, object sender)
        {
            try
            {
                var x = Database.xGetAccount(username);
                if (x != null)
                {
                    LogedInGebruiker = x;
                    x.OsID = SystemId;
                    Database?.xUpSert(x.Username, x, $"{x.Username} Ingelogd!");
                    LoginChanged(sender,true, true);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Log uit
        /// </summary>
        /// <param name="sender">De afzender die deze taak oproept</param>
        public static void LogOut(object sender, bool raiseevent)
        {
            if (LogedInGebruiker != null)
            {
                LogedInGebruiker = null;
                LoginChanged(sender,true, raiseevent);
            }
        }

        /// <summary>
        /// Een taak voor het achterhalen van de productie waaraan een persoon mee bezig is
        /// </summary>
        /// <param name="personeelnaam"></param>
        /// <returns></returns>
        public Task<List<Bewerking>> Werktaan(string personeelnaam)
        {
            return Task.Run(async () =>
            {
                var pers = await Manager.Database.GetPersoneel(personeelnaam);
                if (pers == null) return new List<Bewerking>();
                var xreturn = pers.Klusjes?.Where(x => x.IsActief && x.Status == ProductieState.Gestart)
                    .Select(x => x.GetWerk()?.Bewerking).ToList()??new List<Bewerking>();
                xreturn.RemoveAll(x => x == null);
                return xreturn;
            });
        }

        /// <summary>
        /// Laad instellingen
        /// </summary>
        /// <param name="sender">De afzender van deze taak</param>
        /// <param name="raiseEvent">True als je een event wilt oproepen dat de instellingen zijn geladen</param>
        /// <returns>Een taak voor het laden van de instellingen</returns>
        public static Task<bool> LoadSettings(object sender, bool raiseEvent)
        {
            return Task.Factory.StartNew(() =>
            {
                return xLoadSettings(sender, raiseEvent);
            });
        }

        public static bool xLoadSettings(object sender, bool raiseEvent)
        {
            if (Database == null || Database.IsDisposed) return false;
            var os = SystemId;
            var id = LogedInGebruiker == null ? $"Default[{os}]" : LogedInGebruiker.Username;
            var optiesid = Opties?.Username != null ? Opties.Username.ToLower().StartsWith("default") ? $"Default[{Opties.SystemID}]" : Opties.Username : null;
            string name = LogedInGebruiker == null ? "Default" : LogedInGebruiker.Username;
            var oldset = Opties;
            //if (Opties != null && !string.Equals(optiesid, id, StringComparison.CurrentCultureIgnoreCase))
            //{
            //    SaveSettings(Opties, false, true, true);
            //}

            try
            {
                //if (Opties != null && string.Equals($"{Opties.Username}[{os}]", id, StringComparison.CurrentCultureIgnoreCase))
                //    return false;
                try
                {
                    UserSettings xt = Database.GetSetting(id);
                    if (xt == null)
                    {
                        xt = (Database.xGetAllSettings()).FirstOrDefault(x =>
                            x.Username.ToLower().StartsWith(name.ToLower()));
                        if (xt != null)
                        {
                            Database.xDeleteSettings($"{xt.Username}[{xt.SystemID}]", false);
                            xt.SystemID = os;
                            xt.Username = name;
                            xt.xSave(null, false, false, false);
                        }
                        else name += "_tmp";
                    }
                    else xt.Username = name;
                    Opties = xt;
                }
                catch (Exception)
                {
                    // ignored
                }

                if (Opties == null)
                {
                    var opties = new UserSettings { Username = name };
                    Database.xUpSert(opties.Username, opties, "Nieuwe instellingen aangemaakt");
                    Opties = opties;
                }

                if (Opties != null)
                {
                    if (Opties.ProductieWeergaveFilters == null || Opties.ProductieWeergaveFilters.Length == 0)
                        Opties.ProductieWeergaveFilters = new[] { ViewState.Alles };
                    if (!Directory.Exists(DbPath))
                        Directory.CreateDirectory(DbPath);
                    bool flag = oldset == null || (!string.Equals(Opties.Username, oldset.Username, StringComparison.CurrentCultureIgnoreCase) || !Opties.xPublicInstancePropertiesEqual(oldset));
                    if (raiseEvent && flag)
                        SettingsChanged(sender, true);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Instellingen opslaan
        /// </summary>
        /// <param name="force">True voor als je hoedan ook wilt opslaan</param>
        /// <returns>True voor als de instellingen zijn opgeslagen</returns>
        public bool SaveSettings(bool force)
        {
            return SaveSettings(Opties, true,true, force);
        }

        /// <summary>
        /// Sla instellingen op
        /// </summary>
        /// <param name="settings">De instellingen om op te slaan</param>
        /// <param name="replace">True voor als je de huidige instellingen wilt vervangen met aangegevn instellingen</param>
        /// <param name="triggersaved">True voor als je de event wilt roepen dat de instellingen zijn opgeslagen</param>
        /// <param name="force">True voor als je wilt opslaan zonder te kijken voor wijzigingen</param>
        /// <returns>True voor als de istellingen zijn opgeslagen</returns>
        public static bool SaveSettings(UserSettings settings, bool replace, bool triggersaved, bool force = false)
        {
            var cancel = false;
            SettingsChanging(null, ref settings, ref cancel);

            try
            {
                if (cancel)
                    return false;
                var changed = Opties == null || force;
                if (settings == null)
                {
                    settings = new UserSettings();
                    var name = LogedInGebruiker == null ? "Default" : LogedInGebruiker.Username;
                    settings.Username = name;
                    changed = true;
                }

                if (!changed)
                    changed = !settings.xPublicInstancePropertiesEqual(Opties, new[] { typeof(UserChange) });

                if (changed)
                {
                    Functions.SetAutoStartOnBoot(settings.StartNaOpstart);
                    var res = settings.xSave();
                }

                if (replace)
                {
                    Opties = settings;
                    if (triggersaved)
                        SettingsChanged(null,true);
                }

                return changed;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Ze de marameters van de productie beheerder vanuit de aangegeven instellingen
        /// </summary>
        /// <param name="options">In instellingen waarvan de parameters overgenomen kunnen worden</param>
        public void SetSettings(UserSettings options)
        {
            if (options == null)
                return;
            Task.Factory.StartNew(() =>
            {
                _afsluittijd = options.AfsluitTijd;
                options.SystemID = SystemId;
                var bkms = TimeSpan.FromHours(0.10).TotalMilliseconds;
                SyncInterval = options.SyncInterval < 10000 ? 10000 : options.SyncInterval;
                options.BackupInterval = options.BackupInterval < bkms ? bkms : options.BackupInterval;
                if (options.CreateBackup)
                    BackupInfo?.StartBackupSyncer();
                //MailInterval = options.MailSyncInterval < 10000 ? 10000 : options.MailSyncInterval;
                if (Database != null)
                    Database.NotificationEnabled = options.ToonLogNotificatie;
              
               
                if (options.GebruikLocalSync || options.GebruikTaken)
                {
                    ProductieProvider?.StartSyncProducties();
                }

                if (options.DbUpdateEntries != null)
                {
                    foreach (var ent in Opties.DbUpdateEntries)
                        ent.LastUpdated = DateTime.MinValue;
                }

                if (options.ExcelColumns is {Count: > 0})
                {
                    UpdateExcelColumns(options.ExcelColumns, false,false,false);
                }
                //load files sync instances
                LoadSyncDirectories();
                DbUpdater?.Start(DefaultSettings);
               
            });
            _syncTimer?.Start();
            _overzichtSyncTimer?.Stop();
            if (_overzichtSyncTimer != null && options.CreateWeekOverzichten)
            {
                _overzichtSyncTimer.Interval = options.WeekOverzichtUpdateInterval;
                _overzichtSyncTimer?.Start();
            }
            //if (!options.GebruikTaken)
            //    Taken?.StopBeheer();
            //else Taken?.StartBeheer();
            //UpdateFormOldDbProducties();
            // Manager.Database.PersoneelLijst.DeleteAll();
            // Manager.Database.LiteDatabase.Rebuild();
            //InitPersoneel();
        }

        public static Task<int> UpdateFormulierenFromDirectory(string directory, bool addifnotexists, ProgressChangedHandler changedhandler = null)
        {
            return Task.Run(() =>
            {
                int xreturn = 0;
                try
                {
                    var xprog = new ProgressArg();
                    xprog.Type = ProgressType.WriteBussy;

                    if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory)) return 0;
                    xprog.Message = $"Bestanden verzamelen...";
                    changedhandler?.Invoke(null, xprog);
                    var files = Directory.GetFiles(directory);
                    int count = 0;
                    foreach (var file in files)
                    {
                        xprog.Progress = (int)(((double)count / files.Length) * 100);
                        xprog.Message = $"{Path.GetFileNameWithoutExtension(file)} Updaten ({count}/{files.Length})...";
                        changedhandler?.Invoke(null, xprog);
                        if (xprog.IsCanceled) break;
                        var prods = ProductieFormulier.xFromPdf(File.ReadAllBytes(file));
                        if (prods == null || prods.Count == 0) continue;

                        foreach (var prod in prods)
                        {

                            changedhandler?.Invoke(null, xprog);
                            if (xprog.IsCanceled) break;
                            var xold = Database?.GetProductie(prod.ProductieNr, true);
                            if (xold == null)
                            {
                                if (addifnotexists)
                                {
                                    if (xAddProductie(prod, true).Action == MessageAction.NieweProductie)
                                        xreturn++;
                                }

                                continue;
                            }

                            if (xold.xUpdateFieldsFrom(prod) && Manager.Database.xUpSert(xold.ProductieNr, xold, null, false))
                            {
                                xreturn++;
                            }
                        }

                        count++;
                    }

                    var x1 = xreturn == 1 ? "bestand" : "bestanden";
                    xprog.Type = ProgressType.WriteCompleet;
                    xprog.Message = $"{xreturn} {x1} geüpdatet";
                    xprog.Progress = 100;
                    changedhandler?.Invoke(null, xprog);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return xreturn;
            });
        }

        /// <summary>
        /// Verkrijg alle producties
        /// </summary>
        /// <param name="states">Verkrijg producties op basis van de gekozen status lijst</param>
        /// <param name="filter">true als je wilt dat de producties worden gefiltered volgens een geldige bewerking</param>
        /// <param name="incform">true als de productie ook die aangegeven status moet zijn, false je alleen wilt verkrijgen op basis van een geldige bewerking</param>
        /// <param name="handler"></param>
        /// <returns>Lijst van productieformulieren</returns>
        public static Task<List<ProductieFormulier>> GetProducties(ViewState[] states, bool filter, bool incform, IsValidHandler handler, bool checksecondary)
        {
            return Task.Factory.StartNew(() =>
            {
                return xGetProducties(states, filter, incform, handler, checksecondary);
            });
        }

        public static List<ProductieFormulier> xGetProducties(ViewState[] states, bool filter, bool incform, IsValidHandler handler, bool checksecondary)
        {
                if (ProductieProvider == null) return new List<ProductieFormulier>();
                var type = ProductieProvider.LoadedType.None;
                if (states.Any(x => x == ViewState.Gereed))
                    type = ProductieProvider.LoadedType.Gereed;
                if (states.Any(x => x != ViewState.Gereed))
                {
                    type = type == ProductieProvider.LoadedType.Gereed
                        ? ProductieProvider.LoadedType.Alles
                        : ProductieProvider.LoadedType.Producties;
                }

                var xitems = ProductieProvider.xGetProducties(type, filter, handler, checksecondary);
                return xitems;
        }

        /// <summary>
        /// Verkrijg alle bewerkingen die voldoen aan de argumenten
        /// </summary>
        /// <param name="states">Een lijst van de states waarvan de bewerkingen aan moeten voldoen</param>
        /// <param name="filter">True voor als je de bewerkingen wilt laten filteren volgens de basis filter instellingen</param>
        /// <param name="handler"></param>
        /// <returns>Een taak die op de achtergrond de bewerkinglijst samen stelt</returns>
        public static Task<List<Bewerking>> GetBewerkingen(ViewState[] states, bool filter, bool checksecondary, IsValidHandler handler = null)
        {
            return Task.Factory.StartNew(() => xGetBewerkingen(states, filter, checksecondary, handler));
        }

        public static List<Bewerking> xGetBewerkingen(ViewState[] states, bool filter, bool checksecondary, IsValidHandler handler = null)
        {
            if (ProductieProvider == null) return new List<Bewerking>();
            var type = ProductieProvider.LoadedType.Producties;
            if (states.Any(x => x is ViewState.Gereed))
                type = ProductieProvider.LoadedType.Gereed;
            if (states.Any(x => x is ViewState.Alles))
                type = ProductieProvider.LoadedType.Alles;
            return ProductieProvider.xGetBewerkingen(type, states, filter, handler, checksecondary);
        }

        /// <summary>
        /// Verkrijg alle productie Id's
        /// </summary>
        /// <param name="incgereed">True voor als je ook de gereed producties wilt verkrijgen</param>
        /// <param name="checksecondary">True voor als je wilt verkrijgen vanuit de locale database (als die bestaat)</param>
        /// <returns>Een taak die de Id's verkrijgt op de achtergrond</returns>
        public static Task<List<string>> GetAllProductieIDs(bool incgereed, bool checksecondary)
        {
            return Task.Factory.StartNew(() =>
            {
                return xGetAllProductieIDs(incgereed, checksecondary);
            });
        }

        public static List<string> xGetAllProductieIDs(bool incgereed, bool checksecondary)
        {
            if (Database?.ProductieFormulieren == null || Database.IsDisposed)
                return new List<string>();
            var xreturn = new List<string>();
            try
            {
                var xitems = Database.ProductieFormulieren.GetAllIDs(checksecondary);
                if (xitems is { Count: > 0 })
                    xreturn.AddRange(xitems);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (!incgereed) return xreturn;
            {
                try
                {
                    var gereed = Database.GereedFormulieren.GetAllIDs(true);
                    if (gereed is { Count: > 0 })
                        xreturn.AddRange(gereed);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return xreturn;
        }

        /// <summary>
        /// Verkrijg alle productie locaties
        /// </summary>
        /// <param name="incgereed">True voor als je ook de gereed producties wilt verkrijgen</param>
        /// <param name="checksecondary">True voor als je wilt verkrijgen vanuit de locale database (als die bestaat)</param>
        /// <returns>Een taak die de locaties verkrijgt op de achtergrond</returns>
        public static Task<List<string>> GetAllProductiePaths(bool incgereed, bool checksecondary)
        {
            return Task.Factory.StartNew(() =>
            {
                return xGetAllProductiePaths(incgereed, checksecondary);
            });
        }

        public static List<string> xGetAllProductiePaths(bool incgereed, bool checksecondary)
        {
            if (Database?.ProductieFormulieren == null || Database.IsDisposed)
                return new List<string>();
            var xreturn = new List<string>();
            try
            {
                var xitems = Database.ProductieFormulieren.GetAllPaths(checksecondary);
                if (xitems is { Count: > 0 })
                    xreturn.AddRange(xitems);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (!incgereed) return xreturn;
            {
                try
                {
                    var gereed = Database.GereedFormulieren.GetAllPaths(checksecondary);
                    if (gereed is { Count: > 0 })
                        xreturn.AddRange(gereed);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return xreturn;
        }

        /// <summary>
        /// Verkrijg producties die voldoen aan de argumenten
        /// </summary>
        /// <param name="state">De staat waaraan de producties moeten voldoen</param>
        /// <param name="filter">True voor als je wilt filteren vanuit de basis instellingen</param>
        /// <param name="incform">True voor als de filter ook moet gelden voor de ProductieFormulier i.p.v alleen de bewerking</param>
        /// <param name="checksecondary"></param>
        /// <returns>Een taak die op de achtergrond de productieformulieren verkrijgt</returns>
        public static Task<List<ProductieFormulier>> GetProducties(ViewState state, bool filter, bool incform, bool checksecondary)
        {
            return GetProducties(new[] {state}, filter, incform,null, checksecondary);
        }

        /// <summary>
        /// Een taak voor het kijken naar producties waarvan er openstaande onderbrekingen zijn
        /// </summary>
        /// <returns>Een taak die opp de achtergrond kijkt of er onderbroken producties zijn</returns>
        public static Task<bool> ContainsOnderbrokenProductie()
        {
            return Task.Run(() =>
            {
                var prods = xGetProducties(new[] {ViewState.Gestart, ViewState.Gestopt}, true, false,null,true);
                return prods.Any(x => x.IsOnderbroken());
            });
        }

        /// <summary>
        /// Roep op een event dat aangeeft dat de database is gewijzigd
        /// </summary>
        /// <returns></returns>
        public static bool ProductiesChanged()
        {
            ProductiesChanged(Database);
            return true;
        }

        /// <summary>
        /// Voeg toe een nieuwe productieformulier
        /// </summary>
        /// <param name="prod">De productieformulier om toe te voegen</param>
        /// <param name="updateifexist"></param>
        /// <returns>Een taak die op de achtergrond een productie toevoegd</returns>
        public static Task<RemoteMessage> AddProductie(ProductieFormulier prod, bool updateifexist)
        {
            return Task.Factory.StartNew(() =>
            {
                return xAddProductie(prod, updateifexist);
            });
        }

        public static RemoteMessage xAddProductie(ProductieFormulier prod, bool updateifexist)
        {
            try
            {
                ProductieFormulier xprod = null;
                if (Database.Exist(prod))
                {
                    xprod = Database.GetProductie(prod.ProductieNr, false);
                    if (xprod == null)
                        return new RemoteMessage("Er is iets fout gegaan bij toevoegen van een Productie",
                            MessageAction.AlgemeneMelding, MsgType.Fout);
                    if (updateifexist || (xprod.Bewerkingen == null || xprod.Bewerkingen.Length == 0))
                    {
                        if (xprod.xUpdateFieldsFrom(prod) && Database.xUpSert(xprod.ProductieNr, xprod,
                            $"[{xprod.ArtikelNr}, {xprod.ProductieNr}] geupdate!", false))
                        {
                            return new RemoteMessage(
                                $"Productie [{prod.ProductieNr}, {prod.ArtikelNr}] Bestaat al, maar is wel geupdate!",
                                MessageAction.ProductieWijziging, MsgType.Waarschuwing, null, prod,
                                prod.ProductieNr);
                        }
                        else
                            return new RemoteMessage(
                                $"Productie [{prod.ProductieNr}, {xprod.ProductieNr}] bestaat al, en kan niet nogmaals worden toegevoegd!",
                                MessageAction.ProductieWijziging, MsgType.Waarschuwing, null, prod,
                                prod.ProductieNr);
                    }
                    else
                        return new RemoteMessage(
                            $"Productie [{prod.ProductieNr}] bestaat al, en kan niet nogmaals worden toegevoegd!",
                            MessageAction.ProductieWijziging, MsgType.Waarschuwing, null, prod, prod.ProductieNr);

                }
                var xa = prod.Aantal == 1 ? "stuk" : "stuks";
                // prod = await ProductieFormulier.UpdateDoorloopTijd(prod);
                try
                {
                    if (prod.VerpakkingsInstructies != null && Verpakkingen is { Disposed: false })
                    {
                        var xver = Verpakkingen.GetVerpakking(prod.ArtikelNr);
                        if (xver != null)
                        {
                            bool thesame = xver.CompareTo(prod.VerpakkingsInstructies);
                            if (!thesame)
                            {
                                prod.VerpakkingsInstructies = xver;
                            }
                            else Verpakkingen.RemoveVerpakking(prod.ArtikelNr);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                if (!Database.xUpSert(prod.ProductieNr, prod,
                   $"[{prod.ArtikelNr}, {prod.ProductieNr}] Nieuwe productie toegevoegd({prod.Aantal} {xa}) met doorlooptijd van {prod.DoorloopTijd} uur."))
                    return new RemoteMessage($"Het is niet gelukt om {prod.ProductieNr} toe te voegen!",
                        MessageAction.None,
                        MsgType.Fout, null, prod, prod.ProductieNr);
                xprod = Database.GetProductie(prod.ProductieNr, true);
                if (xprod != null)
                {
                    prod = xprod;
                    _ = ProductieFormulier.UpdateDoorloopTijd(null, prod, "", false, true, false);

                    return new RemoteMessage($"{prod.ProductieNr} toegevoegd!", MessageAction.AlgemeneMelding,
                        MsgType.Success, null, prod, prod.ProductieNr);
                }
                return new RemoteMessage($"Het is niet gelukt om {prod.ProductieNr} toe te voegen!",
                    MessageAction.None,
                    MsgType.Fout, null, prod, prod.ProductieNr);
            }
            catch (Exception ex)
            {
                return new RemoteMessage(ex.Message, MessageAction.None,
                    MsgType.Fout);
            }
        }

        /// <summary>
        /// Voeg een nieuwe productie toe vanuit een bestand
        /// </summary>
        /// <param name="pdffile">De pdf bestand die teoegevoegd zou moeten worden</param>
        /// <param name="updateifexist">True voor als je een productie wilt updaten als die bestaat</param>
        /// <param name="delete">True voor als je het bestand wilt verwijderen zodra het is toegevoegd</param>
        /// <returns>Een taak die een productie toevoegd op de achtergrond</returns>
        public static Task<List<RemoteMessage>> AddProductie(string pdffile, bool updateifexist, bool delete)
        {
            return Task.Run(async () =>
            {
                var rms = new List<RemoteMessage>();
                try
                {
                    if (!File.Exists(pdffile)) return rms;
                    var prods = await ProductieFormulier.FromPdf(File.ReadAllBytes(pdffile));
                    if (prods == null || prods.Count == 0)
                        rms.Add(new RemoteMessage($"{Path.GetFileName(pdffile)} is geen geldige productieformulier!",
                            MessageAction.None, MsgType.Fout));
                    else
                        foreach (var prod in prods)
                        {
                            RemoteMessage msg;
                            if (prod.IsAllowed(null))
                            {
                                msg = await AddProductie(prod, updateifexist);
                                try
                                {
                                    if (msg.Value is ProductieFormulier xprod)
                                    {

                                        var bew = xprod.Bewerkingen?.FirstOrDefault(x => x.IsAllowed());
                                        if (Opties is {ToonProductieNaToevoegen: true} && bew != null)
                                            FormulierActie(new object[] {xprod, bew}, MainAktie.OpenProductie);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                            }
                            else
                                msg = new RemoteMessage(
                                    $"[{prod.ProductieNr}, {prod.ArtikelNr}]Kan niet toevoegen omdat de bewerking is gefilterd!",
                                    MessageAction.AlgemeneMelding, MsgType.Info);

                            rms.Add(msg);
                            try
                            {
                                string fpath = Path.Combine(ProductieFormPath, $"{prod.ProductieNr}.pdf");
                                if (!string.Equals(fpath, pdffile, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    for (int i = 0; i < 5; i++)
                                    {
                                        try
                                        {
                                            if (delete)
                                            {

                                                if (File.Exists(fpath))
                                                    File.Delete(pdffile);
                                                else
                                                    File.Move(pdffile, fpath);
                                            }
                                            else
                                                File.Copy(pdffile, fpath, true);

                                            if (File.Exists(fpath)) break;
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e);
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            // pdffile.CleanupFilePath(ProductieFormPath, prod.ProductieNr, false,false);

                            if (delete && Opties is
                            {
                                VerwijderVerwerkteBestanden: true
                            } && File.Exists(pdffile))
                            {
                                try
                                {
                                    File.Delete(pdffile);
                                }
                                catch
                                {
                                    // ignored
                                }
                            }
                        }
                }
                catch (Exception ex)
                {
                    rms.Add(new RemoteMessage(ex.Message, MessageAction.None,
                        MsgType.Fout));
                }

                return rms;
            });
        }

        public static List<RemoteMessage> xAddProductie(string pdffile, bool updateifexist, bool delete)
        {
            var rms = new List<RemoteMessage>();
            try
            {
                if (!File.Exists(pdffile)) return rms;
                var prods = ProductieFormulier.xFromPdf(File.ReadAllBytes(pdffile));
                if (prods == null || prods.Count == 0)
                    rms.Add(new RemoteMessage($"{Path.GetFileName(pdffile)} is geen geldige productieformulier!",
                        MessageAction.None, MsgType.Fout));
                else
                    foreach (var prod in prods)
                    {
                        RemoteMessage msg;
                        if (prod.IsAllowed(null))
                        {
                            msg = xAddProductie(prod, updateifexist);
                            try
                            {
                                if (msg.Value is ProductieFormulier xprod)
                                {

                                    var bew = xprod.Bewerkingen?.FirstOrDefault(x => x.IsAllowed());
                                    if (Opties is { ToonProductieNaToevoegen: true } && bew != null)
                                        FormulierActie(new object[] { xprod, bew }, MainAktie.OpenProductie);
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                        else
                            msg = new RemoteMessage(
                                $"[{prod.ProductieNr}, {prod.ArtikelNr}]Kan niet toevoegen omdat de bewerking is gefilterd!",
                                MessageAction.AlgemeneMelding, MsgType.Info);

                        rms.Add(msg);
                        try
                        {
                            string fpath = Path.Combine(ProductieFormPath, $"{prod.ProductieNr}.pdf");

                            for (int i = 0; i < 5; i++)
                            {
                                try
                                {
                                    File.Copy(pdffile, fpath, true);
                                    if (delete && File.Exists(pdffile))
                                    {
                                        File.Delete(pdffile);
                                    }

                                    if (File.Exists(fpath)) break;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        // pdffile.CleanupFilePath(ProductieFormPath, prod.ProductieNr, false,false);

                        if (delete && Opties is
                            {
                                VerwijderVerwerkteBestanden: true
                            } && File.Exists(pdffile))
                        {
                            try
                            {
                                File.Delete(pdffile);
                            }
                            catch
                            {
                                // ignored
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                rms.Add(new RemoteMessage(ex.Message, MessageAction.None,
                    MsgType.Fout));
            }

            return rms;
        }

        /// <summary>
        /// Voeg meerdere bestanden toe
        /// </summary>
        /// <param name="pdffiles">Een reeks pdf bestanden die toegevoegd moeten worden</param>
        /// <param name="updateifexist"></param>
        /// <param name="delete">True voor als je de bestanden wilt verwijderen zodra ze zijn toegevoegd</param>
        /// <param name="showerror">True voor als je een foutmelding wilt laten zien</param>
        /// <returns></returns>
        public static Task<int> AddProductie(string[] pdffiles, bool updateifexist, bool delete, bool showerror)
        {
            return Task.Factory.StartNew(() =>
            {
                return xAddProductie(pdffiles, updateifexist, delete, showerror);
            });
        }

        public static int xAddProductie(string[] pdffiles, bool updateifexist, bool delete, bool showerror)
        {
            if (pdffiles?.Length == 0) return 0;
            var xreturn = 0;
            if (pdffiles != null)
                foreach (var file in pdffiles)
                    try
                    {
                        var msgs = xAddProductie(file, updateifexist, delete);
                        foreach (var msg in msgs)
                        {
                            if (msg.MessageType == MsgType.Fout && !showerror)
                                continue;
                            if (msg.MessageType == MsgType.Success) xreturn++;
                            RemoteMessage(msg);
                        }
                    }
                    catch
                    {
                        // ignored
                    }

            return xreturn;
        }

        /// <summary>
        /// Voeg meerdere productieformulieren toe
        /// </summary>
        /// <param name="prods">De productieformulieren om toe te voegen</param>
        /// <returns>Een taak die de productieformulieren op de achtergrond toevoegd</returns>
        public Task<List<RemoteMessage>> AddProducties(ProductieFormulier[] prods, bool updateifexist)
        {
            return Task.Run(async () =>
            {
                var msgs = new List<RemoteMessage>();
                try
                {
                    if (prods == null || prods.Length == 0)
                        throw new Exception("Geen geldige productieformulieren om toe te voegen!");
                    foreach (var prod in prods)
                    {
                        var msg = await AddProductie(prod, updateifexist);
                        msgs.Add(msg);
                    }
                }
                catch (Exception ex)
                {
                    msgs.Add(new RemoteMessage(ex.Message, MessageAction.None,
                        MsgType.Fout));
                }

                return msgs;
            });
        }

        /// <summary>
        /// Verwijder een productie
        /// </summary>
        /// <param name="prod">De productie om te verwijderen</param>
        /// <param name="realremove">True voor als je de productie helmaal wilt verwijderen</param>
        /// <returns>Een taak die de productie verwijderd op de achtergrond</returns>
        public static Task<bool> RemoveProductie(ProductieFormulier prod, bool realremove)
        {
            return RemoveProductie(prod.ProductieNr, realremove);
        }

        /// <summary>
        /// Verwijder meerdere producties
        /// </summary>
        /// <param name="prods">De producties om te verwijderen</param>
        /// <param name="realremove">True voor als de producties helemaal wilt verwijderen</param>
        /// <returns>Een taak die de producties verwijderd op de achtergrond</returns>
        public static Task<int> RemoveProducties(ProductieFormulier[] prods, bool realremove)
        {
            return Task.Run(async () =>
            {
                try
                {
                    if (prods == null || prods.Length == 0)
                        return 0;
                    var removed = 0;
                    var remove = new List<ProductieFormulier>();

                    foreach (var prod in prods)
                    {
                        if (prod.State == ProductieState.Verwijderd && realremove)
                            remove.Add(prod);
                        else
                        {
                            if (prod.Bewerkingen != null)
                                foreach (var b in prod.Bewerkingen)
                                {
                                    if (b.State == ProductieState.Gestart)
                                        await b.StopProductie(true, false, true);
                                    b.State = ProductieState.Verwijderd;
                                    b.DatumVerwijderd = DateTime.Now;
                                }

                            if (prod.State != ProductieState.Gestart && prod.State != ProductieState.Verwijderd)
                            {
                                prod.State = ProductieState.Verwijderd;
                                prod.DatumVerwijderd = DateTime.Now;
                                string change = $"Productie [{prod.ProductieNr.ToUpper()}] is zojuist verwijderd";
                                if (await Database.UpSert(prod, change))
                                {
                                    removed++;
                                    RemoteProductie.RespondByEmail(prod, change);
                                }
                            }
                        }
                    }

                    if (remove.Count > 0) removed += await Database.Delete(remove.ToArray());
                    return removed;
                }
                catch
                {
                    return -1;
                }
            });
        }

        /// <summary>
        /// Verwijder producties d.m.v een productienr
        /// </summary>
        /// <param name="prodnr">De productienr van de productie die verwijderd moet worden</param>
        /// <param name="realremove">True voor als je de productie helemaal wilt verwijderen</param>
        /// <returns>Een taak die de productie verwijderd op de achtergrond</returns>
        public static Task<bool> RemoveProductie(string prodnr, bool realremove)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var current = Database.GetProductie(prodnr, true);
                    var removed = false;
                    if (current != null)
                    {
                        if (realremove && current.State == ProductieState.Verwijderd)
                        {
                            if (await Database.Delete(current))
                            {
                                var personeel = current.Personen;
                                if (personeel.Length > 0)
                                {
                                    foreach (var pers in personeel)
                                    {
                                        var xdbpers = await Manager.Database.GetPersoneel(pers.PersoneelNaam);
                                        if (xdbpers == null) continue;
                                        if (xdbpers.IngezetAanKlus(current.ProductieNr, false, out var klusjes))
                                        {
                                            foreach (var klus in klusjes)
                                                xdbpers.Klusjes.Remove(klus);
                                            await Manager.Database.UpSert(xdbpers, $"{xdbpers.PersoneelNaam} Klusjes verwijderd");
                                        }
                                    }
                                }
                                removed = true;
                                RemoteProductie.RespondByEmail(current,
                                    $"Productie [{current.ArtikelNr}|{current.ProductieNr.ToUpper()}] is zojuist helemaal verwijderd uit de Database!");
                            }
                        }
                        else if (current.State != ProductieState.Verwijderd)
                        {
                            if (current.Bewerkingen != null)
                                foreach (var b in current.Bewerkingen)
                                {
                                    if (b.State == ProductieState.Gestart)
                                        await b.StopProductie(true,false,true);
                                    b.State = ProductieState.Verwijderd;
                                    b.DatumVerwijderd = DateTime.Now;
                                    var personeel = b.Personen;
                                    if (personeel.Length > 0)
                                    {
                                        foreach (var pers in personeel)
                                        {
                                            var xdbpers = await Manager.Database.GetPersoneel(pers.PersoneelNaam);
                                            if (xdbpers == null) continue;
                                            if (xdbpers.IngezetAanKlus(current.ProductieNr, false, out var klusjes))
                                            {
                                                foreach (var klus in klusjes)
                                                {
                                                    klus.Stop();
                                                    klus.Status = ProductieState.Verwijderd;
                                                }
                                                await Manager.Database.UpSert(xdbpers, $"{xdbpers.PersoneelNaam} Klusjes verwijderd");
                                            }
                                        }
                                    }
                                }

                            current.DatumVerwijderd = DateTime.Now;
                            current.State = ProductieState.Verwijderd;
                            string change =
                                "Productie [{current.ArtikelNr}|{current.ProductieNr.ToUpper()}] is zojuist verwijderd";
                            if (await Database.UpSert(current,change))
                            {
                                removed = true;
                                RemoteProductie.RespondByEmail(current,change);
                            }
                        }
                    }

                    return removed;
                }
                catch
                {
                    return false;
                }
            });
        }

        /// <summary>
        /// Vervang een productie
        /// </summary>
        /// <param name="oldform">De oude productie die je wilt vervangen</param>
        /// <param name="newform">De nieuwe die inplaats komt</param>
        /// <returns>Een taak die de productie vervangt op de achtergrond</returns>
        public Task<bool> ReplaceProductie(ProductieFormulier oldform, ProductieFormulier newform)
        {
            return Task.Run(async () =>
            {
                if (oldform == null || newform == null)
                    return false;
                return await Database.Replace(oldform, newform);
            });
        }

        /// <summary>
        /// Verkrijg de aantal producties die gemaakt zijn of worden met de opgegeven artikel nummer
        /// </summary>
        /// <param name="artikelnr">De artikel nummer van het product die gemaakt is</param>
        /// <returns>Een taak die de aantal geproduceerde producties telt op de achtergrond</returns>
        public Task<int> AantalProductiesGemaakt(string artikelnr)
        {
            return Task.Run(async () =>
            {
                var forms = await Database.GetProducties(artikelnr, true, ProductieState.Gereed, true);
                var xreturn = forms.Count;
                return xreturn;
            });
        }

        /// <summary>
        /// Verkrijg de werkplekken vanuit de database op basis van een bewerking naam
        /// </summary>
        /// <param name="bewerking">De bewerking naam waarvan je de werkplekken wilt verkrijgen</param>
        /// <returns>Een reeks werkplekken</returns>
        public static string[] GetWerkplekken(string bewerking)
        {
            var realname = bewerking.Split('[')[0];
            var xs = BewerkingenLijst.GetEntry(realname);
            if (xs != null)
                return xs.WerkPlekken.ToArray();
            return new string[] { };
        }

        /// <summary>
        /// Een taak voor het updaten van de productie roosters.
        /// Er wordt dan gekeken of er speciale roosters zijn.
        /// </summary>
        /// <returns>Een taak die de productie roosters update op de achtergrond</returns>
        public static Task<int> UpdateGestarteProductieRoosters(List<WerkPlek> werkplekken, Rooster rooster)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    int done = 0;
                    var acces1 = LogedInGebruiker is {AccesLevel: >= AccesType.ProductieBasis};
                    if (!acces1) return -1;
                        foreach (var wp in werkplekken)
                        {
                            var bw = wp.Werk;
                            if (!wp.IsActief() || !bw.IsAllowed() || bw.State != ProductieState.Gestart) continue;

                            wp.UpdateWerkRooster(rooster.CreateCopy(), true, true, true, true, false, true, true);
                        
                            if (bw.xUpdateBewerking(null,
                                    $"[{wp.Path}] Werkrooster aangepast"))
                                done++;
                        }

                        return done;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return -1;
                }
            });
        }

        #endregion "Public Methods"

        #region "Private Methods"
        private bool LoadSyncDirectories()
        {
            try
            {
                _fileWatchers ??= new List<FileSystemWatcher>();
                if (Opties.SyncLocaties is {Length: > 0})
                {
                    foreach (var s in Opties.SyncLocaties)
                    {
                        if (!Directory.Exists(s))
                            continue;
                        if (_fileWatchers.Any(t => string.Equals(t.Path, s, StringComparison.CurrentCultureIgnoreCase))) continue;
                        var sw = new FileSystemWatcher(s);
                        sw.EnableRaisingEvents = true;
                        sw.Changed += Sw_Created;
                        sw.Created += Sw_Created;
                        sw.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName |
                                          NotifyFilters.LastWrite | NotifyFilters.CreationTime;
                        sw.Filter = "*.pdf";

                        _fileWatchers.Add(sw);
                    }

                    for (int i = 0; i < _fileWatchers.Count; i++)
                    {
                        var fs = _fileWatchers[i];
                        if (!Opties.SyncLocaties.Contains(fs.Path))
                        {
                            fs.Dispose();
                            _fileWatchers.RemoveAt(i--);
                        }
                    }

                    LoadUnloadedFiles();
                }
                else
                {
                    foreach (var v in _fileWatchers)
                        v.Dispose();
                    _fileWatchers.Clear();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Backup opgegeven bestanden in de opgegeven locatie
        /// </summary>
        /// <param name="files">Bestanden waarvan er een beackup gemaakt moet worden</param>
        /// <param name="path">De lokatie waar de bestanden moeten worden gebackuped</param>
        /// <returns>True voor als de bestanden zijn gebackuped</returns>
        public static bool BackupFiles(string[] files, string path)
        {
            if (files == null || files.Length == 0)
                return false;
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                foreach (var file in files)
                {
                    var filename = Path.GetFileName(file);
                    var ext = Path.GetExtension(file);
                    var count = Directory.GetFiles(path, "*.db").Count(x =>
                        string.Equals(Path.GetFileName(x).Replace(ext, "").Split('[')[0], filename.Replace(ext, ""),
                            StringComparison.CurrentCultureIgnoreCase));
                    if (count > 0)
                        filename = $"{filename.Replace(ext, "")}[{count}]";
                    filename += ext;
                    if (File.Exists(file))
                        File.Copy(file, path + "\\" + filename, true);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool _isLoadingFiles;

        private void LoadUnloadedFiles()
        {
            try
            {
                if (_isLoadingFiles) return;
                _isLoadingFiles = true;
                var files = new List<string>();
                if (Opties?.SyncLocaties != null)
                {
                    foreach (var s in Opties.SyncLocaties)
                    {
                        if (Directory.Exists(s))
                            files.AddRange(Directory.GetFiles(s, "*.pdf").Where(t => !t.Contains("_old")).ToArray());
                    }
                    if (files.Count > 0)
                    {
                        AddProductie(files.ToArray(),false, true, false);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _isLoadingFiles = false;
        }

        private static Task<bool> AutoLogin(object sender)
        {
            return Task.Factory.StartNew(() =>
            {
                return xAutoLogin(sender);
            });
        }

        private static bool xAutoLogin(object sender)
        {
            try
            {
                if (LogedInGebruiker != null) return false;
                //var pcid = CpuID.ProcessorId();
                var xdef = Manager.DefaultSettings ??= UserSettings.GetDefaultSettings();
                if (!string.IsNullOrEmpty(xdef.AutoLoginUsername))
                    return xLogin(xdef.AutoLoginUsername, sender);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;
        }

        #region Timers & Auto Productie detection

        private List<string> _filestoadd = new();
        private System.Timers.Timer _filesaddertimer;
        private bool _isbusy;

        private void Sw_Created(object sender, FileSystemEventArgs e)
        {
            if (_isbusy)
                return;
            _isbusy = true;
            _filesaddertimer?.Stop();
            _filesaddertimer?.Dispose();
            _filesaddertimer = new System.Timers.Timer {Interval = 1000};
            _filesaddertimer.Elapsed += _filesaddertimer_Tick;
            if (!_filestoadd.Any(x => string.Equals(x, e.FullPath)))
            {
                _filestoadd.Add(e.FullPath);
                _filesaddertimer.Start();
            }
        }

        private void _filesaddertimer_Tick(object sender, EventArgs e)
        {
            _filesaddertimer?.Stop();
            if (_filestoadd?.Count > 0)
            {
                xAddProductie(_filestoadd.ToArray(), true, true, false);
                _filestoadd.Clear();
            }
            _isbusy = false;
        }
        

        private static DateTime _lastchecked = default;

        public static Task CheckForAantalChange()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    var form = Application.OpenForms["AantalGemaaktProducties"];
                    if (form == null && Database is {IsDisposed: false} && Manager.LogedInGebruiker != null && Manager.Opties != null)
                    {
                        List<Bewerking> bws = new List<Bewerking>();
                        int mins = Opties.MinVoorControle;
                        if (_lastchecked.AddMinutes(5) < DateTime.Now)
                        {

                            // Task.Factory.StartNew(() =>
                            //{
                            bws = Database.xGetBewerkingen(ViewState.Gestart, true, null, null, false);

                            var rooster = Opties.GetWerkRooster();
                            var einddag = DateTime.Now.Date.Add(rooster.EindWerkdag);

                            if (DateTime.Now.AddMinutes(15) >= einddag && DateTime.Now < einddag)
                            {
                                mins = 15;
                            }

                            bws = bws.Where(x => string.Equals(
                                                     x.GestartDoor, Manager.Opties.Username,
                                                     StringComparison.CurrentCultureIgnoreCase) &&
                                                 x.WerkPlekken.Any(w => w.NeedsAantalUpdate(mins))).ToList();
                            if (bws.Count > 0)
                            {
                                FormulierActie(new object[] {bws, mins}, MainAktie.OpenAantalGemaaktProducties);
                                _lastchecked = DateTime.Now;
                            }
                        }



                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }

        private async void _syncTimer_Tick(object sender, EventArgs e)
        {
            _syncTimer.Stop();
            try
            {
                if (Opties is {SluitPcAf: true} && DateTime.Now.TimeOfDay >= _afsluittijd)
                {
                    var tijd = new TimeSpan();
                    if (Shutdown(ref tijd) == DialogResult.OK)
                    {
                        _afsluittijd = DateTime.Now.TimeOfDay.Add(tijd);
                        if (tijd.TotalMinutes == 0)
                        {
                            Process.Start("shutdown", "/s /t 0");
                            Application.Exit();
                        }
                    }
                }
                else
                {
                    if (ProductieProvider.FolderSynchronization is { Syncing: false })
                        ProductieProvider?.InitOfflineDb();

                    await CheckForAantalChange();
                }
            }
            catch (Exception s)
            {
                Console.WriteLine(s);
            }

            _syncTimer.Start();
        }

        private bool _isCreatingOverzicht;
        private async void _overzichtSyncTimer_Tick(object sender, EventArgs e)
        {
            _overzichtSyncTimer.Stop();
            if (_isCreatingOverzicht) return;
            _isCreatingOverzicht = true;
            //laten we controleren naar de instellingen en daar na handelen
            if (Opties is {CreateWeekOverzichten: true})
                await ExcelWorkbook.UpdateWeekOverzichten();
            _isCreatingOverzicht = false;
            _overzichtSyncTimer.Start();
        }

        #endregion Timers & Auto Productie detection
        private void _tasks_OnRunInstanceComplete(object sender, EventArgs e)
        {
            if (sender is not IQueue {Results: {Count: > 0}} instance) return;
            foreach (var rm in instance.Results)
                RemoteMessage(rm);
        }

        private void _tasks_OnRunComplete()
        {
        }

        #endregion "Private Methods"

        #region "Events"

        public static event EventHandler OpmerkingenChanged;
        private static void OnOpmerkingenChanged()
        {
            if (!AllowEvents) return;
            OpmerkingenChanged?.Invoke(Opmerkingen, EventArgs.Empty);
        }
        /// <summary>
        /// Een event om een dialog te verzoeken vanuit de beheerder
        /// </summary>
        public static event RequestRespondDialogHandler RequestRespondDialog;
        /// <summary>
        /// Een event om een formulierwijziging door te geven
        /// </summary>
        public static event FormulierChangedHandler OnFormulierChanged;
        /// <summary>
        /// Een even om aan tegeven dat een productie is verwijderd
        /// </summary>
        public static event FormulierDeletedHandler OnFormulierDeleted;
        /// <summary>
        /// Een event een ebwerking wijziging door te geven
        /// </summary>
        public static event BewerkingChangedHandler OnBewerkingChanged;
        /// <summary>
        /// Een event om aan te geven dat een berwerking is verwijderd
        /// </summary>
        public static event BewerkingChangedHandler OnBewerkingDeleted;
        /// <summary>
        /// Een event dat aangeeft als de productie database is geladen
        /// </summary>
        public static event ProductiesChangedHandler OnProductiesLoaded;
        /// <summary>
        ///Een event dat aangeeft of er een personeel wijziging is 
        /// </summary>
        public static event PersoneelChangedHandler OnPersoneelChanged;
        /// <summary>
        /// Een event om aan te geven dat een personeel is verwijderd
        /// </summary>
        public static event PersoneelDeletedHandler OnPersoneelDeleted;
        /// <summary>
        /// Een event om aan tegeven dat een account is gewijzigd
        /// </summary>
        public static event AccountChangedHandler OnAccountChanged;
        /// <summary>
        /// Een event om aan te geven dat de instellingen zijn gewijzigd
        /// </summary>
        public static event UserSettingsChangedHandler OnSettingsChanged;
        /// <summary>
        /// Een event voor het tonen van een bericht
        /// </summary>
        private static event RemoteMessageHandler _remoteMessage;
        private static event FormulierActieHandler _formulierActie;
        /// <summary>
        /// Veilig event voor het oproepen van een bericht
        /// </summary>
        public static event RemoteMessageHandler OnRemoteMessage
        {
            add
            {
                lock (_messageLocker)
                {
                    _remoteMessage += value;
                }
            }

            remove
            {
                lock (_messageLocker)
                {
                    _remoteMessage -= value;
                }
            }
        }

        /// <summary>
        /// Een event om vanuit de beheerder een formulier actie laten uitvoeren
        /// </summary>
        public static event FormulierActieHandler OnFormulierActie
        {
            add
            {
                lock (_actieLocker)
                {
                    _formulierActie += value;
                }
            }

            remove
            {
                lock (_actieLocker)
                {
                    _formulierActie -= value;
                }
            }
        }
        /// <summary>
        /// Een event om door te geven dat er een login wijziging is
        /// </summary>
        public static event LogInChangedHandler OnLoginChanged;
        /// <summary>
        /// Een event om een intelling wijgiging te verzoeken en door te geven
        /// </summary>
        public static event UserSettingsChangingHandler OnSettingsChanging;
        /// <summary>
        /// Een event voor het doorgeven dat de database wordt geupdate
        /// </summary>
        public static event ManagerLoadedHandler OnDbBeginUpdate;
        /// <summary>
        /// Een event om door te geven dat de update van de database is geeindigd.
        /// </summary>
        public static event ManagerLoadedHandler OnDbEndUpdate;
        /// <summary>
        /// Een event om door te geven dat de beheerder bezig is met laden
        /// </summary>
        public static event ManagerLoadingHandler OnManagerLoading;
        /// <summary>
        /// Een event om door te geven dat het laden van de beheerder is beeindigd.
        /// </summary>
        public static event ManagerLoadedHandler OnManagerLoaded;
        /// <summary>
        /// Een event om door te geven dat de basis filter is gewijzigd
        /// </summary>
        public static event EventHandler FilterChanged;
        /// <summary>
        /// Een event om op te roepen dat de pc afgeloten kan worden
        /// </summary>
        public event ShutdownHandler OnShutdown;

        /// <summary>
        /// Oproepen van wijziging op de productie database
        /// </summary>
        /// <param name="sender">De gene die het oproept</param>
        public static void ProductiesChanged(object sender)
        {
            if (!AllowEvents) return;
            OnProductiesLoaded?.Invoke(sender);
        }
        /// <summary>
        /// Roep op om de pc af te sluiten
        /// </summary>
        /// <param name="verlengtijd">De tijd waarmee er evntueel verlegd kan worden</param>
        /// <returns>De resultaat of de pc afgeloten mag worden of niet</returns>
        public DialogResult Shutdown(ref TimeSpan verlengtijd)
        {
            if (OnShutdown != null)
                return OnShutdown.Invoke(this, ref verlengtijd);
            return DialogResult.Cancel;
        }

        /// <summary>
        /// Roep op dat de database begint met updaten
        /// </summary>
        public static void DbBeginUpdate()
        {
            if (!AllowEvents) return;
            OnDbBeginUpdate?.Invoke();
        }

        /// <summary>
        /// Roep op dat de database is geeindig met updaten
        /// </summary>
        public static void DbEndUpdate()
        {
            if (!AllowEvents) return;
            OnDbEndUpdate?.Invoke();
        }

        /// <summary>
        /// Roep op dat de beheerder is begonnen met laden
        /// </summary>
        /// <param name="cancel"></param>
        public static void ManagerLoading(ref bool cancel)
        {
            if (!AllowEvents) return;
            OnManagerLoading?.Invoke( ref cancel);
        }

        /// <summary>
        /// Roep op dat de beheerder is geladen
        /// </summary>
        public static void ManagerLoaded()
        {
            if (!AllowEvents) return;
            OnManagerLoaded?.Invoke();
        }

        /// <summary>
        /// Roep op dat de instellingen worden opgeslagen
        /// Geef eventuele wijzigingen door
        /// </summary>
        /// <param name="sender">De afzender die dit oproept</param>
        /// <param name="settings">De instellingen die worden opgeslagen</param>
        /// <param name="cancel">True voor als je het opslaan van de instellingen wilt annuleren</param>
        public static void SettingsChanging(object sender, ref UserSettings settings, ref bool cancel)
        {
            if (!AllowEvents) return;
            OnSettingsChanging?.Invoke(sender, ref settings, ref cancel);
        }

        private void Manager_OnManagerLoaded()
        {
            IsLoaded = true;
            //start sync timer
        }

        /// <summary>
        /// Roep op dat de logged in gebruiker is gewijzigd
        /// </summary>
        /// <param name="sender">de afzender die dit oproept</param>
        public static void LoginChanged(object sender,bool loadsettings, bool raiseevent)
        {
            if (loadsettings)
            {
                _ = LoadSettings(sender, raiseevent);
            }
            if (LogedInGebruiker == null)
                ProductieChat?.LogOut();
            else
                ProductieChat?.Login();
            if (!AllowEvents) return;
            OnLoginChanged?.Invoke(LogedInGebruiker, sender);
        }

        /// <summary>
        /// Roep op dat de instellingen zijn gewijzigd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="init"></param>
        public static void SettingsChanged(object sender, bool init)
        {
            if (!AllowEvents) return;
            OnSettingsChanged?.Invoke(sender, Opties, init);
        }

        /// <summary>
        /// Roep op om een formulier actie uit te voeren
        /// </summary>
        /// <param name="values">De objecten waarvan er een aktie van ondernomen moet worden</param>
        /// <param name="type">De soort aktie die ondernomen moet worden</param>
        public static void FormulierActie(object[] values, MainAktie type)
        {
            if (!AllowEvents) return;
            FormulierActieHandler handler;

            lock (_actieLocker)
            {
                handler = _formulierActie;
            }

            handler?.Invoke(values, type);
        }

        /// <summary>
        /// Roep op dat de productie is gewijzigd
        /// </summary>
        /// <param name="sender">de afzender die dit oproept</param>
        /// <param name="formulier">De productie die gewijzig is</param>
        public static void FormulierChanged(object sender, ProductieFormulier formulier)
        {
            if (!AllowEvents) return;
            if (formulier != null)
                OnFormulierChanged?.Invoke(sender, formulier);
        }

        /// <summary>
        /// Roep op dat er een productie is verwijderd
        /// </summary>
        /// <param name="sender">Der afzender die dit oproept</param>
        /// <param name="id">De productienr die verwijderd is</param>
        public static void FormulierDeleted(object sender, string id)
        {
            if (!AllowEvents) return;
            if (Manager.Database != null && Manager.Database.ProductieExist(id)) return;
            OnFormulierDeleted?.Invoke(sender, id);
        }

        /// <summary>
        /// Roep op dat de bewerking is gewijzigd
        /// </summary>
        /// <param name="sender">De afzender die dit oproept</param>
        /// <param name="bew">De bewerking die gewijzigd is</param>
        /// <param name="change">De verandering van de bewerking</param>
        public static void BewerkingChanged(object sender, Bewerking bew, string change, bool shownotification)
        {
            if (!AllowEvents) return;
            if (bew != null)
                OnBewerkingChanged?.Invoke(sender, bew, change,shownotification);
        }

        /// <summary>
        /// Roep op dat een bewerking verwijderd is
        /// </summary>
        /// <param name="sender">De afzender die dit oproept</param>
        /// <param name="bew">De bewerking die verwijderd is</param>
        /// <param name="shownotification">Toon notificatie voor als de bewerking is verwijderd</param>
        public static void BewerkingDeleted(object sender, Bewerking bew, bool shownotification)
        {
            if (!AllowEvents) return;
            if (bew != null)
                OnBewerkingDeleted?.Invoke(sender, bew, $"{bew.Path} Verwijderd!",shownotification);
        }

        /// <summary>
        /// Roep op dat een personeel is gewijzigd
        /// </summary>
        /// <param name="sender">De afzender die dit oproept</param>
        /// <param name="pers">De personeel die gewijzigd is</param>
        public static void PersoneelChanged(object sender, Personeel pers)
        {
            if (!AllowEvents) return;
            if (pers != null) OnPersoneelChanged?.Invoke(sender, pers);
        }

        /// <summary>
        /// Roep op dat een personeel is gewijzigd
        /// </summary>
        /// <param name="sender">De afzender die dit oproept</param>
        /// <param name="id">De personeelnaam van diegene die verwijderd is</param>
        public static void PersoneelDeleted(object sender, string id)
        {
            if (!AllowEvents) return;
            OnPersoneelDeleted?.Invoke(sender, id);
        }

        /// <summary>
        /// Roep om dat er een account is gewijzigd
        /// </summary>
        /// <param name="sender">De afzender die dit oproept</param>
        /// <param name="account">De account die is gewijzigd</param>
        public static void AccountChanged(object sender, UserAccount account)
        {
            if (!AllowEvents) return;
            OnAccountChanged?.Invoke(sender, account);
        }

        /// <summary>
        /// Roep op dat de instellingen zijn gewijzigd
        /// </summary>
        /// <param name="sender">De afzender die dit oproept</param>
        /// <param name="setting">De  instellingen die zijn gewijzigd</param>
        public static void UserSettingChanged(object sender, UserSettings setting)
        {
            if (setting != null && Opties != null && string.Equals(setting.Username, Opties.Username, StringComparison.CurrentCultureIgnoreCase))
            {
                Opties = setting;
                if (!AllowEvents) return;
                OnSettingsChanged?.Invoke(sender, setting,true);
            }
        }

        /// <summary>
        /// Roep op dat de filter is gewijzigd
        /// </summary>
        /// <param name="sender">De afzender die dit oproept</param>
        public static void OnFilterChanged(object sender)
        {
            if (!AllowEvents) return;
            FilterChanged?.Invoke(sender,EventArgs.Empty);
        }

        /// <summary>
        /// Roep een verzoek op om iets uit te voeren
        /// </summary>
        /// <param name="message">De bericht wat er uit gevoerd moet worden</param>
        /// <param name="title">De titel van de taak</param>
        /// <param name="buttons">Wat voor soort knop je wilt laten zien</param>
        /// <param name="icon">De bericht afbeelding</param>
        /// <param name="chooseitems">Een reeks keuzes die je eventueel kan geven</param>
        /// <param name="custombuttons">Aangepaste knoppen die je eventueel kan laten zien</param>
        /// <param name="customImage"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static DialogResult OnRequestRespondDialog(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon, string[] chooseitems = null, Dictionary<string, DialogResult> custombuttons = null, Image customImage = null, MetroColorStyle style = MetroColorStyle.Default)
        {
            var result = RequestRespondDialog?.Invoke(null, message, title, buttons, icon, chooseitems,
                custombuttons, customImage,style);
            return result ?? DialogResult.None;
        }

        public static event EventHandler KlachtChanged;
        private static void OnKlachtChanged(object sender, EventArgs e)
        {
            if (!AllowEvents) return;
            KlachtChanged?.Invoke(sender, e);
        }

        public static event EventHandler KlachtDeleted;
        private static void OnKlachtDeleted(object sender, EventArgs e)
        {
            if (!AllowEvents) return;
            KlachtDeleted?.Invoke(sender, e);
        }

        public static event EventHandler LayoutChanged;
        private static void OnLayoutChanged(object sender, EventArgs e)
        {
            if (!AllowEvents) return;
            LayoutChanged?.Invoke(sender, e);
        }

        public static event EventHandler LayoutDeleted;
        private static void OnLayoutDeleted(object sender, EventArgs e)
        {
            if (!AllowEvents) return;
            LayoutDeleted?.Invoke(sender, e);
        }

        public static event EventHandler VerpakkingChanged;
        private static void OnVerpakkingChanged(object sender, EventArgs e)
        {
            if (!AllowEvents) return;
            VerpakkingChanged?.Invoke(sender, e);
        }

        public static event EventHandler VerpakkingDeleted;
        private static void OnVerpakkingDeleted(object sender, EventArgs e)
        {
            if (!AllowEvents) return;
            VerpakkingDeleted?.Invoke(sender, e);
        }

        public static event EventHandler SpoorChanged;
        private static void OnSpoorChanged(object sender, EventArgs e)
        {
            if (!AllowEvents) return;
            SpoorChanged?.Invoke(sender, e);
        }

        public static event EventHandler SpoorDeleted;
        private static void OnSpoorDeleted(object sender, EventArgs e)
        {
            if (!AllowEvents) return;
            SpoorDeleted?.Invoke(sender, e);
        }

        #region Threadsafe RespondMessage

        private static object _messageLocker = new();
        private static object _actieLocker = new();
        /// <summary>
        /// Roep op om een bericht te laten zien
        /// </summary>
        /// <param name="message">De bericht die je wilt laten zien</param>
        public static void RemoteMessage(RemoteMessage message)
        {
            switch (message.Action)
            {
                case MessageAction.NieweProductie:
                    if (message.Value is ProductieFormulier form)
                    {
                        AddProductie(form,true);
                    }

                    break;

                case MessageAction.ProductieNotitie:
                    break;

                case MessageAction.ProductieWijziging:
                    break;

                case MessageAction.ProductieVerwijderen:
                    break;

                case MessageAction.AlgemeneMelding:
                    break;

                case MessageAction.GebruikerUpdate:
                    if (message.Value is UserAccount acc)
                    {
                        if (LogedInGebruiker != null && string.Equals(LogedInGebruiker.Username, acc.Username, StringComparison.CurrentCultureIgnoreCase))
                            LogOut(null,true);
                    }

                    break;
            }

            RemoteMessageHandler rms;
            lock (_messageLocker)
            {
                rms = _remoteMessage;
            }
            rms?.Invoke(message, null);
        }

        private static void BackgroundMessageHandler_ProgressChanged(ProgressChangedEventArgs e)
        {
            if (e.UserState is RemoteMessage message)
                _remoteMessage?.Invoke(message, null);
        }
        #endregion Threadsafe RespondMessage

        #endregion "Events"

        #region Disposing

        private bool _disposed;

        // Instantiate a SafeHandle instance.
        private readonly SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        /// <summary>
        /// Schoon op en sluit de beheerder
        /// </summary>
        public void Dispose()
        {
            Close();
            _syncTimer?.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            IsLoaded = false;
            if (LogedInGebruiker != null)
                LogOut(null, false);
            //_emailcheckTimer?.Stop();
            //_emailcheckTimer?.Dispose();
            //_backemailchecker?.CancelAsync();
            //_backemailchecker?.Dispose();
            _fileWatchers?.ForEach(x => x.Dispose());
            _syncTimer?.Stop();
            Database?.Dispose();
            ProductieProvider?.Dispose();
            ProductieChat?.Dispose();
            Opmerkingen = null;
            ProductieChat = null;
            Klachten?.Dispose();
            Verpakkingen?.Dispose();
            SporenBeheer?.Dispose();
            ArtikelRecords?.Dispose();
            ListLayouts?.Dispose();
            MailServer?.Stop();
            Meldingen?.Dispose();
        }

        /// <summary>
        /// Schoon op en sluit de beheerder
        /// </summary>
        /// <param name="disposing">True als je de beheerder volledig wilt afsluiten</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            _fileWatchers = null;
            Klachten = null;
            Verpakkingen = null;
            SporenBeheer = null;
            ListLayouts = null;
            ArtikelRecords = null;
            if (disposing)
                _safeHandle?.Dispose();

            _disposed = true;
        }

        public void DisposeEvents()
        {
            KlachtChanged = null;
            KlachtDeleted = null;
            SpoorChanged = null;
            SpoorDeleted = null;
            VerpakkingChanged = null;
            VerpakkingDeleted = null;
            LayoutChanged = null;
            LayoutDeleted = null;
            OnManagerLoaded = null;
            OnAccountChanged = null;
            OnBewerkingChanged = null;
            OnBewerkingDeleted = null;
            OnProductiesLoaded = null;
            OnDbBeginUpdate = null;
            OnProductiesLoaded = null;
            OnPersoneelChanged = null;
            OnPersoneelDeleted = null;
            OnFormulierChanged = null;
            OnFormulierDeleted = null;
            OnDbEndUpdate = null;
            OnLoginChanged = null;
            OnSettingsChanged = null;
            OnSettingsChanging = null;
        }

        #endregion Disposing
        
    }
}