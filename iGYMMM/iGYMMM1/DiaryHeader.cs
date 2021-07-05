namespace iGYMMM1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DiaryHeader
    {
        [Key]
        public int DryHdrId { get; set; }

        public int GymId { get; set; }

        public int TrnTmId { get; set; }

        public int TmGrpId { get; set; }

        public int PkgId { get; set; }

        public int PkgReqId { get; set; }

        public int TrnDate { get; set; }

        public int TrnHour { get; set; }

        [StringLength(200)]
        public string PkReqDOW { get; set; }

        [StringLength(200)]
        public string PkReqDayTime { get; set; }

        public int PkReqHour1 { get; set; }

        public int PkReqHour2 { get; set; }

        public int PkReqTrnTime { get; set; }

        [StringLength(200)]
        public string ActualTrnDOW { get; set; }

        [StringLength(200)]
        public string ActualTrnDayTime { get; set; }

        public int ActualTrnHour1 { get; set; }

        public int ActualTrnHour2 { get; set; }

        public int ActualTrnTrnTime { get; set; }

        public int PlannedInstrId { get; set; }

        public int ActualInstrId { get; set; }

        public decimal PerHour1 { get; set; }

        public decimal PerHour2 { get; set; }

        public decimal PerWaitHour { get; set; }

        public decimal PerTrip1 { get; set; }

        public decimal PerTrip2 { get; set; }

        public decimal ChargeTot { get; set; }

        public decimal CreditTot { get; set; }

        [StringLength(200)]
        public string ColorView { get; set; }

        [Required]
        [StringLength(100)]
        public string TrStatus { get; set; }

        [Required]
        [StringLength(100)]
        public string Status { get; set; }

        public int CreatedBy { get; set; }

        public long CreatedAt { get; set; }

        public int ChangedBy { get; set; }

        public long ChangedAt { get; set; }



        [NotMapped]
        public List<DiaryItem> LDiaryItems { get; set; }

        public DiaryHeader()
        {
            LDiaryItems = new List<DiaryItem>();
        }

    }
}
