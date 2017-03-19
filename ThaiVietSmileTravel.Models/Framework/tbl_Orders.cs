using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThaiVietSmileTravel.Models.Framework
{
    //[Table("tbl_Orders")]
    public partial class tbl_Orders
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [StringLength(50)]
        [Display(Name = "Tên khách hàng")]
        public string TenKH { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [StringLength(255)]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [StringLength(50)]
        [Display(Name = "Số điện thoại")]
        public string SoDT { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public DateTime NgayDat { get; set; }

        [Display(Name = "Ngày cần đi")]
        public DateTime NgayCanDi { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        [Display(Name = "Trạng thái")]
        public bool TrangThai { get; set; }
    }
}