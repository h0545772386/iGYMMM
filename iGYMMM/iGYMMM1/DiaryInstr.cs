namespace iGYMMM1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DiaryInstr
    {
        [Key]
        public int DryInstrId { get; set; }

        public int GymId { get; set; }

        public int DryTmId { get; set; }

        public int PkgId { get; set; }

        public int TrnDate { get; set; }     // actual training date for team  YYYYMMDD

        public int TrnHour { get; set; }   // actual training hour for team  0 - 23

        public int PkReqHour2 { get; set; }

        public int InstrIdPlanned { get; set; }

        public int InstrId { get; set; }        

        public decimal PerHour1 { get; set; }

        public decimal PerHour2 { get; set; }

        public decimal PerWaitHour { get; set; }

        public decimal PerTrip1 { get; set; }

        public decimal PerTrip2 { get; set; }

        public decimal ChargeTot { get; set; }

        public decimal CreditTot { get; set; }

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
    }
}
