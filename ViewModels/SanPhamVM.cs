using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aznews.Models;

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

        public class SachVM
    {
        public int ID_Sach { get; set; }

        public int ID_NXB { get; set; }

            public int ID_DM { get; set; }

        public string? TacGia { get; set; }
        public string? TenSach { get; set; }
        public string? NamXB { get; set; }
        public string? MoTa { get; set; }
        public string? Hinh { get; set; }
        public string? SoTrang { get; set; }
    public int SoLuongBanSao { get; set; } = 1; // mặc định là 1 bản

    public class SachWithCountViewModel
{
    public tblSach Sachs { get; set; }
    public int SoLuongChuaMuon { get; set; }
}

 public class PhieuMuonVM
{
     public int ID_PM { get; set; }
        public string? MaDG { get; set; }
        public string? TenSach { get; set; }
        public DateTime? NgayMuon { get; set; } = DateTime.Now;
        public DateTime? NgayTra { get; set; }
        public string? TrangThai {get; set; }
}

    } 
}