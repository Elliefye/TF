using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace TF2
{
    class Encryption
    {
        public string Encrypt(string textToEncrypt)
        {
            try
            {
                string encrypted = "";
                string key = "ay$a5%&jwrtmnh;lasjdf98787";
                string iv = "abc@98797hjkas$&asd(*$%";
                byte[] ivByte = { };
                ivByte = System.Text.Encoding.UTF8.GetBytes(iv.Substring(0, 8));
                byte[] keyByte = { };
                keyByte = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
                MemoryStream ms = null; CryptoStream cs = null;
                byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(keyByte, ivByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    encrypted = Convert.ToBase64String(ms.ToArray());
                }
                return encrypted;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
        public string Decrypt(string textToDecrypt)
        {
            try
            {
                string decrypted = "";
                string key = "ay$a5%&jwrtmnh;lasjdf98787";
                string iv = "abc@98797hjkas$&asd(*$%";
                byte[] ivByte = { };
                ivByte = System.Text.Encoding.UTF8.GetBytes(iv.Substring(0, 8));
                byte[] keyByte = { };
                keyByte = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
                MemoryStream ms = null; CryptoStream cs = null;
                byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
                inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(keyByte, ivByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    decrypted = encoding.GetString(ms.ToArray());
                }
                return decrypted;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}
