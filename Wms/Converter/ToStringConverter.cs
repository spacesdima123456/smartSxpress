using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Wms.Converter
{
    public class ToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = (string)value;
            if (string.IsNullOrWhiteSpace(str))
                return DependencyProperty.UnsetValue;
            return str;
        }
    }
}
