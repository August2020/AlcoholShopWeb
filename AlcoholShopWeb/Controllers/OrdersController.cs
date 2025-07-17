using AlcoholShopWeb.Data;
using AlcoholShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AlcoholShopWeb.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AlcoholShopContext _context;

        public OrdersController(AlcoholShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var cart = await _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserID == userId);

            if (cart == null || !cart.CartItems.Any())
                return RedirectToAction("Index", "Cart", new { userId });

            ViewBag.DeliveryMethods = new SelectList(_context.DeliveryMethods, "DeliveryMethodID", "Name");
            ViewBag.PaymentMethods = new SelectList(_context.PaymentMethods, "PaymentMethodID", "Name");

            var user = await _context.Users.FindAsync(userId);

            return View(new Order
            {
                UserID = user.UserID,
                Name = $"{user.FirstName} {user.LastName}",
                Email = user.Email
            });
        }

        [HttpPost]
        public async Task<IActionResult> Confirm(Order model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var cart = await _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserID == userId);

            if (cart == null || !cart.CartItems.Any())
                return RedirectToAction("Index", "Cart", new { userId });

            var order = new Order
            {
                UserID = userId,
                Name = model.Name,
                Address = model.Address,
                Email = model.Email,
                DeliveryMethodID = model.DeliveryMethodID,
                PaymentMethodID = model.PaymentMethodID,
                StatusID = 1,
                CreatedAt = DateTime.UtcNow,
                TotalAmount = cart.CartItems.Sum(i => i.Product.Price * i.Quantity)
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var item in cart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderID = order.OrderID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                };
                _context.OrderItems.Add(orderItem);
            }

            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();

            return View("Confirm");
        }
    }
}
