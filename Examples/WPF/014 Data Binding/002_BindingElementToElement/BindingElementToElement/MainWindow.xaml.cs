using System.Windows;

namespace BindingElementToElement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void cmd_SetSmall(object sender, RoutedEventArgs e)
        {
            lblSampleText.FontSize = 2;
        }

        private void cmd_SetNormal(object sender, RoutedEventArgs e)
        {
            lblSampleText.FontSize = this.FontSize;
        }

        private void cmd_SetLarge(object sender, RoutedEventArgs e)
        {
            // Работает только в режиме two-way.
            lblSampleText.FontSize = 30;
        }
    }
}
