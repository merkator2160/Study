using System.Windows;

namespace SimpleWindow
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
                
        private void cmd_Click(object sender, RoutedEventArgs e)
        {
            VisualTreeDisplay treeDisplay = new VisualTreeDisplay();
            treeDisplay.ShowVisualTree(this);
            treeDisplay.Show();
        }
    }
}