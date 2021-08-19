using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace ProductieManager.Rpm.Misc
{
    public static class GraphicsExtensions
    {
        public static void DrawCircle(this Graphics g, Pen pen,
            float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                radius + radius, radius + radius);
        }

        public static void FillCircle(this Graphics g, Brush brush,
            float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius,
                radius + radius, radius + radius);
        }

        public static byte[] ToByteArray(this Image imageIn)
        {
            using var ms = new MemoryStream();
            imageIn.Save(ms, imageIn.RawFormat);
            return ms.ToArray();
        }

        public static Bitmap ResizeImage(this Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }

            return destImage;
        }

        public static Bitmap CombineImage(this Bitmap a, Bitmap b, double scale)
        {
            return a.CombineImage(b, a.Width, a.Height, ContentAlignment.BottomRight, scale);
        }

        public static Image WriteTextWithCircle(this Image image, string text, ContentAlignment alignment, Font font, Brush textbrush, Brush circlebrush, int radius)
        {
            try
            {
                Point location = GetAlignmentLocation(alignment, image.Size, radius, radius);
                //var rectf = new RectangleF(location.X, location.Y, image.Width, image.Height); //rectf for My Text
                using (var g = Graphics.FromImage(image))
                {
                    //g.DrawRectangle(new Pen(Color.Red, 2), 655, 460, 535, 90); 
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    //g.DrawString(text,
                    //    font,
                    //    brush, location);
                    //I am drawing a oval around my text.
                    //g.DrawArc(new Pen(Color.Red, 3), 90, 235, 150, 50, 0, 360);
                    g.FillCircle(circlebrush, location.X, location.Y, radius);
                    //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    location = GetAlignmentLocation(alignment, image.Size, 1, 0);
                    g.DrawString(text, font, textbrush, location);
                }
            }
            catch
            {
            }

            return image;
        }

        public static Image WriteTextWithCircle(this Image image, string text, ContentAlignment alignment, Font font, Brush textbrush, Brush circlebrush)
        {
            try
            {
                
                //var rectf = new RectangleF(location.X, location.Y, image.Width, image.Height); //rectf for My Text
                using (var g = Graphics.FromImage(image))
                {
                    //g.DrawRectangle(new Pen(Color.Red, 2), 655, 460, 535, 90); 
                    int radius = (int) g.MeasureString(text, font).Width -6;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    //g.DrawString(text,
                    //    font,
                    //    brush, location);
                    //I am drawing a oval around my text.
                    //g.DrawArc(new Pen(Color.Red, 3), 90, 235, 150, 50, 0, 360);
                    Point location = GetAlignmentLocation(alignment, image.Size, radius, radius);
                    g.FillCircle(circlebrush, location.X, location.Y, radius);
                    //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    //location = GetAlignmentLocation(alignment, image.Size, radius / 2, 0);
                    g.DrawString(text, font, textbrush, location.X / 2 , 0);
                }
            }
            catch
            {
            }

            return image;
        }

        public static Bitmap DrawUserCircle(Size PicSize, Brush Brush, string Text, Font font, Color BackColor)
        {

            Bitmap Canvas = new Bitmap(PicSize.Width, PicSize.Height);
            Graphics g = Graphics.FromImage(Canvas);
            

            Rectangle outerRect = new Rectangle(-1, -1, Canvas.Width + 1, Canvas.Height + 1);
            Rectangle rect = Rectangle.Inflate(outerRect, -2, -2);

            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using GraphicsPath path = new GraphicsPath();
            g.FillEllipse(new SolidBrush(BackColor),
                new RectangleF(0, 0, PicSize.Width, PicSize.Height));
                
            path.AddEllipse(rect);
            g.FillPath(Brushes.Transparent, path);

            SizeF stringSize = g.MeasureString(Text, font); //<- Obtiene el tamaño del Texto en pixeles                                                                                 
            int posX = Convert.ToInt32((PicSize.Width - stringSize.Width) / 2); //<- Calcula la posicion para centrar el Texto
            int posY = Convert.ToInt32((PicSize.Height - stringSize.Height) / 2);
                
            g.DrawString(Text, font, Brush, new Point(posX, posY));
            return Canvas;
        }

        public static string Base64Encoded(this Image image, Size size)
        {
            using MemoryStream m = new MemoryStream();
            image.ResizeImage(size.Width, size.Height).Save(m, ImageFormat.Png);
            var imageBytes = m.ToArray();

            // Convert byte[] to Base64 String
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }

        public static string Base64Encoded(this Bitmap image)
        {
            if (image == null) return string.Empty;
            using MemoryStream m = new MemoryStream();
            image.MakeTransparent();
            image.Save(m, ImageFormat.Png);
            var imageBytes = m.ToArray();

            // Convert byte[] to Base64 String
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }

        public static Point GetAlignmentLocation(ContentAlignment alignment, Size bound, int xwidth,int xheight)
        {
            Point point = new Point();
            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                    point = new Point(xwidth, xheight);
                    break;
                case ContentAlignment.TopCenter:
                    point = new Point((bound.Width / 2) + xwidth, xheight);
                    break;
                case ContentAlignment.TopRight:
                    point = new Point(bound.Width - xwidth, xheight);
                    break;
                case ContentAlignment.MiddleLeft:
                    point = new Point(xwidth, (bound.Height / 2) + xheight);
                    break;
                case ContentAlignment.MiddleCenter:
                    point = new Point((bound.Width / 2) + xwidth, (bound.Height / 2) + xheight);
                    break;
                case ContentAlignment.MiddleRight:
                    point = new Point(bound.Width - xwidth, (bound.Height / 2) + xheight);
                    break;
                case ContentAlignment.BottomLeft:
                    point = new Point(xwidth, bound.Height - xheight);
                    break;
                case ContentAlignment.BottomCenter:
                    point = new Point((bound.Width / 2) + xwidth, bound.Height - xheight);
                    break;
                case ContentAlignment.BottomRight:
                    point = new Point(bound.Width - xwidth, bound.Height - xheight);
                    break;
            }

            return point;
        }

        public static Bitmap CombineImage(this Bitmap a, Bitmap b, ContentAlignment contentAlignment, double scale)
        {
            return a.CombineImage(b, a.Width, a.Height, contentAlignment, scale);
        }

        public static Image CombineImage(this Bitmap a, Bitmap b, int width, int height, double scale)
        {
            return a.CombineImage(b, width, height, ContentAlignment.BottomRight, scale);
        }

        public static Image CombineImage(this Bitmap a, Bitmap b, int width, int height)
        {
            return a.CombineImage(b, width, height, ContentAlignment.BottomRight, 1.5);
        }

        public static Bitmap CombineImage(this Bitmap a, Bitmap b, int width, int height, ContentAlignment alignment,
            double scale)
        {
            Bitmap xreturn = a.ResizeImage(width, height);
            b = b.ResizeImage((int)(width / scale), (int)(height / scale));
            try
            {
                var align = new Size(width - b.Width, height - b.Height);
                var cwidth = align.Width;
                var cheight = align.Height;
                switch (alignment)
                {
                    case ContentAlignment.TopLeft:
                        align = new Size(0, 0);
                        break;

                    case ContentAlignment.TopCenter:
                        align = new Size(cwidth / 2, 0);
                        break;

                    case ContentAlignment.TopRight:
                        align = new Size(cwidth, 0);
                        break;

                    case ContentAlignment.MiddleLeft:
                        align = new Size(0, cheight / 2);
                        break;

                    case ContentAlignment.MiddleCenter:
                        align = new Size(cwidth / 2, cheight / 2);
                        break;

                    case ContentAlignment.MiddleRight:
                        align = new Size(cwidth, cheight / 2);
                        break;

                    case ContentAlignment.BottomLeft:
                        align = new Size(0, cheight);
                        break;

                    case ContentAlignment.BottomCenter:
                        align = new Size(cwidth / 2, cheight);
                        break;

                    case ContentAlignment.BottomRight:
                        align = new Size(cwidth, cheight);
                        break;
                }

                using (var g = Graphics.FromImage(xreturn))
                {
                    g.Clear(Color.Transparent);
                    g.DrawImage(a,
                        new Rectangle(0, 0, width, height));

                    g.DrawImage(b,
                        new Rectangle(align.Width, align.Height, b.Width, b.Height));
                }

                return xreturn;
            }
            catch (Exception)
            {
                if (xreturn != null)
                    xreturn.Dispose();

                return null;
            }
        }
    }
}
