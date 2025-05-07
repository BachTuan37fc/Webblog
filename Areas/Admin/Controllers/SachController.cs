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
using static aznews.ViewModels.SachVM;

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

        // public IActionResult Index()
        // {
        //     var mnList = _context.Sachs.OrderBy(m => m.ID_Sach).ToList();
        //     return View(mnList);
        // }
        public IActionResult Index()
{
    var sachList = _context.Sachs
        .Select(s => new SachWithCountViewModel
        {
            Sachs = s,
            SoLuongChuaMuon = _context.MaCaBiets
                .Where(mcb => mcb.ID_Sach == s.ID_Sach && mcb.TrangThai == "chưa mượn")
                .Count()
        })
        .ToList();

    return View(sachList);
}

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var mn = _context.Sachs.Find(id);
            if (mn == null)
                return NotFound();
            return View(mn);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var delMenu = _context.Sachs.Find(id);
            if(delMenu == null)
               return NotFound();
            _context.Sachs.Remove(delMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            var mnList = (from m in _context.Sachs
                          join n in _context.NhaXuatBans on m.ID_NXB equals n.ID_NXB into nxbs
                           from n in nxbs.DefaultIfEmpty()
                         select new SelectListItem()
                         {
                            Text = (m.ID_NXB == 1) ? n.TenNXB : n.TenNXB,
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
                            Text = (m.ID_DM == 1) ? n.TenDM : n.TenDM,
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
//         public async Task<IActionResult> Create(tblSach ss, IFormFile Hinh)
//         {
//             if(ModelState.IsValid)
//             {
//                 if (Hinh != null && Hinh.Length > 0)
//                 {
//                     var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image"); 
//                     Directory.CreateDirectory(uploadsFolder);

//                     var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Hinh.FileName);
//                     var filePath = Path.Combine(uploadsFolder, fileName);

//                     using (var stream = new FileStream(filePath, FileMode.Create))
//                     {
//                         await Hinh.CopyToAsync(stream);
//                     }

//                     ss.Hinh = fileName;
//                 }
//                 _context.Sachs.Add(ss);
//                 _context.SaveChanges();
//                 return RedirectToAction("Index");
//             }

//                 Console.WriteLine("123");
//                 foreach (var entry in ModelState)
// {
//     foreach (var error in entry.Value.Errors)
//     {
//         Console.WriteLine($"Field: {entry.Key}, Error: {error.ErrorMessage}");
//     }
// }
            
//             return RedirectToAction("Index");
//             return View(ss);
//         }

[HttpPost]
public async Task<IActionResult> Create(tblSach ss, IFormFile Hinh)
{
    if (ModelState.IsValid)
    {
        if (Hinh != null && Hinh.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image");
            Directory.CreateDirectory(uploadsFolder);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Hinh.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Hinh.CopyToAsync(stream);
            }
            ss.Hinh = fileName;
        }

        _context.Sachs.Add(ss);
        _context.SaveChanges();

        // Lấy ID sách vừa thêm
        int idSachMoi = ss.ID_Sach;

        // Thêm n bản ghi vào bảng tblMaCaBiet
        for (int i = 0; i < ss.SoLuong; i++)
        {
            var mcb = new tblMaCaBiet
            {
                ID_Sach = idSachMoi,
                MCB = $"MCB_{idSachMoi}_{i + 1}",
                TrangThai = "Chưa mượn"
            };
            _context.MaCaBiets.Add(mcb);
        }

        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    return View(ss);
}


public IActionResult Edit(int? id)
{
    if (id == null || id == 0)
        return NotFound();

    var sach = _context.Sachs.Find(id);
    if (sach == null)
        return NotFound();

    // NXB Dropdown
    var mnList = (from m in _context.NhaXuatBans
                  select new SelectListItem()
                  {
                      Text = m.TenNXB,
                      Value = m.ID_NXB.ToString()
                  }).ToList();
    ViewBag.mnList = mnList;

    // Danh Mục Dropdown
    var sList = (from m in _context.DanhMucs
                 select new SelectListItem()
                 {
                     Text = m.TenDM,
                     Value = m.ID_DM.ToString()
                 }).ToList();
    ViewBag.sList = sList;

    return View(sach);
}

// [HttpPost]
// public async Task<IActionResult> Edit(int id, tblSach ss, IFormFile? Hinh)
// {
//             if(ModelState.IsValid)
//             {
//                 if (Hinh != null && Hinh.Length > 0)
//                 {
//                     var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image"); 
//                     Directory.CreateDirectory(uploadsFolder);

//                     var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Hinh.FileName);
//                     var filePath = Path.Combine(uploadsFolder, fileName);

//                     using (var stream = new FileStream(filePath, FileMode.Create))
//                     {
//                         await Hinh.CopyToAsync(stream);
//                     }

//                     ss.Hinh = fileName;
//                 }
//                 _context.Sachs.Update(ss);
//                 _context.SaveChanges();
//                 return RedirectToAction("Index");
//             }

//                 Console.WriteLine("123");
//                 foreach (var entry in ModelState)
// {
//     foreach (var error in entry.Value.Errors)
//     {
//         Console.WriteLine($"Field: {entry.Key}, Error: {error.ErrorMessage}");
//     }
// }
            
//             return RedirectToAction("Index");
//             return View(ss);

//     }
[HttpPost]
public async Task<IActionResult> Edit(int id, tblSach ss, IFormFile? Hinh)
{
    if (!ModelState.IsValid)
    {
        // Log errors for debugging
        foreach (var entry in ModelState)
        {
            foreach (var error in entry.Value.Errors)
            {
                Console.WriteLine($"Field: {entry.Key}, Error: {error.ErrorMessage}");
            }
        }

        // Reload dropdown lists for the view
        ViewBag.mnList = (from m in _context.NhaXuatBans
                          select new SelectListItem()
                          {
                              Text = m.TenNXB,
                              Value = m.ID_NXB.ToString()
                          }).ToList();

        ViewBag.sList = (from m in _context.DanhMucs
                         select new SelectListItem()
                         {
                             Text = m.TenDM,
                             Value = m.ID_DM.ToString()
                         }).ToList();

        return View(ss);
    }

    // Find the existing book
    var existingSach = await _context.Sachs.FindAsync(id);
    if (existingSach == null)
    {
        return NotFound();
    }

    // Update fields
    existingSach.TenSach = ss.TenSach;
    existingSach.ID_NXB = ss.ID_NXB;
    existingSach.ID_DM = ss.ID_DM;
    // Update other fields as needed

    // Handle image upload
    if (Hinh != null && Hinh.Length > 0)
    {
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image");
        Directory.CreateDirectory(uploadsFolder);

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Hinh.FileName);
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await Hinh.CopyToAsync(stream);
        }

        // Update image path
        existingSach.Hinh = fileName;
    }

    // Update the existing record
    _context.Sachs.Update(existingSach);
    await _context.SaveChangesAsync();

    return RedirectToAction("Index");
}
}
}