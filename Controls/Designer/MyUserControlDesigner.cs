using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ProductieManager.Controls.Expandable;

namespace ProductieManager.Controls.Designer
{
    public class MyUserControlDesigner : ParentControlDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            var contentsPanel = ((ExpandablePanel) Control).ContentsPanel;
            EnableDesignMode(contentsPanel, "ContentsPanel");
        }

        public override bool CanParent(Control control)
        {
            return false;
        }

        protected override void OnDragOver(DragEventArgs de)
        {
            de.Effect = DragDropEffects.None;
        }

        protected override IComponent[] CreateToolCore(ToolboxItem tool,
            int x, int y, int width, int height, bool hasLocation, bool hasSize)
        {
            return null;
        }
    }
}