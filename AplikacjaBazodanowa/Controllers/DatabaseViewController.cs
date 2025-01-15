using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AplikacjaBazodanowa.Data;
using AplikacjaBazodanowa.Models;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaBazodanowa.Controllers
{
    public class DatabaseViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DatabaseViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Pobieranie listy lotów
            var loty = await _context.Loty.ToListAsync();
            return View(loty);
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return RedirectToAction(nameof(Index));
            }

            // Filtrowanie lotów
            var wyniki = await _context.Loty
                .Where(l => l.Miasto_Odlotu.Contains(searchTerm) || l.Miasto_Przylotu.Contains(searchTerm))
                .ToListAsync();

            return View("Index", wyniki);
        }
    }
}
