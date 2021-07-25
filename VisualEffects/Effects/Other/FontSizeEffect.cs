using System.Drawing;
using System.Windows.Forms;

namespace ProductieManager.VisualEffects.Effects.Other
{
    public class FontSizeEffect : IEffect
    {
        public EffectInteractions Interaction => EffectInteractions.SIZE;

        public int GetCurrentValue(Control control)
        {
            return (int) control.Font.SizeInPoints;
        }

        public void SetValue(Control control, int originalValue, int valueToReach, int newValue)
        {
            control.Font = new Font(control.Font.FontFamily, newValue);
        }

        public int GetMinimumValue(Control control)
        {
            return 0;
        }

        public int GetMaximumValue(Control control)
        {
            return int.MaxValue;
        }
    }
}