<%@ Application Language="C#" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.Reflection" %>
<%@ Import Namespace="System.Security.Principal" %>
<%@ Import Namespace="InMotionGIT.Core.Configuration" %>
<%@ Import Namespace="InMotionGIT.Common.Extensions" %>
<%@ Import Namespace="InMotionGIT.FASI.Support" %>


<script RunAt="server">

    VisualTIME config = (VisualTIME)ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection");

    public override void Init()
    {
        this.PostAuthenticateRequest += Application_PostAuthenticateRequest;
        base.Init();
    }

    void Application_PostAuthenticateRequest(object sender, EventArgs e)
    {
        System.Web.HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
    }

    /// <summary>
    /// Se le llama cuando se solicita el primer recurso de la aplicación. Sólo se llama una vez durante el ciclo de vida de la aplicación.
    /// </summary>
    void Application_Start(object sender, EventArgs e)
    {
        if (config.General.EnabledFrontOffice)
        {
            InMotionGIT.Common.Helpers.LogHandler.TraceLog("Global.asax", "Application Start");
        }

        Application.Lock();
        Application.Add("upgradeInfo", string.Format("Upgrade {0}", InMotionGIT.FASI.Support.Handlers.UpgradeInfoHandler.Version()));
        Application.UnLock();
        //if (System.Configuration.ConfigurationManager.AppSettings["Workflow.Settings.AutomaticResume.Enabled"].ToString() == "True")
        //    InMotionGIT.Workflow.Support.Runtime.AutomaticResume();
        InMotionGIT.FASI.Utils.Track.UserActivity.UserOffLineAll();


        //RouteTable.Routes.MapHttpRoute(
        //                                name: "DefaultApi",
        //                                routeTemplate: "api/{controller}/{id}",
        //                                defaults: new { id = System.Web.Http.RouteParameter.Optional }
        //                            );
    }

    /// <summary>
    /// Se le llama una vez durante el período de duración de la aplicación antes de que ésta se detenga.
    /// </summary>
    void Application_End(object sender, EventArgs e)
    {
        if (config.General.EnabledFrontOffice)
            InMotionGIT.Common.Helpers.LogHandler.TraceLog("Global.asax", "Application End");
    }

    public string SessionIdFromCookie()
    {
        string result = string.Empty;
        if (Request.Headers["Cookie"].IndexOf("ASP.NET_SessionId") >= 0)
        {
            foreach (string item in Request.Headers["Cookie"].Split(';'))
            {
                if (item.Contains("ASP.NET_SessionId"))
                {
                    result = item.Split('=')[1];
                }
            }
        }

        return result;
    }


    /// <summary>
    /// Se produce cuando un módulo de seguridad ha establecido la identidad del usuario.
    /// </summary>
    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        if (HttpContext.Current.User != null)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.Identity is FormsIdentity)
                {
                    var sessionId = SessionIdFromCookie();
                    InMotionGIT.FASI.Utils.Track.UserActivity.Tracking(HttpContext.Current, sessionId);

                    FormsIdentity id =
                        (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;

                    // Get the stored user-data, in this case, our roles

                    string userData = ticket.UserData;
                    string[] roles = userData.Split(',');
                    HttpContext.Current.User = new GenericPrincipal(id, roles);
                }
            }
        }
    }

    /// <summary>
    /// Se puede provocar en cualquier fase del ciclo de vida de la aplicación.
    /// </summary>
    void Application_Error(object sender, EventArgs e)
    {
        try
        {
            if (config.General.EnabledFrontOffice)
            {
                InMotionGIT.Common.Helpers.LogHandler.ErrorLog("Global.asax", "Application Error", Server.GetLastError());
            }
            if (System.Configuration.ConfigurationManager.AppSettings["ErrorHandler"].ToString().IsNotEmpty() &&
                System.Configuration.ConfigurationManager.AppSettings["ErrorHandler"].ToString().ToLower().Equals("true"))
            {
                Exception exc = Server.GetLastError();
                HttpContext.Current.ClearError();

                String detail = string.Empty;

                if (exc.InnerException == null)
                {
                    detail = InMotionGIT.Common.Helpers.LogHandler.GetMessage(exc);
                }
                else
                    detail = InMotionGIT.Common.Helpers.LogHandler.GetMessage(exc.InnerException);

                if (detail.IsEmpty())
                {
                    detail = exc.Message;
                }
                string key = Guid.NewGuid().ToString();
                HttpContext.Current.Application.UnLock();
                HttpContext.Current.Application.Add(key, detail);
                HttpContext.Current.Application.Lock();

                HttpContext.Current.Response.Redirect(string.Format("~/dropthings/ErrorHandler.html?detail={0}", key), false);
            }

        }
        catch (Exception ex)
        {
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog("Global.asax", "Application Error", ex);
        }
    }

    /// <summary>
    /// Se provoca cuando se inicia una nueva sesión.
    /// </summary>
    void Session_Start(object sender, EventArgs e)
    {
        Session["CompanyId"] = ConfigurationManager.AppSettings["BackOffice.CompanyDefault"];
        Session["IsFirtView"] = false;


        if (Request.QueryString["SessionTimeOut"] != "Yes")
        {
            if (Context.Session != null)
            {
                if (Session.IsNewSession)
                {
                    var szCookieHeader = Request.Headers["Cookie"];
                    if (szCookieHeader != null)
                    {
                        if (szCookieHeader.IndexOf("ASP.NET_SessionId") >= 0 && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                        {
                            if (Request.AppRelativeCurrentExecutionFilePath.ToLower().Contains("/fasi"))
                            {
                                InMotionGIT.FASI.Support.Authentication.LogOut();
                                Response.Redirect("~/fasi/Default.aspx?SessionTimeOut=Yes");
                            }
                            else
                            {
                                FormsAuthentication.SignOut();
                                Response.Redirect("~/dropthings/Default.aspx?SessionTimeOut=Yes");
                            }

                        }
                    }
                }
            }
        }

        try
        {
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated && (Request.AppRelativeCurrentExecutionFilePath.ToLower().Contains("/generated") || Request.AppRelativeCurrentExecutionFilePath.ToLower().Contains("/dropthings")))
            {
                InMotionGIT.Common.Helpers.LogHandler.TraceLog("Global IsAuthenticated", "IsAuthenticated");
                var message = "";
                if (InMotionGIT.FrontOffice.Proxy.Helpers.Authentication.ValidateAcccesByToken(true, ref message))
                {
                    new InMotionGIT.Membership.Providers.MemberContext(true);
                    InMotionGIT.Common.Helpers.LogHandler.TraceLog("Global MemberContext", "MemberContext");
                }
            }
        }
        catch (Exception ex)
        {

            InMotionGIT.Common.Helpers.LogHandler.ErrorLog("Global.asax", "Error by Token", ex);
        }

    }

    /// <summary>
    /// Se provoca cuando una sesión se abandona o expira.
    /// </summary>
    void Session_End(object sender, EventArgs e)
    {
        InMotionGIT.FASI.Utils.Track.UserActivity.Finish(this.Session.SessionID);
    }

</script>
