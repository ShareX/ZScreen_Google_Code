using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TransparentWindowLibrary;

namespace Crop
{
    public partial class CropWPF : TransparentWindow
    {
        public Point CropPosition { get; set; }
        public Size CropSize { get; set; }

        public CropWPF()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, new ExecutedRoutedEventHandler((x, x2) => OnClose(false))));
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                OnClose(true);
            }
            else
            {
                this.DragMove();
            }
        }

        private void Border_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            OnClose(false);
        }

        private void OnClose(bool result)
        {
            if (this.DialogResult != null)
            {
                this.DialogResult = result;
            }

            this.Close();
        }

        private void Border_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.CropSize = new Size(CropArea.ActualWidth, CropArea.ActualHeight);
            lblWidth.Text = "Width: " + this.CropSize.Width;
            lblHeight.Text = "Height: " + this.CropSize.Height;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            this.CropPosition = CropArea.PointToScreen(new Point(0, 0));
            lblX.Text = "X: " + this.CropPosition.X;
            lblY.Text = "Y: " + this.CropPosition.Y;
        }
    }
}