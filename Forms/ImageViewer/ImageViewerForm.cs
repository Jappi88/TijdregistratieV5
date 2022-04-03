using Forms.MetroBase;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Various;
using WeifenLuo.WinFormsUI.Docking;

namespace Forms.ImageViewer
{
    public partial class ImageViewerForm : MetroBaseForm
    {
        public string LoadedFile { get; private set; }
        public PictureBox SelectedImage { get; private set; }
        #region Constants
        
        #endregion

        private GlobalMouseHandler _globalMouse;
        public ImageViewerForm(string filename)
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            LoadedFile = filename;
            _globalMouse = new GlobalMouseHandler();
            _globalMouse.MouseMovedEvent += GlobalMouseHandler_MouseMovedEvent;
            Application.AddMessageFilter(_globalMouse);
        }

        private void GlobalMouseHandler_MouseMovedEvent(object sender, MouseEventArgs e)
        {
            InitNavigationButton(e);
        }

        private void frmNewForm_ResizeEnd(object sender, EventArgs e)
        {
            CenterFlowLayoutPanelNavigationControls();
        }

        /// <summary>

        /// ''' Sets the padding-left of the FlowLayoutPanelNavigation to imitate as if the controls are centered

        /// ''' </summary>
        private void CenterFlowLayoutPanelNavigationControls()
        {
            // Get the total width of all Buttons in FlowLayoutPanelNavigation
            int totalControlWidth = xFlowImagePanel.Controls.OfType<PictureBox>().Sum(btn => btn.Width + 5);

            if (totalControlWidth > 0 && totalControlWidth < xFlowImagePanel.Width)
                // If the total width is less than FlowLayoutPanelNavigation then get the difference, divide by 2, and set that as the left-padding
                xFlowImagePanel.Padding = new Padding(Convert.ToInt32(((xFlowImagePanel.Width - totalControlWidth) / (double)2)), 5, 5, 5);
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
                    CenterFlowLayoutPanelNavigationControls();
                }
                LoadImage(LoadedFile);
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }

            xFlowImagePanel.ResumeLayout(true);
        }

        public void LoadImage(string filename)
        {
            try
            {
                var img = Image.FromFile(filename);
               
                if (img == null)
                    throw new Exception($"'{filename}' is geen geldige afbeelding");
               
                if (img.PropertyIdList.Contains(0x0112))
                {
                    PropertyItem propOrientation = img.GetPropertyItem(0x0112);
                    short orientation = BitConverter.ToInt16(propOrientation.Value, 0);
                    if (orientation == 6)
                    {
                        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                    else if (orientation == 8)
                    {
                        img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    }
                }

                bool flag = (img.Width > this.Width - 40 ||
                             img.Height > this.Height - 80);
                xMainImage.AutoScroll = false;
                xMainImage.Image = img.ResizeImage(img.Size);
                img.Dispose();
                if (flag)
                {
                    xMainImage.ZoomToFit();
                }
                else xMainImage.Zoom = 80;
                xMainImage.AutoScroll = true;
                this.Text = $"{Path.GetFileName(filename)}";
                LoadedFile = filename;
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
                    fs.BackColor = Color.DodgerBlue;
                    SelectedImage = fs;
                    xFlowImagePanel.ScrollControlIntoView(fs);
                    fs.Focus();
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
                if (img.PropertyIdList.Contains(0x0112))
                {
                    PropertyItem propOrientation = img.GetPropertyItem(0x0112);
                    short orientation = BitConverter.ToInt16(propOrientation.Value, 0);
                    if (orientation == 6)
                    {
                        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                    else if (orientation == 8)
                    {
                        img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    }
                }

                var pc = new PictureBox();
                pc.SizeMode = PictureBoxSizeMode.StretchImage;
                pc.Image = img.ResizeImage(128, 96);
                img.Dispose();
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
                    pic.BackColor = Color.DodgerBlue;
                else pic.BackColor = Color.Transparent;
                pic.Margin = new Padding(5, 5, 5, 5);
                pic.Invalidate();
            }
        }

        private void Pc_MouseEnter(object sender, EventArgs e)
        {
            if (sender is PictureBox pic)
            {
                pic.BackColor = Color.DodgerBlue;
                pic.Margin = new Padding(5, 10, 5, 10);
                pic.Invalidate();
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

        private void InitNavigationButton()
        {
            InitNavigationButton(new MouseEventArgs(MouseButtons.None, 0, MousePosition.X, MousePosition.Y, 0));
        }

        private void InitNavigationButton(MouseEventArgs e)
        {
            try
            {
                var xloc = this.PointToClient(e.Location);
                bool xb = CanNavigateBack();
                bool xf = CanNavigateForward();
               
                var rec =  new Rectangle(0,60,100, xMainImage.Height);
                xnavigateleft.Visible = rec.Contains(xloc) && xb;
                rec = new Rectangle(this.Width - 60, 60,100 , xMainImage.Height);
                xnavigateright.Visible = rec.Contains(xloc) && xf;
                xleftbutton.Enabled = xb;
                xrechtbutton.Enabled = xf;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void xrotaterecht_Click(object sender, EventArgs e)
        {
            try
            {
                xMainImage.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                xMainImage.Invalidate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void xrotateleft_Click(object sender, EventArgs e)
        {
            try
            {
                xMainImage.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                xMainImage.Invalidate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void xopeninexplorer_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(LoadedFile)) return;
                if (!File.Exists(LoadedFile)) return;
                System.Diagnostics.Process.Start(LoadedFile);
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void ImageViewerForm_Shown(object sender, EventArgs e)
        {
            LoadImages();
        }

        private void ImageViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_globalMouse != null)
            {
                Application.RemoveMessageFilter(_globalMouse);
                _globalMouse = null;
            }
        }
    }
}