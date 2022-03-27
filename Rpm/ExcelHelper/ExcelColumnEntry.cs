using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ProductieManager.Rpm.ExcelHelper
{
    public enum CalculationType
    {
        None,
        SOM,
        Gemiddeld
    }

    public enum ColorRuleType
    {
        None,
        Static,
        Dynamic
    }

    public enum SorteerType
    {
        None,
        Ascending,
        Descending
    }

    public class ExcelColumnEntry
    {
        public string Naam { get; set; }
        public int ColumnIndex { get; set; }
        public string ColumnText { get; set; }
        public string ColumnFormat { get; set; }
        public int ColumnBreedte { get; set; } = 150;
        public bool IsVerborgen { get; set; }
        public SorteerType Sorteer { get; set; } = SorteerType.None;
        public bool AutoSize { get; set; } = false;
        public short ColumnColorIndex { get; set; } = -1;
        public int ColomnRGB { get; set; }
        public short ColumnTextColorIndex { get; set; } = -1;
        public int ColomnTextRGB { get; set; }
        public CalculationType Type { get; set; } = CalculationType.None;
        public List<ExcelRegelEntry> KleurRegels { get; set; } = new List<ExcelRegelEntry>();
        public ColorRuleType ColorType { get; set; }

        public static Color GetColorFromIndex(int index)
        {
            try
            {
                var color = IndexedColors.ValueOf(index);
                if (color == null) return Color.Empty;
                return ColorFromRGB(color.RGB);
            }
            catch (Exception e)
            {
                return Color.Empty;
            }

        }

        public static List<Color> GetExcelCollors()
        {
            var xreturn = new List<Color>();
            var props = typeof(IndexedColors).GetFields().Select(x => (IndexedColors)x.GetValue("Index")).ToArray();
            if (props.Length > 0)
            {
                xreturn.AddRange(props.Select(prop => Color.FromArgb(prop.RGB[0], prop.RGB[1], prop.RGB[2])).ToArray());
            }

            return xreturn;
        }

        public static List<int> GetExcelCollorsRGB()
        {
            return GetExcelCollors().Select(x => x.ToArgb()).ToList();
        }

        public static Color ColorFromRGB(byte[] rgb)
        {
            return Color.FromArgb(rgb[0], rgb[1], rgb[2]);
        }

        public static short GetColorIndex(Color color)
        {
            var props = typeof(IndexedColors).GetFields().Select(x => ((IndexedColors)x.GetValue("Index"))).ToArray();
            if (props.Length > 0)
            {
                //var rgb = $"{color.R:X2}{color.G:X2}{color.B:X2}";
                var prop = props.FirstOrDefault(x =>
                    string.Equals(ColorFromRGB(x.RGB).Name, color.Name, StringComparison.CurrentCultureIgnoreCase));
                if (prop == null) return -1;
                var xindex = ((IndexedColors)prop).Index;
                return xindex;
            }

            return -1;
        }

        public ExcelColumnEntry()
        {

        }

        public ExcelColumnEntry(string name)
        {
            Naam = name;
            ColumnText = name;
        }
    }
}
