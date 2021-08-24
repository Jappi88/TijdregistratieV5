using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using FolderSync;
using Microsoft.Win32.SafeHandles;
using ProductieManager;
using ProductieManager.Rpm.Productie;
using ProductieManager.Rpm.SqlLite;
using ProductieManager.Rpm.Various;
using Rpm.SqlLite;
using Rpm.Mailing;
using Rpm.Misc;
using Rpm.Settings;
using Rpm.Various;

namespace Rpm.Productie
{
    [Serializable]
    public class Manager : IDisposable
    {
        #region "Variables"
        private TimeSpan _afsluittijd;
        // public static LocalService LocalConnection { get; private set; }
        public static ProductieChat ProductieChat { get; private set; } = new ProductieChat();
        public static LocalDatabase Database { get; private set; }
        public static DatabaseUpdater DbUpdater { get; private set; }
        public static ProductieProvider ProductieProvider { get; private set; }
        //public static FileDeletionManager FileDeletionManager { get; private set; }

        //public static SqlDatabase Server { get; private set; }
        private List<FileSystemWatcher> _fileWatchers = new();
        private readonly TaskQueues _tasks = new();
        public static string AppRootPath;
        public static string DbPath;
        public static string WeekOverzichtPath;
        public static string TempPath;
        public static string BackupPath;

        public static string ProductieFormPath;
        //private static readonly string SettingsDirectory = Mainform.BootDir + "\\RPM_Settings";

        // [NonSerialized] private readonly BackgroundWorker _backemailchecker;

        // [NonSerialized] private readonly Timer _emailcheckTimer = new();

        [NonSerialized] private Timer _syncTimer = new Timer();
        [NonSerialized] private Timer _overzichtSyncTimer = new Timer();

        public string Versie => "1.0";
        public static UserSettings Opties { get; set; }
        public static UserAccount LogedInGebruiker { get; set; }
        public static Logger Logbook { get; set; }
        public static BackupInfo BackupInfo { get; set; }
        public static UserSettings DefaultSettings { get; set; } = UserSettings.GetDefaultSettings();

        public int SyncInterval
        {
            get => _syncTimer.Interval;
            set => _syncTimer.Interval = value;
        }

        public int OverzichtSyncInterval
        {
            get => _overzichtSyncTimer.Interval;
            set => _overzichtSyncTimer.Interval = value;
        }

        //public int MailInterval
        //{
        //    get => _emailcheckTimer.Interval;
        //    set => _emailcheckTimer.Interval = value;
        //}

        public static BewerkingLijst BewerkingenLijst;
        public static bool IsLoaded { get; private set; }
        //public static string xInstanceID { get; private set; }
        public static string SystemID { get; private set; }
        public bool LoggerEnabled { get; private set; }

        #endregion "Variables"

        #region "Constructor"

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
        public void InitManager()
        {
            //_backemailchecker.DoWork += _worker_DoWork;
            //_backemailchecker.RunWorkerCompleted += _worker_RunWorkerCompleted;

            OnManagerLoaded += Manager_OnManagerLoaded;

            //_emailcheckTimer.Tick += _timer_Tick;
            _syncTimer = new Timer();
            _syncTimer.Tick += _syncTimer_Tick;
            _overzichtSyncTimer = new Timer();
            _overzichtSyncTimer.Tick += _overzichtSyncTimer_Tick;
            _tasks.OnRunComplete += _tasks_OnRunComplete;
            _tasks.RunInstanceComplete += _tasks_OnRunInstanceComplete;
        }

        private void InitDirectories()
        {
            if (!Directory.Exists(DbPath))
                Directory.CreateDirectory(DbPath);
            if (!Directory.Exists(TempPath))
                Directory.CreateDirectory(TempPath);
            if (!Directory.Exists(BackupPath))
                Directory.CreateDirectory(BackupPath);
            if (!Directory.Exists(WeekOverzichtPath))
                Directory.CreateDirectory(WeekOverzichtPath);
            if (!Directory.Exists(ProductieFormPath))
                Directory.CreateDirectory(ProductieFormPath);
        }

        public void LoadPath(string rootpath)
        {
            AppRootPath = rootpath;
            DbPath = AppRootPath + "\\RPM_Data";
            TempPath = AppRootPath + "\\Temp";
            BackupPath = AppRootPath + "\\Backup";
            WeekOverzichtPath = AppRootPath + "\\Week Overzichten";
            ProductieFormPath = AppRootPath + "\\RPM_Data\\Productie Formulieren";
            InitDirectories();
        }

        public async Task<bool> Load(string path, bool autologin, bool loadsettings, bool raiseManagerLoadingEvents)
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

                LoadPath(path);
               
                Database = new LocalDatabase(this, SystemID, DbPath, true)
                {
                    LoggerEnabled = LoggerEnabled,
                    NotificationEnabled = false
                };
                await Database.LoadMultiFiles();
                ProductieProvider = new ProductieProvider();
                // LocalConnection = new LocalService();
                //Server = new SqlDatabase();

                DbUpdater = new DatabaseUpdater();
                Logbook = new Logger();
                BackupInfo = BackupInfo.Load();
                BewerkingenLijst = new BewerkingLijst();
                if (string.IsNullOrEmpty(DefaultSettings.SystemID))
                {
                    DefaultSettings.SystemID = Guid.NewGuid().ToByteArray().ToHexString(4);
                    DefaultSettings.SaveAsDefault();
                }

                SystemID = DefaultSettings.SystemID;
                if (autologin)
                    await AutoLogin(this);
                if (Opties == null || loadsettings)
                    await LoadSettings(this, raiseManagerLoadingEvents);

                if (loadsettings)
                    ProductieProvider?.InitOfflineDb();

                DbUpdater.UpdateStartupDbs();
                while (Mainform.IsLoading)
                    Application.DoEvents();
                if (raiseManagerLoadingEvents)
                    ManagerLoaded();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
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
        public string Get8CharacterRandomString()
        {
            var path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path.Substring(0, 8); // Return 8 character string
        }

        public Task<bool> CreateAccount(UserAccount account)
        {
            return Database.UpSert(account);
        }

        public static Task<bool> Login(string username, string password, bool autologin, object sender)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var x = await Database.GetAccount(username);
                    if (x != null)
                    {
                        if (x.ValidateUser(username, password))
                        {
                            DefaultSettings ??= UserSettings.GetDefaultSettings();
                            var xdef = DefaultSettings;
                            if (autologin)
                                xdef.AutoLoginUsername = x.Username;
                            else xdef.AutoLoginUsername = null;
                            xdef.SaveAsDefault();
                            LogedInGebruiker = x;
                            LoginChanged(sender);
                            return true;
                        }

                        LogedInGebruiker = null;

                        LoginChanged(sender);
                        return false;
                    }

                    throw new Exception("Gebruiker bestaat niet!");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public static Task<bool> Login(string username, object sender)
        {
            return Task.Run<bool>(async () =>
            {
                try
                {
                    var x = await Database.GetAccount(username);
                    if (x != null)
                    {
                        LogedInGebruiker = x;
                        x.OsID = SystemID;
                        await Database?.UpSert(x, $"{x.Username} Ingelogd!");
                        LoginChanged(sender);
                        return true;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public static void LogOut(object sender)
        {
            if (LogedInGebruiker != null)
            {
                LogedInGebruiker = null;
                LoginChanged(sender);
            }
        }

        public Task<ProductieFormulier> Werktaan(string personeelnaam)
        {
            return Task.Run(async () =>
            {
                var prods = await GetProducties(ViewState.Gestart, false, false,false);
                return prods.FirstOrDefault(t =>
                    t.State == ProductieState.Gestart &&
                    (t.Bewerkingen?.Where(x =>
                        x.GetPersoneel().FirstOrDefault(x => string.Equals(x.PersoneelNaam, personeelnaam, StringComparison.CurrentCultureIgnoreCase)) !=
                        null) ?? new Bewerking[] { }).FirstOrDefault() != null);
            });
        }

        public static async Task<bool> LoadSettings(object sender, bool raiseEvent)
        {
            if (Database.IsDisposed) return false;
            var os = SystemID;
            var id = LogedInGebruiker == null ? $"Default[{os}]" : LogedInGebruiker.Username;
            var optiesid = Opties?.Username != null ? Opties.Username.ToLower().StartsWith("default")? $"Default[{Opties.SystemID}]" : Opties.Username : null;
            string name = LogedInGebruiker == null ? "Default" : LogedInGebruiker.Username;
            if (Opties != null && !string.Equals(optiesid, id, StringComparison.CurrentCultureIgnoreCase))
                SaveSettings(Opties, false, true);

            try
            {
                //if (Opties != null && string.Equals($"{Opties.Username}[{os}]", id, StringComparison.CurrentCultureIgnoreCase))
                //    return false;
                try
                {
                    UserSettings xt = await Database.GetSetting(id);
                    if (xt == null)
                    {
                        xt = (await Database.GetAllSettings()).FirstOrDefault(x=> x.Username.ToLower().StartsWith(name.ToLower()));
                        if (xt != null)
                        {
                            await Database.DeleteSettings($"{xt.Username}[{xt.SystemID}]", false);
                            xt.SystemID = os;
                            await xt.Save(null, false, false, false);
                        }
                    }

                    Opties = xt;
                }
                catch (Exception)
                {
                }

                if (Opties == null)
                {
                    var opties = new UserSettings {Username = name};
                    await Database.UpSert(opties);
                    Opties = opties;
                }

                if (Opties != null)
                {
                    if (Opties.ProductieWeergaveFilters == null || Opties.ProductieWeergaveFilters.Length == 0)
                        Opties.ProductieWeergaveFilters = new[] {ViewState.Alles};
                    if (!Directory.Exists(DbPath))
                        Directory.CreateDirectory(DbPath);
                    if (raiseEvent)
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

        public bool SaveSettings(bool force)
        {
            return SaveSettings(Opties, true, force);
        }

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

                //if (!changed)
                //    changed = !settings.xPublicInstancePropertiesEqual(Opties, new[] {typeof(UserChange)});

                //if (changed)
                //{
                    Functions.SetAutoStartOnBoot(settings.StartNaOpstart);
                    settings.Save();
                //}
            
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

        public void SetSettings(UserSettings options)
        {
            if (options == null)
                return;
            options.SystemID = SystemID;
            var bkms = TimeSpan.FromHours(0.10).TotalMilliseconds;
            SyncInterval = options.SyncInterval < 10000 ? 10000 : options.SyncInterval;
            OverzichtSyncInterval = options.WeekOverzichtUpdateInterval;
            options.BackupInterval = options.BackupInterval < bkms ? bkms : options.BackupInterval;
            if (options.CreateBackup)
                BackupInfo?.StartBackupSyncer(options.BackupInterval);
            //MailInterval = options.MailSyncInterval < 10000 ? 10000 : options.MailSyncInterval;
            if (Database != null)
                Database.NotificationEnabled = options.ToonLogNotificatie;
            //load files sync instances
            LoadSyncDirectories();
            _afsluittijd = options.AfsluitTijd;
            if (options.GebruikLocalSync || options.GebruikTaken)
            {
                ProductieProvider?.StartSyncProducties();
                ProductieProvider?.UpdateProducties();
            }
            if (options.DbUpdateEntries != null)
            {
                foreach (var ent in Opties.DbUpdateEntries)
                    ent.LastUpdated = DateTime.MinValue;
            }
            DbUpdater?.Start();
            _syncTimer?.Start();
            if (options.CreateWeekOverzichten)
                _overzichtSyncTimer?.Start();
            else _overzichtSyncTimer?.Stop();
            //if (!options.GebruikTaken)
            //    Taken?.StopBeheer();
            //else Taken?.StartBeheer();
            //UpdateFormOldDbProducties();
            // Manager.Database.PersoneelLijst.DeleteAll();
            // Manager.Database.LiteDatabase.Rebuild();
            //InitPersoneel();
        }

        /// <summary>
        /// Verkrijg alle producties
        /// </summary>
        /// <param name="states">Verkrijg producties op basis van de gekozen status lijst</param>
        /// <param name="filter">true als je wilt dat de producties worden gefiltered volgens een geldige bewerking</param>
        /// <param name="incform">true als de productie ook die aangegeven status moet zijn, false je alleen wilt verkrijgen op basis van een geldige bewerking</param>
        /// <param name="loaddb">true als je de database als standaard wilt laden</param>
        /// <returns>Lijst van productieformulieren</returns>
        public static Task<List<ProductieFormulier>> GetProducties(ViewState[] states, bool filter, bool incform, bool loaddb)
        {
            return Task.Run(async () =>
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

                var xitems = await ProductieProvider.GetProducties(type, states, filter,incform);
                return xitems;
            });
        }

        public static Task<List<Bewerking>> GetBewerkingen(ViewState[] states, bool filter, bool loaddb)
        {
            return Task.Run(async () =>
            {
                if (ProductieProvider == null) return new List<Bewerking>();
                var type = ProductieProvider.LoadedType.None;
                if (states.Any(x => x == ViewState.Gereed))
                    type = ProductieProvider.LoadedType.Gereed;
                if (states.Any(x => x != ViewState.Gereed))
                {
                    type = type == ProductieProvider.LoadedType.Gereed
                        ? ProductieProvider.LoadedType.Alles
                        : ProductieProvider.LoadedType.Producties;
                }

                var xitems = await ProductieProvider.GetBewerkingen(type, states, filter);
                return xitems;
            });
        }

        public static Task<List<string>> GetAllProductieIDs(bool incgereed, bool checksecondary)
        {
            return Task.Run(async () =>
            {
                if (Database?.ProductieFormulieren == null || Database.IsDisposed)
                    return new List<string>();
                List<string> xreturn = new List<string>();
                try
                {
                    var xitems = await Database.ProductieFormulieren.GetAllIDs(checksecondary);
                    if (xitems != null && xitems.Count > 0)
                        xreturn.AddRange(xitems);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (incgereed)
                {
                    try
                    {
                        var gereed = await Database.GereedFormulieren.GetAllIDs(true);
                        if (gereed != null && gereed.Count > 0)
                            xreturn.AddRange(gereed);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
                return xreturn;
            });
        }

        public static Task<List<string>> GetAllProductiePaths(bool incgereed, bool checksecondary)
        {
            return Task.Run(async () =>
            {
                if (Database?.ProductieFormulieren == null || Database.IsDisposed)
                    return new List<string>();
                List<string> xreturn = new List<string>();
                try
                {
                    var xitems = await Database.ProductieFormulieren.GetAllPaths(checksecondary);
                    if (xitems is {Count: > 0})
                        xreturn.AddRange(xitems);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (incgereed)
                {
                    try
                    {
                        var gereed = await Database.GereedFormulieren.GetAllPaths(checksecondary);
                        if (gereed is {Count: > 0})
                            xreturn.AddRange(gereed);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
                return xreturn;
            });
        }

        public static Task<List<ProductieFormulier>> GetProducties(ViewState state, bool filter, bool incform, bool loaddb)
        {
            return GetProducties(new[] {state}, filter, incform,loaddb);
        }

        public static Task<bool> ContainsOnderbrokenProductie()
        {
            return Task.Run(async () =>
            {
                var prods = await GetProducties(new[] {ViewState.Gestart, ViewState.Gestopt}, true, false,false);
                return prods.Any(x => x.IsOnderbroken());
            });
        }

        public static bool ProductiesChanged()
        {
            ProductiesChanged(Database);
            return true;
        }

        public static Task<RemoteMessage> AddProductie(ProductieFormulier prod)
        {
            return Task.Run(async () =>
            {
                try
                {
                    if (await Database.Exist(prod))
                    {
                        prod = await Database.GetProductie(prod.ProductieNr);
                        return new RemoteMessage(
                            $"Productie [{prod.ProductieNr}] bestaat al, en kan niet nogmaals worden toegevoegd!",
                            MessageAction.ProductieWijziging, MsgType.Waarschuwing, null, prod, prod.ProductieNr);

                    }
                    var xa = prod.Aantal == 1 ? "stuk" : "stuks";
                   // prod = await ProductieFormulier.UpdateDoorloopTijd(prod);
                    if (await Database.UpSert(prod,
                        $"[{prod.ArtikelNr}|{prod.ProductieNr}] Nieuwe productie toegevoegd({prod.Aantal} {xa}) met doorlooptijd van {prod.DoorloopTijd} uur.")
                    )
                    {
                        var xprod = await Database.GetProductie(prod.ProductieNr);
                        if (xprod != null)
                        {
                            prod = xprod;
                            _ = ProductieFormulier.UpdateDoorloopTijd(null, prod, null, true, true,false);
                        }
                        return new RemoteMessage($"{prod.ProductieNr} toegevoegd!", MessageAction.NieweProductie,
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
            });
        }

        public Task<List<RemoteMessage>> AddProductie(string pdffile, bool delete)
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
                            RemoteMessage msg = null;
                            if (prod.IsAllowed(null))
                            {
                                msg = await AddProductie(prod);
                                try
                                {
                                    if (msg.Value is ProductieFormulier xprod)
                                    {

                                        var bew = xprod.Bewerkingen?.FirstOrDefault(x => x.IsAllowed());
                                        if (bew != null)
                                            FormulierActie(new object[] { xprod, bew}, MainAktie.OpenProductie);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                            }
                            else
                                msg = new RemoteMessage($"[{prod.ProductieNr}, {prod.AantalGemaakt}] is gefilterd!",
                                    MessageAction.AlgemeneMelding, MsgType.Info);
                            rms.Add(msg);
                            if (msg.Action == MessageAction.NieweProductie || msg.Action == MessageAction.ProductieWijziging)
                            {
                                try
                                {
                                    string fpath = ProductieFormPath + $"\\{prod.ProductieNr}.pdf";
                                    if (delete)
                                    {

                                        if (File.Exists(fpath))
                                            File.Delete(pdffile);
                                        else
                                            File.Move(pdffile, fpath);
                                    }
                                    else
                                        File.Copy(pdffile, ProductieFormPath + $"\\{prod.ProductieNr}.pdf", true);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }

                                // pdffile.CleanupFilePath(ProductieFormPath, prod.ProductieNr, false,false);
                            }

                            if (msg.Action == MessageAction.NieweProductie && delete && Opties != null && Opties.VerwijderVerwerkteBestanden && File.Exists(pdffile))
                            {
                                try
                                {
                                    File.Delete(pdffile);
                                }
                                catch
                                {
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

        public Task<int> AddProductie(string[] pdffiles, bool rename, bool showerror)
        {
            return Task.Run(() =>
            {
                if (pdffiles?.Length == 0) return 0;
                var xreturn = 0;
                if (pdffiles != null)
                    foreach (var file in pdffiles)
                        try
                        {
                            var msgs = AddProductie(file, rename).Result;
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
                        }

                return xreturn;
            });
        }

        public Task<List<RemoteMessage>> AddProducties(ProductieFormulier[] prods)
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
                        var msg = await AddProductie(prod);
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

        public static Task<bool> RemoveProductie(ProductieFormulier prod, bool realremove)
        {
            return RemoveProductie(prod.ProductieNr, realremove);
        }

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
                        if (prod.Bewerkingen != null)
                            foreach (var b in prod.Bewerkingen)
                            {
                                if (b.State == ProductieState.Gestart) await b.StopProductie(true);
                                b.State = ProductieState.Verwijderd;
                                b.DatumVerwijderd = DateTime.Now;
                            }

                        if (prod.State != ProductieState.Gestart && prod.State != ProductieState.Verwijderd)
                        {
                            prod.State = ProductieState.Verwijderd;
                            prod.DatumVerwijderd = DateTime.Now;
                            string change = $"Productie [{prod.ProductieNr.ToUpper()}] is zojuist verwijderd";
                            if (await Database.UpSert(prod,change,true,false))
                            {
                                removed++;
                                RemoteProductie.RespondByEmail(prod,change);
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

        public static Task<bool> RemoveProductie(string prodnr, bool realremove)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var current = await Database.GetProductie(prodnr);
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
                                        await b.StopProductie(true);
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

        public Task<bool> ReplaceProductie(ProductieFormulier oldform, ProductieFormulier newform)
        {
            return Task.Run(async () =>
            {
                if (oldform == null || newform == null)
                    return false;
                return await Database.Replace(oldform, newform);
            });
        }

        public Task<int> AantalProductiesGemaakt(string artikelnr)
        {
            return Task.Run(async () =>
            {
                var xreturn = 0;
                var forms = await Database.GetProducties(artikelnr, true, ProductieState.Gereed, true);
                xreturn = forms.Count;
                forms = null;
                return xreturn;
            });
        }

        public static string[] GetWerkplekken(string bewerking)
        {
            var realname = bewerking.Split('[')[0];
            var xs = BewerkingenLijst.GetEntry(realname);
            if (xs != null)
                return xs.WerkPlekken.ToArray();
            return new string[] { };
        }

        public static Task<int> UpdateGestarteProductieRoosters()
        {
            return Task.Run(async () =>
            {
                try
                {
                    int done = 0;
                    var prods = await GetProducties(ViewState.Gestart, true, false, false);
                    var acces1 = LogedInGebruiker != null &&
                                 LogedInGebruiker.AccesLevel >= AccesType.ProductieBasis;
                    if (!acces1) return -1;
                    for (int i = 0; i < prods.Count; i++)
                    {
                        var prod = prods[i];
                        if (prod.Bewerkingen == null || prod.Bewerkingen.Length == 0) continue;
                        bool changed = false;
                        List<string> changes = new List<string>();
                        foreach (var bw in prod.Bewerkingen)
                        {
                            if (!bw.IsAllowed() || bw.State != ProductieState.Gestart) continue;
                            if (!string.Equals(Opties.Username, bw.GestartDoor,
                                StringComparison.CurrentCultureIgnoreCase)) continue;
                            var wps = bw.WerkPlekken.Where(x => x.IsActief()).ToList();
                            foreach (var wp in wps)
                            {
                                wp.UpdateWerkRooster(true, true, true, false, true);
                                changed = true;
                                changes.Add(wp.Naam);
                            }

                        }

                        if (changed)
                        {
                            done++;
                            await prod.UpdateForm(true, false, null,
                                $"[{prod.ProductieNr} | {prod.ArtikelNr}] Werkrooster aangepast voor: \n" +
                                $"{string.Join(", ", changes)}");
                        }
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
                if (_fileWatchers == null)
                    _fileWatchers = new List<FileSystemWatcher>();
                if (Opties.SyncLocaties != null && Opties.SyncLocaties.Length > 0)
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

                    foreach (var fs in _fileWatchers)
                        if (!Opties.SyncLocaties.Contains(fs.Path))
                        {
                            fs.Dispose();
                            _fileWatchers.Remove(fs);
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
        private bool BackupFiles(string[] files, string path)
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

        private bool _IsLoadingFiles = false;
        private async void LoadUnloadedFiles()
        {
            try
                {
                if (_IsLoadingFiles) return;
                _IsLoadingFiles = true;
                var files = new List<string>();
                if (Opties?.SyncLocaties != null)
                {
                    foreach (var s in Opties.SyncLocaties)
                        files.AddRange(Directory.GetFiles(s, "*.pdf").Where(t => !t.Contains("_old")).ToArray());
                    if (files.Count > 0)
                    {
                        await AddProductie(files.ToArray(), true, false);
                    }
                }
            }
            catch (Exception)
            {
            }
            _IsLoadingFiles = false;
        }

        private static Task<bool> AutoLogin(object sender)
        {
            return Task.Run(async () =>
            {
                try
                {
                    if (LogedInGebruiker != null) return false;
                    //var pcid = CpuID.ProcessorId();
                    var xdef = UserSettings.GetDefaultSettings();
                    if(!string.IsNullOrEmpty(xdef.AutoLoginUsername))
                        return await Login(xdef.AutoLoginUsername, sender);
                }
                catch
                {
                }

                return false;
            });
        }

        #region Timers & Auto Productie detection

        private List<string> _filestoadd = new();
        private System.Timers.Timer _filesaddertimer;
        private bool isbusy = false;

        private void Sw_Created(object sender, FileSystemEventArgs e)
        {
            if (isbusy)
                return;
            isbusy = true;
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
                AddProductie(_filestoadd.ToArray(), true, false).Wait();
                _filestoadd.Clear();
            }
            isbusy = false;
        }
        //deze fixen|!!!
        private void _syncTimer_Tick(object sender, EventArgs e)
        {
            _syncTimer.Stop();
            //Even checken of we geladen zijn in de goede database.
            //await Task.Run(() =>
            //{
                //try
                //{
                //    if (DefaultSettings.MainDB != null)
                //    {
                //        if (string.Equals(AppRootPath, DefaultSettings.MainDB.RootPath,
                //                StringComparison.CurrentCultureIgnoreCase) &&
                //            !Directory.Exists(AppRootPath + "\\RPM_Data"))
                //        {
                //            LogOut(this);
                //            Application.Restart();
                //            return;
                //        }

                //        if (!string.Equals(AppRootPath, DefaultSettings.MainDB.RootPath,
                //            StringComparison.CurrentCultureIgnoreCase))
                //        {
                //            try
                //            {
                //                if (Directory.Exists(DefaultSettings.MainDB.UpdatePath))
                //                {
                //                    LogOut(this);
                //                    Application.Restart();
                //                }
                //            }
                //            catch (Exception e)
                //            {
                //                //Console.WriteLine(e);
                //            }
                //        }
                //    }
                //}
                //catch (Exception exception)
                //{
                //    Console.WriteLine(exception);
                //}

                

            //});
            //laten we controleren naar de instellingen en daar na handelen
            try
            {


                if (Opties != null && Opties.SluitPcAf && DateTime.Now.TimeOfDay >= _afsluittijd)
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
            }
            catch
            {
            }

            _syncTimer.Start();
        }

        private async void _overzichtSyncTimer_Tick(object sender, EventArgs e)
        {
            _overzichtSyncTimer.Stop();
            //laten we controleren naar de instellingen en daar na handelen
            if (Opties != null && Opties.CreateWeekOverzichten)
                await ExcelWorkbook.UpdateWeekOverzichten();

            _overzichtSyncTimer.Start();
        }

        #endregion Timers & Auto Productie detection

        //private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    var messages = (RemoteMessage[]) e.Result;
        //    if (messages != null && messages.Length > 0)
        //        foreach (var message in messages)
        //            try
        //            {
        //                _tasks.Add(new TaskManager(this, message));
        //            }
        //            catch
        //            {
        //            }

        //    StartMonitor();
        //}

        //private void _timer_Tick(object sender, EventArgs e)
        //{
        //    if (!_backemailchecker.IsBusy)
        //    {
        //        StopMonitor();
        //        _backemailchecker.RunWorkerAsync();
        //    }
        //}

        //private void _worker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    //e.Result = RemoteProductie.ControlleerOpMessages();
        //}

        private void _tasks_OnRunInstanceComplete(object sender, EventArgs e)
        {
            if (sender is IQueue instance)
            {
                if (instance.Results != null && instance.Results.Length > 0)
                    foreach (var rm in instance.Results)
                        RemoteMessage(rm);
            }
        }

        private void _tasks_OnRunComplete()
        {
        }

        #endregion "Private Methods"

        #region "Events"

        public static event FormulierActieHandler OnFormulierActie;

        public static event FormulierChangedHandler OnFormulierChanged;

        public static event FormulierDeletedHandler OnFormulierDeleted;

        public static event BewerkingChangedHandler OnBewerkingChanged;

        public static event BewerkingChangedHandler OnBewerkingDeleted;

        public static event ProductiesChangedHandler OnProductiesLoaded;

        public static event PersoneelChangedHandler OnPersoneelChanged;

        public static event PersoneelDeletedHandler OnPersoneelDeleted;

        public static event AccountChangedHandler OnAccountChanged;

        public static event UserSettingsChangedHandler OnSettingsChanged;
        private static event RemoteMessageHandler _OnRemoteMessage;

        public static event RemoteMessageHandler OnRemoteMessage
        {
            add
            {
                lock (_messageLocker)
                {
                    _OnRemoteMessage += value;
                }
            }

            remove
            {
                lock (_messageLocker)
                {
                    _OnRemoteMessage -= value;
                }
            }
        }

        public static event LogInChangedHandler OnLoginChanged;

        public static event UserSettingsChangingHandler OnSettingsChanging;
        public static event ManagerLoadedHandler OnDbBeginUpdate;
        public static event ManagerLoadedHandler OnDbEndUpdate;
        public static event ManagerLoadingHandler OnManagerLoading;

        public static event ManagerLoadedHandler OnManagerLoaded;
        public static event EventHandler FilterChanged;

        public event ShutdownHandler OnShutdown;

        public static void ProductiesChanged(object sender)
        {
            OnProductiesLoaded?.Invoke(sender);
        }

        public DialogResult Shutdown(ref TimeSpan verlengtijd)
        {
            if (OnShutdown != null)
                return OnShutdown.Invoke(this, ref verlengtijd);
            return DialogResult.Cancel;
        }

        public static void DbBeginUpdate()
        {
            OnDbBeginUpdate?.Invoke();
        }

        public static void DbEndUpdate()
        {
            OnDbEndUpdate?.Invoke();
        }

        public static void ManagerLoading(ref bool cancel)
        {
            OnManagerLoading?.Invoke( ref cancel);
        }

        public static void ManagerLoaded()
        {
            OnManagerLoaded?.Invoke();
        }

        public static void SettingsChanging(object sender, ref UserSettings settings, ref bool cancel)
        {
            OnSettingsChanging?.Invoke(sender, ref settings, ref cancel);
        }

        private void Manager_OnManagerLoaded()
        {
            IsLoaded = true;
            //start sync timer
        }

        public static async void LoginChanged(object sender)
        {
            await LoadSettings(sender,true);
            if (LogedInGebruiker == null)
                ProductieChat?.LogOut();
            else
                ProductieChat?.Login();
            OnLoginChanged?.Invoke(LogedInGebruiker, sender);
        }

        public static void SettingsChanged(object sender, bool init)
        {
            OnSettingsChanged?.Invoke(sender, Opties, init);
        }

        public static void FormulierActie(object[] values, MainAktie type)
        {
            OnFormulierActie?.Invoke(values, type);
        }

        public static void FormulierChanged(object sender, ProductieFormulier formulier)
        {
            if (formulier != null)
                OnFormulierChanged?.Invoke(sender, formulier);
        }

        public static void FormulierDeleted(object sender, string id)
        {
            OnFormulierDeleted?.Invoke(sender, id);
        }

        public static void BewerkingChanged(object sender, Bewerking bew, string change)
        {
            if (bew != null)
                OnBewerkingChanged?.Invoke(sender, bew, change);
        }

        public static void BewerkingDeleted(object sender, Bewerking bew)
        {
            if (bew != null)
                OnBewerkingDeleted?.Invoke(sender, bew, $"{bew.Path} Verwijderd!");
        }

        public static void PersoneelChanged(object sender, Personeel pers)
        {
            if (pers != null) OnPersoneelChanged?.Invoke(sender, pers);
        }

        public static void PersoneelDeleted(object sender, string id)
        {
            OnPersoneelDeleted?.Invoke(sender, id);
        }

        public static void AccountChanged(object sender, UserAccount account)
        {
            OnAccountChanged?.Invoke(sender, account);
        }

        public static void UserSettingChanged(object sender, UserSettings setting)
        {
            if (setting != null && Opties != null && string.Equals(setting.Username, Opties.Username, StringComparison.CurrentCultureIgnoreCase))
            {
                Opties = setting;
                OnSettingsChanged?.Invoke(sender, setting,true);
            }
        }

        public static void OnFilterChanged(object sender)
        {
            FilterChanged?.Invoke(sender, EventArgs.Empty);
        }

        #region Threadsafe RespondMessage

        private static object _messageLocker = new object();

        public static void RemoteMessage(RemoteMessage message)
        {
            switch (message.Action)
            {
                case MessageAction.NieweProductie:
                    if (message.Value is ProductieFormulier form)
                    {
                        AddProductie(form);
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
                            LogOut(null);
                    }

                    break;
            }
            RemoteMessageHandler rms;
            lock (_messageLocker)
            {
                rms = _OnRemoteMessage;
            }
            rms?.Invoke(message, null);
        }

        private static void BackgroundMessageHandler_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is RemoteMessage message)
                _OnRemoteMessage?.Invoke(message, null);
        }
        #endregion Threadsafe RespondMessage

        #endregion "Events"

        #region Disposing

        private bool _disposed;

        // Instantiate a SafeHandle instance.
        private readonly SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            IsLoaded = false;
            LogOut(null);
            //_emailcheckTimer?.Stop();
            //_emailcheckTimer?.Dispose();
            //_backemailchecker?.CancelAsync();
            //_backemailchecker?.Dispose();
            _fileWatchers?.ForEach(x => x.Dispose());
            _syncTimer?.Stop();
            _syncTimer?.Dispose();
            Database?.Dispose();
            ProductieProvider?.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            _fileWatchers = null;

            if (disposing)
                // Dispose managed state (managed objects).
                _safeHandle?.Dispose();

            _disposed = true;
        }

        #endregion Disposing
    }
}