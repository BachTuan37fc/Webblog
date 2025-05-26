using System.Reflection.Emit;
using System.Data;
using Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using aznews.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;



namespace aznews.Controllers
{
    [AllowAnonymous]
public class LoginController : Controller
{
    private readonly DataContext _context;

    public LoginController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string MaDG, string Password)
    {
        var user = _context.Users.SingleOrDefault(u => u.MaDG == MaDG && u.Password == Password);
        if (user == null)
        {
            ViewBag.Error = "Sai tài khoản hoặc mật khẩu.";
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.MaDG),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var identity = new ClaimsIdentity(claims, "MyCookie");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("MyCookie", principal);

        // Điều hướng theo role
        if (user.Role == "admin")
            return Redirect("Admin");

        return Redirect("/");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("MyCookie");
        return RedirectToAction("Login");
    }
}
}