using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AplikacjaBazodanowa.Data;
using AplikacjaBazodanowa.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AplikacjaBazodanowa.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SamolotyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SamolotyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var samoloty = await _context.Samoloty.Include(s => s.LinieLotnicze).ToListAsync();
            return View(samoloty);
        }

        public IActionResult Create()
        {
            ViewData["LinieLotnicze"] = new SelectList(_context.LinieLotnicze, "Id_Linii", "Nazwa");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Samoloty samolot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(samolot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LinieLotnicze"] = new SelectList(_context.LinieLotnicze, "Id_Linii", "Nazwa", samolot.Id_Linii);
            return View(samolot);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samolot = await _context.Samoloty.FindAsync(id);
            if (samolot == null)
            {
                return NotFound();
            }
            ViewData["LinieLotnicze"] = new SelectList(_context.LinieLotnicze, "Id_Linii", "Nazwa", samolot.Id_Linii);
            return View(samolot);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Samoloty samolot)
        {
            if (id != samolot.Id_Samolotu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(samolot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SamolotyExists(samolot.Id_Samolotu))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LinieLotnicze"] = new SelectList(_context.LinieLotnicze, "Id_Linii", "Nazwa", samolot.Id_Linii);
            return View(samolot);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samolot = await _context.Samoloty
                .Include(s => s.LinieLotnicze)
                .FirstOrDefaultAsync(m => m.Id_Samolotu == id);
            if (samolot == null)
            {
                return NotFound();
            }

            return View(samolot);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var samolot = await _context.Samoloty.FindAsync(id);
            _context.Samoloty.Remove(samolot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SamolotyExists(int id)
        {
            return _context.Samoloty.Any(e => e.Id_Samolotu == id);
        }
    }
} 