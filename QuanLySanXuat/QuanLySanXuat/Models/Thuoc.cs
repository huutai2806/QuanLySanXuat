using System;
using System.Collections.Generic;

namespace QuanLySanXuat.Models;

public partial class Thuoc
{
    public int MaThuoc { get; set; }

    public string? TenThuoc { get; set; }

    public string? QuyCach { get; set; }

    public virtual ICollection<Chitietphieudathang> Chitietphieudathangs { get; set; } = new List<Chitietphieudathang>();

    public virtual ICollection<Phancong> Phancongs { get; set; } = new List<Phancong>();
}
