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
            // очищаем дерево.
            treeElements.Items.Clear();

            // Начинаем обрабатывать элементы начиная с корня.
            ProcessElement(element, null);
        }

        private void ProcessElement(DependencyObject element, TreeViewItem previousItem)
        {
            // Создание TreeViewItem для текущего элемента
            TreeViewItem item = new TreeViewItem();
            item.Header = element.GetType().Name;

            // Проверяем, нужно ли добавлять элемент в корень дерева
            // (если он первый), или вложить его в уже существующий item.
            if (previousItem == null)
            {
                treeElements.Items.Add(item);
            }
            else
            {
                previousItem.Items.Add(item);
            }

            // Проверка, имеет ли элемент вложенные элементы.
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                // Рекурсивно обрабатываем каждый элемент содержащийся внутри данного.
                ProcessElement(VisualTreeHelper.GetChild(element, i), item);
            }
        }
    }
}

