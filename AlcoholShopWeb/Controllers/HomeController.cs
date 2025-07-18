using AlcoholShopWeb.Data;
using AlcoholShopWeb.Models;
using AlcoholShopWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AlcoholShopWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly AlcoholShopContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AlcoholShopContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var beer = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Category.Name == "Piwo")
                .OrderBy(p => Guid.NewGuid()) // losowo
                .FirstOrDefaultAsync();

            var vodka = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Category.Name == "Wódka")
                .OrderBy(p => Guid.NewGuid())
                .FirstOrDefaultAsync();

            var whisky = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Category.Name == "Whisky")
                .OrderBy(p => Guid.NewGuid())
                .FirstOrDefaultAsync();

            var blogs = await _context.BlogPosts
                .Where(b => b.IsPublished)
                .OrderByDescending(b => b.PublishDate)
                .Take(3)
                .ToListAsync();

            var vm = new HomeViewModel
            {
                FeaturedProducts = new List<Product?> { beer, vodka, whisky },
                LatestBlogPosts = blogs
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
