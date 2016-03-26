using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ETECHPOS.fnc
{
    public class MD5EncryptionFunction
    {
        public static string Encrypt(string plainText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(plainText);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);
            string encryptedText = BitConverter.ToString(encodedBytes);
            encryptedText = encryptedText.Replace("-", "");
            return encryptedText;
        }
    }
}
