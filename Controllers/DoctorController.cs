using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectBigData.Data;
using ProiectBigData.Models;

namespace ProiectBigData.Controllers
{
    public class DoctorController : Controller
    {
        private readonly AppDbContext _context;

        public DoctorController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // AICI ESTE SECRETUL: Eager Loading
            // Îi spunem: "Adu-mi doctorii, DAR include și datele despre Specializare și Spital"
            var doctori = _context.Doctori
                                  .Include(d => d.Specializare)
                                  .Include(d => d.Spital);

            return View(await doctori.ToListAsync());
        }

        public IActionResult Create()
        {
            // Pregătim listele pentru Dropdown-uri
            ViewBag.SpecializareId = new SelectList(_context.Specializari, "Id", "Nume");
            ViewBag.SpitalId = new SelectList(_context.Spitale, "Id", "Nume");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doctor doctor)
        {
        
             ModelState.Remove("Spital");
             ModelState.Remove("Specializare");

            if (ModelState.IsValid)
            {
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Dacă validarea eșuează, reîncărcăm listele ca să nu apară formularul gol
            ViewBag.SpecializareId = new SelectList(_context.Specializari, "Id", "Nume", doctor.SpecializareId);
            ViewBag.SpitalId = new SelectList(_context.Spitale, "Id", "Nume", doctor.SpitalId);

            return View(doctor);
        }

        // 1. GET: Editare - Căutăm doctorul și populăm formularul
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Căutăm doctorul în baza de date
            var doctor = await _context.Doctori.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            // Pregătim listele pentru Dropdown-uri, selectând valoarea curentă
            // Observă al 4-lea parametru: doctor.SpecializareId (valoarea deja selectată)
            ViewBag.SpecializareId = new SelectList(_context.Specializari, "Id", "Nume", doctor.SpecializareId);
            ViewBag.SpitalId = new SelectList(_context.Spitale, "Id", "Nume", doctor.SpitalId);

            return View(doctor);
        }

        // 2. POST: Editare - Salvarea modificărilor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Doctor doctor)
        {
            // Verificare de securitate: ID-ul din URL să coincidă cu cel din formular
            if (id != doctor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor); // Comanda de UPDATE
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Doctori.Any(e => e.Id == doctor.Id))
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

            // Dacă e eroare, reîncărcăm listele
            ViewBag.SpecializareId = new SelectList(_context.Specializari, "Id", "Nume", doctor.SpecializareId);
            ViewBag.SpitalId = new SelectList(_context.Spitale, "Id", "Nume", doctor.SpitalId);

            return View(doctor);


        }
        // POST: Ștergere directă din pagina de Editare
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _context.Doctori.FindAsync(id);

            if (doctor != null)
            {
                // Ștergem doctorul
                _context.Doctori.Remove(doctor);
                await _context.SaveChangesAsync();
            }

            // Ne întoarcem la listă după ștergere
            return RedirectToAction(nameof(Index));
        }

    }
}