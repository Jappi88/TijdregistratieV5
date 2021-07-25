using System;
using System.Drawing;
using System.Windows.Forms;

namespace Controls
{
    public partial class ProgressWait : UserControl
    {
        public ProgressWait()
        {
            InitializeComponent();
            this.Resize += ProgressWait_Resize;
        }

        private void ProgressWait_Resize(object sender, EventArgs e)
        {
            UpdateProgressSize();
        }

        private int GetFontHeight()
        {
            return (int)Math.Round(((double)(xprogress.Width -  xprogress.ProgressWidth) / 100) * FontSizePercentage);
        }

        private int GetProgressWidth()
        {
            return (int) Math.Round(((double) xprogress.Width / 100) * ProgressWidthPercentage);
        }

        public double FontSizePercentage { get; set; } = 6.21d;
        public double ProgressWidthPercentage { get; set; } = 9.2d;

        public void UpdateProgressSize()
        {
            int minpwidth = 1;
            int maxpwidth = 60;
            int minfwidth = 1;
            int maxfwidth = 60;
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    if (xprogress.Height > this.Width)
                    {
                        xprogress.Height = this.Width;
                        xprogress.Width = this.Width;
                    }
                    else
                    {
                        xprogress.Height = this.Height;
                        xprogress.Width = this.Height;
                    }

                    xprogress.Location = new Point((this.Width / 2) - (xprogress.Width / 2), (this.Height / 2) - (xprogress.Height / 2));
                    int x = GetProgressWidth();
                    xprogress.ProgressWidth = x < minpwidth ? minpwidth : x > maxpwidth ? maxpwidth : x;
                    xprogress.Invalidate();
                    int y = GetFontHeight();
                    xprogress.Font = new Font(xprogress.Font.FontFamily, y < minfwidth ? minfwidth : y > maxfwidth ? maxfwidth : y);
                    xprogress.Invalidate();
                }));
            }
            else
            {
                if (this.Height > this.Width)
                {
                    xprogress.Height = this.Width;
                    xprogress.Width = this.Width;
                }
                else
                {
                    xprogress.Height = this.Height;
                    xprogress.Width = this.Height;
                }

                xprogress.Location = new Point((this.Width / 2) - (xprogress.Width / 2), (this.Height / 2) - (xprogress.Height / 2));
                int x = GetProgressWidth();
                xprogress.ProgressWidth = x < minpwidth ? minpwidth : x > maxpwidth ? maxpwidth : x;
                xprogress.Invalidate();
                int y = GetFontHeight();
                xprogress.Font = new Font(xprogress.Font.FontFamily, y < minfwidth ? minfwidth : y > maxfwidth ? maxfwidth : y);
                xprogress.Invalidate();
            }
        }

        public ProgressBarStyle Style
        {
            get => xprogress.Style;
            set => xprogress.Style = value;
        }

        public Color ProgressBarColor
        {
            get => xprogress.ProgressColor;
            set => xprogress.ProgressColor = value;
        }
        
        public string ProgressText
        {
            get => xprogress.Text;
            set => xprogress.Text = value;
        }
    }
}
