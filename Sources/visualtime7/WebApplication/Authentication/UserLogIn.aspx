<%@ Page Language="VB" AutoEventWireup="true" meta:resourcekey="PageResource" CodeFile="UserLogIn.aspx.vb"
    Inherits="Authentication_UserLogIn" Title="Sign in with your account" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/jquery-ui-1.11.4.css" rel="stylesheet">
    <link href="../Styles/fasi.css" rel="stylesheet" />

    <!--JQuery Toast-->
    <link href="../Styles/jquery.toast.css" rel="stylesheet" />

    <style type="text/css">
        .auto-style1 {
            height: 1%;
        }
    </style>
</head>
<body style="padding: 15px; margin: 0px; background-image: none;">
    <form id="LoginForm" runat="server">
        <div align="center">
            <asp:MultiView ID="StepMultiView" runat="server" ActiveViewIndex="0">
                <asp:View ID="LoginView" runat="server">
                    <table style="height: 65px; width: 320px;" border="0">
                        <tr align="center" valign='top' style="height: 1%">
                            <td width="120px">
                                <div align="left">
                                    <dxe:ASPxLabel ID="EmailAddressLabel" runat="server" Text="E-mail" meta:resourcekey="EmailAddressLabelResource"
                                        AssociatedControlID="EmailAddressTextBox">
                                    </dxe:ASPxLabel>
                                </div>
                            </td>
                            <td align="left" width="200px">
                                <dxe:ASPxTextBox ID="EmailAddressTextBox" ClientEnabled="true" runat="server" Paddings-PaddingLeft="8px"
                                    meta:resourcekey="EmailAddressTextBoxResource" Width="200px">
                                    <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                                        Repeat="NoRepeat" VerticalPosition="center" />
                                    <Paddings PaddingLeft="8px"></Paddings>
                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField IsRequired='True' ErrorText='The e-mail address is required' />
                                        <RegularExpression ValidationExpression="\w+([-+.']\w+)*@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]\s*$"
                                            ErrorText="The format is invalid" />
                                    </ValidationSettings>
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr align="center" valign='top' style="height: 1%">
                            <td width="120px">
                                <div align="left">
                                    <dxe:ASPxLabel ID="PasswordLabel" runat="server" Text="Password" meta:resourcekey="PasswordLabelResource"
                                        AssociatedControlID="PasswordTextBox">
                                    </dxe:ASPxLabel>
                                </div>
                            </td>
                            <td align="left" width="200px">
                                <dxe:ASPxTextBox ID="PasswordTextBox" runat="server" ClientEnabled="True" Password="True" Paddings-PaddingLeft="8px"
                                    Width="200px" meta:resourcekey="PasswordTextBoxResource">
                                    <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                                        Repeat="NoRepeat" VerticalPosition="center" />
                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField IsRequired='True' ErrorText='The password is required' />
                                    </ValidationSettings>
                                </dxe:ASPxTextBox>
                                <dxe:ASPxTextBox ID="SavedPasswordTextBox" runat="server" Paddings-PaddingLeft="8px"
                                    Width="200px" Text="**********" Visible="false" Enabled="false">
                                    <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                                        Repeat="NoRepeat" VerticalPosition="center" />
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr valign="top" style="height: 1%">
                            <td width="120px" align="left">&nbsp;
                            </td>
                            <td align="left">
                                <dxe:ASPxCheckBox ID="RememberCheckBox" ClientEnabled="True" ClientInstanceName="RememberCheckBox" runat="server" Text="Remember me" meta:resourcekey="RememberCheckBoxResource">
                                </dxe:ASPxCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td id="tdCaptcha" colspan="2" style="height: 0px">
                                <div id="captcha_container"></div>
                            </td>
                        </tr>
                        <tr valign="top" style="height: 1%">
                            <td align="left" colspan="2">
                                <dxe:ASPxLabel ID="InvalidLogOnLabel" ClientInstanceName="InvalidLogOnLabel" runat="server"
                                    ClientVisible="false" EnableViewState="False" />
                                <asp:Panel ID="BlokedAccountPanel" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td width="100%" align="center">
                                                <dxe:ASPxLabel ID="LockedAccountLabel" ClientInstanceName="LockedAccountLabel" runat="server"
                                                    EnableViewState="False" Text="The account is locked. You can retrieve your" meta:resourcekey="LockedAccountLabelResource" />
                                                &nbsp;<dxe:ASPxHyperLink ID="PasswordHyperLink" runat="server" AutoPostBack="False"
                                                    ClientInstanceName="PasswordHyperLink" Text="password" NavigateUrl="ForgotPassword.aspx"
                                                    meta:resourcekey="PasswordHyperLinkResource">
                                                </dxe:ASPxHyperLink>
                                                &nbsp;<dxe:ASPxLabel ID="MessageEnterLabel" ClientInstanceName="MessageEnterLabel"
                                                    runat="server" EnableViewState="False" Text="or attempting to enter" meta:resourcekey="MessageEnterLabelResource" />
                                                &nbsp;<dxe:ASPxHyperLink ID="AccountHyperLink" runat="server" AutoPostBack="False"
                                                    meta:resourcekey="AccountHyperLinkResource" ClientInstanceName="AccountHyperLink"
                                                    Text="another account." NavigateUrl="UserLogIn.aspx">
                                                </dxe:ASPxHyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr align="center" valign='top' style="height: 1%">
                            <td width="120px">
                                <div align="left">
                                    <dxe:ASPxLabel ID="CompanyLabel" ClientEnabled="true" ClientInstanceName="CompanyLabel" runat="server" Text="Company" meta:resourcekey="CompanyLabelResource"
                                        AssociatedControlID="CompanyComboBox" ClientVisible="false">
                                    </dxe:ASPxLabel>
                                </div>
                            </td>
                            <td align="left" width="200px">
                                <dxe:ASPxComboBox ID="CompanyComboBox" ClientEnabled="true" ClientInstanceName="CompanyComboBox" runat="server" ClientIDMode="AutoID" Paddings-PaddingLeft="8px"
                                    Width="200px" DropDownRows="12" ValueType="System.Int32" ClientVisible="false" meta:resourcekey="CompanyComboBoxResource">
                                    <BackgroundImage HorizontalPosition="left" ImageUrl="../images/generaluse/required.PNG"
                                        Repeat="NoRepeat" VerticalPosition="center" />
                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ErrorTextPosition="Bottom">
                                        <RequiredField IsRequired='True' ErrorText='The company is required' />
                                    </ValidationSettings>
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr align="center" valign='top' style="height: 1%">
                            <td width="120px">&nbsp;
                            </td>
                            <td align="left" width="200px">
                                <table width="100%" border="0">
                                    <tr align="center">
                                        <td width="50%" align="right">
                                            <dxe:ASPxButton ID="LoginButton" runat="server" ClientEnabled="true" ClientInstanceName="btnLogin" meta:resourcekey="LoginButtonResource"
                                                Text="Login" EnableClientSideAPI="True">
                                                <ClientSideEvents Click="function(s, e) {
	                                                                                        LogIn(e);
                                                                                        }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td width="50%" align="left">
                                            <dxe:ASPxButton ID="CancelButton" runat="server" Text="Cancel" AutoPostBack="true"
                                                CausesValidation="false" meta:resourcekey="CancelButtonResource">
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="MessageView" runat="server">
                    <table width="100%" border="0" cellpadding="5" cellspacing="10">
                        <tr>
                            <td width="100%" align="justify">
                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblMessage" ID="lblMessage" Text="" />
                            </td>
                        </tr>
                        <tr align="right">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxButton ID="AcceptBtn" ClientEnabled="true" ClientInstanceName="btnAccept" runat="server" Text="Accept" meta:resourcekey="AcceptBtnResource" />
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="CancelBtn" runat="server" Text="Cancel" AutoPostBack="true" CausesValidation="false"
                                                meta:resourcekey="CancelButtonResource" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </div>
    </form>

    <script src="../Scripts/jquery.min.js" type="text/jscript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/jscript"></script>
    <script src="../Scripts/master-page.js" type="text/jscript"></script>
    <script src="../Scripts/UserLogIn.js" type="text/jscript"></script>
    <script src='https://www.google.com/recaptcha/api.js?hl=es&onload=loadCaptcha&render=explicit'></script>

    <!--JQuery Toast-->
    <script src="../Scripts/jquery.toast.js" type="text/jscript"></script>

    <script type="text/javascript" language="javascript">

        var urlBase = window.location.protocol + '//' + window.location.host + '/Authentication/UserLogIn.aspx';

        function PupulationCompanyCombox() {
            var urlAction = urlBase + "/CompanyLookUp";
            $.ajax({
                url: urlAction,
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (CompanyComboBox.GetItemCount() == 0) {
                        if (data.d.length != 0) {
                            for (var i = 0; i < data.d.length; i++) {
                                CompanyComboBox.AddItem(data.d[i].Name, parseInt(data.d[i].Identification));
                            }
                            CompanyComboBox.SetSelectedIndex(0);
                        }
                    }
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        }

        function LogIn(e) {
            window.parent.ShowLoadingPanel(true);
            SetSetting(false);
            e.processOnServer = false;
            var urlAction = urlBase + "/LogIn";
            var isVisibleCompany = CompanyComboBox.GetVisible();
            var companyId = 0;
            if (isVisibleCompany == true) {
                companyId = CompanyComboBox.GetSelectedItem().value;
            }
            var param = JSON.stringify({ user: EmailAddressTextBox.GetValue(), password: PasswordTextBox.GetValue(), Remember: RememberCheckBox.GetChecked(), isVisibleCompany: isVisibleCompany, companyId: companyId });
            $.ajax({
                url: urlAction,
                data: param,
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.d.State === true) {

                        window.parent.ShowLoadingPanel(false);
                        SetSetting(true);
                        CompanyLabel.SetVisible(false);
                        CompanyComboBox.SetVisible(false);
                        InvalidLogOnLabel.SetVisible(false);
                        if (data.d.Message == "") {
                            if (data.d.IsAuthenticated == true) {
                                if (data.d.ShowStartUpMessage == true) {
                                    window.parent.HidePopupControl(true, data.d.Url, 2000);
                                } else {
                                    window.parent.HidePopupControl(true, data.d.Url, 0);
                                }

                            } else {
                                if (data.d.IsMultiCompany == true) {
                                    CompanyLabel.SetVisible(true);
                                    CompanyComboBox.SetVisible(true);
                                    PupulationCompanyCombox();
                                }
                                else {
                                    CompanyLabel.SetVisible(false);
                                    CompanyComboBox.SetVisible(false);
                                }
                            }
                        }
                        else {
                            SetSetting(true);
                            if (data.d.IsShowRecaptcha) {
                                window.parent.ResizePopupWithCaptcha();
                                createCaptcha(btnLogin);
                                btnLogin.SetEnabled(false);
                            } else {
                                if (!data.d.SecurityValidate) {
                                    InvalidLogOnLabel.SetVisible(true);
                                    InvalidLogOnLabel.SetText(data.d.Message);
                                    btnLogin.SetEnabled(false);
                                }
                                else {
                                    InvalidLogOnLabel.SetVisible(true);
                                    InvalidLogOnLabel.SetText(data.d.Message);
                                }

                            }
                        }

                    }
                    else {
                        window.parent.ShowLoadingPanel(false);
                        btnLogin.SetEnabled(true);
                        $.toast({
                            heading: 'Error',
                            hideAfter: '5000',
                            position: 'mid-center',
                            text: data.d.Message,
                            showHideTransition: 'slide',
                            icon: 'error'
                        })
                    }
                },
                error: function (response) {
                    window.parent.ShowLoadingPanel(false);
                    alert(response.responseText);
                    SetSetting(true);
                }
            });

        }

        function SetSetting(isEnable) {
            PasswordTextBox.SetEnabled(isEnable);
            EmailAddressTextBox.SetEnabled(isEnable);
            btnLogin.SetEnabled(isEnable);
            window.parent.HideRegisterOption();
        }

        $(document).ready(function () {

        });
    </script>
</body>
</html>