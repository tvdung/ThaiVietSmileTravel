namespace ThaiVietSmileTravel.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Menu
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string NameTL { get; set; }

        [StringLength(50)]
        public string NameVN { get; set; }

        [StringLength(50)]
        public string NameEN { get; set; }

        [StringLength(2)]
        public string LanguageID { get; set; }
    }
}
