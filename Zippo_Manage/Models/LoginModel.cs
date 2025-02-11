using System.ComponentModel.DataAnnotations;

namespace Zippo_Manage.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Không để trống tên đăng nhập")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập không được dài hơn 50 ký tự")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Không để trống mật khẩu")]
        public string MatKhau { get; set; }
    }
}
