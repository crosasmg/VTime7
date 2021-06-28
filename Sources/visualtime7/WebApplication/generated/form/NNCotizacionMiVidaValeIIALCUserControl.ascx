<%@ Control Language="VB" AutoEventWireup="false" CodeFile="NNCotizacionMiVidaValeIIALCUserControl.ascx.vb" Inherits="NNCotizacionMiVidaValeIIALCUserControl" %>
<%@ Register Src="~/Controls/PhysicalAddressControl.ascx" TagName="PhysicalAddress" TagPrefix="ucPhysicalAddress" %>
 
<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">
    var msgControlsDependencyResource='<asp:Localize runat="server" Text="Espere..." meta:resourcekey="ControlsDependencyResource"></asp:Localize>';
    var msgCotizarInformationMessageResource='<asp:Localize runat="server" Text="Realizando el cálculo de la prima. Por favor espere..." meta:resourcekey="CotizarInformationMessageResource"></asp:Localize>';
    var titleCotizarInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titleCotizarInformationMessageResource"></asp:Localize>';
    var msgAceptoInformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="AceptoInformationMessageResource"></asp:Localize>';
    var titleAceptoInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titleAceptoInformationMessageResource"></asp:Localize>';
    var msgbtnAutenticarInformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="btnAutenticarInformationMessageResource"></asp:Localize>';
    var titlebtnAutenticarInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebtnAutenticarInformationMessageResource"></asp:Localize>';
    var msgbutton8InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button8InformationMessageResource"></asp:Localize>';
    var titlebutton8InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton8InformationMessageResource"></asp:Localize>';
    var msgbutton14InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button14InformationMessageResource"></asp:Localize>';
    var titlebutton14InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton14InformationMessageResource"></asp:Localize>';
    var msgbutton1InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button1InformationMessageResource"></asp:Localize>';
    var titlebutton1InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton1InformationMessageResource"></asp:Localize>';
    var msgbutton33InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button33InformationMessageResource"></asp:Localize>';
    var titlebutton33InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton33InformationMessageResource"></asp:Localize>';
    var msgbutton12InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button12InformationMessageResource"></asp:Localize>';
    var titlebutton12InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton12InformationMessageResource"></asp:Localize>';
    var msgbuttonGPagoInformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="buttonGPagoInformationMessageResource"></asp:Localize>';
    var titlebuttonGPagoInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebuttonGPagoInformationMessageResource"></asp:Localize>';
    var msgbutton0InformationMessageResource='<asp:Localize runat="server" Text="Se está guardando la información del caso. Por favor espere..." meta:resourcekey="button0InformationMessageResource"></asp:Localize>';
    var titlebutton0InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton0InformationMessageResource"></asp:Localize>';
    var msgbutton19InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button19InformationMessageResource"></asp:Localize>';
    var titlebutton19InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton19InformationMessageResource"></asp:Localize>';
    var msgEnviarCotizacionEmailInformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="EnviarCotizacionEmailInformationMessageResource"></asp:Localize>';
    var titleEnviarCotizacionEmailInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titleEnviarCotizacionEmailInformationMessageResource"></asp:Localize>';
    var msgAcceptInformationMessageResource='<asp:Localize runat="server" Text="Por favor espere... Si ha solicitado la impresión en línea, esta acción puede tomar algunos minutos..." meta:resourcekey="AcceptInformationMessageResource"></asp:Localize>';
    var titleAcceptInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titleAcceptInformationMessageResource"></asp:Localize>';
    var msgControlsDependencyResource='<asp:Localize runat="server" Text="Espere..." meta:resourcekey="ControlsDependencyResource"></asp:Localize>';



</script>

<script src="/generated/form/NNCotizacionMiVidaValeIIALC.js" type="text/javascript"></script>      
<asp:UpdatePanel ID="NNCotizacionMiVidaValeIIALCUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='NNCotizacionMiVidaValeIIALCTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zoneTOTAL" ClientInstanceName="zoneTOTAL" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zoneTOTALResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone0" ClientInstanceName="zone0" runat="server" HeaderText="Información general de la cotización" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone0Resource"
 Width="100%" SkinID="SquareBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='8'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:10%;' colspan='2' align='left'>

       <dxe:ASPxLabel ID='ProductMasterDescription' EncodeHtml='false' ClientInstanceName='ProductMasterDescription' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

    </td>

<td style='width:12.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='RiskInformationEffectiveDateLabel' EncodeHtml='false' ClientInstanceName='RiskInformationEffectiveDateLabel' runat='server' ClientIDMode='Static' meta:resourcekey="RiskInformationEffectiveDateLabelResource"  Text="Fecha de efecto"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='RiskInformationEffectiveDate'       ></dxe:ASPxLabel></td>    <td style='width:12.5%;' align='left'>

        <table>
            <tr>
                <td>
       <dxe:ASPxDateEdit runat='server' ID='RiskInformationEffectiveDate' ToolTip="Fecha de efecto (inicio de vigencia) de la póliza o certificado." ClientIDMode='Static' ClientVisible='True' meta:resourcekey="RiskInformationEffectiveDateResource"  Width='100px' ClientEnabled='False'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
       </dxe:ASPxDateEdit>
                </td>
                <td align='left'>
       <dxe:ASPxLabel ID='RiskInformationEffectiveDateMeasureLabel' ClientInstanceName='RiskInformationEffectiveDateMeasureLabel' runat='server' ClientEnabled='False' Text="(dd/mm/aaaa)" meta:resourcekey="RiskInformationEffectiveDateMeasureLabelResource"  ClientVisible='true'        ></dxe:ASPxLabel>
               </td>
            </tr>
        </table>
    </td>

<td style='width:17.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='uwcaseidLabel' EncodeHtml='false' ClientInstanceName='uwcaseidLabel' runat='server' ClientIDMode='Static' meta:resourcekey="uwcaseidLabelResource"  Text="Caso en tratamiento"  ClientEnabled='false'  ClientVisible='false'  AssociatedControlID='uwcaseid'       ></dxe:ASPxLabel></td>    <td style='width:17.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='uwcaseid' ClientInstanceName='uwcaseid' ToolTip="Caso de suscripción en tratamiento" Size='15' NullText="" ClientVisible='False' MaxLength='15' ClientIDMode='Static' meta:resourcekey="uwcaseidResource" ClientEnabled='False'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
    <dxtc:ASPxPageControl ID="tabcontainer0" runat="server" ClientIDMode='Static' ClientVisible="True" ActiveTabIndex="0" EnableHierarchyRecreation="True" Width="100%" TabAlign="Left" TabPosition="Top"  >
          <TabPages>

              <dxtc:TabPage Name="InfBasica" Text="Información básica" ClientVisible="True" ClientEnabled="True" ToolTip="" meta:resourcekey="InfBasicaResource">
  <TabStyle  Font-Bold="True"  Font-Size="12"  />
                 <ContentCollection>
                    <dxw:ContentControl runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
      <dxe:ASPxLabel ID='InfBasicaInstructionLabel' runat="server" style='font-size: xx-small;font-family: Verdana;font-weight: normal; color: gray;' Text="Incluya la información mínima necesaria para realizar el cálculo de la prima y presione el botón &quot;Cotizar&quot;. Si está de acuerdo con la prima mostrada, presione el botón &quot;Guardar y seguir&quot;.  Recuerde que los campos marcados con asterisco son de entrada obligatoria." meta:resourcekey="InfBasicaInstructionResource"/>
      <br /> <br />
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone17" ClientInstanceName="zone17" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone17Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone12IB" ClientInstanceName="zone12IB" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone12IBResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:25%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone6IB" ClientInstanceName="zone6IB" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone6IBResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label26IB' EncodeHtml='false' ClientInstanceName='label26IB' runat='server' ClientIDMode='Static' meta:resourcekey="label26IBResource"  Text="Información del asegurado"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="10"  ForeColor="#000000"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='GenderLabel' EncodeHtml='false' ClientInstanceName='GenderLabel' runat='server' ClientIDMode='Static' meta:resourcekey="GenderLabelResource"  Text="Género"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Gender'       ></dxe:ASPxLabel><br />

       <dxe:ASPxRadioButtonList ID='Gender' ClientInstanceName='Gender' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="Sexo del cliente." ClientVisible='true' ClientEnabled='True'  meta:resourcekey="GenderResource"  ValueType='System.String'   >
            <Items>
                <dxe:ListEditItem Value='1' Text='Femenino' meta:resourcekey="GenderListItemValue1Resource"/>
                <dxe:ListEditItem Value='2' Text='Masculino' meta:resourcekey="GenderListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="8px" PaddingRight="0px" PaddingTop="0px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone6IB" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='SmokerIndicatorLabel' EncodeHtml='false' ClientInstanceName='SmokerIndicatorLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SmokerIndicatorLabelResource"  Text="¿Fumador?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='SmokerIndicator'       ></dxe:ASPxLabel><br />

       <dxe:ASPxRadioButtonList ID='SmokerIndicator' ClientInstanceName='SmokerIndicator' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="Indicador de cliente fumador." ClientVisible='true' ClientEnabled='True'  meta:resourcekey="SmokerIndicatorResource"  ValueType='System.String'   >
            <Items>
                <dxe:ListEditItem Value='1' Text='Si' meta:resourcekey="SmokerIndicatorListItemValue1Resource"/>
                <dxe:ListEditItem Value='2' Text='No' meta:resourcekey="SmokerIndicatorListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="8px" PaddingRight="0px" PaddingTop="0px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone6IB" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='BirthDateLabel' EncodeHtml='false' ClientInstanceName='BirthDateLabel' runat='server' ClientIDMode='Static' meta:resourcekey="BirthDateLabelResource"  Text="Fecha de nacimiento"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='BirthDate'       ></dxe:ASPxLabel><br />

        <table>
            <tr>
                <td>
       <dxe:ASPxDateEdit runat='server' ID='BirthDate' ToolTip="Fecha de nacimiento del cliente." ClientIDMode='Static' ClientVisible='True' meta:resourcekey="BirthDateResource"  Width='180px' ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone6IB" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxDateEdit>
                </td>
                <td align='left'>
       <dxe:ASPxLabel ID='BirthDateMeasureLabel' ClientInstanceName='BirthDateMeasureLabel' runat='server' ClientEnabled='False' Text="(dd/mm/aaaa)" meta:resourcekey="BirthDateMeasureLabelResource"  ClientVisible='true'        ></dxe:ASPxLabel>
               </td>
            </tr>
        </table>
    </td>

  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:35%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone5IB" ClientInstanceName="zone5IB" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone5IBResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:90%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label27' EncodeHtml='false' ClientInstanceName='label27' runat='server' ClientIDMode='Static' meta:resourcekey="label27Resource"  Text="Suma por la que desea estar asegurado"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="10"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



    <td style="width:45%">
      &nbsp;
    </td>
    <td style="width:45%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:270%;' colspan='2' align='left'><div style='float: left;'>

       <dxe:ASPxTextBox runat='server' ID='InsuredAmountForCalculation' ClientInstanceName='InsuredAmountForCalculation' ToolTip="Capital Asegurado de Cálculo" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="InsuredAmountForCalculationResource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithText" ErrorTextPosition="Bottom" ValidationGroup="zone5IB" >
        <RequiredField IsRequired='true' ErrorText="Incluya suma asegurada" />
        <RegularExpression ValidationExpression="^[-0-9]\d*([,\.]\d+)?$" ErrorText="Incluya suma asegurada" />
</ValidationSettings>
<ClientSideEvents  ValueChanged="InsuredAmountForCalculationValueChanged" />
       </dxe:ASPxTextBox>
</div>

<div style='float: left;'>


<dxe:ASPxComboBox ID='Currency' runat='server' ClientInstanceName='Currency' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Moneda en la que se realiza la cotización" ClientVisible='true' ClientEnabled='False' meta:resourcekey="CurrencyResource"  Width='50px'  ValueType='System.Int32'    TextField='SSHORT_DES' ValueField='NCODIGINT'><ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone5IB" >
</ValidationSettings>
</dxe:ASPxComboBox>
</div>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:90%;' colspan='2' align='left'>


<dxe:ASPxComboBox ID='InsuredAmountSelected' runat='server' ClientInstanceName='InsuredAmountSelected' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Suma asegurada por la que desea estar asegurado" ClientVisible='true' ClientEnabled='True' meta:resourcekey="InsuredAmountSelectedResource"  Width='117px'  ValueType='System.Decimal'  AutoPostBack='false' OnSelectedIndexChanged='InsuredAmountSelected_SelectedIndexChanged' >
            <Items>
                <dxe:ListEditItem Value='10000' Text='10.000' meta:resourcekey="InsuredAmountSelectedListItemValue1Resource"/>
                <dxe:ListEditItem Value='20000' Text='20.000' meta:resourcekey="InsuredAmountSelectedListItemValue2Resource"/>
                <dxe:ListEditItem Value='35000' Text='35.000' meta:resourcekey="InsuredAmountSelectedListItemValue3Resource"/>
                <dxe:ListEditItem Value='50000' Text='50.000' meta:resourcekey="InsuredAmountSelectedListItemValue4Resource"/>
            </Items>
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="Address" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
<ClientSideEvents  ValueChanged="function(s, e) {AsyncPostBack()}" />
</dxe:ASPxComboBox>
    </td>

    <td style="width:45%">
      &nbsp;
    </td>
    <td style="width:45%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:90%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone3Cumulo" ClientInstanceName="zone3Cumulo" runat="server" HeaderText="" ToolTip="" Enabled="False" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone3CumuloResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='AccumulatedAmountLabel' EncodeHtml='false' ClientInstanceName='AccumulatedAmountLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AccumulatedAmountLabelResource"  Text="Suma asegurada otras pólizas"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='AccumulatedAmount'       ></dxe:ASPxLabel><br />

       <dxe:ASPxLabel ID='AccumulatedAmount' EncodeHtml='false' ClientInstanceName='AccumulatedAmount' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

    </td>

  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style="width:45%">
      &nbsp;
    </td>
    <td style="width:45%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:90%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone3" ClientInstanceName="zone3" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="False" meta:resourcekey="zone3Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='PaymentFrequencyLabel' EncodeHtml='false' ClientInstanceName='PaymentFrequencyLabel' runat='server' ClientIDMode='Static' meta:resourcekey="PaymentFrequencyLabelResource"  Text="Frecuencia de Pago"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='PaymentFrequency'       ></dxe:ASPxLabel><br />


<dxe:ASPxComboBox ID='PaymentFrequency' runat='server' ClientInstanceName='PaymentFrequency' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Frecuencia de Pago" ClientVisible='true' ClientEnabled='True' meta:resourcekey="PaymentFrequencyResource"  Width='180px'  ValueType='System.Int32'    TextField='SDESCRIPT' ValueField='NPAYFREQ'><ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone3" >
</ValidationSettings>
<ClientSideEvents  BeginCallback="PaymentFrequencyBeginCallback"  EndCallback="PaymentFrequencyEndCallback" />
</dxe:ASPxComboBox>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='PaymentMethodLabel' EncodeHtml='false' ClientInstanceName='PaymentMethodLabel' runat='server' ClientIDMode='Static' meta:resourcekey="PaymentMethodLabelResource"  Text="Vía de Pago"  ClientEnabled='true'  ClientVisible='false'  AssociatedControlID='PaymentMethod'       ></dxe:ASPxLabel><br />


<dxe:ASPxComboBox ID='PaymentMethod' runat='server' ClientInstanceName='PaymentMethod' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Vía de Pago" ClientVisible='false' ClientEnabled='True' meta:resourcekey="PaymentMethodResource"  Width='180px'  ValueType='System.Int32'    TextField='SDESCRIPT' ValueField='NWAY_PAY'><ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone3" >
</ValidationSettings>
<ClientSideEvents  SelectedIndexChanged="PaymentMethodSelectedIndexChanged" />
</dxe:ASPxComboBox>
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
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:40%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="Coverages" ClientInstanceName="Coverages" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="False" meta:resourcekey="CoveragesResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone12" ClientInstanceName="zone12" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone12Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='CoverageForAmendment' EnableRowsCache='False' EnableViewState='True' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='CoverageForAmendment' runat='server' Width='100%' KeyFieldName='Consecutive' Caption="COBERTURAS" meta:resourcekey="CoverageForAmendmentResource"

>
    <Settings ShowFooter="True"/>
    <TotalSummary>
        <dxwgv:ASPxSummaryItem FieldName="AnnualPremium" SummaryType="Sum" DisplayFormat="Prima total anual {0:n2}" meta:resourcekey="CoverageForAmendmentSummaryItem0" /> 

    </TotalSummary>
    <GroupSummary>
        <dxwgv:ASPxSummaryItem FieldName="AnnualPremium" SummaryType="Sum" DisplayFormat="Prima total anual {0:n2}" meta:resourcekey="CoverageForAmendmentSummaryItem0" /> 

    </GroupSummary>
                 <ClientSideEvents 
 RowDblClick="function(s, e) { CoverageForAmendment.StartEditRow(e.visibleIndex); }" />
            <SettingsEditing Mode="Inline" />
               <SettingsPager Visible="True" PageSize="10"/>
               <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AllowSort="True"/>
        <Columns>
<dxwgv:GridViewDataTextColumn FieldName='Consecutive' Visible='false'>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='DescriptionOfCoverage' FieldName='DescriptionOfCoverage' Caption="Cobertura" ToolTip="Cobertura" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="DescriptionOfCoverageFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='100' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='InsuredAmount' FieldName='InsuredAmount' Caption="Suma asegurada" ToolTip="Suma asegurada" GroupIndex="-1" VisibleIndex="1" meta:resourcekey="InsuredAmountFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,###,###,###,###,##0.00">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <-999999999999999999..999999999999999999g>.<00..99>' />
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='AnnualPremium' FieldName='AnnualPremium' Caption="Prima Anual" ToolTip="Prima Anual" GroupIndex="-1" VisibleIndex="2" meta:resourcekey="AnnualPremiumFieldResource"
 Visible='True'
 FooterCellStyle-HorizontalAlign="Right" 
><EditFormSettings VisibleIndex="2" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="##,###,###,###,###,##0.00">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <-9999999999999999..9999999999999999g>.<00..99>' />
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn Caption=' ' meta:resourcekey='CoverageForAmendmentCommandColumsResource'>
                                <EditButton Visible='True' Text='Editar' />
                                <CancelButton Visible='True' Text='Cancelar' />
                                <UpdateButton Visible='True' Text='Actualizar' />
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
                    <dxrp:ASPxRoundPanel ID="zone5" ClientInstanceName="zone5" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone5Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Center'>       <dxe:ASPxLabel ID='label1' EncodeHtml='false' ClientInstanceName='label1' runat='server' ClientIDMode='Static' meta:resourcekey="label1Resource"  Text="Una vez que incluya toda la información requerida, presione el botón 'Cotizar' para que pueda visualizar las coberturas y la prima."  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



    <td style='width:50%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='Cotizar' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Realiza el cálculo de la prima en base a la información indicada" ClientVisible='True' ClientEnabled='True' meta:resourcekey="CotizarResource" Text="Cotizar" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/settings1_16x16.gif" Width='250px'  Height='16px'  Font-Bold="True"  Font-Size="10"  BackColor="#D8D8D8"   OnClick='Cotizar_Click' AutoPostBack='false'>
<ClientSideEvents  Click="CotizarClick" />
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Center'>       <dxe:ASPxLabel ID='label6' EncodeHtml='false' ClientInstanceName='label6' runat='server' ClientIDMode='Static' meta:resourcekey="label6Resource"  Text="Si está de acuerdo con la prima mostrada y desea continuar con la solicitud, presione el botón 'Guardar y seguir'. En caso contrario, cambie algún dato y vuelva a usar el botón 'Cotizar'."  ClientEnabled='true'  ClientVisible='false'        ></dxe:ASPxLabel></td>



    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style='width:50%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='Acepto' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="" ClientVisible='False' ClientEnabled='True' meta:resourcekey="AceptoResource" Text="Guardar y seguir" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/ok_16x16.gif" Width='250px'  Height='16px'  Font-Bold="True"  Font-Size="10"  BackColor="#BFBFBF"   OnClick='Acepto_Click' AutoPostBack='false'>
<ClientSideEvents  Click="AceptoClick" />
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
          </dxw:ContentControl>
       </ContentCollection>
           </dxtc:TabPage>
              <dxtc:TabPage Name="tab1Autenticacion" Text="Autenticación" ClientVisible="False" ClientEnabled="True" ToolTip="" meta:resourcekey="tab1AutenticacionResource">
  <TabStyle  Font-Bold="True"  Font-Size="12"  />
                 <ContentCollection>
                    <dxw:ContentControl runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone28Autenticacion" ClientInstanceName="zone28Autenticacion" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone28AutenticacionResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone46" ClientInstanceName="zone46" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone46Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='ClienteInformaEsUsuarioLabel' EncodeHtml='false' ClientInstanceName='ClienteInformaEsUsuarioLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ClienteInformaEsUsuarioLabelResource"  Text="¿Está usted registrado como usuario del portal de la compañía de seguros?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='ClienteInformaEsUsuario'       ></dxe:ASPxLabel><br />

       <dxe:ASPxRadioButtonList ID='ClienteInformaEsUsuario' ClientInstanceName='ClienteInformaEsUsuario' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="¿Está usted registrado como usuario del portal de la compañía de seguros?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="ClienteInformaEsUsuarioResource"  ValueType='System.Boolean'  AutoPostBack='false' >
            <Items>
                <dxe:ListEditItem Value='True' Text='Si' meta:resourcekey="ClienteInformaEsUsuarioListItemValue1Resource"/>
                <dxe:ListEditItem Value='False' Text='No' meta:resourcekey="ClienteInformaEsUsuarioListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone46" >
</ValidationSettings>
<ClientSideEvents  ValueChanged="ClienteInformaEsUsuarioValueChanged" />
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
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone45" ClientInstanceName="zone45" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone45Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='ClienteInformaExisteLabel' EncodeHtml='false' ClientInstanceName='ClienteInformaExisteLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ClienteInformaExisteLabelResource"  Text="¿Es usted cliente de la compañía de seguros? ¿Tiene alguna póliza en la compañía o ha estado involucrado en algún siniestro?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='ClienteInformaExiste'       ></dxe:ASPxLabel><br />

       <dxe:ASPxRadioButtonList ID='ClienteInformaExiste' ClientInstanceName='ClienteInformaExiste' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="¿Es usted cliente de la compañía de seguros? ¿Tiene alguna póliza en la compañía o ha estado involucrado en algún siniestro?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="ClienteInformaExisteResource"  ValueType='System.Boolean'   >
            <Items>
                <dxe:ListEditItem Value='True' Text='Si' meta:resourcekey="ClienteInformaExisteListItemValue1Resource"/>
                <dxe:ListEditItem Value='False' Text='No' meta:resourcekey="ClienteInformaExisteListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone45" >
</ValidationSettings>
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
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone38" ClientInstanceName="zone38" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="False" meta:resourcekey="zone38Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='UsuarioClaveEntradaLabel' EncodeHtml='false' ClientInstanceName='UsuarioClaveEntradaLabel' runat='server' ClientIDMode='Static' meta:resourcekey="UsuarioClaveEntradaLabelResource"  Text="Usuario"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='UsuarioClaveEntrada'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='UsuarioClaveEntrada' ClientInstanceName='UsuarioClaveEntrada' ToolTip="" Size='60' NullText="" ClientVisible='True' MaxLength='60' ClientIDMode='Static' meta:resourcekey="UsuarioClaveEntradaResource" Width='270px'  ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone38" >
     <RequiredField IsRequired='True' ErrorText="" />
     <RegularExpression ValidationExpression="\w+([-+.']\w+)*@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]\s*$" ErrorText="Correo electrónico inválido" />
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
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone36" ClientInstanceName="zone36" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone36Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:300%;' colspan='3' align='left'>       <dxe:ASPxLabel ID='ClientIDAutLabel' EncodeHtml='false' ClientInstanceName='ClientIDAutLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ClientIDAutLabelResource"  Text="Identificación"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='ClientIDAut'       ></dxe:ASPxLabel><br /><div style='float: left;'>

       <dxe:ASPxTextBox runat='server' ID='ClientIDAut' ClientInstanceName='ClientIDAut' ToolTip="Código de Cliente" Size='14' NullText="" ClientVisible='True' MaxLength='14' ClientIDMode='Static' meta:resourcekey="ClientIDAutResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone36" >
     <RequiredField IsRequired='True' ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
</div>

       <dxe:ASPxLabel ID='CheckDigitNewLabel' EncodeHtml='false' ClientInstanceName='CheckDigitNewLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CheckDigitNewLabelResource"  Text="Dígito Verificador"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='CheckDigitNew'       ></dxe:ASPxLabel><div style='float: left;'>

       <dxe:ASPxTextBox runat='server' ID='CheckDigitNew' ClientInstanceName='CheckDigitNew' ToolTip="Dígito Verificador" Size='1' NullText="" ClientVisible='True' MaxLength='1' ClientIDMode='Static' meta:resourcekey="CheckDigitNewResource" Width='25px'  ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone36" >
     <RequiredField IsRequired='True' ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
</div>
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
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='FirstNameAutLabel' EncodeHtml='false' ClientInstanceName='FirstNameAutLabel' runat='server' ClientIDMode='Static' meta:resourcekey="FirstNameAutLabelResource"  Text="Nombre(s)"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='FirstNameAut'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='FirstNameAut' ClientInstanceName='FirstNameAut' ToolTip="Primer Nombre" Size='20' NullText="" ClientVisible='True' MaxLength='20' ClientIDMode='Static' meta:resourcekey="FirstNameAutResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone36" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='LastNameAutLabel' EncodeHtml='false' ClientInstanceName='LastNameAutLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LastNameAutLabelResource"  Text="Apellido paterno"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LastNameAut'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='LastNameAut' ClientInstanceName='LastNameAut' ToolTip="Apellido Paterno" Size='20' NullText="" ClientVisible='True' MaxLength='20' ClientIDMode='Static' meta:resourcekey="LastNameAutResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone36" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='LastName2AutLabel' EncodeHtml='false' ClientInstanceName='LastName2AutLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LastName2AutLabelResource"  Text="Apellido materno"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LastName2Aut'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='LastName2Aut' ClientInstanceName='LastName2Aut' ToolTip="Apellido Materno" Size='20' NullText="" ClientVisible='True' MaxLength='20' ClientIDMode='Static' meta:resourcekey="LastName2AutResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone36" >
     <RequiredField IsRequired='True' ErrorText="" />
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
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone29SeguirAutenticacion" ClientInstanceName="zone29SeguirAutenticacion" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone29SeguirAutenticacionResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='btnAutenticar' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="" ClientVisible='True' ClientEnabled='True' meta:resourcekey="btnAutenticarResource" Text="Autenticar" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/computer1_(start)_16x16.gif" Height='16px'   OnClick='btnAutenticar_Click' AutoPostBack='false'>
<ClientSideEvents  Click="btnAutenticarClick" />
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
          </dxw:ContentControl>
       </ContentCollection>
           </dxtc:TabPage>
              <dxtc:TabPage Name="tab13" Text="Información adicional" ClientVisible="False" ClientEnabled="True" ToolTip="" meta:resourcekey="tab13Resource">
  <TabStyle  Font-Bold="True"  Font-Size="12"  />
                 <ContentCollection>
                    <dxw:ContentControl runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
      <dxe:ASPxLabel ID='tab13InstructionLabel' runat="server" style='font-size: xx-small;font-family: Verdana;font-weight: normal; color: gray;' Text="Incluya la información adicional del asegurado. Recuerde que los campos marcados con asterisco son de entrada obligatoria." meta:resourcekey="tab13InstructionResource"/>
      <br /> <br />
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone2" ClientInstanceName="zone2" runat="server" HeaderText="" ToolTip="Por favor introduzca los datos adicionales del asegurado" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone2Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
      <dxe:ASPxLabel ID='zone2InstructionLabel' runat="server" style='font-size: xx-small;font-family: Verdana;font-weight: normal; color: gray;' Text="Por favor introduzca los datos adicionales del asegurado" meta:resourcekey="zone2InstructionResource"/>
      <br /> <br />
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone14" ClientInstanceName="zone14" runat="server" HeaderText="" ToolTip="Información adicional del asegurado" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone14Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label0' EncodeHtml='false' ClientInstanceName='label0' runat='server' ClientIDMode='Static' meta:resourcekey="label0Resource"  Text="Información adicional del asegurado"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="10"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone16" ClientInstanceName="zone16" runat="server" HeaderText="" ToolTip="Dirección del cliente" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone16Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label17' EncodeHtml='false' ClientInstanceName='label17' runat='server' ClientIDMode='Static' meta:resourcekey="label17Resource"  Text="Dirección"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="10"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



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
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone13" ClientInstanceName="zone13" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone13Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='ClientIDLabel' EncodeHtml='false' ClientInstanceName='ClientIDLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ClientIDLabelResource"  Text="Identificación"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='ClientID'       ></dxe:ASPxLabel></td>    <td style='width:150%;' colspan='3' align='left'><div style='float: left;'>

       <dxe:ASPxTextBox runat='server' ID='ClientID' ClientInstanceName='ClientID' ToolTip="Código del cliente que aparece como asegurado principal de la póliza" Size='14' NullText="" ClientVisible='True' MaxLength='14' ClientIDMode='Static' meta:resourcekey="ClientIDResource" Width='155px'  ClientEnabled='True' AutoPostBack='true' OnTextChanged='ClientID_TextChanged' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone6" >
     <RequiredField IsRequired='True' ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
</div>

<div style='float: left;'>

       <dxe:ASPxTextBox runat='server' ID='CheckDigit' ClientInstanceName='CheckDigit' ToolTip="Dígito Verificador" Size='1' NullText="" ClientVisible='True' MaxLength='1' ClientIDMode='Static' meta:resourcekey="CheckDigitResource" Width='25px'  ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone13" >
     <RequiredField IsRequired='True' ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
</div>
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
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HeightLabel' EncodeHtml='false' ClientInstanceName='HeightLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HeightLabelResource"  Text="Altura"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Height'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

        <table>
            <tr>
                <td>
       <dxe:ASPxTextBox runat='server' ID='Height' ClientInstanceName='Height' ToolTip="Altura del cliente en metros" Size='4' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="HeightResource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..9g>.<00..99>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="zone13" >
        <RequiredField IsRequired='true' ErrorText="El campo es requerido." />
        <RegularExpression ValidationExpression="^[-0-9]\d*([,\.]\d+)?$" ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
                </td>
                <td align='left'>
       <dxe:ASPxLabel ID='HeightMeasureLabel' ClientInstanceName='HeightMeasureLabel' runat='server' ClientEnabled='False' Text="mts" meta:resourcekey="HeightMeasureLabelResource"  ClientVisible='true'        ></dxe:ASPxLabel>
               </td>
            </tr>
        </table>
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
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='WeightLabel' EncodeHtml='false' ClientInstanceName='WeightLabel' runat='server' ClientIDMode='Static' meta:resourcekey="WeightLabelResource"  Text="Peso"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Weight'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

        <table>
            <tr>
                <td>
       <dxe:ASPxTextBox runat='server' ID='Weight' ClientInstanceName='Weight' ToolTip="Peso del cliente en kilogramos" Size='6' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="WeightResource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999g>.<00..99>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="zone13" >
        <RequiredField IsRequired='true' ErrorText="El campo es requerido." />
        <RegularExpression ValidationExpression="^[-0-9]\d*([,\.]\d+)?$" ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
                </td>
                <td align='left'>
       <dxe:ASPxLabel ID='WeightMeasureLabel' ClientInstanceName='WeightMeasureLabel' runat='server' ClientEnabled='False' Text="kgs" meta:resourcekey="WeightMeasureLabelResource"  ClientVisible='true'        ></dxe:ASPxLabel>
               </td>
            </tr>
        </table>
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
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='FirstNameLabel' EncodeHtml='false' ClientInstanceName='FirstNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="FirstNameLabelResource"  Text="Nombre(s)"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='FirstName'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='FirstName' ClientInstanceName='FirstName' ToolTip="Primer Nombre" Size='20' NullText="" ClientVisible='True' MaxLength='20' ClientIDMode='Static' meta:resourcekey="FirstNameResource" Width='180px'  ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zoneAS2" >
     <RequiredField IsRequired='True' ErrorText="" />
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
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LastNameLabel' EncodeHtml='false' ClientInstanceName='LastNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LastNameLabelResource"  Text="Apellido paterno"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LastName'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='LastName' ClientInstanceName='LastName' ToolTip="Apellido paterno (primer apellido) del cliente." Size='20' NullText="" ClientVisible='True' MaxLength='20' ClientIDMode='Static' meta:resourcekey="LastNameResource" Width='180px'  ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone13" >
     <RequiredField IsRequired='True' ErrorText="El campo es requerido." />
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
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LastName2Label' EncodeHtml='false' ClientInstanceName='LastName2Label' runat='server' ClientIDMode='Static' meta:resourcekey="LastName2LabelResource"  Text="Apellido materno"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LastName2'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='LastName2' ClientInstanceName='LastName2' ToolTip="Apellido materno (segundo apellido) del cliente." Size='20' NullText="" ClientVisible='True' MaxLength='20' ClientIDMode='Static' meta:resourcekey="LastName2Resource" Width='180px'  ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone13" >
     <RequiredField IsRequired='True' ErrorText="El campo es requerido." />
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
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone10" ClientInstanceName="zone10" runat="server" HeaderText="" ToolTip="Dirección" Enabled="False" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone10Resource"
 Width="100%" SkinID="SquareBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%;' colspan='2' align='left'>

          <ucPhysicalAddress:PhysicalAddress ID='physicaladdress0' Visible='True' runat='server' />

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
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style='width:50%;' align='left'>       <dxe:ASPxLabel ID='eMailclientLabel' EncodeHtml='false' ClientInstanceName='eMailclientLabel' runat='server' ClientIDMode='Static' meta:resourcekey="eMailclientLabelResource"  Text="Correo electrónico"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='eMailclient' Font-Bold="True"  Font-Size="10"  Theme="Office2003Olive"        ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='eMailclient' ClientInstanceName='eMailclient' ToolTip="Dirección del correo electrónico del cliente" Size='50' NullText="usuario@proveedor.com" ClientVisible='True' MaxLength='50' ClientIDMode='Static' meta:resourcekey="eMailclientResource" Width='200px'  ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone2" >
     <RequiredField IsRequired='True' ErrorText="El campo es requerido." />
     <RegularExpression ValidationExpression="\w+([-+.']\w+)*@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]\s*$" ErrorText="Correo electrónico inválido" />
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
                    <dxrp:ASPxRoundPanel ID="zone15" ClientInstanceName="zone15" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone15Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='button8' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button8Resource" Text="Guardar y seguir" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/ok_16x16.gif" Width='250px'  Font-Bold="True"  Font-Size="10"  BackColor="#BFBFBF"   OnClick='button8_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button8Click" />
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
          </dxw:ContentControl>
       </ContentCollection>
           </dxtc:TabPage>
              <dxtc:TabPage Name="tab17" Text="Salud" ClientVisible="False" ClientEnabled="True" ToolTip="Cuestionario básico de salud" meta:resourcekey="tab17Resource">
  <TabStyle  Font-Bold="True"  Font-Size="12"  />
                 <ContentCollection>
                    <dxw:ContentControl runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
      <dxe:ASPxLabel ID='tab17InstructionLabel' runat="server" style='font-size: xx-small;font-family: Verdana;font-weight: normal; color: gray;' Text="Dependiendo de las respuestas suministradas en este cuestionario, es posible que sea necesario que conteste cuestionarios adicionales de salud. Los mismos serán enviados a la cuenta de correo electrónico que usted indique para proceder a su llenado." meta:resourcekey="tab17InstructionResource"/>
      <br /> <br />
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone4" ClientInstanceName="zone4" runat="server" HeaderText="Cuestionario básico de salud" ToolTip="Cuestionario básico de salud" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone4Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label3' EncodeHtml='false' ClientInstanceName='label3' runat='server' ClientIDMode='Static' meta:resourcekey="label3Resource"  Text="Información básica de salud del asegurado"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="10"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label25' EncodeHtml='false' ClientInstanceName='label25' runat='server' ClientIDMode='Static' meta:resourcekey="label25Resource"  Text="Información confidencial: Las siguientes preguntas de su salud son con la finalidad de ofrecerle una cotización personalizada. Sabemos que es información privada, y por lo tanto la trataremos como tal. No compartimos esta información."  ClientEnabled='true'  ClientVisible='true'  Font-Size="8"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



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
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='DiabetesLabel' EncodeHtml='false' ClientInstanceName='DiabetesLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DiabetesLabelResource"  Text="¿Sufre o ha sufrido de diábetes?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Diabetes'       ></dxe:ASPxLabel><br />

       <dxe:ASPxRadioButtonList ID='Diabetes' ClientInstanceName='Diabetes' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="Cuestionario de diábetes." ClientVisible='true' ClientEnabled='True'  meta:resourcekey="DiabetesResource"  ValueType='System.Boolean'   >
            <Items>
                <dxe:ListEditItem Value='true' Text='Si' meta:resourcekey="DiabetesListItemValue1Resource"/>
                <dxe:ListEditItem Value='false' Text='No' meta:resourcekey="DiabetesListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="8px" PaddingRight="0px" PaddingTop="0px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone4" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='HeartLabel' EncodeHtml='false' ClientInstanceName='HeartLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HeartLabelResource"  Text="¿Sufre o ha sufrido de alguna enfermedad coronaria?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Heart'       ></dxe:ASPxLabel><br />

       <dxe:ASPxRadioButtonList ID='Heart' ClientInstanceName='Heart' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="Cuestionario de Corazón." ClientVisible='true' ClientEnabled='True'  meta:resourcekey="HeartResource"  ValueType='System.Boolean'   >
            <Items>
                <dxe:ListEditItem Value='true' Text='Si' meta:resourcekey="HeartListItemValue1Resource"/>
                <dxe:ListEditItem Value='false' Text='No' meta:resourcekey="HeartListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="8px" PaddingRight="0px" PaddingTop="0px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone4" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='CancerLabel' EncodeHtml='false' ClientInstanceName='CancerLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CancerLabelResource"  Text="¿Tiene o ha tenido cáncer, tumores o quistes?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Cancer'       ></dxe:ASPxLabel><br />

       <dxe:ASPxRadioButtonList ID='Cancer' ClientInstanceName='Cancer' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="Cuestionario de Cáncer." ClientVisible='true' ClientEnabled='True'  meta:resourcekey="CancerResource"  ValueType='System.Boolean'   >
            <Items>
                <dxe:ListEditItem Value='true' Text='Si' meta:resourcekey="CancerListItemValue1Resource"/>
                <dxe:ListEditItem Value='false' Text='No' meta:resourcekey="CancerListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="8px" PaddingRight="0px" PaddingTop="0px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone4" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
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
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone7" ClientInstanceName="zone7" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone7Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='button14' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button14Resource" Text="Guardar y seguir" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/ok_16x16.gif" Width='250px'  Height='16px'  Font-Bold="True"  Font-Size="10"  BackColor="#BFBFBF"   OnClick='button14_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button14Click" />
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
          </dxw:ContentControl>
       </ContentCollection>
           </dxtc:TabPage>
              <dxtc:TabPage Name="tab18" Text="Beneficiarios" ClientVisible="False" ClientEnabled="True" ToolTip="" meta:resourcekey="tab18Resource">
  <TabStyle  Font-Bold="True"  Font-Size="12"  />
                 <ContentCollection>
                    <dxw:ContentControl runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
      <dxe:ASPxLabel ID='tab18InstructionLabel' runat="server" style='font-size: xx-small;font-family: Verdana;font-weight: normal; color: gray;' Text="Indique la información de los beneficiarios" meta:resourcekey="tab18InstructionResource"/>
      <br /> <br />
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zoneBeneficiarios" ClientInstanceName="zoneBeneficiarios" runat="server" HeaderText="Beneficiarios" ToolTip="Beneficiarios" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zoneBeneficiariosResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:20%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone9" ClientInstanceName="zone9" runat="server" HeaderText="" ToolTip="Beneficiarios" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone9Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%;' colspan='2' align='left'>

       <dxe:ASPxRadioButtonList ID='BeneficiaryType' ClientInstanceName='BeneficiaryType' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Vertical' ClientIDMode='Static' ToolTip="" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="BeneficiaryTypeResource"  ValueType='System.Int32'  AutoPostBack='false' OnSelectedIndexChanged='BeneficiaryType_SelectedIndexChanged' >
            <Items>
                <dxe:ListEditItem Value='1' Text='Herederos legales' meta:resourcekey="BeneficiaryTypeListItemValue1Resource"/>
                <dxe:ListEditItem Value='3' Text='Asignados por el asegurado' meta:resourcekey="BeneficiaryTypeListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone9" >
</ValidationSettings>
<ClientSideEvents SelectedIndexChanged="AsyncPostBack"/>
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
    <td style='width:80%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zoneBeneficiariosAsignados" ClientInstanceName="zoneBeneficiariosAsignados" runat="server" HeaderText="Beneficiarios" ToolTip="Beneficiarios" Enabled="True" ClientIDMode='Static' ClientVisible="False" meta:resourcekey="zoneBeneficiariosAsignadosResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone31" ClientInstanceName="zone31" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone31Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Left'>

       <dxe:ASPxButton ID='button1' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Agregar beneficiario" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button1Resource" EnableTheming='False' EnableDefaultAppearance='False' Image-Url="/images/Library/16x16_ASPNetIcons/add_16x16.gif"   OnClick='button1_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button1Click" />
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='Beneficiary' EnableRowsCache='False' EnableViewState='True' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='Beneficiary' runat='server' Width='100%' KeyFieldName='ClientID' Caption="Beneficiarios" meta:resourcekey="BeneficiaryResource"

>
                 <ClientSideEvents 
 RowDblClick="function(s, e) { Beneficiary.StartEditRow(e.visibleIndex); }" />
            <SettingsEditing Mode="Inline" />
               <SettingsPager Visible="True" PageSize="10"/>
               <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AllowSort="True"/>
                     <SettingsEditing EditFormColumnCount="1"/>
        <Columns>
<dxwgv:GridViewDataTextColumn FieldName='ClientID' Visible='false'>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='CompleteClientName' FieldName='Client.CompleteClientName' Caption="Nombre" ToolTip="Nombre Completo del Cliente" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="CompleteClientNameFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='63' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='PercentageShare' FieldName='PercentageShare' Caption="%Participación en la póliza" ToolTip="Porcentaje de participación del beneficiario." GroupIndex="-1" VisibleIndex="1" meta:resourcekey="PercentageShareFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0.00">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <0..99999g>.<00..99>' />
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='Relationship' FieldName='Relationship' Caption="Relación con el asegurado" ToolTip="Relación del beneficiario con el asegurado de la póliza."  GroupIndex="-1" VisibleIndex="2" meta:resourcekey="RelationshipFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
<PropertiesComboBox EnableCallbackMode='false' IncrementalFilteringMode='StartsWith'
 TextField='SDESCRIPT' ValueField='NRELATION'>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="Beneficiary" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
                            <dxwgv:GridViewCommandColumn Caption=' ' meta:resourcekey='BeneficiaryCommandColumsResource'>
                                <EditButton Visible='True' Text='Editar' />
                                <DeleteButton Visible='True' Text='Eliminar' />
                                <CancelButton Visible='True' Text='Cancelar' />
                                <UpdateButton Visible='True' Text='Actualizar' />
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
                    <dxrp:ASPxRoundPanel ID="zone24" ClientInstanceName="zone24" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="False" meta:resourcekey="zone24Resource"
 Width="100%" SkinID="SquareBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone34" ClientInstanceName="zone34" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone34Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='8'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:25%;' align='left'>       <dxe:ASPxLabel ID='RelationshipBDLabel' EncodeHtml='false' ClientInstanceName='RelationshipBDLabel' runat='server' ClientIDMode='Static' meta:resourcekey="RelationshipBDLabelResource"  Text="Relación con el asegurado"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='RelationshipBD'       ></dxe:ASPxLabel><br />


<dxe:ASPxComboBox ID='RelationshipBD' runat='server' ClientInstanceName='RelationshipBD' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Nexo" ClientVisible='true' ClientEnabled='True' meta:resourcekey="RelationshipBDResource"  Width='200px'  ValueType='System.Int32'    TextField='SDESCRIPT' ValueField='NRELATION'>         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone24" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
</dxe:ASPxComboBox>
    </td>

    <td style='width:25%;' align='left'>       <dxe:ASPxLabel ID='PercentageShareBPLabel' EncodeHtml='false' ClientInstanceName='PercentageShareBPLabel' runat='server' ClientIDMode='Static' meta:resourcekey="PercentageShareBPLabelResource"  Text="%Participación en la póliza"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='PercentageShareBP'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='PercentageShareBP' ClientInstanceName='PercentageShareBP' ToolTip="Porcentaje de Participación" Size='8' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="PercentageShareBPResource"  Width='200px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-99999..99999g>.<00..99>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="zone34" >
        <RequiredField IsRequired='true' ErrorText="" />
        <RegularExpression ValidationExpression="^[-0-9]\d*([,\.]\d+)?$" ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:25%;' colspan='2' align='left'>

       <dxe:ASPxRadioButtonList ID='TypeOfPersonBenef' ClientInstanceName='TypeOfPersonBenef' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Vertical' ClientIDMode='Static' ToolTip="Tipo de Persona" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="TypeOfPersonBenefResource"  ValueType='System.Int32'  AutoPostBack='false' OnSelectedIndexChanged='TypeOfPersonBenef_SelectedIndexChanged' >
            <Items>
                <dxe:ListEditItem Value='1' Text='Persona' meta:resourcekey="TypeOfPersonBenefListItemValue1Resource"/>
                <dxe:ListEditItem Value='2' Text='Empresa' meta:resourcekey="TypeOfPersonBenefListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="8px" PaddingRight="0px" PaddingTop="0px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone34" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
<ClientSideEvents SelectedIndexChanged="AsyncPostBack"/>
       </dxe:ASPxRadioButtonList>
    </td>

    <td style='width:75%;' colspan='3' align='left'>       <dxe:ASPxLabel ID='ClientIDBDLabel' EncodeHtml='false' ClientInstanceName='ClientIDBDLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ClientIDBDLabelResource"  Text="Identificación"  ClientEnabled='true'  ClientVisible='false'  AssociatedControlID='ClientIDBD'       ></dxe:ASPxLabel><br /><div style='float: left;'>

       <dxe:ASPxTextBox runat='server' ID='ClientIDBD' ClientInstanceName='ClientIDBD' ToolTip="Código de Cliente" Size='14' NullText="" ClientVisible='False' MaxLength='14' ClientIDMode='Static' meta:resourcekey="ClientIDBDResource" ClientEnabled='True' AutoPostBack='true' OnTextChanged='ClientIDBD_TextChanged' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone24" >
     <RequiredField IsRequired='True' ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
</div>

<div style='float: left;'>

       <dxe:ASPxTextBox runat='server' ID='CheckDigitBenif' ClientInstanceName='CheckDigitBenif' ToolTip="Dígito Verificador" Size='1' NullText="" ClientVisible='True' MaxLength='1' ClientIDMode='Static' meta:resourcekey="CheckDigitBenifResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone34" >
     <RequiredField IsRequired='True' ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
</div>
    </td>

  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone35" ClientInstanceName="zone35" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone35Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='8'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:25%;' align='left'>       <dxe:ASPxLabel ID='FirstNameBPLabel' EncodeHtml='false' ClientInstanceName='FirstNameBPLabel' runat='server' ClientIDMode='Static' meta:resourcekey="FirstNameBPLabelResource"  Text="Nombre(s)"  ClientEnabled='true'  ClientVisible='false'  AssociatedControlID='FirstNameBP'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='FirstNameBP' ClientInstanceName='FirstNameBP' ToolTip="Primer Nombre" Size='20' NullText="" ClientVisible='False' MaxLength='20' ClientIDMode='Static' meta:resourcekey="FirstNameBPResource" Width='200px'  ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone35" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:25%;' align='left'>       <dxe:ASPxLabel ID='LastNameBPLabel' EncodeHtml='false' ClientInstanceName='LastNameBPLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LastNameBPLabelResource"  Text="Apellido paterno"  ClientEnabled='true'  ClientVisible='false'  AssociatedControlID='LastNameBP'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='LastNameBP' ClientInstanceName='LastNameBP' ToolTip="Apellido Paterno" Size='20' NullText="" ClientVisible='False' MaxLength='20' ClientIDMode='Static' meta:resourcekey="LastNameBPResource" Width='200px'  ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone35" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:25%;' align='left'>       <dxe:ASPxLabel ID='LastName2BPLabel' EncodeHtml='false' ClientInstanceName='LastName2BPLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LastName2BPLabelResource"  Text="Apellido materno"  ClientEnabled='true'  ClientVisible='false'  AssociatedControlID='LastName2BP'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='LastName2BP' ClientInstanceName='LastName2BP' ToolTip="Apellido Materno" Size='20' NullText="" ClientVisible='False' MaxLength='20' ClientIDMode='Static' meta:resourcekey="LastName2BPResource" Width='200px'  ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone35" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label2' EncodeHtml='false' ClientInstanceName='label2' runat='server' ClientIDMode='Static' meta:resourcekey="label2Resource"  Text="label 2"  ClientEnabled='true'  ClientVisible='false'        ></dxe:ASPxLabel></td>



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
                    <dxrp:ASPxRoundPanel ID="zone37" ClientInstanceName="zone37" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone37Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='LegalNameBPLabel' EncodeHtml='false' ClientInstanceName='LegalNameBPLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LegalNameBPLabelResource"  Text="Nombre legal"  ClientEnabled='true'  ClientVisible='false'  AssociatedControlID='LegalNameBP'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='LegalNameBP' ClientInstanceName='LegalNameBP' ToolTip="Nombre Legal" Size='60' NullText="" ClientVisible='False' MaxLength='60' ClientIDMode='Static' meta:resourcekey="LegalNameBPResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone24" >
     <RequiredField IsRequired='True' ErrorText="" />
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
    <td style="width:50%">
      &nbsp;
    </td>
    <td style="width:50%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone25" ClientInstanceName="zone25" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone25Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style="width:0%">
      &nbsp;
    </td>
    <td style="width:0%">
      &nbsp;
    </td>
    <td style='width:50%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='button33' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="" ClientVisible='False' ClientEnabled='True' meta:resourcekey="button33Resource" EnableTheming='False' EnableDefaultAppearance='False' Image-Url="/images/Library/16x16_ASPNetIcons/ok_16x16.gif"  Height='16px'   OnClick='button33_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button33Click" />
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
                    <dxrp:ASPxRoundPanel ID="zone11" ClientInstanceName="zone11" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone11Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='button12' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button12Resource" Text="Guardar y seguir" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/ok_16x16.gif" Width='250px'  Font-Bold="True"  Font-Size="10"  BackColor="#BFBFBF"   OnClick='button12_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button12Click" />
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
          </dxw:ContentControl>
       </ContentCollection>
           </dxtc:TabPage>
              <dxtc:TabPage Name="tab0" Text="Pago" ClientVisible="False" ClientEnabled="True" ToolTip="" meta:resourcekey="tab0Resource">
  <TabStyle  Font-Bold="True"  Font-Size="12"  />
                 <ContentCollection>
                    <dxw:ContentControl runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone6" ClientInstanceName="zone6" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone6Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label20' EncodeHtml='false' ClientInstanceName='label20' runat='server' ClientIDMode='Static' meta:resourcekey="label20Resource"  Text="Los datos que se indican a continuación corresponden a la tarjeta de crédito a utilizar para realizar el pago de la primera prima. Una vez emitida la póliza, nuestro personal se pondrá en contacto con el asegurado a fin de establecer el medio de pago que se utilizará en los pagos posteriores."  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



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
                    <dxrp:ASPxRoundPanel ID="zone22" ClientInstanceName="zone22" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone22Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:20%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone26" ClientInstanceName="zone26" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone26Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:50%;' colspan='2' align='left'>

       <dxe:ASPxRadioButtonList ID='CreditCardType' ClientInstanceName='CreditCardType' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Vertical' ClientIDMode='Static' ToolTip="Tipo de Tarjeta de Crédito" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="CreditCardTypeResource"  ValueType='System.Int32'    TextField='SDESCRIPT' ValueField='NCARD_TYPE'>         <Paddings PaddingBottom="0px" PaddingLeft="8px" PaddingRight="0px" PaddingTop="0px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone22" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:80%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone27" ClientInstanceName="zone27" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone27Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='BankCodeLabel' EncodeHtml='false' ClientInstanceName='BankCodeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="BankCodeLabelResource"  Text="Banco"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='BankCode'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>


<dxe:ASPxComboBox ID='BankCode' runat='server' ClientInstanceName='BankCode' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Código del Banco" ClientVisible='true' ClientEnabled='True' meta:resourcekey="BankCodeResource"  Width='180px'  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NBANK_CODE'>         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone22" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
</dxe:ASPxComboBox>
    </td>

    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='CreditCardNumberLabel' EncodeHtml='false' ClientInstanceName='CreditCardNumberLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CreditCardNumberLabelResource"  Text="Tarjeta"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='CreditCardNumber'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='CreditCardNumber' ClientInstanceName='CreditCardNumber' ToolTip="Tarjeta de Crédito" Size='20' NullText="" ClientVisible='True' MaxLength='20' ClientIDMode='Static' meta:resourcekey="CreditCardNumberResource" Width='180px'  ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone22" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='MesTarjetaLabel' EncodeHtml='false' ClientInstanceName='MesTarjetaLabel' runat='server' ClientIDMode='Static' meta:resourcekey="MesTarjetaLabelResource"  Text="Vencimiento"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='MesTarjeta'       ></dxe:ASPxLabel></td>    <td style='width:75%;' colspan='3' align='left'><div style='float: left;'>

        <table>
            <tr>
                <td>
       <dxe:ASPxTextBox runat='server' ID='MesTarjeta' ClientInstanceName='MesTarjeta' ToolTip="Mes de Vencimiento" Size='2' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="MesTarjetaResource"  Width='90px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-99..99g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="zone22" >
        <RequiredField IsRequired='true' ErrorText="" />
        <RegularExpression ValidationExpression="^[-0-9]\d*([,\.]\d+)?$" ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
                </td>
                <td align='left'>
       <dxe:ASPxLabel ID='MesTarjetaMeasureLabel' ClientInstanceName='MesTarjetaMeasureLabel' runat='server' ClientEnabled='False' Text="/" meta:resourcekey="MesTarjetaMeasureLabelResource"  ClientVisible='true'        ></dxe:ASPxLabel>
               </td>
            </tr>
        </table>
</div>

<div style='float: left;'>

       <dxe:ASPxTextBox runat='server' ID='AnoTarjeta' ClientInstanceName='AnoTarjeta' ToolTip="Año de Vencimiento" Size='4' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="AnoTarjetaResource"  Width='90px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-9999..9999>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="zone22" >
        <RequiredField IsRequired='true' ErrorText="" />
        <RegularExpression ValidationExpression="^[-0-9]\d*([,\.]\d+)?$" ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
</div>
    </td>

  </tr>
  <tr valign='top'>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='AuthorizationNumberLabel' EncodeHtml='false' ClientInstanceName='AuthorizationNumberLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AuthorizationNumberLabelResource"  Text="Nro.Autorización"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='AuthorizationNumber'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='AuthorizationNumber' ClientInstanceName='AuthorizationNumber' ToolTip="Número de Autorización" Size='5' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="AuthorizationNumberResource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..99999>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="zone22" >
        <RequiredField IsRequired='true' ErrorText="" />
        <RegularExpression ValidationExpression="^[-0-9]\d*([,\.]\d+)?$" ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
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
                    <dxrp:ASPxRoundPanel ID="zone19" ClientInstanceName="zone19" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone19Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='buttonGPago' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="" ClientVisible='True' ClientEnabled='True' meta:resourcekey="buttonGPagoResource" Text="Finalizar" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/ok_16x16.gif" Width='250px'  Font-Bold="True"  Font-Size="10"  BackColor="#BFBFBF"   OnClick='buttonGPago_Click' AutoPostBack='false'>
<ClientSideEvents  Click="buttonGPagoClick" />
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
          </dxw:ContentControl>
       </ContentCollection>
           </dxtc:TabPage>
          </TabPages>
      </dxtc:ASPxPageControl>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zonegeneral" ClientInstanceName="zonegeneral" runat="server" HeaderText="" ToolTip="" Enabled="False" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zonegeneralResource"
 Width="100%" SkinID="SquareBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone1" ClientInstanceName="zone1" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="False" meta:resourcekey="zone1Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:33%'  colspan='2' align='Left'>

       <dxe:ASPxButton ID='button0' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="El caso quedará guardado de manera temporal. Posteriormente puede ser recuperado y completado." ClientVisible='True' ClientEnabled='True' meta:resourcekey="button0Resource" Text="Guardar temporalmente" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/data_16x16.gif" Width='250px'  Font-Bold="True"  Font-Size="10"  BackColor="#BFBFBF"   OnClick='button0_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button0Click" />
       </dxe:ASPxButton>
    </td>

    <td style='width:33%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='button19' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Ver el resumen de la cotizaciòn" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button19Resource" Text="Ver resumen" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/zoom_in_16x16.gif" Width='250px'  Height='16px'  Font-Bold="True"  Font-Size="10"  BackColor="#BFBFBF"   OnClick='button19_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button19Click" />
       </dxe:ASPxButton>
    </td>

    <td style='width:34%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='EnviarCotizacionEmail' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Enviar la cotización al correo electrónico indicado" ClientVisible='True' ClientEnabled='True' meta:resourcekey="EnviarCotizacionEmailResource" Text="Enviar eMail" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/mail1_16x16.gif" Width='250px'  Font-Bold="True"  Font-Size="10"  BackColor="#BFBFBF"   OnClick='EnviarCotizacionEmail_Click' AutoPostBack='false'>
<ClientSideEvents  Click="EnviarCotizacionEmailClick" />
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
    <td style="width:17%">
      &nbsp;
    </td>
    <td style="width:17%">
      &nbsp;
    </td>
    <td style="width:17%">
      &nbsp;
    </td>
    <td style="width:17%">
      &nbsp;
    </td>
    <td style="width:17%">
      &nbsp;
    </td>
    <td style="width:17%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style="width:17%">
      &nbsp;
    </td>
    <td style="width:17%">
      &nbsp;
    </td>
    <td style="width:17%">
      &nbsp;
    </td>
    <td style="width:17%">
      &nbsp;
    </td>
    <td style='width:34%;' colspan='2' align='right'>

       <dxe:ASPxTextBox runat='server' ID='eMail' ClientInstanceName='eMail' ToolTip="Dirección del correo electrónico donde le llegará la cotización." Size='60' NullText="usuario@proveedor.com" ClientVisible='True' MaxLength='60' ClientIDMode='Static' meta:resourcekey="eMailResource" Width='250px'  ClientEnabled='True' AutoPostBack='true' OnTextChanged='eMail_TextChanged' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zoneemail" >
     <RequiredField IsRequired='True' ErrorText="" />
     <RegularExpression ValidationExpression="\w+([-+.']\w+)*@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]\s*$" ErrorText="Correo electrónico inválido" />
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
                    <dxrp:ASPxRoundPanel ID="zone8" ClientInstanceName="zone8" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="False" meta:resourcekey="zone8Resource"
 Width="100%" SkinID="RoundedBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:66%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone32" ClientInstanceName="zone32" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone32Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='labelComprar' EncodeHtml='false' ClientInstanceName='labelComprar' runat='server' ClientIDMode='Static' meta:resourcekey="labelComprarResource"  Text="Esta cotización se emite a modo puramente referencial, a partir de los datos suministrados en línea; por lo tanto, la suma asegurada, tasas y demás rubros aquí previstos, pueden ser objeto de modificación con base en la documentación que efectivamente presente el Propuesto Tomador al solicitar formalmente la contratación de la póliza, la cual también dependerá de los resultados de la evaluación del asegurado."  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='labelSOlicitud' EncodeHtml='false' ClientInstanceName='labelSOlicitud' runat='server' ClientIDMode='Static' meta:resourcekey="labelSOlicitudResource"  Text="Esta cotización debe ser completada con los cuestionarios de salud que deben ser rellenados por el asegurado. Al “Registrar la solicitud” le llegará, a la cuenta de correo proporcionada, el link correspondiente para el llenado del cuestionario requerido."  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:34%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone33" ClientInstanceName="zone33" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone33Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='Accept' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Permite comprar la póliza" ClientVisible='False' ClientEnabled='True' meta:resourcekey="AcceptResource" Text="Procesar solicitud" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/shopping_cart_16x16.gif" Width='250px'  Font-Bold="True"  Font-Size="10"  BackColor="#BFBFBF"   OnClick='Accept_Click' AutoPostBack='false'>
<ClientSideEvents  Click="AcceptClick" />
       </dxe:ASPxButton>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Right'>       <dxe:ASPxCheckBox ID='OnLinePrintIndicator' runat='server' Text="Ver cuadro de póliza en línea" ClientIDMode='Static' ClientVisible='false' ClientEnabled='True' meta:resourcekey="OnLinePrintIndicator"   EncodeHtml='false' > 
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
                    <dxrp:ASPxRoundPanel ID="zone20" ClientInstanceName="zone20" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone20Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone21" ClientInstanceName="zone21" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone21Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone18" ClientInstanceName="zone18" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone18Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style="width:0%">
      &nbsp;
    </td>
    <td style="width:0%">
      &nbsp;
    </td>
    <td style='width:50%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='Rechazar' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="El cliente no desea registrar la cotización. Finaliza el proceso." ClientVisible='True' ClientEnabled='True' meta:resourcekey="RechazarResource" Text="Salir sin guardar" ImagePosition='Right' Image-Url="/images/Library/16x16_ASPNetIcons/delete_16x16.gif" Width='250px'  Font-Bold="True"  Font-Size="10"  BackColor="#BFBFBF"   OnClick='Rechazar_Click' AutoPostBack='true'>
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
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone23" ClientInstanceName="zone23" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="False" meta:resourcekey="zone23Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LineOfBusinessLabel' EncodeHtml='false' ClientInstanceName='LineOfBusinessLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LineOfBusinessLabelResource"  Text="Ramo"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LineOfBusiness'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>


<dxe:ASPxComboBox ID='LineOfBusiness' runat='server' ClientInstanceName='LineOfBusiness' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Código del ramo comercial." ClientVisible='true' ClientEnabled='True' meta:resourcekey="LineOfBusinessResource"  ValueType='System.Int32'    TextField='SDESCRIPT' ValueField='NBRANCH'>         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone0" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
<ClientSideEvents  SelectedIndexChanged="LineOfBusinessSelectedIndexChanged" />
</dxe:ASPxComboBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='RiskInformationProductCodeLabel' EncodeHtml='false' ClientInstanceName='RiskInformationProductCodeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="RiskInformationProductCodeLabelResource"  Text="Plan"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='RiskInformationProductCode'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>


<dxe:ASPxComboBox ID='RiskInformationProductCode' runat='server' ClientInstanceName='RiskInformationProductCode' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Código del producto." ClientVisible='true' ClientEnabled='False' meta:resourcekey="RiskInformationProductCodeResource"  Width='300px'  ValueType='System.Int32'  TextFormatString="{1}" DropDownStyle= "DropDownList"   TextField='SDESCRIPT' ValueField='NPRODUCT'>
           <Columns>
                <dxe:ListBoxColumn FieldName="NPRODUCT" Caption="CódigoDelProducto" Visible="True"   meta:resourcekey="RiskInformationPrResource"/>
                <dxe:ListBoxColumn FieldName="SDESCRIPT" Caption="Descripción" Visible="True"   meta:resourcekey="RiskInformationPrResource"/>
            </Columns>
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="RiskInformation" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
<ClientSideEvents  BeginCallback="RiskInformationProductCodeBeginCallback"  EndCallback="RiskInformationProductCodeEndCallback" />
</dxe:ASPxComboBox>
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