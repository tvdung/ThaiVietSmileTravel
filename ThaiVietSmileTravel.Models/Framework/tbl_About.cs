using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ThaiVietSmileTravel.Models.Framework
{
    public partial class tbl_About
    {
        public int Id { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Vui lòng nhập nội dung tiếng việt")]
        [Display(Name = "Nội dung tiếng việt")]
        public string NoiDungVN { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Vui lòng nhập nội dung tiếng anh")]
        [Display(Name = "Nội dung tiếng anh")]
        public string NoiDungEN { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Vui lòng nhập nội dung tiếng thái")]
        [Display(Name = "Nội dung tiếng thái")]
        public string NoiDungTL { get; set; }

        [StringLength(50)]
        [Display(Name = "Hình ảnh")]
        public string HinhAnh { get; set; }
    }
}