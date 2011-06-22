using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UploadersLib;

namespace ZScreenLib
{
    public class DestSelectorHelper
    {
        private DestSelector ucDestOptions;

        public DestSelectorHelper(DestSelector ds)
        {
            ucDestOptions = ds;
        }

        public void AddEnumDestToMenuWithConfigSettings()
        {
            AddEnumOutputTypeWithConfigSettings();
            AddEnumLinkFormatWithConfigSettings();
            AddEnumDestImageToMenuWithConfigSettings();
            AddEnumDestTextToMenuWithConfigSettings();
            AddEnumDestFileToMenuWithConfigSettings();
            AddEnumDestLinkToMenuWithConfigSettings();
        }

        public void AddEnumLinkFormatWithConfigSettings()
        {
            if (Engine.conf.ConfLinkFormat.Count == 0)
            {
                Engine.conf.ConfLinkFormat.Add((int)OutputTypeEnum.Bitmap);
            }
            AddEnumLinkFormatWithConfigSettings(ucDestOptions.tsddbLinkFormat, Engine.conf.ConfLinkFormat);
        }

        public void AddEnumLinkFormatWithRuntimeSettings(List<int> list)
        {
            AddEnumLinkFormatWithConfigSettings(ucDestOptions.tsddbLinkFormat, list);
        }

        public void AddEnumLinkFormatWithConfigSettings(ToolStripDropDownButton tsddb, List<int> list)
        {
            if (tsddb.DropDownItems.Count == 0)
            {
                foreach (LinkFormatEnum t in Enum.GetValues(typeof(LinkFormatEnum)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(t.GetDescription());
                    tsmi.Tag = t;
                    tsmi.Checked = list.Contains((int)t);
                    tsmi.Click += new EventHandler(tsmiDestLinkFormat_Click);
                    tsddb.DropDownItems.Add(tsmi);
                }
                UpdateToolStripLinkFormat();
            }
        }

        public void AddEnumOutputTypeWithConfigSettings()
        {
            if (Engine.conf.ConfOutputType.Count == 0)
            {
                Engine.conf.ConfOutputType.Add((int)OutputTypeEnum.Bitmap);
            }
            AddEnumOutputTypeWithRuntimeSettings(ucDestOptions.tsddbOutputType, Engine.conf.ConfOutputType);
            ucDestOptions.EnableDisableDestControls();
        }

        public void AddEnumOutputTypeWithRuntimeSettings(List<int> cctList)
        {
            AddEnumOutputTypeWithRuntimeSettings(ucDestOptions.tsddbOutputType, cctList);
        }

        public void AddEnumOutputTypeWithRuntimeSettings(ToolStripDropDownButton tsddb, List<int> ClipboardContentType)
        {
            foreach (OutputTypeEnum t in Enum.GetValues(typeof(OutputTypeEnum)))
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(t.GetDescription());
                tsmi.Tag = t;
                tsmi.Checked = ClipboardContentType.Contains((int)t);
                tsmi.Click += new EventHandler(tsmiDestClipboardContent_Click);
                tsddb.DropDownItems.Add(tsmi);
            }
            UpdateToolStripClipboardContent();
        }

        public void AddEnumDestImageToMenuWithConfigSettings()
        {
            AddEnumDestImageToMenuWithRuntimeSettings(ucDestOptions.tsddbDestImage, Engine.conf.MyImageUploaders);
        }

        public void AddEnumDestImageToMenuWithRuntimeSettings(List<int> MyImageUploaders)
        {
            AddEnumDestImageToMenuWithRuntimeSettings(ucDestOptions.tsddbDestImage, MyImageUploaders);
        }

        private void AddEnumDestImageToMenuWithRuntimeSettings(ToolStripDropDownButton tsddb, List<int> MyImageUploaders)
        {
            if (tsddb.DropDownItems.Count == 0)
            {
                foreach (ImageUploaderType t in Enum.GetValues(typeof(ImageUploaderType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(t.GetDescription());
                    tsmi.Tag = t;
                    tsmi.CheckOnClick = true;
                    tsmi.Checked = MyImageUploaders.Contains((int)t);
                    tsmi.Click += new EventHandler(tsmiDestImage_Click);
                    tsddb.DropDownItems.Add(tsmi);
                }
                UpdateToolStripDestImage();
            }
        }

        public void AddEnumDestTextToMenuWithConfigSettings()
        {
            AddEnumDestTextToMenuWithRuntimeSettings(ucDestOptions.tsddDestText, Engine.conf.MyTextUploaders);
        }

        public void AddEnumDestTextToMenuWithRuntimeSettings(List<int> MyTextUploaders)
        {
            AddEnumDestTextToMenuWithRuntimeSettings(ucDestOptions.tsddDestText, MyTextUploaders);
        }

        private void AddEnumDestTextToMenuWithRuntimeSettings(ToolStripDropDownButton tsddb, List<int> MyTextUploaders)
        {
            if (tsddb.DropDownItems.Count == 0)
            {
                foreach (TextUploaderType ut in Enum.GetValues(typeof(TextUploaderType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(ut.GetDescription());
                    tsmi.Tag = ut;
                    tsmi.CheckOnClick = true;
                    tsmi.Checked = MyTextUploaders.Contains((int)ut);
                    tsmi.Click += new EventHandler(tsmiDestText_Click);
                    tsddb.DropDownItems.Add(tsmi);
                }
                UpdateToolStripDestText();
            }
        }

        public void AddEnumDestFileToMenuWithConfigSettings()
        {
            AddEnumDestFileToMenuWithRuntimeSettings(ucDestOptions.tsddDestFile, Engine.conf.MyFileUploaders);
        }

        public void AddEnumDestFileToMenuWithRuntimeSettings(List<int> MyFileUploaders)
        {
            AddEnumDestFileToMenuWithRuntimeSettings(ucDestOptions.tsddDestFile, MyFileUploaders);
        }

        private void AddEnumDestFileToMenuWithRuntimeSettings(ToolStripDropDownButton tsddb, List<int> MyFileUploaders)
        {
            if (tsddb.DropDownItems.Count == 0)
            {
                foreach (FileUploaderType ut in Enum.GetValues(typeof(FileUploaderType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(ut.GetDescription());
                    tsmi.Tag = ut;
                    tsmi.CheckOnClick = true;
                    tsmi.Checked = MyFileUploaders.Contains((int)ut);
                    tsmi.Click += new EventHandler(tsmiDestFiles_Click);
                    tsddb.DropDownItems.Add(tsmi);
                }
                UpdateToolStripDestFile();
            }
        }

        public void AddEnumDestLinkToMenuWithConfigSettings()
        {
            AddEnumDestLinkToMenuWithRuntimeSettings(ucDestOptions.tsddbDestLink, Engine.conf.MyURLShorteners);
        }

        public void AddEnumDestLinkToMenuWithRuntimeSettings(List<int> MyLinkUploaders)
        {
            AddEnumDestLinkToMenuWithRuntimeSettings(ucDestOptions.tsddbDestLink, MyLinkUploaders);
        }

        private void AddEnumDestLinkToMenuWithRuntimeSettings(ToolStripDropDownButton tsddb, List<int> MyLinkUploaders)
        {
            if (tsddb.DropDownItems.Count == 0)
            {
                foreach (UrlShortenerType ut in Enum.GetValues(typeof(UrlShortenerType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(ut.GetDescription());
                    tsmi.Tag = ut;
                    tsmi.Checked = MyLinkUploaders.Contains((int)ut);
                    tsmi.Click += new EventHandler(tsmiDestLinks_Click);
                    tsddb.DropDownItems.Add(tsmi);
                }
                UpdateToolStripDestLink();
            }
        }

        void UpdateToolStripDest(ToolStripDropDownButton tsdd, string descr)
        {
            string dest = string.Empty;
            int count = 0;

            foreach (var obj in tsdd.DropDownItems)
            {
                if (obj.GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem gtsmi = obj as ToolStripMenuItem;
                    if (gtsmi.Checked)
                    {
                        count++;
                        dest = ((Enum)gtsmi.Tag).GetDescription();
                    }
                }
            }

            if (count == 0)
            {
                tsdd.Text = descr + ": None";
            }
            else if (count == 1)
            {
                tsdd.Text = descr + ": " + dest;
            }
            else if (count > 1)
            {
                tsdd.Text = string.Format("{0}: {1} and {2} other destination(s)", descr, dest, count - 1);
            }
        }

        void UpdateToolStripDestImage()
        {
            UpdateToolStripDest(ucDestOptions.tsddbDestImage, "Image output");
        }

        void UpdateToolStripDestText()
        {
            UpdateToolStripDest(ucDestOptions.tsddDestText, "Text output");
        }

        void UpdateToolStripDestFile()
        {
            UpdateToolStripDest(ucDestOptions.tsddDestFile, "File output");
        }

        void UpdateToolStripDestLink()
        {
            UpdateToolStripDest(ucDestOptions.tsddbDestLink, "URL shortener");
        }

        void UpdateToolStripClipboardContent()
        {
            UpdateToolStripDest(ucDestOptions.tsddbOutputType, "Clipboard content");
        }

        void UpdateToolStripLinkFormat()
        {
            UpdateToolStripDest(ucDestOptions.tsddbLinkFormat, "URL format");
        }

        void tsmiDestImage_Click(object sender, EventArgs e)
        {
            UpdateToolStripDestImage();
        }

        void tsmiDestText_Click(object sender, EventArgs e)
        {
            UpdateToolStripDestText();
        }

        void tsmiDestFiles_Click(object sender, EventArgs e)
        {
            UpdateToolStripDestFile();
        }

        void tsmiDestLinks_Click(object sender, EventArgs e)
        {
            UpdateToolStripDestLink();
        }

        void tsmiDestClipboardContent_Click(object sender, EventArgs e)
        {
            UpdateToolStripClipboardContent();
        }

        void tsmiDestLinkFormat_Click(object sender, EventArgs e)
        {
            UpdateToolStripLinkFormat();
        }

    }
}
