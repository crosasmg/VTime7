<%@ Page Title="" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false"
    CodeFile="GroupsManager.aspx.vb" Inherits="Maintenance_GroupsManager" meta:resourcekey="PageResource" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script type="text/javascript">

        function btnYes_Click(s, e) {
            popupDelete.Hide();
            if (CurrentGrid.GetSelectedRowCount() != 0) {
                CurrentGrid.PerformCallback('delete');
                CurrentGrid.PerformCallback('');
            }
        }

        function btnNo_Click(s, e) {
            popupDelete.Hide();
        }

        var gridPerformingCallback = false;

        var CurrentGrid = null;

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

        function HandlerView(name) {
            switch (name) {
                case 'ViewItem':
                case 'USERGROUPS_ViewItem':


                    CurrentGrid = Groups_Grid;
                    CurrentGrid.PerformCallback('');
                    break;



            }

        }                      
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div align="center">
                <table width="1000px" border="0" cellspacing="5">
                    <tbody>
                        <tr>
                            <td width="15px">
                            </td>
                            <td width="1000px">
                                <dxm:ASPxMenu ID="MainMenu" runat="server" Width="100%" ClientInstanceName="MainMenu">
                                    <ClientSideEvents ItemClick="function(s, e) {
                            e.processOnServer = false;

                                switch (e.item.name) {
                                                        
                                    case 'AddRegisterItem' :
                                        CurrentGrid.AddNewRow();
                                        break;
                                    case 'EditRegisterItem':
                                        CurrentGrid.StartEditRow(CurrentGrid.GetFocusedRowIndex());
                                        break;
                                    case 'RemoveRegisterItem':
                                        popupDelete.Show();
                                        break;
                                    case 'export_pdf':
                                    case 'export_xls':
                                    case 'export_xlsx':
                                    case 'export_rtf':
                                    case 'export_csv':
                                        CurrentGrid.PerformCallback(e.item.name);
                                        break;
                                    
                                    default:
                                        e.processOnServer = true;}}" />
                                    <Items>
                                        <dxm:MenuItem Name="AddRegisterItem" Text="Agregar" meta:resourcekey="AddRegisterItem"
                                            Image-UrlDisabled="/images/16x16/Toolbar/disabledAdd.png" Image-Url="/images/16x16/Toolbar/add.png">
                                            <ItemStyle Width="5%" />
                                        </dxm:MenuItem>
                                        <dxm:MenuItem BeginGroup="True" Name="EditRegisterItem" Text="Editar" Image-UrlDisabled="/images/16x16/Toolbar/disabledEdit.png"
                                            Image-Url="/images/16x16/Toolbar/edit.png" meta:resourcekey="EditRegisterItem">
                                            <ItemStyle Width="5%" />
                                        </dxm:MenuItem>
                                        <dxm:MenuItem BeginGroup="True" Name="RemoveRegisterItem" Text="Eliminar" Image-UrlDisabled="/images/16x16/Toolbar/disabledDelete.png"
                                            Image-Url="/images/16x16/Toolbar/delete.png" meta:resourcekey="RemoveRegisterItem">
                                            <ItemStyle Width="5%" />
                                        </dxm:MenuItem>
                                        <dxm:MenuItem BeginGroup="True" Text="" Enabled="False">
                                            <ItemStyle Width="85%" />
                                        </dxm:MenuItem>
                                        <dxm:MenuItem BeginGroup="True" Name="ExportItemMenu" Text="Export" DropDownMode="True"
                                            Image-Url="/images/16x16/Toolbar/export.png" Image-UrlDisabled="/images/16x16/Toolbar/disabledExport.png"
                                            meta:resourcekey="ExportItemMenu">
                                            <Items>
                                                <dxm:MenuItem Name="export_pdf" Text="PDF" Image-Url="/images/16x16/FileFormat/pdf.png" />
                                                <dxm:MenuItem Name="export_xls" Text="XLS" Image-Url="/images/16x16/FileFormat/xls.png" />
                                                <dxm:MenuItem Name="export_xlsx" Text="XLSX" Image-Url="/images/16x16/FileFormat/xlsx.png" />
                                                <dxm:MenuItem Name="export_rtf" Text="RTF" Image-Url="/images/16x16/FileFormat/rtf.png" />
                                            </Items>
                                            <ItemStyle Width="10%" />
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
                                <dxp:ASPxPanel ID="USERGROUPS_View" ClientInstanceName="USERGROUPS_View" runat="server"
                                    Visible="True" ClientVisible="True" Width="100%">
                                    <PanelCollection>
                                        <dxp:PanelContent ID="USERGROUPS_ViewPanel" runat="server" SupportsDisabledAttribute="True">
                                            <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientInstanceName='Groups_Grid'
                                                ID='Groups_Grid' runat='server' Width='100%' KeyFieldName='GROUPID' Caption='Groups'
                                                Enabled="True" ClientVisible="True" meta:resourcekey="Groups_GridResource" EnableRowsCache="False"
                                                EnableViewState="False" KeyboardSupport="True" EnableCallbackCompression="True"
                                                EnableCallBacks="True">
                                                <SettingsPager PageSize="20" />
                                                <SettingsBehavior AllowFocusedRow="True" />
                                                <SettingsEditing Mode="Inline" />
                                                <ClientSideEvents RowDblClick="function(s, e) {s.StartEditRow(e.visibleIndex);}"
                                                    FocusedRowChanged="function(s, e) {MainMenu.GetItemByName('EditRegisterItem').SetEnabled(s.GetFocusedRowIndex()>-1);MainMenu.GetItemByName('RemoveRegisterItem').SetEnabled(s.GetSelectedRowCount()>0);}" />
                                                <Columns>
                                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" ButtonType="Image" Width="8px" ShowSelectCheckbox="True" />
                                                    <dxwgv:GridViewDataTextColumn Name='GROUPID' FieldName='GROUPID' Caption='GROUP'
                                                        ToolTip='...' VisibleIndex="0" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom"
                                                        HeaderStyle-HorizontalAlign="Center" Width="8%" Visible="True" meta:resourcekey="GROUPID">
                                                        <EditFormSettings VisibleIndex="0" Visible="True" />
                                                        <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
                                                            <ValidationSettings ErrorDisplayMode="Text">
                                                                <RequiredField IsRequired='True' ErrorText='The "GROUP" is required.' />
                                                            </ValidationSettings>
                                                            <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl="/images/generaluse/required.PNG"
                                                                BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
                                                                
                                                            </Style>
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataComboBoxColumn Name='OWNERID' FieldName='OWNERID' Caption='OWNER'
                                                        ToolTip='...' VisibleIndex="1" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom"
                                                        HeaderStyle-HorizontalAlign="Center" Width="31%" Visible="True" meta:resourcekey="OWNERID">
                                                        <EditFormSettings VisibleIndex="1" Visible="True" />
                                                        <PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith"
                                                            EnableCallbackMode="true" DropDownStyle="DropDownList" TextFormatString="{0}"
                                                            TextField='SFIRSTNAME' ValueField='NUSERCODE'>
                                                            <Columns>
                                                                <dxe:ListBoxColumn FieldName="SFIRSTNAME" Caption="FIRST NAME" Visible="True" meta:resourcekey="OWNERIDColumnSFIRSTNAMEResource" />
                                                                <dxe:ListBoxColumn FieldName="SLASTNAME" Caption="LAST NAME" Visible="True" meta:resourcekey="OWNERIDColumnSLASTNAMEResource" />
                                                            </Columns>
                                                        </PropertiesComboBox>
                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                    <dxwgv:GridViewDataTextColumn Name='DESCRIPTION' FieldName='DESCRIPTION' Caption='DESCRIPTION'
                                                        ToolTip='...' VisibleIndex="2" CellStyle-HorizontalAlign="Left" Width="62%" Visible="True"
                                                        HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="DESCRIPTION">
                                                        <EditFormSettings VisibleIndex="2" Visible="True" />
                                                        <PropertiesTextEdit Size='40' MaxLength='40'>
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewCommandColumn VisibleIndex="3" ButtonType="Image" Caption=" " Width="24px">
                                                        <EditButton Visible="true">
                                                            <Image Url="~/images/empty.png" />
                                                        </EditButton>
                                                        <CancelButton>
                                                            <Image Url="~/images/16x16/Commands/cancel.png" />
                                                        </CancelButton>
                                                        <UpdateButton>
                                                            <Image Url="~/images/16x16/Commands/accept.png" />
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
            </div>
            <script type="text/javascript">
                AddKeyboardNavigationTo(Groups_Grid);

                HandlerView('USERGROUPS_ViewItem')                
            </script>
            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter" runat="server" />
            <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                ID="popupDelete" runat="server" ClientInstanceName="popupDelete" Modal="true">
                <HeaderTemplate>
                    <div>
                        <asp:Literal ID="popupMessageHeader" runat="server">Confirmación de Borrado</asp:Literal></div>
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
                                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="¿Está seguro de querer eliminar las filas seleccionadas?">
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
            <dx:ASPxHiddenField ID="CurrentState" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
