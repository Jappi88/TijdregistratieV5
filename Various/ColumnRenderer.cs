﻿using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BrightIdeasSoftware;

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
            using (var gradient = new LinearGradientBrush(r, Color.Gold, Color.Fuchsia, 0.0))
            {
                g.FillRectangle(gradient, r);
            }

            var fmt = new StringFormat(StringFormatFlags.NoWrap);
            fmt.LineAlignment = StringAlignment.Center;
            fmt.Trimming = StringTrimming.EllipsisCharacter;
            switch (Column.TextAlign)
            {
                case HorizontalAlignment.Center:
                    fmt.Alignment = StringAlignment.Center;
                    break;
                case HorizontalAlignment.Left:
                    fmt.Alignment = StringAlignment.Near;
                    break;
                case HorizontalAlignment.Right:
                    fmt.Alignment = StringAlignment.Far;
                    break;
            }

            g.DrawString(GetText(), Font, TextBrush, r, fmt);
        }
    }
}