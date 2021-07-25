using System;
using System.Windows.Forms;

namespace ProductieManager.VisualEffects.Effects.Color
{
    public class ColorShiftEffect : IEffect
    {
        public EffectInteractions Interaction => EffectInteractions.COLOR;

        public int GetCurrentValue(Control control)
        {
            return control.BackColor.ToArgb();
        }

        public void SetValue(Control control, int originalValue, int valueToReach, int newValue)
        {
            var actualValueChange = Math.Abs(originalValue - valueToReach);
            var currentValue = GetCurrentValue(control);

            var absoluteChangePerc = (double) ((originalValue - newValue) * 100) / actualValueChange;
            absoluteChangePerc = Math.Abs(absoluteChangePerc);

            if (absoluteChangePerc > 100.0f)
                return;

            var originalColor = System.Drawing.Color.FromArgb(originalValue);
            var newColor = System.Drawing.Color.FromArgb(valueToReach);

            var newA = Interpolate(originalColor.A, newColor.A, absoluteChangePerc);
            var newR = Interpolate(originalColor.R, newColor.R, absoluteChangePerc);
            var newG = Interpolate(originalColor.G, newColor.G, absoluteChangePerc);
            var newB = Interpolate(originalColor.B, newColor.B, absoluteChangePerc);

            control.BackColor = System.Drawing.Color.FromArgb(newA, newR, newG, newB);
            Console.WriteLine(control.BackColor + " " + newColor);
        }

        public int GetMinimumValue(Control control)
        {
            return System.Drawing.Color.Black.ToArgb();
        }

        public int GetMaximumValue(Control control)
        {
            return System.Drawing.Color.White.ToArgb();
        }

        private int Interpolate(int val1, int val2, double changePerc)
        {
            var difference = val2 - val1;
            var distance = (int) (difference * (changePerc / 100));
            return val1 + distance;
        }
    }
}