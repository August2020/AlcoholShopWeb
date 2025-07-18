﻿using Microsoft.AspNetCore.Mvc;
using AlcoholShopWeb.Data;
using AlcoholShopWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AlcoholShopWeb.Controllers
{
    public class AdminProducersController : Controller
    {
        private readonly AlcoholShopContext _context;

        public AdminProducersController(AlcoholShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() => View(await _context.Producers.ToListAsync());

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

        public async Task<IActionResult> Edit(int id)
        {
            var producer = await _context.Producers.FindAsync(id);
            return producer == null ? NotFound() : View(producer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Producer producer)
        {
            if (ModelState.IsValid)
            {
                _context.Update(producer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var producer = await _context.Producers.FindAsync(id);
            if (producer != null)
            {
                _context.Producers.Remove(producer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
