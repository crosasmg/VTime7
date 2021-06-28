<%@ Page Title="" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false"
    CodeFile="RolesManager.aspx.vb" Inherits="Maintenance_RolesManager" meta:resourcekey="PageResource" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script type="text/javascript">

        function btnYes_Click(s, e) {
            popupDelete.Hide();
            if (CurrentGrid.GetSelectedRowCount() != 0) {
                CurrentGrid.PerformCallback('delete');
                MainMenu.GetItemByName('EditRegisterItem').SetEnabled(false);
                MainMenu.GetItemByName('RemoveRegisterItem').SetEnabled(false);
            }
        }

        function OnEndCallback(s, e) {
            if (s.cp_error) {
                alert(s.cp_error_Message);
                delete s.cp_error;
                delete s.cp_error_Message;
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
                case 'Role_ViewItem':

                    CurrentGrid = Role;
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
                            <td width="15px"></td>
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
                                    case 'TemplateItemMenu':
                                        break;
                                    case 'PageExcelItemMenu':
                                        document.getElementById('confirm').style.display = 'none';
                                        popupSaveTemplate.Show();
                                        break;
                                    case 'ImportItemMenu':
                                        document.getElementById('confirm').style.display = 'none';
                                        ExcelFileName.SetClientVisible(false);
                                        ExcelFileUpload.SetClientVisible(true);
                                        popupImport.Show();
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
                                            Image-Url="/images/16x16/Toolbar/edit.png" meta:resourcekey="EditRegisterItem"
                                            ClientEnabled="false">
                                            <ItemStyle Width="5%" />
                                        </dxm:MenuItem>
                                        <dxm:MenuItem BeginGroup="True" Name="RemoveRegisterItem" Text="Eliminar" Image-UrlDisabled="/images/16x16/Toolbar/disabledDelete.png"
                                            Image-Url="/images/16x16/Toolbar/delete.png" meta:resourcekey="RemoveRegisterItem"
                                            ClientEnabled="false">
                                            <ItemStyle Width="5%" />
                                        </dxm:MenuItem>
                                        <dxm:MenuItem BeginGroup="True" Text="" Enabled="False">
                                            <ItemStyle Width="85%" />
                                        </dxm:MenuItem>
                                        <dxm:MenuItem BeginGroup="True" Name="LoadDataItemMenu" Text="Carga" DropDownMode="True"
                                            Image-Url="/images/16x16/Toolbar/loadData.png" Image-UrlDisabled="/images/16x16/Toolbar/disabledExport.png"
                                            meta:resourcekey="LoadDataItemMenu">
                                            <Items>
                                                <dxm:MenuItem Name="PageExcelItemMenu" Text="Plantilla de Excel" meta:resourcekey="PageExcelItemMenu"
                                                    Image-Url="/images/16x16/Toolbar/pageExcel.png">
                                                </dxm:MenuItem>
                                                <dxm:MenuItem Name="ImportItemMenu" Text="Importar..." meta:resourcekey="ImportItemMenu"
                                                    Image-Url="/images/16x16/Toolbar/import.png" />
                                            </Items>
                                            <ItemStyle Width="10%" />
                                        </dxm:MenuItem>
                                        <dxm:MenuItem BeginGroup="True" Name="ExportItemMenu" Text="Export..." DropDownMode="True"
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
                                        <dxm:MenuItem BeginGroup="True" Name="HelpItemMenu" Text="Ayuda" Image-UrlDisabled="/images/16x16/Toolbar/help.png"
                                            Image-Url="/images/16x16/Toolbar/help.png" meta:resourcekey="HelpItemMenu">
                                            <ItemStyle Width="5%" />
                                        </dxm:MenuItem>
                                    </Items>
                                </dxm:ASPxMenu>
                            </td>
                            <td width="15px"></td>
                        </tr>
                        <tr>
                            <td width="15px">
                                <p style="font-weight: lighter">
                                </p>
                            </td>
                            <td width="1000px">
                                <dxp:ASPxPanel ID="Role_View" ClientInstanceName="Role_View" runat="server" ClientVisible="True"
                                    Width="100%">
                                    <PanelCollection>
                                        <dxp:PanelContent ID="Role_ViewPanel" runat="server" SupportsDisabledAttribute="True">
                                            <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientInstanceName='Role' ID='Role'
                                                runat='server' Width='100%' KeyFieldName='ROLEID' Caption='ROLES' Enabled="True"
                                                ClientVisible="True" meta:resourcekey="RoleResource" EnableRowsCache="False"
                                                EnableViewState="False" KeyboardSupport="False" EnableCallbackCompression="True"
                                                EnableCallBacks="True">
                                                <SettingsPager PageSize="20" />
                                                <SettingsBehavior AllowFocusedRow="True" />
                                                <SettingsEditing Mode="Inline" />
                                                <ClientSideEvents RowDblClick="function(s, e) {s.StartEditRow(e.visibleIndex);}"
                                                    FocusedRowChanged="function(s, e) {MainMenu.GetItemByName('EditRegisterItem').SetEnabled(s.GetFocusedRowIndex()>-1);}"
                                                    SelectionChanged="function(s, e) {MainMenu.GetItemByName('RemoveRegisterItem').SetEnabled(s.GetSelectedRowCount()>0);}"
                                                    EndCallback="function(s, e) { OnEndCallback(s, e);}"  />
                                                <Columns>
                                                    <dxwgv:GridViewCommandColumn VisibleIndex="0" ButtonType="Image" Width="8px" ShowSelectCheckbox="True" />
                                                    <dxwgv:GridViewDataTextColumn Name='RoleId' FieldName='ROLEID' Caption='Identifier'
                                                        ToolTip='Id Rol' VisibleIndex="0" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom"
                                                        HeaderStyle-HorizontalAlign="Center" Width="3%" Visible="false" meta:resourcekey="RoleId">
                                                        <EditFormSettings VisibleIndex="0" Visible="True" />
                                                        <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,###,##0"
                                                            Size='9'>
                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999999999g>" />
                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
                                                                <RequiredField IsRequired='True' ErrorText='The "Identifier" is required.' />
                                                            </ValidationSettings>
                                                            <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl="/images/generaluse/required.PNG"
                                                                BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
                                                            </Style>
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Name='RoleName' FieldName='ROLENAME' Caption='Role Name'
                                                        ToolTip='Role Name' VisibleIndex="1" CellStyle-HorizontalAlign="Left" Width="76%"
                                                        Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="RoleName">
                                                        <EditFormSettings VisibleIndex="1" Visible="True" />
                                                        <PropertiesTextEdit Size='255' MaxLength='255'>
                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
                                                                <RequiredField IsRequired='True' ErrorText='The "Role Name" is required.' />
                                                            </ValidationSettings>
                                                            <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl="/images/generaluse/required.PNG"
                                                                BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
                                                            </Style>
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Name="SecurityLevel" FieldName="SECURITYLEVEL" Width="20%" VisibleIndex="2"
                                                        meta:resourcekey="SecurityLevel" Caption='Nivel Seguridad' ToolTip='Nivel de Seguridad'
                                                        CellStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-Wrap="True" HeaderStyle-HorizontalAlign="Left">
                                                        <EditItemTemplate>
                                                            <dx:ASPxTrackBar ID="tkbrSecurityLevel"
                                                                runat="server" Width="100%" MinValue="1" MaxValue="9"
                                                                Step="1" SmallTickFrequency="1" LargeTickStartValue="1" LargeTickEndValue="9"
                                                                LargeTickInterval="1" ValueToolTipPosition="LeftOrTop" ScalePosition="LeftOrTop"
                                                                Value='<%# Bind("SECURITYLEVEL") %>' PositionStart="9">
                                                            </dx:ASPxTrackBar>
                                                        </EditItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataCheckColumn Name='IsBackOfficeSource' FieldName='ISBACKOFFICESOURCE'
                                                        Caption='Is Back Office Source' ToolTip='Is BackOffice Source' VisibleIndex="3"
                                                        HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center"
                                                        Width="0%" Visible="True" ReadOnly="True" meta:resourcekey="IsBackOfficeSource">
                                                        <EditFormSettings VisibleIndex="2" Visible="True" />
                                                        <PropertiesCheckEdit ValueChecked="1" ValueUnchecked="0" ValueType="System.Int32">
                                                        </PropertiesCheckEdit>
                                                    </dxwgv:GridViewDataCheckColumn>
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
                            <td width="15px"></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <script type="text/javascript">
                AddKeyboardNavigationTo(Role);

                HandlerView('Role_ViewItem')
            </script>
            <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter" runat="server" />
            <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                ID="popupDelete" runat="server" ClientInstanceName="popupDelete" Modal="true"
                meta:resourcekey="popupDeleteResource" HeaderText="Confirmación de borrado" HeaderImage-Url="/images/16x16/Toolbar/deleteRow.png">
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
                                        <dxe:ASPxLabel ID="DeleteRowsLabel" runat="server" Text="¿Está seguro de querer eliminar las filas seleccionadas?"
                                            meta:resourcekey="DeleteRowsLabelResource">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table>
                                <tr>
                                    <td></td>
                                    <td style="width: 100%"></td>
                                    <td>
                                        <dxe:ASPxButton ID="btnYes" runat="server" Width="50px" AutoPostBack="False" ClientInstanceName="btnYes"
                                            meta:resourcekey="btnacceptonResource" Text="Aceptar" Image-Url="~/images/16x16/Commands/accept.png">
                                            <ClientSideEvents Click="btnYes_Click" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btnNo" runat="server" Width="50px" AutoPostBack="False" ClientInstanceName="btnNo"
                                            meta:resourcekey="btncancelonResource" Text="Cancelar" Image-Url="~/images/16x16/Commands/cancel.png">
                                            <ClientSideEvents Click="btnNo_Click" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                ID="popupImport" runat="server" ClientInstanceName="popupImport" Modal="true"
                meta:resourcekey="popupImportResource" HeaderText="Importar una plantilla" HeaderImage-Url="/images/16x16/Toolbar/import.png">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlExcelFileUpload" runat="server">
                        <div style="width: 350px">
                            <dx:ASPxUploadControl ID="ExcelFileUpload" ClientInstanceName="ExcelFileUpload" runat="server"
                                meta:resourcekey="ExcelFileUploadResource" ShowProgressPanel="True" Width="100%"
                                NullText="Haz clic aquí para seleccionar el archivo...">
                                <ValidationSettings AllowedFileExtensions=".xlsx">
                                </ValidationSettings>
                                <ClientSideEvents FileUploadComplete="function(s, e) {
                                    ExcelFileName.SetText(e.callbackData);s.SetClientVisible(false);ExcelFileName.SetClientVisible(true);Role.PerformCallback('import_'+ExcelFileName.GetValue());
                                    }"
                                    TextChanged="function(s, e) {s.Upload();}" />
                                <BrowseButton Text="Browse..." Image-Url="../images/16x16/General/openFolder.png">
                                </BrowseButton>
                                <CancelButton Text="Cancel" Image-Url="~/images/16x16/Commands/cancel.png">
                                </CancelButton>
                                <AdvancedModeSettings TemporaryFolder="~\Temp\" />
                            </dx:ASPxUploadControl>
                            <dxe:ASPxTextBox runat="server" Text="" ClientInstanceName="ExcelFileName" ID="ExcelFileName"
                                ClientVisible="false" ClientEnabled="false" Width="100%">
                            </dxe:ASPxTextBox>
                            <div style="width: 350px; display: none" id="confirm">
                                <table>
                                    <tr>
                                        <td rowspan="2">
                                            <dxe:ASPxImage ID="QuestionImage" runat="server" ImageUrl="~/images/generaluse/ConfirmDelete/Question.png" />
                                        </td>
                                        <td>
                                            <dxe:ASPxLabel ID="TemplateMessageLabel" runat="server" meta:resourcekey="TemplateMessageLabelResource"
                                                Text="¿La planilla a importar ya existe, desea eliminar la planilla existente antes de ser importada?" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table>
                                    <tr>
                                        <td></td>
                                        <td style="width: 100%"></td>
                                        <td>
                                            <dxe:ASPxButton ID="btnaccepton" runat="server" Width="50px" AutoPostBack="False"
                                                Text="Aceptar" ClientInstanceName="btnaccepton" meta:resourcekey="btnacceptonResource"
                                                Image-Url="~/images/16x16/Commands/accept.png">
                                                <ClientSideEvents Click="function(s, e) {Role.PerformCallback('importconf_'+ExcelFileName.GetValue());popupImport.Hide();}" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btncancelon" runat="server" Width="50px" AutoPostBack="False"
                                                Text="Cancelar" ClientInstanceName="btncancelon" meta:resourcekey="btncancelonResource"
                                                Image-Url="~/images/16x16/Commands/cancel.png">
                                                <ClientSideEvents Click="function(s, e) {popupImport.Hide();}" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                ID="popupSaveTemplate" runat="server" ClientInstanceName="popupSaveTemplate"
                meta:resourcekey="popupSaveTemplateResource" Modal="true" HeaderText="Características de la plantilla"
                HeaderImage-Url="/images/16x16/Toolbar/pageExcel.png">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlSaveTemplate" runat="server">
                        <div style="width: 350px">
                            <table width="100%">
                                <tr>
                                    <td colspan="2">
                                        <dxe:ASPxCheckBox ID="WithDataCheckBox" ClientInstanceName="WithDataCheckBox" runat="server"
                                            meta:resourcekey="WithDataCheckBoxResource" Text="Con datos" ToolTip="Se generar la plantilla con los datos de la tabla en caso contrario solamente la estructura" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <dxe:ASPxCheckBox ID="WithLookupCheckBox" ClientInstanceName="WithLookupCheckBox"
                                            meta:resourcekey="WithLookupCheckBoxResource" runat="server" Text="Con lista de valores"
                                            ToolTip="Se generan las columnas con la lista de valores posibles" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxButton ID="GenerateButton" runat="server" Width="50px" AutoPostBack="False"
                                                        Image-Url="~/images/16x16/Operations/config.png" Text="Generar" ClientInstanceName="GenerateButton"
                                                        meta:resourcekey="GenerateButtonResource">
                                                        <ClientSideEvents Click="function(s, e)
                                                 {Role.PerformCallback('template.'+WithDataCheckBox.GetChecked()+'.'+WithLookupCheckBox.GetChecked());
                                                 popupSaveTemplate.Hide();}" />
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td>
                                                    <dxe:ASPxButton ID="ExitButton" runat="server" Width="50px" AutoPostBack="False"
                                                        Image-Url="~/images/16x16/Commands/cancel.png" Text="Cancelar" ClientInstanceName="ExitButton"
                                                        meta:resourcekey="ExitButtonResource">
                                                        <ClientSideEvents Click="function(s, e) {popupSaveTemplate.Hide();}" />
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
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