using System.ComponentModel.DataAnnotations;

namespace ThaiVietSmileTravel.Models.Framework
{
    public partial class tbl_Administrator
    {
        [Key]
        public int UserId { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên tài khoản")]
        public string UserName { get; set; }

        [StringLength(4000)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(4000)]
        [Display(Name = "Mật khẩu email")]
        public string PasswordEmail { get; set; }

        public bool IsAdmin { get; set; }
    }
}