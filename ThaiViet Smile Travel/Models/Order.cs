using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThaiViet_Smile_Travel.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string TenKH { get; set; }

        public string DiaChi { get; set; }

        public string SoDT { get; set; }

        public string Email { get; set; }

        public DateTime NgayDat { get; set; }

        public DateTime NgayCanDi { get; set; }

        public string GhiChu { get; set; }

        public bool TrangThai { get; set; }
    }
}