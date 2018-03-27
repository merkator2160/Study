using System.Windows;
using System.Windows.Input;

namespace GridViewSample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Person current = listView1.SelectedItem as Person;
            if (current != null)
            {
                string result = string.Format("{0} {1}\nPosition : {2}\nAge: {3}", 
                    current.FirstName, 
                    current.LastName, 
                    current.Position, 
                    current.Age); 

                MessageBox.Show(result);
            }
        }
    }
}
