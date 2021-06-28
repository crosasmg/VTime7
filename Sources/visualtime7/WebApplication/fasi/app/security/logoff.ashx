<%@ WebHandler Language="C#" Class="logoff" %>

using System;
using System.Web;
using InMotionGIT.Common.Extensions;

public class logoff : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        if (System.Configuration.ConfigurationManager.AppSettings["FASI.Security.Logoff.Redirect"].IsEmpty())
        {
            InMotionGIT.FASI.Support.Authentication.LogOut();
            if (context.Request.QueryString.Count > 0 && context.Request.QueryString.Get(0).ToString().Contains("login"))
                context.Server.Transfer("logIn.aspx");
            else
                context.Server.Transfer("../default.aspx");
        }else
        {
            context.Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["FASI.Security.Logoff.Redirect"]);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}