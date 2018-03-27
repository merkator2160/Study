using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace ConverterSample
{
    // Интерфейс для добавления пользовательской логики при привязке данных.
    public class Converter: IValueConverter
    {
        // Parameters:
        //   value:
        //     Значение которое задает источник данных
        //
        //   targetType:
        //     Тип целевого свойства. (Тип который нужно вернуть)
        //
        //   parameter:
        //     Дополнительные параметры
        //
        //   culture:
        //     Культура
        //
        // Returns:
        //     Конвертированное значение.

        /// От источника к цели.
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int number = System.Convert.ToInt32(value);
            switch(number)
            {
                case 0:
                    return "Zero";
                case 1:
                    return "One";
                case 2:
                    return "Two";
                case 3:
                    return "Three";
                case 4:
                    return "Four";
                case 5:
                    return "Five";
                default:
                    return "ERROR";
            }
        }

        // От цели к источнику
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string numberString = value.ToString();
            switch (numberString)
            {
                
                case "Zero":
                    return 0;
                case "One":
                    return 1;
                case "Two":
                    return 2;
                case "Three":
                    return 3;
                case "Four":
                    return 4;
                case "Five":    
                    return 5;
                default:
                    return 0;
            }
        }
    }
}
