using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("tblDanhMuc")]
    public class tblDanhMuc
    {
        [Key]
        public int ID_DM { get; set; }
        public string? TenDM { get; set; }
    }
}