using AlcoholShop.Models;
using AlcoholShopWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace AlcoholShop.Controllers
{
    public class CartController : Controller
    {
        private readonly AlcoholShopContext _context;

        public CartController(AlcoholShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(int userId, int productId, int quantity)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId, CreatedAt = DateTime.UtcNow };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var item = await _context.CartItems
                .FirstOrDefaultAsync(i => i.CartId == cart.CartId && i.ProductId == productId);

            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                item = new CartItem { CartId = cart.CartId, ProductId = productId, Quantity = quantity };
                _context.CartItems.Add(item);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { userId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int userId, int productId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                var item = await _context.CartItems
                    .FirstOrDefaultAsync(i => i.CartId == cart.CartId && i.ProductId == productId);

                if (item != null)
                {
                    _context.CartItems.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index", new { userId });
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", new { userId });
        }
    }

}
