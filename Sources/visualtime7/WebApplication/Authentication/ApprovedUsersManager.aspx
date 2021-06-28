<%@ Page Title="" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false"
    meta:resourcekey="PageResource" CodeFile="ApprovedUsersManager.aspx.vb" Inherits="Authentication_ApprovedUsersManager" %>

<%@ Register Src="AccountInformation.ascx" TagName="AccountInformation" TagPrefix="uc2" %>
<%@ Register Src="PersonalInformationControl.ascx" TagName="PersonalInformationControl"
    TagPrefix="uc3" %>
<%@ Register Src="UserPreferencesControl.ascx" TagName="UserPreferencesControl" TagPrefix="uc6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        //Shows the activity information popup

        // BEGIN CONFIRMDELETE  
        function DeleteButton_Click(s, e) {
            if (GridViewUsers.GetSelectedRowCount() != 0) {
                popupDelete.Show();
                btnYes.Focus();
            }
        }
        function btnYes_Click(s, e) {
            popupDelete.Hide();
            GridViewUsers.PerformCallback('delete');
            GridViewUsers.UnselectRows();
        }
        function btnNo_Click(s, e) {
            popupDelete.Hide();
        }
        // END CONFIRMDELETE
 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <dxrp:ASPxRoundPanel ID="InformationRoundPanel" runat="server" Width="100%"
        HeaderText="Here we show the users that are not approved by the administrator."
        meta:resourcekey="InformationRoundPanelResource">
        <PanelCollection>
            <dxp:PanelContent ID="PanelContent1" runat="server">
                <table style='width: 100%;'>
                    <tr valign='top'>
                        <td>
                            <dxe:ASPxButton ID="btnDelete" ClientInstanceName="btnDelete" EnableDefaultAppearance="False"
                                runat="server" AutoPostBack="False" Width="35px" Height="30px" meta:resourcekey="btnDeleteResource"
                                ToolTip="Eliminar información" EnableTheming="False">
                                <ClientSideEvents Click="DeleteButton_Click" />
                                <Image Url="~/images/generaluse/ConfirmDelete/btnDeleteoff.gif" UrlChecked="~/images/generaluse/ConfirmDelete/btnDeleteon.gif"
                                    UrlPressed="~/images/generaluse/ConfirmDelete/btnDeleteon.gif" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dxwgv:ASPxGridView ID="UsersGridView" runat="server" AutoGenerateColumns="False"
                                KeyFieldName="OriginalUserName" ClientInstanceName='GridViewUsers' Caption='Users Manager'
                                Width="100%" EnableRowsCache="False" meta:resourcekey="UsersGridViewResource">
                                <SettingsPager PageSize="20">
                                </SettingsPager>
                                <SettingsBehavior AllowFocusedRow="True" />
                                <SettingsEditing Mode="EditFormAndDisplayRow" PopupEditFormWidth="800px" PopupEditFormModal="true"
                                    PopupEditFormHorizontalAlign="WindowCenter" PopupEditFormVerticalAlign="WindowCenter"
                                    PopupEditFormShowHeader="True" />
                                <Settings ShowPreview="True" ShowFilterBar="Visible" />
                                <Columns>
                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" ButtonType="Image" Width="50px" ShowSelectCheckbox="True">
                                        <EditButton Visible="True">
                                            <Image Url="~/images/generaluse/edit.gif" />
                                        </EditButton>
                                        <CancelButton>
                                            <Image Url="~/images/generaluse/btncanceloff.gif" />
                                        </CancelButton>
                                        <UpdateButton>
                                            <Image Url="~/images/generaluse/btnacceptoff.gif" />
                                        </UpdateButton>
                                        <DeleteButton Visible="false">
                                            <Image Url="~/images/generaluse/del.gif" />
                                        </DeleteButton>
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="OriginalUserName" Visible="False" meta:resourcekey="OriginalUserNameColumnResource" />
                                    <dxwgv:GridViewDataTextColumn FieldName="UserName" VisibleIndex="1" Visible="True"
                                        ReadOnly="true" meta:resourcekey="UserNameColumnResource">
                                        <EditFormSettings Visible="True" VisibleIndex="1" />
                                        <PropertiesTextEdit MaxLength="30" Width="150px">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataComboBoxColumn FieldName="UserType" VisibleIndex="2" Visible="True"
                                        ReadOnly="true" meta:resourcekey="UserTypeColumnResource">
                                        <EditFormSettings Visible="True" VisibleIndex="2" />
                                        <PropertiesComboBox>
                                            <Items>
                                                <dxe:ListEditItem Text="Agent" Value="Agent" meta:resourcekey="ComboBoxListItemResourceValue1" />
                                                <dxe:ListEditItem Text="Client" Value="Client" meta:resourcekey="ComboBoxListItemResourceValue2" />
                                                <dxe:ListEditItem Text="Employee" Value="Employee" meta:resourcekey="ComboBoxListItemResourceValue3" />
                                            </Items>
                                        </PropertiesComboBox>
                                    </dxwgv:GridViewDataComboBoxColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Email" VisibleIndex="3" Visible="True" meta:resourcekey="EmailColumnResource">
                                        <EditFormSettings Visible="False" VisibleIndex="3" />
                                        <PropertiesTextEdit MaxLength="30" Width="150px">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataDateColumn FieldName="CreationDate" VisibleIndex="4" Visible="True"
                                        meta:resourcekey="CreationDateColumnResource">
                                        <EditFormSettings Visible="False" />
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataDateColumn FieldName="LastLoginDate" VisibleIndex="5" Visible="True"
                                        meta:resourcekey="LastLoginDateColumnResource">
                                        <EditFormSettings Visible="False" />
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataDateColumn FieldName="LastActivityDate" VisibleIndex="6" Visible="True"
                                        meta:resourcekey="LastActivityDateColumnResource">
                                        <EditFormSettings Visible="False" />
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataCheckColumn FieldName="IsLockedOut" VisibleIndex="7" Visible="False"
                                        ReadOnly="False" meta:resourcekey="IsLockedOutColumnResource">
                                        <EditFormSettings Visible="True" VisibleIndex="5" />
                                    </dxwgv:GridViewDataCheckColumn>
                                    <dxwgv:GridViewDataDateColumn FieldName="LastLockoutDate" VisibleIndex="8" Visible="True"
                                        meta:resourcekey="LastLockoutDateColumnResource">
                                        <EditFormSettings Visible="False" />
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="PasswordQuestion" VisibleIndex="9" Visible="False"
                                        meta:resourcekey="PasswordQuestionColumnResource">
                                        <EditFormSettings Visible="False" />
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Comment" VisibleIndex="10" Visible="False"
                                        meta:resourcekey="CommentColumnResource">
                                        <EditFormSettings Visible="True" VisibleIndex="3" />
                                        <PropertiesTextEdit MaxLength="100" Width="400px">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataCheckColumn FieldName="IsApproved" VisibleIndex="11" Visible="False"
                                        meta:resourcekey="IsApprovedColumnResource">
                                        <EditFormSettings Visible="True" />
                                    </dxwgv:GridViewDataCheckColumn>
                                    <dxwgv:GridViewDataDateColumn FieldName="LastPasswordChangedDate" VisibleIndex="12"
                                        Visible="False" meta:resourcekey="LastPasswordChangedDateColumnResource">
                                        <EditFormSettings Visible="False" />
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataCheckColumn FieldName="IsOnline" VisibleIndex="13" Visible="False"
                                        ReadOnly="True" meta:resourcekey="IsOnlineColumnResource">
                                        <EditFormSettings Visible="True" VisibleIndex="6" />
                                    </dxwgv:GridViewDataCheckColumn>
                                    <dxwgv:GridViewDataColumn Caption="Profile" VisibleIndex="14" Width="30px" meta:resourcekey="ProfileColumnResource">    
                                        <DataItemTemplate>
                                            <center>
                                            <dxe:ASPxButton ID="btnTemplate" runat="server" CommandArgument='<%# Container.VisibleIndex%>'
                                                OnClick="btnTemplate_Click" BackgroundImage-ImageUrl="~/images/generaluse/user.png"
                                                BackgroundImage-HorizontalPosition="center" BackgroundImage-VerticalPosition="center"
                                                Height="20px" Width="20px" ToolTip="" meta:resourcekey="btnTemplateResource"/>
                                                </center>
                                        </DataItemTemplate>
                                    </dxwgv:GridViewDataColumn>
                                    
                                </Columns>
                            </dxwgv:ASPxGridView>
                        </td>
                    </tr>
                </table>
            </dxp:PanelContent>
        </PanelCollection>
    </dxrp:ASPxRoundPanel>
    <dxpc:ASPxPopupControl ID="popupUserProfileData" runat="server" Width="665px" AllowDragging="True"
        AllowResize="True" ClientInstanceName="popupUserProfileData" DragElement="Window"
        HeaderText="User Profile Data" Modal="True" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" meta:resourcekey="popupUserProfileDataResource">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server"
                meta:resourcekey="PopupControlContentControlResource1">
                <dxrp:ASPxRoundPanel ID="AccountInformationRoundPanel" runat="server" Width="100%"
                    HeaderText="Account Information" meta:resourcekey="AccountInformationRoundPanelResource"
                    Enabled="false">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent2" runat="server">
                            <uc2:AccountInformation ID="AccountInformation1" runat="server" OnNavigation="Navigation" />
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxrp:ASPxRoundPanel>
                <dxrp:ASPxRoundPanel ID="PersonalInformationRoundPanel" runat="server" Width="100%"
                    HeaderText="Personal Information" meta:resourcekey="PersonalInformationRoundPanelResource"
                    Enabled="false">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent3" runat="server">
                            <uc3:PersonalInformationControl ID="PersonalInformation1" runat="server" OnNavigation="Navigation" />
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxrp:ASPxRoundPanel>
                <dxrp:ASPxRoundPanel ID="PreferencesRoundPanel" runat="server" Width="100%" meta:resourcekey="PreferencesRoundPanelResource"
                    HeaderText="Preferences" Visible="false" Enabled="false">
                    <PanelCollection>
                        <dxp:PanelContent ID="PanelContent6" runat="server">
                            <uc6:UserPreferencesControl ID="UserPreferencesControl1" runat="server" OnNavigation="Navigation" />
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxrp:ASPxRoundPanel>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ID="popupDelete" runat="server" ClientInstanceName="popupDelete" Modal="true">
        <HeaderTemplate>
            <div>
                <asp:Literal ID="popupMessageHeader" runat="server" Text="Confirmación de Borrado"
                    meta:resourcekey="popupMessageHeaderResource"></asp:Literal></div>
        </HeaderTemplate>
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <div style="width: 350px">
                    <table>
                        <tr>
                            <td rowspan="2">
                                <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/images/generaluse/ConfirmDelete/Question.png">
                                </dxe:ASPxImage>
                            </td>
                            <td>
                                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" meta:resourcekey="ASPxLabel1Resource"
                                    Text="¿Está seguro de querer eliminar las filas seleccionadas?">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table>
                        <tr>
                            <td>
                            </td>
                            <td style="width: 100%">
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btnYes" runat="server" Width="50px" AutoPostBack="False" ClientInstanceName="btnYes"
                                    EnableDefaultAppearance="False" EnableTheming="False">
                                    <Image Url="~/images/generaluse/ConfirmDelete/btnacceptoff.gif" UrlChecked="~/images/generaluse/ConfirmDelete/btnaccepton.gif"
                                        UrlPressed="~/images/generaluse/ConfirmDelete/btnaccepton.gif" />
                                    <ClientSideEvents Click="btnYes_Click" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btnNo" runat="server" Width="50px" AutoPostBack="False" ClientInstanceName="btnNo"
                                    EnableDefaultAppearance="False" EnableTheming="False">
                                    <Image Url="~/images/generaluse/ConfirmDelete/btncanceloff.gif" UrlChecked="~/images/generaluse/ConfirmDelete/btncancelon.gif"
                                        UrlPressed="~/images/generaluse/ConfirmDelete/btncancelon.gif" />
                                    <ClientSideEvents Click="btnNo_Click" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
</asp:Content>
