using Rpm.Misc;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using ProductieManager.Forms;

namespace Forms
{
    public partial class ScreenImageForm : Forms.MetroBase.MetroBaseForm
    {
        Bitmap bmp;
        public string SavedImagePath { get; set; }
        public ScreenImageForm(Int32 x, Int32 y, Int32 w, Int32 h, Size s)
        {
            InitializeComponent();

            Rectangle rect = new Rectangle(x, y, w, h);
            bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, s, CopyPixelOperation.SourceCopy);

            //bmp.Save("screen.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            pbCapture.Image = bmp;
            this.Size = new Size(s.Width + 23, s.Height + 86);
        }

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
            string xdir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            sfd.InitialDirectory = xdir;
            string xscreen = Functions.GetAvailibleFilepath(xdir, "ScreenShot.jpg");
            sfd.FileName = Path.GetFileName(xscreen);
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pbCapture.Image.Save(sfd.FileName);
                SavedImagePath = sfd.FileName;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            SelectScreenImage area = new SelectScreenImage();
            this.Hide();
            var result = area.ShowDialog();
            SavedImagePath = area.SelectedImagePath;
            this.DialogResult = result;
            //frmHome home = new frmHome();
            //this.Hide();
            //home.Show();
        }
    }
}
