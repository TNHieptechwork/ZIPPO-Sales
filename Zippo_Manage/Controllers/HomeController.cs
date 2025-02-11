using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Zippo_Manage.Models;

namespace Zippo_Manage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        QLyZIPPOContext db = new QLyZIPPOContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var sanphamEntities = db.Sanphams.ToList();
            var sanphamModels = sanphamEntities.Select(s => new SanPhamModel
            {
                MaSp = s.MaSp,
                TenSp = s.TenSp,
                MotaSp = s.MotaSp,
                Gia = s.Gia,  // Gia có thể là null hoặc có giá trị, nên không cần chuyển đổi kiểu
                SoLuong = s.SoLuong ?? 0,
                HinhAnhPath = s.HinhAnh,
                MaLoai = s.MaLoai
            }).ToList();

            return View(sanphamModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult GioiThieu()
        {
            return View();
        }

        public IActionResult dashBoardManage()
        {
            int totalProducts = db.Sanphams.Count();

            int totalOrders = db.Donhangs.Count();

            int totalCustomers = db.Khachhangs.Count();

            var model = new dashBoardModel
            {
                SoSP = totalProducts,
                SoDH = totalOrders,
                SoKhachHang = totalCustomers,
            };

            return View(model);
        }

    }
}
