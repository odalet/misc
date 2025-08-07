using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Performance
{
    // Adapted from https://github.com/dotnet/BenchmarkDotNet/blob/master/src/BenchmarkDotNet.Core/Horology/TimeInterval.cs
    public class Duration
    {
        // Adapted from https://github.com/dotnet/BenchmarkDotNet/blob/master/src/BenchmarkDotNet.Core/Horology/TimeUnit.cs
        private class Unit
        {
            private Unit(string name, string description, long nanosecondAmount)
            {
                Name = name;
                Description = description;
                NanosecondAmount = nanosecondAmount;
            }

            public static Unit Nanosecond { get; } = new Unit("ns", "Nanosecond", 1);
            public static Unit Microsecond { get; } = new Unit("us", "Microsecond", 1000);
            public static Unit Millisecond { get; } = new Unit("ms", "Millisecond", 1000 * 1000);
            public static Unit Second { get; } = new Unit("s", "Second", 1000 * 1000 * 1000);
            public static Unit Minute { get; } = new Unit("m", "Minute", Second.NanosecondAmount * 60);
            public static Unit Hour { get; } = new Unit("h", "Hour", Minute.NanosecondAmount * 60);
            public static Unit Day { get; } = new Unit("d", "Day", Hour.NanosecondAmount * 24);
            
            // Order should be respected from shorter to longer unit
            public static Unit[] All { get; } = { Nanosecond, Microsecond, Millisecond, Second, Minute, Hour, Day }; 

            public string Name { get; }
            public string Description { get; }
            public long NanosecondAmount { get; }

            public static Unit GetBestUnit(params double[] values)
            {
                if (values.Length == 0) return Nanosecond;
                
                // Use the largest unit to display the smallest recorded measurement without loss of precision.
                var minValue = values.Min();
                foreach (var timeUnit in All)
                {
                    if (minValue < timeUnit.NanosecondAmount * 1000)
                        return timeUnit;
                }
                return All.Last();
            }
        }

        public Duration(double nanoseconds)
        {
            Nanoseconds = nanoseconds;
        }

        public double Nanoseconds { get; }
    }
}
