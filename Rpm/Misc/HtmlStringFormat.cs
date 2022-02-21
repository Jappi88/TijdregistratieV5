using System;
using System.Drawing;
using ProductieManager.Rpm.Misc;

namespace Rpm.Misc
{
    public enum HtmlStringAlignment
    {
        None,
        Left,
        Right,
        Centre
    }

    public class HtmlStringFormat
    {
        public Color Color { get; set; }
        public Font Font { get; set; }
        public string Format { get; set; }
        public Image Image { get; set; }
        public HtmlStringAlignment Alignment { get; set; }
        public bool IsHeader { get; set; }

        public string ToHtmlString()
        {
            var align = Alignment == HtmlStringAlignment.None
                ? string.Empty
                : $"text-align:{Enum.GetName(typeof(HtmlStringAlignment), Alignment)};";
            var font = Font ?? new Font("Segoe UI", 12, FontStyle.Regular);
            var color = Color.IsDefault() ? Color.Black : Color;
            var ximage = Image == null
                ? string.Empty
                : "<td width = '32' style = 'padding: 5px 5px 0 0' >\r\n" +
                  $"<img width='{Image.Width}' height='{Image.Height}'  src = 'data:image/png;base64,{((Bitmap) Image).Base64Encoded()}' />\r\n" +
                  "</td>";
            var xtype = IsHeader ? "h1" : "div";
            var xstart =
                $"<{xtype}>{ximage}<span style = 'color:{color.Name}; {align} font-size:{font.SizeInPoints}; font-family:{font.FontFamily.Name}'>";
            var xend = $"</span></{xtype}>";
            switch (font.Style)
            {
                case FontStyle.Regular:
                    break;
                case FontStyle.Bold:
                    xstart += "<b>";
                    xend = "</b>" + xend;
                    break;
                case FontStyle.Italic:
                    xstart += "<i>";
                    xend = "</i>" + xend;
                    break;
                case FontStyle.Underline:
                    xstart += "<u>";
                    xend = "</u>" + xend;
                    break;
                case FontStyle.Strikeout:
                    xstart += "<s>";
                    xend = "</s>" + xend;
                    break;
            }

            var xformat = $"{xstart} {Format.Replace("/n", "<br>")} {xend}";
            return xformat;
        }
    }
}