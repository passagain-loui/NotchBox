using System;
using System.IO;
using Microsoft.UI.Xaml;
using NotchBox.OS;

namespace NotchBox
{
    public sealed partial class App : Application
    {
        private MainWindow? m_window;

        private static readonly string s_logDir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NotchBox");

        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                try
                {
                    Directory.CreateDirectory(s_logDir);
                    File.WriteAllText(Path.Combine(s_logDir, "crash_report.log"),
                        $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [FATAL CRASH] {e.ExceptionObject ?? "Unknown error"}");
                }
                catch { }
            };

            this.InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }
    }
}
