using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UploadersLib.HelperClasses;

namespace UploadersLib
{
    public class ProxyConfig
    {
        public ProxyInfo ProxyActive = null;
        public EProxyConfigType ProxyConfigType = EProxyConfigType.NoProxy;
        public List<ProxyInfo> ProxyList = new List<ProxyInfo>();
        public int ProxySelected = 0;
    }
}