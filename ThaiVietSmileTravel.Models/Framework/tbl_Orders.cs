namespace ThaiVietSmileTravel.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Orders
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [StringLength(50)]
        public string TenKH { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [StringLength(255)]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [StringLength(50)]
        public string SoDT { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public DateTime NgayDat { get; set; }

        public DateTime NgayCanDi { get; set; }

        public string GhiChu { get; set; }

        public bool TrangThai { get; set; }
    }
}
