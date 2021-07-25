using System.Windows.Forms;
using ProductieManager.VisualEffects.Animators;
using ProductieManager.VisualEffects.Effects;

namespace ProductieManager.VisualEffects.Extensions
{
    public static class AnimationExtensions
    {
        public static AnimationStatus Animate(this Control control, IEffect iAnimation,
            EasingDelegate easing, int valueToReach, int duration, int delay, bool reverse = false, int loops = 1)
        {
            return Animator.Animate(control, iAnimation, easing, valueToReach, duration, delay, reverse, loops);
        }
    }
}