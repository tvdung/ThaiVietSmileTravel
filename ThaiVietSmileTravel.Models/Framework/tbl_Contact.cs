using System;
using System.ComponentModel.DataAnnotations;

using ThaiVietSmileTravel.Globalization;

namespace ThaiVietSmileTravel.Models.Framework
{
    public partial class tbl_Contact
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "plhEnterFirtName")]
        public string Ho { get; set; }

        [StringLength(50)]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "plhEnterLastName")]
        public string Ten { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string TenCongTy { get; set; }

        [StringLength(50)]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "plhEnterAdress")]
        public string DiaChi { get; set; }

        [StringLength(50)]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "plhEnterPhone")]
        public string SoDT { get; set; }

        [StringLength(50)]
        public string GhiChu { get; set; }

        public bool IsReply { get; set; }

        public DateTime NgayGui { get; set; }
    }
}