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

namespace aznews.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhieuMuonController : Controller
    {
        private readonly DataContext _context;

        public PhieuMuonController(DataContext context)
        {
            _context = context;
        }

        // public IActionResult Index()
        // {
        //     var mnList = _context.PhieuMuons.OrderBy(m => m.ID_PM).ToList();
        //     return View(mnList);
        // }
    //      var sachList = _context.Sachs
    //     .Select(s => new SachWithCountViewModel
    //     {
    //         Sachs = s,
    //         SoLuongChuaMuon = _context.MaCaBiets
    //             .Where(mcb => mcb.ID_Sach == s.ID_Sach && mcb.TrangThai == "chưa mượn")
    //             .Count()
    //     })
    //     .ToList();

    // return View(sachList);

        public IActionResult Index()
{
    var mnList = _context.PhieuMuons
        .Include(pm => pm.IdMCBNavigation)
            .ThenInclude(mcb => mcb.IdSachNavigation)
        .OrderBy(pm => pm.ID_PM)
        .ToList();

    return View(mnList);
}
        public IActionResult DeleteP(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var mn = _context.PhieuMuons.Find(id);
            if (mn == null)
                return NotFound();
            return View(mn);
        }
        [HttpPost]
        public IActionResult DeleteP(int id)
        {
            var delMenu = _context.PhieuMuons.Find(id);
            if(delMenu == null)
               return NotFound();
            _context.PhieuMuons.Remove(delMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}