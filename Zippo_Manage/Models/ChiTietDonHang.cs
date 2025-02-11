using System;
using System.Collections.Generic;

namespace Zippo_Manage.Models
{
    public partial class ChiTietDonHang
    {
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public string MaSp { get; set; } = null!;
        public string MaDh { get; set; } = null!;
        public double ThanhTien { get; set; }

        public virtual Donhang MaDhNavigation { get; set; } = null!;
        public virtual Sanpham MaSpNavigation { get; set; } = null!;
    }
}
