using System.Drawing;
using System.Windows.Forms;

namespace Controls
{
    public partial class CustomImageBox : PictureBox
    {
        public CustomImageBox()
        {
            InitializeComponent();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
            // Paint background with underlying graphics from other controls
        {
            base.OnPaintBackground(e);
            return;
            var g = e.Graphics;

            if (Parent != null)
            {
                // Take each control in turn
                var index = Parent.Controls.GetChildIndex(this);
                for (var i = Parent.Controls.Count - 1; i > index; i--)
                {
                    var c = Parent.Controls[i];

                    // Check it's visible and overlaps this control
                    if (c.Bounds.IntersectsWith(Bounds) && c.Visible)
                    {
                        // Load appearance of underlying control and redraw it on this background
                        var bmp = new Bitmap(c.Width, c.Height, g);
                        c.DrawToBitmap(bmp, c.ClientRectangle);
                        g.TranslateTransform(c.Left - Left, c.Top - Top);
                        g.DrawImageUnscaled(bmp, Point.Empty);
                        g.TranslateTransform(Left - c.Left, Top - c.Top);
                        bmp.Dispose();
                    }
                }
            }
        }
    }
}