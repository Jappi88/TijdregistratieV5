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