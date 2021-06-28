<%@ Page Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false"
    CodeFile="UserRegister.aspx.vb" Inherits="Authentication_UserRegister" Title="User Information"
    meta:resourcekey="PageResource" %>

<%@ Register Src="UserType.ascx" TagName="UserType" TagPrefix="uc1" %>
<%@ Register Src="AccountInformation.ascx" TagName="AccountInformation" TagPrefix="uc2" %>
<%@ Register Src="PersonalInformationControl.ascx" TagName="PersonalInformationControl"
    TagPrefix="uc3" %>
<%@ Register Src="ContractOwnerInformationControl.ascx" TagName="ContractOwnerInformationControl"
    TagPrefix="uc4" %>
<%@ Register Src="TermsOfUseUserControl.ascx" TagName="TermsOfUseUserControl" TagPrefix="uc5" %>
<%@ Register Src="UserPreferencesControl.ascx" TagName="UserPreferencesControl" TagPrefix="uc6" %>
<%@ Register Src="ProducerInformationControl.ascx" TagName="ProducerInformationControl"
    TagPrefix="uc7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="StepMultiView" runat="server" ActiveViewIndex="0">
                <asp:View ID="UserTypeView" runat="server">
                    <uc1:UserType ID="UserType1" runat="server" OnNavigation="Navigation" />
                </asp:View>
                <asp:View ID="UserDetailView" runat="server">
                    <dxrp:ASPxRoundPanel ID="AccountInformationRoundPanel" runat="server" Width="100%"
                        HeaderText="Account Information" meta:resourcekey="AccountInformationRoundPanelResource">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <uc2:AccountInformation ID="AccountInformation1" runat="server" OnNavigation="Navigation" />
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                    <dxrp:ASPxRoundPanel ID="PersonalInformationRoundPanel" runat="server" Width="100%"
                        HeaderText="Personal Information" meta:resourcekey="PersonalInformationRoundPanelResource">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <uc3:PersonalInformationControl ID="PersonalInformation1" runat="server" OnNavigation="Navigation" />
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                    <dxrp:ASPxRoundPanel ID="ContractOwnerInformationRoundPanel" runat="server" Width="100%"
                        HeaderText="Contract Owner Information" meta:resourcekey="ContractOwnerInformationRoundPanelResource">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <uc4:ContractOwnerInformationControl ID="ContractOwnerInformationControl1" runat="server"
                                    OnNavigation="Navigation" />
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                    <dxrp:ASPxRoundPanel ID="ProducerInformationRoundPanel" runat="server" Width="100%"
                        HeaderText="Producer Information" meta:resourcekey="ProducerInformationRoundPanelResource">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <uc7:ProducerInformationControl ID="ProducerInformationControl1" runat="server" OnNavigation="Navigation" />
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                    <dxrp:ASPxRoundPanel ID="TermsOfUseRoundPanel" runat="server" Width="100%" meta:resourcekey="TermsOfUseRoundPanelResource"
                        HeaderText="Term of Use">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <uc5:TermsOfUseUserControl ID="TermsOfUseUserControl1" runat="server" OnNavigation="Navigation" />
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                    <dxrp:ASPxRoundPanel ID="PreferencesRoundPanel" runat="server" Width="100%" meta:resourcekey="PreferencesRoundPanelResource"
                        HeaderText="Preferences" Visible="false">
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                                <uc6:UserPreferencesControl ID="UserPreferencesControl1" runat="server" OnNavigation="Navigation" />
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
                    <table width="100%">
                        <tr>
                            <td width="50%" align="right">
                                <dxe:ASPxButton ID="CancelButton" runat="server" Text="Cancel" AutoPostBack="false"
                                    CausesValidation="false" OnClick="CancelButton_Click" meta:resourcekey="CancelButtonResource">
                                </dxe:ASPxButton>
                            </td>
                            <td width="50%" align="left">
                                <dxe:ASPxButton ID="RegisterButton" runat="server" Text="Register" AutoPostBack="True"
                                    meta:resourcekey="RegisterButtonResource">
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                    <dxpc:ASPxPopupControl ShowPageScrollbarWhenModal="true" ID="popupMessageControl"
                        runat="server" ClientInstanceName="popupMessageControl" ShowCloseButton="False"
                        EnableHotTrack="False" CloseAction="None" Modal="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" Width="200px" HeaderText="Notification">
                        <SizeGripImage Height="16px" Width="16px" />
                        <CloseButtonImage Height="12px" Width="13px" />
                        <HeaderStyle>
                            <Paddings PaddingRight="6px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dxpc:PopupControlContentControl>
                                <table width="100%">
                                    <tr>
                                        <td width="100%" colspan="2" align="center">
                                            <dxe:ASPxLabel runat="server" ClientInstanceName="lblMessage" ID="lblMessage" Text="In a few minutes you will receive your password by email." />
                                            <br />
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="50%" align="center">
                                            <dxe:ASPxButton ID="btnConfirm" runat="server" AutoPostBack="False" ClientInstanceName="btnConfirm"
                                                Text="Ok">
                                                <ClientSideEvents Click="function(s, e) {popupMessageControl.Hide();} " />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </dxpc:PopupControlContentControl>
                        </ContentCollection>
                    </dxpc:ASPxPopupControl>
                </asp:View>
                <asp:View ID="MessageView" runat="server">
                    <br />
                    <table width="100%">
                        <tr>
                            <td width="100%" colspan="2" align="center">
                                <dxe:ASPxLabel runat="server" ClientInstanceName="lblLoginMessage" ID="lblLoginMessage"
                                    Text="In a few minutes you will receive your password by email." meta:resourcekey="lblLoginMessageResource" />
                                <br />
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" colspan="2" align="center">
                                <dxe:ASPxButton ID="RedirectFormBtn" ClientInstanceName="RedirectFormBtn" runat="server" Text="Ok" AutoPostBack="True" />
                                <br />
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
            <dx:ASPxHiddenField ID="UserData" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>