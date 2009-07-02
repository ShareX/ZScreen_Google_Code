using System;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ZSS
{
    [Serializable()]
    public class DekiWikiAccount
    {
        public string Name     { get; set; }
        public string Url      { get; set; }
        public string Username { get; set; }
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
    }
}