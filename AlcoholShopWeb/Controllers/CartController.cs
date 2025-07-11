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

        public async Task<IActionResult> Index(int UserID)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserID == UserID);

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(int UserID, int productID, int quantity)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserID == UserID);

            if (cart == null)
            {
                cart = new Cart { UserID = UserID, CreatedAt = DateTime.UtcNow };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var item = await _context.CartItems
                .FirstOrDefaultAsync(i => i.CartID == cart.CartID && i.ProductID == productID);

            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                item = new CartItem { CartID = cart.CartID, ProductID = productID, Quantity = quantity };
                _context.CartItems.Add(item);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { UserID });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int UserID, int productID)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserID == UserID);

            if (cart != null)
            {
                var item = await _context.CartItems
                    .FirstOrDefaultAsync(i => i.CartID == cart.CartID && i.ProductID == productID);

                if (item != null)
                {
                    _context.CartItems.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index", new { UserID });
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart(int UserID)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserID == UserID);

            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", new { UserID });
        }
    }

}
