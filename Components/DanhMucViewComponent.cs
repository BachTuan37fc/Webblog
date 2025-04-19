using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using aznews.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace aznews.Components
{
    [ViewComponent(Name = "DanhMucView")]
    public class DanhMucViewComponent : ViewComponent
    {
        private readonly DataContext _context;
        public DanhMucViewComponent(DataContext context) { _context = context; }
       public  IViewComponentResult Invoke()
{
    var data = _context.DanhMucs.Select(lo => new DanhMucMenuVM {
        MaMuc = lo.ID_DM, 
        TenMuc = lo.TenDM
       
    }).OrderBy(p => p.TenMuc);

   
    return View("Default",data);
}
    }
}