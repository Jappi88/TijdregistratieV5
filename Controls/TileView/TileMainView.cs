using Forms;
using ProductieManager.Rpm.Misc;
using ProductieManager.Rpm.Productie;
using ProductieManager.Rpm.Various;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Controls.TileView
{
    public partial class TileMainView : UserControl
    {
        public TileMainView()
        {
            InitializeComponent();
        }

        public void LoadTileViewer()
        {
            if (Manager.Opties != null)
            {
                InitToolStripMenu();
                UpdateFilterTiles();
                tileViewer1.BackColor = Color.FromArgb(Manager.Opties.TileViewBackgroundColorRGB);
                tileViewer1.LoadTiles(Manager.Opties.TileLayout);
            }
        }

        public void UpdateFilterTiles()
        {
            var xtiles = Manager.Opties.TileLayout.Where(x =>
                string.Equals(x.GroupName, "filter", StringComparison.CurrentCultureIgnoreCase)).ToList();
            var xremove = xtiles.Where(x => Manager.Opties.Filters.All(f => f.ID != x.LinkID)).ToList();
            if (xremove.Count > 0)
                xremove.ForEach(f => Manager.Opties.TileLayout.Remove(f));
            xtiles = Manager.Opties.TileLayout.Where(x =>
                string.Equals(x.GroupName, "filter", StringComparison.CurrentCultureIgnoreCase)).ToList();
            xtiles.ForEach(f =>
            {
                var xent = Manager.Opties.Filters.FirstOrDefault(x => x.ID == f.LinkID);
                if (xent != null)
                {
                    f.Name = xent.Name;
                }

            });
        }

        public int TileCountRefreshInterval
        {
            get => tileViewer1.TileInfoRefresInterval;
            set=> tileViewer1.TileInfoRefresInterval = value;
        }

        public TileViewer Viewer => tileViewer1;

        private void InitToolStripMenu()
        {
            tileViewer1.FlowDirection = Manager.Opties.TileFlowDirection;
            int xindex = (int)tileViewer1.FlowDirection;
            var xitems = toolStripMenuItem1.DropDownItems.Cast<ToolStripItem>();
            int i = 0;
            foreach (var xitem in xitems)
            {
                if (xitem is ToolStripMenuItem item)
                {
                    item.Checked = i.ToString() == xindex.ToString();
                }
                i++;
            }
        }

        public bool SaveLayout(bool save)
        {
            return tileViewer1.SaveTiles(save);
        }

        private void tileViewer1_TileClicked(object sender, EventArgs e)
        {
            OnTileClicked(sender);
        }

        private TileInfoEntry tileViewer1_TileRequestInfo(Tile tile)
        {
            if (tile.Tag is TileInfoEntry entry)
            {
                return GetTileInfo(entry);
            }

            return null;
        }

        public Tile GetTile(string name)
        {
            try
            {
                return tileViewer1.GetTile(name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public TileInfoEntry GetTileInfo(TileInfoEntry entry)
        {
            try
            {
                if (entry == null) return null;
                if (!entry.EnableTileCount)
                {
                    entry.TileCount = 0;
                    return entry;
                }

                if (Manager.ProductieProvider == null) return entry;
                List<Bewerking> bws = new List<Bewerking>();
                switch (entry.Name.ToLower())
                {
                    case "producties":
                        bws = Manager.ProductieProvider.GetBewerkingen(ProductieProvider.LoadedType.Producties,
                            new ViewState[]
                            {
                                ViewState.Gestart, ViewState.Gestopt
                            }, true, null).Result;
                        entry.TileCount = bws.Count;
                        break;
                    case "werkplaatsen":
                            var xxbws = Manager.ProductieProvider.GetBewerkingen(
                                ProductieProvider.LoadedType.Producties,
                                new ViewState[]
                                {
                                    ViewState.Gestart
                                }, true, null).Result;
                            var wps = xxbws.SelectMany(x => x.WerkPlekken.Where(w => w.IsActief())).ToList();
                            entry.TileCount = wps.Count;
                           
                            break;
                    case "gereedmeldingen":
                        bws = Manager.ProductieProvider.GetBewerkingen(ProductieProvider.LoadedType.Alles,
                            new ViewState[]
                            {
                                ViewState.Gereed
                            }, true, null).Result;
                        if (Manager.Opties != null)
                        {
                            if (Manager.Opties.UseLastGereedStart || Manager.Opties.UseLastGereedStop)
                            {
                                var xstart = new DateTime();
                                if (Manager.Opties.UseLastGereedStart)
                                    xstart = Manager.Opties.LastGereedStart;
                                var xstop = DateTime.MaxValue;
                                if (Manager.Opties.UseLastGereedStop)
                                    xstop = Manager.Opties.LastGereedStop;
                                bws = bws.Where(x => x.DatumGereed >= xstart && x.DatumGereed <= xstop).ToList();
                            }
                            else
                            {
                                var xstart = DateTime.Now.Subtract(TimeSpan.FromDays(1));
                                var xstop = DateTime.Now;
                                bws = bws.Where(x => x.DatumGereed >= xstart && x.DatumGereed <= xstop).ToList();
                            }
                        }

                        entry.TileCount = bws.Count;
                        break;
                    case "onderbrekingen":
                        bws = Manager.ProductieProvider.GetBewerkingen(ProductieProvider.LoadedType.Alles,
                            new ViewState[]
                            {
                               ViewState.Alles
                            }, true, null).Result;
                        var storingen = bws.SelectMany(x => x.GetStoringen(false)).ToList();
                        entry.TileCount = storingen.Count;
                       
                        break;
                    case "xverbruik":
                        break;
                    case "xverbruikbeheren":
                        entry.TileCount = Manager.SporenBeheer?.Database?.Count()??0;
                        break;
                    case "xchangeaantal":
                        bws = Manager.ProductieProvider.GetBewerkingen(ProductieProvider.LoadedType.Producties,
                            new ViewState[]
                            {
                                ViewState.Gestart
                            }, true, null).Result;
                        bws = bws
                            .Where(x => x.State == ProductieState.Gestart && string.Equals(x.GestartDoor, Manager.Opties.Username,
                                StringComparison.CurrentCultureIgnoreCase)).ToList();
                        entry.TileCount = bws.Count;
                        break;
                    case "xsearchtekening":
                        break;
                    case "xpersoneelindeling":
                        entry.TileCount = Manager.Opties.PersoneelIndeling.Count;
                        break;
                    case "xwerkplaatsindeling":
                        entry.TileCount = Manager.Opties.WerkplaatsIndeling.Count;
                        break;
                    case "xcreateexcel":
                        break;
                    case "xstats":
                        break;
                    case "xzoekproducties":
                        break;
                    case "xpersoneel":
                        entry.TileCount = Manager.Database?.PersoneelLijst?.Count().Result??0;
                        break;
                    case "xalleartikelen":
                        bws = Manager.ProductieProvider.GetBewerkingen(ProductieProvider.LoadedType.Alles,
                            new ViewState[]
                            {
                                ViewState.Alles
                            }, true, null).Result;
                        var xlist = new List<string>();
                        for (int i = 0; i < bws.Count; i++)
                        {
                            if (xlist.IndexOf(bws[i].ArtikelNr) > -1)
                                continue;
                            xlist.Add(bws[i].ArtikelNr);
                        }
                        entry.TileCount = xlist.Count;
                        break;
                    case "xartikelrecords":
                        entry.TileCount = Manager.ArtikelRecords?.Database?.Count()??0;
                        break;
                    case "xproductievolgorde":
                        break;
                    case "xklachten":
                        entry.TileCount = Manager.Klachten?.GetAlleKlachten().Count??0;
                        break;
                    case "xweekoverzicht":
                        break;
                    case "xallenotities":
                        bws = Manager.ProductieProvider.GetBewerkingen(ProductieProvider.LoadedType.Alles,
                            new ViewState[]
                            {
                                ViewState.Alles
                            }, true, null).Result;
                        var xnots = bws.SelectMany(x => x.GetAlleNotities()).ToList();
                        entry.TileCount = xnots.Count;
                        break;
                    case "xbeheerfilters":
                        entry.TileCount = Manager.Opties?.Filters?.Count ?? 0;
                        break;
                    case "xchat":
                        entry.TileCount = ProductieChat.Gebruikers?.Count(x=> x.IsOnline && x.UserName.ToLower() != "iedereen")??0;
                        var xunread = ProductieChat.Chat?.GetAllUnreadMessages().Count ?? 0;
                        if (xunread > 0)
                        {
                            var ximg = GraphicsExtensions.DrawUserCircle(new Size(32, 32), Brushes.White,
                                xunread.ToString(),
                                new Font("Ariel", 16, FontStyle.Bold), Color.DarkRed);
                            entry.SecondaryImage = ximg;
                        }
                        else entry.SecondaryImage = null;
                        break;
                    default:
                        switch (entry.GroupName.ToLower())
                        {
                            case "filter":
                                if (Manager.Opties != null && Manager.Opties.Filters.Count > 0)
                                {
                                    var xf = Manager.Opties.Filters.FirstOrDefault(x =>
                                        string.Equals(x.Name, entry.Name.Replace("_", " "),
                                            StringComparison.CurrentCultureIgnoreCase));
                                    if (xf != null)
                                    {
                                        bws = Manager.ProductieProvider.GetBewerkingen(
                                            ProductieProvider.LoadedType.Producties,
                                            new ViewState[]
                                            {
                                                ViewState.Gestart,
                                                ViewState.Gestopt,
                                            }, true, null).Result;
                                        bws = bws.Where(x => xf.IsAllowed(x, null)).ToList();
                                        entry.TileCount = bws.Count;
                                    }
                                    else entry.TileCount = 0;
                                }
                                else entry.TileCount = 0;
                                break;
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return entry;
        }

        private void tileViewer1_TilesLoaded(object sender, EventArgs e)
        {
            tileViewer1.StartTimer();
            OnTilesLoaded();
        }

        public event EventHandler TileClicked;
        public event EventHandler TilesLoaded;
        public bool EnableTimer
        {
            get => tileViewer1.EnableTimer;
            set => tileViewer1.EnableTimer = value;
        }

        protected virtual void OnTileClicked(object sender)
        {
            TileClicked?.Invoke(sender, EventArgs.Empty);
        }

        private void xBeheerweergavetoolstrip_ButtonClick(object sender, EventArgs e)
        {
            xBeheerweergavetoolstrip.ShowDropDown();
        }

        private void reserLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = XMessageBox.Show(this, "Weetje zeker dat je de TileLayout wilt resetten?!\n\n" +
                                                "Alle gewijzigde tiles zullen ongedaan worden, toch doorgaan?",
                "TileLayout Resetten", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.No) return;
            if (Manager.Opties != null)
            {
                Manager.Opties.TileFlowDirection = FlowDirection.LeftToRight;
                Manager.Opties.TileLayout = Manager.Opties.GetAllDefaultEntries(false);
                Manager.Opties.Save("Tiles gereset!");
                LoadTileViewer();
            }
        }

        private void toolStripMenuItem1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var xindex = toolStripMenuItem1.DropDownItems.IndexOf(e.ClickedItem);
            if (xindex > -1 && Manager.Opties != null)
            {
                Manager.Opties.TileFlowDirection = (FlowDirection) xindex;
                Manager.Opties.Save(null, false, false, false);
                LoadTileViewer();
            }
        }

        private void xBeheerLijstenToolstripItem_Click(object sender, EventArgs e)
        {
            var xform = new BeheerTilesForm();
            if (xform.ShowDialog() == DialogResult.OK)
            {
                LoadTileViewer();
            }
        }

        protected virtual void OnTilesLoaded()
        {
            TilesLoaded?.Invoke(Viewer, EventArgs.Empty);
        }

        private void ShowTileLayoutEditor()
        {
            try
            {
                if (Manager.Opties?.TileLayout == null) return;
                var xeditor = new TileEditorForm(Manager.Opties.TileLayout.CreateCopy(), Manager.Opties.TileFlowDirection, null);
                xeditor.Size = new Size(1200, 750);
                if (xeditor.ShowDialog() == DialogResult.OK)
                {
                    Manager.Opties.TileLayout = xeditor.SelectedEntries;
                    Manager.Opties.TileFlowDirection = xeditor.Direction;
                    Manager.Opties.Save(null, false, false, false);
                    LoadTileViewer();
                }

            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void beheerTileLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTileLayoutEditor();
        }

        private void beheerTileLayoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowTileLayoutEditor();
        }

        public void SetBackgroundImage(string path)
        {
            try
            {
                if (Manager.Opties == null) return;
                if (path.IsImageFile())
                {
                    Manager.Opties.BackgroundImagePath = path;
                    //pictureBox1.Image = Image.FromFile(path);
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        public void SetBackgroundImage()
        {
            try
            {
                if (Manager.Opties == null) return;
                var ofd = new OpenFileDialog();
                ofd.Filter = "JPG|*.jpg|JPEG|*.jpeg|PNG|*.png|Alles|*.*";
                ofd.Multiselect = false;
                ofd.Title = "Kies een achtergrond Afbeelding";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var xfile = ofd.FileName;
                    SetBackgroundImage(xfile);
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        public void ClearBackgroundImage()
        {
            try
            {
                if (Manager.Opties == null) return;
                Manager.Opties.BackgroundImagePath = null;
                //pictureBox1.Image = null;
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void kiesAchtergrondAfbeeldingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetBackgroundImage();
        }

        private void verwijderAchtergrondAfbeeldingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearBackgroundImage();
        }

        private void ChooseBackgroundColor()
        {
            try
            {
                if (Manager.Opties == null) return;
                var xcolorpicker = new ColorDialog();
                xcolorpicker.AllowFullOpen = true;
                xcolorpicker.Color = Color.FromArgb(Manager.Opties.TileViewBackgroundColorRGB);
                xcolorpicker.AnyColor = true;
                if (xcolorpicker.ShowDialog() == DialogResult.OK)
                {
                    Manager.Opties.TileViewBackgroundColorRGB = xcolorpicker.Color.ToArgb();
                    Manager.Opties.Save(null, false, false, false);
                    LoadTileViewer();
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void kiesAchtergrondKleurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChooseBackgroundColor();
        }
    }
}
