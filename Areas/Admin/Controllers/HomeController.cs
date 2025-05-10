using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using aznews.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static aznews.ViewModels.SachVM;

namespace aznews.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        public HomeController(DataContext context)
        {
            _context = context;
        }
       public IActionResult Index() 
        {
   
        var totalBooksBorrowed = _context.PhieuMuons.Count(pm => pm.TrangThai == "Approved" && pm.NgayTra >= DateTime.Now);
        var totalBooksPendingApproval = _context.PhieuMuons.Count(pm => pm.TrangThai == "Pending");
        var totalBooksOverdue = _context.PhieuMuons.Count(pm => pm.NgayTra < DateTime.Now && pm.TrangThai == "Approved");

        var model = new DashboardViewModel
        {
       
        TotalBooksBorrowed = totalBooksBorrowed,
        TotalBooksPendingApproval = totalBooksPendingApproval,
        TotalBooksOverdue = totalBooksOverdue
        };

    return View(model);
        }

    }
}

