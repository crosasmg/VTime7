// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar

using System;
using System.Configuration;
using System.Web;
using System.Web.Profile;
using System.Web.Script.Services;
using System.Web.Services;
using InMotionGIT.Core.Configuration;
using InMotionGIT.Core.Configuration.Enumerations;
using InMotionGIT.Common.Extensions;
namespace Dropthings.Web.Framework
{
    /// <summary>
    /// Summary description for WebServiceBase
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class WebServiceBase : System.Web.Services.WebService
    {
        public WebServiceBase()
        {
        }

        ////ISAAC SE DOCUMENTO
        ////    protected UserProfile dProfile = HttpContext.Current.Profile as UserProfile;

        //private Dropthings.Web.Framework.UserProfile _ProfileInfo;

        ///// <summary>
        ///// Return the user profile information conditional on the type of security
        ///// </summary>
        //protected Dropthings.Web.Framework.UserProfile ProfileInfo
        //{
        //    get
        //    {
        //        if (_ProfileInfo == null)
        //        {
        //            VisualTIME config = (VisualTIME)ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection");
        //            EnumSecurityMode securityMode = config.Security.Mode;
        //            string name = string.Empty;
        //            switch (securityMode)
        //            {
        //                case EnumSecurityMode.ActiveDirectory:
        //                    _ProfileInfo = (UserProfile)HttpContext.Current.Profile;
        //                    break;

        //                case EnumSecurityMode.DataBase:
        //                    _ProfileInfo = (UserProfile)HttpContext.Current.Profile;
        //                    break;

        //                case EnumSecurityMode.HeaderAuthentication:
        //                    if (HttpContext.Current.Request.Headers.Get("SM_USER") != null)
        //                    {
        //                        name = HttpContext.Current.Request.Headers.Get("SM_USER");
        //                    }
        //                    else
        //                    {
        //                        name = string.Empty;
        //                    }
        //                    if (string.IsNullOrEmpty(name))
        //                    {
        //                        _ProfileInfo = (UserProfile)HttpContext.Current.Profile;
        //                    }
        //                    else
        //                    {
        //                        _ProfileInfo = (UserProfile)ProfileBase.Create(name);
        //                    }

        //                    break;

        //                case EnumSecurityMode.Windows:
        //                    name = HttpContext.Current.User.Identity.Name.Replace("\\", ".").Split(new Char[] { '.' })[1];
        //                    _ProfileInfo = (UserProfile)ProfileBase.Create(name);
        //                    break;

        //                default:
        //                    _ProfileInfo = (UserProfile)HttpContext.Current.Profile;
        //                    break;
        //            }
        //        }
        //        return _ProfileInfo;
        //    }
        //    set
        //    {
        //        this._ProfileInfo = value;
        //    }
        //}



        
    }
}