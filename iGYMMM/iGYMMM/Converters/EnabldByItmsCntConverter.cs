﻿using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace iGYMMM
{
    public class EnabldByItmsCntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((int)value == 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }

    }
}
