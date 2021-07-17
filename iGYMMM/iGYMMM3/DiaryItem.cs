namespace iGYMMM3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DiaryItem
    {
        [Key]
        public int DryItmId { get; set; }

        public int DryHdrId { get; set; }

        public int GymId { get; set; }

        public int TrnTmId { get; set; }

        public int TmGrpId { get; set; }

        public int PkgId { get; set; }

        public int PkgReqId { get; set; }

        public int TrnDate { get; set; }

        public int TrnHour { get; set; }

        public int ClntId { get; set; }

        public decimal PerHour1 { get; set; }

        public decimal PerHour2 { get; set; }

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
