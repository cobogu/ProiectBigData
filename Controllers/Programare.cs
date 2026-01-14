using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Necesar pentru Dropdown (Select)
using Microsoft.EntityFrameworkCore;
using ProiectBigData.Data;
using ProiectBigData.Models;

namespace ProiectBigData.Controllers
{
    public class ProgramareController : Controller
    {
        private readonly AppDbContext _context;

        public ProgramareController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Pagina cu lista de programări
        public async Task<IActionResult> Index()
        {
            // AICI ESTE CHEIA PENTRU SPITAL + ADRESA
            // "Include Doctor" aduce doctorul.
            // "ThenInclude Spital" merge mai departe și aduce spitalul doctorului.
            var programari = _context.Programari
                                     .Include(p => p.Doctor)
                                        .ThenInclude(d => d.Spital);

            return View(await programari.ToListAsync());
        }

        // 2. Pagina de creare (Formularul gol - GET)
        [HttpGet]
        public IActionResult Create()
        {
            // 1. Luăm doctorii și includem datele despre Spital
            // 2. Creăm un "Text Personalizat" pentru dropdown care conține și locurile libere
            var listaDoctori = _context.Doctori
                .Include(d => d.Spital)
                .Select(d => new
                {
                    Id = d.Id,
                    // Textul va arăta: "Dr. Popescu (Spitalul X - 5 locuri libere)"
                    InfoComplet = "Dr. " + d.Nume + " (" + d.Spital.Nume + " - " +
                                  (d.Spital.NrPaturiTotal - d.Spital.NrPaturiOcupate) + " locuri libere)"
                })
                .ToList();
            // Trimitem lista de doctori către View ca să populăm Dropdown-ul
            // Parametrii: Sursa datelor, Ce salvăm (Id), Ce afișăm (Nume)
            ViewBag.DoctorId = new SelectList(_context.Doctori, "Id", "Nume");
            return View();
        }

        // 3. Salvarea datelor (Când apeși butonul - POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Programare programare)
        {
            ModelState.Remove("Doctor");

            if (ModelState.IsValid)
            {
                _context.Add(programare);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Dacă e eroare, reîncărcăm lista de doctori
            ViewBag.DoctorId = new SelectList(_context.Doctori, "Id", "Nume", programare.DoctorId);
            return View(programare);
        }

        // POST: Ștergere directă
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            // 1. Căutăm elementul
            var programare = await _context.Programari.FindAsync(id);

            // 2. Dacă există, îl ștergem
            if (programare != null)
            {
                _context.Programari.Remove(programare);
                await _context.SaveChangesAsync();
            }

            // 3. Ne întoarcem imediat la listă (Refresh automat)
            return RedirectToAction(nameof(Index));
        }
    }
}