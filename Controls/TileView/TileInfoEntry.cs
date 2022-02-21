using Polenter.Serialization;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Various;
using System;
using System.Drawing;

namespace Controls
{
    public delegate TileInfoEntry TileChangeEventhandler(Tile tile);

    public enum ResizeMode
    {
        None,
        Custom,
        Auto
    }

    public class TileInfoEntry
    {
        public int TileCount { get; set; }
        public bool EnableTileCount { get; set; } = true;
        public string Name { get; set; }
        public string Text { get; set; }
        public string GroupName { get; set; }
        public int ID { get; set; }
        public int LinkID { get; set; } = -1;
        public Size Size { get; set; } = new Size(256, 128);
        public Size ImageSize { get; set; } = new Size(64, 64);
        public ResizeMode ImageResize { get; set; } = ResizeMode.Auto;
        public bool IsDefault { get; set; }
        public bool IsViewed { get; set; }
        public AccesType AccesLevel { get; set; } = AccesType.AlleenKijken;
        public int TileIndex { get; set; }
        [ExcludeFromSerialization]
        public Color TileColor
        {
            get => Color.FromArgb(TileColorARgb);
            set => TileColorARgb = value.ToArgb();
        }
        [ExcludeFromSerialization]
        public Image SecondaryImage { get; set; }
        public int TileColorARgb { get; set; } = Color.CornflowerBlue.ToArgb();

        [ExcludeFromSerialization]
        public Color ForeColor
        {
            get => Color.FromArgb(ForeColorARgb);
            set => ForeColorARgb = value.ToArgb();
        }

        public int ForeColorARgb { get; set; } = Color.Black.ToArgb();
        public string TextFontFamily { get; set; } = "Segoe UI";
        public int TextFontSize { get; set; } = 14;
        public FontStyle TextFontStyle { get; set; } = FontStyle.Regular;

        public string CountFontFamily { get; set; } = "Segoe UI";
        public int CountFontSize { get; set; } = 20;
        public FontStyle CountFontStyle { get; set; } = FontStyle.Bold;
        [ExcludeFromSerialization]
        public Image TileImage
        {
            get => GetImageFromData();
            set => SetImageDate(value);
        }

        public byte[] ImageData { get; set; }

        public bool SetImageDate(Image image)
        {
            try
            {
                ImageData = image?.ToByteArray();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public Image GetImageFromData()
        {
            if (ImageData == null) return null;
            try
            {
                var img = ImageData.ImageFromBytes();
                if (img != null)
                {
                    switch (ImageResize)
                    {
                        case ResizeMode.Custom:
                            return new Bitmap(img.ResizeImage(ImageSize.Width, ImageSize.Height));
                        case ResizeMode.Auto:
                            return new Bitmap(img.ResizeImage(Size.Width / 4, (int) (Size.Height / 1.5m)));
                    }
                }

                return img;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public TileInfoEntry()
        {
            Name = "xTile";
            ID = Functions.GenerateRandomID();
        }

        public override bool Equals(object obj)
        {
            if (obj is TileInfoEntry entry)
                return entry.ID == ID || string.Equals(Name, entry.Name, StringComparison.CurrentCultureIgnoreCase);
               // return string.Equals(Name, entry.Name, StringComparison.CurrentCultureIgnoreCase);
            return false;
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}
