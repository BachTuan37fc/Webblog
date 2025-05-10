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

    var mn = _context.PhieuMuons
        .Include(pm => pm.IdMCBNavigation)
        .ThenInclude(mcb => mcb.IdSachNavigation)
        .SingleOrDefault(pm => pm.ID_PM == id);

    if (mn == null)
        return NotFound();

    // Kiểm tra xem IdMCBNavigation hoặc IdSachNavigation có null không
    if (mn.IdMCBNavigation == null || mn.IdMCBNavigation.IdSachNavigation == null)
    {
        Console.WriteLine($"Phiếu mượn ID {id} có dữ liệu không hợp lệ: IdMCBNavigation hoặc IdSachNavigation là null.");
        return NotFound();
    }
    ViewData["Menus"] = _context.Menus
        .Where(m => m.IsActive == true)
        .ToList();

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
    public IActionResult Create()
{

    var sachList = _context.Sachs
        .Where(s => _context.MaCaBiets.Any(mcb => mcb.ID_Sach == s.ID_Sach && mcb.TrangThai == "Chưa mượn"))
        .Select(s => new SelectListItem
        {
            Text = s.TenSach,
            Value = s.ID_Sach.ToString()
        }).ToList();

    ViewBag.mnList = sachList;
    return View();
}


    [HttpPost]
    public IActionResult Create(int ID_Sach)
{
    // Tìm mã cá biệt chưa được mượn
    var phieumuon = _context.MaCaBiets
    .Include(m => m.IdSachNavigation)
    .FirstOrDefault(m => m.ID_Sach == ID_Sach && m.TrangThai == "Chưa mượn");

    if (phieumuon == null)
    {
        TempData["Error"] = "Không còn bản sao sách này có sẵn để mượn";
        return RedirectToAction("Index", new { id = ID_Sach });
    }
    var pm = new tblPhieuMuon
    {
        ID_MCB = phieumuon.ID_MCB,
        NgayMuon = DateTime.Now,
        NgayTra = DateTime.Now.AddDays(7), // hoặc quy định khác
        TrangThai = "Approved",
        MaDG = "1"
    };

    // Cập nhật trạng thái mã cá biệt
    phieumuon.TrangThai = "Đã mượn";

    // Thêm và lưu
    _context.PhieuMuons.Add(pm);
    _context.MaCaBiets.Update(phieumuon);
    _context.SaveChanges();
    return RedirectToAction("Index", new { id = phieumuon.ID_Sach });
    // tại sao nó lại return về trang 404 thay vì là trang detail của cuốn sách, phải chăng trong csdl chỉ ghi 1 cuốn nên khi đã mượn nó chuyển sang trang ko tìm kiếm đc nữa, và nó đã đc update vào trong cơ sở dữ liệu chưa, đã test và chưa????

}

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(int id)
{
    var phieu = await _context.PhieuMuons.FindAsync(id);
    if (phieu == null)
    {
        return NotFound();
    }

    phieu.TrangThai = "Approved";
    _context.Update(phieu);
    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}

    }
}