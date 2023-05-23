using System;
using System.Collections.Generic;

namespace QuanLySanXuat.Models;

public partial class Phancong
{
    public int SoPhanCong { get; set; }

    public int? MaPhanXuong { get; set; }

    public int? SoPhieu { get; set; }

    public int? MaThuoc { get; set; }

    public int? SoLuong { get; set; }

    public virtual Phanxuong? MaPhanXuongNavigation { get; set; }

    public virtual Thuoc? MaThuocNavigation { get; set; }

    public virtual Phieudathang? SoPhieuNavigation { get; set; }
}
