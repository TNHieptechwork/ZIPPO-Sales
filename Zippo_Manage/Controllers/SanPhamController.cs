using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zippo_Manage.Models;
using Microsoft.AspNetCore.Http;

namespace Zippo_Manage.Controllers
{
    public class SanPhamController : Controller
    {
        QLyZIPPOContext db = new QLyZIPPOContext();
        public IActionResult Index()
        {
            var sanphamEntities = db.Sanphams?.ToList();
            var sanphamModels = sanphamEntities.Select(s => new SanPhamModel
            {
                MaSp = s.MaSp,
                TenSp = s.TenSp,
                MotaSp = s.MotaSp,
                Gia = s.Gia,
                SoLuong = s.SoLuong,
                HinhAnhPath = s.HinhAnh,
                MaLoai = s.MaLoai
            }).ToList();

            return View(sanphamModels);
        }
        [HttpGet]
        public IActionResult TimKiemSanPham(string keyword, string maLoai, string maSp)
        {
            if (string.IsNullOrEmpty(keyword) && string.IsNullOrEmpty(maLoai) && string.IsNullOrEmpty(maSp))
            {
                return RedirectToAction("Index");
            }

            var query = db.Sanphams.Include(s => s.MaLoaiNavigation).AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(s => s.TenSp.Contains(keyword) || s.MotaSp.Contains(keyword));
            }

            if (!string.IsNullOrEmpty(maLoai))
            {
                query = query.Where(s => s.MaLoaiNavigation.MaLoai == maLoai || s.MaLoaiNavigation.TenLoai.Contains(maLoai));
            }

            if (!string.IsNullOrEmpty(maSp))
            {
                query = query.Where(s => s.MaSp.Contains(maSp));
            }

            var sanphamModels = query.Select(s => new SanPhamModel
            {
                MaSp = s.MaSp,
                TenSp = s.TenSp,
                MotaSp = s.MotaSp,
                Gia = s.Gia,
                SoLuong = s.SoLuong,
                HinhAnhPath = s.HinhAnh,
                MaLoai = s.MaLoaiNavigation.MaLoai,
                TenLoai = s.MaLoaiNavigation.TenLoai
            }).ToList();

            return View("TimKiemSanPham", sanphamModels);
        }

        public IActionResult dropDownSanPham(string maLoai)
        {
            if (string.IsNullOrEmpty(maLoai))
            {
                return BadRequest("Mã loại sản phẩm không hợp lệ.");
            }

            var loaiSanPham = db.Loaisanphams
                                .Where(l => l.MaLoai == maLoai)
                                .Select(l => new
                                {
                                    l.TenLoai,
                                    SanPhams = l.Sanphams
                                })
                                .FirstOrDefault();

            if (loaiSanPham == null)
            {
                return NotFound("Không tìm thấy loại sản phẩm với mã loại này.");
            }

            ViewBag.TenLoai = loaiSanPham.TenLoai;

            return View(loaiSanPham.SanPhams);
        }
        [HttpGet]
        public IActionResult XemMaLoaiSP(string maloai)
        {
            Loaisanpham lsp = db.Loaisanphams.Find(maloai);
            return PartialView(lsp);
        }

        public IActionResult themSP()
        {
            ViewBag.MaLoaiList = new SelectList(db.Loaisanphams.ToList(), "MaLoai", "MaLoai");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult themSP(SanPhamModel sanPhamModel)
        {

            if (db.Sanphams.Any(sp => sp.MaSp == sanPhamModel.MaSp))
            {
                ModelState.AddModelError("MaSp", "Mã sản phẩm đã tồn tại.");
                return View(sanPhamModel);
            }
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(sanPhamModel);
            }

            if (ModelState.IsValid)
            {
                var sanPham = new Sanpham
                {
                    MaSp = sanPhamModel.MaSp,
                    TenSp = sanPhamModel.TenSp,
                    MotaSp = sanPhamModel.MotaSp,
                    Gia = sanPhamModel.Gia,
                    SoLuong = sanPhamModel.SoLuong,
                    MaLoai = sanPhamModel.MaLoai
                };

                if (sanPhamModel.HinhAnh != null && sanPhamModel.HinhAnh.Length > 0)
                {
                    var filePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "img",
                        "products",
                        sanPhamModel.HinhAnh.FileName
                    );

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        sanPhamModel.HinhAnh.CopyTo(stream);
                    }

                    sanPham.HinhAnh = sanPhamModel.HinhAnh.FileName;
                }
                else
                {
                    ModelState.AddModelError("HinhAnh", "Vui lòng tải lên hình ảnh.");
                    return View(sanPhamModel);
                }

                db.Sanphams.Add(sanPham);
                db.SaveChanges();

                return RedirectToAction("dsSanPham");
            }

            return View(sanPhamModel);
        }
        public IActionResult dsSanPham()
        {
            var sanPhams = db.Sanphams
                .Include(s => s.MaLoaiNavigation) 
                .ToList();
            return View(sanPhams);
        }
        public IActionResult ChiTietSP_admin(string maSp)
        {
            if (string.IsNullOrEmpty(maSp))
            {
                return NotFound();
            }

            var sanPham = db.Sanphams
                .Include(s => s.MaLoaiNavigation)
                .FirstOrDefault(s => s.MaSp == maSp);
            var cleanedMaSp = sanPham.MaSp.Trim();

            var thumbnails = new List<string>();
            for (int i = 1; i <= 4; i++)
            {
                thumbnails.Add($"~/img/products/thumbnails/{cleanedMaSp}_{i}_thumb.jpg");
            }

            ViewData["Thumbnails"] = thumbnails;

            return View(sanPham);
        }
        public IActionResult ChiTietSanPham(string maSp)
        {
            if (string.IsNullOrEmpty(maSp))
            {
                return NotFound();
            }

            var sanPham = db.Sanphams
                .Include(s => s.MaLoaiNavigation) 
                .FirstOrDefault(s => s.MaSp == maSp);
            var cleanedMaSp = sanPham.MaSp.Trim();

            var thumbnails = new List<string>();
            for (int i = 1; i <= 4; i++) 
            {
                thumbnails.Add($"~/img/products/thumbnails/{cleanedMaSp}_{i}_thumb.jpg");
            }

            ViewData["Thumbnails"] = thumbnails;

            return View(sanPham);
        }

        public IActionResult formXoaSP(string maSp)
        {
            if (string.IsNullOrEmpty(maSp))
            {
                return BadRequest("Mã sản phẩm không hợp lệ.");
            }

            var sp = db.Sanphams
                       .Include(s => s.MaLoaiNavigation) 
                       .FirstOrDefault(a => a.MaSp == maSp);

            if (sp == null)
            {
                ViewBag.flag = 0;
                return View();
            }

            ViewBag.flag = 1;
            return View(sp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult XoaConfirmed(string maSp)
        {
            if (string.IsNullOrEmpty(maSp))
            {
                return BadRequest("Mã sản phẩm không hợp lệ.");
            }

            var sp = db.Sanphams.Find(maSp);
            if (sp == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }

            db.Sanphams.Remove(sp);
            db.SaveChanges();

            return RedirectToAction("dsSanPham");
        }
        
        public IActionResult formSuaSP(string maSp)
        {
            if (string.IsNullOrEmpty(maSp))
            {
                return BadRequest("Mã sản phẩm không hợp lệ.");
            }

            var sanPham = db.Sanphams.Find(maSp);
            if (sanPham == null)
            {
                return NotFound("Không tìm thấy sản phẩm.");
            }

            var sanPhamModel = new SanPhamModel
            {
                MaSp = sanPham.MaSp,
                TenSp = sanPham.TenSp,
                MotaSp = sanPham.MotaSp,
                Gia = sanPham.Gia,
                SoLuong = sanPham.SoLuong,
                MaLoai = sanPham.MaLoai,
                HinhAnhPath = sanPham.HinhAnh
            };

            ViewBag.MaLoaiList = new SelectList(db.Loaisanphams.ToList(), "MaLoai", "TenLoai", sanPham.MaLoai);
            return View(sanPhamModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult suaSP(SanPhamModel sanPhamModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MaLoaiList = new SelectList(db.Loaisanphams.ToList(), "MaLoai", "TenLoai", sanPhamModel.MaLoai);
                return View(sanPhamModel);
            }

            var sanPham = db.Sanphams.Find(sanPhamModel.MaSp);
            if (sanPham == null)
            {
                return NotFound("Không tìm thấy sản phẩm.");
            }

            sanPham.TenSp = sanPhamModel.TenSp;
            sanPham.MotaSp = sanPhamModel.MotaSp;
            sanPham.Gia = sanPhamModel.Gia;
            sanPham.SoLuong = sanPhamModel.SoLuong;
            sanPham.MaLoai = sanPhamModel.MaLoai;

            if (sanPhamModel.HinhAnh != null && sanPhamModel.HinhAnh.Length > 0)
            {
                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "img",
                    "products",
                    sanPhamModel.HinhAnh.FileName
                );

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    sanPhamModel.HinhAnh.CopyTo(stream);
                }

                sanPham.HinhAnh = sanPhamModel.HinhAnh.FileName;
            }

            db.Sanphams.Update(sanPham);
            db.SaveChanges();

            return RedirectToAction("dsSanPham");
        }


    }
}

