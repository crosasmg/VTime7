<%@ WebHandler Language="C#" Class="useractivity" %>

using System;
using System.Web;
using InMotionGIT.Common.Extensions;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Linq;
using System.Web.SessionState;

public class useractivity : IHttpHandler, IReadOnlySessionState
{

    public void ProcessRequest(HttpContext context)
    {
        InMotionGIT.FASI.Utils.Track.ShowActivity.ProcessRequest(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}