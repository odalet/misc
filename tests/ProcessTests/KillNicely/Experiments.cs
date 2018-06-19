using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KillNicelyCmdProg
{
    public static class Experiments
    {
        #region pinvoke


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate HandlerRoutine, bool Add);
        // Delegate type to be used as the Handler Routine for SCCH
        delegate Boolean ConsoleCtrlDelegate(CtrlTypes CtrlType);

        // Enumerated type for the control messages sent to the handler routine
        enum CtrlTypes : uint
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GenerateConsoleCtrlEvent(CtrlTypes dwCtrlEvent, uint dwProcessGroupId);

        private enum ShowCommands
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }
        [DllImport("shell32.dll")]
        static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, ShowCommands nShowCmd);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        }

        [DllImport("kernel32.dll")]
        static extern bool CreateProcess(string lpApplicationName, 
           string lpCommandLine, ref SECURITY_ATTRIBUTES lpProcessAttributes,
           ref SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles,
           uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory,
           [In] ref STARTUPINFO lpStartupInfo,
           out PROCESS_INFORMATION lpProcessInformation);

        private const int WM_VSCROLL = 277;
        private const int SB_BOTTOM = 7;

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CreatePipe(out IntPtr hReadPipe, out IntPtr hWritePipe, ref SECURITY_ATTRIBUTES lpPipeAttributes, uint nSize);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, ShowCommands nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        public delegate bool EnumedWindow(IntPtr handleWindow, ArrayList handles);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumedWindow lpEnumFunc, ArrayList lParam);

        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const int VK_CONTROL = 0x11;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_CHAR = 0x102;
        private const int WM_KEYUP = 0x101;
        private const int VK_CANCEL = 0x03;
        private const int VK_C = 0x0043;
        #endregion pinvoke

        public static Process StartProgramUsingDotNet(RichTextBox output, bool createNoWindow)
        {
            Process RunningRrocess = new Process();
            try
            {
                RunningRrocess.StartInfo.WorkingDirectory = "";
                RunningRrocess.StartInfo.FileName = "ping.exe";
                RunningRrocess.StartInfo.Arguments = "-t 127.0.0.1";
                RunningRrocess.StartInfo.CreateNoWindow = createNoWindow;
                RunningRrocess.StartInfo.UseShellExecute = false;

                RunningRrocess.StartInfo.RedirectStandardError = true;
                RunningRrocess.StartInfo.RedirectStandardOutput = true;
                RunningRrocess.StartInfo.RedirectStandardInput = true;
                RunningRrocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                RunningRrocess.OutputDataReceived += (origin, args) => WriteTextAsync(args.Data, output);
                RunningRrocess.ErrorDataReceived += (origin, args) => WriteTextAsync(args.Data, output);

                RunningRrocess.Start();

                RunningRrocess.BeginOutputReadLine();
                RunningRrocess.BeginErrorReadLine();
            }
            catch (Exception)
            {
            }
            return RunningRrocess;
        }

        public static int StartProgramWithoutWindowUsingPinvoke(RichTextBox output)
        {
            const uint NORMAL_PRIORITY_CLASS = 0x0020;


            string Application = @"c:\windows\system32\ping.exe";
            string CommandLine = " -t 127.0.0.1";
            PROCESS_INFORMATION pInfo;
            STARTUPINFO sInfo = new STARTUPINFO();
            SECURITY_ATTRIBUTES pSec = new SECURITY_ATTRIBUTES();
            SECURITY_ATTRIBUTES tSec = new SECURITY_ATTRIBUTES();
            pSec.nLength = Marshal.SizeOf(pSec);
            tSec.nLength = Marshal.SizeOf(tSec);

            sInfo.wShowWindow = (Int16)ShowCommands.SW_MAXIMIZE;
            
            //Output redirection would go here, using CreatePipe

            bool retValue = CreateProcess(Application, CommandLine, ref pSec, ref tSec, false, NORMAL_PRIORITY_CLASS, IntPtr.Zero, null, ref sInfo, out pInfo);

            return pInfo.dwProcessId; //PID
        }

        public static void StopProgramUsingProcessObjectWithVisibleMainWindow(Process proc)
        {
            //Close the main window of the process. For command-line applications this will
            //generate ControlConsoleEvent, giving them a chance to clean up.
            //The application has only a few milliseconds for clean up, so the method, shown in
            //StopProgramByAttachingToItsConsoleAndIssuingCtrlCEvent() might be preferrable.
            //Note that the window must be visible for this method to work.
            proc.CloseMainWindow();
        }

        public static void StopProgramByKillingIt(Process proc)
        {
            //Brutally kill the process without giving it any chance to clean up.
            proc.Kill();
        }

        public static void StopProgramByAttachingToItsConsoleAndIssuingCtrlCEvent(Process proc)
        {
            //This does not require the console window to be visible.
            if (AttachConsole((uint)proc.Id))
            {
                //Disable Ctrl-C handling for our program
                SetConsoleCtrlHandler(null, true); 
                GenerateConsoleCtrlEvent(CtrlTypes.CTRL_C_EVENT, 0);
                
                //Must wait here. If we don't and re-enable Ctrl-C handling below too fast, we might terminate ourselves.
                proc.WaitForExit();

                FreeConsole();
                
                //Re-enable Ctrl-C handling or any subsequently started programs will inherit the disabled state.
                SetConsoleCtrlHandler(null, false); 
            }
        }

        public static void StopProgramWithInvisibleWindowUsingPinvoke(IntPtr hWnd)
        {
            //TODO: There is indication that something similar to his should work. 
            //TODO: The trick is to find the right message combination.
            PostMessage(hWnd, WM_KEYDOWN, new IntPtr(VK_CONTROL), IntPtr.Zero);
            PostMessage(hWnd, WM_KEYDOWN, new IntPtr('C'), IntPtr.Zero);
            PostMessage(hWnd, WM_KEYUP, new IntPtr('C'), IntPtr.Zero);
            PostMessage(hWnd, WM_KEYUP, new IntPtr(VK_CONTROL), IntPtr.Zero);
        }

        public static IntPtr FindWindowHandleFromProcessObjectWithVisibleWindow(Process proc)
        {
            //MainWindowHandle will be Zero after the process is started until the window is created,
            //so we need to wait for it to turn up.
            //Note also that MainWindowHandle will be Zero as long as the main window is hidden.
            int retries = 1000;
            while (retries > 0)
            {
                if (proc.MainWindowHandle != IntPtr.Zero)
                {
                    return proc.MainWindowHandle;
                }
                retries--;
            }
            return proc.MainWindowHandle;
        }

        public static IntPtr FindWindowHandleFromPid(int pid)
        {
            //The window is not created immediately after the process is started, so we need to try several times
            //to find it among the active windows, until it finally shows up.
            Thread.Sleep(200);
            for (int i = 0; i < 5; i++)
            {
                ArrayList windowList = GetWindows();

                foreach (IntPtr intPtr in windowList)
                {
                    uint foundPid;
                    GetWindowThreadProcessId(intPtr, out foundPid);

                    if (pid == foundPid)
                    {
                        return intPtr;
                    }
                }

                Thread.Sleep(1000);
            }

            return IntPtr.Zero;
        }

        public static void HideCommandWindowUsingPInvoke(IntPtr hWnd)
        {
            //Hide the window. It is still there, though in this case the Process object will
            //start to report Zero instead of the valid window handle.
            ShowWindow(hWnd, ShowCommands.SW_HIDE);
        }

        public static void ShowCommandWindowUsingPInvoke(IntPtr hWnd)
        {
            ShowWindow(hWnd, ShowCommands.SW_SHOWNORMAL);
        }

        public static ArrayList GetWindows()
        {
            ArrayList windowHandles = new ArrayList();
            EnumedWindow callBackPtr = GetWindowHandleHelper;
            EnumWindows(callBackPtr, windowHandles);

            return windowHandles;
        }

        private static bool GetWindowHandleHelper(IntPtr windowHandle, ArrayList windowHandles)
        {
            windowHandles.Add(windowHandle);
            return true;
        }

        private static void WriteTextAsync(string text, RichTextBox output)
        {
            Action action = () =>
            {
                if (text != null)
                {
                    output.AppendText(text + Environment.NewLine);
                    IntPtr ptrWparam = new IntPtr(SB_BOTTOM);
                    IntPtr ptrLparam = new IntPtr(0);
                    SendMessage(output.Handle, WM_VSCROLL, ptrWparam, ptrLparam);
                }
            };
            output.BeginInvoke(action, null);
        }
    }
}
