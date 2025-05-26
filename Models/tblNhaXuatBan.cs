using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("tblNhaXuatBan")]
    public class tblNhaXuatBan
    {
        [Key]
        public int ID_NXB { get; set; }
        public string? TenNXB { get; set; }
        public string? DiaChi { get; set; }
        public string? Email { get; set; }
        public string? SDT { get; set; }  
    }
}