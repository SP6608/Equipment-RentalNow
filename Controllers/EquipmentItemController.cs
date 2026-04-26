using Equipment_RentalNow.Data;
using Equipment_RentalNow.Data.Models;
using Equipment_RentalNow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Equipment_RentalNow.Controllers
{
    public class EquipmentItemController : Controller
    {
        private readonly ApplicationDbContext context;

        public EquipmentItemController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            var query = context.EquipmentItems.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e => e.Name.Contains(search));
            }

            var equipment = await query
                .OrderBy(e => e.Name)
                .ToListAsync();

            ViewBag.Search = search;

            return View(equipment);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var item = await context.EquipmentItems
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EquipmentItem item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            await context.EquipmentItems.AddAsync(item);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await context.EquipmentItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EquipmentItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(item);
            }

            context.EquipmentItems.Update(item);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await context.EquipmentItems
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await context.EquipmentItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            context.EquipmentItems.Remove(item);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}