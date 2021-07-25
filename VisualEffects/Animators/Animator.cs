using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using ProductieManager.VisualEffects.Effects;
using Timer = System.Timers.Timer;

namespace ProductieManager.VisualEffects.Animators
{
    public static class Animator
    {
        /*I decided to add no abstraction over animators and provide bare functionality.
         *I think nothing is better than a static method here.*/

        public static event EventHandler<AnimationStatus> Animated;

        /// <summary>
        ///     Animate a control property from its present value to a target one
        /// </summary>
        /// <param name="control">Target control</param>
        /// <param name="iEffect">Effect to apply</param>
        /// <param name="easing">Easing function to apply</param>
        /// <param name="valueToReach">Target value reached when animation completes</param>
        /// <param name="duration">Amount of time taken to reach the target value</param>
        /// <param name="delay">Amount of delay to apply before animation starts</param>
        /// <param name="reverse">
        ///     If set to true, animation reaches target value and animates back to initial value. It takes 2*
        ///     <paramref name="duration" />
        /// </param>
        /// <param name="loops">
        ///     If reverse is set to true, indicates how many loops to perform. Negatives or zero mean infinite
        ///     loop
        /// </param>
        /// <returns></returns>
        public static AnimationStatus Animate(Control control, IEffect iEffect,
            EasingDelegate easing, int valueToReach, int duration, int delay,
            bool reverse = false, int loops = 1)
        {
            //used to calculate animation frame based on how much time has effectively passed
            var stopwatch = new Stopwatch();

            //used to cancel animation
            var cancelTokenSource = new CancellationTokenSource();

            //used to access animation progress
            var animationStatus = new AnimationStatus(cancelTokenSource, stopwatch);

            //This timer allows delayed start. Control's state checks and evaluations are delayed too.
            new System.Threading.Timer(state =>
            {
                //is there anything to do here?
                var originalValue = iEffect.GetCurrentValue(control);
                if (originalValue == valueToReach)
                {
                    animationStatus.IsCompleted = true;
                    return;
                }

                //upper bound check
                var maxVal = iEffect.GetMaximumValue(control);
                if (valueToReach > maxVal)
                {
                    var msg = string.Format("Value must be lesser than the maximum allowed. " +
                                            "Max: {0}, provided value: {1}", maxVal, valueToReach);

                    throw new ArgumentException(msg, "valueToReach");
                }

                //lower bound check
                var minVal = iEffect.GetMinimumValue(control);
                if (valueToReach < iEffect.GetMinimumValue(control))
                {
                    var msg = string.Format("Value must be greater than the minimum allowed. " +
                                            "Min: {0}, provided value: {1}", minVal, valueToReach);

                    throw new ArgumentException(msg, "valueToReach");
                }

                var reversed = false;
                var performedLoops = 0;

                var actualValueChange = Math.Abs(originalValue - valueToReach);

                var animationTimer = new Timer();
                //adjust interval (naive, edge cases can mess up)
                animationTimer.Interval =
                    duration > actualValueChange ? duration / actualValueChange : actualValueChange;

                //because of naive interval calculation this is required
                if (iEffect.Interaction == EffectInteractions.COLOR)
                    animationTimer.Interval = 10;

                //main animation timer tick
                animationTimer.Elapsed += (o, e2) =>
                {
                    //cancellation support
                    if (cancelTokenSource.Token.IsCancellationRequested)
                    {
                        animationStatus.IsCompleted = true;
                        animationTimer.Stop();
                        stopwatch.Stop();
                        return;
                    }

                    //main logic
                    var increasing = originalValue < valueToReach;

                    var minValue = Math.Min(originalValue, valueToReach);
                    var maxValue = Math.Abs(valueToReach - originalValue);
                    var newValue = (int) easing(stopwatch.ElapsedMilliseconds, minValue, maxValue, duration);

                    if (!increasing)
                        newValue = originalValue + valueToReach - newValue - 1;
                    try
                    {
                        control.BeginInvoke(new MethodInvoker(() =>
                        {
                            iEffect.SetValue(control, originalValue, valueToReach, newValue);

                            var timeout = stopwatch.ElapsedMilliseconds >= duration;
                            if (timeout)
                            {
                                if (reverse && (!reversed || loops <= 0 || performedLoops < loops))
                                {
                                    reversed = !reversed;
                                    if (reversed)
                                        performedLoops++;

                                    var initialValue = originalValue;
                                    var finalValue = valueToReach;

                                    valueToReach = valueToReach == finalValue ? initialValue : finalValue;
                                    originalValue = valueToReach == finalValue ? initialValue : finalValue;

                                    stopwatch.Restart();
                                    animationTimer.Start();
                                }
                                else
                                {
                                    animationStatus.IsCompleted = true;
                                    animationTimer.Stop();
                                    stopwatch.Stop();

                                    Animated?.Invoke(control, animationStatus);
                                }
                            }
                        }));
                    }
                    catch
                    {
                    }
                };

                //start
                stopwatch.Start();
                animationTimer.Start();
            }, null, delay, Timeout.Infinite);

            return animationStatus;
        }
    }
}