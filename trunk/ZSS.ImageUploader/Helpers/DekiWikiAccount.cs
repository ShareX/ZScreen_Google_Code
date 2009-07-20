using System;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;

namespace ZSS
{
    [Serializable()]
    public class DekiWikiAccount
    {
        [Category("MindTouch")]
        public string Name { get; set; }
        [Category("MindTouch")]
        public string Url { get; set; }
        [Category("MindTouch")]
        public string Username { get; set; }
        [Category("MindTouch"), PasswordPropertyText(true)]
        public string Password { get; set; }

        public List<DekiWikiHistory> History = new List<DekiWikiHistory>();

        public DekiWikiAccount() { }

        public DekiWikiAccount(string name)
        {
            this.Name = name;
        }

        public string getUriPath(string fileName)
        {
            // Create the Uri
            return Url + "/@api/deki/pages/=" + HttpUtility.UrlEncode(HttpUtility.UrlEncode(DekiWiki.savePath)) + "/files/=" + HttpUtility.UrlEncode(HttpUtility.UrlEncode(fileName));
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}