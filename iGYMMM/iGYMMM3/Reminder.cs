namespace iGYMMM3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reminder
    {
        [Key]
        public int RemId { get; set; }

        [Required]
        [StringLength(100)]
        public string ObjType { get; set; }

        public int ObjId { get; set; }

        public int GymId { get; set; }

        public long PopUpAt_YYYYMMDDHHMM { get; set; }

        [StringLength(300)]
        public string RemText { get; set; }

        public bool IsRecorsive { get; set; }

        [StringLength(100)]
        public string RecorsivType { get; set; }

        [StringLength(100)]
        public string RecorsivTime { get; set; }

        [StringLength(100)]
        public string RecorsivTime1 { get; set; }

        [StringLength(100)]
        public string Frequence { get; set; }

        [StringLength(200)]
        public string RemColor { get; set; }

        [Required]
        [StringLength(100)]
        public string Status { get; set; }

        public int CreatedBy { get; set; }

        public long CreatedAt { get; set; }

        public int ChangedBy { get; set; }

        public long ChangedAt { get; set; }
    }
}
