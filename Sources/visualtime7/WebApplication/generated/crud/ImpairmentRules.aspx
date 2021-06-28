<%@ Page Title="" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false"
    CodeFile="ImpairmentRules.aspx.vb" Inherits="Maintenance_ImpairmentRules" meta:resourcekey="PageResource" %>

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
                        case 'ImpairmentRules_ViewItem':     
     MainMenu.GetItemByName('ViewItem').SetText('<%=GetLocalResourceObject("ViewItemMenu")%>' + '<%=GetLocalResourceObject("ImpairmentRules_ViewItem.Text")%>');
           ImpairmentRules_View.SetClientVisible(true);
           MainMenu.GetItemByName('ImpairmentRules_ViewItem').SetVisible(false);
       ImpairmentRuleTrans_View.SetClientVisible(false);
  MainMenu.GetItemByName('ImpairmentRuleTrans_ViewItem').SetVisible(true);
  MainMenu.GetItemByName('LanguageItem').SetVisible(true);
  MainMenu.GetItemByName('AddRegisterItem').SetVisible(true);
  MainMenu.GetItemByName('RemoveRegisterItem').SetVisible(true);
     CurrentGrid = ImpairmentRules_Grid;
     CurrentGrid.PerformCallback('');
     break;
case 'ImpairmentRuleTrans_ViewItem':     
     MainMenu.GetItemByName('ViewItem').SetText('<%=GetLocalResourceObject("ViewItemMenu")%>' + '<%=GetLocalResourceObject("ImpairmentRuleTrans_ViewItem.Text")%>');
           ImpairmentRuleTrans_View.SetClientVisible(true);
           MainMenu.GetItemByName('ImpairmentRuleTrans_ViewItem').SetVisible(false);
       ImpairmentRules_View.SetClientVisible(false);
  MainMenu.GetItemByName('ImpairmentRules_ViewItem').SetVisible(true);
  MainMenu.GetItemByName('LanguageItem').SetVisible(false);
  MainMenu.GetItemByName('AddRegisterItem').SetVisible(false);
  MainMenu.GetItemByName('RemoveRegisterItem').SetVisible(false);
     CurrentGrid = ImpairmentRuleTrans_Grid;
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
                       case 'ImpairmentRules_ViewItem':
  case 'ImpairmentRuleTrans_ViewItem':
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
                                            <dxm:MenuItem Name="ImpairmentRules_ViewItem" Text="Standard" meta:resourcekey="ImpairmentRules_ViewItem"/>
<dxm:MenuItem Name="ImpairmentRuleTrans_ViewItem" Text="Translator" meta:resourcekey="ImpairmentRuleTrans_ViewItem"/>

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
                      
                      <dxp:ASPxPanel ID="ImpairmentRules_View" ClientInstanceName="ImpairmentRules_View" runat="server" ClientVisible="True" Width="100%" >
    <PanelCollection>        <dxp:PanelContent ID="ImpairmentRules_ViewPanel" runat="server" SupportsDisabledAttribute="True" >

<dxwgv:ASPxGridView AutoGenerateColumns='False' ClientInstanceName='ImpairmentRules_Grid' ID='ImpairmentRules_Grid' runat='server' Width='100%' KeyFieldName='IMPAIRMENTRULEID' Caption='Impairment Rules' Enabled="True" ClientVisible ="True" meta:resourcekey="ImpairmentRules_GridResource" EnableRowsCache="False" EnableViewState="False" KeyboardSupport="False" EnableCallbackCompression="True" EnableCallBacks="True">
<SettingsPager PageSize="20"/>
<SettingsBehavior AllowFocusedRow="True"/>
<SettingsEditing Mode="Inline" />
<ClientSideEvents RowDblClick="function(s, e) {s.StartEditRow(e.visibleIndex);}" FocusedRowChanged="function(s, e) {MainMenu.GetItemByName('EditRegisterItem').SetEnabled(s.GetFocusedRowIndex()>-1);}" SelectionChanged="function(s, e) {MainMenu.GetItemByName('RemoveRegisterItem').SetEnabled(s.GetSelectedRowCount()>0);}" />
<Columns>
<dxwgv:GridViewCommandColumn VisibleIndex="0" ButtonType="Image" Width="8px" ShowSelectCheckbox="True" />
<dxwgv:GridViewDataTextColumn Name='ImpairmentRuleId' FieldName='IMPAIRMENTRULEID' Caption='Impairment Rule Id' ToolTip='Impairment rule code.' VisibleIndex="0" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="1%" Visible="True" meta:resourcekey="ImpairmentRuleId">
<EditFormSettings VisibleIndex="0" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Impairment Rule Id" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='LineOfBusiness' FieldName='LINEOFBUSINESS' Caption='Line of Business' ToolTip='Code of the line of business.' VisibleIndex="1" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="1%" Visible="True" meta:resourcekey="LineOfBusiness">
<EditFormSettings VisibleIndex="1" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Product' FieldName='PRODUCT' Caption='Product' ToolTip='Code of the product.' VisibleIndex="2" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="1%" Visible="True" meta:resourcekey="Product">
<EditFormSettings VisibleIndex="2" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='CoverageCode' FieldName='COVERAGECODE' Caption='Coverage Code' ToolTip='Code of the coverage.' VisibleIndex="3" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="1%" Visible="True" meta:resourcekey="CoverageCode">
<EditFormSettings VisibleIndex="3" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='ImpairmentCode' FieldName='IMPAIRMENTCODE' Caption='Impairment Code' ToolTip='Impairment Code.' VisibleIndex="4" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="26%" Visible="True" meta:resourcekey="ImpairmentCode" >
<EditFormSettings VisibleIndex="4" Visible="True"/>
<PropertiesComboBox ValueType="System.String" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='IMPAIRMENTCODE'> 
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='DegreeId' FieldName='DEGREEID' Caption='Degree Id' ToolTip='Degree Id.' VisibleIndex="5" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="6%" Visible="True" meta:resourcekey="DegreeId" >
<EditFormSettings VisibleIndex="5" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='DEGREEID'> 
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='MortalityDebits' FieldName='MORTALITYDEBITS' Caption='Mortality Debits' ToolTip='Mortality Debits.' VisibleIndex="6" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="1%" Visible="True" meta:resourcekey="MortalityDebits">
<EditFormSettings VisibleIndex="6" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='FlatExtraPremium' FieldName='FLATEXTRAPREMIUM' Caption='Flat Extra Premium' ToolTip='Flat Extra Premium.' VisibleIndex="7" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="2%" Visible="True" meta:resourcekey="FlatExtraPremium">
<EditFormSettings VisibleIndex="7" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,###,###,###,###,##0.00" Size='21'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999999999999999999g>.<00..99>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='DOfFlatExtraPremiumDays' FieldName='DOFFLATEXTRAPREMIUMDAYS' Caption='DO fF lat Extra Premium Days' ToolTip='DOfFlat Extra Premium Days.' VisibleIndex="8" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="DOfFlatExtraPremiumDays">
<EditFormSettings VisibleIndex="8" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,##0" Size='3'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='DOfFlatExtraPremiumMonths' FieldName='DOFFLATEXTRAPREMIUMMONTHS' Caption='DO f Flat Extra Premium Months' ToolTip='DOf Flat Extra Premium Months.' VisibleIndex="9" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="DOfFlatExtraPremiumMonths">
<EditFormSettings VisibleIndex="9" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,##0" Size='3'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='DOfFlatExtraPremiumYears' FieldName='DOFFLATEXTRAPREMIUMYEARS' Caption='DO f Flat Extra Premium Years' ToolTip='DOf Flat Extra Premium Years.' VisibleIndex="10" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="DOfFlatExtraPremiumYears">
<EditFormSettings VisibleIndex="10" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,##0" Size='3'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='AlarmType' FieldName='ALARMTYPE' Caption='Alarm Type' ToolTip='Alarm Type' VisibleIndex="11" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="6%" Visible="True" meta:resourcekey="AlarmType" >
<EditFormSettings VisibleIndex="11" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='ALARMTYPE'> 
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='Decision' FieldName='DECISION' Caption='Decision' ToolTip='Decision.' VisibleIndex="12" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="6%" Visible="True" meta:resourcekey="Decision" >
<EditFormSettings VisibleIndex="12" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='DECISION'> 
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='DecisionComplement' FieldName='DECISIONCOMPLEMENT' Caption='Decision Complement' ToolTip='Decision Complement.' VisibleIndex="13" CellStyle-HorizontalAlign="Left" Width="8%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="DecisionComplement">
<EditFormSettings VisibleIndex="13" Visible="True"/>
<PropertiesTextEdit Size='80' MaxLength='80'>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='WaitingPeriodDays' FieldName='WAITINGPERIODDAYS' Caption='Waiting Period Days' ToolTip='Waiting Period Days.' VisibleIndex="14" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="WaitingPeriodDays">
<EditFormSettings VisibleIndex="14" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,##0" Size='3'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='WaitingPeriodMonths' FieldName='WAITINGPERIODMONTHS' Caption='Waiting Period Months' ToolTip='Waiting Period Months.' VisibleIndex="15" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="WaitingPeriodMonths">
<EditFormSettings VisibleIndex="15" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,##0" Size='3'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='WaitingPeriodYears' FieldName='WAITINGPERIODYEARS' Caption='Waiting Period Years' ToolTip='Waiting Period Years.' VisibleIndex="16" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="WaitingPeriodYears">
<EditFormSettings VisibleIndex="16" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,##0" Size='3'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='MaximumInsuredAmount' FieldName='MAXIMUMINSUREDAMOUNT' Caption='Maximum Insured Amount' ToolTip='Maximum Insured Amount.' VisibleIndex="17" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="2%" Visible="True" meta:resourcekey="MaximumInsuredAmount">
<EditFormSettings VisibleIndex="17" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,###,###,###,###,##0.00" Size='21'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999999999999999999g>.<00..99>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='RestrictionType' FieldName='RESTRICTIONTYPE' Caption='Restriction Type' ToolTip='Restriction Type.' VisibleIndex="18" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="6%" Visible="True" meta:resourcekey="RestrictionType" >
<EditFormSettings VisibleIndex="18" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='RESTRICTIONTYPE'> 
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='RequirementType' FieldName='REQUIREMENTTYPE' Caption='Requirement Type' ToolTip='Requirement Type.' VisibleIndex="19" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="6%" Visible="True" meta:resourcekey="RequirementType" >
<EditFormSettings VisibleIndex="19" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='REQUIREMENTTYPE'> 
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='ExclusionPeriodType' FieldName='EXCLUSIONPERIODTYPE' Caption='Exclusion Period Type' ToolTip='Exclusion Period Type.' VisibleIndex="20" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="6%" Visible="True" meta:resourcekey="ExclusionPeriodType" >
<EditFormSettings VisibleIndex="20" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='EXCLUSIONPERIODTYPE'> 
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='ExclusionType' FieldName='EXCLUSIONTYPE' Caption='Exclusion Type' ToolTip='Exclusion Type' VisibleIndex="21" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="6%" Visible="True" meta:resourcekey="ExclusionType" >
<EditFormSettings VisibleIndex="21" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='EXCLUSIONTYPE'> 
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='RecordStatus' FieldName='RECORDSTATUS' Caption='Record Status' ToolTip='Status of the record.' VisibleIndex="22" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="3%" Visible="True" meta:resourcekey="RecordStatus" >
<EditFormSettings VisibleIndex="22" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='RECORDSTATUS'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Record Status" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='Description' FieldName='DESCRIPTION' Caption='Description' ToolTip='Description associated with the code. ' VisibleIndex="23" CellStyle-HorizontalAlign="Left" Width="6%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="Description">
<EditFormSettings VisibleIndex="23" Visible="True"/>
<PropertiesTextEdit Size='60' MaxLength='60'>
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Description" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='ShortDescription' FieldName='SHORTDESCRIPTION' Caption='Short Description' ToolTip='Brief description associated with the code. ' VisibleIndex="24" CellStyle-HorizontalAlign="Left" Width="6%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="ShortDescription">
<EditFormSettings VisibleIndex="24" Visible="True"/>
<PropertiesTextEdit Size='60' MaxLength='60'>
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Short Description" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewCommandColumn VisibleIndex="25" ButtonType="Image" Caption=" " Width="24px">
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
<dxp:ASPxPanel ID="ImpairmentRuleTrans_View" ClientInstanceName="ImpairmentRuleTrans_View" runat="server" ClientVisible="False" Width="100%" >
    <PanelCollection>        <dxp:PanelContent ID="ImpairmentRuleTrans_ViewPanel" runat="server" SupportsDisabledAttribute="True" >

<dxwgv:ASPxGridView AutoGenerateColumns='False' ClientInstanceName='ImpairmentRuleTrans_Grid' ID='ImpairmentRuleTrans_Grid' runat='server' Width='100%' KeyFieldName='IMPAIRMENTRULEID;LANGUAGEID' Caption='Impairment Rules' Enabled="True" ClientVisible ="True" meta:resourcekey="ImpairmentRuleTrans_GridResource" EnableRowsCache="False" EnableViewState="False" KeyboardSupport="False" EnableCallbackCompression="True" EnableCallBacks="True">
<SettingsPager PageSize="20"/>
<SettingsBehavior AllowFocusedRow="True"/>
<SettingsEditing Mode="Inline" />
<ClientSideEvents RowDblClick="function(s, e) {s.StartEditRow(e.visibleIndex);}" FocusedRowChanged="function(s, e) {MainMenu.GetItemByName('EditRegisterItem').SetEnabled(s.GetFocusedRowIndex()>-1);}" SelectionChanged="function(s, e) {MainMenu.GetItemByName('RemoveRegisterItem').SetEnabled(s.GetSelectedRowCount()>0);}" />
<Columns>
<dxwgv:GridViewDataTextColumn Name='ImpairmentRuleId' FieldName='IMPAIRMENTRULEID' Caption='Impairment Rule Id' ToolTip='Impairment rule code.' VisibleIndex="0" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="ImpairmentRuleId">
<EditFormSettings VisibleIndex="0" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Impairment Rule Id" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='LineOfBusiness' FieldName='LINEOFBUSINESS' Caption='Line of Business' ToolTip='Code of the line of business.' VisibleIndex="1" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="LineOfBusiness">
<EditFormSettings VisibleIndex="1" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Product' FieldName='PRODUCT' Caption='Product' ToolTip='Code of the product.' VisibleIndex="2" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="Product">
<EditFormSettings VisibleIndex="2" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='CoverageCode' FieldName='COVERAGECODE' Caption='Coverage Code' ToolTip='Code of the coverage.' VisibleIndex="3" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="CoverageCode">
<EditFormSettings VisibleIndex="3" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='ImpairmentCode' FieldName='IMPAIRMENTCODE' Caption='Impairment Code' ToolTip='Impairment Code.' VisibleIndex="4" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="25%" Visible="True" meta:resourcekey="ImpairmentCode" >
<EditFormSettings VisibleIndex="4" Visible="True"/>
<PropertiesComboBox ValueType="System.String" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='IMPAIRMENTCODE'> 
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='DegreeId' FieldName='DEGREEID' Caption='Degree Id' ToolTip='Degree Id.' VisibleIndex="5" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="6%" Visible="True" meta:resourcekey="DegreeId" >
<EditFormSettings VisibleIndex="5" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='DEGREEID'> 
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='MortalityDebits' FieldName='MORTALITYDEBITS' Caption='Mortality Debits' ToolTip='Mortality Debits.' VisibleIndex="6" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="MortalityDebits">
<EditFormSettings VisibleIndex="6" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0" Size='5'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..99999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='FlatExtraPremium' FieldName='FLATEXTRAPREMIUM' Caption='Flat Extra Premium' ToolTip='Flat Extra Premium.' VisibleIndex="7" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="2%" Visible="True" meta:resourcekey="FlatExtraPremium">
<EditFormSettings VisibleIndex="7" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,###,###,###,###,##0.00" Size='21'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999999999999999999g>.<00..99>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='DOfFlatExtraPremiumDays' FieldName='DOFFLATEXTRAPREMIUMDAYS' Caption='DO fF lat Extra Premium Days' ToolTip='DOfFlat Extra Premium Days.' VisibleIndex="8" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="DOfFlatExtraPremiumDays">
<EditFormSettings VisibleIndex="8" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,##0" Size='3'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='DOfFlatExtraPremiumMonths' FieldName='DOFFLATEXTRAPREMIUMMONTHS' Caption='DO f Flat Extra Premium Months' ToolTip='DOf Flat Extra Premium Months.' VisibleIndex="9" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="DOfFlatExtraPremiumMonths">
<EditFormSettings VisibleIndex="9" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,##0" Size='3'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='DOfFlatExtraPremiumYears' FieldName='DOFFLATEXTRAPREMIUMYEARS' Caption='DO f Flat Extra Premium Years' ToolTip='DOf Flat Extra Premium Years.' VisibleIndex="10" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="DOfFlatExtraPremiumYears">
<EditFormSettings VisibleIndex="10" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,##0" Size='3'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='AlarmType' FieldName='ALARMTYPE' Caption='Alarm Type' ToolTip='Alarm Type' VisibleIndex="11" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="6%" Visible="True" meta:resourcekey="AlarmType" >
<EditFormSettings VisibleIndex="11" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='ALARMTYPE'> 
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='Decision' FieldName='DECISION' Caption='Decision' ToolTip='Decision.' VisibleIndex="12" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="6%" Visible="True" meta:resourcekey="Decision" >
<EditFormSettings VisibleIndex="12" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='DECISION'> 
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='DecisionComplement' FieldName='DECISIONCOMPLEMENT' Caption='Decision Complement' ToolTip='Decision Complement.' VisibleIndex="13" CellStyle-HorizontalAlign="Left" Width="8%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="DecisionComplement">
<EditFormSettings VisibleIndex="13" Visible="True"/>
<PropertiesTextEdit Size='80' MaxLength='80'>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='WaitingPeriodDays' FieldName='WAITINGPERIODDAYS' Caption='Waiting Period Days' ToolTip='Waiting Period Days.' VisibleIndex="14" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="WaitingPeriodDays">
<EditFormSettings VisibleIndex="14" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,##0" Size='3'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='WaitingPeriodMonths' FieldName='WAITINGPERIODMONTHS' Caption='Waiting Period Months' ToolTip='Waiting Period Months.' VisibleIndex="15" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="WaitingPeriodMonths">
<EditFormSettings VisibleIndex="15" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,##0" Size='3'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='WaitingPeriodYears' FieldName='WAITINGPERIODYEARS' Caption='Waiting Period Years' ToolTip='Waiting Period Years.' VisibleIndex="16" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="0%" Visible="True" meta:resourcekey="WaitingPeriodYears">
<EditFormSettings VisibleIndex="16" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,##0" Size='3'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999g>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='MaximumInsuredAmount' FieldName='MAXIMUMINSUREDAMOUNT' Caption='Maximum Insured Amount' ToolTip='Maximum Insured Amount.' VisibleIndex="17" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="2%" Visible="True" meta:resourcekey="MaximumInsuredAmount">
<EditFormSettings VisibleIndex="17" Visible="True"/>
<PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,###,###,###,###,##0.00" Size='21'>
 <MaskSettings IncludeLiterals="DecimalSymbol" Mask=" <0..999999999999999999g>.<00..99>" />
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='RestrictionType' FieldName='RESTRICTIONTYPE' Caption='Restriction Type' ToolTip='Restriction Type.' VisibleIndex="18" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="6%" Visible="True" meta:resourcekey="RestrictionType" >
<EditFormSettings VisibleIndex="18" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='RESTRICTIONTYPE'> 
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='RequirementType' FieldName='REQUIREMENTTYPE' Caption='Requirement Type' ToolTip='Requirement Type.' VisibleIndex="19" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="6%" Visible="True" meta:resourcekey="RequirementType" >
<EditFormSettings VisibleIndex="19" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='REQUIREMENTTYPE'> 
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='ExclusionPeriodType' FieldName='EXCLUSIONPERIODTYPE' Caption='Exclusion Period Type' ToolTip='Exclusion Period Type.' VisibleIndex="20" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="6%" Visible="True" meta:resourcekey="ExclusionPeriodType" >
<EditFormSettings VisibleIndex="20" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='EXCLUSIONPERIODTYPE'> 
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='ExclusionType' FieldName='EXCLUSIONTYPE' Caption='Exclusion Type' ToolTip='Exclusion Type' VisibleIndex="21" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="6%" Visible="True" meta:resourcekey="ExclusionType" >
<EditFormSettings VisibleIndex="21" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='EXCLUSIONTYPE'> 
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='RecordStatus' FieldName='RECORDSTATUS' Caption='Record Status' ToolTip='Status of the record.' VisibleIndex="22" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="3%" Visible="True" meta:resourcekey="RecordStatus" >
<EditFormSettings VisibleIndex="22" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='RECORDSTATUS'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Record Status" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='LanguageId' FieldName='LANGUAGEID' Caption='Language Id' ToolTip='Language in which the system shows the information. ' VisibleIndex="23" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" HeaderStyle-HorizontalAlign="Center" Width="3%" Visible="True" meta:resourcekey="LanguageId" >
<EditFormSettings VisibleIndex="23" Visible="True"/>
<PropertiesComboBox ValueType="System.Int32" IncrementalFilteringMode="StartsWith" EnableCallbackMode="false"  TextField='DESCRIPTION' ValueField='LANGUAGEID'> 
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Language Id" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='Description' FieldName='DESCRIPTION' Caption='Description' ToolTip='Description associated with the code. ' VisibleIndex="24" CellStyle-HorizontalAlign="Left" Width="6%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="Description">
<EditFormSettings VisibleIndex="24" Visible="True"/>
<PropertiesTextEdit Size='60' MaxLength='60'>
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Description" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='ShortDescription' FieldName='SHORTDESCRIPTION' Caption='Short Description' ToolTip='Brief description associated with the code. ' VisibleIndex="25" CellStyle-HorizontalAlign="Left" Width="6%" Visible="True" HeaderStyle-Wrap="True" HeaderStyle-VerticalAlign="Bottom" meta:resourcekey="ShortDescription">
<EditFormSettings VisibleIndex="25" Visible="True"/>
<PropertiesTextEdit Size='60' MaxLength='60'>
     <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
       <RequiredField IsRequired='True' ErrorText='The "Short Description" is required.'/>
     </ValidationSettings>
     <Style Paddings-PaddingLeft="8px" BackgroundImage-HorizontalPosition="left" BackgroundImage-ImageUrl ="/images/generaluse/required.PNG" BackgroundImage-Repeat="NoRepeat" BackgroundImage-VerticalPosition="center">
</Style>
</PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewCommandColumn VisibleIndex="26" ButtonType="Image" Caption=" " Width="24px">
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
                 AddKeyboardNavigationTo(ImpairmentRules_Grid);
AddKeyboardNavigationTo(ImpairmentRuleTrans_Grid);

                 HandlerView('ImpairmentRules_ViewItem')                
            </script> 
            <dxpc:ASPxPopupControl ID="popupHelp" runat="server" AllowDragging="True" ContentStyle-Paddings-Padding="0"
                ClientInstanceName="popupHelp" HeaderText="Ayuda" Height="400px" meta:resourcekey="popupHelpResource"
                Modal="True" ModalBackgroundStyle-BackgroundImage-HorizontalPosition="center"
                PopupHorizontalAlign="LeftSides" PopupVerticalAlign="TopSides" ShowPageScrollbarWhenModal="True"
                Width="400px" ContentUrl="~/generated/crud/help/ImpairmentRules.html" AllowResize="True"
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
