// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar

using DashboardBusiness;
using Dropthings.Web.Util;
using GIT.Core;
using InMotionGIT.Common.Extensions;
using InMotionGIT.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Page = InMotionGIT.FrontOffice.Contracts.Page;

public partial class DefaultWebForm : PageBase
{
    #region Utility
    [WebMethod()]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object AppInfo()
    {
        string result = null;
        try
        {
            result = InMotionGIT.FrontOffice.Proxy.Helpers.AppInfo.AppInfo();
        }
        catch (Exception ex)
        {
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog("AppInfo", ex.Message, ex);
        }
        return result;
    }
    #endregion

    #region Búsqueda de transacciones de BackOffice

    [WebMethod()]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object GetUrlTransaction(string windowLogicalCode)
    {
        InMotionGIT.BackOffice.Support.Contracts.Transaction result = null;
        try
        {
            result= InMotionGIT.BackOffice.Support.BackOfficeProcess.GetUrlTransaction(windowLogicalCode, System.Web.HttpContext.Current.Session["nUsercode"].ToString(), System.Web.HttpContext.Current.Session["sSche_code"].ToString());
        }
        catch (Exception ex)
        {
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog(String.Format("GetUrlTransaction({0})", windowLogicalCode), ex.Message, ex);
        }
        return result;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string[] GetTransaction(string prefix)
    {
        string[] result = null;
        try
        {
            result = InMotionGIT.BackOffice.Support.BackOfficeProcess.GetTransaction(prefix);
        }
        catch (Exception ex)
        {
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog(String.Format("GetTransaction({0})", prefix), ex.Message, ex);
        }
        return result;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object IsAllowed(string windowLogicalCode)
    {
        return InMotionGIT.BackOffice.Support.BackOfficeProcess.IsAllowed(string.Empty, string.Empty, windowLogicalCode, 0); 
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object Configurations()
    {
        Dictionary<string, string> result = null;
        try
        {
            result = InMotionGIT.FrontOffice.Support.Helpers.ConfigurationHandler.ConfigurationFormat();
        }
        catch (Exception ex)
        {
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog("Configurations", ex.Message, ex);
        }
        return result;
    }

    #endregion Búsqueda de transacciones de BackOffice

    private const string WIDGET_CONTAINER = "WidgetContainer.ascx";
    private string[] updatePanelIDs = new string[] { "LeftUpdatePanel", "MiddleUpdatePanel", "RightUpdatePanel" };

    private InMotionGIT.FrontOffice.Contracts.UserPageSetup _Setup
    {
        get { return Context.Items[typeof(InMotionGIT.FrontOffice.Contracts.UserPageSetup)] as InMotionGIT.FrontOffice.Contracts.UserPageSetup; }
        set { Context.Items[typeof(InMotionGIT.FrontOffice.Contracts.UserPageSetup)] = value; }
    }

    private int AddStuffPageIndex
    {
        get { object val = ViewState["AddStuffPageIndex"]; if (val == null) return 0; else return (int)val; }
        set { ViewState["AddStuffPageIndex"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void OnInit(EventArgs e)
    {
        if (ConfigurationManager.AppSettings["Url.Default.Redirect"].IsNotEmpty())
        {
            Response.Redirect(ConfigurationManager.AppSettings["Url.Default.Redirect"]);
        }
        VisualTIME config = (VisualTIME)ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection");
        base.OnInit(e);

        Page.Title = Dropthings.Web.Util.ResourceManager.getResource("WebSiteTitle");

        // Check if revisit is valid or not
        if (!base.IsPostBack)
        {
            // Block cookie less visit attempts
            if (base.IsFirstTime)
            {
                if (!ActionValidator.IsValid(ActionValidator.ActionTypeEnum.FirstVisit)) Response.End();
            }
            else
            {
                if (!ActionValidator.IsValid(ActionValidator.ActionTypeEnum.Revisit)) Response.End();
            }

            if (Context.Request.QueryString["SessionTimeOut"] == "Yes")
            {
                popupExpired.ShowOnPageLoad = true;

                if (config.Security.Mode == InMotionGIT.Core.Configuration.Enumerations.EnumSecurityMode.HeaderAuthentication)
                {
                    var message = GetGlobalResourceObject("Resource", "ExpiredMessage").ToString();
                    Response.Redirect("~/dropthings/ErrorPage.aspx?ErrorMessage=" + message);
                }
            }
        }
        else
        {
            // Limit number of postbacks
            if (!ActionValidator.IsValid(ActionValidator.ActionTypeEnum.Postback)) Response.End();
        }

        if (UserInfo.IsAnonymous == true)
        {
            this.ShowAddContentPanel.Visible = false;
            this.ChangePageTitleLinkButton.Visible = false;
            this.OnPageMenuUpdatePanel.Visible = false;
        }
        //Agregado para corregir incidencias relacionadas al tratamiento de pólizas.
        else
        {
            if (Session["nUsercode"] != null && Session["nUsercodeVtime"] == null)
            {
                Session["nUsercodeVtime"] = Session["nUsercode"];
            }

            if (Session["nUsercode"] != null &&
                    Session["nUsercodeVtime"] != null &&
                        !Session["nUsercode"].Equals(Session["nUsercodeVtime"]))
            {
                Session["nUsercode"] = Session["nUsercodeVtime"];
            }
        }
    }

    protected override void CreateChildControls()
    {
        base.CreateChildControls();

        this.LoadAddStuff();

        this.WidgetPanelsLayout.SetLayout(_Setup.CurrentPage.LayoutType);

        // First visit, non postback
        this.SetupWidgets(wi => !ScriptManager.GetCurrent(Page).IsInAsyncPostBack);

        this.Master.SetupTabs();
    }

    private void SetupWidgets(Func<InMotionGIT.FrontOffice.Contracts.WidgetInstance, bool> isWidgetFirstLoad)
    {
        InMotionGIT.FrontOffice.Contracts.UserPageSetup setup = _Setup;

        List<InMotionGIT.FrontOffice.Contracts.WidgetInstance> listWidgetInstanceByPage = (from item in setup.WidgetInstances
                                                                                                          where item.PageId == setup.CurrentPage.ID
                                                                                                          select item).ToList();

        if (listWidgetInstanceByPage.IsEmpty() | listWidgetInstanceByPage.Count() == 0)
        {
            if (setup.CurrentPage.IsLoad == false)
            {
                List<InMotionGIT.FrontOffice.Contracts.WidgetInstance> result = DashboardBusiness.Helpers.WidgetService.GetWidgetsInPage(setup.CurrentPage.ID, UserInfo.LanguageId, UserInfo.RoleName);
                setup.CurrentPage.IsLoad = true;
                if (result.IsNotEmpty() && result.Count != 0)
                {
                    setup.WidgetInstances.AddRange(result);
                }
                _Setup = setup;
                Session["UserPageSetup"] = _Setup;
                listWidgetInstanceByPage = result;
            }
        }

        GIT.Core.PageBase.DumpSetup("SetupWidgets", _Setup, UserInfo);

        var columnPanels = new Panel[] {
            this.WidgetPanelsLayout.FindControl("LeftPanel") as Panel,
            this.WidgetPanelsLayout.FindControl("MiddlePanel") as Panel,
            this.WidgetPanelsLayout.FindControl("RightPanel") as Panel };

        // Clear existing widgets if any
        foreach (Panel panel in columnPanels)
        {
            List<WidgetContainer> widgets = panel.Controls.OfType<WidgetContainer>().ToList();
            foreach (var widget in widgets) panel.Controls.Remove(widget);
        }

        if (listWidgetInstanceByPage.IsNotEmpty())
        {
            listWidgetInstanceByPage = (from itemWidget in listWidgetInstanceByPage
                                        orderby itemWidget.PageId ascending
                                        select itemWidget).ToList();
        }

        List<String> listWidgetIds = new List<string>();

        foreach (InMotionGIT.FrontOffice.Contracts.WidgetInstance instance in listWidgetInstanceByPage)
        {
            string found = (from itemWidget in listWidgetIds
                            where listWidgetIds.Contains(instance.Id.ToString())
                            select itemWidget).FirstOrDefault();

            if (found.IsEmpty())
            {
                listWidgetIds.Add(instance.Id.ToString());
                var panel = columnPanels[instance.ColumnNo];

                var widget = LoadControl(WIDGET_CONTAINER) as WidgetContainer;
                widget.ID = "WidgetContainer" + instance.Id.ToString();
                widget.IsFirstLoad = isWidgetFirstLoad(instance);
                widget.WidgetInstance = instance;
                widget.UserInfo = UserInfo;

                widget.Deleted += new Action<InMotionGIT.FrontOffice.Contracts.WidgetInstance>(widget_Deleted);

                try
                {
                    panel.Controls.Add(widget);
                }
                catch (Exception ex)
                {
                    InMotionGIT.Common.Helpers.LogHandler.ErrorLog(String.Format("Error in SetupWidgets in widgetid: '{0}' with name '{1}'", instance.Id.ToString(), instance.Url.ToString()), ex.Message, ex);
                }
            }
        }
    }

    private void widget_Deleted(InMotionGIT.FrontOffice.Contracts.WidgetInstance obj)
    {
        new DashboardFacade(UserInfo.UserName).DeleteWidgetInstance(UserInfo.UserName, obj.Id);

        this.ReloadPage(wi => false);

        this.RefreshColumn(obj.ColumnNo);
    }

    private void ReloadPage(Func<InMotionGIT.FrontOffice.Contracts.WidgetInstance, bool> isWidgetFirstLoad)
    {
        this.Master.LoadUserPageSetup(false);
        this.Master.SetupTabs();

        this.SetupWidgets(isWidgetFirstLoad);
    }

    private void RefreshAllColumns()
    {
        this.RefreshColumn(0);
        this.RefreshColumn(1);
        this.RefreshColumn(2);
    }

    private void RefreshColumn(int columnNo)
    {
        var updatePanel = this.WidgetPanelsLayout.FindControl(this.updatePanelIDs[columnNo]) as UpdatePanel;
        updatePanel.Update();
    }

    protected void ShowAddContentPanel_Click(object sender, EventArgs e)
    {
        this.AddContentPanel.Visible = true;
        this.HideAddContentPanel.Visible = true;
        this.ShowAddContentPanel.Visible = false;

        this.LoadAddStuff();
    }

    protected void HideAddContentPanel_Click(object sender, EventArgs e)
    {
        this.AddContentPanel.Visible = false;
        this.HideAddContentPanel.Visible = false;
        this.ShowAddContentPanel.Visible = true;
    }

    private List<InMotionGIT.FrontOffice.Contracts.Widget> WidgetList
    {
        get
        {
            string cacheKey = "Widgets" + UserInfo.RoleName + UserInfo.User.LanguageID; ;
            List<InMotionGIT.FrontOffice.Contracts.Widget> WidgetsListByRole = InMotionGIT.Common.Helpers.Caching.GetItem(cacheKey) as List<InMotionGIT.FrontOffice.Contracts.Widget>;

            if (Request.QueryString["m"] == "r")
            {
                InMotionGIT.Common.Helpers.Caching.Remove(cacheKey);
                WidgetsListByRole = null;
            }
            if (WidgetsListByRole == null)
            {
                using (InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient client = new InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient())
                {
                    WidgetsListByRole = client.WidgetGetListByRole(UserInfo.RoleName, UserInfo.User.LanguageID);
                    if (WidgetsListByRole.IsNotEmpty() && WidgetsListByRole.Count != 0)
                    {
                        WidgetsListByRole = (from item in WidgetsListByRole
                                             orderby item.Name ascending
                                             select item).ToList();
                    }
                }
                InMotionGIT.Common.Helpers.Caching.SetItem(cacheKey, WidgetsListByRole);
            }
            return WidgetsListByRole;
        }
    }

    private void LoadAddStuff()
    {
        if (this.AddContentPanel.Visible)
        {
            this.WidgetDataList.ItemCommand += new DataListCommandEventHandler(WidgetDataList_ItemCommand);

            var itemsToShow = WidgetList.Skip(AddStuffPageIndex * 30).Take(30);
            this.WidgetDataList.DataSource = itemsToShow;
            this.WidgetDataList.DataBind();

            this.WidgetListPreviousLinkButton.Visible = AddStuffPageIndex > 0;
            this.WidgetListNextButton.Visible = AddStuffPageIndex * 30 + 30 < WidgetList.Count;
        }
    }

    private void WidgetDataList_ItemCommand(object source, DataListCommandEventArgs e)
    {
        InMotionGIT.Membership.Providers.FrontOfficeMembershipUser profile = UserInfo.User;

        if (!ActionValidator.IsValid(ActionValidator.ActionTypeEnum.AddNewWidget)) return;

        int widgetId = int.Parse(e.CommandArgument.ToString());

        InMotionGIT.FrontOffice.Contracts.WidgetInstance newWidget;
        VisualTIME config = (VisualTIME)ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection");
        using (InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient client = new InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient())
        {
            newWidget = client.WidgetAdd((int)UserInfo.User.ProviderUserKey, widgetId, _Setup.CurrentPage.ID, UserInfo.User.LanguageID, UserInfo.RoleName);
            _Setup.WidgetInstances.Add(newWidget);
            Session["UserPageSetup"] = _Setup;
        }

        newWidget = (from wiIn in _Setup.WidgetInstances
                     where wiIn.Id == newWidget.Id
                     select wiIn).Single();

        /// User added a new widget. The new widget is loaded for the first time. So, it's not
        /// a postback experience for the widget. But for rest of the widgets, it is a postback experience.
        this.ReloadPage(wi => wi.Id == newWidget.Id);
        this.RefreshColumn(newWidget.ColumnNo); // Refresh the middle column where the new widget is added

        ScriptManager.RegisterStartupScript(this, typeof(Page), "ReloadBeforeAddWidget",
                                            "<script type='text/javascript'>" + ActionRedirect() + ";</script>", false);
    }

    private string ActionRedirect()
    {
        StringBuilder body = new StringBuilder();
        body.AppendLine(string.Format("$.toast({0}", "{"));
        body.AppendLine(string.Format("heading: '{0}',", ResourceManager.getResource("WidgetReloadPageTitle")));
        body.AppendLine(string.Format("text: '{0}',", ResourceManager.getResource("WidgetReloadPageBody")));
        body.AppendLine("showHideTransition: 'slide',");
        body.AppendLine("loaderBg: 'yellow',");
        body.AppendLine("textColor: '#eee',");
        body.AppendLine("bgColor:'gray',");
        body.AppendLine("position: 'mid-center',");
        body.AppendLine("allowToastClose: false,");
        body.AppendLine("icon: 'info',");
        body.AppendLine("hideAfter: '5000',");
        body.AppendLine(string.Format("afterHidden: function () {0}", "{"));
        body.AppendLine("    window.location.reload();");
        body.AppendLine(string.Format("{0}", "}"));
        body.AppendLine(string.Format("{0})", "}"));
        return body.ToString();
    }

    private void WidgetDataList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            InMotionGIT.FrontOffice.Contracts.Widget widget = e.Item.DataItem as InMotionGIT.FrontOffice.Contracts.Widget;

            LinkButton link = e.Item.Controls.OfType<LinkButton>().Single();

            link.Text = widget.Name;

            link.CommandName = "AddWidget";
            link.CommandArgument = widget.ID.ToString();
        }
    }

    private void AddWidgetLink_Click(object sender, EventArgs e)
    {
    }

    protected void WidgetListPreviousLinkButton_Click(object sender, EventArgs e)
    {
        if (0 == this.AddStuffPageIndex)
            return;

        this.AddStuffPageIndex--;

        this.LoadAddStuff();
    }

    protected void WidgetListNextButton_Click(object sender, EventArgs e)
    {
        this.AddStuffPageIndex++;
        this.LoadAddStuff();
    }

    protected void ChangeTabSettingsLinkButton_Clicked(object sender, EventArgs e)
    {
        if (this.ChangePageSettingsPanel.Visible)
            this.HideChangeSettingsPanel();
        else
            this.ShowChangeSettingsPanel();
    }

    protected void SaveNewTitleButton_Clicked(object sender, EventArgs e)
    {
        var newTitle = this.NewTitleTextBox.Text.Trim();

        if (newTitle != InMotionGIT.FrontOffice.Support.Helpers.Page.GetTitle(_Setup.CurrentPage))
        {
            new DashboardFacade().ChangePageName(_Setup.CurrentPage.ID, newTitle, UserInfo.LanguageId);

            DashboardBusiness.Helpers.PageService.CleanCache();
            this.Master.LoadUserPageSetup(false);
            this.Master.RedirectToTab(_Setup.CurrentPage);
        }
    }

    protected void DeleteTabLinkButton_Clicked(object sender, EventArgs e)
    {
        var currentPage = new DashboardFacade().DeleteCurrentPage((int)UserInfo.User.ProviderUserKey, _Setup.CurrentPage.ID);
        Context.Cache.Remove(UserInfo.UserName);
        DashboardBusiness.Helpers.PageService.CleanCache();
        this.Master.RedirectToTab(currentPage);
    }

    private void ShowChangeSettingsPanel()
    {
        this.ChangePageSettingsPanel.Visible = true;
        this.ChangePageTitleLinkButton.Text = ResourceManager.getResource("HideSettings");

        this.NewTitleTextBox.Text = InMotionGIT.FrontOffice.Support.Helpers.Page.GetTitle(_Setup.CurrentPage);
    }

    private void HideChangeSettingsPanel()
    {
        this.ChangePageSettingsPanel.Visible = false;
        this.ChangePageTitleLinkButton.Text = ResourceManager.getResource("ChangeSettings");
    }
}