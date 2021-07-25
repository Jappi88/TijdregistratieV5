using System.Windows.Forms;

namespace ProductieManager.VisualEffects.Effects.Bounds
{
    public class YLocationEffect : IEffect
    {
        public int GetCurrentValue(Control control)
        {
            return control.Top;
        }

        public void SetValue(Control control, int originalValue, int valueToReach, int newValue)
        {
            control.Top = newValue;
        }

        public int GetMinimumValue(Control control)
        {
            return int.MinValue;
        }

        public int GetMaximumValue(Control control)
        {
            return int.MaxValue;
        }

        public EffectInteractions Interaction => EffectInteractions.Y;
    }
}