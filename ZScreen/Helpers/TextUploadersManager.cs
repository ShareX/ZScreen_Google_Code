using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using ZSS.TextUploaders;

namespace ZSS.Helpers
{
    [Serializable]
    public class TextUploadersManager
    {

        public List<TextUploader> TextUploadersSettings = new List<TextUploader> { new PastebinUploader(), new Paste2Uploader(), new SlexyUploader() };
        public List<TextUploader> UrlShortenerSettings = new List<TextUploader> { new TinyURLUploader() };

        public TextUploader TextUploaderActive = new Paste2Uploader();
        public TextUploader UrlShortenerActive = new TinyURLUploader();

        public static TextUploadersManager Read()
        {
            return ReadBF(Program.TextUploadersFilePath);
        }

        public void Write()
        {
            WriteBF(Program.TextUploadersFilePath);
        }

        private void WriteBF(string filePath)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    bf.Serialize(fs, this);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        private void WriteXML(string filePath)
        {
            //try
            //{
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                XmlSerializer xs = new XmlSerializer(typeof(TextUploadersManager));
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    xs.Serialize(fs, this);
                }
            //}
            //catch (Exception e)
            //{
            //    System.Windows.Forms.MessageBox.Show(e.Message);
            //}
        }

        private static TextUploadersManager ReadBF(string filePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            if (File.Exists(filePath))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        return (TextUploadersManager)bf.Deserialize(fs);
                    }
                }
                catch (Exception ex)
                {
                    FileSystem.AppendDebug(ex.ToString());
                }
            }

            return new TextUploadersManager();
        }

        private static TextUploadersManager ReadXML(string filePath)
        {

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            if (File.Exists(filePath))
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(TextUploadersManager));
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        return (TextUploadersManager)xs.Deserialize(fs);
                    }
                }
                catch (Exception ex)
                {
                    FileSystem.AppendDebug(ex.ToString());
                }
            }

            return new TextUploadersManager();
        }
    }
}