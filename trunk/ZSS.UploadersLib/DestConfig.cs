using System;
using System.Collections.Generic;
using System.Text;
using HelpersLib;

namespace UploadersLib
{
    public class DestConfig
    {
        public List<OutputEnum> Outputs = new List<OutputEnum>();
        public List<ClipboardContentEnum> TaskClipboardContent = new List<ClipboardContentEnum>();
        public List<LinkFormatEnum> LinkFormat = new List<LinkFormatEnum>();

        public List<ImageDestination> ImageUploaders = new List<ImageDestination>();
        public List<TextDestination> TextUploaders = new List<TextDestination>();
        public List<FileDestination> FileUploaders = new List<FileDestination>();
        public List<UrlShortenerType> LinkUploaders = new List<UrlShortenerType>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            string temp = ToStringOutputs();
            if (!string.IsNullOrEmpty(temp))
            {
                sb.Append(temp);
            }

            temp = ToStringImageUploaders();
            if (!string.IsNullOrEmpty(temp))
            {
                sb.Append(" using ");
                sb.Append(temp);
            }

            temp = ToStringTextUploaders();
            if (!string.IsNullOrEmpty(temp))
            {
                sb.Append(", ");
                sb.Append(temp);
            }

            temp = ToStringFileUploaders();
            if (!string.IsNullOrEmpty(temp))
            {
                sb.Append(", ");
                sb.Append(temp);
            }

            temp = ToStringLinkUploaders();
            if (!string.IsNullOrEmpty(temp))
            {
                sb.Append(", ");
                sb.Append(temp);
            }

            return sb.ToString();
        }

        private string ToString<T>(List<T> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T ut in list)
            {
                Enum en = (Enum)Convert.ChangeType(ut, typeof(Enum));
                sb.Append(en.GetDescription());
                sb.Append(", ");
            }
            if (sb.Length > 2)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            return sb.ToString();
        }

        public string ToStringOutputs()
        {
            return ToString<OutputEnum>(Outputs);
        }

        public string ToStringImageUploaders()
        {
            return ToString<ImageDestination>(ImageUploaders);
        }

        public string ToStringTextUploaders()
        {
            return ToString<TextDestination>(TextUploaders);
        }

        public string ToStringFileUploaders()
        {
            return ToString<FileDestination>(FileUploaders);
        }

        public string ToStringLinkUploaders()
        {
            return ToString<UrlShortenerType>(LinkUploaders);
        }
    }
}