using MetroFramework;
using MetroFramework.Controls;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using MetroFramework.Drawing;

namespace Controls
{
    public partial class Tile : MetroTile
    {
        private readonly Button flatbutton;
        private readonly ContextMenuStrip xContextMenu;
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

        public ToolStripItem[] MenuItems { get => GetMenuItems(); set => SetMenuItems(value); }

        public ToolStripItem[] GetMenuItems()
        {
           var xret = new List<ToolStripItem>();
            if(xContextMenu?.Items != null)
            {
                for(int i = 0; i < xContextMenu?.Items.Count; i++)
                {
                    var item = xContextMenu.Items[i];
                    if (item is ToolStripMenuItem menuItem)
                        xret.Add(menuItem);
                }
            }
            return xret.ToArray();
        }

        public void SetMenuItems(ToolStripItem[] items)
        {
            xContextMenu?.Items?.Clear();
            if (items != null && xContextMenu?.Items != null)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    var item = items[i];
                    xContextMenu.Items.Add(item);
                }
            }
        }

        public Tile()
        {
            InitializeComponent();
            UseTileImage = true;
            UseCustomBackColor = true;
            UseCustomForeColor = true;
            TextAlign = ContentAlignment.BottomLeft;
            Size = new Size(128, 64);
            Padding = new Padding(5);
            UseStyleColors = true;
            this.Style = MetroColorStyle.Purple;
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
            xContextMenu = new ContextMenuStrip();
            xContextMenu.Opening += Xcontext_Opening;
            flatbutton.ContextMenuStrip = xContextMenu;
            this.ContextMenuStrip = xContextMenu;
            this.MenuItems = new ToolStripItem[] {
            new ToolStripMenuItem("Tile Wijzigen", Resources.Tile_colors_icon_32x32, TileChangeClicked),
            new ToolStripSeparator(),
            new ToolStripMenuItem("Tile Verwijderen", Resources.delete_1577, TileRemoveClicked)
            };
           
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

        //protected override void OnPaintForeground(PaintEventArgs e)
        //{
        //    Color color = IProductieBase.GetProductSoortColor("solar");
        //    Color foreColor1 = !this.isHovered || this.isPressed || !this.Enabled ? (!this.isHovered || !this.isPressed || !this.Enabled ? (this.Enabled ? MetroPaint.ForeColor.Tile.Normal(this.Theme) : MetroPaint.ForeColor.Tile.Disabled(this.Theme)) : MetroPaint.ForeColor.Tile.Press(this.Theme)) : MetroPaint.ForeColor.Tile.Hover(this.Theme);
        //    if (this.UseCustomForeColor)
        //        foreColor1 = this.ForeColor;
        //    if (this.isPressed || (this.isHovered || this.isFocused) && this.DisplayFocusBorder)
        //    {
        //        using (Pen pen = new Pen(color))
        //        {
        //            pen.Width = 5f;
        //            Rectangle rect = new Rectangle(1, 1, this.Width - 5, this.Height - 5);
        //            e.Graphics.DrawRectangle(pen, rect);
        //        }
        //    }
        //    e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
        //    e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
        //    if (this.UseTileImage && this.TileImage != null)
        //    {
        //        Rectangle rect;
        //        switch (this.TileImageAlign)
        //        {
        //            case ContentAlignment.TopLeft:
        //                rect = new Rectangle(new Point(0, 0), new Size(this.TileImage.Width, this.TileImage.Height));
        //                break;
        //            case ContentAlignment.TopCenter:
        //                rect = new Rectangle(new Point(this.Width / 2 - this.TileImage.Width / 2, 0), new Size(this.TileImage.Width, this.TileImage.Height));
        //                break;
        //            case ContentAlignment.TopRight:
        //                rect = new Rectangle(new Point(this.Width - this.TileImage.Width, 0), new Size(this.TileImage.Width, this.TileImage.Height));
        //                break;
        //            case ContentAlignment.MiddleLeft:
        //                rect = new Rectangle(new Point(0, this.Height / 2 - this.TileImage.Height / 2), new Size(this.TileImage.Width, this.TileImage.Height));
        //                break;
        //            case ContentAlignment.MiddleCenter:
        //                rect = new Rectangle(new Point(this.Width / 2 - this.TileImage.Width / 2, this.Height / 2 - this.TileImage.Height / 2), new Size(this.TileImage.Width, this.TileImage.Height));
        //                break;
        //            case ContentAlignment.MiddleRight:
        //                rect = new Rectangle(new Point(this.Width - this.TileImage.Width, this.Height / 2 - this.TileImage.Height / 2), new Size(this.TileImage.Width, this.TileImage.Height));
        //                break;
        //            case ContentAlignment.BottomLeft:
        //                rect = new Rectangle(new Point(0, this.Height - this.TileImage.Height), new Size(this.TileImage.Width, this.TileImage.Height));
        //                break;
        //            case ContentAlignment.BottomCenter:
        //                rect = new Rectangle(new Point(this.Width / 2 - this.TileImage.Width / 2, this.Height - this.TileImage.Height), new Size(this.TileImage.Width, this.TileImage.Height));
        //                break;
        //            case ContentAlignment.BottomRight:
        //                rect = new Rectangle(new Point(this.Width - this.TileImage.Width, this.Height - this.TileImage.Height), new Size(this.TileImage.Width, this.TileImage.Height));
        //                break;
        //            default:
        //                rect = new Rectangle(new Point(0, 0), new Size(this.TileImage.Width, this.TileImage.Height));
        //                break;
        //        }
        //        e.Graphics.DrawImage(this.TileImage, rect);
        //    }
        //    if (this.TileCount > 0 && this.PaintTileCount)
        //    {
        //        int tileCount = this.TileCount;
        //        Size size = TextRenderer.MeasureText(tileCount.ToString(), this.TileCountFont);
        //        e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        //        Graphics graphics = e.Graphics;
        //        tileCount = this.TileCount;
        //        string text = tileCount.ToString();
        //        Font tileCountFont = this.TileCountFont;
        //        Point pt = new Point(this.Width - size.Width, 0);
        //        Color foreColor2 = foreColor1;
        //        TextRenderer.DrawText((IDeviceContext)graphics, text, tileCountFont, pt, foreColor2);
        //        e.Graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
        //    }
        //    TextRenderer.MeasureText(this.Text, MetroFonts.Tile(this.TileTextFontSize, this.TileTextFontWeight));
        //    TextFormatFlags flags = MetroPaint.GetTextFormatFlags(this.TextAlign) | TextFormatFlags.LeftAndRightPadding | TextFormatFlags.EndEllipsis;
        //    Rectangle clientRectangle = this.ClientRectangle;
        //    if (this.isPressed)
        //        clientRectangle.Inflate(-4, -12);
        //    else
        //        clientRectangle.Inflate(-2, -10);
        //    TextRenderer.DrawText((IDeviceContext)e.Graphics, this.Text, this.Font, clientRectangle, foreColor1, flags);
        //}

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
