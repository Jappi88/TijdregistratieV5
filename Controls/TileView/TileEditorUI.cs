using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Forms;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;

namespace Controls
{
    public partial class TileEditorUI : UserControl
    {
        public TileInfoEntry InfoEntry { get; set; }

        public List<TileInfoEntry> Tiles => tileViewer1.GetAllTileEntries();
        public FlowDirection Direction
        {
            get => tileViewer1.FlowDirection;
            set=> tileViewer1.FlowDirection = value;
        }

        public TileEditorUI()
        {
            InitializeComponent();
        }

        public void InitEntry(TileInfoEntry entry)
        {
            InfoEntry = null;
            xomschrijving.Text = entry?.Text;
            xafbeelding.Image = entry?.TileImage;
            xtextkleurimage.Image = entry?.ForeColor.ColorToImage(28, 28);
            xtilekleur.Image = entry?.TileColor.ColorToImage(28, 28);
            xtoontelling.Checked = entry?.EnableTileCount??false;
            xtilebreedte.SetValue(entry?.Size.Width??0);
            xtilehoogte.SetValue(entry?.Size.Height??0);
            ximagebreedte.SetValue(entry?.ImageSize.Width ?? 0);
            ximagehoogte.SetValue(entry?.ImageSize.Height ?? 0);
            if (entry != null)
            {
                switch (entry.ImageResize)
                {
                    case ResizeMode.None:
                        xnonecheckbox.Checked = true;
                        break;
                    case ResizeMode.Custom:
                        xaangepastcheckbox.Checked = true;
                        break;
                    case ResizeMode.Auto:
                        xautocheckbox.Checked = true;
                        break;
                }
            }
            InfoEntry = entry;
            if (InfoEntry == null) return;
            UpdateTile();
        }

        public void InitEntries(List<TileInfoEntry> entries,FlowDirection direction, TileInfoEntry selected = null)
        {
            tileViewer1.EnableTileSelection = true;
            xtileview.Visible = true;
            xtileview.SelectedIndex = (int) direction;
            tileViewer1.FlowDirection = direction;
            tileViewer1.LoadTiles(entries, selected);
        }

        private void xomschrijving_TextChanged(object sender, System.EventArgs e)
        {
            if (InfoEntry != null)
            {
                InfoEntry.Text = xomschrijving.Text.Trim();
                UpdateTile();
            }

        }

        private void xtoontelling_CheckedChanged(object sender, System.EventArgs e)
        {
            if (InfoEntry != null)
            {
                InfoEntry.EnableTileCount = xtoontelling.Checked;
                UpdateTile();
            }

        }

        private void UpdateTile()
        {
            if (InfoEntry == null) return;
            var xtile = tileViewer1.UpdateTile(InfoEntry);
            if (InfoEntry.EnableTileCount)
                xtile.TileCount = InfoEntry.TileCount == 0? 123 : InfoEntry.TileCount;
        }

        private void xtilekleurbutton_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (InfoEntry == null)
                    return;
                var xd = new ColorDialog();
                xd.AnyColor = true;
                xd.Color = InfoEntry.TileColor;
                xd.AllowFullOpen = true;
                if (xd.ShowDialog() == DialogResult.OK)
                {
                    var xcolor = xd.Color;
                    InfoEntry.TileColor = xcolor;
                    xtilekleur.Image = xcolor.ColorToImage(28, 28);
                    UpdateTile();
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xtextkleur_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (InfoEntry == null)
                    return;
                var xd = new ColorDialog();
                xd.AnyColor = true;
                xd.AllowFullOpen = true;
                xd.Color = InfoEntry.ForeColor;
                if (xd.ShowDialog() == DialogResult.OK)
                {
                    var xcolor = xd.Color;
                    InfoEntry.ForeColor = xcolor;
                    xtextkleurimage.Image = xcolor.ColorToImage(28, 28);
                    UpdateTile();
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xtextfont_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (InfoEntry == null)
                    return;
                var xd = new FontDialog();
                xd.Font = new Font(InfoEntry.TextFontFamily, InfoEntry.TextFontSize, InfoEntry.TextFontStyle);
                xd.Color = InfoEntry.ForeColor;
                if (xd.ShowDialog() == DialogResult.OK)
                {
                    var xcolor = xd.Color;
                    InfoEntry.ForeColor = xcolor;
                    xtextkleurimage.Image = xcolor.ColorToImage(28, 28);
                    InfoEntry.TextFontFamily = xd.Font.FontFamily.Name;
                    InfoEntry.TextFontSize = (int)xd.Font.Size;
                    InfoEntry.TextFontStyle = xd.Font.Style;
                    UpdateTile();
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xtellingtextfont_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (InfoEntry == null)
                    return;
                var xd = new FontDialog();
                xd.Font = new Font(InfoEntry.CountFontFamily, InfoEntry.CountFontSize, InfoEntry.CountFontStyle);
                xd.Color = InfoEntry.ForeColor;
                if (xd.ShowDialog() == DialogResult.OK)
                {
                    var xcolor = xd.Color;
                    InfoEntry.ForeColor = xcolor;
                    xtextkleur.Image = xcolor.ColorToImage(28, 28);
                    InfoEntry.CountFontFamily = xd.Font.FontFamily.Name;
                    InfoEntry.CountFontSize = (int)xd.Font.Size;
                    InfoEntry.CountFontStyle = xd.Font.Style;
                    UpdateTile();
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xformaatwijzigen_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (InfoEntry == null)
                    return;
                var form = new ChangeSizeForm(InfoEntry.Size);
                form.ChangeMinimumSize(new Size(128, 96));
                form.InitInfo();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    InfoEntry.Size = form.SelectedSize;
                    UpdateTile();
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xafbeeldingbutton_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (InfoEntry == null)
                    return;
                var ofd = new OpenFileDialog();
                ofd.Filter = "PNG|*.png|JPG|*.jpg|Toon Alles|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var xfile = ofd.FileName;
                    if (xfile.IsImageFile())
                    {
                        var img = File.ReadAllBytes(xfile).ImageFromBytes();
                        InfoEntry.TileImage = img;
                        xafbeelding.Image = img;
                        UpdateTile();
                    }
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xnonecheckbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAfbeeldingFormaatPanel();
        }

        private void UpdateAfbeeldingFormaatPanel()
        {
            if (InfoEntry != null)
            {
                if (xnonecheckbox.Checked)
                    InfoEntry.ImageResize = ResizeMode.None;
                else if (xaangepastcheckbox.Checked)
                    InfoEntry.ImageResize = ResizeMode.Custom;
                else if (xautocheckbox.Checked)
                    InfoEntry.ImageResize = ResizeMode.Auto;
            }
            xformaatpanel.Visible = xaangepastcheckbox.Checked;
            UpdateTile();
        }

        private void xbreedte_ValueChanged(object sender, EventArgs e)
        {
            if (InfoEntry != null)
            {
                InfoEntry.ImageSize = new Size((int) ximagebreedte.Value, (int) ximagehoogte.Value);
                UpdateTile();
            }
        }

        private void xtilebreedte_ValueChanged(object sender, EventArgs e)
        {
            if (InfoEntry != null)
            {
                InfoEntry.Size = new Size((int)xtilebreedte.Value, (int)xtilehoogte.Value);
                UpdateTile();
            }
        }

        private void tileViewer1_SelectionChanged(object sender, EventArgs e)
        {
            if (sender is Tile {Tag: TileInfoEntry entry} tile)
            {
                if (tile.Selected)
                    InfoEntry = entry;
                else InfoEntry = null;
                InitEntry(InfoEntry);
            }
        }

        private void xtileview_SelectedIndexChanged(object sender, EventArgs e)
        {
            tileViewer1.FlowDirection = (FlowDirection) xtileview.SelectedIndex;
            tileViewer1.LoadTiles(tileViewer1.GetAllTileEntries(), InfoEntry);
        }
    }
}
