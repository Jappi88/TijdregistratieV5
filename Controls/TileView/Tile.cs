using MetroFramework;
using MetroFramework.Controls;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Controls
{
    public partial class Tile : MetroTile
    {
        private readonly Button flatbutton;
        public List<string> InfoFields { get; set; }

        public bool AllowTileEdit
        {
            get => flatbutton?.Visible ?? false;
            set
            {
                if (flatbutton != null)
                    flatbutton.Visible = value;
            }
        }

        private readonly PictureBox SelectedBox;
        
        public bool Selected
        {
            get => SelectedBox.Visible;
            set => SelectedBox.Visible = value;
        }

        public bool AllowSelection { get; set; }

        public Tile()
        {
            InitializeComponent();
            this.Resize += Tile_Resize;
            UseTileImage = true;
            UseCustomBackColor = true;
            UseCustomForeColor = true;
            TextAlign = ContentAlignment.BottomLeft;
            Size = new Size(128, 64);
            Padding = new Padding(5);
            UseStyleColors = true;
            BackColor = Color.DeepSkyBlue;
            flatbutton = new Button();
            flatbutton.FlatStyle = FlatStyle.Flat;
            flatbutton.BackColor = Color.Transparent;
            flatbutton.FlatAppearance.BorderSize = 0;
            flatbutton.FlatAppearance.MouseOverBackColor = Color.AliceBlue;
            flatbutton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            flatbutton.Image = Resources.icons8_Menu_Vertical_32;
            flatbutton.Size = new Size(16, 32);
            flatbutton.ImageAlign = ContentAlignment.MiddleCenter;
            flatbutton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            flatbutton.Location = new Point(this.Width - 16, this.Height - 32);
            var xcontext = new ContextMenuStrip();
            xcontext.Opening += Xcontext_Opening;
            xcontext.Items.Add(new ToolStripMenuItem("Tile Wijzigen", Resources.Tile_colors_icon_32x32, TileChangeClicked));
            xcontext.Items.Add(new ToolStripSeparator());
            xcontext.Items.Add(new ToolStripMenuItem("Tile Verwijderen", Resources.delete_1577, TileRemoveClicked));
            flatbutton.ContextMenuStrip = xcontext;
            this.ContextMenuStrip = xcontext;
            flatbutton.Click += Flatbutton_Click;
            SelectedBox = new PictureBox();
            SelectedBox.Visible = false;
            SelectedBox.Image = Resources.notification_done_114461;
            SelectedBox.SizeMode = PictureBoxSizeMode.StretchImage;
            SelectedBox.BackColor = Color.Transparent;
            SelectedBox.Size = new Size(32, 32);
            SelectedBox.Click += (x, y) => this.PerformClick();
            this.SuspendLayout();
            this.Controls.Add(flatbutton);
            this.Controls.Add(SelectedBox);
            SelectedBox.BringToFront();
            flatbutton.BringToFront();
            UpdateSelectedImageLocation();
            this.ResumeLayout(false);
        }

        private void UpdateSelectedImageLocation()
        {
            if (SelectedBox == null) return;
            SelectedBox.Location = new Point(((this.Size.Width / 2) - SelectedBox.Width / 2), ((this.Size.Height / 2) - SelectedBox.Height));
        }

        private void Tile_Resize(object sender, EventArgs e)
        {
            UpdateSelectedImageLocation();
        }

        private void TileChangeClicked(object sender, EventArgs e)
        {
            if (!AllowTileEdit) return;
            if (this.Tag is TileInfoEntry entry)
            {
                var xindex = Manager.Opties?.TileLayout?.IndexOf(entry)??-1;
                var form = new TileEditorForm(entry.CreateCopy());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    this.UpdateTile(form.SelectedEntry);
                    if (Manager.Opties?.TileLayout != null && xindex > -1)
                        Manager.Opties.TileLayout[xindex] = form.SelectedEntry;
                }
            }
        }

        private void TileRemoveClicked(object sender, EventArgs e)
        {
            if (!AllowTileEdit) return;
            if (this.Parent is TileViewer table && Tag is TileInfoEntry entry)
            {
                table.Controls.Remove(this);
                Manager.Opties.TileLayout.Remove(entry);
                this.Dispose();
            }
        }

        private void Xcontext_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !AllowTileEdit;
        }

        public Tile UpdateTile(TileInfoEntry entry)
        {
            var xtile = this;
            xtile.Name = entry.Name.Trim().Replace(" ", "_");
            xtile.Text = entry.Text;
            xtile.Tag = entry;
            xtile.TileCount = entry.EnableTileCount ? entry.TileCount : 0;
            xtile.TileCountFont = new Font(entry.CountFontFamily, entry.CountFontSize, entry.CountFontStyle);
            xtile.TileTextFontWeight = MetroTileTextWeight.Regular;
            xtile.TileTextFontSize = MetroTileTextSize.Tall;
            xtile.Font = new Font(entry.TextFontFamily, entry.TextFontSize, entry.TextFontStyle);
            xtile.ForeColor = entry.ForeColor;
            xtile.UseCustomBackColor = true;
            xtile.UseCustomForeColor = true;
            xtile.BackColor = entry.TileColor;
            xtile.TileImage = entry.TileImage;
            xtile.TileImageAlign = ContentAlignment.TopLeft;
            xtile.Size = entry.Size;
            xtile.Anchor = AnchorStyles.None;
            if (!AllowSelection && entry.SecondaryImage != null)
            {
                SelectedBox.Visible = true;
                SelectedBox.Image = entry.SecondaryImage;
                Selected = true;
            }
            else
            {
                if (!AllowSelection)
                    SelectedBox.Visible = false;
                SelectedBox.Image = Resources.notification_done_114461;
            }
            return xtile;
        }

        private void Flatbutton_Click(object sender, EventArgs e)
        {
            if (sender is Button {ContextMenuStrip: { }} b)
                b.ContextMenuStrip.Show(b,10,10);
        }

        public sealed override Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        #region Dragging
        private bool _isDragging;
        private readonly int _DDradius = 40;
        private int _mX;
        private int _mY;

        private Cursor BitMapCursor;

        public void TileMouseDown(object sender, MouseEventArgs e)
        {
            Focus();
            //base.OnMouseDown(e);
            _mX = e.X;
            _mY = e.Y;
            _isDragging = false;
            //Cast the sender to control type youre using
            //Copy the control in a bitmap
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
            bmp = bmp.ChangeOpacity(0.75f);
            //In a variable save the cursor with the image of your controler
            this.BitMapCursor = new Cursor(bmp.GetHicon());
            //this.DoDragDrop(this.Parent, DragDropEffects.Move);
        }

        public void TileMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging)
            {
                // This is a check to see if the mouse is moving while pressed.
                // Without this, the DragDrop is fired directly when the control is clicked, now you have to drag a few pixels first.
                if (e.Button == MouseButtons.Left && _DDradius > 0)
                {
                    var num1 = _mX - e.X;
                    var num2 = _mY - e.Y;
                    if (num1 * num1 + num2 * num2 > _DDradius)
                    {
                        DoDragDrop(this, DragDropEffects.All);
                        _isDragging = true;
                        return;
                    }
                }
                //base.OnMouseMove(e);
                _isDragging = false;

            }
        }

        private void DoMove(int x, int y, bool select)
        {
            var _destination = this.Parent;
            if (_destination == null) return;
            // Just add the control to the new panel.
            // No need to remove from the other panel, this changes the Control.Parent property.
            var p = _destination.PointToClient(new Point(x, y));
            var item = _destination.GetChildAtPoint(p);
            if (item is Tile {Tag: TileInfoEntry entry} xtile)
            {
                xtile.BackColor = select ? Color.White : entry.TileColor;
            }
        }

        private void Tile_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            //Deactivate the default cursor
            e.UseDefaultCursors = false;
            //Use the cursor created from the bitmap
            Cursor.Current = this.BitMapCursor;
           // base.OnGiveFeedback(e);
        }
        #endregion
    }
}
