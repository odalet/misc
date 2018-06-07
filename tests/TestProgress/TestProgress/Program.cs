using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
//using Microsoft.WindowsAPICodePack.Taskbar;

namespace TestProgress
{
    class Program
    {
        static void Main(string[] args)
        {
            NativeImpl();
            //APICodePackImpl();
        }

        static void NativeImpl()
        {
            //var tb = Taskbar.IsSupported ? new Taskbar() : null;

            using (var tb = new Taskbar())
            {

                for (var i = 0; i < 100; i++)
                {
                    Console.Write(".");

                    if (i == 25)
                    {
                        tb.SetProgressState(TaskbarProgressBarStatus.Paused);
                        Thread.Sleep(1000);
                        tb.SetProgressState(TaskbarProgressBarStatus.Normal);
                    }

                    if (i == 50)
                        tb.SetProgressState(TaskbarProgressBarStatus.Error);

                    if (i == 75)
                        tb.SetProgressState(TaskbarProgressBarStatus.Normal);

                    tb.SetProgressValue(i, 100);
                    Thread.Sleep(100);
                }

                tb.SetProgressValue(100, 100);
            }

            Console.ReadKey();
        }
        /*
        static void APICodePackImpl()
        {
            var manager = TaskbarManager.IsPlatformSupported ? TaskbarManager.Instance : null;

            for (var i = 0; i < 100; i++)
            {
                Console.Write(".");

                if (i == 25)
                {
                    manager.SetProgressState(TaskbarProgressBarState.Paused);
                    Thread.Sleep(1000);
                    manager.SetProgressState(TaskbarProgressBarState.Normal);
                }

                if (i == 50)
                    manager.SetProgressState(TaskbarProgressBarState.Error);

                if (i == 75)
                    manager.SetProgressState(TaskbarProgressBarState.Normal);

                manager.SetProgressValue(i, 100);
                Thread.Sleep(100);
            }

            manager.SetProgressValue(100, 100);

            Console.ReadKey();
        }*/
    }

    // See https://dzone.com/articles/using-windows-78-taskbar

    public class Taskbar : IDisposable
    {
        public static bool IsSupported
        {
            get
            {
                // >= Win7
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    return Environment.OSVersion.Version.CompareTo(new Version(6, 1)) >= 0;
                return false;
            }
        }

        public Taskbar()
        {
            if (!IsSupported)
                throw new PlatformNotSupportedException("Windows >= 7");

            var currentProcess = Process.GetCurrentProcess();
            if (currentProcess != null && currentProcess.MainWindowHandle != IntPtr.Zero)
                OwnerHandle = currentProcess.MainWindowHandle;
            else throw new InvalidOperationException("Need a window");

            taskbarList = (ITaskbarList4)new CTaskbarList();
        }

        private IntPtr OwnerHandle { get; }

        private ITaskbarList4 taskbarList;

        public void SetProgressValue(int currentValue, int maximumValue) =>
            taskbarList.SetProgressValue(OwnerHandle, (ulong)currentValue, (ulong)maximumValue);

        public void SetProgressState(TaskbarProgressBarStatus state) =>
            taskbarList.SetProgressState(OwnerHandle, state);

        public void Dispose()
        {
            if (taskbarList == null) return;
            Marshal.ReleaseComObject(taskbarList);
            taskbarList = null;
               }
    }

    [ComImport]
    [Guid("56FDF344-FD6D-11d0-958A-006097C9A090")]
    [ClassInterface(ClassInterfaceType.None)]
    internal class CTaskbarList
    {
    }

    [ComImport]
    [Guid("c43dc798-95d1-4bea-9030-bb99e2983a1a")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ITaskbarList4
    {
        [PreserveSig]
        void HrInit();

        [PreserveSig]
        void AddTab(IntPtr hwnd);

        [PreserveSig]
        void DeleteTab(IntPtr hwnd);

        [PreserveSig]
        void ActivateTab(IntPtr hwnd);

        [PreserveSig]
        void SetActiveAlt(IntPtr hwnd);

        [PreserveSig]
        void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

        [PreserveSig]
        void SetProgressValue(IntPtr hwnd, ulong ullCompleted, ulong ullTotal);

        [PreserveSig]
        void SetProgressState(IntPtr hwnd, TaskbarProgressBarStatus tbpFlags);

        [PreserveSig]
        void RegisterTab(IntPtr hwndTab, IntPtr hwndMDI);

        [PreserveSig]
        void UnregisterTab(IntPtr hwndTab);

        [PreserveSig]
        void SetTabOrder(IntPtr hwndTab, IntPtr hwndInsertBefore);

        [PreserveSig]
        void SetTabActive(IntPtr hwndTab, IntPtr hwndInsertBefore, uint dwReserved);

        [PreserveSig]
        HResult ThumbBarAddButtons(IntPtr hwnd, uint cButtons, [MarshalAs(UnmanagedType.LPArray)] ThumbButton[] pButtons);

        [PreserveSig]
        HResult ThumbBarUpdateButtons(IntPtr hwnd, uint cButtons, [MarshalAs(UnmanagedType.LPArray)] ThumbButton[] pButtons);

        [PreserveSig]
        void ThumbBarSetImageList(IntPtr hwnd, IntPtr himl);

        [PreserveSig]
        void SetOverlayIcon(IntPtr hwnd, IntPtr hIcon, [MarshalAs(UnmanagedType.LPWStr)] string pszDescription);

        [PreserveSig]
        void SetThumbnailTooltip(IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] string pszTip);

        [PreserveSig]
        void SetThumbnailClip(IntPtr hwnd, IntPtr prcClip);

        void SetTabProperties(IntPtr hwndTab, SetTabPropertiesOption stpFlags);
    }

    // Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarStatus
    public enum TaskbarProgressBarStatus
    {
        NoProgress = 0,
        Indeterminate = 1,
        Normal = 2,
        Error = 4,
        Paused = 8
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct ThumbButton
    {
        internal const int Clicked = 6144;

        [MarshalAs(UnmanagedType.U4)]
        internal ThumbButtonMask Mask;

        internal uint Id;

        internal uint Bitmap;

        internal IntPtr Icon;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        internal string Tip;

        [MarshalAs(UnmanagedType.U4)]
        internal ThumbButtonOptions Flags;
    }

    internal enum ThumbButtonMask
    {
        Bitmap = 1,
        Icon,
        Tooltip = 4,
        THB_FLAGS = 8
    }

    [Flags]
    internal enum ThumbButtonOptions
    {
        Enabled = 0,
        Disabled = 1,
        DismissOnClick = 2,
        NoBackground = 4,
        Hidden = 8,
        NonInteractive = 0x10
    }

    public enum HResult
    {
        Ok,
        False,
        InvalidArguments = -2147024809,
        OutOfMemory = -2147024882,
        NoInterface = -2147467262,
        Fail = -2147467259,
        ElementNotFound = -2147023728,
        TypeElementNotFound = -2147319765,
        NoObject = -2147221019,
        Win32ErrorCanceled = 1223,
        Canceled = -2147023673,
        ResourceInUse = -2147024726,
        AccessDenied = -2147287035
    }

    internal enum SetTabPropertiesOption
    {
        None,
        UseAppThumbnailAlways,
        UseAppThumbnailWhenActive,
        UseAppPeekAlways = 4,
        UseAppPeekWhenActive = 8
    }
}
