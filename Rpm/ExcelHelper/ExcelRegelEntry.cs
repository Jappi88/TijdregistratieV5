using System.Drawing;
using Polenter.Serialization;
using Rpm.Misc;

namespace ProductieManager.Rpm.ExcelHelper
{
    public class ExcelRegelEntry
    {
        public FilterEntry Filter { get; set; }
        public short ColorIndex { get; set; }
        [ExcludeFromSerialization]
        public string lColorName { get; set; }
        public int ColorRGB { get; set; }
        public bool IsFontColor { get; set; }
    }
}
