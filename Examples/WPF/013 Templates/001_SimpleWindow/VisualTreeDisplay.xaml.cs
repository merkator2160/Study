using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SimpleWindow
{
    public partial class VisualTreeDisplay : System.Windows.Window
    {
        public VisualTreeDisplay()
        {
            InitializeComponent();
        }

        public void ShowVisualTree(DependencyObject element)
        {
            // ������� ������.
            treeElements.Items.Clear();

            // �������� ������������ �������� ������� � �����.
            ProcessElement(element, null);
        }

        private void ProcessElement(DependencyObject element, TreeViewItem previousItem)
        {
            // �������� TreeViewItem ��� �������� ��������
            TreeViewItem item = new TreeViewItem();
            item.Header = element.GetType().Name;

            // ���������, ����� �� ��������� ������� � ������ ������
            // (���� �� ������), ��� ������� ��� � ��� ������������ item.
            if (previousItem == null)
            {
                treeElements.Items.Add(item);
            }
            else
            {
                previousItem.Items.Add(item);
            }

            // ��������, ����� �� ������� ��������� ��������.
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                // ���������� ������������ ������ ������� ������������ ������ �������.
                ProcessElement(VisualTreeHelper.GetChild(element, i), item);
            }
        }
    }
}

