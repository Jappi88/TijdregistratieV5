using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductieManager.Various
{
    public class DockContentEntry
    {
        public int ID { get; set; }
        public string PersistString { get; set; }
        public double AutoHidePortion { get; set; }
        public bool IsHidden { get; set; }
        public bool IsFloat { get; set; }
    }
}
