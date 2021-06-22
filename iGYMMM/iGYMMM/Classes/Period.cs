using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iGYMMM
{
    public class Period
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Date1 { get; set; }
        public int Date2 { get; set; }

        public Period()
        {
            Start = DateTime.Today.AddDays(-1 * DateTime.Today.Day + 1);
            End = DateTime.Today;

            Date1 = Start.DateTo_int_YYYYMMDD();
            Date2 = End.DateTo_int_YYYYMMDD();
        }
    }
}
