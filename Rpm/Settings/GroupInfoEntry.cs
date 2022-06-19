using System.Drawing;
using System.Windows.Forms;

namespace Rpm.Settings
{
    public class GroupInfoEntry
    {
        public string Name { get; set; }
        public Point Location { get; set; }
        public Size Size { get; set; }
        public FlowDirection TileFlowDirection { get; set; } = FlowDirection.TopDown;
        public int TileViewBackgroundColorRGB { get; set; } = Color.White.ToArgb();
    }
}
