using System;
using System.Windows;

namespace HhScraper.Views
{
    public partial class FancyPopup
    {
        public static readonly DependencyProperty ClickCountProperty = DependencyProperty.Register("ClickCount",
                typeof(Int32),
                typeof(FancyPopup),
                new FrameworkPropertyMetadata(0));

        public FancyPopup()
        {
            InitializeComponent();
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public Int32 ClickCount
        {
            get
            {
                return (int)GetValue(ClickCountProperty);
            }
            set
            {
                SetValue(ClickCountProperty, value);
            }
        }


        // HANDLERS ///////////////////////////////////////////////////////////////////////////////
        private void OnButtonClick(Object sender, RoutedEventArgs e)
        {
            ClickCount++;
        }
    }
}