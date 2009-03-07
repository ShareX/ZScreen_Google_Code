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
            comboBox1.Items.AddRange(Enum.GetNames(typeof(DrawStyle)));
            comboBox1.SelectedIndex = 0;
            colorBox1.ColorChanged += new EventHandler(colorBox1_ColorChanged); 
        }

        private void colorBox1_ColorChanged(object sender, EventArgs e)
        {
            txtTest.Text = colorBox1.GetColor.ToString();
            testColor = MyColors.GetPixelColor(MousePosition);
            txtTest.AppendText("\r\n" + (colorBox1.GetColor == testColor).ToString());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            colorBox1.DrawStyle = (DrawStyle)Enum.Parse(typeof(DrawStyle), comboBox1.SelectedItem.ToString());
            colorSlider1.DrawStyle = colorBox1.DrawStyle;
        }
    }
}