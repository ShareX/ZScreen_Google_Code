using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ZSS.Helpers
{
    [Serializable]
    public class ProxyInfo : NetworkCredential
    {
        public ProxyInfo(string userName, string password, string domain)
        {
            this.UserName = userName;
            this.Password = password;
            this.Domain = domain;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", this.UserName, this.Domain);
        }
    }
}
