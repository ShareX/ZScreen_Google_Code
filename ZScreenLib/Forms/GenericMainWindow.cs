using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZScreenLib
{
    public partial class GenericMainWindow : Form
    {
        public GenericMainWindow()
        {
            InitializeComponent();
        }

        public NotifyIcon niTray = new NotifyIcon();
    }
}
