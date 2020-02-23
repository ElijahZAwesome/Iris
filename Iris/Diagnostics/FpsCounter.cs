using SdlSharp;

namespace Iris.Diagnostics
{
    public class FpsCounter
    {
        private const int MaxSnapshots = 60;
        
        private readonly uint[] _frameTimes;
        private uint _lastFrameTime;

        public uint TotalFrameCount { get; private set; }
        public float AverageFps { get; private set; }

        internal FpsCounter()
        {
            _frameTimes = new uint[MaxSnapshots];
        }

        internal void Update()
        {
            var index = TotalFrameCount % MaxSnapshots;
            var ticks = Timer.Ticks;

            _frameTimes[index] = ticks - _lastFrameTime;
            _lastFrameTime = ticks;

            TotalFrameCount++;

            var count = TotalFrameCount < MaxSnapshots ? (int)TotalFrameCount 
                                                       : MaxSnapshots;

            for (var i = 0; i < count; i++)
                AverageFps += _frameTimes[i];

            AverageFps /= count;
            AverageFps = 1000f / AverageFps;
        }
    }
}