using Equipment_RentalNow.Data;
using Equipment_RentalNow.Data.Models;
using Equipment_RentalNow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Equipment_RentalNow.Controllers
{
    [Authorize]
    public class RentalRequestsController : Controller
    {
        private readonly ApplicationDbContext context;

        public RentalRequestsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var requests = await context.RentalRequests
                .Include(r => r.User)
                .Include(r => r.RentalRequestItems)
                    .ThenInclude(ri => ri.EquipmentItem)
                .OrderByDescending(r => r.StartDate)
                .ToListAsync();

            return View(requests);
        }

        [HttpGet]
        public async Task<IActionResult> MyRequests()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var requests = await context.RentalRequests
                .Include(r => r.RentalRequestItems)
                    .ThenInclude(ri => ri.EquipmentItem)
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.StartDate)
                .ToListAsync();

            return View(requests);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.EquipmentItems = await context.EquipmentItems
                .AsNoTracking()
                .Where(e => e.AvailableQuantity > 0)
                .OrderBy(e => e.Name)
                .ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            DateTime startDate,
            DateTime endDate,
            string? comment,
            int[] equipmentIds,
            int[] quantities)
        {
            if (endDate < startDate)
            {
                ModelState.AddModelError(string.Empty, "End date cannot be before start date.");
            }

            if (equipmentIds.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "Select at least one equipment item.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.EquipmentItems = await context.EquipmentItems
                    .AsNoTracking()
                    .Where(e => e.AvailableQuantity > 0)
                    .OrderBy(e => e.Name)
                    .ToListAsync();

                return View();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            RentalRequest request = new RentalRequest
            {
                StartDate = startDate,
                EndDate = endDate,
                Comment = comment,
                Status = "В изчакване",
                UserId = userId
            };

            for (int i = 0; i < equipmentIds.Length; i++)
            {
                if (quantities[i] > 0)
                {
                    request.RentalRequestItems.Add(new RentalRequestItem
                    {
                        EquipmentItemId = equipmentIds[i],
                        Quantity = quantities[i]
                    });
                }
            }

            await context.RentalRequests.AddAsync(request);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(MyRequests));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, string status)
        {
            var request = await context.RentalRequests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            request.Status = status;
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var request = await context.RentalRequests
                .Include(r => r.RentalRequestItems)
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);

            if (request == null)
            {
                return NotFound();
            }

            if (request.Status != "В изчакване")
            {
                return Forbid();
            }

            context.RentalRequests.Remove(request);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(MyRequests));
        }
    }
}