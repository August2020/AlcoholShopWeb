using Microsoft.AspNetCore.Mvc;
using AlcoholShopWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace AlcoholShopWeb.Controllers
{
    public class AdminLogsController : Controller
    {
        private readonly AlcoholShopContext _context;

        public AdminLogsController(AlcoholShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Logs()
        {
            var logs = await _context.Logs
                .Include(l => l.User)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();

            return View(logs);
        }
    }
}
