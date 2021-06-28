<%@ Control Language="VB" AutoEventWireup="false" CodeFile="RequirementsAttachments.ascx.vb"
    Inherits="Underwriting_Controls_RequirementsAttachements" %>

<script type="text/javascript">

</script>

<div style="width: 100%">
            <dxwgv:ASPxGridView ID="gvAttachments" runat="server" 
                Width="100%" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                AutoGenerateColumns="False" 
                DataSourceID="RequirementsAttachments_AttachmentsCollection" KeyFieldName="AttachmentsID"
                ClientInstanceName="gvAttachments" 
                meta:resourcekey="gvAttachmentsResource1" EnableViewState="False">
                <Templates>
                    <EditForm>
                        <div style="padding: 4px 4px 3px 4px">
                            <table width="100%">
                                <tr>
                                    <td width="10%">
                                        <dxe:ASPxLabel ID="lblName" runat="server" Text="Nombre:" 
                                            ClientInstanceName="lblName" meta:resourcekey="lblNameResource1">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td width="40%">
                                        <dxe:ASPxTextBox ID="txtName" runat="server" Width="300px" Text='<%# Eval("FileName") %>'
                                            ReadOnly="True" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" 
                                            CssPostfix="SoftOrange" ondatabound="txtName_DataBound" 
                                            SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" 
                                            ClientInstanceName="txtName" meta:resourcekey="txtNameResource1">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td width="10%">
                                        <dxe:ASPxLabel ID="lblDescription" runat="server" Text="Descripción:" 
                                            meta:resourcekey="lblDescriptionResource1">
                                        </dxe:ASPxLabel>
                                    </td>
                                    <td width="40%">
                                        <dxe:ASPxTextBox ID="txtDescription" runat="server" Width="300px" 
                                            Text='<%# Eval("FileDescription") %>' 
                                            CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" 
                                            CssPostfix="SoftOrange"
                                            SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" 
                                            ondatabound="txtDescription_DataBound" 
                                            meta:resourcekey="txtDescriptionResource1">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <dx:ASPxUploadControl ID="uploadFile" runat="server" CancelButtonHorizontalPosition="Right"
                                            ClientInstanceName="uploader" ShowProgressPanel="True" Width="100%" 
                                            onfilesuploadcomplete="uploadFile_FilesUploadComplete" 
                                            meta:resourcekey="uploadFileResource1">
                                            <ClientSideEvents FileUploadComplete="function(s, e) { if (e.isValid) { gvAttachments.UpdateEdit(); }}" />
                                            <UploadButton Text="Cargar">
                                            </UploadButton>
                                        </dx:ASPxUploadControl>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                            <dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="Salvar" 
                                Cursor="pointer" Font-Underline="True" 
                                meta:resourcekey="ASPxHyperLink1Resource1">
                            <ClientSideEvents Click="function(s, e) {        
        if (uploader.GetText() != &quot;&quot;) { 
            uploader.Upload();
        }
        else {
            gvAttachments.UpdateEdit();
        }
}"/>
                            </dxe:ASPxHyperLink>
                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" 
                                runat="server" ColumnID="">
                            </dxwgv:ASPxGridViewTemplateReplacement>
                        </div>
                    </EditForm>
                </Templates>
                <SettingsBehavior ConfirmDelete="True" />
                <Styles CssPostfix="SoftOrange" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css">
                </Styles>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Id" FieldName="AttachmentsID" Visible="False"
                        VisibleIndex="0" meta:resourcekey="GridViewDataTextColumnResource1">
                        <EditFormSettings Visible="False" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Name="colShow" VisibleIndex="0" Width="3%" 
                        meta:resourcekey="GridViewDataTextColumnResource2">
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <dxe:ASPxButton ID="btnWatch" runat="server" EnableDefaultAppearance="False" EnableTheming="False"
                                EnableViewState="False" Height="16px" ToolTip="Ver documento" Width="16px" 
                                AutoPostBack="False" Cursor="pointer" meta:resourcekey="btnWatchResource1">
                                <Image Url="../Images/page_white_find.png">
                                </Image>
                            </dxe:ASPxButton>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Fecha" Name="colDate" VisibleIndex="1" 
                        FieldName="UploadedDate" Width="15%" 
                        meta:resourcekey="GridViewDataDateColumnResource1">
                        <PropertiesDateEdit DisplayFormatString="">
                        </PropertiesDateEdit>
                        <EditFormSettings Visible="False" />
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Nombre" Name="colName" VisibleIndex="2" 
                        FieldName="FileName" Width="20%" 
                        meta:resourcekey="GridViewDataTextColumnResource3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Descripción" Name="colDescription" 
                        VisibleIndex="3" FieldName="FileDescription" 
                        meta:resourcekey="GridViewDataTextColumnResource4">
                        <EditFormSettings Visible="False" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewCommandColumn VisibleIndex="4" Width="15%" Caption=" " 
                        meta:resourcekey="GridViewCommandColumnResource1">
                        <EditButton Visible="True" Text="Editar">
                        </EditButton>
                        <NewButton Visible="True" Text="Cargar">
                        </NewButton>
                        <DeleteButton Visible="True" Text="Eliminar">
                        </DeleteButton>
                        <CancelButton Text="Cancelar">
                        </CancelButton>
                        <UpdateButton Text="Salvar">
                        </UpdateButton>
                        <ClearFilterButton Visible="True" Text="Limpiar">
                        </ClearFilterButton>
                    </dxwgv:GridViewCommandColumn>
                </Columns>
                <Settings ShowFilterRow="True" />
                <SettingsDetail IsDetailGrid="True" />
                <SettingsText CommandDelete="¿Desea eliminar este anexo?" />
                <ClientSideEvents CallbackError="function(s,e){ gvAttachments.PerformCallback('true'); }" />
            </dxwgv:ASPxGridView>
</div>
<asp:ObjectDataSource 
    ID="RequirementsAttachments_AttachmentsCollection" 
    runat="server"
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="SelectAll"
    DeleteMethod="DeleteOnCache" 
    InsertMethod="InsertOnCache" 
    UpdateMethod="UpdateOnCache"
    TypeName="InMotionGIT.Underwriting.Proxy.Helpers.Attachment" 
    DataObjectTypeName="InMotionGIT.Underwriting.Contracts.Attachment" ></asp:ObjectDataSource>
