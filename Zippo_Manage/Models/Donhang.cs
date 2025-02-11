using System;
using System.Collections.Generic;

namespace Zippo_Manage.Models
{
    public partial class Donhang
    {
        public Donhang()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        public string MaDh { get; set; } = null!;
        public DateTime NgayDatHang { get; set; }
        public string MaKh { get; set; } = null!;

        public virtual Khachhang MaKhNavigation { get; set; } = null!;
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
