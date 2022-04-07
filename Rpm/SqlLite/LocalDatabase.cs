using Microsoft.Win32.SafeHandles;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Rpm.SqlLite
{
    public class LocalDatabase : IDisposable
    {
        //public static readonly string DbVersion = Version;

        public LocalDatabase(Manager instance, string pcid, string path)
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
                        if (e.UserState is Personeel[] {Length: > 0} acc)
                        {
                            LastPersoneelUpdate = DateTime.Now;
                            foreach (var pers in acc)
                                if (e.ProgressPercentage < 1)
                                    Manager.PersoneelChanged(this, pers);
                        }
                    }
                    else if (e.UserState is ProductieFormulier[] state)
                    {
                        if (state is {Length: > 0})
                        {
                            LastProductieUpdate = DateTime.Now;
                            foreach (var prod in state)
                                if (e.ProgressPercentage == 0)
                                    Manager.FormulierChanged(this, prod);
                                //else Manager.FormulierDeleted(this, prod);
                            //  Manager.LoadProducties();
                        }
                    }
                    else if (e.UserState is UserAccount[] acc)
                    {
                        if (acc is {Length: > 0})
                        {
                            LastAccountUpdate = DateTime.Now;
                            foreach (var account in acc) Manager.AccountChanged(this, account);
                        }
                    }
                    else if (e.UserState is UserSettings settings)
                    {
                        LastSettingUpdate = DateTime.Now;
                        Manager.UserSettingChanged(this, settings);
                    }
                    else if (e.UserState is UserChange {Change: { }} change)
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
                                    case DbType.Geen:
                                        break;
                                    default:
                                        throw new ArgumentOutOfRangeException();
                                }

                        Manager.RemoteMessage(change.CreateMessage(change.DbIds.FirstOrDefault().Key));
                    }
                }
                catch
                {
                }
        }

        #endregion BackgroundWorker

        #region ProductieFormulieren

        public Task<ProductieFormulier> FindProductie(string criteria, bool Fullmatch)
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

        public ProductieFormulier GetProductie(string productienr, bool usesecondary)
        {
            try
            {
                ProductieFormulier prod = null;
                if (ProductieFormulieren != null)
                    prod = ProductieFormulieren.FindOne(productienr.Trim(), usesecondary).Result;
                if (prod == null && GereedFormulieren != null)
                    prod = GereedFormulieren.FindOne(productienr.Trim(), usesecondary).Result;
                prod?.UpdateForm(true, false, null, null, false, false, false);
                return prod;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        public Task<ProductieFormulier> GetProductieFromPath(string path)
        {
            return Task.Run( () =>
            {
                try
                {
                    if (!File.Exists(path)) return null;
                    return MultipleFileDb.FromPath<ProductieFormulier>(path,false);
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

        public Task<List<ProductieFormulier>> GetProducties(List<string> ids)
        {
            return Task.Run(() =>
            {
                var xret = new List<ProductieFormulier>();
                try
                {
                    foreach (var id in ids)
                    {
                        var xprod = Manager.Database?.GetProductie(id, true);
                        if (xprod == null) continue;
                        xret.Add(xprod);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return xret;
            });
        }

        public Task<List<Bewerking>> GetBewerkingen(List<string> ids, bool filter)
        {
            return Task.Run(() =>
            {
                var xret = new List<Bewerking>();
                try
                {
                    foreach (var id in ids)
                    {
                        var xprod = Manager.Database?.GetProductie(id, true);
                        if (xprod?.Bewerkingen == null) continue;
                        foreach (var bw in xprod.Bewerkingen)
                        {
                            if (filter && !bw.IsAllowed())
                                continue;
                            xret.Add(bw);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return xret;
            });
        }


        public Task<List<ProductieFormulier>> GetAllProducties(bool incgereed, bool filter, bool checksecondary)
        {
            return GetAllProducties(incgereed, filter, null, checksecondary);
        }

        public Task<List<ProductieFormulier>> GetAllProducties(bool incgereed, bool filter, IsValidHandler validhandler, bool checksecondary)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed)
                    return new List<ProductieFormulier>();
                var prods = new List<ProductieFormulier>();
                if (filter && validhandler == null)
                    validhandler = Functions.IsAllowed;
                if (ProductieFormulieren != null) prods = await ProductieFormulieren.FindAll(validhandler, checksecondary);

                if (incgereed && GereedFormulieren != null)
                {
                    var xprods = await GereedFormulieren.FindAll(validhandler, checksecondary);
                    if (xprods.Count > 0)
                        prods.AddRange(xprods);
                }

                return prods;//filter ? prods.Where(x => x.IsAllowed(null)).ToList() : prods;
            });
        }

        public Task<List<Bewerking>> GetAllBewerkingen(bool incgereed, bool filter, bool checksecondary)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed)
                    return new List<Bewerking>();
                var prods = new List<ProductieFormulier>();
                if (ProductieFormulieren != null) prods = await ProductieFormulieren.FindAll(checksecondary);

                if (incgereed && GereedFormulieren != null)
                {
                    var xprods = await GereedFormulieren.FindAll(checksecondary);
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

        public Task<Dictionary<string, List<Bewerking>>> GetBewerkingenInArtnrSections(bool incgereed, bool filter, bool checksecondary)
        {
            return Task.Factory.StartNew(() =>
            {
                var xreturn = new Dictionary<string, List<Bewerking>>();
                try
                {
                    var bws = Manager.Database.GetAllBewerkingen(true, true, checksecondary).Result;
                    foreach (var bw in bws)
                    {
                        if (xreturn.ContainsKey(bw.ArtikelNr.ToUpper()))
                            xreturn[bw.ArtikelNr.ToUpper()].Add(bw);
                        else
                            xreturn.Add(bw.ArtikelNr.ToUpper(), new List<Bewerking>() { bw });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return xreturn;
            });
           
        }

        public Task<List<ProductieFormulier>> GetAllProducties(bool incgereed, bool filter, TijdEntry bereik, IsValidHandler validhandler, bool checksecondary)
        {
            return Task.Run(() =>
            {
                if (IsDisposed)
                    return new List<ProductieFormulier>();
                var prods = new List<ProductieFormulier>();
                if (ProductieFormulieren != null)
                {
                    prods = ProductieFormulieren.FindAll(validhandler, checksecondary).Result;
                    if (bereik != null)
                        prods = prods.Where(x => x.HeeftGewerkt(bereik)).ToList();
                }

                if (incgereed && GereedFormulieren != null)
                {
                    var xprods = GereedFormulieren.FindAll(validhandler, checksecondary).Result;
                    if (bereik != null)
                        xprods = xprods.Where(x => x.HeeftGewerkt(bereik)).ToList();
                    if (xprods.Count > 0)
                        prods.AddRange(xprods);
                }

                return filter ? prods.Where(x => x.IsAllowed(null)).ToList() : prods;
            });
        }

        public Task<List<Bewerking>> GetBewerkingen(ViewState state, bool filter, TijdEntry bereik,
            IsValidHandler validhandler, bool checksecondary)
        {
            return Task.Run(() =>
            {
                if (IsDisposed)
                    return new List<Bewerking>();
                var prods = GetAllProducties(state is ViewState.Alles or ViewState.Gereed, filter,
                    bereik, validhandler, checksecondary).Result;

                var bws = new List<Bewerking>();
                foreach (var pr in prods)
                {
                    if (pr?.Bewerkingen == null || pr.Bewerkingen.Length == 0)
                        continue;
                    foreach (var bw in pr.Bewerkingen)
                        if (bw != null)
                        {
                            if (filter && !bw.IsAllowed(null, state))
                                continue;
                            if (validhandler != null && !validhandler.Invoke(bw, null)) continue;
                            if (bereik != null && !bw.HeeftGewerkt(bereik)) continue;
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
                if (!alleengereed && ProductieFormulieren != null) prods = await ProductieFormulieren.FindAll(criteria, fullmatch,true);

                if (incgereed && GereedFormulieren != null)
                {
                    var xprods = await GereedFormulieren.FindAll(criteria, fullmatch,true);
                    if (xprods.Count > 0)
                        prods.AddRange(xprods);
                }

                return prods;
            });
        }

        public Task<List<ProductieFormulier>> GetAllGereedProducties(IsValidHandler validhandler)
        {
            return Task.Run(() =>
            {
                if (IsDisposed || GereedFormulieren == null)
                    return new List<ProductieFormulier>();
                return GereedFormulieren.FindAll(validhandler, true).Result;
            });
        }

        public Task<List<ProductieFormulier>> GetAllGereedProducties()
        {
            return Task.Run(() =>
            {
                if (IsDisposed || GereedFormulieren == null)
                    return new List<ProductieFormulier>();
                return GereedFormulieren.FindAll(true).Result;
            });
        }

        public Task<bool> UpSert(ProductieFormulier form, string change, bool showmessage = true, bool onlylocal = false)
        {
            return UpSert(form.ProductieNr, form, change, showmessage,onlylocal);
        }

        public Task<bool> UpSert(ProductieFormulier form, bool showmessage = true, bool onlylocal = false, string change = null)
        {
            return UpSert(form.ProductieNr, form, change??$"[{form.ArtikelNr}|{form.ProductieNr}] ProductieFormulier Update",
                showmessage,onlylocal);
        }

        public Task<bool> UpSert(string id, ProductieFormulier form, string change, bool showmessage = true, bool onlylocal = false)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || id == null || form == null || ProductieFormulieren == null || GereedFormulieren == null)
                    return false;
                bool xreturn = false;
                try
                {
                    form.ExcludeFromUpdate();
                    form.LastChanged = form.LastChanged.UpdateChange(change, DbType.Producties);
                    if (ProductieFormulieren != null && form.Bewerkingen.All(x => x.State == ProductieState.Gereed))
                    {
                        GereedFormulieren.RaiseEventWhenChanged = !RaiseEventWhenChanged;
                        if (await GereedFormulieren.Upsert(id, form,onlylocal,change))
                        {
                            _=UpdateChange(form.LastChanged, DbType.GereedProducties,
                                showmessage);
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
                        if (await ProductieFormulieren.Upsert(id, form,onlylocal,change))
                        {
                            _= UpdateChange(form.LastChanged, DbType.Producties,
                                showmessage);
                           

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
                    if (xreturn && RaiseEventWhenChanged)
                        Manager.FormulierChanged(this, form);
                    //Manager.FormulierChanged(this, form);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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
                    var form = await ProductieFormulieren.FindOne(id,false);
                    if (form == null) return false;
                    form.ExcludeFromUpdate();
                    var changed = new UserChange().UpdateChange(change, DbType.Producties);
                    changed.IsRemoved = true;
                    await UpdateChange(changed, DbType.Producties, showmessage);
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
                        await UpdateChange(form.LastChanged, DbType.Producties, showmessage);
                        if (RaiseEventWhenDeleted)
                            Manager.FormulierDeleted(this, form.ProductieNr);
                        xreturn = true;
                    }

                    if (await GereedFormulieren.Delete(form.ProductieNr))
                    {
                        form.LastChanged = form.LastChanged.UpdateChange(change, DbType.GereedProducties);
                        form.LastChanged.IsRemoved = true;
                        await UpdateChange(form.LastChanged, DbType.GereedProducties,
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

        public Task<bool> Replace(string id, ProductieFormulier newform, bool showmessage = true, bool onlylocal = false)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || ProductieFormulieren == null || id == null || newform == null)
                    return false;
                try
                {
                    await DeleteProductie(id, showmessage);
                    await UpSert(newform, showmessage,onlylocal);
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
                    return UserAccounts?.FindOne(username, false).Result;
                }
                catch
                {
                    return null;
                }
            });
        }

        public Task<List<UserAccount>> GetAllAccounts()
        {
            return Task.Run(() =>
            {
                if (IsDisposed || UserAccounts == null)
                    return new List<UserAccount>();
                return UserAccounts.FindAll(false).Result;
            });
        }

        public Task<bool> UpSert(UserAccount account, bool showmessage = true, bool onlylocal = false)
        {
            return UpSert(account.Username, account, "Gebruiker Account Update", showmessage,onlylocal);
        }

        public Task<bool> UpSert(UserAccount account, string change, bool showmessage = true, bool onlylocal = false)
        {
            return UpSert(account.Username, account, change, showmessage);
        }

        public Task<bool> UpSert(string id, UserAccount account, string change, bool showmessage = true, bool onlylocal = false)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || UserAccounts == null || id == null || account == null)
                    return false;
                try
                {
                    account.LastChanged = account.LastChanged.UpdateChange(change, DbType.Accounts);
                    _=UpdateChange(account.LastChanged, DbType.Accounts, showmessage);
                    await UserAccounts.Upsert(id, account,onlylocal,change);
                    Manager.AccountChanged(this, account);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<int> UpSert(UserAccount[] accounts, string change, bool showmessage = true, bool onlylocal = false)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || UserAccounts == null || accounts == null)
                    return -1;
                try
                {
                    var count = 0;
                    foreach (var account in accounts)
                        if (await UpSert(account.Username, account, change, showmessage,onlylocal))
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
                        await UpdateChange(changed, DbType.Accounts, showmessage);
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
                    var pers = await UserAccounts.FindOne(id, false);
                    if (pers != null && await UserAccounts.Delete(id))
                    {
                        var changed = new UserChange
                        {
                            Change = $"[{id}] Account Verwijderd",
                            IsRemoved = true,
                            PcId = OwnerId,
                            User = Manager.Opties == null ? "Default" : Manager.Opties.Username
                        };
                        await UpdateChange(changed, DbType.Accounts, showmessage);
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
                return AllSettings.FindOne(username, false);
            }
            catch
            {
                return null;
            }
        }

        public Task<List<UserSettings>> GetAllSettings()
        {
            return Task.Run(() =>
            {
                if (IsDisposed || AllSettings == null)
                    return new List<UserSettings>();
                return AllSettings.FindAll(false).Result;
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

        public Task<bool> UpSert(string id, UserSettings account, string change, bool showmessage = true, bool onlylocal = false)
        {
            return Task.Run(async () =>
            {
                if (IsDisposed || AllSettings == null || id == null || account == null)
                    return false;
                try
                {
                    account.LastChanged = account.LastChanged.UpdateChange(change, DbType.Opties);
                    await UpdateChange(account.LastChanged, DbType.Opties, showmessage);
                    if (AllSettings != null)
                        await AllSettings.Upsert(id, account,onlylocal,change);
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
                    var xset = AllSettings.FindOne(id, false);
                    if (xset != null && await AllSettings.Delete(id))
                    {
                        var lastchange = new UserChange().UpdateChange($"[{id}] Optie Verwijderd", DbType.Opties);
                        lastchange.IsRemoved = true;
                        await UpdateChange(lastchange, DbType.Opties, showmessage);
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
                    return await PersoneelLijst.FindOne(username, false);
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
                        if (personen.Any(x => string.Equals(x.PersoneelNaam, name, StringComparison.CurrentCultureIgnoreCase)))
                            continue;
                        var pers = await PersoneelLijst.FindOne(name, false);
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
            return Task.Run(() =>
            {
                if (IsDisposed || PersoneelLijst == null)
                    return new List<Personeel>();
                return PersoneelLijst.FindAll(true).Result;
            });
        }

        public Task<bool> MaakPersoneelVrijVanWerk(string personeel, bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (personeel != null && !IsDisposed && PersoneelLijst != null)
                {
                    var xpers = await PersoneelLijst.FindOne(personeel, false);
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
                if (personen is {Length: > 0} && !IsDisposed && PersoneelLijst != null)
                    foreach (var personeel in personen)
                    {
                        var xpers = await PersoneelLijst.FindOne(personeel.PersoneelNaam, false);
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
            return Task.Run(() =>
            {
                var done = 0;
                if (!IsDisposed && PersoneelLijst != null)
                {
                    var personen = PersoneelLijst.FindAll(false).Result;
                    foreach (var personeel in personen)
                        if (personeel.WerktAan != null)
                        {
                            personeel.WerktAan = null;
                            if (UpSert(personeel, "Vrij gemaakt van werk", showmessage).Result)
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

        public Task<bool> UpSert(string id, Personeel persoon, string change, bool showmessage = true, bool onlylocal = false)
        {
            return Task.Run( () =>
            {
                if (IsDisposed || PersoneelLijst == null || id == null || persoon == null)
                    return false;
                try
                {
                    persoon.LastChanged = persoon.LastChanged.UpdateChange(change, DbType.Medewerkers);
                    UpdateChange(persoon.LastChanged, DbType.Medewerkers,
                        showmessage);
                    PersoneelLijst.RaiseEventWhenChanged = !RaiseEventWhenChanged;
                    _= PersoneelLijst.Upsert(id, persoon,onlylocal,change).Result;
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
                   
                    var per = await PersoneelLijst.FindOne(id, false);
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
                        await UpdateChange(lastchange,DbType.Medewerkers, showmessage);
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

        public Task UpdateChange(UserChange change, DbType dbname,
            bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                try
                {
                    if (NotificationEnabled && showmessage && !string.IsNullOrEmpty(change?.Change))
                        Manager.RemoteMessage(change.CreateMessage(dbname));
                    if (LoggerEnabled && showmessage && !string.IsNullOrEmpty(change?.Change)) await AddLog(change.Change, MsgType.Info);
                    // PManager.RemoteMessage(new Mailing.RemoteMessage(change.Change, MessageAction.None, MsgType.Info));
                }
                catch
                {
                    // ignored
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
                        return await Logger.Upsert(ent.Id.ToString(), ent, false,null);
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
                        var found = await Logger.FindAll(new TijdEntry(from, to),validhandler,true);
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

        public Task<int> RemoveFromCollection(string collection,string[] items)
        {
            switch (collection.ToLower())
            {
                case "sqldatabase":
                    return ProductieFormulieren.Delete(items);
                case "personeeldb":
                    return PersoneelLijst.Delete(items);
                case "gereeddb":
                    return GereedFormulieren.Delete(items);
                case "settingdb":
                    return AllSettings.Delete(items);
                case "accountsdb":
                    return UserAccounts.Delete(items);
                case "logdb":
                    return Logger.Delete(items);
                case "versiondb":
                    return DbVersions.Delete(items);
            }

            return Task<int>.Factory.StartNew(() => 0);
        }

        public void LoadMultiFiles()
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
            ProductieFormulieren = new DatabaseInstance<ProductieFormulier>(DbInstanceType.MultipleFiles, DbType.Producties, RootPath,
                dbfilename, "ProductieFormulieren", true);
            ProductieFormulieren.InstanceChanged += ProductieFormulieren_InstanceChanged;
            ProductieFormulieren.InstanceDeleted += ProductieFormulieren_InstanceDeleted;

            PersoneelLijst =
                new DatabaseInstance<Personeel>(DbInstanceType.MultipleFiles, DbType.Medewerkers, RootPath, personeeldb, "Personeel", true);
            PersoneelLijst.InstanceChanged += PersoneelLijst_InstanceChanged;
            PersoneelLijst.InstanceDeleted += PersoneelLijst_InstanceDeleted;

            UserAccounts = new DatabaseInstance<UserAccount>(DbInstanceType.MultipleFiles, DbType.Accounts, RootPath, Accountdb,
                "UserAccounts", true);
            UserAccounts.InstanceChanged += UserAccounts_InstanceChanged;
            UserAccounts.InstanceDeleted += UserAccounts_InstanceDeleted;

            AllSettings = new DatabaseInstance<UserSettings>(DbInstanceType.MultipleFiles, DbType.Opties, RootPath, Settingdb,
                "AllSettings", true);
            AllSettings.InstanceChanged += AllSettings_InstanceChanged;
            AllSettings.InstanceDeleted += AllSettings_InstanceDeleted;

            Logger = new DatabaseInstance<LogEntry>(DbInstanceType.MultipleFiles, DbType.Logs, RootPath, Logdb, "Logs", false);
            // ChangeLog = new DatabaseInstance<UserChange>(DbInstanceType.MultipleFiles, RootPath, changeddb,
            // "ChangeLog");
            GereedFormulieren = new DatabaseInstance<ProductieFormulier>(DbInstanceType.MultipleFiles, DbType.GereedProducties, RootPath,
                Gereeddb, "GereedFormulieren", true);
            GereedFormulieren.InstanceChanged += ProductieFormulieren_InstanceChanged;
            GereedFormulieren.InstanceDeleted += ProductieFormulieren_InstanceDeleted;

            DbVersions = new DatabaseInstance<DbVersion>(DbInstanceType.MultipleFiles, DbType.Versions, RootPath, versiondb,
                "DbVersions", false);
            //BewerkingEntries = new DatabaseInstance<BewerkingEntry>(DbInstanceType.MultipleFiles, RootPath,
            //    bewerkingentriesdb, "BewerkingEntries");
        }

        #region Database Events
        private void AllSettings_InstanceDeleted(object sender, FileSystemEventArgs y)
        {
            
        }

        private void AllSettings_InstanceChanged(object sender, FileSystemEventArgs y)
        {
            //lock (_locker1)
            //{
                try
                {
                    if (AllSettings == null || !RaiseEventWhenChanged) return;
                    var id = Path.GetFileNameWithoutExtension(y.FullPath);
                    var xset = AllSettings.FindOne(id, false).Result;
                    if(xset != null && Manager.Opties != null && string.Equals(Manager.Opties.Username, xset.Username, StringComparison.CurrentCultureIgnoreCase))
                    {
                        var xdiffers = new List<string>();
                        if (!Manager.Opties.xPublicInstancePropertiesEqual(xset, xdiffers))
                        {
                            Manager.Opties = xset;
                            Manager.SettingsChanged(this, true);
                        }
                    }
                }
                catch (Exception x)
                {
                    Console.WriteLine(x);
                }
            //}
        }

        private void UserAccounts_InstanceDeleted(object sender, FileSystemEventArgs y)
        {
            
        }

        private void UserAccounts_InstanceChanged(object sender, FileSystemEventArgs y)
        {
            
        }

        private void PersoneelLijst_InstanceDeleted(object sender, FileSystemEventArgs y)
        {
            //lock (_locker2)
            //{
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
            //}
        }

        private void PersoneelLijst_InstanceChanged(object sender, FileSystemEventArgs y)
        {
            // lock (_locker2)
            // {
            try
            {
                if (!RaiseEventWhenChanged || PersoneelLijst == null) return;
                var pers = PersoneelLijst.FromPath<Personeel>(y.FullPath).Result;
                if (pers != null)
                    Manager.PersoneelChanged(this, pers);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            //}
        }

        private void ProductieFormulieren_InstanceDeleted(object sender, FileSystemEventArgs y)
        {
           // lock (_locker1)
            //{
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
            //}
        }

        private void ProductieFormulieren_InstanceChanged(object sender, FileSystemEventArgs e)
        {
            //lock (_locker1)
            //{
                try
                {
                    if (ProductieFormulieren == null || !RaiseEventWhenChanged) return;
                    ProductieFormulier prod = null;
                    Task.Factory.StartNew(() =>
                    {
                        prod = MultipleFileDb.FromPath<ProductieFormulier>(e.FullPath, false).Result;
                    }).Wait(60000);

                    if (prod != null && prod.IsAllowed(null))
                        Manager.FormulierChanged(this, prod);
                }
                catch (Exception x)
                {
                    Console.WriteLine(x);
                }
            //}
        }
        #endregion

        //private static readonly object _locker1 = new object();
        //private static readonly object _locker2 = new object();
        //private static readonly object _locker3 = new object();

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
                            if (t == DbType.Alles || t == DbType.Geen)
                                continue;
                            count += await Count(t);
                        }

                        return count;
                    case DbType.Opmerkingen:
                        break;
                    case DbType.Klachten:
                        break;
                    case DbType.Verpakkingen:
                        break;
                    case DbType.ArtikelRecords:
                        break;
                    case DbType.SpoorOverzicht:
                        break;
                    case DbType.LijstLayouts:
                        break;
                    case DbType.MeldingCenter:
                        break;
                    case DbType.Bijlages:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }

                return 0;
            });
        }

        //public async void CheckVersions(DbType type)
        //{
        //    if (IsDisposed || DbVersions == null)
        //        return;
        //    double cur = 1;
        //    var version = await DbVersions.FindOne(Enum.GetName(typeof(DbType), type)) ?? new DbVersion
        //    { Version = cur, Name = Enum.GetName(typeof(DbType), type) };
        //    switch (type)
        //    {
        //        case DbType.Producties:
        //            cur = 2; // huidige productie db versie
        //            if (version.Version < cur && cur > 1)
        //                try
        //                {
        //                    var prods = await ProductieFormulieren.FindAll();
        //                    foreach (var prod in prods)
        //                    {
        //                        if (prod.State == ProductieState.Verwijderd) continue;
        //                        await prod.UpdateVersion();
        //                    }

        //                    version.Version = cur;
        //                    await DbVersions.Upsert(version.Name, version,false);
        //                }
        //                catch
        //                {
        //                }

        //            break;
        //        case DbType.Changes:
        //            break;
        //        case DbType.Medewerkers:
        //            break;
        //        case DbType.GereedProducties:
        //            break;
        //        case DbType.Opties:
        //            break;
        //        case DbType.Accounts:
        //            break;
        //        case DbType.Logs:
        //            break;
        //        case DbType.Versions:
        //            break;
        //    }
        //}

        public void Close(DbType type)
        {
            switch (type)
            {
                case DbType.Producties:
                    ProductieFormulieren = null;
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

        //public Task<bool> NeedDbCheck()
        //{
        //    return Task.Run(async () => (await GetLastDbChanges()).Count > 0);
        //}

        public Task<int> UpdateDbFromDb(DatabaseUpdateEntry dbentry, CancellationTokenSource token,
            ProgressChangedHandler changed = null)
        {
            return Task.Run(() =>
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
                    var database = new LocalDatabase(PManager, Manager.SystemId, dbpath);
                    database.NotificationEnabled = false;
                    database.LoggerEnabled = false;

                    NotificationEnabled = false;
                    LoggerEnabled = false;
                    RaiseEventWhenChanged = false;
                    RaiseEventWhenDeleted = false;
                    database.LoadMultiFiles();
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
                            var xs = database.AllSettings.FindAll(new TijdEntry(dbentry.LastUpdated, DateTime.MaxValue),null,false).Result;
                            if (xs is {Count: > 0})
                            {
                                max = xs.Count;
                                count = 0;
                                foreach (var s in xs)
                                {
                                    token.Token.ThrowIfCancellationRequested();
                                    var myitem = GetSetting($"{s.Username}[{s.SystemID}]").Result;
                                    if (myitem == null || myitem.LastChanged != null &&
                                        (myitem.LastChanged == null && s.LastChanged != null ||
                                         s.LastChanged != null && myitem.LastChanged.TimeChanged <
                                         s.LastChanged.TimeChanged))
                                    {
                                         UpSert(s,
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
                            var xs1 = database.UserAccounts.FindAll(new TijdEntry(dbentry.LastUpdated, DateTime.MaxValue),null, false).Result;
                            if (xs1 is {Count: > 0})
                            {
                                //count = 0;
                                max += xs1.Count;
                                foreach (var s in xs1)
                                {
                                    token.Token.ThrowIfCancellationRequested();
                                    var myitem = GetAccount(s.Username).Result;
                                    if (myitem == null || myitem.LastChanged != null &&
                                        (myitem.LastChanged == null && s.LastChanged != null ||
                                         s.LastChanged != null && myitem.LastChanged.TimeChanged <
                                         s.LastChanged.TimeChanged))
                                    {
                                        UpSert(s,
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
                            var xs2 = database.PersoneelLijst.FindAll(new TijdEntry(dbentry.LastUpdated, DateTime.MaxValue),null,false).Result;
                            if (xs2 is {Count: > 0})
                            {
                                // count = 0;
                                max += xs2.Count;
                                foreach (var s in xs2)
                                {
                                    token.Token.ThrowIfCancellationRequested();
                                    var myitem = GetPersoneel(s.PersoneelNaam).Result;
                                    if (myitem != null)
                                    {
                                        updated += myitem.UpdateFrom(s, true);
                                    }
                                    else
                                    {
                                        UpSert(s,
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
                           
                            var xs3 = database.ProductieFormulieren?.FindAll(new TijdEntry(dbentry.LastUpdated, DateTime.MaxValue), null, false).Result;
                            if (xs3 is {Count: > 0})
                            {
                                max += xs3.Count;
                                foreach (var s in xs3)
                                {
                                    token.Token.ThrowIfCancellationRequested();
                                    var myitem = Manager.Database.GetProductie(s.ProductieNr, true);
                                    if (myitem != null)
                                    {
                                        if (myitem.UpdateFrom(s,
                                            $"[{myitem.ProductieNr}, {myitem.ArtikelNr}] geupdate vanuit {dbentry.Naam} database.").Result
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
                            var xs3 = database.GereedFormulieren?.FindAll(new TijdEntry(dbentry.LastUpdated, DateTime.MaxValue), null, false).Result;
                            if (xs3 is {Count: > 0})
                            {
                                max += xs3.Count;
                                foreach (var s in xs3)
                                {
                                    token.Token.ThrowIfCancellationRequested();
                                    var myitem = Manager.Database.GetProductie(s.ProductieNr, true);
                                    if (myitem != null)
                                    {
                                        if (myitem.UpdateFrom(s,
                                            $"[{myitem.ProductieNr}, {myitem.ArtikelNr}] geupdate vanuit {dbentry.Naam} database.").Result
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
                catch
                {
                    // ignored
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
            ProductieFormulieren?.Close();
            PersoneelLijst?.Close();
            GereedFormulieren?.Close();
            UserAccounts?.Close();
            DbVersions?.Close();
            AllSettings?.Close();
            Logger?.Close();
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