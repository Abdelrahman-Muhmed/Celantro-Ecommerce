using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EcommerceCoffee.Controllers
{
    public class EncryptController : Controller
    {
        [HttpPost]
        public JsonResult EncryptPath(string key, string plainText)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(plainText))
            {
                return Json(new { success = false, message = "Invalid input." });
            }

            string encryptedString = EncryptString(key, plainText);
            return Json(new { success = true, encryptedString });
        }

        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }



        [HttpPost]
        public JsonResult DecryptPath(string key, string cipherText)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(cipherText))
            {
                return Json(new { success = false, message = "Invalid input." });
            }

            string decryptedString = DecryptString(key, cipherText);
            return Json(new { success = true, decryptedString });
        }


        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16]; // Initialization vector
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}