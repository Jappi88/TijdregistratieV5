using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Forms;
using Forms.MetroBase;
using ProductieManager.Rpm.Misc;

namespace Rpm.Controls
{
    public partial class ImageCombineForm : MetroBaseForm
    {
        public ImageCombineForm()
        {
            InitializeComponent();
            LoadLocaties();
        }

        private void LoadLocaties()
        {
            xalingments.Items.Clear();
            var names = Enum.GetNames(typeof(ContentAlignment));
            xalingments.Items.AddRange(names.Select(x => (object) x).ToArray());
        }

        private Image LoadImage()
        {
            try
            {
                var ofd = new OpenFileDialog();
                ofd.Title = "Kies Afbeelding";
                ofd.Filter = "PNG|*.png|JPG|*.jpg|Alle Bestanden|*.*";
                if (ofd.ShowDialog() == DialogResult.OK) return Image.FromFile(ofd.FileName);

                return null;
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
                return null;
            }
        }

        private void SetResult()
        {
            try
            {
                var image = (Bitmap) xfirstimage.Image;
                if (image != null && xsecondimage.Image != null)
                {
                    var align = ContentAlignment.BottomRight;
                    if (xalingments.SelectedIndex > -1)
                        Enum.TryParse(xalingments.SelectedItem.ToString(), true, out align);
                    image = image.CombineImage((Bitmap) xsecondimage.Image, align, (double) xverhouding.Value);
                }

                xresult.Image = image;
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xsluitenb_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void xopslaanalsb_Click(object sender, EventArgs e)
        {
        }

        private void xkiesimage1_Click(object sender, EventArgs e)
        {
            xfirstimage.Image = LoadImage();
            SetResult();
        }

        private void xkiesimage2_Click(object sender, EventArgs e)
        {
            xsecondimage.Image = LoadImage();
            SetResult();
        }

        private void xverhouding_ValueChanged(object sender, EventArgs e)
        {
            SetResult();
        }

        private void xalingments_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetResult();
        }
    }
}