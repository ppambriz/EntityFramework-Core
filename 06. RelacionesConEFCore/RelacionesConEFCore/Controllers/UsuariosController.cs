using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Datos;
using RelacionesConEFCore.Models;

namespace RelacionesConEFCore.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.Perfil) // Relación 1:1
                .Include(u => u.Comentarios) // Relación 1:N
                .ToListAsync();
            return View(usuarios);
        }

        // GET: Usuarios/Detalle/5
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _context.Usuarios
                .Include(c => c.Perfil)
                .Include(c => c.Comentarios)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (usuario == null) return NotFound();

            return View(usuario);
        }

        // GET: Usuario/Crear
        public IActionResult Crear()
        {
            return View();
        }


        // POST: Usuario/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("Id, Nombre, Email")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        // GET: Usuarios/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            return View(usuario);
        }

        // POST: Usuarios/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id, Nombre, Email")] Usuario usuario)
        {
            if (id != usuario.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!_context.Categorias.Any(e => e.Id == usuario.Id)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        // GET: Usuarios/Borrar/5
        public async Task<IActionResult> Borrar(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(e => e.Id == id);
            if (usuario == null) return NotFound();

            return View(usuario);
        }

        // POST: Usuarios/Borrar/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarConfirmado(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Perfil)
                .Include(u => u.Comentarios)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
