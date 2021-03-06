﻿using System;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml;

namespace HelpersLib
{
    public class CryptKeys
    {
        CspParameters cp;
        RSACryptoServiceProvider rsa;
        string Container = string.Empty;
        public EncryptionStrength KeySize = EncryptionStrength.High;

        public CryptKeys()
        {
            Container = Application.ProductName;
            cp = new CspParameters();
            cp.KeyContainerName = Container;
            GenerateKey();
        }

        public void GenerateKey()
        {
            rsa = new RSACryptoServiceProvider(cp);
        }

        public String ReturnKeys()
        {
            return rsa.ToXmlString(true);
        }

        public void DeleteKey()
        {
            rsa.PersistKeyInCsp = false;
            rsa.Clear();
        }

        public CryptInfo ReturnInfo()
        {
            CryptInfo ci = new CryptInfo();
            string xml = ReturnKeys();
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            ci.Password = document.DocumentElement.SelectSingleNode("//P").InnerXml;
            ci.Salt = document.DocumentElement.SelectSingleNode("//Q").InnerText;
            ci.Vector = document.DocumentElement.SelectSingleNode("//D").InnerText.Substring(0, 16);
            return ci;
        }

        public string Encrypt(string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : Encrypt(text, ReturnInfo(), (int)this.KeySize);
        }

        public string Decrypt(string CryptedText)
        {
            return string.IsNullOrEmpty(CryptedText) ? string.Empty : Decrypt(CryptedText, ReturnInfo(), (int)this.KeySize);
        }

        public string Encrypt(string PlainText, CryptInfo ci, int KeySize = 256, string EncryptionMethod = CryptMethod.SHA1, int PassIterations = 2)
        {
            return AESEncryption.Encrypt(PlainText, ci.Password, ci.Salt, EncryptionMethod, PassIterations, ci.Vector, KeySize);
        }

        public string Decrypt(string CryptedText, CryptInfo ci, int Keysize = 256, string EncryptionMethod = CryptMethod.SHA1, int PassIterations = 2)
        {
            return AESEncryption.Decrypt(CryptedText, ci.Password, ci.Salt, EncryptionMethod, PassIterations, ci.Vector, Keysize);
        }
    }
}