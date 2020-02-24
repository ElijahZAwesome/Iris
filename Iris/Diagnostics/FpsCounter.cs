using SFML.System;

namespace Iris.Diagnostics
{
    public class FpsCounter
    {
        private Clock Clock { get; }
        
        public ulong TotalFrameCount { get; private set; }
        public float FramesPerSecond { get; private set; }

        internal FpsCounter()
        {
            Clock = new Clock();
        }

        internal void Update()
        {
            FramesPerSecond = 1f / Clock.ElapsedTime.AsSeconds();
            Clock.Restart();

            TotalFrameCount++;
        }
    }
}