using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetroFramework;
using ProductieManager.Rpm.Misc;
using Rpm.Productie;

namespace Controls
{
    public partial class TileViewer : FlowLayoutPanel
    {
        private readonly System.Timers.Timer _timer;
        
        public int TileInfoRefresInterval { get; set; } = 10000;

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
                    Tile xtile = null;
                    if (this.InvokeRequired)
                        this.Invoke(new MethodInvoker(() => xtile = GetTile(xtileinfo.Name)));
                    else
                        xtile = GetTile(xtileinfo.Name);
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
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            StartTimer();
        }

        private void UpdateTiles()
        {
            try
            {
                var xTiles = Manager.Opties.TileLayout;
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
                    var xtile = GetTile(xtileinfo.Name);
                    bool isnew = xtile == null;
                    xtile = UpdateTile(xtileinfo, xtile);
                    if (isnew)
                    {
                        xtileinfo.TileIndex = Controls.Count;
                        Controls.Add(xtile);
                        xtile.Click += (x, y) => OnTileClicked(x);
                    }


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
                var xtile = tile ?? new Tile();
                return xtile.UpdateTile(entry);
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
                    if (string.Equals(xcon.Name, name, StringComparison.CurrentCultureIgnoreCase) && xcon is Tile tile)
                        return tile;
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

        public bool LoadTiles()
        {
            try
            {
                if (Manager.Opties == null) return false;
                UpdateTiles();
                OnTilesLoaded();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            var data = (Tile)e.Data.GetData(typeof(Tile));
            if (data == null) return;
            var _destination = (FlowLayoutPanel)sender;
            var _source = (FlowLayoutPanel)data.Parent;

            if (_source != _destination)
            {
                // Add control to panel
                _destination.Controls.Add(data);
                data.Size = new Size(_destination.Width, 50);

                // Reorder
                var p = _destination.PointToClient(new Point(e.X, e.Y));
                var item = _destination.GetChildAtPoint(p);

                var index = item == null ? 0 : _destination.Controls.GetChildIndex(item, false);

                _destination.Controls.SetChildIndex(data, index);
                if (data.Tag is TileInfoEntry ent)
                {
                    ent.TileIndex = index;
                    var xindex = Manager.Opties?.TileLayout?.IndexOf(ent) ?? -1;
                    if (xindex > -1 && Manager.Opties?.TileLayout != null)
                        Manager.Opties.TileLayout[xindex] = ent;
                }
                // Invalidate to paint!
                _destination.Invalidate();
                _source.Invalidate();
            }
            else
            {
                // Just add the control to the new panel.
                // No need to remove from the other panel, this changes the Control.Parent property.
                var p = _destination.PointToClient(new Point(e.X, e.Y));
                var item = _destination.GetChildAtPoint(p) ?? _destination.Controls[_destination.Controls.Count - 1];
                var index = item == null ? 0 : _destination.Controls.GetChildIndex(item, false);
                _destination.Controls.SetChildIndex(data, index);
                if (data.Tag is TileInfoEntry ent)
                {
                    ent.TileIndex = index;
                    var xindex = Manager.Opties?.TileLayout?.IndexOf(ent) ?? -1;
                    if (xindex > -1 && Manager.Opties?.TileLayout != null)
                        Manager.Opties.TileLayout[xindex] = ent;
                }
                _destination.Invalidate();
            }
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
            TileClicked?.Invoke(sender, EventArgs.Empty);
        }

        protected virtual TileInfoEntry OnTileRequestInfo(Tile tile)
        {
            return TileRequestInfo?.Invoke(tile);
        }

        public void PerformClick(Tile tile)
        {
            OnTileClicked(tile);
        }

        #endregion Events

    }
}
