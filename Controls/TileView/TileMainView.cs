using Forms;
using ProductieManager.Rpm.Misc;
using ProductieManager.Rpm.Productie;
using ProductieManager.Rpm.Various;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
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
            //groupedTileView1.TileClicked += OnTileClicked;
            //groupedTileView1.TilesLoaded += OnTilesLoaded;
            //groupedTileView1.RequestInfo += tileViewer1_TileRequestInfo;
        }

        public void LoadTileViewer()
        {
            //groupedTileView1.LoadTileViewer();
            //if (Manager.Opties.TileCountRefreshRate > 0)
            //    groupedTileView1.Viewer.StartTimer(true);
            //else
            //    groupedTileView1.Viewer.StopTimer();
           // Manager.Opties.GroupEntries?.Clear();
            var groups = xGroupContainer.Controls.OfType<GroupedTileView>();
            var oldgroups = Manager.Opties.GroupEntries;
            var newgroups = new List<GroupInfoEntry>();
            for (int i = 0; i < Manager.Opties.TileLayout.Count; i++)
            {
                var tile = Manager.Opties.TileLayout[i];
                tile.Group = string.Empty;
                var xold = oldgroups.FirstOrDefault(x => string.Equals(x.Name, tile.Group, StringComparison.CurrentCultureIgnoreCase));
                if (!newgroups.Any(x => string.Equals(x.Name, tile.Group, StringComparison.CurrentCultureIgnoreCase)))
                {
                    if (xold != null)
                    {
                        oldgroups.Remove(xold);
                        newgroups.Add(xold);
                    }
                    else
                    {
                        var newgroup = new GroupInfoEntry() { Name = tile.Group };
                        newgroups.Add(newgroup);
                    }
                }
            }
            if (oldgroups.Count > 0)
                newgroups.AddRange(oldgroups);
            var removes = groups.Where(x => !newgroups.Any(g => string.Equals(x.GroupName, g.Name, StringComparison.CurrentCultureIgnoreCase)));
            foreach (var remove in removes)
                xGroupContainer.Controls.Remove(remove);
            groups = xGroupContainer.Controls.OfType<GroupedTileView>();

            foreach (var group in newgroups)
            {
                var xold = groups.FirstOrDefault(x => string.Equals(x.GroupName, group.Name, StringComparison.CurrentCultureIgnoreCase));
                if (xold == null)
                {
                    xold = new GroupedTileView();
                    xold.GroupEntry = group;
                    xold.Viewer.GroupName = group.Name;
                    xold.Viewer.FlowDirection = group.TileFlowDirection;
                    xold.Viewer.TilesLoaded += OnTilesLoaded;
                    xold.Viewer.TileClicked += OnTileClicked;
                    xold.RequestInfo += tileViewer1_TileRequestInfo;
                    //xold.Location = group.Location;
                    //xold.Size = group.Size;
                    //ControlMoverOrResizer.Init(xold);
                    xold.Dock = DockStyle.Fill;
                    xGroupContainer.Controls.Add(xold);
                }
                xold.LoadTileViewer();
            }
            //xGroupContainer.BackColor = Color.FromArgb(Manager.Opties.TileViewBackgroundColorRGB);
            // xGroupContainer.FlowDirection = Manager.Opties.TileFlowDirection;
            xGroupContainer.Invalidate();
            xGroupContainer.PerformLayout();
        }

        private void InitDefaultGroup()
        {
            try
            {

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void UpdateFilterTiles()
        {
            var groups = xGroupContainer.Controls.OfType<GroupedTileView>();
            foreach (var group in groups)
                group.UpdateFilterTiles();
        }

        public void SaveLayout(bool save)
        {
            if (Manager.Opties == null) return;
            var groups = xGroupContainer.Controls.OfType<GroupedTileView>();
            var grp = new List<GroupInfoEntry>();
            foreach (var group in groups)
            {
                group.SaveTileLayout(false);
                var item = group.GroupEntry;
                //item.Location = group.Location;
                //item.Size = group.Size;
            }
            Manager.Opties.GroupEntries = groups.Select(x => x.GroupEntry).ToList();
            if (save)
                Manager.Opties.Save();
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
                var groups = xGroupContainer.Controls.OfType<GroupedTileView>();
                foreach (var group in groups)
                {
                    var tile = group.Viewer.GetTile(name);
                    if (tile != null) return tile;
                }
                return null;
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
                        bws = Manager.ProductieProvider.xGetBewerkingen(ProductieProvider.LoadedType.Producties,
                            new ViewState[]
                            {
                                ViewState.Gestart, ViewState.Gestopt
                            }, true, null, true);
                        entry.TileCount = bws.Count;
                        break;
                    case "werkplaatsen":
                            var xxbws = Manager.ProductieProvider.xGetBewerkingen(
                                ProductieProvider.LoadedType.Producties,
                                new ViewState[]
                                {
                                    ViewState.Gestart
                                }, true, null, true);
                            var wps = xxbws.SelectMany(x => x.WerkPlekken.Where(w => w.IsActief())).ToList();
                            entry.TileCount = wps.Count;
                           
                            break;
                    case "gereedmeldingen":
                        bws = Manager.ProductieProvider.xGetBewerkingen(ProductieProvider.LoadedType.Alles,
                            new ViewState[]
                            {
                                ViewState.Gereed
                            }, true, null, true);
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
                        bws = Manager.ProductieProvider?.xGetBewerkingen(ProductieProvider.LoadedType.Alles,
                            new ViewState[]
                            {
                               ViewState.Alles
                            }, true, null, true);
                        var storingen = bws?.SelectMany(x => x.GetStoringen(false)).ToList();
                        entry.TileCount = storingen?.Count??0;
                       
                        break;
                    case "xverbruik":
                        break;
                    case "xverbruikbeheren":
                        entry.TileCount = Manager.SporenBeheer?.Database?.Count()??0;
                        break;
                    case "xchangeaantal":
                        bws = Manager.ProductieProvider?.xGetBewerkingen(ProductieProvider.LoadedType.Producties,
                            new ViewState[]
                            {
                                ViewState.Gestart
                            }, true, null, true);
                        bws = bws
                            ?.Where(x => x.State == ProductieState.Gestart && string.Equals(x.GestartDoor, Manager.Opties.Username,
                                StringComparison.CurrentCultureIgnoreCase)).ToList();
                        entry.TileCount = bws?.Count??0;
                        break;
                    case "xsearchtekening":
                        break;
                    case "xpersoneelindeling":
                        entry.TileCount = Manager.Opties?.PersoneelIndeling?.Count??0;
                        break;
                    case "xwerkplaatsindeling":
                        entry.TileCount = Manager.Opties?.WerkplaatsIndelingen?.Count??0;
                        break;
                    case "xcreateexcel":
                        break;
                    case "xstats":
                        break;
                    case "xzoekproducties":
                        break;
                    case "xpersoneel":
                        entry.TileCount = Manager.Database?.PersoneelLijst?.Count()??0;
                        break;
                    case "xalleartikelen":
                        bws = Manager.ProductieProvider.xGetBewerkingen(ProductieProvider.LoadedType.Alles,
                            new ViewState[]
                            {
                                ViewState.Alles
                            }, true, null, true);
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
                        bws = Manager.ProductieProvider.xGetBewerkingen(ProductieProvider.LoadedType.Alles,
                            new ViewState[]
                            {
                                ViewState.Alles
                            }, true, null, true);
                        var xnots = bws.SelectMany(x => x.GetAlleNotities()).Count();
                        entry.TileCount = xnots;
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
                                        bws = Manager.ProductieProvider.xGetBewerkingen(
                                            ProductieProvider.LoadedType.Producties,
                                            new ViewState[]
                                            {
                                                ViewState.Gestart,
                                                ViewState.Gestopt,
                                            }, true, null, true);
                                        bws = bws.Where(x => xf.IsAllowed(x, null, false)).ToList();
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

        public event EventHandler TileClicked;
        public event EventHandler TilesLoaded;

        protected virtual void OnTileClicked(object sender, EventArgs arg)
        {
            TileClicked?.Invoke(sender, arg);
        }

       protected virtual void OnTilesLoaded(object sender, EventArgs arg)
        {
            TilesLoaded?.Invoke(sender, arg);
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

        private void beheerTileLayoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void xBeheerLijstenToolstripItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void reserLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void kiesAchtergrondKleurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChooseBackgroundColor();
        }

        private void wijzigGroepGrootteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tf = new TextFieldEditor();
            tf.MultiLine = false;
            tf.UseSecondary = false;
            tf.Title = "Vul in een groep naam";
            if (tf.ShowDialog(this) != DialogResult.OK) return;
            var name = tf.SelectedText.Trim();
            var old = Manager.Opties.GroupEntries.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if (old != null)
                XMessageBox.Show(this, $"'{name}' bestaat al");
            else
            {
                Manager.Opties.GroupEntries.Add(new GroupInfoEntry() { Name = name });
                Manager.Opties.Save(null, false, false, false);
                LoadTileViewer();
            }
        }
    }
}
