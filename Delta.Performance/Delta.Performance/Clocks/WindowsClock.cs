using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Delta.Performance.Clocks
{
    // Adapted from https://github.com/dotnet/BenchmarkDotNet/blob/master/src/BenchmarkDotNet.Core/Horology/WindowsClock.cs
    internal class WindowsClock : IClock
    {
        private static readonly long frequency;

        static WindowsClock()
        {
            IsAvailable = Initialize(out frequency);
        }

        public static bool IsAvailable { get; }

        [DllImport("kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long value);

        [DllImport("kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long value);
        
        public Frequency Frequency => new Frequency(frequency);

        public long GetTimestamp()
        {
            long value;
            QueryPerformanceCounter(out value);
            return value;
        }
        
        [HandleProcessCorruptedStateExceptions, SecurityCritical]
        private static bool Initialize(out long qpf)
        {
            try
            {
                long counter;
                return
                    QueryPerformanceFrequency(out qpf) &&
                    QueryPerformanceCounter(out counter);
            }
            catch
            {
                qpf = default(long);
                return false;
            }
        }
    }
}
