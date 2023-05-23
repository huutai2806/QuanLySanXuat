using System;
using System.Collections.Generic;

namespace QuanLySanXuat.Models;

public partial class Khachhang
{
    public int MaKhachHang { get; set; }

    public string? TenKhachHang { get; set; }

    public string? DiaChi { get; set; }

    public string? SoDienThoai { get; set; }

    public virtual ICollection<Phieudathang> Phieudathangs { get; set; } = new List<Phieudathang>();
}
