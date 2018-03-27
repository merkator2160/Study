using System.Collections.Generic;
using System.Windows;
using System.Collections.ObjectModel;
using System;
using System.Windows.Controls;

namespace UsingCollections
{
    public partial class MainWindow : Window
    {
        List<string> list = new List<string>();
        //ObservableCollection<string> list = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();

            string[] arr = {"First", "Second", "Third", "Fourth" };
            foreach (string item in arr)
            {
                list.Add(item);
            }
            
            listBox1.ItemsSource = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Новое значение добавляется в коллекцию но ListBox его не отобразит.
            // Для того, чтобы контрол реагировал на изменения коллекции,
            // Коллекция, которая используется как ItemsSource, должна реализовывать интерфейс INotifyCollectionChanged.
            // В WPF есть только одна коллекция, которая реализующая этот интерфейс ObservableCollection<T>
            list.Add(DateTime.Now.ToLongTimeString());
        }
    }
}
