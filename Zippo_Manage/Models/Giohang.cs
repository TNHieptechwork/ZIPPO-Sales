using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zippo_Manage.Models
{
    public partial class Giohang
    {
        public Giohang()
        {
            ChiTietGioHangs = new HashSet<ChiTietGioHang>();
        }

       
        
        public string MaGh { get; set; } = null!;
        public string MaKh { get; set; } = null!;
        public DateTime NgayTao { get; set; }
        public string TrangThai { get; set; } = null!;

        public Sanpham? Sanpham { get; set; } 
        public int SoLuongMua { get; set; }
        public double Gia { get; set; }

        public virtual Khachhang MaKhNavigation { get; set; } = null!;
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }
    }
}
