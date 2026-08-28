using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

namespace NotchBox
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void NotchBorder_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            NotchBorder.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(255, 30, 30, 35));
        }

        private void NotchBorder_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            NotchBorder.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(255, 24, 24, 27));
        }
    }
}
