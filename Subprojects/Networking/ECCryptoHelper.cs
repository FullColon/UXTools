using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EC.Networking
{
    /// <summary>
    /// Class to help with cryptography.
    /// </summary>
    public static class ECCryptoHelper
    {
        /// <summary>
        /// Get a new instance of our encryptor.
        /// </summary>
        /// <returns>Encryptor</returns>
        public static AesManaged InitializeAesManaged()
        {
            AesManaged aes = new AesManaged();
            aes.GenerateIV();
            aes.GenerateKey();
            return aes;
        }

        /// <summary>
        /// Encrypt a byte array
        /// </summary>
        /// <param name="_original">Original byte array to encrypt</param>
        /// <param name="_aesManaged">Managed encryptor to use</param>
        /// <returns></returns>
        public static byte[] EncryptBytes(ref byte[] _original, AesManaged _aesManaged)
        {
            if (
                _original == null          ||
                _original.Length == 0      ||
                _aesManaged == null         ||
                _aesManaged.Key.Length == 0 ||
                _aesManaged.IV.Length == 0
            )
                return null;
            byte[] encrypted;
            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, _aesManaged.CreateEncryptor(), CryptoStreamMode.Write);
            csEncrypt.Write(_original, 0, _original.Length);
            csEncrypt.FlushFinalBlock();
            encrypted = msEncrypt.ToArray();
            return encrypted;
        }

        /// <summary>
        /// Decrypt a byte array
        /// </summary>
        /// <param name="_encrypted">Encrypted bytes</param>
        /// <param name="_aesManaged">Encryption manager</param>
        /// <returns></returns>
        public static byte[] DecryptBytes(ref byte[] _encrypted, AesManaged _aesManaged)
        {
            if (
                _encrypted == null         ||
                _encrypted.Length == 0     ||
                _aesManaged == null         ||
                _aesManaged.Key.Length == 0 ||
                _aesManaged.IV.Length == 0
            )
                return null;
            byte[] decrypted;
            MemoryStream msDecrypt = new MemoryStream();
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, _aesManaged.CreateDecryptor(), CryptoStreamMode.Write);
            csDecrypt.Write(_encrypted, 0, _encrypted.Length);
            csDecrypt.FlushFinalBlock();
            decrypted = msDecrypt.ToArray();
            return decrypted;
        }
    }
}
