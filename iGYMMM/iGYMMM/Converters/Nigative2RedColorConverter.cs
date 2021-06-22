using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace iGYMMM
{
    public class Nigative2RedColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int i = 0;
            decimal d = 0;
            int.TryParse(value.ToString(), out i);
            if (i == 0)
            {
                decimal.TryParse(value.ToString(), out d);
                if (d > 0)
                    i = 1;
                else
                    i = -1;
            }
            if (i < 0)
                return Brushes.Red;
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Brushes.Black;
        }
    }
}
