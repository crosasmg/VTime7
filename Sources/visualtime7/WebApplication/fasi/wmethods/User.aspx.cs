using InMotionGIT.Membership.Providers;
using System;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

public partial class fasi_wmethods_User : System.Web.UI.Page
{
    #region Fields

    private static InMotionGIT.Core.Configuration.FASIConfiguration _configurations = InMotionGIT.Core.Configuration.FASIConfiguration.Configuration();

    #endregion Fields

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object Operation(string operation, string body)
    {
        return InMotionGIT.FASI.Utils.Operations.Process.DoWork(operation, body);
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object Authorization(string[] Roles)
    {
        return InMotionGIT.FASI.Support.Authentication.AuthorizationProcess(Roles, false);
    }

    /// <summary>
    /// Permite mantener activa la sesión establecida en el browser
    /// </summary>
    /// <param name="IsFirst">Indica si es la primera vez que se usa la aplicación</param>
    /// <param name="SessionId">Identificador de la sesión registrada en el browser</param>
    /// <param name="TokenRenew">Indica que se debe renovar el token</param>
    /// <param name="Roles">Define los roles autorizados</param>
    /// <returns>Información del usuario y su sesión</returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object IsALive(bool IsFirst, string SessionId, bool TokenRenew, string[] Roles, string InMotionGITToken)
    {
        return InMotionGIT.FASI.Support.Authentication.IsALive(IsFirst, SessionId, TokenRenew, Roles, InMotionGITToken);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static object SpecialRoles()
    {
        return new { AnonymousRole = _configurations.Security.AnonymousRole, AdministratorRole = _configurations.Security.AdministratorRole };
    }

    // <summary>
    /// Indica se el usuario está autenticado
    /// </summary>
    [WebMethod(EnableSession = true)]
    public static object LanguageByCultureName(string languageName)
    {
        return InMotionGIT.FASI.Support.Handlers.LanguageHandler.LanguageByCultureName(languageName);
    }

    // <summary>
    /// Indica se el usuario está autenticado
    /// </summary>
    [WebMethod(EnableSession = true)]
    public static object LanguageSynchronization(int languageId, string languageName)
    {
        return InMotionGIT.FASI.Support.Handlers.LanguageHandler.Synchronization(ref languageId, ref languageName);
    }

    // <summary>
    /// Indica se el usuario está autenticado
    /// </summary>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static object SessionLive()
    {
        return HttpContext.Current.User.Identity.IsAuthenticated;
    }

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
        return InMotionGIT.FASI.Support.Authentication.GetUserInformation();
    }

    /// <summary>
    /// Desconecta el usuario
    /// </summary>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object LogOut()
    {
        return InMotionGIT.FASI.Support.Authentication.LogOut();
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
            var userRoles = roles.ToString().ToUpper().Split(',');
            if (userRoles.Contains("SUSCRIPTOR") || userRoles.Contains("DNEADMIN") || userRoles.Contains("DNEGET"))
            {
                return true;
            }
        }

        return false;
    }
}