using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace iGYMMM
{
    public class NegativConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Brushes.Black;
            if (value.ToString().Trim() == "")
                return Brushes.Black;

            decimal d = 0;
            decimal.TryParse(value.ToString().Trim(), out d);
            if (d == 0)
                return Brushes.Black;

            string v = value.ToString().Substring(0, value.ToString().IndexOf('.', 0, value.ToString().Length - 1));

            if (int.Parse(v) < 0)
                return Brushes.Red;
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Brushes.Black;
        }
    }
}
