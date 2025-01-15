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
    public class PilociController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PilociController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var piloci = await _context.Piloci.Include(p => p.LinieLotnicze).ToListAsync();
            return View(piloci);
        }

        public IActionResult Create()
        {
            ViewData["LinieLotnicze"] = new SelectList(_context.LinieLotnicze, "Id_Linii", "Nazwa");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Piloci pilot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pilot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LinieLotnicze"] = new SelectList(_context.LinieLotnicze, "Id_Linii", "Nazwa", pilot.Id_Linii);
            return View(pilot);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pilot = await _context.Piloci.FindAsync(id);
            if (pilot == null)
            {
                return NotFound();
            }
            ViewData["LinieLotnicze"] = new SelectList(_context.LinieLotnicze, "Id_Linii", "Nazwa", pilot.Id_Linii);
            return View(pilot);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Piloci pilot)
        {
            if (id != pilot.Id_Pilota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pilot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PilociExists(pilot.Id_Pilota))
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
            ViewData["LinieLotnicze"] = new SelectList(_context.LinieLotnicze, "Id_Linii", "Nazwa", pilot.Id_Linii);
            return View(pilot);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pilot = await _context.Piloci
                .Include(p => p.LinieLotnicze)
                .FirstOrDefaultAsync(m => m.Id_Pilota == id);
            if (pilot == null)
            {
                return NotFound();
            }

            return View(pilot);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pilot = await _context.Piloci.FindAsync(id);
            _context.Piloci.Remove(pilot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PilociExists(int id)
        {
            return _context.Piloci.Any(e => e.Id_Pilota == id);
        }
    }
} 