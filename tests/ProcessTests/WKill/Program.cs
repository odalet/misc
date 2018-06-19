using System.Diagnostics;

namespace WKill
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            if (args.Length == 0)
                return (int)ReturnCode.BadArguments;

            if (!uint.TryParse(args[0], out var pid))
                return (int)ReturnCode.BadArguments;

            // Do we have a timeout?
            if (args.Length > 1)
            {
                if (!int.TryParse(args[1], out int timeout))
                    return (int)ReturnCode.BadArguments;

                return (int)Kill(pid, timeout);
            }

            return (int)Kill(pid, -1); // no timeout
        }

        private static ReturnCode Kill(uint pid, int timeout)
        {
            Process process = null;
            try
            {
                process = Process.GetProcessById((int)pid);
            }
            catch
            {
                return ReturnCode.InvalidPid;
            }

            using (process)
            {
                // In case we have our own console, we must first detach from it. If not the call will fail, but it is ok.
                // see https://stackoverflow.com/questions/40059902/attachconsole-error-5-access-is-denied
                NativeApi.FreeConsole();

                // Let's attach ourself to the target process console (if any)
                // NB: This does not require the console window to be visible.
                if (!NativeApi.AttachConsole(pid))
                    return ReturnCode.AttachConsoleFailure;

                // Disable Ctrl-C handling for our program
                NativeApi.SetConsoleCtrlHandler(null, true);
                
                // Then send Ctrl+C to the target process
                NativeApi.GenerateConsoleCtrlEvent(CtrlTypes.CTRL_C_EVENT, 0);

                // Must wait here. Just to be sure the target process was indeed killed
                if (process.WaitForExit(timeout))
                    return ReturnCode.OK;

                // Well... we couldn't kill the target process. Let's be brutal
                process.Kill();
                return ReturnCode.Forced;
            }
        }
    }
}
