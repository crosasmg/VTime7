<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SolicitudesAsociadasAGuiaDespachoUserControl.ascx.vb" Inherits="SolicitudesAsociadasAGuiaDespachoUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">
    var msgbutton16InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button16InformationMessageResource"></asp:Localize>';
    var titlebutton16InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton16InformationMessageResource"></asp:Localize>';



</script>

<script src="/generated/form/SolicitudesAsociadasAGuiaDespacho.js" type="text/javascript"></script>      
<asp:UpdatePanel ID="SolicitudesAsociadasAGuiaDespachoUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >


  
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='SolicitudesAsociadasAGuiaDespachoTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone9" ClientInstanceName="zone9" runat="server" HeaderText="Recepción de guías de despacho" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone9Resource"
 Width="100%" SkinID="CaptionAndSquareBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="UnderwritingCaseGuide" ClientInstanceName="UnderwritingCaseGuide" runat="server" HeaderText="Recepción de guías de despacho" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="UnderwritingCaseGuideResource"
 Width='1150px' SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:5%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone5" ClientInstanceName="zone5" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone5Resource"
 Width='58px' SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:70%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone7" ClientInstanceName="zone7" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone7Resource"
 Width='770px' SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:17.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='UnderwritingCaseGuideGuideIdLabel' EncodeHtml='false' ClientInstanceName='UnderwritingCaseGuideGuideIdLabel' runat='server' ClientIDMode='Static' meta:resourcekey="UnderwritingCaseGuideGuideIdLabelResource"  Text="Gu&#237;a"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='UnderwritingCaseGuideGuideId'       ></dxe:ASPxLabel></td>    <td style='width:17.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='UnderwritingCaseGuideGuideId' ClientInstanceName='UnderwritingCaseGuideGuideId' ToolTip="Número de la guía de despacho" Size='9' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="UnderwritingCaseGuideGuideIdResource"  Width='120px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <-999999999..999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="UnderwritingCaseGuide" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:32.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='GuidestatuscodeLabel' EncodeHtml='false' ClientInstanceName='GuidestatuscodeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="GuidestatuscodeLabelResource"  Text="Estado"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='Guidestatuscode'       ></dxe:ASPxLabel></td>    <td style='width:32.5%;' align='left'>


<dxe:ASPxComboBox ID='Guidestatuscode' runat='server' ClientInstanceName='Guidestatuscode' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Estado de la guía." ClientVisible='true' ClientEnabled='False' meta:resourcekey="GuidestatuscodeResource"  Width='200px'  ValueType='System.Int32'    TextField='DESCRIPTION' ValueField='GUIDESTATUS'>         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone7" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
</dxe:ASPxComboBox>
    </td>

  </tr>
  <tr valign='top'>
    <td style="width:32.5%">
      &nbsp;
    </td>
    <td style="width:32.5%">
      &nbsp;
    </td>
    <td style="width:32.5%">
      &nbsp;
    </td>
    <td style="width:32.5%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:17.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='text0Label' EncodeHtml='false' ClientInstanceName='text0Label' runat='server' ClientIDMode='Static' meta:resourcekey="text0LabelResource"  Text="Intermediario"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='text0'       ></dxe:ASPxLabel></td>    <td style='width:17.5%;' align='left'>


<dxe:ASPxComboBox ID='text0' runat='server' ClientInstanceName='text0' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Nombre del intermediario asociado a la guía" ClientVisible='true' ClientEnabled='False' meta:resourcekey="text0Resource"  Width='230px'  ValueType='System.Int32'    TextField='SCLIENAME' ValueField='NUSERCODE'><ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone7" >
</ValidationSettings>
</dxe:ASPxComboBox>
    </td>

  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:25%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone6" ClientInstanceName="zone6" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone6Resource"
 Width='230px' SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
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
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="UnderwritingCaseGuideCollection" ClientInstanceName="UnderwritingCaseGuideCollection" runat="server" HeaderText="Underwriting Case Guide Collection" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="UnderwritingCaseGuideCollectionResource"
 Width='1150px' SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='UnderwritingCaseForGuide' EnableRowsCache='False' EnableViewState='False' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='UnderwritingCaseForGuide' runat='server' Width='100%' KeyFieldName='underwritingCaseId' Caption="" meta:resourcekey="UnderwritingCaseForGuideResource"
>
    <Settings ShowFooter="True"/>
    <TotalSummary>
        <dxwgv:ASPxSummaryItem FieldName="fullProposalID" SummaryType="Count" DisplayFormat="Cantidad de solicitudes: {0}" meta:resourcekey="UnderwritingCaseForGuideSummaryItem0" /> 
        <dxwgv:ASPxSummaryItem FieldName="faceAmount" SummaryType="Sum" DisplayFormat="Total capital: {0:n2}" meta:resourcekey="UnderwritingCaseForGuideSummaryItem1" /> 
        <dxwgv:ASPxSummaryItem FieldName="premium" SummaryType="Sum" DisplayFormat="Total prima: {0:n2}" meta:resourcekey="UnderwritingCaseForGuideSummaryItem2" /> 

    </TotalSummary>
    <GroupSummary>
        <dxwgv:ASPxSummaryItem FieldName="fullProposalID" SummaryType="Count" DisplayFormat="Cantidad de solicitudes: {0}" meta:resourcekey="UnderwritingCaseForGuideSummaryItem0" /> 
        <dxwgv:ASPxSummaryItem FieldName="faceAmount" SummaryType="Sum" DisplayFormat="Total capital: {0:n2}" meta:resourcekey="UnderwritingCaseForGuideSummaryItem1" /> 
        <dxwgv:ASPxSummaryItem FieldName="premium" SummaryType="Sum" DisplayFormat="Total prima: {0:n2}" meta:resourcekey="UnderwritingCaseForGuideSummaryItem2" /> 

    </GroupSummary>
               <SettingsPager Visible="True" PageSize="10"/>
               <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AllowSort="True"/>
        <Columns>
<dxwgv:GridViewDataTextColumn FieldName='underwritingCaseId' Visible='false'>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='fullProposalID' FieldName='fullProposalID' ToolTip="Número de solicitud asociada a la guía en tratamiento" Caption="Solicitud" VisibleIndex="0" meta:resourcekey="fullProposalIDFieldResource"
 Visible='True'
 FooterCellStyle-HorizontalAlign="Right" 
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="##,###,###,##0">
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="UnderwritingCaseForGuide" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='productCode' FieldName='productCode' ToolTip="Código y descripción del producto al cual está asociada la solicitud" Caption="Producto" VisibleIndex="1" meta:resourcekey="productCodeFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
<PropertiesComboBox EnableCallbackMode='false' IncrementalFilteringMode='StartsWith'
 TextField='SDESCRIPT' ValueField='NPRODUCT'>
           <Columns>
                <dxe:ListBoxColumn FieldName="NPRODUCT" Caption="CódigoDelProducto" Visible="True"  meta:resourcekey="productCodeColumnNPRODUCTResource"/>
                <dxe:ListBoxColumn FieldName="SDESCRIPT" Caption="Descripción" Visible="True"  meta:resourcekey="productCodeColumnSDESCRIPTResource"/>
            </Columns>

<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="UnderwritingCaseForGuide" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='clientID' FieldName='clientID' ToolTip="Nombre completo de la persona que realiza la solicitud" Caption="Nombre solicitante" VisibleIndex="2" meta:resourcekey="clientIDFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
<PropertiesComboBox EnableCallbackMode='false' IncrementalFilteringMode='StartsWith'
 TextField='SCLIENAME' ValueField='SCLIENT'>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="UnderwritingCaseForGuide" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='stageDescription' FieldName='stageDescription' ToolTip="Estado" Caption="Estado" VisibleIndex="3" meta:resourcekey="stageDescriptionFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="3" Visible="True" />
           <PropertiesTextEdit NullText="">
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="UnderwritingCaseForGuide" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='faceAmount' FieldName='faceAmount' ToolTip="Monto del capital (en U.F.) asociado a la solicitud." Caption="Capital U.F." VisibleIndex="4" meta:resourcekey="faceAmountFieldResource"
 Visible='True'
 FooterCellStyle-HorizontalAlign="Right" 
><EditFormSettings VisibleIndex="4" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,###,###,###,###,##0.00">
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="UnderwritingCaseForGuide" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='premium' FieldName='premium' ToolTip="Monto (en pesos) de la prima acordada en la solicitud" Caption="Prima pactada" VisibleIndex="5" meta:resourcekey="premiumFieldResource"
 Visible='True'
 FooterCellStyle-HorizontalAlign="Right" 
><EditFormSettings VisibleIndex="5" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,###,###,###,###,##0.00">
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="UnderwritingCaseForGuide" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='lineOfBusiness' FieldName='lineOfBusiness' ToolTip="Fecha de Efecto del Registro" Caption="Ramo" VisibleIndex="6" meta:resourcekey="lineOfBusinessFieldResource"
 Visible='False'
><EditFormSettings VisibleIndex="6" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0">
<ClientSideEvents TextChanged="function(s, e) {UnderwritingCaseForGuide.GetEditor('productCode').PerformCallback(s.GetValue().toString()); }"/> 

<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="UnderwritingCaseForGuide" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewCommandColumn>
<CancelButton Visible="True" Text="Cancel" />
                                    <UpdateButton Visible="True" Text="Actualizar" />
                                </dxwgv:GridViewCommandColumn>
            </Columns>
        </dxwgv:ASPxGridView>
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
                    <dxrp:ASPxRoundPanel ID="zone15" ClientInstanceName="zone15" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone15Resource"
 Width='1150px' SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style="width:0%">
      &nbsp;
    </td>
    <td style="width:0%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style="width:0%">
      &nbsp;
    </td>
    <td style="width:0%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style="width:0%">
      &nbsp;
    </td>
    <td style="width:0%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style="width:0%">
      &nbsp;
    </td>
    <td style="width:0%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='button16' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Recepcionar guía" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button16Resource" Text="Recepcionar guía" Width='150px'   AutoPostBack='false'>
<ClientSideEvents  Click="button16Click" />
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

    <table style="width: 100%;">
        <tr valign='top'>
            <td>
                <br />
                <asp:UpdatePanel ID="UpdatePanelErrors" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="ErrorsGridView" runat="server" AutoGenerateColumns="False" Visible="False"
                            SkinID="Main" Width="74px">
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
                        ClientInstanceName="btnConfirm" Text="Confirm" >
                        <ClientSideEvents Click="function(s,e){ 
                            var window = popControl.GetWindowByName('pwUno');                                                    
                           
                           if (msgbutton16ConfirmationMessageResource!=''){
                            document.getElementById(btnCancel.name).style.visibility = 'hidden';
                            document.getElementById(btnConfirm.name).style.visibility = 'hidden';
                            document.getElementById(lblMessage.name).innerHTML = msgbutton16ConfirmationMessageResource;                     
                            window.SetHeaderText('Message');                           
                            
                            window.popupControl.ShowWindow(window);
                           }
                           
                    }" />
                    </dxe:ASPxButton>
            </td>            
            <td width="50%">
                    <dxe:ASPxButton ID="btnCancel" runat="server" AutoPostBack="False" 
                        ClientInstanceName="btnCancel" Text="Cancel">
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