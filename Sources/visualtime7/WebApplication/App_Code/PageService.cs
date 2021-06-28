// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar

using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using DashboardBusiness;

/// <summary>
/// Summary description for WidgetService
/// </summary>

namespace Dropthings.Web.Framework
{
    public class PageService : WebServiceBase
    {
        public PageService()
        {
            //Uncomment the following line if using designed components
            //InitializeComponent();
        }

       
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true)]
        public string NewPage(string newLayout)
        {
           DashboardBusiness.Helpers.PageService.CleanCache(); 
            List<InMotionGIT.Common.DataType.LookUpValue> list = Dropthings.Web.Util.ResourceManager.getListResourceAllSource("NewTab");
            list = InMotionGIT.Membership.Providers.Helper.ConvertList(list); 
            var UserInfo = new InMotionGIT.Membership.Providers.MemberContext();
            DashboardBusiness.Helpers.PageService.CleanCache(); 
            return DashboardBusiness.Helpers.PageService.NewPage(newLayout, (int)UserInfo.User.ProviderUserKey, list);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true)]
        public void ChangeCurrentPage(int pageId)
        {
            var UserInfo = new InMotionGIT.Membership.Providers.MemberContext();
            DashboardBusiness.Helpers.PageService.ChangeCurrentPage((int)UserInfo.User.ProviderUserKey, pageId, UserInfo.UserName, "", UserInfo.User.LanguageID);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true)]
        public string DeletePage(int PageID)
        {
            var UserInfo = new InMotionGIT.Membership.Providers.MemberContext();
            DashboardBusiness.Helpers.PageService.CleanCache();
            return DashboardBusiness.Helpers.PageService.DeletePage(PageID, (int)UserInfo.User.ProviderUserKey, Context);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true)]
        public void RenamePage(string newName)
        {
            var UserInfo = new InMotionGIT.Membership.Providers.MemberContext();

            (new DashboardFacade()).ChangePageName((int)UserInfo.User.ProviderUserKey, newName, UserInfo.LanguageId );
            DashboardBusiness.Helpers.PageService.CleanCache(); 
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true)]
        public void ChangePageLayout(int newLayout)
        {
            var UserInfo = new InMotionGIT.Membership.Providers.MemberContext();

            DashboardBusiness.Helpers.PageService.ChangePageLayout(newLayout, (int)UserInfo.User.ProviderUserKey, UserInfo.UserName, UserInfo.RoleName, UserInfo.User.LanguageID);
            DashboardBusiness.Helpers.PageService.CleanCache(); 
        }
    }
}