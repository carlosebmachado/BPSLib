using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BPS.Cryptography
{
    internal class Encrypt
    {
        #region Vars

        /// <summary></summary>
        internal Aes Algorithm { get; set; }
        /// <summary></summary>
        internal byte[] Key { get; set; }
        /// <summary></summary>
        internal byte[] InitVector { get; set; }

        #endregion Vars


        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        internal Encrypt(byte[] key)
        {
            Key = key;
            InitVector = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            Algorithm = Aes.Create();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="initVector"></param>
        internal Encrypt(byte[] key, byte[] initVector)
        {
            Key = key;
            InitVector = initVector;
            Algorithm = Aes.Create();
        }

        #endregion Constructors


        #region Methods

        #region Public

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal string Execute(string data)
        {
            byte[] encryptedData;
            byte[] dataToProtectAsArray = Encoding.UTF8.GetBytes(data);
            ICryptoTransform encryptor = Algorithm.CreateEncryptor(Key, InitVector);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(dataToProtectAsArray, 0, dataToProtectAsArray.Length);
            cryptoStream.FlushFinalBlock();
            encryptedData = memoryStream.ToArray();

            Algorithm.Dispose();
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal string Decrypt(string data)
        {
            byte[] encryptedData = Convert.FromBase64String(data);
            byte[] unencryptedData;
            ICryptoTransform decryptor = Algorithm.CreateDecryptor(Key, InitVector);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write);

            cryptoStream.Write(encryptedData, 0, encryptedData.Length);
            cryptoStream.FlushFinalBlock();
            unencryptedData = memoryStream.ToArray();

            Algorithm.Dispose();
            return Encoding.Default.GetString(unencryptedData);
        }

        #endregion Public

        #endregion Methods
    }
}
