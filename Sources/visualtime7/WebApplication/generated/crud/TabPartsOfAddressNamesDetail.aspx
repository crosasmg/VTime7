<%@ Page Title="" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false"
    CodeFile="TabPartsOfAddressNamesDetail.aspx.vb" Inherits="Maintenance_TabPartsOfAddressNamesDetail" meta:resourcekey="PageResource" %>

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
                MainMenu.GetItemByName('RemoveRegisterItem').SetEnabled(false);               
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
                        case 'TabPartsOfAddressNamesDetail_ViewItem':     
     MainMenu.GetItemByName('ViewItem').SetText('<%=GetLocalResourceObject("ViewItemMenu")%>' + '<%=GetLocalResourceObject("TabPartsOfAddressNamesDetail_ViewItem.Text")%>');
           TabPartsOfAddressNamesDetail_View.SetClientVisible(true);
           MainMenu.GetItemByName('TabPartsOfAddressNamesDetail_ViewItem').SetVisible(false);
       TransPartsOfAddressNamesDetail_View.SetClientVisible(false);
  MainMenu.GetItemByName('TransPartsOfAddressNamesDetail_ViewItem').SetVisible(true);
  MainMenu.GetItemByName('LanguageItem').SetVisible(true);
  MainMenu.GetItemByName('AddRegisterItem').SetVisible(true);
  MainMenu.GetItemByName('RemoveRegisterItem').SetVisible(true);
     CurrentGrid = TabPartsOfAddressNamesDetail_Grid;
     CurrentGrid.PerformCallback('');
     break;
case 'TransPartsOfAddressNamesDetail_ViewItem':     
     MainMenu.GetItemByName('ViewItem').SetText('<%=GetLocalResourceObject("ViewItemMenu")%>' + '<%=GetLocalResourceObject("TransPartsOfAddressNamesDetail_ViewItem.Text")%>');
           TransPartsOfAddressNamesDetail_View.SetClientVisible(true);
           MainMenu.GetItemByName('TransPartsOfAddressNamesDetail_ViewItem').SetVisible(false);
       TabPartsOfAddressNamesDetail_View.SetClientVisible(false);
  MainMenu.GetItemByName('TabPartsOfAddressNamesDetail_ViewItem').SetVisible(true);
  MainMenu.GetItemByName('LanguageItem').SetVisible(false);
  MainMenu.GetItemByName('AddRegisterItem').SetVisible(false);
  MainMenu.GetItemByName('RemoveRegisterItem').SetVisible(false);
     CurrentGrid = TransPartsOfAddressNamesDetail;
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
                                    case 'ViewItem':                                       
                       case 'TabPartsOfAddressNamesDetail_ViewItem':
  case 'TransPartsOfAddressNamesDetail_ViewItem':
HandlerView(e.item.name);
break;

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
                                <dxm:MenuItem BeginGroup="True"  Name="RemoveRegisterItem" Text="Eliminar" Image-UrlDisabled="/images/16x16/Toolbar/disabledDelete.png" 
                                Image-Url="/images/16x16/Toolbar/delete.png" meta:resourcekey="RemoveRegisterItem" ClientEnabled="false">
                                    <ItemStyle Width="5%" />
                                </dxm:MenuItem>
                                <dxm:MenuItem BeginGroup="True" Text="" Enabled="False">
                                    <ItemStyle Width="85%" />
                                </dxm:MenuItem>
                                <dxm:MenuItem BeginGroup="True" Name="LanguageItem" Text="Language" DropDownMode="True" Image-Url="/images/16x16/Toolbar/language.png">                                       
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
                                <dxm:MenuItem BeginGroup="True" Name="ViewItem" Text="View" DropDownMode="True" Image-Url="/images/16x16/Toolbar/view.png">
                                        <Items>
                                            <dxm:MenuItem Name="TabPartsOfAddressNamesDetail_ViewItem" Text="Standard" meta:resourcekey="TabPartsOfAddressNamesDetail_ViewItem"/>
<dxm:MenuItem Name="TransPartsOfAddressNamesDetail_ViewItem" Text="Translator" meta:resourcekey="TransPartsOfAddressNamesDetail_ViewItem"/>

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
                    <p style="font-weight:lighter"></p>
                    </td>
                    <td width="1000px">
                      
                      <dxp:ASPxPanel ID="TabPartsOfAddressNamesDetail_View" ClientInstanceName="TabPartsOfAddressNamesDetail_View" runat="server" ClientVisible="True" Width="100%" >
    <PanelCollection>        <dxp:PanelContent ID="TabPartsOfAddressNamesDetail_ViewPanel" runat="server" SupportsDisabledAttribute="True" >

<dxwgv:ASPxGridView AutoGenerateColumns='False' ClientInstanceName='TabPartsOfAddressNamesDetail_Grid' ID='TabPartsOfAddressNamesDetail_Grid' runat='server' Width='100%' KeyFieldName='COUNTRYID;TYPEOFROUTE;PARTNAMEID;PARTNAMEDETAILID' Caption='Descriptions of the identification of levels of physical address table' Enabled="True" ClientVisible ="True" meta:resourcekey="TabPartsOfAddressNamesDetail_GridResource" EnableRowsCache="False" EnableViewState="False" KeyboardSupport="False" EnableCallbackCompression="True" EnableCallBacks="True">
<SettingsPager PageSize="20"/>
<SettingsBehavior AllowFocusedRow="True"/>
<SettingsEditing Mode="Inline" />
<ClientSideEvents RowDblClick="function(s, e) {s.StartEditRow(e.visibleIndex);}" FocusedRowChanged="function(s, e) {MainMenu.GetItemByName('EditRegisterItem').SetEnabled(s.GetFocusedRowIndex()>-1);}" SelectionChanged="function(s, e) {MainMenu.GetItemByName('RemoveRegisterItem').SetEnabled(s.GetSelectedRowCount()>0);}" />
<Columns>
<dxwgv:GridViewCommandColumn VisibleIndex="0" ButtonType="Image" Width="8px" ShowSelectCheckbox="True" />
<dxwgv:GridViewDataComboBoxColumn Name='CountryID' FieldName='COUNTRYID' Caption='Country' ToolTip='Code of the country.' VisibleIndex="0" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="CountryID" >
<EditFormSettings VisibleIndex="0" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='COUNTRYID'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Country" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='TypeOfRoute' FieldName='TYPEOFROUTE' Caption='Type of Route' ToolTip='Type of route.' VisibleIndex="1" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="TypeOfRoute" >
<EditFormSettings VisibleIndex="1" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='TYPEOFROUTE'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Type of Route" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='PartNameID' FieldName='PARTNAMEID' Caption='Part Name ID' ToolTip='Identification of levels of physical address.' VisibleIndex="2" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="10%" Visible="True" meta:resourcekey="PartNameID">
<EditFormSettings VisibleIndex="2" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Part Name ID" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='PartNameDetailID' FieldName='PARTNAMEDETAILID' Caption='Part Name Detail ID' ToolTip='Identification of details of levels of physical address.' VisibleIndex="3" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="10%" Visible="True" meta:resourcekey="PartNameDetailID">
<EditFormSettings VisibleIndex="3" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Part Name Detail ID" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='RecordStatus' FieldName='RECORDSTATUS' Caption='Record Status' ToolTip='General status of the record.' VisibleIndex="4" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="RecordStatus" >
<EditFormSettings VisibleIndex="4" Visible="True"/>
<PropertiesComboBox ValueType="System.String" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='RECORDSTATUS'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Record Status" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='Description' FieldName='DESCRIPTION' Caption='Description' ToolTip='Description of the location name.' VisibleIndex="5" CellStyle-HorizontalAlign="Left" Width="58%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="Description">
<EditFormSettings VisibleIndex="5" Visible="True"/>
<PropertiesTextEdit Size='30' MaxLength='30'>
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Description" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='ShortDescription' FieldName='SHORTDESCRIPTION' Caption='Short Description' ToolTip='Abbreviated description of the location name.' VisibleIndex="6" CellStyle-HorizontalAlign="Left" Width="23%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="ShortDescription">
<EditFormSettings VisibleIndex="6" Visible="True"/>
<PropertiesTextEdit Size='12' MaxLength='12'>
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Short Description" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewCommandColumn VisibleIndex="7" ButtonType="Image" Caption=" " Width="24px">
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
<dxp:ASPxPanel ID="TransPartsOfAddressNamesDetail_View" ClientInstanceName="TransPartsOfAddressNamesDetail_View" runat="server" ClientVisible="False" Width="100%" >
    <PanelCollection>        <dxp:PanelContent ID="TransPartsOfAddressNamesDetail_ViewPanel" runat="server" SupportsDisabledAttribute="True" >

<dxwgv:ASPxGridView AutoGenerateColumns='False' ClientInstanceName='TransPartsOfAddressNamesDetail' ID='TransPartsOfAddressNamesDetail' runat='server' Width='100%' KeyFieldName='COUNTRYID;TYPEOFROUTE;PARTNAMEID;PARTNAMEDETAILID;LANGUAGEID' Caption='Trans Parts of Address Names Detail' Enabled="True" ClientVisible ="True" meta:resourcekey="TransPartsOfAddressNamesDetailResource" EnableRowsCache="False" EnableViewState="False" KeyboardSupport="False" EnableCallbackCompression="True" EnableCallBacks="True">
<SettingsPager PageSize="20"/>
<SettingsBehavior AllowFocusedRow="True"/>
<SettingsEditing Mode="Inline" />
<ClientSideEvents RowDblClick="function(s, e) {s.StartEditRow(e.visibleIndex);}" FocusedRowChanged="function(s, e) {MainMenu.GetItemByName('EditRegisterItem').SetEnabled(s.GetFocusedRowIndex()>-1);}" SelectionChanged="function(s, e) {MainMenu.GetItemByName('RemoveRegisterItem').SetEnabled(s.GetSelectedRowCount()>0);}" />
<Columns>
<dxwgv:GridViewDataComboBoxColumn Name='CountryID' FieldName='COUNTRYID' Caption='Country' ToolTip='Code of the country.' VisibleIndex="0" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="CountryID" >
<EditFormSettings VisibleIndex="0" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='COUNTRYID'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Country" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='TypeOfRoute' FieldName='TYPEOFROUTE' Caption='Type of Route' ToolTip='Type of route.' VisibleIndex="1" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="TypeOfRoute" >
<EditFormSettings VisibleIndex="1" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='TYPEOFROUTE'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Type of Route" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='PartNameID' FieldName='PARTNAMEID' Caption='Part Name ID' ToolTip='Identification of levels of physical address.' VisibleIndex="2" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="10%" Visible="True" meta:resourcekey="PartNameID">
<EditFormSettings VisibleIndex="2" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Part Name ID" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='PartNameDetailID' FieldName='PARTNAMEDETAILID' Caption='Part Name Detail ID' ToolTip='Identification of details of levels of physical address.' VisibleIndex="3" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="10%" Visible="True" meta:resourcekey="PartNameDetailID">
<EditFormSettings VisibleIndex="3" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Part Name Detail ID" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='RecordStatus' FieldName='RECORDSTATUS' Caption='Record Status' ToolTip='General status of the record.' VisibleIndex="4" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="RecordStatus" >
<EditFormSettings VisibleIndex="4" Visible="True"/>
<PropertiesComboBox ValueType="System.String" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='RECORDSTATUS'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Record Status" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='LanguageID' FieldName='LANGUAGEID' Caption='Language ID' ToolTip='Language in which the description is indicated' VisibleIndex="5" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="LanguageID" >
<EditFormSettings VisibleIndex="5" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='LANGUAGEID'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Language ID" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='Description' FieldName='DESCRIPTION' Caption='Description' ToolTip='Description of the location name.' VisibleIndex="6" CellStyle-HorizontalAlign="Left" Width="58%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="Description">
<EditFormSettings VisibleIndex="6" Visible="True"/>
<PropertiesTextEdit Size='30' MaxLength='30'>
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Description" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='ShortDescription' FieldName='SHORTDESCRIPTION' Caption='Short Description' ToolTip='Abbreviated description of the location name.' VisibleIndex="7" CellStyle-HorizontalAlign="Left" Width="23%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="ShortDescription">
<EditFormSettings VisibleIndex="7" Visible="True"/>
<PropertiesTextEdit Size='12' MaxLength='12'>
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Short Description" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewCommandColumn VisibleIndex="8" ButtonType="Image" Caption=" " Width="24px">
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
                 AddKeyboardNavigationTo(TabPartsOfAddressNamesDetail_Grid);
AddKeyboardNavigationTo(TransPartsOfAddressNamesDetail);

                 HandlerView('TabPartsOfAddressNamesDetail_ViewItem')                
            </script> 
            <dxpc:ASPxPopupControl ID="popupHelp" runat="server" AllowDragging="True" ContentStyle-Paddings-Padding="0"
                ClientInstanceName="popupHelp" HeaderText="Ayuda" Height="400px" meta:resourcekey="popupHelpResource"
                Modal="True" ModalBackgroundStyle-BackgroundImage-HorizontalPosition="center"
                PopupHorizontalAlign="LeftSides" PopupVerticalAlign="TopSides" ShowPageScrollbarWhenModal="True"
                Width="400px" ContentUrl="~/generated/crud/help/TabPartsOfAddressNamesDetail.html" AllowResize="True"
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
