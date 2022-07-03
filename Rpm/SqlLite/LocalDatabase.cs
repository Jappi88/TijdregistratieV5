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
            return Task.Factory.StartNew(() =>
            {
                ProductieFormulier form = null;
                try
                {
                    if (IsDisposed || ProductieFormulieren == null)
                        return null;

                    form = (xGetAllProducties(criteria, Fullmatch, false,true)).FirstOrDefault();
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
                    prod = ProductieFormulieren.FindOne(productienr.Trim(), usesecondary);
                if (prod == null && GereedFormulieren != null)
                    prod = GereedFormulieren.FindOne(productienr.Trim(), usesecondary);
                prod?.UpdateValues(null, true, false);
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
            return Task.Run(() =>
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
            return Task.Factory.StartNew(() =>
            {
                bool isgereed = state == ProductieState.Gereed;
                var xreturn = xGetAllProducties(criteria, fullmatch, isgereed, isgereed);
                xreturn = xreturn.Where(x => x.State == state && (!tijdgewerkt || (x.TijdGewerkt > 0 && x.ActueelPerUur > 0))).ToList();
                return xreturn;
            });
        }

        public List<ProductieFormulier> xGetProducties(string criteria, bool fullmatch, ProductieState state,
    bool tijdgewerkt)
        {
            bool isgereed = state == ProductieState.Gereed;
            var xreturn = xGetAllProducties(criteria, fullmatch, isgereed, isgereed);
            xreturn = xreturn.Where(x => x.State == state && (!tijdgewerkt || (x.TijdGewerkt > 0 && x.ActueelPerUur > 0))).ToList();
            return xreturn;
        }

        public Task<List<ProductieFormulier>> GetProducties(string criteria, bool fullmatch,
            bool tijdgewerkt)
        {
            return Task.Factory.StartNew(() =>
            {
                var xreturn = xGetAllProducties(criteria, fullmatch, false, true);
                if (tijdgewerkt)
                    xreturn = xreturn.Where(x => x.TijdGewerkt > 0).ToList();
                return xreturn;
            });
        }

        public Task<List<ProductieFormulier>> GetProducties(List<string> ids)
        {
            return Task.Factory.StartNew(() =>
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
            return Task.Factory.StartNew(() =>
            {
                return xGetBewerkingen(ids, filter);
            });
        }

        public List<Bewerking> xGetBewerkingen(List<string> ids, bool filter)
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
        }

        public Task<List<ProductieFormulier>> GetAllProducties(bool incgereed, bool filter, bool checksecondary)
        {
            return GetAllProducties(incgereed, filter, null, checksecondary);
        }

        public Task<List<ProductieFormulier>> GetAllProducties(bool incgereed, bool filter, IsValidHandler validhandler, bool checksecondary)
        {
            return Task.Factory.StartNew(() =>
            {
                if (IsDisposed)
                    return new List<ProductieFormulier>();
                var prods = new List<ProductieFormulier>();
                if (filter && validhandler == null)
                    validhandler = Functions.IsAllowed;
                if (ProductieFormulieren != null) prods = ProductieFormulieren.FindAll(validhandler, checksecondary);

                if (incgereed && GereedFormulieren != null)
                {
                    var xprods = GereedFormulieren.FindAll(validhandler, checksecondary);
                    if (xprods.Count > 0)
                        prods.AddRange(xprods);
                }

                return prods;//filter ? prods.Where(x => x.IsAllowed(null)).ToList() : prods;
            });
        }

        public List<ProductieFormulier> xGetAllProducties(bool incgereed, bool filter, IsValidHandler validhandler, bool checksecondary)
        {
            if (IsDisposed)
                return new List<ProductieFormulier>();
            var prods = new List<ProductieFormulier>();
            if (filter && validhandler == null)
                validhandler = Functions.IsAllowed;
            if (ProductieFormulieren != null) prods = ProductieFormulieren.FindAll(validhandler, checksecondary);

            if (incgereed && GereedFormulieren != null)
            {
                var xprods = GereedFormulieren.FindAll(validhandler, checksecondary);
                if (xprods.Count > 0)
                    prods.AddRange(xprods);
            }

            return prods;//filter ? prods.Where(x => x.IsAllowed(null)).ToList() : prods;
        }

        public Task<List<Bewerking>> GetAllBewerkingen(bool incgereed, bool filter, bool checksecondary)
        {
            return Task.Factory.StartNew(() =>
            {
               return xGetAllBewerkingen(incgereed, filter, checksecondary);
            });
        }

        public List<Bewerking> xGetAllBewerkingen(bool incgereed, bool filter, bool checksecondary)
        {
            if (IsDisposed)
                return new List<Bewerking>();
            var prods = new List<ProductieFormulier>();
            if (ProductieFormulieren != null) prods = ProductieFormulieren.FindAll(checksecondary);

            if (incgereed && GereedFormulieren != null)
            {
                var xprods = GereedFormulieren.FindAll(checksecondary);
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
        }

        public Task<Dictionary<string, List<Bewerking>>> GetBewerkingenInArtnrSections(bool incgereed, bool filter, bool checksecondary)
        {
            return Task.Factory.StartNew(() =>
            {
                return xGetBewerkingenInArtnrSections(incgereed, filter, checksecondary);
            });
        }

        public Dictionary<string, List<Bewerking>> xGetBewerkingenInArtnrSections(bool incgereed, bool filter, bool checksecondary)
        {
            var xreturn = new Dictionary<string, List<Bewerking>>();
            try
            {
                var bws = Manager.Database.xGetAllBewerkingen(incgereed, filter, checksecondary);
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
        }

        public Task<List<ProductieFormulier>> GetAllProducties(bool incgereed, bool filter, TijdEntry bereik, IsValidHandler validhandler, bool checksecondary)
        {
            return Task.Factory.StartNew(() =>
            {
                return xGetAllProducties(incgereed, filter, bereik, validhandler, checksecondary);
            });
        }

        public List<ProductieFormulier> xGetAllProducties(bool incgereed, bool filter, TijdEntry bereik, IsValidHandler validhandler, bool checksecondary)
        {
            if (IsDisposed)
                return new List<ProductieFormulier>();
            var prods = new List<ProductieFormulier>();
            if (ProductieFormulieren != null)
            {
                prods = ProductieFormulieren.FindAll(validhandler, checksecondary);
                if (bereik != null)
                    prods = prods.Where(x => x.HeeftGewerkt(bereik)).ToList();
            }

            if (incgereed && GereedFormulieren != null)
            {
                var xprods = GereedFormulieren.FindAll(validhandler, checksecondary);
                if (bereik != null)
                    xprods = xprods.Where(x => x.HeeftGewerkt(bereik)).ToList();
                if (xprods.Count > 0)
                    prods.AddRange(xprods);
            }

            return filter ? prods.Where(x => x.IsAllowed(null)).ToList() : prods;
        }

        public Task<List<Bewerking>> GetBewerkingen(ViewState state, bool filter, TijdEntry bereik,
            IsValidHandler validhandler, bool checksecondary)
        {
            return Task.Factory.StartNew(() =>
            {
                return xGetBewerkingen(state, filter, bereik, validhandler, checksecondary);
            });
        }

        public List<Bewerking> xGetBewerkingen(ViewState state, bool filter, TijdEntry bereik,
    IsValidHandler validhandler, bool checksecondary)
        {
            if (IsDisposed)
                return new List<Bewerking>();
            var prods = xGetAllProducties(state is ViewState.Alles or ViewState.Gereed, filter,
                bereik, validhandler, checksecondary);

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
        }

        public Task<List<ProductieFormulier>> GetAllProducties(string criteria, bool fullmatch, bool alleengereed, bool incgereed)
        {
            return Task.Factory.StartNew(() =>
            {
                return xGetAllProducties(criteria, fullmatch, alleengereed, incgereed);
            });
        }


        public List<ProductieFormulier> xGetAllProducties(string criteria, bool fullmatch, bool alleengereed, bool incgereed)
        {
            if (IsDisposed)
                return new List<ProductieFormulier>();
            var prods = new List<ProductieFormulier>();
            if (!alleengereed && ProductieFormulieren != null)
                prods = ProductieFormulieren.FindAll(criteria, fullmatch, true);

            if (incgereed && GereedFormulieren != null)
            {
                var xprods = GereedFormulieren.FindAll(criteria, fullmatch, true);
                if (xprods.Count > 0)
                    prods.AddRange(xprods);
            }

            return prods;
        }

        public Task<List<ProductieFormulier>> GetAllGereedProducties(IsValidHandler validhandler)
        {
            return Task.Factory.StartNew(()=> GereedFormulieren?.FindAll(validhandler, true));
        }

        public Task<List<ProductieFormulier>> GetAllGereedProducties()
        {
            return Task.Factory.StartNew(() => GereedFormulieren?.FindAll(true));
        }

        public List<ProductieFormulier> xGetAllGereedProducties()
        {
            return GereedFormulieren?.FindAll(true);
        }

        public Task<bool> UpSert(ProductieFormulier form, string change, bool showmessage = true, bool onlylocal = false)
        {
            return UpSert(form.ProductieNr, form, change, showmessage, onlylocal);
        }

        public bool xUpSert(ProductieFormulier form, string change, bool showmessage = true, bool onlylocal = false)
        {
            return xUpSert(form.ProductieNr, form, change, showmessage, onlylocal);
        }

        public Task<bool> UpSert(ProductieFormulier form, bool showmessage = true, bool onlylocal = false, string change = null)
        {
            return UpSert(form.ProductieNr, form, change ?? $"[{form.ArtikelNr}|{form.ProductieNr}] ProductieFormulier Update",
                  showmessage, onlylocal);
        }

        public Task<int> UpSert(ProductieFormulier[] forms, string change, bool showmessage = true)
        {
            return Task.Factory.StartNew(() =>
            {
                if (IsDisposed || ProductieFormulieren == null || forms == null)
                    return -1;
                try
                {
                    var done = 0;
                    foreach (var prod in forms)
                        if (xUpSert(prod.ProductieNr, prod, change, showmessage))
                            done++;
                    return done;
                }
                catch
                {
                    return -1;
                }
            });
        }

        public Task<bool> UpSert(string id, ProductieFormulier form, string change, bool showmessage = true, bool onlylocal = false)
        {
            return Task.Factory.StartNew(() =>
            {
                return xUpSert(id, form, change, showmessage, onlylocal);
            });
        }

        public bool xUpSert(string id, ProductieFormulier form, string change, bool showmessage = true, bool onlylocal = false)
        {
            if (IsDisposed || id == null || form == null || ProductieFormulieren == null || GereedFormulieren == null)
                return false;
            bool xreturn = false;
            try
            {
                form.ExcludeFromUpdate();
                form.LastChanged = form.LastChanged.UpdateChange(change, DbType.Producties);
                var del = false;
                if (ProductieFormulieren != null && form.Bewerkingen.All(x => x.State == ProductieState.Gereed))
                {
                    if (ProductieFormulieren.Exists(id))
                    {
                        ProductieFormulieren.RaiseEventWhenDeleted = !RaiseEventWhenDeleted;
                        if (ProductieFormulieren.Delete(id))
                        {
                            del = true;
                        }
                        ProductieFormulieren.RaiseEventWhenDeleted = true;
                    }
                    GereedFormulieren.RaiseEventWhenChanged = !RaiseEventWhenChanged;
                    if (GereedFormulieren.Upsert(id, form, onlylocal, change))
                    {
                        del = false;
                        _ = UpdateChange(form.LastChanged, DbType.GereedProducties,
                            showmessage);
                        xreturn = true;
                    }

                    GereedFormulieren.RaiseEventWhenChanged = true;
                }
                else if (ProductieFormulieren != null)
                {
                    if (GereedFormulieren != null && GereedFormulieren.Exists(id))
                    {
                        GereedFormulieren.RaiseEventWhenDeleted = !RaiseEventWhenDeleted;
                        if (GereedFormulieren.Delete(id))
                        {
                            del = true;
                            //if (RaiseEventWhenDeleted)
                            //    Manager.FormulierDeleted(this, id);
                        }
                        GereedFormulieren.RaiseEventWhenDeleted = true;
                    }
                    ProductieFormulieren.RaiseEventWhenChanged = !RaiseEventWhenChanged;
                    if (ProductieFormulieren.Upsert(id, form, onlylocal, change))
                    {
                        del = false;
                        _ = UpdateChange(form.LastChanged, DbType.Producties,
                            showmessage);
                        xreturn = true;
                    }
                    ProductieFormulieren.RaiseEventWhenChanged = true;
                }
                if (del && RaiseEventWhenDeleted)
                    Manager.FormulierDeleted(this, id);
                else if (!del && xreturn && RaiseEventWhenChanged)
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
        }

        public Task<bool> DeleteProductie(string id, bool showmessage = true)
        {
            return Task.Factory.StartNew(() =>
            {
                return xDeleteProductie(id, showmessage);
            });
        }

        public bool xDeleteProductie(string id, bool showmessage = true)
        {
            if (IsDisposed || ProductieFormulieren == null || id == null)
                return false;
            try
            {
                var change = $"[{id}] Productie Verwijderd";
                var form = ProductieFormulieren.FindOne(id, false);
                if (form == null) return false;
                form.ExcludeFromUpdate();
                var changed = new UserChange().UpdateChange(change, DbType.Producties);
                changed.IsRemoved = true;
                UpdateChange(changed, DbType.Producties, showmessage);
                bool deleted = true;
                if (!ProductieFormulieren.Delete(id))
                    deleted = GereedFormulieren.Delete(form.ProductieNr);

                if (deleted && RaiseEventWhenDeleted)
                    Manager.FormulierDeleted(this, id);
                form.RemoveExcludeFromUpdate();
                return deleted;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> Delete(ProductieFormulier form, bool showmessage = true)
        {
            return Task.Factory.StartNew(() =>
            {
                return xDelete(form, showmessage);
            });
        }

        public bool xDelete(ProductieFormulier form, bool showmessage = true)
        {
            if (IsDisposed || ProductieFormulieren == null || form == null)
                return false;
            bool xreturn = false;
            try
            {
                form.ExcludeFromUpdate();
                var change = $"[{form.ProductieNr}] Productie Verwijderd";
                if (ProductieFormulieren.Delete(form.ProductieNr))
                {
                    form.LastChanged = form.LastChanged.UpdateChange(change, DbType.Producties);
                    form.LastChanged.IsRemoved = true;
                    UpdateChange(form.LastChanged, DbType.Producties, showmessage);
                    if (RaiseEventWhenDeleted)
                        Manager.FormulierDeleted(this, form.ProductieNr);
                    xreturn = true;
                }

                if (GereedFormulieren.Delete(form.ProductieNr))
                {
                    form.LastChanged = form.LastChanged.UpdateChange(change, DbType.GereedProducties);
                    form.LastChanged.IsRemoved = true;
                    UpdateChange(form.LastChanged, DbType.GereedProducties,
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
        }

        public Task<int> Delete(ProductieFormulier[] forms, bool showmessage = true)
        {
            return Task.Factory.StartNew(() =>
            {
                if (IsDisposed || ProductieFormulieren == null || forms == null)
                    return -1;
                try
                {
                    var xreturn = 0;
                    foreach (var prod in forms)
                    {
                        if (xDelete(prod,showmessage))
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
            return Replace(oldform.ProductieNr, newform, showmessage);
        }

        public Task<bool> Replace(string id, ProductieFormulier newform, bool showmessage = true, bool onlylocal = false)
        {
            return Task.Factory.StartNew(() =>
            {
                if (IsDisposed || ProductieFormulieren == null || id == null || newform == null)
                    return false;
                try
                {
                    if (xDeleteProductie(id, showmessage))
                        return xUpSert(newform.ProductieNr, newform,null, showmessage, onlylocal);
                    return false;
                }
                catch
                {
                    return false;
                }
            });
        }

        public bool Exist(ProductieFormulier form)
        {
            if (IsDisposed || form == null)
                return false;
            try
            {
                return ProductieExist(form.ProductieNr);
            }
            catch
            {
                return false;
            }
        }

        public bool ProductieExist(string id)
        {
            if (IsDisposed || id == null)
                return false;
            try
            {
                return (ProductieFormulieren != null &&
                       ProductieFormulieren.Exists(id)) || (GereedFormulieren != null && GereedFormulieren.Exists(id));
            }
            catch
            {
                return false;
            }
        }

        #endregion ProductieFormulieren

        #region UserAccounts

        public Task<UserAccount> GetAccount(string username)
        {
            return Task.Factory.StartNew(() =>
            {
                if (IsDisposed || username == null)
                    return null;
                try
                {
                    return UserAccounts?.FindOne(username, false);
                }
                catch
                {
                    return null;
                }
            });
        }

        public UserAccount xGetAccount(string username)
        {
            if (IsDisposed || username == null)
                return null;
            try
            {
                return UserAccounts?.FindOne(username, false);
            }
            catch
            {
                return null;
            }
        }

        public Task<List<UserAccount>> GetAllAccounts()
        {
            return Task.Factory.StartNew(() =>
            {
                return xGetAllAccounts();
            });
        }

        public List<UserAccount> xGetAllAccounts()
        {
            if (IsDisposed || UserAccounts == null)
                return new List<UserAccount>();
            return UserAccounts.FindAll(false);
        }

        public Task<bool> UpSert(UserAccount account, bool showmessage = true, bool onlylocal = false)
        {
            return UpSert(account.Username, account, "Gebruiker Account Update", showmessage,onlylocal);
        }

        public bool xUpSert(UserAccount account, bool showmessage = true, bool onlylocal = false)
        {
            return xUpSert(account.Username, account, "Gebruiker Account Update", showmessage, onlylocal);
        }

        public Task<bool> UpSert(UserAccount account, string change, bool showmessage = true, bool onlylocal = false)
        {
            return UpSert(account.Username, account, change, showmessage);
        }

        public Task<bool> UpSert(string id, UserAccount account, string change, bool showmessage = true, bool onlylocal = false)
        {
            return Task.Factory.StartNew(() =>
            {
                if (IsDisposed || UserAccounts == null || id == null || account == null)
                    return false;
                try
                {
                    return xUpSert(id, account, change, showmessage, onlylocal);
                }
                catch
                {
                    return false;
                }
            });
        }

        public bool xUpSert(string id, UserAccount account, string change, bool showmessage = true, bool onlylocal = false)
        {
            if (IsDisposed || UserAccounts == null || id == null || account == null)
                return false;
            try
            {
                account.LastChanged = account.LastChanged.UpdateChange(change, DbType.Accounts);
                _ = UpdateChange(account.LastChanged, DbType.Accounts, showmessage);
                UserAccounts.Upsert(id, account, onlylocal, change);
                Manager.AccountChanged(this, account);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<int> UpSert(UserAccount[] accounts, string change, bool showmessage = true, bool onlylocal = false)
        {
            return Task.Factory.StartNew(() =>
            {
                if (IsDisposed || UserAccounts == null || accounts == null)
                    return -1;
                try
                {
                    var count = 0;
                    foreach (var account in accounts)
                        if (xUpSert(account.Username, account, change, showmessage,onlylocal))
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
            return DeleteAccount(account.Username, showmessage);
        }

        public Task<int> Delete(UserAccount[] accounts, bool showmessage = true)
        {
            return Task.Factory.StartNew(() =>
            {
                if (IsDisposed || UserAccounts == null || accounts == null)
                    return -1;
                try
                {
                    var xreturn = 0;

                    foreach (var v in accounts)
                        if (UserAccounts.Delete(v.Username))
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
                        UpdateChange(changed, DbType.Accounts, showmessage);
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
            return Task.Factory.StartNew(() =>
            {
                return xDeleteAccount(id, showmessage);
            });
        }

        public bool xDeleteAccount(string id, bool showmessage = true)
        {
            if (IsDisposed || UserAccounts == null || id == null)
                return false;
            try
            {
                var pers = UserAccounts.FindOne(id, false);
                if (pers != null && UserAccounts.Delete(id))
                {
                    var changed = new UserChange
                    {
                        Change = $"[{id}] Account Verwijderd",
                        IsRemoved = true,
                        PcId = OwnerId,
                        User = Manager.Opties == null ? "Default" : Manager.Opties.Username
                    };
                    UpdateChange(changed, DbType.Accounts, showmessage);
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> Replace(UserAccount oldaccount, UserAccount newaccount, bool showmessage = true)
        {
            return Replace(oldaccount.Username, newaccount, showmessage);
        }

        public Task<bool> Replace(string id, UserAccount newaccount, bool showmessage = true)
        {
            return Task.Factory.StartNew(() =>
            {
                if (IsDisposed || UserAccounts == null || id == null || newaccount == null)
                    return false;
                try
                {
                    if (xDeleteAccount(id))
                        return xUpSert(newaccount);
                    return false;
                }
                catch
                {
                    return false;
                }
            });
        }

        public bool Exist(UserAccount account)
        {
            if (IsDisposed || UserAccounts == null || account == null)
                return false;
            try
            {
                return UserAccounts.Exists(account.Username);
            }
            catch
            {
                return false;
            }
        }

        public bool AccountExist(string id)
        {
            if (IsDisposed || UserAccounts == null || id == null)
                return false;
            try
            {
                return UserAccounts.Exists(id);
            }
            catch
            {
                return false;
            }
        }

        #endregion UserAccounts

        #region Usersettings

        public UserSettings GetSetting(string username)
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
            return Task.Factory.StartNew(() =>
            {
                if (IsDisposed || AllSettings == null)
                    return new List<UserSettings>();
                return AllSettings.FindAll(false);
            });
        }

        public List<UserSettings> xGetAllSettings()
        {
            if (IsDisposed || AllSettings == null)
                return new List<UserSettings>();
            return AllSettings.FindAll(false);
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

        public Task<int> UpSert(UserSettings[] accounts, string change, bool showmessage = true)
        {
            return Task.Factory.StartNew(() =>
            {
                if (IsDisposed || AllSettings == null || accounts == null)
                    return -1;
                try
                {
                    if (accounts.Length > 0)
                    {
                        var count = 0;
                        foreach (var account in accounts)
                            if (xUpSert(account.Username, account, change, showmessage))
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

        public Task<bool> UpSert(string id, UserSettings account, string change, bool showmessage = true, bool onlylocal = false)
        {
            return Task.Factory.StartNew(() =>
            {
                return xUpSert(id, account, change, showmessage, onlylocal);
            });
        }

        public bool xUpSert(string id, UserSettings account, string change, bool showmessage = true, bool onlylocal = false)
        {
            if (IsDisposed || AllSettings == null || id == null || account == null)
                return false;
            try
            {
                account.LastChanged = account.LastChanged.UpdateChange(change, DbType.Opties);
                UpdateChange(account.LastChanged, DbType.Opties, showmessage);
                if (AllSettings != null)
                    AllSettings.Upsert(id, account, onlylocal, change);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> Delete(UserSettings account, bool showmessage = true)
        {
            string id = account.Username != null ? account.Username.ToLower().StartsWith("default") ? $"Default[{account.SystemID}]" : account.Username : null;
            return DeleteSettings(id, showmessage);
        }

        public Task<int> Delete(UserSettings[] settings, bool showmessage = true)
        {
            return Task.Factory.StartNew(() =>
            {
                if (IsDisposed || AllSettings == null || settings == null)
                    return -1;
                try
                {
                    var xreturn = 0;

                    foreach (var v in settings)
                        if (xDeleteSettings(v.Username, showmessage))
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
            return Task.Factory.StartNew(() =>
            {
               return xDeleteSettings(id, showmessage);
            });
        }

        public bool xDeleteSettings(string id, bool showmessage = true)
        {
            if (IsDisposed || AllSettings == null || id == null)
                return false;
            try
            {
                var xset = AllSettings.FindOne(id, false);
                if (xset != null && AllSettings.Delete(id))
                {
                    var lastchange = new UserChange().UpdateChange($"[{id}] Optie Verwijderd", DbType.Opties);
                    lastchange.IsRemoved = true;
                    UpdateChange(lastchange, DbType.Opties, showmessage);
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> Replace(UserSettings oldsettings, UserSettings newsettings, bool showmessage = true)
        {
            string id = oldsettings.Username != null
                  ? oldsettings.Username.ToLower().StartsWith("default") ? $"Default[{oldsettings.SystemID}]" :
                  oldsettings.Username
                  : null;
            return Replace(id, newsettings, showmessage);
        }

        public Task<bool> Replace(string id, UserSettings newsettings, bool showmessage = true)
        {
            return Task.Factory.StartNew(() =>
            {
                if (IsDisposed || AllSettings == null || id == null || newsettings == null)
                    return false;
                try
                {
                    if (xDeleteSettings(id, showmessage))
                        return xUpSert(newsettings.Username,newsettings,null, showmessage);
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public bool Exist(UserSettings settings)
        {
            if (IsDisposed || AllSettings == null || settings == null)
                return false;
            try
            {
                string id = settings.Username != null
                    ? settings.Username.ToLower().StartsWith("default") ? $"Default[{settings.SystemID}]" :
                    settings.Username
                    : null;
                return AllSettings.Exists(id);
            }
            catch
            {
                return false;
            }
        }

        public bool SettingsExist(string id)
        {
            if (IsDisposed || AllSettings == null || id == null)
                return false;
            try
            {
                return AllSettings.Exists(id);
            }
            catch
            {
                return false;
            }
        }

        #endregion Usersettings

        #region Personeel

        public Task<Personeel> GetPersoneel(string username)
        {
            return Task.Factory.StartNew(() =>
            {
                if (IsDisposed || PersoneelLijst == null || username == null)
                    return null;
                try
                {
                    return PersoneelLijst.FindOne(username, false);
                }
                catch
                {
                    return null;
                }
            });
        }

        public Personeel xGetPersoneel(string username)
        {
            if (IsDisposed || PersoneelLijst == null || username == null)
                return null;
            try
            {
                return PersoneelLijst.FindOne(username, false);
            }
            catch
            {
                return null;
            }
        }

        public Task<List<Personeel>> GetPersoneel(string[] usernames)
        {
            return Task.Factory.StartNew(() =>
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
                        var pers = PersoneelLijst.FindOne(name, false);
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
            return Task.Factory.StartNew(() =>
            {
                return xGetAllPersoneel();
            });
        }

        public List<Personeel> xGetAllPersoneel()
        {
            if (IsDisposed || PersoneelLijst == null)
                return new List<Personeel>();
            return PersoneelLijst.FindAll(true);
        }

        public Task<bool> MaakPersoneelVrijVanWerk(string personeel, bool showmessage = true)
        {
            return Task.Factory.StartNew(() =>
            {
                if (personeel != null && !IsDisposed && PersoneelLijst != null)
                {
                    var xpers = PersoneelLijst.FindOne(personeel, false);
                    if (xpers != null)
                    {
                        xpers.WerktAan = null;
                        return xUpSert(xpers.PersoneelNaam, xpers, "Vrij gemaakt van werk", showmessage);
                    }
                }

                return false;
            });
        }

        public Task<int> MaakPersoneelVrijVanWerk(Personeel[] personen, bool showmessage = true)
        {
            return Task.Factory.StartNew(() =>
            {
                var done = 0;
                if (personen is {Length: > 0} && !IsDisposed && PersoneelLijst != null)
                    foreach (var personeel in personen)
                    {
                        var xpers = PersoneelLijst.FindOne(personeel.PersoneelNaam, false);
                        if (xpers != null)
                        {
                            xpers.WerktAan = null;
                            if (xUpSert(xpers.PersoneelNaam, xpers, "Vrij gemaakt van werk", showmessage))
                                done++;
                        }
                    }

                return done;
            });
        }

        public Task<int> MaakAllePersoneelVrijVanWerk(bool showmessage = true)
        {
            return Task.Factory.StartNew(() =>
            {
                var done = 0;
                if (!IsDisposed && PersoneelLijst != null)
                {
                    var personen = PersoneelLijst.FindAll(false);
                    foreach (var personeel in personen)
                        if (personeel.WerktAan != null)
                        {
                            personeel.WerktAan = null;
                            if (xUpSert(personeel.PersoneelNaam, personeel, "Vrij gemaakt van werk", showmessage))
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
            return Task.Factory.StartNew(() =>
            {
                return xUpSert(id, persoon, change, showmessage, onlylocal);
            });
        }

        public bool xUpSert(string id, Personeel persoon, string change, bool showmessage = true, bool onlylocal = false)
        {
            if (IsDisposed || PersoneelLijst == null || id == null || persoon == null)
                return false;
            try
            {
                persoon.LastChanged = persoon.LastChanged.UpdateChange(change, DbType.Medewerkers);
                UpdateChange(persoon.LastChanged, DbType.Medewerkers,
                    showmessage);
                PersoneelLijst.RaiseEventWhenChanged = !RaiseEventWhenChanged;
                _ = PersoneelLijst.Upsert(id, persoon, onlylocal, change);
                if (RaiseEventWhenChanged)
                    Manager.PersoneelChanged(this, persoon);
                PersoneelLijst.RaiseEventWhenChanged = true;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<int> UpSert(Personeel[] personen, string change, bool showmessage = true)
        {
            return Task.Factory.StartNew(() =>
            {
                if (IsDisposed || PersoneelLijst == null || personen == null)
                    return -1;
                try
                {
                    var count = 0;
                    foreach (var account in personen)
                        if (xUpSert(account.PersoneelNaam, account, change, showmessage))
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
            return DeletePersoneel(persoon.PersoneelNaam, showmessage);
        }

        public Task<int> Delete(Personeel[] personeel, bool showmessage = true)
        {
            return Task.Factory.StartNew(() =>
            {
                if (IsDisposed || PersoneelLijst == null || personeel == null)
                    return -1;
                try
                {
                    var xreturn = 0;

                    foreach (var v in personeel)
                        if (xDeletePersoneel(v.PersoneelNaam, showmessage))
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
            return Task.Factory.StartNew(() =>
            {
                return xDeletePersoneel(id, showmessage);
            });
        }

        public bool xDeletePersoneel(string id, bool showmessage = true)
        {
            if (IsDisposed || PersoneelLijst == null || id == null)
                return false;
            bool xreturn = false;
            try
            {

                var per = PersoneelLijst.FindOne(id, false);
                PersoneelLijst.RaiseEventWhenDeleted = !RaiseEventWhenDeleted;
                if (per != null && PersoneelLijst.Delete(id))
                {
                    var lastchange = new UserChange
                    {
                        Change = $"[{id}] Personeel Verwijderd",
                        IsRemoved = true,
                        PcId = OwnerId,
                        User = Manager.Opties == null ? "Default" : Manager.Opties.Username
                    };
                    UpdateChange(lastchange, DbType.Medewerkers, showmessage);
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
            return Task.Factory.StartNew(() =>
            {
                if (IsDisposed || PersoneelLijst == null || id == null || newpersoon == null)
                    return false;
                try
                {
                    if(xDeletePersoneel(id, showmessage))
                       xUpSert(newpersoon.PersoneelNaam, newpersoon, change?? $"{id} is verplaatst met {newpersoon.PersoneelNaam}", showmessage);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public bool Exist(Personeel persoon)
        {
            if (IsDisposed || PersoneelLijst == null || persoon == null)
                return false;
            try
            {
                return PersoneelLijst.Exists(persoon.PersoneelNaam);
            }
            catch
            {
                return false;
            }
        }

        public bool PersoneelExist(string id)
        {
            if (IsDisposed || PersoneelLijst == null || id == null)
                return false;
            try
            {
                return PersoneelLijst.Exists(id);
            }
            catch
            {
                return false;
            }
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
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    if (NotificationEnabled && showmessage && !string.IsNullOrEmpty(change?.Change))
                        Manager.RemoteMessage(change.CreateMessage(dbname));
                    if (LoggerEnabled && showmessage && !string.IsNullOrEmpty(change?.Change)) 
                        AddLog(change.Change, MsgType.Info);
                    // PManager.RemoteMessage(new Mailing.RemoteMessage(change.Change, MessageAction.None, MsgType.Info));
                }
                catch
                {
                    // ignored
                }
            });
        }


        public bool AddLog(string message, MsgType type)
        {
            if (Logger != null && !IsDisposed)
                try
                {
                    var ent = new LogEntry(message.Replace("\n", " "), type);
                    return Logger.Upsert(ent.Id.ToString(), ent, false, null);
                }
                catch (Exception)
                {
                    return false;
                }

            return false;
        }

        public Task<List<LogEntry>> GetLogs(DateTime from, DateTime to, IsValidHandler validhandler = null)
        {
            return Task.Factory.StartNew(() =>
            {
                var logs = new List<LogEntry>();
                if (Logger != null && !IsDisposed)
                    try
                    {
                        var found = Logger.FindAll(new TijdEntry(from, to),validhandler,true);
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

        public int RemoveFromCollection(string collection,string[] items)
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

            return 0;
        }

        public static string GetDbName(DbType type)
        {
            switch (type)
            {
                case DbType.Producties:
                    return "SqlDatabase";
                case DbType.Opmerkingen:
                    return "Opmerkingen";
                case DbType.Medewerkers:
                    return "PersoneelDb";
                case DbType.GereedProducties:
                    return "GereedDb";
                case DbType.Opties:
                    return "SettingDb";
                case DbType.Accounts:
                    return "AccountsDb";
                case DbType.Logs:
                    return "LogDb";
                case DbType.Versions:
                    return "VersionDb";
                case DbType.Messages:
                    return "Chat";
                case DbType.Klachten:
                    return "Klachten";
                case DbType.Verpakkingen:
                    return "Verpakking";
                case DbType.ArtikelRecords:
                    return "ArtikelRecords";
                case DbType.SpoorOverzicht:
                    return "Sporen";
                case DbType.LijstLayouts:
                    return "LijstLayouts";
                case DbType.MeldingCenter:
                    return "Meldingen";
                case DbType.Bijlages:
                    return "Bijlages";
                case DbType.ProductieFormulieren:
                    return "Productie Formulieren";
                case DbType.Alles:
                    return "RPM_Data";
            }
            return "";
        }

        public static DbType GetDbType(string dbname)
        {
            if (string.IsNullOrEmpty(dbname)) return DbType.Geen;
           switch(dbname.ToLower())
            {
                case "sqldatabase":
                    return DbType.Producties;
                case "opmerkingen":
                    return DbType.Opmerkingen;
                case "personeeldb":
                    return DbType.Medewerkers;
                case "gereeddb":
                    return DbType.GereedProducties;
                case "settingdb":
                    return DbType.Opties;
                case "accountsdb":
                    return DbType.Accounts;
                case "logdb":
                    return DbType.Logs;
                case "versiondb":
                    return DbType.Versions;
                case "chat":
                    return DbType.Messages;
                case "klachten":
                    return DbType.Klachten;
                case "verpakking":
                    return DbType.Verpakkingen;
                case "artikelrecords":
                    return DbType.ArtikelRecords;
                case "sporen":
                    return DbType.SpoorOverzicht;
                case "lijstlayouts":
                    return DbType.LijstLayouts;
                case "meldingen":
                    return DbType.MeldingCenter;
                case "bijlages":
                    return DbType.Bijlages;
                case "productie formulieren":
                    return DbType.ProductieFormulieren;
                case "rpm_data":
                    return DbType.Alles;
            }

            return DbType.Geen;
        }

        public void LoadMultiFiles()
        {
            //var bewerkingentriesdb = "BewerkingLijst";
            ProductieFormulieren = new DatabaseInstance<ProductieFormulier>(DbInstanceType.MultipleFiles, DbType.Producties, RootPath,
                GetDbName(DbType.Producties), true);
            ProductieFormulieren.InstanceChanged += ProductieFormulieren_InstanceChanged;
            ProductieFormulieren.InstanceDeleted += ProductieFormulieren_InstanceDeleted;

            PersoneelLijst =
                new DatabaseInstance<Personeel>(DbInstanceType.MultipleFiles, DbType.Medewerkers, RootPath, GetDbName(DbType.Medewerkers), true);
            PersoneelLijst.InstanceChanged += PersoneelLijst_InstanceChanged;
            PersoneelLijst.InstanceDeleted += PersoneelLijst_InstanceDeleted;

            UserAccounts = new DatabaseInstance<UserAccount>(DbInstanceType.MultipleFiles, DbType.Accounts, RootPath, GetDbName(DbType.Accounts), true);
            UserAccounts.InstanceChanged += UserAccounts_InstanceChanged;
            UserAccounts.InstanceDeleted += UserAccounts_InstanceDeleted;

            AllSettings = new DatabaseInstance<UserSettings>(DbInstanceType.MultipleFiles, DbType.Opties, RootPath, GetDbName(DbType.Opties), true);
            AllSettings.InstanceChanged += AllSettings_InstanceChanged;
            AllSettings.InstanceDeleted += AllSettings_InstanceDeleted;

            Logger = new DatabaseInstance<LogEntry>(DbInstanceType.MultipleFiles, DbType.Logs, RootPath, GetDbName(DbType.Logs), false);
            // ChangeLog = new DatabaseInstance<UserChange>(DbInstanceType.MultipleFiles, RootPath, changeddb,
            // "ChangeLog");
            GereedFormulieren = new DatabaseInstance<ProductieFormulier>(DbInstanceType.MultipleFiles, DbType.GereedProducties, RootPath,
                GetDbName(DbType.GereedProducties), true);
            GereedFormulieren.InstanceChanged += ProductieFormulieren_InstanceChanged;
            GereedFormulieren.InstanceDeleted += ProductieFormulieren_InstanceDeleted;

            DbVersions = new DatabaseInstance<DbVersion>(DbInstanceType.MultipleFiles, DbType.Versions, RootPath, GetDbName(DbType.Versions), false);
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
                    var xset = AllSettings.FindOne(id, false);
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
            try
            {
                if (Manager.LogedInGebruiker == null) return;
                var id = Path.GetFileNameWithoutExtension(y.FullPath);
                if (string.Equals(Manager.LogedInGebruiker.Username, id, StringComparison.CurrentCultureIgnoreCase))
                {
                    Manager.LogOut(this,true);
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }

        private void UserAccounts_InstanceChanged(object sender, FileSystemEventArgs y)
        {
            try
            {
                if (Manager.LogedInGebruiker == null) return;
                var id = Path.GetFileNameWithoutExtension(y.FullPath);
                var acc = UserAccounts.FindOne(id, false);
                if (string.Equals(Manager.LogedInGebruiker.Username, id, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (acc == null)
                        Manager.LogOut(this, true);
                    else
                    {
                        Manager.LogedInGebruiker = acc;
                        Manager.LoginChanged(acc,false, true);
                    }
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
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
                var pers = PersoneelLijst.FromPath<Personeel>(y.FullPath);
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
                if(Manager.Database != null && !Manager.Database.ProductieExist(id))
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
                        prod = MultipleFileDb.xFromPath<ProductieFormulier>(e.FullPath, false);

                        if (prod != null && prod.IsAllowed(null))
                            Manager.FormulierChanged(this, prod);
                    });
                }
                catch (Exception x)
                {
                    Console.WriteLine(x);
                }
            //}
        }
        #endregion

        public int Count(DbType type)
        {
            if (IsDisposed)
                return 0;
            switch (type)
            {
                case DbType.Producties:
                    if (ProductieFormulieren == null)
                        return 0;
                    return ProductieFormulieren.Count();
                case DbType.Medewerkers:
                    if (PersoneelLijst == null)
                        return 0;
                    return PersoneelLijst.Count();
                case DbType.GereedProducties:
                    if (GereedFormulieren == null)
                        return 0;
                    return GereedFormulieren.Count();
                case DbType.Opties:
                    if (AllSettings == null)
                        return 0;
                    return AllSettings.Count();
                case DbType.Accounts:
                    if (UserAccounts == null)
                        return 0;
                    return UserAccounts.Count();
                case DbType.Logs:
                    if (Logger == null)
                        return 0;
                    return Logger.Count();
                case DbType.Versions:
                    if (DbVersions == null)
                        return 0;
                    return DbVersions.Count();
                case DbType.Messages:
                    return 0;
                case DbType.Alles:
                    var count = 0;
                    var types = (DbType[])Enum.GetValues(typeof(DbType));
                    foreach (var t in types)
                    {
                        if (t == DbType.Alles || t == DbType.Geen)
                            continue;
                        count += Count(t);
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
            return Task.Factory.StartNew(() =>
            {
                return xUpdateDbFromDb(dbentry, token, changed);
            }, token.Token);
        }

        public int xUpdateDbFromDb(DatabaseUpdateEntry dbentry, CancellationTokenSource token,
    ProgressChangedHandler changed = null)
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
                        var xs = database.AllSettings.FindAll(new TijdEntry(dbentry.LastUpdated, DateTime.MaxValue), null, false);
                        if (xs is { Count: > 0 })
                        {
                            max = xs.Count;
                            count = 0;
                            foreach (var s in xs)
                            {
                                token.Token.ThrowIfCancellationRequested();
                                var myitem = GetSetting($"{s.Username}[{s.SystemID}]");
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
                        var xs1 = database.UserAccounts.FindAll(new TijdEntry(dbentry.LastUpdated, DateTime.MaxValue), null, false);
                        if (xs1 is { Count: > 0 })
                        {
                            //count = 0;
                            max += xs1.Count;
                            foreach (var s in xs1)
                            {
                                token.Token.ThrowIfCancellationRequested();
                                var myitem = xGetAccount(s.Username);
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
                        var xs2 = database.PersoneelLijst.FindAll(new TijdEntry(dbentry.LastUpdated, DateTime.MaxValue), null, false);
                        if (xs2 is { Count: > 0 })
                        {
                            // count = 0;
                            max += xs2.Count;
                            foreach (var s in xs2)
                            {
                                token.Token.ThrowIfCancellationRequested();
                                var myitem = xGetPersoneel(s.PersoneelNaam);
                                if (myitem != null)
                                {
                                    updated += myitem.UpdateFrom(s, true);
                                }
                                else
                                {
                                    xUpSert(s.PersoneelNaam, s,
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

                        var xs3 = database.ProductieFormulieren?.FindAll(new TijdEntry(dbentry.LastUpdated, DateTime.MaxValue), null, false);
                        if (xs3 is { Count: > 0 })
                        {
                            max += xs3.Count;
                            foreach (var s in xs3)
                            {
                                token.Token.ThrowIfCancellationRequested();
                                var myitem = Manager.Database.GetProductie(s.ProductieNr, true);
                                if (myitem != null)
                                {
                                    if (myitem.xUpdateFrom(s,
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
                        var xs3 = database.GereedFormulieren?.FindAll(new TijdEntry(dbentry.LastUpdated, DateTime.MaxValue), null, false);
                        if (xs3 is { Count: > 0 })
                        {
                            max += xs3.Count;
                            foreach (var s in xs3)
                            {
                                token.Token.ThrowIfCancellationRequested();
                                var myitem = Manager.Database.GetProductie(s.ProductieNr, true);
                                if (myitem != null)
                                {
                                    if (myitem.xUpdateFrom(s,
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
            catch
            {
                // ignored
            }

            RaiseEventWhenChanged = true;
            RaiseEventWhenDeleted = true;
            NotificationEnabled = oldnotif;
            LoggerEnabled = oldlogger;
            return updated;
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