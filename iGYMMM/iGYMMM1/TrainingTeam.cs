namespace iGYMMM1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TrainingTeam
    {
        [Key]
        public int TrnTmId { get; set; }

        public int GymId { get; set; }

        [Required]
        [StringLength(200)]
        public string TrnTmName { get; set; }

        [Required]
        [StringLength(500)]
        public string TrnTmDescr { get; set; }

        public int InstrIdCount { get; set; }

        public bool OnePayer { get; set; }

        [StringLength(200)]
        public string TrnTmColor { get; set; }

        public bool MustFavIntr4Grp { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        public int CreatedBy { get; set; }

        public long CreatedAt { get; set; }

        public int ChangedBy { get; set; }

        public long ChangedAt { get; set; }





        [NotMapped]
        public List<TeamGroup> LTeamGroups { get; set; }

        [NotMapped]
        public Package Package { get; set; }


        public TrainingTeam()
        {
            Package = new Package();
            LTeamGroups = new List<TeamGroup>();
        }
    }
}
