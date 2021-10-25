using BrightIdeasSoftware;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Various
{
    public class ColumnRenderer : BaseRenderer
    {

        public override bool RenderSubItem(DrawListViewSubItemEventArgs e, Graphics g, Rectangle cellBounds, object x)
        {
            //do you own stuff here

            //default rendering
            return base.RenderSubItem(e, g, cellBounds, x);
        }

        public override void Render(Graphics g, Rectangle r)
        {
            using (LinearGradientBrush gradient = new LinearGradientBrush(r, Color.Gold, Color.Fuchsia, 0.0))
            {
                g.FillRectangle(gradient, r);
            }
            StringFormat fmt = new StringFormat(StringFormatFlags.NoWrap);
            fmt.LineAlignment = StringAlignment.Center;
            fmt.Trimming = StringTrimming.EllipsisCharacter;
            switch (this.Column.TextAlign)
            {
                case HorizontalAlignment.Center: fmt.Alignment = StringAlignment.Center; break;
                case HorizontalAlignment.Left: fmt.Alignment = StringAlignment.Near; break;
                case HorizontalAlignment.Right: fmt.Alignment = StringAlignment.Far; break;
            }
            g.DrawString(this.GetText(), this.Font, this.TextBrush, r, fmt);
        }
    }
}
