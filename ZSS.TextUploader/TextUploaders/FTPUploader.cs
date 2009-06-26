using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.TextUploaders.Helpers;
using System.IO;

namespace ZSS.TextUploaders
{
    [Serializable]
    public sealed class FTPUploader : TextUploader
    {
        public FTPAccount FTPAccount;

        public override string Name
        {
            get { return FTPAccount.Name; }
        }

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
        /// <param name="localFilePath"></param>
        /// <returns>Returns a list of images.</returns>
        public override string UploadText(TextFile text)
        {

            FTP ftpClient = new FTP(ref this.FTPAccount);
            //removed binary mode code line

            string fName = Path.GetFileName(text.LocalFilePath);
            ftpClient.UploadFile(text.LocalFilePath, fName);
            return this.FTPAccount.getUriPath(fName);

        }

        /// <summary>
        /// Descriptive name for the FTP Uploader
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("FTP ({0})", this.Name);
        }
    }
}
