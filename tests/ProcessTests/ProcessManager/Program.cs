using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using WKill;

namespace ProcessManager
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("> Enter PID to terminate: ");
                var read = Console.ReadLine();
                if (!int.TryParse(read, out int pid))
                {
                    Console.WriteLine("Invalid Value!");
                    continue;
                }

                var rc = Kill(pid, 1000);
                Console.WriteLine($"--> {rc}");
                Console.WriteLine();
            }
        }

        private static ReturnCode Kill(int pid, int timeout)
        {
            using (var wkill = Process.Start(new ProcessStartInfo
            {
                FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wkill.exe"),
                Arguments = $"{pid} {timeout}",
                UseShellExecute = false,
                CreateNoWindow = true,
            }))
            {
                wkill.WaitForExit();
                return (ReturnCode)wkill.ExitCode;
            }
        }
    }
}
