using AlcoholShopWeb.Data;
using AlcoholShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AlcoholShopWeb.Controllers
{
    public class AdminBlogController : Controller
    {
        private readonly AlcoholShopContext _context;

        public AdminBlogController(AlcoholShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _context.BlogPosts
                .Include(p => p.BlogCategory)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return View(posts);
        }

        public IActionResult Create()
        {
            ViewData["BlogCategoryID"] = new SelectList(_context.BlogCategories, "BlogCategoryID", "Name");
            ViewData["Tags"] = _context.BlogTags.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogPost post, int[] selectedTags)
        {
            if (ModelState.IsValid)
            {
                post.CreatedAt = DateTime.UtcNow;
                _context.BlogPosts.Add(post);
                await _context.SaveChangesAsync();

                foreach (var tagId in selectedTags)
                    _context.BlogPostTags.Add(new BlogPostTag { PostID = post.PostID, TagID = tagId });

                await _context.SaveChangesAsync();
                await LogAction("Stworzono post", $"Stworzony nowy post o Nazwie: {post.Title}");
                return RedirectToAction(nameof(Index));
            }

            ViewData["BlogCategoryID"] = new SelectList(_context.BlogCategories, "BlogCategoryID", "Name", post.BlogCategoryID);
            ViewData["Tags"] = _context.BlogTags.ToList();
            return View(post);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var post = await _context.BlogPosts
                .Include(p => p.BlogPostTags)
                .FirstOrDefaultAsync(p => p.PostID == id);

            if (post == null) return NotFound();

            ViewData["BlogCategoryID"] = new SelectList(_context.BlogCategories, "BlogCategoryID", "Name", post.BlogCategoryID);
            ViewData["Tags"] = _context.BlogTags.ToList();
            ViewData["SelectedTags"] = post.BlogPostTags.Select(pt => pt.TagID).ToArray();

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogPost post, int[] selectedTags)
        {
            if (ModelState.IsValid)
            {
                _context.Update(post);
                await _context.SaveChangesAsync();

                var oldTags = _context.BlogPostTags.Where(t => t.PostID == post.PostID);
                _context.BlogPostTags.RemoveRange(oldTags);

                foreach (var tagId in selectedTags)
                    _context.BlogPostTags.Add(new BlogPostTag { PostID = post.PostID, TagID = tagId });

                await _context.SaveChangesAsync();

                await LogAction("Edycja postu", $"Zedytowano post o ID: {post.PostID}, Nazwa: {post.Title}");
                return RedirectToAction(nameof(Index));
            }

            ViewData["BlogCategoryID"] = new SelectList(_context.BlogCategories, "BlogCategoryID", "Name", post.BlogCategoryID);
            ViewData["Tags"] = _context.BlogTags.ToList();
            ViewData["SelectedTags"] = selectedTags;

            return View(post);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var post = await _context.BlogPosts
                .Include(p => p.BlogPostTags)
                .FirstOrDefaultAsync(p => p.PostID == id);

            if (post != null)
            {
                _context.BlogPostTags.RemoveRange(post.BlogPostTags);

                _context.BlogPosts.Remove(post);

                await _context.SaveChangesAsync();
                await LogAction("Usunięcie postu", $"Usunięto post o ID: {post.PostID}, Nazwa: {post.Title}");
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task LogAction(string action, string? description = null)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            var log = new Log
            {
                UserID = userId,
                Action = action,
                Description = description,
                CreatedAt = DateTime.Now
            };

            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }

    }
}
