using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.xmp.impl;
using Microsoft.Win32.SafeHandles;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

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
        public List<ProductieFormulier> Producties { get; private set; } = new List<ProductieFormulier>();
        public LoadedType Type { get; private set; }
        public bool IsLoaded { get; private set; }
        public bool IsLoadedSyncing { get; private set; }
        public bool IsProductiesSyncing { get; private set; }
        public int SyncInterval { get; set; } = 20000; //10 seconden
        public List<IProductieBase> ExcludeProducties { get; set; } = new List<IProductieBase>();
        public ProductieProvider()
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted += Manager_OnFormulierDeleted;
        }

        private void Manager_OnFormulierDeleted(object sender, string id)
        {
            if (!IsLoaded || Producties.Count == 0) return;
            int removed = Producties.RemoveAll(x => string.Equals(x.ProductieNr, id, StringComparison.CurrentCultureIgnoreCase));
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
           UpdateProductie(changedform,false);
        }

        public void UpdateProductie(ProductieFormulier productie, bool raisechangedevent)
        {
            lock (_locker)
            {
                if (!IsLoaded || productie == null) return;
               
                if (productie.State == ProductieState.Gereed && Type == LoadedType.Producties)
                {
                    int removed = Producties.RemoveAll(x =>
                        string.Equals(x.ProductieNr, productie.ProductieNr, StringComparison.CurrentCultureIgnoreCase));
                    if (removed > 0)
                        Manager.FormulierDeleted(this, productie.ProductieNr);
                    return;
                }

                var index = Producties.IndexOf(productie);
                if (index > -1)
                    Producties[index] = productie;
                else
                    Producties.Add(productie);
                if (raisechangedevent)
                    Manager.FormulierChanged(this, productie);
            }
        }

        public void StartSyncLoadedProducties()
        {
            if (IsLoadedSyncing) return;
            IsLoadedSyncing = true;
            Task.Run(async () =>
            {
                while (IsLoadedSyncing && Producties.Count > 0)
                {
                    try
                    {
                        for (int i = 0; i < Producties.Count; i++)
                        {
                            if (!IsLoadedSyncing) break;
                            var prod = Producties[i];
                            if (prod == null || string.IsNullOrEmpty(prod.ProductieNr))
                            {
                                Producties.RemoveAt(i--);
                                continue;
                            }

                            string id = prod.ProductieNr;
                            prod = await Manager.Database.GetProductie(id);
                            if (prod == null) continue;
                            if (string.IsNullOrEmpty(prod.ProductieNr) ||
                                (prod.State == ProductieState.Gereed && Type == LoadedType.Producties))
                            {
                                Producties.RemoveAt(i--);
                                Manager.FormulierDeleted(this, id);
                                continue;
                            }

                            var index = Producties.IndexOf(prod);
                            if (index > -1)
                                Producties[index] = prod;
                            else
                                Producties.Add(prod);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    await Task.Delay(SyncInterval);
                }

                IsLoadedSyncing = false;
            });
        }

        public void StartSyncProducties()
        {
            if (IsProductiesSyncing) return;
            IsProductiesSyncing = true;
            Task.Run(async () =>
            {
                while (IsProductiesSyncing)
                {
                    UpdateProducties();
                    await Task.Delay(SyncInterval);
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
        public void UpdateProducties()
        {
            if (_isupdating) return;
            _isupdating = true;
            Task.Run(async () =>
            {
                try
                {
                    var forms = await Manager.GetAllProductieIDs(false);
                    for (int i = 0; i < forms.Count; i++)
                    {
                        if (!IsProductiesSyncing) break;
                        var prod = await Manager.Database.GetProductie(forms[i]);
                        if (prod == null || !prod.IsAllowed(null) || IsExcluded(prod)) 
                            continue;
                        if (prod.State == ProductieState.Verwijderd ||
                            prod.State == ProductieState.Gereed)
                            continue;
                        bool invoke = true;
                        if (Manager.Opties.GebruikLocalSync || Manager.Opties.GebruikTaken)
                        {
                            //opslaan als de productie voor het laatst is gestart door de huidige gebruiker.
                            bool save = prod.Bewerkingen != null && prod.Bewerkingen.Any(x =>
                                string.Equals(x.GestartDoor, Manager.Opties.Username,
                                    StringComparison.CurrentCultureIgnoreCase));
                            invoke = false;
                            await prod.UpdateForm(true, false, null, "", false, false);
                        }
                        UpdateProductie(prod, invoke);
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
            IsLoadedSyncing = false;
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
        public async Task<List<ProductieFormulier>> GetProducties(LoadedType type, ViewState[] states, bool filter, bool incform, bool loaddb)
        {
            if (Type == type && IsLoaded)
                return Producties;
            var prods = new List<ProductieFormulier>();
            if (IsLoaded && Type == LoadedType.Alles && (type == LoadedType.Gereed || (type == LoadedType.Producties && states.Any(x=> x == ViewState.Gereed))))
            {
                
                for (int i = 0; i < Producties.Count; i++)
                {
                    var prod = Producties[i];
                    if (type == LoadedType.Producties && prod.State == ProductieState.Gereed) continue;
                    if (filter && !prod.IsAllowed(null, states, incform)) continue;
                    prods.Add(prod);
                }
            }
            else
            {
                prods = await GetProducties(type, filter, loaddb);
            }
            
            
            return prods;
        }

        public async Task<List<Bewerking>> GetBewerkingen(LoadedType type, ViewState[] states, bool filter, bool loaddb)
        {
            List<Bewerking> bws;

            if (IsLoaded && Type == LoadedType.Alles && (type == LoadedType.Gereed || (type == LoadedType.Producties && states.Any(x => x == ViewState.Gereed))))
            {
                bws = GetBewerkingen(Producties, type, states, filter);
            }
            else
            {
                var prods = await GetProducties(type, false, loaddb);
                bws = GetBewerkingen(prods, type, states, filter);
            }


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

        private async Task<List<ProductieFormulier>> GetProducties(LoadedType type, bool filter, bool loaddb)
        {
            var prods = new List<ProductieFormulier>();
            if (loaddb)
                Manager.DbBeginUpdate();
            try
            {
                if (!IsLoaded || type != Type)
                {
                    switch (type)
                    {
                        case LoadedType.Alles:
                            //prods = await Manager.Database.GetAllProducties(true, filter,null);
                           // break;
                        case LoadedType.Gereed:
                          //  IsValidHandler validhandler = filter ? Functions.IsAllowed : null;
                            prods = await Manager.Database.GetAllProducties(true,filter, null);
                            break;
                        case LoadedType.Producties:
                            prods = await Manager.Database.GetAllProducties(false, filter,null);
                            break;
                        case LoadedType.None:
                            prods = new List<ProductieFormulier>();
                            break;
                    }

                    if (loaddb)
                    {
                        Type = type;
                        Producties = prods;
                        IsLoaded = true;
                        StartSyncLoadedProducties();
                    }
                }
                else prods = Producties;

            }
            catch
            {
            }

            if (loaddb)
                Manager.DbEndUpdate();
            return prods;
        }

        #region Disposing

        private bool _disposed;

        // Instantiate a SafeHandle instance.
        private readonly SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted -= Manager_OnFormulierDeleted;
            IsLoaded = false;
            Type = LoadedType.None;
            Producties.Clear();
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
