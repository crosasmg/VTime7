using System.Configuration;
using System.Web.Services;

public partial class fasi_wmethods_address : System.Web.UI.Page
{    
    /// <summary>
    /// Retorna el endpoint del servicio de conexión de DNE
    /// </summary>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public static string GetAddressEndpoint()
    {
        return ConfigurationManager.AppSettings["Address.URL"].ToString();
    }
}