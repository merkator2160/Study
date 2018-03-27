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

            // Получаем все типы сборки, в которой объявлен класс Control
            Assembly assembly = Assembly.GetAssembly(controlType);
            foreach (Type type in assembly.GetTypes())
            {
                // Добавляем в список только те классы, которые производные от Control не абстрактные и открытые.
                if (type.IsSubclassOf(controlType) && !type.IsAbstract && type.IsPublic)
                {
                    derivedTypes.Add(type);
                }
            }

            // Сортируем по имени.
            derivedTypes.Sort(new TypeComparer());

            // Привязываем коллекцию к списку.
            lstTypes.ItemsSource = derivedTypes;
        }

        private void lstTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Получаем выбранный элемент в списке и приводим его к типу Type
                Type type = (Type)lstTypes.SelectedItem;

                // Создаем экземпляр выбранного типа. (для того чтобы можно было получить XAML разметку шаблона)
                ConstructorInfo info = type.GetConstructor(System.Type.EmptyTypes);
                Control control = (Control)info.Invoke(null);

                // Добавление элемента в грид, но при этом элемент остается спрятанным.
                control.Visibility = Visibility.Collapsed;
                grid.Children.Add(control);

                // Получаем шаблон контрола.
                ControlTemplate template = control.Template;

                // Получаем XAML разметку выбранного элемента.
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true; // для добавления отступов.
                StringBuilder sb = new StringBuilder();
                XmlWriter writer = XmlWriter.Create(sb, settings);
                XamlWriter.Save(template, writer);

                // Отображаем элемент управления.
                txtTemplate.Text = sb.ToString();

                // Удаление элемента из грида.
                grid.Children.Remove(control);
            }
            catch (Exception err)
            {
                txtTemplate.Text = "<< Ошибка чтения шаблона: " + err.Message + ">>";
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

