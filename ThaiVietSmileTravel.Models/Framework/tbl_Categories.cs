namespace ThaiVietSmileTravel.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Categories
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string TenTL { get; set; }

        [StringLength(50)]
        public string TenVN { get; set; }

        [StringLength(50)]
        public string TenEN { get; set; }

        [StringLength(2)]
        public string LanguageID { get; set; }
    }
}
