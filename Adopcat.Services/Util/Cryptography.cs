using System;
using System.Security.Cryptography;
using System.Text;

namespace Adopcat.Services.Util
{
    public class Cryptography
    {
        public static string GetMD5Hash(string plainText)
        {
            byte[] cryptoByte = new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes(plainText));

            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.Length);
        }
    }
}
