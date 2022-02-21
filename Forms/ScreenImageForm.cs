using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Forms.MetroBase;
using Rpm.Misc;

namespace Forms
{
    public partial class ScreenImageForm : MetroBaseForm
    {
        private readonly Bitmap bmp;

        public ScreenImageForm(int x, int y, int w, int h, Size s)
        {
            InitializeComponent();

            var rect = new Rectangle(x, y, w, h);
            bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            var g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, s, CopyPixelOperation.SourceCopy);

            //bmp.Save("screen.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            pbCapture.Image = bmp;
            Size = new Size(s.Width + 23, s.Height + 86);
        }

        public string SavedImagePath { get; set; }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.CheckPathExists = true;
            //sfd.FileName = "Capture"; 
            //sfd.Filter = "PNG Image(*.png)|*.png|JPG Image(*.jpg)|*.jpg|BMP Image(*.bmp)|*.bmp";
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    pbCapture.Image.Save(sfd.FileName);
            //}


            var sfd = new SaveFileDialog();
            sfd.Title = "Screenshot Opslaan";
            var xdir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            sfd.InitialDirectory = xdir;
            var xscreen = Functions.GetAvailibleFilepath(xdir, "ScreenShot.jpg");
            sfd.FileName = Path.GetFileName(xscreen);
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pbCapture.Image.Save(sfd.FileName);
                SavedImagePath = sfd.FileName;
                DialogResult = DialogResult.OK;
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            var area = new SelectScreenImage();
            Hide();
            var result = area.ShowDialog();
            SavedImagePath = area.SelectedImagePath;
            DialogResult = result;
            //frmHome home = new frmHome();
            //this.Hide();
            //home.Show();
        }
    }
}