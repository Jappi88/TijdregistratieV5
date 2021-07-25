using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductieManager.Various
{
    public class DockStateInfo
    {
        public double DockLeftPortion { get; set; }
        public double DockRightPortion { get; set; }
        public double DockTopPortion { get; set; }
        public double DockBottomPortion { get; set; }

        public List<DockContentEntry> DockContents { get; set; } = new List<DockContentEntry>();
    }
}
