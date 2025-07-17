using AlcoholShopWeb.Data;
using AlcoholShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AdminProducersController : Controller
{
    private readonly AlcoholShopContext _context;

    public AdminProducersController(AlcoholShopContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Producers.ToListAsync());
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Producer producer)
    {
        if (ModelState.IsValid)
        {
            _context.Producers.Add(producer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(producer);
    }
}
