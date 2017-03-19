using System.ComponentModel.DataAnnotations;

namespace ThaiVietSmileTravel.Models.Framework
{
    public partial class tbl_Customers
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Ho { get; set; }

        [StringLength(50)]
        public string Ten { get; set; }

        [StringLength(50)]
        public string CongTy { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        [StringLength(50)]
        public string QuocGia { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string SoDienThoai { get; set; }

        public bool? Status { get; set; }
    }
}