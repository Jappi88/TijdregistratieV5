using System.Drawing;
using Polenter.Serialization;
using Rpm.Misc;

namespace ProductieManager.Rpm.ExcelHelper
{
    public class ExcelRegelEntry
    {
        public FilterEntry Filter { get; set; }
        public short ColorIndex { get; set; } = -1;
        public int ColorRGB { get; set; }
        public bool IsFontColor { get; set; }
    }
}
