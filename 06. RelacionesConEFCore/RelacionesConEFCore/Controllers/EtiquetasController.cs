using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Datos;
using RelacionesConEFCore.Models;

namespace RelacionesConEFCore.Controllers
{
    public class EtiquetasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EtiquetasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Etiquetas
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var etiquetas = await _context.Etiquetas
                .Include(e => e.Articulos) // Para ver qué artículos usan la etiqueta
                .ToListAsync();
            return View(etiquetas);
        }



        // GET: Etiquetas/Detalle/5
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null) return NotFound();

            var etiqueta = await _context.Etiquetas
                 .Include(e => e.Articulos) // Para ver qué artículos usan la etiqueta
                 .FirstOrDefaultAsync(m => m.Id == id);

            if (etiqueta == null) return NotFound();

            return View(etiqueta);
        }

        // GET: Etiquetas/Crear
        public IActionResult Crear()
        {
            return View();
        }


        // POST: Etiquetas/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("Id, Nombre")] Etiqueta etiqueta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(etiqueta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(etiqueta);
        }

        // GET: Etiquetas/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var etiqueta = await _context.Etiquetas.FindAsync(id);
            if (etiqueta == null) return NotFound();

            return View(etiqueta);
        }

        // POST: Etiquetas/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id, Nombre")] Etiqueta etiqueta)
        {
            if (id != etiqueta.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(etiqueta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!_context.Etiquetas.Any(e => e.Id == etiqueta.Id)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(etiqueta);
        }

        // GET: Etiqueta/Borrar/5
        public async Task<IActionResult> Borrar(int? id)
        {
            if (id == null) return NotFound();

            var etiqueta = await _context.Etiquetas.FirstOrDefaultAsync(e => e.Id == id);
            if (etiqueta == null) return NotFound();

            return View(etiqueta);
        }

        // POST: Etiquetas/Borrar/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarConfirmado(int id)
        {
            var etiqueta = await _context.Etiquetas.FindAsync(id);
            if (etiqueta != null)
            {
                _context.Etiquetas.Remove(etiqueta);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
