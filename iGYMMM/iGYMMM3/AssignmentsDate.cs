using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iGYMMM3
{
    public partial class AssignmentsDate
    {
        public int TrnDate { get; set; }

        public List<DiaryTeam> LDiaryTeams { get; set; }
        public List<DiaryInstr> LDiaryInstrs { get; set; }
        public List<DiaryClnt> LDiaryClnts { get; set; }

        public AssignmentsDate()
        {
            LDiaryTeams = new List<DiaryTeam>();
            LDiaryInstrs = new List<DiaryInstr>();
            LDiaryClnts = new List<DiaryClnt>();
        }

    }
}
