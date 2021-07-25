using System;
using System.Diagnostics;
using System.Threading;

namespace ProductieManager.VisualEffects.Animators
{
    public class AnimationStatus : EventArgs
    {
        private readonly Stopwatch _stopwatch;

        public AnimationStatus(CancellationTokenSource token, Stopwatch stopwatch)
        {
            CancellationToken = token;
            _stopwatch = stopwatch;
        }

        public long ElapsedMilliseconds => _stopwatch.ElapsedMilliseconds;

        public CancellationTokenSource CancellationToken { get; }
        public bool IsCompleted { get; set; }
    }
}