using System;
using System.Collections.Generic;

namespace QuanLySanXuat.Models;

public partial class Phieudathang
{
    public int SoPhieu { get; set; }

    public DateTime? NgayLapPhieu { get; set; }

    public int? MaKhachHang { get; set; }

    public virtual ICollection<Chitietphieudathang> Chitietphieudathangs { get; set; } = new List<Chitietphieudathang>();

    public virtual Khachhang? MaKhachHangNavigation { get; set; }

    public virtual ICollection<Phancong> Phancongs { get; set; } = new List<Phancong>();
}
