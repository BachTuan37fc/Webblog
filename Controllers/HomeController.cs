using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using aznews.Models;

namespace aznews.Controllers;

public class HomeController : Controller
{
   
    private readonly DataContext _context;

    public HomeController(ILogger<HomeController> logger, DataContext context)
    {
       
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [Route("/404")]
    public IActionResult PageNotFound()
    {
        return View();
    }


   
}
