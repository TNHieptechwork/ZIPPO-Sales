using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zippo_Manage.Helper;
using Zippo_Manage.Models;

namespace Zippo_Manage.Controllers
{
    public class TaiKhoanController : Controller
    {
        QLyZIPPOContext db = new QLyZIPPOContext();
        public IActionResult Index()
        {
            var taikhoans = db.Taikhoans.Select(tk => new TaiKhoanModel
            {
                MaTk = tk.MaTk,
                TenDangNhap = tk.TenDangNhap,
                MatKhau = tk.MatKhau,
                Vaitro = tk.Vaitro,
                Ngaytao = tk.Ngaytao
            }).ToList();

            return View(taikhoans);
        }
        public IActionResult themTK()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult themTK(Taikhoan tk)
        {
            if (ModelState.IsValid)
            {
                tk.Ngaytao = DateTime.Now;
                db.Taikhoans.Add(tk);
                db.SaveChanges();
                TempData["Success"] = "Thêm tài khoản thành công!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Dữ liệu không hợp lệ!";
            return View(tk); 
        }
        public IActionResult formXoaTaiKhoan(string maTk)
        {
            if (string.IsNullOrEmpty(maTk))
            {
                return BadRequest("Mã tài khoản không hợp lệ.");
            }

            var tk = db.Taikhoans
                       .FirstOrDefault(a => a.MaTk == maTk);

            if (tk == null)
            {
                ViewBag.flag = 0; 
                return View();
            }

            ViewBag.flag = 1; 
            return View(tk); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult XoaTaiKhoanConfirmed(string maTk)
        {
            if (string.IsNullOrEmpty(maTk))
            {
                return BadRequest("Mã tài khoản không hợp lệ.");
            }

            var tk = db.Taikhoans.Find(maTk);
            if (tk == null)
            {
                return NotFound("Tài khoản không tồn tại.");
            }

            db.Taikhoans.Remove(tk);
            db.SaveChanges();

            return RedirectToAction("Index"); 
        }

        public IActionResult dangNhap()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult dangNhap(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Taikhoans
                    .FirstOrDefault(tk => tk.TenDangNhap == model.TenDangNhap && tk.MatKhau == model.MatKhau);

                if (user != null)
                {
                    var khachHang = db.Khachhangs.FirstOrDefault(kh => kh.MaKh == user.MaKh);

                    HttpContext.Session.SetString("TenDangNhap", user.TenDangNhap);
                    HttpContext.Session.SetString("Vaitro", user.Vaitro);
                    HttpContext.Session.SetString("MaKh", user.MaKh);

                    if (khachHang != null)
                    {
                        HttpContext.Session.SetString("TenKhachHang", $"{khachHang.HoLot} {khachHang.Ten}");
                    }
                    else
                    {
                        HttpContext.Session.SetString("TenKhachHang", "Người dùng không rõ");
                    }

                    var gioHangBackup = db.Giohangs
                        .Include(gh => gh.ChiTietGioHangs)
                        .ThenInclude(ct => ct.MaSpNavigation)
                        .FirstOrDefault(gh => gh.MaKh == user.MaKh && gh.TrangThai == "Chưa xác nhận");

                    if (gioHangBackup != null)
                    {
                        var gioHang = gioHangBackup.ChiTietGioHangs.Select(ct => new Giohang
                        {
                            Sanpham = ct.MaSpNavigation,
                            SoLuongMua = ct.SoLuong,
                            Gia = ct.Gia
                        }).ToList();

                        HttpContext.Session.SetObjectAsJson("GioHang", gioHang);
                    }

                    TempData["Message"] = user.Vaitro == "Admin"
                        ? "Đăng nhập thành công! Chào mừng Admin!"
                        : "Đăng nhập thành công! Chào mừng User!";

                    return RedirectToAction(user.Vaitro == "Admin" ? "dashBoardManage" : "Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!");
                }
            }

            return View(model);
        }

        public IActionResult dangKy()
        {
            return View();
        }
        public IActionResult DangXuat()
        {
            var gioHang = HttpContext.Session.GetObjectFromJson<List<Giohang>>("GioHang");
            var maKh = HttpContext.Session.GetString("MaKh");

            if (gioHang != null && gioHang.Any() && !string.IsNullOrEmpty(maKh))
            {
                var khachHang = db.Khachhangs.FirstOrDefault(kh => kh.MaKh == maKh);

                if (khachHang != null)
                {
                    var donHang = new Giohang
                    {
                        MaGh = Guid.NewGuid().ToString(), // Tạo mã giỏ hàng mới
                        MaKh = maKh,
                        NgayTao = DateTime.Now,
                        TrangThai = "Chưa xác nhận"
                    };

                    db.Giohangs.Add(donHang); // Lưu đơn hàng mới

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

                    return RedirectToAction("dangNhap","TaiKhoan"); 
                }
            }

            HttpContext.Session.Clear();

            TempData["Message"] = "Đăng xuất thành công và giỏ hàng đã được lưu!";

            return RedirectToAction("dangNhap", "TaiKhoan");
        }

    }

}
   
