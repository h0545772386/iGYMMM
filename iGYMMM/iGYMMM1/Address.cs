namespace iGYMMM1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Address
    {
        [Key]
        public int AdrsId { get; set; }

        [Required]
        [StringLength(100)]
        public string ObjType { get; set; }

        public int ObjId { get; set; }

        public int GymId { get; set; }

        [StringLength(100)]
        public string Street { get; set; }

        [StringLength(100)]
        public string Street1 { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string ZipCode { get; set; }

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [StringLength(100)]
        public string Address3 { get; set; }

        [StringLength(100)]
        public string Address4 { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        public int CreatedBy { get; set; }

        public long CreatedAt { get; set; }

        public int ChangedBy { get; set; }

        public long ChangedAt { get; set; }
    }
}
