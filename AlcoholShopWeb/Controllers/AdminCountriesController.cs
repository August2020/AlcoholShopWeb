using Microsoft.AspNetCore.Mvc;
using AlcoholShopWeb.Data;
using AlcoholShopWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AlcoholShopWeb.Controllers
{
    public class AdminCountriesController : Controller
    {
        private readonly AlcoholShopContext _context;

        public AdminCountriesController(AlcoholShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() => View(await _context.Countries.ToListAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Countries.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            return country == null ? NotFound() : View(country);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Update(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
