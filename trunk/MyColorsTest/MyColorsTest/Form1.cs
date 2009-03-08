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
        MyColor colorBox;
        MyColor colorSlider;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(Enum.GetNames(typeof(DrawStyle)));
            comboBox1.SelectedIndex = 0;
            colorBox1.ColorChanged += new ColorEventHandler(colorBox1_ColorChanged);
            colorSlider1.ColorChanged += new ColorEventHandler(colorSlider1_ColorChanged);
        }

        private void colorBox1_ColorChanged(object sender, ColorEventArgs e)
        {
            colorBox = e.Color;    
            if(e.UpdateControl) colorSlider1.SetColor = colorBox;
            txtColorBox.Text = colorBox.ToString();
            pictureBox1.BackColor = colorBox;
        }

        private void colorSlider1_ColorChanged(object sender, ColorEventArgs e)
        {
            colorSlider = colorSlider1.SetColor;
            if (e.UpdateControl) txtColorSlider.Text = colorSlider.ToString();
            colorBox1.SetColor = colorSlider;
            pictureBox2.BackColor = colorSlider;
        }

        private void CheckControl(MyColor color, TextBox textbox)
        {
            if (color.Color == MyColors.GetPixelColor(MousePosition))
            {
                textbox.BackColor = Color.LightGreen;
            }
            else
            {
                textbox.BackColor = Color.LightPink;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            colorBox1.DrawStyle = (DrawStyle)Enum.Parse(typeof(DrawStyle), comboBox1.SelectedItem.ToString());
            colorSlider1.DrawStyle = colorBox1.DrawStyle;
        }
    }
}