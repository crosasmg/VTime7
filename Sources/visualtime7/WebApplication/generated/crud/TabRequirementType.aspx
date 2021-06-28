<%@ Page Title="" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false"
    CodeFile="TabRequirementType.aspx.vb" Inherits="Maintenance_TabRequirementType" meta:resourcekey="PageResource" %>

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
                        case 'TabRequirementType_ViewItem':     
     MainMenu.GetItemByName('ViewItem').SetText('<%=GetLocalResourceObject("ViewItemMenu")%>' + '<%=GetLocalResourceObject("TabRequirementType_ViewItem.Text")%>');
           TabRequirementType_View.SetClientVisible(true);
           MainMenu.GetItemByName('TabRequirementType_ViewItem').SetVisible(false);
       TransRequirementType_View.SetClientVisible(false);
  MainMenu.GetItemByName('TransRequirementType_ViewItem').SetVisible(true);
  MainMenu.GetItemByName('LanguageItem').SetVisible(true);
  MainMenu.GetItemByName('AddRegisterItem').SetVisible(true);
  MainMenu.GetItemByName('RemoveRegisterItem').SetVisible(true);
     CurrentGrid = TabRequirementType_Grid;
     CurrentGrid.PerformCallback('');
     break;
case 'TransRequirementType_ViewItem':     
     MainMenu.GetItemByName('ViewItem').SetText('<%=GetLocalResourceObject("ViewItemMenu")%>' + '<%=GetLocalResourceObject("TransRequirementType_ViewItem.Text")%>');
           TransRequirementType_View.SetClientVisible(true);
           MainMenu.GetItemByName('TransRequirementType_ViewItem').SetVisible(false);
       TabRequirementType_View.SetClientVisible(false);
  MainMenu.GetItemByName('TabRequirementType_ViewItem').SetVisible(true);
  MainMenu.GetItemByName('LanguageItem').SetVisible(false);
  MainMenu.GetItemByName('AddRegisterItem').SetVisible(false);
  MainMenu.GetItemByName('RemoveRegisterItem').SetVisible(false);
     CurrentGrid = TransRequirementType_Grid;
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
                       case 'TabRequirementType_ViewItem':
  case 'TransRequirementType_ViewItem':
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
                                            <dxm:MenuItem Name="TabRequirementType_ViewItem" Text="Standard" meta:resourcekey="TabRequirementType_ViewItem"/>
<dxm:MenuItem Name="TransRequirementType_ViewItem" Text="Translator" meta:resourcekey="TransRequirementType_ViewItem"/>

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
                      
                      <dxp:ASPxPanel ID="TabRequirementType_View" ClientInstanceName="TabRequirementType_View" runat="server" ClientVisible="True" Width="100%" >
    <PanelCollection>        <dxp:PanelContent ID="TabRequirementType_ViewPanel" runat="server" SupportsDisabledAttribute="True" >

<dxwgv:ASPxGridView AutoGenerateColumns='False' ClientInstanceName='TabRequirementType_Grid' ID='TabRequirementType_Grid' runat='server' Width='100%' KeyFieldName='REQUIREMENTTYPE' Caption='Types of the requirement' Enabled="True" ClientVisible ="True" meta:resourcekey="TabRequirementType_GridResource" EnableRowsCache="False" EnableViewState="False" KeyboardSupport="False" EnableCallbackCompression="True" EnableCallBacks="True">
<SettingsPager PageSize="20"/>
<SettingsBehavior AllowFocusedRow="True"/>
<SettingsEditing Mode="Inline" />
<ClientSideEvents RowDblClick="function(s, e) {s.StartEditRow(e.visibleIndex);}" FocusedRowChanged="function(s, e) {MainMenu.GetItemByName('EditRegisterItem').SetEnabled(s.GetFocusedRowIndex()>-1);}" SelectionChanged="function(s, e) {MainMenu.GetItemByName('RemoveRegisterItem').SetEnabled(s.GetSelectedRowCount()>0);}" />
<Columns>
<dxwgv:GridViewCommandColumn VisibleIndex="0" ButtonType="Image" Width="8px" ShowSelectCheckbox="True" />
<dxwgv:GridViewDataTextColumn Name='RequirementType' FieldName='REQUIREMENTTYPE' Caption='Requirement Type' ToolTip='Requirement Type.' VisibleIndex="0" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="1%" Visible="True" meta:resourcekey="RequirementType">
<EditFormSettings VisibleIndex="0" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Requirement Type" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='ProcessType' FieldName='PROCESSTYPE' Caption='Process Type' ToolTip='Process Type.' VisibleIndex="1" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="9%" Visible="True" meta:resourcekey="ProcessType" >
<EditFormSettings VisibleIndex="1" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='PROCESSTYPE'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Process Type" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='UnderwritingArea' FieldName='UNDERWRITINGAREA' Caption='Underwriting Area' ToolTip='Underwriting Area.' VisibleIndex="2" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="9%" Visible="True" meta:resourcekey="UnderwritingArea" >
<EditFormSettings VisibleIndex="2" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='UNDERWRITINGAREA'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Underwriting Area" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='Payer' FieldName='PAYER' Caption='Payer' ToolTip='Payer.' VisibleIndex="3" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="9%" Visible="True" meta:resourcekey="Payer" >
<EditFormSettings VisibleIndex="3" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='PAYER'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Payer" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='Cost' FieldName='COST' Caption='Cost' ToolTip='Cost.' VisibleIndex="4" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="3%" Visible="True" meta:resourcekey="Cost">
<EditFormSettings VisibleIndex="4" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,###,###,###,###,##0.00" Size='21'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999999999999999999g>.<00..99>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Link' FieldName='LINK' Caption='Link' ToolTip='Link.' VisibleIndex="5" CellStyle-HorizontalAlign="Left" Width="45%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="Link">
<EditFormSettings VisibleIndex="5" Visible="True"/>
<PropertiesTextEdit Size='300' MaxLength='300'>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='AcordRequirementCode' FieldName='ACORDREQUIREMENTCODE' Caption='Acord Requirement Code' ToolTip='Acord Requirement Code.' VisibleIndex="6" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="1%" Visible="True" meta:resourcekey="AcordRequirementCode">
<EditFormSettings VisibleIndex="6" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='RecordStatus' FieldName='RECORDSTATUS' Caption='Record Status' ToolTip='Status of the record.' VisibleIndex="7" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="5%" Visible="True" meta:resourcekey="RecordStatus" >
<EditFormSettings VisibleIndex="7" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='RECORDSTATUS'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Record Status" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='Description' FieldName='DESCRIPTION' Caption='Description' ToolTip='Description associated with the code.' VisibleIndex="8" CellStyle-HorizontalAlign="Left" Width="9%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="Description">
<EditFormSettings VisibleIndex="8" Visible="True"/>
<PropertiesTextEdit Size='60' MaxLength='60'>
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Description" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='ShortDescription' FieldName='SHORTDESCRIPTION' Caption='Short Description' ToolTip='Brief description associated with the code.' VisibleIndex="9" CellStyle-HorizontalAlign="Left" Width="9%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="ShortDescription">
<EditFormSettings VisibleIndex="9" Visible="True"/>
<PropertiesTextEdit Size='60' MaxLength='60'>
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Short Description" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewCommandColumn VisibleIndex="10" ButtonType="Image" Caption=" " Width="24px">
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
<dxp:ASPxPanel ID="TransRequirementType_View" ClientInstanceName="TransRequirementType_View" runat="server" ClientVisible="False" Width="100%" >
    <PanelCollection>        <dxp:PanelContent ID="TransRequirementType_ViewPanel" runat="server" SupportsDisabledAttribute="True" >

<dxwgv:ASPxGridView AutoGenerateColumns='False' ClientInstanceName='TransRequirementType_Grid' ID='TransRequirementType_Grid' runat='server' Width='100%' KeyFieldName='REQUIREMENTTYPE;LANGUAGEID' Caption='Types of the requirement' Enabled="True" ClientVisible ="True" meta:resourcekey="TransRequirementType_GridResource" EnableRowsCache="False" EnableViewState="False" KeyboardSupport="False" EnableCallbackCompression="True" EnableCallBacks="True">
<SettingsPager PageSize="20"/>
<SettingsBehavior AllowFocusedRow="True"/>
<SettingsEditing Mode="Inline" />
<ClientSideEvents RowDblClick="function(s, e) {s.StartEditRow(e.visibleIndex);}" FocusedRowChanged="function(s, e) {MainMenu.GetItemByName('EditRegisterItem').SetEnabled(s.GetFocusedRowIndex()>-1);}" SelectionChanged="function(s, e) {MainMenu.GetItemByName('RemoveRegisterItem').SetEnabled(s.GetSelectedRowCount()>0);}" />
<Columns>
<dxwgv:GridViewDataTextColumn Name='RequirementType' FieldName='REQUIREMENTTYPE' Caption='Requirement Type' ToolTip='Requirement Type.' VisibleIndex="0" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="1%" Visible="True" meta:resourcekey="RequirementType">
<EditFormSettings VisibleIndex="0" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Requirement Type" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='ProcessType' FieldName='PROCESSTYPE' Caption='Process Type' ToolTip='Process Type.' VisibleIndex="1" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="9%" Visible="True" meta:resourcekey="ProcessType" >
<EditFormSettings VisibleIndex="1" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='PROCESSTYPE'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Process Type" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='UnderwritingArea' FieldName='UNDERWRITINGAREA' Caption='Underwriting Area' ToolTip='Underwriting Area.' VisibleIndex="2" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="9%" Visible="True" meta:resourcekey="UnderwritingArea" >
<EditFormSettings VisibleIndex="2" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='UNDERWRITINGAREA'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Underwriting Area" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='Payer' FieldName='PAYER' Caption='Payer' ToolTip='Payer.' VisibleIndex="3" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="9%" Visible="True" meta:resourcekey="Payer" >
<EditFormSettings VisibleIndex="3" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='PAYER'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Payer" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='Cost' FieldName='COST' Caption='Cost' ToolTip='Cost.' VisibleIndex="4" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="3%" Visible="True" meta:resourcekey="Cost">
<EditFormSettings VisibleIndex="4" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,###,###,###,###,##0.00" Size='21'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999999999999999999g>.<00..99>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Link' FieldName='LINK' Caption='Link' ToolTip='Link.' VisibleIndex="5" CellStyle-HorizontalAlign="Left" Width="43%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="Link">
<EditFormSettings VisibleIndex="5" Visible="True"/>
<PropertiesTextEdit Size='300' MaxLength='300'>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='AcordRequirementCode' FieldName='ACORDREQUIREMENTCODE' Caption='Acord Requirement Code' ToolTip='Acord Requirement Code.' VisibleIndex="6" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="1%" Visible="True" meta:resourcekey="AcordRequirementCode">
<EditFormSettings VisibleIndex="6" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='RecordStatus' FieldName='RECORDSTATUS' Caption='Record Status' ToolTip='Status of the record.' VisibleIndex="7" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="4%" Visible="True" meta:resourcekey="RecordStatus" >
<EditFormSettings VisibleIndex="7" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='RECORDSTATUS'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Record Status" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='LanguageId' FieldName='LANGUAGEID' Caption='Language Id' ToolTip='Language information.' VisibleIndex="8" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="4%" Visible="True" meta:resourcekey="LanguageId" >
<EditFormSettings VisibleIndex="8" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='LANGUAGEID'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Language Id" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='Description' FieldName='DESCRIPTION' Caption='Description' ToolTip='Description associated with the code.' VisibleIndex="9" CellStyle-HorizontalAlign="Left" Width="9%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="Description">
<EditFormSettings VisibleIndex="9" Visible="True"/>
<PropertiesTextEdit Size='60' MaxLength='60'>
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Description" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='ShortDescription' FieldName='SHORTDESCRIPTION' Caption='Short Description' ToolTip='Brief description associated with the code.' VisibleIndex="10" CellStyle-HorizontalAlign="Left" Width="9%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="ShortDescription">
<EditFormSettings VisibleIndex="10" Visible="True"/>
<PropertiesTextEdit Size='60' MaxLength='60'>
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Short Description" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewCommandColumn VisibleIndex="11" ButtonType="Image" Caption=" " Width="24px">
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
                 AddKeyboardNavigationTo(TabRequirementType_Grid);
AddKeyboardNavigationTo(TransRequirementType_Grid);

                 HandlerView('TabRequirementType_ViewItem')                
            </script> 
            <dxpc:ASPxPopupControl ID="popupHelp" runat="server" AllowDragging="True" ContentStyle-Paddings-Padding="0"
                ClientInstanceName="popupHelp" HeaderText="Ayuda" Height="400px" meta:resourcekey="popupHelpResource"
                Modal="True" ModalBackgroundStyle-BackgroundImage-HorizontalPosition="center"
                PopupHorizontalAlign="LeftSides" PopupVerticalAlign="TopSides" ShowPageScrollbarWhenModal="True"
                Width="400px" ContentUrl="~/generated/crud/help/TabRequirementType.html" AllowResize="True"
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
