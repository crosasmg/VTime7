<%@ Page Title="Relación entre usuarios" Language="VB" MasterPageFile="~/DropthingsMasterPage.master"
    AutoEventWireup="false" CodeFile="UserRelationship.aspx.vb" Inherits="Prototype_CRUD"
    meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script type="text/javascript">
        function isEnable(s, e) {
            if (s.GetSelectedRowCount() > 0) {
                MainMenu.GetItemByName('RemoveRegisterItem').SetEnabled(true);
            } else {
                MainMenu.GetItemByName('RemoveRegisterItem').SetEnabled(false);
            }

        }
       
        function btnYes_Click(s, e) {
            popupDelete.Hide();
            if (GridViewDirectory.GetSelectedRowCount() != 0) {
                GridViewDirectory.PerformCallback('delete');
                GridViewDirectory.UnselectRows();
            }
        }

        function btnNo_Click(s, e) {
            popupDelete.Hide();
        }

        var gridPerformingCallback = false;

        function AddKeyboardNavigationTo(grid) {
            grid.BeginCallback.AddHandler(function (s, e) {
                gridPerformingCallback = true;
            });

            grid.EndCallback.AddHandler(function (s, e) {
                gridPerformingCallback = false;
            });

            ASPxClientUtils.AttachEventToElement(document, "keydown",
                function (evt) {
                    if (typeof (event) != "undefined" && event != null)
                        evt = event;
                    if (!gridPerformingCallback) {
                        switch (evt.keyCode) {
                            case ASPxKey.Esc:
                                if (grid.IsEditing())
                                    grid.CancelEdit();
                                break;
                            case ASPxKey.Enter:
                                if (grid.IsEditing())
                                    grid.UpdateEdit();
                                else
                                    grid.StartEditRow(grid.GetFocusedRowIndex());
                                break;
                            default:
                                evt = event;
                        }
                    }
                });
        }

        var CurrentGrid = null;
        function HandlerView(name) {
            switch (name) {
                case 'ViewItem':
                case 'GROUPMEMBERS_ViewItem':

                    CurrentGrid = GridViewDirectory;
                    CurrentGrid.PerformCallback('');
                    break;

            }
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="1000px" border="0" cellspacing="5">
                <tbody>
                    <tr>
                        <td width="15px">
                        </td>
                        <td width="1000px">
                            <dxm:ASPxMenu ID="MainMenu" runat="server" Width="100%" ClientInstanceName="MainMenu"
                                meta:resourcekey="MainMenuResource1">
                                <ClientSideEvents ItemClick="function(s, e) {
                            e.processOnServer = false;
                        switch (e.item.name) {

                            case 'AddRegisterItem' :
                                CurrentGrid.AddNewRow();
                                break;
                            case 'EditRegisterItem':
                                CurrentGrid.StartEditRow(GridViewDirectory.GetFocusedRowIndex());
                                break;
                            case 'RemoveRegisterItem':
                                popupDelete.Show();
                                break;
                            default:
                                e.processOnServer = true;
                        }

}" />
                                <Items>
                                    <dxm:MenuItem Name="AddRegisterItem" Text="Agregar" meta:resourcekey="MenuItemResource1">
                                        <ItemStyle Width="5%" />
                                    </dxm:MenuItem>
                                    <dxm:MenuItem BeginGroup="True" Name="EditRegisterItem" Text="Editar" meta:resourcekey="MenuItemResource2">
                                        <ItemStyle Width="5%" />
                                    </dxm:MenuItem>
                                    <dxm:MenuItem BeginGroup="True" Name="RemoveRegisterItem" Text="Eliminar" meta:resourcekey="MenuItemResource3">
                                        <ItemStyle Width="5%" />
                                    </dxm:MenuItem>
                                    <dxm:MenuItem BeginGroup="True" Text="" Enabled="False" meta:resourcekey="MenuItemResource4">
                                        <ItemStyle Width="85%" />
                                    </dxm:MenuItem>
                                </Items>
                            </dxm:ASPxMenu>
                        </td>
                        <td width="15px">
                        </td>
                    </tr>
                    <tr>
                        <td width="15px">
                            <p style="font-weight: lighter">
                            </p>
                        </td>
                        <td width="1000px">
                            <dxp:ASPxPanel ID="GROUPMEMBERS_View" ClientInstanceName="GROUPMEMBERS_View" runat="server"
                                Width="100%" meta:resourcekey="GROUPMEMBERS_ViewResource1">
                                <PanelCollection>
                                    <dxp:PanelContent runat="server" SupportsDisabledAttribute="True" meta:resourcekey="GROUPMEMBERS_ViewPanelResource1">
                                        <dxwgv:ASPxGridView ID="GridViewDirectory" runat="server" AutoGenerateColumns="False"
                                            KeyFieldName="USERID;USERINDICATOR;RELATIONSHIPUSERID" ClientInstanceName='GridViewDirectory'
                                            Width="100%" EnableRowsCache="False" EnableViewState="False" KeyboardSupport="True"
                                            meta:resourcekey="GridViewDirectoryResource1">
                                            <SettingsPager PageSize="20">
                                            </SettingsPager>
                                            <SettingsBehavior AllowFocusedRow="True" />
                                            <SettingsEditing Mode="Inline" />
                                            <ClientSideEvents RowDblClick="function(s, e) {s.StartEditRow(e.visibleIndex);}" Init="function(s, e) { isEnable(s, e); }" SelectionChanged="function(s, e) { isEnable(s, e); }" />
                                            <Columns>
                                                <dxwgv:GridViewCommandColumn VisibleIndex="0" ButtonType="Image" Width="20px" ShowSelectCheckbox="True"
                                                    ShowInCustomizationForm="True" meta:resourcekey="GridViewCommandColumnResource1">
                                                </dxwgv:GridViewCommandColumn>
                                                <dxwgv:GridViewDataComboBoxColumn Name='RELATIONSHIPUSERID' FieldName='RELATIONSHIPUSERID'
                                                    VisibleIndex="1" Caption='Usuario' meta:resourcekey="GridViewDataComboBoxColumnResource1"
                                                    ToolTip='...' ShowInCustomizationForm="True">
                                                    <EditFormSettings VisibleIndex="0" Visible="True" />
                                                    <PropertiesComboBox IncrementalFilteringMode="StartsWith" EnableCallbackMode="True"
                                                        TextFormatString="{0}" TextField='RealName' ValueField='ID'>
                                                        <Columns>
                                                            <dxe:ListBoxColumn FieldName="RealName" Caption="Name" meta:resourcekey="USERIDColumnSFIRSTNAMEResource" />
                                                        </Columns>
                                                        <ValidationSettings ErrorDisplayMode="Text">
                                                            <RequiredField IsRequired='True' ErrorText='The "User" is required.' />
                                                        </ValidationSettings>
                                                        <Style>
                                                            <Paddings PaddingLeft="8px" />
                                                            <BackgroundImage HorizontalPosition="left"
                                                                ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat"
                                                                VerticalPosition="center" />
                                                        </Style>
                                                    </PropertiesComboBox>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Bottom" Wrap="True" />
                                                </dxwgv:GridViewDataComboBoxColumn>
                                                <dxwgv:GridViewDataComboBoxColumn FieldName="RELATIONSHIPTYPE" VisibleIndex="2" Caption="Relación"
                                                    meta:resourcekey="GridViewDataComboBoxColumnResource2" ShowInCustomizationForm="True">
                                                    <PropertiesComboBox IncrementalFilteringMode="StartsWith" EnableCallbackMode="True">
                                                        <Items>
                                                            <dxe:ListEditItem Text="Supervisa a" Value="1" meta:resourcekey="ListEditItemResource1" />
                                                            <dxe:ListEditItem Text="Coopera con" Value="2" meta:resourcekey="ListEditItemResource2" />
                                                        </Items>
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesComboBox>
                                                </dxwgv:GridViewDataComboBoxColumn>
                                                <dxwgv:GridViewDataComboBoxColumn Name='USERID' FieldName='USERID' VisibleIndex="3"
                                                    Caption='Usuario relacionado' meta:resourcekey="GridViewDataComboBoxColumnResource3"
                                                    ToolTip='...' ShowInCustomizationForm="True">
                                                    <EditFormSettings VisibleIndex="0" Visible="True" />
                                                    <PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith"
                                                        EnableCallbackMode="True" TextFormatString="{0}" TextField='RealName' ValueField='ID'>
                                                        <Columns>
                                                            <dxe:ListBoxColumn FieldName="RealName" Caption="Name" meta:resourcekey="USERIDColumnSFIRSTNAMEResource" />
                                                        </Columns>
                                                        <ValidationSettings ErrorDisplayMode="Text">
                                                            <RequiredField IsRequired='True' ErrorText='The "User" is required.' />
                                                        </ValidationSettings>
                                                        <Style>
                                                            <Paddings PaddingLeft="8px" />
                                                            <BackgroundImage HorizontalPosition="left"
                                                                ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat"
                                                                VerticalPosition="center" />
                                                        </Style>
                                                    </PropertiesComboBox>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Bottom" Wrap="True" />
                                                </dxwgv:GridViewDataComboBoxColumn>
                                                <dxwgv:GridViewDataCheckColumn FieldName="USERINDICATOR" VisibleIndex="4" Visible="False"
                                                    ShowInCustomizationForm="True" meta:resourcekey="GridViewDataCheckColumnResource1">
                                                </dxwgv:GridViewDataCheckColumn>
                                                <dxwgv:GridViewDataCheckColumn FieldName="ALLOWQUERY" Caption="Consultar" VisibleIndex="5"
                                                    Width="40px" meta:resourcekey="GridViewDataCheckColumnResource2" ShowInCustomizationForm="True">
                                                </dxwgv:GridViewDataCheckColumn>
                                                <dxwgv:GridViewDataCheckColumn FieldName="ALLOWCREATE" Caption="Crear" VisibleIndex="6"
                                                    Width="40px" meta:resourcekey="GridViewDataCheckColumnResource3" ShowInCustomizationForm="True">
                                                </dxwgv:GridViewDataCheckColumn>
                                                <dxwgv:GridViewDataCheckColumn FieldName="ALLOWCANCEL" Caption="Cancelar" VisibleIndex="7"
                                                    Width="40px" meta:resourcekey="GridViewDataCheckColumnResource4" ShowInCustomizationForm="True">
                                                </dxwgv:GridViewDataCheckColumn>
                                                <dxwgv:GridViewDataCheckColumn FieldName="ALLOWCOMPLETED" Caption="Completar" VisibleIndex="8"
                                                    Width="40px" meta:resourcekey="GridViewDataCheckColumnResource5" ShowInCustomizationForm="True">
                                                </dxwgv:GridViewDataCheckColumn>
                                                <dxwgv:GridViewDataCheckColumn FieldName="ALLOWREASSIGN" Caption="Reasignar" VisibleIndex="9"
                                                    Width="40px" meta:resourcekey="GridViewDataCheckColumnResource6" ShowInCustomizationForm="True">
                                                </dxwgv:GridViewDataCheckColumn>
                                                <dxwgv:GridViewCommandColumn VisibleIndex="10" ButtonType="Image" Caption=" " ShowInCustomizationForm="True"
                                                    meta:resourcekey="GridViewCommandColumnResource2">
                                                    <EditButton Visible="True">
                                                        <Image Url="~/images/empty.png" />
                                                    </EditButton>
                                                    <CancelButton>
                                                        <Image Url="~/images/generaluse/btncanceloff.gif" />
                                                    </CancelButton>
                                                    <UpdateButton>
                                                        <Image Url="~/images/generaluse/btnacceptoff.gif" />
                                                    </UpdateButton>
                                                </dxwgv:GridViewCommandColumn>
                                            </Columns>
                                        </dxwgv:ASPxGridView>
                                    </dxp:PanelContent>
                                </PanelCollection>
                            </dxp:ASPxPanel>
                        </td>
                        <td width="15px">
                        </td>
                    </tr>
                </tbody>
            </table>
            <script type="text/javascript">
                AddKeyboardNavigationTo(GridViewDirectory);
                HandlerView('GROUPMEMBERS_ViewItem')
            </script>
            <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                ID="popupDelete" runat="server" ClientInstanceName="popupDelete" Modal="True"
                meta:resourcekey="popupDeleteResource1">
                <HeaderTemplate>
                    <div>
                        <asp:Literal ID="popupMessageHeader" runat="server" meta:resourcekey="popupMessageHeaderResource1"
                            Text="Confirmación de Borrado"></asp:Literal></div>
                </HeaderTemplate>
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server" SupportsDisabledAttribute="True"
                        meta:resourcekey="PopupControlContentControlResource1">
                        <div style="width: 350px">
                            <table>
                                <tr>
                                    <td rowspan="2">
                                        <dxe:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/images/generaluse/ConfirmDelete/Question.png"
                                            meta:resourcekey="ASPxImage1Resource1">
                                        </dxe:ASPxImage>
                                    </td>
                                    <td>
                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="¿Está seguro de querer eliminar las filas seleccionadas?"
                                            meta:resourcekey="ASPxLabel1Resource">
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
                                            EnableDefaultAppearance="False" EnableTheming="False" meta:resourcekey="btnYesResource1">
                                            <Image Url="~/images/generaluse/ConfirmDelete/btnacceptoff.gif" UrlChecked="~/images/generaluse/ConfirmDelete/btnaccepton.gif"
                                                UrlPressed="~/images/generaluse/ConfirmDelete/btnaccepton.gif" />
                                            <ClientSideEvents Click="btnYes_Click" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btnNo" runat="server" Width="50px" AutoPostBack="False" ClientInstanceName="btnNo"
                                            EnableDefaultAppearance="False" EnableTheming="False" meta:resourcekey="btnNoResource1">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>