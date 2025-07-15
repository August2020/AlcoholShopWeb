
using AlcoholShopWeb.Data;
using AlcoholShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlcoholShopWeb.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AlcoholShopContext _context;

        public ProductsController(AlcoholShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Producer)
                .Where(p => p.Availability == true)
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
                .Include(p => p.Reviews)
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null) return NotFound();

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(int productId, string comment, int rating)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || string.IsNullOrWhiteSpace(comment) || rating < 1 || rating > 5)
                return RedirectToAction("Details", new { id = productId });

            var review = new Review
            {
                ProductID = productId,
                UserID = userId.Value,
                Comment = comment,
                Rating = rating,
                CreatedAt = DateTime.UtcNow
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = productId });
        }

    }
}
