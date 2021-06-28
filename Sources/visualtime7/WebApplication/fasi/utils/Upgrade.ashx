<%@ WebHandler Language="C#" Class="Upgrade" %>

using System;
using System.Web;
using InMotionGIT.Common.Extensions;

public class Upgrade : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string body = InMotionGIT.FASI.Support.Handlers.UpgradeInfoHandler.Version();
        if (body.IsNotEmpty())
        {
            context.Response.Write(body);
        }
        else
        {
            context.Response.Write("File not found");
        }
        context.Response.StatusCode = 200;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}