using System;
using System.Diagnostics;

namespace Delta.Performance.Clocks
{
    internal class StopwatchClock : IClock
    {
        public Frequency Frequency => new Frequency(Stopwatch.Frequency);
        public long GetTimestamp() => Stopwatch.GetTimestamp();
    }
}
