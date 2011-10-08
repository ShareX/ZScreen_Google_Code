using System;
using System.Security.Cryptography;
using System.Xml;

namespace ZScreenLib.Cryptography
{
    class CryptKeys
    {
        CspParameters cp;
        RSACryptoServiceProvider rsa;
        string Container = string.Empty;
        public CryptKeys(string container = "ZScreen")
        {
            Container = container;
            cp = new CspParameters();
            cp.KeyContainerName = Container;
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
        public string Encrypt(string PlainText, CryptInfo ci, int KeySize, string EncryptionMethod = CryptMethod.SHA1, int PassIterations = 2)
        {
            return AESEncryption.Encrypt(PlainText, ci.Password, ci.Salt, EncryptionMethod, PassIterations, ci.Vector, KeySize);
        }

        public string Decrypt(string CryptedText, CryptInfo ci, int Keysize, string EncryptionMethod = CryptMethod.SHA1, int PassIterations = 2)
        {
            return AESEncryption.Decrypt(CryptedText, ci.Password, ci.Salt, EncryptionMethod, PassIterations, ci.Vector, Keysize);
        }
    }
}
