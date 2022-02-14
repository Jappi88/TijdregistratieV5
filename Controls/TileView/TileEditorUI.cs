using System;
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

        public TileEditorUI()
        {
            InitializeComponent();
        }

        public void InitEntry(TileInfoEntry entry)
        {
            InfoEntry = entry;
            InfoEntry ??= new TileInfoEntry();
            xomschrijving.Text = InfoEntry.Text;
            xafbeelding.Image = InfoEntry.TileImage;
            xtextkleurimage.Image = InfoEntry.ForeColor.ColorToImage(28, 28);
            xtilekleur.Image = InfoEntry.TileColor.ColorToImage(28, 28);
            xtoontelling.Checked = InfoEntry.EnableTileCount;
            UpdateTile();
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
            tileViewer1.UpdateTile(InfoEntry, tile1);
            if (InfoEntry.EnableTileCount)
                tile1.TileCount = InfoEntry.TileCount == 0? 123 : InfoEntry.TileCount;
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
    }
}
