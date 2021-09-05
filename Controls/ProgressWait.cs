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
            Resize += ProgressWait_Resize;
        }

        private void ProgressWait_Resize(object sender, EventArgs e)
        {
            UpdateProgressSize();
        }

        private int GetFontHeight()
        {
            return (int) Math.Round((double) (xprogress.Width - xprogress.ProgressWidth) / 100 * FontSizePercentage);
        }

        private int GetProgressWidth()
        {
            return (int) Math.Round((double) xprogress.Width / 100 * ProgressWidthPercentage);
        }

        public double FontSizePercentage { get; set; } = 6.21d;
        public double ProgressWidthPercentage { get; set; } = 9.2d;

        public void UpdateProgressSize()
        {
            var minpwidth = 1;
            var maxpwidth = 60;
            var minfwidth = 1;
            var maxfwidth = 60;
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() =>
                {
                    if (xprogress.Height > Width)
                    {
                        xprogress.Height = Width;
                        xprogress.Width = Width;
                    }
                    else
                    {
                        xprogress.Height = Height;
                        xprogress.Width = Height;
                    }

                    xprogress.Location = new Point(Width / 2 - xprogress.Width / 2, Height / 2 - xprogress.Height / 2);
                    var x = GetProgressWidth();
                    xprogress.ProgressWidth = x < minpwidth ? minpwidth : x > maxpwidth ? maxpwidth : x;
                    xprogress.Invalidate();
                    var y = GetFontHeight();
                    xprogress.Font = new Font(xprogress.Font.FontFamily,
                        y < minfwidth ? minfwidth : y > maxfwidth ? maxfwidth : y);
                    xprogress.Invalidate();
                }));
            }
            else
            {
                if (Height > Width)
                {
                    xprogress.Height = Width;
                    xprogress.Width = Width;
                }
                else
                {
                    xprogress.Height = Height;
                    xprogress.Width = Height;
                }

                xprogress.Location = new Point(Width / 2 - xprogress.Width / 2, Height / 2 - xprogress.Height / 2);
                var x = GetProgressWidth();
                xprogress.ProgressWidth = x < minpwidth ? minpwidth : x > maxpwidth ? maxpwidth : x;
                xprogress.Invalidate();
                var y = GetFontHeight();
                xprogress.Font = new Font(xprogress.Font.FontFamily,
                    y < minfwidth ? minfwidth : y > maxfwidth ? maxfwidth : y);
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