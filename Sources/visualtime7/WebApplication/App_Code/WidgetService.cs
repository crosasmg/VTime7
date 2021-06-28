// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar

using System.Web.Script.Services;
using System.Web.Services;
using System.Linq;
using InMotionGIT.Common.Extensions;
 
namespace Dropthings.Web.Framework
{
    /// <summary>
    /// Summary description for WidgetService
    /// </summary>

    public class WidgetService : WebServiceBase
    {
        public WidgetService()
        {
            //Uncomment the following line if using designed components
            //InitializeComponent();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true)]
        public void MoveWidgetInstance(int widgetId, int toColumn, int toRow)
        {
            DashboardBusiness.Helpers.WidgetService.MoveWidgetInstance(widgetId, toColumn, toRow, (int)(new InMotionGIT.Membership.Providers.MemberContext()).User.ProviderUserKey, (new InMotionGIT.Membership.Providers.MemberContext()).UserName, Context);
            if (System.Web.HttpContext.Current.Session != null)
            {
                if (System.Web.HttpContext.Current.Session["UserPageSetup"] != null)
                {
                    InMotionGIT.FrontOffice.Contracts.UserPageSetup _SetupTemporal = (InMotionGIT.FrontOffice.Contracts.UserPageSetup)System.Web.HttpContext.Current.Session["UserPageSetup"];
                 InMotionGIT.FrontOffice.Contracts.WidgetInstance temporalWidget = (from itemWidget in _SetupTemporal.WidgetInstances 
                                                                                    where itemWidget.Id ==  widgetId
                                                                                    select itemWidget).FirstOrDefault() ;
                 if (temporalWidget.IsNotEmpty()) {
                     temporalWidget.ColumnNo = toColumn;
                     temporalWidget.OrderNo = toRow; 
                 }
                }
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = false, XmlSerializeString = true)]
        public void DeleteWidgetInstance(int widgetId)
        {
            DashboardBusiness.Helpers.WidgetService.DeleteWidgetInstance(widgetId, (new InMotionGIT.Membership.Providers.MemberContext()).UserName, Context);
            if (System.Web.HttpContext.Current.Session != null)
            {
                if (System.Web.HttpContext.Current.Session["UserPageSetup"] != null)
                {
                    InMotionGIT.FrontOffice.Contracts.UserPageSetup _SetupTemporal = (InMotionGIT.FrontOffice.Contracts.UserPageSetup) System.Web.HttpContext.Current.Session["UserPageSetup"];
                    _SetupTemporal.WidgetInstances.RemoveAll((x) => x.Id == widgetId);  
                }
            }
        }
    }
}