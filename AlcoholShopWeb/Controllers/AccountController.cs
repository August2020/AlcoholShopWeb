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

            Console.WriteLine($"Formularz: {model.Email}, {model.FirstName}, {password}");
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Nieprawidłowy model!");
                return View(model);
            }

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
    }
}
