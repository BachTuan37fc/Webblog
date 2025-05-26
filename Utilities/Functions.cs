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
        internal static string _Role;
        internal static string _Password;
        internal static string _MaDG;
        internal static int _ID_User;

        public static string TitleSlugGeneration(string type, string? title, long id) 
     {
        return type + "-" + SlugGenerator.SlugGenerator.GenerateSlug(title) + "-" + id.ToString() + ".html";
     }

        // internal static bool IsLogin()
        // {
        //     throw new NotImplementedException();
        // }

        // internal static string MD5Password(string password)
        // {
        //     throw new NotImplementedException();
        // }
        public static string MD5Hash(string text)
    {
    MD5 md5 = new MD5CryptoServiceProvider();
    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
    byte[] result = md5.Hash;
    StringBuilder strBuilder = new StringBuilder();
    for (int i = 0; i < result.Length; i++)
    {
        strBuilder.Append(result[i].ToString("x2"));
    }
    return strBuilder.ToString();
    }

    public static string MD5Password(string? text)
    {
    string str = MD5Hash(text);
    for (int i = 0; i <= 5; i++)
        str = MD5Hash(str + str);
    return str;
    }
    public static bool IsLogin() 
    {
        if((Functions._ID_User <=0) || string.IsNullOrEmpty(Functions._MaDG) || string.IsNullOrEmpty(Functions._Password))
            return false;
        return true;

    }

}}