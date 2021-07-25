using LiteDB;
using Microsoft.Win32.SafeHandles;
using ProductieManager.Misc;
using ProductieManager.Productie;
using ProductieManager.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading;

namespace ProductieManager.Classes.SqlLite
{
    public class SqlDatabase : IDisposable
    {
        public DateTime LastDbCheck { get; set; } = DateTime.Now;
        public DateTime LastProductieUpdate { get; set; }
        public DateTime LastPersoneelUpdate { get; set; }
        public DateTime LastAccountUpdate { get; set; }
        public DateTime LastSettingUpdate { get; set; }

        public string OwnerId { get; private set; }
        public int SyncInterval { get; set; }
        private BackgroundWorker _worker;

        public LiteDB.LiteDatabase LiteDatabase { get; private set; }
        public LiteDB.LiteDatabase ChangedDb { get; private set; }
        public LiteDB.LiteDatabase GereedDb { get; private set; }

        public string SqlDatabaseFileName { get; private set; }
        public string ChangedDbFileName { get; private set; }
        public string GereedDbFileName { get; private set; }

        public ILiteCollection<Personeel> PersoneelLijst { get; private set; }
        public ILiteCollection<ProductieFormulier> ProductieFormulieren { get; private set; }
        public ILiteCollection<UserAccount> UserAccounts { get; private set; }
        public ILiteCollection<UserSettings> AllSettings { get; private set; }

        public ILiteCollection<UserChange> ChangeLog { get; private set; }
        public ILiteCollection<ProductieFormulier> GereedFormulieren { get; private set; }

        public SqlDatabase(string pcid)
        {
            OwnerId = pcid;
            LastProductieUpdate = DateTime.Now;
            LastPersoneelUpdate = DateTime.Now;
            LastAccountUpdate = DateTime.Now;
            LastSettingUpdate = DateTime.Now;
            _worker = new BackgroundWorker();
            _worker.DoWork += _worker_DoWork;
            _worker.WorkerSupportsCancellation = true;
            SyncInterval = 1000;
            _worker.ProgressChanged += _worker_ProgressChanged;
            _worker.WorkerReportsProgress = true;

            string dbfilename = "SqlDatabase.db";
            string changeddb = "LogDb.db";
            string Gereeddb = "GereedDb.db";
            SqlDatabaseFileName = Manager.DbPath + "\\" + dbfilename;
            ChangedDbFileName = Manager.DbPath + "\\" + changeddb;
            GereedDbFileName = Manager.DbPath + "\\" + Gereeddb;

            LiteDatabase = new LiteDatabase(new ConnectionString(SqlDatabaseFileName) { Connection = ConnectionType.Shared, ReadOnly = false,});
            ChangedDb = new LiteDatabase(new ConnectionString(ChangedDbFileName) { Connection = ConnectionType.Shared, ReadOnly = false, });
            GereedDb = new LiteDatabase(new ConnectionString(GereedDbFileName) { Connection = ConnectionType.Shared, ReadOnly = false, });

            PersoneelLijst = LiteDatabase.GetCollection<Personeel>("Personeel");
           // PersoneelLijst.EnsureIndex("personeelnaam");

            ProductieFormulieren = LiteDatabase.GetCollection<ProductieFormulier>("ProductieFormulieren");
            //ProductieFormulieren.EnsureIndex("productienr");
            //ProductieFormulieren.DeleteAll();

            UserAccounts = LiteDatabase.GetCollection<UserAccount>("UserAccounts");
           // UserAccounts.EnsureIndex("Username");

            AllSettings = LiteDatabase.GetCollection<UserSettings>("AllSettings");
            //AllSettings.EnsureIndex("Username");

            ChangeLog = LiteDatabase.GetCollection<UserChange>("ChangeLog");
            //ChangeLog.EnsureIndex("pcid");

            GereedFormulieren = LiteDatabase.GetCollection<ProductieFormulier>("GereedFormulieren");
            //GereedFormulieren.EnsureIndex("productienr");
            // LiteDatabase.Rebuild();
        }

        #region BackgroundWorker

        private void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                try
                {
                    if (e.UserState is Personeel[])
                    {
                        var acc = e.UserState as Personeel[];
                        if (acc != null && acc.Length > 0)
                        {
                            LastPersoneelUpdate = DateTime.Now;
                            foreach (var pers in acc)
                                Manager.PersoneelChanged(this, pers);
                        }
                    }
                    else if (e.UserState is ProductieFormulier[])
                    {
                        var acc = e.UserState as ProductieFormulier[];
                        if (acc != null && acc.Length > 0)
                        {
                            LastProductieUpdate = DateTime.Now;
                            foreach (var prod in acc)
                                Manager.FormulierChanged(this, prod);
                            //  Manager.LoadProducties();
                        }
                    }
                    else if (e.UserState is UserAccount[])
                    {
                        var acc = e.UserState as UserAccount[];
                        if (acc != null && acc.Length > 0)
                        {
                            LastAccountUpdate = DateTime.Now;
                            foreach (var account in acc)
                                Manager.AccountChanged(this, account);
                        }
                    }
                    else if (e.UserState is UserSettings)
                    {
                        LastSettingUpdate = DateTime.Now;
                        Manager.UserSettingChanged(this, e.UserState as UserSettings);
                    }
                    else if(e.UserState is UserChange)
                    {
                        UserChange change = e.UserState as UserChange;
                        if (change != null && change.Change != null)
                            change.CreateMessage().Notify();
                    }
                }
                catch { }
            }
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //elke db item langs gaan om te kijken of er wat gewijzigd is.
            while (!e.Cancel && !_disposed)
            {
                Thread.Sleep(SyncInterval);
                if (e.Cancel)
                    break;
                try
                {
                    UserChange[] changes = GetLastDbChanges();
                    if (changes != null && changes.Length > 0)
                    {
                        foreach (UserChange change in changes.Where(x => x.DbIds.Any(s => s.Value > LastDbCheck)))
                        {
                            _worker.ReportProgress(0, change);
                            foreach (var v in change.DbIds.Where(x => x.Value > LastDbCheck))
                            {
                                #region Database Checks
                                switch (v.Key)
                                {
                                    case 0:
                                        if (ProductieFormulieren != null && !e.Cancel)
                                        {
                                            //pak alle productieformulieren die gewijzigd zijn door een andere systeem, en naa onze laatste update.
                                            ProductieFormulier[] forms = ProductieFormulieren.Find(x => x.LastChanged != null && x.LastChanged.TimeChanged > LastProductieUpdate && x.LastChanged.PcId != OwnerId).ToArray();
                                            if (forms != null && forms.Length > 0)
                                                _worker.ReportProgress(0, forms);
                                            if (e.Cancel)
                                                break;
                                        }
                                        break;
                                    case 1:
                                        if (GereedFormulieren != null && !e.Cancel)
                                        {
                                            //pak alle productieformulieren die gewijzigd zijn door een andere systeem, en naa onze laatste update.
                                            ProductieFormulier[] forms = GereedFormulieren.Find(x => x.LastChanged != null && x.LastChanged.TimeChanged > LastProductieUpdate && x.LastChanged.PcId != OwnerId).ToArray();
                                            if (forms != null && forms.Length > 0)
                                                _worker.ReportProgress(0, forms);
                                            if (e.Cancel)
                                                break;
                                        }
                                        break;
                                    case 2:
                                        if (PersoneelLijst != null && !e.Cancel)
                                        {
                                            //pak alle personen die gewijzigd zijn door een andere systeem, en naa onze laatste update.
                                            Personeel[] personen = PersoneelLijst.Find(x => x.LastChanged != null && x.LastChanged.TimeChanged > LastPersoneelUpdate && x.LastChanged.PcId != OwnerId).ToArray();
                                            if (personen != null && personen.Length > 0)
                                                _worker.ReportProgress(0, personen);
                                            if (e.Cancel)
                                                break;
                                        }
                                        break;
                                    case 3:
                                        if (UserAccounts != null && !e.Cancel)
                                        {
                                            //pak alle accounts die gewijzigd zijn door een andere systeem, en naa onze laatste update.
                                            UserAccount[] accounts = UserAccounts.Find(x => x.LastChanged != null && x.LastChanged.TimeChanged > LastAccountUpdate && x.LastChanged.PcId != OwnerId).ToArray();
                                            if (accounts != null && accounts.Length > 0)
                                                _worker.ReportProgress(0, accounts);
                                            if (e.Cancel)
                                                break;
                                        }
                                        break;
                                    case 4:
                                        if (AllSettings != null && !e.Cancel)
                                        {
                                            //pak alle opties die gewijzigd zijn door een andere systeem, en naa onze laatste update.
                                            UserSettings[] opties = AllSettings.Find(x => x.LastChanged != null && x.LastChanged.TimeChanged > LastSettingUpdate && x.LastChanged.PcId != OwnerId).ToArray();
                                            if (opties != null && opties.Length > 0)
                                                foreach (var xs in opties)
                                                {
                                                    if (Manager.Opties != null && Manager.Opties.Username.ToLower() == xs.Username.ToLower())
                                                        _worker.ReportProgress(0, xs);
                                                    if (e.Cancel)
                                                        break;
                                                }
                                        }
                                        break;

                                }
                                #endregion
                            }
                        }
                        LastDbCheck = DateTime.Now;
                    }
                }
                catch(Exception ex) { }
            }
        }

        public void StartSync()
        {
            if (_worker == null || _worker.IsBusy)
                return;
            _worker.RunWorkerAsync();
        }

        public void StopSync()
        {
            if (_worker != null && _worker.IsBusy)
                _worker.CancelAsync();
        }

        #endregion BackgroundWorker

        #region ProductieFormulieren

        public ProductieFormulier GetProductie(string productienr, string artikelnr)
        {
            if (_disposed || ProductieFormulieren == null || (productienr == null && artikelnr == null))
                return null;
            var query = ProductieFormulieren
                    .Include(x => x.Bewerkingen)
                   .Include(x => x.Materialen);
            ProductieFormulier form = null;
            if (productienr != null)
                form = query.FindById(productienr);
            else if (artikelnr != null)
                form = query.FindOne(x => x.ArtikelNr.ToLower() == artikelnr.ToLower());

            if (form == null)
            {
                query = GereedFormulieren
                    .Include(x => x.Bewerkingen)
                   .Include(x => x.Materialen);
                if (productienr != null)
                    form = query.FindById(productienr);
                else if (artikelnr != null)
                    form = query.FindOne(x => x.ArtikelNr.ToLower() == artikelnr.ToLower());
            }
            return form;
        }

        public ProductieFormulier GetProductie(string productienr)
        {
            return GetProductie(productienr, null);
        }

        public ProductieFormulier[] GetProducties(Expression<Func<ProductieFormulier, bool>> predicate, int page = 0, int view = int.MaxValue)
        {
            if (_disposed || ProductieFormulieren == null)
                return new ProductieFormulier[] { };
            var query = ProductieFormulieren
                   .Include(x => x.Bewerkingen)
                   .Include(x => x.Materialen);
            List<ProductieFormulier> forms = query.Find(predicate, page, view).ToList();

              var query2 = GereedFormulieren
                   .Include(x => x.Bewerkingen)
                   .Include(x => x.Materialen);
            forms.AddRange(query2.Find(predicate, page, view));
            return forms.ToArray();
        }

        public ProductieFormulier[] GetAllProducties()
        {
            if (_disposed || ProductieFormulieren == null)
                return new ProductieFormulier[] { };
            var query = ProductieFormulieren
                   .Include(x => x.Bewerkingen)
                   .Include(x => x.Materialen);
            return query.FindAll().ToArray();
        }

        public ProductieFormulier[] GetAllGereedProducties()
        {
            if (_disposed || ProductieFormulieren == null)
                return new ProductieFormulier[] { };
            var query = GereedFormulieren
                   .Include(x => x.Bewerkingen)
                   .Include(x => x.Materialen);
            return query.FindAll().ToArray();
        }

        public bool UpSert(ProductieFormulier form, string change)
        {
            return UpSert(form.ProductieNr, form, change);
        }

        public bool UpSert(ProductieFormulier form)
        {
            return UpSert(form.ProductieNr, form, "ProductieFormulier Update");
        }

        public bool UpSert(string id, ProductieFormulier form, string change)
        {
            if (_disposed || ProductieFormulieren == null || id == null || form == null)
                return false;
            try
            {
                form.LastChanged = new UserChange() { Change = change,PcId = OwnerId, User = Manager.Opties == null ? "Default" : Manager.Opties.Username };
                UpdateChange(form.LastChanged,0);
                if (form.State == ProductieState.Gereed)
                {
                    ProductieFormulieren.Delete(form.ProductieNr);
                    GereedFormulieren.Upsert(id, form);
                }
                else
                    ProductieFormulieren.Upsert(id, form);
                Manager.FormulierChanged(this, form);
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public int UpSert(ProductieFormulier[] forms, string change)
        {
            if (_disposed || ProductieFormulieren == null || forms == null)
                return -1;
            try
            {
                int xreturn = 0;
                foreach (var form in forms)
                {
                    if (UpSert(form, change))
                        xreturn++;
                }
                return xreturn;
            }
            catch { return -1; }
        }

        public bool Delete(ProductieFormulier form)
        {
            if (_disposed || ProductieFormulieren == null || form == null)
                return false;
            try
            {
                if (ProductieFormulieren.Delete(form.ProductieNr))
                {
                    form.LastChanged = new UserChange() { Change = $"[{form.ProductieNr}] Productie Verwijderd", PcId = OwnerId, User = Manager.Opties == null ? "Default" : Manager.Opties.Username };
                    UpdateChange(form.LastChanged,0);
                    Manager.FormulierDeleted(this, form);
                    return true;
                }
                else if (GereedFormulieren.Delete(form.ProductieNr))
                {
                    form.LastChanged = new UserChange() { Change = $"[{form.ProductieNr}] Productie Verwijderd", PcId = OwnerId, User = Manager.Opties == null ? "Default" : Manager.Opties.Username };
                    UpdateChange(form.LastChanged, 1);
                    Manager.FormulierDeleted(this, form);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public int Delete(ProductieFormulier[] forms)
        {
            if (_disposed || ProductieFormulieren == null || forms == null)
                return -1;
            try
            {
                int xreturn = 0;
                foreach (var v in forms)
                    if (ProductieFormulieren.Delete(v.ProductieNr))
                        xreturn++;

                if (xreturn > 0)
                {
                    var changed = new UserChange() { Change = $"{xreturn} Producties Verwijderd", PcId = OwnerId, User = Manager.Opties == null ? "Default" : Manager.Opties.Username };
                    UpdateChange(changed,0);
                    Manager.LoadProducties();
                }
                else
                {
                    xreturn = 0;
                    foreach (var v in forms)
                        if (GereedFormulieren.Delete(v.ProductieNr))
                            xreturn++;
                    if (xreturn > 0)
                    {
                        var changed = new UserChange() { Change = $"{xreturn} Gereed Producties Verwijderd", PcId = OwnerId, User = Manager.Opties == null ? "Default" : Manager.Opties.Username };
                        UpdateChange(changed, 1);
                        Manager.LoadProducties();
                    }
                }
                
                return xreturn;
            }
            catch { return -1; }
        }

        public bool Replace(ProductieFormulier oldform, ProductieFormulier newform)
        {
            if (oldform == null)
                return false;
            return Replace(oldform.ProductieNr, newform);
        }

        public bool Replace(string id, ProductieFormulier newform)
        {
            if (_disposed || ProductieFormulieren == null || id == null || newform == null)
                return false;
            try
            {

                newform.ProductieNr = id;
                newform.LastChanged = new UserChange() { Change = $"[{id}] Productie Update", PcId = OwnerId, User = Manager.Opties == null ? "Default" : Manager.Opties.Username };
                
                if (ProductieFormulieren.Update(id, newform))
                {
                    UpdateChange(newform.LastChanged,0);
                    Manager.FormulierChanged(this, newform);
                    return true;
                }
                if (newform.State == ProductieState.Gereed)
                {
                    GereedFormulieren.Upsert(id, newform);
                    UpdateChange(newform.LastChanged, 1);
                    Manager.FormulierChanged(this, newform);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public bool Exist(ProductieFormulier form)
        {
            if (_disposed || ProductieFormulieren == null || form == null)
                return false;
            try
            {
                return ProductieFormulieren.FindById(form.ProductieNr) != null;
            }
            catch { return false; }
        }

        public bool ProductieExist(string id)
        {
            if (_disposed || ProductieFormulieren == null || id == null)
                return false;
            try
            {
                return ProductieFormulieren.FindById(id) != null;
            }
            catch { return false; }
        }

        #endregion ProductieFormulieren

        #region UserAccounts

        public UserAccount GetAccount(string username)
        {
            if (_disposed || UserAccounts == null || username == null)
                return null;
            try
            {
                return UserAccounts.FindById(username);
            }
            catch { return null; }
        }

        public UserAccount[] GetAccounts(Expression<Func<UserAccount, bool>> predicate, int page = 0, int view = int.MaxValue)
        {
            if (_disposed || UserAccounts == null)
                return new UserAccount[] { };
            try
            {
                return UserAccounts.Find(predicate, page, view).ToArray();
            }
            catch { return new UserAccount[] { }; }
        }

        public UserAccount[] GetAllAccounts()
        {
            if (_disposed || UserAccounts == null)
                return new UserAccount[] { };
            return UserAccounts.FindAll().ToArray();
        }

        public bool UpSert(UserAccount account)
        {
            return UpSert(account.Username, account, "Gebruiker Account Update");
        }

        public bool UpSert(UserAccount account, string change)
        {
            return UpSert(account.Username, account, change);
        }

        public bool UpSert(string id, UserAccount account, string change)
        {
            if (_disposed || UserAccounts == null || id == null || account == null)
                return false;
            try
            {
                account.LastChanged = new UserChange() { Change = change, PcId = OwnerId, User = Manager.Opties == null ? "Default" : Manager.Opties.Username };
                UpdateChange(account.LastChanged,3);
                UserAccounts.Upsert(id, account);
                Manager.AccountChanged(this, account);
                return true;
            }
            catch { return false; }
        }

        public int UpSert(UserAccount[] accounts, string change)
        {
            if (_disposed || UserAccounts == null || accounts == null)
                return -1;
            try
            {
                int count = 0;
                foreach (var account in accounts)
                    if (UpSert(account.Username, account, change))
                        count++;
                return count;
            }
            catch { return -1; }
        }

        public bool Delete(UserAccount account)
        {
            if (account == null)
                return false;
            return DeleteAccount(account.Username);
        }

        public int Delete(UserAccount[] accounts)
        {
            if (_disposed || UserAccounts == null || accounts == null)
                return -1;
            try
            {
                int xreturn = 0;

                foreach (var v in accounts)
                    if (UserAccounts.Delete(v.Username))
                        xreturn++;
                if(xreturn > 0)
                {
                    var changed = new UserChange() { Change = $"{xreturn} Accounts Verwijderd", PcId = OwnerId, User = Manager.Opties == null? "Default" : Manager.Opties.Username };
                    UpdateChange(changed,3);
                }
                return xreturn;
            }
            catch { return -1; }
        }

        public bool DeleteAccount(string id)
        {
            if (_disposed || UserAccounts == null || id == null)
                return false;
            try
            {
                var changed = new UserChange() { Change = $"[{id}] Account Verwijderd", PcId = OwnerId, User = Manager.Opties == null ? "Default" : Manager.Opties.Username };
                UpdateChange(changed,3);
                return UserAccounts.Delete(id);
            }
            catch { return false; }
        }

        public bool Replace(UserAccount oldaccount, UserAccount newaccount)
        {
            if (oldaccount == null)
                return false;
            return Replace(oldaccount.Username, newaccount);
        }

        public bool Replace(string id, UserAccount newaccount)
        {
            if (_disposed || UserAccounts == null || id == null || newaccount == null)
                return false;
            try
            {
                newaccount.Username = id;
                if (UserAccounts.Update(id, newaccount))
                {
                    var changed = new UserChange() { Change = $"[{id}] Account Gewijzigd", PcId = OwnerId, User = Manager.Opties == null ? "Default" : Manager.Opties.Username };
                    UpdateChange(changed,3);
                    Manager.AccountChanged(this, newaccount);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public bool Exist(UserAccount account)
        {
            if (_disposed || UserAccounts == null || account == null)
                return false;
            try
            {
                return UserAccounts.FindById(account.Username) != null;
            }
            catch { return false; }
        }

        public bool AccountExist(string id)
        {
            if (_disposed || UserAccounts == null || id == null)
                return false;
            try
            {
                return UserAccounts.FindById(id) != null;
            }
            catch { return false; }
        }

        #endregion UserAccounts

        #region Usersettings

        public UserSettings GetSetting(string username)
        {
            if (_disposed || AllSettings == null || username == null)
                return null;
            try
            {
                return AllSettings.FindById(username);
            }
            catch { return null; }
        }

        public UserSettings[] GetSettings(Expression<Func<UserSettings, bool>> predicate, int page = 0, int view = int.MaxValue)
        {
            if (_disposed || AllSettings == null)
                return new UserSettings[] { };
            try
            {
                return AllSettings.Find(predicate, page, view).ToArray();
            }
            catch { return new UserSettings[] { }; }
        }

        public UserSettings[] GetAllSettings()
        {
            if (_disposed || AllSettings == null)
                return new UserSettings[] { };
            return AllSettings.FindAll().ToArray();
        }

        public bool UpSert(UserSettings setting)
        {
            return UpSert(setting.Username, setting, "Gebruiker Optie Update");
        }

        public bool UpSert(UserSettings user, string change)
        {
            return UpSert(user.Username, user, change);
        }

        public bool UpSert(string id, UserSettings account, string change)
        {
            if (_disposed || AllSettings == null || id == null || account == null)
                return false;
            try
            {
                account.LastChanged = new UserChange() { Change = change, PcId = OwnerId, User = Manager.Opties == null ? "Default" : Manager.Opties.Username };
                AllSettings.Upsert(id, account);
                UpdateChange(account.LastChanged,4);
                return true;
            }
            catch { return false; }
        }

        public int UpSert(UserSettings[] accounts, string change)
        {
            if (_disposed || AllSettings == null || accounts == null)
                return -1;
            try
            {
                if (accounts.Length > 0)
                {
                    foreach (var account in accounts)
                        account.LastChanged = new UserChange() { Change = change, PcId = OwnerId, User = Manager.Opties == null ? "Default" : Manager.Opties.Username };
                    int count = AllSettings.Upsert(accounts);
                    if(count > 0)
                    {
                        UpdateChange(accounts[0].LastChanged,4);
                        return count;
                    }
                }
                return 0;
            }
            catch { return -1; }
        }

        public bool Delete(UserSettings account)
        {
            if (account == null)
                return false;
            return DeleteSettings(account.Username);
        }

        public int Delete(UserSettings[] settings)
        {
            if (_disposed || AllSettings == null || settings == null)
                return -1;
            try
            {
                int xreturn = 0;

                foreach (var v in settings)
                    if (AllSettings.Delete(v.Username))
                        xreturn++;
                if(xreturn > 0)
                {
                    var lastchange = new UserChange() { Change = $"{xreturn} Opties Verwijderd", PcId = OwnerId, User = Manager.Opties == null ? "Default" : Manager.Opties.Username };
                    UpdateChange(lastchange,4);
                }
                return xreturn;
            }
            catch { return -1; }
        }

        public bool DeleteSettings(string id)
        {
            if (_disposed || AllSettings == null || id == null)
                return false;
            try
            {
                if( AllSettings.Delete(id))
                {
                    var lastchange = new UserChange() { Change = $"[{id}] Optie Verwijderd", PcId = OwnerId, User = Manager.Opties == null ? "Default" : Manager.Opties.Username };
                    UpdateChange(lastchange,4);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public bool Replace(UserSettings oldsettings, UserSettings newsettings)
        {
            if (oldsettings == null)
                return false;
            return Replace(oldsettings.Username, newsettings);
        }

        public bool Replace(string id, UserSettings newsettings)
        {
            if (_disposed || AllSettings == null || id == null || newsettings == null)
                return false;
            try
            {
                var lastchange = new UserChange() { Change = $"[{id}] Optie Vervangen", PcId = OwnerId, User = Manager.Opties == null ? "Default" : Manager.Opties.Username };
                newsettings.LastChanged = lastchange;
                if (AllSettings.Update(id, newsettings))
                {
                    UpdateChange(lastchange,4);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public bool Exist(UserSettings settings)
        {
            if (_disposed || AllSettings == null || settings == null)
                return false;
            try
            {
                return AllSettings.FindById(settings.Username) != null;
            }
            catch { return false; }
        }

        public bool SettingsExist(string id)
        {
            if (_disposed || AllSettings == null || id == null)
                return false;
            try
            {
                return AllSettings.FindById(id) != null;
            }
            catch { return false; }
        }

        #endregion Usersettings

        #region Personeel

        public Personeel GetPersoneel(string username)
        {
            if (_disposed || PersoneelLijst == null || username == null)
                return null;
            try
            {
                return PersoneelLijst.FindById(username);
            }
            catch { return null; }
        }

        public Personeel[] GetPersoneel(string[] usernames)
        {
            if (_disposed || PersoneelLijst == null || usernames == null)
                return new Personeel[] { };
            try
            {
                List<Personeel> personen = new List<Personeel> { };
                foreach (var name in usernames)
                {
                    if (personen.Any(x => x.PersoneelNaam.ToLower() == name.ToLower()))
                        continue;
                    var pers = PersoneelLijst.FindById(name);
                    if (pers != null)
                        personen.Add(pers);
                }
                return personen.ToArray();
               
            }
            catch { return new Personeel[] { }; }
        }

        public Personeel[] GetPersoneel(Expression<Func<Personeel, bool>> predicate, int page = 0, int view = int.MaxValue)
        {
            if (_disposed || PersoneelLijst == null)
                return new Personeel[] { };
            try
            {
                return PersoneelLijst.Find(predicate, page, view).ToArray();
            }
            catch { return new Personeel[] { }; }
        }

        public Personeel[] GetAllPersoneel()
        {
            if (_disposed || PersoneelLijst == null)
                return new Personeel[] { };
            return PersoneelLijst.FindAll().ToArray();
        }

        public bool MaakPersoneelVrijVanWerk(string personeel)
        {
            if (personeel != null && !_disposed && PersoneelLijst != null)
            {
                var xpers = PersoneelLijst.FindById(personeel);
                if (xpers != null)
                {
                    xpers.WerktAan = null;
                    return UpSert(xpers, "Vrij gemaakt van werk");
                }
            }
            return false;
        }

        public int MaakPersoneelVrijVanWerk(Personeel[] personen)
        {
            int done = 0;
            if (personen != null && personen.Length > 0 && !_disposed && PersoneelLijst != null)
            {
                foreach (Personeel personeel in personen)
                {
                    var xpers = PersoneelLijst.FindById(personeel.PersoneelNaam);
                    if (xpers != null)
                    {
                        xpers.WerktAan = null;
                        if (UpSert(xpers, "Vrij gemaakt van werk"))
                            done++;
                    }
                }
            }
            return done;
        }

        public int MaakAllePersoneelVrijVanWerk()
        {
            int done = 0;
            if (!_disposed && PersoneelLijst != null)
            {
                var personen = PersoneelLijst.FindAll();
                foreach (Personeel personeel in personen)
                {
                    if (personeel.WerktAan != null)
                    {
                        personeel.WerktAan = null;
                        if (UpSert(personeel, "Vrij gemaakt van werk"))
                            done++;
                    }
                }
            }
            return done;
        }

        public bool UpSert(Personeel persoon)
        {
            return UpSert(persoon.PersoneelNaam, persoon, "Gebruiker Optie Update");
        }

        public bool UpSert(Personeel user, string change)
        {
            return UpSert(user.PersoneelNaam, user, change);
        }

        public bool UpSert(string id, Personeel persoon, string change)
        {
            if (_disposed || PersoneelLijst == null || id == null || persoon == null)
                return false;
            try
            {
                persoon.LastChanged = new UserChange() { Change = change, PcId = OwnerId, User = Manager.Opties == null ? "Default" : Manager.Opties.Username };
                PersoneelLijst.Upsert(id, persoon);
                UpdateChange(persoon.LastChanged,2);
                Manager.PersoneelChanged(this, persoon);
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public int UpSert(Personeel[] personen, string change)
        {
            if (_disposed || PersoneelLijst == null || personen == null)
                return -1;
            try
            {
                int count = 0;
                foreach (var account in personen)
                    if (UpSert(account, change))
                        count++;
                return count;
            }
            catch { return -1; }
        }

        public bool Delete(Personeel persoon)
        {
            if (persoon == null)
                return false;
            return DeletePersoneel(persoon.PersoneelNaam);
        }

        public int Delete(Personeel[] personeel)
        {
            if (_disposed || PersoneelLijst == null || personeel == null)
                return -1;
            try
            {
                int xreturn = 0;

                foreach (var v in personeel)
                    if (Delete(v))
                        xreturn++;
                return xreturn;
            }
            catch { return -1; }
        }

        public bool DeletePersoneel(string id)
        {
            if (_disposed || PersoneelLijst == null || id == null)
                return false;
            try
            {
                Personeel per = PersoneelLijst.FindOne(x => x.PersoneelNaam.ToLower() == id.ToLower());
                if (per != null && PersoneelLijst.Delete(id))
                {
                    var lastchange = new UserChange() { Change = $"[{id}] Personeel Verwijderd", PcId = OwnerId, User = Manager.Opties == null ? "Default" : Manager.Opties.Username };
                    UpdateChange(lastchange,2);
                    Manager.PersoneelDeleted(this, per);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public bool Replace(Personeel oldpersoon, Personeel newpersoon)
        {
            if (oldpersoon == null)
                return false;
            return Replace(oldpersoon.PersoneelNaam, newpersoon);
        }

        public bool Replace(string id, Personeel newpersoon)
        {
            if (_disposed || PersoneelLijst == null || id == null || newpersoon == null)
                return false;
            try
            {
                if (DeletePersoneel(id))
                {
                    UpSert(newpersoon);
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public bool Exist(Personeel persoon)
        {
            if (_disposed || PersoneelLijst == null || persoon == null)
                return false;
            try
            {
                return PersoneelLijst.FindById(persoon.PersoneelNaam) != null;
            }
            catch { return false; }
        }

        public bool PersoneelExist(string id)
        {
            if (_disposed || PersoneelLijst == null || id == null)
                return false;
            try
            {
                return PersoneelLijst.FindById(id) != null;
            }
            catch { return false; }
        }

        #endregion Personeel

        #region ChangeLog

        public UserChange[] GetLastDbChanges()
        {
            if(ChangeLog != null && !_disposed)
            {
                return ChangeLog.Find(x => x.PcId.ToLower() != OwnerId.ToLower() && x.TimeChanged > LastDbCheck).ToArray();
            }
            return new UserChange[] { };
        }

        public bool NeedDbCheck()
        {
            return GetLastDbChanges().Length > 0;
        }

        public bool UpdateChange(UserChange change, int dbid)
        {
            if (ChangeLog != null && !_disposed)
            {
                change.PcId = OwnerId;
                change.DbIds[dbid] = DateTime.Now;
                return ChangeLog.Upsert(OwnerId, change);
            }
            return false;
        }

        #endregion ChangeLog

        #region Disposing

        // To detect redundant calls
        private bool _disposed = false;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            StopSync();
            LiteDatabase.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            UserAccounts = null;
            ProductieFormulieren = null;
            AllSettings = null;
            PersoneelLijst = null;
            _worker.Dispose();
            if (disposing)
            {
                // Dispose managed state (managed objects).
                _safeHandle?.Dispose();
            }

            _disposed = true;
        }

        #endregion Disposing
    }
}