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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(Enum.GetNames(typeof(DrawStyle)));
            comboBox1.SelectedIndex = 0;
            colorPicker.ColorChanged += new ColorEventHandler(colorPicker_ColorChanged);
        }

        private void colorPicker_ColorChanged(object sender, ColorEventArgs e)
        {
            txtColor.Text = e.Color.ToString();
            pbColor.BackColor = e.Color;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            colorPicker.DrawStyle = (DrawStyle)Enum.Parse(typeof(DrawStyle), comboBox1.SelectedItem.ToString());
        }
    }
}