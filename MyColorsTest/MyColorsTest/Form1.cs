using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZSS.Colors;

namespace MyColorsTest
{
    public partial class Form1 : Form
    {
        MyColors.MyColor testColor;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(Enum.GetNames(typeof(ColorBox.eDrawStyle)));
            comboBox1.SelectedIndex = 0;
            colorBox1.ColorChanged += new EventHandler(colorBox1_ColorChanged); 
            //timer1.Start();
        }

        private void colorBox1_ColorChanged(object sender, EventArgs e)
        {
            txtTest.Text = colorBox1.MyColor2.ToString();
            testColor = MyColors.GetPixelColor(MousePosition);
            txtTest.AppendText("\r\n" + (colorBox1.MyColor2 == testColor).ToString());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            colorBox1.DrawStyle = (ColorBox.eDrawStyle)Enum.Parse(typeof(ColorBox.eDrawStyle), comboBox1.SelectedItem.ToString());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}