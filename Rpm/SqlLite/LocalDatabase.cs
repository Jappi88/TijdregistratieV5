using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Controls;
using Microsoft.Win32.SafeHandles;
using Rpm.Connection;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;

namespace Rpm.SqlLite
{
    public class LocalDatabase : IDisposable
    {
        public static readonly string DbVersion = "2.2.0.0";
        public LocalDatabase(Manager instance, string pcid, string path, bool createnew)
        {
            OwnerId = pcid;
            LastProductieUpdate = DateTime.Now;
            LastPersoneelUpdate = DateTime.Now;
            LastAccountUpdate = DateTime.Now;
            LastSettingUpdate = DateTime.Now;
            RootPath = path;
            PManager = instance;
            LoggerEnabled = instance.LoggerEnabled;
        }

        //private List<FileSystemWatcher> _fileWatchers = new();
        public bool NotificationEnabled { get; set; }
        public bool LoggerEnabled { get; set; }
        public bool RaiseEventWhenChanged { get; set; } = true;
        public bool RaiseEventWhenDeleted { get; set; } = true;

        public DateTime LastDbCheck { get; set; } = DateTime.Now;
        public DateTime LastProductieUpdate { get; set; }
        public DateTime LastPersoneelUpdate { get; set; }
        public DateTime LastAccountUpdate { get; set; }
        public DateTime LastSettingUpdate { get; set; }

        public bool IsDisposed { get; private set; }
        public Manager PManager { get; }
        public string OwnerId { get; }

        public string RootPath { get; }

        //public string SqlDatabaseFileName { get; private set; }
        //public string ChangedDbFileName { get; private set; }
        //public string PersoneelDbFileName { get; private set; }
        //public string GereedDbFileName { get; private set; }
        //public string SettingDbFileName { get; private set; }
        //public string AccountDbFileName { get; private set; }
        //public string LogDbFileName { get; private set; }
        //public string DbVersionFileName { get; private set; }
        public IDbCollection<Personeel> PersoneelLijst { get; private set; }
        public IDbCollection<ProductieFormulier> ProductieFormulieren { get; private set; }
        public IDbCollection<UserAccount> UserAccounts { get; private set; }
        public IDbCollection<UserSettings> AllSettings { get; private set; }
        public IDbCollection<LogEntry> Logger { get; private set; }
        //public IDbCollection<UserChange> ChangeLog { get; private set; }
        public IDbCollection<ProductieFormulier> GereedFormulieren { get; private set; }
        public IDbCollection<DbVersion> DbVersions { get; private set; }
        //public IDbCollection<BewerkingEntry> BewerkingEntries { get; private set; }
        #region BackgroundWorker

        private void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
                try
                {
                    if (e.UserState is Personeel[])
                    {
                        var acc = e.UserState as Personeel[];
                        if (acc != null && acc.Length > 0)
                        {
                            LastPersoneelUpdate = DateTime.Now;
                            foreach (var pers in acc)
                                if (e.ProgressPercentage < 1)
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
                                if (e.ProgressPercentage == 0)
                                    Manager.FormulierChanged(this, prod);
                                //else Manager.FormulierDeleted(this, prod);
                            //  Manager.LoadProducties();
                        }
                    }
                    else if (e.UserState is UserAccount[])
                    {
                        var acc = e.UserState as UserAccount[];
                        if (acc != null && acc.Length > 0)
                        {
                            LastAccountUpdate = DateTime.Now;
                            foreach (var account in acc) Manager.AccountChanged(this, account);
                        }
                    }
                    else if (e.UserState is UserSettings)
                    {
                        LastSettingUpdate = DateTime.Now;
                        Manager.UserSettingChanged(this, e.UserState as UserSettings);
                    }
                    else if (e.UserState is UserChange change)
                    {
                        if (change.Change != null)
                        {
                            if (change.IsRemoved)
                                foreach (var v in change.DbIds)
                                    switch (v.Key)
                                    {
                                        case DbType.Producties:
                                            Manager.ProductiesChanged();
                                            break;
                                        case DbType.GereedProducties:
                                            Manager.ProductiesChanged();
                                            break;
                                        case DbType.Opties:
                                            break;
                                        case DbType.Accounts:
                                            break;
                                        case DbType.Medewerkers:
                                            break;
                                        case DbType.None:
                                            break;
                                        default:
                                            throw new ArgumentOutOfRangeException();
                                    }

                            Manager.RemoteMessage(change.CreateMessage(change.DbIds.FirstOrDefault().Key));
                        }
                    }
                }
                catch
                {
                }
        }

        #endregion BackgroundWorker

        #region ProductieFormulieren

        public Task<ProductieFormulier> GetProductie(string criteria, bool Fullmatch)
        {
            return Task.Run(async () =>
            {
                ProductieFormulier form = null;
                try
                {
                    if (IsDisposed || ProductieFormulieren == null)
                        return null;

                    form = (await GetAllProducties(criteria, Fullmatch, false,true)).FirstOrDefault();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return form;
            });
        }

        public Task<ProductieFormulier> GetProductie(string productienr)
        {
            return Task.Run(async () =>
            {
                try
                {
                    ProductieFormulier prod = null;
                    if (ProductieFormulieren != null)
                        prod = await ProductieFormulieren.FindOne(productienr);
                    if (prod == null && GereedFormulieren != null)
                        prod = await GereedFormulieren.FindOne(productienr);
                    return prod;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            });

        }

        public Task<List<ProductieFormulier>> GetProducties(string criteria,bool fullmatch, ProductieState state,
            bool tijdgewerkt)
        {
            return Task.Run(async () =>
            {
                bool isgereed = state == ProductieState.Gereed;
                var xreturn = await GetAllProducties(criteria, fullmatch, isgereed, isgereed);
                xreturn = xreturn.Where(x => x.State == state && (!tijdgewerkt || (x.TijdGewerkt > 0 && x.ActueelPerUur > 0))).ToList();
                return xreturn;
            });
        }

        public Task<List<ProductieFormulier>> GetProducties(string criteria, bool fullmatch,
            bool tijdgewerkt)
        {
            return Task.Run(async () =>
            {
                var xreturn = await GetAllProducties(criteria, fullmatch, false, true);
                if (tijdgewerkt)
                    xreturn = xreturn.Where(x => x.TijdGewerkt > 0).ToList();
                return xreturn;
            });
        }

        public Task<List<ProductieFormulier>> GetAllProducties(bool incgereed, bool filter)
        {
            return GetAllProducties(incgereed, filter, null);
        }

        public Task<List<ProductieFormulier>> GetAllProducties(bool incgereed, bool filter, IsValidHandler validhandler)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed)
                    return new List<ProductieFormulier>();
                var prods = new List<ProductieFormulier>();
                if (filter && validhandler == null)
                    validhandler = Functions.IsAllowed;
                if (ProductieFormulieren != null) prods = await ProductieFormulieren.FindAll(validhandler);

                if (incgereed && GereedFormulieren != null)
                {
                    var xprods = await GereedFormulieren.FindAll(validhandler);
                    if (xprods.Count > 0)
                        prods.AddRange(xprods);
                }

                return prods;//filter ? prods.Where(x => x.IsAllowed(null)).ToList() : prods;
            });
        }

        public Task<List<Bewerking>> GetAllBewerkingen(bool incgereed, bool filter)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed)
                    return new List<Bewerking>();
                var prods = new List<ProductieFormulier>();
                if (ProductieFormulieren != null) prods = await ProductieFormulieren.FindAll();

                if (incgereed && GereedFormulieren != null)
                {
                    var xprods = await GereedFormulieren.FindAll();
                    if (xprods.Count > 0)
                        prods.AddRange(xprods);
                }

                var bws = new List<Bewerking>();
                foreach (var pr in prods)
                {
                    if (pr?.Bewerkingen == null || pr.Bewerkingen.Length == 0)
                        continue;
                    foreach (var bw in pr.Bewerkingen)
                        if (bw != null)
                        {
                            if (filter && !bw.IsAllowed())
                                continue;
                            bws.Add(bw);
                        }
                }

                return bws;
            });
        }

        public Task<List<ProductieFormulier>> GetAllProducties(bool incgereed, bool filter, TijdEntry bereik, IsValidHandler validhandler)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed)
                    return new List<ProductieFormulier>();
                var prods = new List<ProductieFormulier>();
                if (ProductieFormulieren != null)
                    if (bereik != null)
                        prods = await ProductieFormulieren.FindAll(bereik.Start, bereik.Stop, validhandler);
                else prods = await ProductieFormulieren.FindAll(validhandler);

                if (incgereed && GereedFormulieren != null)
                {
                    var xprods = new List<ProductieFormulier>();
                    if (bereik != null)
                        xprods = await GereedFormulieren.FindAll(bereik.Start, bereik.Stop, validhandler);
                    else xprods = await GereedFormulieren.FindAll(validhandler);
                    if (xprods.Count > 0)
                        prods.AddRange(xprods);
                }

                return filter ? prods.Where(x => x.IsAllowed(null)).ToList() : prods;
            });
        }

        public Task<List<Bewerking>> GetBewerkingen(ViewState state, bool filter, TijdEntry bereik,
            IsValidHandler validhandler)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed)
                    return new List<Bewerking>();
                var prods = new List<ProductieFormulier>();
                if (ProductieFormulieren != null)
                    if (bereik != null)
                        prods = await ProductieFormulieren.FindAll(bereik.Start, bereik.Stop, validhandler);
                    else prods = await ProductieFormulieren.FindAll(validhandler);

                if ((state == ViewState.Alles || state == ViewState.Gereed) && GereedFormulieren != null)
                {
                    var xprods = new List<ProductieFormulier>();
                    if (bereik != null)
                        xprods = await GereedFormulieren.FindAll(bereik.Start, bereik.Stop, validhandler);
                    else xprods = await GereedFormulieren.FindAll(validhandler);
                    if (xprods.Count > 0)
                        prods.AddRange(xprods);
                }

                var bws = new List<Bewerking>();
                foreach (var pr in prods)
                {
                    if (pr?.Bewerkingen == null || pr.Bewerkingen.Length == 0)
                        continue;
                    foreach (var bw in pr.Bewerkingen)
                        if (bw != null)
                        {
                            if (filter && !bw.IsAllowed(null,state))
                                continue;
                            if (validhandler != null && !validhandler.Invoke(bw, null)) continue;
                            bws.Add(bw);
                        }
                }

                return bws;
            });
        }

        public Task<List<ProductieFormulier>> GetAllProducties(string criteria, bool fullmatch, bool alleengereed, bool incgereed)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed)
                    return new List<ProductieFormulier>();
                var prods = new List<ProductieFormulier>();
                if (!alleengereed && ProductieFormulieren != null) prods = await ProductieFormulieren.FindAll(criteria, fullmatch);

                if (incgereed && GereedFormulieren != null)
                {
                    var xprods = await GereedFormulieren.FindAll(criteria, fullmatch);
                    if (xprods.Count > 0)
                        prods.AddRange(xprods);
                }

                return prods;
            });
        }

        public Task<List<ProductieFormulier>> GetAllGereedProducties(IsValidHandler validhandler)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || GereedFormulieren == null)
                    return new List<ProductieFormulier>();
                return await GereedFormulieren.FindAll(validhandler);
            });
        }

        public Task<List<ProductieFormulier>> GetAllGereedProducties()
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || GereedFormulieren == null)
                    return new List<ProductieFormulier>();
                return await GereedFormulieren.FindAll();
            });
        }

        public Task<bool> UpSert(ProductieFormulier form, string change, bool showmessage = true)
        {
            return UpSert(form.ProductieNr, form, change, showmessage);
        }

        public Task<bool> UpSert(ProductieFormulier form, bool showmessage = true)
        {
            return UpSert(form.ProductieNr, form, $"[{form.ArtikelNr}|{form.ProductieNr}] ProductieFormulier Update",
                showmessage);
        }

        public Task<bool> UpSert(string id, ProductieFormulier form, string change, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || id == null || form == null)
                    return false;
                bool xreturn = false;
                try
                {
                    form.ExcludeFromUpdate();
                    form.LastChanged = form.LastChanged.UpdateChange(change, DbType.Producties);
                    if (ProductieFormulieren != null && form.Bewerkingen.All(x => x.State == ProductieState.Gereed))
                    {
                        GereedFormulieren.RaiseEventWhenChanged = !RaiseEventWhenChanged;
                        if (await GereedFormulieren.Upsert(id, form))
                        {
                            await UpdateChange(form.LastChanged, form, RespondType.Update, DbType.GereedProducties,
                                showmessage);
                            if (RaiseEventWhenChanged)
                                Manager.FormulierChanged(this, form);

                            if (await ProductieFormulieren.Exists(id))
                            {
                                ProductieFormulieren.RaiseEventWhenDeleted = !RaiseEventWhenDeleted;
                                await ProductieFormulieren.Delete(id);
                                if (RaiseEventWhenDeleted)
                                    Manager.FormulierDeleted(this, id);
                                ProductieFormulieren.RaiseEventWhenDeleted = true;
                            }

                            xreturn = true;
                        }

                        GereedFormulieren.RaiseEventWhenChanged = true;
                    }
                    else if (ProductieFormulieren != null)
                    {
                        ProductieFormulieren.RaiseEventWhenChanged = !RaiseEventWhenChanged;
                        if (await ProductieFormulieren.Upsert(id, form))
                        {
                            await UpdateChange(form.LastChanged, form, RespondType.Update, DbType.Producties,
                                showmessage);
                            if (RaiseEventWhenChanged)
                                Manager.FormulierChanged(this, form);

                            if (GereedFormulieren != null && await GereedFormulieren.Exists(id))
                            {
                                GereedFormulieren.RaiseEventWhenDeleted = !RaiseEventWhenDeleted;
                                await GereedFormulieren.Delete(id);
                                if (RaiseEventWhenDeleted)
                                    Manager.FormulierDeleted(this, id);
                                GereedFormulieren.RaiseEventWhenDeleted = true;
                            }

                            xreturn = true;
                        }

                        
                        ProductieFormulieren.RaiseEventWhenChanged = true;
                    }

                    // Manager.FormulierChanged(this, form);
                }
                catch (Exception)
                {
                    xreturn = false;
                }
                form.RemoveExcludeFromUpdate();
                return xreturn;
            });
        }

        public Task<int> UpSert(ProductieFormulier[] forms, string change, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || ProductieFormulieren == null || forms == null)
                    return -1;
                try
                {
                    var done = 0;
                    foreach (var prod in forms)
                        if (await UpSert(prod, change, showmessage))
                            done++;
                    return done;
                }
                catch
                {
                    return -1;
                }
            });
        }

        public Task<bool> DeleteProductie(string id, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || ProductieFormulieren == null || id == null)
                    return false;
                try
                {
                    var change = $"[{id}] Productie Verwijderd";
                    var form = await ProductieFormulieren.FindOne(id);
                    if (form == null) return false;
                    form.ExcludeFromUpdate();
                    var changed = new UserChange().UpdateChange(change, DbType.Producties);
                    changed.IsRemoved = true;
                    await UpdateChange(changed, form, RespondType.Delete, DbType.Producties, showmessage);
                    bool deleted = true;
                    if (!await ProductieFormulieren.Delete(id))
                        deleted = await GereedFormulieren.Delete(form.ProductieNr);
                    
                    if (deleted && RaiseEventWhenDeleted)
                        Manager.FormulierDeleted(this, id);
                    form.RemoveExcludeFromUpdate();
                    return deleted;
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<bool> Delete(ProductieFormulier form, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || ProductieFormulieren == null || form == null)
                    return false;
                bool xreturn = false;
                try
                {
                    form.ExcludeFromUpdate();
                    var change = $"[{form.ProductieNr}] Productie Verwijderd";
                    if (await ProductieFormulieren.Delete(form.ProductieNr))
                    {
                        form.LastChanged = form.LastChanged.UpdateChange(change, DbType.Producties);
                        form.LastChanged.IsRemoved = true;
                        await UpdateChange(form.LastChanged, form, RespondType.Delete, DbType.Producties, showmessage);
                        if (RaiseEventWhenDeleted)
                            Manager.FormulierDeleted(this, form.ProductieNr);
                        xreturn = true;
                    }

                    if (await GereedFormulieren.Delete(form.ProductieNr))
                    {
                        form.LastChanged = form.LastChanged.UpdateChange(change, DbType.GereedProducties);
                        form.LastChanged.IsRemoved = true;
                        await UpdateChange(form.LastChanged, form, RespondType.Delete, DbType.GereedProducties,
                            showmessage);
                        if (RaiseEventWhenDeleted)
                            Manager.FormulierDeleted(this, form.ProductieNr);
                        xreturn = true;
                    }
                }
                catch
                {
                }
                form.RemoveExcludeFromUpdate();
                return xreturn;
            });
        }

        public Task<int> Delete(ProductieFormulier[] forms, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || ProductieFormulieren == null || forms == null)
                    return -1;
                try
                {
                    var xreturn = 0;
                    foreach (var prod in forms)
                    {
                        if (await Delete(prod))
                            xreturn++;

                    }

                    return xreturn;
                }
                catch
                {
                    return -1;
                }
            });
        }

        public Task<bool> Replace(ProductieFormulier oldform, ProductieFormulier newform, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (oldform == null)
                    return false;
                return await Replace(oldform.ProductieNr, newform, showmessage);
            });
        }

        public Task<bool> Replace(string id, ProductieFormulier newform, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || ProductieFormulieren == null || id == null || newform == null)
                    return false;
                try
                {
                    await DeleteProductie(id, showmessage);
                    await UpSert(newform, showmessage);
                    return false;
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<bool> Exist(ProductieFormulier form)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || form == null)
                    return false;
                try
                {
                    return await ProductieExist(form.ProductieNr);
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<bool> ProductieExist(string id)
        {
            return Task.Run(() =>
            {
                if (IsDisposed || id == null)
                    return false;
                try
                {
                    return (ProductieFormulieren != null &&
                           ProductieFormulieren.Exists(id).Result ) || (GereedFormulieren != null && GereedFormulieren.Exists(id).Result);
                }
                catch
                {
                    return false;
                }

            });
        }

        #endregion ProductieFormulieren

        #region UserAccounts

        public Task<UserAccount> GetAccount(string username)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || username == null)
                    return null;
                try
                {
                    return await UserAccounts?.FindOne(username);
                }
                catch
                {
                    return null;
                }
            });
        }

        public Task<List<UserAccount>> GetAllAccounts()
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || UserAccounts == null)
                    return new List<UserAccount>();
                return await UserAccounts.FindAll();
            });
        }

        public Task<bool> UpSert(UserAccount account, bool showmessage = true)
        {
            return UpSert(account.Username, account, "Gebruiker Account Update", showmessage);
        }

        public Task<bool> UpSert(UserAccount account, string change, bool showmessage = true)
        {
            return UpSert(account.Username, account, change, showmessage);
        }

        public Task<bool> UpSert(string id, UserAccount account, string change, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || UserAccounts == null || id == null || account == null)
                    return false;
                try
                {
                    account.LastChanged = account.LastChanged.UpdateChange(change, DbType.Accounts);
                    await UpdateChange(account.LastChanged, account, RespondType.Update, DbType.Accounts, showmessage);
                    await UserAccounts.Upsert(id, account);
                    Manager.AccountChanged(this, account);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<int> UpSert(UserAccount[] accounts, string change, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || UserAccounts == null || accounts == null)
                    return -1;
                try
                {
                    var count = 0;
                    foreach (var account in accounts)
                        if (await UpSert(account.Username, account, change, showmessage))
                            count++;
                    return count;
                }
                catch
                {
                    return -1;
                }
            });
        }

        public Task<bool> Delete(UserAccount account, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (account == null)
                    return false;
                return await DeleteAccount(account.Username, showmessage);
            });
        }

        public Task<int> Delete(UserAccount[] accounts, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || UserAccounts == null || accounts == null)
                    return -1;
                try
                {
                    var xreturn = 0;

                    foreach (var v in accounts)
                        if (await UserAccounts.Delete(v.Username))
                            xreturn++;
                    if (xreturn > 0)
                    {
                        var changed = new UserChange
                        {
                            Change = $"{xreturn} Accounts Verwijderd",
                            IsRemoved = true,
                            PcId = OwnerId,
                            User = Manager.Opties == null ? "Default" : Manager.Opties.Username
                        };
                        await UpdateChange(changed, accounts, RespondType.Delete, DbType.Accounts, showmessage);
                    }

                    return xreturn;
                }
                catch
                {
                    return -1;
                }
            });
        }

        public Task<bool> DeleteAccount(string id, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || UserAccounts == null || id == null)
                    return false;
                try
                {
                    var pers = await UserAccounts.FindOne(id);
                    if (pers != null && await UserAccounts.Delete(id))
                    {
                        var changed = new UserChange
                        {
                            Change = $"[{id}] Account Verwijderd",
                            IsRemoved = true,
                            PcId = OwnerId,
                            User = Manager.Opties == null ? "Default" : Manager.Opties.Username
                        };
                        await UpdateChange(changed, pers, RespondType.Delete, DbType.Accounts, showmessage);
                        return true;
                    }

                    return false;
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<bool> Replace(UserAccount oldaccount, UserAccount newaccount, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (oldaccount == null)
                    return false;
                return await Replace(oldaccount.Username, newaccount, showmessage);
            });
        }

        public Task<bool> Replace(string id, UserAccount newaccount, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || UserAccounts == null || id == null || newaccount == null)
                    return false;
                try
                {
                    await DeleteAccount(id);
                    await UpSert(newaccount);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<bool> Exist(UserAccount account)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || UserAccounts == null || account == null)
                    return false;
                try
                {
                    return await UserAccounts.Exists(account.Username);
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<bool> AccountExist(string id)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || UserAccounts == null || id == null)
                    return false;
                try
                {
                    return await UserAccounts.Exists(id);
                }
                catch
                {
                    return false;
                }
            });
        }

        #endregion UserAccounts

        #region Usersettings

        public Task<UserSettings> GetSetting(string username)
        {
            if (IsDisposed || AllSettings == null || username == null)
                return null;
            try
            {
                return AllSettings.FindOne(username);
            }
            catch
            {
                return null;
            }
        }

        public Task<List<UserSettings>> GetAllSettings()
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || AllSettings == null)
                    return new List<UserSettings>();
                return await AllSettings.FindAll();
            });
        }

        public Task<bool> UpSert(UserSettings setting, bool showmessage = true)
        {
            string id = setting.Username != null ? setting.Username.ToLower().StartsWith("default") ? $"Default[{setting.SystemID}]" : setting.Username : null;
            return UpSert(id, setting, "Gebruiker Optie Update", showmessage);
        }

        public Task<bool> UpSert(UserSettings user, string change, bool showmessage = true)
        {
            string id = user.Username != null ? user.Username.ToLower().StartsWith("default") ? $"Default[{user.SystemID}]" : user.Username : null;
            return UpSert(id, user, change, showmessage);
        }

        public Task<bool> UpSert(string id, UserSettings account, string change, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || AllSettings == null || id == null || account == null)
                    return false;
                try
                {
                    account.LastChanged = account.LastChanged.UpdateChange(change, DbType.Opties);
                    await UpdateChange(account.LastChanged, account, RespondType.Update, DbType.Opties, showmessage);
                    await AllSettings.Upsert(id, account);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<int> UpSert(UserSettings[] accounts, string change, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || AllSettings == null || accounts == null)
                    return -1;
                try
                {
                    if (accounts.Length > 0)
                    {
                        var count = 0;
                        foreach (var account in accounts)
                            if (await UpSert(account, change, showmessage))
                                count++;

                        return count;
                    }

                    return 0;
                }
                catch
                {
                    return -1;
                }
            });
        }

        public Task<bool> Delete(UserSettings account, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (account == null)
                    return false;
                string id = account.Username != null ? account.Username.ToLower().StartsWith("default") ? $"Default[{account.SystemID}]" : account.Username : null;
                return await DeleteSettings(id, showmessage);
            });
        }

        public Task<int> Delete(UserSettings[] settings, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || AllSettings == null || settings == null)
                    return -1;
                try
                {
                    var xreturn = 0;

                    foreach (var v in settings)
                        if (await Delete(v,showmessage))
                            xreturn++;
                    return xreturn;
                }
                catch
                {
                    return -1;
                }
            });
        }

        public Task<bool> DeleteSettings(string id, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || AllSettings == null || id == null)
                    return false;
                try
                {
                    var xset = AllSettings.FindOne(id);
                    if (xset != null && await AllSettings.Delete(id))
                    {
                        var lastchange = new UserChange().UpdateChange($"[{id}] Optie Verwijderd", DbType.Opties);
                        lastchange.IsRemoved = true;
                        await UpdateChange(lastchange, xset, RespondType.Delete, DbType.Opties, showmessage);
                        return true;
                    }

                    return false;
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<bool> Replace(UserSettings oldsettings, UserSettings newsettings, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (oldsettings == null)
                    return false;
                string id = oldsettings.Username != null
                    ? oldsettings.Username.ToLower().StartsWith("default") ? $"Default[{oldsettings.SystemID}]" :
                    oldsettings.Username
                    : null;
                return await Replace(id, newsettings, showmessage);
            });
        }

        public Task<bool> Replace(string id, UserSettings newsettings, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || AllSettings == null || id == null || newsettings == null)
                    return false;
                try
                {
                    if (await DeleteSettings(id, showmessage))
                        return await UpSert(newsettings, showmessage);
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public Task<bool> Exist(UserSettings settings)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || AllSettings == null || settings == null)
                    return false;
                try
                {
                    string id = settings.Username != null
                        ? settings.Username.ToLower().StartsWith("default") ? $"Default[{settings.SystemID}]" :
                        settings.Username
                        : null;
                    return await AllSettings.Exists(id);
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<bool> SettingsExist(string id)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || AllSettings == null || id == null)
                    return false;
                try
                {
                    return await AllSettings.Exists(id);
                }
                catch
                {
                    return false;
                }
            });
        }

        #endregion Usersettings

        #region Personeel

        public Task<Personeel> GetPersoneel(string username)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || PersoneelLijst == null || username == null)
                    return null;
                try
                {
                    return await PersoneelLijst.FindOne(username);
                }
                catch
                {
                    return null;
                }
            });
        }

        public Task<List<Personeel>> GetPersoneel(string[] usernames)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || PersoneelLijst == null || usernames == null)
                    return new List<Personeel>();
                try
                {
                    var personen = new List<Personeel>();
                    foreach (var name in usernames)
                    {
                        if (personen.Any(x => x.PersoneelNaam.ToLower() == name.ToLower()))
                            continue;
                        var pers = await PersoneelLijst.FindOne(name);
                        if (pers != null)
                            personen.Add(pers);
                    }

                    return personen;
                }
                catch
                {
                    return new List<Personeel>();
                }
            });
        }

        public Task<List<Personeel>> GetAllPersoneel()
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || PersoneelLijst == null)
                    return new List<Personeel>();
                return await PersoneelLijst.FindAll();
            });
        }

        public Task<bool> MaakPersoneelVrijVanWerk(string personeel, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (personeel != null && !IsDisposed && PersoneelLijst != null)
                {
                    var xpers = await PersoneelLijst.FindOne(personeel);
                    if (xpers != null)
                    {
                        xpers.WerktAan = null;
                        return await UpSert(xpers, "Vrij gemaakt van werk", showmessage);
                    }
                }

                return false;
            });
        }

        public Task<int> MaakPersoneelVrijVanWerk(Personeel[] personen, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                var done = 0;
                if (personen != null && personen.Length > 0 && !IsDisposed && PersoneelLijst != null)
                    foreach (var personeel in personen)
                    {
                        var xpers = await PersoneelLijst.FindOne(personeel.PersoneelNaam);
                        if (xpers != null)
                        {
                            xpers.WerktAan = null;
                            if (await UpSert(xpers, "Vrij gemaakt van werk", showmessage))
                                done++;
                        }
                    }

                return done;
            });
        }

        public Task<int> MaakAllePersoneelVrijVanWerk(bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                var done = 0;
                if (!IsDisposed && PersoneelLijst != null)
                {
                    var personen = await PersoneelLijst.FindAll();
                    foreach (var personeel in personen)
                        if (personeel.WerktAan != null)
                        {
                            personeel.WerktAan = null;
                            if (await UpSert(personeel, "Vrij gemaakt van werk", showmessage))
                                done++;
                        }
                }

                return done;
            });
        }

        //public Task<bool> UpSert(Personeel persoon, bool showmessage = true)
        //{
        //    return UpSert(persoon.PersoneelNaam, persoon, "Gebruiker Optie Update", showmessage);
        //}

        public Task<bool> UpSert(Personeel user, string change, bool showmessage = true)
        {
            return UpSert(user.PersoneelNaam, user, change, showmessage);
        }

        public Task<bool> UpSert(string id, Personeel persoon, string change, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || PersoneelLijst == null || id == null || persoon == null)
                    return false;
                try
                {
                    persoon.LastChanged = persoon.LastChanged.UpdateChange(change, DbType.Medewerkers);
                    await UpdateChange(persoon.LastChanged, persoon, RespondType.Update, DbType.Medewerkers,
                        showmessage);
                    PersoneelLijst.RaiseEventWhenChanged = !RaiseEventWhenChanged;
                    await PersoneelLijst.Upsert(id, persoon);
                    if (RaiseEventWhenChanged)
                        Manager.PersoneelChanged(this, persoon);
                    PersoneelLijst.RaiseEventWhenChanged = true;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public Task<int> UpSert(Personeel[] personen, string change, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || PersoneelLijst == null || personen == null)
                    return -1;
                try
                {
                    var count = 0;
                    foreach (var account in personen)
                        if (await UpSert(account, change, showmessage))
                            count++;
                    return count;
                }
                catch
                {
                    return -1;
                }
            });
        }

        public Task<bool> Delete(Personeel persoon, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (persoon == null)
                    return false;
                return await DeletePersoneel(persoon.PersoneelNaam, showmessage);
            });
        }

        public Task<int> Delete(Personeel[] personeel, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || PersoneelLijst == null || personeel == null)
                    return -1;
                try
                {
                    var xreturn = 0;

                    foreach (var v in personeel)
                        if (await Delete(v, showmessage))
                            xreturn++;
                    return xreturn;
                }
                catch
                {
                    return -1;
                }
            });
        }

        public Task<bool> DeletePersoneel(string id, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || PersoneelLijst == null || id == null)
                    return false;
                bool xreturn = false;
                try
                {
                   
                    var per = await PersoneelLijst.FindOne(id);
                    PersoneelLijst.RaiseEventWhenDeleted = !RaiseEventWhenDeleted;
                    if (per != null && await PersoneelLijst.Delete(id))
                    {
                        var lastchange = new UserChange
                        {
                            Change = $"[{id}] Personeel Verwijderd",
                            IsRemoved = true,
                            PcId = OwnerId,
                            User = Manager.Opties == null ? "Default" : Manager.Opties.Username
                        };
                        await UpdateChange(lastchange, per, RespondType.Delete, DbType.Medewerkers, showmessage);
                        if (RaiseEventWhenDeleted)
                            Manager.PersoneelDeleted(this, id);
                        xreturn = true;
                    }
                }
                catch
                {
                    xreturn = false;
                }
                PersoneelLijst.RaiseEventWhenDeleted = true;
                return xreturn;
            });
        }

        public Task<bool> Replace(Personeel oldpersoon, Personeel newpersoon, string change = null, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (oldpersoon == null)
                    return false;
                return await Replace(oldpersoon.PersoneelNaam, newpersoon,change, showmessage);
            });
        }

        public Task<bool> Replace(string id, Personeel newpersoon, string change = null,bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || PersoneelLijst == null || id == null || newpersoon == null)
                    return false;
                try
                {
                    await DeletePersoneel(id, showmessage);
                    await UpSert(newpersoon, change?? $"{id} is verplaatst met {newpersoon.PersoneelNaam}", showmessage);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<bool> Exist(Personeel persoon)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || PersoneelLijst == null || persoon == null)
                    return false;
                try
                {
                    return await PersoneelLijst.Exists(persoon.PersoneelNaam);
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<bool> PersoneelExist(string id)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || PersoneelLijst == null || id == null)
                    return false;
                try
                {
                    return await PersoneelLijst.Exists(id);
                }
                catch
                {
                    return false;
                }
            });
        }

        #endregion Personeel

        #region ChangeLog

        //public Task<List<UserChange>> GetLastDbChanges()
        //{
        //    return Task.Run(async () =>
        //    {
        //        if (ChangeLog != null && !IsDisposed)
        //            return (await ChangeLog.FindAll())
        //                .Where(x => x.PcId.ToLower() != OwnerId.ToLower() && x.TimeChanged > LastDbCheck).ToList();
        //        return new List<UserChange>();
        //    });
        //}

        //public Task<bool> UpdateChange(RespondMessage msg)
        //{
        //    return Task.Run(async () =>
        //    {
        //        try
        //        {
        //            if (Manager.Database.IsDisposed)
        //                return false;
        //            //var changes = await Manager.Database.ChangeLog.FindOne(msg.Sender.MachinId) ?? new UserChange();
        //            //changes.Change = msg.Message;
        //            //changes.TimeChanged = DateTime.Now;
        //            //changes.DbIds[msg.DBName] = DateTime.Now;
        //            //changes.PcId = msg.Sender.MachinId;
        //            //changes.User = msg.Sender.Name;
        //            if (LoggerEnabled) await Manager.Database.ChangeLog.Upsert(msg.Sender.MachinId, changes);
        //            if (NotificationEnabled) Manager.RemoteMessage(changes.CreateMessage(msg.DBName));
        //            return true;
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //    });
        //}

        public Task UpdateChange(UserChange change, object value, RespondType type, DbType dbname,
            bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                #region Update Server
                //update server
                //if (Manager.Server != null && Manager.Server.IsSyncing && updatelocal && ChangeLog != null &&
                //    !IsDisposed)
                //    try
                //    {
                //        var old = await ChangeLog.FindOne(OwnerId);
                //        if (old != null)
                //            change.DbIds = old.DbIds;
                //        if (value != null)
                //        {
                //            var container = new ValueContainer(value);
                //            switch (dbname)
                //            {
                //                case DbType.Accounts:
                //                    if (container.Accounts != null && container.Accounts.Count > 0)
                //                        foreach (var acc in container.Accounts)
                //                            switch (type)
                //                            {
                //                                case RespondType.Add:
                //                                case RespondType.Update:
                //                                    if (await Manager.Server.Upsert(acc, acc.Username,
                //                                        DbType.Accounts) && acc.LastChanged != null)
                //                                        acc.LastChanged.ServerUpdated = DateTime.Now;
                //                                    break;
                //                                case RespondType.Delete:
                //                                    await Manager.Server.Delete(acc.Username, DbType.Accounts);
                //                                    break;
                //                            }

                //                    break;
                //                case DbType.GereedProducties:
                //                case DbType.Producties:
                //                    if (container.Producties != null && container.Producties.Count > 0)
                //                        foreach (var prod in container.Producties)
                //                            switch (type)
                //                            {
                //                                case RespondType.Add:
                //                                case RespondType.Update:
                //                                    if (await Manager.Server.Upsert(prod, prod.ProductieNr,
                //                                        DbType.Producties) && prod.LastChanged != null)
                //                                        prod.LastChanged.ServerUpdated = DateTime.Now;

                //                                    break;
                //                                case RespondType.Delete:
                //                                    await Manager.Server.Delete(prod.ProductieNr, DbType.Producties);
                //                                    break;
                //                            }

                //                    break;
                //                case DbType.Medewerkers:
                //                    if (container.Personen != null && container.Personen.Count > 0)
                //                        foreach (var pers in container.Personen)
                //                            switch (type)
                //                            {
                //                                case RespondType.Add:
                //                                case RespondType.Update:
                //                                    if (await Manager.Server.Upsert(pers, pers.PersoneelNaam,
                //                                        DbType.Medewerkers) && pers.LastChanged != null)
                //                                        pers.LastChanged.ServerUpdated = DateTime.Now;

                //                                    break;
                //                                case RespondType.Delete:
                //                                    await Manager.Server.Delete(pers.PersoneelNaam, DbType.Medewerkers);
                //                                    break;
                //                            }

                //                    break;
                //                case DbType.Opties:
                //                    if (container.Settings != null && container.Settings.Count > 0)
                //                        foreach (var opties in container.Settings)
                //                            switch (type)
                //                            {
                //                                case RespondType.Add:
                //                                case RespondType.Update:
                //                                    if (await Manager.Server.Upsert(opties, opties.Username,
                //                                        DbType.Opties) && opties.LastChanged != null)
                //                                        opties.LastChanged.ServerUpdated = DateTime.Now;
                //                                    break;
                //                                case RespondType.Delete:
                //                                    await Manager.Server.Delete(opties.Username, DbType.Opties);
                //                                    break;
                //                            }

                //                    break;
                //                case DbType.Messages:
                //                    break;
                //            }
                //        }
                //    }
                //    catch (Exception)
                //    {
                //    }
                #endregion

                try
                {
                    //change.PcId = OwnerId;
                    //change.DbIds[dbname] = DateTime.Now;
                    //if (ChangeLog != null) await ChangeLog.Upsert(OwnerId, change);
                   
                    if (NotificationEnabled && showmessage) Manager.RemoteMessage(change.CreateMessage(dbname));
                    if (LoggerEnabled && showmessage) await AddLog(change.Change, MsgType.Info);
                    // PManager.RemoteMessage(new Mailing.RemoteMessage(change.Change, MessageAction.None, MsgType.Info));
                }
                catch
                {
                }
            });
        }


        public Task<bool> AddLog(string message, MsgType type)
        {
            return Task.Run(async () =>
            {
                if (Logger != null && !IsDisposed)
                    try
                    {
                        var ent = new LogEntry(message.Replace("\n", " "), type);
                        return await Logger.Upsert(ent.Id.ToString(), ent);
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                return false;
            });
        }

        public Task<List<LogEntry>> GetLogs(DateTime from, DateTime to, IsValidHandler validhandler = null)
        {
            return Task.Run(async () =>
            {
                var logs = new List<LogEntry>();
                if (Logger != null && !IsDisposed)
                    try
                    {
                        var found = await Logger.FindAll(from, to,validhandler);
                        if (found.Any())
                            logs.AddRange(found);
                        logs.Sort((x, y) => DateTime.Compare(x.Added, y.Added));
                    }
                    catch (Exception)
                    {
                        return logs;
                    }

                return logs;
            });
        }

        #endregion ChangeLog

        #region Database

        public Task LoadMultiFiles(bool migrate)
        {
            return Task.Run(async () =>
            {
                var dbfilename = "SqlDatabase";
               // var changeddb = "ChangeDb";
                var personeeldb = "PersoneelDb";
                var Gereeddb = "GereedDb";
                var Settingdb = "SettingDb";
                var Accountdb = "AccountsDb";
                var Logdb = "LogDb";
                var versiondb = "VersionDb";
                //var bewerkingentriesdb = "BewerkingLijst";
                ProductieFormulieren = new DatabaseInstance<ProductieFormulier>(DbInstanceType.MultipleFiles, RootPath,
                    dbfilename, "ProductieFormulieren");

                ProductieFormulieren.InstanceChanged += ProductieFormulieren_InstanceChanged;
                ProductieFormulieren.InstanceDeleted += ProductieFormulieren_InstanceDeleted;

                PersoneelLijst =
                    new DatabaseInstance<Personeel>(DbInstanceType.MultipleFiles, RootPath, personeeldb, "Personeel");
                PersoneelLijst.InstanceChanged += PersoneelLijst_InstanceChanged;
                PersoneelLijst.InstanceDeleted += PersoneelLijst_InstanceDeleted;

                UserAccounts = new DatabaseInstance<UserAccount>(DbInstanceType.MultipleFiles, RootPath, Accountdb,
                    "UserAccounts");
                UserAccounts.InstanceChanged += UserAccounts_InstanceChanged;
                UserAccounts.InstanceDeleted += UserAccounts_InstanceDeleted;

                AllSettings = new DatabaseInstance<UserSettings>(DbInstanceType.MultipleFiles, RootPath, Settingdb,
                    "AllSettings");
                AllSettings.InstanceChanged += AllSettings_InstanceChanged;
                AllSettings.InstanceDeleted += AllSettings_InstanceDeleted;

                Logger = new DatabaseInstance<LogEntry>(DbInstanceType.MultipleFiles, RootPath, Logdb, "Logs");
               // ChangeLog = new DatabaseInstance<UserChange>(DbInstanceType.MultipleFiles, RootPath, changeddb,
                   // "ChangeLog");
                GereedFormulieren = new DatabaseInstance<ProductieFormulier>(DbInstanceType.MultipleFiles, RootPath,
                    Gereeddb, "GereedFormulieren");
                GereedFormulieren.InstanceChanged += ProductieFormulieren_InstanceChanged;
                GereedFormulieren.InstanceDeleted += ProductieFormulieren_InstanceDeleted;

                DbVersions = new DatabaseInstance<DbVersion>(DbInstanceType.MultipleFiles, RootPath, versiondb, "DbVersions");
                //BewerkingEntries = new DatabaseInstance<BewerkingEntry>(DbInstanceType.MultipleFiles, RootPath,
                //    bewerkingentriesdb, "BewerkingEntries");
                if (!migrate) return;
                var dbpath = RootPath + "\\" + dbfilename + ".db";
                if (File.Exists(dbpath) && !File.Exists(dbpath.Replace(".db", "_migrated.db")))
                {
                    var prodsdb = new DatabaseInstance<ProductieFormulier>(DbInstanceType.LiteDb, RootPath, dbfilename,
                        "ProductieFormulieren");
                    var prods = await prodsdb.FindAll();
                    foreach (var prod in prods)
                        await ProductieFormulieren.Upsert(prod.ProductieNr, prod);
                    File.Move(dbpath, dbpath.Replace(".db", "_migrated.db"));
                }

                //dbpath = RootPath + "\\" + changeddb + ".db";
                //if (File.Exists(dbpath) && !File.Exists(dbpath.Replace(".db", "_migrated.db")))
                //{
                //    var prodsdb =
                //        new DatabaseInstance<UserChange>(DbInstanceType.LiteDb, RootPath, changeddb, "ChangeLog");
                //    var prods = await prodsdb.FindAll();
                //    foreach (var prod in prods)
                //        await ChangeLog.Upsert(prod.User, prod);
                //    File.Move(dbpath, dbpath.Replace(".db", "_migrated.db"));
                //}

                dbpath = RootPath + "\\" + personeeldb + ".db";
                if (File.Exists(dbpath) && !File.Exists(dbpath.Replace(".db", "_migrated.db")))
                {
                    var prodsdb =
                        new DatabaseInstance<Personeel>(DbInstanceType.LiteDb, RootPath, personeeldb, "Personeel");
                    var prods = await prodsdb.FindAll();
                    foreach (var prod in prods)
                        await PersoneelLijst.Upsert(prod.PersoneelNaam, prod);
                    File.Move(dbpath, dbpath.Replace(".db", "_migrated.db"));
                }

                dbpath = RootPath + "\\" + Gereeddb + ".db";
                if (File.Exists(dbpath) && !File.Exists(dbpath.Replace(".db", "_migrated.db")))
                {
                    var prodsdb = new DatabaseInstance<ProductieFormulier>(DbInstanceType.LiteDb, RootPath, Gereeddb,
                        "GereedFormulieren");
                    var prods = await prodsdb.FindAll();
                    foreach (var prod in prods)
                        await GereedFormulieren.Upsert(prod.ProductieNr, prod);
                    File.Move(dbpath, dbpath.Replace(".db", "_migrated.db"));
                }

                dbpath = RootPath + "\\" + Settingdb + ".db";
                if (File.Exists(dbpath) && !File.Exists(dbpath.Replace(".db", "_migrated.db")))
                {
                    var prodsdb =
                        new DatabaseInstance<UserSettings>(DbInstanceType.LiteDb, RootPath, Settingdb, "AllSettings");
                    var prods = await prodsdb.FindAll();
                    foreach (var prod in prods)
                        await AllSettings.Upsert(prod.Username, prod);
                    File.Move(dbpath, dbpath.Replace(".db", "_migrated.db"));
                }

                dbpath = RootPath + "\\" + Accountdb + ".db";
                if (File.Exists(dbpath) && !File.Exists(dbpath.Replace(".db", "_migrated.db")))
                {
                    var prodsdb =
                        new DatabaseInstance<UserAccount>(DbInstanceType.LiteDb, RootPath, Accountdb, "UserAccounts");
                    var prods = await prodsdb.FindAll();
                    foreach (var prod in prods)
                        await UserAccounts.Upsert(prod.Username, prod);
                    File.Move(dbpath, dbpath.Replace(".db", "_migrated.db"));
                }

                dbpath = RootPath + "\\" + Logdb + ".db";
                if (File.Exists(dbpath) && !File.Exists(dbpath.Replace(".db", "_migrated.db")))
                {
                    var prodsdb = new DatabaseInstance<LogEntry>(DbInstanceType.LiteDb, RootPath, Logdb, "Logs");
                    var prods = await prodsdb.FindAll();
                    foreach (var prod in prods)
                        await Logger.Upsert(prod.Id.ToString(), prod);
                    File.Move(dbpath, dbpath.Replace(".db", "_migrated.db"));
                }

                dbpath = RootPath + "\\" + versiondb + ".db";
                if (File.Exists(dbpath) && !File.Exists(dbpath.Replace(".db", "_migrated.db")))
                {
                    var prodsdb =
                        new DatabaseInstance<DbVersion>(DbInstanceType.LiteDb, RootPath, versiondb, "DbVersions");
                    var prods = await prodsdb.FindAll();
                    foreach (var prod in prods)
                        await DbVersions.Upsert(prod.Name, prod);
                    File.Move(dbpath, dbpath.Replace(".db", "_migrated.db"));
                }

                //LoadSyncDirectories();
                //dbpath = RootPath + "\\" + bewerkingentriesdb + ".db";
                //if (File.Exists(dbpath) && !File.Exists(dbpath.Replace(".db", "_migrated.db")))
                //{
                //    var prodsdb =
                //        new DatabaseInstance<BewerkingEntry>(DbInstanceType.LiteDb, RootPath, bewerkingentriesdb, "BewerkingEntries");
                //    var prods = await prodsdb.FindAll();
                //    foreach (var prod in prods)
                //        await BewerkingEntries.Upsert(prod.Naam, prod);
                //    File.Move(dbpath, dbpath.Replace(".db", "_migrated.db"));
                //}
            });
        }

        #region Database Events
        private void AllSettings_InstanceDeleted(object sender, FileSystemEventArgs y)
        {
            
        }

        private void AllSettings_InstanceChanged(object sender, FileSystemEventArgs y)
        {
            lock (_locker1)
            {
                try
                {
                    if (AllSettings == null || !RaiseEventWhenChanged) return;
                    var id = Path.GetFileNameWithoutExtension(y.FullPath);
                    var xset = AllSettings.FindOne(id).Result;
                    if(xset != null && Manager.Opties != null && string.Equals(Manager.Opties.Username, xset.Username, StringComparison.CurrentCultureIgnoreCase))
                    {
                        Manager.Opties = xset;
                        Manager.SettingsChanged(this,true);
                    }
                }
                catch (Exception x)
                {
                    Console.WriteLine(x);
                }
            }
        }

        private void UserAccounts_InstanceDeleted(object sender, FileSystemEventArgs y)
        {
            
        }

        private void UserAccounts_InstanceChanged(object sender, FileSystemEventArgs y)
        {
            
        }

        private void PersoneelLijst_InstanceDeleted(object sender, FileSystemEventArgs y)
        {
            lock (_locker2)
            {
                try
                {
                    if (!RaiseEventWhenDeleted || File.Exists(y.FullPath)) return;
                    var id = Path.GetFileNameWithoutExtension(y.FullPath);
                    Manager.PersoneelDeleted(this, id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private void PersoneelLijst_InstanceChanged(object sender, FileSystemEventArgs y)
        {
            lock (_locker2)
            {
                try
                {
                    if (!RaiseEventWhenChanged || PersoneelLijst == null) return;
                    var id = Path.GetFileNameWithoutExtension(y.FullPath);
                    var pers = PersoneelLijst.FindOne(id).Result;
                    if (pers != null)
                        Manager.PersoneelChanged(this, pers);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private void ProductieFormulieren_InstanceDeleted(object sender, FileSystemEventArgs y)
        {
            lock (_locker1)
            {
                try
                {
                    if (!RaiseEventWhenDeleted || File.Exists(y.FullPath)) return;
                    var id = Path.GetFileNameWithoutExtension(y.FullPath);
                    Manager.FormulierDeleted(this, id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private void ProductieFormulieren_InstanceChanged(object sender, FileSystemEventArgs e)
        {
            lock (_locker1)
            {
                try
                {
                    if (ProductieFormulieren == null || !RaiseEventWhenChanged) return;
                    var id = Path.GetFileNameWithoutExtension(e.FullPath);
                    var prod = ProductieFormulieren.FindOne(id).Result;
                    if (prod != null && prod.IsAllowed(null))
                        Manager.FormulierChanged(this, prod);
                }
                catch (Exception x)
                {
                    Console.WriteLine(x);
                }
            }
        }
        #endregion

        private static readonly object _locker1 = new object();
        private static readonly object _locker2 = new object();
        private static readonly object _locker3 = new object();

        //private void LoadSyncDirectories()
        //{
        //    try
        //    {
        //        if (_fileWatchers == null)
        //            _fileWatchers = new List<FileSystemWatcher>();
        //        var dbfilename = RootPath + "\\SqlDatabase";
        //        var personeeldb = RootPath + "\\PersoneelDb";
        //        var Gereeddb = RootPath + "\\GereedDb";

        //        foreach (var fs in _fileWatchers)
        //        {
        //            fs.Dispose();
        //            _fileWatchers.Remove(fs);
        //        }

        //        //load productieformulieren watcher.
        //        if (Directory.Exists(dbfilename))
        //        {
        //            var sw = new FileSystemWatcher(dbfilename) { EnableRaisingEvents = true };
        //            sw.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
        //            sw.Changed += (x, y) =>
        //            {
        //                lock (_locker1)
        //                {
        //                    try
        //                    {
        //                        if (ProductieFormulieren == null) return;
        //                        var id = Path.GetFileNameWithoutExtension(y.FullPath);
        //                        var prod = ProductieFormulieren.FindOne(id).Result;
        //                        if (prod != null && prod.IsAllowed(null))
        //                            Manager.FormulierChanged(this, prod);
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        Console.WriteLine(e);
        //                    }
        //                }
        //            };
        //            sw.Deleted += (x, y) =>
        //            {
        //                lock (_locker1)
        //                {
        //                    try
        //                    {
        //                        if (File.Exists(y.FullPath)) return;
        //                        var id = Path.GetFileNameWithoutExtension(y.FullPath);
        //                        Manager.FormulierDeleted(this, id);
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        Console.WriteLine(e);
        //                    }
        //                }
        //            };
        //            //sw.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.CreationTime;
        //            _fileWatchers.Add(sw);
        //        }
        //        //load personeel watcher.
        //        if (Directory.Exists(personeeldb))
        //        {
        //            var sw = new FileSystemWatcher(personeeldb) { EnableRaisingEvents = true };
        //            sw.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
        //            sw.Changed += (x, y) =>
        //            {
        //                lock (_locker2)
        //                {
        //                    try
        //                    {
        //                        if (PersoneelLijst == null) return;
        //                        var id = Path.GetFileNameWithoutExtension(y.FullPath);
        //                        var pers = PersoneelLijst.FindOne(id).Result;
        //                        if (pers != null)
        //                            Manager.PersoneelChanged(this, pers);
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        Console.WriteLine(e);
        //                    }
        //                }
        //            };

        //            sw.Deleted += (x, y) =>
        //            {
        //                lock (_locker2)
        //                {
        //                    try
        //                    {
        //                        if (File.Exists(y.FullPath)) return;
        //                        var id = Path.GetFileNameWithoutExtension(y.FullPath);
        //                        Manager.PersoneelDeleted(this, id);
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        Console.WriteLine(e);
        //                    }
        //                }
        //            };
        //            //sw.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.CreationTime;
        //            _fileWatchers.Add(sw);
        //        }
        //        //load gereed formulieren watcher.
        //        if (Directory.Exists(Gereeddb))
        //        {
        //            var sw = new FileSystemWatcher(Gereeddb) { EnableRaisingEvents = true };
        //            sw.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
        //            sw.Changed += (x, y) =>
        //            {
        //                lock (_locker3)
        //                {
        //                    try
        //                    {
        //                        if (GereedFormulieren == null) return;
        //                        var id = Path.GetFileNameWithoutExtension(y.FullPath);
        //                        var pers = GereedFormulieren.FindOne(id).Result;
        //                        if (pers != null && pers.IsAllowed(null))
        //                            Manager.FormulierChanged(this, pers);
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        Console.WriteLine(e);
        //                    }
        //                }
        //            };
        //            sw.Deleted += (x, y) =>
        //            {
        //                lock (_locker3)
        //                {
        //                    try
        //                    {
        //                        if (File.Exists(y.FullPath)) return;
        //                        var id = Path.GetFileNameWithoutExtension(y.FullPath);
        //                        Manager.FormulierDeleted(this, id);
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        Console.WriteLine(e);
        //                    }
        //                }
        //            };

        //            //sw.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.CreationTime;
        //            _fileWatchers.Add(sw);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}

        public Task LoadServerFiles(bool migrate)
        {
            return Task.Run(async () =>
            {
                var dbfilename = "SqlDatabase";
               // var changeddb = "ChangeDb";
                var personeeldb = "PersoneelDb";
                var Gereeddb = "GereedDb";
                var Settingdb = "SettingDb";
                var Accountdb = "AccountsDb";
                var Logdb = "LogDb";
                var versiondb = "VersionDb";

                ProductieFormulieren = new DatabaseInstance<ProductieFormulier>(DbInstanceType.Server, RootPath,
                    dbfilename, "ProductieFormulieren");
                PersoneelLijst =
                    new DatabaseInstance<Personeel>(DbInstanceType.Server, RootPath, personeeldb, "Personeel");
                UserAccounts =
                    new DatabaseInstance<UserAccount>(DbInstanceType.Server, RootPath, Accountdb, "UserAccounts");
                AllSettings =
                    new DatabaseInstance<UserSettings>(DbInstanceType.Server, RootPath, Settingdb, "AllSettings");
                Logger = new DatabaseInstance<LogEntry>(DbInstanceType.MultipleFiles, RootPath, Logdb, "Logs");
                //ChangeLog = new DatabaseInstance<UserChange>(DbInstanceType.Server, RootPath, changeddb, "ChangeLog");
                GereedFormulieren = new DatabaseInstance<ProductieFormulier>(DbInstanceType.Server, RootPath, Gereeddb,
                    "GereedFormulieren");
                DbVersions = new DatabaseInstance<DbVersion>(DbInstanceType.Server, RootPath, versiondb, "DbVersions");
                if (!migrate) return;
                var dbpath = RootPath + "\\" + dbfilename;
                if (Directory.Exists(dbpath) && !Directory.Exists(dbpath + "_migrated"))
                {
                    var prodsdb = new DatabaseInstance<ProductieFormulier>(DbInstanceType.MultipleFiles, RootPath,
                        dbfilename, "ProductieFormulieren");
                    var prods = await prodsdb.FindAll();
                    foreach (var prod in prods)
                        await ProductieFormulieren.Upsert(prod.ProductieNr, prod);
                    Directory.Move(dbpath, dbpath + "_migrated");
                }

                //dbpath = RootPath + "\\" + changeddb;
                //if (Directory.Exists(dbpath) && !Directory.Exists(dbpath + "_migrated"))
                //{
                //    var prodsdb = new DatabaseInstance<UserChange>(DbInstanceType.MultipleFiles, RootPath, changeddb,
                //        "ChangeLog");
                //    var prods = await prodsdb.FindAll();
                //    foreach (var prod in prods)
                //        await ChangeLog.Upsert(prod.User, prod);
                //    Directory.Move(dbpath, dbpath + "_migrated");
                //}

                dbpath = RootPath + "\\" + personeeldb;
                if (Directory.Exists(dbpath) && !Directory.Exists(dbpath + "_migrated"))
                {
                    var prodsdb = new DatabaseInstance<Personeel>(DbInstanceType.MultipleFiles, RootPath, personeeldb,
                        "Personeel");
                    var prods = await prodsdb.FindAll();
                    foreach (var prod in prods)
                        await PersoneelLijst.Upsert(prod.PersoneelNaam, prod);
                    Directory.Move(dbpath, dbpath + "_migrated");
                }

                dbpath = RootPath + "\\" + Gereeddb;
                if (Directory.Exists(dbpath) && !Directory.Exists(dbpath + "_migrated"))
                {
                    var prodsdb = new DatabaseInstance<ProductieFormulier>(DbInstanceType.MultipleFiles, RootPath,
                        Gereeddb, "GereedFormulieren");
                    var prods = await prodsdb.FindAll();
                    foreach (var prod in prods)
                        await GereedFormulieren.Upsert(prod.ProductieNr, prod);
                    Directory.Move(dbpath, dbpath + "_migrated");
                }

                dbpath = RootPath + "\\" + Settingdb;
                if (Directory.Exists(dbpath) && !Directory.Exists(dbpath + "_migrated"))
                {
                    var prodsdb = new DatabaseInstance<UserSettings>(DbInstanceType.MultipleFiles, RootPath, Settingdb,
                        "AllSettings");
                    var prods = await prodsdb.FindAll();
                    foreach (var prod in prods)
                        await AllSettings.Upsert(prod.Username, prod);
                    Directory.Move(dbpath, dbpath + "_migrated");
                }

                dbpath = RootPath + "\\" + Accountdb;
                if (Directory.Exists(dbpath) && !Directory.Exists(dbpath + "_migrated"))
                {
                    var prodsdb = new DatabaseInstance<UserAccount>(DbInstanceType.MultipleFiles, RootPath, Accountdb,
                        "UserAccounts");
                    var prods = await prodsdb.FindAll();
                    foreach (var prod in prods)
                        await UserAccounts.Upsert(prod.Username, prod);
                    Directory.Move(dbpath, dbpath + "_migrated");
                }

                dbpath = RootPath + "\\" + versiondb;
                if (Directory.Exists(dbpath) && !Directory.Exists(dbpath + "_migrated"))
                {
                    var prodsdb = new DatabaseInstance<DbVersion>(DbInstanceType.MultipleFiles, RootPath, versiondb,
                        "DbVersions");
                    var prods = await prodsdb.FindAll();
                    foreach (var prod in prods)
                        await DbVersions.Upsert(prod.Name, prod);
                    Directory.Move(dbpath, dbpath + "_migrated");
                }
            });
        }

        //private void InitDbs(bool createnew)
        //{
        //    if (File.Exists(SqlDatabaseFileName) || createnew)
        //        ProductiesDb = new LiteDatabase(new ConnectionString(SqlDatabaseFileName)
        //        { Connection = ConnectionType.Shared });
        //    if (File.Exists(ChangedDbFileName) || createnew)
        //        ChangedDb = new LiteDatabase(new ConnectionString(ChangedDbFileName)
        //        { Connection = ConnectionType.Shared });

        //    if (File.Exists(PersoneelDbFileName) || createnew)
        //        PersoneelDb = new LiteDatabase(new ConnectionString(PersoneelDbFileName)
        //            {Connection = ConnectionType.Shared});

        //    if (File.Exists(GereedDbFileName) || createnew)
        //        GereedDb = new LiteDatabase(new ConnectionString(GereedDbFileName)
        //            {Connection = ConnectionType.Shared });

        //    if (File.Exists(SettingDbFileName) || createnew)
        //        SettingsDb = new LiteDatabase(new ConnectionString(SettingDbFileName)
        //            {Connection = ConnectionType.Shared });

        //    if (File.Exists(AccountDbFileName) || createnew)
        //        AccountsDb = new LiteDatabase(new ConnectionString(AccountDbFileName)
        //            {Connection = ConnectionType.Shared });

        //    if (File.Exists(LogDbFileName) || createnew)
        //        LogDb = new LiteDatabase(new ConnectionString(LogDbFileName)
        //            {Connection = ConnectionType.Shared });

        //    //if (File.Exists(DbVersionFileName) || createnew)
        //        VersionsDb = new LiteDatabase(new ConnectionString(DbVersionFileName)
        //        { Connection = ConnectionType.Shared });

        //    PersoneelLijst = PersoneelDb?.GetCollection<Personeel>("Personeel");
        //    // PersoneelLijst.EnsureIndex("personeelnaam");

        //    ProductieFormulieren = ProductiesDb?.GetCollection<ProductieFormulier>("ProductieFormulieren");
        //    //ProductieFormulieren.EnsureIndex("productienr");
        //    //ProductieFormulieren.DeleteAll();

        //    UserAccounts = AccountsDb?.GetCollection<UserAccount>("UserAccounts");
        //    // UserAccounts.EnsureIndex("Username");

        //    Logger = LogDb?.GetCollection<LogEntry>("Logs");

        //    AllSettings = SettingsDb?.GetCollection<UserSettings>("AllSettings");

        //    //AllSettings.EnsureIndex("Username");
        //    //AllSettings.DeleteAll();
        //    ChangeLog = ChangedDb?.GetCollection<UserChange>("ChangeLog");
        //    //ChangeLog.EnsureIndex("pcid");

        //    GereedFormulieren = GereedDb?.GetCollection<ProductieFormulier>("GereedFormulieren");

        //    DbVersions = VersionsDb?.GetCollection<DbVersion>("DbVersions");
        //}

        public Task<int> Count(DbType type)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed)
                    return 0;
                switch (type)
                {
                    case DbType.Producties:
                        if (ProductieFormulieren == null)
                            return 0;
                        return await ProductieFormulieren.Count();
                    case DbType.Changes:
                        return 0;
                    case DbType.Medewerkers:
                        if (PersoneelLijst == null)
                            return 0;
                        return await PersoneelLijst.Count();
                    case DbType.GereedProducties:
                        if (GereedFormulieren == null)
                            return 0;
                        return await GereedFormulieren.Count();
                    case DbType.Opties:
                        if (AllSettings == null)
                            return 0;
                        return await AllSettings.Count();
                    case DbType.Accounts:
                        if (UserAccounts == null)
                            return 0;
                        return await UserAccounts.Count();
                    case DbType.Logs:
                        if (Logger == null)
                            return 0;
                        return await Logger.Count();
                    case DbType.Versions:
                        if (DbVersions == null)
                            return 0;
                        return await DbVersions.Count();
                    case DbType.Messages:
                        return 0;
                    case DbType.Alles:
                        var count = 0;
                        var types = (DbType[])Enum.GetValues(typeof(DbType));
                        foreach (var t in types)
                        {
                            if (t == DbType.Alles || t == DbType.None)
                                continue;
                            count += await Count(t);
                        }

                        return count;
                    case DbType.None:
                        break;
                }

                return 0;
            });
        }

        public async void CheckVersions(DbType type)
        {
            if (IsDisposed || DbVersions == null)
                return;
            double cur = 1;
            var version = await DbVersions.FindOne(Enum.GetName(typeof(DbType), type)) ?? new DbVersion
            { Version = cur, Name = Enum.GetName(typeof(DbType), type) };
            switch (type)
            {
                case DbType.Producties:
                    cur = 2; // huidige productie db versie
                    if (version.Version < cur && cur > 1)
                        try
                        {
                            var prods = await ProductieFormulieren.FindAll();
                            foreach (var prod in prods)
                            {
                                if (prod.State == ProductieState.Verwijderd) continue;
                                await prod.UpdateVersion();
                            }

                            version.Version = cur;
                            await DbVersions.Upsert(version.Name, version);
                        }
                        catch
                        {
                        }

                    break;
                case DbType.Changes:
                    break;
                case DbType.Medewerkers:
                    break;
                case DbType.GereedProducties:
                    break;
                case DbType.Opties:
                    break;
                case DbType.Accounts:
                    break;
                case DbType.Logs:
                    break;
                case DbType.Versions:
                    break;
            }
        }

        public void Close(DbType type)
        {
            switch (type)
            {
                case DbType.Producties:
                    ProductieFormulieren = null;
                    break;
                case DbType.Changes:
                    //ChangeLog = null;
                    break;
                case DbType.Medewerkers:
                    PersoneelLijst = null;
                    break;
                case DbType.GereedProducties:
                    GereedFormulieren = null;
                    break;
                case DbType.Opties:
                    AllSettings = null;
                    break;
                case DbType.Accounts:
                    UserAccounts = null;
                    break;
                case DbType.Logs:
                    Logger = null;
                    break;
                case DbType.Versions:
                    DbVersions = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }


        private Task<bool> Imigrate()
        {
            return Task.Run(async () =>
            {
                if (PersoneelLijst != null)
                {
                    var personen = await PersoneelLijst?.FindAll();
                    if (personen == null)
                        return false;
                    foreach (var v in personen) await PersoneelLijst?.Upsert(v.PersoneelNaam, v);
                }

                if (Logger != null)
                    await Logger?.DeleteAll();
                return true;
            });
        }

        public Task UpdateUserActivity(bool gestart)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var users = await GetAllPersoneel();
                    foreach (var user in users)
                    {
                        bool userchanged = false;
                        if (user.PersoneelNaam.EndsWith(" "))
                        {
                            user.PersoneelNaam = user.PersoneelNaam.Trim();
                            userchanged = true;
                        }
                        List<Klus> toremove = new List<Klus>();
                        
                        var klusjes = gestart
                            ? user.Klusjes.Where(x => x.Status == ProductieState.Gestart).ToList()
                            : user.Klusjes.Where(x=> x.IsActief).ToList(); //.Where(x => x.Status == ProductieState.Gestart).ToArray();
                        
                        if (klusjes.Count > 0)
                        {

                            foreach (var klus in klusjes)
                            {
                                var pair = klus.GetWerk();
                                var prod = pair?.Formulier;
                                var bew = pair?.Bewerking;
                                if (prod == null || bew == null)
                                {
                                    user.Klusjes.Remove(klus);
                                    userchanged = true;
                                    continue;
                                }

                                var saved = false;
                                var plek =
                                    bew.WerkPlekken.FirstOrDefault(x =>
                                        string.Equals(x.Naam, klus.WerkPlek,
                                            StringComparison.CurrentCultureIgnoreCase));
                                if (plek == null)
                                {
                                    toremove.Add(klus);
                                    continue;
                                }

                                var xolduser = plek.Personen.FirstOrDefault(x =>
                                    string.Equals(x.PersoneelNaam, user.PersoneelNaam,
                                        StringComparison.CurrentCultureIgnoreCase));
                                var msg = "";
                                if (bew.State != klus.Status)
                                {
                                    switch (bew.State)
                                    {
                                        case ProductieState.Gestopt:
                                            klus.Stop();
                                            saved = true;
                                            break;
                                        case ProductieState.Gestart:
                                            if (klus.IsActief)
                                            {
                                                klus.Start();
                                                saved = true;
                                            }
                                            break;
                                        case ProductieState.Gereed:
                                            klus.MeldGereed();
                                            saved = true;
                                            break;
                                        case ProductieState.Verwijderd:
                                            klus.Stop();
                                            klus.Status = ProductieState.Verwijderd;
                                            saved = true;
                                            break;
                                    }

                                    msg =
                                        $"{klus.Path} van {user.PersoneelNaam}  is verandert naar {Enum.GetName(typeof(ProductieState), klus.Status)}.";
                                   
                                }

                                if (klus.Tijden.IsActief && klus.Status != ProductieState.Gestart)
                                {
                                    klus.Stop();
                                    saved = true;
                                }

                                if (saved)
                                {
                                    userchanged = true;
                                    if (xolduser != null && xolduser.ReplaceKlus(klus))
                                        await bew.UpdateBewerking(null, $"{klus.Naam} klus geupdate");
                                }
                            }
                        }
                        if (userchanged || toremove.Count > 0)
                        {
                            foreach (var k in toremove)
                                user.Klusjes.Remove(k);
                            await UpSert(user, $"{user.PersoneelNaam} update");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }

        public Task<int> UpdateDbEntries(ValueContainer value, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                var done = 0;
                if (value.Settings.Count > 0)
                    done += await UpSert(value.Settings.ToArray(), "Update from local server", showmessage);
                if (value.Producties.Count > 0)
                    done += await UpSert(value.Producties.ToArray(), "Update from local server", showmessage);
                if (value.Accounts.Count > 0)
                    done += await UpSert(value.Accounts.ToArray(), "Update from local server", showmessage);
                if (value.Personen.Count > 0)
                    done += await UpSert(value.Personen.ToArray(), "Update from local server", showmessage);
                return done;
            });
        }

        //public Task<bool> NeedDbCheck()
        //{
        //    return Task.Run(async () => (await GetLastDbChanges()).Count > 0);
        //}

        public async Task<int> UpdateDbFromDb(DatabaseUpdateEntry dbentry, CancellationTokenSource token,
            ProgressChangedHandler changed = null)
        {
            return await Task.Run(async () =>
            {
                var updated = 0;
                var oldnotif = NotificationEnabled;
                var oldlogger = LoggerEnabled;
                try
                {
                    var dbpath = dbentry.UpdatePath;
                    var types = dbentry.UpdateDatabases;
                    if (string.IsNullOrEmpty(dbpath) || !Directory.Exists(dbpath)) return 0;

                    DoProgress(changed, "Database gegevens laden...", 0, 0);
                    token.Token.ThrowIfCancellationRequested();
                    var database = new LocalDatabase(PManager, Manager.SystemID, dbpath, false);
                    database.NotificationEnabled = false;
                    database.LoggerEnabled = false;

                    NotificationEnabled = false;
                    LoggerEnabled = false;
                    RaiseEventWhenChanged = false;
                    RaiseEventWhenDeleted = false;
                    await database.LoadMultiFiles(false);
                    //foreach (var dbtype in Enum.GetValues(typeof(DbType)))
                    //    database.CheckVersions((DbType) dbtype);

                    token.Token.ThrowIfCancellationRequested();
                    //update settings
                    var count = 0;
                    var max = 0;
                    if (types.Any(x => x == DbType.Opties))
                    {
                        DoProgress(changed, "Instellingen laden...", count, max);
                        try
                        {
                            var xs = await database.AllSettings.FindAll(dbentry.LastUpdated, DateTime.MaxValue,null);
                            if (xs != null && xs.Count > 0)
                            {
                                max = xs.Count;
                                count = 0;
                                foreach (var s in xs)
                                {
                                    token.Token.ThrowIfCancellationRequested();
                                    var myitem = await GetSetting($"{s.Username}[{s.SystemID}]");
                                    if (myitem == null || myitem.LastChanged != null &&
                                        (myitem.LastChanged == null && s.LastChanged != null ||
                                         s.LastChanged != null && myitem.LastChanged.TimeChanged <
                                         s.LastChanged.TimeChanged))
                                    {
                                        await UpSert(s,
                                            $"{s.Username} Instellingen geupdate vanuit {dbentry.Naam} database.");
                                        updated++;
                                    }

                                    count++;
                                    DoProgress(changed, "Instellingen updaten...", count, max);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            DoProgress(changed, e.Message, 0, 0);
                        }
                    }

                    if (types.Any(x => x == DbType.Accounts))
                    {
                        //update Accounts
                        DoProgress(changed, "Accounts laden...", count, max);
                        token.Token.ThrowIfCancellationRequested();
                        try
                        {
                            var xs1 = await database.UserAccounts.FindAll(dbentry.LastUpdated, DateTime.MaxValue,null);
                            if (xs1 != null && xs1.Count > 0)
                            {
                                //count = 0;
                                max += xs1.Count;
                                foreach (var s in xs1)
                                {
                                    token.Token.ThrowIfCancellationRequested();
                                    var myitem = await GetAccount(s.Username);
                                    if (myitem == null || myitem.LastChanged != null &&
                                        (myitem.LastChanged == null && s.LastChanged != null ||
                                         s.LastChanged != null && myitem.LastChanged.TimeChanged <
                                         s.LastChanged.TimeChanged))
                                    {
                                        await UpSert(s,
                                            $"{s.Username} Account geupdate vanuit {dbentry.Naam} database.");
                                        updated++;
                                    }

                                    count++;
                                    DoProgress(changed, "Accounts updaten...", count, max);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            DoProgress(changed, e.Message, 0, 0);
                        }
                    }

                    if (types.Any(x => x == DbType.Medewerkers))
                    {
                        //update users
                        DoProgress(changed, "Personeel laden...", count, max);
                        token.Token.ThrowIfCancellationRequested();
                        try
                        {
                            var xs2 = await database.PersoneelLijst.FindAll(dbentry.LastUpdated, DateTime.MaxValue,null);
                            if (xs2 != null && xs2.Count > 0)
                            {
                                // count = 0;
                                max += xs2.Count;
                                foreach (var s in xs2)
                                {
                                    token.Token.ThrowIfCancellationRequested();
                                    var myitem = await GetPersoneel(s.PersoneelNaam);
                                    if (myitem != null)
                                    {
                                        updated += myitem.UpdateFrom(s, true);
                                    }
                                    else
                                    {
                                        await UpSert(s,
                                            $"{s.PersoneelNaam} Instellingen geupdate vanuit {dbentry.Naam} database.");
                                        updated++;
                                    }

                                    //if (myitem == null || myitem.LastChanged != null &&
                                    //    (myitem.LastChanged == null && s.LastChanged != null ||
                                    //     s.LastChanged != null && myitem.LastChanged.TimeChanged <
                                    //     s.LastChanged.TimeChanged))
                                    //{
                                    //    UpSert(s);
                                    //    count++;
                                    //}
                                    count++;
                                    DoProgress(changed, "Personeel updaten...", count, max);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            DoProgress(changed, e.Message, 0, 0);
                        }
                    }

                    if (types.Any(x => x == DbType.Producties))
                    {
                        //update producties
                        DoProgress(changed, "Producties laden...", count, max);
                        token.Token.ThrowIfCancellationRequested();
                        try
                        {
                           
                            var xs3 = await database.ProductieFormulieren?.FindAll(dbentry.LastUpdated,
                                DateTime.MaxValue,null);
                            if (xs3 != null && xs3.Count > 0)
                            {
                                max += xs3.Count;
                                foreach (var s in xs3)
                                {
                                    token.Token.ThrowIfCancellationRequested();
                                    var myitem = await Manager.Database.GetProductie(s.ProductieNr);
                                    if (myitem != null)
                                    {
                                        if (await myitem.UpdateFrom(s,
                                            $"[{myitem.ProductieNr}, {myitem.ArtikelNr}] geupdate vanuit {dbentry.Naam} database.")
                                        ) updated++;
                                    }
                                    //else if(s.IsAllowed(null))
                                    //{
                                    //        await UpSert(s,
                                    //            $"[{s.ProductieNr}, {s.ArtikelNr}] toegevoegd vanuit {dbentry.Naam} database.");
                                    //        updated++;
                                    //}

                                    count++;
                                    DoProgress(changed, "Producties updaten...", count, max);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            DoProgress(changed, e.Message, 0, 0);
                        }

                        //update producties
                        DoProgress(changed, "Gereed Producties laden...", count, max);
                        token.Token.ThrowIfCancellationRequested();
                        try
                        {
                            var xs3 = await database.GereedFormulieren?.FindAll(dbentry.LastUpdated,
                                DateTime.MaxValue,null);
                            if (xs3 != null && xs3.Count > 0)
                            {
                                max += xs3.Count;
                                foreach (var s in xs3)
                                {
                                    token.Token.ThrowIfCancellationRequested();
                                    var myitem = await Manager.Database.GetProductie(s.ProductieNr);
                                    if (myitem != null)
                                    {
                                        if (await myitem.UpdateFrom(s,
                                            $"[{myitem.ProductieNr}, {myitem.ArtikelNr}] geupdate vanuit {dbentry.Naam} database.")
                                        ) updated++;
                                    }
                                    //else if(s.IsAllowed(null))
                                    //{
                                    //    await UpSert(s,
                                    //        $"[{s.ProductieNr}, {s.ArtikelNr}] toegevoegd vanuit {dbentry.Naam} database.");
                                    //    updated++;
                                    //}

                                    count++;
                                    DoProgress(changed, "Gereed producties updaten...", count, max);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            DoProgress(changed, e.Message, 0, 0);
                        }
                    }
                    

                    if (updated > 0)
                        DoProgress(changed, $"{updated} Entries zijn succesvol geupdate!", 100, 100);
                    else
                        DoProgress(changed, "Geen updates gevonden...", 0, 0);
                }
                catch (Exception)
                {
                }
                RaiseEventWhenChanged = true;
                RaiseEventWhenDeleted = true;
                NotificationEnabled = oldnotif;
                LoggerEnabled = oldlogger;
                return updated;
            }, token.Token);
        }

        private void DoProgress(ProgressChangedHandler changed, string msg, double current, double max)
        {
            var value = max > 0 ? current / max * 100 : 0;
            if (value > 100)
                value = 100;
            var progress = new ProgressArg
            {
                Message = msg,
                Type = ProgressType.ReadBussy,
                Pogress = (int) value
            };
            changed?.Invoke(this, progress);
        }

        public long Rebuild()
        {
            return 0;
            //if (IsDisposed)
            //    return -1;
            //long result = 0;
            //if (ProductiesDb != null)
            //    result &= ProductiesDb.Rebuild();
            //if (PersoneelDb != null)
            //    result &= PersoneelDb.Rebuild();
            //if (GereedDb != null)
            //    result &= GereedDb.Rebuild();
            //if (AccountsDb != null)
            //    result &= AccountsDb.Rebuild();
            //if (ChangedDb != null)
            //    result &= ChangedDb.Rebuild();
            //if (SettingsDb != null)
            //    result &= SettingsDb.Rebuild();
            //if (LogDb != null)
            //    result &= LogDb.Rebuild();
            //return result;
        }

        #endregion Database;

        #region Disposing

        // To detect redundant calls

        // Instantiate a SafeHandle instance.
        private readonly SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            //ProductiesDb?.Checkpoint();
            //PersoneelDb?.Checkpoint();
            //GereedDb?.Checkpoint();
            //AccountsDb?.Checkpoint();
            //ChangedDb?.Checkpoint();
            //SettingsDb?.Checkpoint();
            //LogDb?.Checkpoint();
            //ProductiesDb?.Dispose();
            //PersoneelDb?.Dispose();
            //GereedDb?.Dispose();
            //AccountsDb?.Dispose();
            //ChangedDb?.Dispose();
            //SettingsDb?.Dispose();
            //LogDb?.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) return;
            UserAccounts = null;
            ProductieFormulieren = null;
            AllSettings = null;
            PersoneelLijst = null;
            //ChangeLog = null;
            Logger = null;
            GereedFormulieren = null;
            //ProductiesDb = null;
            //PersoneelDb = null;
            //GereedDb = null;
            //AccountsDb = null;
            //ChangedDb = null;
            //SettingsDb = null;
            //LogDb = null;
            if (disposing)
                // Dispose managed state (managed objects).
                _safeHandle?.Dispose();

            IsDisposed = true;
        }

        #endregion Disposing
    }
}