using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.ViewModels
{
    public class SanPhamVM
    {
        public int ID { get; set; }
        public string? TenSP { get; set; }
        public string? Hinh { get; set; }
        public string? DanhMuc { get; set; }
        public int Soluong { get; set; }
    }

    public class CTSanPhamVM
    {
        public int ID { get; set; }
        public string? HoTen { get; set; }
        public string? TenSP { get; set; }
        public string? Hinh { get; set; }
        public string? DanhMuc { get; set; }
        public string? MoTa { get; set; }
        public DateTime? NgayMuon { get; set; }
        public DateTime? NgayTra { get; set; }
        public int Soluong { get; set; }

    } 
    public class PhieuMuonVM
    {
        public int ID { get; set; }
        public int MaDG { get; set; }
        public DateTime? NgayMuon { get; set; }
        public DateTime? NgayTra { get; set; }
        public string? TrangThai { get; set; }

    } 
}