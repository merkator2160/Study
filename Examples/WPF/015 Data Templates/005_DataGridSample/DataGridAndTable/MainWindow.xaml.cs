using System.Windows;
using System.Collections;
using System.Data;
using System.Windows.Controls;
using System.Collections.Generic;

namespace DataGridAndTable
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            grid1.ItemsSource = CreateTable().DefaultView;
        }

        DataTable CreateTable()
        {
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[] { new DataColumn("ID"), new DataColumn("FirstName"), new DataColumn("Last Name") });
            table.LoadDataRow(new object[] { "1", "Jhon", "Doe" }, true);
            table.LoadDataRow(new object[] { "2", "Kate", "Khone" }, true);
            table.LoadDataRow(new object[] { "3", "Tom", "Ronald" }, true);
            return table;
        }

        private void grid1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataRowView selectedRow = grid1.SelectedItem as DataRowView;

            if (selectedRow == null)
            {
                return;
            }

            string result = string.Empty;
            for (int i = 0; i < 3; ++i)
            {
                result += selectedRow.Row[i] + " ";
            }
            MessageBox.Show(result);
        }
    }
}
