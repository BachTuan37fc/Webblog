using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("tblPhieuMuon")]
    public class tblPhieuMuon
    {
        [Key]
        public int ID_PM { get; set; }
        [ForeignKey("IdUserNavigation")]
        public string? MaDG { get; set; }
        [ForeignKey("IdMCBNavigation")]
        public int? ID_MCB { get; set; }
        public DateTime? NgayMuon { get; set; } = DateTime.Now;
        public DateTime? NgayTra { get; set; }
        public string? TrangThai {get; set; }

        public virtual tblMaCaBiet IdMCBNavigation { get; set; }
        public virtual tblUser IdUserNavigation { get; set; }
    }
}