using System;
using System.Collections.Generic;

namespace QuanLySanXuat.Models;

public partial class Chitietphieudathang
{
    public int SoPhieu { get; set; }

    public int MaThuoc { get; set; }

    public int? SoLuongDat { get; set; }

    public virtual Thuoc MaThuocNavigation { get; set; } = null!;

    public virtual Phieudathang SoPhieuNavigation { get; set; } = null!;
}
