using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FASI : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["updateInfo"] == null)
            HttpContext.Current.Session["updateInfo"] = InMotionGIT.FASI.Support.Handlers.UpgradeInfoHandler.Version();
        if (System.Web.HttpContext.Current.Session["PasswordExpiration"]  != null && 
            ! HttpContext.Current.Request.Url.PathAndQuery.ToLower().Contains("dli/forms/passwordchange.aspx") && 
                ! HttpContext.Current.Request.Url.PathAndQuery.ToLower().Contains("passwordexpiration"))
        {
            System.Web.HttpContext.Current.Session.Remove("PasswordExpiration");
            Response.Redirect("/fasi/security/logoff.ashx");
        }
        if(System.Configuration.ConfigurationManager.AppSettings["FASI.UI.TopRightText"] !=null)
            TopRightText.InnerHtml = System.Configuration.ConfigurationManager.AppSettings["FASI.UI.TopRightText"];
    }
}
