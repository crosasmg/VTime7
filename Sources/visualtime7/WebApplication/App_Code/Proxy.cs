// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar

using System;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;

using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Net;
using System.IO;
using System.Net.Sockets;
using Dropthings.Widget.Widgets.RSS;

/// <summary>
/// Summary description for Proxy
/// </summary>

namespace Dropthings.Web.Framework
{
    [WebService(Namespace = "http://www.dropthings.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class Proxy : System.Web.Services.WebService
    {

        public Proxy()
        {
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public string GetString(string url, int cacheDuration)
        {
            return DashboardBusiness.Helpers.Proxy.GetString(url, cacheDuration,Context);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetXml(string url, int cacheDuration)
        {
            return GetString(url, cacheDuration);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public object GetRSS(string url, int count, int cacheDuration)
        {
            return DashboardBusiness.Helpers.Proxy.GetRSS(url,count, cacheDuration, Context);
        }
    }
}