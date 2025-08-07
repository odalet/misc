namespace Delta.Performance.Clocks
{
    // Adapted from https://github.com/dotnet/BenchmarkDotNet/blob/master/src/BenchmarkDotNet.Core/Horology/Frequency.cs     
    internal struct Frequency
    {
        // Adapted from https://github.com/dotnet/BenchmarkDotNet/blob/master/src/BenchmarkDotNet.Core/Horology/FrequencyUnit.cs
        private class Unit
        {
            private Unit(string name, string description, long hertzAmount)
            {
                Name = name;
                Description = description;
                HertzAmount = hertzAmount;
            }

            public static Unit Hz { get; } = new Unit("Hz", "Hertz", 1);
            public static Unit KHz { get; } = new Unit("KHz", "Kilohertz", 1000);
            public static Unit MHz { get; } = new Unit("MHz", "Megahertz", 1000 * 1000);
            public static Unit GHz { get; } = new Unit("GHz", "Gigahertz", 1000 * 1000 * 1000);

            public string Name { get; }
            public string Description { get; }
            public long HertzAmount { get; }

            public Frequency ToFrequency(long value = 1) => new Frequency(HertzAmount);
        }

        public Frequency(double hertz)
        {
            Hertz = hertz;
        }

        public static Frequency Hz { get; } = Unit.Hz.ToFrequency();
        public static Frequency KHz { get; } = Unit.KHz.ToFrequency();
        public static Frequency MHz { get; } = Unit.MHz.ToFrequency();
        public static Frequency GHz { get; } = Unit.GHz.ToFrequency();

        public double Hertz { get; }

        public double ToHz() => this / Hz;
        public double ToKHz() => this / KHz;
        public double ToMHz() => this / MHz;
        public double ToGHz() => this / GHz;

        public static double operator /(Frequency a, Frequency b) => 1.0 * a.Hertz / b.Hertz;
        ////public static Frequency operator /(Frequency a, double k) => new Frequency(a.Hertz / k);
        ////public static Frequency operator *(Frequency a, double k) => new Frequency(a.Hertz * k);
        ////public static Frequency operator *(double k, Frequency a) => new Frequency(a.Hertz * k);
    }
}
