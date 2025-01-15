using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AplikacjaBazodanowa.Data;
using AplikacjaBazodanowa.Models;


namespace AplikacjaBazodanowa.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PracownicyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PracownicyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Pracownicy.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pracownicy pracownik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pracownik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pracownik);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pracownik = await _context.Pracownicy.FindAsync(id);
            if (pracownik == null)
            {
                return NotFound();
            }
            return View(pracownik);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Pracownicy pracownik)
        {
            if (id != pracownik.Id_Pracownika)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pracownik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PracownicyExists(pracownik.Id_Pracownika))
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
            return View(pracownik);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pracownik = await _context.Pracownicy
                .FirstOrDefaultAsync(m => m.Id_Pracownika == id);
            if (pracownik == null)
            {
                return NotFound();
            }

            return View(pracownik);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pracownik = await _context.Pracownicy.FindAsync(id);
            _context.Pracownicy.Remove(pracownik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PracownicyExists(int id)
        {
            return _context.Pracownicy.Any(e => e.Id_Pracownika == id);
        }
    }
} 