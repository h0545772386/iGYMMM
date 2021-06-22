using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace iGYMMM
{
    class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.ToString().Length < 8) return DateTime.Now.Date;
            if (int.TryParse(value.ToString(), out int val))
            {
                int year = val / 10000;
                int month = (val / 100) % 100;
                int day = val % 100;
                DateTime date = new DateTime(year, month, day);
            }
            return DateTime.Now.Date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.ToString() == "")
                return 0;
            if(DateTime.TryParse(value.ToString(), out DateTime dt))
            {
                string str = dt.Year.ToString("D4") + dt.Month.ToString("D2") + dt.Day.ToString("D2");
                return int.Parse(str);
            }
            return 0;

        }
    }
}
