using System;
using Common.Logging;
using Delta.Performance;

namespace TestApp
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            log.Info("BEGIN");
            using (var chrono = ChronoBuilder.New())
            {
                var min = double.MaxValue;
                for (var i = 0; i < 10000; i++)
                {
                    var c = Math.Cos(i / Math.PI);
                    if (c < min) min = c;
                }
            }

            log.Info("END");
            Console.ReadKey();
        }
    }
}
