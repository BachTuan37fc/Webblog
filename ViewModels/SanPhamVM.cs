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
    public class ChitietSanPhamVM
    {
        public int ID { get; set; }
        public string? TenSP { get; set; }
        public string? Hinh { get; set; }
        public string? DanhMuc { get; set; }
        public int Soluong { get; set; }
    }
}