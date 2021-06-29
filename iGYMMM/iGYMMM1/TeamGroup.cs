namespace iGYMMM1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TeamGroup
    {
        [Key]
        public int TmGrpId { get; set; }

        public int GymId { get; set; }

        public int TrnTmId { get; set; }

        [Required]
        [StringLength(200)]
        public string TmGrpName { get; set; }

        [Required]
        [StringLength(500)]
        public string TmGrpDescr { get; set; }

        public int FavIntrId { get; set; }

        public bool MustFavIntrId { get; set; }

        [StringLength(200)]
        public string TmGrpColor { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        public int CreatedBy { get; set; }

        public long CreatedAt { get; set; }

        public int ChangedBy { get; set; }

        public long ChangedAt { get; set; }




        [NotMapped]
        public Instructor Instructor { get; set; }

        [NotMapped]
        public List<Client> LClients { get; set; }


        public TeamGroup()
        {
            LClients = new List<Client>();
            Instructor = new Instructor();
        }
    }
}
