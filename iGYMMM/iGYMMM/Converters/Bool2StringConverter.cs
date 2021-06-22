using System;
using System.Globalization;
using System.Windows.Data;

namespace iGYMMM
{
    public class Bool2StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value;
            if ((bool)v == true)
            {
                return "כן";
            }
            if ((bool)v == false)
            {
                return "לא";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
