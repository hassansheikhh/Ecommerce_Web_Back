using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Ecommerce_Web_API.Common
{
    public class CommonUser

    {

        public const string Passphrase = "HassanJaree";
        public static string GetMd5Hash(string input, string postPhrase)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(GetSaltAndPepper(input, postPhrase)));

                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));

                }
                return sBuilder.ToString();
            }
        }


        public static bool VerifyMd5Hash(string input, string postPhrase, string hash)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string hashOfInput = GetMd5Hash(input, postPhrase);

                StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                if (0 == comparer.Compare(hashOfInput, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        private static string GetSaltAndPepper(string input, string postPhrase)
        {
            return input + postPhrase + Passphrase;
        }
    }

}