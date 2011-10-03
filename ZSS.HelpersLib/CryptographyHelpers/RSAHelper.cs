using System.Security.Cryptography;

namespace HelpersLib
{
    public class RSAHelper
    {
        private RSAParameters RSAParams;

        public RSAHelper(RSAParameters key)
        {
            RSAParams = key;
        }

        public string Encrypt(string text)
        {
            var provider = new System.Security.Cryptography.RSACryptoServiceProvider();
            provider.ImportParameters(RSAParams);

            var encryptedBytes = provider.Encrypt(System.Text.Encoding.UTF8.GetBytes(text), true);

            return provider.ToString();
        }

        public string Decrypt(byte[] encryptedBytes)
        {
            var provider = new System.Security.Cryptography.RSACryptoServiceProvider();
            provider.ImportParameters(RSAParams);

            return System.Text.Encoding.UTF8.GetString(provider.Decrypt(encryptedBytes, true));
        }
    }
}