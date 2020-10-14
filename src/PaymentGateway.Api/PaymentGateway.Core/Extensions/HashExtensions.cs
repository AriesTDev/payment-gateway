using System;
using System.Security.Cryptography;
using System.Text;

namespace PaymentGateway.Core.Extensions
{
    public static class HashExtensions
    {
        public static string ToMd5Hash(this string input)
        {
            string hex;
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                hex = BitConverter.ToString(data).Replace("-", string.Empty);
            }
            return hex;
        }

        public static string ToSha256Hash(this string plaintext)
        {
            string hex;
            using (var sha256 = SHA256.Create())
            {
                var data = sha256.ComputeHash(Encoding.UTF8.GetBytes(plaintext));
                hex = BitConverter.ToString(data).Replace("-", string.Empty);
            }
            return hex;
        }

        public static string ToSha1Hash(this string text)
        {
            string hex;
            using (var sha1 = SHA1.Create())
            {
                var data = sha1.ComputeHash(Encoding.UTF8.GetBytes(text));
                hex = BitConverter.ToString(data).Replace("-", string.Empty);
            }
            return hex;
        }

        public static string ToHmacSha1Hash(this string text, string key)
        {
            string hex;
            using (var hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(key)))
            {
                var data = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(text));
                hex = BitConverter.ToString(data).Replace("-", string.Empty);
            }

            return hex;
        }

        public static string ToHmacSha256Hash(this string input, string key)
        {
            var sBuilder = new StringBuilder();
            byte[] bkey = Encoding.UTF8.GetBytes(key);
            using (var hmacsha256 = new HMACSHA256(bkey))
            {
                byte[] data = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                foreach (byte b in data)
                    sBuilder.Append(b.ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// AES Encrypt
        /// </summary>
        /// <param name="content">Need encrypted string</param>
        /// <returns>Encrypted 16 hex string</returns>
        public static string ToAES256Hash(this string input, string key)
        {
            byte[] keyArray = UTF8Encoding.ASCII.GetBytes(key);
            byte[] keyBytes = new byte[16];
            int len = keyArray.Length;
            if (len > keyBytes.Length)
                len = keyBytes.Length;
            System.Array.Copy(Convert.FromBase64String(key.PadRight(key.Length)), keyBytes, len);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyBytes;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(input);
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray);
        }

        /// <summary>
        /// AES Decrypt
        /// </summary>
        /// <param name="encryptedSource"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string FromAES256Hash(this string encryptedSource, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Convert.FromBase64String(encryptedSource);

            byte[] keyBytes = new byte[16];
            int len = keyArray.Length;
            if (len > keyBytes.Length)
                len = keyBytes.Length;
            System.Array.Copy(Convert.FromBase64String(key.PadRight(key.Length)), keyBytes, len);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyBytes;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] originalSrouceData = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(originalSrouceData);
        }

    }
}
