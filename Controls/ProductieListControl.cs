using BrightIdeasSoftware;
using Forms;
using Forms.GereedMelden;
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
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Various;
using Comparer = Rpm.Various.Comparer;
using Timer = System.Timers.Timer;

namespace Controls
{
    public partial class ProductieListControl : UserControl
    {
        public static ImageList ProductieImageList = new ImageList()
        { ColorDepth = ColorDepth.Depth32Bit, ImageSize = new Size(32, 32) };
        private bool _enableEntryFilter;

        private bool _enableFilter;

        protected Timer _WaitTimer;
        //private object _selectedItem;

        public bool ShowWaitUI { get; set; } = true;

        public ProductieListControl()
        {
            InitializeComponent();
            ProductieLijst.AllowCellEdit = false;
            ProductieLijst.SmallImageList = ProductieImageList;
            ProductieLijst.LargeImageList = ProductieImageList;
            ProductieLijst.CustomSorter = delegate (OLVColumn column, SortOrder order)
            {
                if (order != SortOrder.None)
                    ProductieLijst.ListViewItemSorter = new Comparer(order, column);
                //ArrayList objects = (ArrayList)ProductieLijst.Objects;
                //objects.Sort(new Comparer(order, column));
            };
            // ProductieLijst.BeforeSearching += ProductieLijst_BeforeSearching;
            xsearch.ShowClearButton = true;
            EnableEntryFiltering = false;
            EnableFiltering = true;
            _WaitTimer = new Timer(100);
            _WaitTimer.Elapsed += _WaitTimer_Elapsed;
            _SyncTimer = new Timer();
            _SyncTimer.Enabled = false;
            _SyncTimer.Elapsed += _SyncTimer_Elapsed;
        }

        public object SelectedItem
        {
            get => ProductieLijst.SelectedObject;
            set
            {
                ProductieLijst.SelectedObject = value;
                ProductieLijst.SelectedItem?.EnsureVisible();
            }
        }

        // ReSharper disable once ConvertToAutoProperty
        public CustomObjectListview ProductieLijst => xProductieLijst1;

        public string ListName { get; set; }
       // public bool RemoveCustomItemIfNotValid { get; set; }
        public bool CustomList { get; private set; }
        public List<ProductieFormulier> Producties { get; set; } = new();
        public List<Bewerking> Bewerkingen { get; set; } = new();
        public List<Bewerking> CheckedBewerkingen { get => ProductieLijst.CheckedObjects.OfType<Bewerking>().ToList(); }
        public IsValidHandler ValidHandler { get; set; }
        public bool IsBewerkingView { get; set; }
        public bool CanLoad { get; set; }
        public bool IsLoaded { get; private set; }
        public bool EnableContextMenu { get; set; } = true;
        public bool EnableToolBar { get => xToolBarPanel.Visible; set => xToolBarPanel.Visible = value; }
        public bool EnableCheckBox
        {
            get => ProductieLijst.CheckBoxes;
            set
            {
                ProductieLijst.CheckBoxes = value;
                xCheckAllTogle.Visible = value;
                xresultpanel.Visible = value;
            }
        }

        public bool EnableEntryFiltering
        {
            get => _enableEntryFilter;
            set
            {
                _enableEntryFilter = value;
                xfiltertoolstrip.Visible = value;
            }
        }

        public bool EnableFiltering
        {
            get => _enableFilter;
            set
            {
                _enableFilter = value;
                xfiltercontainer.Visible = value;
            }
        }

        private void _WaitTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _WaitTimer?.Stop();
            if (Disposing || IsDisposed) return;
            SetButtonEnable();
            OnSelectedItemChanged();
        }

        #region Lijst Sync
        public bool IsSyncing => _SyncTimer.Enabled;
        public bool EnableSync { get; set; }
        private readonly Timer _SyncTimer;

        private void _SyncTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_IsUpdating) return;
            UpdateList(false);
        }

        private void UpdateSyncTimer()
        {
            if (_SyncTimer != null)
            {
                EnableSync = Manager.Opties?.AutoProductieLijstSync ?? false;
                if (EnableSync && !IsSyncing)
                    _SyncTimer.Start();
                else if (IsSyncing && !EnableSync)
                    _SyncTimer.Stop();
                _SyncTimer.Interval = (Manager.Opties?.ProductieLijstSyncInterval ?? 60000);
            }
        }
        #endregion Lijst Sync

        #region Init Methods

        /// <summary>
        ///     Laadt producties
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

        public void InitProductie(List<Bewerking> bewerkingen, bool initlist, bool enablefilter, bool loadproducties,
            bool reload, bool firstselect = true)
        {

            Bewerkingen = bewerkingen;
            InitProductie(true, enablefilter, true, initlist, loadproducties, reload, firstselect);
        }

        public void InitProductie(bool bewerkingen, bool enablefilter, bool customlist, bool initlist,
            bool loadproducties, bool reload, bool firstselect = true)
        {
            EnableEntryFiltering = enablefilter;
            CustomList = customlist;
            IsBewerkingView = bewerkingen;
            if (initlist)
                try
                {
                    xsearch.TextChanged -= xsearchbox_TextChanged;
                    xsearch.TextChanged += xsearchbox_TextChanged;
                    if (InvokeRequired)
                    {
                        Invoke(new MethodInvoker(InitColumns));
                    }
                    else
                    {
                        //InitImageList();
                        InitColumns();
                    }

                    CanLoad = true;
                    IsLoaded = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            if (loadproducties)
                UpdateProductieList(reload, ShowWaitUI, firstselect);
        }

        private void UpdateStatusText()
        {
            try
            {
                var xvalue = "";
                //var xitems = new List<IProductieBase>();
                if (ProductieLijst.SelectedObjects.Count == 1)
                {
                    if (ProductieLijst.SelectedObject is IProductieBase xitem)
                        xvalue = $"{xitem.Omschrijving} ({xitem.ArtikelNr})";
                }
                var xitems =
                    (ProductieLijst.SelectedObjects.Count > 0 ? ProductieLijst.SelectedObjects : ProductieLijst.Objects)
                    ?.Cast<IProductieBase>().ToList() ?? new List<IProductieBase>();
                var xtijdgewerkt = xitems.Sum(x => x?.TijdGewerkt ?? 0);
                var xtijd = xitems.Sum(x => x?.DoorloopTijd ?? 0);
                var xgemaakt = xitems.Sum(x => x?.TotaalGemaakt ?? 0);
                var xtotaal = xitems.Sum(x => x?.Aantal ?? 0);
                var xpu = xtijdgewerkt == 0 || xgemaakt == 0 ? xgemaakt : (int)Math.Round(xgemaakt / xtijdgewerkt, 0);
                var x1 = xitems.Count == 1 ? "Productie" : "Producties";
                xstatuslabel.Text = xvalue.Replace("\n", " ");
                xgemiddeldpu.Text = $"Gemiddeld: {xpu} p/u";
                xgemaaktlabel.Text = $"Geproduceerd: {(xgemaakt == 0 ? "0" : xgemaakt.ToString("#,##0"))}";
                xtotaaltijdlabel.Text = $"Doorlooptijd: {xtijd:#,##0.##} uur";
                xtotaalgewerktlabel.Text = $"Gewerkt: {xtijdgewerkt:#,##0.##} uur";
                xtotaalAantallabel.Text = $"Aantal: {(xtotaal == 0 ? "0" : xtotaal.ToString("#,##0"))}";
                xitemcount.Text = xitems.Count == 0 ? "0" : xitems.Count.ToString("#,##0") + $" {x1}";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SetButtonEnable()
        {
            if (this.Disposing || this.IsDisposed) return;
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new MethodInvoker(SetButtonEnable));
                }
                catch { }
               
            }
            else
            {
                try
                {
                    UpdateStatusText();
                    UpdateCheckTogle();
                    if (!EnableContextMenu && !EnableToolBar) return;
                    var enable1 = ProductieLijst.SelectedObjects is { Count: 1 };
                    var enable2 = ProductieLijst.SelectedObjects is { Count: > 1 };
                    var enable3 = enable1 || enable2;
                    var acces1 = Manager.LogedInGebruiker is { AccesLevel: >= AccesType.ProductieBasis };
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
                        else
                            bws = ProductieLijst.SelectedObjects.Cast<Bewerking>().ToList();
                    }

                    if (SelectedItem is Bewerking bw && acces1 &&
                        bw.State is ProductieState.Gestart or ProductieState.Gestopt)
                    {
                        xonderbreek.Enabled = true;
                        var xont = bw.GetStoringen(true);
                        xonderbreek.Image =
                            xont.Length == 0 ? Resources.Stop_Hand__32x32 : Resources.playcircle_32x32;
                    }
                    else
                    {
                        xonderbreek.Enabled = false;
                    }

                    var isprod = !IsBewerkingView;
                    var verwijderd1 = enable3 && isprod
                        ? bws.All(x => x != null && x.State == ProductieState.Verwijderd)
                        : bws.Any(x => x != null && x.State == ProductieState.Verwijderd);
                    var verwijderd2 = enable3 && bws.Any(x => x != null && x.State != ProductieState.Verwijderd);
                    var isgereed1 = enable3 && isprod
                        ? bws.All(x => x != null && x.State == ProductieState.Gereed)
                        : bws.Any(x => x != null && x.State == ProductieState.Gereed);
                    var isgereed2 = enable3 && bws.Any(x => x != null && x.State != ProductieState.Gereed);
                    var isgestart = enable3 && bws.Any(x => x != null && x.State == ProductieState.Gestart);
                    var isgestopt = enable3 && bws.Any(x => x != null && x.State == ProductieState.Gestopt);
                    var haspdf = bws.Count > 0 && bws[0]?.Parent != null && bws[0].Parent.ContainsProductiePdf();
                    bool canprint = Functions.CanPrint();
                    //var nietbemand = bws.Any(x => !x.IsBemand);
                    xbewerkingeninfob.Enabled = ProductieLijst.Items.Count > 0;
                    xwijzigproductieinfo.Enabled = enable3 && acces1;
                    xtoonpdfb.Enabled = haspdf;
                    xPrinten.Enabled = canprint && bws.Any(x => x.Parent?.ContainsProductiePdf() ?? false);
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
                    xtoonTekening.Enabled = enable1;
                    xbijlage.Enabled = enable1 && acces1;

                    xToonVDatumsbutton.Enabled = ProductieLijst.Items.Count > 0;
                    xberekendatums.Enabled = ProductieLijst.Items.Count > 0 && acces1;
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
                    kopiërenToolStripMenuItem.Enabled = !_selectedSubitem.IsDefault() && _selectedSubitem.Value != null;
                    werkTekeningToolStripMenuItem.Enabled = enable1;
                    bijlagesToolStripMenuItem.Enabled = enable1 && acces1;
                    filterOpslaanToolStripMenuItem.Visible = Manager.Opties?.Filters != null && Manager.Opties.Filters.Any(x => x.IsTempFilter);
                    printenToolStripMenuItem.Visible = xPrinten.Enabled;
                    //resetToolStripMenuItem.Visible = Manager.Opties?.Filters?.Any(x =>
                    //    x.IsTempFilter && x.ListNames.Any(s =>
                    //        string.Equals(s, ListName, StringComparison.CurrentCultureIgnoreCase))) ?? false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void InitEvents()
        {
            Manager.OnSettingsChanged += _manager_OnSettingsChanged;
            // Manager.OnSettingsChanging += Manager_OnSettingsChanging;
            Manager.OnFormulierChanged += _manager_OnFormulierChanged;
            Manager.OnFormulierDeleted += Manager_OnFormulierDeleted;
            //Manager.OnProductiesLoaded += Manager_OnProductiesChanged;
            //Manager.OnLoginChanged += _manager_OnLoginChanged;
            //Manager.DbUpdater.DbEntryUpdated += DbUpdater_DbEntryUpdated;
            Manager.OnBewerkingDeleted += _manager_OnBewerkingDeleted;
            //Manager.OnDbBeginUpdate += Manager_OnDbBeginUpdate;
            // Manager.OnDbEndUpdate += Manager_OnDbEndUpdate;
            //Manager.OnManagerLoaded += _manager_OnManagerLoaded;
            Manager.FilterChanged += Manager_FilterChanged;
            Manager.LayoutChanged += Xcols_OnColumnsSettingsChanged;
        }

        private void Manager_OnSettingsChanging(object instance, ref UserSettings settings, ref bool cancel)
        {
            try
            {
                if (Disposing || IsDisposed) return;
                var x = settings;
                //this.Invoke(new Action(() => SaveColumns(false, x, false)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DetachEvents()
        {
            Manager.OnSettingsChanged -= _manager_OnSettingsChanged;
            //Manager.OnSettingsChanging -= Manager_OnSettingsChanging;
            Manager.OnFormulierDeleted -= Manager_OnFormulierDeleted;
            Manager.OnFormulierChanged -= _manager_OnFormulierChanged;
            // Manager.OnProductiesLoaded -= Manager_OnProductiesChanged;
            //Manager.DbUpdater.DbEntryUpdated -= DbUpdater_DbEntryUpdated;
            //Manager.OnLoginChanged -= _manager_OnLoginChanged;
            Manager.OnBewerkingDeleted -= _manager_OnBewerkingDeleted;
            //Manager.OnDbBeginUpdate -= Manager_OnDbBeginUpdate;
            //Manager.OnDbEndUpdate -= Manager_OnDbEndUpdate;
            //Manager.OnManagerLoaded -= _manager_OnManagerLoaded;
            Manager.FilterChanged -= Manager_FilterChanged;
            Manager.LayoutChanged -= Xcols_OnColumnsSettingsChanged;
            _SyncTimer?.Stop();
        }

        public void CloseUI()
        {
            DetachEvents();
            SaveColumns(true);
            ProductieLijst.Clear();
        }

        /// <summary>
        ///     Toon laad scherm
        /// </summary>
        public void SetWaitUI()
        {
            ProductieLijst.StartWaitUI("Bewerkingen laden");
        }

        public void SetWaitUI(string value)
        {
            ProductieLijst.StartWaitUI(value);
        }

        /// <summary>
        ///     verberg het laad scherm
        /// </summary>
        public void StopWait()
        {
            ProductieLijst.StopWait();
        }

        public ExcelSettings SaveColumns(bool raiseevent)
        {
            if (Manager.ListLayouts == null || Manager.ListLayouts.Disposed)
                return null;
            var xlist = Manager.ListLayouts.GetAlleLayouts();
            var xname = $"{Manager.Opties?.Username}_{ListName}";
            var xcols = xlist.FirstOrDefault(x =>
                x.ListNames.Any(listname => string.Equals(listname, xname, StringComparison.CurrentCultureIgnoreCase)));
            if (xcols == null)
            {
                xcols = xlist.FirstOrDefault(x => x.UseAsDefault);
                if (xcols == null)
                {
                    xcols = new ExcelSettings(ListName, ListName, false);
                    Manager.ListLayouts.SaveLayout(xcols, "Nieuwe Lijst Layout Aangemaakt!", raiseevent);
                }
                else
                {
                    xcols.SetSelected(true, ListName);
                }
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

            xcols.GroupBy = ProductieLijst.AlwaysGroupByColumn?.AspectName;
            xcols.ShowGroups = ProductieLijst.ShowGroups;
            xcols.Columns = xcols.Columns.OrderBy(x => x.ColumnIndex).ToList();
            Manager.ListLayouts.SaveLayout(xcols, null, raiseevent);
            return xcols;
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
                                    return bw.DatumGereed;
                                if (bw.TotaalGemaakt >= bw.Aantal)
                                    return "Aantal Behaald!";
                                return bw.VerwachtLeverDatum.ToString(8, "over {0} {1}", "{0} {1} geleden", false);
                            }

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
                if (Manager.ListLayouts == null || Manager.ListLayouts.Disposed)
                    return;
                var xlists = Manager.ListLayouts.GetAlleLayouts().Where(x => !x.IsExcelSettings).ToList();
                var xcols = xlists.FirstOrDefault(x =>
                    x.IsUsed(ListName));
                OLVColumn xsort = null;
                if (xcols == null)
                {
                    xcols = xlists.FirstOrDefault(x => x.UseAsDefault);
                    if (xcols != null)
                    {
                        xcols.SetSelected(true, ListName);
                    }
                    else
                    {
                        xcols = ExcelSettings.CreateSettings(ListName, false, xlists);
                        Manager.ListLayouts.SaveLayout(xcols, "Nieuwe Lijst Layout Aangemaakt!", false);
                    }
                }

                InitLayoutStrips(xlists);
                if (xcols?.Columns != null)
                {
                    if (xcols.Columns.Count == 0) xcols.SetDefaultColumns();

                    ProductieLijst.ShowGroups = xcols.ShowGroups;
                    ProductieLijst.BeginUpdate();
                    OLVColumn groucolumn = null;
                    var xcurcols = ProductieLijst.AllColumns.ToList();
                    for (var i = 0; i < xcurcols.Count; i++)
                    {
                        var xcurcol = xcurcols[i];
                        var xcol = xcols.Columns.FirstOrDefault(x =>
                            string.Equals(x.Naam, xcurcol.AspectName, StringComparison.CurrentCultureIgnoreCase));
                        if (!string.IsNullOrEmpty(xcols.GroupBy) && string.Equals(xcols.GroupBy, xcurcol.AspectName,
                                StringComparison.CurrentCultureIgnoreCase))
                            groucolumn = xcurcol;
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

                        if (!string.IsNullOrEmpty(xcols.GroupBy) && string.Equals(xcols.GroupBy, col.AspectName,
                                StringComparison.CurrentCultureIgnoreCase))
                            groucolumn = col;
                        ProductieLijst.AllColumns.Add(col);
                    }

                    ProductieLijst.AlwaysGroupByColumn = groucolumn;
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
                if (settings.IsUsed(ListName))
                    if (!IsDisposed)
                        BeginInvoke(new Action(InitColumns));
        }

        private int GetProductieImageIndex(IProductieBase productie)
        {
            if (productie == null) return 0;
            return productie.GetImageIndexFromList(ProductieImageList);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var e = new KeyEventArgs(keyData);
            if (e.Control && e.KeyCode == Keys.L)
            {
                xListColumnsButton_Click(this, EventArgs.Empty);
                return true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                ProductieLijst.SelectedObject = null;
                return true;
            }
            if (e.Alt)
            {
                var keys = Keys.Alt | e.KeyCode;
                var item = xfiltertoolstripitem.DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(x => x.ShortcutKeys == keys);
                if (item != null)
                {
                    item.PerformClick();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region ProductieLijstGetters

        private object ImageGetter(object sender)
        {
            return GetProductieImageIndex(sender as IProductieBase);
        }

        private object VerpakkingsInstructiesGetter(object sender)
        {
            if (sender is IProductieBase productie)
            {
                var xinfo = $"{productie.VerpakkingsInstructies?.VerpakkingType} (Verpakker per {productie.VerpakkingsInstructies?.VerpakkenPer})";
                if (productie.VerpakkingsInstructies != null)
                    return xinfo;
                return "N.V.T.";
            }

            return "N.V.T.";
        }

        private object PersonenGetter(object sender)
        {
            if (sender is IProductieBase productie)
                return string.Join(", ",
                    productie.Personen?.Select(x => x.PersoneelNaam).ToArray() ?? new[] { "N.V.T." });

            return "N.V.T.";
        }

        private object LastChangedGetter(object sender)
        {
            if (sender is IProductieBase productie)
            {
                var name = productie.LastChanged?.User;
                var xvalue = name == null ? "" : name + ": ";
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
                var name = productie.Note?.Naam;
                var xvalue = name == null ? "" : name + ": ";
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
                var name = productie.GereedNote?.Naam;
                var xvalue = name == null ? "" : name + ": ";
                xvalue += productie.GereedNote?.Notitie;
                if (string.IsNullOrEmpty(xvalue)) return "N.V.T.";
                return xvalue;
            }

            return "N.V.T.";
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
        #endregion ProductieLijstGetters

        #endregion Init Methods

        #region Listing Methods

        private bool _loadingproductielist;

        public string GetFilter()
        {
            return xsearch.Text.ToLower() == "zoeken..." ? "" : xsearch.Text;
        }

        private bool IsAllowd(Bewerking bewerking, bool tempfilter)
        {
            var filters = Manager.Opties.Filters;
            if (filters is { Count: > 0 })
            {
                var xreturn = true;
                foreach (var filter in filters)
                    if (filter.ListNames.Any(x =>
                            string.Equals(ListName, x, StringComparison.CurrentCultureIgnoreCase)))
                        xreturn &= filter.IsAllowed(bewerking, ListName, tempfilter);

                return xreturn;
            }

            return true;
        }

        private bool IsAllowd(ProductieFormulier productie, bool tempfilter)
        {
            var filters = Manager.Opties.Filters;
            if (filters is { Count: > 0 })
            {
                if (productie?.Bewerkingen == null) return false;
                return productie.Bewerkingen.Any(x=> IsAllowd(x, tempfilter));
            }

            return true;
        }

        private void UpdateListObjects<T>(List<T> objects)
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(() => UpdateListObjects<T>(objects)));
            else
            {
                //if (objects != null && (objects.Count != ProductieLijst.Items.Count || ProductieLijst.Items.Count > 0 &&
                //!ProductieLijst.Objects.Cast<T>().Any(x => objects.Any(o => o.Equals(x)))))
                //{
                if (ProductieLijst.Columns.Count == 0) return;
                objects ??= new List<T>();
                var changed = ProductieLijst.Items.Count != objects.Count;
                _loadingproductielist = true;
                ProductieLijst.BeginUpdate();
                try
                {
                    //var sel = ProductieLijst.SelectedObject;
                    ProductieLijst.SetObjects(objects);
                    //ProductieLijst.SelectedObject = sel;
                    //ProductieLijst.SelectedItem?.EnsureVisible();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                ProductieLijst.EndUpdate();
                _loadingproductielist = false;
                //if (changed)
                OnItemCountChanged();
                //}
            }
        }

        public Task<List<Bewerking>> GetBewerkingen(bool reload, bool customfilter)
        {
            return Task.Factory.StartNew(() =>
            {
                return xGetBewerkingen(reload, customfilter);
            });
        }

        public List<Bewerking> xGetBewerkingen(bool reload, bool customfilter)
        {
            List<Bewerking> bws = new List<Bewerking>();
            try
            {
                var states = GetCurrentViewStates();
                bool check = states.Any(x => x is ViewState.Alles or ViewState.Gereed);


                if (!reload && CustomList && Bewerkingen != null)
                {
                    for (int i = 0; i < Bewerkingen.Count; i++)
                    {
                        var b = Bewerkingen[i];
                        if (states.Any(b.IsValidState) && b.ContainsFilter(GetFilter()))
                            bws.Add(b);
                    }

                }
                else
                {
                    bws.AddRange(Bewerkingen = Manager.xGetBewerkingen(states, true, check));
                }
                if (customfilter && ValidHandler != null)
                    bws = bws.Where(x => IsAllowd(x, true) && ValidHandler.Invoke(x, GetFilter()))
                        .ToList();
                else
                    bws = bws.Where(x => IsAllowd(x, true) && x.IsAllowed(GetFilter())).ToList();
                return bws;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return bws;
        }

        public List<ProductieFormulier> GetProducties(bool reload, bool customfilter)
        {
            var states = GetCurrentViewStates();
            bool check = states.Any(x => x is ViewState.Alles or ViewState.Gereed);
            var xprods = !reload && CustomList && Producties != null
                ? Producties.Where(x => states.Any(x.IsValidState) && x.ContainsFilter(GetFilter()))
                    .ToList()
                : Producties = Manager.xGetProducties(states, true, true, null, check);
            if (customfilter && ValidHandler != null)
                xprods = xprods.Where(x => IsAllowd(x, true) && ValidHandler.Invoke(x, GetFilter()))
                    .ToList();
            else
                xprods = xprods.Where(x => IsAllowd(x, true) && x.IsAllowed(GetFilter(), states, true))
                    .ToList();
            return xprods;
        }

        public void UpdateProductieList(bool reload, bool showwaitui, bool firstselect)
        {
            try
            {
                if (InvokeRequired)
                    Invoke(new MethodInvoker(() => { UpdateProductieList(reload, showwaitui, firstselect); }));
                else
                {
                    if (Manager.Opties == null || !CanLoad || _loadingproductielist) return;
                    try
                    {
                        if (showwaitui)
                            SetWaitUI();
                        try
                        {
                            InitFilterStrips();
                            IList selected1 = null;
                            OLVGroup[] groups1 = Array.Empty<OLVGroup>();
                            this.Invoke(new MethodInvoker(() =>
                            {
                                selected1 = ProductieLijst.SelectedObjects;

                                groups1 = ProductieLijst.Groups.Cast<ListViewGroup>().Select(t => (OLVGroup)t.Tag)
                                    .Where(x => x.Collapsed)
                                    .ToArray();
                            }));
                            _loadingproductielist = true;
                            if (!IsBewerkingView)
                            {
                                if (CanLoad)
                                {
                                    var xprods = GetProducties(reload, true);
                                    if (!IsDisposed && !Disposing)
                                        UpdateListObjects(xprods);
                                }
                            }
                            else if (CanLoad)
                            {
                               
                                var bws = xGetBewerkingen(reload, true);
                                if (!IsDisposed && !Disposing)
                                    UpdateListObjects(bws);
                              
                            }
                           
                            //this.Invoke(new MethodInvoker(() =>
                            //{
                            //    var xgroups = ProductieLijst.Groups.Cast<ListViewGroup>().ToList();
                            //    if (groups1.Length > 0)
                            //        for (var i = 0; i < xgroups.Count; i++)
                            //        {
                            //            var group = xgroups[i].Tag as OLVGroup;
                            //            if (group == null)
                            //                continue;
                            //            if (groups1.Any(t => !group.Collapsed && t.Header == group.Header))
                            //                group.Collapsed = true;
                            //        }
                            //}));

                            SetButtonEnable();
                            OnSelectedItemChanged();
                            UpdateSyncTimer();

                            this.Invoke(new MethodInvoker(() =>
                            {
                                ProductieLijst.SelectedObjects = selected1;
                                if (ProductieLijst.SelectedObject == null && firstselect)
                                    ProductieLijst.SelectedIndex = 0;
                            }));

                        }
                        catch (Exception e)
                        {
                            _loadingproductielist = false;
                            Console.WriteLine(e);
                        }
                        StopWait();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        _loadingproductielist = false;
                        StopWait();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _loadingproductielist = false;
        }

        private string Criteria()
        {
            return _criteria;
        }

        public bool UpdateFormulier(ProductieFormulier form)
        {
            if (IsDisposed || Disposing || form == null || _loadingproductielist)
                return false;

            try
            {
                var crit = Criteria();
                var filter = crit?.ToLower() == "zoeken..."
                    ? null
                    : crit?.Trim();

                var states = GetCurrentViewStates();
                var changed = false;
                var xreturn = false;
                IList xselected = null;
                int curc = 0;
                if (InvokeRequired)
                    this.Invoke(new MethodInvoker(() =>
                    {
                        xselected = ProductieLijst.SelectedObjects;
                        curc = ProductieLijst.Items.Count;
                    }));
                else
                {
                    xselected = ProductieLijst.SelectedObjects;
                    curc = ProductieLijst.Items.Count;
                }
                if (!IsBewerkingView)
                {
                    var isvalid = IsAllowd(form, true) && form.IsAllowed(filter, states, true);
                    if (isvalid && ValidHandler != null)
                        isvalid &= ValidHandler.Invoke(form, filter);

                    var xproducties = Producties;
                    if (xproducties == null)
                    {
                        if (InvokeRequired)
                            this.Invoke(new MethodInvoker(() => xproducties = ProductieLijst.Objects?.Cast<ProductieFormulier>().ToList()));
                        else
                            ProductieLijst.Objects?.Cast<ProductieFormulier>().ToList();
                    }

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
                            else
                                Producties.Add(form);
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
                    xreturn = isvalid;
                }
                else
                {
                    //this.Invoke(new Action(() =>
                    //{
                    //    changed = UpdateBewerking(form, null, states, filter);
                    //}));
                    changed = UpdateBewerkingen(form, null, states, filter);
                }
                SetButtonEnable();
                if (changed)
                {
                    SelectObjects(xselected);
                    OnItemCountChanged();
                }
                return xreturn;
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine(@"Disposed!");
                return false;
            }
        }

        private void SelectObjects(IList list)
        {
            try
            {
                if (InvokeRequired)
                    this.Invoke(new MethodInvoker(() => SelectObjects(list)));
                else
                {
                    if (ProductieLijst.Items.Count > 0)
                    {
                        ProductieLijst.SelectedObjects = list;
                        if (ProductieLijst.SelectedObject == null) ProductieLijst.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private bool _IsUpdating;

        private void XUpdateList(bool onlywhilesyncing)
        {


            try
            {
                if (_IsUpdating) return;
                _IsUpdating = true;
                ViewState[] states = null;
                if (InvokeRequired)
                    this.Invoke(new MethodInvoker(() => states = GetCurrentViewStates()));
                else
                    states = GetCurrentViewStates();
                if (states == null) return;
                if (Producties is { Count: > 0 })
                    for (var i = 0; i < Producties.Count; i++)
                    {
                        if (!EnableSync) break;
                        if (onlywhilesyncing && !IsSyncing) break;
                        if (IsDisposed || Disposing) break;
                        var prod = Producties[i];
                        var xprod = Manager.Database.GetProductie(prod.ProductieNr, true);
                        var index = i;
                        if (InvokeRequired)
                            this.Invoke(new MethodInvoker(() => UpdateForm(xprod, states, ref index, false)));
                        else UpdateForm(xprod, states, ref index, false);
                        i = index;
                    }

                if (Bewerkingen is { Count: > 0 })
                    for (var i = 0; i < Bewerkingen.Count; i++)
                    {
                        if (!EnableSync) break;
                        if (onlywhilesyncing && !IsSyncing) break;
                        if (IsDisposed || Disposing) break;
                        var bew = Bewerkingen[i];
                        var xbew = Werk.FromPath(bew.Path)?.Bewerking;
                        var index = i;
                        if (InvokeRequired)
                            this.Invoke(new MethodInvoker(() => UpdateBewerking(xbew, states, ref index, false, true)));
                        else UpdateBewerking(xbew, states, ref index, false, true);
                        i = index;
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _IsUpdating = false;


        }

        public Task UpdateList(bool onlywhilesyncing)
        {
            return Task.Factory.StartNew(() =>
            {

                try
                {
                    XUpdateList(onlywhilesyncing);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }
        private static object _locker = new object();
        public bool UpdateBewerkingen(ProductieFormulier form, List<Bewerking> bewerkingen, ViewState[] states,
            string filter)
        {
            if (!IsBewerkingView || form == null || Disposing || IsDisposed) return false;
            var changed = false;
            try
            {
                var xbewerkingen = bewerkingen ?? Bewerkingen;//ProductieLijst.Objects?.Cast<Bewerking>().ToList();
                states ??= GetCurrentViewStates();
                // bool checkall = xbewerkingen != null && !xbewerkingen.Any(x=> string.Equals(x.ProductieNr, form.ProductieNr, StringComparison.CurrentCultureIgnoreCase));

                if (form?.Bewerkingen != null && form.Bewerkingen.Length > 0)
                {
                    for (int i = 0; i < form?.Bewerkingen.Length; i++)
                    {
                        var b = form.Bewerkingen[i];
                        var index = -1;
                        UpdateBewerking(b, states, ref index, false, false);
                        var isvalid = IsAllowd(b, true) && b.IsAllowed(filter ?? GetFilter()) &&
                                      states.Any(x => b.IsValidState(x));
                        if (isvalid && ValidHandler != null)
                            isvalid &= ValidHandler.Invoke(b, filter ?? GetFilter());
                       // lock (_locker)
                        //{
                            if (InvokeRequired)
                                this.Invoke(new MethodInvoker(() => index = ProductieLijst.IndexOf(b)));
                            else
                                index = ProductieLijst.IndexOf(b);
                            if (index > -1)
                            {
                                if (isvalid)
                                    ProductieLijst.RefreshObject(b);
                                else
                                {
                                    changed = true;
                                    ProductieLijst.RemoveObject(b);
                                }
                            }
                            else
                            {
                                if (isvalid)
                                {
                                    changed = true;
                                    ProductieLijst.AddObject(b);
                                }
                            }
                       // }
                    }
                }
                //var xremove = xbewerkingen?.Where(x =>
                //    string.Equals(x.ProductieNr, form.ProductieNr, StringComparison.CurrentCultureIgnoreCase) &&
                //    !form.Bewerkingen.Any(xb => xb.Equals(x))).ToList();
                //if (xremove is {Count: > 0})
                //{
                //    ProductieLijst.BeginUpdate();
                //    xremove.ForEach(xr =>
                //    {
                //        changed = true;
                //        Bewerkingen?.Remove(xr);
                //        ProductieLijst.RemoveObject(xr);
                //    });
                //    ProductieLijst.EndUpdate();
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return changed;
        }

        private void DeleteID(string id)
        {
            if (this.Disposing || this.IsDisposed) return;
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(() => DeleteID(id)));
            else
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
                            Producties.RemoveAll(x => x == null ||
                                string.Equals(id, x.ProductieNr, StringComparison.CurrentCultureIgnoreCase));
                        }
                    }
                    else
                    {
                        var bws = ProductieLijst.Objects?.Cast<Bewerking>().Where(x =>
                            string.Equals(id, x.ProductieNr, StringComparison.CurrentCultureIgnoreCase)).ToArray();
                        if (bws is { Length: > 0 })
                        {
                            ProductieLijst.RemoveObjects(bws);
                            Bewerkingen.RemoveAll(x => x == null ||
                                string.Equals(id, x.ProductieNr, StringComparison.CurrentCultureIgnoreCase));
                        }
                    }

                    OnItemCountChanged();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                ProductieLijst.EndUpdate();
            }
        }

        public bool UpdateBewerking(Bewerking bew, ViewState[] states, ref int index, bool refresh, bool updatelijst)
        {

            var valid = bew != null && IsAllowd(bew, false);
            if (valid)
            {
                if (ValidHandler != null)
                    valid &= states.Any(bew.IsValidState) && ValidHandler.Invoke(bew, null);
                else valid &= states.Any(bew.IsValidState) && bew.IsAllowed(null, false);
            }
            var xret = false;
            //lock (Bewerkingen)
            //{
            if (index == -1)
                index = Bewerkingen.IndexOf(bew);

            if (!valid)
            {
                if (index > -1)
                {

                    Bewerkingen.Remove(bew);
                    xret = true;
                    if (updatelijst)
                    {
                        ProductieLijst.BeginUpdate();
                        ProductieLijst.RemoveObject(bew);
                        ProductieLijst.EndUpdate();
                    }
                }
            }
            else
            {

                if (index > -1)
                {
                    Bewerkingen[index] = bew;
                    xret = true;
                    if (refresh)
                        ProductieLijst.RefreshObject(bew);
                }
                else
                {
                    xret = true;
                    Bewerkingen.Add(bew);
                    if (updatelijst)
                    {
                        ProductieLijst.BeginUpdate();
                        ProductieLijst.AddObject(bew);
                        ProductieLijst.EndUpdate();
                    }
                }
            }
            //}
            return xret;
        }

        public void UpdateForm(ProductieFormulier form, ViewState[] states, ref int index, bool refresh)
        {
            var valid = form != null && IsAllowd(form, true);
            if (valid)
            {
                if (ValidHandler != null)
                    valid &= states.Any(form.IsValidState) && ValidHandler.Invoke(form, null);
                else valid &= states.Any(form.IsValidState) && form.IsAllowed(null);
            }
            if (index == -1)
                index = Producties.IndexOf(form);
            valid &= index > -1;
            if (!valid)
            {
                if (index > -1)
                    Producties.RemoveAt(index--);
                ProductieLijst.BeginUpdate();
                ProductieLijst.RemoveObject(form);
                ProductieLijst.EndUpdate();
            }
            else
            {
                if (index > -1)
                {
                    Producties[index] = form;
                    if (refresh)
                        ProductieLijst.RefreshObject(form);
                }
            }
        }

        #endregion Listing Methods

        #region Manager Events

        private void Manager_FilterChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.IsDisposed || this.Disposing) return;
                if (!_enableEntryFilter) return;
                if (sender is ProductieListControl xc && string.Equals(xc.ListName, ListName))
                {
                    //var xfilters = Manager.Opties.Filters.Where(x =>
                    //        x.ListNames.Any(l => string.Equals(ListName, l, StringComparison.CurrentCultureIgnoreCase)))
                    //    .ToList();
                    //var reload = false;
                    //if (xfilters.Count == 0)
                    //    reload = true;
                    //else
                    //    reload = !xfilters.All(x => x.IsTempFilter);
                    var states = GetCurrentViewStates();
                    UpdateProductieList(false, false, false);
                }
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
                if (!init || IsDisposed || Disposing) return;
                BeginInvoke(new MethodInvoker(() =>
                {
                    //InitColumns();
                    if (CanLoad)
                        UpdateProductieList(true, ShowWaitUI, true);
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
            if (IsDisposed || Disposing || !IsLoaded || _loadingproductielist)
                return;
            try
            {
                UpdateFormulier(changedform);
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
                DeleteBewerking(sender, bew, change, shownotification);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void DeleteBewerking(object sender, Bewerking bew, string change, bool shownotification)
        {
            if (IsDisposed || Disposing || !IsBewerkingView || !IsLoaded) return;
            if (InvokeRequired)
                this.Invoke(new MethodInvoker(() => DeleteBewerking(sender, bew, change, shownotification)));
            else
            {
                ProductieLijst.BeginUpdate();
                ProductieLijst.RemoveObject(bew);
                ProductieLijst.EndUpdate();
                Bewerkingen?.Remove(bew);
                SetButtonEnable();
                OnItemCountChanged();
            }
        }

        private void Manager_OnFormulierDeleted(object sender, string id)
        {
            if (IsDisposed || Disposing || !IsLoaded || string.IsNullOrEmpty(id)) return;
            DeleteID(id);
        }

        #endregion Manager Events

        #region Search
        private void xsearchtimer_Tick(object sender, EventArgs e)
        {
            if (_loadingproductielist) return;
            xsearchtimer.Stop();
            _criteria = xsearch.Text.Trim();
            if (xsearch.Text.ToLower().Trim() != "zoeken...")
            {
                if (string.Equals(_lastSearch, xsearch.Text.Trim(), StringComparison.CurrentCultureIgnoreCase))
                    return;
                _lastSearch = xsearch.Text.Trim();
                OnSearchItems(xsearch.Text.Trim());
                UpdateProductieList(false, false, true);
            }
        }
        public bool DoSearch(string criteria)
        {
            try
            {
                var crit1 = string.IsNullOrEmpty(criteria) ? "Zoeken..." : criteria;
                var crit2 = string.IsNullOrEmpty(xsearch.Text.Trim()) ? "Zoeken..." : xsearch.Text.Trim();
                var flag = string.Equals(crit1, crit2, StringComparison.CurrentCultureIgnoreCase) ||
                           crit1.ToLower().Trim() == "zoeken..." || ProductieLijst._isLoading;
                xsearch.Text = crit1.ToLower().StartsWith("zoeken...") ? "" : crit1;
                xsearch.Text = crit1;
                return !flag;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        string _lastSearch = "";
        private string _criteria;
        private void xsearchbox_TextChanged(object sender, EventArgs e)
        {
            xsearchtimer.Stop();
            xsearchtimer.Start();
        }

        private void xsearch_Enter(object sender, EventArgs e)
        {
            if (sender is MetroTextBox { Text: "Zoeken..." } tb) tb.Text = "";
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
            Manager.FormulierActie(new object[] { pform, bewerking, (Control)this?.FindForm()}, MainAktie.OpenProductie);
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

                StartBewerkingen(this, bws.ToArray());
                Focus();
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

                StopBewerkingen(bws);
                Focus();
            }
        }

        public static bool StartBewerkingen(IWin32Window owner, Bewerking[] bws)
        {
            var count = bws.Length;
            if (count > 0)
            {
                var withnopers = bws.Count(x => x.AantalActievePersonen == 0);
                var valid = true;
                if (withnopers > 1)
                    valid = XMessageBox.Show(owner,
                        $"Je staat op het punt {withnopers} bewerkingen te starten waar geen personeel voor is ingezet.\n" +
                        "Wil je ze allemaal achter elkaar indelen?\n\n" +
                        $"Je zal dat {withnopers} keer moeten doen.\n" +
                        "Klik op 'Ja' als je door wilt gaan, klik anders op 'Nee'.", "Opgelet", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning) == DialogResult.Yes;
                if (valid)
                    try
                    {
                        //var mainform = sender??Application.OpenForms["MainForm"];

                        // async void Action()
                        //{
                        for (var i = 0; i < bws.Length; i++)
                        {
                            var werk = bws[i];
                            if (werk == null) continue;

                            if (werk.State != ProductieState.Gestart)
                            {
                                if (werk.AantalActievePersonen == 0)
                                    AddPersoneel(owner, ref werk, werk.WerkPlekken.FirstOrDefault()?.Naam);

                                _ = werk.StartProductie(true, true, true);
                            }
                        }
                        //  }
                        //}

                        //mainform?.BeginInvoke(new Action( Action));
                        return true;
                    }
                    catch (Exception exception)
                    {
                        XMessageBox.Show(owner, exception.Message,
                            "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                return false;
            }

            return false;
        }

        public static bool AddPersoneel(IWin32Window owner, ref Bewerking werk, string werkplek)
        {
            var pers = new PersoneelsForm(werk, true);
            if (pers.ShowDialog() == DialogResult.OK)
            {
                if (pers.SelectedPersoneel is { Length: > 0 })
                {
                    foreach (var per in pers.SelectedPersoneel) per.Klusjes?.Clear();
                    var afzonderlijk = false;
                    if (pers.SelectedPersoneel.Length > 1 && werk.IsBemand)
                    {
                        var result = XMessageBox.Show(owner,
                            $"Je hebt {pers.SelectedPersoneel.Length} medewerkers geselecteerd," +
                            " wil je ze allemaal afzonderlijk indelen?", "Indeling",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Cancel) return false;
                        afzonderlijk = result == DialogResult.Yes;
                    }

                    var parent = werk.GetParent();
                    if (!werk.IsBemand || !afzonderlijk)
                    {
                        var klusui = new NieuwKlusForm(parent, pers.SelectedPersoneel, true,
                            false, werk, werkplek);
                        if (klusui.ShowDialog() != DialogResult.OK) return false;
                        var pair = klusui.SelectedKlus.GetWerk(parent);
                        var prod = pair.Formulier;
                        werk = pair.Bewerking;
                        if (werk is { IsBemand: false } && klusui.SelectedKlus?.Tijden?.Count > 0)
                            pair.Plek?.Tijden.UpdateLijst(klusui.SelectedKlus.Tijden,
                                false);

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
                                var wp = werk.WerkPlekken.FirstOrDefault(x =>
                                    string.Equals(x.Naam, klusui.SelectedKlus.WerkPlek,
                                        StringComparison.CurrentCultureIgnoreCase));
                                if (wp == null)
                                {
                                    wp = new WerkPlek(per, klusui.SelectedKlus.WerkPlek, werk);
                                    wp.Tijden.UpdateLijst(klusui.SelectedKlus.Tijden, false);
                                    werk.WerkPlekken.Add(wp);
                                }
                                else
                                {
                                    wp.AddPersoon(per, werk);
                                }

                                werk.xUpdateBewerking(null,
                                        $"{wp.Path} indeling aangepast", false, false);
                            }
                        }
                    }

                    if (werk != null) return true;
                }

                return false;
            }

            return false;
        }

        private async void StopBewerkingen(List<Bewerking> bws)
        {
            var count = bws.Count;
            if (count > 0)
            {
                var dialog = new LoadingForm();
                dialog.CloseIfFinished = true;
                var arg = dialog.Arg;
                arg.Message = "Bewerkingen stoppen...";
                arg.Max = bws.Count;
                arg.Type = ProgressType.WriteBussy;
                arg.OnChanged(null);
                _ = dialog.ShowDialogAsync(this?.ParentForm);
                await Task.Factory.StartNew(new Action(() =>
                {
                    var msg = bws.Count < 10;
                    try
                    {
                        for (var i = 0; i < bws.Count; i++)
                        {
                            var pr = bws[i];
                            if (pr == null)
                                continue;
                            arg.Current = i;
                            var change = $"Bewerking '{pr.Path}' stoppen...";
                            arg.OnChanged(this);
                            pr.xStopProductie(true, true, msg);
                            if (arg.IsCanceled) break;
                        }
                    }
                    catch { }
                    arg.Type = ProgressType.WriteCompleet;
                    arg.OnChanged(this);
                }));
            }
        }

        private void ShowSelectedProducties()
        {
            if (Manager.LogedInGebruiker is { AccesLevel: >= AccesType.ProductieBasis })
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
                MeldGereed(this, p);
            else if (ProductieLijst.SelectedObject is Bewerking bew)
                MeldBewerkingGereed(this, bew);
        }

        public static bool MeldGereed(IWin32Window owner, ProductieFormulier form)
        {
            var p = form;
            if (p != null && p.State != ProductieState.Verwijderd && p.State != ProductieState.Gereed)
            {
                var bews = p.Bewerkingen.Where(t => t.TotaalGemaakt < t.Aantal && t.IsAllowed()).ToArray();
                var res = DialogResult.Yes;
                if (bews.Length > 0)
                    res = XMessageBox.Show(
                        owner,
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

        public static bool MeldBewerkingGereed(IWin32Window owner, Bewerking bewerking)
        {
            var b = bewerking;
            if (b != null && b.State != ProductieState.Verwijderd && b.State != ProductieState.Gereed)
            {
                var res = DialogResult.Yes;
                if (b.TotaalGemaakt < b.Aantal)
                    res = XMessageBox.Show(owner,
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
                var rf = new RangeFilter
                {
                    Enabled = true,
                    Criteria = bws[i].ArtikelNr,
                    Bewerking = bws[i].Naam
                };
                _calcform.Filter = rf;
                _calcform.Show();
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
                var rf = new RangeFilter
                {
                    Enabled = true,
                    Bewerking = bws[i].Naam
                };
                _calcform.Filter = rf;
                _calcform.Show();
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
                    bw.ShowWerktIjden(this);
            }
        }

        private void ShowSelectedStoringen()
        {
            ProductieFormulier form = null;
            if (ProductieLijst.SelectedObject is ProductieFormulier prod)
                form = prod;
            else if (ProductieLijst.SelectedObject is Bewerking bew) form = bew.GetParent();
            if (form == null) return;
            var allst = new AlleStoringenForm();
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
                    XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
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
                    XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
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
            if (ProductieLijst.SelectedObject is IProductieBase prod)
            {
                var xafk = new AfkeurForm(prod);
                xafk.ShowDialog();
            }
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
                XMessageBox.Show(this, "Er zijn geen aanbevelingen", "Geen Aanbevelingen");
            }
        }

        private async void RemoveSelectedProducties()
        {
            if (ProductieLijst.SelectedObjects.Count == 0)
                return;
            var res = XMessageBox.Show(this, "Wil je de geselecteerde producties helemaal verwijderen?\n\n" +
                                             "Click op 'Ja' als je helemaal van de database wilt verwijderen.\n" +
                                             "Click op 'Nee' als je alleen op een verwijderde status wilt te zetten.",
                "",
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
                var dialog = new LoadingForm();
                dialog.CloseIfFinished = true;
                var arg = dialog.Arg;
                arg.Message = "Bewerkingen verwijderen...";
                arg.Max = bws.Count;
                arg.Type = ProgressType.WriteBussy;
                arg.OnChanged(this);
                _ = dialog.ShowDialogAsync(this?.ParentForm);
                var skip = res == DialogResult.No;
                await Task.Factory.StartNew(new Action(() =>
                    {
                        var msg = bws.Count < 10;
                        try
                        {
                            for (var i = 0; i < bws.Count; i++)
                            {
                                var pr = bws[i];
                                if (pr == null)
                                    continue;
                                arg.Current = i;
                                var change = $"Bewerking '{pr.Path}' verwijderen...";
                                arg.OnChanged(this);
                                pr.xRemoveBewerking(skip, res == DialogResult.Yes, msg);
                                if (arg.IsCanceled) break;
                            }
                        }
                        catch { }
                        arg.Type = ProgressType.WriteCompleet;
                        arg.Current = bws.Count;
                        arg.OnChanged(this);
                    }));
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

                var dialog = new LoadingForm();
                dialog.CloseIfFinished = true;
                var arg = dialog.Arg;
                arg.Message = "Bewerkingen terugzetten...";
                arg.Max = bws.Count;
                arg.Type = ProgressType.WriteBussy;
                arg.OnChanged(this);
                _ = dialog.ShowDialogAsync(this.ParentForm);
                await Task.Factory.StartNew(new Action(() =>
                {
                    var msg = bws.Count < 10;
                    try
                    {
                        for (var i = 0; i < bws.Count; i++)
                        {
                            var bw = bws[i];
                            if (bw == null) continue;
                            string key = $"'{bw.Path}' terugzetten...";
                            arg.Current = i;
                            arg.Message = key;
                            arg.OnChanged(this);
                            bw.xUndo(msg);
                            if (arg.IsCanceled) break;
                        }
                    }
                    catch { }
                    arg.Type = ProgressType.WriteCompleet;
                    arg.Current = bws.Count;
                    arg.OnChanged(this);
                }));
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
            try
            {
                var items = ProductieLijst.SelectedObjects?.OfType<IProductieBase>().ToList();
                if (items == null || items.Count == 0) return;
                var item = items.FirstOrDefault();
                var datum = item?.LeverDatum ?? DateTime.Now;
                var x1 = items.Count == 1 && item != null
                    ? $"[{item.Path}|{item.ArtikelNr}]\n\n{item.Omschrijving}"
                    : $"{items.Count} producties";
                var msg = $"Wijzig leverdatum voor {x1}.";
                var dc = new DatumChanger();
                if (dc.ShowDialog(datum, msg) == DialogResult.OK)
                {
                    var dialog = new LoadingForm();
                    dialog.CloseIfFinished = true;
                    var arg = dialog.Arg;
                    arg.Message = "Leverdatums wijzigen...";
                    arg.Max = items.Count;
                    arg.Type = ProgressType.WriteBussy;
                    arg.OnChanged(null);
                    _ = dialog.ShowDialogAsync(this.ParentForm);
                    await Task.Factory.StartNew(new Action(() =>
                    {
                        var msg = items.Count < 10;
                        try
                        {
                            for (var i = 0; i < items.Count; i++)
                            {
                                var pr = items[i];
                                if (pr == null)
                                    continue;
                                arg.Current = i;
                                var xchange = $"Bewerking '{pr.Path}' leverdatum wijzigen...";
                                arg.Message = xchange;
                                arg.OnChanged(this);
                                if (arg.IsCanceled) break;
                                var date = dc.SelectedValue;
                                if (dc.AddTime)
                                {
                                    if (dc.TimeToAdd.TotalHours < 0)
                                        date = Werktijd.EerstVorigeWerkdag(pr.LeverDatum.Add(dc.TimeToAdd));
                                    else
                                        date = Werktijd.EerstVolgendeWerkdag(pr.LeverDatum.Add(dc.TimeToAdd));
                                }
                                var change = $"{pr.Path} | {pr.ArtikelNr} Leverdatum gewijzigd!\n" +
                                             $"Van: {pr.LeverDatum:dd MMMM yyyy HH:mm} uur\n" +
                                             $"Naar: {date:dd MMMM yyyy HH:mm} uur";
                                pr.LeverDatum = date;
                                pr.xUpdate(change, true, true, msg);
                            }
                        }
                        catch { }
                        arg.Type = ProgressType.WriteCompleet;
                        arg.Current = items.Count;
                        arg.OnChanged(this);
                    }));

                }
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private async void ShowSelectedAantal()
        {
            try
            {
                var items = ProductieLijst.SelectedObjects?.OfType<IProductieBase>().ToList();
                if (items == null || items.Count == 0) return;
                var item = items.FirstOrDefault();
                var x1 = items.Count == 1 && item != null
                    ? $"[{item.Path}|{item.ArtikelNr}]\n\n{item.Omschrijving}"
                    : $"{items.Count} producties";
                var msg = $"Wijzig aantal voor {x1}.";
                var aantal = items.Count > 1 ? 0 : item.Aantal;
                var dc = new AantalChanger();
                if (dc.ShowDialog(aantal, msg) == DialogResult.OK)
                {
                    var dialog = new LoadingForm();
                    dialog.CloseIfFinished = true;
                    var arg = dialog.Arg;
                    arg.Message = "Aantallen wijzigen...";
                    arg.Max = items.Count;
                    arg.Type = ProgressType.WriteBussy;
                    arg.OnChanged(null);
                    _ = dialog.ShowDialogAsync(this.ParentForm);
                    await Task.Factory.StartNew(new Action(() =>
                    {
                        var msg = items.Count < 10;
                        try
                        {
                            for (var i = 0; i < items.Count; i++)
                            {
                                var pr = items[i];
                                if (pr == null)
                                    continue;
                                arg.Current = i;
                                var xchange = $"Bewerking '{pr.Path}' aantal wijzigen...";
                                arg.Message = xchange;
                                arg.OnChanged(this);
                                if (arg.IsCanceled) break;

                                var change = $"{pr.Path} | {pr.ArtikelNr} Aantal gewijzigd!\n" +
                                             $"Van: {pr.Aantal}\n" +
                                             $"Naar: {dc.Aantal} uur";
                                pr.Aantal = dc.Aantal;
                                pr.xUpdate(change, true, true, msg);
                            }
                        }
                        catch { }
                        arg.Current = items.Count;
                        arg.Type = ProgressType.WriteCompleet;
                        arg.OnChanged(this);
                    }));
                }
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private async void ShowSelectedNotitie()
        {
            try
            {
                var items = ProductieLijst.SelectedObjects?.OfType<IProductieBase>().ToList();
                if (items == null || items.Count == 0) return;
                var item = items.FirstOrDefault();
                var x1 = items.Count == 1 && item != null
                    ? $"[{item.Path}|{item.ArtikelNr}]\n\n{item.Omschrijving}"
                    : $"{items.Count} producties";
                var msg = $"Wijzig notitie voor {x1}.";
                var dc = new NotitieForms(item.Note, item)
                {
                    Title = msg
                };
                if (dc.ShowDialog() == DialogResult.OK)
                {
                    var dialog = new LoadingForm();
                    dialog.CloseIfFinished = true;
                    var arg = dialog.Arg;
                    arg.Message = "Notities wijzigen...";
                    arg.Max = items.Count;
                    arg.Type = ProgressType.WriteBussy;
                    arg.OnChanged(null);
                    _ = dialog.ShowDialogAsync(this.ParentForm);
                    await Task.Factory.StartNew(new Action(() =>
                    {
                        var msg = items.Count < 10;
                        try
                        {
                            for (var i = 0; i < items.Count; i++)
                            {
                                var pr = items[i];
                                if (pr == null)
                                    continue;
                                arg.Current = i;
                                var xchange = $"Bewerking '{pr.Path}' notitie wijzigen...";
                                arg.Message = xchange;
                                arg.OnChanged(this);
                                if (arg.IsCanceled) break;

                                var change = $"{pr.Path} | {pr.ArtikelNr} Notitie gewijzigd!";
                                pr.Note = dc.Notitie;
                                pr.xUpdate(change, true, true, msg);
                            }
                        }
                        catch { }
                        arg.Type = ProgressType.WriteCompleet;
                        arg.OnChanged(this);
                    }));

                }
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        public ViewState[] GetCurrentViewStates()
        {
            if (this.InvokeRequired)
            {
                var xret = new ViewState[] { };
                this.Invoke(new MethodInvoker(() => xret = GetCurrentViewStates()));
                return xret;
            }
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

        public void SetCurrentViewStates(ViewState[] states, bool reload)
        {
            states ??= new ViewState[0];
            var items = xfiltercontainer.DropDownItems;
            foreach (var tb in items.Cast<ToolStripMenuItem>())
            {
                if (int.TryParse(tb.Tag.ToString(), out var xstate))
                {
                    var state = (ViewState)xstate;
                    tb.Checked = states.Any(x => x == state);
                }
            }
            if (reload)
            {
                UpdateProductieList(true, ShowWaitUI, true);
            }
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
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void ShowSelectedTekening()
        {
            if (ProductieLijst.SelectedObject is IProductieBase prod)
                Tools.ShowSelectedTekening(prod.ArtikelNr, TekeningClosed);
        }

        private void TekeningClosed(object sender, EventArgs e)
        {
            try
            {
                Parent?.BringToFront();
                Parent?.Focus();
            }
            catch { }
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
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }

            return xreturn;
        }

        private void UpdateCheckTogle()
        {
            if (!EnableCheckBox) return;
            var sel = false;
            if (ProductieLijst.SelectedItems.Count > 0)
                sel = !ProductieLijst.SelectedItems.Cast<OLVListItem>().Any(x => x.Checked);
            else sel = ProductieLijst.CheckedObjects.Count == 0;
            if (sel)
                xCheckAllTogle.Image = Resources.checked_accept_32x32;
            else xCheckAllTogle.Image = Resources.checked_done_32x32;
            xCheckAllTogle.Tag = sel;
        }

        public async void PrintSelectedFormulieren()
        {
            try
            {
                var prods = ProductieLijst.SelectedObjects.OfType<IProductieBase>().Distinct(new ProductieDistinctComparer()).ToList();
                if (prods.Count == 0) return;
                var xprint = new PrintDialog();
                xprint.ShowHelp = false;
                xprint.AllowSomePages = false;
                xprint.AllowCurrentPage = false;
                xprint.AllowPrintToFile = false;

                xprint.UseEXDialog = true;
                if (xprint.ShowDialog(this) != DialogResult.OK) return;
                var loading = new LoadingForm();
                loading.CloseIfFinished = true;
                var arg = loading.Arg;
                arg.Type = ProgressType.WriteBussy;
                arg.Message = "Producties Printen...";
                arg.Max = prods.Count;
                arg.OnChanged(this);
                loading.Show();
                loading.BringToFront();
                for(int i= 0; i < prods.Count; i++)
                {
                    if (arg.IsCanceled) break;
                    arg.Current = i + 1;
                    arg.Message = $"'{prods[i].ProductieNr}.pdf' Printen...";
                    arg.OnChanged(this);
                    await Functions.PrintPDFWithAcrobat(this, prods[i], xprint.PrinterSettings);
                }
                arg.Current = arg.Max;
                arg.Message = $"Printen Gereed!";
                arg.Type = ProgressType.WriteCompleet;
                arg.OnChanged(this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
            if (Manager.Opties == null || !CanLoad || _loadingproductielist) return;
            if (e.ClickedItem is ToolStripMenuItem b)
            {
                b.Checked = !b.Checked;
                UpdateProductieList(true, ShowWaitUI, true);
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
                            var flag = bw.IsAllowed() && IsAllowd(bw, true);
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

        private void xbijlage_Click(object sender, EventArgs e)
        {
            if (ProductieLijst.SelectedObject is IProductieBase productie)
            {
                var bl = new BijlageForm(productie);
                bl.Title = $"Bijlages Voor: {productie.ArtikelNr}";
                bl.ShowDialog();
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

        private void xtoonTekening_Click(object sender, EventArgs e)
        {
            ShowSelectedTekening();
        }

        private void filterOpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_selectedSubitem.IsDefault() && sender is ToolStripMenuItem { Tag: FilterType type })
                FilterOp(type, _selectedSubitem);
        }

        private void verwijderFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearTempFilters();
        }

        private void filterOpslaanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var key = "opslaan";
            var value = _selectedSubitem.Value;
            FilterOp(FilterType.FilterOpslaan, new KeyValuePair<string, object>(key,value));
        }

        private void kopiërenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedSubitem.IsDefault())
                return;
            if (string.IsNullOrEmpty(_selectedSubitem.Value?.ToString()))
                Clipboard.Clear();
            else
                Clipboard.SetText(_selectedSubitem.Value.ToString());
        }

        private void xonderbreek_Click(object sender, EventArgs e)
        {
            if (SelectedItem is Bewerking bw) bw.DoOnderbreking(this);
        }

        private void xBeheerweergavetoolstrip_Click(object sender, EventArgs e)
        {
            xBeheerweergavetoolstrip.ShowDropDown();
        }

        private void filterAanmakenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x = Manager.Opties;
            if (x.Filters == null) return;
            x.Filters ??= new List<Filter>();
            string name = null;
            while (true)
            {
                var txt = new TextFieldEditor();
                txt.MultiLine = false;
                txt.MinimalTextLength = 4;
                txt.EnableSecondaryField = false;
                txt.Title = "Kies een Filternaam om aan te maken...";
                if (txt.ShowDialog() != DialogResult.OK) return;
                name = txt.SelectedText.Trim();
                bool flag = x.Filters.Any(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
                if (flag)
                {
                    var res = XMessageBox.Show(this, $"Filternaam '{name}' bestaat al!\n\n" +
                        $"Kies een andere Filternaam a.u.b.", "Filternaam Bestaat Al", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (res != DialogResult.OK) return;
                    continue;
                }
                break;
            }
            if (string.IsNullOrEmpty(name)) return;
            var xcrits = new EditCriteriaForm(typeof(IProductieBase), null, false);
            xcrits.Title = $"Filter '{name}' aanmaken...";
            if (xcrits.ShowDialog() != DialogResult.OK || xcrits.SelectedFilter.Count == 0) return;
            var f = new Filter { IsTempFilter = false, Name = name };
            f.Filters.AddRange(xcrits.SelectedFilter);
            f.ListNames.Add(ListName);
            x.Filters.Add(f);
            Manager.OnFilterChanged(this);
        }

        private void xPrinten_Click(object sender, EventArgs e)
        {
            PrintSelectedFormulieren();
        }

        #endregion MenuButton Events

        #region LayoutMenuStrip

        private void LoadLayout(List<ExcelSettings> settings = null)
        {
            settings ??= Manager.ListLayouts?.GetAlleLayouts();
            foreach (var tb in GetLayoutToolstripItems())
                if (tb.Tag is ExcelSettings xs)
                {
                    tb.Checked = xs.IsUsed(ListName);
                    tb.Image = tb.Checked
                        ? Resources.layout_widget_icon_32x32.CombineImage(Resources.check_1582, 1.5)
                        : Resources.layout_widget_icon_32x32;
                }
        }

        public void InitLayoutStrips(List<ExcelSettings> settings = null)
        {
            //verwijder alle toegevoegde filters
            //items = xfiltersStripItem.DropDownItems.Cast<ToolStripItem>().Where(x => x.Tag != null).ToList();
            //for (int i = 0; i < items.Count; i++)
            //    xfiltersStripItem.DropDownItems.Remove(items[i]);
            //Voeg toe alle filters indien mogelijk
            try
            {
                settings ??= Manager.ListLayouts?.GetAlleLayouts();
                if (settings == null) return;
                var xitems = GetLayoutToolstripItems();
                var xremove = xitems.Where(x => x.Tag is ExcelSettings xset &&
                                                !settings.Any(s => string.Equals(s.Name, xset.Name,
                                                    StringComparison.CurrentCultureIgnoreCase)))
                    .ToList();
                foreach (var f in settings)
                {
                    var xold = xitems.FirstOrDefault(x =>
                        x.Tag is ExcelSettings set &&
                        string.Equals(f.Name, set.Name, StringComparison.CurrentCultureIgnoreCase));
                    if (xold == null)
                    {
                        var xitem = new ToolStripMenuItem(f.Name) { Tag = f };
                        xitem.ToolTipText = f.Name;
                        xitem.Checked = f.IsUsed(ListName);
                        xitem.Image = xitem.Checked
                            ? Resources.layout_widget_icon_32x32.CombineImage(Resources.check_1582, 1.5)
                            : Resources.layout_widget_icon_32x32;
                        xBeheerweergavetoolstrip.DropDownItems.Add(xitem);
                    }
                    else
                    {
                        xold.Tag = f;
                        xold.ToolTipText = f.Name;
                        xold.Checked = f.IsUsed(ListName);
                        xold.Image = xold.Checked
                            ? Resources.layout_widget_icon_32x32.CombineImage(Resources.check_1582, 1.5)
                            : Resources.layout_widget_icon_32x32;
                    }
                    //AddLayoutToolstripItem(f);
                }

                xremove.ForEach(x => xBeheerweergavetoolstrip.DropDownItems.Remove(x));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private List<ToolStripMenuItem> GetLayoutToolstripItems()
        {
            var xret = new List<ToolStripMenuItem>();
            try
            {
                foreach (var xitem in xBeheerweergavetoolstrip.DropDownItems)
                    if (xitem is ToolStripMenuItem { Tag: ExcelSettings } ts)
                        xret.Add(ts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xret;
        }

        private void xLayoutStripItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag is ExcelSettings setting)
            {
                var enabled = !setting.IsUsed(ListName);
                if (enabled)
                {
                    SaveColumns(false);
                    var xitems = Manager.ListLayouts?.GetAlleLayouts();
                    if (xitems == null) return;
                    xitems.ForEach(x =>
                    {
                        x.SetSelected(false, ListName);
                        Manager.ListLayouts?.SaveLayout(x, null, false);
                    });
                }

                setting.SetSelected(enabled, ListName);
                Manager.ListLayouts?.SaveLayout(setting, null, false);

                LoadLayout();
                InitColumns();
            }
        }

        #endregion LayoutMenuStrip

        #region FilterStrip
        private void FilterOp(FilterType type, KeyValuePair<string, object> value)
        {
            if (!value.IsDefault() && Manager.Opties?.Filters != null)
            {
                var xval = value.Value;

                if (type is FilterType.Bevat or FilterType.BevatNiet)
                {
                    var txt = new TextFieldEditor();
                    txt.MultiLine = false;
                    txt.MinimalTextLength = 1;
                    txt.EnableSecondaryField = false;
                    txt.Title = $"Als {value.Key} {Enum.GetName(typeof(FilterType), type)}...";
                    txt.SelectedText = value.Value.ToString();
                    if (txt.ShowDialog() != DialogResult.OK) return;
                    xval = txt.SelectedText.Trim();
                }
                else if(type is FilterType.FilterOpslaan)
                {
                    var crits = Manager.Opties.Filters.Where(x => x.IsTempFilter).SelectMany(x=> x.Filters).ToList();
                    if (!crits.Any()) return;
                    string name = null;
                    while (true)
                    {
                        var txt = new TextFieldEditor();
                        txt.MultiLine = false;
                        txt.MinimalTextLength = 4;
                        txt.EnableSecondaryField = false;
                        txt.Title = "Kies een Filternaam om op te slaan...";
                        if (txt.ShowDialog() != DialogResult.OK) return;
                        name = txt.SelectedText.Trim();
                        bool flag = Manager.Opties.Filters.Any(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
                        if(flag)
                        {
                            var res = XMessageBox.Show(this, $"Filternaam '{name}' bestaat al!\n\n" +
                                $"Kies een andere Filternaam a.u.b.", "Filternaam Bestaat Al", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            if (res != DialogResult.OK) return;
                            continue;
                        }
                        break;
                    }
                    if (string.IsNullOrEmpty(name)) return;

                    var f = new Filter { IsTempFilter = false, Name = name };
                    f.Filters.AddRange(crits);
                    Manager.Opties.Filters.Add(f);
                    Manager.Opties.Save($"{name} Filter is opgeslagen!");
                    return;
                }
                var fe = FilterEntry.CreateNewFromValue(value.Key,
   xval, type);
                fe.OperandType = Operand.ALS;
                var xold = Manager.Opties.Filters.FirstOrDefault(x =>
                    x.ListNames.Any(f => string.Equals(f, ListName, StringComparison.CurrentCultureIgnoreCase)) &&
                    x.Filters.Any(s => s.Equals(fe)));
                if (xold != null) return;
                var xf = new Filter { IsTempFilter = true, Name = fe.PropertyName };
                xf.ListNames.Add(ListName);
                xf.Filters.Add(fe);
                //_selectedSubitem = new KeyValuePair<string, object>();
                //InitContextFilterToolStripItems(xfiltertoolstripitem);
                Manager.Opties.Filters.Add(xf);
                InitFilterStrips();
                Manager.OnFilterChanged(this);
            }
        }

        private void ClearTempFilters()
        {
            if (Manager.Opties?.Filters == null) return;
            var xlast = Manager.Opties.Filters.LastOrDefault(x =>
                x.IsTempFilter &&
                x.ListNames.Any(s => string.Equals(s, ListName, StringComparison.CurrentCultureIgnoreCase)));
            if (xlast != null && Manager.Opties.Filters.Remove(xlast))
                Manager.OnFilterChanged(this);
        }

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
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(InitFilterStrips));
                return;
            }
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

        #region ContextMenuStrip
        private void InitContextFilterToolStripItems(ToolStripMenuItem item)
        {
            var xitems = item.DropDownItems;
            for (var i = 0; i < xitems.Count; i++)
            {
                var xitem = xitems[i];
                if (xitem.Tag != null && xitem.Tag.GetType() == typeof(FilterType))
                    item.DropDownItems.RemoveAt(i--);
            }

            if (!_selectedSubitem.IsDefault())
            {
                var xv = FilterEntry.GetFilterTypesByType(_selectedSubitem.Value == null
                    ? typeof(string)
                    : _selectedSubitem.Value.GetType());
                if (xv.Count == 0) return;
                var xval = _selectedSubitem.Value?.ToString() ?? "";
                if (xval.Length > 8)
                    xval = xval.Substring(0, 8) + "...";

                foreach (var type in xv)
                {
                    if (type == FilterType.FilterOpslaan) continue;
                    var xtype = Enum.GetName(typeof(FilterType), type);
                    var xtxt = type == FilterType.FilterOpslaan? "Filter(s) Opslaan" : $"Als [{_selectedSubitem.Key}] {xtype} '{xval}'";
                    var xf = (ToolStripMenuItem)item.DropDownItems[xtype];
                    if (xf == null)
                    {
                        xf = new ToolStripMenuItem(xtxt, null, filterOpToolStripMenuItem_Click);
                        xf.Tag = type;
                        xf.ShortcutKeys = GetShortCut(type);
                        xf.Name = xtype;
                        item.DropDownItems.Add(xf);
                    }
                    else
                    {
                        xf.Text = xtxt;
                    }
                }
            }
        }

        private Keys GetShortCut(FilterType type)
        {
            switch (type)
            {
                case FilterType.None:
                    return Keys.None;
                case FilterType.BegintMet:
                    return Keys.Alt | Keys.B;
                case FilterType.EindigtMet:
                    return Keys.Alt | Keys.E;
                case FilterType.GelijkAan:
                    return Keys.Alt | Keys.F;
                case FilterType.NietGelijkAan:
                    return Keys.Alt | Keys.O;
                case FilterType.Bevat:
                    return Keys.Alt | Keys.W;
                case FilterType.BevatNiet:
                    return Keys.Alt | Keys.Q;
                case FilterType.KleinerDan:
                    return Keys.Alt | Keys.K;
                case FilterType.KleinerOfGelijkAan:
                    return Keys.Alt | Keys.J;
                case FilterType.GroterDan:
                    return Keys.Alt | Keys.G;
                case FilterType.GroterOfGelijkAan:
                    return Keys.Alt | Keys.H;
                case FilterType.FilterOpslaan:
                    return Keys.Alt | Keys.S;
            }

            return Keys.None;
        }
        #endregion ContextMenuStrip

        #region ProductieLijst Events

        private void xproductieLijst_SelectedIndexChanged(object sender, EventArgs e)
        {
            _WaitTimer?.Stop();
            _WaitTimer?.Start();
        }

        private void xproductieLijst_DoubleClick(object sender, EventArgs e)
        {
            if (EnableCheckBox)
            {
                if (ProductieLijst.SelectedItem != null)
                    ProductieLijst.SelectedItem.Checked = !ProductieLijst.SelectedItem.Checked;
            }
            else
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
                            var first = openstaand.FirstOrDefault(x => !x.IsVerholpen);
                            if (openstaand.Count == 1 && first != null)
                                xstmessage +=
                                    $"\n{first.StoringType} {(string.IsNullOrEmpty(first.Omschrijving) ? "" : first.Omschrijving.Trim())} van {first.TotaalTijd} uur";
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
                        var first = open.FirstOrDefault(x => !x.IsVerholpen);
                        if (open.Length == 1 && first != null)
                            xstmessage +=
                                $"\n{first.StoringType} {(string.IsNullOrEmpty(first.Omschrijving) ? "" : first.Omschrijving.Trim())} van {first.TotaalTijd} uur";
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

        private void xListColumnsButton_Click(object sender, EventArgs e)
        {
            var xf = new ExcelOptiesForm();
            xf.EnableCalculation = false;
            SaveColumns(false);
            xf.LoadOpties(ListName, false);
            if (xf.ShowDialog() == DialogResult.OK)
            {
                Manager.UpdateExcelColumns(xf.Settings, true, true, false, new List<string> { ListName });
                InitColumns();
            }
        }

        private void xProductieLijst_FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.Column.Tag is ExcelColumnEntry entry && e.Model is IProductieBase productie)
                FormatCell(e.SubItem, entry, productie);
        }

        private void xProductieLijst1_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            if (e.SubItem?.ModelValue is not null)
                _selectedSubitem =
                    new KeyValuePair<string, object>(e.Column.AspectName, e.Model.GetPropValue(e.Column.AspectName));
            else
                _selectedSubitem = new KeyValuePair<string, object>();
            InitContextFilterToolStripItems(xfiltertoolstripitem);
        }

        private void xProductieLijst1_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.SubItem?.ModelValue is not null)
                _selectedSubitem =
                    new KeyValuePair<string, object>(e.Column.AspectName, e.Model.GetPropValue(e.Column.AspectName));
            else
                _selectedSubitem = new KeyValuePair<string, object>();
            InitContextFilterToolStripItems(xfiltertoolstripitem);
        }

        private KeyValuePair<string, object> _selectedSubitem;

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
                        if (k.Filter != null && (k.ColorIndex > -1 || k.ColorRGB != 0))
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
            if (e.OldDisplayIndex == 0 || e.NewDisplayIndex == 0)
            {
                e.Cancel = true;
            }
            else
            {
                if (e.Header.Tag is ExcelColumnEntry entry)
                {
                    var xset = Manager.ListLayouts?.GetAlleLayouts().FirstOrDefault(x =>
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
                    SaveColumns(false);
                    // Manager.ColumnsSettingsChanged(xset);
                }
            }
        }

        private void xproductieLijstcontext_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = ProductieLijst.Items.Count == 0 || !EnableContextMenu;
        }

        private void xCheckAllTogle_Click(object sender, EventArgs e)
        {
            if (!EnableCheckBox) return;
            var sel = (xCheckAllTogle.Tag is bool val && val);
            if (ProductieLijst.SelectedItems.Count > 0)
            {
                foreach (var item in ProductieLijst.SelectedItems.Cast<OLVListItem>())
                {
                    item.Checked = sel;
                }
                ProductieLijst.SelectedItems[0].EnsureVisible();
            }
            else if (ProductieLijst.Items.Count > 0)
            {
                foreach (var item in ProductieLijst.Items.Cast<OLVListItem>())
                    item.Checked = sel;
            }
            UpdateCheckTogle();
        }

        #endregion ProductieLijst Events

        #region Events

        public event EventHandler SelectedItemChanged;
        public event EventHandler ItemCountChanged;
        public event EventHandler SearchItems;

        protected virtual void OnSelectedItemChanged()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(OnSelectedItemChanged));
            else
                SelectedItemChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnItemCountChanged()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(OnItemCountChanged));
            else
                ItemCountChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnSearchItems(object sender)
        {
            SearchItems?.Invoke(sender, EventArgs.Empty);
        }

        #endregion Events

        private void xToonVDatumsbutton_Click(object sender, EventArgs e)
        {
            if (ProductieLijst.Items.Count == 0) return;
            var items = ProductieLijst.Objects.OfType<Bewerking>().ToList();
            var form = new ProductieDatumOvericht(items);
            form.ShowDialog();
        }

        private void xberekendatums_Click(object sender, EventArgs e)
        {
            bool sel = false;
            var prods = new List<IProductieBase>();
            if (ProductieLijst.SelectedObjects?.Count > 1)
            {
                sel = true;
                prods = ProductieLijst.SelectedObjects.Cast<IProductieBase>().ToList();
            }
            else
            {
                prods = ProductieLijst.Objects.Cast<IProductieBase>().ToList();
            }
            if (prods.Count == 0) return;
            var x0 = sel ? " geselecteerde" : "";
            var x1 = prods.Count == 1 ? " productie" : " producties";
            var res = XMessageBox.Show(this, $"Weet u zeker dat u van {prods.Count}{x0}{x1} een berekening van de leverdatum wilt maken?\n\n" +
                $"De berekening wordt opgeslagen in de 'BerekendLeverDatum' info veld", "Leverdatums Berekenen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res != DialogResult.Yes) return;
            var loading = new LoadingForm();
            loading.CloseIfFinished = true;
            loading.ShowDialogAsync(this.ParentForm);
            var d = prods.BerekenLeverDatums(true, true, false, loading.Arg);
            if (!d.IsDefault())
            {
                x1 = loading.Arg.Current == 1 ? " productie" : " producties";
                XMessageBox.Show(this, $"BerekendLeverdatum is succesvol aangepast voor {loading.Arg.Current}{x1}!\n\n" +
                    $"Met alles ben je op {d.ToString("f")} klaar");
            }
        }

        private void xok_Click(object sender, EventArgs e)
        {
            OnItemsChosen();
        }

        private void xannuleren_Click(object sender, EventArgs e)
        {
            OnItemsCancel();
        }

        public event EventHandler ItemsChosen;
        public event EventHandler ItemsCancel;
        protected virtual void OnItemsChosen()
        {
            ItemsChosen?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnItemsCancel()
        {
            ItemsCancel?.Invoke(this, EventArgs.Empty);
        }
    }
}