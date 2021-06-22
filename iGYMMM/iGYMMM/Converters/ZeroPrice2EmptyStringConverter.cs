using System;
using System.Globalization;
using System.Windows.Data;

namespace iGYMMM
{
    public class ZeroPrice2EmptyStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value;
            if (v == null || (decimal)value == 0)
                return "";
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (string)value == "")
                return 0;

            return decimal.Parse(value.ToString());
        }
    }
}
