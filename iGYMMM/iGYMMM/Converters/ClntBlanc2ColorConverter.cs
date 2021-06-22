using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace iGYMMM
{
    public class ClntBlanc2ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal d = 0;
            Brush color = null;

            if (decimal.TryParse(value.ToString(), out d))
                color = (d >= 0) ? Brushes.Green : Brushes.Red;

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
