using System;
using Microsoft.UI.Dispatching;

namespace NotchBox.Bootstrap
{
    public static class Program
    {
        [System.STAThread]
        static void Main(string[] args)
        {
            Microsoft.UI.Xaml.Application.Start((p) =>
            {
                var context = new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread());
                System.Threading.SynchronizationContext.SetSynchronizationContext(context);
                new App();
            });
        }
    }
}
