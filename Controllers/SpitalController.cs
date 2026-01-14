using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectBigData.Data;
using ProiectBigData.Models;

namespace ProiectBigData.Controllers
{
    public class SpitalController : Controller
    {
        private readonly AppDbContext _context;
        public SpitalController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Mergem în baza de date, luăm lista de spitale și o trimitem la View
            var listaSpitale = await _context.Spitale.ToListAsync();
            return View(listaSpitale);
        }

        // 1. GET: Deschide pagina de modificare a spitalului
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spital = await _context.Spitale.FindAsync(id);
            if (spital == null)
            {
                return NotFound();
            }
            return View(spital);
        }

        // 2. POST: Salvează modificările (inclusiv nr de paturi)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Spital spital)
        {
            if (id != spital.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(spital);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spital);
        }
    }
}
