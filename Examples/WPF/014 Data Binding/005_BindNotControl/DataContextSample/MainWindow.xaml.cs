using System.Windows;

namespace DataContextSample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class Person 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
