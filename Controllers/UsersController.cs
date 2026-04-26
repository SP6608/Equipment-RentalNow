using Equipment_RentalNow.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Equipment_RentalNow.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public UsersController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users
                .OrderBy(u => u.Email)
                .ToListAsync();

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                TempData["Error"] = "Admin user cannot be deleted.";
                return RedirectToAction(nameof(Index));
            }

            await userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}