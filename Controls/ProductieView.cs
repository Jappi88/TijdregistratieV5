using AutoUpdaterDotNET;
using Forms;
using Forms.Aantal;
using Forms.ArtikelRecords;
using Forms.Excel;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using ProductieManager.Forms;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using ProductieManager.Rpm.Various;
using Rpm.Controls;
using Rpm.DailyUpdate;
using Rpm.Mailing;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.SqlLite;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductieManager.Forms.Welcome;
using Various;

namespace Controls
{
    public partial class ProductieView : UserControl
    {
        public ProductieView()
        {
            InitializeComponent();
            _specialRoosterWatcher = new Timer();
            _specialRoosterWatcher.Interval = 60000; //1 minuut;
            _specialRoosterWatcher.Tick += (x, y) => CheckForSpecialRooster(false);
            DailyMessage = new Daily
            {
                ImageList =
                {
                    ImageSize = new Size(128, 128)
                }
            };
            DailyMessage.DailyCreated += DailyMessage_DailyCreated;
        }

        private void DailyMessage_DailyCreated(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(ShowDaily));
            else ShowDaily();
        }

        private void ShowDaily()
        {
            try
            {
                if (DailyMessage.CreatedDailies.Count > 0)
                {
                    var xenum = Application.OpenForms.GetEnumerator();
                    var xremove = new List<Form_Alert>();
                    while (xenum.MoveNext())
                    {
                        if (xenum.Current is Form_Alert alert)
                        {
                            xremove.Add(alert);
                        }
                    }
                    xremove.ForEach(x =>
                    {
                        x.Close();
                        x.Dispose();
                    });
                    var xf = new DailyMessageForm(DailyMessage);
                    xf.Height = (80 + (DailyMessage.CreatedDailies.Count * 250));
                    if (xf.Height > 850)
                        xf.Height = 850;
                    xf.Width = DailyMessage.CreatedDailies.Count > 1 ? 1100 : 850;
                    xf.HtmlText = string.Join("\r\n", DailyMessage.CreatedDailies);
                    xf.ShowDialog();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public event EventHandler OnFormLoaded;

        protected void FormLoaded()
        {
            OnFormLoaded?.Invoke(this, EventArgs.Empty);
        }

        #region Variables

        private readonly Timer _specialRoosterWatcher;
        public static Manager _manager;

        public int ProductieRefreshInterval { get; set; } = 10000; // 10 sec

        public bool ProductieSyncEnabled { get; set; }
        //private PersoneelsForm _persform;
        //private LogForm _logform;
        //private AlleStoringen _storingen;
        //private AlleVaardigheden _vaardigeheden;
        //private RangeCalculatorForm _rangeform;

        private static readonly List<StartProductie> _formuis = new();
        private static readonly List<ProductieLijstForm> _Productelijsten = new();
        private static Producties _producties;
        private static ProductieLijsten _productielijstdock;
        private static LogForm _logform;
        private static ChatForm _chatform;
        private static OpmerkingenForm _opmerkingform;
        private static ArtikelsForm _ArtikelsForm;
        private static PersoneelsForm _PersoneelForm;
        private static ProductieOverzichtForm _ProductieOverzicht;
        private static KlachtenForm _Klachten;
        private static PersoneelIndelingForm _PersoneelIndeling;
        private static WerkplaatsIndelingForm _WerkplaatsIndeling;
        private static MetroForm _berekenverbruik;

        private readonly Daily DailyMessage;
        public bool ShowUnreadMessage { get; set; }

        // [NonSerialized] private Opties _opties;

        //private string[] _groups = null;

        #endregion Variables

        #region Manager
        private void InitManager(string path, bool autologin, bool disposeold)
        {
            try
            {
                if (_manager == null || disposeold)
                {
                    _manager?.Dispose();
                    //if (_manager == null)
                    _manager = new Manager(false);
                }

                DetachEvents();
                //if (Manager.DefaultSettings is {WelcomeShown: false})
                //{
                //    var xwelcome = new WelcomeForm();
                //    xwelcome.ShowDialog();
                //}
                _manager.InitManager();
                takenManager1.InitManager();
                InitEvents();
                _manager.Load(path, autologin, true, true);
                if (Manager.Opmerkingen != null)
                    Manager.Opmerkingen.OnOpmerkingenChanged += Opmerkingen_OnOpmerkingenChanged;
                // _manager.StartMonitor();
                FormLoaded();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #region Tab Buttons
        private void InitWerkplekkenUITab(TileInfoEntry entry, bool select)
        {
            if (CheckTab(entry, select))
                return;
            var xtabpage = new MetroTabPage();
            xtabpage.Padding = new Padding(5);
            xtabpage.Text = entry.Text + "    ";
            xtabpage.Tag = entry;
            var xprodlist = new WerkPlekkenUI();
            xprodlist.AutoScroll = true;
            xprodlist.BackColor = System.Drawing.Color.White;
            xprodlist.Dock = System.Windows.Forms.DockStyle.Fill;
            xprodlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xprodlist.OnPlekkenChanged += (sender, args) =>
            {
                if (xprodlist.Parent is MetroTabPage page)
                {
                    var xend = page.Text.IndexOf('[');
                    if (xend > -1)
                        page.Text = page.Text.Substring(0, xend) + $"[{xprodlist.xwerkpleklist.Items.Count}]";
                    else
                        page.Text = $"{page.Text}[{xprodlist.xwerkpleklist.Items.Count}]";
                    page.Invalidate();
                }
            };
            xprodlist.OnRequestOpenWerk += WerkPlekkenUI1_OnRequestOpenWerk;
            xprodlist.Name = entry.Name;
            xtabpage.Controls.Add(xprodlist);
            metroCustomTabControl1.SuspendLayout();
            metroCustomTabControl1.TabPages.Add(xtabpage);
            metroCustomTabControl1.ResumeLayout(false);
            if (select)
                metroCustomTabControl1.SelectedTab = xtabpage;
            xprodlist.InitUI(_manager);
            xprodlist.LoadPlekken(true);
            xprodlist.InitEvents();
            UpdateTileViewed(entry, true);
        }

        private void InitGereedmeldingenTab(TileInfoEntry entry, bool select)
        {
            if (CheckTab(entry, select))
                return;
            var xtabpage = new MetroTabPage();
            xtabpage.Padding = new Padding(5);
            xtabpage.Text = entry.Text + "    ";
            xtabpage.Tag = entry;
            var xprodlist = new RecentGereedMeldingenUI();
            xprodlist.AutoScroll = true;
            xprodlist.BackColor = System.Drawing.Color.White;
            xprodlist.Dock = System.Windows.Forms.DockStyle.Fill;
            xprodlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xprodlist.ItemCountChanged += (sender, args) =>
            {
                if (xprodlist.Parent is MetroTabPage page)
                {
                    var xend = page.Text.IndexOf('[');
                    if (xend > -1)
                        page.Text = page.Text.Substring(0, xend) + $"[{xprodlist.ProductieLijst.Items.Count}]";
                    else
                        page.Text = $"{page.Text}[{xprodlist.ProductieLijst.Items.Count}]";
                    page.Invalidate();
                }
            };
            xprodlist.Name = entry.Name;
            xtabpage.Controls.Add(xprodlist);
            metroCustomTabControl1.SuspendLayout();
            metroCustomTabControl1.TabPages.Add(xtabpage);
            metroCustomTabControl1.ResumeLayout(false);
            if (select)
                metroCustomTabControl1.SelectedTab = xtabpage;
            xprodlist.LoadBewerkingen();
            UpdateTileViewed(entry, true);
        }

        private bool CheckTab(TileInfoEntry entry, bool select)
        {
            var xold = metroCustomTabControl1.TabPages.Cast<MetroTabPage>()
                .FirstOrDefault(x => x.Tag is TileInfoEntry xent && entry.Equals(xent));
            if (xold != null)
            {
                if (select)
                    metroCustomTabControl1.SelectedTab = xold;
                return true;
            }

            return false;
        }

        private void InitProductiesTab(TileInfoEntry entry, bool select)
        {
            try
            {
                if (CheckTab(entry, select))
                    return;
                var xtabpage = new MetroTabPage();
                xtabpage.Padding = new Padding(5);
                xtabpage.Text = entry.Text + "    ";
                xtabpage.Tag = entry;
                var xprodlist = new ProductieListControl();
                xprodlist.AutoScroll = true;
                xprodlist.BackColor = System.Drawing.Color.White;
                xprodlist.CanLoad = true;
                xprodlist.Dock = System.Windows.Forms.DockStyle.Fill;
                xprodlist.EnableEntryFiltering = true;
                xprodlist.EnableFiltering = true;
                xprodlist.EnableSync = false;
                xprodlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                xprodlist.IsBewerkingView = true;
                xprodlist.ListName = entry.Name;
                xprodlist.ItemCountChanged += (sender, args) =>
                {
                    if (xprodlist.Parent is MetroTabPage page)
                    {
                        var xend = page.Text.IndexOf('[');
                        if (xend > -1)
                            page.Text = page.Text.Substring(0, xend) + $"[{xprodlist.ProductieLijst.Items.Count}]";
                        else
                            page.Text = $"{page.Text}[{xprodlist.ProductieLijst.Items.Count}]";
                        page.Invalidate();
                    }
                };
                xprodlist.Name = entry.Name;
                xprodlist.RemoveCustomItemIfNotValid = false;
                xtabpage.Controls.Add(xprodlist);
                metroCustomTabControl1.SuspendLayout();
                metroCustomTabControl1.TabPages.Add(xtabpage);
                metroCustomTabControl1.ResumeLayout(false);
                if (select)
                    metroCustomTabControl1.SelectedTab = xtabpage;
                xprodlist.InitProductie(true, true, true, true, true, true);
                xprodlist.InitEvents();
                UpdateTileViewed(entry, true);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void InitFilterProductieTab(TileInfoEntry entry, bool select)
        {
            try
            {
                var xold = metroCustomTabControl1.TabPages.Cast<MetroTabPage>()
    .FirstOrDefault(x => x.Tag is TileInfoEntry xent && entry.Equals(xent));
                if (xold != null)
                {
                    if (select)
                        metroCustomTabControl1.SelectedTab = xold;
                    return;
                }

                var xname = entry.Name.Trim().Replace("_", " ");
                var xfilter = Manager.Opties.Filters.FirstOrDefault(x => x.ID == entry.LinkID);
                if (xfilter == null)
                    throw new Exception($"'{xname}' bestaat niet meer!");
                var xtabpage = new MetroTabPage();
                xtabpage.Padding = new Padding(5);
                xtabpage.Text = entry.Text + "    ";
                xtabpage.Tag = entry;
                var xprodlist = new ProductieListControl();
                xprodlist.AutoScroll = true;
                xprodlist.BackColor = System.Drawing.Color.White;
                xprodlist.CanLoad = true;
                xprodlist.Dock = System.Windows.Forms.DockStyle.Fill;
                xprodlist.EnableEntryFiltering = true;
                xprodlist.EnableFiltering = true;
                xprodlist.EnableSync = false;
                xprodlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                xprodlist.IsBewerkingView = true;
                xprodlist.ListName = entry.Name;
                if (xfilter.ListNames.IndexOf(entry.Name) == -1)
                    xfilter.ListNames.Add(entry.Name);
                xprodlist.ItemCountChanged += (sender, args) =>
                {
                    if (xprodlist.Parent is MetroTabPage page)
                    {
                        var xend = page.Text.IndexOf('[');
                        if (xend > -1)
                            page.Text = page.Text.Substring(0, xend) + $"[{xprodlist.ProductieLijst.Items.Count}]";
                        else
                            page.Text = $"{page.Text}[{xprodlist.ProductieLijst.Items.Count}]";
                        page.Invalidate();
                    }
                };
                xprodlist.Name = entry.Name;
                xprodlist.RemoveCustomItemIfNotValid = false;
                xtabpage.Controls.Add(xprodlist);
                metroCustomTabControl1.SuspendLayout();
                metroCustomTabControl1.TabPages.Add(xtabpage);
                metroCustomTabControl1.ResumeLayout(false);
                if (select)
                    metroCustomTabControl1.SelectedTab = xtabpage;
                xprodlist.InitProductie(true, true, true, true, true, true);
                xprodlist.InitEvents();
                UpdateTileViewed(entry, true);
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void InitZoekProductiesUITab(TileInfoEntry entry, bool select)
        {
            if (CheckTab(entry, select))
                return;
            var xtabpage = new MetroTabPage();
            xtabpage.Padding = new Padding(5);
            xtabpage.Text = entry.Text + "    ";
            xtabpage.Tag = entry;
            var xprodlist = new ZoekProductiesUI();

            xprodlist.AutoScroll = true;
            xprodlist.BackColor = System.Drawing.Color.White;
            xprodlist.Dock = System.Windows.Forms.DockStyle.Fill;
            xprodlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xprodlist.Name = entry.Name;
            xprodlist.ClosedClicked += (x, y) =>
            {
                if (xprodlist.Parent is MetroTabPage page)
                    metroCustomTabControl1.CloseTab(page);
            };
           
            xtabpage.Controls.Add(xprodlist);
            metroCustomTabControl1.SuspendLayout();
            metroCustomTabControl1.TabPages.Add(xtabpage);
            metroCustomTabControl1.ResumeLayout(false);
            if (select)
                metroCustomTabControl1.SelectedTab = xtabpage;
            xprodlist.InitUI();
            UpdateTileViewed(entry, true);
        }

        private void InitAlleStoringenUITab(TileInfoEntry entry, bool select)
        {
            if (CheckTab(entry, select))
                return;
            var xtabpage = new MetroTabPage();
            xtabpage.Padding = new Padding(5);
            xtabpage.Text = entry.Text + "    ";
            xtabpage.Tag = entry;
            var xprodlist = new AlleStoringenUI();

            xprodlist.AutoScroll = true;
            xprodlist.BackColor = System.Drawing.Color.White;
            xprodlist.Dock = System.Windows.Forms.DockStyle.Fill;
            xprodlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xprodlist.Name = entry.Name;
            xprodlist.CloseClicked += (x, y) =>
            {
                if (xprodlist.Parent is MetroTabPage page)
                    metroCustomTabControl1.CloseTab(page);
            };
          
            xprodlist.InitStoringen();
            xtabpage.Controls.Add(xprodlist);
            metroCustomTabControl1.SuspendLayout();
            metroCustomTabControl1.TabPages.Add(xtabpage);
            metroCustomTabControl1.ResumeLayout(false);
            if (select)
                metroCustomTabControl1.SelectedTab = xtabpage;
            xprodlist.InitUI();
            UpdateTileViewed(entry, true);
        }

        private void InitArtikelenVerbruikUITab(TileInfoEntry entry, bool select)
        {
            if (CheckTab(entry, select))
                return;
            var xtabpage = new MetroTabPage();
            xtabpage.Padding = new Padding(5);
            xtabpage.Text = entry.Text + "    ";
            xtabpage.Tag = entry;
            var xprodlist = new ArtikelenVerbruikUI();

            xprodlist.AutoScroll = true;
            xprodlist.BackColor = System.Drawing.Color.White;
            xprodlist.Dock = System.Windows.Forms.DockStyle.Fill;
            xprodlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xprodlist.Name = entry.Name;
            xprodlist.CloseClicked += (x, y) =>
            {
                if (xprodlist.Parent is MetroTabPage page)
                    metroCustomTabControl1.CloseTab(page);
            };
         
            xtabpage.Controls.Add(xprodlist);
            metroCustomTabControl1.SuspendLayout();
            metroCustomTabControl1.TabPages.Add(xtabpage);
            metroCustomTabControl1.ResumeLayout(false);
            if (select)
                metroCustomTabControl1.SelectedTab = xtabpage;
            xprodlist.InitUI();
            UpdateTileViewed(entry, true);
        }


        private void InitPersoneelIndelingTab(TileInfoEntry entry, bool select)
        {
            if (CheckTab(entry, select))
                return;
            var xtabpage = new MetroTabPage();
            xtabpage.Padding = new Padding(5);
            xtabpage.Text = entry.Text + "    ";
            xtabpage.Tag = entry;
            var xprodlist = new PersoneelIndelingUI();

            xprodlist.AutoScroll = true;
            xprodlist.BackColor = System.Drawing.Color.White;
            xprodlist.Dock = System.Windows.Forms.DockStyle.Fill;
            xprodlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xprodlist.Name = entry.Name;
           
            xtabpage.Controls.Add(xprodlist);
            metroCustomTabControl1.SuspendLayout();
            metroCustomTabControl1.TabPages.Add(xtabpage);
            metroCustomTabControl1.ResumeLayout(false);
            if (select)
                metroCustomTabControl1.SelectedTab = xtabpage;
            xprodlist.InitUI();
            UpdateTileViewed(entry, true);
        }

        private void InitWerkplaatsIndelingTab(TileInfoEntry entry, bool select)
        {
            if (CheckTab(entry, select))
                return;
            var xtabpage = new MetroTabPage();
            xtabpage.Padding = new Padding(5);
            xtabpage.Text = entry.Text + "    ";
            xtabpage.Tag = entry;
            var xprodlist = new WerkplaatsIndelingUI();

            xprodlist.AutoScroll = true;
            xprodlist.BackColor = System.Drawing.Color.White;
            xprodlist.Dock = System.Windows.Forms.DockStyle.Fill;
            xprodlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xprodlist.Name = entry.Name;
          
            xtabpage.Controls.Add(xprodlist);
            metroCustomTabControl1.SuspendLayout();
            metroCustomTabControl1.TabPages.Add(xtabpage);
            metroCustomTabControl1.ResumeLayout(false);
            if (select)
                metroCustomTabControl1.SelectedTab = xtabpage;
            xprodlist.InitUI();
            UpdateTileViewed(entry, true);
        }

        private void InitPersoneelTab(TileInfoEntry entry, bool select)
        {
            if (CheckTab(entry, select))
                return;
            var xtabpage = new MetroTabPage();
            xtabpage.Padding = new Padding(5);
            xtabpage.Text = entry.Text + "    ";
            xtabpage.Tag = entry;
            var xprodlist = new PersoneelsUI();

            xprodlist.AutoScroll = true;
            xprodlist.BackColor = System.Drawing.Color.White;
            xprodlist.Dock = System.Windows.Forms.DockStyle.Fill;
            xprodlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xprodlist.Name = entry.Name;
          
            xtabpage.Controls.Add(xprodlist);
            metroCustomTabControl1.SuspendLayout();
            metroCustomTabControl1.TabPages.Add(xtabpage);
            metroCustomTabControl1.ResumeLayout(false);
            if (select)
                metroCustomTabControl1.SelectedTab = xtabpage;
            xprodlist.InitUI();
            UpdateTileViewed(entry, true);
        }

        private void InitArtikelenTab(TileInfoEntry entry, bool select)
        {
            if (CheckTab(entry, select))
                return;
            var xtabpage = new MetroTabPage();
            xtabpage.Padding = new Padding(5);
            xtabpage.Text = entry.Text + "    ";
            xtabpage.Tag = entry;
            var xprodlist = new ArtikelsUI();

            xprodlist.AutoScroll = true;
            xprodlist.BackColor = System.Drawing.Color.White;
            xprodlist.Dock = System.Windows.Forms.DockStyle.Fill;
            xprodlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xprodlist.Name = entry.Name;
            xtabpage.Controls.Add(xprodlist);
            metroCustomTabControl1.SuspendLayout();
            metroCustomTabControl1.TabPages.Add(xtabpage);
            metroCustomTabControl1.ResumeLayout(false);
            if (select)
                metroCustomTabControl1.SelectedTab = xtabpage;
            xprodlist.InitUI();
            UpdateTileViewed(entry, true);
        }

        private void InitArtikelenRecordsTab(TileInfoEntry entry, bool select)
        {
            if (CheckTab(entry, select))
                return;
            var xtabpage = new MetroTabPage();
            xtabpage.Padding = new Padding(5);
            xtabpage.Text = entry.Text + "    ";
            xtabpage.Tag = entry;
            var xprodlist = new ArtikelRecordsUI();

            xprodlist.AutoScroll = true;
            xprodlist.BackColor = System.Drawing.Color.White;
            xprodlist.Dock = System.Windows.Forms.DockStyle.Fill;
            xprodlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xprodlist.Name = entry.Name;
            xprodlist.CloseClicked += (x, y) =>
            {
                if (xprodlist.Parent is MetroTabPage page)
                    metroCustomTabControl1.CloseTab(page);
            };
           
            xtabpage.Controls.Add(xprodlist);
            metroCustomTabControl1.SuspendLayout();
            metroCustomTabControl1.TabPages.Add(xtabpage);
            metroCustomTabControl1.ResumeLayout(false);
            if (select)
                metroCustomTabControl1.SelectedTab = xtabpage;
            xprodlist.InitUI();
            UpdateTileViewed(entry, true);
        }

        private void InitBerekenVerbruikUITab(TileInfoEntry entry, bool select)
        {
            if (CheckTab(entry, select))
                return;
            var xtabpage = new MetroTabPage();
            xtabpage.Padding = new Padding(5);
            xtabpage.Text = entry.Text + "    ";
            xtabpage.Tag = entry;
            var xprodlist = new ProductieVerbruikUI();
            xprodlist.Dock = DockStyle.Fill;
            xprodlist.ShowMateriaalSelector = false;
            xprodlist.ShowOpslaan = true;
            xprodlist.ShowPerUur = true;
            xprodlist.ShowSluiten = true;
            xprodlist.ShowOpdrukkerArtikelNr = true;
            xprodlist.UpdateFields(true);
            xprodlist.MaxUitgangsLengte = 12450;
            xprodlist.RestStuk = 50;
            xprodlist.AutoScroll = true;
            xprodlist.BackColor = System.Drawing.Color.White;
            xprodlist.Dock = System.Windows.Forms.DockStyle.Fill;
            xprodlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xprodlist.Name = entry.Name;
            xprodlist.CloseClicked += (x, y) =>
            {
                if (xprodlist.Parent is MetroTabPage page)
                    metroCustomTabControl1.CloseTab(page);
            };
            xtabpage.Controls.Add(xprodlist);
            metroCustomTabControl1.SuspendLayout();
            metroCustomTabControl1.TabPages.Add(xtabpage);
            metroCustomTabControl1.ResumeLayout(false);
            if (select)
                metroCustomTabControl1.SelectedTab = xtabpage;
            UpdateTileViewed(entry, true);
        }

        private void InitAlleNotitiesTab(TileInfoEntry entry, bool select)
        {
            if (CheckTab(entry, select))
                return;
            var xtabpage = new MetroTabPage();
            xtabpage.Padding = new Padding(5);
            xtabpage.Text = entry.Text + "    ";
            xtabpage.Tag = entry;
            var xprodlist = new AlleNotitiesUI();

            xprodlist.AutoScroll = true;
            xprodlist.BackColor = System.Drawing.Color.White;
            xprodlist.Dock = System.Windows.Forms.DockStyle.Fill;
            xprodlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xprodlist.Name = entry.Name;
            xtabpage.Controls.Add(xprodlist);
            metroCustomTabControl1.SuspendLayout();
            metroCustomTabControl1.TabPages.Add(xtabpage);
            metroCustomTabControl1.ResumeLayout(false);
            if (select)
                metroCustomTabControl1.SelectedTab = xtabpage;
            xprodlist.InitUI();
            UpdateTileViewed(entry, true);
        }

        private void InitProductieVolgordeTab(TileInfoEntry entry, bool select)
        {
            if (CheckTab(entry, select))
                return;
            var xtabpage = new MetroTabPage();
            xtabpage.Padding = new Padding(5);
            xtabpage.Text = entry.Text + "    ";
            xtabpage.Tag = entry;
            var xprodlist = new ProductieOverzichtUI();

            xprodlist.AutoScroll = true;
            xprodlist.BackColor = System.Drawing.Color.White;
            xprodlist.Dock = System.Windows.Forms.DockStyle.Fill;
            xprodlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xprodlist.Name = entry.Name;
           
            xtabpage.Controls.Add(xprodlist);
            metroCustomTabControl1.SuspendLayout();
            metroCustomTabControl1.TabPages.Add(xtabpage);
            metroCustomTabControl1.ResumeLayout(false);
            if (select)
                metroCustomTabControl1.SelectedTab = xtabpage;
            xprodlist.InitUI();
            UpdateTileViewed(entry, true);
        }
        private void InitChartsTab(TileInfoEntry entry, bool select)
        {
            if (CheckTab(entry, select))
                return;
            var xtabpage = new MetroTabPage();
            xtabpage.Padding = new Padding(5);
            xtabpage.Text = entry.Text + "    ";
            xtabpage.Tag = entry;
            var xprodlist = new ChartView();

            xprodlist.AutoScroll = true;
            xprodlist.BackColor = System.Drawing.Color.White;
            xprodlist.Dock = System.Windows.Forms.DockStyle.Fill;
            xprodlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xprodlist.Name = entry.Name;
            
            xtabpage.Controls.Add(xprodlist);
            metroCustomTabControl1.SuspendLayout();
            metroCustomTabControl1.TabPages.Add(xtabpage);
            metroCustomTabControl1.ResumeLayout(false);
            if (select)
                metroCustomTabControl1.SelectedTab = xtabpage;
            xprodlist.LoadData();
            UpdateTileViewed(entry, true);
        }

        private void InitChatTab(TileInfoEntry entry, bool select, string username = null)
        {
            if (CheckTab(entry, select))
                return;
            var xtabpage = new MetroTabPage();
            xtabpage.Padding = new Padding(5);
            xtabpage.Text = entry.Text + "    ";
            xtabpage.Tag = entry;
            var xprodlist = new ProductieChatUI();
            xprodlist._Selected = username;
            xprodlist.AutoScroll = true;
            xprodlist.BackColor = System.Drawing.Color.White;
            xprodlist.Dock = System.Windows.Forms.DockStyle.Fill;
            xprodlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xprodlist.Name = entry.Name;

            xtabpage.Controls.Add(xprodlist);
            metroCustomTabControl1.SuspendLayout();
            metroCustomTabControl1.TabPages.Add(xtabpage);
            metroCustomTabControl1.ResumeLayout(false);
            if (select)
                metroCustomTabControl1.SelectedTab = xtabpage;
            if (!xprodlist.InitUI())
            {
                metroCustomTabControl1.TabPages.Remove(xtabpage);
                UpdateTileViewed(entry, false);
            }
            else UpdateTileViewed(entry, true);
        }

        private void CloseTabPage(object sender, bool updateview)
        {
            if (sender is MetroTabPage page && page.Controls.Count > 0)
            {
                var xcontrol = page.Controls.Cast<Control>().FirstOrDefault(x=> x is not MetroScrollBar);
                if (xcontrol is ProductieListControl xprod)
                {
                    xprod.DetachEvents();
                    xprod.SaveColumns(true);
                }
                else if (xcontrol is RecentGereedMeldingenUI gereed)
                {
                    gereed.DetachEvents();
                    gereed.productieListControl1.SaveColumns(true);
                }
                else if (xcontrol is WerkPlekkenUI werkplekken)
                {
                    werkplekken.DetachEvents();
                    werkplekken.SaveLayout();
                }
                else if (xcontrol is ZoekProductiesUI zoeken)
                {
                    zoeken.CloseUI();
                }
                else if (xcontrol is AlleStoringenUI storingen)
                {
                    storingen.CloseUI();
                }
                else if (xcontrol is ArtikelenVerbruikUI artikelverbuik)
                {
                    artikelverbuik.CloseUI();
                }
                else if (xcontrol is PersoneelIndelingUI pers)
                {
                    pers.CloseUI();
                }
                else if (xcontrol is WerkplaatsIndelingUI werkp)
                {
                    werkp.CloseUI();
                }
                else if (xcontrol is PersoneelsUI xpers)
                {
                    xpers.CloseUI();
                }
                else if (xcontrol is ArtikelsUI artikels)
                {
                    artikels.CloseUI();
                }
                else if (xcontrol is ArtikelRecordsUI records)
                {
                    records.CloseUI();
                }
                else if (xcontrol is AlleNotitiesUI notes)
                {
                    notes.CloseUI();
                }
                else if (xcontrol is ProductieOverzichtUI overzicht)
                {
                    overzicht.CloseUI();
                }
                else if (xcontrol is ProductieChatUI xchat)
                {
                    xchat.CloseUI();
                }
                else if (xcontrol is ChartView xchart)
                {
                    xchart.CloseUI();
                }

                if (updateview && page.Tag is TileInfoEntry info)
                    UpdateTileViewed(info, false);
                xcontrol?.Dispose();
            }
        }

        public void UpdateTileViewed(TileInfoEntry entry, bool isviewed)
        {
            try
            {
                if (entry == null) return;
                entry.IsViewed = isviewed;
                if (Manager.Opties?.TileLayout != null)
                {
                    var xindex = Manager.Opties.TileLayout.IndexOf(entry);
                    if (xindex > -1)
                        Manager.Opties.TileLayout[xindex] = entry;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void ShowFilterForm()
        {
            try
            {
                var xf = new FilterEditor();
                if (xf.ShowDialog() == DialogResult.OK)
                    Manager.OnFilterChanged(this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void metroCustomTabControl1_TabClosed(object sender, EventArgs e)
        {
            CloseTabPage(sender, true);
        }

        public void ShowTileTabPage(TileInfoEntry entry, bool select)
        {
            try
            {
                if (entry?.Name == null) return;
                switch (entry.Name.ToLower())
                {
                    case "producties":
                        InitProductiesTab(entry, select);
                        break;
                    case "werkplaatsen":
                        InitWerkplekkenUITab(entry, select);
                        break;
                    case "gereedmeldingen":
                        InitGereedmeldingenTab(entry, select);
                        break;
                    case "onderbrekingen":
                        InitAlleStoringenUITab(entry, select);
                        break;
                    case "xverbruik":
                        InitBerekenVerbruikUITab(entry, select);
                        break;
                    case "xverbruikbeheren":
                        InitArtikelenVerbruikUITab(entry, select);
                        break;
                    case "xchangeaantal":
                        DoAantalGemaakt(this);
                        break;
                    case "xsearchtekening":
                        ZoekWerkTekening();
                        break;
                    case "xpersoneelindeling":
                        InitPersoneelIndelingTab(entry, select);
                        break;
                    case "xwerkplaatsindeling":
                        InitWerkplaatsIndelingTab(entry,select);
                        break;
                    case "xcreateexcel":
                        DoCreateExcel();
                        break;
                    case "xstats":
                        InitChartsTab(entry, select);
                        break;
                    case "xzoekproducties":
                        InitZoekProductiesUITab(entry, select);
                        break;
                    case "xpersoneel":
                        InitPersoneelTab(entry,select);
                        break;
                    case "xalleartikelen":
                        InitArtikelenTab(entry, select);
                        break;
                    case "xartikelrecords":
                        InitArtikelenRecordsTab(entry,select);
                        break;
                    case "xproductievolgorde":
                        InitProductieVolgordeTab(entry, select);
                        break;
                    case "xklachten":
                        ShowKlachtenWindow();
                        break;
                    case "xweekoverzicht":
                        ShowCreateWeekOverzicht();
                        break;
                    case "xallenotities":
                        InitAlleNotitiesTab(entry,select);
                        break;
                    case "xbeheerfilters":
                        ShowFilterForm();
                        break;
                    case "xchat":
                        InitChatTab(entry, select);
                        break;
                    default:
                        switch (entry.GroupName.ToLower())
                        {
                            case "filter":
                                InitFilterProductieTab(entry, select);
                                break;
                        }

                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void tileMainView1_TileClicked(object sender, EventArgs e)
        {
            if (sender is Tile {Tag: TileInfoEntry entry})
            {
                ShowTileTabPage(entry, true);
            }
        }

        private void tileMainView1_TilesLoaded(object sender, EventArgs e)
        {
            try
            {
                if (sender is TileViewer viewer)
                {

                    var xtiles = viewer.GetAllTiles().Where(x => x.Tag is TileInfoEntry {IsViewed: true}).ToList();
                    foreach (var xt in xtiles)
                    {
                        if (xt.Tag is TileInfoEntry entry)
                        {
                            ShowTileTabPage(entry, false);
                            Application.DoEvents();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        #endregion Tab Buttons

        public void LoadManager(string path, bool disposeOld, bool autologin = true)
        {
            InitManager(path, autologin, disposeOld);
        }

        public void ShowStartupForms()
        {
            try
            {
                if (this.Disposing || this.IsDisposed) return;
                this.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        //CheckForSyncDatabase();
                        //CheckForUpdateDatabase();
                        CheckForPreview(false, true);
                        CheckForSpecialRooster(true);
                        LoadStartedProducties();
                        //LoadProductieLogs();
                        //RunProductieRefresh();
                        UpdateKlachtButton();
                        UpdateVerpakkingenButton();
                        Manager.ArtikelRecords?.CheckForOpmerkingen(true);
                        InitDBCorupptedMonitor();
                        DailyMessage.CreateDaily();
                        UpdateUnreadMessages(null);
                        UpdateUnreadOpmerkingen();
                        if(Manager.Opties is {ToonPersoneelIndelingNaOpstart: true})
                            ShowPersoneelIndelingWindow();
                        if (Manager.Opties is {ToonWerkplaatsIndelingNaOpstart: true})
                            ShowWerkplaatsIndelingWindow();
                        //UpdateAllLists();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }));


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void InitEvents()
        {
            Manager.OnSettingsChanged += _manager_OnSettingsChanged;
            //Manager.OnProductiesLoaded += Manager_OnProductiesChanged;
            Manager.OnLoginChanged += _manager_OnLoginChanged;
            Manager.OnFormulierActie += Manager_OnFormulierActie;
            //Manager.DbUpdater.DbEntryUpdated += DbUpdater_DbEntryUpdated;
            //Manager.OnDbBeginUpdate += Manager_OnDbBeginUpdate;
            //Manager.OnDbEndUpdate += Manager_OnDbEndUpdate;
            Manager.OnManagerLoaded += _manager_OnManagerLoaded;
            ProductieChat.MessageRecieved += ProductieChat_MessageRecieved;
            ProductieChat.GebruikerUpdate += ProductieChat_GebruikerUpdate;
            MultipleFileDb.CorruptedFilesChanged += MultipleFileDb_CorruptedFilesChanged;
            Manager.FilterChanged += Manager_FilterChanged;

            Manager.KlachtChanged += Klachten_KlachtChanged;
            Manager.KlachtDeleted += Klachten_KlachtChanged;

            Manager.VerpakkingChanged += Manager_VerpakkingChanged; 
            Manager.VerpakkingDeleted += Manager_VerpakkingDeleted;

            Manager.RequestRespondDialog += Manager_RequestRespondDialog;

            _manager.OnShutdown += _manager_OnShutdown;
        }

        private void Manager_FilterChanged(object sender, EventArgs e)
        {
            if (tileMainView1.InvokeRequired)
                tileMainView1.Invoke(new MethodInvoker(() => tileMainView1.UpdateFilterTiles()));
            else
                tileMainView1.UpdateFilterTiles();
        }

        private DialogResult Manager_RequestRespondDialog(object sender, string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon, string[] chooseitems = null, Dictionary<string, DialogResult> custombuttons = null, Image customImage = null, MetroColorStyle style = MetroColorStyle.Default)
        {
            var xret = DialogResult.Cancel;
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(() =>
                    xret = DoOpmerking(sender, message, title, buttons, icon, chooseitems, custombuttons, customImage, style)));
            else xret = DoOpmerking(sender, message, title, buttons, icon, chooseitems, custombuttons, customImage, style);
            return xret;
        }

        private DialogResult DoOpmerking(object sender, string message, string title, MessageBoxButtons buttons,
            MessageBoxIcon icon, string[] chooseitems = null, Dictionary<string, DialogResult> custombuttons = null,
            Image customImage = null, MetroColorStyle style = MetroColorStyle.Default)
        {
            var xmsg = new XMessageBox();
            xmsg.StartPosition = FormStartPosition.CenterParent;
            return xmsg.ShowDialog(this,message, title, buttons, icon, chooseitems, custombuttons, customImage, style);
        }

        public void DetachEvents()
        {
            Manager.OnSettingsChanged -= _manager_OnSettingsChanged;
            Manager.OnFormulierActie -= Manager_OnFormulierActie;
            // Manager.OnProductiesLoaded -= Manager_OnProductiesChanged;
            //Manager.DbUpdater.DbEntryUpdated -= DbUpdater_DbEntryUpdated;
            Manager.OnLoginChanged -= _manager_OnLoginChanged;
            if (Manager.Opmerkingen != null)
                Manager.Opmerkingen.OnOpmerkingenChanged -= Opmerkingen_OnOpmerkingenChanged;
            Manager.RequestRespondDialog -= Manager_RequestRespondDialog;
            ProductieChat.MessageRecieved -= ProductieChat_MessageRecieved;
            ProductieChat.GebruikerUpdate -= ProductieChat_GebruikerUpdate;
            MultipleFileDb.CorruptedFilesChanged -= MultipleFileDb_CorruptedFilesChanged;
            Manager.FilterChanged -= Manager_FilterChanged;
            //Manager.OnDbBeginUpdate -= Manager_OnDbBeginUpdate;
            //Manager.OnDbEndUpdate -= Manager_OnDbEndUpdate;
            Manager.OnManagerLoaded -= _manager_OnManagerLoaded;

            Manager.KlachtChanged -= Klachten_KlachtChanged;
            Manager.KlachtDeleted -= Klachten_KlachtChanged;

            Manager.VerpakkingChanged -= Manager_VerpakkingChanged;
            Manager.VerpakkingDeleted -= Manager_VerpakkingDeleted;
            if (_manager != null)
                _manager.OnShutdown -= _manager_OnShutdown;
        }

        public void CloseUI()
        {
            try
            {
                DetachEvents();
                for (int i = 0; i < metroCustomTabControl1.TabCount; i++)
                {
                    var xtab = metroCustomTabControl1.TabPages[i];
                    CloseTabPage(xtab,false);
                    metroCustomTabControl1.TabPages.Remove(xtab);
                    i--;
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SaveLayouts()
        {
            try
            {
                tileMainView1.SaveLayout(false);
                Manager.Opties.LastShownTabName = (metroCustomTabControl1.SelectedTab?.Tag as TileInfoEntry)?.Name;
                foreach (var xtab in metroCustomTabControl1.TabPages)
                {
                    if (xtab is MetroTabPage page && page.Controls.Count > 0)
                    {
                        foreach (var xcontrol in page.Controls)
                        {
                            if (xcontrol is ProductieListControl xprod)
                                xprod.SaveColumns(true);
                            else if (xcontrol is RecentGereedMeldingenUI gereed)
                                gereed.productieListControl1.SaveColumns(true);
                            else if (xcontrol is WerkPlekkenUI werkplekken)
                                werkplekken.SaveLayout();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Manager_VerpakkingDeleted(object sender, EventArgs e)
        {
           UpdateVerpakkingenButton();
        }

        private void Manager_VerpakkingChanged(object sender, EventArgs e)
        {
            UpdateVerpakkingenButton();
        }

        private void Klachten_KlachtChanged(object sender, EventArgs e)
        {
            UpdateKlachtButton();
        }

        private void UpdateKlachtButton()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(xUpdateKlachtButton));
            else xUpdateKlachtButton();
        }

        private void UpdateVerpakkingenButton()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(xUpdateVerpakkingenButton));
            else xUpdateVerpakkingenButton();
        }

        private void xUpdateKlachtButton()
        {
            try
            {
                var xkl = Manager.Klachten?.GetUnreadKlachten();
                if (xkl is {Count: > 0})
                {
                    string cnt = xkl.Count > 99 ? "99+" : xkl.Count.ToString();
                    int fs = cnt.Length < 2 ? 16 : cnt.Length < 3 ? 14 : cnt.Length < 4 ? 12 : 10;
                    var ximg = GraphicsExtensions.DrawUserCircle(new Size(32, 32), Brushes.White,
                        cnt,
                        new Font("Ariel", fs, FontStyle.Bold), Color.DarkRed);
                    xklachten.Image = Resources.Leave_80_icon_icons_com_57305.CombineImage(ximg, 1.75);
                    var xopen = Application.OpenForms["KlachtenForm"];
                    if (xopen != null) return;
                    var x0 = xkl.Count == 1 ? "is" : "zijn";
                    var x1 = xkl.Count == 1 ? "klacht" : "klachten";
                    if (XMessageBox.Show(this,$"Er {x0} {xkl.Count} nieuwe {x1}!\n\n" +
                                         $"Nu bekijken?", $"Nieuwe {x1}", MessageBoxButtons.YesNo, Resources.Leave_80_icon_icons_com_57305_128x128, MetroColorStyle.Red) == DialogResult.Yes)
                    {
                        xklachten_Click(this, EventArgs.Empty);
                    }
                }
                else
                {
                    xkl = Manager.Klachten?.GetAlleKlachten();
                    if (xkl is {Count: > 0})
                    {
                        var cnt = xkl.Count > 99 ? "99+" : xkl.Count.ToString();
                        int fs = cnt.Length < 2? 16: cnt.Length < 3? 14 : cnt.Length < 4? 12 : 10;
                        var ximg = GraphicsExtensions.DrawUserText(new Size(32, 32), Brushes.LightCoral,
                            cnt,
                            new Font("Ariel", fs, FontStyle.Bold));
                        xklachten.Image = Resources.Leave_80_icon_icons_com_57305.CombineImage(ximg, 1.75);
                    }
                    else
                        xklachten.Image = Resources.Leave_80_icon_icons_com_57305;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void xUpdateVerpakkingenButton()
        {
            try
            {
                var xkl = Manager.Verpakkingen?.Database?.Count()??0;
                if (xkl > 0)
                {
                    string cnt = xkl > 99 ? "99+" : xkl.ToString();
                    int fs = cnt.Length < 2 ? 16 : cnt.Length < 3 ? 14 : cnt.Length < 4 ? 12 : 10;
                    var ximg = GraphicsExtensions.DrawUserCircle(new Size(32, 32), Brushes.White,
                        cnt,
                        new Font("Ariel", fs, FontStyle.Bold), Color.DarkRed);
                    xverpakkingen.Image = Resources.Box_1_35524.CombineImage(ximg, 1.75);
                }
                else
                {
                    xverpakkingen.Image = Resources.Box_1_35524;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void MultipleFileDb_CorruptedFilesChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(UpdateCorruptedButtonImage));
                else UpdateCorruptedButtonImage();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void UpdateCorruptedButtonImage()
        {
            try
            {
                var xcorrupted = MultipleFileDb.CorruptedFilePaths;
                if (xcorrupted.Count > 0)
                {
                    var ximg = GraphicsExtensions.DrawUserCircle(new Size(32, 32), Brushes.White,
                        xcorrupted.Count.ToString(),
                        new Font("Ariel", 16, FontStyle.Bold), Color.DarkRed);
                    xcorruptedfilesbutton.Image = Resources.error_notification_32x32.CombineImage(ximg, 1.75);
                }
                else
                {
                    xcorruptedfilesbutton.Image = Resources.error_notification_32x32;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Opmerkingen_OnOpmerkingenChanged(object sender, EventArgs e)
        {
          UpdateUnreadOpmerkingen();
        }

        private void ProductieChat_GebruikerUpdate(UserChat user)
        {
            UpdateUnreadMessages(user);
        }

        private void ProductieChat_MessageRecieved(ProductieChatEntry message)
        {
            UpdateUnreadMessages(message?.Afzender);
        }

      

        private DialogResult _manager_OnShutdown(Manager instance, ref TimeSpan verlengtijd)
        {
            var af = new AfsluitPromp();
            var res = af.ShowDialog();
            if (res == DialogResult.OK) verlengtijd = af.VerlengTijd;
            return res;
        }
        
        private void _manager_OnSettingsChanged(object instance, UserSettings settings, bool init)
        {
            try
            {
                if (IsDisposed || Disposing) return;
                mainMenu1.OnSettingChanged(instance, settings, init);
                if (!init) return;
                BeginInvoke(new MethodInvoker(() =>
                {
                    if (IsDisposed || Disposing) return;
                    try
                    {
                        var name = Manager.Opties == null ? "Default" : Manager.Opties.Username;
                        Text = @$"ProductieManager [{name}]";
                        tileMainView1.SetBackgroundImage(Manager.Opties?.BackgroundImagePath);
                        tileMainView1.LoadTileViewer();
                        if (!string.IsNullOrEmpty(Manager.Opties?.LastShownTabName))
                            metroCustomTabControl1.SelectedTab = metroCustomTabControl1.TabPages?.Cast<MetroTabPage>()
                                .FirstOrDefault(x => x.Tag is TileInfoEntry entry && string.Equals(entry.Name,
                                    Manager.Opties.LastShownTabName, StringComparison.CurrentCultureIgnoreCase));
                        tileMainView1.TileCountRefreshInterval = Manager.Opties?.TileCountRefreshRate??30000;
                        var xrooster = mainMenu1.GetButton("xroostermenubutton");
                        if (xrooster != null)
                            xrooster.Image = Manager.Opties?.TijdelijkeRooster == null
                                ? Resources.schedule_32_32
                                : Resources.schedule_32_32.CombineImage(Resources.exclamation_warning_15590, 1.75);

                        //LoadFilter();
                        _manager.SetSettings(settings);
                        CheckForSpecialRooster(false);
                        this.Invalidate();
                        //Manager.Taken?.StartBeheer();
                        //if (Manager.IsLoaded)
                        //    CheckForSpecialRooster(true);
                        if (_specialRoosterWatcher is {Enabled: false})
                            _specialRoosterWatcher.Start();
                        metroCustomTabControl1.Invalidate();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public event LogInChangedHandler OnLoginChanged;

        private void _manager_OnLoginChanged(UserAccount user, object instance)
        {
            try
            {
                BeginInvoke(new Action(() =>
                {
                    if (IsDisposed || Disposing) return;
                    xloginb.Image = user != null ? Resources.Logout_37127__1_ : Resources.Login_37128__1_;
                    xcorruptedfilesbutton.Visible = user is {AccesLevel: AccesType.Manager};
                    xMissingTekening.Visible = user is { AccesLevel: AccesType.Manager };
                    OnLoginChanged?.Invoke(user, instance);
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
            this.BeginInvoke(new Action(() => { _specialRoosterWatcher?.Start();}));

        }

        private void InitDBCorupptedMonitor()
        {
            try
            {
                if (Manager.Database?.ProductieFormulieren?.MultiFiles != null)
                    Manager.Database.ProductieFormulieren.MultiFiles.MonitorCorrupted = true;
                if (Manager.Database?.GereedFormulieren?.MultiFiles != null)
                    Manager.Database.GereedFormulieren.MultiFiles.MonitorCorrupted = true;
                if (Manager.Database?.UserAccounts?.MultiFiles != null)
                    Manager.Database.UserAccounts.MultiFiles.MonitorCorrupted = true;
                if (Manager.Database?.AllSettings?.MultiFiles != null)
                    Manager.Database.AllSettings.MultiFiles.MonitorCorrupted = true;
                if (Manager.Database?.PersoneelLijst?.MultiFiles != null)
                    Manager.Database.PersoneelLijst.MultiFiles.MonitorCorrupted = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DoActie(object[] values, MainAktie type)
        {
            var flag = values is {Length: > 0};
            var xforms = Application.OpenForms.Cast<Form>().Where(x => x is Form_Alert).ToList();
            xforms.ForEach(x=> x.Close());
            switch (type)
            {
                case MainAktie.OpenProductie:
                    if (flag)
                    {
                        var form =
                            (ProductieFormulier) values.FirstOrDefault(x => x is ProductieFormulier);
                        var bew = (Bewerking) values.FirstOrDefault(x => x is Bewerking);
                        if (form != null)
                            ShowProductieForm(this,form, true, bew);
                    }

                    break;
                case MainAktie.OpenIndeling:
                    if (flag)
                    {
                        var form =
                            (ProductieFormulier) values.FirstOrDefault(x => x is ProductieFormulier);
                        ShowWerkplekken(form);
                    }

                    break;
                case MainAktie.OpenProductieWijziging:
                    if (flag)
                    {
                        var form =
                            (ProductieFormulier) values.FirstOrDefault(x => x is ProductieFormulier);
                        ShowProductieSettings(form);
                    }

                    break;
                case MainAktie.OpenInstellingen:
                    ShowOptieWidow();
                    break;
                case MainAktie.OpenRangeSearcher:
                    ShowCalculatieWindow();
                    break;
                case MainAktie.OpenPersoneel:
                    ShowPersoneelWindow();
                    break;
                case MainAktie.OpenStoringen:
                    if (flag)
                    {
                        var bew = (Bewerking) values.FirstOrDefault(x => x is Bewerking);
                        if (bew != null)
                            ShowBewStoringen(bew);
                    }

                    break;
                case MainAktie.OpenAlleStoringen:
                    ShowOnderbrekeningenWidow();
                    break;
                case MainAktie.OpenVaardigheden:
                    if (flag)
                    {
                        var per = (Personeel) values.FirstOrDefault(x => x is Personeel);
                        if (per != null)
                            ShowPersoonVaardigheden(per);
                    }

                    break;
                case MainAktie.OpenAlleVaardigheden:
                    ShowAlleVaardighedenWidow();
                    break;
                case MainAktie.OpenAantalGemaaktProducties:
                    if (values.FirstOrDefault() is List<Bewerking> bws && values.LastOrDefault() is int mins)
                        DoAantalGemaakt(this,bws, mins);
                    break;
                case MainAktie.StartBewerking:
                    if (values.FirstOrDefault() is Bewerking bew2)
                        ProductieListControl.StartBewerkingen(this, new Bewerking[] {bew2});
                    break;
                case MainAktie.StopBewerking:
                    if (values.FirstOrDefault() is Bewerking bew3)
                        _= bew3.StopProductie(true,true);
                    break;
            }
        }

        private void Manager_OnFormulierActie(object[] values, MainAktie type)
        {
            if (IsDisposed || Disposing) return;
            try
            {
                if (InvokeRequired)
                    Invoke(new MethodInvoker(() =>
                    {
                        DoActie(values, type);
                    }));
                else DoActie(values, type);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion Manager

        #region Main Menu Methods

        private void DoCreateExcel()
        {
            var f = new CreateExcelForm();
            f.ShowDialog();
        }

        private async void DoQuickProductie()
        {
            if (Manager.BewerkingenLijst == null || Manager.Database?.ProductieFormulieren == null)
            {
                XMessageBox.Show(this,"Kan geen productie aanmaken, omdat de Database niet is geladen.", "Database niet geladen!", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            var xnewform = new NiewProductieForm();
            var result = xnewform.ShowDialog();
            if (result == DialogResult.Cancel) return;

            var created = xnewform.CreatedFormulier;
            if (created != null)
            {
                //await created.UpdateForm(true, false);
                if (result == DialogResult.Yes && created.Bewerkingen.Length > 0)
                    ProductieListControl.StartBewerkingen(this, created.Bewerkingen);
                else
                    await created.UpdateForm(true, false);
                ShowProductieForm(this, created, true, created.Bewerkingen.FirstOrDefault());
            }
        }

        private async void DoOnderbreking()
        {
            var message =
                "Wat zou je willen doen?\n" +
                "Wil je een storing/onderbreking bekijken, toevoegen of wijzigen?";
            var bttns = new Dictionary<string, DialogResult>();
            bttns.Add("Annuleren", DialogResult.Cancel);
            bttns.Add("Wijzigen", DialogResult.No);
            bttns.Add("Toevoegen", DialogResult.Yes);
            bttns.Add("Bekijken", DialogResult.Ignore);


            var res = XMessageBox.Show(this, message, "Onderbreking", MessageBoxButtons.OK, MessageBoxIcon.Question, null,
                bttns);
            if (res == DialogResult.Cancel) return;
            var prods = await Manager.GetProducties(new[] {ViewState.Gestart, ViewState.Gestopt}, true, false,null);
            var plekken = new List<WerkPlek>();
            switch (res)
            {
                case DialogResult.Ignore:
                case DialogResult.Yes:

                    prods.ForEach(x =>
                    {
                        var xplekken = x.GetAlleWerkplekken();
                        if (res == DialogResult.Ignore)
                            xplekken = xplekken.Where(w => w.Storingen.Count > 0).ToList();
                        if (xplekken.Count > 0)
                            plekken.AddRange(xplekken);
                    });
                    if (plekken.Count == 0)
                    {
                        var xvalue = res == DialogResult.Ignore
                            ? "onderbrekeningen van te bekijken"
                            : "een onderbreking aan toe te voegen";
                        XMessageBox.Show(this, $"Er zijn geen  aangemaakte werkplekken om {xvalue}.", "Geen Werkplekken",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    break;
                case DialogResult.No:
                    prods.ForEach(x =>
                    {
                        var xplekken = x.GetAlleWerkplekken();
                        if (xplekken.Count > 0)
                            foreach (var plek in xplekken)
                                if (plek.Storingen.Any(x => !x.IsVerholpen))
                                    plekken.Add(plek);
                    });
                    if (plekken.Count == 0)
                        XMessageBox.Show(this, $"Er zijn geen openstaande onderbrekeningen om te wijzigen.", "Onderbreking",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }

            if (plekken.Count > 0)
            {
                var selector = new WerkPlekChooser(plekken, null);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    var plek = selector.Selected;
                    if (plek == null) return;
                    NewStoringForm storingform = null;
                    switch (res)
                    {
                        case DialogResult.Yes:
                            storingform = new NewStoringForm(plek);
                            break;
                        case DialogResult.No:
                            var storingen = plek.Storingen.Where(x => !x.IsVerholpen).ToList();
                            if (storingen.Count == 0) return;
                            if (storingen.Count > 1)
                            {
                                var msg = $"Er zijn {storingen.Count} openstaande onderbrekeningen beschikbaar.\n" +
                                          "Kies een openstaande onderbreking om te wijzigen.";

                                var msgbox = new XMessageBox();
                                if (msgbox.ShowDialog(this, msg, "Kies Onderbreking", MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Information,
                                    storingen.Select(x => x.ToString()).ToArray()) == DialogResult.OK)
                                {
                                    var selected = msgbox.SelectedValue;
                                    if (selected != null)
                                    {
                                        var storing = storingen.FirstOrDefault(x => x.Equals(selected));
                                        if (storing != null) storingform = new NewStoringForm(plek, storing);
                                    }
                                }
                            }
                            else
                            {
                                storingform = new NewStoringForm(plek, storingen[0]);
                            }

                            break;
                    }

                    if (storingform != null)
                    {
                        if (storingform.ShowDialog() == DialogResult.OK)
                        {
                            plek.UpdateStoring(storingform.Onderbreking);
                            if (plek.Werk != null)
                            {
                                await plek.Werk?.UpdateBewerking(null,
                                    $"Onderbreking aangepast op {storingform.Onderbreking.Path}");
                                RemoteProductie.SendStoringMail(storingform.Onderbreking, plek.Werk);
                            }

                            var msg = $"Onderbreking {plek.Path}\n" +
                                      $"{storingform.Onderbreking.StoringType} is ";

                            switch (res)
                            {
                                case DialogResult.Yes:
                                    msg += "toegevoegd.";
                                    break;
                                case DialogResult.No:
                                    msg += "gewijzigd.";
                                    break;
                            }

                            XMessageBox.Show(this, msg, "Onderbreking", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (res == DialogResult.Ignore)
                    {
                        var sts = new StoringForm(plek);
                        sts.ShowDialog();
                    }
                }
            }
        }

        //private void SelectProductieItem(object item)
        //{
        //    if (item is ProductieFormulier form)
        //    {
        //        //xproductieListControl1.SelectedItem = form;
        //        metroTabControl.SelectedIndex = 0;
        //    }
        //    else if (item is Bewerking bew)
        //    {
        //        xbewerkingListControl.SelectedItem = bew;
        //        metroTabControl.SelectedIndex = 0;
        //    }
        //    else if (item is WerkPlek plek)
        //    {
        //        werkPlekkenUI1.SelectedWerkplek = plek;
        //        metroTabControl.SelectedIndex = 1;
        //    }
        //}

        private async void DoEigenRooster()
        {
            try
            {
                if (Manager.Opties == null)
                    throw new Exception(
                        "Opties zijn niet geladen en kan daarvoor geen rooster aanpassen.\n\nRaadpleeg Ihab a.u.b.");
                //if (Manager.Opties.TijdelijkeRooster != null)
                //{
                //    var xvalue = "\nMet een bereik:\n";
                //    if (Manager.Opties.TijdelijkeRooster.GebruiktVanaf)
                //        xvalue += $"Vanaf {Manager.Opties.TijdelijkeRooster.Vanaf} ";
                //    if (Manager.Opties.TijdelijkeRooster.GebruiktTot)
                //        xvalue += $"Tot {Manager.Opties.TijdelijkeRooster.Tot}\n";
                //    if (Manager.Opties.TijdelijkeRooster.GebruiktVanaf || Manager.Opties.TijdelijkeRooster.GebruiktTot)
                //        xrstr += xvalue;
                //}

                //var message = "Wat voor rooster zou je willen gebruiken voor alle producties?\n\n" +
                //              $"Momenteel gebruik je {xrstr}.";
                //var dialog = XMessageBox.Show(message, "Eigen Rooster",
                //    MessageBoxButtons.OK, MessageBoxIcon.Question, null, bttns);
               // if (dialog == DialogResult.Cancel) return;
                var xold = Manager.Opties?.GetWerkRooster() ?? Rooster.StandaartRooster();
                var roosterform = new RoosterForm(Manager.Opties.TijdelijkeRooster,
                           "Kies een rooster voor al je werkzaamheden");
                roosterform.ViewPeriode = false;
                if (roosterform.ShowDialog() == DialogResult.Cancel)
                    return;
                Manager.Opties.TijdelijkeRooster = roosterform.WerkRooster;
                var thesame = xold.SameTijden(Manager.Opties?.GetWerkRooster());
                if (!thesame)
                {
                    var bws = await Manager.GetBewerkingen(new ViewState[] {ViewState.Gestart}, true);

                    bws = bws.Where(x => string.Equals(Manager.Opties.Username, x.GestartDoor,
                        StringComparison.CurrentCultureIgnoreCase)).ToList();

                    if (bws.Count > 0)
                    {
                        var bwselector = new BewerkingSelectorForm(bws,true,true);
                        bwselector.Title = "Selecteer Werkplekken waarvan de rooster aangepast moet worden";
                        if (bwselector.ShowDialog() == DialogResult.OK)
                            await Manager.UpdateGestarteProductieRoosters(bwselector.SelectedWerkplekken, roosterform.WerkRooster);
                    }

                }
                var xrooster = mainMenu1.GetButton("xroostermenubutton");
                var iscustom = Manager.Opties.TijdelijkeRooster != null && Manager.Opties.TijdelijkeRooster.IsCustom();
                if (xrooster != null)
                    xrooster.Image = iscustom
                        ? Resources.schedule_32_32.CombineImage(Resources.exclamation_warning_15590, 1.75)
                        : Resources.schedule_32_32;
                var xchange = iscustom ? "aangepaste rooster" : "standaard rooster";
                await Manager.Opties.Save($"{Manager.Opties.Username} heeft een {xchange}.");
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private async void DoOpenStartedProducties()
        {
            try
            {
                var startedprods =
                    (await Manager.Database.GetAllProducties(false, true, null))
                    .Where(x => x.State == ProductieState.Gestart)
                    .ToArray();
                if (startedprods.Length == 0)
                {
                    XMessageBox.Show(this, $"Er zijn geen gestarte producties om te openen.", "Geen Producties",
                        MessageBoxIcon.Exclamation);
                }
                else
                {
                    var xvalue0 = startedprods.Length == 1 ? "is" : "zijn";
                    var xvalue1 = startedprods.Length == 1 ? "productie" : "producties";
                    if (XMessageBox.Show(
                            this, $"Er {xvalue0} {startedprods.Length} gestarte {xvalue1}.\n\nWil je ze allemaal openen?",
                        "Open Producties", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        for (var i = 0; i < startedprods.Length; i++)
                        {
                            var prod = startedprods[i];
                            var bws = prod.Bewerkingen?.Where(x => x.IsAllowed()).ToArray();
                            if (bws?.Length > 0)
                            {
                                var bw = bws.FirstOrDefault(x => x.State == ProductieState.Gestart);
                                if (bw != null)
                                    ShowProductieForm(this,prod, true, bw);
                            }
                        }
                }
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void DoUpdateFormsFromDirectory()
        {
            if (Manager.LogedInGebruiker == null ||
                Manager.LogedInGebruiker.AccesLevel <= AccesType.ProductieBasis) return;
            var ofd = new FolderBrowserDialog()
            {
                Description = "Kies een folder met productieFormulieren pdf's"

            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var xupdater = new UpdateProducties();
                xupdater.CloseWhenFinished = false;
                xupdater.ShowStop = true;
                xupdater.StartWhenShown = true;
                xupdater.UpdateMethod = ()=> Manager.UpdateFormulierenFromDirectory(ofd.SelectedPath, false,xupdater.ProgressChanged);
                xupdater.ShowDialog();
            }
        }

        private void DoLoadDbInstance()
        {
            try
            {
                var msgbox = new XMessageBox();
                var msg = "Kies een database om te laden";
                var bttns = new Dictionary<string, DialogResult>();
                bttns.Add("Annuleren", DialogResult.Cancel);
                bttns.Add("Database Laden", DialogResult.OK);
                bttns.Add("Kies Folder", DialogResult.Yes);
                //var dbs = Manager.DefaultSettings?.DbUpdateEntries ??
                          //UserSettings.GetDefaultSettings()?.DbUpdateEntries ?? new List<DatabaseUpdateEntry>();
                          var dbnames = new List<string>();
                var xroot = Path.GetDirectoryName(Manager.AppRootPath);
                if (Directory.Exists(xroot))
                {
                    var dirs = Directory.GetDirectories(xroot);
                    foreach (var dir in dirs)
                    {
                        if (Directory.Exists(Path.Combine(dir, "RPM_Data")))
                        {
                            dbnames.Add(dir);
                        }
                    }
                }

                dbnames.Add( "Standaard Database");
                var result = msgbox.ShowDialog(this, msg, "Database Laden", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Information, dbnames.ToArray(), bttns);
                if (result == DialogResult.Cancel) return;
                string path = null;
                if (result == DialogResult.OK)
                {
                    if (msgbox.SelectedValue != null && msgbox.SelectedValue.ToLower() == "standaard database")
                    {
                        var stng = Manager.DefaultSettings ?? UserSettings.GetDefaultSettings();
                        if (Directory.Exists(stng.MainDB.RootPath))
                            path = stng.MainDB.RootPath;
                        else path = Application.StartupPath;
                    }
                    else
                    {
                        var ent = dbnames.FirstOrDefault(x =>
                            string.Equals(x, msgbox.SelectedValue, StringComparison.CurrentCultureIgnoreCase));
                        path = ent;
                    }
                }

                if (path == null)
                {
                    var fb = new FolderBrowserDialog {Description = "Kies een database locatie"};
                    if (fb.ShowDialog() == DialogResult.OK)
                        path = fb.SelectedPath;
                }

                if (path != null)
                {
                    if (!Directory.Exists(path))
                    {
                        XMessageBox.Show(this, $"'{path}' bestaat niet, of is niet toegankelijk!", "Fout",
                            MessageBoxIcon.Error);
                        return;
                    }

                    //if (name != null)
                    //{
                    //    var opties = await Manager.Database.GetSetting(name);
                    //    if (opties != null)
                    //        Manager.Opties = opties;
                    //}
                    this.BeginInvoke(new Action(()=> LoadManager(path,true)));
                    //await _manager.Load(path, true, true, true);
                }
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void mainMenu1_OnMenuClick(object sender, EventArgs e)
        {
            if (sender is Button {Tag: MenuButton menubutton})
                try
                {
                    switch (menubutton.Name.ToLower())
                    {
                        case "xhome":
                            if (metroCustomTabControl1.TabCount > 0)
                                metroCustomTabControl1.SelectedIndex = 0;
                            break;
                        case "xniewproductie":
                            if (Manager.Database?.ProductieFormulieren == null)
                            {
                                XMessageBox.Show(this, $"Kan geen productie aanmaken, omdat de Database niet is geladen.", "Database niet geladen!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                                return;
                            }
                            var AddProduction = new WijzigProductie();
                            if (AddProduction.ShowDialog() == DialogResult.OK)
                                BeginInvoke(new MethodInvoker(async () =>
                                {
                                    var form = AddProduction.Formulier;
                                    var msg = await Manager.AddProductie(form,false);
                                    Manager.RemoteMessage(msg);
                                }));

                            break;
                        case "xopenproductie":
                            if (Manager.Database?.ProductieFormulieren == null)
                            {
                                XMessageBox.Show(this, $"Kan geen productie toevoegen, omdat de Database niet is geladen.", "Database niet geladen!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                                return;
                            }
                            var ofd = new OpenFileDialog
                            {
                                Title = @"Open Productie Formulier(en)",
                                Filter = @"Pdf|*.pdf|Image|*.JPG;*.Img|Text|*.txt|Alles|*.*",
                                Multiselect = true
                            };
                            if (ofd.ShowDialog() == DialogResult.OK)
                                BeginInvoke(new MethodInvoker(async () =>
                                {
                                    var files = ofd.FileNames;
                                    await _manager.AddProductie(files,true, false, true);
                                }));
                            break;
                        case "xquickproductie":
                            DoQuickProductie();
                            break;
                        case "xsearchtekening":
                            ZoekWerkTekening();
                            break;
                        case "xchangeaantal":
                            DoAantalGemaakt(this);
                            break;
                        case "xpersoneelindeling":
                            ShowPersoneelIndelingWindow();
                            break;
                        case "xwerkplaatsindeling":
                            ShowWerkplaatsIndelingWindow();
                            break;
                        case "xverbruik":
                            ShowBerekenVerbruikWindow();
                            break;
                        case "xcreateexcel":
                            //maak een nieuwe excel aan
                            DoCreateExcel();
                            break;
                        case "xupdatedb":
                            var updater = new DbUpdater();
                            updater.ShowDialog();
                            break;
                        case "xlaaddb":
                            DoLoadDbInstance();
                            break;
                        case "xupdateforms":
                            DoUpdateFormsFromDirectory();
                            break;
                        case "xstats":
                            var chartform = new ViewChartForm();
                            chartform.ShowDialog();
                            break;
                        case "xstoringmenubutton": //storing verwerken
                            DoOnderbreking();
                            break;
                        case "xbekijkproductiepdf":
                            break;
                        case "xroostermenubutton":
                            SetSpecialeRooster();
                            break;
                        case "xspecialeroosterbutton":
                          BeheerSpecialeRoosters();
                            break;
                        case "xopenproducties":
                            DoOpenStartedProducties();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    XMessageBox.Show(this, ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        #endregion Main Menu Methods

        #region Control Events

        private void xUpdate_Click(object sender, EventArgs e)
        {
            AutoUpdater.Start(false);
        }

        private void xsettingsb_Click(object sender, EventArgs e)
        {
            ShowOptieWidow();
        }

        private void xloginb_Click(object sender, EventArgs e)
        {
            if (Manager.LogedInGebruiker != null)
            {
                Manager.LogOut(this,true);
            }
            else
            {
                var xlogin = new LogIn();
                xlogin.StartPosition = FormStartPosition.CenterParent;
                xlogin.ShowDialog();
            }
        }

        private void xupdateallform_Click(object sender, EventArgs e)
        {
            var prod = new UpdateProducties();
            prod.ShowDialog();
        }

        private void xmateriaalverbruikb_Click(object sender, EventArgs e)
        {
            var xmats = new MateriaalVerbruikForm();
            xmats.ShowDialog();
        }

        private void xsendemail_Click(object sender, EventArgs e)
        {
            var emailform = new SendEmailForm();
            emailform.ShowDialog();
        }

        private void xallenotities_Click(object sender, EventArgs e)
        {
            var noteform = new AlleNotitiesForm();
            noteform.ShowDialog();
        }

        private void xchatformbutton_Click(object sender, EventArgs e)
        {
            ShowChatWindow();
        }

        private void xpersoneelb_Click(object sender, EventArgs e)
        {
            ShowPersoneelWindow();
        }

        private void xallstoringenb_Click(object sender, EventArgs e)
        {
            ShowOnderbrekeningenWidow();
        }

        private void xtoonlogsb_Click(object sender, EventArgs e)
        {
            ShowProductieLogWindow();
        }

        private void xallevaardighedenb_Click(object sender, EventArgs e)
        {
            ShowAlleVaardighedenWidow();
        }

        private void xprodinfob_Click(object sender, EventArgs e)
        {
            ShowCalculatieWindow();
        }

        private void xspeciaalroosterbutton_Click(object sender, EventArgs e)
        {
            SetSpecialeRooster();
        }

        private void xdbbewerkingen_Click(object sender, EventArgs e)
        {
            ShowBewerkingDb();
        }

        private void WerkPlekkenUI1_OnRequestOpenWerk(object sender, EventArgs e)
        {
            if (!(sender is Bewerking b)) return;
            if (b.Parent != null) ShowProductieForm(this,b.Parent, true, b);
        }

        private void xaboutb_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }

        //private readonly Timer _prodsearch = new() {Interval = 250};
        //private readonly Timer _bwssearch = new() {Interval = 250};

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.valksolarsystems.com/nl");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.valksystemen.nl/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.valksystemen.nl/");
        }

        #endregion Control Events

        #region Methods

        public Task RunProductieRefresh()
        {
            return Task.Run(async () =>
            {
                try
                {
                    while (ProductieSyncEnabled && !IsDisposed && !Disposing)
                    {
                        await Task.Delay(ProductieRefreshInterval);
                        if (!ProductieSyncEnabled || IsDisposed || Disposing) break;
                        BeginInvoke(new MethodInvoker(xUpdateList));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }

        private void xUpdateList()
        {
            try
            {
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void CheckForSpecialRooster(bool prompchange)
        {
            if (Manager.Opties == null || Manager.LogedInGebruiker == null ||
                Manager.LogedInGebruiker.AccesLevel == AccesType.AlleenKijken)
            {
                xspeciaalroosterlabel.Visible = false;
                return;
            }
           
            var xtime = DateTime.Now;
            //eerst kijken of het weekend is.
            var culture = new CultureInfo("nl-NL");
            var day = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
            if (xtime.DayOfWeek == DayOfWeek.Saturday || xtime.DayOfWeek == DayOfWeek.Sunday)
            {
                //het is weekend, dus we zullen moeten kijken of er wel gewerkt wordt.
                var xbttntxt =
                    $"Het is {day} {xtime.TimeOfDay:hh\\:mm} uur. Vandaag is geen officiële werkdag. Click hier voor de speciale rooster";
                xspeciaalroosterbutton.Text = xbttntxt;
                var rooster = Manager.Opties.SpecialeRoosters.FirstOrDefault(x => x.Vanaf.Date == xtime.Date);
                if (rooster == null && prompchange)
                {
                    var splash = Application.OpenForms["SplashScreen"];
                    splash?.Close();
                    var xmsg =
                        $"Het is vandaag {day}, en geen officiële werkdag.\n" +
                        "Je kan een speciale rooster toevoegen als er vandaag toch wordt gewerkt.\n\n" +
                        "Wil je een rooster nu toevoegen?\nSpeciale roosters kan je achteraf ook aanpassen in de instellingen";
                    if (XMessageBox.Show(this, xmsg, "Speciaal Rooster", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        SetSpecialeRooster();
                }

                xspeciaalroosterlabel.Visible = true;
            }
            else
            {
                var rooster = Manager.Opties.WerkRooster;
                if (xtime.TimeOfDay < rooster.StartWerkdag || xtime.TimeOfDay > rooster.EindWerkdag)
                {
                    var xbttntxt =
                        $"Het is nu {day} {xtime.TimeOfDay:hh\\:mm} uur, dat is buiten om de gewone werkrooster van {rooster.StartWerkdag:hh\\:mm} tot {rooster.EindWerkdag:hh\\:mm} uur. Click hier voor de aangepaste rooster";
                    xspeciaalroosterbutton.Text = xbttntxt;
                    xspeciaalroosterlabel.Visible = true;
                }
                else
                {
                    xspeciaalroosterlabel.Visible = false;
                }
            }

            if (_specialRoosterWatcher != null)
            {
                xtime = DateTime.Now;
                _specialRoosterWatcher.Interval = (60 - xtime.Second) * 1000 - xtime.Millisecond;
            }
        }

        private void SetSpecialeRooster()
        {
            if (Manager.Opties == null) return;
            var xtime = DateTime.Now;
            //eerst kijken of het weekend is.
            var culture = new CultureInfo("nl-NL");
            var day = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
            if (xtime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                var rooster = Manager.Opties?.SpecialeRoosters?.FirstOrDefault(x => x.Vanaf.Date == DateTime.Now.Date);
                var xreturn = rooster != null;
                if (rooster == null)
                {
                    rooster = Rooster.StandaartRooster();
                    rooster.Vanaf = DateTime.Now;
                    rooster.GebruiktPauze = false;
                    rooster.StartWerkdag = new TimeSpan(7, 0, 0);
                    rooster.EindWerkdag = new TimeSpan(12, 0, 0);
                }

                var roosterform = new RoosterForm(rooster, "Vul in de speciale werkdag tijden");
                roosterform.ViewPeriode = false;
                if (roosterform.ShowDialog() == DialogResult.OK)
                {
                    if (Manager.Opties.SpecialeRoosters == null)
                        Manager.Opties.SpecialeRoosters = new List<Rooster>();
                    var newrooster = roosterform.WerkRooster;
                    var dt = DateTime.Now;
                    var tijd = roosterform.WerkRooster.StartWerkdag;
                    newrooster.Vanaf = new DateTime(dt.Year, dt.Month, dt.Day, tijd.Hours, tijd.Minutes, 0);
                    Manager.Opties.SpecialeRoosters.Remove(rooster);
                    Manager.Opties.SpecialeRoosters.Add(newrooster);
                    Manager.Opties.SpecialeRoosters = Manager.Opties.SpecialeRoosters.OrderBy(x => x.Vanaf).ToList();

                    var bws = Manager.GetBewerkingen(new ViewState[] { ViewState.Gestart }, true).Result;
                    bws = bws.Where(x => string.Equals(Manager.Opties.Username, x.GestartDoor,
                        StringComparison.CurrentCultureIgnoreCase)).ToList();
                    if (bws.Count > 0)
                    {
                        var bwselector = new BewerkingSelectorForm(bws,true,true);
                        bwselector.Title = "Selecteer Werkplekken waarvan de rooster aangepast moet worden";
                        if (bwselector.ShowDialog() == DialogResult.OK)
                            Manager.UpdateGestarteProductieRoosters(bwselector.SelectedWerkplekken, null);
                    }

                    Manager.Opties.Save("Speciale roosters aangepast.");
                }
            }
            else
            {
                //var rooster = Manager.Opties.WerkRooster;
                //if (xtime.TimeOfDay < rooster.StartWerkdag || xtime.TimeOfDay > rooster.EindWerkdag)
                //{

                //}
                DoEigenRooster();
            }
        }

        private async void BeheerSpecialeRoosters()
        {
            var sproosters = new SpeciaalWerkRoostersForm(Manager.Opties.SpecialeRoosters);
            if (sproosters.ShowDialog() == DialogResult.OK)
            {
               
                var acces1 = Manager.LogedInGebruiker is { AccesLevel: >= AccesType.ProductieBasis };
                if (acces1)
                {
                    Manager.Opties.SpecialeRoosters = sproosters.Roosters;
                }
                if (acces1 && sproosters.Roosters.Count > 0)
                {
                    var bws = await Manager.GetBewerkingen(new ViewState[] { ViewState.Gestart }, true);
                    bws = bws.Where(x => string.Equals(Manager.Opties.Username, x.GestartDoor,
                        StringComparison.CurrentCultureIgnoreCase)).ToList();
                    if (bws.Count > 0)
                    {
                        var bwselector = new BewerkingSelectorForm(bws,true,true);
                        bwselector.Title = "Selecteer Werkplekken waarvan de rooster aangepast moet worden";
                        if (bwselector.ShowDialog() == DialogResult.OK)
                            await Manager.UpdateGestarteProductieRoosters(bwselector.SelectedWerkplekken, null);
                    }
                }

                if (acces1)
                    _ = Manager.Opties.Save();
            }
        }

        private void CheckForSyncDatabase()
        {
            var opties = Manager.DefaultSettings ?? UserSettings.GetDefaultSettings();
            if (opties?.TempMainDB != null && opties.MainDB != null &&
                opties.TempMainDB.LastUpdated > opties.MainDB.LastUpdated && Directory.Exists(opties.MainDB.UpdatePath))
            {
                var splash = (SplashScreen) Application.OpenForms["SplashScreen"];
                if (splash != null)
                {
                    while (splash.Visible && !splash.CanClose)
                        Application.DoEvents();
                    splash.Close();
                }

                opties.TempMainDB.LastUpdated = opties.MainDB.LastUpdated;
                var prod = new UpdateProducties(opties.TempMainDB)
                    {CloseWhenFinished = true, ShowStop = false, StartWhenShown = true};
                prod.ShowDialog();
                if (prod.IsFinished)
                {
                    opties.MainDB.LastUpdated = DateTime.Now;
                    opties.SaveAsDefault();
                }
            }
        }

        //public static void CheckForUpdateDatabase()
        //{
        //    var opties = Manager.DefaultSettings ?? UserSettings.GetDefaultSettings();
        //    if (opties != null && new Version(LocalDatabase.DbVersion) > new Version(opties.UpdateDatabaseVersion))
        //    {
        //        var splash = Application.OpenForms["SplashScreen"];
        //        splash?.Close();

        //        var prod = new UpdateProducties {CloseWhenFinished = true, ShowStop = false, StartWhenShown = true};
        //        if (prod.ShowDialog() == DialogResult.OK)
        //        {
        //            opties.UpdateDatabaseVersion = LocalDatabase.DbVersion;
        //            opties.SaveAsDefault();
        //        }
        //    }
        //}

        private async void LoadStartedProducties()
        {
            if (_manager == null || Manager.Opties == null || !Manager.Opties.ToonAlleGestartProducties)
                return;
            if (Manager.LogedInGebruiker is {AccesLevel: >= AccesType.ProductieBasis})
            {
                var prs = await Manager.GetAllProductieIDs(false, true);
                foreach (var v in prs)
                {
                    var prod = Manager.Database.GetProductie(v);
                    if (prod?.Bewerkingen == null || prod.Bewerkingen.Length == 0) continue;
                    var xs = prod.Bewerkingen.FirstOrDefault(x => x.State == ProductieState.Gestart);
                    if (xs == null) continue;
                    ShowProductieForm(this,prod, true, xs);
                }
            }
        }

        private void LoadProductieLogs()
        {
            if (_manager == null || Manager.Opties == null || !Manager.Opties.ToonProductieLogs)
                return;
            ShowProductieLogWindow();
        }

        private XMessageBox _unreadMessages;

        public void UpdateUnreadMessages(UserChat user)
        {
            if (this.InvokeRequired)
                Invoke(new Action(xUpdateUnreadMessages));
            else xUpdateUnreadMessages();
        }

        private void xUpdateUnreadMessages()
        {
            try
            {
                if (ProductieChat.Chat == null) return;
                var unread = ProductieChat.Chat.GetAllUnreadMessages();
                var xtile = tileMainView1.GetTile("xchat");
                if (unread.Count > 0)
                {
                    var ximg = GraphicsExtensions.DrawUserCircle(new Size(32, 32), Brushes.White,
                        unread.Count.ToString(),
                        new Font("Ariel", 16, FontStyle.Bold), Color.DarkRed);
                    xchatformbutton.Image = Resources.conversation_chat_32x321.CombineImage(ximg, 1.75);

                    if (xtile is {Tag: TileInfoEntry entry})
                    {
                        entry.TileCount = ProductieChat.Gebruikers.Count(x=> x.IsOnline && x.UserName.ToLower() != "iedereen");
                        entry.SecondaryImage = ximg;
                        xtile.UpdateTile(entry);
                    }
                }
                else
                {
                    if (xtile is {Tag: TileInfoEntry entry})
                    {
                        entry.TileCount = ProductieChat.Gebruikers.Count(x => x.IsOnline && x.UserName.ToLower() != "iedereen");
                        entry.SecondaryImage = null;
                        xtile.UpdateTile(entry);
                    }
                    xchatformbutton.Image = Resources.conversation_chat_32x321;
                }

                if (_chatform != null)
                {
                    if (_chatform.WindowState == FormWindowState.Minimized)
                        _chatform.WindowState = FormWindowState.Normal;
                    //if (user != null)
                    //    _chatform.SelectedUser(user);
                    _chatform.Show();
                    _chatform.BringToFront();
                    _chatform.Focus();
                }
                else if (unread.Count > 0 && ShowUnreadMessage)
                {
                    if (_unreadMessages != null && !_unreadMessages.IsDisposed)
                    {
                        if (!_unreadMessages.Visible)
                            _unreadMessages.Show();
                        _unreadMessages.BringToFront();
                        _unreadMessages.Select();
                        _unreadMessages.Focus();
                        return;
                    }
                    _unreadMessages?.Dispose();
                    var names = new List<string>();
                    foreach (var msg in unread.Where(msg => msg.Afzender != null && !names.Any(x =>
                        string.Equals(x, msg.Afzender.UserName, StringComparison.CurrentCultureIgnoreCase))))
                        names.Add(msg.Afzender.UserName);
                    bool iedereen = unread.Any(x =>
                        string.Equals(x.Ontvanger, "iedereen", StringComparison.CurrentCultureIgnoreCase));
                    if (names.Count == 0) return;
                    {
                        Application.OpenForms["SplashScreen"]?.Close();
                        var xv = names.Count == 1 ? "bericht" : "berichten";
                        var bttns = new Dictionary<string, DialogResult>();
                        bttns.Add("OK", DialogResult.OK);
                        bttns.Add("Toon Bericht", DialogResult.Yes);
                        _unreadMessages = new XMessageBox();
                        _unreadMessages.StartPosition = FormStartPosition.CenterScreen;
                        _unreadMessages.Location = new Point(0, this.Height / 2);
                        var result = _unreadMessages.ShowDialog(
                            this, $"Je hebt {unread.Count} ongelezen {xv} van {string.Join(", ", names)}",
                            $"{unread.Count} ongelezen berichten", MessageBoxButtons.OK, MessageBoxIcon.None, null,
                            bttns);
                        _unreadMessages?.Dispose();
                        _unreadMessages = null;
                        if (result == DialogResult.Yes)
                        {
                            xtile = tileMainView1.GetTile("xchat");
                            if (xtile is { Tag: TileInfoEntry entry})
                            {
                                InitChatTab(entry, true, iedereen ? "Iedereen" : names[0]);
                            }
                            else
                                ShowChatWindow(iedereen ? "Iedereen" : names[0]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UpdateUnreadOpmerkingen()
        {
            if (this.InvokeRequired)
                BeginInvoke(new Action(xUpdateUnreadOpmerkingen));
            else xUpdateUnreadOpmerkingen();
        }

        public void xUpdateUnreadOpmerkingen()
        {
            try
            {
                if (Manager.Opmerkingen?.OpmerkingenLijst == null) return;
                var unread = Manager.Opmerkingen.GetUnreadNotes();
                if (unread.Count > 0)
                {
                    var ximg = GraphicsExtensions.DrawUserCircle(new Size(32, 32), Brushes.White,
                        unread.Count.ToString(),
                        new Font("Ariel", 16, FontStyle.Bold), Color.DarkRed);
                    xopmerkingentoolstripbutton.Image = Resources.notes_office_page_papers_32x32.CombineImage(ximg, 1.75);
                }
                else
                {
                    xopmerkingentoolstripbutton.Image = Resources.notes_office_page_papers_32x32;
                }

                if (_opmerkingform != null)
                {
                    if (_opmerkingform.WindowState == FormWindowState.Minimized)
                        _opmerkingform.WindowState = FormWindowState.Normal;
                    //if (user != null)
                    //    _chatform.SelectedUser(user);
                    _opmerkingform.Show();
                    _opmerkingform.BringToFront();
                    _opmerkingform.Focus();
                }
                else if (unread.Count > 0 && ShowUnreadMessage)
                {
                    Application.OpenForms["SplashScreen"]?.Close();
                    var xv = unread.Count == 1 ? "opmerking" : "opmerkingen";
                    var bttns = new Dictionary<string, DialogResult>();
                    bttns.Add("OK", DialogResult.OK);
                    bttns.Add("Toon Opmerkingen", DialogResult.Yes);
                    var xmsgBox = new XMessageBox();
                    var result = xmsgBox.ShowDialog(
                        this, $"Je hebt {unread.Count} ongelezen {xv}",
                        $"{unread.Count} ongelezen {xv}", MessageBoxButtons.OK, MessageBoxIcon.None, null,
                        bttns);
                    xmsgBox.Dispose();
                    if (result == DialogResult.Yes)
                        ShowOpmerkingWindow();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion Methods

        #region MenuButton Methods

        public static StartProductie ShowProductieForm(IWin32Window owner, ProductieFormulier pform, bool showform,
            Bewerking bewerking = null)
        {
            if (pform == null || Manager.LogedInGebruiker == null ||
                Manager.LogedInGebruiker.AccesLevel < AccesType.ProductieBasis)
                return null;
            try
            {
                var bws = pform.Bewerkingen;
                    //.Where(x =>
                   // x.IsAllowed() && x.State != ProductieState.Verwijderd).ToList();
               // if (bws.Count == 0)
                   // throw new Exception(
                 //       $"Kan '{pform.Omschrijving}' niet openen omdat er geen geldige bewerkingen zijn!\n\n" +
                   //     "Bewerkingen zijn verwijderd of gefiltered.");
                var productie = _formuis.FirstOrDefault(t => t.Name == pform.ProductieNr.Trim().Replace(" ", ""));
                if (productie is {IsDisposed: false} && _producties != null && showform)
                {
                    productie.Show(_producties.DockPanel);
                }
                else
                {
                    if (productie != null)
                        _formuis.Remove(productie);
                    //productie.Dispose();

                    productie = new StartProductie(pform, bewerking)
                    {
                        Name = pform.ProductieNr.Trim().Replace(" ", ""),
                        TabText = $"[{pform.ProductieNr}, {pform.ArtikelNr}]"
                    };
                    productie.FormClosing += AddProduction_FormClosing;
                    productie.SelectedBewerking = bewerking;
                    _formuis.Add(productie);

                    if (_producties == null || _producties.IsDisposed)
                    {
                        _producties = new Producties
                        {
                            Tag = productie,
                            StartPosition = FormStartPosition.CenterScreen
                        };
                        _producties.FormClosed += (x, y) => _producties = null;
                    }

                    if (showform)
                        productie.Show(_producties.DockPanel);
                }

                if (!_producties.Visible && showform)
                    _producties.Show();
                if (_producties.WindowState == FormWindowState.Minimized)
                    _producties.WindowState = FormWindowState.Normal;
                _producties.BringToFront();
                _producties.Focus();
                _producties.Select();
                return productie;
            }
            catch (Exception e)
            {
                XMessageBox.Show(owner, e.Message, "Fout", MessageBoxIcon.Error);
                return null;
            }
        }

        public static ProductieLijstForm ShowProductieLijstForm(IWin32Window owner)
        {
            try
            {
                var prodform = new ProductieLijstForm(_Productelijsten.Count);

                prodform.FormClosing += AddProduction_FormClosing;
                _Productelijsten.Add(prodform);
                if (_productielijstdock == null || _productielijstdock.IsDisposed)
                {
                    _productielijstdock = new ProductieLijsten
                    {
                        Tag = prodform,
                        StartPosition = FormStartPosition.CenterScreen
                    };
                    _productielijstdock.FormClosed += (x, y) => _productielijstdock = null;
                }

                prodform.Show(_productielijstdock.DockPanel);

                if (!_productielijstdock.Visible)
                    _productielijstdock.Show();
                if (_productielijstdock.WindowState == FormWindowState.Minimized)
                    _productielijstdock.WindowState = FormWindowState.Normal;
                _productielijstdock.BringToFront();
                _productielijstdock.Focus();
                _productielijstdock.Select();
                return prodform;
            }
            catch (Exception e)
            {
                XMessageBox.Show(owner, e.Message, "Fout", MessageBoxIcon.Error);
                return null;
            }
        }

        public static void AddProduction_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sender is StartProductie prod)
                _formuis.Remove(prod);
            else if (sender is ProductieLijstForm form)
                _Productelijsten.Remove(form);
        }

        public static void ShowProductieLogWindow()
        {
            if (_logform == null)
            {
                _logform = new LogForm();
                _logform.FormClosed += (x, y) =>
                {
                    _logform?.Dispose();
                    _logform = null;
                };
            }

            _logform.Show();
            if (_logform.WindowState == FormWindowState.Minimized)
                _logform.WindowState = FormWindowState.Normal;
            _logform.BringToFront();
            _logform.Focus();
        }

        public static void ShowCalculatieWindow()
        {
            var _calcform = new RangeCalculatorForm();
            _calcform.Show();
            _calcform.BringToFront();
            _calcform.Focus();
        }

        public static void ShowChatWindow(string username = null)
        {
            if (_chatform == null)
            {
                _chatform = new ChatForm();
                _chatform.FormClosed += (x, y) =>
                {
                    _chatform?.Dispose();
                    _chatform = null;
                };
            }

            _chatform.Show(username);
            if (_chatform.WindowState == FormWindowState.Minimized)
                _chatform.WindowState = FormWindowState.Normal;
            _chatform.BringToFront();
            _chatform.Focus();
        }

        public static void ShowOpmerkingWindow()
        {
            if (_opmerkingform == null)
            {
                _opmerkingform = new OpmerkingenForm();
                _opmerkingform.LoadOpmerkingen();
                _opmerkingform.FormClosed += (x, y) =>
                {
                    _opmerkingform?.Dispose();
                    _opmerkingform = null;
                };
            }

            _opmerkingform.Show();
            if (_opmerkingform.WindowState == FormWindowState.Minimized)
                _opmerkingform.WindowState = FormWindowState.Normal;
            _opmerkingform.BringToFront();
            _opmerkingform.Focus();
        }

        public static void ShowArtikelenWindow(string artnr = null)
        {
            if (_ArtikelsForm == null)
            {
                _ArtikelsForm = new ArtikelsForm();
                _ArtikelsForm.FormClosed += (x, y) =>
                {
                    _ArtikelsForm?.Dispose();
                    _ArtikelsForm = null;
                };
            }

            _ArtikelsForm.Show();
            if (_ArtikelsForm.WindowState == FormWindowState.Minimized)
                _ArtikelsForm.WindowState = FormWindowState.Normal;
            _ArtikelsForm.BringToFront();
            _ArtikelsForm.Focus();
            if (artnr != null)
                _ArtikelsForm.SelectedArtikelNr = artnr;
        }

        public static void ShowOnderbrekeningenWidow()
        {
            var x = new AlleStoringenForm();
            x.InitStoringen();
            x.ShowDialog();
        }

        public static void ShowAlleVaardighedenWidow()
        {
            new AlleVaardigheden().ShowDialog();
        }

        public static void ShowPersoneelWindow()
        {
            if (_PersoneelForm == null)
            {
                _PersoneelForm = new PersoneelsForm();
                _PersoneelForm.FormClosed += (x, y) =>
                {
                    _PersoneelForm?.Dispose();
                    _PersoneelForm = null;
                };
            }

            _PersoneelForm.Show();
            if (_PersoneelForm.WindowState == FormWindowState.Minimized)
                _PersoneelForm.WindowState = FormWindowState.Normal;
            _PersoneelForm.BringToFront();
            _PersoneelForm.Focus();
        }

        public static void ShowWerkplaatsIndelingWindow()
        {
            if (_WerkplaatsIndeling == null)
            {
                _WerkplaatsIndeling = new WerkplaatsIndelingForm();
                _WerkplaatsIndeling.FormClosed += (x, y) =>
                {
                    _WerkplaatsIndeling?.Dispose();
                    _WerkplaatsIndeling = null;
                };
            }

            _WerkplaatsIndeling.Show();
            if (_WerkplaatsIndeling.WindowState == FormWindowState.Minimized)
                _WerkplaatsIndeling.WindowState = FormWindowState.Normal;
            _WerkplaatsIndeling.BringToFront();
            _WerkplaatsIndeling.Focus();
        }

        public static void ShowPersoneelIndelingWindow()
        {
            if (_PersoneelIndeling == null)
            {
                _PersoneelIndeling = new PersoneelIndelingForm();
                _PersoneelIndeling.FormClosed += (x, y) =>
                {
                    _PersoneelIndeling?.Dispose();
                    _PersoneelIndeling = null;
                };
            }

            _PersoneelIndeling.Show();
            if (_PersoneelIndeling.WindowState == FormWindowState.Minimized)
                _PersoneelIndeling.WindowState = FormWindowState.Normal;
            _PersoneelIndeling.BringToFront();
            _PersoneelIndeling.Focus();
        }

        public static void ShowBerekenVerbruikWindow()
        {
            if (_berekenverbruik == null)
            {
                _berekenverbruik = new MetroForm();
                _berekenverbruik.Style = MetroColorStyle.Orange;
                _berekenverbruik.StartPosition = FormStartPosition.CenterScreen;
                _berekenverbruik.ShadowType = MetroFormShadowType.AeroShadow;
                _berekenverbruik.MinimumSize = new Size(750, 550);
                _berekenverbruik.Text = "Bereken Verbruik";
                var xver = new ProductieVerbruikUI();
                xver.Dock = DockStyle.Fill;
                xver.ShowMateriaalSelector = false;
                xver.ShowOpslaan = true;
                xver.ShowPerUur = true;
                xver.ShowSluiten = true;
                xver.ShowOpdrukkerArtikelNr = true;
                xver.UpdateFields(true);
                xver.MaxUitgangsLengte = 12450;
                xver.RestStuk = 50;
                xver.CloseClicked += (x, y) =>
                {
                    _berekenverbruik.Close();
                };
                _berekenverbruik.Controls.Add(xver);
                _berekenverbruik.FormClosed += (x, y) =>
                {
                    _berekenverbruik?.Dispose();
                    _berekenverbruik = null;
                };
            }

            _berekenverbruik.Show();
            if (_berekenverbruik.WindowState == FormWindowState.Minimized)
                _berekenverbruik.WindowState = FormWindowState.Normal;
            _berekenverbruik.BringToFront();
            _berekenverbruik.Focus();
        }

        public static void ShowPersoonVaardigheden(Personeel persoon)
        {
            if (persoon == null)
                return;
            new VaardighedenForm(persoon).ShowDialog();
        }

        public static void ShowBewStoringen(Bewerking bew)
        {
            var form = bew?.GetParent();
            if (form == null) return;
            var allst = new AlleStoringenForm();
            allst.InitStoringen(form);
            allst.ShowDialog();
        }

        public static void ShowWerkplekken(ProductieFormulier form)
        {
            if (form == null)
                return;
            var ind = new Indeling(form);
            ind.ShowDialog();
        }

        public void ZoekWerkTekening()
        {
            try
            {
                var tb = new TextFieldEditor();
                tb.Style = MetroColorStyle.Yellow;
                tb.FieldImage = Resources.libreoffice_draw_icon_128x128.CombineImage(Resources.search_locate_find_6278,
                    ContentAlignment.BottomRight, 2.5);
                tb.MultiLine = false;
                tb.Title = "Zoek WerkTekening";
                if (tb.ShowDialog() == DialogResult.OK)
                {
                    Tools.ShowSelectedTekening(tb.SelectedText, TekeningClosed);
                }
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private static AantalGemaaktProducties _gemaaktform;
        public static async void DoAantalGemaakt(IWin32Window owner, List<Bewerking> bewerkingen = null, int lastchangedminutes = -1)
        {
            try
            {
                if (Manager.Database == null || Manager.Database.IsDisposed)
                    return;
                if (_gemaaktform != null) return;
                var bws = bewerkingen??await Manager.Database.GetBewerkingen(ViewState.Gestart, true,null, null);
                _gemaaktform = new AantalGemaaktProducties(bws, lastchangedminutes);
                _gemaaktform.FormClosed += (o,e) =>
                {
                    _gemaaktform?.Dispose();
                    _gemaaktform = null;
                };
                _gemaaktform.ShowDialog();

            }
            catch (Exception e)
            {
                XMessageBox.Show(owner, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void TekeningClosed(object sender, EventArgs e)
        {
            this.Parent?.BringToFront();
            this.Parent?.Focus();
        }

        public static void ShowProductieSettings(ProductieFormulier form)
        {
            if (form == null)
                return;
            var x = new WijzigProductie(form);
            x.ShowDialog();
        }

        public static void ShowOptieWidow()
        {
            var opties = new Opties();
            opties.ShowDialog();
            //if (_opties != null)
            //{
            //    if (!_opties.Visible)
            //        _opties.Show();
            //    if (_opties.WindowState == FormWindowState.Minimized)
            //        _opties.WindowState = FormWindowState.Normal;
            //}
            //else
            //{
            //    _opties = new Opties();
            //    _opties.FormClosed += (x, y) => { _opties = null; };
            //    _opties.Show();
            //}

            //_opties.BringToFront();
            //_opties.Select();
        }

        public static void ShowBewerkingDb()
        {
            var db = new DbBewerkingChanger();
            db.ShowDialog();
        }

        public static void ShowProductieOverzicht()
        {
            if (_ProductieOverzicht == null)
            {
                _ProductieOverzicht = new ProductieOverzichtForm();
                _ProductieOverzicht.FormClosed += (x, y) =>
                {
                    _ProductieOverzicht?.Dispose();
                    _ProductieOverzicht = null;
                };
            }

            _ProductieOverzicht.Show();
            if (_ProductieOverzicht.WindowState == FormWindowState.Minimized)
                _ProductieOverzicht.WindowState = FormWindowState.Normal;
            _ProductieOverzicht.BringToFront();
            _ProductieOverzicht.Focus();
        }

        public void ShowKlachtenWindow()
        {
            if (_Klachten == null)
            {
                _Klachten = new KlachtenForm();
                _Klachten.FormClosed += (x, y) =>
                {
                    _Klachten?.Dispose();
                    _Klachten = null;
                };
            }

            _Klachten.Show();
            if (_Klachten.WindowState == FormWindowState.Minimized)
                _Klachten.WindowState = FormWindowState.Normal;
            _Klachten.BringToFront();
            _Klachten.Focus();
        }

        public void ShowCreateWeekOverzicht()
        {
            var xf = new CreateWeekExcelForm();
            xf.ShowDialog();
        }

        public static void CheckForPreview(bool showall, bool onlyifnew)
        {
            try
            {
                var xprevs =
                    Functions.GetVersionPreviews(Manager.LastPreviewsUrl);
                var xvers = Assembly.GetExecutingAssembly().GetName().Version;
                if (xprevs.Count > 0)
                {
                    UpdatePreviewForm xshowform = null;
                    if (!showall)
                    {
                        
                        if (!xprevs.ContainsKey(xvers.ToString())) return;
                        bool isnew = new Version(Manager.DefaultSettings.LastPreviewVersion) < xvers;
                        if (onlyifnew && (Manager.DefaultSettings.PreviewShown && !isnew))
                            return;
                        xshowform = new UpdatePreviewForm(xprevs[xvers.ToString()], onlyifnew,false);
                        xshowform.Title = $"NIEUW In {xvers}!";
                    }
                    else
                    {
                        var urls = xprevs.Select(x => x.Value).ToArray();
                        xshowform = new UpdatePreviewForm(urls);
                        xshowform.Title = $"Alle Aanpassingen In Versie: {xprevs.LastOrDefault().Key?? xvers.ToString()}";
                    }

                    xshowform.ShowDialog();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion MenuButton Methods

        #region Menu Button Events
        private void xproductieoverzichtb_Click(object sender, EventArgs e)
        {
            ShowProductieOverzicht();
        }

        private void xsearchprodnr_Click(object sender, EventArgs e)
        {
            try
            {
                if (Manager.Database == null)
                    throw new Exception("Database is niet geladen!");
                var tb = new TextFieldEditor();
                tb.FieldImage = Resources.search_page_document_128x128;
                tb.MultiLine = false;
                tb.Title = "Zoek Productie";
                tb.EnableSecondaryField = true;
                tb.SecondaryDescription = "Vul in een Artikelnr, bewerking naam of een omschrijving";
                if (tb.ShowDialog() == DialogResult.OK)
                {
                    if (tb.UseSecondary)
                    {
                        var calcform = new RangeCalculatorForm();
                        var rf = new ZoekProductiesUI.RangeFilter
                        {
                            Enabled = true,
                            Criteria = tb.SecondaryText.Trim()
                        };
                        calcform.Filter = rf;
                        calcform.Show();
                    }
                    else
                    {
                        var xsel = tb.SelectedText.Trim();
                        var prod = Manager.Database.GetProductie(xsel);
                        if (prod == null)
                            throw new Exception($"Er is geen productie gevonden met '{xsel}'.");
                        var bw = prod.Bewerkingen?.FirstOrDefault(x => x.IsAllowed());
                        Manager.FormulierActie(new object[] {prod, bw}, MainAktie.OpenProductie);
                    }
                }
            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var e = new KeyEventArgs(keyData);

            if (e.Control && e.KeyCode == Keys.F)

            {
                xsearchprodnr_Click(null, EventArgs.Empty);
                return true; // handled
            }

            if (e.Control && e.KeyCode == Keys.T)
            {
                mainMenu1.PressButton("xSearchTekening");
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void xopennewlijst_Click(object sender, EventArgs e)
        {
            ShowProductieLijstForm(this);
        }

        private void xtoonartikels_Click(object sender, EventArgs e)
        {
            ShowArtikelenWindow();
            //new ArtikelsForm().ShowDialog();
            //if (Manager.Opties != null)
            //    xbewerkingListControl.ProductieLijst.RestoreState(Manager.Opties.ViewDataBewerkingenState);
        }

        private void xopmerkingentoolstripbutton_Click(object sender, EventArgs e)
        {
            ShowOpmerkingWindow();
        }

        private void xShowPreview_Click(object sender, EventArgs e)
        {
            try
            {
                CheckForPreview(true, false);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xHelpButton_Click(object sender, EventArgs e)
        {
            var xhelp = new UpdatePreviewForm(Manager.HelpDropUrl, false, true);
            xhelp.Title = "ProductieManager HelpDesk";
            if (xhelp.IsValid)
            {
                xhelp.ShowDialog();
            }
            else
            {
                XMessageBox.Show(this, $"HelpDesk is tijdelijk niet beschikbaar", "Niet Beschikbaar",
                    MessageBoxIcon.Exclamation);
            }
        }

        private void xcorruptedfilesbutton_Click(object sender, EventArgs e)
        {
            var xlist = MultipleFileDb.CorruptedFilePaths;
            var xweergave = new LijstWeergaveForm();
            xweergave.AllowAddItem = false;
            xweergave.AllowOpslaan = false;
            xweergave.ViewedData = xlist.ToArray();

            MultipleFileDb.CorruptedFilesChanged += xweergave.OnItemsChanged;

            xweergave.ItemRemoved += (x, y) =>
            {

                if (x is string[] items)
                {
                    int deleted = 0;
                    foreach (var item in items)
                    {
                        var itemname = Path.GetFileNameWithoutExtension(item);
                        var xdir = Path.GetDirectoryName(item);
                        var xdirname = Path.GetFileName(xdir);
                        var xdel = Manager.Database.RemoveFromCollection(xdirname, new string[] {itemname}).Result;
                        if(xdel > 0)
                            MultipleFileDb.CorruptedFilePaths.Remove(item);
                        deleted += xdel;
                    }

                    if (deleted > 0)
                    {
                        MultipleFileDb.OnCorruptedFilesChanged();
                    }
                }
            };
            xweergave.ShowDialog();
            MultipleFileDb.CorruptedFilesChanged -= xweergave.OnItemsChanged;
            xweergave.Dispose();
        }

        private bool _xtekeningbusy;
        private void xMissingTekening_Click(object sender, EventArgs e)
        {
            if (_xtekeningbusy) return;
            _xtekeningbusy = true;
            var xloading = new LoadingForm();
            xloading.StartPosition = FormStartPosition.CenterParent;
            var progarg = xloading.Arg;
            progarg.Message = "Producties Laden...";
            progarg.OnChanged(this);
            var wb = new WebBrowserForm();
            wb.ShowErrorMessage = false;
            wb.Arg = progarg;
            wb.FilesFormatToOpen = new string[] { "{0}_fbr.pdf" };
            wb.ShowNotFoundList = true;
            wb.StopNavigatingAfterError = false;
            Task.Factory.StartNew(async () =>
            {
               
                var prods = await Manager.Database.GetBewerkingenInArtnrSections(true, false);
                progarg.Max = prods.Count;
                progarg.OnChanged(this);
                if (progarg.IsCanceled) return;
                var arts = prods.Select(x => x.Key).ToArray();
                //Process.Start(xtek); 
                
                wb.FilesToOpen.AddRange(arts);
                wb.BeginInvoke(new MethodInvoker(() => wb.Navigate()));
                //this.BeginInvoke(new MethodInvoker(()=> wb.ShowDialog()));
                //this.Invoke(new MethodInvoker(() => { xloading.BringToFront();
                //    xloading.Focus();
                //}));
            });
           
            wb.Show();
            xloading.ShowDialog();
            wb.Close();
            this.BringToFront();
            this.Focus();
            _xtekeningbusy = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var xdraw = new DrawArrow(this, Direction.Top, true, this.PointToScreen(xToolButtons.Location));
            xdraw.Draw();
            var xdraw1 = new DrawArrow(this, Direction.Left, true,this.PointToScreen(mainMenu1.Location));
            xdraw1.Draw();
            //var xdraw = new DrawArrow(this, Direction.Top, true, this.PointToScreen(xToolButtons.Location));
            //xdraw.Draw();
            //var xdraw = new DrawArrow(this, Direction.Top, true, this.PointToScreen(xToolButtons.Location));
            //xdraw.Draw();
        }

        private Point FindLocation(Control ctrl, bool height)
        {
            if (ctrl.Parent is Form)
                return new Point(ctrl.Location.X + (height ? 0 : ctrl.Width),
                    ctrl.Location.Y + (height ? ctrl.Height : 0));

            Point p = FindLocation(ctrl.Parent, height);
            p.X += ctrl.Location.X;
            p.Y += ctrl.Location.Y;
            return p;
        }

        private void xklachten_Click(object sender, EventArgs e)
        {
            ShowKlachtenWindow();
        }

        private void xverpakkingen_Click(object sender, EventArgs e)
        {
            var vr = new VerpakkingenForm();
            vr.ShowDialog();
        }

        private void xMaakWeekOverzichtToolstrip_Click(object sender, EventArgs e)
        {
            ShowCreateWeekOverzicht();
        }
        #endregion Menu Button Events

        #region Taken Lijst

        private async void takenManager1_OnTaakUitvoeren(Taak taak)
        {
            var save = false;
            switch (taak.Type)
            {
                case AktieType.ControleCheck:
                    if (taak.Bewerking != null)
                    {
                        var xui = new AantalGemaaktUI();
                        if (xui.ShowDialog(taak.Formulier, taak.Bewerking, taak.Plek) == DialogResult.OK)
                            //taak.Update();
                            save = false;
                    }

                    break;

                case AktieType.Beginnen:
                    if (taak.Formulier != null)
                    {
                        var p = taak.Formulier;
                        if (p.State != ProductieState.Verwijderd && p.State != ProductieState.Gereed)
                            ShowProductieForm(this,p, true, taak.Bewerking);
                    }

                    break;

                case AktieType.GereedMelden:
                    if (taak.Formulier != null)
                    {
                        var p = taak.Formulier;
                        if (p.State != ProductieState.Verwijderd && p.State != ProductieState.Gereed)
                            ProductieListControl.MeldGereed(this, p);
                        //taak.Update();
                    }

                    break;

                case AktieType.BewerkingGereed:
                    if (taak.Bewerking != null)
                    {
                        var b = taak.Bewerking;
                        if (b.State != ProductieState.Verwijderd && b.State != ProductieState.Gereed)
                            ProductieListControl.MeldBewerkingGereed(this, b);
                        //taak.Update();
                    }

                    break;

                case AktieType.KlaarZetten:
                    if (taak.Formulier != null)
                    {
                        var p = taak.Formulier;
                        var matform = new MateriaalForm();
                        if (matform.ShowDialog(p) == DialogResult.OK)
                        {
                            taak.Formulier = matform.Formulier;
                            save = true;
                        }
                    }

                    break;

                case AktieType.Stoppen:
                    break;

                case AktieType.PersoneelChange:
                    if (taak.Bewerking != null)
                    {
                        var ind = new Indeling(taak.Formulier, taak.Bewerking);
                        ind.StartPosition = FormStartPosition.CenterParent;
                        if (ind.ShowDialog() == DialogResult.OK)
                        {
                            save = false;
                        }//taak.Update();
                           
                    }

                    break;

                case AktieType.Telaat:
                    if (taak.Bewerking != null)
                    {
                        var dt = new DatumChanger();
                        if (dt.ShowDialog(taak.Bewerking.LeverDatum,
                                $"Wijzig leverdatum voor ({taak.Bewerking.Path}) {taak.Bewerking.Omschrijving}.") ==
                            DialogResult.OK)
                        {
                            taak.Formulier = Manager.Database.GetProductie(taak.Bewerking.ProductieNr);
                            taak.Bewerking = taak.Formulier.Bewerkingen?.FirstOrDefault(x =>
                                string.Equals(x.Naam, taak.Bewerking.Naam, StringComparison.CurrentCultureIgnoreCase));

                            if (taak.Bewerking != null)
                            {
                                taak.Bewerking.LeverDatum = dt.SelectedValue;
                                save = true;
                            }
                        }

                        dt.Dispose();
                    }
                    else if (taak.Formulier != null)
                    {
                        var dt = new DatumChanger();
                        if (dt.ShowDialog(taak.Formulier.LeverDatum,
                            $"Wijzig leverdatum voor {taak.Formulier.Omschrijving}.") == DialogResult.OK)
                        {
                            taak.Formulier = Manager.Database.GetProductie(taak.Formulier.ProductieNr);
                            if (taak.Formulier.Bewerkingen.Length > 0)
                                taak.Formulier.Bewerkingen[taak.Formulier.Bewerkingen.Length - 1].LeverDatum =
                                    dt.SelectedValue;
                            else taak.Formulier.LeverDatum = dt.SelectedValue;
                            save = true;
                        }

                        dt.Dispose();
                    }

                    break;

                case AktieType.PersoneelVrij:
                    if (taak.Bewerking != null)
                    {
                        var ind = new Indeling(taak.Formulier, taak.Bewerking);
                        ind.StartPosition = FormStartPosition.CenterParent;
                        ind.ShowDialog();
                        // if (ind.ShowDialog() == DialogResult.OK)
                        //taak.Update();
                    }

                    break;
                case AktieType.Onderbreking:
                    if (taak.Plek != null)
                    {
                        var st = new StoringForm(taak.Plek);
                        st.ShowDialog();
                    }

                    break;
                case AktieType.None:
                    break;
            }

            if (save && taak.Formulier != null)
                await taak.Formulier.UpdateForm(true, false, null, $"[{taak.GetPath()}] Taak Uitgevoerd");
            else if (save)
                taak.Bewerking?.UpdateBewerking(null, $"[{taak.GetPath()}] Taak Uitgevoerd");
        }


        #endregion Taken Lijst

        private void xArtikelRecordsToolstripButton_Click(object sender, EventArgs e)
        {
            var artikels = new ArtikelRecordsForm();
            artikels.ShowDialog();
        }

        private void xShowDaily_Click(object sender, EventArgs e)
        {
            DailyMessage.CreateDaily(true);
        }

        private void xSporenButton_Click(object sender, EventArgs e)
        {
           new ArtikelenVerbruikForm().ShowDialog();
        }
    }
}