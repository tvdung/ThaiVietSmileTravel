using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiViet_Smile_Travel.Models
{
    public class OrderDetail
    {
        public int TourID { get; set; }

        public int OrderID { get; set; }

        public int? SoNguoi { get; set; }

        public decimal? DonGia { get; set; }
    }
}