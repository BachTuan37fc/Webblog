using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;
using Microsoft.AspNetCore.Mvc;

namespace aznews.Components
{
    [ViewComponent(Name = "MenuView")]
    public class MenuViewComponent : ViewComponent
    {
        private readonly DataContext _context;
        public MenuViewComponent(DataContext context) { _context = context; }
       public async Task<IViewComponentResult> InvokeAsync()
{
    var listOfMenu = (from m in _context.Menus
                      where (m.IsActive == true) && (m.Position == 1)
                      select m).ToList();

    // Log danh sách menu ra console
    foreach (var menu in listOfMenu)
    {
        Console.WriteLine($"Menu ID: {menu.MenuID}, Name: {menu.MenuName}, Position: {menu.Position}");
    }

    return await Task.FromResult((IViewComponentResult)View(listOfMenu));
}
    }
}