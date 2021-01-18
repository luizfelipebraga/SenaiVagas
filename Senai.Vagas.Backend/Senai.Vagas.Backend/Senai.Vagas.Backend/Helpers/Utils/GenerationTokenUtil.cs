using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Utils
{
    public static class GenerationTokenUtil
    {
        public static string TokenDefault()
        {
            string charSet = "abcdefghijkmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var chars = charSet.ToCharArray();

            var data = new byte[1];

            var crypto = new RNGCryptoServiceProvider();

            crypto.GetNonZeroBytes(data);

            data = new byte[60];

            crypto.GetNonZeroBytes(data);

            var result = new StringBuilder(60);

            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }

            return result.ToString();
        }

        public static string AccessKey()
        {
            string charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var chars = charSet.ToCharArray();

            var data = new byte[1];

            var crypto = new RNGCryptoServiceProvider();

            crypto.GetNonZeroBytes(data);

            data = new byte[6];

            crypto.GetNonZeroBytes(data);

            var result = new StringBuilder(60);

            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }

            return result.ToString();
        }
    }
}
