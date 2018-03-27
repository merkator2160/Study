using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GradientButton
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        bool flag = false;

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            string path = string.Empty;

            if (flag)
            {
                path = "TemplateResources/GradientButton1.xaml";
            }
            else
            {
                path = "TemplateResources/GradientButton2.xaml";
            }

            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri(path, UriKind.Relative);
            this.Resources.MergedDictionaries[0] = resourceDictionary;

            flag = !flag;
        }
    }
}
