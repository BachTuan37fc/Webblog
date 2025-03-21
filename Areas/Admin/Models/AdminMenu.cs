using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace blogs.Areas.Admin.Models
{
    [Table("AdminMenu")]
    public class AdminMenu
    {
        [Key]
        public int AdminMenuID { get; set; }
        public string? ItemName { get; set; }
        public bool? IsActive { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public int? ItemLevel { get; set; }
        public int? ParentLevel { get; set; }
        public int? ItemOrder { get; set; }
        public int? Icon { get; set; }
        public string? IDName { get; set; }
        public string? AreaName { get; set; }
        public string? ItemTarget { get; set; }
        
    }
}