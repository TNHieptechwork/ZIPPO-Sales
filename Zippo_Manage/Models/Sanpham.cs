using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zippo_Manage.Models
{
    public partial class Sanpham
    {
        public Sanpham()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            ChiTietGioHangs = new HashSet<ChiTietGioHang>();
        }
        [Key]
        [DisplayName("Mã sản phẩm"),Required(ErrorMessage ="Không để trống mã sản phẩm")]
        [StringLength(10,MinimumLength =5,ErrorMessage ="Phải nhập ít nhất 5 kí tự và tối đa là 10 kí tự")]
        public string MaSp { get; set; } = null!;
        [DisplayName("Tên sản phẩm"), Required(ErrorMessage = "Không để trống tên sản phẩm")]

        public string? TenSp { get; set; }
        [DisplayName("Mô tả"), Required(ErrorMessage = "Không để trống mô tả")]

        public string? MotaSp { get; set; }
        [DisplayName("Giá"), Required(ErrorMessage = "Không để trống giá sản phẩm")]

        public double? Gia { get; set; }
        [DisplayName("Số lượng"), Required(ErrorMessage = "Không để trống số lượng sản phẩm")]

        public int? SoLuong { get; set; }
        [Required(ErrorMessage = "Vui lòng tải lên hình ảnh.")]

        public string? HinhAnh { get; set; }
        public string? MaLoai { get; set; }

        public virtual Loaisanpham? MaLoaiNavigation { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }
    }
}
