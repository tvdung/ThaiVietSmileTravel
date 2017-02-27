namespace ThaiVietSmileTravel.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Contact
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập họ")]
        public string Ho { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string Ten { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string TenCongTy { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string DiaChi { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string SoDT { get; set; }

        [StringLength(50)]
        public string GhiChu { get; set; }
    }
}
