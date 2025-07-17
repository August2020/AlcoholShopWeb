
using AlcoholShopWeb.Data;
using AlcoholShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AlcoholShopWeb.Controllers
{
    public class AdminProductsController : Controller
    {
        private readonly AlcoholShopContext _context;

        public AdminProductsController(AlcoholShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Producer)
                .ToListAsync();

            return View(products);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Producer)
                .Include(p => p.Country)
                .Include(p => p.ProductionMethod)
                .Include(p => p.Aging)
                .FirstOrDefaultAsync(m => m.ProductID == id);

            if (product == null) return NotFound();

            return View(product);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryID", "Name");
            ViewBag.Producers = new SelectList(_context.Producers, "ProducerID", "Name");
            ViewBag.Countries = new SelectList(_context.Countries, "CountryID", "Name");
            ViewBag.ProductionMethods = new SelectList(_context.ProductionMethods, "ProductionMethodID", "Name");
            ViewBag.Aging = new SelectList(_context.Aging, "AgingID", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryID", "Name");
            ViewBag.Producers = new SelectList(_context.Producers, "ProducerID", "Name");
            ViewBag.Countries = new SelectList(_context.Countries, "CountryID", "Name");
            ViewBag.ProductionMethods = new SelectList(_context.ProductionMethods, "ProductionMethodID", "Name");
            ViewBag.Aging = new SelectList(_context.Aging, "AgingID", "Name");

            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.Producers = new SelectList(_context.Producers, "ProducerID", "Name", product.ProducerID);
            ViewBag.Countries = new SelectList(_context.Countries, "CountryID", "Name", product.CountryID);
            ViewBag.ProductionMethods = new SelectList(_context.ProductionMethods, "ProductionMethodID", "Name", product.ProductionMethodID);
            ViewBag.Aging = new SelectList(_context.Aging, "AgingID", "Name", product.AgingID);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.ProductID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(e => e.ProductID == id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewBag.Categories = new SelectList(_context.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.Producers = new SelectList(_context.Producers, "ProducerID", "Name", product.ProducerID);
            ViewBag.Countries = new SelectList(_context.Countries, "CountryID", "Name", product.CountryID);
            ViewBag.ProductionMethods = new SelectList(_context.ProductionMethods, "ProductionMethodID", "Name", product.ProductionMethodID);
            ViewBag.Aging = new SelectList(_context.Aging, "AgingID", "Name", product.AgingID);

            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var post = await _context.BlogPosts
                .Include(p => p.BlogCategory)
                .FirstOrDefaultAsync(p => p.PostID == id);

            if (post == null)
                return NotFound();

            return View(post);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.BlogPosts
                .Include(p => p.BlogPostTags)
                .FirstOrDefaultAsync(p => p.PostID == id);

            if (post != null)
            {
                _context.BlogPostTags.RemoveRange(post.BlogPostTags);
                _context.BlogPosts.Remove(post);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
