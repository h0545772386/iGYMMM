using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iGYMMM1
{
    public static partial class UTILS
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="pattern">YYYYMMDD or YYYYMMDDHHMM</param>
        /// <returns></returns>
        public static string Date2String(this DateTime date, string pattern = "YYYYMMDD")
        {
            string d = date.Year > 2000 ? date.Year.ToString() : (date.Year + 2000).ToString();
            d += date.Month > 9 ? date.Month.ToString() : "0" + date.Month.ToString();
            d += date.Day > 9 ? date.Day.ToString() : "0" + date.Day.ToString();
            if (pattern == "YYYYMMDDHHMM")
            {
                d += date.Hour > 9 ? date.Hour.ToString() : "0" + date.Hour.ToString();
                d += date.Minute > 9 ? date.Minute.ToString() : "0" + date.Minute.ToString();
            }
            return d;
        }


        /// <summary>
        /// DateTime date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>int YYYYMMDD</returns>
        public static int Date2Int(this DateTime date)
        {
            return Convert.ToInt32(Date2String(date));
        }

        /// <summary>
        /// DateTime date
        /// </summary>
        /// <param name="int_date"></param>
        /// <returns>DateTime</returns>
        public static DateTime Int2Date(this int int_date)
        {
            int year = int_date / 10000;
            int month = (int_date / 100) % 100;
            int day = int_date % 100;
            return new DateTime(year, month, day);
        }


        /// <summary>
        /// DateTime date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>long YYYYMMDDHHMM</returns>
        public static long Date2Long(this DateTime date)
        {
            return long.Parse(Date2String(date, "YYYYMMDDHHMM"));
        }

        /// <summary>
        /// DateTime date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>DateTime</returns>
        public static DateTime Long2Date(this long long_date)
        {
            string long_s = long_date.ToString();   // YYYYMMDDHHMM

            int year = Convert.ToInt32(long_s.Substring(0, 4));
            int month = Convert.ToInt32(long_s.Substring(4, 2));
            int day = Convert.ToInt32(long_s.Substring(6, 2));
            int hour = Convert.ToInt32(long_s.Substring(8, 2));
            int minute = Convert.ToInt32(long_s.Substring(10, 2));
            return new DateTime(year, month, day, hour, minute, 0);

        }
    }
}
