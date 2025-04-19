using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using aznews.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aznews.Controllers
{
    
    public class SanphamController : Controller
    {
        private readonly DataContext _context;    
        public SanphamController(ILogger<SanphamController> logger, DataContext context)
    {
       
        _context = context;
    }

    public IActionResult Index(int? loai)
    {
        var sps = _context.Sachs.AsQueryable();
        if (loai.HasValue)
        {
            sps = sps.Where(p => p.ID_Sach == loai.Value);
        }
        var result = sps.Select(p => new SanPhamVM{
            ID = p.ID_Sach,
            TenSP = p.TenSach,
            Hinh = p.Hinh ?? "",
            

        });
        return View(result);
    }  
    public IActionResult Search(string? query) 
    {
        Console.WriteLine("o day");
        var sps = _context.Sachs.Include(p => p.).AsQueryable();
        if (query != null)
        {
            sps = sps.Where(p => p.TenSach.Contains(query));
        }
        var result = sps.Select(p => new SanPhamVM{
            ID = p.ID_Sach,
            TenSP = p.TenSach,
            Hinh = p.Hinh ?? ""
            
        });
        return View(result);
    }  
    public IActionResult Detail(int id) 
    {
        Console.WriteLine("o day");
        var data = _context.Sachs.Include(p => p.ID_DM).SingleOrDefault(p => p.ID_Sach == id);
        if (data == null)
        {
           TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
           return Redirect("/404");
        }

        return View(data);
    }  
    }
}