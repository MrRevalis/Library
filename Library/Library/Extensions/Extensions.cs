using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Library.Extensions
{
    public static class Extensions
    {
        public static string hashSHA256(this string password)
        {
            using (SHA256 hash = SHA256.Create())
            {
                return string.Concat(hash
                    .ComputeHash(Encoding.UTF8.GetBytes(password))
                    .Select(x => x.ToString("x2")));
            }
        }
    }
}