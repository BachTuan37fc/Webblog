using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogs.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace blogs.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<tblMenu> Menus { get; set; }
        public DbSet<AdminMenu> AdminMenus { get; set; }
    }
}