using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace iGYMMM
{
    public class Category2ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush brush = Brushes.Transparent;
            if (value == null)
                return brush;

            if ((string)value == "")
                return brush;
            try
            {
                brush = (Brush)new BrushConverter().ConvertFromString((string)value);
            }
            catch
            {
                brush = Brushes.Transparent;
            }

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
