using Microsoft.UI.Xaml;
using NotchBox.Core;
using NotchBox.OS;
using WinRT.Interop;

namespace NotchBox.UI
{
    public sealed partial class NotchShell : Window
    {
        public NotchShell()
        {
            this.InitializeComponent();

            var hWnd = WindowNative.GetWindowHandle(this);
            WindowHooks.ApplyTopMostAndToolWindow(hWnd);

            this.Title = AppInfo.FullTitle;
        }

        public string GetHeaderBranding()
        {
            return $"{AppInfo.Name} v{AppInfo.Version} by {AppInfo.Author}";
        }
    }
}
