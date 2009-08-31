using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Starksoft.Net.Proxy;

namespace UploadersLib.Helpers
{
    public class ProxySettings
    {
        public bool ProxyEnabled { get; set; }

        public ProxyInfo ProxyActive { get; set; }

        public IWebProxy GetWebProxy
        {
            get
            {
                if (ProxyEnabled)
                {
                    if (ProxyActive != null)
                    {
                        NetworkCredential credential = new NetworkCredential(ProxyActive.UserName, ProxyActive.Password);
                        return new WebProxy(ProxyActive.GetAddress(), true, null, credential);
                    }
                    else
                    {
                        return HttpWebRequest.DefaultWebProxy;
                    }
                }

                return null;
            }
        }

        public IProxyClient GetProxyClient
        {
            get
            {
                if (ProxyEnabled && ProxyActive != null)
                {
                    ProxyType proxyType;

                    switch (ProxyActive.ProxyType)
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

                    return proxy.CreateProxyClient(proxyType, ProxyActive.Host, ProxyActive.Port, ProxyActive.UserName, ProxyActive.Password);
                }

                return null;
            }
        }
    }
}