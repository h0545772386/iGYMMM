using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iGYMMM1
{
    public partial class DIARY
    {
        List<AssigningLog> LAL;    // logs for new diary
        List<DiaryHeader> LDH_db;   // diary saved at database
        List<DiaryHeader> LDHnew;  // diary that assigned now - to be saved at DB 

        public DIARY()
        {
            LAL = new List<AssigningLog>();
            LDH_db = new List<DiaryHeader>();
            LDHnew = new List<DiaryHeader>();
        }
    }
}
