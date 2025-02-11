using System;
using System.Collections.Generic;

namespace Zippo_Manage.Models
{
    public partial class ChiTietGioHang
    {
        public int SoLuong { get; set; }
        public double Gia { get; set; }
        public string MaSp { get; set; } = null!;
        public string MaGh { get; set; } = null!;
        public double TongCong { get; set; }

        public virtual Giohang MaGhNavigation { get; set; } = null!;
        public virtual Sanpham MaSpNavigation { get; set; } = null!;
    }
}
