using System;
using System.Collections.Generic;

namespace Zippo_Manage.Models
{
    public partial class Khachhang
    {
        public Khachhang()
        {
            Donhangs = new HashSet<Donhang>();
            Giohangs = new HashSet<Giohang>();
            Taikhoans = new HashSet<Taikhoan>();
        }

        public string MaKh { get; set; } = null!;
        public string HoLot { get; set; } = null!;
        public string Ten { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Sdt { get; set; } = null!;

        public virtual ICollection<Donhang> Donhangs { get; set; }
        public virtual ICollection<Giohang> Giohangs { get; set; }
        public virtual ICollection<Taikhoan> Taikhoans { get; set; }
    }
}
