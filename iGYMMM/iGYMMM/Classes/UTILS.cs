using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace iGYMMM
{
    public static class UTILS
    {
        public static string DateToString()
        {
            DateTime now = DateTime.Now;
            int Year = now.Year;
            int Month = now.Month;
            int Day = now.Day;

            return Year.ToString() + (Month < 10 ? "0" + Month.ToString() : Month.ToString()) + (Day < 10 ? "0" + Day.ToString() : Day.ToString());
        }

        public static string DateToString_YYYYMMDDHHMM(this DateTime d)
        {
            int Year = d.Year;
            int Month = d.Month;
            int Day = d.Day;
            int Hour = d.Hour;
            int Minute = d.Minute;

            return Year.ToString() +
                   (Month < 10 ? "0" + Month.ToString() : Month.ToString()) +
                   (Day < 10 ? "0" + Day.ToString() : Day.ToString()) +
                   (Hour < 10 ? "0" + Hour.ToString() : Hour.ToString()) +
                   (Minute < 10 ? "0" + Minute.ToString() : Minute.ToString());
        }

        public static string DateToString_YYYYMMDDHHMMSS(this DateTime d)
        {
            int Year = d.Year;
            int Month = d.Month;
            int Day = d.Day;
            if (d.Hour == 0 && d.Minute == 0 && d.Second == 0)
            {
                d = d.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
            }
            int Hour = d.Hour;
            int Minute = d.Minute;
            int Second = d.Second;

            return (Year < 100 ? (2000 + Year).ToString() : Year.ToString() +
                   (Month < 10 ? "0" + Month.ToString() : Month.ToString()) +
                   (Day < 10 ? "0" + Day.ToString() : Day.ToString()) +
                   (Hour < 10 ? "0" + Hour.ToString() : Hour.ToString()) +
                   (Minute < 10 ? "0" + Minute.ToString() : Minute.ToString()) +
                   (Second < 10 ? "0" + Second.ToString() : Second.ToString()));
        }

        public static long DateToLONG_YYYYMMDDHHMMSS(this DateTime d)
        {
            long l = 0;
            long.TryParse(DateToString_YYYYMMDDHHMMSS(d), out l);
            return l;
        }

        public static long DateToLONG_YYYYMMDDHHMMSS_1(this DateTime d)
        {
            long l = 0;
            int Year = d.Year;
            int Month = d.Month;
            int Day = d.Day;
            int Hour = d.Hour;
            int Minute = d.Minute;
            int Second = d.Second;

            string s =
                   (Year < 100 ? (2000 + Year).ToString() : Year.ToString() +
                   (Month < 10 ? "0" + Month.ToString() : Month.ToString()) +
                   (Day < 10 ? "0" + Day.ToString() : Day.ToString()) +
                   (Hour < 10 ? "0" + Hour.ToString() : Hour.ToString()) +
                   (Minute < 10 ? "0" + Minute.ToString() : Minute.ToString()) +
                   (Second < 10 ? "0" + Second.ToString() : Second.ToString()));
            long.TryParse(s, out l);
            return l;
        }

        public static int LONG2INT_YYYYMMDD(this long l_YYYYMMDDHHMMSS)
        {
            return int.Parse(l_YYYYMMDDHHMMSS.ToString().Substring(0, 8));
        }

        public static string int_ToDate_YYYYMMDD(this int YYYYMMDD)
        {
            string result = "{0}/{1}/{2}";
            string str_YYYYMMDD = YYYYMMDD.ToString();
            string Year = str_YYYYMMDD.Substring(0, 4);
            string Month = str_YYYYMMDD.Substring(4, 2);
            string Day = str_YYYYMMDD.Substring(6, 2);

            return string.Format(result, Day, Month, Year);
        }

        public static int DateTo_int_YYYYMMDD(this DateTime d)
        {
            int Year = d.Year;
            Year = Year < 100 ? 2000 + Year : Year;
            int Month = d.Month;
            int Day = d.Day;

            string int_date = Year.ToString() +
                   (Month < 10 ? "0" + Month.ToString() : Month.ToString()) +
                   (Day < 10 ? "0" + Day.ToString() : Day.ToString());

            int date1 = 0;
            int.TryParse(int_date, out date1);
            return date1;
        }

        public static string Int2Date_YYYYMMDD(this int YYYYMMDD)
        {
            if (YYYYMMDD == 0)
                return "";
            string result = "{0}/{1}/{2}";
            string str_YYYYMMDD = YYYYMMDD.ToString();
            string Year = str_YYYYMMDD.Substring(0, 4);
            string Month = str_YYYYMMDD.Substring(4, 2);
            string Day = str_YYYYMMDD.Substring(6, 2);

            return string.Format(result, Day, Month, Year);
        }

        public static int Date2Int_YYYYMMDD(this DateTime d)
        {
            int l = 0;
            int.TryParse(DateToString_YYYYMMDDHHMMSS(d).Substring(0, 8), out l);
            return l;
        }

        public static DateTime Long2Date(this long YYYYMMDDHHMMSS)
        {
            string s = YYYYMMDDHHMMSS.ToString();
            if (s.Length == 8)
                s += "000000";
            if (s.Length == 14)
            {
                int Year = int.Parse(s.Substring(0, 4));
                int Month = int.Parse(s.Substring(4, 2));
                int Day = int.Parse(s.Substring(6, 2));
                int Hour = int.Parse(s.Substring(8, 2));
                int Minute = int.Parse(s.Substring(10, 2));
                int Second = int.Parse(s.Substring(12, 2));
                return new DateTime(Year, Month, Day, Hour, Minute, Second);
            }
            else
                return DateTime.Now;
        }

        public static long Date2Long(this DateTime d)
        {
            long l = 0;
            long.TryParse(DateToString_YYYYMMDDHHMMSS(d), out l);
            return l;
        }

        public static string Long_ToDate_YYYYMMDD(this long YYYYMMDDHHMMSS)
        {
            if (YYYYMMDDHHMMSS == 0)
                return "";
            string result = "{0}/{1}/{2}";
            string str_YYYYMMDD = YYYYMMDDHHMMSS.ToString();
            string Year = str_YYYYMMDD.Substring(0, 4);
            string Month = str_YYYYMMDD.Substring(4, 2);
            string Day = str_YYYYMMDD.Substring(6, 2);

            return string.Format(result, Day, Month, Year);
        }

        public static DateTime int_ToDateTime_YYYYMMDD(this int YYYYMMDD)
        {
            string str_YYYYMMDD = YYYYMMDD.ToString();
            int Year = int.Parse(str_YYYYMMDD.Substring(0, 4));
            int Month = int.Parse(str_YYYYMMDD.Substring(4, 2));
            int Day = int.Parse(str_YYYYMMDD.Substring(6, 2));

            return new DateTime(Year, Month, Day);
        }

        public static int TimeTo_int_HHMMSS(this DateTime d)
        {
            int HH = d.Hour;
            int MM = d.Minute;
            int SS = d.Second;

            string int_time = HH.ToString() +
                   (MM < 10 ? "0" + MM.ToString() : MM.ToString()) +
                   (SS < 10 ? "0" + SS.ToString() : SS.ToString());

            int time1 = 0;
            int.TryParse(int_time, out time1);
            return time1;
        }

        public static string ToStringFromDateTimeYYYYMMDDHHMMSS(this DateTime date)
        {
            string Year = date.Year < 100 ? (2000 + date.Year).ToString() : date.Year.ToString();
            string Month = date.Month < 10 ? "0" + date.Month.ToString() : date.Month.ToString();
            string Day = date.Day < 10 ? "0" + date.Day.ToString() : date.Day.ToString();
            string Hour = date.Hour < 10 ? "0" + date.Hour.ToString() : date.Hour.ToString();
            string Minute = date.Minute < 10 ? "0" + date.Minute.ToString() : date.Minute.ToString();
            string Second = date.Second < 10 ? "0" + date.Second.ToString() : date.Second.ToString();
            return Year + Month + Day + Hour + Minute + Second;
        }

        public static string ToStringFromDateTimeYYYYMMDD(this DateTime date)
        {
            string Year = date.Year < 100 ? (2000 + date.Year).ToString() : date.Year.ToString();
            string Month = date.Month < 10 ? "0" + date.Month.ToString() : date.Month.ToString();
            string Day = date.Day < 10 ? "0" + date.Day.ToString() : date.Day.ToString();
            return Year + Month + Day;
        }

        /// <summary>
        /// return Month as MM string 2 digits
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToStringFromDateTimeYYYYMM(this DateTime date)
        {
            string Year = date.Year < 100 ? (2000 + date.Year).ToString() : date.Year.ToString();
            string Month = date.Month < 10 ? "0" + date.Month.ToString() : date.Month.ToString();
            return Year + Month;
        }

        public static string ToStringFromDateTimeYYYY(this DateTime date)
        {
            string Year = date.Year < 100 ? (2000 + date.Year).ToString() : date.Year.ToString();
            return Year;
        }

        public static string ToStringDateTimeFormatDateTimeYYYYMMDD(this DateTime date)
        {
            string result = "{0}/{1}/{2}";
            string Year = date.Year < 100 ? (2000 + date.Year).ToString() : date.Year.ToString();
            string Month = date.Month < 10 ? "0" + date.Month.ToString() : date.Month.ToString();
            string Day = date.Day < 10 ? "0" + date.Day.ToString() : date.Day.ToString();
            return string.Format(result, Day, Month, Year);

        }

        public static string ToStringDateTimeFormatStringYYYYMMDD(this string date)
        {
            if (date.Length >= 8)
            {
                string result = "{0}/{1}/{2}";
                string Year = date.Substring(0, 4);
                string Month = date.Substring(4, 2);
                string Day = date.Substring(6, 2);
                return string.Format(result, Day, Month, Year);
            }
            else
            {
                return "";
            }
        }

        public static string ToStringDateTimeFormatStringHHMMYYYYMMDD(this string date)
        {
            if (date.Length != 8)
            {
                string result = "{0}/{1}/{2} {3}:{4}";
                string Hour = date.Substring(8, 2);
                string Minute = date.Substring(10, 2);
                string Year = date.Substring(0, 4);
                string Month = date.Substring(4, 2);
                string Day = date.Substring(6, 2);
                return string.Format(result, Day, Month, Year, Hour, Minute);
            }
            else
            {
                string result = "{0}/{1}/{2}";
                string Year = date.Substring(0, 4);
                string Month = date.Substring(4, 2);
                string Day = date.Substring(6, 2);
                return string.Format(result, Day, Month, Year);
            }
        }

        public static string ToStringDateTimeFormatStringHHMM(this string date)
        {
            if (date == "19730207000000")
            {
                return "";
            }

            string result = "{0}:{1}";
            string Hour = date.Substring(8, 2);
            string Minute = date.Substring(10, 2);
            return string.Format(result, Hour, Minute);
        }

        public static string Old_Ver_Postfix(this DateTime date)
        {
            string Year = date.Year < 100 ? (2000 + date.Year).ToString() : date.Year.ToString();
            string Month = date.Month < 10 ? "0" + date.Month.ToString() : date.Month.ToString();
            string Day = date.Day < 10 ? "0" + date.Day.ToString() : date.Day.ToString();
            return "-OLD-" + Day + "." + Month + "." + Year;
        }

        public static DateTime ToDateTimeFROMStringYYYYMMDD(this string date_yyyyMMddHHmmss)
        {
            int Year = int.Parse(date_yyyyMMddHHmmss.Substring(0, 4));
            int Month = int.Parse(date_yyyyMMddHHmmss.Substring(4, 2));
            int Day = int.Parse(date_yyyyMMddHHmmss.Substring(6, 2));

            return new DateTime(Year, Month, Day);
        }

        public static DateTime ToDateTimeFROMStringYYYYMMDDHHMMSS(this string date_yyyyMMddHHmmss)
        {
            int Year = int.Parse(date_yyyyMMddHHmmss.Substring(0, 4));
            int Month = int.Parse(date_yyyyMMddHHmmss.Substring(4, 2));
            int Day = int.Parse(date_yyyyMMddHHmmss.Substring(6, 2));
            int Hour = int.Parse(date_yyyyMMddHHmmss.Substring(8, 2));
            int Minute = int.Parse(date_yyyyMMddHHmmss.Substring(10, 2));
            int Second = int.Parse(date_yyyyMMddHHmmss.Substring(12, 2));

            return new DateTime(Year, Month, Day, Hour, Minute, Second);
        }
        /// <summary>
        /// Convert to DateTime from string YYYYMMDDHHMMSS Or YYYYMMDD
        /// </summary>
        /// <param name="date_yyyyMMddHHmmss"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeFROMLong(this long YYYYMMDDHHMMSS)
        {
            string s = YYYYMMDDHHMMSS.ToString();
            if (s.Length == 8)
            {
                s += "000000";
            }

            if (s.Length == 14)
            {
                int Year = int.Parse(s.Substring(0, 4));
                int Month = int.Parse(s.Substring(4, 2));
                int Day = int.Parse(s.Substring(6, 2));
                int Hour = int.Parse(s.Substring(8, 2));
                int Minute = int.Parse(s.Substring(10, 2));
                int Second = int.Parse(s.Substring(12, 2));
                return new DateTime(Year, Month, Day, Hour, Minute, Second);
            }
            else
            {
                return DateTime.Now;
            }
        }

        public static string Get_DOW_Heb(this DateTime date_)
        {
            string _DayOfWeek = date_.DayOfWeek.ToString();

            if (_DayOfWeek == "Sunday" || _DayOfWeek == "ראשון")
            {
                _DayOfWeek = "ראשון";
            }
            else if (_DayOfWeek == "Monday" || _DayOfWeek == "שני")
            {
                _DayOfWeek = "שני";
            }
            else if (_DayOfWeek == "Tuesday" || _DayOfWeek == "שלישי")
            {
                _DayOfWeek = "שלישי";
            }
            else if (_DayOfWeek == "Wednesday" || _DayOfWeek == "רביעי")
            {
                _DayOfWeek = "רביעי";
            }
            else if (_DayOfWeek == "Thursday" || _DayOfWeek == "חמישי")
            {
                _DayOfWeek = "חמישי";
            }
            else if (_DayOfWeek == "Friday" || _DayOfWeek == "ששי")
            {
                _DayOfWeek = "ששי";
            }
            else if (_DayOfWeek == "Saturday" || _DayOfWeek == "שבת")
            {
                _DayOfWeek = "שבת";
            }

            return _DayOfWeek;
        }

        public static string Get_DOW_Eng(this DateTime date_)
        {
            string _DayOfWeek = date_.DayOfWeek.ToString();

            if (_DayOfWeek == "Sunday" || _DayOfWeek == "ראשון")
            {
                _DayOfWeek = "Sunday";
            }
            else if (_DayOfWeek == "Monday" || _DayOfWeek == "שני")
            {
                _DayOfWeek = "Monday";
            }
            else if (_DayOfWeek == "Tuesday" || _DayOfWeek == "שלישי")
            {
                _DayOfWeek = "Tuesday";
            }
            else if (_DayOfWeek == "Wednesday" || _DayOfWeek == "רביעי")
            {
                _DayOfWeek = "Wednesday";
            }
            else if (_DayOfWeek == "Thursday" || _DayOfWeek == "חמישי")
            {
                _DayOfWeek = "Thursday";
            }
            else if (_DayOfWeek == "Friday" || _DayOfWeek == "ששי")
            {
                _DayOfWeek = "Friday";
            }
            else if (_DayOfWeek == "Saturday" || _DayOfWeek == "שבת")
            {
                _DayOfWeek = "Saturday";
            }

            return _DayOfWeek;
        }

        public static void Integer_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (e.Text.Any(c => !char.IsDigit(c)))
            {
                e.Handled = true;
            }
        }

        public static void Decimal_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (e.Text.Any(c => !char.IsDigit(c) && c != '.'))
            {
                e.Handled = true;
            }
        }

        public static void Decimal1_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (e.Text.Any(c => !char.IsDigit(c) && c != '.' && c != '-'))
            {
                e.Handled = true;
            }
        }

        public static string PrepareOutput(this string s, int len)
        {
            string result = "";
            result = s.ReverseString();
            result.Trim();
            //var arr = s.Split(' ');
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    string txt = arr[i];
            //    if (!Regex.IsMatch(txt, "^[a-zA-Z0-9()./]*$"))
            //    {
            //        txt = txt.ReverseString();
            //    }
            //    arr[i] = txt;
            //}
            //for (int i = arr.Length; i > 0; i--)
            //{
            //    result += arr[i - 1];
            //    result += " ";
            //}
            //if (result.Length > len)
            //{
            //    result = result.Substring(0, len);
            //}

            while (result.Length < len)
            {
                result = result.Insert(0, " ");
            }

            return result;
        }

        public static string ReverseString(this string text)
        {
            if (text == null)
                return "";
            text = text.Replace("(", " ( ");
            text = text.Replace(")", " ) ");
            string HebPat = @"^[.""'אבגדהוזחטיכלמנסעפצקרשתךץףןם]+$";
            string EngPat = @"^[a-zA-Z]+$";
            string DigPat = @"^[0-9]+$";
            List<string> l1 = new List<string>();
            var l = text.Split(' ');
            foreach (var item in l)
            {
                Regex regex = new Regex(HebPat);
                if (regex.IsMatch(item))
                    l1.Add(item.ReverseString1());
                else
                    l1.Add(item);
            }

            string s = "";
            string s1 = "";
            foreach (var item in l1)
            {
                s = item;
                if (s.Length > 0)
                    s += " ";
                s += s1;
                s1 = s;
            }

            string tmp = "";
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] == '(')
                    tmp += ')';
                if (s1[i] == ')')
                    tmp += '(';
                if (s1[i] != '(' && s1[i] != ')')
                    tmp += s1[i];
            }
            s1 = tmp;
            return s1;
        }

        private static string ReverseString1(this string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = string.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
        }

        public static void MoveWithArrows(object sender, KeyEventArgs e, int Times = 1)
        {
            var _FrameworkElement = sender as FrameworkElement;
            for (int i = 0; i < Times; i++)
            {
                if (e.Key == Key.Left || e.Key == Key.Down || e.Key == Key.Enter)
                {
                    _FrameworkElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
                if (e.Key == Key.Right || e.Key == Key.Up)
                {
                    _FrameworkElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
                }
            }
        }

    }
}
