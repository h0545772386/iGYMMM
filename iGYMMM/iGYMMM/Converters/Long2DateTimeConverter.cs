using System;
using System.Globalization;
using System.Windows.Data;

namespace iGYMMM
{
    public class Long2DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value;
            if (v == null)
                return "";
            string d = ((long)v).ToString().ToStringDateTimeFormatStringYYYYMMDD();
            return d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
