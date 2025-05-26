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
    public class UserController : Controller
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var mnList = _context.Users.OrderBy(m => m.MaDG).ToList();
            return View(mnList);
        }

        public IActionResult Delete(string id)
        {
             if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = _context.Users.FirstOrDefault(u => u.MaDG == id);
            if (user == null)
                return NotFound();
                return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
           var user = _context.Users.FirstOrDefault(u => u.MaDG == id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();
        return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            var mnList = (from m in _context.Users
                         select new SelectListItem()
                         {
                            Text = m.MaDG,
                            Value = m.MaDG.ToString()
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
        public IActionResult Create(tblUser mn)
        {
            if(ModelState.IsValid)
            {
                _context.Users.Add(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(mn);
        }

        // public IActionResult Edit(int? id)
        // {
        //     if (id == null || id == 0)
        //         return NotFound();

        //     var mn = _context.Users.Find(id);
        //     if (mn == null)
        //         return NotFound();

        //     var mnList = (from m in _context.Users
        //                  select new SelectListItem()
        //                  {
        //                     Text = (m.Levels == 1) ? m.MenuName : "--" + m.MenuName,
        //                     Value = m.MenuID.ToString()
        //                  }).ToList();
        //     mnList.Insert(0, new SelectListItem()
        //     {
        //         Text = "--- select ---",
        //         Value = "0"
            
        //     });
        //     ViewBag.mnList = mnList;
        //     return View(mn);
        // }
        // [HttpPost]
        // public IActionResult Edit(tblMenu mn)
        // {
        //     if(ModelState.IsValid)
        //     {
        //         _context.Users.Update(mn);
        //         _context.SaveChanges();
        //         return RedirectToAction("Index");
        //     }
            
        //     return View(mn);
        // }
    }
}