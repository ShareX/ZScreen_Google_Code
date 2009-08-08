#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
   
    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/
#endregion

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