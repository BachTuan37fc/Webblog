using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using aznews.Models;
using Microsoft.AspNetCore.Authorization;
using aznews.Utilities;

namespace aznews.Controllers;

[Authorize(Roles = "docgia")]

public class HomeController : Controller
{
    private ILogger<HomeController> _logger;
    private readonly DataContext _context;

    public HomeController(ILogger<HomeController> logger, DataContext context)
    {
        _logger = logger;
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

