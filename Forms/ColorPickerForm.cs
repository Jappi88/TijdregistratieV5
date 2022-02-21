using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Forms.MetroBase;

namespace Forms
{
    public partial class ColorPickerForm : MetroBaseForm
    {
        public ColorPickerForm()
        {
            InitializeComponent();
            var colors = new List<Color>();
            foreach (var colorValue in Enum.GetValues(typeof(KnownColor)))
                colors.Add(Color.FromKnownColor((KnownColor) colorValue));
            SetKleuren(colors);
        }

        public string Title
        {
            get => Text;
            set
            {
                Text = value;
                Invalidate();
            }
        }


        public Color SelectedColor
        {
            get => xkleurenlist.SelectedItems.Count == 0 ? Color.Empty : (Color) xkleurenlist.SelectedItems[0].Tag;
            set => SetSelectedColor(value);
        }

        private Image CreateImage(Color color, int width, int height)
        {
            var Bmp = new Bitmap(width, height);
            using (var gfx = Graphics.FromImage(Bmp))
            using (var brush = new SolidBrush(color))
            {
                gfx.FillRectangle(brush, 0, 0, width, height);
            }

            return Bmp;
        }

        private void SetSelectedColor(Color kleur)
        {
            foreach (var lv in xkleurenlist.Items)
                if (lv is ListViewItem item)
                    if (item.Tag is Color c && c == kleur)
                    {
                        item.Selected = true;
                        break;
                    }
        }

        public void SetKleuren(List<Color> kleuren)
        {
            xkleurenlist.Items.Clear();
            imageList1.Images.Clear();
            foreach (var color in kleuren)
            {
                imageList1.Images.Add(CreateImage(color, imageList1.ImageSize.Width, imageList1.ImageSize.Height));
                var lv = new ListViewItem();
                lv.Tag = color;
                lv.ImageIndex = imageList1.Images.Count - 1;
                xkleurenlist.Items.Add(lv);
            }
        }

        private void xkleurenlist_DoubleClick(object sender, EventArgs e)
        {
            if (xkleurenlist.SelectedItems.Count > 0)
                if (xkleurenlist.SelectedItems[0].Tag is Color kleur)
                {
                    SelectedColor = kleur;
                    DialogResult = DialogResult.OK;
                }
        }

        private void xkleurenlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xkleurenlist.SelectedItems.Count > 0)
            {
                xselectedcolor.Image = imageList1.Images[xkleurenlist.SelectedItems[0].ImageIndex];
                xOpslaan.Visible = true;
            }
            else
            {
                xselectedcolor.Image = null;
                xOpslaan.Visible = false;
            }
        }

        private void xOpslaan_Click(object sender, EventArgs e)
        {
            if (xkleurenlist.SelectedItems.Count > 0)
            {
                SelectedColor = (Color) xkleurenlist.SelectedItems[0].Tag;
                DialogResult = DialogResult.OK;
            }
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}