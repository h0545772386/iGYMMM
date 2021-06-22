using System;
using System.Globalization;
using System.Windows.Data;

namespace iGYMMM
{
    public class Int2DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value;
            if (v == null || (int)value == 0)
                return DateTime.Now;
            return ((int)v).Int2Date_YYYYMMDD();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value;
            if (v == null)
                return (int)0;
            return ((DateTime)v).Date2Int_YYYYMMDD();
        }
    }
}
