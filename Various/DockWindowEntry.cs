using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;

namespace ProductieManager.Various
{
    public class DockWindowEntry
    {
        public int ID { get; set; }
        public DockState DockState { get; set; }
        public int ZOrderIndex { get; set; }
        public List<DockNestedPaneEntry> NestedPaneEntries { get; set; } = new();
    }
}