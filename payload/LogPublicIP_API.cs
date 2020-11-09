using System;
using System.Collections.Generic;
using System.Net;

namespace InfoGather
{
    class LogPublicIP_API
    {
        public static IPAddress GetExternalIp()
        {
            using (WebClient client = new WebClient())
            {
                List<String> hosts = new List<String>
                {
                    "https://api.ipify.org",
                    "https://icanhazip.com",
                    "https://ipinfo.io/ip",
                    "https://wtfismyip.com/text",
                    "https://checkip.amazonaws.com/",
                    "https://bot.whatismyipaddress.com/",
                    "https://ipecho.net/plain"
                };
                foreach (String host in hosts)
                {
                    try
                    {
                        String ipAdressString = client.DownloadString(host);
                        ipAdressString = ipAdressString.Replace("\n", "");
                        return IPAddress.Parse(ipAdressString);
                    }
                    catch
                    {
                    }
                }
            }
            return null;
        }
    }
}
