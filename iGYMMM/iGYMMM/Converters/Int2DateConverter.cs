using System;
using System.Globalization;
using System.Windows.Data;

namespace iGYMMM
{
    public class int2DateTimeConverter : IValueConverter
    {
        public object Convert ( object value, Type targetType, object parameter, CultureInfo culture )
        {
            var v = value;
            if (v == null || (int)value == 0)
                return "";
            string d = ( (int)v ).int_ToDate_YYYYMMDD();
            return d;
        }

        public object ConvertBack ( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return "";
        }
    }
}
