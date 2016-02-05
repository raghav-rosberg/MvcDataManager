using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MvcDataManager
{
    public sealed class Helper
    {
        /// <summary>
        /// Encrypts the string to base 64
        /// </summary>
        /// <param name="input">String to be encoded</param>
        /// <returns></returns>
        public static string Encrypt(string input)
        {
            return Convert.ToBase64String(EncryptByte(Encoding.UTF8.GetBytes(input), "myhelper"));
        }

        /// <summary>
        /// Encrypts the string to base 64
        /// </summary>
        /// <param name="input">String to be encoded</param>
        /// <param name="password">Password for encrypting</param>
        /// <returns></returns>
        public static string Encrypt(string input, string password)
        {
            return Convert.ToBase64String(EncryptByte(Encoding.UTF8.GetBytes(input), password));
        }

        /// <summary>
        /// Encryption function
        /// </summary>
        /// <param name="input">Data to be encoded</param>
        /// <param name="password">Password for encrypting</param>
        /// <returns></returns>
        private static byte[] EncryptByte(byte[] input, string password)
        {
            var pdb = new PasswordDeriveBytes(password, new byte[] { 0x43, 0x87, 0x23, 0x72, 0x45, 0x56, 0x68, 0x14, 0x62, 0x84 });
            var ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// Decrypts the string
        /// </summary>
        /// <param name="input">String to be decoded</param>
        /// <returns></returns>
        public static string Decrypt(string input)
        {
            input = input.Replace(" ", "+");
            return Encoding.UTF8.GetString(DecryptByt(Convert.FromBase64String(input), "myhelper"));
        }

        /// <summary>
        /// Decrypts the string
        /// </summary>
        /// <param name="input">String to be decoded</param>
        /// <param name="password">Password for decrypting</param>
        /// <returns></returns>
        public static string Decrypt(string input, string password)
        {
            input = input.Replace(" ", "+");
            return Encoding.UTF8.GetString(DecryptByt(Convert.FromBase64String(input), password));
        }

        /// <summary>
        /// Decryption Function
        /// </summary>
        /// <param name="input">String to be decoded</param>
        /// <param name="password">Password for decrypting</param>
        /// <returns></returns>
        private static byte[] DecryptByt(byte[] input, string password)
        {
            var pdb = new PasswordDeriveBytes(password, new byte[] { 0x43, 0x87, 0x23, 0x72, 0x45, 0x56, 0x68, 0x14, 0x62, 0x84 });
            var ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }
    }
}