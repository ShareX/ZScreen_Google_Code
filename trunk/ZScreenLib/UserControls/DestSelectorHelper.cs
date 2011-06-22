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
            AddEnumDestImageToMenuWithConfigSettings();
            AddEnumDestTextToMenuWithConfigSettings();
            AddEnumDestFileToMenuWithConfigSettings();
            AddEnumDestLinkToMenuWithConfigSettings();
        }

        public void AddEnumDestImageToMenuWithConfigSettings()
        {
            AddEnumDestImageToMenuWithSettings(ucDestOptions.tsddbDestImage, Engine.conf.MyImageUploaders);
        }

        public void AddEnumDestImageToMenuWithRuntimeSettings(List<int> MyImageUploaders)
        {
            AddEnumDestImageToMenuWithSettings(ucDestOptions.tsddbDestImage, MyImageUploaders);
        }

        private void AddEnumDestImageToMenuWithSettings(ToolStripDropDownButton tsddb, List<int> MyImageUploaders)
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
                    if (t == ImageUploaderType.PRINTER)
                    {
                        tsddb.DropDownItems.Add(new ToolStripSeparator());
                    }
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
            AddEnumDestLinkToMenuWithRuntimeSettings(ucDestOptions.tsddDestLink, Engine.conf.MyURLShorteners);
        }

        public void AddEnumDestLinkToMenuWithRuntimeSettings(List<int> MyLinkUploaders)
        {
            AddEnumDestLinkToMenuWithRuntimeSettings(ucDestOptions.tsddDestLink, MyLinkUploaders);
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
            UpdateToolStripDest(ucDestOptions.tsddDestLink, "URL Shortener");
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

    }
}
