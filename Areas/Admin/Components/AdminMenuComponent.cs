using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogs.Models;
using Microsoft.AspNetCore.Mvc;

namespace blogs.Areas.Admin.Components
{
    [ViewComponent(Name = "AdminMenu")]
    public class AdminMenuComponent : ViewComponent
    {
        private readonly DataContext _context;
        public AdminMenuComponent(DataContext context) {
            _context = context; 
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            var mnlist = (from mn in _context.AdminMenus
                             where (mn.IsActive == true) 
                             select mn).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default",mnlist));
        }
    }
}