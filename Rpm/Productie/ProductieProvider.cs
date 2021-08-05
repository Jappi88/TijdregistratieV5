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
        public bool IsProductiesSyncing { get; private set; }
        public List<IProductieBase> ExcludeProducties { get; set; } = new List<IProductieBase>();

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
                        var forms = await Manager.GetAllProductieIDs(false);
                        for (int i = 0; i < forms.Count; i++)
                        {
                            if (!IsProductiesSyncing || (!Manager.Opties.GebruikLocalSync && !Manager.Opties.GebruikTaken)) break;
                            var prod = await Manager.Database.GetProductie(forms[i]);
                            if (prod == null || !prod.IsAllowed(null) || IsExcluded(prod))
                                continue;
                            if (prod.State == ProductieState.Verwijderd ||
                                prod.State == ProductieState.Gereed)
                                continue;
                            // bool invoke = true;

                            //opslaan als de productie voor het laatst is gestart door de huidige gebruiker.
                            bool save = prod.Bewerkingen != null && prod.Bewerkingen.Any(x =>
                                string.Equals(x.GestartDoor, Manager.Opties.Username,
                                    StringComparison.CurrentCultureIgnoreCase));
                            await prod.UpdateForm(true, false, null, "", false, false);
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
