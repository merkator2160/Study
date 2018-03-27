using System;
using System.Windows;
using System.Windows.Controls;

namespace BindingToSQL
{
    public partial class MainWindow : Window
    {
        Book _currentBook = null;
        StoreDB _db = new StoreDB();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonGetBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int bookID = Convert.ToInt32(textBoxID.Text);
                _currentBook = _db.GetBook(bookID);
                gridMain.DataContext = _currentBook;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при запросе к базе данных.");
            }
        }

        private void UpdateBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int bookID = Convert.ToInt32(textBoxID.Text);
                _db.UpdateBook(_currentBook, bookID);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при обновлении записи в базе данных.");
            }
        }

        bool isCollapsed = true;
        private void buttonShowList_Click(object sender, RoutedEventArgs e)
        {
            if (isCollapsed)
            {
                listBooks.Visibility = System.Windows.Visibility.Visible;
                try
                {
                    listBooks.ItemsSource = _db.GetAllBooks();
                    listBooks.DisplayMemberPath = "Title";
                }
                catch
                {
                    MessageBox.Show("Ошибка при запросе к базе данных.");
                }
            }
            else
            {
                listBooks.Visibility = System.Windows.Visibility.Collapsed;
            }
            isCollapsed = !isCollapsed;
        }

        private void listBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gridMain.DataContext = listBooks.SelectedItem;
        }
    }
}
