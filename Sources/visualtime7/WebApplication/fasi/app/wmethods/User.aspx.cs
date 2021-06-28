using InMotionGIT.Common.Extensions;
using InMotionGIT.Core.Configuration;
using InMotionGIT.Membership.Providers;
using InMotionGIT.Seguridad.Proxy;
using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using Thinktecture.IdentityModel.Client;

public partial class fasi_wmethods_User : System.Web.UI.Page
{
    /// <summary>
    /// Indica se el usuario está autenticado
    /// </summary>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static object IsAuthenticated()
    {
        return !new MemberContext().IsAnonymous;
    }

    /// <summary>
    /// Obtiene informaciones del usuario autenticado
    /// </summary>
    /// <returns>Código del usuario se está autenticado o -1 si no</returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static object GetUserInformation()
    {
        object user = null;

        VisualTIME config = (VisualTIME)ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection");

        try
        {
            var token = string.Empty;

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsIdentity valueToken = (FormsIdentity)HttpContext.Current.User.Identity;

                InMotionGIT.FASI.Contracts.Security.UserProfile dataProfile = null;

                if (valueToken.IsNotEmpty())
                {
                    if (valueToken.Ticket.IsNotEmpty() && valueToken.Ticket.UserData.IsNotEmpty())
                    {
                        dataProfile = InMotionGIT.Common.Helpers.Serialize.DeserializeJSON<InMotionGIT.FASI.Contracts.Security.UserProfile>(valueToken.Ticket.UserData);
                    }
                }
                // Si no existe token lo solicita
                if (HttpContext.Current.Session["TokenResponse"] == null || ((TokenResponse)HttpContext.Current.Session["TokenResponse"]).AccessToken == null)
                {
                    TokenHelper.RequestToken(dataProfile.Email);
                }
                else
                {
                    TokenHelper.GetValidToken();
                }
                token = ((TokenResponse)HttpContext.Current.Session["TokenResponse"]).AccessToken;
                if (token.IsEmpty())
                {
                    token = "";
                }

                user = new
                {
                    username = dataProfile.UserName,
                    userId = dataProfile.UserId,
                    companyId = dataProfile.CompanyId,
                    schemeCode = HttpContext.Current.Session["sSche_Code"],
                    isAnonymous = false,
                    token = token,
                    clientId = dataProfile.ClientId,
                    producerId = dataProfile.ProducerId,
                    firstNameAndSecondLastName = dataProfile.FirstNameAndSecondLastName,
                    type = dataProfile.Type,
                    languageID = dataProfile.LanguageID,
                    languageName = dataProfile.LanguageName
                };
            }
            else
            {
                // Si no existe token lo solicita
                if (HttpContext.Current.Session["AnonymousTokenResponse"] == null || ((TokenResponse)HttpContext.Current.Session["AnonymousTokenResponse"]).AccessToken == null)
                    TokenHelper.RequestAnonymousToken();
                else
                    TokenHelper.GetAnonymousToken();

                token = ((TokenResponse)HttpContext.Current.Session["AnonymousTokenResponse"]).AccessToken;
                if (token.IsEmpty())
                {
                    token = "";
                }

                user = new
                {
                    // Para usuario anónimo devuelve el código del usuario igual a -1
                    userId = -1,
                    companyId = -1,
                    schemeCode = HttpContext.Current.Session["sSche_Code"],
                    isAnonymous = true,
                    token = token
                };
            }
        }
        catch (Exception ex)
        {
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog("FASI-GetUserInformation", ex.Message, ex);
            user = new
            {
                // Para usuario anónimo devuelve el código del usuario igual a -1
                userId = -1,
                companyId = -1,
                schemeCode = "",
                isAnonymous = true,
                token = ""
            };
        }

        return user;
    }

    /// <summary>
    /// Desconecta el usuario
    /// </summary>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object LogOut()
    {
       return  InMotionGIT.FASI.Support.Authentication.LogOut();
    }

    /// <summary>
    /// Método que proporciona el auto login
    /// </summary>
    /// <param name="Token">Token del usuario</param>
    /// <param name="LanguageId">Id del lenguaje</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Object AutoLogin(string Token, int LanguageId)
    {
        return InMotionGIT.FASI.Support.Authentication.AutoLoginByToken(Token, LanguageId);
    }

    /// <summary>
    /// Método que proporciona el auto login
    /// </summary>
    /// <param name="Token">Token del usuario</param>
    /// <param name="UserId">Id del current id</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Object UserCheckEquals(string Token, int UserId)
    {
        return InMotionGIT.FASI.Support.Authentication.UserCheckEquals(Token, UserId);
    }

    [WebMethod(EnableSession = true)]
    public static bool IsUserSuscriptor()
    {
        var roles = HttpContext.Current.Session["UserRoles"];
        if (roles != null && !roles.Equals(""))
        {
            if (roles.ToString().Split(';').Contains("suscriptor"))
            {
                return true;
            }
        }

        return false;
    }
}