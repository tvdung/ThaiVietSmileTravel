using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ThaiVietSmileTravel.Models.Framework
{
    public partial class tbl_Tour
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên tour tiếng anh")]
        [Display(Name = "Tên tour tiếng anh")]
        [StringLength(255)]
        public string TenTourEN { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên tour tiếng thái")]
        [Display(Name = "Tên tour tiếng thái")]
        [StringLength(255)]
        public string TenTourTL { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên tour tiếng việt")]
        [Display(Name = "Tên tour tiếng việt")]
        [StringLength(255)]
        public string TenTourVN { get; set; }

        [Display(Name = "Giá tour")]
        public int? DonGia { get; set; }

        [StringLength(10)]
        public string DonViTinh { get; set; }

        [StringLength(50)]
        public string NgayDuKien { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số ngày")]
        [Display(Name = "Số ngày")]
        public int? SoNgay { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số tour")]
        [Display(Name = "Số đêm")]
        public int? SoDem { get; set; }

        [Display(Name = "Số chổ")]
        public int? SoCho { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Vui lòng nhập nội dung tour tiếng anh")]
        [Display(Name = "Nội dung tour tiếng anh")]
        [Column(TypeName = "ntext")]
        public string NoiDungEN { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Vui lòng nhập nội dung tour tiếng thái")]
        [Display(Name = "Nội dung tour tiếng thái")]
        [Column(TypeName = "ntext")]
        public string NoiDungTL { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Vui lòng nhập nội dung tour tiếng việt")]
        [Display(Name = "Nội dung tour tiếng việt")]
        [Column(TypeName = "ntext")]
        public string NoiDungVN { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng chọn hình tour")]
        [Display(Name = "Hình ảnh")]
        public string HinhAnh { get; set; }

        [Display(Name = "Khuyến mãi")]
        public bool KhuyenMai { get; set; }

        [Display(Name = "Tour hot")]
        public bool TourHot { get; set; }

        public DateTime? NgayTao { get; set; }

        [Display(Name = "Danh mục tour")]
        [Required(ErrorMessage = "Vui lòng chọn danh mục tour")]
        public int CategoryId { get; set; }

        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; }
    }
}