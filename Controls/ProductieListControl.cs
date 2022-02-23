using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
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
using Various;
using Timer = System.Timers.Timer;

namespace Controls
{
    public partial class ProductieListControl : UserControl
    {
        private bool _enableEntryFilter;

        private bool _enableFilter;

        protected Timer _WaitTimer;
        //private object _selectedItem;

        public ProductieListControl()
        {
            InitializeComponent();
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
        public ObjectListView ProductieLijst => xProductieLijst1;

        public string ListName { get; set; }
        public bool RemoveCustomItemIfNotValid { get; set; }
        public bool CustomList { get; private set; }
        public List<ProductieFormulier> Producties { get; private set; } = new();
        public List<Bewerking> Bewerkingen { get; private set; } = new();
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

        private void ProductieLijst_BeforeSearching(object sender, BeforeSearchingEventArgs e)
        {
            var filter = TextMatchFilter.Contains(ProductieLijst, e.StringToFind);
            //this.ProductieLijst.ModelFilter = filter;
            ProductieLijst.DefaultRenderer = new HighlightTextRenderer(filter);
        }

        private void _WaitTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _WaitTimer?.Stop();
            if (Disposing || IsDisposed) return;
            BeginInvoke(new Action(() =>
            {
                SetButtonEnable();
                OnSelectedItemChanged();
            }));
        }

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
            bool reload)
        {
            Bewerkingen = bewerkingen;
            InitProductie(true, enablefilter, true, initlist, loadproducties, reload);
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
                    xsearch.TextChanged -= xsearchbox_TextChanged;
                    xsearch.TextChanged += xsearchbox_TextChanged;
                    if (InvokeRequired)
                    {
                        Invoke(new MethodInvoker(() =>
                        {
                            //InitImageList();
                            InitColumns();
                        }));
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
                UpdateProductieList(reload, true);
        }

        private void UpdateStatusText()
        {
            try
            {
                var xvalue = "Totaal";
                var xitems = new List<IProductieBase>();
                if (ProductieLijst.SelectedObjects.Count > 0)
                {
                    xitems = ProductieLijst.SelectedObjects.Cast<IProductieBase>().ToList();
                    xvalue = "Geselecteerd:";
                }
                else if (ProductieLijst.Objects != null)
                {
                    xitems = ProductieLijst.Objects.Cast<IProductieBase>().ToList();
                    xvalue = "Totaal";
                }

                var x0 = xitems.Count == 1
                    ? $"({xitems[0].ArtikelNr}){xitems[0].Omschrijving}"
                    : xitems.Count.ToString();
                var x1 = xitems.Count == 1 ? "" : " producties";
                var xtijd = xitems.Sum(x => x.TijdGewerkt);
                var xtotaaltijd = xitems.Sum(x => x.DoorloopTijd);
                var xpu = xitems.Count > 0 ? Math.Round(xitems.Sum(x => x.ActueelPerUur) / xitems.Count, 0) : 0;
                var xgemaakt = xitems.Sum(x => x.TotaalGemaakt);
                var xtotaal = xitems.Sum(x => x.Aantal);
                var xret = $"<span color='{Color.Navy.Name}'>" +
                           $"{xvalue} <b>{x0}</b>{x1}, Gemaakt <b>{xgemaakt}/ {xtotaal}</b>, in <b>{xtijd}/ {xtotaaltijd} uur</b>, met een gemiddelde van <b>{xpu}p/u</b>" +
                           "</span>";
                //xStatusLabel.Text = xret;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SetButtonEnable()
        {
            try
            {
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
                xtoonTekening.Enabled = enable1;
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
                //resetToolStripMenuItem.Visible = Manager.Opties?.Filters?.Any(x =>
                //    x.IsTempFilter && x.ListNames.Any(s =>
                //        string.Equals(s, ListName, StringComparison.CurrentCultureIgnoreCase))) ?? false;
                //UpdateStatusText();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
                    if (Disposing || IsDisposed) return;
                    xloadinglabel.Invoke(new MethodInvoker(() => { xloadinglabel.Visible = true; }));

                    var cur = 0;
                    var xwv = IsBewerkingView ? "Bewerkingen Laden" : "Producties laden";
                    //var xcurvalue = xwv;
                    var tries = 0;
                    while (_iswaiting && tries < 200)
                    {
                        if (cur > 5) cur = 0;
                        if (Disposing || IsDisposed) return;
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
                    }
                }
                catch (Exception e)
                {
                }

                if (Disposing || IsDisposed) return;
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
            ximagelist.Images.Add(img.CombineImage(Resources.Stop_Hand__32x32, 1.5)); // onderbroken document

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
            ximagelist.Images.Add(img.CombineImage(Resources.Stop_Hand__32x32, 1.5)
                .CombineImage(Resources.Note_msgIcon_32x32, ContentAlignment.TopLeft,
                    imgscale)); // onderbroken document

            imgscale = 1.75;
            //zelfde afbeeldingen, maar dan met de combi icon
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
            ximagelist.Images.Add(img.CombineImage(Resources.Stop_Hand__32x32, 1.5)
                .CombineImage(Resources.Note_msgIcon_32x32, ContentAlignment.TopLeft,
                    imgscale));
        }

        private int GetProductieImageIndex(IProductieBase productie)
        {
            if (productie == null) return 0;
            return productie.GetImageIndexFromList(ximagelist);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var e = new KeyEventArgs(keyData);
            if (e.Control && e.KeyCode == Keys.L)
            {
                xListColumnsButton_Click(this, EventArgs.Empty);
                return true;
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
            if (sender is IProductieBase productie) return productie.VerpakkingsInstructies?.VerpakkingType ?? "n.v.t.";

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

        private string GetBewerkingGroup(Bewerking bew)
        {
            var group = "";
            if (Manager.Opties is { Bewerkingen: { } } &&
                (Manager.Opties.ToonAllesVanBeide || Manager.Opties.ToonVolgensBewerkingen))
                group = Manager.Opties.Bewerkingen
                    .FirstOrDefault(t => bew.Naam.Split('[')[0].ToLower() == t.ToLower());
            else group = bew.Naam;

            if (bew == null)
                return "Zonder Bewerkingen";
            return group;
        }

        #endregion ProductieLijstGetters

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
                var xreturn = true;
                foreach (var filter in filters)
                    if (filter.ListNames.Any(x =>
                            string.Equals(ListName, x, StringComparison.CurrentCultureIgnoreCase)))
                        xreturn &= filter.IsAllowed(bewerking, ListName);

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

        private void UpdateListObjects<T>(List<T> objects)
        {
            if (objects != null && (objects.Count != ProductieLijst.Items.Count || ProductieLijst.Items.Count > 0 &&
                    !ProductieLijst.Objects.Cast<T>().Any(x => objects.Any(o => o.Equals(x)))))
            {
                ProductieLijst.BeginUpdate();
                //var sel = ProductieLijst.SelectedObject;
                ProductieLijst.SetObjects(objects);
                //ProductieLijst.SelectedObject = sel;
                //ProductieLijst.SelectedItem?.EnsureVisible();
                ProductieLijst.EndUpdate();
                OnItemCountChanged();
            }
        }

        public List<Bewerking> GetBewerkingen(bool reload, bool customfilter)
        {
            var states = GetCurrentViewStates();
            var bws = !reload && CustomList && Bewerkingen != null
                ? Bewerkingen.Where(x => states.Any(x.IsValidState) && x.ContainsFilter(GetFilter()))
                    .ToList()
                : Bewerkingen = Manager.GetBewerkingen(states, true).Result;
            if (customfilter && ValidHandler != null)
                bws = bws.Where(x => IsAllowd(x) && ValidHandler.Invoke(x, GetFilter()))
                    .ToList();
            else
                bws = bws.Where(x => IsAllowd(x) && x.IsAllowed(GetFilter())).ToList();
            return bws;
        }

        public List<ProductieFormulier> GetProducties(bool reload, bool customfilter)
        {
            var states = GetCurrentViewStates();
            var xprods = !reload && CustomList && Producties != null
                ? Producties.Where(x => states.Any(x.IsValidState) && x.ContainsFilter(GetFilter()))
                    .ToList()
                : Producties = Manager.GetProducties(states, true, true, null).Result;
            if (customfilter && ValidHandler != null)
                xprods = xprods.Where(x => IsAllowd(x) && ValidHandler.Invoke(x, GetFilter()))
                    .ToList();
            else
                xprods = xprods.Where(x => IsAllowd(x) && x.IsAllowed(GetFilter(), states, true))
                    .ToList();
            return xprods;
        }

        public void UpdateProductieList(bool reload, bool showwaitui)
        {
            if (Manager.Opties == null || !CanLoad) return;
            try
            {
                if (InvokeRequired)
                    Invoke(new MethodInvoker(() => { xUpdateProductieList(reload, showwaitui); }));
                else
                    xUpdateProductieList(reload, showwaitui);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void xUpdateProductieList(bool reload, bool showwaitui)
        {
            if (Manager.Opties == null || !CanLoad) return;
            try
            {
                if (showwaitui)
                    SetWaitUI();
                try
                {
                    _loadingproductielist = true;
                    InitFilterStrips();
                    var selected1 = ProductieLijst.SelectedObjects;
                    var groups1 = ProductieLijst.Groups.Cast<ListViewGroup>().Select(t => (OLVGroup)t.Tag)
                        .Where(x => x.Collapsed)
                        .ToArray();
                    // Manager.Opties.ProductieWeergaveFilters = GetCurrentProductieViewStates();


                    var xlistcount = ProductieLijst.Items.Count;
                    if (!IsBewerkingView)
                    {
                        if (CanLoad)
                        {
                            var xprods = GetProducties(reload, true);
                            UpdateListObjects(xprods);
                        }
                    }
                    else if (CanLoad)
                    {
                        var bws = GetBewerkingen(reload, true);
                        UpdateListObjects(bws);
                    }

                    var xgroups = ProductieLijst.Groups.Cast<ListViewGroup>().ToList();
                    if (groups1.Length > 0)
                        for (var i = 0; i < xgroups.Count; i++)
                        {
                            var group = xgroups[i].Tag as OLVGroup;
                            if (group == null)
                                continue;
                            if (groups1.Any(t => !group.Collapsed && t.Header == group.Header))
                                group.Collapsed = true;
                        }

                    SetButtonEnable();
                    OnSelectedItemChanged();
                    //if (xfocused != null)
                    //{
                    //    ProductieLijst.FocusedObject = xfocused;
                    //    ProductieLijst.FocusedItem?.EnsureVisible();
                    //}

                    ProductieLijst.SelectedObjects = selected1;
                    if (ProductieLijst.SelectedObject == null)
                        ProductieLijst.SelectedIndex = 0;
                    //if (xfocused == null)
                    //    ProductieLijst.SelectedItem?.EnsureVisible();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                _loadingproductielist = false;
                if (EnableSync)
                    StartSync();
                StopWait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public bool UpdateFormulier(ProductieFormulier form)
        {
            if (IsDisposed || Disposing || form == null || _loadingproductielist)
                return false;

            try
            {
                var filter = xsearch.Text.ToLower() == "zoeken..."
                    ? null
                    : xsearch.Text.Trim();

                var states = GetCurrentViewStates();
                var changed = false;
                var xreturn = false;
                var xselected = ProductieLijst.SelectedObjects;
                if (!IsBewerkingView)
                {
                    var isvalid = IsAllowd(form) && form.IsAllowed(filter, states, true);
                    if (isvalid && ValidHandler != null)
                        isvalid &= ValidHandler.Invoke(form, filter);

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
                {
                    if (ProductieLijst.Items.Count > 0)
                    {
                        ProductieLijst.SelectedObjects = xselected;
                        if (ProductieLijst.SelectedObject == null) ProductieLijst.SelectedIndex = 0;
                    }

                    OnItemCountChanged();
                }

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
                    BeginInvoke(new Action(() =>
                    {
                        var states = GetCurrentViewStates();
                        if (Producties is { Count: > 0 })
                            for (var i = 0; i < Producties.Count; i++)
                            {
                                var prod = Producties[i];
                                var xprod = Manager.Database.GetProductie(prod.ProductieNr);
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
                                    _ = xprod.UpdateForm(true, false, null, "", false, false, false).Result;
                                    Producties[i] = xprod;
                                    ProductieLijst.RefreshObject(xprod);
                                }
                            }

                        if (Bewerkingen is { Count: > 0 })
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
                                    _ = xbew.Parent.UpdateForm(true, false, null, "", false, false, false).Result;
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
                        var isvalid = IsAllowd(b) && b.IsAllowed(filter ?? GetFilter()) &&
                                      states.Any(x => b.IsValidState(x));
                        if (isvalid && ValidHandler != null)
                            isvalid &= ValidHandler.Invoke(b, filter ?? GetFilter());

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
                                else
                                    Bewerkingen.Add(b);
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
                        var bws = ProductieLijst.Objects?.Cast<Bewerking>().Where(x =>
                            string.Equals(id, x.ProductieNr, StringComparison.CurrentCultureIgnoreCase)).ToArray();
                        if (bws is { Length: > 0 })
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
                {
                    var xfilters = Manager.Opties.Filters.Where(x =>
                            x.ListNames.Any(l => string.Equals(ListName, l, StringComparison.CurrentCultureIgnoreCase)))
                        .ToList();
                    var reload = false;
                    if (xfilters.Count == 0)
                        reload = true;
                    else
                        reload = !xfilters.All(x => x.IsTempFilter);
                    BeginInvoke(new MethodInvoker(() => { UpdateProductieList(reload, false); }));
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
                        UpdateProductieList(true, true);
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
            if (IsDisposed || Disposing || !IsLoaded)
                return;
            try
            {
                if (InvokeRequired)
                    BeginInvoke(new MethodInvoker(() => UpdateFormulier(changedform)));
                else UpdateFormulier(changedform);
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

        private void Manager_OnFormulierDeleted(object sender, string id)
        {
            if (IsDisposed || Disposing || !IsLoaded || string.IsNullOrEmpty(id)) return;
            DeleteID(id);
        }

        #endregion Manager Events

        #region Search

        public bool DoSearch(string criteria)
        {
            try
            {
                var crit1 = string.IsNullOrEmpty(criteria) ? "Zoeken..." : criteria;
                var crit2 = string.IsNullOrEmpty(xsearch.Text.Trim()) ? "Zoeken..." : xsearch.Text.Trim();
                var flag = string.Equals(crit1, crit2, StringComparison.CurrentCultureIgnoreCase) ||
                           crit1.ToLower().Trim() == "zoeken..." || _iswaiting;
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

        private void xsearchbox_TextChanged(object sender, EventArgs e)
        {
            if (xsearch.Text.ToLower().Trim() != "zoeken..." && !_iswaiting)
            {
                OnSearchItems(xsearch.Text.Trim());
                UpdateProductieList(false, false);
            }
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

                StopBewerkingen(bws.ToArray());
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

                                if (wp.Werk.UpdateBewerking(null,
                                        $"{wp.Path} indeling aangepast", false, false)
                                    .Result) werk.CopyTo(ref werk);
                            }
                        }
                    }

                    if (werk != null) return true;
                }

                return false;
            }

            return false;
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
                        var action = new Task<bool>(() => bw.StopProductie(true, true).Result);
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
                var rf = new ZoekProductiesUI.RangeFilter
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
                var rf = new ZoekProductiesUI.RangeFilter
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
                var skip = res == DialogResult.No;
                var taskf = new Dictionary<string, Task<bool>>();
                for (var i = 0; i < bws.Count; i++)
                {
                    var pr = bws[i];
                    if (pr == null)
                        continue;
                    var action = new Task<bool>(() => pr.RemoveBewerking(skip, res == DialogResult.Yes).Result);
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
                //    XMessageBox.Show(this, $"{done} {xvalue} succesvol verwijderd!", "Verwijderd", MessageBoxButtons.OK,
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
                    XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
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
                    XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
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
                    XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
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
                    XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
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
            Parent?.BringToFront();
            Parent?.Focus();
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
                UpdateProductieList(true, true);
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

        private void InitFilterToolStripItems()
        {
            var xitems = xfiltertoolstripitem.DropDownItems;
            for (var i = 0; i < xitems.Count; i++)
            {
                var xitem = xitems[i];
                if (xitem.Tag != null && xitem.Tag.GetType() == typeof(FilterType))
                    xfiltertoolstripitem.DropDownItems.RemoveAt(i--);
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
                    var xtype = Enum.GetName(typeof(FilterType), type);
                    var xtxt = $"Als [{_selectedSubitem.Key}] {xtype} '{xval}'";
                    var xf = (ToolStripMenuItem)xfiltertoolstripitem.DropDownItems[xtype];
                    if (xf == null)
                    {
                        xf = new ToolStripMenuItem(xtxt, null, filterOpToolStripMenuItem_Click);
                        xf.Tag = type;
                        xf.ShortcutKeys = GetShortCut(type);
                        xf.Name = xtype;
                        xfiltertoolstripitem.DropDownItems.Add(xf);
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
                    return Keys.Alt | Keys.N;
                case FilterType.Kleinerdan:
                    return Keys.Alt | Keys.K;
                case FilterType.KleinerOfGelijkAan:
                    return Keys.Alt | Keys.J;
                case FilterType.Groterdan:
                    return Keys.Alt | Keys.G;
                case FilterType.GroterOfGelijkAan:
                    return Keys.Alt | Keys.H;
            }

            return Keys.None;
        }

        private void FilterOp(FilterType type, KeyValuePair<string, object> value)
        {
            if (!value.IsDefault() && Manager.Opties?.Filters != null)
            {
                var fe = FilterEntry.CreateNewFromValue(value.Key,
                    value.Value, type);
                fe.OperandType = Operand.ALS;
                var xold = Manager.Opties.Filters.FirstOrDefault(x =>
                    x.ListNames.Any(f => string.Equals(f, ListName, StringComparison.CurrentCultureIgnoreCase)) &&
                    x.Filters.Any(s => s.Equals(fe)));
                if (xold != null) return;
                var xf = new Filter { IsTempFilter = true, Name = fe.PropertyName };
                xf.ListNames.Add(ListName);
                xf.Filters.Add(fe);
                _selectedSubitem = new KeyValuePair<string, object>();
                InitFilterToolStripItems();
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
            InitFilterToolStripItems();
        }

        private void xProductieLijst1_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.SubItem?.ModelValue is not null)
                _selectedSubitem =
                    new KeyValuePair<string, object>(e.Column.AspectName, e.Model.GetPropValue(e.Column.AspectName));
            else
                _selectedSubitem = new KeyValuePair<string, object>();
            InitFilterToolStripItems();
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

        private void xProductieLijst1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex > ProductieLijst.Columns.Count - 1)
                return;
            if (ProductieLijst.Columns[e.ColumnIndex]?.Tag is ExcelColumnEntry entry)
            {
                if (entry.ColumnBreedte == e.NewWidth) return;
                SaveColumns(false);
                //var xset = Manager.Opties.ExcelColumns.FirstOrDefault(x =>
                //    x.IsUsed(ListName) && !x.IsExcelSettings);
                //if (xset == null) return;
                //entry.ColumnBreedte = ProductieLijst.Columns[e.ColumnIndex].Width;
            }
        }

        #endregion ProductieLijst Events

        #region Events

        public event EventHandler SelectedItemChanged;
        public event EventHandler ItemCountChanged;
        public event EventHandler SearchItems;

        protected virtual void OnSelectedItemChanged()
        {
            SelectedItemChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnItemCountChanged()
        {
            ItemCountChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnSearchItems(object sender)
        {
            SearchItems?.Invoke(sender, EventArgs.Empty);
        }

        #endregion Events
    }
}