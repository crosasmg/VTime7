<%@ Page Title="" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false"
    CodeFile="Consumidor.aspx.vb" Inherits="Maintenance_Consumidor" meta:resourcekey="PageResource" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <script type="text/javascript">

        function btnYes_Click(s, e) {
            popupDelete.Hide();
            if (CurrentGrid.GetSelectedRowCount() != 0) {
                CurrentGrid.PerformCallback('delete');
                CurrentGrid.PerformCallback('');
                MainMenu.GetItemByName('EditRegisterItem').SetEnabled(false);
	                            
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
                        case 'Consumidor_ViewItem':     
     
     
     CurrentGrid = Consumidor;
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
                                    case 'TemplateItemMenu':
                                        break;
                                    
                                    case 'export_pdf':
                                    case 'export_xls':
                                    case 'export_xlsx':
                                    case 'export_rtf':
                                    case 'export_csv':
                                        CurrentGrid.PerformCallback(e.item.name);
                                        break;
                                    case 'HelpItemMenu':
                                        popupHelp.Show();
                                    
                                    default:
                                        e.processOnServer = true;}}" />

                            <Items>
                                
                                <dxm:MenuItem  Name="AddRegisterItem" Text="Agregar" meta:resourcekey="AddRegisterItem" Image-UrlDisabled="/images/16x16/Toolbar/disabledAdd.png" 
                             Image-Url="/images/16x16/Toolbar/add.png">
                            <ItemStyle Width="5%" />
                                </dxm:MenuItem>
                                <dxm:MenuItem BeginGroup="True" Name="EditRegisterItem" Text="Editar" Image-UrlDisabled="/images/16x16/Toolbar/disabledEdit.png" 
                            Image-Url="/images/16x16/Toolbar/edit.png" meta:resourcekey="EditRegisterItem" ClientEnabled="false">
                           <ItemStyle Width="5%" />
                                </dxm:MenuItem>
                                
                                <dxm:MenuItem BeginGroup="True" Text="" Enabled="False">
                                    <ItemStyle Width="85%" />
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
                    <td width="15px">
                    </td>
                </tr>
                <tr>
                    <td width="15px">
                    <p style="font-weight:lighter"></p>
                    </td>
                    <td width="1000px">
                      
                      <dxp:ASPxPanel ID="Consumidor_View" ClientInstanceName="Consumidor_View" runat="server" ClientVisible="True" Width="100%" >
    <PanelCollection>        <dxp:PanelContent ID="Consumidor_ViewPanel" runat="server" SupportsDisabledAttribute="True" >

<dxwgv:ASPxGridView AutoGenerateColumns='False' ClientInstanceName='Consumidor' ID='Consumidor' runat='server' Width='100%' KeyFieldName='ID_CONSUMIDOR;ID_COMPANIA' Caption='Consumer' Enabled="True" ClientVisible ="True" meta:resourcekey="ConsumidorResource" EnableRowsCache="False" EnableViewState="False" KeyboardSupport="False" EnableCallbackCompression="True" EnableCallBacks="True">
<SettingsPager PageSize="20"/>
<SettingsBehavior AllowFocusedRow="True"/>
<SettingsEditing Mode="Inline" />
 <ClientSideEvents RowDblClick="function(s, e) {s.StartEditRow(e.visibleIndex);}"  FocusedRowChanged="function(s, e) {MainMenu.GetItemByName('EditRegisterItem').SetEnabled(s.GetFocusedRowIndex()>-1);}" />
<Columns>
<dxwgv:GridViewDataTextColumn Name='Id_Consumidor' FieldName='ID_CONSUMIDOR' Caption='Consumer Id' ToolTip='Code of the application.' VisibleIndex="0" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="8%" Visible="True" meta:resourcekey="Id_Consumidor">
<EditFormSettings VisibleIndex="0" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="##,###,###,##0" Size='10'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..9999999999g>" />
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Consumer Id" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Id_Compania' FieldName='ID_COMPANIA' Caption='Company Id' ToolTip='Code that identifies the user company owner of the information.' VisibleIndex="1" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="4%" Visible="True" meta:resourcekey="Id_Compania">
<EditFormSettings VisibleIndex="1" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Company Id" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Nombre_Consumidor' FieldName='NOMBRE_CONSUMIDOR' Caption='Consumer Name' ToolTip='Application name.' VisibleIndex="2" CellStyle-HorizontalAlign="Left" Width="41%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="Nombre_Consumidor">
<EditFormSettings VisibleIndex="2" Visible="True"/>
<PropertiesTextEdit Size='50' MaxLength='50'>
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Consumer Name" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Identificador_Consumidor' FieldName='IDENTIFICADOR_CONSUMIDOR' Caption='Consumer Identifier' ToolTip='Unique name of the application. (Identifier).' VisibleIndex="3" CellStyle-HorizontalAlign="Left" Width="41%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="Identificador_Consumidor">
<EditFormSettings VisibleIndex="3" Visible="True"/>
<PropertiesTextEdit Size='50' MaxLength='50'>
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Consumer Identifier" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Vida_Token_Acceso' FieldName='VIDA_TOKEN_ACCESO' Caption='Access Token Lifetime' ToolTip='Lifetime of access token in seconds.' VisibleIndex="4" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="5%" Visible="True" meta:resourcekey="Vida_Token_Acceso">
<EditFormSettings VisibleIndex="4" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,##0" Size='6'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999999g>" />
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Access Token Lifetime" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewCommandColumn VisibleIndex="5" ButtonType="Image" Caption=" " Width="24px">
<EditButton Visible="true">
<Image Url="~/images/empty.png"/>
</EditButton>
<CancelButton>
<Image Url="~/images/16x16/Commands/cancel.png"/>
</CancelButton>
<UpdateButton>
<Image Url="~/images/16x16/Commands/accept.png"/>
</UpdateButton>
</dxwgv:GridViewCommandColumn>
</Columns>
 </dxwgv:ASPxGridView>

                                    </dxp:PanelContent>                                </PanelCollection>                            </dxp:ASPxPanel>

                    </td>
                   <td width="15px">
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
            <script type="text/javascript">                
                 AddKeyboardNavigationTo(Consumidor);

                 HandlerView('Consumidor_ViewItem')                
            </script> 
            <dxpc:ASPxPopupControl ID="popupHelp" runat="server" AllowDragging="True" ContentStyle-Paddings-Padding="0"
                ClientInstanceName="popupHelp" HeaderText="Ayuda" Height="400px" meta:resourcekey="popupHelpResource"
                Modal="True" ModalBackgroundStyle-BackgroundImage-HorizontalPosition="center"
                PopupHorizontalAlign="LeftSides" PopupVerticalAlign="TopSides" ShowPageScrollbarWhenModal="True"
                Width="400px" ContentUrl="~/generated/crud/help/Consumidor.html" AllowResize="True"
                ShowMaximizeButton="False" PopupHorizontalOffset="20" PopupVerticalOffset="90">
                <ModalBackgroundStyle>
                    <BackgroundImage HorizontalPosition="center" />
                </ModalBackgroundStyle>
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControlHelp" runat="server" SupportsDisabledAttribute="True">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
       <dxwgv:ASPxGridViewExporter ID="ASPxGridViewExporter" runat="server"/>
       <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                ID="popupDelete" runat="server" ClientInstanceName="popupDelete" Modal="true" meta:resourcekey="popupDeleteResource"
                HeaderText="Confirmación de borrado" HeaderImage-Url="/images/16x16/Toolbar/deleteRow.png">               
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
                                        <dxe:ASPxLabel ID="DeleteRowsLabel" runat="server"
                                        Text="¿Está seguro de querer eliminar las filas seleccionadas?" meta:resourcekey="DeleteRowsLabelResource" >
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
         
            
            <dx:ASPxHiddenField ID="CurrentState" runat="server" />
        </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>