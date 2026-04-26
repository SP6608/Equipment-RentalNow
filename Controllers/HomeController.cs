using Equipment_RentalNow.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Equipment_RentalNow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.UsersCount = await context.Users.CountAsync();
            ViewBag.EquipmentCount = await context.EquipmentItems.CountAsync();
            ViewBag.RequestsCount = await context.RentalRequests.CountAsync();
            ViewBag.PendingRequestsCount = await context.RentalRequests
                .CountAsync(r => r.Status == "В изчакване");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}