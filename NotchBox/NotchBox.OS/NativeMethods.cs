using System;
using System.Runtime.InteropServices;

namespace NotchBox.OS
{
    public static class NativeMethods
    {
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_TOPMOST = 0x00000008;
        public const int WS_EX_TOOLWINDOW = 0x00000080;
        public const int WS_EX_NOACTIVATE = 0x08000000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public static IntPtr GetWindowLong(IntPtr hWnd, int nIndex) => GetWindowLongPtr(hWnd, nIndex);

        public static void SetWindowLong(IntPtr hWnd, int nIndex, IntPtr value) => SetWindowLongPtr(hWnd, nIndex, value);
    }

    public static class WindowInterop
    {
        private static readonly IntPtr HWND_TOPMOST = new(-1);
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SWP_SHOWWINDOW = 0x0040;

        public static void ApplyTopmostToolWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) return;

            var ex = NativeMethods.GetWindowLong(hWnd, NativeMethods.GWL_EXSTYLE);
            ex = new IntPtr(ex.ToInt64()
                | NativeMethods.WS_EX_TOPMOST
                | NativeMethods.WS_EX_TOOLWINDOW);

            NativeMethods.SetWindowLong(hWnd, NativeMethods.GWL_EXSTYLE, ex);
            NativeMethods.SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE | SWP_SHOWWINDOW);
        }
    }
}
