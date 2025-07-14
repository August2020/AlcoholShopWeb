using AlcoholShopWeb.Data;
using AlcoholShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlcoholShopWeb.Controllers
{
    public class BlogController : Controller
    {
        private readonly AlcoholShopContext _context;

        public BlogController(AlcoholShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _context.BlogPosts
                .Where(p => p.IsPublished && p.PublishDate <= DateTime.Now)
                .Include(p => p.BlogCategory)
                .Include(p => p.BlogPostTags).ThenInclude(t => t.Tag)
                .OrderByDescending(p => p.PublishDate)
                .ToListAsync();

            return View(posts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _context.BlogPosts
                .Include(p => p.BlogCategory)
                .Include(p => p.BlogPostTags).ThenInclude(t => t.Tag)
                .FirstOrDefaultAsync(p => p.PostID == id && p.IsPublished);

            if (post == null) return NotFound();

            return View(post);
        }
    }
}