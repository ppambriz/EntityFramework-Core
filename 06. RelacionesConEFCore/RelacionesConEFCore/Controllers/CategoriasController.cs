using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Datos;
using RelacionesConEFCore.Models;

namespace RelacionesConEFCore.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categorias
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return View(categorias);
        }

        // GET: Categorias/Detalle/5
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null) return NotFound();

            var categoria = await _context.Categorias
                .Include(c => c.Articulos) // muestra artículos relacionados
                .FirstOrDefaultAsync(m => m.Id == id);

            if (categoria == null) return NotFound();

            return View(categoria);
        }

        // GET: Categorias/Crear
        public IActionResult Crear()
        {
            return View();
        }


        // POST: Categorias/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("Id, Nombre")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        // GET: Categorias/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return NotFound();

            return View(categoria);
        }

        // POST: Categorias/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id, Nombre")] Categoria categoria)
        {
            if (id != categoria.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!_context.Categorias.Any(e => e.Id == categoria.Id)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        // GET: Categorias/Borrar/5
        public async Task<IActionResult> Borrar(int? id)
        {
            if (id == null) return NotFound();

            var categoria = await _context.Categorias.FirstOrDefaultAsync(e => e.Id == id);
            if (categoria == null) return NotFound();

            return View(categoria);
        }

        // POST: Categorias/Borrar/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarConfirmado(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
