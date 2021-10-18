using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Wms.Converter
{
    public class ToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int?)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            if (!Regex.IsMatch((string)value, @"^[0-9]+$", RegexOptions.IgnoreCase))
                return DependencyProperty.UnsetValue;
            // ReSharper disable once PossibleInvalidCastException
            var val = System.Convert.ToInt32(value);
            if (val>0)
                return System.Convert.ToInt32(value);
            return DependencyProperty.UnsetValue;
        }
    }
}
