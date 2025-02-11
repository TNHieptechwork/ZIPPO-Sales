using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Zippo_Manage.Models
{
    public partial class Loaisanpham
    {
        public Loaisanpham()
        {
            Sanphams = new HashSet<Sanpham>();
        }

        public string MaLoai { get; set; } = null!;
        [DisplayName("Tên loại")]
        public string? TenLoai { get; set; }

        public virtual ICollection<Sanpham> Sanphams { get; set; }
    }
}
