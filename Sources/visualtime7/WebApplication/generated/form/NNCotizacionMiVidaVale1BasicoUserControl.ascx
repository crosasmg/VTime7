<%@ Control Language="VB" AutoEventWireup="false" CodeFile="NNCotizacionMiVidaVale1BasicoUserControl.ascx.vb" Inherits="NNCotizacionMiVidaVale1BasicoUserControl" %>
<%@ Register Src="NNCotizacionMiVidaValeSecuenciaUserControl.ascx" TagName="NNCotizacionMiVidaValeSecuencia" TagPrefix="NNCotizacionMiVidaValeSecuencia_UC" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">
    var msgControlsDependencyResource='<asp:Localize runat="server" Text="Espere..." meta:resourcekey="ControlsDependencyResource"></asp:Localize>';
    var msgCotizarInformationMessageResource='<asp:Localize runat="server" Text="Realizando el cálculo de la prima. Por favor espere..." meta:resourcekey="CotizarInformationMessageResource"></asp:Localize>';
    var titleCotizarInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titleCotizarInformationMessageResource"></asp:Localize>';
    var msgAceptoInformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="AceptoInformationMessageResource"></asp:Localize>';
    var titleAceptoInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titleAceptoInformationMessageResource"></asp:Localize>';
    var msgControlsDependencyResource='<asp:Localize runat="server" Text="Espere..." meta:resourcekey="ControlsDependencyResource"></asp:Localize>';



</script>

<script src="/generated/form/NNCotizacionMiVidaVale1Basico.js" type="text/javascript"></script>      
<asp:UpdatePanel ID="NNCotizacionMiVidaVale1BasicoUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='NNCotizacionMiVidaVale1BasicoTablePage' runat='server' style='width: 100%;margin: auto;'>
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
                    <dxrp:ASPxRoundPanel ID="zone28" ClientInstanceName="zone28" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone28Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:10%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone29" ClientInstanceName="zone29" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone29Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
<NNCotizacionMiVidaValeSecuencia_UC:NNCotizacionMiVidaValeSecuencia ID="NNCotizacionMiVidaValeSecuencia" runat="server" />
    </td>
  </tr>
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:90%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone30" ClientInstanceName="zone30" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone30Resource"
 Width="100%" SkinID="SquareBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Center'>       <dxe:ASPxLabel ID='label3' EncodeHtml='false' ClientInstanceName='label3' runat='server' ClientIDMode='Static' meta:resourcekey="label3Resource"  Text="INFORMACIÓN BÁSICA"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="12"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone39" ClientInstanceName="zone39" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone39Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone17InfBasica" ClientInstanceName="zone17InfBasica" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone17InfBasicaResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
      <dxe:ASPxLabel ID='zone17InfBasicaInstructionLabel' runat="server" style='font-size: xx-small;font-family: Verdana;font-weight: normal; color: gray;' Text="Incluya la información mínima necesaria para realizar el cálculo de la prima y presione el botón &quot;Cotizar&quot;. Si está de acuerdo con la prima mostrada, presione el botón &quot;Guardar y seguir&quot;.  Recuerde que los campos marcados con asterisco son de entrada obligatoria." meta:resourcekey="zone17InfBasicaInstructionResource"/>
      <br /> <br />
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
               <SettingsPager Visible="True" PageSize="10"/>
 <SettingsBehavior AllowFocusedRow="True" AllowSort="False"/>
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
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Center'>       <dxe:ASPxLabel ID='FinalMessageLabel' EncodeHtml='false' ClientInstanceName='FinalMessageLabel' runat='server' ClientIDMode='Static' meta:resourcekey="FinalMessageLabelResource"  Text="FinalMessage"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Italic="True"  Font-Size="12"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



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
                <dxe:ListBoxColumn FieldName="NPRODUCT" Caption="CódigoDelProducto" Visible="True"   meta:resourcekey="RiskInformationProductCodeColumnNPRODUCTResource"/>
                <dxe:ListBoxColumn FieldName="SDESCRIPT" Caption="Descripción" Visible="True"   meta:resourcekey="RiskInformationProductCodeColumnSDESCRIPTResource"/>
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