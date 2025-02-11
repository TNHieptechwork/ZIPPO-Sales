using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Zippo_Manage.Models
{
    public class SanPhamModel
    {
        [Display(Name = "Mã sản phẩm")]
        [Required(ErrorMessage = "Không để trống mã sản phẩm")]
        public string MaSp { get; set; } = null!;

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Không để trống tên sản phẩm")]
        public string? TenSp { get; set; } = null!;

        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Không để trống mô tả sản phẩm")]
        public string? MotaSp { get; set; } = null!;

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Không để trống giá sản phẩm")]
        public double? Gia { get; set; } 

        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Không để trống số lượng sản phẩm")]
        public int? SoLuong { get; set; }

        [Required(ErrorMessage = "Vui lòng tải lên hình ảnh.")]
        public IFormFile? HinhAnh { get; set; }

        public string? HinhAnhPath { get; set; }
        public string? HinhAnhPhu { get; set; }

        [Display(Name = "Mã Loại")]
        [Required(ErrorMessage = "Mã loại là bắt buộc.")]
        public string MaLoai { get; set; } = null!;
        public string? TenLoai { get; set; }


        public Loaisanpham? MaLoaiNavigation { get; set; }

        public static implicit operator SanPhamModel(Sanpham v)
        {
            return new SanPhamModel
            {
                MaSp = v.MaSp,
                TenSp = v.TenSp,
                MotaSp = v.MotaSp,
                Gia = v.Gia,
                SoLuong = v.SoLuong,
                HinhAnhPath = v.HinhAnh,
                MaLoai = v.MaLoai,
                MaLoaiNavigation = v.MaLoaiNavigation,
            };
        }
    }
}
