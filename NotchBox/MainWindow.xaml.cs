using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;

namespace NotchBox
{
    public sealed partial class MainWindow : Window
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ChangeWindowMessageFilterEx(IntPtr hWnd, uint msg, uint action, IntPtr pChangeFilterStruct);

        private const uint WM_DROPFILES = 0x0233;
        private const uint WM_COPYDATA = 0x004A;
        private const uint MSGFLT_ALLOW = 1;

        public MainWindow()
        {
            this.InitializeComponent();
            ConfigureAsFloatingNotch();
        }

        private void ConfigureAsFloatingNotch()
        {
            try
            {
                this.ExtendsContentIntoTitleBar = true;
                this.SetTitleBar(null);

                IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

                // Safe P/Invoke execution with exception isolation
                if (hWnd != IntPtr.Zero)
                {
                    try
                    {
                        ChangeWindowMessageFilterEx(hWnd, WM_DROPFILES, MSGFLT_ALLOW, IntPtr.Zero);
                        ChangeWindowMessageFilterEx(hWnd, WM_COPYDATA, MSGFLT_ALLOW, IntPtr.Zero);
                        ChangeWindowMessageFilterEx(hWnd, 0x0049, MSGFLT_ALLOW, IntPtr.Zero);
                    }
                    catch (Exception ex)
                    {
                        LogDiagnostic($"UIPI Bypass Non-Fatal: {ex.GetType().Name}: {ex.Message}");
                    }
                }

                var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
                var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

                if (appWindow != null)
                {
                    var presenter = appWindow.Presenter as Microsoft.UI.Windowing.OverlappedPresenter;
                    if (presenter != null)
                    {
                        presenter.IsAlwaysOnTop = true;
                        presenter.IsResizable = false;
                        presenter.IsMinimizable = false;
                        presenter.IsMaximizable = false;
                        presenter.SetBorderAndTitleBar(false, false);
                    }
                    appWindow.IsShownInSwitchers = false;

                    var displayArea = Microsoft.UI.Windowing.DisplayArea.GetFromWindowId(windowId, Microsoft.UI.Windowing.DisplayAreaFallback.Primary);
                    if (displayArea != null)
                    {
                        int width = 420;
                        int height = 50;
                        int x = (displayArea.WorkArea.Width - width) / 2;
                        appWindow.MoveAndResize(new Windows.Graphics.RectInt32(x, 0, width, height));
                    }
                }
            }
            catch (Exception ex)
            {
                LogDiagnostic($"ConfigureAsFloatingNotch Critical: {ex.GetType().Name}: {ex.Message}");
            }
        }

        private static void LogDiagnostic(string message)
        {
            try
            {
                string logDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NotchBox");
                Directory.CreateDirectory(logDir);
                string logPath = Path.Combine(logDir, "runtime_diag.log");
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                File.AppendAllText(logPath, $"[{timestamp}] {message}\n");
            }
            catch { }
        }

        private void RootGrid_DragOver(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                e.AcceptedOperation = DataPackageOperation.Copy;
                e.DragUIOverride.Caption = "Drop to Shelf";
                e.DragUIOverride.IsCaptionVisible = true;

                NotchCard.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(245, 15, 23, 42));
                NotchCard.BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 229, 255));
                StatusText.Text = "Release to Drop";
                StatusText.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
                DropBadge.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 229, 255));
                BadgeText.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 15, 23, 42));
            }
        }

        private void RootGrid_DragLeave(object sender, DragEventArgs e)
        {
            ResetUIState();
        }

        private async void RootGrid_Drop(object sender, DragEventArgs e)
        {
            ResetUIState();

            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                IReadOnlyList<IStorageItem> items = await e.DataView.GetStorageItemsAsync();
                if (items.Count > 0)
                {
                    StatusText.Text = $"Stored {items.Count} item(s)";
                    StatusText.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 52, 211, 153));
                }
            }
        }

        private void ResetUIState()
        {
            NotchCard.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(238, 15, 23, 42));
            NotchCard.BorderBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(51, 255, 255, 255));
            StatusText.Text = "Drop files here";
            StatusText.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 209, 213, 219));
            DropBadge.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 30, 41, 59));
            BadgeText.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 148, 163, 184));
        }
    }
}
