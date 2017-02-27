using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiViet_Smile_Travel.Areas.Admin.Models
{
    public class TourModel
    {
        public int Id { get; set; }

        public string TenTour { get; set; }

        public int? DonGia { get; set; }

        public string DonViTinh { get; set; }

        public DateTime? NgayKhoiHanh { get; set; }

        public DateTime? NgayKetThuc { get; set; }

        public int? SoNgay { get; set; }

        public int? SoDem { get; set; }

        public int? SoCho { get; set; }

        public string NoiDung { get; set; }

        public string HinhAnh { get; set; }

        public bool? KhuyenMai { get; set; }

        public bool? TourHot { get; set; }

        public DateTime? NgayTao { get; set; }

        public string Language { get; set; }
    }
}