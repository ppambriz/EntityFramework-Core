using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Datos;
using RelacionesConEFCore.Models;

namespace RelacionesConEFCore.Controllers
{
    public class ComentariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComentariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comentarios
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var comentarios = await _context.Comentarios
                .Include(c => c.Usuario)
                .Include(c => c.Articulo)
                .ToListAsync();

            return View(comentarios);
        }

        // GET: Comentarios/Detalle/5
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null) return NotFound();

            var comentario = await _context.Comentarios
                .Include(c => c.Usuario)
                .Include(c => c.Articulo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (comentario == null) return NotFound();

            return View(comentario);
        }

        // GET: Comentarios/Crear
        public IActionResult Crear()
        {
            ViewBag.Usuarios = new SelectList(_context.Usuarios, "Id", "Nombre");
            ViewBag.Articulos = new SelectList(_context.Articulos, "Id", "Titulo");
            return View();
        }


        // POST: Comentarios/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("Id, Texto, UsuarioId, ArticuloId, Aprobado")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comentario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //Recargar combos si falla validación
            ViewBag.Usuarios = new SelectList(_context.Usuarios, "Id", "Nombre");
            ViewBag.Articulos = new SelectList(_context.Articulos, "Id", "Titulo");

            return View(comentario);
        }

        // GET: Comentarios/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario == null) return NotFound();

            //Recargar combos si falla validación
            ViewBag.Usuarios = new SelectList(_context.Usuarios, "Id", "Nombre");
            ViewBag.Articulos = new SelectList(_context.Articulos, "Id", "Titulo");

            return View(comentario);
        }

        // POST: Comentarios/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id, Texto, UsuarioId, ArticuloId, Aprobado")] Comentario comentario)
        {
            if (id != comentario.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comentario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!_context.Comentarios.Any(e => e.Id == comentario.Id)) return NotFound();
                    else throw;
                }
                
            }

            // si algo falla, volver a cargar los combos
            //Recargar combos si falla validación
            ViewBag.Usuarios = new SelectList(_context.Usuarios, "Id", "Nombre");
            ViewBag.Articulos = new SelectList(_context.Articulos, "Id", "Titulo");

            return View(comentario);
        }

        // GET: Comentarios/Borrar/5
        public async Task<IActionResult> Borrar(int? id)
        {
            if (id == null) return NotFound();

            var comentario = await _context.Comentarios
                .Include(c => c.Usuario)
                .Include(c => c.Articulo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (comentario == null) return NotFound();

            return View(comentario);
        }

        // POST: Comentarios/Borrar/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarConfirmado(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario != null)
            {
                _context.Comentarios.Remove(comentario);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
