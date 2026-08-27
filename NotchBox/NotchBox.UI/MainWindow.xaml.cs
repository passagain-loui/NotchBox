using Microsoft.UI.Xaml;
using NotchBox.Core;

namespace NotchBox.UI
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.Title = AppInfo.FullTitle;
        }

        public string GetUIBrandingText()
        {
            return $"{AppInfo.Name} v{AppInfo.Version} | Created by {AppInfo.Author}";
        }
    }
}
