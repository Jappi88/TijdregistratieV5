using System.Drawing;
using System.Windows.Forms;

namespace Rpm.Settings
{
    public class LastFormScreenInfo
    {
        public string Name { get; set; }
        public Size Size { get; set; }
        public Point Location { get; set; }
        public FormWindowState WindowState { get; set; }
    }
}
