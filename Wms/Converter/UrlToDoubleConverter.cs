using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Wms.Converter
{
    public class UrlToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double?) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pattern = @"\A[0-9]{1,9}(?:[.,][0-9]{1," + parameter + @"})?\z";
            if (value != null && Regex.IsMatch(value.ToString(), pattern, RegexOptions.IgnoreCase))
            {
                var isNumber = Double.TryParse(value.ToString().Replace(".", ","), out var number);
                if (!isNumber) 
                    return DependencyProperty.UnsetValue;
                return number > 0 ? number : DependencyProperty.UnsetValue;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
