using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThaiVietSmileTravel.Models.Framework;

namespace ThaiViet_Smile_Travel.Models
{
    [Serializable]
    public class CardItemModel
    {
        public tbl_Tour Tour { get; set; }

        public int SoNguoi { get; set; }
    }
}