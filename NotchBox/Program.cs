using System;
using System.Runtime.InteropServices;
using Microsoft.UI.Dispatching;

namespace NotchBox.Bootstrap
{
    public static class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);

        [System.STAThread]
        static void Main(string[] args)
        {
            try
            {
                Microsoft.UI.Xaml.Application.Start((p) =>
                {
                    var context = new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread());
                    System.Threading.SynchronizationContext.SetSynchronizationContext(context);
                    new App();
                });
            }
            catch (Exception ex)
            {
                MessageBox(IntPtr.Zero, ex.ToString(), "NotchBox Runtime Exception", 0x00000010);
            }
        }
    }
}
