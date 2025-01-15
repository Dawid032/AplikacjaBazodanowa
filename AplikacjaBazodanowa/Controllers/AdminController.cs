using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AplikacjaBazodanowa.Data;
using AplikacjaBazodanowa.Models;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Strona główna panelu administracyjnego
    public IActionResult Index()
    {
        var tables = new[]
        {
            new { Name = "Rezerwacje", Url = "Rezerwacje" },
            new { Name = "Pracownicy", Url = "Pracownicy" },
            new { Name = "Pasazerowie", Url = "Pasazerowie" },
            new { Name = "Piloci", Url = "Piloci" },
            new { Name = "Samoloty", Url = "Samoloty" },
            new { Name = "LinieLotnicze", Url = "LinieLotnicze" },
            new { Name = "Zaloga", Url = "Zaloga" }
        };

        return View(tables);
    }

    // Wyświetlanie tabeli
    public async Task<IActionResult> ViewTable(string tableName)
    {
        if (string.IsNullOrEmpty(tableName))
        {
            return BadRequest("Nazwa tabeli jest wymagana.");
        }

        var tableData = await GetTableDataAsync(tableName);
        if (tableData == null)
        {
            return NotFound($"Tabela {tableName} nie istnieje.");
        }

        ViewBag.TableName = tableName;
        return View(tableData);
    }

    // Dodawanie rekordu
    [HttpGet]
    public IActionResult AddRecord(string tableName)
    {
        if (string.IsNullOrEmpty(tableName))
        {
            return BadRequest("Nazwa tabeli jest wymagana.");
        }

        ViewBag.TableName = tableName;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddRecord(string tableName, object record)
    {
        if (string.IsNullOrEmpty(tableName) || record == null)
        {
            return BadRequest("Nieprawidłowe dane wejściowe.");
        }

        if (await AddRecordToTableAsync(tableName, record))
        {
            TempData["Message"] = "Rekord został dodany.";
        }
        else
        {
            TempData["Error"] = "Nie udało się dodać rekordu.";
        }

        return RedirectToAction("ViewTable", new { tableName });
    }

    // Modyfikowanie rekordu
    [HttpGet]
    public async Task<IActionResult> EditRecord(string tableName, int id)
    {
        if (string.IsNullOrEmpty(tableName))
        {
            return BadRequest("Nazwa tabeli jest wymagana.");
        }

        var record = await GetRecordByIdAsync(tableName, id);
        if (record == null)
        {
            return NotFound($"Nie znaleziono rekordu w tabeli {tableName} o ID {id}.");
        }

        ViewBag.TableName = tableName;
        return View(record);
    }

    [HttpPost]
    public async Task<IActionResult> EditRecord(string tableName, object record)
    {
        if (string.IsNullOrEmpty(tableName) || record == null)
        {
            return BadRequest("Nieprawidłowe dane wejściowe.");
        }

        if (await UpdateRecordInTableAsync(tableName, record))
        {
            TempData["Message"] = "Rekord został zaktualizowany.";
        }
        else
        {
            TempData["Error"] = "Nie udało się zaktualizować rekordu.";
        }

        return RedirectToAction("ViewTable", new { tableName });
    }

    // Usuwanie rekordu
    [HttpPost]
    public async Task<IActionResult> DeleteRecord(string tableName, int id)
    {
        if (string.IsNullOrEmpty(tableName))
        {
            return BadRequest("Nazwa tabeli jest wymagana.");
        }

        if (await DeleteRecordFromTableAsync(tableName, id))
        {
            TempData["Message"] = "Rekord został usunięty.";
        }
        else
        {
            TempData["Error"] = "Nie udało się usunąć rekordu.";
        }

        return RedirectToAction("ViewTable", new { tableName });
    }

    // Funkcje pomocnicze
    private Task<object> GetTableDataAsync(string tableName) => throw new NotImplementedException();
    private Task<bool> AddRecordToTableAsync(string tableName, object record) => throw new NotImplementedException();
    private Task<object> GetRecordByIdAsync(string tableName, int id) => throw new NotImplementedException();
    private Task<bool> UpdateRecordInTableAsync(string tableName, object record) => throw new NotImplementedException();
    private Task<bool> DeleteRecordFromTableAsync(string tableName, int id) => throw new NotImplementedException();
}
