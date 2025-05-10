using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("tblSach")]
    public class tblSach
    {
        [Key]
        public int ID_Sach { get; set; }

        [ForeignKey("IdNxbNavigation")]
        public int ID_NXB { get; set; }

        [ForeignKey("IdDmNavigation")]
        public int ID_DM { get; set; }

        public string? TacGia { get; set; }
        public string? TenSach { get; set; }
        public string? NamXB { get; set; }
        public string? MoTa { get; set; }
        public string? Hinh { get; set; }
        public string? SoTrang { get; set; }
        [NotMapped]
        public int SoLuong { get; set; }


        [ValidateNever]
        public virtual tblDanhMuc IdDmNavigation { get; set; }
        [ValidateNever]
        public virtual tblNhaXuatBan IdNxbNavigation { get; set; }
    }
}