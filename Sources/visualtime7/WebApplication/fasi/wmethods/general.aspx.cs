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

public partial class fasi_wmethods_General : System.Web.UI.Page
{
    #region Fields

    public struct UserData
    {
        public string username { get; set; }
        public int userId { get; set; }
        public int companyId { get; set; }
        public string schemeCode { get; set; }
        public bool isAnonymous { get; set; }
        public string token { get; set; }
        public string clientId { get; set; }
        public int producerId { get; set; }
        public string firstNameAndSecondLastName { get; set; }
        public int languageID { get; set; }
        public string type { get; set; }

        public string languageName { get; set; }

        public string utcOffset { get; set; }

    }

    #endregion Fields

    // <summary>
    /// Indica se el usuario está autenticado
    /// </summary>
    [WebMethod(EnableSession = true)]
    public static object Error(string Message, string Url, string Line, string Column)
    {
        string key = Guid.NewGuid().ToString();
        InMotionGIT.Common.Helpers.LogHandler.ErrorLog(string.Format("Key:'{0}'- FASI Client Side", key), string.Format("UnexpectedError: Message='{0}, Url='{1}', Line='{2}', Column='{3}'", Message, Url, Line, Column)); 
        return key;
    }

    // <summary>
    /// Permite recuperar un valor de los settings del webapplication.
    /// </summary>
    [WebMethod(EnableSession = true)]
    public static object SettingValue(string name)
    {
        return ConfigurationManager.AppSettings[name].ToString();
    }
}