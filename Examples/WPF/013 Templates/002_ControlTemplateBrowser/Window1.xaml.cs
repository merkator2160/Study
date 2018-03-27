using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Reflection;
using System.Xml;
using System.Windows.Markup;

namespace ControlTemplateBrowser
{
    public partial class Window1 : System.Windows.Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, EventArgs e)
        {
            Type controlType = typeof(Control);
            List<Type> derivedTypes = new List<Type>();

            // �������� ��� ���� ������, � ������� �������� ����� Control
            Assembly assembly = Assembly.GetAssembly(controlType);
            foreach (Type type in assembly.GetTypes())
            {
                // ��������� � ������ ������ �� ������, ������� ����������� �� Control �� ����������� � ��������.
                if (type.IsSubclassOf(controlType) && !type.IsAbstract && type.IsPublic)
                {
                    derivedTypes.Add(type);
                }
            }

            // ��������� �� �����.
            derivedTypes.Sort(new TypeComparer());

            // ����������� ��������� � ������.
            lstTypes.ItemsSource = derivedTypes;
        }

        private void lstTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // �������� ��������� ������� � ������ � �������� ��� � ���� Type
                Type type = (Type)lstTypes.SelectedItem;

                // ������� ��������� ���������� ����. (��� ���� ����� ����� ���� �������� XAML �������� �������)
                ConstructorInfo info = type.GetConstructor(System.Type.EmptyTypes);
                Control control = (Control)info.Invoke(null);

                // ���������� �������� � ����, �� ��� ���� ������� �������� ����������.
                control.Visibility = Visibility.Collapsed;
                grid.Children.Add(control);

                // �������� ������ ��������.
                ControlTemplate template = control.Template;

                // �������� XAML �������� ���������� ��������.
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true; // ��� ���������� ��������.
                StringBuilder sb = new StringBuilder();
                XmlWriter writer = XmlWriter.Create(sb, settings);
                XamlWriter.Save(template, writer);

                // ���������� ������� ����������.
                txtTemplate.Text = sb.ToString();

                // �������� �������� �� �����.
                grid.Children.Remove(control);
            }
            catch (Exception err)
            {
                txtTemplate.Text = "<< ������ ������ �������: " + err.Message + ">>";
            }
        }
    }

    public class TypeComparer : IComparer<Type>
    {
        public int Compare(Type x, Type y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}

