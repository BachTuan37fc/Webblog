using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("tblMaCaBiet")]
    public class tblMaCaBiet
    {
        [Key]
        public int ID_MCB { get; set; }
        public string? MCB { get; set; }
        public int ID_Sach { get; set; }
        public string? TrangThai { get; set; }
        
    }
}