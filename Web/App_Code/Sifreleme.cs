using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Collections.Generic;

/// <summary>
/// Summary description for Sifreleme
/// </summary>
public static class Sifreleme
{
    // herhangi birşey olabilir
    private const string passPhrase = "lazkopat";

    // herhangi birşey olabilir
    private const string saltValue = "YUTxyzPOMNBGHTYRFDSEW";

    // SHA1 ya da MD5
    private const string hashAlgorithm = "SHA1";

    // herhangi bir sayı olabilir
    private const int passwordIterations = 2;

    // 16 byte olmalı
    private const string initVector = "@5A2c2!x@5FPx7H8";

    // kaç bit şifreleme?
    private const int keySize = 256;

    public static string Sifrele(this string plainText)
    {
        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        PasswordDeriveBytes password =
            new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

        byte[] keyBytes = password.GetBytes(keySize / 8);

        RijndaelManaged symmetricKey = new RijndaelManaged();

        symmetricKey.Mode = CipherMode.CBC;

        ICryptoTransform encryptor =
            symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

        MemoryStream memoryStream = new MemoryStream();

        CryptoStream cryptoStream =
            new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

        cryptoStream.FlushFinalBlock();

        byte[] cipherTextBytes = memoryStream.ToArray();

        memoryStream.Close();
        cryptoStream.Close();

        string cipherText = Convert.ToBase64String(cipherTextBytes);

        return cipherText;
    }

    public static string Sifrele(this string plainText, bool URLEncode)
    {
        if (URLEncode)
            return SafeBase64UrlEncoder.EncodeBase64Url(plainText.Sifrele());
        else
            return plainText.Sifrele();
    }

    public static string SifreCoz(this string cipherText)
    {
        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

        PasswordDeriveBytes password =
            new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

        byte[] keyBytes = password.GetBytes(keySize / 8);

        RijndaelManaged symmetricKey = new RijndaelManaged();

        symmetricKey.Mode = CipherMode.CBC;

        ICryptoTransform decryptor =
            symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

        CryptoStream cryptoStream =
            new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

        byte[] plainTextBytes = new byte[cipherTextBytes.Length];

        int decryptedByteCount =
            cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

        memoryStream.Close();
        cryptoStream.Close();

        string plainText =
            Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

        return plainText;
    }

    public static string SifreCoz(this string cipherText, bool URLDecode)
    {
        if (URLDecode)
            return SafeBase64UrlEncoder.DecodeBase64Url(cipherText).SifreCoz();
        else
            return cipherText.SifreCoz();
    }

    public static class SafeBase64UrlEncoder
    {
        private const string Plus = "+";
        private const string Minus = "-";
        private const string Slash = "/";
        private const string Underscore = "_";
        private const string EqualSign = "=";
        private const string Pipe = "|";
        private static readonly IDictionary<string, string> _mapper;
        static SafeBase64UrlEncoder()
        {
            _mapper = new Dictionary<string, string> 
            { 
                { Plus, Minus }, 
                { Slash, Underscore }, 
                { EqualSign, Pipe } 
            };
        }
        public static string EncodeBase64Url(string base64Str)
        {
            if (string.IsNullOrEmpty(base64Str)) return base64Str;
            foreach (var pair in _mapper)
                base64Str = base64Str.Replace(pair.Key, pair.Value);
            return base64Str;
        }
        public static string DecodeBase64Url(string safe64Url)
        {
            if (string.IsNullOrEmpty(safe64Url)) return safe64Url;
            foreach (var pair in _mapper)
                safe64Url = safe64Url.Replace(pair.Value, pair.Key);
            return safe64Url;
        }
    }

}