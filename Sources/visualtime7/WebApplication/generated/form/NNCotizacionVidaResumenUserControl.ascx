<%@ Control Language="VB" AutoEventWireup="false" CodeFile="NNCotizacionVidaResumenUserControl.ascx.vb" Inherits="NNCotizacionVidaResumenUserControl" %>
<%@ Register Src="NNCotizacionMiVidaValeSecuenciaUserControl.ascx" TagName="NNCotizacionMiVidaValeSecuencia" TagPrefix="NNCotizacionMiVidaValeSecuencia_UC" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">



</script>

<script src="/generated/form/NNCotizacionVidaResumen.js" type="text/javascript"></script>      
<asp:UpdatePanel ID="NNCotizacionVidaResumenUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='NNCotizacionVidaResumenTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:10%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone1" ClientInstanceName="zone1" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone1Resource"
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
                    <dxrp:ASPxRoundPanel ID="zone5" ClientInstanceName="zone5" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone5Resource"
 Width="100%" SkinID="SquareBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Center'>       <dxe:ASPxLabel ID='label0' EncodeHtml='false' ClientInstanceName='label0' runat='server' ClientIDMode='Static' meta:resourcekey="label0Resource"  Text="Resumen de cotización"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="12"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone2" ClientInstanceName="zone2" runat="server" HeaderText="" ToolTip="" Enabled="False" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone2Resource"
 Width="100%" SkinID="SquareBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='EffectiveDateLabel' EncodeHtml='false' ClientInstanceName='EffectiveDateLabel' runat='server' ClientIDMode='Static' meta:resourcekey="EffectiveDateLabelResource"  Text="Fecha de inicio"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='EffectiveDate'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='EffectiveDate' ToolTip="Fecha de Inicio" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="EffectiveDateResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone2" >
</ValidationSettings>
       </dxe:ASPxDateEdit>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='ProductMasterDescriptionLabel' EncodeHtml='false' ClientInstanceName='ProductMasterDescriptionLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ProductMasterDescriptionLabelResource"  Text="Plan/Producto"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='ProductMasterDescription'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='ProductMasterDescription' ClientInstanceName='ProductMasterDescription' ToolTip="Descripción" Size='30' NullText="" ClientVisible='True' MaxLength='30' ClientIDMode='Static' meta:resourcekey="ProductMasterDescriptionResource" Width='270px'  ClientEnabled='False'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone2" >
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
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label1' EncodeHtml='false' ClientInstanceName='label1' runat='server' ClientIDMode='Static' meta:resourcekey="label1Resource"  Text="Información del asegurado"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone3" ClientInstanceName="zone3" runat="server" HeaderText="" ToolTip="" Enabled="False" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone3Resource"
 Width="100%" SkinID="SquareBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone11" ClientInstanceName="zone11" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone11Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:33%;' align='left'>       <dxe:ASPxLabel ID='PrimaryInsuredClientFirstNameLabel' EncodeHtml='false' ClientInstanceName='PrimaryInsuredClientFirstNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="PrimaryInsuredClientFirstNameLabelResource"  Text="Nombre(s)"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='PrimaryInsuredClientFirstName'       ></dxe:ASPxLabel><br />

       <dxe:ASPxLabel ID='PrimaryInsuredClientFirstName' EncodeHtml='false' ClientInstanceName='PrimaryInsuredClientFirstName' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

    </td>

    <td style='width:33%;' align='left'>       <dxe:ASPxLabel ID='PrimaryInsuredClientLastNameLabel' EncodeHtml='false' ClientInstanceName='PrimaryInsuredClientLastNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="PrimaryInsuredClientLastNameLabelResource"  Text="Apellido paterno"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='PrimaryInsuredClientLastName'       ></dxe:ASPxLabel><br />

       <dxe:ASPxLabel ID='PrimaryInsuredClientLastName' EncodeHtml='false' ClientInstanceName='PrimaryInsuredClientLastName' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

    </td>

    <td style='width:34%;' align='left'>       <dxe:ASPxLabel ID='PrimaryInsuredClientLastName2Label' EncodeHtml='false' ClientInstanceName='PrimaryInsuredClientLastName2Label' runat='server' ClientIDMode='Static' meta:resourcekey="PrimaryInsuredClientLastName2LabelResource"  Text="Apellido materno"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='PrimaryInsuredClientLastName2'       ></dxe:ASPxLabel><br />

       <dxe:ASPxLabel ID='PrimaryInsuredClientLastName2' EncodeHtml='false' ClientInstanceName='PrimaryInsuredClientLastName2' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

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
    <td style='width:33%;' align='left'>       <dxe:ASPxLabel ID='BirthDateLabel' EncodeHtml='false' ClientInstanceName='BirthDateLabel' runat='server' ClientIDMode='Static' meta:resourcekey="BirthDateLabelResource"  Text="Fecha de nacimiento"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='BirthDate'       ></dxe:ASPxLabel><br />

       <dxe:ASPxDateEdit runat='server' ID='BirthDate' ToolTip="Fecha de Nacimiento" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="BirthDateResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone11" >
</ValidationSettings>
       </dxe:ASPxDateEdit>
    </td>

    <td style='width:33%;' align='left'>       <dxe:ASPxLabel ID='GenderLabel' EncodeHtml='false' ClientInstanceName='GenderLabel' runat='server' ClientIDMode='Static' meta:resourcekey="GenderLabelResource"  Text="Género"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Gender'       ></dxe:ASPxLabel><br />


<dxe:ASPxComboBox ID='Gender' runat='server' ClientInstanceName='Gender' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Sexo del cliente" ClientVisible='true' ClientEnabled='True' meta:resourcekey="GenderResource"  ValueType='System.String'    TextField='SDESCRIPT' ValueField='SSEXCLIEN'><ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone11" >
</ValidationSettings>
</dxe:ASPxComboBox>
    </td>

    <td style='width:34%;' align='left'>       <dxe:ASPxLabel ID='HeightLabel' EncodeHtml='false' ClientInstanceName='HeightLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HeightLabelResource"  Text="Altura (metros)"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Height'       ></dxe:ASPxLabel><br />

       <dxe:ASPxLabel ID='Height' EncodeHtml='false' ClientInstanceName='Height' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

    </td>

  </tr>
  <tr valign='top'>
    <td style='width:33%;' align='left'>       <dxe:ASPxLabel ID='WeightLabel' EncodeHtml='false' ClientInstanceName='WeightLabel' runat='server' ClientIDMode='Static' meta:resourcekey="WeightLabelResource"  Text="Peso (kilogramos)"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Weight'       ></dxe:ASPxLabel><br />

       <dxe:ASPxLabel ID='Weight' EncodeHtml='false' ClientInstanceName='Weight' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

    </td>

    <td style='width:33%;' align='left'>       <dxe:ASPxLabel ID='SmokerIndicatorLabel' EncodeHtml='false' ClientInstanceName='SmokerIndicatorLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SmokerIndicatorLabelResource"  Text="¿Fumador?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='SmokerIndicator'       ></dxe:ASPxLabel><br />

       <dxe:ASPxRadioButtonList ID='SmokerIndicator' ClientInstanceName='SmokerIndicator' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="Indicador condición de fumador" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="SmokerIndicatorResource"  ValueType='System.String'   >
            <Items>
                <dxe:ListEditItem Value='1' Text='Si' meta:resourcekey="SmokerIndicatorListItemValue1Resource"/>
                <dxe:ListEditItem Value='2' Text='No' meta:resourcekey="SmokerIndicatorListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone11" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone6" ClientInstanceName="zone6" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone6Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="AddressClient" ClientInstanceName="AddressClient" runat="server" HeaderText="" ToolTip="Dirección" Enabled="False" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="AddressClientResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='DireccionMostrarLabel' EncodeHtml='false' ClientInstanceName='DireccionMostrarLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DireccionMostrarLabelResource"  Text="Dirección"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DireccionMostrar'       ></dxe:ASPxLabel><br />

       <dxe:ASPxMemo runat='server' ClientIDMode='Static' ID='DireccionMostrar' ToolTip="notes13" meta:resourcekey="DireccionMostrarResource" Columns='60' Rows='4' Size='0' NullText="" ClientVisible='True'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="AddressClient" >
</ValidationSettings>
       </dxe:ASPxMemo>
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
                    <dxrp:ASPxRoundPanel ID="zone8" ClientInstanceName="zone8" runat="server" HeaderText="" ToolTip="Dirección de correo electrónico" Enabled="False" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone8Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:33%;' align='left'>       <dxe:ASPxLabel ID='eMailLabel' EncodeHtml='false' ClientInstanceName='eMailLabel' runat='server' ClientIDMode='Static' meta:resourcekey="eMailLabelResource"  Text="Correo electrónico"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='eMail'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='eMail' ClientInstanceName='eMail' ToolTip="Dirección de correo electrónico" Size='50' NullText="" ClientVisible='True' MaxLength='50' ClientIDMode='Static' meta:resourcekey="eMailResource" Width='540px'  ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone8" >
     <RequiredField IsRequired='True' ErrorText="El campo es requerido." />
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
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label3' EncodeHtml='false' ClientInstanceName='label3' runat='server' ClientIDMode='Static' meta:resourcekey="label3Resource"  Text="Suma asegurada, coberturas y prima"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label4' EncodeHtml='false' ClientInstanceName='label4' runat='server' ClientIDMode='Static' meta:resourcekey="label4Resource"  Text="Beneficiarios"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="RiskInformation" ClientInstanceName="RiskInformation" runat="server" HeaderText="" ToolTip="Suma asegurada, coberturas y prima" Enabled="False" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="RiskInformationResource"
 Width="100%" SkinID="SquareBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:60%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone9" ClientInstanceName="zone9" runat="server" HeaderText="" ToolTip="Suma asegurada, coberturas y prima" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone9Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:300%;' colspan='3' align='left'>       <dxe:ASPxLabel ID='InsuredAmountLabel' EncodeHtml='false' ClientInstanceName='InsuredAmountLabel' runat='server' ClientIDMode='Static' meta:resourcekey="InsuredAmountLabelResource"  Text="Suma asegurada"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='InsuredAmount'       ></dxe:ASPxLabel><br /><div style='float: left;'>

       <dxe:ASPxTextBox runat='server' ID='InsuredAmount' ClientInstanceName='InsuredAmount' ToolTip="Capital Asegurado" Size='21' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="InsuredAmountResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-999999999999999999..999999999999999999g>.<00..99>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="zone9" >
</ValidationSettings>
       </dxe:ASPxTextBox>
</div>

<div style='float: left;'>


<dxe:ASPxComboBox ID='Currency' runat='server' ClientInstanceName='Currency' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Moneda" ClientVisible='true' ClientEnabled='True' meta:resourcekey="CurrencyResource"  Width='50px'  ValueType='System.Int32'    TextField='SSHORT_DES' ValueField='NCODIGINT'><ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone9" >
</ValidationSettings>
</dxe:ASPxComboBox>
</div>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%;' align='left'>       <dxe:ASPxLabel ID='TotalOriginalAnnualPremiumLabel' EncodeHtml='false' ClientInstanceName='TotalOriginalAnnualPremiumLabel' runat='server' ClientIDMode='Static' meta:resourcekey="TotalOriginalAnnualPremiumLabelResource"  Text="Prima total anual"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='TotalOriginalAnnualPremium'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='TotalOriginalAnnualPremium' ClientInstanceName='TotalOriginalAnnualPremium' ToolTip="Prima Anual" Size='8' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="TotalOriginalAnnualPremiumResource"  Text='0' ClientEnabled='true' ClientVisible='true'  Font-Bold="True"  Font-Size="10"  MaskSettings-Mask=' <-99999..99999g>.<00..99>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="zone9" >
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
    <td style="width:30%">
      &nbsp;
    </td>
    <td style="width:30%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:60%' colspan='2'>
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='CoverageWithCalculatedPremium' EnableRowsCache='False' EnableViewState='True' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='CoverageWithCalculatedPremium' runat='server' Width='100%' KeyFieldName='CoverageCode' Caption="Coberturas" meta:resourcekey="CoverageWithCalculatedPremiumResource"

>
               <SettingsPager Visible="True" PageSize="10"/>
 <SettingsBehavior AllowFocusedRow="True" AllowSort="False"/>
        <Columns>
<dxwgv:GridViewDataTextColumn Name='CoverageCode' FieldName='CoverageCode' Caption="Cobertura" ToolTip="Código de la cobertura" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="CoverageCodeFieldResource"
 Visible='False'
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <-99999..99999g>' />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="CoverageWithCalculatedPremium" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataMemoColumn Name='Description1' FieldName='Description' Caption="Plan/Producto" ToolTip="Descripción" GroupIndex="-1" VisibleIndex="1" meta:resourcekey="Description1FieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesMemoEdit NullText="">
           </PropertiesMemoEdit>
</dxwgv:GridViewDataMemoColumn>
<dxwgv:GridViewDataTextColumn Name='CoverageWithCalculatedPremiumInsuredAmount' FieldName='InsuredAmount' Caption="Suma asegurada" ToolTip="Capital asegurado" GroupIndex="-1" VisibleIndex="2" meta:resourcekey="CoverageWithCalculatedPremiumInsuredAmountFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,###,###,###,###,##0.00">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <-999999999999999999..999999999999999999g>.<00..99>' />
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='CoverageWithCalculatedPremiumAnnualPremium' FieldName='AnnualPremium' Caption="Prima anual" ToolTip="Monto de prima" GroupIndex="-1" VisibleIndex="3" meta:resourcekey="CoverageWithCalculatedPremiumAnnualPremiumFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="3" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,###,###,###,###,##0.00">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <-999999999999999999..999999999999999999g>.<00..99>' />
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </td>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone10" ClientInstanceName="zone10" runat="server" HeaderText="" ToolTip="Información de los beneficiarios" Enabled="False" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone10Resource"
 Width="100%" SkinID="SquareBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%;' colspan='2' align='left'>

       <dxe:ASPxRadioButtonList ID='BeneficiaryType' ClientInstanceName='BeneficiaryType' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="radiobuttonlist4" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="BeneficiaryTypeResource"  ValueType='System.Int32'   >
            <Items>
                <dxe:ListEditItem Value='1' Text='Herederos legales' meta:resourcekey="BeneficiaryTypeListItemValue1Resource"/>
                <dxe:ListEditItem Value='3' Text='Asignados por el asegurado' meta:resourcekey="BeneficiaryTypeListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone10" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='Beneficiary' EnableRowsCache='False' EnableViewState='True' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='Beneficiary' runat='server' Width='100%' KeyFieldName='ClientID' Caption="" meta:resourcekey="BeneficiaryResource"

>
               <SettingsPager Visible="True" PageSize="10"/>
 <SettingsBehavior AllowFocusedRow="True" AllowSort="False"/>
        <Columns>
<dxwgv:GridViewDataTextColumn FieldName='ClientID' Visible='false'>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='CompleteClientName' FieldName='Client.CompleteClientName' Caption="Beneficiario" ToolTip="Nombre Completo del Cliente" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="CompleteClientNameFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='63' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='PercentageShare' FieldName='PercentageShare' Caption="%Participación" ToolTip="Porcentaje de Participación" GroupIndex="-1" VisibleIndex="1" meta:resourcekey="PercentageShareFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0.00">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <-99999..99999g>.<00..99>' />
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='Relationship' FieldName='Relationship' Caption="Relación" ToolTip="Nexo"  GroupIndex="-1" VisibleIndex="2" meta:resourcekey="RelationshipFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
<PropertiesComboBox EnableCallbackMode='false' IncrementalFilteringMode='StartsWith'
 TextField='SDESCRIPT' ValueField='NRELATION'>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Beneficiary" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
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
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label5' EncodeHtml='false' ClientInstanceName='label5' runat='server' ClientIDMode='Static' meta:resourcekey="label5Resource"  Text="Información de salud del asegurado"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone4" ClientInstanceName="zone4" runat="server" HeaderText="" ToolTip="Información de salud del asegurado" Enabled="False" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone4Resource"
 Width="100%" SkinID="SquareBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:33%;' align='left'>       <dxe:ASPxLabel ID='radiobuttonlist1Label' EncodeHtml='false' ClientInstanceName='radiobuttonlist1Label' runat='server' ClientIDMode='Static' meta:resourcekey="radiobuttonlist1LabelResource"  Text="¿Sufre o ha sufrido de diábetes?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='radiobuttonlist1'       ></dxe:ASPxLabel><br />

       <dxe:ASPxRadioButtonList ID='radiobuttonlist1' ClientInstanceName='radiobuttonlist1' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="radiobuttonlist1Resource"  ValueType='System.Boolean'   >
            <Items>
                <dxe:ListEditItem Value='true' Text='Si' meta:resourcekey="radiobuttonlist1ListItemValue1Resource"/>
                <dxe:ListEditItem Value='false' Text='No' meta:resourcekey="radiobuttonlist1ListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone4" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

    <td style='width:33%;' align='left'>       <dxe:ASPxLabel ID='radiobuttonlist2Label' EncodeHtml='false' ClientInstanceName='radiobuttonlist2Label' runat='server' ClientIDMode='Static' meta:resourcekey="radiobuttonlist2LabelResource"  Text="¿Sufre o ha sufrido de alguna enfermedad coronaria?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='radiobuttonlist2'       ></dxe:ASPxLabel><br />

       <dxe:ASPxRadioButtonList ID='radiobuttonlist2' ClientInstanceName='radiobuttonlist2' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="radiobuttonlist2Resource"  ValueType='System.Boolean'   >
            <Items>
                <dxe:ListEditItem Value='true' Text='Si' meta:resourcekey="radiobuttonlist2ListItemValue1Resource"/>
                <dxe:ListEditItem Value='false' Text='No' meta:resourcekey="radiobuttonlist2ListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone4" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

    <td style='width:34%;' align='left'>       <dxe:ASPxLabel ID='Radiobutton3Label' EncodeHtml='false' ClientInstanceName='Radiobutton3Label' runat='server' ClientIDMode='Static' meta:resourcekey="Radiobutton3LabelResource"  Text="¿Tiene o ha tenido cáncer, tumores o quistes?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Radiobutton3'       ></dxe:ASPxLabel><br />

       <dxe:ASPxRadioButtonList ID='Radiobutton3' ClientInstanceName='Radiobutton3' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="Monto de prima" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="Radiobutton3Resource"  ValueType='System.Boolean'   >
            <Items>
                <dxe:ListEditItem Value='true' Text='Si' meta:resourcekey="Radiobutton3ListItemValue1Resource"/>
                <dxe:ListEditItem Value='false' Text='No' meta:resourcekey="Radiobutton3ListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone4" >
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