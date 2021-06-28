using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fasi_security_Register :  System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["FASI.HTML5.Security.UserRegistrationPage"]);
    }
}