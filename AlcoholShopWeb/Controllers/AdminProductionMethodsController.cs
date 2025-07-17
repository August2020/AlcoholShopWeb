using Microsoft.AspNetCore.Mvc;
using AlcoholShopWeb.Data;
using AlcoholShopWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AlcoholShopWeb.Controllers
{
    public class AdminProductionMethodsController : Controller
    {
        private readonly AlcoholShopContext _context;

        public AdminProductionMethodsController(AlcoholShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() => View(await _context.ProductionMethods.ToListAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ProductionMethod method)
        {
            if (ModelState.IsValid)
            {
                _context.ProductionMethods.Add(method);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(method);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var method = await _context.ProductionMethods.FindAsync(id);
            return method == null ? NotFound() : View(method);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductionMethod method)
        {
            if (ModelState.IsValid)
            {
                _context.Update(method);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(method);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var method = await _context.ProductionMethods.FindAsync(id);
            if (method != null)
            {
                _context.ProductionMethods.Remove(method);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
