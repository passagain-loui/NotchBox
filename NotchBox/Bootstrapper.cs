using System;
using Microsoft.UI.Dispatching;
using Microsoft.Windows.ApplicationModel.DynamicDependency;

namespace NotchBox
{
    public static class Bootstrapper
    {
        [System.STAThread]
        static void Main(string[] args)
        {
            try
            {
                // Force initialize Windows App SDK for Unpackaged execution
                Bootstrap.Initialize(0);
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText("bootstrap_error.log", $"[BOOTSTRAP FAIL] {ex}");
                return;
            }

            Microsoft.UI.Xaml.Application.Start((p) =>
            {
                var context = new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread());
                System.Threading.SynchronizationContext.SetSynchronizationContext(context);
                new App();
            });
        }
    }
}
