using ProductieManager.Controls;
using System.Reflection;
using System.Windows.Forms;

namespace ProductieManager.Classes
{
   public static class Utils
    {
        public static void DoubleBuffered(this Control control, bool enabled)
        {
            var prop = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            prop.SetValue(control, enabled, null);
        }
    }
}
