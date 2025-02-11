using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Zippo_Manage.Models
{
    public partial class QLyZIPPOContext : DbContext
    {
        public QLyZIPPOContext()
        {
        }

        public QLyZIPPOContext(DbContextOptions<QLyZIPPOContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; } = null!;
        public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; } = null!;
        public virtual DbSet<Donhang> Donhangs { get; set; } = null!;
        public virtual DbSet<Giohang> Giohangs { get; set; } = null!;
        public virtual DbSet<Khachhang> Khachhangs { get; set; } = null!;
        public virtual DbSet<Loaisanpham> Loaisanphams { get; set; } = null!;
        public virtual DbSet<Sanpham> Sanphams { get; set; } = null!;
        public virtual DbSet<Taikhoan> Taikhoans { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\TRANHIEP;Database=QLyZIPPO;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietDonHang>(entity =>
            {
                entity.HasKey(e => new { e.MaSp, e.MaDh })
                    .HasName("PK__ChiTietD__2557507A23F2F6BC");

                entity.ToTable("ChiTietDonHang");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .HasColumnName("MaSP")
                    .IsFixedLength();

                entity.Property(e => e.MaDh)
                    .HasMaxLength(10)
                    .HasColumnName("MaDH")
                    .IsFixedLength();

                entity.Property(e => e.ThanhTien).HasComputedColumnSql("([SoLuong]*[DonGia])", true);

                entity.HasOne(d => d.MaDhNavigation)
                    .WithMany(p => p.ChiTietDonHangs)
                    .HasForeignKey(d => d.MaDh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietDon__MaDH__48CFD27E");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.ChiTietDonHangs)
                    .HasForeignKey(d => d.MaSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietDon__MaSP__47DBAE45");
            });

            modelBuilder.Entity<ChiTietGioHang>(entity =>
            {
                entity.HasKey(e => new { e.MaSp, e.MaGh })
                    .HasName("PK__ChiTietG__655752F4807AFB6F");

                entity.ToTable("ChiTietGioHang");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .HasColumnName("MaSP")
                    .IsFixedLength();

                entity.Property(e => e.MaGh)
                    .HasMaxLength(10)
                    .HasColumnName("MaGH")
                    .IsFixedLength();

                entity.Property(e => e.TongCong).HasComputedColumnSql("([SoLuong]*[Gia])", true);

                entity.HasOne(d => d.MaGhNavigation)
                    .WithMany(p => p.ChiTietGioHangs)
                    .HasForeignKey(d => d.MaGh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietGio__MaGH__4CA06362");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.ChiTietGioHangs)
                    .HasForeignKey(d => d.MaSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietGio__MaSP__4BAC3F29");
            });

            modelBuilder.Entity<Donhang>(entity =>
            {
                entity.HasKey(e => e.MaDh)
                    .HasName("PK__DONHANG__27258661D34B6D5A");

                entity.ToTable("DONHANG");

                entity.Property(e => e.MaDh)
                    .HasMaxLength(10)
                    .HasColumnName("MaDH")
                    .IsFixedLength();

                entity.Property(e => e.MaKh)
                    .HasMaxLength(10)
                    .HasColumnName("MaKH")
                    .IsFixedLength();

                entity.Property(e => e.NgayDatHang).HasColumnType("datetime");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.Donhangs)
                    .HasForeignKey(d => d.MaKh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DONHANG__MaKH__4222D4EF");
            });

            modelBuilder.Entity<Giohang>(entity =>
            {
                entity.HasKey(e => e.MaGh)
                    .HasName("PK__GIOHANG__2725AE85FFCB73F6");

                entity.ToTable("GIOHANG");

                entity.Property(e => e.MaGh)
                    .HasMaxLength(10)
                    .HasColumnName("MaGH")
                    .IsFixedLength();

                entity.Property(e => e.MaKh)
                    .HasMaxLength(10)
                    .HasColumnName("MaKH")
                    .IsFixedLength();

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.TrangThai).HasMaxLength(40);

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.Giohangs)
                    .HasForeignKey(d => d.MaKh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GIOHANG__MaKH__44FF419A");
            });

            modelBuilder.Entity<Khachhang>(entity =>
            {
                entity.HasKey(e => e.MaKh)
                    .HasName("PK__KHACHHAN__2725CF1E34D722F2");

                entity.ToTable("KHACHHANG");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(10)
                    .HasColumnName("MaKH")
                    .IsFixedLength();

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.HoLot).HasMaxLength(30);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(15)
                    .HasColumnName("SDT");

                entity.Property(e => e.Ten).HasMaxLength(10);
            });

            modelBuilder.Entity<Loaisanpham>(entity =>
            {
                entity.HasKey(e => e.MaLoai)
                    .HasName("PK__LOAISANP__730A5759A4B8E1BF");

                entity.ToTable("LOAISANPHAM");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.TenLoai)
                    .HasMaxLength(30)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Sanpham>(entity =>
            {
                entity.HasKey(e => e.MaSp)
                    .HasName("PK__SANPHAM__2725081C5810BFAB");

                entity.ToTable("SANPHAM");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(10)
                    .HasColumnName("MaSP")
                    .IsFixedLength();

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MotaSp).HasColumnName("MotaSP");

                entity.Property(e => e.TenSp)
                    .HasMaxLength(100)
                    .HasColumnName("TenSP");

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.Sanphams)
                    .HasForeignKey(d => d.MaLoai)
                    .HasConstraintName("FK__SANPHAM__MaLoai__3F466844");
            });

            modelBuilder.Entity<Taikhoan>(entity =>
            {
                entity.HasKey(e => e.MaTk)
                    .HasName("PK__TAIKHOAN__2725007093B5CB48");

                entity.ToTable("TAIKHOAN");

                entity.Property(e => e.MaTk)
                    .HasMaxLength(10)
                    .HasColumnName("MaTK")
                    .IsFixedLength();

                entity.Property(e => e.MaKh)
                    .HasMaxLength(10)
                    .HasColumnName("MaKH")
                    .IsFixedLength();

                entity.Property(e => e.MatKhau).HasMaxLength(50);

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TenDangNhap).HasMaxLength(50);

                entity.Property(e => e.Vaitro).HasMaxLength(20);

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.Taikhoans)
                    .HasForeignKey(d => d.MaKh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TAIKHOAN__MaKH__3A81B327");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
