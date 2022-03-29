using Forms.MetroBase;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Forms.ImageViewer
{
    public partial class ImageViewerForm : MetroBaseForm
    {
        public string LoadedFile { get; private set; }
        public PictureBox SelectedImage { get; private set; }
        #region Constants
        
        #endregion

        public ImageViewerForm(string filename)
        {
            InitializeComponent();
            LoadedFile = filename;
            LoadImages();
        }

        private void LoadImages()
        {
            try
            {
                if (string.IsNullOrEmpty(LoadedFile))
                    throw new Exception("Er is een afbeelding gekozen");
                if (!File.Exists(LoadedFile))
                    throw new Exception($"Afbeelding '{LoadedFile}' bestaat niet");
                var root = Directory.GetParent(LoadedFile);
                xFlowImagePanel.SuspendLayout();
                xFlowImagePanel.Controls.Clear();
                if (root != null)
                {
                    var images = root.GetFiles();
                    foreach (var image in images)
                    {
                        var pc = CreateImageBox(image.FullName);
                        if (pc != null)
                        {
                            xFlowImagePanel.Controls.Add(pc);
                        }
                    }
                    xFlowImagePanel.FlowDirection = FlowDirection.LeftToRight;
                }
                LoadImage(LoadedFile);
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }

            xFlowImagePanel.ResumeLayout(false);
        }

        public void LoadImage(string filename)
        {
            try
            {
                var img = Image.FromFile(filename);
                if (img == null)
                    throw new Exception($"'{filename}' is geen geldige afbeelding");
                xMainImage.Image = img;
                this.Text = $"{Path.GetFileName(filename)}";
                SelectImageBox(filename);
                InitNavigationButton();
                this.Invalidate();
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void SelectImageBox(string filename)
        {
            try
            {
                var fcs = xFlowImagePanel.Controls.Cast<PictureBox>().ToList();
                var fs = fcs.FirstOrDefault(x =>
                    x.Tag is string val && string.Equals(val, filename, StringComparison.CurrentCultureIgnoreCase));
                fcs.ForEach(x => x.BackColor = Color.Transparent);
                if (fs != null)
                {
                    fs.BackColor = Color.AliceBlue;
                    SelectedImage = fs;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public PictureBox CreateImageBox(string filename)
        {
            try
            {
                var img = Image.FromFile(filename);
                if (img == null)
                    throw new Exception($"'{filename}' is geen geldige afbeelding");
                var pc = new PictureBox();
                pc.SizeMode = PictureBoxSizeMode.StretchImage;
                pc.Image = img;
                pc.Size = new Size(128, 96);
                pc.Tag = filename;
                pc.Click += Pc_Click;
                pc.MouseEnter += Pc_MouseEnter;
                pc.MouseLeave += Pc_MouseLeave;
                return pc;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private void Pc_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox pic)
            {
                if (pic.Equals(SelectedImage))
                    pic.BackColor = Color.AliceBlue;
                else pic.BackColor = Color.Transparent;
            }
        }

        private void Pc_MouseEnter(object sender, EventArgs e)
        {
            if (sender is PictureBox pic)
            {
                pic.BackColor = Color.AliceBlue;
            }
        }

        private void Pc_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is Control { Tag: string filename })
                {
                    LoadImage(filename);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void xnavigateleft_Click(object sender, EventArgs e)
        {
            if (!CanNavigateBack()) return;
            var index = xFlowImagePanel.Controls.IndexOf(SelectedImage);
            index--;
            if (index < 0) return;
            var xsel = xFlowImagePanel.Controls[index];
            if (xsel.Tag is string filename)
            {
                LoadImage(filename);
            }
        }

        private void xnavigateright_Click(object sender, EventArgs e)
        {
          
            if (!CanNavigateForward()) return;
            var index = xFlowImagePanel.Controls.IndexOf(SelectedImage);
            index++;
            if (index > xFlowImagePanel.Controls.Count - 1) return;
            var xsel = xFlowImagePanel.Controls[index];
            if (xsel.Tag is string filename)
            {
                LoadImage(filename);
            }
        }

        public bool CanNavigateBack()
        {
            try
            {
                if (xFlowImagePanel.Controls.Count == 0 || SelectedImage == null) return false;
                return xFlowImagePanel.Controls.IndexOf(SelectedImage) > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool CanNavigateForward()
        {
            try
            {
                if (xFlowImagePanel.Controls.Count == 0 || SelectedImage == null) return false;
                return xFlowImagePanel.Controls.IndexOf(SelectedImage) < xFlowImagePanel.Controls.Count -1;
            }
            catch
            {
                return false;
            }
        }

        private void xMainImage_MouseMove(object sender, MouseEventArgs e)
        {
            InitNavigationButton(e);
        }

        private void ImageViewerForm_MouseMove(object sender, MouseEventArgs e)
        {
            InitNavigationButton(e);
        }

        private void InitNavigationButton()
        {
            InitNavigationButton(new MouseEventArgs(MouseButtons.None, 0, MousePosition.X, MousePosition.Y, 0));
        }

        private void InitNavigationButton(MouseEventArgs e)
        {
            try
            {
                var rec =  new Rectangle(0,60,60, xMainImage.Height);
                xleftpanel.Visible = rec.Contains(e.Location) && CanNavigateBack();
                rec = new Rectangle(this.Width - 60, 60,60 , xMainImage.Height);
                xrechtpanel.Visible = rec.Contains(e.Location) && CanNavigateForward();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}