using System;
using Common.Logging;
using Delta.Performance.Clocks;

namespace Delta.Performance
{
    public static class ChronoBuilder
    {
        private class CreationSettings
        {
            public static CreationSettings Default { get; } = new CreationSettings
            {
                AutoStart = true,
                LogAction = null,
                ClockBuilder = null
            };

            public bool AutoStart { get; set; }
            public Action<string> LogAction { get; set; }
            public Func<IClock> ClockBuilder { get; set; }
        }

        private static readonly ILog log = LogManager.GetLogger("PERFORMANCES");

        public static Chronometer New() => New(CreationSettings.Default);

        private static Chronometer New(CreationSettings settings)
        {
            var logAction = settings.LogAction ?? GetDefaultLogAction();
            var clockBuilder = settings.ClockBuilder ?? GetBestClock;
            var clock = clockBuilder();

            var chrono = new Chronometer(logAction, clock);

            if (settings.AutoStart)
                chrono.Start();

            return chrono;
        }                

        private static Action<string> GetDefaultLogAction() =>
            text => log.Debug(text);

        private static IClock GetBestClock() =>
            WindowsClock.IsAvailable ? (IClock)new WindowsClock() : (IClock)new StopwatchClock();
    }
}
