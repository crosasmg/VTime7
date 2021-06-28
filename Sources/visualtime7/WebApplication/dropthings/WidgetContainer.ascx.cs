// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar

using Dropthings.Widget.Framework;
using InMotionGIT.Common.Extensions;
using InMotionGIT.Core.Configuration;
using System;
using System.Configuration;
using System.Web.UI;

public partial class WidgetContainer : System.Web.UI.UserControl, IWidgetHost
{
    public event Action<InMotionGIT.FrontOffice.Contracts.WidgetInstance> Deleted;

    public bool SettingsOpen
    {
        get
        {
            object val = ViewState[this.ClientID + "_SettingsOpen"] ?? false;
            return (bool)val;
        }
        set { ViewState[this.ClientID + "_SettingsOpen"] = value; }
    }

    private InMotionGIT.FrontOffice.Contracts.WidgetInstance _WidgetInstance;

    public InMotionGIT.FrontOffice.Contracts.WidgetInstance WidgetInstance
    {
        get { return _WidgetInstance; }
        set { _WidgetInstance = value; }
    }

    public InMotionGIT.FrontOffice.Contracts.Widget WidgetDef { get; set; }

    private IWidget _WidgetRef;

    private bool _IsFirstLoad;

    public bool IsFirstLoad
    {
        get { return _IsFirstLoad; }
        set { _IsFirstLoad = value; }
    }

    private InMotionGIT.Membership.Providers.MemberContext _UserInfo;

    public InMotionGIT.Membership.Providers.MemberContext UserInfo
    {
        get
        {
            if (_UserInfo.IsEmpty())
            {
                _UserInfo = new InMotionGIT.Membership.Providers.MemberContext();
            }
            return _UserInfo;
        }
        set { _UserInfo = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        WidgetTitleButton.Text = this.WidgetInstance.Title;
        WidgetTitleLabel.Text = this.WidgetInstance.Title;
        this.SetExpandCollapseButtons();

        //this.CloseWidget.OnClientClick = "DeleteWarning.show( function() { __doPostBack('" + this.CloseWidget.UniqueID+ "','') }, Function.emptyFunction ); return false; ";
        this.CloseWidgetButton.OnClientClick = "DeleteWarning.show( function() { DropthingsUI.Actions.deleteWidget('" + this.WidgetInstance.Id + "')}, Function.emptyFunction ); return false; ";
        this.CollapseWidgetButton.OnClientClick = "DropthingsUI.Actions.minimizeWidget('" + this.WidgetBodyPanel.ClientID + "')";
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        string SessionRole = UserInfo.RoleName;
        VisualTIME config = (VisualTIME)ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection");

        Control widget = null;

        try
        {
            widget = LoadControl(this.WidgetInstance.Url);
        }
        catch (Exception ex)
        {
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog("WidgetContainer","LoadWidget", ex);
            widget = LoadControl("widgets/NotFound.ascx");
        }

        widget.ID = "Widget" + this.WidgetInstance.Id.ToString();

        //WidgetBodyUpdatePanel.ContentTemplateContainer.Controls.Add(widget);
        WidgetBodyPanel.Controls.Add(widget);
        this._WidgetRef = widget as IWidget;
        this._WidgetRef.Init(this);

        EditWidgetButton.Visible = WidgetInstance.IsEditAllow;

        if (!Page.IsPostBack)
        {
            bool IsAllowedToEditTheTitle = WidgetInstance.IsAllowedToEditTheTitle;
            WidgetTitleButton.Visible = IsAllowedToEditTheTitle;
            WidgetTitleLabel.Visible = !IsAllowedToEditTheTitle;
        }
    }

    private void SetExpandCollapseButtons()
    {
        if (!this.WidgetInstance.Expanded)
        {
            ExpandWidgetButton.Visible = true;
            CollapseWidgetButton.Visible = false;
            WidgetBodyPanel.Visible = false;
        }
        else
        {
            ExpandWidgetButton.Visible = false;
            CollapseWidgetButton.Visible = true;
            WidgetBodyPanel.Visible = true;
        }
    }

    protected void EditWidgetButton_Click(object sender, EventArgs e)
    {
        if (this.SettingsOpen)
        {
            (this as IWidgetHost).HideSettings();
        }
        else
        {
            (this as IWidgetHost).ShowSettings();
        }

        WidgetBodyUpdatePanel.Update();
    }

    protected void CollapseWidgetButton_Click(object sender, EventArgs e)
    {
        (this as IWidgetHost).Minimize();
    }

    protected void ExpandWidgetButton_Click(object sender, EventArgs e)
    {
        (this as IWidgetHost).Maximize();
    }

    protected void CloseWidgetButton_Click(object sender, EventArgs e)
    {
        this._WidgetRef.Closed();
        (this as IWidgetHost).Close();
    }

    protected void SaveWidgetTitleButton_Click(object sender, EventArgs e)
    {
        WidgetTitleTextBox.Visible = SaveWidgetTitleButton.Visible = false;
        WidgetTitleButton.Visible = true;
        WidgetTitleButton.Text = WidgetTitleTextBox.Text;

        using (InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient client = new InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient())
        {
            client.WidgetInstanceChangeTitle(WidgetInstance.Id, UserInfo.User.LanguageID, WidgetTitleButton.Text);
        }
    }

    protected void WidgetTitleButton_Click(object sender, EventArgs e)
    {
        if (this.WidgetInstance.IsAllowedToEditTheTitle)
        {
            WidgetTitleTextBox.Text = this.WidgetInstance.Title;
            WidgetTitleTextBox.Visible = true;
            SaveWidgetTitleButton.Visible = true;
            WidgetTitleButton.Visible = false;
        }
    }

    protected void CancelEditWidgetButton_Click(object sender, EventArgs e)
    {
    }

    int IWidgetHost.ID
    {
        get
        {
            return this.WidgetInstance.Id;
        }
    }

    void IWidgetHost.Maximize()
    {
        using (InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient cliente = new InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient())
        {
            cliente.WidgetInstanceExpanded(this.WidgetInstance.Id, true);
        }

        this.WidgetInstance.Expanded = true;

        this.SetExpandCollapseButtons();
        this._WidgetRef.Maximized();

        WidgetBodyUpdatePanel.Update();
        WidgetHeaderUpdatePanel.Update();
    }

    void IWidgetHost.Minimize()
    {
        using (InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient cliente = new InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient())
        {
            cliente.WidgetInstanceExpanded(this.WidgetInstance.Id, false);
        }

        this.WidgetInstance.Expanded = false;

        this.SetExpandCollapseButtons();
        this._WidgetRef.Minimized();

        WidgetBodyUpdatePanel.Update();
        WidgetHeaderUpdatePanel.Update();
    }

    void IWidgetHost.Close()
    {
        using (InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient cliente = new InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient())
        {
            cliente.WidgetInstanceDeleteProcess(this.WidgetInstance.Id);
        }
    }

    public override void RenderControl(HtmlTextWriter writer)
    {
        writer.AddAttribute("InstanceId", this.WidgetInstance.Id.ToString());
        base.RenderControl(writer);
    }

    void IWidgetHost.SaveState(string state)
    {
        using (InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient cliente = new InMotionGIT.FrontOffice.Proxy.FrontOfficeManager.ManagerClient())
        {
            cliente.WidgetInstanceStateChange(this.WidgetInstance.Id, state);
        }

        this.WidgetInstance.State = state;

        // Invalidate cache because widget's state is stored in cache
        Cache.Remove(UserInfo.UserName);
    }

    /// <summary>
    /// Detach associated objects from WidgetInstance so that
    /// they do not get inserted again
    /// </summary>
    /// <param name="a"></param>
    private void DetachAssociation(Action a)
    {
        //var pageRef = this.WidgetInstance.Page;
        //var widgetRef = this.WidgetInstance.Widget;

        //this.WidgetInstance.Detach();

        //a.Invoke();

        //this.WidgetInstance.Detach();

        //this.WidgetInstance.Page = pageRef;
        //this.WidgetInstance.Widget = widgetRef;
    }

    string IWidgetHost.GetState()
    {
        return this.WidgetInstance.State;
    }

    bool IWidgetHost.IsFirstLoad
    {
        get
        {
            return this.IsFirstLoad;
        }
    }

    void IWidgetHost.ShowSettings()
    {
        this.SettingsOpen = true;
        this._WidgetRef.ShowSettings();
        (this as IWidgetHost).Maximize();
        EditWidgetButton.Visible = false;
        CancelEditWidgetButton.Visible = true;
        this.WidgetHeaderUpdatePanel.Update();
    }

    void IWidgetHost.HideSettings()
    {
        this.SettingsOpen = false;
        this._WidgetRef.HideSettings();
        EditWidgetButton.Visible = true;
        CancelEditWidgetButton.Visible = false;
        this.WidgetHeaderUpdatePanel.Update();
    }
}