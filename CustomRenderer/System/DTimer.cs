using System.Diagnostics;
namespace SummerWave.Renderer.System
{
    public class DTimer
    {
        private Stopwatch _stopWatch;
        private float m_ticksPerMs;
        private long m_LastFrameTime = 0;

        public float FrameTime { get; private set; }
        public float CumulativeFrameTime { get; private set; }
        public void Initialize()
        {
            _stopWatch = Stopwatch.StartNew();
            m_ticksPerMs = (float)(Stopwatch.Frequency / 1000.0f);
        }
        public void Frame2()
        {
            long currentTime = _stopWatch.ElapsedTicks;
            float timeDifference = currentTime - m_LastFrameTime;
            FrameTime = timeDifference / m_ticksPerMs;
            CumulativeFrameTime += FrameTime;
            m_LastFrameTime = currentTime;
        }
    }
}