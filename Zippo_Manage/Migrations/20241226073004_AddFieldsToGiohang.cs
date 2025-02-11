using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zippo_Manage.Migrations
{
    public partial class AddFieldsToGiohang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Thêm các trường mới vào bảng GIOHANG
            migrationBuilder.AddColumn<string>(
                name: "SanphamMaSp",
                table: "GIOHANG",
                type: "nchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoLuongMua",
                table: "GIOHANG",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Gia",
                table: "GIOHANG",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_GIOHANG_SANPHAM_SanphamMaSp",
                table: "GIOHANG",
                column: "SanphamMaSp",
                principalTable: "SANPHAM",
                principalColumn: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_GIOHANG_SanphamMaSp",
                table: "GIOHANG",
                column: "SanphamMaSp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GIOHANG_SANPHAM_SanphamMaSp",
                table: "GIOHANG");

            migrationBuilder.DropIndex(
                name: "IX_GIOHANG_SanphamMaSp",
                table: "GIOHANG");

            migrationBuilder.DropColumn(
                name: "SanphamMaSp",
                table: "GIOHANG");

            migrationBuilder.DropColumn(
                name: "SoLuongMua",
                table: "GIOHANG");

            migrationBuilder.DropColumn(
                name: "Gia",
                table: "GIOHANG");
        }
    }
}
