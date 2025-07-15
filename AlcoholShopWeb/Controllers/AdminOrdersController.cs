using AlcoholShopWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AlcoholShopWeb.Controllers
{
    public class AdminOrdersController : Controller
    {
        private readonly AlcoholShopContext _context;

        public AdminOrdersController(AlcoholShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? statusId)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Login", "Account");

            var statuses = await _context.OrderStatus.ToListAsync();
            ViewBag.Statuses = new SelectList(statuses, "StatusID", "Name");
            ViewBag.SelectedStatusId = statusId;

            var ordersQuery = _context.Orders
                .Include(o => o.User)
                .Include(o => o.Status)
                .OrderByDescending(o => o.CreatedAt)
                .AsQueryable();

            if (statusId.HasValue)
            {
                ordersQuery = ordersQuery.Where(o => o.StatusID == statusId.Value);
            }

            return View(await ordersQuery.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Login", "Account");

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Status)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderID == id);

            if (order == null)
                return NotFound();

            ViewBag.Statuses = await _context.OrderStatus.ToListAsync();
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int orderId, int statusId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                return NotFound();

            order.StatusID = statusId;
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = orderId });
        }
    }
}
