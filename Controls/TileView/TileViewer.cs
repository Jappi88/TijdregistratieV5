using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using iTextSharp.text.pdf.parser.clipper;
using ProductieManager.Rpm.Misc;

namespace Controls
{
    public partial class TileViewer : FlowLayoutPanel
    {
        private readonly System.Timers.Timer _timer;
        public bool EnableSaveTiles { get; set; } = true;
        public int TileInfoRefresInterval { get; set; } = 10000;
        public bool EnableTileSelection { get; set; }
        public bool MultipleSelections { get; set; }

        public bool EnableTimer
        {
            get => _timer.Enabled;
            set => _timer.Enabled = value;
        }

        public TileViewer()
        {
            InitializeComponent();
            _timer = new System.Timers.Timer();
            _timer.Interval = TileInfoRefresInterval;
            _timer.Elapsed += _timer_Elapsed;
        }

        public void StartTimer()
        {
            _timer?.Start();
        }

        public void StopTimer()
        {
            _timer?.Stop();
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            StopTimer();
            try
            {
                var _Tiles = GetAllTiles();
                for (int i = 0; i < _Tiles.Count; i++)
                {
                    var xtileinfo = _Tiles[i];
                    if (xtileinfo.Tag is TileInfoEntry entry)
                    {
                        Tile xtile = null;
                        if (this.InvokeRequired)
                            this.Invoke(new MethodInvoker(() => xtile = GetTile(entry)));
                        else
                            xtile = GetTile(entry);
                        if (xtile != null)
                        {
                            var xupdate = OnTileRequestInfo(xtile);
                            if (xupdate != null)
                            {
                                if (this.InvokeRequired)
                                {
                                    this.Invoke(new MethodInvoker(() => UpdateTile(xupdate, xtile)));
                                    this.Invoke(new MethodInvoker(Invalidate));
                                }
                                else
                                {
                                    UpdateTile(xupdate, xtile);
                                    Invalidate();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            StartTimer();
        }

        private void UpdateTiles(List<TileInfoEntry> tiles, TileInfoEntry selected = null)
        {
            try
            {
                var xTiles = tiles;
                var _Tiles = GetAllTiles();
                var xtoremove = _Tiles.Where(x => x.Tag is TileInfoEntry ent && !xTiles.Any(t => t.Equals(ent)))
                    .ToList();
                if (xtoremove.Count > 0)
                {
                    SuspendLayout();
                    xtoremove.ForEach(x => Controls.Remove(x));
                    ResumeLayout();
                }

                for (int i = 0; i < xTiles.Count; i++)
                {
                    var xtileinfo = xTiles[i];
                    if (xtileinfo == null)
                        continue;
                    xtileinfo.TileIndex = i;
                    var xtile = GetTile(xtileinfo);
                    xtile = UpdateTile(xtileinfo, xtile);
                    xtile.AllowSelection = EnableTileSelection;
                    if (EnableTileSelection && selected != null && selected.Equals(xtileinfo))
                        SelectTile(xtile, true);
                }

                _Tiles = GetAllTiles();
                _Tiles.ForEach(x =>
                {
                    if (x.Tag is TileInfoEntry ent)
                        Controls.SetChildIndex(x, ent.TileIndex);
                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public Tile UpdateTile(TileInfoEntry entry, Tile tile = null)
        {
            try
            {
                tile ??= GetTile(entry);
                var xtile = tile ?? new Tile();
                xtile.AllowSelection = EnableTileSelection;
                if (tile == null)
                {
                    xtile.Click += Xtile_Click;
                    entry.TileIndex = Controls.Count;
                    Controls.Add(xtile);
                }
                xtile.AllowTileEdit = EnableSaveTiles;
                return xtile.UpdateTile(entry);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private void Xtile_Click(object sender, EventArgs e)
        {
            OnTileClicked(sender);
        }

        public Tile GetTile(TileInfoEntry entry)
        {
            try
            {
                var xcontrols = Controls.Cast<Control>().ToList();
                for (int i = 0; i < xcontrols.Count; i++)
                {
                    var xcon = xcontrols[i];
                    if (xcon is Tile {Tag: TileInfoEntry xinfo} xtile)
                        if (xinfo.Equals(entry))
                            return xtile;
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public Tile GetTile(string name)
        {
            try
            {
                var xcontrols = Controls.Cast<Control>().ToList();
                for (int i = 0; i < xcontrols.Count; i++)
                {
                    var xcon = xcontrols[i];
                    if (xcon is Tile { Tag: TileInfoEntry xinfo } xtile)
                        if (string.Equals(name,xinfo.Name, StringComparison.CurrentCultureIgnoreCase))
                            return xtile;
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }


        public List<Tile> GetAllTiles()
        {
            var xret = new List<Tile>();
            try
            {
                var xcontrols = Controls.Cast<Control>().ToList();
                for (int i = 0; i < xcontrols.Count; i++)
                {
                    var xcon = xcontrols[i];
                    if (xcon is Tile tile)
                        xret.Add(tile);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xret;
        }

        public List<TileInfoEntry> GetAllTileEntries()
        {
            var xret = new List<TileInfoEntry>();
            try
            {
                var xcontrols = Controls.Cast<Control>().ToList();
                for (int i = 0; i < xcontrols.Count; i++)
                {
                    var xcon = xcontrols[i];
                    if (xcon is Tile {Tag: TileInfoEntry entry})
                        xret.Add(entry);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xret;
        }

        public bool SaveTiles(bool save)
        {
            try
            {
                var xtiles = GetAllTiles();
                var xlist = new List<TileInfoEntry>();
                int index = 0;
                foreach (var tile in xtiles)
                {
                    if (tile.Tag is TileInfoEntry ent)
                    {
                        ent.TileIndex = index++;
                        xlist.Add(ent);
                    }
                }

                Manager.Opties.TileLayout = xlist;
                if (save)
                    Manager.Opties.Save(null, false, false, false);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool LoadTiles(List<TileInfoEntry> tiles, TileInfoEntry selected = null)
        {
            try
            {
                UpdateTiles(tiles);
                OnTilesLoaded();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public Tile GetTileFromPoint(Point location, int marge)
        {
            try
            {
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    var xtile = this.Controls[i] as Tile;
                    if (xtile == null) continue;
                    var xloc = new Rectangle(xtile.Location, new Size(xtile.Width + marge, xtile.Height + marge));
                    bool flag = xloc.Contains(location);
                        if (flag)
                            return xtile;
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            var data = (Tile) e.Data.GetData(typeof(Tile));
            if (data == null) return;
            var _destination = (FlowLayoutPanel) sender;
            // Just add the control to the new panel.
            // No need to remove from the other panel, this changes the Control.Parent property.
            var p = _destination.PointToClient(new Point(e.X, e.Y));
            var item = GetTileFromPoint(p,10); //??_destination.Controls[_destination.Controls.Count - 1];
            var index = item == null ? 0 : _destination.Controls.GetChildIndex(item, false);
            var xoldindex = _destination.Controls.GetChildIndex(data, false);

            if (item != null && xoldindex > -1)
            {
                _destination.Controls.SetChildIndex(item, xoldindex);
                if (item.Tag is TileInfoEntry xent)
                {
                    xent.TileIndex = xoldindex;
                    var xindex = Manager.Opties?.TileLayout?.IndexOf(xent) ?? -1;
                    if (EnableSaveTiles && xindex > -1 && Manager.Opties?.TileLayout != null)
                        Manager.Opties.TileLayout[xindex] = xent;
                }
            }
            _destination.Controls.SetChildIndex(data, index);
            if (data.Tag is TileInfoEntry ent)
            {
                ent.TileIndex = index;
                var xindex = Manager.Opties?.TileLayout?.IndexOf(ent) ?? -1;
                if (EnableSaveTiles && xindex > -1 && Manager.Opties?.TileLayout != null)
                    Manager.Opties.TileLayout[xindex] = ent;
            }
            _destination.Invalidate();
        }

        private void flowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        #region Events

        public event EventHandler TilesLoaded;

        protected virtual void OnTilesLoaded()
        {
            TilesLoaded?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler TileClicked;
        public event TileChangeEventhandler TileRequestInfo;

        protected virtual void OnTileClicked(object sender)
        {
            if (EnableTileSelection)
            {
                if (sender is Tile tile)
                {
                    TogleTile(tile);
                }
            }
            TileClicked?.Invoke(sender, EventArgs.Empty);
        }

        public bool TogleTile(Tile tile)
        {
            bool select = !tile.Selected;
            return SelectTile(tile, select);
        }

        public bool SelectTile(Tile tile, bool enable)
        {
            if (enable && !MultipleSelections)
            {
                var xtiles = GetAllTiles();
                xtiles.ForEach(x => x.Selected = false);
            }

            bool flag = tile.Selected != enable;
            tile.Selected = enable;
            if (flag)
                OnSelectionChanged(tile);
            return enable;
        }

        public event EventHandler SelectionChanged;

        protected virtual TileInfoEntry OnTileRequestInfo(Tile tile)
        {
            return TileRequestInfo?.Invoke(tile);
        }

        public void PerformClick(Tile tile)
        {
            OnTileClicked(tile);
        }

        #endregion Events

        protected virtual void OnSelectionChanged(object sender)
        {
            SelectionChanged?.Invoke(sender, EventArgs.Empty);
        }
    }
}
