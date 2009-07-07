using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.TextUploaderLib.Helpers;
using System.IO;

namespace ZSS.TextUploaderLib
{
    [Serializable]
    public sealed class FTPUploader : TextUploader
    {
        public FTPAccount FTPAccount;

        public const string Hostname = "FTP";

        public FTPUploader()
        {
            this.Errors = new List<string>();
            this.FTPAccount = new FTPAccount();
        }

        public FTPUploader(FTPAccount acc)
            : this()
        {
            this.FTPAccount = acc;
        }

        public override string TesterString
        {
            get { return "Testing " + this.ToString(); }
        }

        public override object Settings
        {
            get
            {
                return this.FTPAccount;
            }
            set
            {
                this.FTPAccount = (FTPAccount)value;
            }
        }

        /// <summary>
        /// Uploads Text to the FTP. 
        /// If the method fails, it will return a list of zero images
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Returns a list of images.</returns>
        public override string UploadText(string text)
        {
            FTP ftpClient = new FTP(ref this.FTPAccount);
            string fileName = DateTime.Now.Ticks + ".txt";
            ftpClient.UploadText(text, fileName);
            return this.FTPAccount.GetUriPath(fileName);
        }

        /// <summary>
        /// Descriptive name for the FTP Uploader
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("FTP ({0})", FTPAccount.Name);
        }
    }
}