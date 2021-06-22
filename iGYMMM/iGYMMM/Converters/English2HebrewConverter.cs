using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace iGYMMM
{
    public class English2HebrewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value;
            if (v == null)
            {
                return "";
            }

            var res = "GlobalContext.Instance.ConstsVars.l_ConstVar.FirstOrDefault(tt => tt.EngConst == v.ToString())";

            return res;//!= null ? res.HebConst : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
