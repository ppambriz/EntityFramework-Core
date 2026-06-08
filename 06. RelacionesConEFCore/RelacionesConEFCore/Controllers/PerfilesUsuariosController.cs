using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Datos;
using RelacionesConEFCore.Models;

namespace RelacionesConEFCore.Controllers
{
    public class PerfilesUsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerfilesUsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PerfilesUsuarios
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var perfiles = await _context.PerfilUsuarios
            .Include(p => p.Usuario)  // Relación 1:1 con Usuario
            .ToListAsync();
            return View(perfiles);
        }

        // GET: PerfilesUsuarios/Detalle/5
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null) return NotFound();

            var perfil = await _context.PerfilUsuarios
                .Include(c => c.Usuario) // muestra artículos relacionados
                .FirstOrDefaultAsync(m => m.UsuarioId == id);

            if (perfil == null) return NotFound();

            return View(perfil);
        }

        // GET: PerfilesUsuarios/Crear
        public IActionResult Crear()
        {
            // lista de usuarios que no tienen perfil todavía
            ViewBag.Usuarios = _context.Usuarios
                .Where(u => u.Perfil == null)
                .ToList();

            return View();
        }


        // POST: PerfilesUsuarios/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("UsuarioId, Biografia, FotoUrl")] PerfilUsuario perfilUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(perfilUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Usuarios = _context.Usuarios
                 .Where(u => u.Perfil == null)
                 .ToList();

            return View(perfilUsuario);
        }

        // GET: PerfilesUsuarios/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var perfil = await _context.PerfilUsuarios.FindAsync(id);    
            
            ViewBag.Usuarios = _context.Usuarios               
                 .ToList();

            return View(perfil);
        }

        // POST: PerfilesUsuarios/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("UsuarioId, Biografia, FotoUrl")] PerfilUsuario perfilUsuario)
        {
            if (id != perfilUsuario.UsuarioId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perfilUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!_context.Categorias.Any(e => e.Id == perfilUsuario.UsuarioId)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(perfilUsuario);
        }

        // GET: /Borrar/5
        public async Task<IActionResult> Borrar(int? id)
        {
            if (id == null) return NotFound();

            var perfil = await _context.PerfilUsuarios
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(e => e.UsuarioId == id);

            if (perfil == null) return NotFound();

            return View(perfil);
        }

        // POST: PerfilesUsuarios/Borrar/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarConfirmado(int id)
        {
            var perfil = await _context.PerfilUsuarios.FindAsync(id);
            if (perfil != null)
            {
                _context.PerfilUsuarios.Remove(perfil);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
