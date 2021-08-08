using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Forms;
using MetroFramework.Controls;
using ProductieManager.Forms;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using ProductieManager.Rpm.Productie;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;
using static Forms.RangeCalculatorForm;

namespace Controls
{
    public partial class ProductieListControl : UserControl
    {
        private bool _enableEntryFilter;
        private bool _enableFilter;
        private object _selectedItem;

        public ProductieListControl()
        {
            InitializeComponent();
            xproductieLijst.CustomSorter = delegate (OLVColumn column, SortOrder order) {
                // check which column is about to be sorted and set your custom comparer
                xproductieLijst.ListViewItemSorter = new Comparer(order, column);
            };

            xsearch.ShowClearButton = true;
            EnableEntryFiltering = false;
            EnableFiltering = true;
            CanLoad = true;
        }

        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                ProductieLijst.SelectedObject = value;
                ProductieLijst.SelectedItem?.EnsureVisible();
            }
        }

        // ReSharper disable once ConvertToAutoProperty
        public ObjectListView ProductieLijst => xproductieLijst;
        public bool RemoveCustomItemIfNotValid { get; set; }
        public bool CustomList { get; private set; }
        public List<ProductieFormulier> Producties { get; private set; }
        public List<Bewerking> Bewerkingen { get; private set; }
        public IsValidHandler ValidHandler { get; set; }
        public bool IsBewerkingView { get; set; }
        public bool CanLoad { get; set; }
        public bool IsLoaded { get; private set; }
        public bool EnableEntryFiltering
        {
            get => _enableEntryFilter;
            set
            {
                _enableEntryFilter = value;
                xfiltertoolstrip.Visible = value;
            }
        }
        public bool IsSyncing { get; private set;  }
        public int SyncInterval { get; set; } = 180000; // 3min
        public bool EnableFiltering
        {
            get => _enableFilter;
            set
            {
                _enableFilter = value;
                xfiltercontainer.Visible = value;
            }
        }
        public bool EnableSync { get; set; }
        #region Init Methods

        /// <summary>
        ///     Laad producties
        /// </summary>
        /// <param name="producties">De producties om te laden</param>
        /// <param name="bewerkingen">Laad alleen de bewerkingen</param>
        /// <param name="filter">Filter producties</param>
        public void InitProductie(List<ProductieFormulier> producties, bool bewerkingen,bool initlist, bool loadproducties, bool reload)
        {
            Producties = producties;
            InitProductie(bewerkingen, false, true,initlist, loadproducties, reload);
        }

        public void InitProductie(List<Bewerking> bewerkingen, bool initlist, bool loadproducties, bool reload)
        {
            Bewerkingen = bewerkingen;
            InitProductie(true, false, true,initlist, loadproducties, reload);
        }

        public void InitProductie(bool bewerkingen, bool enablefilter, bool customlist,bool initlist, bool loadproducties, bool reload)
        {
            EnableEntryFiltering = enablefilter;
            CustomList = customlist;
            IsBewerkingView = bewerkingen;
            if (initlist)
            {
                try
                {
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        InitImageList();
                        InitColumns();
                        IsLoaded = true;
                    }));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            if (loadproducties)
                UpdateProductieList(reload);
        }

        private void SetButtonEnable()
        {
            var enable1 = ProductieLijst.SelectedObjects != null && ProductieLijst.SelectedObjects.Count == 1;
            var enable2 = ProductieLijst.SelectedObjects != null && ProductieLijst.SelectedObjects.Count > 1;
            var enable3 = enable1 || enable2;
            var acces1 = Manager.LogedInGebruiker != null &&
                         Manager.LogedInGebruiker.AccesLevel >= AccesType.ProductieBasis;
            //var acces2 = Manager.LogedInGebruiker != null &&
            //Manager.LogedInGebruiker.AccesLevel >= AccesType.ProductieAdvance;

            var bws = new List<Bewerking>();
            if (ProductieLijst.SelectedObjects != null)
            {
                if (!IsBewerkingView)
                    foreach (var prod in ProductieLijst.SelectedObjects.Cast<ProductieFormulier>().ToArray())
                    {
                        if (prod.Bewerkingen == null) continue;
                        var xbws = prod.Bewerkingen.Where(x => x.IsAllowed()).ToList();
                        if (xbws.Count > 0)
                            bws.AddRange(xbws);
                    }
                else bws = ProductieLijst.SelectedObjects.Cast<Bewerking>().ToList();
            }

            bool isprod = !IsBewerkingView;
            var verwijderd1 = enable3 && isprod? bws.All(x => x.State == ProductieState.Verwijderd) : bws.Any(x => x.State == ProductieState.Verwijderd);
            var verwijderd2 = enable3 && bws.Any(x => x.State != ProductieState.Verwijderd);
            var isgereed1 = enable3 && isprod ? bws.All(x => x.State == ProductieState.Gereed) : bws.Any(x => x.State == ProductieState.Gereed);
            var isgereed2 = enable3 && bws.Any(x => x.State != ProductieState.Gereed);
            var isgestart = enable3 && bws.Any(x => x.State == ProductieState.Gestart);
            var isgestopt = enable3 && bws.Any(x => x.State == ProductieState.Gestopt);
            //var nietbemand = bws.Any(x => !x.IsBemand);
            xopenproductieb.Enabled = enable3 && acces1 && verwijderd2;
            xstartb.Enabled = acces1 && isgestopt;
            xstopb.Enabled = acces1 && isgestart;
            xwijzigformb.Enabled = enable1 && acces1;
            xwerktijdenb.Enabled = acces1 && enable3;
            xwerkplekkenb.Enabled = enable1 && acces1;
            xaantalgemaaktb.Enabled = enable1 && acces1;
            xverwijderb.Enabled = enable3 && acces1;
            xzetterugb.Enabled = enable3 && acces1 && (verwijderd1 || isgereed1);
            xmeldgereedb.Enabled = enable1 && acces1 && !verwijderd1 && !isgereed1;
            xdeelgereedmeldingenb.Enabled = enable1 && acces1;
            xonderbrekingb.Enabled = enable1 && acces1;
            xmaterialenb.Enabled = enable1 && acces1;
            xafkeurb.Enabled = enable1 && acces1;
            xproductieInfob.Enabled = enable1;
            xexportexcel.Enabled = enable3;
            xaanbevolenpersb.Enabled = enable1;
            //set context menu
            xopenProductieToolStripMenuItem.Enabled = enable3 && acces1 && verwijderd2;
            xtoolstripstart.Enabled = acces1 && isgestopt;
            xtoolstripstop.Enabled = acces1 && isgestart;
            productieToolStripMenuItem.Enabled = enable1 && acces1;
            xtoolstripbehwerktijden.Enabled = acces1 && enable3;
            xtoolstripbehwerkplekken.Enabled = enable1 && acces1;
            xwijzigToolStripMenuItem1.Enabled = enable3 && acces1;
            xverwijdertoolstrip.Enabled = enable3 && acces1;
            xzetterugtoolstrip.Enabled = enable3 && acces1 && (verwijderd1 || isgereed1);
            xmeldGereedToolStripMenuItem1.Enabled = enable1 && acces1 && !verwijderd1 && !isgereed1;
            xdeelGereedmeldingenToolStripMenuItem.Enabled = enable1 && acces1;
            xonderbrekingtoolstripbutton.Enabled = enable1 && acces1;
            benodigdeMaterialenToolStripMenuItem.Enabled = enable1 && acces1;
            xafkeurstoolstrip.Enabled = enable1 && acces1;
            xshowproductieinfo.Enabled = enable1;
            exportExcelToolStripMenuItem.Enabled = enable3;
            xtoolstripaanbevolenpersonen.Enabled = enable1;
        }

        public void InitEvents()
        {
            Manager.OnSettingsChanged += _manager_OnSettingsChanged;
            Manager.OnFormulierChanged += _manager_OnFormulierChanged;
            Manager.OnFormulierDeleted += Manager_OnFormulierDeleted;
            //Manager.OnProductiesLoaded += Manager_OnProductiesChanged;
            //Manager.OnLoginChanged += _manager_OnLoginChanged;
            //Manager.DbUpdater.DbEntryUpdated += DbUpdater_DbEntryUpdated;
            Manager.OnBewerkingDeleted += _manager_OnBewerkingDeleted;
            //Manager.OnDbBeginUpdate += Manager_OnDbBeginUpdate;
           // Manager.OnDbEndUpdate += Manager_OnDbEndUpdate;
            Manager.OnManagerLoaded += _manager_OnManagerLoaded;
            Manager.FilterChanged += Manager_FilterChanged;
        }

        public void DetachEvents()
        {
            Manager.OnSettingsChanged -= _manager_OnSettingsChanged;
            Manager.OnFormulierDeleted -= Manager_OnFormulierDeleted;
            Manager.OnFormulierChanged -= _manager_OnFormulierChanged;
            // Manager.OnProductiesLoaded -= Manager_OnProductiesChanged;
            //Manager.DbUpdater.DbEntryUpdated -= DbUpdater_DbEntryUpdated;
            //Manager.OnLoginChanged -= _manager_OnLoginChanged;
            Manager.OnBewerkingDeleted -= _manager_OnBewerkingDeleted;
            //Manager.OnDbBeginUpdate -= Manager_OnDbBeginUpdate;
            //Manager.OnDbEndUpdate -= Manager_OnDbEndUpdate;
            Manager.OnManagerLoaded -= _manager_OnManagerLoaded;
            Manager.FilterChanged -= Manager_FilterChanged;
        }

        private void Manager_OnDbEndUpdate()
        {
            _iswaiting = false;
        }

        private void Manager_OnDbBeginUpdate()
        {
            SetWaitUI();
        }

        private bool _iswaiting;

        public void SetWaitUI()
        {
            if (_iswaiting) return;
            _iswaiting = true;
            Task.Run(async () =>
            {
              
                try
                {
                    bool valid = false;
                    this.Invoke(new MethodInvoker(() => valid = !this.IsDisposed));
                    if (!valid) return;
                    xloadinglabel.Invoke(new MethodInvoker(() => { xloadinglabel.Visible = true; }));
                    var cur = 0;
                    var xwv = IsBewerkingView ? "Bewerkingen Laden" : "Producties laden";
                    //var xcurvalue = xwv;
                    var tries = 0;
                    while (_iswaiting && tries < 200)
                    {
                        if (cur > 5) cur = 0;
                        var curvalue = xwv.PadRight(xwv.Length + cur, '.');
                        //xcurvalue = curvalue;
                        xloadinglabel.BeginInvoke(new MethodInvoker(() =>
                        {
                            xloadinglabel.Text = curvalue;
                            xloadinglabel.Invalidate();
                        }));
                        Application.DoEvents();
                       
                        await Task.Delay(500);
                        Application.DoEvents();
                        tries++;
                        cur++;
                        this.Invoke(new MethodInvoker(() => valid = !this.IsDisposed));
                        if (!valid) break;
                    }
                }
                catch (Exception e)
                {
                }

                xloadinglabel.Invoke(new MethodInvoker(() => { xloadinglabel.Visible = false; }));
            });
        }

        public void StopWait()
        {
            _iswaiting = false;
        }

        private void InitColumns()
        {
            foreach (var col in ProductieLijst.Columns.Cast<OLVColumn>())
            {
                col.Groupable = true;
                col.Name = col.AspectName;
                col.ImageGetter = ImageGetter;
                col.GroupFormatter = (@group, parms) =>
                {
                    parms.GroupComparer = Comparer<OLVGroup>.Create((x, y) =>
                        Comparer.Compare(x, y, parms.PrimarySortOrder, parms.PrimarySort ?? parms.SecondarySort));
                };
            }

            var c = ((OLVColumn) ProductieLijst.Columns["Naam"]);
            if (c != null)
                c.IsVisible = IsBewerkingView;
            //((OLVColumn)xproductieLijst.Columns["Omschrijving"]).ImageGetter = ImageGetter;
            //((OLVColumn)xproductieLijst.Columns["Naam"]).ImageGetter = ImageGetter;
            if (!IsBewerkingView)
            {
                c = ((OLVColumn)ProductieLijst.Columns["Omschrijving"]);
                if (c != null)
                    c.GroupKeyGetter = GroupGetter;
            }
            c = ((OLVColumn)ProductieLijst.Columns["TijdOver"]);
            if (c != null)
                c.AspectGetter = BewerkingTijdOverGetter;
            //init time fields
            c = (OLVColumn)ProductieLijst.Columns["LaatstAantalUpdate"];
            if (c != null)
            {
                c.AspectGetter = y =>
                {
                    if (y is Bewerking bw)
                    {
                        return bw.LaatstAantalUpdate.ToString(8, "over {0} {1}", "{0} {1} geleden",false);
                    }
                    if (y is ProductieFormulier pr)
                    {
                        return pr.LaatstAantalUpdate.ToString(8, "over {0} {1}", "{0} {1} geleden",false);
                    }
                    return "N.V.T.";
                };
            }
            //((OLVColumn)xproductieLijst.Columns["TijdGestart"]).AspectGetter = (y) =>
            //{
            //    if (y is Bewerking bw)
            //        return bw.TijdGestart.ToString(48, "over {0} {1}", "{0} {1} geleden");
            //    if (y is ProductieFormulier pr)
            //        return pr.TijdGestart.ToString(48, "over {0} {1}", "{0} {1} geleden");
            //    return "N.V.T.";
            //};

            //((OLVColumn)xproductieLijst.Columns["TijdGestopt"]).AspectGetter = (y) =>
            //{
            //    if (y is Bewerking bw)
            //        return bw.TijdGestopt.ToString(48, "over {0} {1}", "{0} {1} geleden");
            //    if (y is ProductieFormulier pr)
            //        return pr.TijdGestopt.ToString(48, "over {0} {1}", "{0} {1} geleden");
            //    return "N.V.T.";
            //};

            c = (OLVColumn)ProductieLijst.Columns["VerwachtLeverDatum"];
            if (c != null)
            {
                c.AspectGetter = y =>
                {
                    if (y is Bewerking bw)
                        return bw.VerwachtLeverDatum.ToString(8, "over {0} {1}", "{0} {1} geleden",false);
                    if (y is ProductieFormulier pr)
                        return pr.VerwachtLeverDatum.ToString(8, "over {0} {1}", "{0} {1} geleden",false);
                    return "N.V.T.";
                };
            }
            c = (OLVColumn)ProductieLijst.Columns["LeverDatum"];
            if (c != null)
            {
                c.AspectGetter = y =>
                {
                    if (y is Bewerking bw)
                        return bw.LeverDatum.ToString(8, "over {0} {1}", "{0} {1} geleden",false);
                    if (y is ProductieFormulier pr)
                        return pr.LeverDatum.ToString(8, "over {0} {1}", "{0} {1} geleden",false);
                    return "N.V.T.";
                };
            }

            ProductieLijst.RebuildColumns();
            //((OLVColumn)xproductieLijst.Columns["Omschrijving"]).GroupKeyGetter = BewerkingGroupGetter;
        }

        private void InitImageList()
        {
            ximagelist.Images.Clear();
            Bitmap img = null;
            img = !IsBewerkingView ? Resources.page_document_16748 : Resources.operation;
            if (img == null) return;
            //Productieformulieren afbeeldingen
            ximagelist.Images.Add(img); // regular document
            ximagelist.Images.Add(img.CombineImage(Resources.new_25355, 2)); //new document
            ximagelist.Images.Add(img.CombineImage(Resources.Warning_36828, 2)); //warning document
            ximagelist.Images.Add(img.CombineImage(Resources.play_button_icon_icons_com_60615, 2.5)); //play document
            ximagelist.Images.Add(img.CombineImage(Resources.delete_1577, 2)); //deleted document
            ximagelist.Images.Add(img.CombineImage(Resources.check_1582, 2)); // checked document
        }

        private int GetProductieImageIndex(IProductieBase productie)
        {
            if (productie == null) return 0;
            switch (productie.State)
            {
                case ProductieState.Gestopt:
                    if (productie.IsNieuw)
                        return 1;
                    if (productie.TeLaat)
                        return 2;
                    return 0;
                case ProductieState.Gestart:
                    return 3;
                case ProductieState.Verwijderd:
                    return 4;
                case ProductieState.Gereed:
                    return 5;
            }

            return 0;
        }

        private object ImageGetter(object sender)
        {
            return GetProductieImageIndex(sender as IProductieBase);
        }

        private object BewerkingTijdOverGetter(object sender)
        {
            if (sender is Bewerking bew) return bew.TijdOver() + " uur";
            if (sender is ProductieFormulier {Bewerkingen: { }} prod)
                return Math.Round(prod.Bewerkingen.Sum(x => x.TijdOver()), 2) + " uur";
            return 0;
        }

        private object GroupGetter(object sender)
        {
            if (sender is Bewerking bew) return bew.Naam;
            if (sender is ProductieFormulier prod) return GetProductieGroup(prod);

            return "N.V.T";
        }

        private string GetProductieGroup(ProductieFormulier formulier)
        {
            var bew = formulier.GetFirstAvailibleBewerking()?.Naam;
            if (bew == null)
                return "Zonder Bewerkingen";
            return bew;
        }

        private string GetBewerkingGroup(Bewerking bew)
        {
            var group = "";
            if (Manager.Opties != null && Manager.Opties.Bewerkingen != null &&
                (Manager.Opties.ToonAllesVanBeide || Manager.Opties.ToonVolgensBewerkingen))
                group = Manager.Opties.Bewerkingen
                    .FirstOrDefault(t => bew.Naam.Split('[')[0].ToLower() == t.ToLower());
            else group = bew.Naam;

            if (bew == null)
                return "Zonder Bewerkingen";
            return group;
        }

        #endregion Init Methods

        #region Listing Methods

        private bool _loadingproductielist;

        public void StartSync()
        {
            if (IsSyncing || Disposing || IsDisposed) return;
            IsSyncing = true;
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    while(EnableSync && IsSyncing && !IsDisposed && !Disposing)
                    {
                        await Task.Delay(SyncInterval);
                        if (!EnableSync || !IsSyncing || IsDisposed || Disposing) break;
                        if (!_loadingproductielist)
                            UpdateProductieList(true,false);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        public void UpdateProductieList(bool reload, bool showwaitui = true)
        {
            if (_loadingproductielist || Manager.Opties == null || !CanLoad) return;
            _loadingproductielist = true;
            this.Invoke(new MethodInvoker(async() =>
            {
                if (showwaitui)
                    SetWaitUI();
                ProductieLijst.BeginUpdate();
                try
                {
                    var selected1 = ProductieLijst.SelectedObject;
                    var groups1 = ProductieLijst.Groups.Cast<ListViewGroup>().Select(t => (OLVGroup) t.Tag)
                        .Where(x => x.Collapsed)
                        .ToArray();
                    // Manager.Opties.ProductieWeergaveFilters = GetCurrentProductieViewStates();
                    var states = GetCurrentViewStates();
                    var filter = xsearch.Text.ToLower() == "zoeken..." ? "" : xsearch.Text;


                    if (!IsBewerkingView)
                    {
                        var xprods = !reload && CustomList && Producties != null
                            ? Producties.Where(x => states.Any(x.IsValidState) && x.ContainsFilter(filter)).ToList()
                            : Producties = await Manager.GetProducties(states, true, !IsBewerkingView, true);
                        if (!CanLoad) return;
                        if (ValidHandler != null)
                            xprods = xprods.Where(x => ValidHandler.Invoke(x, filter))
                                .ToList();
                        else
                            xprods = xprods.Where(x => x.IsAllowed(filter, states, true)).ToList();
                        ProductieLijst.SetObjects(xprods);
                    }
                    else
                    {
                        var bws = !reload && CustomList && Bewerkingen != null
                            ? Bewerkingen.Where(x => states.Any(x.IsValidState) && x.ContainsFilter(filter)).ToList()
                            : Bewerkingen = (await Manager.GetBewerkingen(states, true, true));
                        if (!CanLoad) return;
                        if (ValidHandler != null)
                            bws = bws.Where(x => ValidHandler.Invoke(x, filter))
                                .ToList();
                        else
                            bws = bws.Where(x => x.IsAllowed(filter)).ToList();

                        ProductieLijst.SetObjects(bws);
                    }

                    var xgroups = ProductieLijst.Groups.Cast<ListViewGroup>().ToList();
                    if (groups1.Length > 0)
                    {
                        for (int i = 0; i < xgroups.Count; i++)
                        {
                            var group = xgroups[i].Tag as OLVGroup;
                            if (group == null)
                                continue;
                            if (groups1.Any(t => !group.Collapsed && t.Header == group.Header))
                                group.Collapsed = true;
                        }
                    }

                    ProductieLijst.SelectedObject = selected1;
                    ProductieLijst.SelectedItem?.EnsureVisible();
                    SetButtonEnable();
                    OnItemCountChanged();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    ProductieLijst.EndUpdate();
                }

                _loadingproductielist = false;
                if (EnableSync)
                    StartSync();
                StopWait();
            }));
        }

        public bool UpdateFormulier(ProductieFormulier form)
        {
            if (IsDisposed || Disposing || form == null || _loadingproductielist) return false;
           
            try
            {
                var filter = xsearch.Text.ToLower() == "zoeken..."
                    ? null
                    : xsearch.Text.Trim();

                var states = GetCurrentViewStates();
                var changed = false;
                var xreturn = false;

                if (!IsBewerkingView)
                {
                    bool isvalid;
                    if (ValidHandler != null)
                        isvalid = states.Any(form.IsValidState) && ValidHandler.Invoke(form, filter);
                    else isvalid = form.IsAllowed(filter, states, true);

                    var xproducties = ProductieLijst.Objects?.Cast<ProductieFormulier>().ToList();
                    var xform = xproducties?.FirstOrDefault(x =>
                        string.Equals(x.ProductieNr, form.ProductieNr, StringComparison.CurrentCultureIgnoreCase));

                    if (xform == null && isvalid)
                    {
                        ProductieLijst.BeginUpdate();
                        ProductieLijst.AddObject(form);
                        Producties?.Add(form);
                        changed = true;
                        ProductieLijst.EndUpdate();
                    }
                    else if (xform != null && !isvalid)
                    {
                        ProductieLijst.BeginUpdate();
                        ProductieLijst.RemoveObject(xform);
                        Producties?.Remove(xform);
                        changed = true;
                        ProductieLijst.EndUpdate();
                    }
                    else if (isvalid)
                    {
                        ProductieLijst.RefreshObject(form);
                    }

                    if (Producties != null)
                    {
                        var index = Producties.IndexOf(form);
                        if (index > -1)
                        {
                            if (isvalid)
                            {
                                Producties[index] = form;
                            }
                            else if(RemoveCustomItemIfNotValid)
                            {
                                Producties.RemoveAt(index);
                                changed = true;
                            }
                        }
                        else if(isvalid)
                        {
                            Producties.Add(form);
                            changed = true;
                        }
                    }

                    xreturn = isvalid;
                }
                else
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        changed = UpdateBewerking(form, null, states, filter);
                    }));
                }

                if (changed)
                    OnItemCountChanged();
                SetButtonEnable();
                return xreturn;
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("Disposed!");
                return false;
            }
            finally
            {
                ProductieLijst.EndUpdate();
            }
        }

        private bool _IsUpdating;
        public Task UpdateList(bool onlywhilesyncing)
        {
            return Task.Factory.StartNew(() =>
            {
                if (_IsUpdating) return;
                _IsUpdating = true;
                try
                {
                    this.BeginInvoke(new Action(async () =>
                    {
                        var states = GetCurrentViewStates();
                        if (Producties != null && Producties.Count > 0)
                        {
                            for (int i = 0; i < Producties.Count; i++)
                            {
                                var prod = Producties[i];
                                var xprod = await Manager.Database.GetProductie(prod.ProductieNr);
                                if (onlywhilesyncing && !IsSyncing) break;
                                if (IsDisposed || Disposing) break;
                                bool valid = xprod != null;
                                if (valid)
                                {
                                    if (ValidHandler != null)
                                        valid = states.Any(xprod.IsValidState) && ValidHandler.Invoke(xprod, null);
                                    else valid = states.Any(x => xprod.IsValidState(x)) && xprod.IsAllowed(null);
                                }
                                if (!valid)
                                {
                                    Producties.RemoveAt(i--);
                                    ProductieLijst.BeginUpdate();
                                    ProductieLijst.RemoveObject(prod);
                                    ProductieLijst.EndUpdate();
                                }
                                else
                                {
                                    await xprod.UpdateForm(true, false, null, "", false, false, false);
                                    Producties[i] = xprod;
                                    ProductieLijst.RefreshObject(xprod);
                                }
                            }
                        }
                        if (Bewerkingen != null && Bewerkingen.Count > 0)
                        {
                            for (int i = 0; i < Bewerkingen.Count; i++)
                            {
                                var bew = Bewerkingen[i];
                                var xbew = Werk.FromPath(bew.Path)?.Bewerking;
                                if (onlywhilesyncing && !IsSyncing) break;
                                if (IsDisposed || Disposing) break;
                                bool valid = xbew != null;
                                if (valid)
                                {
                                    if (ValidHandler != null)
                                        valid = states.Any(xbew.IsValidState) && ValidHandler.Invoke(xbew, null);
                                    else valid = states.Any(x => xbew.IsValidState(x)) && xbew.IsAllowed(null);
                                }
                                if (!valid)
                                {
                                    Bewerkingen.RemoveAt(i--);
                                    ProductieLijst.BeginUpdate();
                                    ProductieLijst.RemoveObject(bew);
                                    ProductieLijst.EndUpdate();
                                }
                                else
                                {
                                    await xbew.Parent.UpdateForm(true, false, null, "", false, false, false);
                                    Bewerkingen[i] = xbew;
                                    ProductieLijst.RefreshObject(xbew);
                                }
                            }
                        }
                    }));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                _IsUpdating = false;

            });

        }

        public bool UpdateBewerking(ProductieFormulier form, List<Bewerking> bewerkingen, ViewState[] states,
            string filter)
        {
            if (!IsBewerkingView || form == null || this.Disposing || this.IsDisposed) return false;
            bool changed = false;
            try
            {
                
                var xbewerkingen = bewerkingen ?? ProductieLijst.Objects?.Cast<Bewerking>().ToList();
                states ??= GetCurrentViewStates();
                filter ??= xsearch.Text.ToLower() == "zoeken..."
                    ? null
                    : xsearch.Text.Trim();
                // bool checkall = xbewerkingen != null && !xbewerkingen.Any(x=> string.Equals(x.ProductieNr, form.ProductieNr, StringComparison.CurrentCultureIgnoreCase));
                if (form?.Bewerkingen != null && form.Bewerkingen.Length > 0)
                    foreach (var b in form.Bewerkingen)
                    {
                        bool isvalid;
                        if (ValidHandler != null)
                            isvalid = states.Any(b.IsValidState) && ValidHandler.Invoke(b, filter);
                        else isvalid = states.Any(x => b.IsValidState(x)) && b.IsAllowed(filter);
                        var xb = xbewerkingen?.FirstOrDefault(x =>
                            string.Equals(x.Path, b.Path, StringComparison.CurrentCultureIgnoreCase));
                        if (xb == null && isvalid)
                        {
                            ProductieLijst.BeginUpdate();
                            ProductieLijst.AddObject(b);
                            Bewerkingen?.Add(b);
                            changed = true;
                            ProductieLijst.EndUpdate();
                        }
                        else if (xb != null && !isvalid)
                        {
                            ProductieLijst.BeginUpdate();
                            ProductieLijst.RemoveObject(xb);
                            Bewerkingen?.Remove(xb);
                            changed = true;
                            ProductieLijst.EndUpdate();
                        }
                        else if (isvalid)
                        {
                            ProductieLijst.RefreshObject(b);
                        }

                        if (Bewerkingen != null)
                        {
                            var index = Bewerkingen.IndexOf(b);
                            if (index > -1)
                            {
                                if (isvalid)
                                {
                                    Bewerkingen[index] = b;
                                }
                                else if (RemoveCustomItemIfNotValid)
                                {
                                    Bewerkingen.RemoveAt(index);
                                    changed = true;
                                }
                            }
                            else if (isvalid)
                            {
                                Bewerkingen.Add(b);
                                changed = true;
                            }
                        }

                    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return changed;
        }

        private void DeleteID(string id)
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                ProductieLijst.BeginUpdate();
                try
                {
                    if (!IsBewerkingView)
                    {
                        var prods = ProductieLijst.Objects?.Cast<ProductieFormulier>()?.Where(x =>
                            string.Equals(id, x.ProductieNr, StringComparison.CurrentCultureIgnoreCase)).ToArray();
                        if (prods != null && prods.Length > 0)
                            ProductieLijst.RemoveObjects(prods);
                    }
                    else
                    {
                        var bws = ProductieLijst.Objects?.Cast<Bewerking>()?.Where(x =>
                            string.Equals(id, x.ProductieNr, StringComparison.CurrentCultureIgnoreCase)).ToArray();
                        if (bws != null && bws.Length > 0)
                            ProductieLijst.RemoveObjects(bws);
                    }

                    OnItemCountChanged();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    ProductieLijst.EndUpdate();
                }
            }));
        }

        #endregion Listing Methods

        #region Manager Events

        private void Manager_FilterChanged(object sender, EventArgs e)
        {
            try
            {
                if (!_enableEntryFilter) return;
                BeginInvoke(new MethodInvoker(() =>
                {
                    InitFilterStrips();
                    UpdateProductieList(true);
                }));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void _manager_OnSettingsChanged(object instance, UserSettings settings, bool init)
        {
            try
            {
                if (!init) return;
                BeginInvoke(new MethodInvoker(() =>
                {
                    if (IsDisposed) return;
                    InitFilterStrips();
                    UpdateProductieList(true);
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void _manager_OnLoginChanged(UserAccount user, object instance)
        {
            try
            {
                BeginInvoke(new Action(() =>
                {
                    if (IsDisposed || Disposing) return;
                    SetButtonEnable();
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void _manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            if (IsDisposed || Disposing || !IsLoaded) return;
            try
            {
                BeginInvoke(new MethodInvoker(() => UpdateFormulier(changedform)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void _manager_OnBewerkingDeleted(object sender, Bewerking bew, string change)
        {
            if (IsDisposed || Disposing || !IsBewerkingView || !IsLoaded) return;
            try
            {
                BeginInvoke(new Action(() =>
                {
                    ProductieLijst.BeginUpdate();
                    ProductieLijst.RemoveObject(bew);
                    ProductieLijst.EndUpdate();
                    SetButtonEnable();
                    OnItemCountChanged();
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void _manager_OnManagerLoaded()
        {
            if (IsDisposed || Disposing) return;
            try
            {
                BeginInvoke(new Action(() =>
                {
                    //RunProductieRefresh();
                    //UpdateAllLists();
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Manager_OnFormulierDeleted(object sender, string id)
        {
            if (IsDisposed || Disposing ||!IsLoaded || string.IsNullOrEmpty(id)) return;
            DeleteID(id);
        }

        #endregion Manager Events

        #region Search

        private void xsearchbox_TextChanged(object sender, EventArgs e)
        {
            if (xsearch.Text.ToLower().Trim() != "zoeken...") UpdateProductieList(false);
        }

        private void xsearch_Enter(object sender, EventArgs e)
        {
            var tb = sender as MetroTextBox;
            if (tb != null)
                if (tb.Text == "Zoeken...")
                    tb.Text = "";
        }

        private void xsearch_Leave(object sender, EventArgs e)
        {
            var tb = sender as MetroTextBox;
            if (tb != null)
                if (string.IsNullOrWhiteSpace(tb.Text))
                    tb.Text = "Zoeken...";
        }

        #endregion Search

        #region MenuButton Methods

        public void ShowProductieForm(ProductieFormulier pform, bool showform, Bewerking bewerking = null)
        {
            Manager.FormulierActie(new object[] {pform, bewerking}, MainAktie.OpenProductie);
        }

        private void StartSelectedProducties()
        {
            if (ProductieLijst.SelectedObjects.Count > 0)
            {
                var bws = new List<Bewerking>();
                if (!IsBewerkingView)
                {
                    var prods = ProductieLijst.SelectedObjects.Cast<ProductieFormulier>().ToList();

                    prods.ForEach(x =>
                    {
                        if (x.Bewerkingen?.Length > 0)
                            bws.AddRange(x.Bewerkingen.Where(b => b.State == ProductieState.Gestopt && b.IsAllowed()));
                    });
                }
                else
                {
                    bws = ProductieLijst.SelectedObjects.Cast<Bewerking>().ToList();
                }

                StartBewerkingen(bws.ToArray());
            }
        }

        private void StopSelectedProducties()
        {
            if (ProductieLijst.SelectedObjects.Count > 0)
            {
                var bws = new List<Bewerking>();
                if (!IsBewerkingView)
                {
                    var prods = ProductieLijst.SelectedObjects.Cast<ProductieFormulier>().ToList();

                    prods.ForEach(x =>
                    {
                        if (x.Bewerkingen?.Length > 0)
                            bws.AddRange(
                                x.Bewerkingen.Where(b => b.State == ProductieState.Gestart && b.IsAllowed(null)));
                    });
                }
                else
                {
                    bws = ProductieLijst.SelectedObjects.Cast<Bewerking>().ToList();
                }

                StopBewerkingen(bws.ToArray());
            }
        }

        public static void StartBewerkingen(Bewerking[] bws)
        {
            var count = bws.Length;
            if (count > 0)
            {
                var withnopers = bws.Count(x => x.AantalActievePersonen == 0);
                var valid = true;
                if (withnopers > 1)
                    valid = XMessageBox.Show(
                        $"Je staat op het punt {withnopers} bewerkingen te starten waar geen personeel voor is ingezet.\n" +
                        "Wil je ze allemaal achter elkaar indelen?\n\n" +
                        $"Je zal dat {withnopers} keer moeten doen.\n" +
                        "Klik op 'Ja' als je door wilt gaan, klik anders op 'Nee'.", "Opgelet", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning) == DialogResult.Yes;
                if (valid)
                    try
                    {
                        var mainform = Application.OpenForms["MainForm"];
                        mainform?.BeginInvoke(new MethodInvoker(async () =>
                        {
                            for (var i = 0; i < bws.Length; i++)
                            {
                                var parent = bws[i].GetParent();
                                var werk = bws[i];
                                if (werk.State != ProductieState.Gestart)
                                {
                                    var initnew = true;

                                    if (werk.AantalActievePersonen == 0)
                                    {
                                        var pers = new PersoneelsForm(ProductieView._manager, true);
                                        if (pers.ShowDialog(werk) == DialogResult.OK)
                                        {
                                            if (pers.SelectedPersoneel != null && pers.SelectedPersoneel.Length > 0)
                                            {
                                                foreach (var per in pers.SelectedPersoneel) per.Klusjes?.Clear();
                                                var afzonderlijk = false;
                                                if (pers.SelectedPersoneel.Length > 1 && werk.IsBemand)
                                                {
                                                    var result = XMessageBox.Show(
                                                        $"Je hebt {pers.SelectedPersoneel.Length} medewerkers geselecteerd," +
                                                        " wil je ze allemaal afzonderlijk indelen?", "Indeling",
                                                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                                    if (result == DialogResult.Cancel)
                                                        return;
                                                    afzonderlijk = result == DialogResult.Yes;
                                                }

                                                if (!werk.IsBemand || !afzonderlijk)
                                                {
                                                    var klusui = new NieuwKlusForm(parent, pers.SelectedPersoneel,
                                                        true,
                                                        werk);
                                                    if (klusui.ShowDialog() != DialogResult.OK)
                                                        return;
                                                    var pair = klusui.SelectedKlus.GetWerk(parent);
                                                    var prod = pair.Formulier;
                                                    werk = pair.Bewerking;
                                                    if (werk != null && !werk.IsBemand &&
                                                        klusui.SelectedKlus?.Tijden?.Count > 0)
                                                        foreach (var wp in werk.WerkPlekken)
                                                            wp.Tijden = klusui.SelectedKlus.Tijden.CreateCopy();

                                                    //var xwp = werk.WerkPlekken.FirstOrDefault(x =>
                                                    //    string.Equals(klusui.SelectedKlus.Path, x.Path, StringComparison.CurrentCultureIgnoreCase));
                                                    //if(xwp != null && xwp.Personen.Count > 0)
                                                    //    foreach (var per in xwp.Personen)
                                                    //        per.ReplaceKlus(klusui.SelectedKlus);
                                                }
                                                else
                                                {
                                                    foreach (var per in pers.SelectedPersoneel)
                                                    {
                                                        var klusui = new NieuwKlusForm(parent, per, true, werk);
                                                        if (klusui.ShowDialog() != DialogResult.OK)
                                                            break;
                                                        //klusui.Persoon.CopyTo(ref per);
                                                        var pair = klusui.SelectedKlus.GetWerk(parent);
                                                        var prod = pair.Formulier;
                                                        werk = pair.Bewerking;
                                                        //Bewerking werk = parent.Bewerkingen.FirstOrDefault(x => x.Path.ToLower() == xp.WerktAan.ToLower());
                                                        if (werk != null)
                                                        {
                                                            per.ReplaceKlus(klusui.SelectedKlus);
                                                            var wp = werk.WerkPlekken.FirstOrDefault(x =>
                                                                string.Equals(x.Naam, klusui.SelectedKlus.WerkPlek,
                                                                    StringComparison.CurrentCultureIgnoreCase));
                                                            if (wp == null)
                                                            {
                                                                wp = new WerkPlek(per,
                                                                    klusui.SelectedKlus.WerkPlek, werk);
                                                                werk.WerkPlekken.Add(wp);
                                                            }
                                                            else
                                                            {
                                                                wp.AddPersoon(per, werk);
                                                            }
                                                            await wp.Werk.UpdateBewerking(null,
                                                                $"{wp.Path} indeling aangepast",false,true);
                                                            werk.CopyTo(ref bws[i]);
                                                        }
                                                    }
                                                }
                                            }


                                            initnew = false;
                                        }
                                    }

                                    werk?.StartProductie(true, initnew);
                                }
                            }
                        }));
                    }
                    catch (Exception exception)
                    {
                        XMessageBox.Show(exception.Message,
                            "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
        }

        private static async void StopBewerkingen(Bewerking[] bws)
        {
            var count = bws.Length;
            if (count > 0)
            {
                var save = false;
                foreach (var bw in bws)
                    if (bw.State == ProductieState.Gestart)
                        save |= await bw.StopProductie(true);
            }
        }

        private void ShowSelectedProducties()
        {
            if (Manager.LogedInGebruiker != null && Manager.LogedInGebruiker.AccesLevel >= AccesType.ProductieBasis)
                if (!IsBewerkingView)
                {
                    foreach (var o in ProductieLijst.SelectedObjects.Cast<ProductieFormulier>().ToArray())
                        if (o.State != ProductieState.Verwijderd)
                            ShowProductieForm(o, true);
                }
                else
                {
                    var bws = ProductieLijst.SelectedObjects.Cast<Bewerking>().ToArray();
                    foreach (var bw in bws)
                        if (bw.State != ProductieState.Verwijderd)
                        {
                            var prod = bw.GetParent();
                            if (prod != null)
                                ShowProductieForm(prod, true, bw);
                        }
                }
        }

        private void MeldSelectedProductieGereed()
        {
            if (ProductieLijst.SelectedObject is ProductieFormulier p)
                MeldGereed(p);
            else if (ProductieLijst.SelectedObject is Bewerking bew)
                MeldBewerkingGereed(bew);
        }

        public static bool MeldGereed(ProductieFormulier form)
        {
            var p = form;
            if (p != null && p.State != ProductieState.Verwijderd && p.State != ProductieState.Gereed)
            {
                var bews = p.Bewerkingen.Where(t => t.TotaalGemaakt < t.Aantal && t.IsAllowed()).ToArray();
                var res = DialogResult.Yes;
                if (bews.Length > 0)
                    res = XMessageBox.Show(
                        "Er is één of meer bewerking(en) waarvan de aantallen niet bereikt zijn...\nWilt u toch doorgaan met gereedmelden?",
                        "Niet Gereed", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (res == DialogResult.Yes)
                {
                    var x = new GereedMelder
                    {
                        Aantal = p.AantalGemaakt,
                        Naam = "Productie",
                        StartPosition = FormStartPosition.CenterParent,
                        Paraaf = p.Paraaf
                    };
                    return x.ShowDialog(p) != DialogResult.Cancel;
                }
            }

            return false;
        }

        public static bool MeldBewerkingGereed(Bewerking bewerking)
        {
            var b = bewerking;
            if (b != null && b.State != ProductieState.Verwijderd && b.State != ProductieState.Gereed)
            {
                var res = DialogResult.Yes;
                if (b.TotaalGemaakt < b.Aantal)
                    res = XMessageBox.Show(
                        $"Aantal van '{b.Naam}' is nog niet bereikt... Je hebt maar {b.TotaalGemaakt} van de {b.Aantal} gemaakt.\nWeetje zeker dat je toch verder wilt gaan met gereedmelden?",
                        "Niet Klaar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (res == DialogResult.Yes)
                {
                    var x = new GereedMelder();
                    x.Aantal = b.AantalGemaakt;
                    x.Naam = $"Bewerking '{b.Naam}'";
                    x.StartPosition = FormStartPosition.CenterParent;
                    x.Paraaf = b.Paraaf;
                    x.ShowDialog(b);
                    //if (x.ShowDialog(b) == DialogResult.OK)
                    //{
                    //    return await b.MeldBewerkingGereed(x.Paraaf, x.Aantal, x.Notitie, true,true);
                    //}
                }
            }

            return false;
        }

        public static async void ShowBewDeelsGereedMeldingen(Bewerking bew)
        {
            if (bew == null) return;
            var xform = new DeelsGereedMeldingenForm(bew);
            if (xform.ShowDialog() == DialogResult.OK)
            {
                bew = xform.Bewerking;
                await bew.UpdateBewerking(null, $"[{bew.Path}] Deels gereedmeldingen aangepast");
            }
        }

        public static async void ShowProdDeelsGereedMeldingen(ProductieFormulier form)
        {
            if (form?.Bewerkingen == null || form.Bewerkingen.Length == 0) return;
            Bewerking bw = null;
            if (form.Bewerkingen.Length > 1)
            {
                var bwc = new BewerkingChooser(form.Bewerkingen.Select(x => x.Naam).ToArray())
                {
                    Text = "Kies bewerking voor de deels gereedmeldingen"
                };
                if (bwc.ShowDialog() == DialogResult.OK)
                    bw = form.Bewerkingen.FirstOrDefault(x =>
                        string.Equals(x.Naam, bwc.SelectedItem, StringComparison.CurrentCultureIgnoreCase));
            }
            else
            {
                bw = form.Bewerkingen[0];
            }

            if (bw == null) return;
            var deelgereed = new DeelsGereedMeldingenForm(bw);
            if (deelgereed.ShowDialog() != DialogResult.OK) return;

            bw = deelgereed.Bewerking;
            await bw.UpdateBewerking(null, $"[{bw.Path}] Deels gereedmeldingen aangepast");
        }

        private void ShowSelectedProdDeelGereedMeldingen()
        {
            if (ProductieLijst.SelectedObject is ProductieFormulier form)
                ShowProdDeelsGereedMeldingen(form);
            else if (ProductieLijst.SelectedObject is Bewerking bew) ShowBewDeelsGereedMeldingen(bew);
        }

        private void ShowSelectedWerkplekken()
        {
            if (ProductieLijst.SelectedObject is ProductieFormulier prod)
            {
                var ind = new Indeling(prod);
                ind.ShowDialog();
            }
            else if (ProductieLijst.SelectedObject is Bewerking bew)
            {
                var parent = bew.GetParent();
                if (parent == null)
                    return;
                var ind = new Indeling(parent, bew);
                ind.ShowDialog();
            }
        }

        private void ShowSelectedProductieSettings()
        {
            ProductieFormulier form = null;
            if (ProductieLijst.SelectedObject is ProductieFormulier prod)
                form = prod;
            else if (ProductieLijst.SelectedObject is Bewerking bew) form = bew.GetParent();
            if (form == null)
                return;
            var x = new WijzigProductie(form);
            x.ShowDialog();
        }

        private void ShowSelectedProductieWerktijd()
        {
            if (ProductieLijst.SelectedObjects.Count > 0)
            {
                var bws = new List<Bewerking>();
                if (!IsBewerkingView)
                    foreach (var prod in ProductieLijst.SelectedObjects.Cast<ProductieFormulier>().ToArray())
                    {
                        if (prod.Bewerkingen == null) continue;
                        var xbws = prod.Bewerkingen.Where(x => x.IsAllowed()).ToList();
                        if (xbws.Count > 0)
                            bws.AddRange(xbws);
                    }
                else bws = ProductieLijst.SelectedObjects.Cast<Bewerking>().ToList();

                foreach (var bw in bws)
                    bw.ShowWerktIjden();
            }
        }

        private void ShowSelectedStoringen()
        {
            ProductieFormulier form = null;
            if (ProductieLijst.SelectedObject is ProductieFormulier prod)
                form = prod;
            else if (ProductieLijst.SelectedObject is Bewerking bew) form = bew.GetParent();
            if (form == null) return;
            var allst = new AlleStoringen();
            allst.InitStoringen(form);
            allst.ShowDialog();
        }

        private void ShowSelectedProductieAantalGemaakt()
        {
            if (!IsBewerkingView)
            {
                try
                {
                    var items = xproductieLijst.SelectedObjects?.Cast<ProductieFormulier>().ToList();
                    if (items == null || items.Count == 0) return;
                    foreach (var form in items)
                    {
                        var ag = new AantalGemaaktUI();
                        ag.ShowDialog(form);
                    }
                }
                catch (Exception e)
                {
                    XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
                }

            }
            else
            {
                try
                {
                    var items = xproductieLijst.SelectedObjects?.Cast<Bewerking>().ToList();
                    if (items == null || items.Count == 0) return;
                    foreach (var bew in items)
                    {
                        var oldaantal = bew.AantalGemaakt;
                        var parent = bew.GetParent();
                        var dt = new AantalGemaaktUI();
                        if (dt.ShowDialog(parent, bew) == DialogResult.OK)
                        {
                            var newaantal = bew.AantalGemaakt;
                            if (oldaantal != newaantal)
                                bew.UpdateBewerking(null,
                                    $"[{bew.Path}] Aantal gemaakt aangepast van {oldaantal} naar {bew.AantalGemaakt}").Wait();
                        }
                    }
                }
                catch (Exception e)
                {
                    XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
                }
            }
        }

        private async void ShowSelectedMaterials()
        {
            ProductieFormulier form = null;

            if (ProductieLijst.SelectedObject is ProductieFormulier prod)
                form = prod;
            else if (ProductieLijst.SelectedObject is Bewerking bew)
                form = bew.GetParent();

            if (form == null) return;
            var matform = new MateriaalForm();
            if (matform.ShowDialog(form) == DialogResult.OK)
                await form.UpdateForm(false, false, null, $"[{form.ProductieNr}] Materialen Gewijzigd");
        }

        private void ShowSelectedAfkeur()
        {
            ProductieFormulier form = null;

            if (ProductieLijst.SelectedObject is ProductieFormulier prod)
                form = prod;
            else if (ProductieLijst.SelectedObject is Bewerking bew)
                form = bew.GetParent();
            if (form == null) return;

            var xafk = new AfkeurForm(form);
            xafk.ShowDialog();
        }

        public void ShowSelectedAanbevolenPersonen()
        {
            try
            {
                if (xproductieLijst.SelectedObject is Bewerking bew)
                    new AanbevolenPersonenForm(bew).ShowDialog();
                else if (ProductieLijst.SelectedObject is ProductieFormulier form)
                    new AanbevolenPersonenForm(form).ShowDialog();
            }
            catch (Exception exception)
            {
                XMessageBox.Show("Er zijn geen aanbevelingen", "Geen Aanbevelingen");
            }
        }

        private async void RemoveSelectedProducties()
        {
            if (ProductieLijst.SelectedObjects.Count == 0)
                return;
            if (XMessageBox.Show("Weetje zeker dat je alle geselecteerde producties wilt verwijderen?", "Verwijderen",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                var bws = new List<Bewerking>();
                if (!IsBewerkingView)
                    foreach (var prod in ProductieLijst.SelectedObjects.Cast<ProductieFormulier>().ToArray())
                    {
                        if (prod.Bewerkingen == null) continue;
                        var xbws = prod.Bewerkingen.Where(x => x.IsAllowed()).ToList();
                        if (xbws.Count > 0)
                            bws.AddRange(xbws);
                    }
                else bws = ProductieLijst.SelectedObjects.Cast<Bewerking>().ToList();

                var done = 0;
                var removed = bws.Any(x => x.State == ProductieState.Verwijderd);
                var skip = false;
                if (removed)
                {
                    var res = XMessageBox.Show("Er zit een verwijderd productie tussen je selecties!\n" +
                                               "Deze nogmaals verwijderen zal ze voorgoed wissen!\n\n" +
                                               "Weet je zeker dat je dat wilt doen!?\n\n" +
                                               "Click op 'Nee' als je ze wilt overslaan", "",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                    if (res == DialogResult.Cancel)
                        return;
                    skip = res == DialogResult.No;
                }

                for (var i = 0; i < bws.Count; i++)
                {
                    var pr = bws[i];
                    if (pr == null)
                        continue;
                    if (await pr.RemoveBewerking(skip))
                        done++;
                }

                //if (done > 0)
                //{
                //    string xvalue = done == 1 ? "productie" : "producties";
                //    XMessageBox.Show($"{done} {xvalue} succesvol verwijderd!", "Verwijderd", MessageBoxButtons.OK,
                //        MessageBoxIcon.Information);
                //}
            }
        }

        private async void UndoSelectedProducties()
        {
            if (ProductieLijst.SelectedObjects.Count > 0)
            {
                var bws = new List<Bewerking>();
                if (!IsBewerkingView)
                    foreach (var prod in ProductieLijst.SelectedObjects.Cast<ProductieFormulier>().ToArray())
                    {
                        if (prod.Bewerkingen == null) continue;
                        var xbws = prod.Bewerkingen.Where(x => x.IsAllowed()).ToList();
                        if (xbws.Count > 0)
                            bws.AddRange(xbws);
                    }
                else bws = ProductieLijst.SelectedObjects.Cast<Bewerking>().ToList();

                for (var i = 0; i < bws.Count; i++)
                {
                    var bw = bws[i];
                    if (bw == null) continue;
                    await bw.Undo();
                }
            }
        }

        private void ShowSelectedProductieInfo()
        {
            if (ProductieLijst.SelectedObject is ProductieFormulier prod)
                new ProductieInfoForm(prod).ShowDialog();
            else if (ProductieLijst.SelectedObject is Bewerking bew)
                new ProductieInfoForm(bew).ShowDialog();
        }

        private async void ShowSelectedLeverdatum()
        {
            if (!IsBewerkingView)
            {
                try
                {
                    var items = xproductieLijst.SelectedObjects?.Cast<ProductieFormulier>().ToList();
                    if (items == null || items.Count == 0) return;
                    var datum = items.FirstOrDefault()?.LeverDatum ?? DateTime.Now;
                    string x1 = items.Count == 1 ? items[0].Omschrijving : $"{items.Count} producties";
                    string msg = $"Wijzig leverdatum voor {x1}.";
                    var dc = new DatumChanger();
                    if (dc.ShowDialog(datum,msg) == DialogResult.OK)
                    {
                        foreach (var form in items)
                        {
                            var change = $"[{form.ProductieNr}|{form.ArtikelNr}] Leverdatum gewijzigd!\n" +
                                         $"Van: {form.LeverDatum:dd MMMM yyyy HH:mm} uur\n" +
                                         $"Naar: {dc.SelectedValue:dd MMMM yyyy HH:mm} uur";
                            form.LeverDatum = dc.SelectedValue;
                            await form.UpdateForm(true, false, null, change);
                        }
                    }

                    dc.Dispose();
                }
                catch (Exception e)
                {
                    XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
                }
               
            }
            else
            {
                try
                {
                    var items = xproductieLijst.SelectedObjects?.Cast<Bewerking>().ToList();
                    if (items == null || items.Count == 0) return;
                    var item = items.FirstOrDefault();
                    var datum = item?.LeverDatum ?? DateTime.Now;
                    string x1 = items.Count == 1 && item != null ? $"[{item.ProductieNr}|{ item.ArtikelNr}] { item.Naam}\n\n{item.Omschrijving}" : $"{items.Count} producties";
                    string msg = $"Wijzig leverdatum voor {x1}.";
                    var dc = new DatumChanger();
                    if (dc.ShowDialog(datum, msg) == DialogResult.OK)
                    {
                        foreach (var form in items)
                        {
                            var change = $"[{form.ProductieNr}|{form.ArtikelNr}] {form.Naam} Leverdatum gewijzigd!\n" +
                                         $"Van: {form.LeverDatum:dd MMMM yyyy HH:mm} uur\n" +
                                         $"Naar: {dc.SelectedValue:dd MMMM yyyy HH:mm} uur";
                            form.LeverDatum = dc.SelectedValue;
                            await form.UpdateBewerking(null, change);
                        }
                    }

                    dc.Dispose();
                }
                catch (Exception e)
                {
                    XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
                }
            }
        }

        private async void ShowSelectedAantal()
        {
            List<ProductieFormulier> prods = IsBewerkingView
                ? ProductieLijst.SelectedObjects?.Cast<Bewerking>().Select(x => x.Parent).Distinct().ToList() : ProductieLijst?.SelectedObjects?.Cast<ProductieFormulier>().ToList();

            if (prods == null || prods.Count == 0) return;
            var aantal = prods.FirstOrDefault()?.Aantal ?? 0;
            string x1 = prods.Count == 1 ? prods[0].Omschrijving : $"{prods.Count} producties";
            string msg = $"Wijzig aantal voor {x1}.";
            var dc = new AantalChanger();
            if (dc.ShowDialog(aantal, msg) == DialogResult.OK)
            {
                foreach (var form in prods)
                {
                    var change = $"[{form.ProductieNr}|{form.ArtikelNr}] Aantal gewijzigd!\n" +
                                 $"Van: {form.Aantal}\n" +
                                 $"Naar: {dc.Aantal}";
                    form.Aantal = dc.Aantal;
                    await form.UpdateForm(true, false, null, change);
                }
            }
        }

        private async void ShowSelectedNotitie()
        {
            if (!IsBewerkingView)
            {
                try
                {
                    var items = xproductieLijst.SelectedObjects?.Cast<ProductieFormulier>().ToList();
                    if (items == null || items.Count == 0) return;
                    foreach (var form in items)
                    {
                        var xtxtform = new NotitieForms(form.Note, form)
                        {
                            Title = $"Notitie voor [{form.ProductieNr}, {form.ArtikelNr}] {form.Omschrijving}"
                        };
                        if (xtxtform.ShowDialog() == DialogResult.OK)
                        {
                            form.Note = xtxtform.Notitie;
                            await form.UpdateForm(false, false, null,
                                $"[{form.ProductieNr}, {form.ArtikelNr}] {form.Naam} Notitie Gewijzigd");
                        }
                    }
                }
                catch (Exception e)
                {
                    XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
                }

            }
            else
            {
                try
                {
                    var items = xproductieLijst.SelectedObjects?.Cast<Bewerking>().ToList();
                    if (items == null || items.Count == 0) return;
                    foreach (var bew in items)
                    {
                        var xtxtform = new NotitieForms(bew.Note, bew)
                        {
                            Title = $"Notitie voor [{bew.ProductieNr}, {bew.ArtikelNr}] {bew.Naam} van {bew.Omschrijving}"
                        };
                        if (xtxtform.ShowDialog() == DialogResult.OK)
                        {
                            bew.Note = xtxtform.Notitie;
                            await bew.UpdateBewerking(null,$"[{bew.ProductieNr}, {bew.ArtikelNr}] {bew.Naam} Notitie Gewijzigd");
                        }
                    }
                }
                catch (Exception e)
                {
                    XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
                }
            }
        }

        public ViewState[] GetCurrentViewStates()
        {
            var states = new List<ViewState>();
            var items = xfiltercontainer.DropDownItems;
            foreach (var tb in items.Cast<ToolStripMenuItem>())
                if (tb.Checked)
                {
                    var xstate = -1;
                    if (int.TryParse(tb.Tag.ToString(), out xstate))
                    {
                        var state = (ViewState) xstate;
                        states.Add(state);
                    }
                }

            if (states.Count == 0)
            {
                if (EnableFiltering)
                {
                    states.Add(ViewState.Gestart);
                    states.Add(ViewState.Gestopt);
                    states.Add(ViewState.Nieuw);
                    states.Add(ViewState.Telaat);
                    states.Add(ViewState.Verwijderd);
                }
                else states.Add(ViewState.Alles);
            }
            return states.ToArray();
        }

        private void ShowExportSelection()
        {
            if (ProductieLijst.SelectedObjects.Count == 0) return;
            try
            {
                if (IsBewerkingView)
                {
                    var bws = ProductieLijst.SelectedObjects.Cast<Bewerking>().ToList();
                    var exc = new CreateExcelForm(bws);
                    exc.ShowDialog();
                }
                else
                {
                    var bws = ProductieLijst.SelectedObjects.Cast<ProductieFormulier>().ToList();
                    var exc = new CreateExcelForm(bws);
                    exc.ShowDialog();
                }
            }
            catch (Exception e)
            {
                XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        #endregion MenuButton Methods

        #region MenuButton Events

        private void xstopb_Click(object sender, EventArgs e)
        {
            StopSelectedProducties();
        }

        private void xstartb_Click(object sender, EventArgs e)
        {
            StartSelectedProducties();
        }

        private void xopenproductieb_Click(object sender, EventArgs e)
        {
            ShowSelectedProducties();
        }

        private void xmeldgereedb_Click(object sender, EventArgs e)
        {
            MeldSelectedProductieGereed();
        }

        private void xdeelgereedmeldingenb_Click(object sender, EventArgs e)
        {
            ShowSelectedProdDeelGereedMeldingen();
        }

        private void xwerkplekkenb_Click(object sender, EventArgs e)
        {
            ShowSelectedWerkplekken();
        }

        private void xwijzigformb_Click(object sender, EventArgs e)
        {
            ShowSelectedProductieSettings();
        }

        private void xbewleverDatumToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowSelectedLeverdatum();
        }

        private void aantalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowSelectedAantal();
        }

        private void xbewaantalGemaaktToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSelectedProductieAantalGemaakt();
        }

        private void xbewnotitieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSelectedNotitie();
        }

        private void xwerktijdenb_Click(object sender, EventArgs e)
        {
            ShowSelectedProductieWerktijd();
        }

        private void xonderbrekingb_Click(object sender, EventArgs e)
        {
            ShowSelectedStoringen();
        }

        private void xaantalgemaaktb_Click(object sender, EventArgs e)
        {
            ShowSelectedProductieAantalGemaakt();
        }

        private void xmaterialenb_Click(object sender, EventArgs e)
        {
            ShowSelectedMaterials();
        }

        private void xafkeurb_Click(object sender, EventArgs e)
        {
            ShowSelectedAfkeur();
        }

        private void xverwijderb_Click(object sender, EventArgs e)
        {
            RemoveSelectedProducties();
        }

        private void xzetterugb_Click(object sender, EventArgs e)
        {
            UndoSelectedProducties();
        }

        private void xproductieInfob_Click(object sender, EventArgs e)
        {
            ShowSelectedProductieInfo();
        }

        private void vouwAllGroepenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewGroup group in ProductieLijst.Groups)
                ((OLVGroup) group.Tag).Collapsed = true;
        }

        private void ontvouwAlleGroepenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewGroup group in ProductieLijst.Groups)
                ((OLVGroup) group.Tag).Collapsed = false;
        }

        private void xfiltercontainer_Click(object sender, EventArgs e)
        {
            xfiltercontainer.ShowDropDown();
        }

        private void xfiltercontainer_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem is ToolStripMenuItem b)
            {
                b.Checked = !b.Checked;
                UpdateProductieList(true);
            }
        }

        private void xexportexcel_Click(object sender, EventArgs e)
        {
            ShowExportSelection();
        }

        private void xaanbevolenpersb_Click(object sender, EventArgs e)
        {
            ShowSelectedAanbevolenPersonen();
        }

        #endregion MenuButton Events

        #region FilterStrip

        private void LoadFilter()
        {
            var valid = Manager.Opties != null && Manager.Opties.ProductieWeergaveFilters.Length > 0;
            foreach (var tb in xfiltercontainer.DropDownItems.Cast<ToolStripMenuItem>())
                if (!valid)
                    tb.Checked = false;
                else
                    tb.Checked =
                        Manager.Opties.ProductieWeergaveFilters.Any(t => tb.Tag.ToString() == ((int) t).ToString());
        }

        public void InitFilterStrips()
        {
            //verwijder alle gekozen filters.
            var items = xfiltersStrip.Items.Cast<ToolStripMenuItem>().ToList();
            ToolStripMenuItem menuitem = null;
            for (var i = 0; i < items.Count; i++)
                if (items[i].Tag != null)
                {
                    xfiltersStrip.Items.Remove(items[i]);
                }
                else if (items[i].DropDownItems.Count > 0)
                {
                    menuitem = items[i];
                    var dropitems = items[i].DropDownItems.Cast<ToolStripItem>().Where(x => x.Tag != null).ToList();
                    for (var j = 0; j < dropitems.Count; j++)
                        menuitem.DropDownItems.Remove(dropitems[j]);
                }

            if (!_enableEntryFilter) return;
            //verwijder alle toegevoegde filters
            //items = xfiltersStripItem.DropDownItems.Cast<ToolStripItem>().Where(x => x.Tag != null).ToList();
            //for (int i = 0; i < items.Count; i++)
            //    xfiltersStripItem.DropDownItems.Remove(items[i]);
            //Voeg toe alle filters indien mogelijk
            if (Manager.Opties?.Filters == null || menuitem == null) return;
            foreach (var f in Manager.Opties.Filters)
            {
                var xitem = new ToolStripMenuItem(f.Name) {Image = Resources.add_1588, Tag = f};
                menuitem.DropDownItems.Add(xitem);
                if (f.Enabled)
                    AddFilterToolstripItem(f, false);
            }
        }

        private void xfiltersStripItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag == null)
            {
                var xf = new FilterEditor();
                if (xf.ShowDialog() == DialogResult.OK)
                    Manager.OnFilterChanged(this);
                //InitFilterStrips();
                //UpdateProductieList();
                //OnFilterChanged();
                return;
            }

            if (e.ClickedItem.Tag is Rpm.Misc.Filter filter)
            {
                filter.Enabled = true;
                Manager.OnFilterChanged(this);
                //if (AddFilterToolstripItem(filter, true))
                //{
                //    filter.Enabled = true;
                //    UpdateProductieList();
                //    OnFilterChanged();
                //}
            }
        }

        private bool AddFilterToolstripItem(Rpm.Misc.Filter filter, bool docheck)
        {
            if (filter.Enabled && docheck) return false;
            var ts = new ToolStripMenuItem(filter.Name) {Image = Resources.delete_1577, Tag = filter};
            ts.Click += Ts_Click;
            xfiltersStrip.Items.Add(ts);
            return true;
        }

        private void Ts_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem {Tag: Rpm.Misc.Filter filter } ts)
            {
                filter.Enabled = false;
                Manager.OnFilterChanged(this);
                //ts = xfiltersStrip.Items.Cast<ToolStripItem>().FirstOrDefault(x =>
                //    string.Equals(x.Text, filter.Name, StringComparison.CurrentCultureIgnoreCase));
                //if (ts != null)
                //    xfiltersStrip.Items.Remove(ts);
                //UpdateProductieList();
                //OnFilterChanged();
            }
        }

        #endregion FilterStrip

        #region ProductieLijst Events

        private void xproductieLijst_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedItem = ProductieLijst.SelectedObject;
            SetButtonEnable();
            OnSelectedItemChanged();
        }
        
        private void xproductieLijst_DoubleClick(object sender, EventArgs e)
        {
            ShowSelectedProducties();
        }

        private void xproductieLijst_CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            try
            {
                if (e.Model is ProductieFormulier prod)
                {
                    e.Title = e.SubItem.Text;
                    e.Text = $"[{prod.ArtikelNr}]{prod.ProductieNr}";
                    e.Text += $"\n{prod.Omschrijving}";
                    if (prod.Bewerkingen?.Length > 0)
                    {
                        var openstaand = new List<Storing>();
                        foreach (var bew in prod.Bewerkingen)
                        {
                            var open = bew.GetAlleStoringen(true);
                            if (open?.Length > 0) openstaand.AddRange(open);
                        }

                        if (openstaand.Count > 0)
                        {
                            var xs = openstaand.Count == 1
                                ? "staat 1 onderbreking"
                                : $"staan {openstaand.Count} onderbrekeningen";
                            var xstmessage = "Let Op!\n" +
                                             $"Er {xs} open!";
                            e.Text += "\n\n" + xstmessage;
                        }
                    }
                }
                else if (e.Model is Bewerking bew)
                {
                    e.Title = e.SubItem.Text;
                    e.Text = $"[{bew.ArtikelNr}]{bew.Path}";
                    e.Text += $"\n{bew.Omschrijving}";
                    var open = bew.GetAlleStoringen(true);

                    if (open?.Length > 0)
                    {
                        var xs = open.Length == 1 ? "staat 1 onderbreking" : $"staan {open.Length} onderbrekeningen";
                        var xstmessage = "Let Op!\n" +
                                         $"Er {xs} open!";
                        e.Text += "\n\n" + xstmessage;
                    }
                }
            }
            catch
            {
            }
        }

        private void xproductieLijst_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // Create a DataObject that holds the ListViewItem.
            if (sender is ObjectListView olv)
                olv.DoDragDrop(new DataObject("Producties", olv.SelectedObjects),
                    DragDropEffects.Link);
        }

        private void xproductieLijst_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        #endregion ProductieLijst Events

        #region Events

        public event EventHandler SelectedItemChanged;
        public event EventHandler ItemCountChanged;

        protected virtual void OnSelectedItemChanged()
        {
            SelectedItemChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnItemCountChanged()
        {
            ItemCountChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion Events
    }
}