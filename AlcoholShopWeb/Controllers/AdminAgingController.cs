using Microsoft.AspNetCore.Mvc;
using AlcoholShopWeb.Data;
using AlcoholShopWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AlcoholShopWeb.Controllers
{
    public class AdminAgingController : Controller
    {
        private readonly AlcoholShopContext _context;

        public AdminAgingController(AlcoholShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() => View(await _context.Aging.ToListAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Aging aging)
        {
            if (ModelState.IsValid)
            {
                _context.Aging.Add(aging);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aging);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var aging = await _context.Aging.FindAsync(id);
            return aging == null ? NotFound() : View(aging);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Aging aging)
        {
            if (ModelState.IsValid)
            {
                _context.Update(aging);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aging);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var aging = await _context.Aging.FindAsync(id);
            if (aging != null)
            {
                _context.Aging.Remove(aging);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
