namespace iGYMMM1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GymsTimeTable")]
    public partial class GymsTimeTable
    {
        [Key]
        public int GymTTId { get; set; }

        public int GymId { get; set; }

        [Required]
        [StringLength(100)]
        public string DOW { get; set; }

        public int OpenAt { get; set; }

        public int CloseAt { get; set; }

        [Required]
        [StringLength(100)]
        public string Status { get; set; }

        public int CreatedBy { get; set; }

        public long CreatedAt { get; set; }

        public int ChangedBy { get; set; }

        public long ChangedAt { get; set; }
    }
}
