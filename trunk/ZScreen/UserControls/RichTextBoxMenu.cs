﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace ZScreenGUI.UserControls
{
    public class RichTextBoxMenu
    {
        private RichTextBox rtb;
        private ContextMenuStrip cms;

        public RichTextBoxMenu(RichTextBox richTextBox)
        {
            rtb = richTextBox;

            cms = new ContextMenuStrip();
            cms.Items.Add("Cut").Click += new EventHandler(RichTextBoxMenuCut_Click);
            cms.Items.Add("Copy").Click += new EventHandler(RichTextBoxMenuCopy_Click);
            cms.Items.Add("Paste").Click += new EventHandler(RichTextBoxMenuPaste_Click);
            cms.Items.Add(new ToolStripSeparator());
            cms.Items.Add("Copy all").Click += new EventHandler(RichTextBoxMenuCopyAll_Click);
            cms.Items.Add("Select all").Click += new EventHandler(RichTextBoxMenuSelectAll_Click);

            rtb.ContextMenuStrip = cms;
        }

        public RichTextBoxMenu(RichTextBox richTextBox, bool makeURLClickable)
            : this(richTextBox)
        {
            if (makeURLClickable)
            {
                rtb.DetectUrls = true;
                rtb.LinkClicked += (v1, v2) => Process.Start(v2.LinkText);
            }
        }

        private void RichTextBoxMenuCut_Click(object sender, EventArgs e)
        {
            rtb.Cut();
        }

        private void RichTextBoxMenuCopy_Click(object sender, EventArgs e)
        {
            rtb.Copy();
        }

        private void RichTextBoxMenuPaste_Click(object sender, EventArgs e)
        {
            rtb.Paste();
        }

        private void RichTextBoxMenuCopyAll_Click(object sender, EventArgs e)
        {
            MemoryStream stream = new MemoryStream();
            rtb.SaveFile(stream, RichTextBoxStreamType.PlainText);
            stream.Position = 0;
            string text = new StreamReader(stream).ReadToEnd();
            if (!string.IsNullOrEmpty(text))
            {
                Clipboard.SetText(text);
            }
        }

        private void RichTextBoxMenuSelectAll_Click(object sender, EventArgs e)
        {
            rtb.SelectAll();
        }
    }
}