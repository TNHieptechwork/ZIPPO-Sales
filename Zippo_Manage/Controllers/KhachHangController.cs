using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zippo_Manage.Models;

namespace Zippo_Manage.Controllers
{
    public class KhachHangController : Controller
    {
        QLyZIPPOContext db = new QLyZIPPOContext();
        public IActionResult Index()
        {
            var customers = db.Khachhangs.ToList(); 
            return View(customers);
        }
    }
}
