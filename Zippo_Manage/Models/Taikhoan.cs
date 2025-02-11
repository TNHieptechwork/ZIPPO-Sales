using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zippo_Manage.Models
{
    public partial class Taikhoan
    {
        [Key]
        [Display(Name ="Mã tài khoản"),Required(ErrorMessage ="Không để trống mã tài khoản")]
        public string MaTk { get; set; } = null!;
        [Display(Name = "Mã khách hàng"), Required(ErrorMessage = "Không để trống mã khách hàng")]

        public string MaKh { get; set; } = null!;
        [Display(Name = "Tên đăng nhập"), Required(ErrorMessage = "Không để trống tên đăng nhập")]

        public string TenDangNhap { get; set; } = null!;
        [Display(Name = "Mật khẩu"), Required(ErrorMessage = "Không để trống mật khẩu")]

        public string MatKhau { get; set; } = null!;

        public string Vaitro { get; set; } = null!;
        public DateTime Ngaytao { get; set; }

        public virtual Khachhang MaKhNavigation { get; set; } = null!;
    }
}
