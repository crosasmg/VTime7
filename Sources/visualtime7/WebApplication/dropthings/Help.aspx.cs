// Copyright (c) Omar AL Zabir. All rights reserved.
// For continued development and updates, visit http://msmvps.com/omar

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using InMotionGIT.Core.Configuration;
using InMotionGIT.Core.Configuration.Enumerations;
using System.Web.Profile;
using Dropthings.Web.Framework;
using InMotionGIT.Common.Extensions; 

public partial class HelpWebForm : System.Web.UI.Page
{



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

    }
    protected override void InitializeCulture()
    {

        if (UserInfo.Language != string.Empty)
        {
            UICulture = UserInfo.Language;
        }
        base.InitializeCulture();
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if ((UserInfo.Theme != string.Empty) && (UserInfo.Theme != "None"))
        {
            Page.Theme = UserInfo.Theme;
        }
        else
        {
            Page.Theme = null;
        }
    }
}
