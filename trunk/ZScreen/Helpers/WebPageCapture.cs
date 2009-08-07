using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZScreenLib
{
    public class WebPageCapture : IDisposable
    {
        public event ImageEventHandler DownloadCompleted;
        public delegate void ImageEventHandler(Image image);
        
        public Size BrowserSize { get; set; }
        public string URL { get; set; }
        public Image Image { get; private set; }

        private WebBrowser webBrowser = new WebBrowser();

        public WebPageCapture() : this(Screen.PrimaryScreen.Bounds.Size) { }

        public WebPageCapture(int width, int height) : this(new Size(width, height)) { }

        public WebPageCapture(Size browserSize)
        {
            BrowserSize = browserSize;
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);
            webBrowser.ScrollBarsEnabled = false;
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Rectangle rect = webBrowser.Document.ActiveElement.ScrollRectangle;
            webBrowser.Size = new Size(rect.Width, rect.Height);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);

            try
            {
                webBrowser.DrawToBitmap(bmp, rect);
                this.Image = bmp;
            }
            finally
            {
                if (DownloadCompleted != null) DownloadCompleted(bmp);
            }
        }

        public void DownloadPage()
        {
            if (!string.IsNullOrEmpty(this.URL))
            {
                DownloadPage(this.URL);
            }
        }

        public void DownloadPage(string url)
        {
            this.URL = url;
            webBrowser.Size = BrowserSize;
            webBrowser.Navigate(url);
        }

        public void Dispose()
        {
            this.Image.Dispose();
            this.webBrowser.Dispose();
        }
    }
}