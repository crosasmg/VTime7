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

public partial class fasi_wmethods_dne : System.Web.UI.Page
{
    /// <summary>
    /// Retorna el provider para DNE
    /// </summary>
    [WebMethod(EnableSession = true)]
    public static string GetProvider()
    {
        return ConfigurationManager.AppSettings["DNEProvider"].ToString();
    }
    
    /// <summary>
    /// Retorna el endpoint del servicio de conexión de DNE
    /// </summary>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public static string GetDNEEndpoint()
    {
        return ConfigurationManager.AppSettings["DNE.URL"].ToString();
    }
}