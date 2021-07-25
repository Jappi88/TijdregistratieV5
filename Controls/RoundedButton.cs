using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Controls
{
    public partial class RoundedButton : Button
    {
        public RoundedButton()
        {
            InitializeComponent();
        }

        public RoundedButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public int Radius { get; set; } = 25;
        public Color BorderColor { get; set; } = Color.DodgerBlue;
        public float BorderThicknes { get; set; } = 1.5f;

        private GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        {
            var r2 = radius / 2f;
            var GraphPath = new GraphicsPath();
            GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
            GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
            GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
            GraphPath.AddArc(Rect.X + Rect.Width - radius,
                Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
            GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
            GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
            GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);
            GraphPath.CloseFigure();
            return GraphPath;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var Rect = new RectangleF(0, 0, Width, Height);
            using (var GraphPath = GetRoundPath(Rect, Radius))
            {
                Region = new Region(GraphPath);
                using (var pen = new Pen(BorderColor, BorderThicknes))
                {
                    pen.Alignment = PenAlignment.Center;
                    e.Graphics.DrawPath(pen, GraphPath);
                }
            }
        }
    }
}