using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zippo_Manage.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Zippo_Manage.Helper;
using System.Linq;

namespace Zippo_Manage.Controllers
{
    public class GioHangController : Controller
    {
        QLyZIPPOContext db = new QLyZIPPOContext();
        private const string CartSessionKey = "GioHang";

        private List<Giohang> getGioHangFromSession()
        {
            var giohang = HttpContext.Session.GetObjectFromJson<List<Giohang>>(CartSessionKey);
            return giohang ?? new List<Giohang>();
        }

        private void saveGioHangToSession(List<Giohang> giohang)
        {
            HttpContext.Session.SetObjectAsJson(CartSessionKey, giohang);
        }

        public IActionResult Index()
        {
            var giohang = getGioHangFromSession();
            return View(giohang);
        }

        public IActionResult dsGioHangAdmin()
        {
            var giohangList = db.Giohangs.Include(gh => gh.MaKhNavigation)
                                         .Include(gh => gh.ChiTietGioHangs)
                                         .ToList();
            return View(giohangList);
        }

        public IActionResult getGioHang()
        {
            var gioHang = HttpContext.Session.GetObjectFromJson<List<Giohang>>(CartSessionKey)
                                              ?? new List<Giohang>();
            return new JsonResult(gioHang);
        }

        [HttpPost]
        public IActionResult themGioHang(string maSp, int soluong)
        {
            var sanPham = db.Sanphams.FirstOrDefault(sp => sp.MaSp == maSp);
            if (sanPham == null)
            {
                return NotFound();
            }

            var gioHang = getGioHangFromSession();
            var spTrongGioHang = gioHang.FirstOrDefault(x => x.Sanpham != null && x.Sanpham.MaSp == maSp);
            if (spTrongGioHang != null)
            {
                spTrongGioHang.SoLuongMua += soluong;
            }
            else
            {
                gioHang.Add(new Giohang
                {
                    MaGh = Guid.NewGuid().ToString(),  
                    MaKh = HttpContext.Session.GetString("MaKh"),  
                    NgayTao = DateTime.Now,
                    TrangThai = "Chờ xử lý", 
                    Sanpham = sanPham,
                    SoLuongMua = soluong,
                    Gia = (double)sanPham.Gia
                });
            }

            saveGioHangToSession(gioHang);
            return RedirectToAction("Index", "GioHang");
        }

        public IActionResult luuVaoGioHang()
        {
            var gioHang = getGioHangFromSession();

            if (gioHang == null || !gioHang.Any())
            {
                return RedirectToAction("Index", "GioHang");
            }

            var maKh = HttpContext.Session.GetString("MaKh");
            if (string.IsNullOrEmpty(maKh))
            {
                return RedirectToAction("Login", "Account");
            }

            var khachHang = db.Khachhangs.FirstOrDefault(kh => kh.MaKh == maKh);
            if (khachHang == null)
            {
                return NotFound("Khách hàng không tồn tại.");
            }

            var donHang = new Giohang
            {
                MaKh = maKh,
                NgayTao = DateTime.Now,
                TrangThai = "Chờ xử lý"
            };

            db.Giohangs.Add(donHang);
            db.SaveChanges();

            foreach (var item in gioHang)
            {
                var chiTiet = new ChiTietGioHang
                {
                    MaGh = donHang.MaGh,
                    MaSp = item.Sanpham.MaSp,
                    SoLuong = item.SoLuongMua,
                    Gia = item.Gia
                };
                db.ChiTietGioHangs.Add(chiTiet);
            }

            db.SaveChanges();

            saveGioHangToSession(new List<Giohang>());  // Clear the session cart after saving

            return RedirectToAction("Index", "GioHang");
        }

        public IActionResult CapNhatGioHang(string maSp, int soluong)
        {
            var gioHang = getGioHangFromSession();
            var spTrongGioHang = gioHang.FirstOrDefault(x => x.Sanpham != null && x.Sanpham.MaSp == maSp);

            if (spTrongGioHang != null)
            {
                spTrongGioHang.SoLuongMua = soluong;
            }

            saveGioHangToSession(gioHang);
            return RedirectToAction("Index", "GioHang");
        }

        public IActionResult XoaKhoiGioHang(string maSp)
        {
            var gioHang = getGioHangFromSession();
            var spTrongGioHang = gioHang.FirstOrDefault(x => x.Sanpham != null && x.Sanpham.MaSp == maSp);

            if (spTrongGioHang != null)
            {
                gioHang.Remove(spTrongGioHang);
            }

            saveGioHangToSession(gioHang);
            return RedirectToAction("Index", "GioHang");
        }

        public IActionResult TiepTucMuaSam()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
