using AlcoholShopWeb.Data;
using AlcoholShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AlcoholShopWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly AlcoholShopContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountController(AlcoholShopContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(User model, string password)
        {

            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                ModelState.AddModelError("", "Użytkownik z tym e-mailem już istnieje.");
                return View(model);
            }

            model.PasswordHash = _passwordHasher.HashPassword(model, password);
            model.Role = "Client";
            model.CreatedAt = DateTime.UtcNow;

            _context.Users.Add(model);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetInt32("UserId", model.UserID);
            HttpContext.Session.SetString("UserRole", model.Role);
            HttpContext.Session.SetString("UserName", model.FirstName);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                ModelState.AddModelError("", "Nieprawidłowe dane logowania.");
                return View();
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Nieprawidłowe dane logowania.");
                return View();
            }

            HttpContext.Session.SetInt32("UserId", user.UserID);
            HttpContext.Session.SetString("UserRole", user.Role);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return NotFound();

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(User updatedUser)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return NotFound();

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Email = updatedUser.Email;

            _context.Update(user);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Dane zostały zaktualizowane";
            return RedirectToAction("Profile");
        }

        public async Task<IActionResult> Orders()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return NotFound();

            var orders = await _context.Orders
                .Where(o => o.UserID == userId)
                .Include(o => o.Status)
                .Include(o => o.OrderItems).ThenInclude(i => i.Product)
                .ToListAsync();

            return View(orders);
        }

    }
}
