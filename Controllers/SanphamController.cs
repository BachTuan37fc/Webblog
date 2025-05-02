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
            Hinh = p.Hinh ?? ""
    
        });
        return View(result);
    }  
    public IActionResult Search(string? query) 
    {
        Console.WriteLine("o day");
        var sps = _context.Sachs.AsQueryable();
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
        var data = _context.Sachs.Include(p => p.IdDmNavigation).SingleOrDefault(p => p.ID_Sach == id);
        if (data == null)
        {
           TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
           return Redirect("/404");
        }
        var result = new CTSanPhamVM {
            ID = data.ID_Sach,
            TenSP = data.TenSach,
            Hinh = data.Hinh,
            MoTa = data.MoTa ?? string.Empty,
            DanhMuc = data.IdDmNavigation.TenDM
        
        };
        return View("Detail", result);
    }
    [HttpPost]
public IActionResult Muon(int id)
{
    // Tìm mã cá biệt chưa được mượn
    var phieumuon = _context.MaCaBiets.FirstOrDefault(m => m.ID_MCB == id && m.TrangThai == "Chưa mượn");

    if (phieumuon == null)
    {
        TempData["Error"] = "Không còn bản sao sách này có sẵn để mượn";
        return RedirectToAction("Detail", new { id = id });
    }
    var pm = new tblPhieuMuon
    {
        ID_MCB = phieumuon.ID_MCB,
        NgayMuon = DateTime.Now,
        NgayTra = DateTime.Now.AddDays(7), // hoặc quy định khác
        TrangThai = "Pending"
    };

    // Cập nhật trạng thái mã cá biệt
    phieumuon.TrangThai = "Đã mượn";

    // Thêm và lưu
    _context.PhieuMuons.Add(pm);
    _context.MaCaBiets.Update(phieumuon);
    _context.SaveChanges();
    return RedirectToAction("Detail", new { id = phieumuon.ID_Sach });
    // tại sao nó lại return về trang 404 thay vì là trang detail của cuốn sách, phải chăng trong csdl chỉ ghi 1 cuốn nên khi đã mượn nó chuyển sang trang ko tìm kiếm đc nữa, và nó đã đc update vào trong cơ sở dữ liệu chưa, đã test và chưa????

}
  
    }
}