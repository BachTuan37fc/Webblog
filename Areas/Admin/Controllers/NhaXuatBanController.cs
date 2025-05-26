using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using aznews.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace aznews.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhaXuatBanController : Controller
    {
        private readonly DataContext _context;

        public NhaXuatBanController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var mnList = _context.NhaXuatBans.OrderBy(m => m.ID_NXB).ToList();
            return View(mnList);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var mn = _context.NhaXuatBans.Find(id);
            if (mn == null)
                return NotFound();
            return View(mn);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var delMenu = _context.NhaXuatBans.Find(id);
            if(delMenu == null)
               return NotFound();
            _context.NhaXuatBans.Remove(delMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            var mnList = (from m in _context.NhaXuatBans
                         select new SelectListItem()
                         {
                            Text = (m.ID_NXB == 1) ? m.TenNXB : "--" + m.TenNXB,
                            Value = m.ID_NXB.ToString()
                         }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "--- select ---",
                Value = "0"
            
            });
            ViewBag.mnList = mnList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(tblNhaXuatBan mn)
        {
            if(ModelState.IsValid)
            {
                _context.NhaXuatBans.Add(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(mn);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var mn = _context.NhaXuatBans.Find(id);
            if (mn == null)
                return NotFound();

            var mnList = (from m in _context.NhaXuatBans
                         select new SelectListItem()
                         {
                            Text = m.TenNXB,
                            Value = m.ID_NXB.ToString()
                         }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "--- select ---",
                Value = "0"
            
            });
            ViewBag.mnList = mnList;
            return View(mn);
        }
        [HttpPost]
        public IActionResult Edit(tblNhaXuatBan mn)
        {
            if(ModelState.IsValid)
            {
                _context.NhaXuatBans.Update(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(mn);
        }
    }
}