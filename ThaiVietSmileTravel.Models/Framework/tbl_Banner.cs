using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThaiVietSmileTravel.Models.Framework
{
   public partial class tbl_Banner
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string HinhAnh { get; set; }

        [Column(TypeName = "ntext")]
        public string UrlTour { get; set; }

        public bool IsActive { get; set; }

        
    }
}
