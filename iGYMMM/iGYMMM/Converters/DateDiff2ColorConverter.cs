using System;
using System.Globalization;
using System.Windows.Data;

namespace iGYMMM
{
    public class DateDiff2ColorConverter : IValueConverter
    {
        public object Convert ( object value, Type targetType, object parameter, CultureInfo culture )
        {
            var v = value;
            if (v == null)
            {
                return "Black";
            }

            TimeSpan ts = DateTime.Now - ( (long)v ).ToString().ToDateTimeFROMStringYYYYMMDD();
            //if (ts.Days > Properties.Settings.Default.TotalDays)
            //    return "Red";
            //else
                return "Black";
        }

        public object ConvertBack ( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return "";
        }
    }
}
