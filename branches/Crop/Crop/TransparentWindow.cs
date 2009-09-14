using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;

namespace TransparentWindowLibrary
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TransparentWindowLibrary"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TransparentWindowLibrary;assembly=TransparentWindowLibrary"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class TransparentWindow : Window
    {
        public TransparentWindow() { }

        static TransparentWindow()
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            dictionary.Source = new Uri("/Crop;component/Themes/Generic.xaml", UriKind.Relative);
            _style = (Style)dictionary[typeof(TransparentWindow)];
            StyleProperty.OverrideMetadata(typeof(TransparentWindow), new FrameworkPropertyMetadata(null, new CoerceValueCallback(OnCoerceStyle)));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransparentWindow), new FrameworkPropertyMetadata(typeof(TransparentWindow)));

            EventManager.RegisterClassHandler(typeof(TransparentWindow), Thumb.DragDeltaEvent, new DragDeltaEventHandler(OnDragDelta));

        }

        private static Style _style;

        private static object OnCoerceStyle(DependencyObject o, object value)
        {
            if (value == null)
            {
                value = _style;
            }
            return value;
        }

        private static void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            TransparentWindow transparentWindow = (TransparentWindow)sender;
            Thumb thumb = e.OriginalSource as Thumb;
            if (thumb != null && transparentWindow.WindowState == WindowState.Normal)
            {
                Rect windowRect = new Rect(transparentWindow.Left, transparentWindow.Top, transparentWindow.ActualWidth, transparentWindow.ActualHeight);
                double maxReducedHeight = Math.Max(0, transparentWindow.ActualHeight - transparentWindow.MinHeight);
                double maxReducedWidth = Math.Max(0, transparentWindow.ActualWidth - transparentWindow.MinWidth);
                double reducedHeight = e.VerticalChange;
                double reducedWidth = e.HorizontalChange;
                if (thumb.Name.Equals("PART_SizeSE"))
                {
                    reducedHeight = Math.Max(reducedHeight, -maxReducedHeight);
                    reducedWidth = Math.Max(reducedWidth, -maxReducedWidth);
                    transparentWindow.Width = Math.Max(transparentWindow.ActualWidth + reducedWidth, transparentWindow.MinWidth);
                    transparentWindow.Height = Math.Max(transparentWindow.ActualHeight + reducedHeight, transparentWindow.MinHeight);
                }
                else if (thumb.Name.Equals("PART_SizeNW"))
                {
                    reducedWidth = Math.Min(reducedWidth, maxReducedWidth);
                    reducedHeight = Math.Min(reducedHeight, maxReducedHeight);
                    windowRect.Y += reducedHeight;
                    windowRect.X += reducedWidth;
                    windowRect.Width -= reducedWidth;
                    windowRect.Height -= reducedHeight;
                    transparentWindow.SetWindowVisualRect(windowRect);
                }
                else if (thumb.Name.Equals("PART_SizeSW"))
                {
                    reducedHeight = Math.Max(reducedHeight, -maxReducedHeight);
                    reducedWidth = Math.Min(reducedWidth, maxReducedWidth);
                    windowRect.X += reducedWidth;
                    windowRect.Width -= reducedWidth;
                    windowRect.Height += reducedHeight;
                    transparentWindow.SetWindowVisualRect(windowRect);
                }
                else if (thumb.Name.Equals("PART_SizeNE"))
                {
                    reducedHeight = Math.Min(reducedHeight, maxReducedHeight);
                    reducedWidth = Math.Max(reducedWidth, -maxReducedWidth);
                    windowRect.Y += reducedHeight;
                    windowRect.Height = transparentWindow.ActualHeight - reducedHeight;
                    windowRect.Width = transparentWindow.ActualWidth + reducedWidth;
                    transparentWindow.SetWindowVisualRect(windowRect);
                }
                else if (thumb.Name.Equals("PART_SizeN"))
                {
                    reducedHeight = Math.Min(reducedHeight, maxReducedHeight);
                    windowRect.Y += reducedHeight;
                    windowRect.Height = transparentWindow.ActualHeight - reducedHeight;
                    transparentWindow.SetWindowVisualRect(windowRect);
                }
                else if (thumb.Name.Equals("PART_SizeS"))
                {
                    reducedHeight = Math.Max(reducedHeight, -maxReducedHeight);
                    transparentWindow.Height = transparentWindow.ActualHeight + reducedHeight;
                }
                else if (thumb.Name.Equals("PART_SizeW"))
                {
                    reducedWidth = Math.Min(reducedWidth, maxReducedWidth);
                    windowRect.X += reducedWidth;
                    windowRect.Width = transparentWindow.ActualWidth - reducedWidth;
                    transparentWindow.SetWindowVisualRect(windowRect);
                }
                else if (thumb.Name.Equals("PART_SizeE"))
                {
                    reducedWidth = Math.Max(reducedWidth, -maxReducedWidth);
                    transparentWindow.Width = transparentWindow.ActualWidth + reducedWidth;
                }
            }
        }

        private void SetWindowVisualRect(Rect rect)
        {
            IntPtr mainWindowPtr = new WindowInteropHelper(this).Handle;
            HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
            Point deviceTopLeft = mainWindowSrc.CompositionTarget.TransformToDevice.Transform(rect.TopLeft);
            Point deviceBottomRight = mainWindowSrc.CompositionTarget.TransformToDevice.Transform(rect.BottomRight);
            NativeMethods.SetWindowPos(mainWindowSrc.Handle,
                                       IntPtr.Zero,
                                       (int)(deviceTopLeft.X),
                                       (int)(deviceTopLeft.Y),
                                       (int)(Math.Abs(deviceBottomRight.X - deviceTopLeft.X)),
                                       (int)(Math.Abs(deviceBottomRight.Y - deviceTopLeft.Y)),
                                       0);
        }
    }
}