// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar

using System;
using System.Diagnostics;
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
using System.IO.Compression;
using System.Net.Sockets;

using AJAXASMXHandler;
using System.Text;
using System.Text.RegularExpressions;
using Dropthings.Widget.Widgets.RSS;
using System.Configuration;

namespace Dropthings.Web.Framework
{
    /// <summary>
    /// Summary description for Proxy
    /// </summary>
    [WebService(Namespace = "http://www.dropthings.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class ProxyAsync : System.Web.Services.WebService
    {
        private const string CACHE_KEY = "ProxyAsync.";

        public ProxyAsync()
        {
        }

        

        [ScriptMethod]
        public IAsyncResult BeginGetString(string url, int cacheDuration, AsyncCallback cb, object state)
        {
            return DashboardBusiness.Helpers.ProxyAsync.BeginGetString(url, cacheDuration, cb, state,Context);
                        
        }

        [ScriptMethod]
        public string EndGetString(IAsyncResult result)
        {
            return DashboardBusiness.Helpers.ProxyAsync.EndGetString(result);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public string GetString(string url, int cacheDuration)
        {
            return DashboardBusiness.Helpers.ProxyAsync.GetString(url,cacheDuration,Context);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetXml(string url, int cacheDuration)
        {
            return GetString(url, cacheDuration);
        }

        [ScriptMethod]
        public IAsyncResult BeginGetXml(string url, int cacheDuration, AsyncCallback cb, object state)
        {
            return BeginGetString(url, cacheDuration, cb, state);
        }

        [ScriptMethod]
        public string EndGetXml(IAsyncResult result)
        {
            return EndGetString(result);
        }
        
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]        
        public static bool IsUrlInCache(Cache cache, string url)
        {
            return (null != cache[CACHE_KEY + url]);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public object GetRSS(string url, int count, int cacheDuration)
        {
            return DashboardBusiness.Helpers.ProxyAsync.GetRSS(url, count, cacheDuration, Context);
        }        
    }

}