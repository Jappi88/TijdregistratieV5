using System.Drawing;
using Rpm.Misc;

namespace ProductieManager.Rpm.ExcelHelper
{
    public class ExcelRegelEntry
    {
        public FilterEntry Filter { get; set; }
        public short ColorIndex { get; set; }

        public bool IsFontColor { get; set; }
    }
}
