using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aznews.Models
{
    [Table("tblUser")]
    public class tblUser
{
    [Key]
    public string MaDG { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}
}