<%@ Page Title="Widgets Manager" Language="VB" MasterPageFile="~/DropthingsMasterPage.master"
    AutoEventWireup="false" CodeFile="WidgetsManager.aspx.vb" Inherits="dropthings_Admin_WidgetsManager"
    meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function MenuItem_Click(s, e) {
            e.processOnServer = false;
            switch (e.item.name) {
                case 'AddRegisterItem':
                    GridViewWidgets.AddNewRow();
                    break;
                case 'RemoveRegisterItem':
                    if (GridViewWidgets.GetSelectedRowCount() != 0) {
                        popupDelete.Show();
                        btnYes.Focus();
                    }
                    break;
            }
        }


        function OnChange_DeleteAll(s, e) {
            var checked = DeleteAll.GetChecked();
            if (checked == true) {
                DeleteInstance.SetChecked(false);
            }
        }

        function OnChange_DeleteInstance(s, e) {
            var checked = DeleteInstance.GetChecked();
            if (checked == true) {
                DeleteAll.SetChecked(false);
            }
        }


        function btnYes_Click(s, e) {
            popupDelete.Hide();
            var selectedOption = DeleteAll.GetChecked()
            if (selectedOption == true) {
                GridViewWidgets.PerformCallback('delete');
                GridViewWidgets.UnselectRows();
            }
            else {
                GridViewWidgets.PerformCallback('delete_partial');
                GridViewWidgets.UnselectRows();
            }

        }

        function btnNo_Click(s, e) {
            popupDelete.Hide();
        }
        // END CONFIRMDELETE
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="Table1" width="100%" runat='server' style="width: 100%;">
                <tr id="Tr1" runat="server">
                    <td id="Td1" style='width: 100%' runat="server">
                        <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="WidGets Manager"
                            Width="100%" meta:resourcekey="DatosResource" EnableDefaultAppearance="False">
                            <PanelCollection>
                                <dxp:PanelContent ID="PanelContent1" runat="server">
                                    <table style='width: 100%;'>
                                        <tr valign='top'>
                                            <td>
                                                <table>
                                                    <tr style="width: 33px">
                                                        <td colspan="2">
                                                            <dxm:ASPxMenu ID="MainMenu" runat="server" Width="100%" ClientInstanceName="MainMenu">
                                                                <ClientSideEvents ItemClick="function(s, e) {
                                                                                                               MenuItem_Click(s, e);
                                                                                                              }" />
                                                                <Items>
                                                                    <dxm:MenuItem Name="AddRegisterItem" Text="Agregar" meta:resourcekey="AddRegisterItem"
                                                                        Image-UrlDisabled="/images/16x16/Toolbar/disabledAdd.png" Image-Url="/images/16x16/Toolbar/add.png">
                                                                        <ItemStyle Width="5%" />
                                                                    </dxm:MenuItem>
                                                                    <dxm:MenuItem BeginGroup="True" Name="RemoveRegisterItem" Text="Eliminar" Image-UrlDisabled="/images/16x16/Toolbar/disabledDelete.png"
                                                                        Image-Url="/images/16x16/Toolbar/delete.png" meta:resourcekey="RemoveRegisterItem">
                                                                        <ItemStyle Width="5%" />
                                                                    </dxm:MenuItem>
                                                                </Items>
                                                            </dxm:ASPxMenu>
                                                        </td>
                                                    </tr>
                                                    <%--<tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="btnADD" Visible="True" Enabled="True" ClientInstanceName="btnADD"
                                                                EnableDefaultAppearance="False" Image-Url="~/images/generaluse/new.png" runat="server"
                                                                meta:resourcekey="btnADDResource" AutoPostBack="False" Width="35px" Height="30px"
                                                                ToolTip="Agregar un nuevo registro" EnableTheming="False">
                                                                <Image Url="~/images/generaluse/ConfirmDelete/btnacceptoff.gif" UrlChecked="~/images/generaluse/ConfirmDelete/btnaccepton.gif"
                                                                    UrlPressed="~/images/generaluse/ConfirmDelete/btnaccepton.gif" />
                                                                <ClientSideEvents Click="AddButton_Click" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btnDelete" ClientInstanceName="btnDelete" EnableDefaultAppearance="False"
                                                                runat="server" AutoPostBack="False" Width="35px" Height="30px" meta:resourcekey="btnDeleteResource"
                                                                ToolTip="Eliminar información" EnableTheming="False">
                                                                <ClientSideEvents Click="DeleteButton_Click" />
                                                                <Image Url="~/images/generaluse/ConfirmDelete/btnDeleteoff.gif" UrlChecked="~/images/generaluse/ConfirmDelete/btnDeleteon.gif"
                                                                    UrlPressed="~/images/generaluse/ConfirmDelete/btnDeleteon.gif" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>--%>
                                                </table>
                                            </td>
                                            <td></td>
                                            <td style="width: 100%"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <dxwgv:ASPxGridView ID="GridViewWidgets" runat="server" AutoGenerateColumns="False"
                                                    KeyFieldName="ID" ClientInstanceName='GridViewWidgets' Caption='Widgets manager'
                                                    Width="100%" meta:resourcekey="GridViewWidgets" EnableRowsCache="False">
                                                    <SettingsPager PageSize="20">
                                                    </SettingsPager>
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="800px" PopupEditFormModal="true"
                                                        PopupEditFormHorizontalAlign="WindowCenter" PopupEditFormVerticalAlign="WindowCenter"
                                                        PopupEditFormShowHeader="True" />
                                                    <Columns>
                                                        <dxwgv:GridViewCommandColumn VisibleIndex="0" ButtonType="Image" ShowSelectCheckbox="True"
                                                            Width="50px">
                                                            <EditButton Visible="True">
                                                                <Image Url="~/images/generaluse/edit.gif" />
                                                            </EditButton>
                                                            <CancelButton>
                                                                <Image Url="~/images/generaluse/btncanceloff.gif" />
                                                            </CancelButton>
                                                            <UpdateButton>
                                                                <Image Url="~/images/generaluse/btnacceptoff.gif" />
                                                            </UpdateButton>
                                                        </dxwgv:GridViewCommandColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="ID" ReadOnly="True" VisibleIndex="1" Visible="True"
                                                            Width="100px" meta:resourcekey="GridViewDataTextColumn1">
                                                            <PropertiesTextEdit MaxLength="10" Width="100px">
                                                            </PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="Name" VisibleIndex="2" Visible="True" Width="200px"
                                                            meta:resourcekey="GridViewDataTextColumn2">
                                                            <PropertiesTextEdit MaxLength="255">
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings Visible="False" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="Url" VisibleIndex="4" Visible="True"
                                                            Width="250px" meta:resourcekey="GridViewDataComboBoxColumn4">
                                                            <PropertiesComboBox TextField="WidgetPath" ValueField="WidgetPath" Width="250px">
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="Description" VisibleIndex="3" Visible="True"
                                                            Width="350px" meta:resourcekey="GridViewDataTextColumn3">
                                                            <PropertiesTextEdit MaxLength="255">
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings ColumnSpan="2" VisibleIndex="14" />
                                                            <EditItemTemplate>
                                                                <dxwgv:ASPxGridView ID='GridViewLanguage' runat='server' AutoGenerateColumns='False' OnRowUpdating="ASPxGridView1_RowUpdating"
                                                                    ClientInstanceName ='GridViewLanguage' KeyFieldName='LanguageCode' Width="100%"
                                                                    EnableRowsCache="False">
                                                                    <SettingsPager PageSize="20">
                                                                    </SettingsPager>
                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                    <Columns>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="LanguageCode" VisibleIndex="0" ReadOnly="True"
                                                                            Visible="False" Width="100px" meta:resourcekey="GridViewDataTextColumn30">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="LanguageDescription" VisibleIndex="1" ReadOnly="True"
                                                                            Visible="True" Width="100px" meta:resourcekey="GridViewDataTextColumn31">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="LanguageShortDescription" VisibleIndex="2"
                                                                            ReadOnly="True" Visible="False" Width="100px" meta:resourcekey="GridViewDataTextColumn32">
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="Name" VisibleIndex="4" Caption="Widget Name"
                                                                            meta:resourcekey="GridViewDataTextColumn33">
                                                                            <DataItemTemplate>
                                                                                <dxe:ASPxTextBox ID="Name" ClientInstanceName="Name" runat="server" Text='<%#Eval("Name")%>' Size="50" MaxLength="40">
                                                                                </dxe:ASPxTextBox>
                                                                            </DataItemTemplate>
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                        <dxwgv:GridViewDataTextColumn FieldName="Description" VisibleIndex="5" meta:resourcekey="GridViewDataTextColumn34">
                                                                            <DataItemTemplate>
                                                                                <dxe:ASPxTextBox ID="Description" ClientInstanceName="Description" runat="server" Text='<%#Eval("Description")%>'
                                                                                    Size="50" MaxLength="100">
                                                                                </dxe:ASPxTextBox>
                                                                            </DataItemTemplate>
                                                                        </dxwgv:GridViewDataTextColumn>
                                                                    </Columns>
                                                                </dxwgv:ASPxGridView>
                                                            </EditItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataDateColumn FieldName="CreatedDate" VisibleIndex="5" Visible="True"
                                                            Width="50px" meta:resourcekey="GridViewDataDateColumn5">
                                                            <EditFormSettings Visible="False" />
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataDateColumn FieldName="LastUpdate" VisibleIndex="6" Visible="True"
                                                            Width="50px" meta:resourcekey="GridViewDataDateColumn6">
                                                            <EditFormSettings Visible="False" />
                                                        </dxwgv:GridViewDataDateColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="VersionNo" VisibleIndex="7" Visible="False"
                                                            Width="50px" meta:resourcekey="GridViewDataTextColumn7">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataCheckColumn FieldName="IsDefault" VisibleIndex="8" Visible="False"
                                                            Width="50px" meta:resourcekey="GridViewDataTextColumn8">
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="DefaultState" VisibleIndex="9" Visible="False"
                                                            Width="200px" meta:resourcekey="GridViewDataTextColumn9">
                                                            <PropertiesTextEdit MaxLength="500" EncodeHtml="False">
                                                            </PropertiesTextEdit>
                                                            <EditFormSettings Visible="True" />
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataImageColumn FieldName="Icon" VisibleIndex="10" Visible="True"
                                                            Width="100px" meta:resourcekey="GridViewDataImageColumn10">
                                                            <EditFormSettings Visible="False" />
                                                        </dxwgv:GridViewDataImageColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="OrderNo" VisibleIndex="11" Visible="False"
                                                            Width="100px" meta:resourcekey="GridViewDataTextColumn11">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataCheckColumn FieldName="IsAnonymouAllow" VisibleIndex="12" Visible="False"
                                                            Width="50px" meta:resourcekey="GridViewDataCheckColumn12">
                                                        </dxwgv:GridViewDataCheckColumn>
                                                        <dxwgv:GridViewDataComboBoxColumn FieldName="IconEdit" VisibleIndex="13" Visible="False"
                                                            Width="250px" meta:resourcekey="GridViewDataComboBoxColumn13">
                                                            <EditFormSettings Visible="True" />
                                                            <PropertiesComboBox ImageUrlField="ImagePath" TextField="ImageName" ValueField="ImagePath"
                                                                ShowImageInEditBox="true" Width="250px">
                                                            </PropertiesComboBox>
                                                        </dxwgv:GridViewDataComboBoxColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxrp:ASPxRoundPanel>
                        <br />
                        <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                            ID="popupDelete" runat="server" ClientInstanceName="popupDelete" Modal="true">
                            <HeaderTemplate>
                                <div>
                                    <asp:Literal ID="popupMessageHeader" runat="server">Confirmación de Borrado</asp:Literal>
                                </div>
                            </HeaderTemplate>
                            <ContentCollection>
                                <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                                    <div style="width: 400px">
                                        <table>
                                            <tr>
                                                <td rowspan="2">
                                                    <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/images/generaluse/ConfirmDelete/Question.png">
                                                    </dxe:ASPxImage>
                                                </td>
                                                <td>
                                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="¿Está seguro de querer eliminar las filas seleccionadas?">
                                                    </dxe:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <div style="text-align: center; width: 400px">
                                            <table style="width: 100%;">
                                                <tr align="left">
                                                    <td>
                                                        <dxe:ASPxRadioButton ID="DeleteAll" runat="server" ClientInstanceName="DeleteAll" EnableViewState="false"
                                                            Checked="true" meta:resourcekey="DeleteAllResource">
                                                            <ClientSideEvents ValueChanged="OnChange_DeleteAll" />
                                                        </dxe:ASPxRadioButton>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td>
                                                        <dxe:ASPxRadioButton ID="DeleteInstance" runat="server" ClientInstanceName="DeleteInstance"
                                                            EnableViewState="false" Checked="false" meta:resourcekey="DeleteInstanceResource">
                                                            <ClientSideEvents ValueChanged="OnChange_DeleteInstance" />
                                                        </dxe:ASPxRadioButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <br />
                                        <table>
                                            <tr>
                                                <td></td>
                                                <td style="width: 100%"></td>
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
                        <br />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>