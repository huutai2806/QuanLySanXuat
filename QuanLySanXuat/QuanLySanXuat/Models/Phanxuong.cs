using System;
using System.Collections.Generic;

namespace QuanLySanXuat.Models;

public partial class Phanxuong
{
    public int MaPhanXuong { get; set; }

    public string? TenPhanXuong { get; set; }

    public string? DiaChi { get; set; }

    public string? DienThoai { get; set; }

    public string? HoTenQuanDoc { get; set; }

    public virtual ICollection<Phancong> Phancongs { get; set; } = new List<Phancong>();
}
