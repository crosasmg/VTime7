<%@ Control Language="VB" AutoEventWireup="false" CodeFile="OnLineServiceUnderwriterIIUserControl.ascx.vb" Inherits="OnLineServiceUnderwriterIIUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">
    var msgbutton4InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button4InformationMessageResource"></asp:Localize>';
    var titlebutton4InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton4InformationMessageResource"></asp:Localize>';
    var msgbutton7InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button7InformationMessageResource"></asp:Localize>';
    var titlebutton7InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton7InformationMessageResource"></asp:Localize>';



</script>

<script src="/generated/form/OnLineServiceUnderwriterII.js" type="text/javascript"></script>      
<asp:UpdatePanel ID="OnLineServiceUnderwriterIIUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='OnLineServiceUnderwriterIITablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone5" ClientInstanceName="zone5" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone5Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:30%;' colspan='2' align='left'>

       <dxe:ASPxRadioButtonList ID='ActionType' ClientInstanceName='ActionType' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Vertical' ClientIDMode='Static' ToolTip="" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="ActionTypeResource"  ValueType='System.Int32'  AutoPostBack='false' OnSelectedIndexChanged='ActionType_SelectedIndexChanged' >
            <Items>
                <dxe:ListEditItem Value='1' Text='Cotizaciones' meta:resourcekey="ActionTypeListItemValue1Resource"/>
                <dxe:ListEditItem Value='2' Text='Panel de suscripción' meta:resourcekey="ActionTypeListItemValue2Resource"/>
                <dxe:ListEditItem Value='3' Text='Consulta de casos pendientes' meta:resourcekey="ActionTypeListItemValue3Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone5" >
</ValidationSettings>
<ClientSideEvents SelectedIndexChanged="AsyncPostBack"/>
       </dxe:ASPxRadioButtonList>
    </td>

    <td style='width:70%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone0" ClientInstanceName="zone0" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="False" meta:resourcekey="zone0Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:150%;' colspan='3' align='left'>       <dxe:ASPxLabel ID='StartDateLabel' EncodeHtml='false' ClientInstanceName='StartDateLabel' runat='server' ClientIDMode='Static' meta:resourcekey="StartDateLabelResource"  Text="Período"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='StartDate'       ></dxe:ASPxLabel><br /><div style='float: left;'>

        <table>
            <tr>
                <td>
       <dxe:ASPxDateEdit runat='server' ID='StartDate' ToolTip="Fecha inicial del período a consultar" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="StartDateResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxDateEdit>
                </td>
                <td align='left'>
       <dxe:ASPxLabel ID='StartDateMeasureLabel' ClientInstanceName='StartDateMeasureLabel' runat='server' ClientEnabled='False' Text="/" meta:resourcekey="StartDateMeasureLabelResource"  ClientVisible='true'        ></dxe:ASPxLabel>
               </td>
            </tr>
        </table>
</div>

<div style='float: left;'>

       <dxe:ASPxDateEdit runat='server' ID='EndDate' ToolTip="Fecha final del período a consultar" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="EndDateResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxDateEdit>
</div>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:50%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='button4' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Busca los casos registrados en el período indicado" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button4Resource" Text="Buscar casos" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/zoom_in_16x16.gif" Height='16px'   OnClick='button4_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button4Click" />
       </dxe:ASPxButton>
    </td>

  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="UnderwritingCaseCollection" ClientInstanceName="UnderwritingCaseCollection" runat="server" HeaderText="Casos registrados en el período indicado" ToolTip="Casos" Enabled="True" ClientIDMode='Static' ClientVisible="False" meta:resourcekey="UnderwritingCaseCollectionResource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='UnderwritingCase' EnableRowsCache='False' EnableViewState='True' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='UnderwritingCase' runat='server' Width='100%' KeyFieldName='UnderwritingCaseID' Caption="" meta:resourcekey="UnderwritingCaseResource"

>
               <SettingsPager Visible="True" PageSize="10"/>
 <SettingsBehavior AllowFocusedRow="True" AllowSort="False"/>
<Settings ShowGroupPanel='True' ShowFilterRow='True' />
        <Columns>
<dxwgv:GridViewDataTextColumn Name='UnderwritingCaseID' FieldName='UnderwritingCaseID' Caption="Caso" ToolTip="" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="UnderwritingCaseIDFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='50' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="UnderwritingCase" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='ReasonDescription' FieldName='ReasonDescription' Caption="Contratante" ToolTip="Motivo" GroupIndex="-1" VisibleIndex="1" meta:resourcekey="ReasonDescriptionFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='15' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataDateColumn Name='OpenDate' FieldName='OpenDate' Caption="Registro" ToolTip="Open Date" GroupIndex="-1" VisibleIndex="2" meta:resourcekey="OpenDateFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
           <PropertiesDateEdit>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="UnderwritingCase" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
           </PropertiesDateEdit>
</dxwgv:GridViewDataDateColumn>
<dxwgv:GridViewDataComboBoxColumn Name='LineOfBusiness' FieldName='LineOfBusiness' Caption="Ramo" ToolTip="Line of Business"  GroupIndex="-1" VisibleIndex="3" meta:resourcekey="LineOfBusinessFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="3" Visible="True" />
<PropertiesComboBox EnableCallbackMode='false' IncrementalFilteringMode='StartsWith'
 TextField='SDESCRIPT' ValueField='NBRANCH'>
<ClientSideEvents ValueChanged="function(s, e) {UnderwritingCase.GetEditor('Product').PerformCallback(s.GetValue().toString()); }"/> 

</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='Product' FieldName='Product' Caption="Producto" ToolTip="Código del producto"  GroupIndex="-1" VisibleIndex="4" meta:resourcekey="ProductFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="4" Visible="True" />
<PropertiesComboBox EnableCallbackMode='false' IncrementalFilteringMode='StartsWith'
 TextField='SDESCRIPT' ValueField='NPRODUCT'>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='Decision' FieldName='Decision' Caption="Decisión" ToolTip="Decisión"  GroupIndex="-1" VisibleIndex="5" meta:resourcekey="DecisionFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="5" Visible="True" />
<PropertiesComboBox EnableCallbackMode='false' IncrementalFilteringMode='StartsWith'
 TextField='DESCRIPTION' ValueField='DECISION'>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="UnderwritingCase" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='Status' FieldName='Status' Caption="Estado" ToolTip="Estado del Caso"  GroupIndex="-1" VisibleIndex="6" meta:resourcekey="StatusFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="6" Visible="True" />
<PropertiesComboBox EnableCallbackMode='false' IncrementalFilteringMode='StartsWith'
 TextField='DESCRIPTION' ValueField='UNDERWRITINGCASESTATUS'>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="UnderwritingCase" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataDateColumn Name='OpenDateG' FieldName='OpenDate' Caption="F.reación" ToolTip="Fecha de creación" GroupIndex="-1" VisibleIndex="7" meta:resourcekey="OpenDateGFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="7" Visible="True" />
           <PropertiesDateEdit>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="UnderwritingCase" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
           </PropertiesDateEdit>
</dxwgv:GridViewDataDateColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone6" ClientInstanceName="zone6" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone6Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:50%;' align='left'>       <dxe:ASPxLabel ID='CaseToQueryLabel' EncodeHtml='false' ClientInstanceName='CaseToQueryLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CaseToQueryLabelResource"  Text="Caso a consultar/modificar"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='CaseToQuery'       ></dxe:ASPxLabel><br />


<dxe:ASPxComboBox ID='CaseToQuery' runat='server' ClientInstanceName='CaseToQuery' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Si así lo desea, coloque en este campo el número del caso a consultar en detalle" ClientVisible='true' ClientEnabled='True' meta:resourcekey="CaseToQueryResource"  Width='90px'  ValueType='System.Int64'    TextField='UNDERWRITINGCASEID' ValueField='UNDERWRITINGCASEID'><ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone6" >
</ValidationSettings>
</dxe:ASPxComboBox>
    </td>

    <td style='width:50%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='button7' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Ir al panel de suscripción a fin de visualizar en detalle el caso seleccionado" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button7Resource" Text="Panel de suscripción" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/right_16x16.gif"  OnClick='button7_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button7Click" />
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Center'>       <dxe:ASPxLabel ID='label4' EncodeHtml='false' ClientInstanceName='label4' runat='server' ClientIDMode='Static' meta:resourcekey="label4Resource"  Text="No existen casos registrados en el período indicado"  ClientEnabled='true'  ClientVisible='false'  Font-Bold="True"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
  </tr>
</table>

    <dxlp:ASPxLoadingPanel ID="LoadingPanelGridView" runat="server" ClientInstanceName="LoadingPanelGridView"  Modal="True" Text="<%$ Resources:Resource, Working %>" />
    <table style="width: 100%;">
        <tr valign='top'>
            <td>
                <br />
                <asp:UpdatePanel ID="UpdatePanelErrors" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="ErrorsGridView" runat="server" AutoGenerateColumns="False" Visible="False"
                            SkinID="Main" Width="74px" PageSize="50">
                            <Columns>
                                <asp:TemplateField HeaderText="" SortExpression="Severity">
                                    <ItemTemplate>
                                        <asp:Image ID="imgButton" runat="server" ImageUrl='<%# eval("SeverityImage") %>'></asp:Image>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# eval("Category") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ErrorButton" runat="server" Text='<%# eval("Message") %>' CommandArgument="<%# DirectCast(Container,GridViewRow).RowIndex %>" CommandName='<%# eval("ControlIdFullPath") %>'> </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:Label ID="ErrorLabel" runat="server" Font-Bold="True" ForeColor="Red" Text="Errors"></asp:Label>
                                    </HeaderTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>       
<dxpc:ASPxPopupControl ShowPageScrollbarWhenModal="true" ID="popControl" runat="server"  ClientInstanceName="popControl"
            ShowCloseButton="False" CloseAction="None" Modal="True" 
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"             
            EnableHotTrack="False" >
        <SizeGripImage Height="16px" Width="16px" />
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
            </dxpc:PopupControlContentControl>
        </ContentCollection>
        <CloseButtonImage Height="12px" Width="13px" />
        <HeaderStyle>
        <Paddings PaddingRight="6px" />
        </HeaderStyle>
        <Windows>
        <dxpc:PopupWindow Name="pwUno" Modal="true">
        <ContentCollection>
        <dxpc:PopupControlContentControl>     
        <table width="100%" >
        <tr>
        <td width="100%" colspan="2" align="center">
        <dxe:ASPxLabel runat="server" ClientInstanceName="lblMessage" ID="lblMessage"> </dxe:ASPxLabel>
            <br />
            &nbsp;
        </td>  
        </tr>
        <tr>
            <td width="50%" align="right">
                    <dxe:ASPxButton ID="btnConfirm" runat="server" AutoPostBack="False" 
                        ClientInstanceName="btnConfirm" Text="<%$ Resources:Resource, Confirm %>" >
                        <ClientSideEvents Click="function(s,e){Confirmation_Actions();}" />
                    </dxe:ASPxButton>
            </td>            
            <td width="50%">
                    <dxe:ASPxButton ID="btnCancel" runat="server" AutoPostBack="False" 
                        ClientInstanceName="btnCancel" Text="<%$ Resources:Resource, Cancel %>">
                        <ClientSideEvents Click="function(s,e){ASPxClientPopupControl.GetPopupControlCollection().HideAllWindows();}" />
                    </dxe:ASPxButton>
            </td>
            </tr>
        </table>
        </dxpc:PopupControlContentControl>        
        </ContentCollection>        
        </dxpc:PopupWindow>        
        </Windows>        
        </dxpc:ASPxPopupControl>
  <asp:Label ID="_FormID" runat="server" Visible="False"></asp:Label>
  <table id='MessageTable' runat='server' style='width: 100%;' visible='false'>
      <tr valign='top' align='center'>
          <td>
					  <div class='FormMessage'>
		            <dxe:ASPxLabel EncodeHtml='false' ClientInstanceName='FormMessageLabel' ID='FormMessageLabel' ClientIDMode='Static' runat='server' Text=''  >
		            </dxe:ASPxLabel>
					  </div>
          </td>
      </tr>
  </table>  
  <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ModalBackgroundStyle-BackgroundImage-HorizontalPosition="center" SkinID="GroupBoxRoundedBorder"
                   ID="popupDelete" runat="server" ClientInstanceName="popupDelete" EnableHotTrack="False" >
                 <SizeGripImage Height="16px" Width="16px" />
                 <ClientSideEvents Init="function(s,e){
                                    popupDelete_Init(popupDelete)                        
                                    } " />
                                <ContentCollection>
                                    <dxpc:popupcontrolcontentcontrol ID="Popupcontrolcontentcontrol1" runat="server">
                                    <uc1:ConfirmDelete ID="ConfirmDelete1" runat="server" />
                                    </dxpc:popupcontrolcontentcontrol>  
                                </ContentCollection>
                 <CloseButtonImage Height="12px" Width="13px" />
                 <HeaderStyle>
                 <Paddings PaddingRight="6px" />
                 </HeaderStyle>
                </dxpc:ASPxPopupControl>
    
    <dxpc:ASPxPopupControl AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ModalBackgroundStyle-BackgroundImage-HorizontalPosition="center" ID="popupNotifyMessage" HeaderText=" "
        runat="server" ClientInstanceName="popupNotifyMessage" EnableHotTrack="False" Height="26px">
        <ModalBackgroundStyle>
            <BackgroundImage HorizontalPosition="center" />
        </ModalBackgroundStyle>
        <SizeGripImage Height="16px" Width="16px" />
        <ContentCollection>
            <dxpc:PopupControlContentControl>
                <table style='width: 100%;'>
                    <tr>
                        <td rowspan="2">
                            <dxe:ASPxImage ID="MessageImage" runat="server" ImageUrl="~/images/generaluse/exclamation.png">
                            </dxe:ASPxImage>
                        </td>
                        <td>
                            <asp:Literal ID='NotifyMessageLabel' Text="" runat='server' >
                            </asp:Literal>
                        </td>
                    </tr>
                </table>
                <br />
                <table style='width: 100%;'>
                    <tr>
                        <td rowspan="2" align='Center'>
                            <dxe:ASPxButton ID="btnOkNotificy" runat="server" AutoPostBack="False" Text="<%$ Resources:Resource, AcceptBtnResource %>"
                                Height="22px" Width="60px">
                                 <ClientSideEvents Click="function(s, e) {popupNotifyMessage.Hide(); }" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
        <CloseButtonImage Height="12px" Width="13px" />
        <HeaderStyle>
            <Paddings PaddingRight="6px" />
        </HeaderStyle>
    </dxpc:ASPxPopupControl>    
      
      
      
</ContentTemplate>
</asp:UpdatePanel>