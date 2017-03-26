using System.ComponentModel.DataAnnotations;

namespace ThaiVietSmileTravel.Models.Framework
{
    public partial class tbl_Account
    {
        [Key]
        public int UserId { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên tài khoản")]
        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản")]
        public string UserName { get; set; }

        [StringLength(4000)]
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        public string Email { get; set; }

        [StringLength(4000)]
        [Display(Name = "Mật khẩu email")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu email")]
        public string PasswordEmail { get; set; }

        public bool IsAdmin { get; set; }
    }
}