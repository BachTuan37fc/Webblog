using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using aznews.Areas.Admin.Models;


namespace aznews.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<tblMenu> Menus { get; set;} 
        public DbSet<AdminMenu> AdminMenus { get; set;}
        public DbSet<tblMaCaBiet> MaCaBiets { get; set;}
        public DbSet<tblNhaXuatBan> NhaXuatBans { get; set;}
        public DbSet<tblSach> Sachs { get; set;}
        public DbSet<tblDanhMuc> DanhMucs { get; set;}
        
    }
}