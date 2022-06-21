using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controls
{
    public partial class TileViewer : FlowLayoutPanel
    {
        private readonly System.Timers.Timer _timer;
        public bool EnableSaveTiles { get; set; } = true;
        public int TileInfoRefresInterval { get; set; } = 10000;
        public bool EnableTileSelection { get; set; }
        public bool MultipleSelections { get; set; }
        public string GroupName { get; set; }

        bool _compact;
        public bool IsCompactMode
        {
            get => _compact;
            set
            {
                bool flag = _compact != value;
                _compact = value;
                if (flag)
                    UpdateTiles();
            }
        }

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
            _timer.Enabled = false;
        }

        public void StartTimer(bool update)
        {
            if (_syncing) return;
            if (update)
                UpdateTilesCount();
            else _timer.Start();
        }

        public void StopTimer()
        {
            _timer?.Stop();
        }

        private bool _syncing;
        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            StopTimer();
            if (_syncing) return;
            _syncing = true;
            UpdateTilesCount();
        }

        public Task UpdateTilesCount()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    _syncing = true;
                    if (Manager.Opties != null)
                    {
                        TileInfoRefresInterval = Manager.Opties.TileCountRefreshRate;
                        _timer.Interval = TileInfoRefresInterval;
                    }
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
                                    }
                                    else
                                    {
                                        UpdateTile(xupdate, xtile);
                                    }
                                }
                            }
                        }
                    }
                    if (this.InvokeRequired)
                        this.Invoke(new MethodInvoker(Invalidate));
                    else
                        Invalidate();

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
                _syncing = false;
                StartTimer(false);
            });
        }

        public void UpdateSize()
        {
            return;
            var items = Controls.Cast<Control>().ToList();
            int x = items.Sum(x=> x.Width + 5);
            int y = items.Sum(x => x.Height + 5);
            var sqrt = Math.Sqrt((items.Count + (items.Count%2)));
            if (this.Parent?.Parent != null)
            {
                this.Parent.Parent.Width = (int)Math.Ceiling(x / sqrt);
                this.Parent.Parent.Height = (int)Math.Ceiling(y / sqrt + 1);
                this.Parent.Parent.PerformLayout();
                this.Parent.Parent?.Parent?.PerformLayout();
            }
            else
            {
                this.Width = (int)(x / sqrt);
                this.Height = (int)(y / sqrt);
            }
            Invalidate();
        }

        public void UpdateTiles(TileInfoEntry selected = null, string groupname = null)
        {
            if (Manager.Opties?.TileLayout != null)
                UpdateTiles(Manager.Opties.TileLayout, selected, groupname);
        }

        public void UpdateTiles(List<TileInfoEntry> tiles, TileInfoEntry selected = null, string groupname = null)
        {
            try
            {
                groupname ??= GroupName;
                var xTiles = tiles?.Where(x=> string.Equals(x.Group??"", groupname??"", StringComparison.CurrentCultureIgnoreCase)).ToList();
                xTiles ??= new List<TileInfoEntry>();
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
                UpdateSize();
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
                entry.Group = GroupName;
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
                if (InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(() => xret = GetAllTiles()));
                    return xret;
                }
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
                if (InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(() => xret = GetAllTileEntries()));
                    return xret;
                }
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
                var xtiles = GetAllTileEntries().OrderBy(x=> x.TileIndex).ToList();
                int index = 0;
                foreach (var tile in xtiles)
                {
                    tile.TileIndex = index++;
                    tile.Group = GroupName;
                }
                Manager.Opties.TileLayout.RemoveAll(x => string.Equals(x.Group, GroupName, StringComparison.CurrentCultureIgnoreCase));
                Manager.Opties.TileLayout.AddRange(xtiles);
                if (save)
                    return Manager.Opties.xSave(null, false, false, false);
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
            var par = data.Parent;
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
                    {
                        Manager.Opties.TileLayout[xindex] = xent;
                    }
                }
            }
            else
            {
                _destination.Controls.Add(data);
            }
            //_destination.Controls.SetChildIndex(data, index);
            if (data.Tag is TileInfoEntry ent)
            {
                ent.TileIndex = index;
                var xindex = Manager.Opties?.TileLayout?.IndexOf(ent) ?? -1;
                if (EnableSaveTiles && xindex > -1 && Manager.Opties?.TileLayout != null)
                {
                    Manager.Opties.TileLayout[xindex] = ent;
                }
            }
            if (par is TileViewer tv && !_destination.Equals(par))
            {
                tv.UpdateSize();
                UpdateSize();
            }

            SaveTiles(false);
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
