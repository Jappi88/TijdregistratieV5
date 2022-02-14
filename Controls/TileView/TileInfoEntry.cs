using Polenter.Serialization;
using ProductieManager.Rpm.Misc;
using Rpm.Various;
using System;
using System.Drawing;

namespace Controls
{
    public delegate TileInfoEntry TileChangeEventhandler(Tile tile);
    public class TileInfoEntry
    {
        public int TileCount { get; set; }
        public bool EnableTileCount { get; set; } = true;
        public string Name { get; set; }
        public string Text { get; set; }
        public string GroupName { get; set; }
        public Size Size { get; set; } = new Size(256, 128);
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
                return ImageData.ImageFromBytes();
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
        }

        public override bool Equals(object obj)
        {
            if (obj is TileInfoEntry entry)
                return string.Equals(Name, entry.Name, StringComparison.CurrentCultureIgnoreCase);
            return false;
        }

        public override int GetHashCode()
        {
            return Name?.GetHashCode()??0;
        }
    }
}
