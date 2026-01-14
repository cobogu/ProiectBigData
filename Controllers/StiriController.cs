using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectBigData.Data;
using ProiectBigData.Models;

namespace ProiectBigData.Controllers
{
    public class StiriController : Controller
    {
        private readonly AppDbContext _context;

        public StiriController(AppDbContext context)
        {
            _context = context;
        }

      
        public async Task<IActionResult> Index(string cautare, int? spitalId)
        {
            var stiriQuery = _context.StiriMedicale
                                .Include(s => s.Doctor)
                                    .ThenInclude(d => d.Spital)
                                .AsQueryable();

            // 1. FILTRU TEXT (Titlu sau Medic) - existent
            if (!string.IsNullOrEmpty(cautare))
            {
                stiriQuery = stiriQuery.Where(s => s.Titlu.Contains(cautare) ||
                                                   s.Doctor.Nume.Contains(cautare));
            }

            // 2. FILTRU SPITAL (NOU)
            // Dacă utilizatorul a selectat un spital (spitalId nu e null)
            if (spitalId.HasValue)
            {
                // Căutăm doar știrile unde Doctorul aparține de spitalul selectat
                stiriQuery = stiriQuery.Where(s => s.Doctor.SpitalId == spitalId);
            }

            // 3. SORTARE
            stiriQuery = stiriQuery.OrderByDescending(s => s.DataPublicarii);

            // 4. PREGĂTIRE DATE PENTRU VIEW
            // Păstrăm textul căutat
            ViewData["CautareCurenta"] = cautare;

            // Pregătim lista de spitale pentru Dropdown (Select)
            // Parametrii: Sursa datelor, Valoarea trimisă (Id), Textul afișat (Nume), Elementul selectat curent
            ViewBag.SpitalId = new SelectList(_context.Spitale, "Id", "Nume", spitalId);

            return View(await stiriQuery.ToListAsync());
        }

        // 2. Create (GET): Formularul
        public IActionResult Create()
        {
            // Lista de doctori pentru Dropdown
            ViewBag.DoctorId = new SelectList(_context.Doctori, "Id", "Nume");
            return View();
        }

        // 3. Create (POST): Salvarea
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StireMedicala stire)
        {
            // Ignorăm validarea obiectului Doctor, ne interesează doar ID-ul
            ModelState.Remove("Doctor");

            if (ModelState.IsValid)
            {
                stire.DataPublicarii = DateTime.Now; // Asigurăm data curentă
                _context.Add(stire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.DoctorId = new SelectList(_context.Doctori, "Id", "Nume", stire.DoctorId);
            return View(stire);
        }
    }
}