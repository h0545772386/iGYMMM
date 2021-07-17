namespace iGYMMM3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PkgPayment
    {
        [Key]
        public int PkgPymntId { get; set; }

        public int PkgId { get; set; }

        public int GymId { get; set; }

        public int ClntId { get; set; }

        public int PkgPymntDate { get; set; }

        public decimal TotalFee { get; set; }

        public bool PaymentDone { get; set; }

        [Required]
        [StringLength(100)]
        public string Status { get; set; }

        public int CreatedBy { get; set; }

        public long CreatedAt { get; set; }

        public int ChangedBy { get; set; }

        public long ChangedAt { get; set; }
    }
}
