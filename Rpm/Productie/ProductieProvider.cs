using FolderSync;
using Microsoft.Win32.SafeHandles;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.SqlLite;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ProductieManager.Rpm.Productie
{
    public class ProductieProvider:IDisposable
    {
        public enum LoadedType
        {
            Alles,
            Gereed,
            Producties,
            None,
        }

        public bool IsProductiesSyncing { get; private set; }
        public List<IProductieBase> ExcludeProducties { get; set; } = new List<IProductieBase>();
        public static FolderSynchronization FolderSynchronization { get; private set; } = new FolderSynchronization();
       // public static Synchronisation MicSync { get; private set; } = new Synchronisation();
       public string AppRootPath { get; private set; }
       public string SecondaryRootPath { get; private set; }

       public void StartSyncProducties()
       {
           if (IsProductiesSyncing) return;
           IsProductiesSyncing = true;
           Task.Factory.StartNew(() =>
           {
               while (IsProductiesSyncing)
               {
                   if (Manager.Opties == null)
                   {
                       Thread.Sleep(1000);
                       continue;
                   }
                   if (Manager.Opties is {GebruikLocalSync: true} || Manager.Opties.GebruikTaken)
                       UpdateProducties();
                   // await Task.Delay(Manager.Opties.SyncInterval);
                   Thread.Sleep(Manager.Opties.SyncInterval);
               }

               IsProductiesSyncing = false;
           });
       }

       public void AddToExclude(IProductieBase productie)
        {
            if (string.IsNullOrEmpty(productie?.ProductieNr)) return;
            if (ExcludeProducties == null)
                ExcludeProducties = new List<IProductieBase>();
            if (!IsExcluded(productie))
                ExcludeProducties.Add(productie);
        }

        public void RemoveFromExclude(IProductieBase productie)
        {
            if (string.IsNullOrEmpty(productie?.ProductieNr)) return;
            ExcludeProducties?.RemoveAll(x =>
                string.Equals(productie.ProductieNr, x.ProductieNr, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(productie.Naam, x.Naam, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool IsExcluded(IProductieBase productie)
        {
            if (productie == null || string.IsNullOrEmpty(productie.ProductieNr)) return true;
            return ExcludeProducties != null && ExcludeProducties.Any(x => string.Equals(productie.ProductieNr, x.ProductieNr, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(productie.Naam, x.Naam, StringComparison.CurrentCultureIgnoreCase));
        }

        private bool _isupdating;

        public void InitOfflineDb()
        {
            if (Manager.DefaultSettings.GebruikOfflineMetSync && !string.Equals(Manager.AppRootPath,
                    Manager.DefaultSettings.TempMainDB.RootPath, StringComparison.CurrentCultureIgnoreCase) &&
                Directory.Exists(Manager.DefaultSettings.TempMainDB.RootPath))
            {
               
                SyncProducties();
            }
            else DisableOfflineDb();
        }

        public void DisableOfflineDb()
        {
            Manager.Database?.ProductieFormulieren?.MultiFiles?.DisposeSecondayPath();
            Manager.Database?.GereedFormulieren?.MultiFiles?.DisposeSecondayPath();
            Manager.Database?.PersoneelLijst?.MultiFiles?.DisposeSecondayPath();
            Manager.Database?.AllSettings?.MultiFiles?.DisposeSecondayPath();
            Manager.Database?.UserAccounts?.MultiFiles?.DisposeSecondayPath();
            Manager.ArtikelRecords?.Database?.DisposeSecondayPath();
            Manager.Verpakkingen?.Database?.DisposeSecondayPath();
            Manager.SporenBeheer?.Database?.DisposeSecondayPath();
            Manager.Klachten?.Database?.DisposeSecondayPath();
            Manager.ListLayouts?.Database?.DisposeSecondayPath();
            FolderSynchronization?.Stop();
        }

        public void SyncProducties()
        {
            //if (Manager.DefaultSettings is not {GebruikOfflineMetSync: true}) return;
            //if (FolderSynchronization is {Syncing: true}) return;
            if (Manager.DefaultSettings?.MainDB == null || Manager.DefaultSettings.TempMainDB == null) return;
            try
            {
                var path1 = Manager.AppRootPath??string.Empty;
                var path2 = Manager.DefaultSettings.TempMainDB.RootPath;
                var xtmp = Path.Combine(path2, DateTime.Now.Year.ToString());
                if (Path.GetFileName(path1) == Path.GetFileName(xtmp))
                {
                    path2 = xtmp;
                }
                else
                {
                    path2 = Path.Combine(path2, Path.GetFileName(path1));
                }

                path1 = Path.Combine(path1, "RPM_Data");
                path2 = Path.Combine(path2, "RPM_Data");
                AppRootPath = path1;
                SecondaryRootPath = path2;
                if (!Directory.Exists(path2))
                {
                    Directory.CreateDirectory(path2);
                }
                if (Manager.DefaultSettings.GebruikOfflineMetSync &&
                    !string.IsNullOrEmpty(path1) &&
                    !string.IsNullOrEmpty(path2) &&
                    !string.Equals(path1,path2, StringComparison.CurrentCultureIgnoreCase) &&
                    Directory.Exists(path1) &&
                    Directory.Exists(path2))
                {
                    //FolderSynchronization?.Stop();
                    if (Manager.DefaultSettings.OfflineDabaseTypes.Count == 0)
                    {
                        FolderSynchronization?.Stop();
                        return;
                    }

                    FolderSynchronization.ClearScans();
                    foreach (var xkey in Manager.DefaultSettings.OfflineDabaseTypes)
                    {
                        string localproductiepath = string.Empty;
                        string remoteproductiepath = string.Empty;
                        switch (xkey)
                        {
                            case DbType.Producties:
                                localproductiepath = path2 + $"\\SqlDatabase";
                                remoteproductiepath = path1 + $"\\SqlDatabase";
                                Manager.Database?.ProductieFormulieren?.MultiFiles?.SetSecondaryPath(
                                    localproductiepath, new[]
                                    {
                                        SecondaryManageType.Write,
                                        SecondaryManageType.Read
                                    });
                                break;
                            case DbType.Medewerkers:
                                localproductiepath = path2 + $"\\PersoneelDb";
                                remoteproductiepath = path1 + $"\\PersoneelDb";
                                Manager.Database?.PersoneelLijst?.MultiFiles?.SetSecondaryPath(
                                    localproductiepath, new[]
                                    {
                                        SecondaryManageType.Write,
                                        SecondaryManageType.Read
                                    });
                                break;
                            case DbType.GereedProducties:
                                localproductiepath = path2 + $"\\GereedDb";
                                remoteproductiepath = path1 + $"\\GereedDb";
                                Manager.Database?.GereedFormulieren?.MultiFiles?.SetSecondaryPath(
                                    localproductiepath, new[]
                                    {
                                        SecondaryManageType.Write,
                                        SecondaryManageType.Read
                                    });
                                break;
                            case DbType.Opties:
                                localproductiepath = path2 + $"\\SettingDb";
                                remoteproductiepath = path1 + $"\\SettingDb";
                                Manager.Database?.AllSettings?.MultiFiles?.SetSecondaryPath(
                                    localproductiepath, new[]
                                    {
                                        SecondaryManageType.Write,
                                        SecondaryManageType.Read
                                    });
                                break;
                            case DbType.Accounts:
                                localproductiepath = path2 + $"\\AccountsDb";
                                remoteproductiepath = path1 + $"\\AccountsDb";
                                Manager.Database?.UserAccounts?.MultiFiles?.SetSecondaryPath(
                                    localproductiepath, new[]
                                    {
                                        SecondaryManageType.Write,
                                        SecondaryManageType.Read
                                    });
                                break;
                            case DbType.Klachten:
                                localproductiepath = path2 + $"\\Klachten";
                                remoteproductiepath = path1 + $"\\Klachten";
                                Manager.Klachten?.Database?.SetSecondaryPath(
                                    localproductiepath, new[]
                                    {
                                        SecondaryManageType.Write,
                                        SecondaryManageType.Read
                                    });
                                break;
                            case DbType.Verpakkingen:
                                localproductiepath = path2 + $"\\Verpakking";
                                remoteproductiepath = path1 + $"\\Verpakking";
                                Manager.Verpakkingen?.Database?.SetSecondaryPath(
                                    localproductiepath, new[]
                                    {
                                        SecondaryManageType.Write,
                                        SecondaryManageType.Read
                                    });
                                break;
                            case DbType.SpoorOverzicht:
                                localproductiepath = path2 + $"\\Sporen";
                                remoteproductiepath = path1 + $"\\Sporen";
                                Manager.SporenBeheer?.Database?.SetSecondaryPath(
                                    localproductiepath, new[]
                                    {
                                        SecondaryManageType.Write,
                                        SecondaryManageType.Read
                                    });
                                break;
                            case DbType.LijstLayouts:
                                localproductiepath = path2 + $"\\LijstLayouts";
                                remoteproductiepath = path1 + $"\\LijstLayouts";
                                Manager.ListLayouts?.Database?.SetSecondaryPath(
                                    localproductiepath, new[]
                                    {
                                        SecondaryManageType.Write,
                                        SecondaryManageType.Read
                                    });
                                break;
                            case DbType.Messages:
                                localproductiepath = path2 + $"\\Chat";
                                remoteproductiepath = path1 + $"\\Chat";
                                break;
                            case DbType.Opmerkingen:
                                localproductiepath = path2 + $"\\Opmerkingen";
                                remoteproductiepath = path1 + $"\\Opmerkingen";
                                break;
                            case DbType.ArtikelRecords:
                                localproductiepath = path2 + $"\\ArtikelRecords";
                                remoteproductiepath = path1 + $"\\ArtikelRecords";
                                Manager.ArtikelRecords?.Database?.SetSecondaryPath(
                                    localproductiepath, new[]
                                    {
                                        SecondaryManageType.Write,
                                        SecondaryManageType.Read
                                    });
                                break;

                        }

                        if (!string.IsNullOrEmpty(localproductiepath) && !string.IsNullOrEmpty(remoteproductiepath))
                        {
                            if (!Directory.Exists(localproductiepath))
                                Directory.CreateDirectory(localproductiepath);
                            if (!Directory.Exists(remoteproductiepath))
                                Directory.CreateDirectory(remoteproductiepath);
                            var fsync = new FolderSynchronizationScannerItem()
                            {
                                Destination = localproductiepath,
                                Monitor = true,
                                Option = xkey == DbType.Messages? FolderSynchorizationOption.SourceCreate : FolderSynchorizationOption.Destination,
                                Source = remoteproductiepath
                            };
                            FolderSynchronization?.AddScan(fsync);
                        }
                    }

                    FolderSynchronization?.Start();
                }
                else DisableOfflineDb();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                FolderSynchronization?.Stop();
            }
        }
        
        private async Task xSyncDirectories(string remotepath, string localpath)
        {
            var remotedb = await Manager.GetAllProductiePaths(true, false);
            var localdb = await Manager.GetAllProductiePaths(true, true);
            //sync producties die offline zijn.
            foreach (var path in remotedb)
            {
                if (!path.ToLower().StartsWith(remotepath.ToLower())) continue;

                var localfilep = localpath + path.ToLower().Replace(remotepath.ToLower(), "");
                if (File.Exists(localfilep))
                {
                    var fi1 = new FileInfo(path);
                    var fi2 = new FileInfo(localfilep);
                    if (fi1.CreationTime > fi2.CreationTime || fi1.LastWriteTime > fi2.LastWriteTime)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            try
                            {
                                //File.Delete(localfilep);
                                //File.WriteAllBytes(localfilep,File.ReadAllBytes(path));
                                File.Copy(path, localfilep, true);
                                fi2.CreationTime = fi1.CreationTime;
                                fi2.LastWriteTime = fi1.LastWriteTime;
                                break;
                            }
                            catch (Exception e)
                            {
                            }

                            await Task.Delay(100);
                        }

                    }
                    else if (fi1.CreationTime < fi2.CreationTime || fi1.LastWriteTime < fi2.LastWriteTime)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            try
                            {
                                //File.Delete(path);
                                //File.WriteAllBytes(path, File.ReadAllBytes(localfilep));
                                File.Copy(localfilep, path, true);
                                fi1.CreationTime = fi2.CreationTime;
                                fi1.LastWriteTime = fi2.LastWriteTime;
                                break;
                            }
                            catch (Exception e)
                            {
                            }

                            await Task.Delay(100);
                        }

                    }


                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            //File.Delete(localfilep);
                            //File.WriteAllBytes(localfilep, File.ReadAllBytes(path));
                            File.Copy(path, localfilep, true);
                            break;
                        }
                        catch (Exception e)
                        {
                        }

                        await Task.Delay(100);
                    }
                }

                localdb.RemoveAll(x => string.Equals(Path.GetFileName(path), Path.GetFileName(x),
                    StringComparison.CurrentCultureIgnoreCase));
            }

            //sync producties die nog over zijn en online zijn.
            foreach (var path in localdb)
            {
                if (!path.ToLower().StartsWith(localpath.ToLower())) continue;

                var remotefilep = remotepath + path.ToLower().Replace(localpath.ToLower(), "");
                if (File.Exists(remotefilep))
                {
                    var fi1 = new FileInfo(path);
                    var fi2 = new FileInfo(remotefilep);
                    if (fi1.CreationTime > fi2.CreationTime || fi1.LastWriteTime > fi2.LastWriteTime)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            try
                            {
                                //File.Delete(remotefilep);
                                //File.WriteAllBytes(remotefilep, File.ReadAllBytes(path));
                                File.Copy(path, remotefilep, true);
                                fi2.CreationTime = fi1.CreationTime;
                                fi2.LastWriteTime = fi1.LastWriteTime;
                                break;
                            }
                            catch (Exception e)
                            {
                            }

                            await Task.Delay(100);
                        }

                    }
                    else if (fi1.CreationTime < fi2.CreationTime || fi1.LastWriteTime < fi2.LastWriteTime)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            try
                            {
                                //File.Delete(path);
                                //File.WriteAllBytes(path, File.ReadAllBytes(remotefilep));
                                File.Copy(remotefilep, path, true);
                                fi1.CreationTime = fi2.CreationTime;
                                fi1.LastWriteTime = fi2.LastWriteTime;
                                break;
                            }
                            catch (Exception e)
                            {
                            }

                            await Task.Delay(100);
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            //File.Delete(remotefilep);
                            //File.WriteAllBytes(remotefilep, File.ReadAllBytes(path));
                            File.Copy(path, remotefilep, true);
                            break;
                        }
                        catch (Exception e)
                        {
                        }

                        await Task.Delay(100);
                    }
                }
            }

            //sync personeel.
            var remotepers = await Manager.Database.PersoneelLijst.GetAllPaths(false);
            var localpers = await Manager.Database.PersoneelLijst.GetAllPaths(true);
            foreach (var path in remotepers)
            {
                if (!path.ToLower().StartsWith(remotepath.ToLower())) continue;

                var localfilep = localpath + path.ToLower().Replace(remotepath.ToLower(), "");
                if (File.Exists(localfilep))
                {
                    var fi1 = new FileInfo(path);
                    var fi2 = new FileInfo(localfilep);
                    if (fi1.CreationTime > fi2.CreationTime || fi1.LastWriteTime > fi2.LastWriteTime)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            try
                            {
                                //File.Delete(localfilep);
                                //File.WriteAllBytes(localfilep,File.ReadAllBytes(path));
                                File.Copy(path, localfilep, true);
                                var dt = DateTime.Now;
                                fi1.CreationTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
                                fi2.CreationTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
                                break;
                            }
                            catch (Exception e)
                            {
                            }

                            await Task.Delay(100);
                        }

                    }
                    else if (fi1.CreationTime < fi2.CreationTime || fi1.LastWriteTime < fi2.LastWriteTime)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            try
                            {
                                //File.Delete(path);
                                //File.WriteAllBytes(path, File.ReadAllBytes(localfilep));
                                File.Copy(localfilep, path, true);
                                var dt = DateTime.Now;
                                fi1.CreationTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
                                fi2.CreationTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
                                break;
                            }
                            catch (Exception e)
                            {
                            }

                            await Task.Delay(100);
                        }

                    }


                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            //File.Delete(localfilep);
                            //File.WriteAllBytes(localfilep, File.ReadAllBytes(path));
                            File.Copy(path, localfilep, true);
                            break;
                        }
                        catch (Exception e)
                        {
                        }

                        await Task.Delay(100);
                    }
                }

                localpers.RemoveAll(x => string.Equals(Path.GetFileName(path), Path.GetFileName(x),
                    StringComparison.CurrentCultureIgnoreCase));
            }

            //sync personeel die nog over zijn en online zijn.
            foreach (var path in localpers)
            {
                if (!path.ToLower().StartsWith(localpath.ToLower())) continue;

                var remotefilep = remotepath + path.ToLower().Replace(localpath.ToLower(), "");
                if (File.Exists(remotefilep))
                {
                    var fi1 = new FileInfo(path);
                    var fi2 = new FileInfo(remotefilep);
                    if (fi1.CreationTime > fi2.CreationTime || fi1.LastWriteTime > fi2.LastWriteTime)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            try
                            {
                                //File.Delete(remotefilep);
                                //File.WriteAllBytes(remotefilep, File.ReadAllBytes(path));
                                File.Copy(path, remotefilep, true);
                                var dt = DateTime.Now;
                                fi1.CreationTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
                                fi2.CreationTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
                                break;
                            }
                            catch (Exception e)
                            {
                            }

                            await Task.Delay(100);
                        }

                    }
                    else if (fi1.CreationTime < fi2.CreationTime || fi1.LastWriteTime < fi2.LastWriteTime)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            try
                            {
                                //File.Delete(path);
                                //File.WriteAllBytes(path, File.ReadAllBytes(remotefilep));
                                File.Copy(remotefilep, path, true);
                                var dt = DateTime.Now;
                                fi1.CreationTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
                                fi2.CreationTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
                                break;
                            }
                            catch (Exception e)
                            {
                            }

                            await Task.Delay(100);
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            //File.Delete(remotefilep);
                            //File.WriteAllBytes(remotefilep, File.ReadAllBytes(path));
                            File.Copy(path, remotefilep, true);
                            break;
                        }
                        catch (Exception e)
                        {
                        }

                        await Task.Delay(100);
                    }
                }
            }
        }

        public void UpdateProducties()
        {
            if (_isupdating) return;
            _isupdating = true;
            // Task.Run(async () =>
            //{
            try
            {
                if (Manager.LogedInGebruiker != null &&
                    (Manager.Opties.GebruikLocalSync || Manager.Opties.GebruikTaken))
                {
                    var forms = Manager.GetAllProductieIDs(false, false).Result;
                    for (int i = 0; i < forms.Count; i++)
                    {
                        if (Manager.LogedInGebruiker == null || !IsProductiesSyncing ||
                            (!Manager.Opties.GebruikLocalSync && !Manager.Opties.GebruikTaken)) break;
                        var prod = Manager.Database.GetProductie(forms[i], false);
                        if (prod == null || !prod.IsAllowed(null) || IsExcluded(prod))
                            continue;
                        if (prod.State is ProductieState.Verwijderd or ProductieState.Gereed)
                            continue;
                        // bool invoke = true;

                        //opslaan als de productie voor het laatst is gestart door de huidige gebruiker.
                        //bool save = prod.Bewerkingen != null && prod.Bewerkingen.Any(x =>
                        //    string.Equals(x.GestartDoor, Manager.Opties.Username,
                        //        StringComparison.CurrentCultureIgnoreCase));
                        prod.FormulierChanged(this);
                        Manager.FormulierChanged(this, prod);
                        //await prod.UpdateForm(true, false, null, "",false, false, true);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _isupdating = false;
            //});
        }

        public void StopSync()
        {
            IsProductiesSyncing = false;
        }

        /// <summary>
        /// Verkrijg alle producties
        /// </summary>
        /// <param name="type">De type producties die je wilt verkrijgen</param>
        /// <param name="states">Verkrijg producties op basis van een bepaalde status types</param>
        /// <param name="filter">true als je wilt dat de producties worden gefiltered volgens een geldige bewerking</param>
        /// <param name="incform">true als de productie ook die aangegeven status moet zijn, false je alleen wilt verkrijgen op basis van een geldige bewerking</param>
        /// <param name="loaddb">true als je de producties als standaard wilt laden.</param>
        /// <returns></returns>
        public async Task<List<ProductieFormulier>> GetProducties(LoadedType type, ViewState[] states, bool filter, IsValidHandler validhandler, bool checksecondary)
        {
            return await GetProducties(type, filter, validhandler, checksecondary);
        }

        public async Task<List<Bewerking>> GetBewerkingen(LoadedType type, ViewState[] states, bool filter, IsValidHandler validhandler, bool checksecondary)
        {
            var prods = await GetProducties(type, false, validhandler, checksecondary);
            var bws = GetBewerkingen(prods, type, states, filter);
            return bws;
        }

        private List<Bewerking> GetBewerkingen(List<ProductieFormulier>  producties, LoadedType type, ViewState[] states, bool filter)
        {
            return (from prod in producties
                where type != LoadedType.Producties || prod.State != ProductieState.Gereed
                where prod?.Bewerkingen != null && prod.Bewerkingen.Length != 0
                from bw in prod.Bewerkingen
                where !filter || (states.Any(bw.IsValidState) && bw.IsAllowed())
                select bw).ToList();
        }

        private async Task<List<ProductieFormulier>> GetProducties(LoadedType type, bool filter, IsValidHandler validhandler, bool checksecondary)
        {
            var prods = new List<ProductieFormulier>();
            //Manager.DbBeginUpdate();
            try
            {
                switch (type)
                {
                    case LoadedType.Alles:
                    //prods = await Manager.Database.GetAllProducties(true, filter,null);
                    // break;
                    case LoadedType.Gereed:
                        //  IsValidHandler validhandler = filter ? Functions.IsAllowed : null;
                        prods = await Manager.Database.GetAllProducties(true, filter, validhandler, checksecondary);
                        break;
                    case LoadedType.Producties:
                        prods = await Manager.Database.GetAllProducties(false, filter, validhandler, checksecondary);
                        break;
                    case LoadedType.None:
                        prods = new List<ProductieFormulier>();
                        break;
                }

            }
            catch
            {
            }
            //Manager.DbEndUpdate();
            return prods;
        }

        #region Disposing

        private bool _disposed;

        // Instantiate a SafeHandle instance.
        private readonly SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            StopSync();
            FolderSynchronization?.Stop();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
                // Dispose managed state (managed objects).
                _safeHandle?.Dispose();

            _disposed = true;
        }

        #endregion Disposing
    }
}
