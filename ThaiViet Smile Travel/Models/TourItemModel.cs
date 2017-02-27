using System;

namespace ThaiViet_Smile_Travel.Models
{
    public class TourItemModel
    {
        public int Id { get; set; }

        public string TenTourEN { get; set; }

        public string TenTourTL { get; set; }

        public string TenTourVN { get; set; }

        public int? DonGia { get; set; }

        public string DonViTinh { get; set; }

        public DateTime? NgayKhoiHanh { get; set; }

        public DateTime? NgayKetThuc { get; set; }

        public int? SoNgay { get; set; }

        public int? SoDem { get; set; }

        public int? SoCho { get; set; }

        public string NoiDungEN { get; set; }

        public string NoiDungTL { get; set; }

        public string NoiDungVN { get; set; }

        public string HinhAnh { get; set; }

        public bool KhuyenMai { get; set; }

        public bool TourHot { get; set; }

        public DateTime? NgayTao { get; set; }

        public string Language { get; set; }

        public int? CategoryId { get; set; }
    }
}