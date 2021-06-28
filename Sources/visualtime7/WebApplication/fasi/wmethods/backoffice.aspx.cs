using InMotionGIT.Membership.Providers;
using InMotionGIT.Seguridad.Proxy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using Thinktecture.IdentityModel.Client;
using System.Linq;

public partial class fasi_wmethods_BackOffice : System.Web.UI.Page
{
    /// <summary>
    /// Indica se el usuario está autenticado
    /// </summary>
    [WebMethod(EnableSession = true)]
    public static string MakeURL(string codispl)
    {
        string result = string.Empty;
        MemberContext userInfo = new MemberContext();
        using (InMotionGIT.FrontOffice.Proxy.MenuService.MenuClient client = new InMotionGIT.FrontOffice.Proxy.MenuService.MenuClient())
        {
            result = client.MakeURL(codispl, userInfo.CompanyId);
        }

        return result;
    }

}