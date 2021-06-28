<%@ Control Language="VB" AutoEventWireup="false" CodeFile="NNCotizacionMiVidaValeSecuenciaUserControl.ascx.vb" Inherits="NNCotizacionMiVidaValeSecuenciaUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">
    var msgbutton31InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button31InformationMessageResource"></asp:Localize>';
    var titlebutton31InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton31InformationMessageResource"></asp:Localize>';
    var msgbutton32InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button32InformationMessageResource"></asp:Localize>';
    var titlebutton32InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton32InformationMessageResource"></asp:Localize>';
    var msgbutton34InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button34InformationMessageResource"></asp:Localize>';
    var titlebutton34InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton34InformationMessageResource"></asp:Localize>';
    var msgbutton35InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button35InformationMessageResource"></asp:Localize>';
    var titlebutton35InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton35InformationMessageResource"></asp:Localize>';
    var msgbutton36InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button36InformationMessageResource"></asp:Localize>';
    var titlebutton36InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton36InformationMessageResource"></asp:Localize>';
    var msgbutton19InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button19InformationMessageResource"></asp:Localize>';
    var titlebutton19InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton19InformationMessageResource"></asp:Localize>';
    var msgEnviarCotizacionEmailInformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="EnviarCotizacionEmailInformationMessageResource"></asp:Localize>';
    var titleEnviarCotizacionEmailInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titleEnviarCotizacionEmailInformationMessageResource"></asp:Localize>';
    var msgAcceptInformationMessageResource='<asp:Localize runat="server" Text="Por favor espere... Si ha solicitado la impresión en línea, esta acción puede tomar algunos minutos..." meta:resourcekey="AcceptInformationMessageResource"></asp:Localize>';
    var titleAcceptInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titleAcceptInformationMessageResource"></asp:Localize>';
    var msgbtnSalirSinGuardarInformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="btnSalirSinGuardarInformationMessageResource"></asp:Localize>';
    var titlebtnSalirSinGuardarInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebtnSalirSinGuardarInformationMessageResource"></asp:Localize>';



</script>

<script src="/generated/form/NNCotizacionMiVidaValeSecuencia.js" type="text/javascript"></script>      
<asp:UpdatePanel ID="NNCotizacionMiVidaValeSecuenciaUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='NNCotizacionMiVidaValeSecuenciaTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone29" ClientInstanceName="zone29" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone29Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='button31' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Información básica" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button31Resource" Text="Información básica" Width='180px'  Font-Bold="True"  Font-Italic="True"  Font-Size="10"  BackColor="#FFFFFF"   OnClick='button31_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button31Click" />
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='button32' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Información adicional" ClientVisible='False' ClientEnabled='True' meta:resourcekey="button32Resource" Text="Información adicional" Width='180px'  Font-Bold="True"  Font-Italic="True"  Font-Size="10"  BackColor="#FFFFFF"   OnClick='button32_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button32Click" />
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='button34' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Cuestionario básico de salud" ClientVisible='False' ClientEnabled='True' meta:resourcekey="button34Resource" Text="Salud" Width='180px'  Font-Bold="True"  Font-Italic="True"  Font-Size="10"  BackColor="#FFFFFF"   OnClick='button34_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button34Click" />
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='button35' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Beneficiarios" ClientVisible='False' ClientEnabled='True' meta:resourcekey="button35Resource" Text="Beneficiarios" Width='180px'  Font-Bold="True"  Font-Italic="True"  Font-Size="10"  BackColor="#FFFFFF"   OnClick='button35_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button35Click" />
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='button36' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Pago" ClientVisible='False' ClientEnabled='True' meta:resourcekey="button36Resource" Text="Pago" Width='180px'  Font-Bold="True"  Font-Italic="True"  Font-Size="10"  BackColor="#FFFFFF"   OnClick='button36_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button36Click" />
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
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zonegeneral" ClientInstanceName="zonegeneral" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zonegeneralResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
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
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='button19' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Ver el resumen de la cotizaciòn" ClientVisible='False' ClientEnabled='True' meta:resourcekey="button19Resource" Text="Ver resumen" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/zoom_in_16x16.gif" Width='180px'  Height='16px'  Font-Bold="True"  Font-Italic="True"  Font-Size="10"  BackColor="#FFFFFF"   OnClick='button19_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button19Click" />
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='EnviarCotizacionEmail' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Enviar la cotización al correo electrónico indicado" ClientVisible='False' ClientEnabled='True' meta:resourcekey="EnviarCotizacionEmailResource" Text="Enviar eMail" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/mail1_16x16.gif" Width='180px'  Font-Bold="True"  Font-Italic="True"  Font-Size="10"  BackColor="#FFFFFF"   OnClick='EnviarCotizacionEmail_Click' AutoPostBack='false'>
<ClientSideEvents  Click="EnviarCotizacionEmailClick" />
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%;' colspan='2' align='center'>

       <dxe:ASPxTextBox runat='server' ID='eMail' ClientInstanceName='eMail' ToolTip="Dirección del correo electrónico donde le llegará la cotización." Size='60' NullText="usuario@proveedor.com" ClientVisible='False' MaxLength='60' ClientIDMode='Static' meta:resourcekey="eMailResource" Width='180px'  ClientEnabled='True' AutoPostBack='true' OnTextChanged='eMail_TextChanged' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zoneemail" >
     <RequiredField IsRequired='True' ErrorText="" />
     <RegularExpression ValidationExpression="^\s*[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]\s*$" ErrorText="Correo electrónico inválido" />
</ValidationSettings>
       </dxe:ASPxTextBox>
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
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='Accept' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Permite comprar la póliza" ClientVisible='False' ClientEnabled='True' meta:resourcekey="AcceptResource" Text="Procesar solicitud" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/shopping_cart_16x16.gif" Width='180px'  Font-Bold="True"  Font-Italic="True"  Font-Size="10"  BackColor="#FFFFFF"   OnClick='Accept_Click' AutoPostBack='false'>
<ClientSideEvents  Click="AcceptClick" />
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Center'>       <dxe:ASPxCheckBox ID='OnLinePrintIndicator' runat='server' Text="Ver cuadro de póliza en línea" ClientIDMode='Static' ClientVisible='false' ClientEnabled='True' meta:resourcekey="OnLinePrintIndicator"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


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
    <td style="width:50%">
      &nbsp;
    </td>
    <td style="width:50%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone0" ClientInstanceName="zone0" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone0Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='btnSalirSinGuardar' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="El cliente no desea registrar la cotización. Finaliza el proceso." ClientVisible='True' ClientEnabled='True' meta:resourcekey="btnSalirSinGuardarResource" Text="Salir sin guardar" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/delete_16x16.gif" Width='180px'  Font-Bold="True"  Font-Italic="True"  Font-Size="10"  BackColor="#FFFFFF"   OnClick='btnSalirSinGuardar_Click' AutoPostBack='false'>
<ClientSideEvents  Click="btnSalirSinGuardarClick" />
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