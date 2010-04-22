using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Starksoft.Net.Proxy;
using System.ComponentModel;

namespace UploadersLib.Helpers
{
    public enum ProxyConfigType
    {
        [Description("No proxy")]
        NoProxy,
        [Description("Manual proxy configuration")]
        ManualProxy,
        [Description("Use system proxy settings")]
        SystemProxy
    }

    public class ProxySettings
    {
        public ProxyConfigType ProxyConfig { get; set; }

        public ProxyInfo ProxyActive { get; set; }

        public IWebProxy GetWebProxy
        {
            get
            {
                switch (ProxyConfig)
                {
                    case ProxyConfigType.ManualProxy:
                        if (ProxyActive != null)
                        {
                            NetworkCredential credential = new NetworkCredential(ProxyActive.UserName, ProxyActive.Password);
                            return new WebProxy(ProxyActive.GetAddress(), true, null, credential);
                        }
                        break;
                    case ProxyConfigType.SystemProxy:
                        return HttpWebRequest.DefaultWebProxy;
                }
                return null;
            }
        }

        public IProxyClient GetProxyClient(ProxyInfo myProxyInfo)
        {

            if (myProxyInfo != null)
            {
                ProxyType proxyType;

                switch (myProxyInfo.ProxyType)
                {
                    case Proxy.HTTP:
                        proxyType = ProxyType.Http;
                        break;
                    case Proxy.SOCKS4:
                        proxyType = ProxyType.Socks4;
                        break;
                    case Proxy.SOCKS4a:
                        proxyType = ProxyType.Socks4a;
                        break;
                    case Proxy.SOCKS5:
                        proxyType = ProxyType.Socks5;
                        break;
                    default:
                        proxyType = ProxyType.None;
                        break;
                }

                ProxyClientFactory proxy = new ProxyClientFactory();

                return proxy.CreateProxyClient(proxyType, myProxyInfo.Host, myProxyInfo.Port, myProxyInfo.UserName, myProxyInfo.Password);
            }

            return null;
        }

        public IProxyClient GetProxyClient()
        {
            if (ProxyConfig != ProxyConfigType.NoProxy && ProxyActive != null)
            {
                return GetProxyClient(ProxyActive);
            }

            return null;
        }
    }
}