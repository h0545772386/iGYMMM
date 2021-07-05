using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iGYMMM1
{
    public partial class EnumsClass
    {
        public string Enum { get; set; }
        public int EnumId { get; set; }
        public string EnumEng { get; set; }
        public string EnumHeb { get; set; }
        public int EnumValue1 { get; set; }
        public int EnumValue2 { get; set; }
        public int EnumValue3 { get; set; }
    }
    public partial class LEnumsClass
    {
        private static List<EnumsClass> _Enums;
        public static List<EnumsClass> Enums
        {
            get
            {
                if (_Enums == null)
                {
                    _Enums = new List<EnumsClass>();
                    Enums.Add(new EnumsClass() { Enum = "Status", EnumId = 1, EnumEng = "Active", EnumHeb = "פעיל" });
                    Enums.Add(new EnumsClass() { Enum = "Status", EnumId = 2, EnumEng = "InActive", EnumHeb = "לא פעיל" });

                    Enums.Add(new EnumsClass() { Enum = "Worhty", EnumId = 1, EnumEng = "1 Star", EnumHeb = "כוכב 1" });
                    Enums.Add(new EnumsClass() { Enum = "Worhty", EnumId = 2, EnumEng = "2 Star", EnumHeb = "כוכב 2" });
                    Enums.Add(new EnumsClass() { Enum = "Worhty", EnumId = 3, EnumEng = "3 Star", EnumHeb = "כוכב 3" });
                    Enums.Add(new EnumsClass() { Enum = "Worhty", EnumId = 4, EnumEng = "4 Star", EnumHeb = "כוכב 4" });
                    Enums.Add(new EnumsClass() { Enum = "Worhty", EnumId = 5, EnumEng = "5 Star", EnumHeb = "כוכב 5" });

                    Enums.Add(new EnumsClass() { Enum = "CRate", EnumId = 1, EnumEng = "1 Star", EnumHeb = "כוכב 1" });
                    Enums.Add(new EnumsClass() { Enum = "CRate", EnumId = 2, EnumEng = "2 Star", EnumHeb = "כוכב 2" });
                    Enums.Add(new EnumsClass() { Enum = "CRate", EnumId = 3, EnumEng = "3 Star", EnumHeb = "כוכב 3" });
                    Enums.Add(new EnumsClass() { Enum = "CRate", EnumId = 4, EnumEng = "4 Star", EnumHeb = "כוכב 4" });
                    Enums.Add(new EnumsClass() { Enum = "CRate", EnumId = 5, EnumEng = "5 Star", EnumHeb = "כוכב 5" });

                    Enums.Add(new EnumsClass() { Enum = "PKGType", EnumId = 1, EnumEng = "2TrnsInWeek", EnumHeb = "2 אימונים בשבוע" });
                    Enums.Add(new EnumsClass() { Enum = "PKGType", EnumId = 1, EnumEng = "3TrnsInWeek", EnumHeb = "3 אימונים בשבוע" });
                    Enums.Add(new EnumsClass() { Enum = "PKGType", EnumId = 1, EnumEng = "4TrnsInWeek", EnumHeb = "4 אימונים בשבוע" });
                    Enums.Add(new EnumsClass() { Enum = "PKGType", EnumId = 1, EnumEng = "BulkTrns", EnumHeb = "מספר טיפולים" });

                    Enums.Add(new EnumsClass() { Enum = "DOW", EnumId = 1, EnumEng = "Sunday", EnumHeb = "ראשון" });
                    Enums.Add(new EnumsClass() { Enum = "DOW", EnumId = 2, EnumEng = "Monday", EnumHeb = "שני" });
                    Enums.Add(new EnumsClass() { Enum = "DOW", EnumId = 3, EnumEng = "Tuesday", EnumHeb = "שלישי" });
                    Enums.Add(new EnumsClass() { Enum = "DOW", EnumId = 4, EnumEng = "Wednesday", EnumHeb = "רביעי" });
                    Enums.Add(new EnumsClass() { Enum = "DOW", EnumId = 5, EnumEng = "Thursday", EnumHeb = "חמישי" });
                    Enums.Add(new EnumsClass() { Enum = "DOW", EnumId = 6, EnumEng = "Friday", EnumHeb = "שישי" });
                    Enums.Add(new EnumsClass() { Enum = "DOW", EnumId = 7, EnumEng = "Saturday", EnumHeb = "שבת" });

                    // morning (06-11)] [Afternoon (12-17)] [Eveinig (18-23)]
                    Enums.Add(new EnumsClass() { Enum = "DayTime", EnumId = 1, EnumEng = "Morning", EnumHeb = "בוקר", EnumValue1 = 6, EnumValue2 = 11, EnumValue3 = 0 });
                    Enums.Add(new EnumsClass() { Enum = "DayTime", EnumId = 2, EnumEng = "Afternoon", EnumHeb = "צהריים", EnumValue1 = 12, EnumValue2 = 17, EnumValue3 = 0 });
                    Enums.Add(new EnumsClass() { Enum = "DayTime", EnumId = 3, EnumEng = "Eveinig", EnumHeb = "ערב", EnumValue1 = 18, EnumValue2 = 23, EnumValue3 = 0 });
                }
                return _Enums;
            }
        }
    }
}