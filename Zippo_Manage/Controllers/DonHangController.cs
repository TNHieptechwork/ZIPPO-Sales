using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zippo_Manage.Models;

namespace Zippo_Manage.Controllers
{
    public class DonHangController : Controller
    {
        QLyZIPPOContext db = new QLyZIPPOContext();
        public IActionResult Index()
        {
           return View();
        }
        public IActionResult dsDonHang()
        {
            var donhangList = db.Donhangs.Include(dh => dh.MaKhNavigation)
                                                .Include(dh => dh.ChiTietDonHangs)
                                                .ToList();
            return View(donhangList);
        }

    }
}
