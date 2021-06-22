using System;
using System.Globalization;
using System.Windows.Data;

namespace iGYMMM
{
    public class Long2DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value;
            if (v == null)
                return DateTime.Now;
            return ((long)v).Long2Date();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value;
            if (v == null)
                return (long)0;
            return ((DateTime)v).Date2Long();
        }
    }
}
