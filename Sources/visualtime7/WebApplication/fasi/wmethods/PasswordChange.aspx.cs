using InMotionGIT.Common.Extensions;
using System;
using System.Web.Script.Services;
using System.Web.Services;

public partial class fasi_wmethods_PasswordChange : System.Web.UI.Page
{
    /// <summary>
    /// Indica se el usuario está autenticado
    /// </summary>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object IsValidToken(string tokeValue)
    {
        bool IsValid = false;
        object userValue=null;
        try
        {
            tokeValue = tokeValue.ToUpper();
            var value = InMotionGIT.Common.Helpers.CryptSupportNew.DecryptString(tokeValue);
            if (value.IsNotEmpty())
            {
                var valueToken = InMotionGIT.Common.Helpers.Serialize.DeserializeJSON<InMotionGIT.FASI.Contracts.Security.UserToken>(value);
                if (valueToken.IsNotEmpty())
                {
                    userValue = valueToken;
                    if (valueToken.ExpirationDate >= DateTime.UtcNow)
                    {
                        IsValid = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog("PasswordChange", "IsValidToken", ex);
        }
        IsValid = true;
        return new { IsValid = IsValid, User = userValue };
    }

}