using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace ProductieManager.Various
{
    public class DockNestedPaneEntry
    {
        public int ID { get; set; }
        public int RefID { get; set; }
        public int PrevPane { get; set; }
        public DockAlignment Alignment { get; set; }
        public double Proportion { get; set; }
    }
}
