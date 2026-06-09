using CursoEFCoreDatabaseFirst.Datos;
using CursoEFCoreDatabaseFirst.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CursoEFCoreDatabaseFirst.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public HomeController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            // Traemos todas las categorias desde la base
            var categorias = _contexto.categorias.ToList();
            //Enviamos la lista a la vista
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
