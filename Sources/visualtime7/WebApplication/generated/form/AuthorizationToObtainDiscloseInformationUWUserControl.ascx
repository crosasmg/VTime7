<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AuthorizationToObtainDiscloseInformationUWUserControl.ascx.vb" Inherits="AuthorizationToObtainDiscloseInformationUWUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">



</script>

<script src="/generated/form/AuthorizationToObtainDiscloseInformationUW.js" type="text/javascript"></script>      
<asp:UpdatePanel ID="AuthorizationToObtainDiscloseInformationUWUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='AuthorizationToObtainDiscloseInformationUWTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="part1" ClientInstanceName="part1" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="part1Resource"
 Width="100%" SkinID="RoundedBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:50%;' colspan='2' align='left'>

<dxe:ASPxImage ID="image0" runat="server" ToolTip="image 0" ClientEnabled="True" ClientVisible="True" ClientIDMode='Static' ImageUrl="/images/Banners/Life Insurance/3.jpg" meta:resourcekey="image0Resource"  Width="40px" > 
</dxe:ASPxImage>
    </td>

    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='ClientNameLabel' EncodeHtml='false' ClientInstanceName='ClientNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ClientNameLabelResource"  Text="Solicitante del seguro"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='ClientName'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='ClientName' ClientInstanceName='ClientName' ToolTip="First name of the proposed insured" Size='30' NullText="" ClientVisible='True' MaxLength='30' ClientIDMode='Static' meta:resourcekey="ClientNameResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='uwcaseidLabel' EncodeHtml='false' ClientInstanceName='uwcaseidLabel' runat='server' ClientIDMode='Static' meta:resourcekey="uwcaseidLabelResource"  Text="Solicitud"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='uwcaseid'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='uwcaseid' ClientInstanceName='uwcaseid' ToolTip="Middle name of the proposed insured" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="uwcaseidResource" Width='270px'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1" >
</ValidationSettings>
       </dxe:ASPxTextBox>
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
                    <dxrp:ASPxRoundPanel ID="PART2" ClientInstanceName="PART2" runat="server" HeaderText="AUTORIZACIÓN" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="PART2Resource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label1' EncodeHtml='false' ClientInstanceName='label1' runat='server' ClientIDMode='Static' meta:resourcekey="label1Resource"  Text="Entiendo que los siguientes partes pueden necesitar recopilar información sobre mí en cuanto a la cobertura propuesta: la empresa citada anteriormente, cualquier organización de apoyo de seguros, cualquier agencia de informes del consumidor, y todas las personas autorizadas para representar a estas organizaciones para este propósito. Aquellos sujetos que puedan necesitar recoger información puede revelar información a lo siguiente: otras compañías de seguros a la que el asegurado solicitante ha aplicado o pueden ser aplicables; reaseguradores, el MIB Group, Inc. o de las personas que realizan actividades empresariales, profesionales o las tareas de seguro para ellos. Podrán revelar la información según lo permitido o requerido por la ley. El MIB puede revelar información sólo según lo establecido en un acuerdo con una empresa u organización miembro. Yo autorizo ​​a la preparación de un informe del consumidor y un informe de investigación del consumidor acerca de mí y mis hijos si su nombre figura como Asegurados propuestos en esta política. Previa solicitud, puedo ser entrevistado como parte de esta solicitud. Además, previa solicitud por escrito, entiendo que tengo derecho a recibir una copia del informe investigativo del consumidor."  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style="width:50%">
      &nbsp;
    </td>
    <td style="width:50%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style="width:50%">
      &nbsp;
    </td>
    <td style="width:50%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label2' EncodeHtml='false' ClientInstanceName='label2' runat='server' ClientIDMode='Static' meta:resourcekey="label2Resource"  Text="La información que puede ser recopilada y revelada incluye: datos sobre mi salud mental o física y consumo de drogas o alcohol, otros seguros, actividades peligrosas, carácter, reputación general, el modo de vida, finanzas, registro de conducir, vocación y otras características personales. No incluye datos acerca de mi orientación sexual."  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label3' EncodeHtml='false' ClientInstanceName='label3' runat='server' ClientIDMode='Static' meta:resourcekey="label3Resource"  Text="Al firmar a continuación, autorizo ​​a petición: cualquier médico o profesional médico, cualquier hospital, clínica u otra instalación relacionada con la medicina, y cualquier compañía de seguros, y cualquier agencia del Gobierno y cualquier otra organización, institución, empresa o persona que tenga registros o conocimiento en relación con la salud de un asegurado solicitante, los hábitos, el empleo, los ingresos y las finanzas si la Compañía nombrado arriba hacer una solicitud, para dar cualquiera de esos registros o conocimiento de: la empresa citada más arriba; sus reaseguradores, afiliados y productores, y los terceros que realizan servicios por la empresa citada anteriormente con el fin de suscribir, procesar reclamaciones y administrar cualquier póliza emitida y ofrecer productos y servicios financieros."  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style="width:50%">
      &nbsp;
    </td>
    <td style="width:50%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style="width:50%">
      &nbsp;
    </td>
    <td style="width:50%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label4' EncodeHtml='false' ClientInstanceName='label4' runat='server' ClientIDMode='Static' meta:resourcekey="label4Resource"  Text="Una copia de esta autorización será tan válida como el original."  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style="width:50%">
      &nbsp;
    </td>
    <td style="width:50%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label5' EncodeHtml='false' ClientInstanceName='label5' runat='server' ClientIDMode='Static' meta:resourcekey="label5Resource"  Text="Entiendo que yo o mi representante autorizado puede solicitar recibir una copia de esta autorización. Reconozco que he recibido una copia del Aviso al Asegurado Solicitante."  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style="width:50%">
      &nbsp;
    </td>
    <td style="width:50%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label6' EncodeHtml='false' ClientInstanceName='label6' runat='server' ClientIDMode='Static' meta:resourcekey="label6Resource"  Text="Las declaraciones de esta autorización son tomadas por el Asegurado Solicitante (s) o de la persona autorizada para actuar en nombre del asegurado solicitante (s)."  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone0" ClientInstanceName="zone0" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone0Resource"
 Width="100%" SkinID="SquareBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='8'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label0' EncodeHtml='false' ClientInstanceName='label0' runat='server' ClientIDMode='Static' meta:resourcekey="label0Resource"  Text="..."  ClientEnabled='true'  ClientVisible='false'        ></dxe:ASPxLabel></td>



<td style='width:25%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label7' EncodeHtml='false' ClientInstanceName='label7' runat='server' ClientIDMode='Static' meta:resourcekey="label7Resource"  Text="..."  ClientEnabled='true'  ClientVisible='false'        ></dxe:ASPxLabel></td>



<td style='width:25%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label8' EncodeHtml='false' ClientInstanceName='label8' runat='server' ClientIDMode='Static' meta:resourcekey="label8Resource"  Text="..."  ClientEnabled='true'  ClientVisible='false'        ></dxe:ASPxLabel></td>



<td style='width:12.5%; padding-top:3px;' colspan='1' align='Right'>       <dxe:ASPxLabel ID='AcceptanceIndicatorLabel' EncodeHtml='false' ClientInstanceName='AcceptanceIndicatorLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AcceptanceIndicatorLabelResource"  Text="Acepto"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='AcceptanceIndicator'       ></dxe:ASPxLabel></td>    <td style='width:12.5%;' align='right'>

       <dxe:ASPxRadioButtonList ID='AcceptanceIndicator' ClientInstanceName='AcceptanceIndicator' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="Indicador de aceptación." ClientVisible='true' ClientEnabled='True'  meta:resourcekey="AcceptanceIndicatorResource"  ValueType='System.Boolean'  AutoPostBack='false' >
            <Items>
                <dxe:ListEditItem Value='true' Text='Si' meta:resourcekey="AcceptanceIndicatorListItemValue1Resource"/>
                <dxe:ListEditItem Value='false' Text='No' meta:resourcekey="AcceptanceIndicatorListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
<ClientSideEvents  ValueChanged="AcceptanceIndicatorValueChanged" />
       </dxe:ASPxRadioButtonList>
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
                    <dxrp:ASPxRoundPanel ID="part4" ClientInstanceName="part4" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="part4Resource"
 Width="100%" SkinID="RoundedBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:33%;' align='left'>       <dxe:ASPxLabel ID='DateReceivedLabel' EncodeHtml='false' ClientInstanceName='DateReceivedLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DateReceivedLabelResource"  Text="Fecha"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DateReceived'       ></dxe:ASPxLabel><br />

       <dxe:ASPxDateEdit runat='server' ID='DateReceived' ToolTip="" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="DateReceivedResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part4" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxDateEdit>
    </td>

    <td style='width:33%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='submit' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="" ClientVisible='True' ClientEnabled='True' meta:resourcekey="submitResource" Text="Guardar temporalmente" ImagePosition='Left' Image-Url="/images/Library/16x16_ASPNetIcons/file_manager_16x16.gif"  AutoPostBack='false'>
<ClientSideEvents  Click="submitClick" />
       </dxe:ASPxButton>
    </td>

    <td style='width:34%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='save' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="" ClientVisible='True' ClientEnabled='False' meta:resourcekey="saveResource" Text="Enviar" ImagePosition='Left' Image-Url="/images/Library/16x16_ASPNetIcons/mail2_(add)_16x16.gif"  OnClick='save_Click' AutoPostBack='false'>
<ClientSideEvents  Click="saveClick" />
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