using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UploadersLib;
using ZScreenLib.Properties;

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
            AddEnumOutputsWithConfigSettings();
            AddEnumClipboardContentWithConfigSettings();
            AddEnumLinkFormatWithConfigSettings();
            AddEnumDestImageToMenuWithConfigSettings();
            AddEnumDestTextToMenuWithConfigSettings();
            AddEnumDestFileToMenuWithConfigSettings();
            AddEnumDestLinkToMenuWithConfigSettings();
        }

        public void AddEnumOutputsWithConfigSettings()
        {
            if (Engine.conf.ConfOutputs.Count == 0)
            {
                Engine.conf.ConfOutputs.Add((int)OutputEnum.Clipboard);
                Engine.conf.ConfOutputs.Add((int)OutputEnum.LocalDisk);
            }
            SetupOutputsWithRuntimeSettings(ucDestOptions.tsddbOutputs, Engine.conf.ConfOutputs.Cast<OutputEnum>().ToList());
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

        public void AddEnumOutputsWithConfigSettings(List<OutputEnum> list)
        {
            SetupOutputsWithRuntimeSettings(ucDestOptions.tsddbOutputs, list);
        }

        private void SetupOutputsWithRuntimeSettings(ToolStripDropDownButton tsddb, List<OutputEnum> list)
        {
            if (tsddb.DropDownItems.Count == 0)
            {
                foreach (OutputEnum t in Enum.GetValues(typeof(OutputEnum)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(t.GetDescription());

                    switch (t)
                    {
                        case OutputEnum.Clipboard:
                            tsmi.Image = Resources.clipboard;
                            tsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((Keys.Control | Keys.Shift | System.Windows.Forms.Keys.C)));
                            break;
                        case OutputEnum.LocalDisk:
                            tsmi.Image = Resources.drive;
                            tsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((Keys.Control | Keys.Shift | Keys.S)));
                            break;
                        case OutputEnum.Printer:
                            tsmi.Image = Resources.printer;
                            tsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((Keys.Control | Keys.Shift | System.Windows.Forms.Keys.P)));
                            break;
                        case OutputEnum.SharedFolder:
                            tsmi.Image = Resources.drive_network;
                            tsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((Keys.Control | Keys.Shift | System.Windows.Forms.Keys.N)));
                            break;
                        case OutputEnum.Email:
                            tsmi.Image = Resources.mail;
                            tsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((Keys.Control | Keys.Shift | System.Windows.Forms.Keys.E)));
                            break;
                        case OutputEnum.RemoteHost:
                            tsmi.Image = Resources.server;
                            tsmi.ShortcutKeys = ((System.Windows.Forms.Keys)((Keys.Control | Keys.Shift | System.Windows.Forms.Keys.H)));
                            break;
                    }

                    tsmi.Tag = t;

                    tsmi.Checked = list.Contains(t);
                    tsmi.Click += new EventHandler(tsmiOutputs_Click);
                    if (Engine.AppConf.SupportMultipleDestinations)
                    {
                        tsmi.CheckOnClick = true;
                    }
                    tsddb.DropDownItems.Add(tsmi);
                }
                UpdateToolStripOutputs();
                ucDestOptions.EnableDisableDestControls();
            }
        }

        public void AddEnumLinkFormatWithConfigSettings()
        {
            if (Engine.conf.ConfLinkFormat.Count == 0)
            {
                Engine.conf.ConfLinkFormat.Add((int)ClipboardContentEnum.Data);
            }
            AddEnumLinkFormatWithConfigSettings(ucDestOptions.tsddbLinkFormat, Engine.conf.ConfLinkFormat);
        }

        public void AddEnumLinkFormatWithRuntimeSettings(List<int> list)
        {
            AddEnumLinkFormatWithConfigSettings(ucDestOptions.tsddbLinkFormat, list);
        }

        public void AddEnumClipboardContentWithConfigSettings()
        {
            if (Engine.conf.ConfClipboardContent.Count == 0)
            {
                Engine.conf.ConfClipboardContent.Add((int)ClipboardContentEnum.Data);
            }
            AddEnumClipboardContentWithRuntimeSettings(ucDestOptions.tsddbClipboardContent, Engine.conf.ConfClipboardContent.Cast<ClipboardContentEnum>().ToList());
        }

        public void AddEnumClipboardContentWithRuntimeSettings(List<ClipboardContentEnum> cctList)
        {
            AddEnumClipboardContentWithRuntimeSettings(ucDestOptions.tsddbClipboardContent, cctList);
        }

        public void AddEnumClipboardContentWithRuntimeSettings(ToolStripDropDownButton tsddb, List<ClipboardContentEnum> ClipboardContentType)
        {
            if (tsddb.DropDownItems.Count == 0)
            {
                foreach (ClipboardContentEnum t in Enum.GetValues(typeof(ClipboardContentEnum)))
                {
                    ToolStripRadioButtonMenuItem tsmi = new ToolStripRadioButtonMenuItem(t.GetDescription());
                    tsmi.Tag = t;
                    tsmi.Checked = ClipboardContentType.Contains(t);
                    tsmi.Click += new EventHandler(tsmiDestClipboardContent_Click);
                    tsddb.DropDownItems.Add(tsmi);
                }
            }
            UpdateToolStripClipboardContent();
            ucDestOptions.EnableDisableDestControls();
        }

        public void AddEnumDestImageToMenuWithConfigSettings()
        {
            AddEnumDestImageToMenuWithRuntimeSettings(ucDestOptions.tsddbDestImage, Engine.conf.MyImageUploaders);
        }

        public void AddEnumDestImageToMenuWithRuntimeSettings(List<int> MyImageUploaders)
        {
            AddEnumDestImageToMenuWithRuntimeSettings(ucDestOptions.tsddbDestImage, MyImageUploaders);
        }

        private void AddEnumDestImageToMenuWithRuntimeSettings(ToolStripDropDownButton tsddb, List<int> list)
        {
            if (tsddb.DropDownItems.Count == 0)
            {
                foreach (ImageUploaderType t in Enum.GetValues(typeof(ImageUploaderType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(t.GetDescription());
                    tsmi.Tag = t;
                    tsmi.Checked = list.Contains((int)t);
                    tsmi.Click += new EventHandler(tsmiDestImage_Click);
                    if (Engine.AppConf.SupportMultipleDestinations)
                    {
                        tsmi.CheckOnClick = true;
                    }
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

        private void AddEnumDestTextToMenuWithRuntimeSettings(ToolStripDropDownButton tsddb, List<int> list)
        {
            if (tsddb.DropDownItems.Count == 0)
            {
                foreach (TextUploaderType ut in Enum.GetValues(typeof(TextUploaderType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(ut.GetDescription());
                    tsmi.Tag = ut;
                    tsmi.Checked = list.Contains((int)ut);
                    tsmi.Click += new EventHandler(tsmiDestText_Click);
                    if (Engine.AppConf.SupportMultipleDestinations)
                    {
                        tsmi.CheckOnClick = true;
                    }
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

        private void AddEnumDestFileToMenuWithRuntimeSettings(ToolStripDropDownButton tsddb, List<int> list)
        {
            if (tsddb.DropDownItems.Count == 0)
            {
                foreach (FileUploaderType ut in Enum.GetValues(typeof(FileUploaderType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(ut.GetDescription());
                    tsmi.Tag = ut;
                    tsmi.Checked = list.Contains((int)ut);
                    tsmi.Click += new EventHandler(tsmiDestFiles_Click);
                    if (Engine.AppConf.SupportMultipleDestinations)
                    {
                        tsmi.CheckOnClick = true;
                    }
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

        private void AddEnumDestLinkToMenuWithRuntimeSettings(ToolStripDropDownButton tsddb, List<int> list)
        {
            if (tsddb.DropDownItems.Count == 0)
            {
                foreach (UrlShortenerType ut in Enum.GetValues(typeof(UrlShortenerType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(ut.GetDescription());
                    tsmi.Tag = ut;
                    tsmi.Checked = list.Contains((int)ut);
                    tsmi.Click += new EventHandler(tsmiDestLinks_Click);
                    tsddb.DropDownItems.Add(tsmi);
                }
                UpdateToolStripDestLink();
            }
        }

        public static void UpdateToolStripDest(ToolStripDropDownButton tsdd)
        {
            string descr = tsdd.Tag as string;
            List<string> dest = new List<string>();

            foreach (var obj in tsdd.DropDownItems)
            {
                if (obj is ToolStripMenuItem)
                {
                    ToolStripMenuItem gtsmi = (ToolStripMenuItem)obj;

                    if (gtsmi.Checked)
                    {
                        dest.Add(((Enum)gtsmi.Tag).GetDescription());
                    }
                }
            }

            if (dest.Count == 0)
            {
                tsdd.Text = descr + ": None";
            }
            else if (dest.Count == 1)
            {
                tsdd.Text = descr + ": " + dest[0];
            }
            else if (dest.Count == 2)
            {
                tsdd.Text = descr + ": " + dest[0] + " and " + dest[1];
            }
            else if (dest.Count == 3)
            {
                tsdd.Text = string.Format("{0}: {1}, {2} and {3}", descr, dest[0], dest[1], dest[2]);
            }
            else if (dest.Count > 3)
            {
                tsdd.Text = string.Format("{0}: {1}, {2} and {3} more", descr, dest[0], dest[1], dest.Count - 2);
            }
        }

        private void UpdateToolStripDestImage()
        {
            UpdateToolStripDest(ucDestOptions.tsddbDestImage);
        }

        private void UpdateToolStripDestText()
        {
            UpdateToolStripDest(ucDestOptions.tsddDestText);
        }

        private void UpdateToolStripDestFile()
        {
            UpdateToolStripDest(ucDestOptions.tsddDestFile);
        }

        private void UpdateToolStripDestLink()
        {
            UpdateToolStripDest(ucDestOptions.tsddbDestLink);
        }

        private void UpdateToolStripClipboardContent()
        {
            UpdateToolStripDest(ucDestOptions.tsddbClipboardContent);
        }

        private void UpdateToolStripLinkFormat()
        {
            UpdateToolStripDest(ucDestOptions.tsddbLinkFormat);
        }

        private void UpdateToolStripOutputs()
        {
            UpdateToolStripDest(ucDestOptions.tsddbOutputs);
        }

        private void tsmiDestImage_Click(object sender, EventArgs e)
        {
            UpdateToolStripDestImage();
        }

        private void tsmiDestText_Click(object sender, EventArgs e)
        {
            UpdateToolStripDestText();
        }

        private void tsmiDestFiles_Click(object sender, EventArgs e)
        {
            UpdateToolStripDestFile();
        }

        private void tsmiDestLinks_Click(object sender, EventArgs e)
        {
            UpdateToolStripDestLink();
        }

        private void tsmiDestClipboardContent_Click(object sender, EventArgs e)
        {
            UpdateToolStripClipboardContent();
        }

        private void tsmiDestLinkFormat_Click(object sender, EventArgs e)
        {
            UpdateToolStripLinkFormat();
        }

        private void tsmiOutputs_Click(object sender, EventArgs e)
        {
            ucDestOptions.EnableDisableDestControls();
            UpdateToolStripOutputs();
            if (NoRemoteOutput())
            {
                ucDestOptions.GetClipboardContentTsmi(ucDestOptions.tsddbClipboardContent, ClipboardContentEnum.Data).Checked = true;
            }
            UpdateToolStripClipboardContent();
        }

        private bool NoRemoteOutput()
        {
            foreach (ToolStripMenuItem tsmi in ucDestOptions.tsddbOutputs.DropDownItems)
            {
                if ((OutputEnum)tsmi.Tag == OutputEnum.RemoteHost && tsmi.Checked)
                {
                    return false;
                }
            }
            return true;
        }
    }
}