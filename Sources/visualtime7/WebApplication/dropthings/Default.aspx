<%@ Page Language="C#" UICulture="auto" MasterPageFile="~/DropthingsMasterPage.master"
    AutoEventWireup="False" CodeFile="Default.aspx.cs" Inherits="DefaultWebForm" %>

<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ OutputCache Location="None" NoStore="true" %>
<%@ Register Assembly="CustomDragDrop" Namespace="CustomDragDrop" TagPrefix="cdd" %>
<%@ Register Src="WidgetContainer.ascx" TagName="WidgetContainer" TagPrefix="widget" %>
<%@ Register Src="WidgetPanels.ascx" TagName="WidgetPanels" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function btnYes_Click(s, e) {
            popupExpired.Hide();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel ID="OnPageMenuUpdatePanel" runat="server">
        <ContentTemplate>
            <div id="onpage_menu_bar" onmouseover="this.className='onpage_menu_bar_hover'" onmouseout="this.className=''">
                <asp:LinkButton CssClass="onpage_menu_action" ID="ShowAddContentPanel" runat="server"
                    Text="<%$ Resources:Resource, AddStuff %>" OnClick="ShowAddContentPanel_Click" />
                <asp:LinkButton CssClass="onpage_menu_action" ID="HideAddContentPanel" runat="server"
                    Text="<%$ Resources:Resource, HideStuff %>" OnClick="HideAddContentPanel_Click"
                    Visible="False" />
                <asp:LinkButton ID="ChangePageTitleLinkButton" CssClass="ChangePageTitleLinkButton"
                    Text="<%$ Resources:Resource, ChangeSettings %>" runat="server" OnClick="ChangeTabSettingsLinkButton_Clicked" />
            </div>
            <div id="onpage_menu_panels">
                <asp:Panel ID="ChangePageSettingsPanel" runat="server" Visible="false" CssClass="onpage_menu_panel">
                    <div class="onpage_menu_panel_column">
                        <h1>
                            <%= Dropthings.Web.Util.ResourceManager.getResource("ChangeTabTitle")%></h1>
                        <p>
                            <%= Dropthings.Web.Util.ResourceManager.getResource("Title")%>
                            <asp:TextBox ID="NewTitleTextBox" runat="server" />
                            <asp:Button ID="SaveNewTitleButton" runat="server" OnClick="SaveNewTitleButton_Clicked"
                                Text="<%$ Resources:resource, Save %>" />
                        </p>
                    </div>
                    <div class="onpage_menu_panel_column">
                        <h1>
                            <%= Dropthings.Web.Util.ResourceManager.getResource("DeleteTab")%></h1>
                        <p>
                            <%= Dropthings.Web.Util.ResourceManager.getResource("DeleteTabConfirmation")%><asp:Button
                                ID="DeleteTabLinkButton" runat="server" OnClick="DeleteTabLinkButton_Clicked"
                                Text="<%$ Resources:Resource, Yes %>" />
                        </p>
                    </div>
                    <div class="onpage_menu_panel_column">
                        <h1>
                            <%= Dropthings.Web.Util.ResourceManager.getResource("ChangeColumns")%></h1>
                        <p>
                            <%= Dropthings.Web.Util.ResourceManager.getResource("ChooseColumnLayout")%><br />
                            <input id="SelectLayoutPopup_Type1" type="image" value="1" src="../images/dropthings/Layout1.jpg"
                                onclick="DropthingsUI.Actions.changePageLayout(1)" />
                            <input id="SelectLayoutPopup_Type2" type="image" value="2" src="../images/dropthings/Layout2.jpg"
                                onclick="DropthingsUI.Actions.changePageLayout(2)" />
                            <input id="SelectLayoutPopup_Type3" type="image" value="3" src="../images/dropthings/Layout3.jpg"
                                onclick="DropthingsUI.Actions.changePageLayout(3)" />
                            <input id="SelectLayoutPopup_Type4" type="image" value="4" src="../images/dropthings/Layout4.jpg"
                                onclick="DropthingsUI.Actions.changePageLayout(4)" />
                        </p>
                    </div>
                </asp:Panel>
                <asp:Panel ID="AddContentPanel" runat="Server" CssClass="onpage_menu_panel widget_showcase"
                    Visible="false">
                    <p class="addcontent_message">
                        <%= Dropthings.Web.Util.ResourceManager.getResource("AddingItem")%>
                    </p>
                    <div class="addcontent_navigation">
                        <asp:LinkButton ID="WidgetListPreviousLinkButton" runat="server" Visible="false"
                            Text="&lt; Previous" OnClick="WidgetListPreviousLinkButton_Click" />
                        -
                        <asp:LinkButton ID="WidgetListNextButton" runat="server" Visible="false" Text="Next &gt;"
                            OnClick="WidgetListNextButton_Click" />
                    </div>
                    <asp:DataList ID="WidgetDataList" runat="server" RepeatDirection="Vertical" RepeatColumns="5"
                        RepeatLayout="Table" CellPadding="3" CellSpacing="3" EnableViewState="False"
                        ShowFooter="False" ShowHeader="False" Width="100%">
                        <ItemTemplate>
                            <asp:Image ID="Icon" ImageAlign="AbsMiddle" runat="server" ImageUrl='<%# Eval("Icon") %>' />&nbsp;<asp:LinkButton
                                CommandArgument='<%# Eval("ID") %>' CommandName="AddWidget" ID="AddWidget" runat="server" ToolTip='<%# Eval("Description") + " (" + Eval("Url") + ")" %>'><%# Eval("Name") %></asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="contents_wrapper">
        <div id="widget_area">
            <div id="widget_area_wrapper">
                <asp:UpdatePanel ID="UpdatePanelLayout" runat="server" UpdateMode="conditional">
                    <ContentTemplate>
                        <uc3:WidgetPanels ID="WidgetPanelsLayout" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ModalBackgroundStyle-BackgroundImage-HorizontalPosition="center" ID="popupExpired"
        runat="server" ClientInstanceName="popupExpired" Modal="true" EnableHotTrack="False">
        <ModalBackgroundStyle>
            <BackgroundImage HorizontalPosition="center" VerticalPosition="center" />
        </ModalBackgroundStyle>
        <SizeGripImage Height="16px" Width="16px" />
        <CloseButtonImage Height="12px" Width="13px" />
        <HeaderTemplate>
            <div>
                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:resource, SessionExpired %>"></asp:Literal>
            </div>
        </HeaderTemplate>
        <HeaderStyle>
            <Paddings PaddingRight="6px" />
        </HeaderStyle>
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server"
                EnableHotTrack="False">
                <div style="width: 350px">
                    <table>
                        <tr>
                            <td rowspan="2">
                                <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/images/generaluse/exclamation.png">
                                </dxe:ASPxImage>
                            </td>
                            <td>
                                <dxe:ASPxLabel ID="popupExpiredLabel" runat="server" Text="<%$ Resources:resource, ExpiredMessage %>">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table>
                        <tr>
                            <td style="width: 100%"></td>
                            <td>
                                <dxe:ASPxButton ID="btnYes" runat="server" Width="50px" AutoPostBack="False" ClientInstanceName="btnYes"
                                    EnableDefaultAppearance="False" EnableTheming="False" CausesValidation="false">
                                    <Image Url="../images/generaluse/btnacceptoff.gif" UrlChecked="../images/generaluse/btnaccepton.gif"
                                        UrlPressed="../images/generaluse/btnaccepton.gif" />
                                    <ClientSideEvents Click="function (s, e) {  popupExpired.Hide(); }" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
</asp:Content>