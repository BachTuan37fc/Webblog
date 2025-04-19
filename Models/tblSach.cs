using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("tblSach")]
    public class tblSach
    {
        [Key]
    public int ID_Sach { get; set; }
    public int ID_NXB { get; set; }
    public int ID_DM { get; set; }
    public int ID_MCB { get; set; }
    public string? TacGia { get; set; }
    public string? TenSach { get; set; }
    public int? NamXB { get; set; }
    public string? MoTa { get; set; }
    public string? Hinh { get; set; }
    
    }
}