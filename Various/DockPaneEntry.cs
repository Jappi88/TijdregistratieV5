using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace ProductieManager.Various
{
    public class DockPaneEntry
    {
        public int ID { get; set; }
        public DockState DockState { get; set; }
        public int ActiveContent { get; set; }
        public List<DockPaneContentEntry> DockPaneContent { get; set; } = new List<DockPaneContentEntry>();
    }
}
