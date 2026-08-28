using Microsoft.UI.Xaml;
using NotchBox.OS;

namespace NotchBox
{
    public sealed partial class App : Application
    {
        public App()
        {
            NativeMethods.SuppressConsoleWindow();
            this.InitializeComponent();
        }
    }
}
