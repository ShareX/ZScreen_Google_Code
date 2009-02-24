using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZSS;

namespace ImageUploaderExample
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<String> regexps = new List<string>();
            for (int i = 0; i <= 9; i++)
            {
                regexps.Add(">" + i.ToString() + ". Regexp<");
            }
            string str = textBox1.Text;
            ImageHostingService ih = new ImageHostingService();
            ih.Regexps = regexps;
            textBox2.AppendText("Syntax: " + str + "\r\n");
            string result = ih.ReturnLink(str);
            textBox2.AppendText("Last If-Else: " + ih.LastOperation + "\r\n");
            textBox2.AppendText("Result: " + result + "\r\n");
        }

        private void Test_Load(object sender, EventArgs e)
        {

        }
    }
}
