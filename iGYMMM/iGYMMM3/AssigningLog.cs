using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iGYMMM3
{
    public partial class AssigningLog
    {
        public int Id { get; set; }
        public DateTime AssignTime { get; set; }
        public int TrnTmId { get; set; }
        public string TrnTmName { get; set; }
        public int TmGrpId { get; set; }
        public int InstrId { get; set; }  // planned
        public int InstrId1 { get; set; } // assigned
        public int PkgId { get; set; }
        public int PkgReqId { get; set; }
        public string ErrorText { get; set; }
        public string ActionText { get; set; }
        public bool OkCode { get; set; }
    }
}
