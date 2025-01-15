using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AplikacjaBazodanowa.Data;
using AplikacjaBazodanowa.Models;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaBazodanowa.Controllers
{
    public class LotyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LotyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Pobieranie listy lotów
            var loty = await _context.Loty.ToListAsync();
            return View(loty);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lot = await _context.Loty
                .FirstOrDefaultAsync(m => m.Id_Lotu == id); // Zmiana z 'Id' na 'Id_Lotu'

            if (lot == null)
            {
                return NotFound();
            }

            return View(lot);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id_Lotu,Miasto_Odlotu,Miasto_Przylotu,Data_Odlotu,Data_Przylotu,Id_Samolotu")] Loty lot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lot);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lot = await _context.Loty.FindAsync(id);
            if (lot == null)
            {
                return NotFound();
            }
            return View(lot);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Lotu,Miasto_Odlotu,Miasto_Przylotu,Data_Odlotu,Data_Przylotu,Id_Samolotu")] Loty lot)
        {
            if (id != lot.Id_Lotu) // Zmiana z 'Id' na 'Id_Lotu'
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LotExists(lot.Id_Lotu)) // Zmiana z 'Id' na 'Id_Lotu'
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
            return View(lot);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lot = await _context.Loty
                .FirstOrDefaultAsync(m => m.Id_Lotu == id); // Zmiana z 'Id' na 'Id_Lotu'

            if (lot == null)
            {
                return NotFound();
            }

            return View(lot);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lot = await _context.Loty.FindAsync(id);
            _context.Loty.Remove(lot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LotExists(int id)
        {
            return _context.Loty.Any(e => e.Id_Lotu == id); // Zmiana z 'Id' na 'Id_Lotu'
        }
    }
}
