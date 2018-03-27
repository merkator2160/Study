using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace ComboBoxBinding
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<MyItem> items = new List<MyItem>();
            items.Add(new MyItem() { Name = "Яблоко", Picture = "1.jpg" });
            items.Add(new MyItem() { Name = "Апельсин", Picture = "2.jpg" });
            items.Add(new MyItem() { Name = "Ананас", Picture = "3.jpg" });
            items.Add(new MyItem() { Name = "Авокадо", Picture = "4.jpg" });
            items.Add(new MyItem() { Name = "Банан", Picture = "5.jpg" });

            Binding binding = new Binding();
            binding.Source = items;
            
            comboBox1.SetBinding(ComboBox.ItemsSourceProperty, binding);
        }
    }
}
