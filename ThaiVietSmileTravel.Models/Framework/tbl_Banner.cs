using System.ComponentModel.DataAnnotations;

namespace ThaiVietSmileTravel.Models.Framework
{
    public partial class tbl_Banner
    {
        public int Id { get; set; }

        [StringLength(4000)]
        [Display(Name = "Hình ảnh")]
        public string HinhAnh { get; set; }

        [StringLength(255)]
        [Display(Name = "Link tới tour")]
        public string UrlTour { get; set; }

        [Display(Name = "Trạng thái hiển thị")]
        public bool IsActive { get; set; }
    }
}