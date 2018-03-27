using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace DirectoryTreeView
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            BuildTree();
        }

        private void BuildTree()
        {
            treeFileSystem.Items.Clear();

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                TreeViewItem item = new TreeViewItem();
                item.Tag = drive;
                item.Header = drive.ToString();

                // * отображаться не будет, так как узел находится в закрытом состоянии,
                item.Items.Add("*");
                treeFileSystem.Items.Add(item);
            }
        }

        private void item_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)e.OriginalSource;

            // выполняем обновление каждый раз, когда узел разворачивается.
            item.Items.Clear();

            DirectoryInfo dir;
            if (item.Tag is DriveInfo)
            {
                DriveInfo drive = (DriveInfo)item.Tag;
                dir = drive.RootDirectory;
            }
            else
            {
                dir = (DirectoryInfo)item.Tag;
            }

            try
            {
                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    TreeViewItem newItem = new TreeViewItem();
                    newItem.Tag = subDir;
                    newItem.Header = subDir.ToString();
                    newItem.Items.Add("*");
                    item.Items.Add(newItem);
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void treeFileSystem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TreeViewItem item = treeFileSystem.SelectedItem as TreeViewItem;
            if (item != null)
            {
                DirectoryInfo dirInfo = item.Tag as DirectoryInfo;
                if (dirInfo != null)
                    Title = dirInfo.FullName;
            }
        }
    }
}


