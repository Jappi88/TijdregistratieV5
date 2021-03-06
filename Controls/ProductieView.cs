using AutoUpdaterDotNET;
using Forms;
using Forms.Aantal;
using Forms.Activiteit;
using Forms.ArtikelRecords;
using Forms.Chat;
using Forms.Excel;
using Forms.PersoneelVerzoek;
using Forms.Sporen;
using Forms.Verzoeken;
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
using Rpm.Verzoeken;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Various;

namespace Controls
{
    public partial class ProductieView : UserControl
    {
        public ProductieView()
        {
            InitializeComponent();
            _specialRoosterWatcher = new System.Windows.Forms.Timer();
            _specialRoosterWatcher.Interval = 60000; //1 minuut;
            _specialRoosterWatcher.Tick += (_, _) => CheckForSpecialRooster(false);
            DailyMessage = new Daily
            {
                ImageList =
                {
                    ImageSize = new Size(128, 128)
                }
            };
            DailyMessage.DailyCreated += DailyMessage_DailyCreated;
        }

        public Point GetNotificationLocation(Form form)
        {
            try
            {
                if (form == null) return new Point();
                var x = Screen.PrimaryScreen.WorkingArea.Width - form.Width;
                var y = Screen.PrimaryScreen.WorkingArea.Height - form.Height;
                var index = NotificationWindows.IndexOf(form);
                for (int i = 0; i < index; i++)
                {
                    var height = NotificationWindows[i].Height;
                    if (y - height < 0)
                    {
                        x -= NotificationWindows.OrderBy(x => x.Width).Last()?.Width ?? 0;
                        y = Screen.PrimaryScreen.WorkingArea.Height - Height;
                        continue;
                    }
                    y -= height;
                }
                return new Point(x, y);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return new Point();
            }
        }

        private NewMessageForm InitMessageForm(ProductieChatEntry[] entries)
        {
            var _newmessage = NotificationWindows.OfType<NewMessageForm>().FirstOrDefault();
            if(_newmessage == null)
            {
                _newmessage = new NewMessageForm();
                _newmessage.MessageClicked += _newmessage_Click;
                _newmessage.FormClosed += _newmessage_Close;
                NotificationWindows.Add(_newmessage);
            }
            _newmessage.InitMessages(entries);
            _newmessage.Location = GetNotificationLocation(_newmessage);
            return _newmessage;
        }

        private VerzoekNotificatieForm InitVerzoekenForm(VerzoekEntry[] entries)
        {
            var _verz = NotificationWindows.OfType<VerzoekNotificatieForm>().FirstOrDefault();
            if (_verz == null)
            {
                _verz = new VerzoekNotificatieForm();
                _verz.MessageClicked += _verz_MessageClicked;
                _verz.FormClosed += _newmessage_Close;
                NotificationWindows.Add(_verz);
            }
            _verz.InitVerzoeken(entries);
            _verz.Location = GetNotificationLocation(_verz);
            _verz.StartPosition = FormStartPosition.Manual;
            return _verz;
        }

        private void _verz_MessageClicked(object sender, EventArgs e)
        {
            if(sender is VerzoekNotificatieForm verz)
            {
                verz.Close();
               var result = XMessageBox.Show(this, $"{verz.MessageText}", verz.TitleLabel.Text, MessageBoxButtons.OK, Resources.transfer_man_128x128, MetroColorStyle.Purple);
                if (result == DialogResult.OK && Manager.Verzoeken?.Database != null && Manager.Verzoeken.Database.CanRead)
                {
                    for (int i = 0; i < verz.Verzoeken.Length; i++)
                    {
                        var vr = verz.Verzoeken[i];
                        if (!vr.IsRead())
                        {
                            vr.SetRead(true);
                            Manager.Verzoeken.UpdateVerzoek(vr);
                        }
                    }
                }
            }
        }

        private void _newmessage_Close(object sender, EventArgs e)
        {
            if(sender is Form form)
            {
                form.Dispose();
                NotificationWindows.RemoveAll(x => x.Equals(form));
            }
        }

        private void _newmessage_Click(object sender, EventArgs e)
        {
            if (sender is NewMessageForm _newmessage)
            {
                var names = new List<string>();
                var unread = _newmessage.Messages;
                foreach (var msg in unread.Where(msg => msg.Afzender != null && !names.Any(x =>
                    string.Equals(x, msg.Afzender.UserName, StringComparison.CurrentCultureIgnoreCase))))
                    names.Add(msg.Afzender.UserName);
                bool iedereen = names.Count == 0 || unread.Any(x =>
                            string.Equals(x.Ontvanger, "iedereen", StringComparison.CurrentCultureIgnoreCase));
                _newmessage.Close();
                var xtile = tileMainView1.GetTile("xchat");
                if ((_chatform == null || _chatform.IsDisposed))
                {
                    if (xtile is { Tag: TileInfoEntry entry })
                    {
                        InitChatTab(entry, true, iedereen ? "Iedereen" : names[0]);
                        return;
                    }
                }
                ShowChatWindow(this, iedereen ? "Iedereen" : names[0]);
            }
        }

        private void DailyMessage_DailyCreated(object sender, EventArgs e)
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(ShowDaily));
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
                    xf.ShowDialog(this);
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

        private readonly System.Windows.Forms.Timer _specialRoosterWatcher;
        public static Manager _manager;
        private List<Form> NotificationWindows { get; set; } = new List<Form>();

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
        private static ActiviteitForm _ActiviteitForm;
        private static PersoneelIndelingForm _PersoneelIndeling;
        private static WerkplaatsIndelingForm _WerkplaatsIndeling;
        private static MetroForm _berekenverbruik;

        private readonly Daily DailyMessage;
        public bool ShowUnreadMessage { get; set; }

        // [NonSerialized] private Opties _opties;

        //private string[] _groups = null;

        #endregion Variables

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
            xprodlist.BackColor = Color.White;
            xprodlist.Dock = DockStyle.Fill;
            xprodlist.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            xprodlist.OnPlekkenChanged += (_, _) =>
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
            xprodlist.LoadPlekken();
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
            xprodlist.BackColor = Color.White;
            xprodlist.Dock = DockStyle.Fill;
            xprodlist.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            xprodlist.ItemCountChanged += (_, _) =>
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

        private bool CheckTab(TileInfoEntry entry, bool select, out MetroTabPage page)
        {
            page = metroCustomTabControl1.TabPages.Cast<MetroTabPage>()
                .FirstOrDefault(x => x.Tag is TileInfoEntry xent && entry.Equals(xent));
            if (page != null)
            {
                if (select)
                {
                    metroCustomTabControl1.SelectedTab = page;
                    page.PerformLayout();
                }
                return true;
            }

            return false;
        }

        private bool CheckTab(TileInfoEntry entry, bool select)
        {
            return CheckTab(entry, select, out _);
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
                xprodlist.BackColor = Color.White;
                xprodlist.CanLoad = true;
                xprodlist.Dock = DockStyle.Fill;
                xprodlist.EnableEntryFiltering = true;
                xprodlist.EnableFiltering = true;
                xprodlist.EnableSync = false;
                xprodlist.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
                xprodlist.IsBewerkingView = true;
                xprodlist.ListName = entry.Name;
                xprodlist.ItemCountChanged += (_, _) =>
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
                xprodlist.InitProductie(true, true, true, true, true, true);
                xprodlist.InitEvents();
                UpdateTileViewed(entry, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void CheckProductieFilterTab()
        {
            try
            {
                var tabs = metroCustomTabControl1.TabPages.Cast<MetroTabPage>().Where(x=> x.Tag is TileInfoEntry entry && entry.LinkID != -1).ToList();
                if(tabs.Count > 0)
                {
                    for(int i = 0; i < tabs.Count; i++)
                    {
                        var tab = tabs[i];
                        if(tab.Tag is TileInfoEntry ent && Manager.Opties.Filters.Any(x => x.ID == ent.LinkID && !x.ListNames.Contains(ent.Name)))
                        {
                            metroCustomTabControl1.CloseTab(tab);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                xprodlist.BackColor = Color.White;
                xprodlist.CanLoad = true;
                xprodlist.Dock = DockStyle.Fill;
                xprodlist.EnableEntryFiltering = true;
                xprodlist.EnableFiltering = true;
                xprodlist.EnableSync = false;
                xprodlist.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
                xprodlist.IsBewerkingView = true;
                xprodlist.ListName = entry.Name;
                if (xfilter.ListNames.IndexOf(entry.Name) == -1)
                    xfilter.ListNames.Add(entry.Name);
                xprodlist.ItemCountChanged += (_, _) =>
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
            xprodlist.BackColor = Color.White;
            xprodlist.Dock = DockStyle.Fill;
            xprodlist.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            xprodlist.Name = entry.Name;
            xprodlist.ClosedClicked += (_, _) =>
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
            xprodlist.BackColor = Color.White;
            xprodlist.Dock = DockStyle.Fill;
            xprodlist.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            xprodlist.Name = entry.Name;
            xprodlist.CloseClicked += (_, _) =>
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
            xprodlist.BackColor = Color.White;
            xprodlist.Dock = DockStyle.Fill;
            xprodlist.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            xprodlist.Name = entry.Name;
            xprodlist.CloseClicked += (_, _) =>
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
            xprodlist.BackColor = Color.White;
            xprodlist.Dock = DockStyle.Fill;
            xprodlist.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
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
            xprodlist.BackColor = Color.White;
            xprodlist.Dock = DockStyle.Fill;
            xprodlist.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
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
            xprodlist.BackColor = Color.White;
            xprodlist.Dock = DockStyle.Fill;
            xprodlist.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
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
            xprodlist.BackColor = Color.White;
            xprodlist.Dock = DockStyle.Fill;
            xprodlist.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
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
            xprodlist.BackColor = Color.White;
            xprodlist.Dock = DockStyle.Fill;
            xprodlist.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            xprodlist.Name = entry.Name;
            xprodlist.CloseClicked += (_, _) =>
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
            xprodlist.BackColor = Color.White;
            xprodlist.Dock = DockStyle.Fill;
            xprodlist.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            xprodlist.Name = entry.Name;
            xprodlist.CloseClicked += (_, _) =>
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
            xprodlist.BackColor = Color.White;
            xprodlist.Dock = DockStyle.Fill;
            xprodlist.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
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
            xprodlist.BackColor = Color.White;
            xprodlist.Dock = DockStyle.Fill;
            xprodlist.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
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
            xprodlist.BackColor = Color.White;
            xprodlist.Dock = DockStyle.Fill;
            xprodlist.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
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
            if (CheckTab(entry, select, out var page))
            {
                if (page != null && !string.IsNullOrEmpty(username))
                {
                    var xpage = page.Controls.OfType<ProductieChatUI>().FirstOrDefault();
                    if (xpage != null)
                    {
                        xpage.SetSelected(username);
                        metroCustomTabControl1.SelectedTab = page;

                    }
                }
                return;
            }
            var xtabpage = new MetroTabPage();
            xtabpage.Padding = new Padding(5);
            xtabpage.Text = entry.Text + "    ";
            xtabpage.Tag = entry;
            var xprodlist = new ProductieChatUI();
            xprodlist._Selected = username;
            xprodlist.AutoScroll = true;
            xprodlist.BackColor = Color.White;
            xprodlist.Dock = DockStyle.Fill;
            xprodlist.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            xprodlist.Name = entry.Name;



            if (!xprodlist.InitUI())
            {
                xprodlist.Dispose();
                //metroCustomTabControl1.TabPages.Remove(xtabpage);
                //UpdateTileViewed(entry, false);
                // metroCustomTabControl1.CloseTab(xtabpage);
            }
            else
            {
                xtabpage.Controls.Add(xprodlist);
                metroCustomTabControl1.SuspendLayout();
                metroCustomTabControl1.TabPages.Add(xtabpage);
                metroCustomTabControl1.ResumeLayout(false);
                if (select)
                    metroCustomTabControl1.SelectedTab = xtabpage;
                Invalidate();
                Application.DoEvents();
                UpdateTileViewed(entry, true);
                xprodlist.SetSelected(username);
            }
        }

        private void CloseTabPage(object sender, bool updateview)
        {
            if (sender is MetroTabPage page && page.Controls.Count > 0)
            {
                var xcontrol = page.Controls.Cast<Control>().FirstOrDefault(x=> x is not MetroScrollBar);
                if (xcontrol is ProductieListControl xprod)
                {
                    xprod.CloseUI();
                }
                else if (xcontrol is RecentGereedMeldingenUI gereed)
                {
                    gereed.DetachEvents();
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
                {
                    _LastSelectedTab.RemoveAll(x => string.Equals(x, info.Name, StringComparison.CurrentCultureIgnoreCase));
                    UpdateTileViewed(info, false);
                    string xlast = _LastSelectedTab.LastOrDefault();
                    metroCustomTabControl1.SelectedTab = metroCustomTabControl1.TabPages.OfType<MetroTabPage>().FirstOrDefault(x => x.Tag is TileInfoEntry ent && string.Equals(ent.Name, xlast, StringComparison.CurrentCultureIgnoreCase));
                }
                xcontrol?.Dispose();
                if (metroCustomTabControl1.SelectedTab == null)
                    metroCustomTabControl1.SelectedIndex = 0;
                metroCustomTabControl1.Invalidate();
            }
        }

        public void UpdateTileViewed(TileInfoEntry entry, bool isviewed)
        {
            try
            {
                if (entry == null) return;
                string lastviewed = (metroCustomTabControl1.SelectedTab?.Tag as TileInfoEntry)?.Name;
                if (Manager.Opties?.TileLayout != null)
                {
                    var xindex = Manager.Opties.TileLayout.IndexOf(entry);
                    if (xindex > -1)
                        Manager.Opties.TileLayout[xindex] = entry;
                }
                entry.IsViewed = isviewed;

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
                if (xf.ShowDialog(this) == DialogResult.OK)
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

        private void metroCustomTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Manager.Opties != null)
            {
                Manager.Opties.LastShownTabName = (metroCustomTabControl1.SelectedTab?.Tag as TileInfoEntry)?.Name;
                if (!string.IsNullOrEmpty(Manager.Opties.LastShownTabName))
                    _LastSelectedTab.Add(Manager.Opties.LastShownTabName);
            }
        }

        private List<string> _LastSelectedTab = new List<string>();

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
                        ShowOnderbrekeningenWidow(this);
                        //InitAlleStoringenUITab(entry, select);
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
                    //var l = new LoadingForm();
                    //l.CloseIfFinished = true;
                    //l.ShowCloseButton = false;
                    //var arg = l.Arg;
                    //arg.Message = "Een moment a.u.b...";
                    //arg.OnChanged(this);
                    //l.ShowDialogAsync(this.ParentForm);
                    metroCustomTabControl1.SuspendLayout();
                    try
                    {

                        var xtiles = viewer.GetAllTiles().Where(x => x.Tag is TileInfoEntry { IsViewed: true }).ToList();

                        foreach (var xt in xtiles)
                        {
                            if (xt.Tag is TileInfoEntry entry)
                            {
                                ShowTileTabPage(entry, false);
                                //Application.DoEvents();
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                    //arg.Type = ProgressType.ReadCompleet;
                    //arg.OnChanged(this);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally { metroCustomTabControl1.ResumeLayout(false); }
        }

        #endregion Tab Buttons

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
                //_manager.InitManager();
                takenManager1.InitManager();
                InitEvents();
                _manager.Load(path, autologin, true, true);
                // _manager.StartMonitor();
                FormLoaded();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void InitChatEvents()
        {
            if (Manager.ProductieChat != null)
            {
                Manager.ProductieChat.MessageChanged -= ProductieChat_Updated;
                Manager.ProductieChat.MessageDeleted -= ProductieChat_Updated;
                Manager.ProductieChat.PublicMessageChanged -= ProductieChat_Updated;
                Manager.ProductieChat.PublicMessageDeleted -= ProductieChat_Updated;
                Manager.ProductieChat.GebruikerChanged -= ProductieChat_Updated;
                Manager.ProductieChat.GebruikerDeleted -= ProductieChat_Updated;
                Manager.ProductieChat.MessageChanged += ProductieChat_Updated;
                Manager.ProductieChat.MessageDeleted += ProductieChat_Updated;
                Manager.ProductieChat.PublicMessageChanged += ProductieChat_Updated;
                Manager.ProductieChat.PublicMessageDeleted += ProductieChat_Updated;
                Manager.ProductieChat.GebruikerChanged += ProductieChat_Updated;
                Manager.ProductieChat.GebruikerDeleted += ProductieChat_Updated;
            }
        }

        public void LoadManager(string path, bool disposeOld, bool autologin = true)
        {
            InitManager(path, autologin, disposeOld);
        }

        public void ShowStartupForms()
        {
            try
            {
                if (Disposing || IsDisposed) return;
                BeginInvoke(new Action(() =>
                {
                    try
                    {
                        //CheckForSyncDatabase();
                        //CheckForUpdateDatabase();
                        CheckForPreview(this, false, true);
                        if (Manager.LogedInGebruiker != null)
                        {
                            CheckForSpecialRooster(true);
                            LoadStartedProducties();
                            //LoadProductieLogs();
                            //RunProductieRefresh();
                            UpdateKlachtButton();
                            UpdateVerpakkingenButton();
                            Manager.ArtikelRecords?.CheckForOpmerkingen(true);
                            InitDBCorupptedMonitor();
                            DailyMessage.CreateDaily();
                            UpdateUnreadMessages();
                            UpdateUnreadVerzoeken();
                            UpdateUnreadOpmerkingen();
                            if (Manager.Opties is { ToonPersoneelIndelingNaOpstart: true })
                                ShowPersoneelIndelingWindow(this);
                            if (Manager.Opties is { ToonWerkplaatsIndelingNaOpstart: true })
                                ShowWerkplaatsIndelingWindow();
                        }
                        else
                        {
                            var xlogin = new LogIn();
                            xlogin.StartPosition = FormStartPosition.CenterParent;
                            xlogin.ShowDialog(this);
                        }
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
            //Manager.OnSettingsChanging += Manager_OnSettingsChanging;
            Manager.OnSettingsChanged += _manager_OnSettingsChanged;
            //Manager.OnProductiesLoaded += Manager_OnProductiesChanged;
            Manager.OnLoginChanged += _manager_OnLoginChanged;
            Manager.OnFormulierActie += Manager_OnFormulierActie;
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            //Manager.DbUpdater.DbEntryUpdated += DbUpdater_DbEntryUpdated;
            //Manager.OnDbBeginUpdate += Manager_OnDbBeginUpdate;
            //Manager.OnDbEndUpdate += Manager_OnDbEndUpdate;
            Manager.OnManagerLoading += Manager_OnManagerLoading;
            Manager.OnManagerLoaded += _manager_OnManagerLoaded;
            Manager.OpmerkingenChanged += Opmerkingen_OnOpmerkingenChanged;

            MultipleFileDb.CorruptedFilesChanged += MultipleFileDb_CorruptedFilesChanged;
            Manager.FilterChanged += Manager_FilterChanged;

            Manager.KlachtChanged += Klachten_KlachtChanged;
            Manager.KlachtDeleted += Klachten_KlachtChanged;

            Manager.VerpakkingChanged += Manager_VerpakkingChanged; 
            Manager.VerpakkingDeleted += Manager_VerpakkingDeleted;

            Manager.VerzoekChanged += Manager_VerzoekChanged;
            Manager.VerzoekDeleted += Manager_VerzoekChanged;

            Manager.RequestRespondDialog += Manager_RequestRespondDialog;

            _manager.OnShutdown += _manager_OnShutdown;
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            if(Disposing || IsDisposed) return;
            try
            {
                 CheckForShared(changedform);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void ShowProductie(IWin32Window owner, Bewerking bew, bool playsound, int selectedtab)
        {
            try
            {
                if (bew == null) return;
                if (Manager.Opties != null)
                    playsound &= Manager.Opties.SpeelMeldingToonAf;
                if (playsound)
                {
                    Task.Factory.StartNew(() =>
                    {
                        using var soundPlayer = new SoundPlayer(Resources.mixkit_alert_bells_echo_765);
                        soundPlayer.Play();
                    });
                }

                if (bew.IsAllowed())
                {
                    var xprod = ShowProductieForm(owner, bew.Parent, true, bew);
                    if (xprod != null)
                        xprod.TabControl.SelectedIndex = selectedtab;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void ShowProductie(IWin32Window owner, ProductieFormulier form, Bewerking bew, bool playsound, int selectedtab)
        {
            try
            {
                if (form == null) return;
                if (Manager.Opties != null)
                    playsound &= Manager.Opties.SpeelMeldingToonAf;
                if (playsound)
                {
                    Task.Factory.StartNew(() =>
                    {
                        using var soundPlayer = new SoundPlayer(Resources.mixkit_alert_bells_echo_765);
                        soundPlayer.Play();
                    });
                }

                if (form.IsAllowed(null))
                {
                    var xprod = ShowProductieForm(owner, form, true, bew);
                    if (xprod != null)
                        xprod.TabControl.SelectedIndex = selectedtab;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void CheckForShared(ProductieFormulier form)
        {
            try
            {
                if (form?.Bewerkingen == null || Manager.Opties == null) return;
                if (form.LastChanged == null) return;
                if (form.LastChanged.ReadBy.Any(x => string.Equals(x, Manager.Opties.Username,
                        StringComparison.CurrentCultureIgnoreCase))) return;
                if (string.Equals(form.LastChanged.User, Manager.Opties.Username,
                        StringComparison.CurrentCultureIgnoreCase)) return;
                var bw = form.Bewerkingen.FirstOrDefault(x => x.SharedUsers.Any(s =>
                    string.Equals(s, Manager.Opties.Username, StringComparison.CurrentCultureIgnoreCase)));
                if (bw == null) return;
                if (InvokeRequired)
                    this.Invoke(new Action(() => ShowProductie(this, bw, true, 10)));
                else
                    ShowProductie(this, bw, true, 10);
                form.UpdateForm(true, false, null, "", true, false, false);
                Manager.RemoteMessage(form.LastChanged.CreateMessage(DbType.Producties));

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void Manager_OnManagerLoading(ref bool cancel)
        {
            if (Disposing || IsDisposed) return;
            try
            {
                BeginInvoke(new Action(CloseAllTabs));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Manager_FilterChanged(object sender, EventArgs e)
        {
            if (tileMainView1.InvokeRequired)
            {

                tileMainView1.Invoke(new MethodInvoker(() =>
                {
                    tileMainView1.UpdateFilterTiles();
                    CheckProductieFilterTab();
                }));
            }
            else
            {
                tileMainView1.UpdateFilterTiles();
                CheckProductieFilterTab();
            }
        }

        private DialogResult Manager_RequestRespondDialog(object sender, string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon, string[] chooseitems = null, Dictionary<string, DialogResult> custombuttons = null, Image customImage = null, MetroColorStyle style = MetroColorStyle.Default)
        {
            var xret = DialogResult.Cancel;
            if (InvokeRequired)
                Invoke(new MethodInvoker(() =>
                    xret = DoOpmerking(message, title, buttons, icon, chooseitems, custombuttons, customImage, style)));
            else xret = DoOpmerking(message, title, buttons, icon, chooseitems, custombuttons, customImage, style);
            return xret;
        }

        private DialogResult DoOpmerking(string message, string title, MessageBoxButtons buttons,
            MessageBoxIcon icon, string[] chooseitems = null, Dictionary<string, DialogResult> custombuttons = null,
            Image customImage = null, MetroColorStyle style = MetroColorStyle.Default)
        {
            var xmsg = new XMessageBox();
            xmsg.StartPosition = FormStartPosition.CenterParent;
            return xmsg.ShowDialog(this,message, title, buttons, icon, chooseitems, custombuttons, customImage, style);
        }

        public void DetachEvents()
        {
            //Manager.OnSettingsChanging -= Manager_OnSettingsChanging;
            Manager.OnSettingsChanged -= _manager_OnSettingsChanged;
            Manager.OnFormulierActie -= Manager_OnFormulierActie;
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            // Manager.OnProductiesLoaded -= Manager_OnProductiesChanged;
            //Manager.DbUpdater.DbEntryUpdated -= DbUpdater_DbEntryUpdated;
            Manager.OnLoginChanged -= _manager_OnLoginChanged;
            Manager.OpmerkingenChanged -= Opmerkingen_OnOpmerkingenChanged;
            Manager.RequestRespondDialog -= Manager_RequestRespondDialog;
            if (Manager.ProductieChat != null)
            {
                Manager.ProductieChat.MessageChanged -= ProductieChat_Updated;
                Manager.ProductieChat.MessageDeleted -= ProductieChat_Updated;
                Manager.ProductieChat.PublicMessageChanged -= ProductieChat_Updated;
                Manager.ProductieChat.PublicMessageDeleted -= ProductieChat_Updated;
                Manager.ProductieChat.GebruikerChanged -= ProductieChat_Updated;
                Manager.ProductieChat.GebruikerDeleted -= ProductieChat_Updated;
            }
            MultipleFileDb.CorruptedFilesChanged -= MultipleFileDb_CorruptedFilesChanged;
            Manager.FilterChanged -= Manager_FilterChanged;
            //Manager.OnDbBeginUpdate -= Manager_OnDbBeginUpdate;
            //Manager.OnDbEndUpdate -= Manager_OnDbEndUpdate;
            Manager.OnManagerLoaded -= _manager_OnManagerLoaded;
            Manager.OnManagerLoading -= Manager_OnManagerLoading;

            Manager.KlachtChanged -= Klachten_KlachtChanged;
            Manager.KlachtDeleted -= Klachten_KlachtChanged;

            Manager.VerpakkingChanged -= Manager_VerpakkingChanged;
            Manager.VerpakkingDeleted -= Manager_VerpakkingDeleted;

            Manager.VerzoekChanged -= Manager_VerzoekChanged;
            Manager.VerzoekDeleted -= Manager_VerzoekChanged;
            if (_manager != null)
                _manager.OnShutdown -= _manager_OnShutdown;
        }

        private void Manager_VerzoekChanged(object sender, FileSystemEventArgs e)
        {
            UpdateUnreadVerzoeken();
        }

        public void CloseUI()
        {
            try
            {
                DetachEvents();
                //SaveLayouts();
                CloseAllTabs();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void CloseAllTabs()
        {
            SaveLayouts();
            var sel = (metroCustomTabControl1.SelectedTab?.Tag as TileInfoEntry)?.Name;
            for (int i = 0; i < metroCustomTabControl1.TabCount; i++)
            {
                var xtab = metroCustomTabControl1.TabPages[i];
                if (xtab.Tag is TileInfoEntry)
                {
                    CloseTabPage(xtab, false);
                    metroCustomTabControl1.TabPages.Remove(xtab);
                    i--;
                }
            }
            if (Manager.Opties != null)
                Manager.Opties.LastShownTabName = sel;
        }

        public void SaveLayouts()
        {
            try
            {
                if (Manager.Opties == null) return;
                tileMainView1.SaveLayout(true);
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
            if (InvokeRequired)
                Invoke(new MethodInvoker(xUpdateKlachtButton));
            else xUpdateKlachtButton();
        }

        private void UpdateVerpakkingenButton()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(xUpdateVerpakkingenButton));
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
                                         "Nu bekijken?", $"Nieuwe {x1}", MessageBoxButtons.YesNo, Resources.Leave_80_icon_icons_com_57305_128x128, MetroColorStyle.Red) == DialogResult.Yes)
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
                if (InvokeRequired)
                    Invoke(new MethodInvoker(UpdateCorruptedButtonImage));
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

        private void ProductieChat_Updated(object item, FileSystemEventArgs e)
        {
            UpdateUnreadMessages();
        }

        private DialogResult _manager_OnShutdown(Manager instance, ref TimeSpan verlengtijd)
        {
            var af = new AfsluitPromp();
            var res = af.ShowDialog(this);
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
                        ShowUnreadMessage = Manager.Opties?.ToonNieweChatBerichtMelding ?? false;
                        tileMainView1.SetBackgroundImage(Manager.Opties?.BackgroundImagePath);
                        if (init)
                            tileMainView1.LoadTileViewer();
                        if (!string.IsNullOrEmpty(Manager.Opties?.LastShownTabName))
                            metroCustomTabControl1.SelectedTab = metroCustomTabControl1.TabPages?.Cast<MetroTabPage>()
                                .FirstOrDefault(x => x.Tag is TileInfoEntry entry && string.Equals(entry.Name,
                                    Manager.Opties.LastShownTabName, StringComparison.CurrentCultureIgnoreCase));
                        if (metroCustomTabControl1.SelectedTab == null)
                            metroCustomTabControl1.SelectedIndex = 0;
                        metroCustomTabControl1.Invalidate();
                        var xrooster = mainMenu1.GetButton("xroostermenubutton");
                        if (xrooster != null)
                            xrooster.Image = Manager.Opties?.TijdelijkeRooster == null
                                ? Resources.schedule_32_32
                                : Resources.schedule_32_32.CombineImage(Resources.exclamation_warning_15590, 1.75);

                        //LoadFilter();
                        _manager.SetSettings(settings);
                        CheckForSpecialRooster(false);
                        Invalidate();
                        //Manager.Taken?.StartBeheer();
                        //if (Manager.IsLoaded)
                        //    CheckForSpecialRooster(true);
                        if (_specialRoosterWatcher is {Enabled: false})
                            _specialRoosterWatcher.Start();
                        metroCustomTabControl1.Invalidate();
                        Application.DoEvents();
                        Manager.Meldingen?.CheckForMeldingen(5000);
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
                    xVerzoeken.Visible = user is { AccesLevel: > AccesType.ProductieBasis };
                    bool flag = user == null || Manager.ProductieChat?.Chat == null || !string.Equals(Manager.ProductieChat?.Chat?.UserName, user?.Username, StringComparison.CurrentCultureIgnoreCase);
                    if (flag)
                    {
                        var p = metroCustomTabControl1.TabPages.OfType<MetroTabPage>().FirstOrDefault(x => x.Tag is TileInfoEntry ent && ent.Name.ToLower().StartsWith("xchat"));
                        if (p != null)
                            metroCustomTabControl1.CloseTab(p);
                        if (_chatform != null && !_chatform.IsDisposed)
                        {
                            _chatform.Close();
                            _chatform = null;
                        }
                    }
                    OnLoginChanged?.Invoke(user, instance);
                }));
                if (user is { AccesLevel: > AccesType.ProductieBasis })
                    UpdateUnreadVerzoeken();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void _manager_OnManagerLoaded()
        {
            if (IsDisposed || Disposing) return;
            InitChatEvents();
            BeginInvoke(new Action(() => { _specialRoosterWatcher?.Start();}));
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
            var f = Manager.ActiveForm ??this.FindForm();
            if (f.InvokeRequired)
            {
                f.Invoke(new MethodInvoker(() => DoActie(values, type)));
            }
            else
            {
                var flag = values is { Length: > 0 };
                var xforms = Application.OpenForms.OfType<Form_Alert>().ToList();
                xforms.ForEach(x =>
                {
                    //if (x.InvokeRequired)
                    //    x.BeginInvoke(new Action(x.Close));
                    //else
                    try
                    {
                        x.Close();
                    }
                    catch (Exception ex)
                    { 
                        Console.WriteLine(ex); 
                    }
                });
                switch (type)
                {
                    case MainAktie.OpenProductie:
                        if (flag)
                        {
                            var form =
                                (ProductieFormulier)values.FirstOrDefault(x => x is ProductieFormulier);
                            var bew = (Bewerking)values.FirstOrDefault(x => x is Bewerking);
                            var index = (int)(values.FirstOrDefault(x => x is int) ?? 0);
                            var playsound = (bool)(values.FirstOrDefault(x => x is bool) ?? false);
                            if (form != null)
                            {
                                var par = values.OfType<Control>().ToList().FirstOrDefault();
                                if (par?.Parent != null)
                                    par = par.Parent;
                                ShowProductie(par??f, form, bew, playsound, index);
                            }
                        }

                        break;
                    case MainAktie.OpenIndeling:
                        if (flag)
                        {
                            var form =
                                (ProductieFormulier)values.FirstOrDefault(x => x is ProductieFormulier);
                            ShowWerkplekken(this, form);
                        }

                        break;
                    case MainAktie.OpenProductieWijziging:
                        if (flag)
                        {
                            var form =
                                (ProductieFormulier)values.FirstOrDefault(x => x is ProductieFormulier);
                            ShowProductieSettings(this, form);
                        }

                        break;
                    case MainAktie.OpenInstellingen:
                        ShowOptieWidow(this);
                        break;
                    case MainAktie.OpenRangeSearcher:
                        ShowCalculatieWindow(this);
                        break;
                    case MainAktie.OpenPersoneel:
                        ShowPersoneelWindow(this);
                        break;
                    case MainAktie.OpenStoringen:
                        if (flag)
                        {
                            var bew = (Bewerking)values.FirstOrDefault(x => x is Bewerking);
                            if (bew != null)
                                ShowBewStoringen(this, bew);
                        }

                        break;
                    case MainAktie.OpenAlleStoringen:
                        ShowOnderbrekeningenWidow(this);
                        break;
                    case MainAktie.OpenVaardigheden:
                        if (flag)
                        {
                            var per = (Personeel)values.FirstOrDefault(x => x is Personeel);
                            if (per != null)
                                ShowPersoonVaardigheden(this, per);
                        }

                        break;
                    case MainAktie.OpenAlleVaardigheden:
                        ShowAlleVaardighedenWidow(this);
                        break;
                    case MainAktie.OpenAantalGemaaktProducties:
                        if (values.FirstOrDefault() is List<Bewerking> bws && values.LastOrDefault() is int mins)
                        {
                            //var form = Application.OpenForms.OfType<Control>().FirstOrDefault(x => x.Focused);
                            DoAantalGemaakt(Manager.ActiveForm??this?.FindForm(), bws, mins);
                        }
                        break;
                    case MainAktie.StartBewerking:
                        if (values.FirstOrDefault() is Bewerking bew2)
                            ProductieListControl.StartBewerkingen(this, new[] { bew2 });
                        break;
                    case MainAktie.StopBewerking:
                        if (values.FirstOrDefault() is Bewerking bew3)
                            _ = bew3.StopProductie(true, true,true);
                        break;
                    case MainAktie.OpenBijlage:
                        if (values.FirstOrDefault() is string id)
                        {
                            ShowBijlageWindow(id);
                        }
                        break;
                }
            }
        }

        private void Manager_OnFormulierActie(object[] values, MainAktie type)
        {
            if (IsDisposed || Disposing) return;
            try
            {
                DoActie(values, type);
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
            f.ShowDialog(this);
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
            var result = xnewform.ShowDialog(this);
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
            var bttns = new Dictionary<string, DialogResult>
            {
                {"Annuleren", DialogResult.Cancel},
                {"Wijzigen", DialogResult.No},
                {"Toevoegen", DialogResult.Yes},
                {"Bekijken", DialogResult.Ignore}
            };


            var res = XMessageBox.Show(this, message, "Onderbreking", MessageBoxButtons.OK, MessageBoxIcon.Question, null,
                bttns);
            if (res == DialogResult.Cancel) return;
            var prods = await Manager.GetProducties(new[] {ViewState.Gestart, ViewState.Gestopt}, true, false,null,false);
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
                                if (plek.Storingen.Any(w => !w.IsVerholpen))
                                    plekken.Add(plek);
                    });
                    if (plekken.Count == 0)
                        XMessageBox.Show(this, "Er zijn geen openstaande onderbrekeningen om te wijzigen.", "Onderbreking",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }

            if (plekken.Count > 0)
            {
                var selector = new WerkPlekChooser(plekken, null);
                if (selector.ShowDialog(this) == DialogResult.OK)
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
                                        var storing = storingen.FirstOrDefault(x =>
                                            string.Equals(x.Path, selected, StringComparison.CurrentCultureIgnoreCase));
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
                        if (storingform.ShowDialog(this) == DialogResult.OK)
                        {
                            plek.UpdateStoring(storingform.Onderbreking);
                            if (plek.Werk != null)
                            {
                                await plek.Werk?.UpdateBewerking(null,
                                    $"{storingform.Onderbreking.StoringType.FirstCharToUpper()} aangepast op {storingform.Onderbreking.Path}");
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
                        sts.ShowDialog(this);
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
                roosterform.SetRooster(Manager.Opties.TijdelijkeRooster,Manager.Opties.NationaleFeestdagen, Manager.Opties.SpecialeRoosters);
                roosterform.RoosterUI.AutoUpdateBewerkingen = true;
                if (roosterform.ShowDialog(this) == DialogResult.Cancel)
                    return;
                Manager.Opties.TijdelijkeRooster = roosterform.WerkRooster;
                Manager.Opties.SpecialeRoosters = roosterform.RoosterUI.SpecialeRoosters;
                Manager.Opties.NationaleFeestdagen = roosterform.RoosterUI.NationaleFeestdagen().ToArray();
                var thesame = xold.SameTijden(Manager.Opties?.GetWerkRooster());
                if (!thesame)
                {
                    var bws = await Manager.GetBewerkingen(new[] {ViewState.Gestart}, true,false);

                    bws = bws.Where(x => string.Equals(Manager.Opties.Username, x.GestartDoor,
                        StringComparison.CurrentCultureIgnoreCase)).ToList();

                    if (bws.Count > 0)
                    {
                        var bwselector = new WerkplekSelectorForm(bws,true);
                        bwselector.Title = "Selecteer Werkplaatsen waarvan de rooster aangepast moet worden";
                        if (bwselector.ShowDialog(this) == DialogResult.OK)
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
                    (await Manager.Database.GetAllProducties(false, true, null,false))
                    .Where(x => x.State == ProductieState.Gestart)
                    .ToArray();
                if (startedprods.Length == 0)
                {
                    XMessageBox.Show(this, "Er zijn geen gestarte producties om te openen.", "Geen Producties",
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
            var ofd = new FolderBrowserDialog
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
                xupdater.ShowDialog(this);
            }
        }

        private void DoLoadDbInstance()
        {
            try
            {
                var xdbloader = new DbSelectorForm();
                xdbloader.ShowDialog(this);
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
                                XMessageBox.Show(this, "Kan geen productie aanmaken, omdat de Database niet is geladen.", "Database niet geladen!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                                return;
                            }
                            var AddProduction = new WijzigProductie();
                            if (AddProduction.ShowDialog(this) == DialogResult.OK)
                                BeginInvoke(new MethodInvoker(() =>
                                {
                                    var form = AddProduction.Formulier;
                                    var msg = Manager.xAddProductie(form,false);
                                    Manager.RemoteMessage(msg);
                                }));

                            break;
                        case "xopenproductie":
                            if (Manager.Database?.ProductieFormulieren == null)
                            {
                                XMessageBox.Show(this, "Kan geen productie toevoegen, omdat de Database niet is geladen.", "Database niet geladen!", MessageBoxButtons.OK,
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
                            {  //BeginInvoke(new MethodInvoker(() =>
                               //{
                                var files = ofd.FileNames;
                                _ = Manager.AddProductie(files, true, false, true);
                                //}));
                            }
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
                            ShowPersoneelIndelingWindow(this);
                            break;
                        case "xwerkplaatsindeling":
                            ShowWerkplaatsIndelingWindow();
                            break;
                        case "xverbruik":
                            ShowBerekenVerbruikWindow(this);
                            break;
                        case "xberekenleverdatum":
                            ShowBerekenLeverdatum(this);
                            break;
                        case "xcreateexcel":
                            //maak een nieuwe excel aan
                            DoCreateExcel();
                            break;
                        case "xupdatedb":
                            var updater = new DbUpdater();
                            updater.ShowDialog(this);
                            break;
                        case "xlaaddb":
                            DoLoadDbInstance();
                            break;
                        case "xupdateforms":
                            DoUpdateFormsFromDirectory();
                            break;
                        case "xstats":
                            var chartform = new ViewChartForm();
                            chartform.ShowDialog(this);
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
            ShowOptieWidow(this);
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
                xlogin.ShowDialog(this);
            }
        }

        private void xupdateallform_Click(object sender, EventArgs e)
        {
            var prod = new UpdateProducties();
            prod.ShowDialog(this);
        }

        private void xmateriaalverbruikb_Click(object sender, EventArgs e)
        {
            var xmats = new MateriaalVerbruikForm();
            xmats.ShowDialog(this);
        }

        private void xVerzoeken_Click(object sender, EventArgs e)
        {
            var verz = new VerzoekForm();
            verz.ShowDialog(this);
        }

        private void xallenotities_Click(object sender, EventArgs e)
        {
            var noteform = new AlleNotitiesForm();
            noteform.ShowDialog(this);
        }

        private void xchatformbutton_Click(object sender, EventArgs e)
        {
            ShowChatWindow(this);
        }

        private void xpersoneelb_Click(object sender, EventArgs e)
        {
            ShowPersoneelWindow(this);
        }

        private void xallstoringenb_Click(object sender, EventArgs e)
        {
            ShowOnderbrekeningenWidow(this);
        }

        private void xallevaardighedenb_Click(object sender, EventArgs e)
        {
            ShowAlleVaardighedenWidow(this);
        }

        private void xprodinfob_Click(object sender, EventArgs e)
        {
            ShowCalculatieWindow(this);
        }

        private void xspeciaalroosterbutton_Click(object sender, EventArgs e)
        {
            SetSpecialeRooster();
        }

        private void xdbbewerkingen_Click(object sender, EventArgs e)
        {
            ShowBewerkingDb(this);
        }

        private void WerkPlekkenUI1_OnRequestOpenWerk(object sender, EventArgs e)
        {
            if (!(sender is Bewerking b)) return;
            if (b.Parent != null) ShowProductieForm(this,b.Parent, true, b);
        }

        private void xaboutb_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog(this);
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
        #endregion Control Events

        #region Methods

        public void ShowNotificationWindow(Form form)
        {

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
            bool isverlofdag = Manager.Opties.NationaleFeestdagen.Any(x => x.Date == DateTime.Today);
            if (isverlofdag || xtime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                //het is weekend, dus we zullen moeten kijken of er wel gewerkt wordt.
                string body = isverlofdag ? "Vandaag is een nationale feestdag" : $"{day.FirstCharToUpper()} is geen officiële werkdag";
                var xbttntxt =
                    $"Het is {day} {xtime.TimeOfDay:hh\\:mm} uur. {body}. Click hier voor de speciale rooster";
                xspeciaalroosterbutton.Text = xbttntxt;
                var rooster = Manager.Opties.SpecialeRoosters.FirstOrDefault(x => x.Vanaf.Date == xtime.Date);
                if (rooster == null && prompchange)
                {
                    var splash = Application.OpenForms["SplashScreen"];
                    splash?.Close();

                    var xday = isverlofdag ? "een nationale feestdag" : $"{day} en geen officiële werkdag";

                    var xmsg =
                        $"Het is vandaag {xday}.\n" +
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
            //var culture = new CultureInfo("nl-NL");
            //var day = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
            bool isverlofdag = Manager.Opties.NationaleFeestdagen.Any(x => x.Date == DateTime.Today);
            if (isverlofdag || xtime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                var rooster = Manager.Opties?.SpecialeRoosters?.FirstOrDefault(x => x.Vanaf.Date == DateTime.Now.Date);
               // var xreturn = rooster != null;
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
                roosterform.EnablePeriode = false;
                roosterform.RoosterUI.ShowSpecialeRoosterButton = false;
                roosterform.SetRooster(rooster,Manager.Opties?.NationaleFeestdagen, Manager.Opties?.SpecialeRoosters);
                if (roosterform.ShowDialog(this) == DialogResult.OK)
                {
                    Manager.Opties.SpecialeRoosters = roosterform.RoosterUI.SpecialeRoosters;
                    Manager.Opties.NationaleFeestdagen = roosterform.RoosterUI.NationaleFeestdagen().ToArray();
                    var newrooster = roosterform.WerkRooster;
                    var dt = DateTime.Now;
                    var tijd = roosterform.WerkRooster.StartWerkdag;
                    newrooster.Vanaf = new DateTime(dt.Year, dt.Month, dt.Day, tijd.Hours, tijd.Minutes, 0);
                    Manager.Opties.SpecialeRoosters.RemoveAll(x => x.Vanaf.Date == newrooster.Vanaf.Date);
                    Manager.Opties.SpecialeRoosters.Add(newrooster);
                    Manager.Opties.SpecialeRoosters = Manager.Opties.SpecialeRoosters.OrderBy(x => x.Vanaf).ToList();

                    var bws = Manager.xGetBewerkingen(new[] { ViewState.Gestart }, true, false);
                    bws = bws.Where(x => string.Equals(Manager.Opties.Username, x.GestartDoor,
                        StringComparison.CurrentCultureIgnoreCase)).ToList();
                    if (bws.Count > 0)
                    {
                        var bwselector = new WerkplekSelectorForm(bws,true);
                        bwselector.Title = "Selecteer Werkplaatsen waarvan de rooster aangepast moet worden";
                        if (bwselector.ShowDialog(this) == DialogResult.OK)
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
            if (sproosters.ShowDialog(this) == DialogResult.OK)
            {
               
                var acces1 = Manager.LogedInGebruiker is { AccesLevel: >= AccesType.ProductieBasis };
                if (acces1)
                {
                    Manager.Opties.SpecialeRoosters = sproosters.Roosters;
                }
                if (acces1 && sproosters.Roosters.Count > 0)
                {
                    var bws = await Manager.GetBewerkingen(new[] { ViewState.Gestart }, true, false);
                    bws = bws.Where(x => string.Equals(Manager.Opties.Username, x.GestartDoor,
                        StringComparison.CurrentCultureIgnoreCase)).ToList();
                    if (bws.Count > 0)
                    {
                        var bwselector = new WerkplekSelectorForm(bws,true);
                        bwselector.Title = "Selecteer Werkplaatsen waarvan de rooster aangepast moet worden";
                        if (bwselector.ShowDialog(this) == DialogResult.OK)
                            await Manager.UpdateGestarteProductieRoosters(bwselector.SelectedWerkplekken, null);
                    }
                }

                if (acces1)
                    _ = Manager.Opties.Save();
            }
        }

        //private void CheckForSyncDatabase()
        //{
        //    var opties = Manager.DefaultSettings ?? UserSettings.GetDefaultSettings();
        //    if (opties?.TempMainDB != null && opties.MainDB != null &&
        //        opties.TempMainDB.LastUpdated > opties.MainDB.LastUpdated && Directory.Exists(opties.MainDB.UpdatePath))
        //    {
        //        var splash = (SplashScreen) Application.OpenForms["SplashScreen"];
        //        if (splash != null)
        //        {
        //            while (splash.Visible && !splash.CanClose)
        //                Application.DoEvents();
        //            splash.Close();
        //        }

        //        opties.TempMainDB.LastUpdated = opties.MainDB.LastUpdated;
        //        var prod = new UpdateProducties(opties.TempMainDB)
        //            {CloseWhenFinished = true, ShowStop = false, StartWhenShown = true};
        //        prod.ShowDialog();
        //        if (prod.IsFinished)
        //        {
        //            opties.MainDB.LastUpdated = DateTime.Now;
        //            opties.SaveAsDefault();
        //        }
        //    }
        //}

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
                var prs = await Manager.GetAllProductieIDs(false, false);
                foreach (var v in prs)
                {
                    var prod = Manager.Database.GetProductie(v, false);
                    if (prod?.Bewerkingen == null || prod.Bewerkingen.Length == 0) continue;
                    var xs = prod.Bewerkingen.FirstOrDefault(x => x.State == ProductieState.Gestart);
                    if (xs == null) continue;
                    ShowProductieForm(this,prod, true, xs);
                }
            }
        }

       // private void LoadProductieLogs()
        //{
        //    if (_manager == null || Manager.Opties == null || !Manager.Opties.ToonProductieLogs)
        //        return;
        //    ShowProductieLogWindow();
        //}

       public void UpdateUnreadMessages()
        {
            if (InvokeRequired)
                Invoke(new Action(xUpdateUnreadMessages));
            else xUpdateUnreadMessages();
        }

        private void xUpdateUnreadMessages()
        {
            try
            {
                if (Manager.ProductieChat?.Chat == null) return;
                var unread = Manager.ProductieChat.GetAllUnreadMessages();
                var xtile = tileMainView1.GetTile("xchat");
                if (unread.Count > 0)
                {
                    var ximg = GraphicsExtensions.DrawUserCircle(new Size(32, 32), Brushes.White,
                        unread.Count.ToString(),
                        new Font("Ariel", 16, FontStyle.Bold), Color.DarkRed);
                    xchatformbutton.Image = Resources.conversation_chat_32x321.CombineImage(ximg, 1.75);

                    if (xtile is {Tag: TileInfoEntry entry})
                    {
                        entry.TileCount = Manager.ProductieChat.Gebruikers.Count(x=> x.IsOnline && x.UserName.ToLower() != "iedereen");
                        entry.SecondaryImage = ximg;
                        xtile.UpdateTile(entry);
                    }
                }
                else
                {
                    if (xtile is {Tag: TileInfoEntry entry})
                    {
                        entry.TileCount = Manager.ProductieChat.Gebruikers.Count(x => x.IsOnline && x.UserName.ToLower() != "iedereen");
                        entry.SecondaryImage = null;
                        xtile.UpdateTile(entry);
                    }
                    xchatformbutton.Image = Resources.conversation_chat_32x321;
                }
                if (Manager.Opties is not { ToonNieweChatBerichtMelding: true }) return;
                if (unread.Count > 0)
                {
                    var _newmessage = InitMessageForm(unread.ToArray());
                    if(_newmessage != null)
                    {
                        if(!_newmessage.Visible)
                        {
                            _newmessage.Show();
                        }
                        _newmessage.BringToFront();
                        var f = xViewPanel.FindForm();
                        if (f != null)
                        {
                            if (f.WindowState == FormWindowState.Minimized)
                                f.WindowState = FormWindowState.Normal;
                            f.BringToFront();
                            f.Focus();
                            f.Select();
                            if (_chatform != null && !_chatform.IsDisposed)
                            {
                                if (_chatform.WindowState == FormWindowState.Minimized)
                                    _chatform.WindowState = FormWindowState.Normal;
                                _chatform.BringToFront();
                                _chatform.Focus();
                                _chatform.Select();
                            }
                            else
                            {
                                var tb = metroCustomTabControl1.TabPages.OfType<MetroTabPage>().ToList().FirstOrDefault(x => x.Tag is TileInfoEntry ent && ent.Name.ToLower().StartsWith("xchat"));
                                if (tb != null)
                                {
                                    metroCustomTabControl1.SelectedTab = tb;
                                    //var xch = tb.Controls.OfType<ProductieChatUI>().FirstOrDefault();
                                    //if (xch != null)
                                    //{
                                    //    xch.SetSelected(unread?.FirstOrDefault()?.Afzender?.UserName);
                                    //}
                                }
                                metroCustomTabControl1.Select();
                                metroCustomTabControl1.Focus();
                            }
                        }
                    }
                }
                else
                {
                    var _newmessage = NotificationWindows.OfType<NewMessageForm>().FirstOrDefault();
                    if (_newmessage != null && _newmessage.Visible)
                    {
                        _newmessage.Close();
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
            if (InvokeRequired)
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
                if (Manager.Opties is not { ToonNieweOpmerkingMelding: true }) return;
                if (_opmerkingform != null)
                {
                    if (_opmerkingform.WindowState == FormWindowState.Minimized)
                        _opmerkingform.WindowState = FormWindowState.Normal;
                    //if (user != null)
                    //    _chatform.SelectedUser(user);
                    _opmerkingform.Show(this);
                    _opmerkingform.BringToFront();
                    _opmerkingform.Focus();
                }
                else if (unread.Count > 0)
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
                        ShowOpmerkingWindow(this);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UpdateUnreadVerzoeken()
        {
            if (InvokeRequired)
                BeginInvoke(new Action(xUpdateUnreadVerzoeken));
            else xUpdateUnreadVerzoeken();
        }

        private void xUpdateUnreadVerzoeken()
        {
            try
            {
                if (Manager.Verzoeken?.Database == null || !Manager.Verzoeken.Database.CanRead) return;
                var unread = Manager.Verzoeken.GetUnreadEntries();
                var xtile = tileMainView1.GetTile("xverzoeken");
                if (unread.Count > 0)
                {
                    var ximg = GraphicsExtensions.DrawUserCircle(new Size(32, 32), Brushes.White,
                        unread.Count.ToString(),
                        new Font("Ariel", 16, FontStyle.Bold), Color.DarkRed);
                    xVerzoeken.Image = Resources.transfer_man_32x32.CombineImage(ximg, 1.75);
                   
                    if (xtile is { Tag: TileInfoEntry entry })
                    {
                        entry.TileCount = unread.Count;
                        entry.SecondaryImage = ximg;
                        xtile.UpdateTile(entry);
                    }
                    if (unread.Any(x => !x.IsRead()) || Manager.LogedInGebruiker is { AccesLevel: AccesType.Manager })
                    {
                        var _newmessage = InitVerzoekenForm(unread.ToArray());
                        if (_newmessage != null)
                        {
                            if (!_newmessage.Visible)
                            {
                                _newmessage.Show();
                            }
                            _newmessage.BringToFront();
                            var f = xViewPanel.FindForm();
                            if (f != null)
                            {
                                if (f.WindowState == FormWindowState.Minimized)
                                    f.WindowState = FormWindowState.Normal;
                                f.BringToFront();
                                f.Focus();
                                f.Select();

                                var tb = metroCustomTabControl1.TabPages.OfType<MetroTabPage>().ToList().FirstOrDefault(x => x.Tag is TileInfoEntry ent && ent.Name.ToLower().StartsWith("xverzoeken"));
                                if (tb != null)
                                {
                                    metroCustomTabControl1.SelectedTab = tb;
                                }
                                metroCustomTabControl1.Select();
                                metroCustomTabControl1.Focus();
                            }
                        }
                    }
                }
                else
                {
                    xVerzoeken.Image = Resources.transfer_man_32x32;
                    if (xtile is { Tag: TileInfoEntry entry })
                    {
                        entry.TileCount = 0;
                        entry.SecondaryImage = null;
                        xtile.UpdateTile(entry);
                    }
                    var _newmessage = NotificationWindows.Count > 0 ? NotificationWindows.OfType<VerzoekNotificatieForm>().First() : null;
                    if (_newmessage != null && _newmessage.Visible)
                    {
                        _newmessage.Close();
                    }
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


                    if (_producties == null || _producties.IsDisposed)
                    {
                        _producties = new Producties(owner)
                        {
                            Tag = productie,
                            StartPosition = FormStartPosition.CenterScreen,
                        };
                        //_producties.OwnerForm = ((Control)owner)?.FindForm();
                        _producties.FormClosed += (_, _) => _producties = null;
                    }

                    if (showform)
                        productie.Show(_producties.DockPanel);
                }
               // productie.OwnerForm = ((Control)_producties)?.FindForm();
                _formuis.Add(productie);
                if (!_producties.Visible && showform)
                    _producties.Show();
                if (_producties.WindowState == FormWindowState.Minimized)
                    _producties.WindowState = FormWindowState.Normal;
               
                _producties.Focus();
                _producties.Select();
                _producties.BringToFront();
                return productie;
            }
            catch (Exception e)
            {
                XMessageBox.Show(owner, e.Message, "Fout", MessageBoxIcon.Error);
                return null;
            }
        }

        public static ProductieLijstForm ShowProductieLijstForm(IWin32Window owner, List<Bewerking> bws = null, string windowname = null, string lijstname = null, IsValidHandler handler = null)
        {
            try
            {
                var prods = bws??Manager.Database.xGetAllBewerkingen(false, true, false);
                var prodform = string.IsNullOrEmpty(lijstname) ? new ProductieLijstForm(_Productelijsten.Count, prods) : new ProductieLijstForm(prods,lijstname);
                prodform.WindowName = windowname;
                prodform.ValidHandler = handler;
               // prodform.OwnerForm = ((Control)owner)?.FindForm();
                prodform.FormClosing += AddProduction_FormClosing;
                _Productelijsten.Add(prodform);
                if (_productielijstdock == null || _productielijstdock.IsDisposed)
                {
                    _productielijstdock = new ProductieLijsten(owner)
                    {
                        Tag = prodform,
                        StartPosition = FormStartPosition.CenterScreen,
                        SaveLastInfo = true
                    };
                    _productielijstdock.FormClosed += (_, _) => _productielijstdock = null;
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

        public static void ShowProductieLogWindow(IWin32Window owner)
        {
            if (_logform == null)
            {
                _logform = new LogForm();
                _logform.FormClosed += (_, _) =>
                {
                    _logform?.Dispose();
                    _logform = null;
                };
            }

            _logform.Show(owner);
            if (_logform.WindowState == FormWindowState.Minimized)
                _logform.WindowState = FormWindowState.Normal;
            _logform.BringToFront();
            _logform.Focus();
        }

        public static void ShowCalculatieWindow(IWin32Window owner)
        {
            var _calcform = new RangeCalculatorForm();
            _calcform.Show(owner);
            _calcform.BringToFront();
            _calcform.Focus();
        }

        public static void ShowChatWindow(IWin32Window owner, string username = null)
        {
            if (_chatform == null)
            {
                _chatform = new ChatForm();
                _chatform.FormClosed += (_, _) =>
                {
                    _chatform?.Dispose();
                    _chatform = null;
                };
                if(!_chatform.InitUI())
                {
                    _chatform.Dispose();
                    _chatform = null;
                    return;
                }
            }

            _chatform.Show(owner, username);
            if (_chatform.WindowState == FormWindowState.Minimized)
                _chatform.WindowState = FormWindowState.Normal;
            _chatform.BringToFront();
            _chatform.Focus();
        }

        public static void ShowOpmerkingWindow(IWin32Window owner)
        {
            if (_opmerkingform == null)
            {
                _opmerkingform = new OpmerkingenForm();
                _opmerkingform.LoadOpmerkingen();
                _opmerkingform.FormClosed += (_, _) =>
                {
                    _opmerkingform?.Dispose();
                    _opmerkingform = null;
                };
            }

            _opmerkingform.Show(owner);
            if (_opmerkingform.WindowState == FormWindowState.Minimized)
                _opmerkingform.WindowState = FormWindowState.Normal;
            _opmerkingform.BringToFront();
            _opmerkingform.Focus();
        }

        public static void ShowArtikelenWindow(IWin32Window owner, string artnr = null)
        {
            if (_ArtikelsForm == null)
            {
                _ArtikelsForm = new ArtikelsForm();
                _ArtikelsForm.FormClosed += (_, _) =>
                {
                    _ArtikelsForm?.Dispose();
                    _ArtikelsForm = null;
                };
            }

            _ArtikelsForm.Show(owner);
            if (_ArtikelsForm.WindowState == FormWindowState.Minimized)
                _ArtikelsForm.WindowState = FormWindowState.Normal;
            _ArtikelsForm.BringToFront();
            _ArtikelsForm.Focus();
            if (artnr != null)
                _ArtikelsForm.SelectedArtikelNr = artnr;
        }

        public static void ShowOnderbrekeningenWidow(IWin32Window owner)
        {
            var x = new AlleStoringenForm();
            x.InitStoringen();
            x.ShowDialog(owner);
        }

        public static void ShowAlleVaardighedenWidow(IWin32Window owner)
        {
            new AlleVaardigheden().ShowDialog(owner);
        }

        public static void ShowPersoneelWindow(IWin32Window owner)
        {
            if (_PersoneelForm == null)
            {
                _PersoneelForm = new PersoneelsForm();
                _PersoneelForm.FormClosed += (_, _) =>
                {
                    _PersoneelForm?.Dispose();
                    _PersoneelForm = null;
                };
            }

            _PersoneelForm.Show(owner);
            if (_PersoneelForm.WindowState == FormWindowState.Minimized)
                _PersoneelForm.WindowState = FormWindowState.Normal;
            _PersoneelForm.BringToFront();
            _PersoneelForm.Focus();
        }

        public void ShowWerkplaatsIndelingWindow()
        {
            if (_WerkplaatsIndeling == null)
            {
                _WerkplaatsIndeling = new WerkplaatsIndelingForm();
                _WerkplaatsIndeling.FormClosed += (_, _) =>
                {
                    _WerkplaatsIndeling?.Dispose();
                    _WerkplaatsIndeling = null;
                };
            }

            _WerkplaatsIndeling.Show(this);
            if (_WerkplaatsIndeling.WindowState == FormWindowState.Minimized)
                _WerkplaatsIndeling.WindowState = FormWindowState.Normal;
            _WerkplaatsIndeling.BringToFront();
            _WerkplaatsIndeling.Focus();
        }

        public static void ShowPersoneelIndelingWindow(IWin32Window owner)
        {
            if (_PersoneelIndeling == null)
            {
                _PersoneelIndeling = new PersoneelIndelingForm();
                _PersoneelIndeling.FormClosed += (_, _) =>
                {
                    _PersoneelIndeling?.Dispose();
                    _PersoneelIndeling = null;
                };
            }

            _PersoneelIndeling.Show(owner);
            if (_PersoneelIndeling.WindowState == FormWindowState.Minimized)
                _PersoneelIndeling.WindowState = FormWindowState.Normal;
            _PersoneelIndeling.BringToFront();
            _PersoneelIndeling.Focus();
        }

        public static void ShowBerekenVerbruikWindow(IWin32Window owner)
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
                xver.CloseClicked += (_, _) =>
                {
                    _berekenverbruik.Close();
                };
                _berekenverbruik.Controls.Add(xver);
                _berekenverbruik.FormClosed += (_, _) =>
                {
                    _berekenverbruik?.Dispose();
                    _berekenverbruik = null;
                };
            }

            _berekenverbruik.Show(owner);
            if (_berekenverbruik.WindowState == FormWindowState.Minimized)
                _berekenverbruik.WindowState = FormWindowState.Normal;
            _berekenverbruik.BringToFront();
            _berekenverbruik.Focus();
        }

        public static void ShowBerekenLeverdatum(IWin32Window owner)
        {
            var xb = new BerekenLeverdatumForm();
            xb.ShowDialog(owner);
        }
        
        public static void ShowPersoonVaardigheden(IWin32Window owner, Personeel persoon)
        {
            if (persoon == null)
                return;
            new VaardighedenForm(persoon).ShowDialog(owner);
        }

        public static void ShowBewStoringen(IWin32Window owner, Bewerking bew)
        {
            var form = bew?.GetParent();
            if (form == null) return;
            var allst = new AlleStoringenForm();
            allst.InitStoringen(form);
            allst.ShowDialog(owner);
        }

        public static void ShowWerkplekken(IWin32Window owner, ProductieFormulier form)
        {
            if (form == null)
                return;
            var ind = new Indeling(form);
            ind.ShowDialog(owner);
        }

        public void ShowBijlageWindow(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;
           
            string root = null;
            id = id.Replace("/", "\\");
            if (id.Contains("\\"))
            {
                root = Path.GetDirectoryName(id);
                if (root != null) root = Path.Combine(Manager.DbPath, "Bijlages", root);
            }
            var bl = new BijlageForm();
            bl.RootPath = root;
            //if(!string.IsNullOrEmpty(root))
            //    bl.SetPath(root);
            var newid = Path.Combine(Manager.DbPath, "Bijlages", id);
            bl.NavigationPath = newid;
            var xforms = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is BijlageForm b && b.Equals(bl));
            if (xforms != null)
            {
                if (xforms.WindowState == FormWindowState.Minimized)
                    xforms.WindowState = FormWindowState.Normal;
                xforms.BringToFront();
                xforms.Select();
                xforms.Focus();
            }
            else
            {
                bl.Title = $"Bijlages Voor: {id}";
                bl.ShowDialog(this);
            }
            bl.Dispose();
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
                if (tb.ShowDialog(this) == DialogResult.OK)
                {
                    Tools.ShowSelectedTekening(this, tb.SelectedText, TekeningClosed);
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
                var bws = bewerkingen??await Manager.Database.GetBewerkingen(ViewState.Gestart, true,null, null,false);
                _gemaaktform = new AantalGemaaktProducties(bws, lastchangedminutes);
                _gemaaktform.FormClosed += (_,_) =>
                {
                    _gemaaktform?.Dispose();
                    _gemaaktform = null;
                };
                _gemaaktform.ShowDialog(owner);

            }
            catch (Exception e)
            {
                XMessageBox.Show(owner, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void TekeningClosed(object sender, EventArgs e)
        {
            Parent?.BringToFront();
            Parent?.Focus();
        }

        public static void ShowProductieSettings(IWin32Window owner, ProductieFormulier form)
        {
            if (form == null)
                return;
            var x = new WijzigProductie(form);
            x.ShowDialog(owner);
        }

        public static void ShowOptieWidow(IWin32Window owner)
        {
            var opties = new Opties();
            opties.ShowDialog(owner);
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

        public static void ShowBewerkingDb(IWin32Window owner)
        {
            var db = new DbBewerkingChanger();
            db.ShowDialog(owner);
        }

        public static void ShowProductieOverzicht(IWin32Window owner)
        {
            if (_ProductieOverzicht == null)
            {
                _ProductieOverzicht = new ProductieOverzichtForm();
                _ProductieOverzicht.FormClosed += (_, _) =>
                {
                    _ProductieOverzicht?.Dispose();
                    _ProductieOverzicht = null;
                };
            }

            _ProductieOverzicht.Show(owner);
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
                _Klachten.FormClosed += (_, _) =>
                {
                    _Klachten?.Dispose();
                    _Klachten = null;
                };
            }

            _Klachten.Show(this);
            if (_Klachten.WindowState == FormWindowState.Minimized)
                _Klachten.WindowState = FormWindowState.Normal;
            _Klachten.BringToFront();
            _Klachten.Focus();
        }

        public void ShowCreateWeekOverzicht()
        {
            var xf = new CreateWeekExcelForm();
            xf.ShowDialog(this);
        }

        public static void CheckForPreview(IWin32Window owner, bool showall, bool onlyifnew)
        {
            try
            {
                var xprevs =
                    Functions.GetVersionPreviews(Manager.LastPreviewsUrl);
                var xvers = Assembly.GetExecutingAssembly().GetName().Version;
                if (xprevs.Count > 0)
                {
                    UpdatePreviewForm xshowform;
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

                    xshowform.ShowDialog(owner);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void ShowActiviteitWindow()
        {
            if (_ActiviteitForm == null)
            {
                _ActiviteitForm = new ActiviteitForm();
                _ActiviteitForm.FormClosed += (_, _) =>
                {
                    _ActiviteitForm?.Dispose();
                    _ActiviteitForm = null;
                };
            }

            _ActiviteitForm.Show(this);
            if (_ActiviteitForm.WindowState == FormWindowState.Minimized)
                _ActiviteitForm.WindowState = FormWindowState.Normal;
            _ActiviteitForm.BringToFront();
            _ActiviteitForm.Focus();
        }

        #endregion MenuButton Methods

        #region Menu Button Events
        private void xproductieoverzichtb_Click(object sender, EventArgs e)
        {
            ShowProductieOverzicht(this);
        }

        private void xBijlagesButton_Click(object sender, EventArgs e)
        {
            try
            {
                var bl = new BijlageForm();
                bl.Title = $"Alle Bijlages";
                bl.StartPosition = FormStartPosition.CenterParent;
                bl.Icon = Resources.files_folder_icon_64x64;
                bl.ShowIcon = true;
                bl.RootPath = Path.Combine(Manager.DbPath, "Bijlages");
                bl.Browser.RootOnlyFilledDirectories = true;
                bl.Browser.AllowEditRoot  = false;
                bl.Browser.FileView.View = View.Details;
                bl.ShowDialog(this);
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xsearchprodnr_Click(object sender, EventArgs e)
        {
            try
            {
                if (Manager.Database == null)
                    throw new Exception("Database is niet geladen!");
                var zoek = new ZoekForm();
                if (zoek.ShowDialog(this) != DialogResult.OK) return;
                var f = zoek.GetFilter();
                if (!string.IsNullOrEmpty(f.Criteria))
                {
                    var xsel = f.Criteria;
                    var prod = Manager.Database.GetProductie(xsel, false);
                    if (prod != null)
                    {
                        var bw = prod.Bewerkingen?.FirstOrDefault(x => x.IsAllowed());
                        Manager.FormulierActie(new object[] { prod, bw }, MainAktie.OpenProductie);
                        return;
                    }
                }
                var calcform = new RangeCalculatorForm();
                calcform.Filter = f;
                calcform.Show(this);
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
            ShowArtikelenWindow(this);
            //new ArtikelsForm().ShowDialog();
            //if (Manager.Opties != null)
            //    xbewerkingListControl.ProductieLijst.RestoreState(Manager.Opties.ViewDataBewerkingenState);
        }

        private void xopmerkingentoolstripbutton_Click(object sender, EventArgs e)
        {
            ShowOpmerkingWindow(this);
        }

        private void xShowPreview_Click(object sender, EventArgs e)
        {
            try
            {
                CheckForPreview(this, true, false);
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
                xhelp.ShowDialog(this);
            }
            else
            {
                XMessageBox.Show(this, "HelpDesk is tijdelijk niet beschikbaar", "Niet Beschikbaar",
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

            xweergave.ItemRemoved += (x, _) =>
            {

                if (x is string[] items)
                {
                    int deleted = 0;
                    foreach (var item in items)
                    {
                        var itemname = Path.GetFileNameWithoutExtension(item);
                        var xdir = Path.GetDirectoryName(item);
                        var xdirname = Path.GetFileName(xdir);
                        var xdel = Manager.Database.RemoveFromCollection(xdirname, new[] {itemname});
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
            xweergave.ShowDialog(this);
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
            wb.FilesFormatToOpen = new[] { "{0}_fbr.pdf" };
            wb.ShowNotFoundList = true;
            wb.StopNavigatingAfterError = false;
            Task.Factory.StartNew(() =>
            {
               
                var prods = Manager.Database.xGetBewerkingenInArtnrSections(true, false,true);
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
           
            wb.Show(this);
            xloading.ShowDialog(this);
            wb.Close();
            BringToFront();
            Focus();
            _xtekeningbusy = false;
        }

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    var xdraw = new DrawArrow(this, Direction.Top, true, PointToScreen(xToolButtons.Location));
        //    xdraw.Draw();
        //    var xdraw1 = new DrawArrow(this, Direction.Left, true,PointToScreen(mainMenu1.Location));
        //    xdraw1.Draw();
        //    //var xdraw = new DrawArrow(this, Direction.Top, true, this.PointToScreen(xToolButtons.Location));
        //    //xdraw.Draw();
        //    //var xdraw = new DrawArrow(this, Direction.Top, true, this.PointToScreen(xToolButtons.Location));
        //    //xdraw.Draw();
        //}

        //private Point FindLocation(Control ctrl, bool height)
        //{
        //    if (ctrl.Parent is Form)
        //        return new Point(ctrl.Location.X + (height ? 0 : ctrl.Width),
        //            ctrl.Location.Y + (height ? ctrl.Height : 0));

        //    Point p = FindLocation(ctrl.Parent, height);
        //    p.X += ctrl.Location.X;
        //    p.Y += ctrl.Location.Y;
        //    return p;
        //}

        private void xklachten_Click(object sender, EventArgs e)
        {
            ShowKlachtenWindow();
        }

        private void xverpakkingen_Click(object sender, EventArgs e)
        {
            var vr = new VerpakkingenForm();
            vr.ShowDialog(this);
        }

        private void xMaakWeekOverzichtToolstrip_Click(object sender, EventArgs e)
        {
            ShowCreateWeekOverzicht();
        }

        private void xArtikelRecordsToolstripButton_Click(object sender, EventArgs e)
        {
            var artikels = new ArtikelRecordsForm();
            artikels.ShowDialog(this);
        }

        private void xShowDaily_Click(object sender, EventArgs e)
        {
            DailyMessage.CreateDaily(true);
        }

        private void xSporenButton_Click(object sender, EventArgs e)
        {
            new ArtikelenVerbruikForm().ShowDialog(this);
        }

        private void OptimaleVerbruik_Click(object sender, EventArgs e)
        {
            var xform = new OptimaleLengteVerbruikForm();
            xform.ShowDialog(this);
        }

        private void xActiviteit_Click(object sender, EventArgs e)
        {
            ShowActiviteitWindow();
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
                        xui.ShowDialog(taak.Formulier, taak.Bewerking, taak.Plek);
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
                        if (matform.ShowDialog(this, p) == DialogResult.OK)
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
                        ind.ShowDialog(this);
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
                            taak.Formulier = Manager.Database.GetProductie(taak.Bewerking.ProductieNr, false);
                            taak.Bewerking = taak.Formulier.Bewerkingen?.FirstOrDefault(x =>
                                string.Equals(x.Naam, taak.Bewerking.Naam, StringComparison.CurrentCultureIgnoreCase));

                            if (taak.Bewerking != null)
                            {
                                var date = dt.SelectedValue;
                                if (dt.AddTime)
                                {
                                    date = taak.Bewerking.LeverDatum.Add(dt.TimeToAdd);
                                }
                                taak.Bewerking.LeverDatum = date;
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
                            taak.Formulier = Manager.Database.GetProductie(taak.Formulier.ProductieNr, false);
                            if (taak.Formulier == null) return;
                            var date = dt.SelectedValue;
                            if (dt.AddTime)
                            {
                                date = taak.Formulier.LeverDatum.Add(dt.TimeToAdd);
                            }
                            if (taak.Formulier.Bewerkingen.Length > 0)
                                taak.Formulier.Bewerkingen[taak.Formulier.Bewerkingen.Length - 1].LeverDatum =
                                    date;
                            else taak.Formulier.LeverDatum = date;
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
                        ind.ShowDialog(this);
                        // if (ind.ShowDialog() == DialogResult.OK)
                        //taak.Update();
                    }

                    break;
                case AktieType.Onderbreking:
                    if (taak.Plek != null)
                    {
                        var st = new StoringForm(taak.Plek);
                        st.ShowDialog(this);
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
    }
}