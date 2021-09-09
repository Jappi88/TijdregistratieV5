using FolderSync;
using Microsoft.Win32.SafeHandles;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Rpm.SqlLite;

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

        private static readonly object _locker = new object();
        public bool IsProductiesSyncing { get; private set; }
        public List<IProductieBase> ExcludeProducties { get; set; } = new List<IProductieBase>();
        public static FolderSynchronization FolderSynchronization { get; private set; } = new FolderSynchronization();
       // public static Synchronisation MicSync { get; private set; } = new Synchronisation();

        public void StartSyncProducties()
        {
            if (IsProductiesSyncing) return;
            IsProductiesSyncing = true;
            Task.Run(async () =>
            {
                while (IsProductiesSyncing)
                {
                    if (Manager.Opties.GebruikLocalSync || Manager.Opties.GebruikTaken)
                        UpdateProducties();
                    await Task.Delay(Manager.Opties.SyncInterval);
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

        private bool _isupdating = false;

        public void InitOfflineDb()
        {
            if (Manager.DefaultSettings.GebruikOfflineMetSync && !string.Equals(Manager.DbPath,
                    Manager.DefaultSettings.TempMainDB.UpdatePath, StringComparison.CurrentCultureIgnoreCase) &&
                Directory.Exists(Manager.DefaultSettings.TempMainDB.UpdatePath))
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
            FolderSynchronization?.Stop();
        }

        public void SyncProducties()
        {
            if (Manager.DefaultSettings is not {GebruikOfflineMetSync: true}) return;

            if (Manager.DefaultSettings?.MainDB == null || Manager.DefaultSettings.TempMainDB == null) return;
            try
            {
                if (Manager.DefaultSettings.GebruikOfflineMetSync &&
                    !string.IsNullOrEmpty(Manager.DefaultSettings.MainDB.UpdatePath) &&
                    !string.IsNullOrEmpty(Manager.DefaultSettings.TempMainDB.UpdatePath) &&
                    !string.Equals(Manager.DefaultSettings.TempMainDB.UpdatePath,
                        Manager.DefaultSettings.MainDB.UpdatePath, StringComparison.CurrentCultureIgnoreCase) &&
                    Directory.Exists(Manager.DefaultSettings.MainDB.UpdatePath) &&
                    Directory.Exists(Manager.DefaultSettings.TempMainDB.UpdatePath))
                {
                    FolderSynchronization?.Stop();
                    if (Manager.DefaultSettings.OfflineDabaseTypes.Count == 0) return;
                    foreach (var xkey in Manager.DefaultSettings.OfflineDabaseTypes)
                    {
                        string localproductiepath = string.Empty;
                        string remoteproductiepath = string.Empty;
                        switch (xkey)
                        {
                            case DbType.Producties:
                                localproductiepath = Manager.DefaultSettings.TempMainDB.UpdatePath + $"\\SqlDatabase";
                                remoteproductiepath = Manager.DefaultSettings.MainDB.UpdatePath + $"\\SqlDatabase";
                                Manager.Database?.ProductieFormulieren?.MultiFiles?.SetSecondaryPath(
                                    localproductiepath, new SecondaryManageType[]
                                    {
                                        SecondaryManageType.Write,
                                        SecondaryManageType.Read
                                    });
                                break;
                            case DbType.Medewerkers:
                                localproductiepath = Manager.DefaultSettings.TempMainDB.UpdatePath + $"\\PersoneelDb";
                                remoteproductiepath = Manager.DefaultSettings.MainDB.UpdatePath + $"\\PersoneelDb";
                                Manager.Database?.PersoneelLijst?.MultiFiles?.SetSecondaryPath(
                                    localproductiepath, new SecondaryManageType[]
                                    {
                                        SecondaryManageType.Write,
                                        SecondaryManageType.Read
                                    });
                                break;
                            case DbType.GereedProducties:
                                localproductiepath = Manager.DefaultSettings.TempMainDB.UpdatePath + $"\\GereedDb";
                                remoteproductiepath = Manager.DefaultSettings.MainDB.UpdatePath + $"\\GereedDb";
                                Manager.Database?.GereedFormulieren?.MultiFiles?.SetSecondaryPath(
                                    localproductiepath, new SecondaryManageType[]
                                    {
                                        SecondaryManageType.Write,
                                        SecondaryManageType.Read
                                    });
                                break;
                            case DbType.Opties:
                                localproductiepath = Manager.DefaultSettings.TempMainDB.UpdatePath + $"\\SettingDb";
                                remoteproductiepath = Manager.DefaultSettings.MainDB.UpdatePath + $"\\SettingDb";
                                Manager.Database?.AllSettings?.MultiFiles?.SetSecondaryPath(
                                    localproductiepath, new SecondaryManageType[]
                                    {
                                        SecondaryManageType.Write,
                                        SecondaryManageType.Read
                                    });
                                break;
                            case DbType.Accounts:
                                localproductiepath = Manager.DefaultSettings.TempMainDB.UpdatePath + $"\\AccountsDb";
                                remoteproductiepath = Manager.DefaultSettings.MainDB.UpdatePath + $"\\AccountsDb";
                                Manager.Database?.UserAccounts?.MultiFiles?.SetSecondaryPath(
                                    localproductiepath, new SecondaryManageType[]
                                    {
                                        SecondaryManageType.Write,
                                        SecondaryManageType.Read
                                    });
                                break;
                            case DbType.Messages:
                                localproductiepath = Manager.DefaultSettings.TempMainDB.UpdatePath + $"\\Chat";
                                remoteproductiepath = Manager.DefaultSettings.MainDB.UpdatePath + $"\\Chat";
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
            }
        }

        private Task SyncDirectories(string remotepath, string localpath)
        {
            return Task.Factory.StartNew(() =>
            {
                using var roboCopyProcess = new Process();
                roboCopyProcess.StartInfo.FileName = "robocopy.exe";
                roboCopyProcess.StartInfo.Arguments = remotepath + " " + localpath + " /MIR /mon:1 /mot:0";
                roboCopyProcess.StartInfo.UseShellExecute = false;
                roboCopyProcess.StartInfo.CreateNoWindow = false;
                roboCopyProcess.Start();
                roboCopyProcess.WaitForExit();
                Console.WriteLine(@"Done!");
            });
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
            Task.Run(async () =>
            {
                try
                {
                    if (Manager.Opties.GebruikLocalSync || Manager.Opties.GebruikTaken)
                    {
                        var forms = await Manager.GetAllProductieIDs(false,false);
                        for (int i = 0; i < forms.Count; i++)
                        {
                            if (!IsProductiesSyncing || (!Manager.Opties.GebruikLocalSync && !Manager.Opties.GebruikTaken)) break;
                            var prod = await Manager.Database.GetProductie(forms[i]);
                            if (prod == null || !prod.IsAllowed(null) || IsExcluded(prod))
                                continue;
                            if (prod.State is ProductieState.Verwijderd or ProductieState.Gereed)
                                continue;
                            // bool invoke = true;

                            //opslaan als de productie voor het laatst is gestart door de huidige gebruiker.
                            bool save = prod.Bewerkingen != null && prod.Bewerkingen.Any(x =>
                                string.Equals(x.GestartDoor, Manager.Opties.Username,
                                    StringComparison.CurrentCultureIgnoreCase));
                            await prod.UpdateForm(true, false, null, "",false, false, true);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                _isupdating = false;
            });
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
        public async Task<List<ProductieFormulier>> GetProducties(LoadedType type, ViewState[] states, bool filter, bool incform)
        {
            return await GetProducties(type, filter);
        }

        public async Task<List<Bewerking>> GetBewerkingen(LoadedType type, ViewState[] states, bool filter)
        {
            var prods = await GetProducties(type, false);
            var bws = GetBewerkingen(prods, type, states, filter);
            return bws;
        }

        private List<Bewerking> GetBewerkingen(List<ProductieFormulier>  producties, LoadedType type, ViewState[] states, bool filter)
        {
            var bws = new List<Bewerking>();
            for (int i = 0; i < producties.Count; i++)
            {
                var prod = producties[i];
                if (type == LoadedType.Producties && prod.State == ProductieState.Gereed) continue;
                if (prod?.Bewerkingen == null || prod.Bewerkingen.Length == 0) continue;
                foreach (var bw in prod.Bewerkingen)
                {
                    if (filter && (!states.Any(x => bw.IsValidState(x)) || !bw.IsAllowed())) continue;
                    bws.Add(bw);
                }
            }

            return bws;
        }

        private async Task<List<ProductieFormulier>> GetProducties(LoadedType type, bool filter)
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
                        prods = await Manager.Database.GetAllProducties(true, filter, null);
                        break;
                    case LoadedType.Producties:
                        prods = await Manager.Database.GetAllProducties(false, filter, null);
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
