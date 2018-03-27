using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace _7DataGridTemplateSelector
{
    public class StatusTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DeliveredTemplate { get; set; }
        public DataTemplate InProgressTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Customer customer = item as Customer;

            if (customer == null)
            {
                return base.SelectTemplate(item, container);
            };

            if (customer.Status == OrderStatus.Delivered)
            {
                return DeliveredTemplate;
            }
            else
            {
                return InProgressTemplate;
            }
        }
    }
}
