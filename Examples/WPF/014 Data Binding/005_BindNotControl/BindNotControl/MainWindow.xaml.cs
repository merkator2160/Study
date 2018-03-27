using System.Windows;

namespace BindNotControl
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class MyData
    {
        public string MyProperty { get; set; }
    }
}
