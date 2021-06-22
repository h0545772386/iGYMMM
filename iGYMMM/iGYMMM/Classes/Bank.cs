namespace iGYMMM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bank
    {

        [Key]
        public int BnkId { get; set; }

        public int BnkNum { get; set; }

        [StringLength(20)]
        public string BankName { get; set; }

        public bool Deleted { get; set; }

        public Bank()
        {

        }
    }
}
