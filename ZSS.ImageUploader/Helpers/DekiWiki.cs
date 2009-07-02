using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace ZSS
{
    public class DekiWiki
    {
        public class Page
        {
            public string id       { set; get; }
            public string path     { set; get; }
            public string name     { set; get; }
            public bool   terminal { set; get; }
            public List<Page> children;

            public Page()
            {
                children = new List<Page>();
            }

            public Page(string id, string path, string name, bool terminal)
            {
                children = new List<Page>();

                this.id       = id;
                this.path     = path;
                this.name     = name;
                this.terminal = terminal;
            }

            public Page(DekiWikiHistory history)
            {
                this.path = history.Path;
            }
        }

        private DekiWikiAccount mAccount;
        private string authToken;

        public static string savePath;

        public DekiWiki()
        {
            mAccount = new DekiWikiAccount();
        }

        public DekiWiki(ref DekiWikiAccount acc)
        {
            mAccount = acc;
        }

        public void UploadImage(string fileName, string remoteName)
        {
            // Log in
            Login();

            // Create the Uri
            string uri = mAccount.Url;
            uri += "/@api/deki/pages/=";
            uri += HttpUtility.UrlEncode(HttpUtility.UrlEncode(savePath));
            uri += "/files/=" + HttpUtility.UrlEncode(HttpUtility.UrlEncode(remoteName));

            // Create the request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            // Get the image's file info
            FileInfo fileInfo = new FileInfo(fileName);

            // Set some request properties
            request.Method = WebRequestMethods.Http.Put;
            request.ContentLength = fileInfo.Length;
            request.ContentType = "image/png";
            request.Headers["Cookie"] = "authtoken=\"" + authToken + "\"";

            // Get the web request stream
            Stream stream = request.GetRequestStream();

            // Open the file
            FileStream fileSystem = fileInfo.OpenRead();

            // Read the file into the request stream
            int contentLength;
            int bufferSize = 2 * 1024;
            byte[] buffer = new byte[bufferSize];
            while ((contentLength = fileSystem.Read(buffer, 0, bufferSize)) != 0)
            {
                stream.Write(buffer, 0, contentLength);
            }

            // Close the request stream
            stream.Close();

            // Close the file
            fileSystem.Close();

            // Get the response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Check whether the operation was successful
            if (response.StatusCode.ToString() != "OK")
            {
                response.Close();
                throw new Exception("Could not upload image to Mindtouch.");
            }

            response.Close();
        }

        public void UpdateHistory()
        {
            List<DekiWikiHistory> remove = new List<DekiWikiHistory>();

            foreach (DekiWikiHistory item in mAccount.History)
            {
                if (item.Path == savePath)
                {
                    remove.Add(item);
                }
            }

            foreach (DekiWikiHistory item in remove)
            {
                mAccount.History.Remove(item);
            }

            if (mAccount.History.Count == 10)
            {
                mAccount.History.RemoveAt(0);
            }

            mAccount.History.Add(new DekiWikiHistory(savePath, DateTime.Now));
        }

        public Page getPageInfo(string path)
        {
            // Create the URI
            string uri = mAccount.Url + "/@api/deki/pages/=" + HttpUtility.UrlEncode(HttpUtility.UrlEncode(path)) + "/info";

            // Create the request
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(uri);

            // We want a GET request
            httpRequest.Method = WebRequestMethods.Http.Get;

            // Get a response
            HttpWebResponse httpResponse;
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (Exception ex)
            {
                throw new Exception("404: " + ex.Message);
            }

            // Check the code
            if (httpResponse.StatusCode.ToString() != "OK")
            {
                httpResponse.Close();
                throw new Exception("Unable to retrieve path information.");
            }

            // Get the server response
            StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream());

            // Get the XML data
            string xmlData = streamReader.ReadToEnd();

            // Close the response stream
            streamReader.Close();

            // Create the XML doc
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlData);

            XmlElement element = xmlDocument.DocumentElement;

            Page page = new Page();
            page.id = element.Attributes["id"].Value;

            foreach (XmlNode child in element.ChildNodes)
            {
                switch (child.Name)
                {
                    case "title":
                        page.name = child.InnerText;
                        break;
                    case "path":
                        page.path = child.InnerText;
                        break;
                }
            }

            // Create the page list
            return page;
        }

        public List<Page> getPathInfo(string path)
        {
            // Create the URI
            string uri = mAccount.Url + "/@api/deki/pages/=" + HttpUtility.UrlEncode(HttpUtility.UrlEncode(path)) + "/tree";

            // Create the request
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(uri);

            // We want a GET request
            httpRequest.Method = WebRequestMethods.Http.Get;

            // Get a response
            HttpWebResponse httpResponse;
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (Exception ex)
            {
                throw new Exception("404: " + ex.Message);
            }

            // Check the code
            if (httpResponse.StatusCode.ToString() != "OK")
            {
                httpResponse.Close();
                throw new Exception("Unable to retrieve path information.");
            }

            // Get the server response
            StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream());

            // Get the XML data
            string xmlData = streamReader.ReadToEnd();

            // Close the response stream
            streamReader.Close();

            // Create the XML doc
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlData);

            // Create the page list
            return parseXml(xmlDocument.DocumentElement);
       }

        private List<Page> parseXml(XmlNode parent)
        {
            List<Page> pages = new List<Page>();

            foreach (XmlElement element in parent.SelectNodes("page"))
            {
                Page page = new Page();

                page.id = element.Attributes["id"].Value;

                foreach (XmlNode child in element.ChildNodes)
                {
                    switch (child.Name)
                    {
                        case "title":
                            page.name = child.InnerText;
                            break;
                        case "path":
                            page.path = child.InnerText;
                            break;
                    }
                }


                page.children = parseXml(element.SelectSingleNode("subpages"));

                pages.Add(page);
            }

            return pages;
        }

        public void Login()
        {
            // Build the URI
            string uri = mAccount.Url + "/@api/deki/users/authenticate";

            // Create the request
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(uri);

            // Set some properties
            httpRequest.Method = WebRequestMethods.Http.Post;
            httpRequest.ContentType = "Content-type: application/x-www-form-urlencoded";
            httpRequest.ContentLength = 0;

            // Encode the auth token
            byte[] authBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(mAccount.Username + ":" + mAccount.Password);
            string authEnc = System.Convert.ToBase64String(authBytes);
            httpRequest.Headers["Authorization"] = "Basic " + authEnc;

            // Send the request
            httpRequest.GetRequestStream().Close();

            // Get the response
            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

            // Check the code
            if (httpResponse.StatusCode.ToString() != "OK")
            {
                httpResponse.Close();
                throw new Exception("Unable to log in to DekiWiki with the provided user name and password.");
            }

            // Parse out the auth token
            string token = ParseAuthToken(httpResponse.Headers["Set-Cookie"]);

            // Set the auto token
            authToken = token;

            // Close the response
            httpResponse.Close();
        }

        private string ParseAuthToken(string cookie)
        {
            int index;

            index = cookie.IndexOf("authtoken=");
            if (index < 0)
            {
                throw new Exception("Could not parse authtoken");
            }
            cookie = cookie.Substring(index + 11);

            index = cookie.IndexOf('"');
            if (index <= 0)
            {
                throw new Exception("Could not parse authtoken");
            }

            cookie = cookie.Substring(0, index);

            return cookie;
        }
    }
}