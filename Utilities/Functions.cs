using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using aznews.Areas.Admin.Models;
using aznews.Models;

namespace aznews.Utilities
{
    public class Functions
    {
    public static int _UserID = 0;
    public static string _UserName = string.Empty;
    public static string _Email = string.Empty;
    public static string _Message = string.Empty;   
     public static string TitleSlugGeneration(string type, string? title, long id) 
     {
        return type + "-" + SlugGenerator.SlugGenerator.GenerateSlug(title) + "-" + id.ToString() + ".html";
     }

    }

}