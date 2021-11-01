using BrightIdeasSoftware;
using Forms;
using MetroFramework.Controls;
using ProductieManager.Forms;
using ProductieManager.Properties;
using ProductieManager.Rpm.ExcelHelper;
using ProductieManager.Rpm.Misc;
using ProductieManager.Rpm.Settings;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Various;
using static Forms.RangeCalculatorForm;
using Comparer = Rpm.Various.Comparer;
using Timer = System.Timers.Timer;

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
            ProductieLijst.CustomSorter = delegate (OLVColumn column, SortOrder order)
            {
                if (order != SortOrder.None)
                {
                    ProductieLijst.ListViewItemSorter = new Comparer(order, column);
                    //ArrayList objects = (ArrayList)ProductieLijst.Objects;
                    //objects.Sort(new Comparer(order, column));
                }
            };
            // ProductieLijst.BeforeSearching += ProductieLijst_BeforeSearching;
            xsearch.ShowClearButton = true;
            EnableEntryFiltering = false;
            EnableFiltering = true;
            _WaitTimer = new Timer(100);
            _WaitTimer.Elapsed += _WaitTimer_Elapsed;
        }

        private void ProductieLijst_BeforeSearching(object sender, BeforeSearchingEventArgs e)
        {
            TextMatchFilter filter = TextMatchFilter.Contains(ProductieLijst, e.StringToFind);
            //this.ProductieLijst.ModelFilter = filter;
            this.ProductieLijst.DefaultRenderer = new HighlightTextRenderer(filter);
        }

        private void _WaitTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _WaitTimer?.Stop();
            if (Disposing || IsDisposed) return;
            BeginInvoke(new Action(() =>
            {
                _selectedItem = ProductieLijst.SelectedObject;
                SetButtonEnable();
                OnSelectedItemChanged();
            }));
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
        public ObjectListView ProductieLijst => xProductieLijst1;

        public string ListName { get; set; }
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

        public bool IsSyncing { get; private set; }

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
        protected Timer _WaitTimer;

        #region Init Methods

        /// <summary>
        /// Laadt producties
        /// </summary>
        /// <param name="producties">De producties om te laden</param>
        /// <param name="bewerkingen">Laad alleen de bewerkingen</param>
        /// <param name="initlist">Of je ook de lijst events en afbeeldingen wilt</param>
        /// <param name="loadproducties">Of je ook gelijk de producties wilt laten</param>
        /// <param name="reload">Of je de lijst wilt refreshen</param>
        public void InitProductie(List<ProductieFormulier> producties, bool bewerkingen, bool initlist,
            bool loadproducties, bool reload)
        {
            Producties = producties;
            InitProductie(bewerkingen, true, true, initlist, loadproducties, reload);
        }

        public void InitProductie(List<Bewerking> bewerkingen, bool initlist, bool loadproducties, bool reload)
        {
            Bewerkingen = bewerkingen;
            InitProductie(true, true, true, initlist, loadproducties, reload);
        }

        public void InitProductie(bool bewerkingen, bool enablefilter, bool customlist, bool initlist,
            bool loadproducties, bool reload)
        {
            EnableEntryFiltering = enablefilter;
            CustomList = customlist;
            IsBewerkingView = bewerkingen;
            if (initlist)
                try
                {
                    CanLoad = true;
                    BeginInvoke(new MethodInvoker(() =>
                    {
                        xsearch.TextChanged -= xsearchbox_TextChanged;
                        xsearch.TextChanged += xsearchbox_TextChanged;
                        InitImageList();
                        InitColumns();
                        IsLoaded = true;
                    }));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            if (loadproducties)
                UpdateProductieList(reload,true);
        }

        private void SetButtonEnable()
        {
            try
            {
                var enable1 = ProductieLijst.SelectedObjects is {Count: 1};
                var enable2 = ProductieLijst.SelectedObjects is {Count: > 1};
                var enable3 = enable1 || enable2;
                var acces1 = Manager.LogedInGebruiker is {AccesLevel: >= AccesType.ProductieBasis};
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

                var isprod = !IsBewerkingView;
                var verwijderd1 = enable3 && isprod
                    ? bws.All(x => x.State == ProductieState.Verwijderd)
                    : bws.Any(x => x.State == ProductieState.Verwijderd);
                var verwijderd2 = enable3 && bws.Any(x => x.State != ProductieState.Verwijderd);
                var isgereed1 = enable3 && isprod
                    ? bws.All(x => x.State == ProductieState.Gereed)
                    : bws.Any(x => x.State == ProductieState.Gereed);
                var isgereed2 = enable3 && bws.Any(x => x.State != ProductieState.Gereed);
                var isgestart = enable3 && bws.Any(x => x.State == ProductieState.Gestart);
                var isgestopt = enable3 && bws.Any(x => x.State == ProductieState.Gestopt);
                var haspdf = bws.Count > 0 && bws[0].Parent != null && bws[0].Parent.ContainsProductiePdf();
                //var nietbemand = bws.Any(x => !x.IsBemand);
                xbewerkingeninfob.Enabled = ProductieLijst.Items.Count > 0;
                xwijzigproductieinfo.Enabled = enable3 && acces1;
                xtoonpdfb.Enabled = haspdf;
                xverpakkingb.Enabled = enable1;
                xopenproductieb.Enabled = enable3 && acces1 && verwijderd2;
                xstartb.Enabled = acces1 && isgestopt;
                xstopb.Enabled = acces1 && isgestart;
                xwijzigformb.Enabled = enable1 && acces1;
                xwerktijdenb.Enabled = acces1 && enable1;
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
                xtoolstripbehwerktijden.Enabled = acces1 && enable1;
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
                zoekToolStripMenuItem.Enabled = enable3;
                verpakkingsInstructieToolStripMenuItem.Enabled = enable1;
                verwijderFiltersToolStripMenuItem.Visible = Manager.Opties?.Filters?.Any(x =>
                    x.IsTempFilter && x.ListNames.Any(s =>
                        string.Equals(s, ListName, StringComparison.CurrentCultureIgnoreCase))) ?? false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void InitEvents()
        {
            Manager.OnSettingsChanged += _manager_OnSettingsChanged;
            Manager.OnSettingsChanging += Manager_OnSettingsChanging;
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
            Manager.OnColumnsSettingsChanged += Xcols_OnColumnsSettingsChanged;
        }

        private void Manager_OnSettingsChanging(object instance, ref UserSettings settings, ref bool cancel)
        {
            try
            {
                if (this.Disposing || IsDisposed) return;
                var x = settings;
                this.Invoke(new Action(() => SaveColumns(false, x, false)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DetachEvents()
        {
            Manager.OnSettingsChanged -= _manager_OnSettingsChanged;
            Manager.OnSettingsChanging -= Manager_OnSettingsChanging;
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
            Manager.OnColumnsSettingsChanged -= Xcols_OnColumnsSettingsChanged;
        }

        private bool _iswaiting;

        /// <summary>
        ///     Toon laad scherm
        /// </summary>
        public void SetWaitUI()
        {
            if (_iswaiting) return;
            _iswaiting = true;
            Task.Run(async () =>
            {
                try
                {
                    var valid = false;
                    Invoke(new MethodInvoker(() => valid = !IsDisposed));
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
                        Invoke(new MethodInvoker(() => valid = !IsDisposed));
                        if (!valid) break;
                    }

                    
                }
                catch (Exception e)
                {
                }
                xloadinglabel.Invoke(new MethodInvoker(() => { xloadinglabel.Visible = false; }));
            });
        }

        /// <summary>
        ///     verberg het laad scherm
        /// </summary>
        public void StopWait()
        {
            _iswaiting = false;
        }

        public bool SaveColumns(bool savesettings, UserSettings opties, bool raisesettingchanged)
        {
            if (opties == null) return false;
            opties.ExcelColumns ??= new List<ExcelSettings>();
            var xcols = opties.ExcelColumns.FirstOrDefault(x =>
                x.ListNames.Any(listname=> string.Equals(listname, ListName, StringComparison.CurrentCultureIgnoreCase)));
            if (xcols == null)
            {
                xcols = opties.ExcelColumns.FirstOrDefault(x =>
                    string.Equals(x.Name, ListName, StringComparison.CurrentCultureIgnoreCase));
                if (xcols == null)
                {
                    xcols = new ExcelSettings(ListName, ListName,false);
                    opties.ExcelColumns.Add(xcols);
                }
                else xcols.SetSelected(true, ListName);
            }
            var xcurcols = ProductieLijst.AllColumns.Cast<OLVColumn>();
            xcols.Columns ??= new List<ExcelColumnEntry>();
            var xsorted = ProductieLijst.PrimarySortColumn;
            foreach (var col in xcurcols)
            {
                var xcol = xcols.Columns.FirstOrDefault(x =>
                    string.Equals(col.Name, x.Naam, StringComparison.CurrentCultureIgnoreCase));
                if (xcol == null)
                {
                    xcol = new ExcelColumnEntry(col.Name);
                    //if (col.DisplayIndex < xcols.Columns.Count)
                    //    xcols.Columns.Insert(col.DisplayIndex, xcol);
                    //else
                        xcols.Columns.Add(xcol);
                }

                //if (col.IsVisible)
                xcol.ColumnIndex = col.LastDisplayIndex;
                xcol.Naam = col.AspectName;
                xcol.ColumnBreedte = col.Width;
                xcol.IsVerborgen = !col.IsVisible;
                xcol.ColumnFormat = col.AspectToStringFormat;
                if (string.Equals(xsorted?.AspectName, xcol.Naam, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (ProductieLijst.PrimarySortOrder == SortOrder.None)
                    {
                        xcol.Sorteer = SorteerType.None;
                    }
                    else
                    {
                        xcols.Columns.ForEach(x => x.Sorteer = SorteerType.None);
                        switch (ProductieLijst.PrimarySortOrder)
                        {
                            case SortOrder.Ascending:
                                xcol.Sorteer = SorteerType.Ascending;
                                break;
                            case SortOrder.Descending:
                                xcol.Sorteer = SorteerType.Descending;
                                break;
                        }
                    }
                  
                }
                
            }
            xcols.ShowGroups = ProductieLijst.ShowGroups;
            xcols.Columns = xcols.Columns.OrderBy(x => x.ColumnIndex).ToList();
            if (raisesettingchanged)
                Manager.ColumnsSettingsChanged(xcols);
            //xcols.ReIndexColumns();
            if (savesettings)
                opties.Save("Columns Opgeslagen!", false, false, false);
            return true;
        }


        private OLVColumn SetColumnsInfo(ref OLVColumn column, ExcelColumnEntry columnEntry)
        {
            try
            {
                var xcol = columnEntry;
                var xDescription = typeof(IProductieBase).GetPropertyDescription(xcol.Naam);
                column.ToolTipText = xDescription;
                column.Tag = xcol;
               // column.Renderer = new ColumnRenderer();
                column.Groupable = true;
                column.Name = xcol.Naam;
                column.Text = xcol.ColumnText;
                column.AspectToStringFormat = xcol.ColumnFormat;
                column.Width = xcol.ColumnBreedte;
                if (xcol.AutoSize)
                    column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                column.GroupFormatter = (group, parms) =>
                {
                    parms.GroupComparer = Comparer<OLVGroup>.Create((x, y) =>
                        Comparer.Compare(x, y, parms.PrimarySortOrder,
                            parms.PrimarySort ?? parms.SecondarySort));
                };
               
                switch (xcol.Naam.ToLower())
                {
                    case "artikelnr":
                        column.ImageGetter = ImageGetter;
                        column.LastDisplayIndex = 0;
                        column.IsVisible = true;
                        return column;
                    case "verpakkingsinstructies":
                    case "verpakkinginstries":
                        column.AspectGetter = VerpakkingsInstructiesGetter;
                        break;
                    case "note":
                        column.AspectGetter = NoteGetter;
                        break;
                    case "gereednote":
                        column.AspectGetter = GereedNoteGetter;
                        break;
                    case "personen":
                        column.AspectGetter = PersonenGetter;
                        break;
                    case "lastchanged":
                        column.AspectGetter = LastChangedGetter;
                        break;
                    case "Omschrijving":
                        column.GroupKeyGetter = GroupGetter;
                        break;
                    case "tijdover":
                        column.AspectGetter = BewerkingTijdOverGetter;
                        break;
                    case "laatstaantalupdate":
                        column.AspectGetter = y =>
                        {
                            if (y is IProductieBase pr)
                                return pr.LaatstAantalUpdate.ToString(8, "over {0} {1}", "{0} {1} geleden", false);
                            return "N.V.T.";
                        };
                        break;
                    case "verwachtleverdatum":
                        column.AspectGetter = y =>
                        {
                            if (y is IProductieBase bw)
                            {
                                if (bw.State == ProductieState.Gereed)
                                    return "Is gereed!";
                                if (bw.TotaalGemaakt >= bw.Aantal)
                                    return "Aantal Behaald!";
                                return bw.VerwachtLeverDatum.ToString(8, "over {0} {1}", "{0} {1} geleden", false);
                            }
                            return "N.V.T.";
                        };
                        break;
                    case "leverdatum":
                        column.AspectGetter = y =>
                        {
                            if (y is IProductieBase pr)
                                return pr.LeverDatum.ToString(8, "over {0} {1}", "{0} {1} geleden", false);
                            return "N.V.T.";
                        };
                        break;
                }

                column.LastDisplayIndex = xcol.ColumnIndex >= 0 &&
                                          xcol.ColumnIndex <= ProductieLijst.Columns.Count - 1
                    ? xcol.ColumnIndex
                    : ProductieLijst.Columns.Count - 1;
                column.IsVisible = !xcol.IsVerborgen;
                return column;
            }
            catch (Exception e)
            {
                return column;
            }
        }

        private void InitColumns()
        {
            try
            {
                var xcols = Manager.Opties?.ExcelColumns?.FirstOrDefault(x =>
                    x.ListNames.Any(
                        listname => string.Equals(listname, ListName, StringComparison.CurrentCultureIgnoreCase)));
                OLVColumn xsort = null;
                if (xcols == null)
                {
                    xcols = Manager.Opties?.ExcelColumns?.FirstOrDefault(x =>
                        string.Equals(x.Name, ListName, StringComparison.CurrentCultureIgnoreCase));
                    if (xcols != null)
                    {
                        xcols.SetSelected(true, ListName);
                    }
                    else if (Manager.Opties?.ExcelColumns != null)
                    {
                        xcols = ExcelSettings.CreateSettings(ListName,false);
                        Manager.Opties.ExcelColumns.Add(xcols);
                    }
                }
                if (xcols?.Columns != null)
                {
                    if (xcols.Columns.Count == 0)
                    {
                        xcols.SetDefaultColumns();
                    }

                    ProductieLijst.ShowGroups = xcols.ShowGroups;
                    ProductieLijst.BeginUpdate();
                    
                    var xcurcols = ProductieLijst.AllColumns.Cast<OLVColumn>().ToList();
                    for (int i = 0; i < xcurcols.Count; i++)
                    {
                        var xcurcol = xcurcols[i];
                        var xcol = xcols.Columns.FirstOrDefault(x => string.Equals(x.Naam, xcurcol.AspectName));
                        
                        if (xcol == null)
                        {
                            ProductieLijst.AllColumns.RemoveAt(i);
                            xcurcols.RemoveAt(i--);
                            continue;
                        }

                        xcurcol = SetColumnsInfo(ref xcurcol, xcol);
                        if (xcol.Sorteer != SorteerType.None)
                        {
                            xsort = xcurcol;
                            xcurcol.Sortable = true;
                        }
                    }

                    var xtoadd = xcols.Columns.Where(x =>
                            !xcurcols.Exists(c =>
                                string.Equals(c.Name, x.Naam, StringComparison.CurrentCultureIgnoreCase)))
                        .ToArray();
                    foreach (var xcol in xtoadd)
                    {

                        var col = new OLVColumn(xcol.ColumnText, xcol.Naam);
                        col = SetColumnsInfo(ref col, xcol);
                        if (xcol.Sorteer != SorteerType.None)
                        {
                            xsort = col;
                            col.Sortable = true;
                        }
                        ProductieLijst.AllColumns.Add(col);
                    }

                    ProductieLijst.EndUpdate();
                }
                else
                {
                    foreach (var col in ProductieLijst.AllColumns.Cast<OLVColumn>())
                    {
                        col.Groupable = true;
                        col.Name = col.AspectName;
                        col.ImageGetter = ImageGetter;
                        col.GroupFormatter = (group, parms) =>
                        {
                            parms.GroupComparer = Comparer<OLVGroup>.Create((x, y) =>
                                Comparer.Compare(x, y, parms.PrimarySortOrder,
                                    parms.PrimarySort ?? parms.SecondarySort));
                        };
                    }
                }
                ProductieLijst.RebuildColumns();
                if (xsort?.Tag is ExcelColumnEntry ec)
                {
                    ProductieLijst.PrimarySortColumn = xsort;
                
                    if (ec.Sorteer == SorteerType.Ascending)
                        ProductieLijst.PrimarySortOrder = SortOrder.Ascending;
                    else
                        ProductieLijst.PrimarySortOrder = SortOrder.Descending;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            //((OLVColumn)xproductieLijst.Columns["Omschrijving"]).GroupKeyGetter = BewerkingGroupGetter;
        }

        private void Xcols_OnColumnsSettingsChanged(object sender, EventArgs e)
        {
            if (sender is ExcelSettings settings)
            {
                if (settings.IsUsed(ListName))
                    this.BeginInvoke(new Action(InitColumns));
            }
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

            var imgscale = 1.75;
            //zelfde afbeeldingen, maar dan met de note icon
            ximagelist.Images.Add(img.CombineImage(Resources.Note_msgIcon_32x32, ContentAlignment.TopLeft,
                imgscale)); // regular document
            ximagelist.Images.Add(img.CombineImage(Resources.new_25355, 2)
                .CombineImage(Resources.Note_msgIcon_32x32, ContentAlignment.TopLeft, imgscale)); //new document
            ximagelist.Images.Add(img.CombineImage(Resources.Warning_36828, 2)
                .CombineImage(Resources.Note_msgIcon_32x32, ContentAlignment.TopLeft, imgscale)); //warning document
            ximagelist.Images.Add(img.CombineImage(Resources.play_button_icon_icons_com_60615, 2.5)
                .CombineImage(Resources.Note_msgIcon_32x32, ContentAlignment.TopLeft, imgscale)); //play document
            ximagelist.Images.Add(img.CombineImage(Resources.delete_1577, 2)
                .CombineImage(Resources.Note_msgIcon_32x32, ContentAlignment.TopLeft, imgscale)); //deleted document
            ximagelist.Images.Add(img.CombineImage(Resources.check_1582, 2)
                .CombineImage(Resources.Note_msgIcon_32x32, ContentAlignment.TopLeft, imgscale)); // checked document
        }

        private int GetProductieImageIndex(IProductieBase productie)
        {
            if (productie == null) return 0;
            var xbase = string.IsNullOrEmpty(productie.Note?.Notitie) ? 0 : 6;
            switch (productie.State)
            {
                case ProductieState.Gestopt:
                    if (productie.IsNieuw)
                        return 1 + xbase;
                    if (productie.TeLaat)
                        return 2 + xbase;
                    return 0 + xbase;
                case ProductieState.Gestart:
                    return 3 + xbase;
                case ProductieState.Verwijderd:
                    return 4 + xbase;
                case ProductieState.Gereed:
                    return 5 + xbase;
            }

            return 0 + xbase;
        }

        private object ImageGetter(object sender)
        {
            return GetProductieImageIndex(sender as IProductieBase);
        }

        private object VerpakkingsInstructiesGetter(object sender)
        {
            if (sender is IProductieBase productie)
            {
                return productie.VerpakkingsInstructies?.VerpakkingType ?? "n.v.t.";
            }

            return "N.V.T.";
        }

        private object PersonenGetter(object sender)
        {
            if (sender is IProductieBase productie)
            {
                return string.Join(", ",
                    productie.Personen?.Select(x => x.PersoneelNaam).ToArray() ?? new string[] {"N.V.T."});
            }

            return "N.V.T.";
        }


        private object LastChangedGetter(object sender)
        {
            if (sender is IProductieBase productie)
            {
                string name = productie.LastChanged?.User;
                string xvalue = name == null ? "" : name + ": ";
                xvalue = $"[{productie.LastChanged?.TimeChanged}]{xvalue}{productie.LastChanged?.Change}";
                if (string.IsNullOrEmpty(xvalue)) return "N.V.T.";
                return xvalue;
            }

            return "N.V.T.";
        }

        private object NoteGetter(object sender)
        {
            if (sender is IProductieBase productie)
            {
                string name = productie.Note?.Naam;
                string xvalue = name == null ? "" : name + ": ";
                xvalue += productie.Note?.Notitie;
                if (string.IsNullOrEmpty(xvalue)) return "N.V.T.";
                return xvalue;
            }

            return "N.V.T.";
        }

        private object GereedNoteGetter(object sender)
        {
            if (sender is IProductieBase productie)
            {
                string name = productie.GereedNote?.Naam;
                string xvalue = name == null ? "" : name + ": ";
                xvalue += productie.GereedNote?.Notitie;
                if (string.IsNullOrEmpty(xvalue)) return "N.V.T.";
                return xvalue;
            }

            return "N.V.T.";
        }

        private object BewerkingTijdOverGetter(object sender)
        {
            if (sender is Bewerking bew) return bew.TijdOver() + " uur";
            if (sender is ProductieFormulier { Bewerkingen: { } } prod)
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
            if (Manager.Opties is {Bewerkingen: { }} && (Manager.Opties.ToonAllesVanBeide || Manager.Opties.ToonVolgensBewerkingen))
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
            if (!EnableSync || !Manager.Opties.AutoProductieLijstSync || IsSyncing || Disposing || IsDisposed) return;
            IsSyncing = true;
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    while (EnableSync && Manager.Opties.AutoProductieLijstSync && IsSyncing && !IsDisposed &&
                           !Disposing)
                    {
                        await Task.Delay(Manager.Opties.ProductieLijstSyncInterval);
                        if (!EnableSync || !Manager.Opties.AutoProductieLijstSync || !IsSyncing || IsDisposed ||
                            Disposing) break;
                        if (!_loadingproductielist && CanLoad)
                            UpdateProductieList(true, false);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        public string GetFilter()
        {
            return xsearch.Text.ToLower() == "zoeken..." ? "" : xsearch.Text;
        }

        private bool IsAllowd(Bewerking bewerking)
        {
            var filters = Manager.Opties.Filters;
            if (filters is { Count: > 0 })
            {
                bool xreturn = true;
                foreach (var filter in filters)
                {
                    if (filter.ListNames.Any(x =>
                        string.Equals(ListName, x, StringComparison.CurrentCultureIgnoreCase)))
                        xreturn &= filter.IsAllowed(bewerking, ListName);
                }

                return xreturn;
            }

            return true;
        }

        private bool IsAllowd(ProductieFormulier productie)
        {
            var filters = Manager.Opties.Filters;
            if (filters is { Count: > 0 })
            {
                if (productie?.Bewerkingen == null) return false;
                return productie.Bewerkingen.Any(IsAllowd);
            }

            return true;
        }

        public void UpdateProductieList(bool reload, bool showwaitui)
        {
            if (_loadingproductielist || Manager.Opties == null || !CanLoad) return;
            try
            {
                Invoke(new MethodInvoker(async () =>
                {
                    if (showwaitui)
                        SetWaitUI();
                    try
                    {
                        _loadingproductielist = true;
                        var xscroloffset = ProductieLijst.LowLevelScrollPosition;
                        InitFilterStrips();
                        var selected1 = ProductieLijst.SelectedObject;
                        var groups1 = ProductieLijst.Groups.Cast<ListViewGroup>().Select(t => (OLVGroup) t.Tag)
                            .Where(x => x.Collapsed)
                            .ToArray();
                        // Manager.Opties.ProductieWeergaveFilters = GetCurrentProductieViewStates();
                        var states = GetCurrentViewStates();

                        var xlistcount = ProductieLijst.Items.Count;
                        if (!IsBewerkingView)
                        {
                            if (CanLoad)
                            {
                                var xprods = !reload && CustomList && Producties != null
                                    ? Producties.Where(x => states.Any(x.IsValidState) && x.ContainsFilter(GetFilter()))
                                        .ToList()
                                    : Producties = await Manager.GetProducties(states, true, !IsBewerkingView);
                                if (ValidHandler != null)
                                    xprods = xprods.Where(x => IsAllowd(x) && ValidHandler.Invoke(x, GetFilter()))
                                        .ToList();
                                else
                                    xprods = xprods.Where(x => IsAllowd(x) && x.IsAllowed(GetFilter(), states, true))
                                        .ToList();
                                ProductieLijst.BeginUpdate();
                                ProductieLijst.SetObjects(xprods);
                                ProductieLijst.EndUpdate();
                            }
                        }
                        else if (CanLoad)
                        {
                            var bws = !reload && CustomList && Bewerkingen != null
                                ? Bewerkingen.Where(x => states.Any(x.IsValidState) && x.ContainsFilter(GetFilter()))
                                    .ToList()
                                : Bewerkingen = await Manager.GetBewerkingen(states, true);
                            if (ValidHandler != null)
                                bws = bws.Where(x => IsAllowd(x) && ValidHandler.Invoke(x, GetFilter()))
                                    .ToList();
                            else
                                bws = bws.Where(x => IsAllowd(x) && x.IsAllowed(GetFilter())).ToList();
                            ProductieLijst.BeginUpdate();
                            ProductieLijst.SetObjects(bws);
                            ProductieLijst.EndUpdate();
                        }

                        var xgroups = ProductieLijst.Groups.Cast<ListViewGroup>().ToList();
                        if (groups1.Length > 0)
                            for (var i = 0; i < xgroups.Count; i++)
                            {
                                var group = xgroups[i].Tag as OLVGroup;
                                if (@group == null)
                                    continue;
                                if (groups1.Any(t => !@group.Collapsed && t.Header == @group.Header))
                                    @group.Collapsed = true;
                            }

                        for (int i = 0; i < 3; i++)
                        {
                            if (ProductieLijst.LowLevelScrollPosition.X != xscroloffset.X ||
                                ProductieLijst.LowLevelScrollPosition.Y != xscroloffset.Y)
                            {
                                ProductieLijst.LowLevelScroll(xscroloffset.X - ProductieLijst.LowLevelScrollPosition.X, xscroloffset.Y - ProductieLijst.LowLevelScrollPosition.Y);
                                Application.DoEvents();
                            }
                            else break;
                        }

                        ProductieLijst.SelectedObject = selected1;
                        //ProductieLijst.SelectedItem?.EnsureVisible();
                        SetButtonEnable();
                        if (xlistcount != ProductieLijst.Items.Count)
                            OnItemCountChanged();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    _loadingproductielist = false;
                    if (EnableSync)
                        StartSync();
                    StopWait();
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

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
                    var isvalid = IsAllowd(form);
                    if (ValidHandler != null)
                        isvalid &= states.Any(form.IsValidState) && ValidHandler.Invoke(form, filter);
                    else isvalid &= form.IsAllowed(filter, states, true);

                    var xproducties = ProductieLijst.Objects?.Cast<ProductieFormulier>().ToList();
                    var xform = xproducties?.FirstOrDefault(x =>
                        string.Equals(x.ProductieNr, form.ProductieNr, StringComparison.CurrentCultureIgnoreCase));

                    if (xform == null && isvalid)
                    {
                        ProductieLijst.BeginUpdate();
                        ProductieLijst.AddObject(form);
                        if (Producties != null)
                        {
                            var index = Producties.IndexOf(form);
                            if (index > -1)
                                Producties[index] = form;
                            else Producties.Add(form);
                        }

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
                            else if (RemoveCustomItemIfNotValid)
                            {
                                Producties.RemoveAt(index);
                                changed = true;
                            }
                        }
                        else if (isvalid)
                        {
                            Producties.Add(form);
                            changed = true;
                        }
                    }

                    xreturn = isvalid;
                }
                else
                {
                    //this.Invoke(new Action(() =>
                    //{
                    //    changed = UpdateBewerking(form, null, states, filter);
                    //}));
                    changed = UpdateBewerking(form, null, states, filter);
                }

                if (changed)
                    OnItemCountChanged();
                SetButtonEnable();
                return xreturn;
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine(@"Disposed!");
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
                    BeginInvoke(new Action(async () =>
                    {
                        var states = GetCurrentViewStates();
                        if (Producties is {Count: > 0})
                            for (var i = 0; i < Producties.Count; i++)
                            {
                                var prod = Producties[i];
                                var xprod = await Manager.Database.GetProductie(prod.ProductieNr);
                                if (onlywhilesyncing && !IsSyncing) break;
                                if (IsDisposed || Disposing) break;
                                var valid = xprod != null && IsAllowd(xprod);
                                if (valid)
                                {
                                    if (ValidHandler != null)
                                        valid = states.Any(xprod.IsValidState) &&
                                                ValidHandler.Invoke(xprod, null);
                                    else
                                        valid = states.Any(x => xprod.IsValidState(x)) && IsAllowd(xprod) &&
                                                xprod.IsAllowed(null);
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

                        if (Bewerkingen is {Count: > 0})
                            for (var i = 0; i < Bewerkingen.Count; i++)
                            {
                                var bew = Bewerkingen[i];
                                var xbew = Werk.FromPath(bew.Path)?.Bewerking;
                                if (onlywhilesyncing && !IsSyncing) break;
                                if (IsDisposed || Disposing) break;
                                var valid = xbew != null && IsAllowd(xbew);
                                if (valid)
                                {
                                    if (ValidHandler != null)
                                        valid &= states.Any(xbew.IsValidState) && ValidHandler.Invoke(xbew, null);
                                    else valid &= states.Any(x => xbew.IsValidState(x)) && xbew.IsAllowed(null);
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
            if (!IsBewerkingView || form == null || Disposing || IsDisposed) return false;
            var changed = false;
            try
            {
                var xbewerkingen = bewerkingen ?? ProductieLijst.Objects?.Cast<Bewerking>().ToList();
                states ??= GetCurrentViewStates();
                // bool checkall = xbewerkingen != null && !xbewerkingen.Any(x=> string.Equals(x.ProductieNr, form.ProductieNr, StringComparison.CurrentCultureIgnoreCase));
                if (form?.Bewerkingen != null && form.Bewerkingen.Length > 0)
                    foreach (var b in form.Bewerkingen)
                    {
                        var isvalid = IsAllowd(b);
                        if (ValidHandler != null)
                            isvalid &= states.Any(b.IsValidState) && ValidHandler.Invoke(b, filter ?? GetFilter());
                        else isvalid &= states.Any(x => b.IsValidState(x)) && b.IsAllowed(filter ?? GetFilter());
                        var xb = xbewerkingen?.FirstOrDefault(x =>
                            string.Equals(x.Path, b.Path, StringComparison.CurrentCultureIgnoreCase));
                        if (xb == null && isvalid)
                        {
                            ProductieLijst.BeginUpdate();
                            ProductieLijst.AddObject(b);
                            if (Bewerkingen != null)
                            {
                                var index = Bewerkingen.IndexOf(b);
                                if (index > -1)
                                    Bewerkingen[index] = b;
                                else Bewerkingen.Add(b);
                            }

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
                        if (prods is { Length: > 0 })
                        {
                            ProductieLijst.RemoveObjects(prods);
                            Producties.RemoveAll(x =>
                                string.Equals(id, x.ProductieNr, StringComparison.CurrentCultureIgnoreCase));
                        }
                    }
                    else
                    {
                        var bws = ProductieLijst.Objects?.Cast<Bewerking>()?.Where(x =>
                            string.Equals(id, x.ProductieNr, StringComparison.CurrentCultureIgnoreCase)).ToArray();
                        if (bws is {Length: > 0})
                        {
                            ProductieLijst.RemoveObjects(bws);
                            Bewerkingen.RemoveAll(x =>
                                string.Equals(id, x.ProductieNr, StringComparison.CurrentCultureIgnoreCase));
                        }
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
                if (sender is ProductieListControl xc && string.Equals(xc.ListName, ListName))
                    BeginInvoke(new MethodInvoker(() => { UpdateProductieList(true,false); }));
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
                    InitColumns();
                    if (CanLoad)
                        UpdateProductieList(true,false);
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
                BeginInvoke(new Action(() => UpdateFormulier(changedform)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void _manager_OnBewerkingDeleted(object sender, Bewerking bew, string change, bool shownotification)
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
            if (IsDisposed || Disposing || !IsLoaded || string.IsNullOrEmpty(id)) return;
            DeleteID(id);
        }

        #endregion Manager Events

        #region Search

        private void xsearchbox_TextChanged(object sender, EventArgs e)
        {
            if (xsearch.Text.ToLower().Trim() != "zoeken..." && !_iswaiting) UpdateProductieList(false,false);
        }

        private void xsearch_Enter(object sender, EventArgs e)
        {
            if (sender is MetroTextBox {Text: "Zoeken..."} tb) tb.Text = "";
        }

        private void xsearch_Leave(object sender, EventArgs e)
        {
            if (sender is MetroTextBox tb)
                if (string.IsNullOrWhiteSpace(tb.Text))
                    tb.Text = "Zoeken...";
        }

        #endregion Search

        #region MenuButton Methods

        public void ShowProductieForm(ProductieFormulier pform, bool showform, Bewerking bewerking = null)
        {
            Manager.FormulierActie(new object[] { pform, bewerking }, MainAktie.OpenProductie);
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

                        async void Action()
                        {
                            for (var i = 0; i < bws.Length; i++)
                            {
                                var parent = bws[i].GetParent();
                                var werk = bws[i];
                                if (werk == null) continue;

                                if (werk.State != ProductieState.Gestart)
                                {
                                    if (werk.AantalActievePersonen == 0)
                                    {
                                        var pers = new PersoneelsForm(true);
                                        if (pers.ShowDialog(werk) == DialogResult.OK)
                                        {
                                            if (pers.SelectedPersoneel is {Length: > 0})
                                            {
                                                foreach (var per in pers.SelectedPersoneel) per.Klusjes?.Clear();
                                                var afzonderlijk = false;
                                                if (pers.SelectedPersoneel.Length > 1 && werk.IsBemand)
                                                {
                                                    var result = XMessageBox.Show($"Je hebt {pers.SelectedPersoneel.Length} medewerkers geselecteerd," + " wil je ze allemaal afzonderlijk indelen?", "Indeling", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                                    if (result == DialogResult.Cancel) return;
                                                    afzonderlijk = result == DialogResult.Yes;
                                                }

                                                if (!werk.IsBemand || !afzonderlijk)
                                                {
                                                    var klusui = new NieuwKlusForm(parent, pers.SelectedPersoneel, true, false, werk);
                                                    if (klusui.ShowDialog() != DialogResult.OK) return;
                                                    var pair = klusui.SelectedKlus.GetWerk(parent);
                                                    var prod = pair.Formulier;
                                                    werk = pair.Bewerking;
                                                    if (werk is {IsBemand: false} && klusui.SelectedKlus?.Tijden?.Count > 0)
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
                                                        var klusui = new NieuwKlusForm(parent, per, true, false, werk);
                                                        if (klusui.ShowDialog() != DialogResult.OK) break;
                                                        //klusui.Persoon.CopyTo(ref per);
                                                        var pair = klusui.SelectedKlus.GetWerk(parent);
                                                        var prod = pair.Formulier;
                                                        werk = pair.Bewerking;
                                                        //Bewerking werk = parent.Bewerkingen.FirstOrDefault(x => x.Path.ToLower() == xp.WerktAan.ToLower());
                                                        if (werk != null)
                                                        {
                                                            per.ReplaceKlus(klusui.SelectedKlus);
                                                            var wp = werk.WerkPlekken.FirstOrDefault(x => string.Equals(x.Naam, klusui.SelectedKlus.WerkPlek, StringComparison.CurrentCultureIgnoreCase));
                                                            if (wp == null)
                                                            {
                                                                wp = new WerkPlek(per, klusui.SelectedKlus.WerkPlek, werk);
                                                                werk.WerkPlekken.Add(wp);
                                                            }
                                                            else
                                                            {
                                                                wp.AddPersoon(per, werk);
                                                            }

                                                            if (wp.Werk.UpdateBewerking(null, $"{wp.Path} indeling aangepast", false, false).Result) werk.CopyTo(ref bws[i]);
                                                        }
                                                    }
                                                }

                                                if (werk != null)
                                                    await werk.StartProductie(true, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        await werk.StartProductie(true, true);
                                    }
                                }
                            }
                        }

                        mainform?.BeginInvoke(new Action( Action));
                    }
                    catch (Exception exception)
                    {
                        XMessageBox.Show(exception.Message,
                            "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
        }

        private static void StopBewerkingen(Bewerking[] bws)
        {
            var count = bws.Length;
            if (count > 0)
            {
                var xdic = new Dictionary<string, Task<bool>>();
                foreach (var bw in bws)
                    if (bw.State == ProductieState.Gestart)
                    {
                        var action = new Task<bool>(() => bw.StopProductie(true).Result);
                        xdic.Add($"Stoppen van '{bw.Path}'...", action);
                    }

                if (xdic.Count > 1)
                {
                    new MethodsForm(xdic).ShowDialog();
                }
                else
                {
                    var xaction = xdic.FirstOrDefault();
                    if (!xaction.IsDefault())
                        xaction.Value.Start();
                    // await xaction.Value;
                }
            }
        }

        private void ShowSelectedProducties()
        {
            if (Manager.LogedInGebruiker is {AccesLevel: >= AccesType.ProductieBasis})
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

        public void ShowSelectedRangeArtikelNr()
        {
            var bws = GetSelectedBewerkingen();
            if (bws.Count == 0) return;
            var done = new List<string>();
            for (var i = 0; i < bws.Count; i++)
            {
                if (done.Any(x => string.Equals(x, bws[i].ArtikelNr, StringComparison.CurrentCultureIgnoreCase)))
                    continue;
                var _calcform = new RangeCalculatorForm();
                var rf = new RangeFilter();
                rf.Enabled = true;
                rf.Criteria = bws[i].ArtikelNr;
                rf.Bewerking = bws[i].Naam;
                _calcform.Show(rf);
                done.Add(bws[i].ArtikelNr);
            }
        }

        public void ShowSelectedRangeBewerking()
        {
            var bws = GetSelectedBewerkingen();
            if (bws.Count == 0) return;
            var done = new List<string>();
            for (var i = 0; i < bws.Count; i++)
            {
                if (done.Any(x => string.Equals(x, bws[i].Naam, StringComparison.CurrentCultureIgnoreCase))) continue;
                var _calcform = new RangeCalculatorForm();
                var rf = new RangeFilter();
                rf.Enabled = true;
                rf.Bewerking = bws[i].Naam;
                _calcform.Show(rf);
                done.Add(bws[i].Naam);
            }
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
                try
                {
                    var items = ProductieLijst.SelectedObjects?.Cast<ProductieFormulier>().ToList();
                    if (items == null || items.Count == 0) return;
                    foreach (var form in items)
                    {
                        var ag = new AantalGemaaktUI();
                        ag.ShowDialog(form);
                        break;
                    }
                }
                catch (Exception e)
                {
                    XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
                }
            else
                try
                {
                    var items = ProductieLijst.SelectedObjects?.Cast<Bewerking>().ToList();
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
                                        $"[{bew.Path}] Aantal gemaakt aangepast van {oldaantal} naar {bew.AantalGemaakt}")
                                    .Wait();
                        }

                        break;
                    }
                }
                catch (Exception e)
                {
                    XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
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
                if (ProductieLijst.SelectedObject is Bewerking bew)
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
            var res = XMessageBox.Show("Wil je de geselecteerde producties helemaal verwijderen?\n\n" +
                                               "Click op 'Ja' als je helemaal van de database wilt verwijderen.\n" +
                                               "Click op 'Nee' als je alleen op een verwijderde status wilt te zetten.", "",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (res != DialogResult.Cancel)
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

                if (bws.Count == 0) return;
                var skip = res == DialogResult.No;
                var taskf = new Dictionary<string, Task<bool>>();
                for (var i = 0; i < bws.Count; i++)
                {
                    var pr = bws[i];
                    if (pr == null)
                        continue;
                    var action = new Task<bool>(() => pr.RemoveBewerking(skip,res == DialogResult.Yes).Result);
                    taskf.Add($"Bewerking '{pr.Path}' wordt verwijderd...", action);
                }

                if (taskf.Count > 1)
                {
                    new MethodsForm(taskf).ShowDialog();
                }
                else
                {
                    var xaction = taskf.FirstOrDefault();
                    if (!xaction.IsDefault())
                    {
                        xaction.Value.Start();
                        await xaction.Value;
                    }
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

                var taskf = new Dictionary<string, Task<bool>>();
                for (var i = 0; i < bws.Count; i++)
                {
                    var bw = bws[i];
                    if (bw == null) continue;
                    var action = new Task<bool>(() => bw.Undo().Result);
                    taskf.Add($@"'{bw.Path}' wordt terug gezet...", action);
                }

                if (taskf.Count > 1)
                {
                    new MethodsForm(taskf).ShowDialog();
                }
                else
                {
                    var xaction = taskf.FirstOrDefault();
                    if (!xaction.IsDefault())
                    {
                        xaction.Value.Start();
                        await xaction.Value;
                    }
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
                try
                {
                    var items = ProductieLijst.SelectedObjects?.Cast<ProductieFormulier>().ToList();
                    if (items == null || items.Count == 0) return;
                    var datum = items.FirstOrDefault()?.LeverDatum ?? DateTime.Now;
                    var x1 = items.Count == 1 ? items[0].Omschrijving : $"{items.Count} producties";
                    var msg = $"Wijzig leverdatum voor {x1}.";
                    var dc = new DatumChanger();
                    if (dc.ShowDialog(datum, msg) == DialogResult.OK)
                    {
                        var xdic = new Dictionary<string, Task<bool>>();
                        foreach (var form in items)
                        {
                            var change = $"[{form.ProductieNr}|{form.ArtikelNr}] Leverdatum gewijzigd!\n" +
                                         $"Van: {form.LeverDatum:dd MMMM yyyy HH:mm} uur\n" +
                                         $"Naar: {dc.SelectedValue:dd MMMM yyyy HH:mm} uur";
                            form.LeverDatum = dc.SelectedValue;
                            var action = new Task<bool>(() => form.UpdateForm(true, false, null, change).Result);
                            xdic.Add(change, action);
                        }

                        if (xdic.Count == 1)
                        {
                            var xaction = xdic.FirstOrDefault();
                            if (!xaction.IsDefault())
                            {
                                xaction.Value.Start();
                                await xaction.Value;
                            }
                        }
                        else if (xdic.Count > 1)
                        {
                            new MethodsForm(xdic).ShowDialog();
                        }
                    }

                    dc.Dispose();
                }
                catch (Exception e)
                {
                    XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
                }
            else
                try
                {
                    var items = ProductieLijst.SelectedObjects?.Cast<Bewerking>().ToList();
                    if (items == null || items.Count == 0) return;
                    var item = items.FirstOrDefault();
                    var datum = item?.LeverDatum ?? DateTime.Now;
                    var x1 = items.Count == 1 && item != null
                        ? $"[{item.ProductieNr}|{item.ArtikelNr}] {item.Naam}\n\n{item.Omschrijving}"
                        : $"{items.Count} producties";
                    var msg = $"Wijzig leverdatum voor {x1}.";
                    var dc = new DatumChanger();
                    if (dc.ShowDialog(datum, msg) == DialogResult.OK)
                    {
                        var xdic = new Dictionary<string, Task<bool>>();
                        foreach (var form in items)
                        {
                            var change = $"[{form.ProductieNr}|{form.ArtikelNr}] {form.Naam} Leverdatum gewijzigd!\n" +
                                         $"Van: {form.LeverDatum:dd MMMM yyyy HH:mm} uur\n" +
                                         $"Naar: {dc.SelectedValue:dd MMMM yyyy HH:mm} uur";
                            form.LeverDatum = dc.SelectedValue;
                            var action = new Task<bool>(() => form.UpdateBewerking(null, change).Result);
                            xdic.Add(change, action);
                        }

                        if (xdic.Count == 1)
                        {
                            var xaction = xdic.FirstOrDefault();
                            if (!xaction.IsDefault())
                            {
                                xaction.Value.Start();
                                await xaction.Value;
                            }
                        }
                        else if (xdic.Count > 1)
                        {
                            new MethodsForm(xdic).ShowDialog();
                        }
                    }

                    dc.Dispose();
                }
                catch (Exception e)
                {
                    XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
                }
        }

        private async void ShowSelectedAantal()
        {
            var prods = IsBewerkingView
                ? ProductieLijst.SelectedObjects?.Cast<Bewerking>().Select(x => x.Parent).Distinct().ToList()
                : ProductieLijst?.SelectedObjects?.Cast<ProductieFormulier>().ToList();

            if (prods == null || prods.Count == 0) return;
            var aantal = prods.FirstOrDefault()?.Aantal ?? 0;
            var x1 = prods.Count == 1 ? prods[0].Omschrijving : $"{prods.Count} producties";
            var msg = $"Wijzig aantal voor {x1}.";
            var dc = new AantalChanger();
            if (dc.ShowDialog(aantal, msg) == DialogResult.OK)
            {
                var xdic = new Dictionary<string, Task<bool>>();
                foreach (var form in prods)
                {
                    var change = $"[{form.ProductieNr}|{form.ArtikelNr}] Aantal gewijzigd!\n" +
                                 $"Van: {form.Aantal}\n" +
                                 $"Naar: {dc.Aantal}";
                    form.Aantal = dc.Aantal;
                    var action = new Task<bool>(() => form.UpdateForm(true, false, null, change).Result);
                    xdic.Add(change, action);
                }

                if (xdic.Count == 1)
                {
                    var xaction = xdic.FirstOrDefault();
                    if (!xaction.IsDefault())
                    {
                        xaction.Value.Start();
                        await xaction.Value;
                    }
                }
                else if (xdic.Count > 1)
                {
                    new MethodsForm(xdic).ShowDialog();
                }
            }
        }

        private async void ShowSelectedNotitie()
        {
            if (!IsBewerkingView)
                try
                {
                    var items = ProductieLijst.SelectedObjects?.Cast<ProductieFormulier>().ToList();
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

                        break;
                    }
                }
                catch (Exception e)
                {
                    XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
                }
            else
                try
                {
                    var items = ProductieLijst.SelectedObjects?.Cast<Bewerking>().ToList();
                    if (items == null || items.Count == 0) return;
                    foreach (var bew in items)
                    {
                        var xtxtform = new NotitieForms(bew.Note, bew)
                        {
                            Title =
                                $"Notitie voor [{bew.ProductieNr}, {bew.ArtikelNr}] {bew.Naam} van {bew.Omschrijving}"
                        };
                        if (xtxtform.ShowDialog() == DialogResult.OK)
                        {
                            bew.Note = xtxtform.Notitie;
                            await bew.UpdateBewerking(null,
                                $"[{bew.ProductieNr}, {bew.ArtikelNr}] {bew.Naam} Notitie Gewijzigd");
                        }

                        break;
                    }
                }
                catch (Exception e)
                {
                    XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
                }
        }

        public ViewState[] GetCurrentViewStates()
        {
            var states = new List<ViewState>();
            var items = xfiltercontainer.DropDownItems;
            foreach (var tb in items.Cast<ToolStripMenuItem>())
                if (tb.Checked)
                    if (int.TryParse(tb.Tag.ToString(), out var xstate))
                    {
                        var state = (ViewState)xstate;
                        states.Add(state);
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
                else
                {
                    states.Add(ViewState.Alles);
                }
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

        public List<Bewerking> GetSelectedBewerkingen()
        {
            var xreturn = new List<Bewerking>();
            try
            {
                if (!IsBewerkingView)
                {
                    foreach (var o in ProductieLijst.SelectedObjects.Cast<ProductieFormulier>().ToArray())
                        foreach (var bw in o.Bewerkingen)
                            if (bw.IsAllowed())
                                xreturn.Add(bw);
                }
                else
                {
                    var bws = ProductieLijst.SelectedObjects.Cast<Bewerking>().ToArray();
                    foreach (var bw in bws)
                        if (bw.IsAllowed())
                            xreturn.Add(bw);
                }
            }
            catch (Exception e)
            {
                XMessageBox.Show(e.Message, "Fout", MessageBoxIcon.Error);
            }

            return xreturn;
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
                ((OLVGroup)group.Tag).Collapsed = true;
        }

        private void ontvouwAlleGroepenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewGroup group in ProductieLijst.Groups)
                ((OLVGroup)group.Tag).Collapsed = false;
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
                UpdateProductieList(true,true);
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

        private void opArtikelNrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSelectedRangeArtikelNr();
        }

        private void opBewerkingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSelectedRangeBewerking();
        }

        private void xbewerkingeninfob_Click(object sender, EventArgs e)
        {
            if (IsBewerkingView)
            {
                var bws = ProductieLijst.Objects?.Cast<Bewerking>().ToList();
                if (bws is { Count: > 0 }) new ProductieInfoForm(bws).ShowDialog();
            }
            else
            {
                var prods = ProductieLijst.Objects?.Cast<ProductieFormulier>().ToList();
                if (prods is { Count: > 0 })
                {
                    var bws = new List<Bewerking>();
                    for (var i = 0; i < prods.Count; i++)
                    {
                        var prod = prods[i];
                        if (prod.Bewerkingen is { Length: < 1 }) continue;
                        foreach (var bw in prod.Bewerkingen)
                        {
                            var flag = bw.IsAllowed() && IsAllowd(bw);
                            if (ValidHandler != null)
                                flag &= ValidHandler.Invoke(bw, null);
                            if (flag)
                                bws.Add(bw);
                        }
                    }

                    new ProductieInfoForm(bws).ShowDialog();
                }
            }
        }

        private void xverpakkingb_Click(object sender, EventArgs e)
        {
            if (ProductieLijst.SelectedObject is IProductieBase productie)
                new VerpakkingInstructieForm(productie).ShowDialog();
        }

        private void xwijzigproductieinfo_ButtonClick(object sender, EventArgs e)
        {
            xwijzigproductieinfo.ShowDropDown();
        }

        private void xtoonpdfb_Click(object sender, EventArgs e)
        {
            if (ProductieLijst.SelectedObject is ProductieFormulier form) form.OpenProductiePdf();
            else if (ProductieLijst.SelectedObject is Bewerking bew) bew.Parent?.OpenProductiePdf();
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
                        Manager.Opties.ProductieWeergaveFilters.Any(t => tb.Tag.ToString() == ((int)t).ToString());
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
                var xitem = new ToolStripMenuItem(f.Name) { Image = Resources.add_1588, Tag = f };
                xitem.ToolTipText = f.ToString();
                menuitem.DropDownItems.Add(xitem);
                if (f.ListNames.Any(x =>
                    string.Equals(ListName, x, StringComparison.CurrentCultureIgnoreCase)))
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

            if (e.ClickedItem.Tag is Filter filter)
            {
                if (filter.ListNames.Any(x =>
                    string.Equals(ListName, x, StringComparison.CurrentCultureIgnoreCase)))
                    return;
                filter.ListNames.Add(ListName);
                Manager.OnFilterChanged(this);
                //if (AddFilterToolstripItem(filter, true))
                //{
                //    filter.Enabled = true;
                //    UpdateProductieList();
                //    OnFilterChanged();
                //}
            }
        }

        private bool AddFilterToolstripItem(Filter filter, bool docheck)
        {
            if (filter.ListNames.Any(x =>
                    string.Equals(ListName, x, StringComparison.CurrentCultureIgnoreCase)) &&
                docheck) return false;
            var ts = new ToolStripMenuItem(filter.Name) { Image = Resources.delete_1577, Tag = filter };
            ts.ToolTipText = filter.ToString();
            ts.Click += Ts_Click;
            xfiltersStrip.Items.Add(ts);
            return true;
        }

        private void Ts_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem { Tag: Filter filter } ts)
            {
                filter.ListNames.RemoveAll(x => string.Equals(x, ListName, StringComparison.CurrentCultureIgnoreCase));
                if (filter.IsTempFilter && filter.ListNames.Count == 0)
                    Manager.Opties?.Filters?.Remove(filter);
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
            _WaitTimer?.Stop();
            _WaitTimer?.Start();
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

        private void xListColumnsButton_Click(object sender, EventArgs e)
        {
            var xf = new ExcelOptiesForm();
            xf.EnableCalculation = false;
            SaveColumns(false, Manager.Opties, false);
            xf.LoadOpties(Manager.Opties, ListName,false);
            if (xf.ShowDialog() == DialogResult.OK)
            {
                Manager.UpdateExcelColumns(xf.Settings,false);
                Manager.Opties.Save($"{ListName} Columns Aangepast!", false, false, true);
            }
        }

        private void xProductieLijst_FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.Column.Tag is ExcelColumnEntry entry && e.Model is IProductieBase productie)
            {
                FormatCell(e.SubItem, entry, productie);
            }
        }

        private void FormatCell(OLVListSubItem item, ExcelColumnEntry entry, IProductieBase productie)
        {
            if (item == null || entry == null || productie == null) return;
            var backc = Color.Empty;
            var fontc = Color.Empty;
            switch (entry.ColorType)
            {
                case ColorRuleType.None:
                    break;
                case ColorRuleType.Static:
                    if (entry.ColumnColorIndex > -1)
                        backc = ExcelColumnEntry.GetColorFromIndex(entry.ColumnColorIndex);
                    else if (entry.ColomnRGB != 0)
                        backc = Color.FromArgb(entry.ColomnRGB);

                    if (entry.ColumnTextColorIndex > -1)
                        fontc = ExcelColumnEntry.GetColorFromIndex(entry.ColumnTextColorIndex);
                    else if (entry.ColomnTextRGB != 0)
                        fontc = Color.FromArgb(entry.ColomnTextRGB);
                    break;
                case ColorRuleType.Dynamic:
                    foreach (var k in entry.KleurRegels)
                    {
                        if (k.Filter != null && (k.ColorIndex > -1 ||  k.ColorRGB != 0))
                        {
                            if (k.Filter.ContainsFilter(productie))
                            {
                                if (k.ColorIndex > -1)
                                {
                                    if (k.IsFontColor)
                                        fontc = ExcelColumnEntry.GetColorFromIndex(k.ColorIndex);
                                    else backc = ExcelColumnEntry.GetColorFromIndex(k.ColorIndex);
                                }
                                else if (k.ColorRGB != 0)
                                {
                                    if (k.IsFontColor)
                                        fontc = Color.FromArgb(k.ColorRGB);
                                    else backc = Color.FromArgb(k.ColorRGB);
                                }
                            }
                        }
                    }
                    break;
            }

            if (!backc.IsEmpty)
                item.BackColor = backc;
            if (!fontc.IsEmpty)
                item.ForeColor = fontc;
            else item.ForeColor = Color.Black;
        }

        private void xProductieLijst_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            if (e.OldDisplayIndex == 0 || e.NewDisplayIndex == 0) e.Cancel = true;
            else
            {
                if (e.Header.Tag is ExcelColumnEntry entry)
                {
                    var xset = Manager.Opties.ExcelColumns.FirstOrDefault(x =>
                        x.IsUsed(ListName) && !x.IsExcelSettings);
                    if (xset == null) return;
                    //var xent = xset.Columns.FirstOrDefault(x => x.ColumnIndex == e.NewDisplayIndex);
                    //if (xent != null)
                    //{
                    //    if(e.OldDisplayIndex > e.NewDisplayIndex)
                    //    xent.ColumnIndex = e.OldDisplayIndex;
                    //}

                    xset.Columns.Remove(entry);
                    xset.Columns.Insert(e.NewDisplayIndex, entry);
                    entry.ColumnIndex = e.NewDisplayIndex;
                    SaveColumns(false, Manager.Opties, false);
                    // Manager.ColumnsSettingsChanged(xset);
                }
            }
        }

        private void xProductieLijst1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex > ProductieLijst.Columns.Count - 1)
                return;
            if (ProductieLijst.Columns[e.ColumnIndex]?.Tag is ExcelColumnEntry entry)
            {
                if (entry.ColumnBreedte == e.NewWidth) return;
                SaveColumns(false, Manager.Opties, false);
                //var xset = Manager.Opties.ExcelColumns.FirstOrDefault(x =>
                //    x.IsUsed(ListName) && !x.IsExcelSettings);
                //if (xset == null) return;
                //entry.ColumnBreedte = ProductieLijst.Columns[e.ColumnIndex].Width;
            }
        }

        private void filterOpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_selectedSubitem.IsDefault() && Manager.Opties?.Filters != null)
            {
                var fe = FilterEntry.CreateNewFromValue(this._selectedSubitem.Key,
                    this._selectedSubitem.Value, FilterType.GelijkAan);
                fe.OperandType = Operand.ALS;
                var xold = Manager.Opties.Filters.FirstOrDefault(x =>
                    x.ListNames.Any(f => string.Equals(f, ListName, StringComparison.CurrentCultureIgnoreCase)) &&
                    x.Filters.Any(s => s.Equals(fe)));
                if (xold != null) return;
                var xf = new Filter() {IsTempFilter = true, Name = fe.PropertyName};
                xf.ListNames.Add(ListName);
                xf.Filters.Add(fe);
                _selectedSubitem = new KeyValuePair<string, object>();
                Manager.Opties.Filters.Add(xf);
                Manager.OnFilterChanged(this);
            }
        }

        private void ClearTempFilters()
        {
            if (Manager.Opties?.Filters == null) return;
            var count = Manager.Opties.Filters.RemoveAll(x =>
                x.IsTempFilter &&
                x.ListNames.Any(s => string.Equals(s, ListName, StringComparison.CurrentCultureIgnoreCase)));
            if (count > 0) Manager.OnFilterChanged(this);
        }

        private KeyValuePair<string,object> _selectedSubitem;

        private void xProductieLijst1_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            if (e.SubItem == null) return;
            _selectedSubitem = new KeyValuePair<string, object>(e.Column.AspectName, e.SubItem.ModelValue);
            if (e.SubItem?.ModelValue is not null)
            {
                xfiltertoolstripitem.Visible = true;
                var xval = e.SubItem.ModelValue.ToString();
                if(xval.Length > 12)
                    xval = xval.Substring(0,12) + "...";
                xfiltertoolstripitem.Text = $"Filter op [{e.Column.Text}] '{xval}'";
            }
            else
            {
                xfiltertoolstripitem.Visible = false;
            }
        }

        private void xProductieLijst1_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.SubItem == null) return;
            _selectedSubitem = new KeyValuePair<string, object>(e.Column.AspectName, e.SubItem.ModelValue);
            if (e.SubItem?.ModelValue is not null)
            {
                xfiltertoolstripitem.Visible = true;
                var xval = e.SubItem.ModelValue.ToString();
                if (xval.Length > 12)
                    xval = xval.Substring(0, 12) + "...";
                xfiltertoolstripitem.Text = $"Filter op [{e.Column.Text}] '{xval}'";
            }
            else
            {
                xfiltertoolstripitem.Visible = false;
            }
        }

        private void verwijderFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearTempFilters();
        }
    }
}