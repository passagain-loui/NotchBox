using Microsoft.UI.Xaml;
using NotchBox.OS;

namespace NotchBox
{
    public sealed partial class App : Application
    {
        private Microsoft.UI.Xaml.Window m_window;

        public App()
        {
            System.IO.File.AppendAllText("startup_trace.log", $"[{System.DateTime.Now:HH:mm:ss.fff}] State 1: Constructor entry\n");

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                try
                {
                    System.IO.File.WriteAllText("crash_report.log", "[FATAL CRASH] " + e.ExceptionObject?.ToString() ?? "Unknown error");
                }
                catch { }
            };
            this.InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            System.IO.File.AppendAllText("startup_trace.log", $"[{System.DateTime.Now:HH:mm:ss.fff}] State 2: OnLaunched entry\n");

            m_window = new MainWindow();

            System.IO.File.AppendAllText("startup_trace.log", $"[{System.DateTime.Now:HH:mm:ss.fff}] State 3: Before m_window.Activate()\n");
            m_window.Activate();
            System.IO.File.AppendAllText("startup_trace.log", $"[{System.DateTime.Now:HH:mm:ss.fff}] State 4: After m_window.Activate()\n");
        }
    }
}
