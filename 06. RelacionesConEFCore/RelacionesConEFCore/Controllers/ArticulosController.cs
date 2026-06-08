using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Datos;
using RelacionesConEFCore.Models;

namespace RelacionesConEFCore.Controllers
{
    public class ArticulosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticulosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Articulos
        public async Task<IActionResult> Index()
        {
            var articulos = await _context.Articulos
                .Include(a => a.Categoria) // Relación 1:N
                .Include(a => a.Etiquetas) // Relación N:N
                .Include(a => a.Comentarios) // Relación 1:N
                .ToListAsync();            
            
            return View(articulos);
        }

        // GET: Articulos/Crear
        public IActionResult Crear()
        {
            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Etiquetas = _context.Etiquetas.ToList();
            return View(new Articulo());
        }


        // POST: Articulos/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Articulo articulo, int[] etiquetasSeleccionadas)
        {
            if (ModelState.IsValid)
            {
                if (etiquetasSeleccionadas != null && etiquetasSeleccionadas.Any())
                {
                    articulo.Etiquetas = new List<Etiqueta>();
                    foreach (var id in etiquetasSeleccionadas)
                    {
                        var etiqueta = await _context.Etiquetas.FindAsync(id);
                        if (etiqueta != null)
                            articulo.Etiquetas.Add(etiqueta);
                    }
                }

                _context.Add(articulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //si la validación falla, recargamos combos y devolvemos la vista
            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Etiquetas = _context.Etiquetas.ToList();

            return View(articulo);
        }

        // GET: Articulos
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null) return NotFound();

            var articulos = await _context.Articulos
                .Include(a => a.Categoria) // Relación 1:N
                .Include(a => a.Etiquetas) // Relación N:N
                .Include(a => a.Comentarios) // Relación 1:N
                .FirstOrDefaultAsync(m => m.Id == id);

            if (articulos == null) return NotFound();

            return View(articulos);
        }

        // GET: Articulos
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var articulo = await _context.Articulos
                .Include(a => a.Etiquetas) // Relación 1:N                
                .FirstOrDefaultAsync(m => m.Id == id);

            if (articulo == null) return NotFound();

            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.Etiquetas = _context.Etiquetas.ToList();


            return View(articulo);
        }

        // POST: Articulos/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Articulo articulo, int[] etiquetasSeleccionadas)
        {
            if (id != articulo.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var articuloExistente =  await _context.Articulos
                        .Include(a => a.Etiquetas)
                        .FirstOrDefaultAsync (a => a.Id == id);

                    if (articuloExistente == null) return NotFound();

                    articuloExistente.Titulo = articulo.Titulo;
                    articuloExistente.Contenido = articulo.Contenido;
                    articuloExistente.CategoriaId = articulo.CategoriaId;

                    articuloExistente.Etiquetas.Clear();

                    if (etiquetasSeleccionadas != null)
                    {
                        foreach (var eid in etiquetasSeleccionadas)
                        {
                            var etiqueta = await _context.Etiquetas.FindAsync(eid);
                            if (etiqueta != null) 
                                articuloExistente.Etiquetas.Add(etiqueta);
                        }
                    }

                    _context.Update(articuloExistente);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if(!_context.Articulos.Any(a => a.Id == articulo.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(articulo);
        }

        // GET: Articulos/Borrar/5
        public async Task<IActionResult> Borrar(int? id)
        {
            if (id == null) return NotFound();

            var articulo = await _context.Articulos
                .Include(a => a.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (articulo == null) return NotFound();
            return View(articulo);
        }

        // POST: Articulos/Borrar/5
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarConfirmado(int id)
        {
            
            var articulo = await _context.Articulos
                .Include(a => a.Comentarios) // Relación 1:N                
                .FirstOrDefaultAsync(m => m.Id == id);

            if (articulo != null)
            {
                // Borrar comentarios asociados(si no está configurado en cascada)
                if (articulo.Comentarios != null)
                {
                    _context.Comentarios.RemoveRange(articulo.Comentarios);
                    _context.Articulos.Remove(articulo);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
