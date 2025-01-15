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
    public class ZalogaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZalogaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var zaloga = await _context.Zaloga
                .Include(z => z.Lot)
                .Include(z => z.Pilot)
                .ToListAsync();
            return View(zaloga);
        }

        public IActionResult Create()
        {
            ViewData["Loty"] = new SelectList(_context.Loty, "Id_Lotu", "Miasto_Odlotu");
            ViewData["Piloci"] = new SelectList(_context.Piloci, "Id_Pilota", "Nazwisko");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Zaloga zaloga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zaloga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Loty"] = new SelectList(_context.Loty, "Id_Lotu", "Miasto_Odlotu", zaloga.Id_Lotu);
            ViewData["Piloci"] = new SelectList(_context.Piloci, "Id_Pilota", "Nazwisko", zaloga.Id_Pilota);
            return View(zaloga);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaloga = await _context.Zaloga.FindAsync(id);
            if (zaloga == null)
            {
                return NotFound();
            }
            ViewData["Loty"] = new SelectList(_context.Loty, "Id_Lotu", "Miasto_Odlotu", zaloga.Id_Lotu);
            ViewData["Piloci"] = new SelectList(_context.Piloci, "Id_Pilota", "Nazwisko", zaloga.Id_Pilota);
            return View(zaloga);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Zaloga zaloga)
        {
            if (id != zaloga.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zaloga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZalogaExists(zaloga.Id))
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
            ViewData["Loty"] = new SelectList(_context.Loty, "Id_Lotu", "Miasto_Odlotu", zaloga.Id_Lotu);
            ViewData["Piloci"] = new SelectList(_context.Piloci, "Id_Pilota", "Nazwisko", zaloga.Id_Pilota);
            return View(zaloga);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaloga = await _context.Zaloga
                .Include(z => z.Lot)
                .Include(z => z.Pilot)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zaloga == null)
            {
                return NotFound();
            }

            return View(zaloga);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zaloga = await _context.Zaloga.FindAsync(id);
            _context.Zaloga.Remove(zaloga);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZalogaExists(int id)
        {
            return _context.Zaloga.Any(e => e.Id == id);
        }
    }
} 