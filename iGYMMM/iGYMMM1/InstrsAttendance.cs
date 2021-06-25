namespace iGYMMM1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InstrsAttendance")]
    public partial class InstrsAttendance
    {
        [Key]
        public int IAtnId { get; set; }

        public int InstrId { get; set; }

        public int GymId { get; set; }

        public int IAShiftDate { get; set; }

        public long IAShiftStart { get; set; }

        public long IAShiftEnd { get; set; }

        [Required]
        [StringLength(100)]
        public string Status { get; set; }

        public int CreatedBy { get; set; }

        public long CreatedAt { get; set; }

        public int ChangedBy { get; set; }

        public long ChangedAt { get; set; }
    }
}
