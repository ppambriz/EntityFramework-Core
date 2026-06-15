using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CEFCoreDBF.Models;
using CEFCoreDBF.Data;

namespace CEFCoreDBF.Controllers;

public class HomeController : Controller
{

    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        var categorias = _context.categorias.ToList();
        return View(categorias);
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
