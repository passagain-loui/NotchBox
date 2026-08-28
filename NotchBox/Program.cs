using System;
using System.Runtime.InteropServices;
using Microsoft.UI.Dispatching;
using Microsoft.Windows.ApplicationModel.DynamicDependency;

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
                // Force initialize Windows App SDK for unpackaged execution
                Microsoft.Windows.ApplicationModel.DynamicDependency.Bootstrap.Initialize(0);
            }
            catch (Exception ex)
            {
                // Force error dialog instead of silent crash
                MessageBox(IntPtr.Zero, ex.ToString(), "NotchBox Bootstrap Exception", 0x00000010);
                return;
            }

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
                MessageBox(IntPtr.Zero, ex.ToString(), "NotchBox UI Start Exception", 0x00000010);
            }
        }
    }
}
