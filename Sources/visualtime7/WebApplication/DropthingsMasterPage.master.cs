using DevExpress.Web.ASPxMenu;
using InMotionGIT.Common.Extensions;
using InMotionGIT.Core.Configuration;
using InMotionGIT.Core.Configuration.Enumerations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Page = InMotionGIT.FrontOffice.Contracts.Page;

public partial class DropthingsMasterPage : GIT.Core.MasterBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Application["upgradeInfo"].IsNotEmpty() )
        {
            CopyrightLabel.ToolTip = Application["upgradeInfo"].ToString();
        }

        int currentLanguageId = 0;

        VisualTIME config = (VisualTIME)ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection");

        var securityMod = config.Security.Mode;

        ChangePasswordPopupControl.ContentUrl = config.Authentification.LinkChangePasswordUrl;
        PrincipalMenu.Items[0].NavigateUrl = config.Authentification.LinkUserProfileUrl; //User MenuItem
        if (config.Authentification.ShowStartUpMessage)
        {
            LogInPopupControl.Height = 270;
            LogInPopupControl.Width = 570;
        }
        else
        {
            LogInPopupControl.Height = 270;
            LogInPopupControl.Width = 370;
        }

        if (string.IsNullOrEmpty(Request.QueryString["resetUser"]) != true)
        {
            InMotionGIT.Common.Helpers.Caching.Remove(string.Format("PropertyUser_{0}", ProfileInfo.UserName));
            ProfileInfo = new InMotionGIT.Membership.Providers.MemberContext(true);
        }

        //Language MenuItem
        if (ProfileInfo.LanguageId != 0)
        {
            currentLanguageId = ProfileInfo.LanguageId;
        }
        else
        {
            var currentLanguage = config.General.DefaultLanguage.ToLower();

            if (string.IsNullOrEmpty(currentLanguage))
            {
                string[] userLanguages = HttpContext.Current.Request.UserLanguages;

                if (userLanguages.Length > 0)
                {
                    string firstLanguage = userLanguages[0];
                    currentLanguage = firstLanguage.Substring(0, 2).ToLower();
                }
            }

            currentLanguageId = InMotionGIT.Common.Proxy.Helpers.Language.GetLanguageIdCurrentContext(currentLanguage);
        }

        List<InMotionGIT.Common.DataType.LookUpValue> languagesValues = InMotionGIT.Common.Proxy.Helpers.Language.LookUpLanguageByCurrentInfo(currentLanguageId);

        PrincipalMenu.Items[6].Items.Clear();
        DevExpress.Web.ASPxMenu.MenuItem newMenuItem;

        foreach (InMotionGIT.Common.DataType.LookUpValue languageItem in languagesValues)
        {
            if (string.Equals(languageItem.Code, currentLanguageId.ToString()))
            {
                PrincipalMenu.Items[6].Text = languageItem.Description;
            }
            else
            {
                newMenuItem = new DevExpress.Web.ASPxMenu.MenuItem();
                newMenuItem.Name = string.Format("{0}MenuItem", languageItem.Description);
                try
                {
                    newMenuItem.Text = languageItem.Description;
                    PrincipalMenu.Items[6].Items.Add(newMenuItem);
                }
                catch
                {
                    newMenuItem = null;
                }
            }
        }

        if (ProfileInfo.IsAnonymous)
        {
            PrincipalMenu.Items[1].Visible = true; //LogIn MenuItem
            PrincipalMenu.Items[2].Visible = false; //LogOff MenuItem
            PrincipalMenu.Items[3].Visible = false; //ChangePassword MenuItem
        }
        else
        {
            PrincipalMenu.Items[1].Visible = false; //LogIn MenuItem
            PrincipalMenu.Items[2].Visible = true; //LogOff MenuItem
            PrincipalMenu.Items[3].Visible = true; //ChangePassword MenuItem

            string userName = string.Empty;

            userName = ProfileInfo.UserName;

            PrincipalMenu.Items[0].Text = userName; //User MenuItem
            PrincipalMenu.Items[0].Visible = true; //User MenuItem
            PrincipalMenu.Items[3].Visible = true; //StartOver MenuItem

            int expirationDays = 0;
            bool exeChangePassword = false;

            if (ProfileInfo.IsEmployee)
                expirationDays = config.Security.EmployeePasswordExpiration;
            else
                expirationDays = config.Security.UserPasswordExpiration;

            if (securityMod == InMotionGIT.Core.Configuration.Enumerations.EnumSecurityMode.DataBase &&
                ProfileInfo.UserName.ToLower().Equals(config.Security.AdministratorUser) == false)
            {
                if (!ProfileInfo.PasswordNeverExpires)
                {
                    if (expirationDays != 0 && ProfileInfo.User.LastPasswordChangedDate.Date.AddDays(expirationDays) < DateTime.Now.Date)
                        exeChangePassword = true;

                    if (ProfileInfo.User.AccountLockoutNotification || ProfileInfo.User.FirstTimePasswordChange || exeChangePassword)
                    {
                        if (exeChangePassword)
                        {
                            ChangePasswordPopupControl.FooterText = Dropthings.Web.Util.ResourceManager.getResource("PasswordExpiration");
                            ChangePasswordPopupControl.ContentUrl += "?mode=expiration";
                        }
                        else
                        {
                            ChangePasswordPopupControl.FooterText = Dropthings.Web.Util.ResourceManager.getResource("FirstTimePassword");
                        }

                        if (ChangePasswordPopupControl.ContentUrl.Contains("mode"))
                        {
                            ChangePasswordPopupControl.ContentUrl += "&IsMaster=true";
                        }
                        else
                        {
                            ChangePasswordPopupControl.ContentUrl += "?IsMaster=true";
                        }

                        ChangePasswordPopupControl.ShowCloseButton = false;

                        ChangePasswordPopupControl.ShowOnPageLoad = true;
                    }
                }
            }
        }

        if (!Page.IsPostBack)
        {
            DeleteWidgetMessages.Clear();
            DeleteWidgetMessages.Add("Title", Dropthings.Web.Util.ResourceManager.getResource("DeleteWidgetTitle"));
            DeleteWidgetMessages.Add("Text", Dropthings.Web.Util.ResourceManager.getResource("DeleteWidget"));
            DeleteWidgetMessages.Add("Yes", Dropthings.Web.Util.ResourceManager.getResource("Yes"));
        }

        SetPageSettings();

        hfContentBtnGo.Value = Dropthings.Web.Util.ResourceManager.getResource("btnGoText");
        hfContentWatermark.Value = Dropthings.Web.Util.ResourceManager.getResource("ContentWatermarkText");

        if (DashboardBusiness.Helpers.Security.IsEmployee())
        {
            hfIsEmployee.Value = "1";
        }
        else
        {
            hfIsEmployee.Value = "0";
        }

        Boolean entryError = false;
        var renderCurrentPage = Page.ToString().Replace("ASP.", "").Replace("_", ".");
        if (renderCurrentPage.Equals("dropthings.error.aspx"))
        {
            InMotionGIT.Common.Helpers.LogHandler.TraceLog("Master", renderCurrentPage, "Master");
            TabUpdatePanel.Visible = false;
            PrincipalMenu.Items[1].Visible = false; //LogIn MenuItem
            PrincipalMenu.Items[6].Enabled = false; //Language MenuItem
            PrincipalMenu.Items[7].Enabled = false; //Helper MenuItem
            entryError = true;
        }

        if (!Page.IsPostBack && !string.IsNullOrEmpty(Request.QueryString["login"]))
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "ShowPopupControl",
                                               "<script type=text/javascript>ShowPopupControl();</script>", false);
        }

        if (!Page.IsPostBack && entryError == false && renderCurrentPage.Equals("dropthings.error.aspx") == false && Session["IsFirtView"] != null && (Boolean)Session["IsFirtView"] == false)
        {
            if (securityMod == EnumSecurityMode.HeaderAuthentication)
            {
                string RevelationHeaderText = GetGlobalResourceObject("Resource", "RevelationHeaderText").ToString();
                ContractPopupControl.HeaderText = RevelationHeaderText;
                ContractPopupControl.ShowHeader = true;
                ContractPopupControl.ShowOnPageLoad = true;
            }
        }
    }

    private void SetPageSettings()
    {
        Boolean settingValue;
        VisualTIME config = (VisualTIME)ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection");

        settingValue = config.Authentification.ChangePasswordEnabled;
        if (settingValue)
            PrincipalMenu.Items[3].Enabled = true; //ChangePassword
        else
            PrincipalMenu.Items[3].Enabled = false; //ChangePassword

        settingValue = config.Authentification.ProfileEnabled;
        if (settingValue)
            PrincipalMenu.Items[0].Enabled = true;//Profile
        else
            PrincipalMenu.Items[0].Enabled = false;//Profile

        settingValue = config.Authentification.PasswordRecoveryVisible;
        if (settingValue)
            LogInPopupControl.FindControl("ForgotPasswordHyperLink").Visible = true;
        else
            LogInPopupControl.FindControl("ForgotPasswordHyperLink").Visible = false;

        settingValue = config.Authentification.RegisterVisible;
        if (settingValue)
            LogInPopupControl.FindControl("RegisterHyperLink").Visible = true;
        else
            LogInPopupControl.FindControl("RegisterHyperLink").Visible = false;

        EnumSecurityMode securityMode = config.Security.Mode;

        if (securityMode == EnumSecurityMode.Windows)
        {
            PrincipalMenu.Items[3].Visible = false;
            PrincipalMenu.Items[1].Visible = false;
            PrincipalMenu.Items[2].Visible = false;
        }

        if (config.Security.Mode == EnumSecurityMode.HeaderAuthentication || config.Security.Mode == EnumSecurityMode.Windows || config.Security.Mode == EnumSecurityMode.ActiveDirectory)
        {
            PrincipalMenu.Items[3].Visible = false;
        }
    }

    private void addNewTabLinkButton_Click2(object sender, EventArgs e)
    {
        Response.Redirect("Default3.aspx");
    }

    private void SetProfileLanguageID(ref InMotionGIT.Membership.Providers.MemberContext profile)
    {
        string CulturaName = InMotionGIT.Common.Proxy.Helpers.Language.GetLanguageNameByCultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToLower());
        profile.LanguageId = InMotionGIT.Common.Proxy.Helpers.Language.GetLanguageIdCurrentContext(CulturaName);
        profile.Language = CulturaName;
        InMotionGIT.Membership.Providers.MemberContext context = new InMotionGIT.Membership.Providers.MemberContext();
        context.UpdateInformation(profile.UserName);
    }

    protected void LanguageMenu_ItemClick(object sender, MenuItemEventArgs e)
    {
        List<InMotionGIT.Common.DataType.LookUpValue> CultureInfoEnable = InMotionGIT.Common.Proxy.Helpers.Language.GetAllCultureInfoName();
        List<InMotionGIT.Common.DataType.LookUpValue> languagesValue = InMotionGIT.Common.Proxy.Helpers.Language.LookUpLanguageByCurrentInfo();

        int currentLanguageId = 0;
        string currentLanguage = string.Empty;
        if (e.Item.Items.Count == 0)
        {
            e.Item.Items.Clear();
            DevExpress.Web.ASPxMenu.MenuItem newMenuItem;

            foreach (InMotionGIT.Common.DataType.LookUpValue languageItem in languagesValue)
            {
                try
                {
                    currentLanguage = languageItem.Description;
                }
                catch
                {
                    currentLanguageId = 1;
                }

                if (string.Equals(currentLanguage, e.Item.Text))
                {
                    currentLanguageId = int.Parse(languageItem.Code);
                    break;
                }
            }

            currentLanguageId = InMotionGIT.Common.Proxy.Helpers.Language.ExistCode(currentLanguageId);

            List<InMotionGIT.Common.DataType.LookUpValue> languagesValueUpdate = InMotionGIT.Common.Proxy.Helpers.Language.LookUpLanguageByCurrentInfo(currentLanguageId);

            foreach (InMotionGIT.Common.DataType.LookUpValue languageItem in languagesValueUpdate)
            {
                if (string.Equals(languageItem.Code, currentLanguageId.ToString()))
                {
                    e.Item.Text = languageItem.Description;
                }
                else
                {
                    newMenuItem = new DevExpress.Web.ASPxMenu.MenuItem();
                    newMenuItem.Name = string.Format("{0}MenuItem", languageItem.Description);
                    try
                    {
                        newMenuItem.Text = languageItem.Description;
                        e.Item.Items.Add(newMenuItem);
                    }
                    catch
                    {
                        newMenuItem = null;
                    }
                }
            }

            string cultureInfoNameTemporal = InMotionGIT.Common.Proxy.Helpers.Language.GetCultureInfoByCode(currentLanguageId); ;


            InMotionGIT.Common.Helpers.LogHandler.TraceLog("ChangeLanguage", string.Format("LanguageId:{0},Language:{1}", currentLanguageId, cultureInfoNameTemporal));

            ProfileInfo.LanguageId = currentLanguageId;
            ProfileInfo.Language = cultureInfoNameTemporal;

            UpdateLanguageUser(ProfileInfo);

            if (Page.IsCallback)
            {
                DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback(Request.FilePath);
            }
            else
            {
                Response.Redirect(Request.FilePath);
            }
        }
    }

    public void UpdateLanguageUser(InMotionGIT.Membership.Providers.MemberContext user)
    {
        InMotionGIT.Common.Helpers.LogHandler.TraceLog("ChangeLanguage", string.Format("ProfileInfo.LanguageId:{0},ProfileInfo.Language:{1}, user.LanguageId:{2},user.Language:{3}", ProfileInfo.LanguageId , ProfileInfo.Language, user .LanguageId, user.Language));
        ProfileInfo.UpdateInformation(user.UserName, user.LanguageId , user.Language   );
    }

    protected void CancelBtnOnClick(object source, EventArgs e)
    {
        Response.Redirect("~/dropthings/LogOff.aspx?Revelation=False");
    }
}