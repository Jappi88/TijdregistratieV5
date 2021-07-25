using ProductieManager.Classes.Productie;
using ProductieManager.Classes.SqlLite;
using ProductieManager.Mailing;
using ProductieManager.Misc;
using ProductieManager.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace ProductieManager.Productie
{
    [Serializable]
    public class Manager
    {
        #region "Variables"

        public static TaakBeheer Taken { get; set; }
        public static SqlDatabase Database { get; private set; }
        private List<FileSystemWatcher> _fileWatchers = new List<FileSystemWatcher> { };
        private readonly TaskQueues _tasks = new TaskQueues();
        public static readonly string DbPath = Mainform.BootDir + "\\RPM_Data";
        public static readonly string TempPath = Mainform.BootDir + "\\RPM_Data\\Temp";
        public static readonly string BackupPath = Mainform.BootDir + "\\Backup";
        private static readonly string LogFilePath = "Log.txt";
        //private static readonly string SettingsDirectory = Mainform.BootDir + "\\RPM_Settings";

        [NonSerialized]
        private readonly BackgroundWorker _backemailchecker;

        [NonSerialized]
        private readonly Timer _emailcheckTimer = new Timer();

        [NonSerialized]
        private readonly Timer _syncTimer = new Timer();

        public string Versie { get { return "1.0"; } }
        public static UserSettings Opties { get;  set; }
        public static UserAccount LogedInGebruiker { get; set; }
        public static Logger Logbook { get; set; }
        public int Interval { get { return _emailcheckTimer.Interval; } set { _emailcheckTimer.Interval = value; } }

        public static Dictionary<string[], string[]> BewerkingenLijst = new Dictionary<string[], string[]> { };

        public string InstanceID { get; private set; }

        public List<ViewState> ViewStates = new List<ViewState> { };

        #endregion "Variables"

        #region "Constructor"

        public Manager()
        {
            InstanceID = Get8CharacterRandomString();//CpuID.ProcessorId();
            _backemailchecker = new BackgroundWorker();
            Database = new SqlDatabase(InstanceID);
            InitManager();
        }

        public string Get8CharacterRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path.Substring(0, 8);  // Return 8 character string
        }

        #endregion "Constructor"

        private void InitManager()
        {
            InitDirectories();
            Taken = new TaakBeheer(this);
            Logbook = new Logger(DbPath + $"\\{LogFilePath}");

            _backemailchecker.DoWork += _worker_DoWork;
            _backemailchecker.RunWorkerCompleted += _worker_RunWorkerCompleted;

            OnManagerLoaded += Manager_OnManagerLoaded;

            _emailcheckTimer.Tick += _timer_Tick;
            _syncTimer.Tick += _syncTimer_Tick;
            _tasks.OnRunComplete += _tasks_OnRunComplete;
            _tasks.RunInstanceComplete += _tasks_OnRunInstanceComplete;
            OnRemoteMessage += Manager_OnRemoteMessage;
        }

        private void InitDirectories()
        {
            if (!Directory.Exists(DbPath))
                Directory.CreateDirectory(DbPath);
            if (!Directory.Exists(TempPath))
                Directory.CreateDirectory(TempPath);
            if (!Directory.Exists(BackupPath))
                Directory.CreateDirectory(BackupPath);
        }

        private void Manager_OnRemoteMessage(RemoteMessage message, Manager instance)
        {
            switch (message.Action)
            {
                case MessageAction.NieweProductie:
                    if (message.Value is ProductieFormulier)
                    {
                        ProductieFormulier form = message.Value as ProductieFormulier;
                        if (form != null)
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
                    if (message.Value != null && message.Value is UserAccount)
                    {
                        UserAccount acc = message.Value as UserAccount;
                        if (LogedInGebruiker != null && LogedInGebruiker.Username.ToLower() == acc.Username.ToLower())
                            LogOut();
                    }
                    break;
            }
        }

        private void Manager_OnManagerLoaded(Manager instance)
        {
            AutoLogin();
            //set sync timer
            _syncTimer.Interval = 1000;
            _syncTimer.Start();
        }

        private TimeSpan _afsluittijd;

        private void _syncTimer_Tick(object sender, EventArgs e)
        {
            _syncTimer.Stop();
            //laten we controleren naar de instellingen en daar na handelen
            if (Opties == null)
                return;

            if (Opties.SluitPcAf && DateTime.Now.TimeOfDay >= _afsluittijd)
            {
                TimeSpan tijd = new TimeSpan();
                if (Shutdown(ref tijd) == DialogResult.OK)
                {
                    _afsluittijd = DateTime.Now.TimeOfDay.Add(tijd);
                    if (tijd.TotalMinutes == 0)
                    {
                        Application.Exit();
                        Functions.Shutdown();
                    }
                }
            }
            _syncTimer.Start();
        }

        #region OldDb

        public ProductieFormulier[] UpdateFormOldDbProducties()
        {
            List<ProductieFormulier> xreturn = new List<ProductieFormulier> { };
            ProductieFormulier[] forms = new ProductieFormulier[] { };
            List<string> filenames = new List<string> { };

            try
            {
                filenames.AddRange(new string[] { DbPath + "\\Database.db", DbPath + "\\DatabaseGereed.db" });
                Database.ProductieFormulieren.DeleteAll();
                foreach (string filename in filenames)
                {
                    //string dbfile = DbPath + "\\" + filename;

                    using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
                    {
                        if (fs.Length > 4)
                        {
                            //lees versie
                            int count = fs.ReadInt32();
                            byte[] buffer = new byte[count];
                            fs.Read(buffer, 0, buffer.Length);
                            string versie = Functions.ByteArrayToString(buffer);
                            buffer = new byte[8];
                            fs.Read(buffer, 0, 8);
                            string rslt = BitConverter.ToString(buffer).Replace("-", "");
                            count = fs.ReadInt32();

                            for (int i = 0; i < count; i++)
                            {
                                ProductieFormulier x = fs.ReadProductie(this);

                                if (x != null)
                                {
                                    bool added = Database.UpSert(x, "Productie Toegevoegd");
                                    //if(x.Bewerkingen != null && x.Bewerkingen.Length > 0)
                                    //{
                                    //    foreach (Bewerking b in x.Bewerkingen)
                                    //        b.OnBewerkingChanged += BewerkingChanged;
                                    //}
                                    xreturn.Add(x);
                                }
                            }
                        }
                        fs.Close();
                    }
                }
                forms = Database.ProductieFormulieren.FindAll().ToArray();
            }
            catch { return new ProductieFormulier[] { }; }
            return forms;
        }

        private void InitPersoneel()
        {
            PersoneelsLijst p = new PersoneelsLijst().LoadPersoneelsLijst(this, false);
            if (p != null && p.PersoneelLeden != null && p.PersoneelLeden.Length > 0)
            {
                Personeel[] personen = Database.GetAllPersoneel().ToArray();
                Database.PersoneelLijst.DeleteAll();
                Database.PersoneelLijst.InsertBulk(p.PersoneelLeden);
                personen = Database.GetAllPersoneel();
            }
        }

        #endregion OldDb

        #region "Public Methods"

        public void StartMonitor()
        {
            if (!_emailcheckTimer.Enabled)
                _emailcheckTimer.Start();
        }

        public void StopMonitor()
        {
            if (_emailcheckTimer.Enabled)
                _emailcheckTimer.Stop();
        }

        public bool Load()
        {
            try
            {
                bool cancel = false;
                ManagerLoading(ref cancel);
                if (cancel)
                    return false;
                Logbook.Load();
                BewerkingenLijst = Functions.LoadBewerkingLijst("Bewerkingen.txt");
                if (Opties == null)
                    LoadSettings();
                ManagerLoaded();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateAccount(UserAccount account)
        {
            return Database.UpSert(account);
        }

        public bool Login(string username, string password)
        {
            try
            {
                var x = Database.GetAccount(username);
                if (x != null)
                {
                    if (x.ValidateUser(username, password))
                    {
                        LogedInGebruiker = x;
                        LoginChanged();
                        return true;
                    }
                    else
                        LogedInGebruiker = null;
                    LoginChanged();
                    return false;
                }
                else throw new Exception("Gebruiker bestaat niet!");
            }
            catch (Exception ex) { throw ex; }
        }

        public bool Login(string username)
        {
            try
            {
                var x = Database.GetAccount(username);
                if (x != null)
                {
                    LogedInGebruiker = x;
                    LoginChanged();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        public void LogOut()
        {
            LogedInGebruiker = null;
            LoginChanged();
        }

        public ProductieFormulier Werktaan(string personeelnaam)
        {
            ProductieFormulier[] prods = GetProducties(ViewState.Gestart, false);
            return prods.Where(t => t.State == ProductieState.Gestart && (t.Bewerkingen?.Where(x => x.GetPersoneel().Where(x => x.PersoneelNaam.ToLower() == personeelnaam.ToLower()).FirstOrDefault() != null).FirstOrDefault() != null)).FirstOrDefault();
        }

        public bool LoadSettings()
        {
            try
            {
                string os = CpuID.ProcessorId();
                string name = LogedInGebruiker == null ? $"Default[{os}]" : LogedInGebruiker.Username;
                if (Opties != null && Opties.Username.ToLower() == name.ToLower())
                    return false;
                if (Opties != null && Opties.Username.ToLower() != name.ToLower())
                    SaveSettings(Opties, false, true);

                try
                {
                    Opties = Database.GetSetting(name);
                }
                catch (Exception ex) { Database.DeleteSettings(name); }

                if (Opties == null)
                {
                    UserSettings opties = new UserSettings();
                    opties.Username = name;
                    Database.UpSert(opties);
                    Opties = opties;
                }
                if (Opties != null)
                {
                    if (Opties.WeergaveFilters == null || Opties.WeergaveFilters.Length == 0)
                        Opties.WeergaveFilters = new ViewState[] { ViewState.Alles };
                    if (!Directory.Exists(DbPath))
                        Directory.CreateDirectory(DbPath);
                    SettingsChanged();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { return false; }
        }

        public bool SaveSettings(bool force)
        {
            return SaveSettings(Opties, true, force);
        }

        public bool SaveSettings(UserSettings settings, bool replace, bool triggersaved, bool force = false)
        {
            try
            {
                bool changed = Opties == null ? true : force;
                if (settings == null)
                {
                    settings = new UserSettings();
                    if (Manager.LogedInGebruiker != null)
                        settings.Username = Manager.LogedInGebruiker.Username;
                    changed = true;
                }

                bool cancel = false;
                SettingsChanging(settings, ref cancel);
                if (cancel)
                    return false;

                if (!changed)
                    changed = !settings.PublicInstancePropertiesEqual<UserSettings>(Opties);

                if (changed)
                {
                    //update user autologin
                    if (LogedInGebruiker != null)
                    {
                        UserAccount user = Database.GetAccount(settings.Username);
                        if (user != null)
                        {
                            if (user.AutoLogIn != settings.AutoLogin)
                            {
                                user.AutoLogIn = settings.AutoLogin;
                                user.OsID = CpuID.ProcessorId();

                                if (user.Username.ToLower() == LogedInGebruiker.Username.ToLower())
                                    LogedInGebruiker = user;
                                Database.UpSert(user.Username, user, "Auto Login Aangepast");
                            }
                        }
                    }
                    Functions.SetAutoStartOnBoot(settings.StartNaOpstart);
                    settings.Save();
                }
                if (replace)
                {
                    Opties = settings;
                    if (triggersaved)
                        SettingsChanged();
                }
                return changed;
            }
            catch (Exception ex) { return false; }
        }

        public void SetSettings(UserSettings options)
        {
            if (options == null)
                return;
            Interval = options.SyncInterval;
            //load files sync instances
            LoadSyncDirectories();
            _afsluittijd = options.AfsluitTijd;
            ViewStates = options.WeergaveFilters.ToList();
            //UpdateFormOldDbProducties();
            // Manager.Database.PersoneelLijst.DeleteAll();
            // Manager.Database.LiteDatabase.Rebuild();
            //InitPersoneel();
        }

        public void AddProductie(string[] files, bool rename)
        {
            try
            {
                _tasks.Add(new TaskManager(this, files) { CleanUp = rename });
            }
            catch { }
        }

        public static ProductieFormulier[] GetProducties(ViewState[] states, bool filter)
        {
            List<ProductieFormulier> items = new List<ProductieFormulier> { };
            if (states.Any(x => x != ViewState.Gereed))
               items.AddRange(Database.GetAllProducties().Where(x => x.IsValidState(states)));
            if (states.Any(x => x == ViewState.Gereed ))
                items.AddRange(Database.GetAllGereedProducties());
            if (filter)
                return FilterForms(items.ToArray());
            return items.ToArray();
        }

        public ProductieFormulier[] GetProducties(ViewState state, bool filter)
        {
            return GetProducties(new ViewState[] { state }, filter);
        }

        public static bool LoadProducties()
        {
            ProductieFormulier[] forms = GetProducties(Manager.Opties.WeergaveFilters, false);
            ProductiesLoaded(Database, forms);
            return true;
        }

        public static ProductieFormulier[] FilterForms(ProductieFormulier[] forms)
        {
            List<ProductieFormulier> xreturn = new List<ProductieFormulier> { };
            UserSettings s = Manager.Opties;
            foreach (ProductieFormulier x in forms)
            {
                //hier gaan we controleren of we de producties kunnen pakken pakken op basis van ingestelde waardes
                int xcount = 0;
                bool valid = false;
                if (s != null && !s.ToonAlles && x.Bewerkingen != null)
                {
                    if (s.ToonVolgensAfdelingen)
                    {
                        if (s.Afdelingen == null)
                            continue;
                        foreach (Bewerking bew in x.Bewerkingen)
                        {
                            var xs = Manager.BewerkingenLijst.Where(t => t.Value.Contains(bew.Naam)).FirstOrDefault().Key;
                            if (xs != null && xs.Length > 0)
                            {
                                if (xs.Where(t => s.Afdelingen.Where(x => x.ToLower() == t.ToLower()).Any()).Any())
                                    xcount++;
                            }
                        }
                        if (xcount == 0)
                            continue;
                    }
                    else if (s.ToonVolgensBewerkingen)
                    {
                        if (s.Bewerkingen == null)
                            continue;
                        if (!s.Bewerkingen.Where(t => x.Bewerkingen.Where(x => x.Naam.ToLower() == t.ToLower()).Any()).Any())
                            continue;
                    }
                    else if (s.ToonAllesVanBeide)
                    {
                        valid = s.Bewerkingen != null && s.Bewerkingen.Where(t => x.Bewerkingen.Where(x => x.Naam.ToLower() == t.ToLower()).Any()).Any();
                        xcount = 0;
                        foreach (Bewerking bew in x.Bewerkingen)
                        {
                            var xs = Manager.BewerkingenLijst.Where(t => t.Value.Contains(bew.Naam)).FirstOrDefault().Key;
                            if (xs != null && xs.Length > 0)
                            {
                                if (xs.Where(t => s.Afdelingen.Where(x => x.ToLower() == t.ToLower()).Any()).Any())
                                    xcount++;
                            }
                        }

                        if (!valid && xcount == 0)
                            continue;
                    }
                }
                xreturn.Add(x);
            }
            return xreturn.ToArray();
        }

        public bool AddProductie(ProductieFormulier prod)
        {
            try
            {
                return Database.UpSert(prod, "Nieuw Toegevoegd!");
            }
            catch { return false; }
        }

        public int AddProducties(ProductieFormulier[] prods)
        {
            try
            {
                if (prods == null || prods.Length == 0)
                    return -1;
                int inserted = 0;
                foreach (var prod in prods)
                    if (AddProductie(prod))
                        inserted++;
                return inserted;
            }
            catch { return -1; }
        }

        public bool RemoveProductie(ProductieFormulier prod, bool realremove)
        {
            return RemoveProductie(prod.ProductieNr, realremove);
        }

        public int RemoveProducties(ProductieFormulier[] prods, bool realremove)
        {
            try
            {
                if (prods == null || prods.Length == 0)
                    return 0;
                int removed = 0;
                List<ProductieFormulier> remove = new List<ProductieFormulier> { };

                foreach (var prod in prods)
                {
                    if (prod.State == ProductieState.Verwijderd && realremove)
                        remove.Add(prod);
                    else if (prod.State == ProductieState.Gestart && prod.Bewerkingen != null)
                    {
                        foreach (Bewerking b in prod.Bewerkingen)
                            if (b.State == ProductieState.Gestart)
                                b.StopProductie(true);
                    }
                    if (prod.State != ProductieState.Gestart && prod.State != ProductieState.Verwijderd)
                    {
                        prod.State = ProductieState.Verwijderd;
                        prod.DatumVerwijderd = DateTime.Now;
                        if (Database.UpSert(prod))
                        {
                            removed++;
                            RemoteProductie.RespondByEmail(prod, $"Productie [{prod.ProductieNr.ToUpper()}] is zojuist verwijderd");
                        }
                    }
                }

                if (remove.Count > 0)
                {
                    removed += Database.Delete(remove.ToArray());
                }
                return removed;
            }
            catch { return -1; }
        }

        public bool RemoveProductie(string prodnr, bool realremove)
        {
            try
            {
                bool removed = false;
                ProductieFormulier current = Database.GetProductie(prodnr);
                if (current != null)
                {
                    if (realremove && current.State == ProductieState.Verwijderd)
                    {
                        if (Database.Delete(current))
                        {
                            RemoteProductie.RespondByEmail(current, $"Productie [{current.ProductieNr.ToUpper()}] is zojuist helemaal verwijderd uit de Database!");
                        }
                    }
                    else if (current.State != ProductieState.Verwijderd)
                    {
                        if (current.State == ProductieState.Gestart && current.Bewerkingen != null)
                        {
                            foreach (Bewerking b in current.Bewerkingen)
                                if (b.State == ProductieState.Gestart)
                                    b.StopProductie(true);
                        }
                        current.DatumVerwijderd = DateTime.Now;
                        current.State = ProductieState.Verwijderd;
                        if (Database.UpSert(current))
                            RemoteProductie.RespondByEmail(current, $"Productie [{current.ProductieNr.ToUpper()}] is zojuist verwijderd");
                    }
                }

                return removed;
            }
            catch { return false; }
        }

        public bool ReplaceProductie(ProductieFormulier oldform, ProductieFormulier newform)
        {
            if (oldform == null || newform == null)
                return false;
            return Database.Replace(oldform, newform);
        }

        public int AantalProductiesGemaakt(string artikelnr)
        {
            int xreturn = 0;
            ProductieFormulier[] forms = Database.GetProducties(x => x.ArtikelNr.ToLower() == artikelnr.ToLower() && x.State == ProductieState.Gereed);
            xreturn = forms.Length;
            forms = null;
            return xreturn;
        }

        #endregion "Public Methods"

        #region "Private Methods"

        private bool BackupFiles(string[] files, string path)
        {
            if (files == null || files.Length == 0)
                return false;
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                foreach (string file in files)
                {
                    string filename = Path.GetFileName(file);
                    string ext = Path.GetExtension(file);
                    int count = Directory.GetFiles(path, "*.db").Where(x => Path.GetFileName(x).Replace(ext, "").Split('[')[0].ToLower() == filename.Replace(ext, "").ToLower()).Count();
                    if (count > 0)
                        filename = $"{filename.Replace(ext, "") }[{count}]";
                    filename += ext;
                    if (File.Exists(file))
                        File.Copy(file, path + "\\" + filename, true);
                }
                return true;
            }
            catch { return false; }
        }

        private void LoadUnloadedFiles()
        {
            try
            {
                List<string> files = new List<string> { };
                if (Opties != null && Opties.SyncLocaties != null)
                {
                    foreach (string s in Opties.SyncLocaties)
                        files.AddRange(Directory.GetFiles(s).Where(t => !t.Contains("_old")).ToArray());
                    if (files.Count > 0)
                    {
                        AddProductie(files.ToArray(), true);
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        private bool AutoLogin()
        {
            try
            {
                UserAccount[] users = Database.GetAllAccounts();
                foreach (UserAccount user in users)
                {
                    if ((LogedInGebruiker == null || LogedInGebruiker.Username.ToLower() != user.Username.ToLower()) && user.AutoLogIn && user.OsID == CpuID.ProcessorId())
                    {
                        if (Login(user.Username))
                            return true;
                    }
                }
            }
            catch { }
            return false;
        }

        private void Sw_Created(object sender, FileSystemEventArgs e)
        {
            if (!e.Name.ToLower().Contains("_old"))
                AddProductie(new string[] { e.FullPath }, true);
        }

        private bool LoadSyncDirectories()
        {
            try
            {
                if (_fileWatchers == null)
                    _fileWatchers = new List<FileSystemWatcher> { };
                if (Opties.SyncLocaties != null && Opties.SyncLocaties.Length > 0)
                {
                    foreach (string s in Opties.SyncLocaties)
                    {
                        if (!Directory.Exists(s))
                            continue;
                        if (!_fileWatchers.Any(t => t.Path.ToLower() == s.ToLower()))
                        {
                            var sw = new FileSystemWatcher(s);
                            sw.Changed += Sw_Created;
                            sw.NotifyFilter = NotifyFilters.LastWrite;
                            sw.EnableRaisingEvents = true;
                            _fileWatchers.Add(sw);
                        }
                    }
                    foreach (FileSystemWatcher fs in _fileWatchers)
                    {
                        if (!Opties.SyncLocaties.Contains(fs.Path))
                        {
                            fs.Dispose();
                            _fileWatchers.Remove(fs);
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

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RemoteMessage[] messages = (RemoteMessage[])e.Result;
            if (messages != null && messages.Length > 0)
            {
                foreach (RemoteMessage message in messages)
                {
                    try
                    {
                        _tasks.Add(new TaskManager(this, message));
                    }
                    catch { }
                }
            }
            StartMonitor();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (!_backemailchecker.IsBusy)
            {
                StopMonitor();
                _backemailchecker.RunWorkerAsync();
            }
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = RemoteProductie.ControlleerOpMessages();
        }

        private void _tasks_OnRunInstanceComplete(object sender, EventArgs e)
        {
            if (sender is IQueue)
            {
                IQueue instance = sender as IQueue;
                if (instance.Results != null && instance.Results.Length > 0)
                {
                    foreach (RemoteMessage rm in instance.Results)
                    {
                        RemoteMessage(rm);
                    }
                }
            }
        }

        private void _tasks_OnRunComplete()
        {
        }

        #endregion "Private Methods"

        #region "Events"

        public static event FormulierActieHandler OnFormulierActie;

        public static event FormulierChangedHandler OnFormulierChanged;

        public static event FormulierChangedHandler OnFormulierDeleted;

        public static event BewerkingChangedHandler OnBewerkingChanged;

        public static event ProductiesLoadedHandler OnProductiesLoaded;

        public static event PersoneelChangedHandler OnPersoneelChanged;

        public static event PersoneelChangedHandler OnPersoneelDeleted;

        public static event AccountChangedHandler OnAccountChanged;

        public static event UserSettingsChangedHandler OnSettingsChanged;

        public event RemoteMessageHandler OnRemoteMessage;

        public event LogInChangedHandler OnLoginChanged;

        public event UserSettingsChangingHandler OnSettingsChanging;

        public event ManagerLoadingHandler OnManagerLoading;

        public event ManagerLoadedHandler OnManagerLoaded;

        public event ShutdownHandler OnShutdown;

        public static void ProductiesLoaded(object sender, ProductieFormulier[] forms)
        {
            OnProductiesLoaded?.Invoke(sender, FilterForms(forms));
        }

        public DialogResult Shutdown(ref TimeSpan verlengtijd)
        {
            if (OnShutdown != null)
                return OnShutdown.Invoke(this, ref verlengtijd);
            return DialogResult.Cancel;
        }

        public void ManagerLoading(ref bool cancel)
        {
            OnManagerLoading?.Invoke(this, ref cancel);
        }

        public void ManagerLoaded()
        {
            OnManagerLoaded?.Invoke(this);
        }

        public void SettingsChanging(UserSettings settings, ref bool cancel)
        {
            OnSettingsChanging?.Invoke(this, settings, ref cancel);
        }

        public void LoginChanged()
        {
            LoadSettings();
            if (LogedInGebruiker == null && Taken != null)
                Taken.StopBeheer();
            OnLoginChanged?.Invoke(LogedInGebruiker, this);
        }

        public void SettingsChanged()
        {
            OnSettingsChanged?.Invoke(this, Opties);
        }

        public static void FormulierActie(ProductieFormulier formulier, Bewerking bew, AktieType type)
        {
            OnFormulierActie?.Invoke(formulier, bew, type);
        }

        public static void FormulierChanged(object sender, ProductieFormulier formulier)
        {
            Taken.UpdateProductieTaken(formulier);
            OnFormulierChanged?.Invoke(sender, formulier);
        }

        public static void FormulierDeleted(object sender, ProductieFormulier formulier)
        {
            OnFormulierDeleted?.Invoke(sender, formulier);
        }

        public static void BewerkingChanged(object sender, Bewerking bew)
        {
            Taken.UpdateProductieTaken(bew.Parent);
            OnBewerkingChanged?.Invoke(sender, bew);
        }

        public static void PersoneelChanged(object sender, Personeel pers)
        {
            OnPersoneelChanged?.Invoke(sender, pers);
        }

        public static void PersoneelDeleted(object sender, Personeel pers)
        {
            OnPersoneelDeleted?.Invoke(sender, pers);
        }

        public static void AccountChanged(object sender, UserAccount account)
        {
            OnAccountChanged?.Invoke(sender, account);
        }

        public static void UserSettingChanged(object sender, UserSettings setting)
        {
            if (setting != null && Opties != null && setting.Username.ToLower() == Opties.Username.ToLower())
            {
                Opties = setting;
                OnSettingsChanged?.Invoke(sender, setting);
            }
        }

        public void RemoteMessage(RemoteMessage message)
        {
            OnRemoteMessage?.Invoke(message, this);
        }

        #endregion "Events"
    }
}