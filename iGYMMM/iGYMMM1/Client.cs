namespace iGYMMM1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client
    {
        [Key]
        public int ClntId { get; set; }

        [StringLength(200)]
        public string ClntNum { get; set; }

        public int GymId { get; set; }

        [StringLength(200)]
        public string FullName { get; set; }

        [StringLength(200)]
        public string AliaseName { get; set; }

        public int TrnTmId { get; set; }

        public int TmGrpId { get; set; }

        public int FavIntrId { get; set; }

        public bool MustFavIntrId { get; set; }

        [StringLength(100)]
        public string ClntIDN { get; set; }

        public decimal PerHour1 { get; set; }

        public decimal PerHour2 { get; set; }

        public decimal PerTrip1 { get; set; }

        public decimal PerTrip2 { get; set; }

        [StringLength(200)]
        public string ClntColor { get; set; }

        [Required]
        [StringLength(100)]
        public string CWorthy { get; set; }

        [Required]
        [StringLength(100)]
        public string CRate { get; set; }

        [Required]
        [StringLength(100)]
        public string Status { get; set; }

        [Required]
        [StringLength(50)]
        public string UName { get; set; }

        [Required]
        [MaxLength(100)]
        public byte[] UPass { get; set; }

        [Required]
        [MaxLength(100)]
        public byte[] UCode { get; set; }

        [Required]
        [MaxLength(100)]
        public byte[] UResetPass { get; set; }

        [Required]
        [MaxLength(100)]
        public byte[] U_GUID { get; set; }

        [Required]
        [MaxLength(100)]
        public byte[] OAuthLvl { get; set; }

        public int CreatedBy { get; set; }

        public long CreatedAt { get; set; }

        public int ChangedBy { get; set; }

        public long ChangedAt { get; set; }
    }
}
