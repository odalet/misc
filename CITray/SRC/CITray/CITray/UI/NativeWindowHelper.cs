using System;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CITray.UI
{
    // Inspiration:
    // - http://www.codeproject.com/KB/cs/cssingprocess.aspx
    //
    // License: creative commons - Copyright-Only Dedication (based on United States law) or Public Domain Certification:
    //
    // The person or persons who have associated work with this document (the "Dedicator" or "Certifier") 
    // hereby either (a) certifies that, to the best of his knowledge, the work of authorship identified 
    // is in the public domain of the country from which the work is published, or (b) hereby dedicates 
    // whatever copyright the dedicators holds in the work of authorship identified below (the "Work") 
    // to the public domain. A certifier, moreover, dedicates any copyright interest he may have in the
    // associated work, and for these purposes, is described as a "dedicator" below.
    //
    // A certifier has taken reasonable steps to verify the copyright status of this work. Certifier 
    // recognizes that his good faith efforts may not shield him from liability if in fact the work certified 
    // is not in the public domain.
    //
    // Dedicator makes this dedication for the benefit of the public at large and to the detriment of the Dedicator's 
    // heirs and successors. Dedicator intends this dedication to be an overt act of relinquishment in perpetuity of all 
    // present and future rights under copyright law, whether vested or contingent, in the Work. Dedicator understands that 
    // such relinquishment of all rights includes the relinquishment of all rights to enforce (by lawsuit or otherwise) 
    // those copyrights in the Work.
    //
    // Dedicator recognizes that, once placed in the public domain, the Work may be freely reproduced, distributed, 
    // transmitted, used, modified, built upon, or otherwise exploited by anyone for any purpose, commercial or non-commercial, 
    // and in any way, including by methods that have not yet been invented or conceived.
    internal class NativeWindowHelper
    {
        /// <summary>
        /// Restores the main window on the first launched CITray process.
        /// </summary>
        public static void RestoreMainWindow()
        {
            try
            {
                var runningProcess = Process.GetCurrentProcess();

                // Using Process.ProcessName does not function properly when
                // the actual name exceeds 15 characters. Using the assembly
                // name takes care of this quirk and is more accurate than 
                // other work arounds.
                var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                foreach (var process in Process.GetProcessesByName(assemblyName))
                {
                    //ignore "this" process
                    if (process.Id == runningProcess.Id) continue;

                    // Found a "same named process".
                    // Assume it is the one we want brought to the foreground.
                    // Now find the window in this process that has the correct title.

                    var helper = new NativeWindowHelper();
                    IntPtr hwnd = helper.FindNamedWindow(
                        process.Id, ThisAssembly.MainWindowTitle);
                    if (hwnd == IntPtr.Zero) break;

                    if (IsIconic(hwnd) || !IsWindowVisible(hwnd))
                        ShowWindowAsync(hwnd, SW_RESTORE);
                    SetForegroundWindow(hwnd);

                    break;
                }
            }
            catch (Exception ex)
            {
                // something wrong happened... never mind.
                var debugException = ex;
            }
        }

        public static void RestoreWindow(IntPtr hwnd)
        {
            SendMessage(hwnd, WM_SYSCOMMAND, SC_RESTORE, 0);
        }

        #region Interop definitions

        private const int SC_RESTORE = 0xF120;
        private const int WM_SYSCOMMAND = 0x112;
        private const int SW_RESTORE = 9;
        private delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            int _left;
            int _top;
            int _right;
            int _bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WINDOWINFO
        {
            public uint cbSize;
            public RECT rcWindow;
            public RECT rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public ushort atomWindowType;
            public ushort wCreatorVersion;

            // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
            public WINDOWINFO(Boolean? filler)
                : this()
            {
                cbSize = (UInt32)(System.Runtime.InteropServices.Marshal.SizeOf(typeof(WINDOWINFO)));
            }
        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(HandleRef handle, out int processId);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumThreadWindowsCallback callback, IntPtr extraData);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindow(HandleRef hWnd, int uCmd);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(HandleRef hWnd);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);

        #endregion

        private IntPtr match = IntPtr.Zero;
        private int processId = 0;
        private string windowTitle = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeWindowHelper"/> class.
        /// </summary>
        private NativeWindowHelper() { }

        /// <summary>
        /// Finds a window of the specified process by its text property.
        /// </summary>
        /// <param name="pid">The process id.</param>
        /// <param name="title">The title of the window to find.</param>
        /// <returns>Zero handle if not found; otherwise, the found window handle.</returns>
        private IntPtr FindNamedWindow(int pid, string title)
        {
            match = IntPtr.Zero;
            processId = pid;
            windowTitle = title;
            EnumThreadWindowsCallback callback = (hwnd, _) =>
            {
                int tpid;
                GetWindowThreadProcessId(new HandleRef(this, hwnd), out tpid);
                if ((tpid == processId) && IsMatch(hwnd))
                {
                    match = hwnd;
                    return false;
                }
                return true;
            };
            EnumWindows(callback, IntPtr.Zero);
            GC.KeepAlive(callback);

            return match;
        }

        /// <summary>
        /// Determines whether the specified window is a match.
        /// </summary>
        /// <param name="hwnd">The window handle.</param>
        /// <returns>
        /// 	<c>true</c> if the specified window is a match; otherwise, <c>false</c>.
        /// </returns>
        private bool IsMatch(IntPtr hwnd)
        {
            var info = new WINDOWINFO(null);
            GetWindowInfo(hwnd, ref info);

            var title = GetWindowTitle(hwnd);
            return string.CompareOrdinal(title, windowTitle) == 0;
        }

        /// <summary>
        /// Gets the window title for the specified window handle.
        /// </summary>
        /// <param name="hwnd">The window handle.</param>
        /// <returns>This window's text.</returns>
        private string GetWindowTitle(IntPtr hwnd)
        {
            // Allocate correct string length first.
            int length = GetWindowTextLength(hwnd);
            var sb = new StringBuilder(length + 1);

            // Then get text.
            GetWindowText(hwnd, sb, sb.Capacity);
            return sb.ToString();
        }
    }
}
