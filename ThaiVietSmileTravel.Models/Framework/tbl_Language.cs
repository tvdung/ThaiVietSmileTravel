using System.ComponentModel.DataAnnotations;

namespace ThaiVietSmileTravel.Models.Framework
{
    public partial class tbl_Language
    {
        [StringLength(2)]
        public string ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public bool IsDefault { get; set; }
    }
}