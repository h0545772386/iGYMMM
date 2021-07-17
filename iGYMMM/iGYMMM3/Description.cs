namespace iGYMMM3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Description
    {
        [Key]
        public int DiscrId { get; set; }

        [Required]
        [StringLength(100)]
        public string ObjType { get; set; }

        public int ObjId { get; set; }

        public int GymId { get; set; }

        [StringLength(100)]
        public string DiscrTyp { get; set; }

        [StringLength(800)]
        public string DiscrText { get; set; }

        [Required]
        [StringLength(100)]
        public string Status { get; set; }

        public int CreatedBy { get; set; }

        public long CreatedAt { get; set; }

        public int ChangedBy { get; set; }

        public long ChangedAt { get; set; }
    }
}
