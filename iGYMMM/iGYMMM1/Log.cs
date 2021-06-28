namespace iGYMMM1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Log
    {
        [Key]
        public int LgId { get; set; }

        [Required]
        [StringLength(100)]
        public string ObjType { get; set; }

        public int ObjId { get; set; }

        public int GymId { get; set; }

        public int LgDate1 { get; set; }

        [StringLength(100)]
        public string LgType { get; set; }

        [StringLength(600)]
        public string LgText { get; set; }

        public int LgDate2 { get; set; }

        [StringLength(200)]
        public string LgColor { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        public int CreatedBy { get; set; }

        public long CreatedAt { get; set; }

        public int ChangedBy { get; set; }

        public long ChangedAt { get; set; }
    }
}
