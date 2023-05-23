using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuanLySanXuat.Models;

public partial class QuanLySanXuatContext : DbContext
{
    public QuanLySanXuatContext()
    {
    }

    public QuanLySanXuatContext(DbContextOptions<QuanLySanXuatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chitietphieudathang> Chitietphieudathangs { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Phancong> Phancongs { get; set; }

    public virtual DbSet<Phanxuong> Phanxuongs { get; set; }

    public virtual DbSet<Phieudathang> Phieudathangs { get; set; }

    public virtual DbSet<Thuoc> Thuocs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=MvcSingerConnectionStrings");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chitietphieudathang>(entity =>
        {
            entity.HasKey(e => new { e.SoPhieu, e.MaThuoc }).HasName("PK__CHITIETP__82B1B180049FEEC6");

            entity.ToTable("CHITIETPHIEUDATHANG");

            entity.HasOne(d => d.MaThuocNavigation).WithMany(p => p.Chitietphieudathangs)
                .HasForeignKey(d => d.MaThuoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETPH__MaThu__4D94879B");

            entity.HasOne(d => d.SoPhieuNavigation).WithMany(p => p.Chitietphieudathangs)
                .HasForeignKey(d => d.SoPhieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHITIETPH__SoPhi__4CA06362");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang).HasName("PK__KHACHHAN__88D2F0E5F74B0EF4");

            entity.ToTable("KHACHHANG");

            entity.Property(e => e.MaKhachHang).ValueGeneratedNever();
            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenKhachHang)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Phancong>(entity =>
        {
            entity.HasKey(e => e.SoPhanCong).HasName("PK__PHANCONG__5692CF5273FE583B");

            entity.ToTable("PHANCONG");

            entity.Property(e => e.SoPhanCong).ValueGeneratedNever();

            entity.HasOne(d => d.MaPhanXuongNavigation).WithMany(p => p.Phancongs)
                .HasForeignKey(d => d.MaPhanXuong)
                .HasConstraintName("FK__PHANCONG__MaPhan__5070F446");

            entity.HasOne(d => d.MaThuocNavigation).WithMany(p => p.Phancongs)
                .HasForeignKey(d => d.MaThuoc)
                .HasConstraintName("FK__PHANCONG__MaThuo__52593CB8");

            entity.HasOne(d => d.SoPhieuNavigation).WithMany(p => p.Phancongs)
                .HasForeignKey(d => d.SoPhieu)
                .HasConstraintName("FK__PHANCONG__SoPhie__5165187F");
        });

        modelBuilder.Entity<Phanxuong>(entity =>
        {
            entity.HasKey(e => e.MaPhanXuong).HasName("PK__PHANXUON__A5594678A2420863");

            entity.ToTable("PHANXUONG");

            entity.Property(e => e.MaPhanXuong).ValueGeneratedNever();
            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.HoTenQuanDoc)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TenPhanXuong)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Phieudathang>(entity =>
        {
            entity.HasKey(e => e.SoPhieu).HasName("PK__PHIEUDAT__960AAEE29BF390D5");

            entity.ToTable("PHIEUDATHANG");

            entity.Property(e => e.SoPhieu).ValueGeneratedNever();
            entity.Property(e => e.NgayLapPhieu).HasColumnType("date");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.Phieudathangs)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__PHIEUDATH__MaKha__34C8D9D1");
        });

        modelBuilder.Entity<Thuoc>(entity =>
        {
            entity.HasKey(e => e.MaThuoc).HasName("PK__THUOC__4BB1F620B2118B87");

            entity.ToTable("THUOC");

            entity.Property(e => e.MaThuoc).ValueGeneratedNever();
            entity.Property(e => e.QuyCach)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TenThuoc)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
