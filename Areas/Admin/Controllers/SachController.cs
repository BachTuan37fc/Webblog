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
    public class SachController : Controller
    {
        private readonly DataContext _context;

        public SachController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var mnList = _context.Sachs.OrderBy(m => m.ID_Sach).ToList();
            return View(mnList);
        }
        public IActionResult Create()
        {
            var mnList = (from m in _context.Sachs
                          join n in _context.NhaXuatBans on m.ID_NXB equals n.ID_NXB into nxbs
                           from n in nxbs.DefaultIfEmpty()
                         select new SelectListItem()
                         {
                            Text = (m.ID_NXB == 1) ? n.TenNXB : "--" + n.TenNXB,
                            Value = m.ID_Sach.ToString()
                         }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "--- select ---",
                Value = "0"
            
            });
            ViewBag.mnList = mnList;
            var sList = (from m in _context.Sachs
                          join n in _context.DanhMucs on m.ID_DM equals n.ID_DM into dms
                           from n in dms.DefaultIfEmpty()
                         select new SelectListItem()
                         {
                            Text = (m.ID_DM == 1) ? n.TenDM : "--" + n.TenDM,
                            Value = m.ID_Sach.ToString()
                         }).ToList();
            sList.Insert(0, new SelectListItem()
            {
                Text = "--- select ---",
                Value = "0"
            
            });
            ViewBag.sList = sList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(tblSach ss, tblNhaXuatBan m)
        {
            if(ModelState.IsValid)
            {
                _context.Sachs.Add(ss);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(ss);
        }


       
    }
}