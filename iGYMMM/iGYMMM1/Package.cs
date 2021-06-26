namespace iGYMMM1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Package
    {
        [Key]
        public int PkgId { get; set; }

        [Required]
        [StringLength(200)]
        public string PkgName { get; set; }

        [Required]
        [StringLength(200)]
        public string PkgType { get; set; }

        public int GymId { get; set; }

        public int TrnTmId { get; set; }

        public int PkDateStart { get; set; }

        public int PkDateStart1 { get; set; }

        public int PkDateEnd { get; set; }

        public bool IsPeriodFixed { get; set; }

        public int PkTrnAmount { get; set; }

        public int PkTrnAmountWeek { get; set; }

        public decimal TotalFee1 { get; set; }

        public decimal TotalFee2 { get; set; }

        public bool AllGrpPymntDone { get; set; }

        [Required]
        [StringLength(100)]
        public string Status { get; set; }

        public int CreatedBy { get; set; }

        public long CreatedAt { get; set; }

        public int ChangedBy { get; set; }

        public long ChangedAt { get; set; }


        [NotMapped]
        public List<PkgPayment> LPayments { get; set; }

        [NotMapped]
        public List<PkgRequrmnt> LRequriments { get; set; }



        [NotMapped]
        public List<DiaryClnt> LDiaryClients { get; set; }

        [NotMapped]
        public List<DiaryInstr> LDiaryInstructors { get; set; }

        [NotMapped]
        public List<DiaryTeam> DiaryTeams { get; set; }

        [NotMapped]
        public List<TrnTmPackage> TeamPackages { get; set; }
        

        public Package()
        {
            LPayments = new List<PkgPayment>();
            LRequriments = new List<PkgRequrmnt>();


            DiaryTeams = new List<DiaryTeam>();
            LDiaryInstructors = new List<DiaryInstr>();
            LDiaryClients = new List<DiaryClnt>();

            TeamPackages = new List<TrnTmPackage>();
        }
    }
}
