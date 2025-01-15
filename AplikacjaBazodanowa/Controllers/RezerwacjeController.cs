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
    public class RezerwacjeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RezerwacjeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var rezerwacje = await _context.Rezerwacje
                .Include(r => r.Pasazer)
                .Include(r => r.Lot)
                .ToListAsync();
            return View(rezerwacje);
        }

        public IActionResult Create()
        {
            ViewData["Pasazerowie"] = new SelectList(_context.Pasazerowie, "Id_Pasazera", "Nazwisko");
            ViewData["Loty"] = new SelectList(_context.Loty, "Id_Lotu", "Miasto_Odlotu");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rezerwacje rezerwacja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezerwacja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Pasazerowie"] = new SelectList(_context.Pasazerowie, "Id_Pasazera", "Nazwisko", rezerwacja.Id_Pasazera);
            ViewData["Loty"] = new SelectList(_context.Loty, "Id_Lotu", "Miasto_Odlotu", rezerwacja.Id_Lotu);
            return View(rezerwacja);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezerwacja = await _context.Rezerwacje.FindAsync(id);
            if (rezerwacja == null)
            {
                return NotFound();
            }
            ViewData["Pasazerowie"] = new SelectList(_context.Pasazerowie, "Id_Pasazera", "Nazwisko", rezerwacja.Id_Pasazera);
            ViewData["Loty"] = new SelectList(_context.Loty, "Id_Lotu", "Miasto_Odlotu", rezerwacja.Id_Lotu);
            return View(rezerwacja);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Rezerwacje rezerwacja)
        {
            if (id != rezerwacja.Id_Rezerwacji)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezerwacja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezerwacjeExists(rezerwacja.Id_Rezerwacji))
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
            ViewData["Pasazerowie"] = new SelectList(_context.Pasazerowie, "Id_Pasazera", "Nazwisko", rezerwacja.Id_Pasazera);
            ViewData["Loty"] = new SelectList(_context.Loty, "Id_Lotu", "Miasto_Odlotu", rezerwacja.Id_Lotu);
            return View(rezerwacja);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezerwacja = await _context.Rezerwacje
                .Include(r => r.Pasazer)
                .Include(r => r.Lot)
                .FirstOrDefaultAsync(m => m.Id_Rezerwacji == id);
            if (rezerwacja == null)
            {
                return NotFound();
            }

            return View(rezerwacja);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezerwacja = await _context.Rezerwacje.FindAsync(id);
            _context.Rezerwacje.Remove(rezerwacja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezerwacjeExists(int id)
        {
            return _context.Rezerwacje.Any(e => e.Id_Rezerwacji == id);
        }
    }
} 