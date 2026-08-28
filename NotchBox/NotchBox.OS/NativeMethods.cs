using System;
using System.Runtime.InteropServices;

namespace NotchBox.OS
{
    public static class NativeMethods
    {
        public const int WS_EX_TOPMOST = 0x00000008;
        public const int WS_EX_TOOLWINDOW = 0x00000080;
        public const uint WM_COPYGLOBALDATA = 0x0049;
        public const uint MSGFLT_ALLOW = 1;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ChangeWindowMessageFilterEx(IntPtr hWnd, uint message, uint action, IntPtr pChangeFilterStruct);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeConsole();

        public static void SuppressConsoleWindow()
        {
            FreeConsole();
        }
    }
}
