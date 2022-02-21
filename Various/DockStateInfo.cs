using System.Collections.Generic;

namespace ProductieManager.Various
{
    public class DockStateInfo
    {
        public double DockLeftPortion { get; set; }
        public double DockRightPortion { get; set; }
        public double DockTopPortion { get; set; }
        public double DockBottomPortion { get; set; }

        public List<DockContentEntry> DockContents { get; set; } = new();
    }
}