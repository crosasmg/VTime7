<%@ Control Language="VB" AutoEventWireup="false" CodeFile="NNCotizacionMiVidaValeViewUserControl.ascx.vb" Inherits="NNCotizacionMiVidaValeViewUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">
    var msgControlsDependencyResource='<asp:Localize runat="server" Text="Espere..." meta:resourcekey="ControlsDependencyResource"></asp:Localize>';
    var msgbutton0FinalizaInformationMessageResource='<asp:Localize runat="server" Text="Redirigiendo al panel de suscripción. Por favor espere..." meta:resourcekey="button0FinalizaInformationMessageResource"></asp:Localize>';
    var titlebutton0FinalizaInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton0FinalizaInformationMessageResource"></asp:Localize>';



</script>

<script src="/generated/form/NNCotizacionMiVidaValeView.js" type="text/javascript"></script>      
<asp:UpdatePanel ID="NNCotizacionMiVidaValeViewUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='NNCotizacionMiVidaValeViewTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone0" ClientInstanceName="zone0" runat="server" HeaderText="General information of the quotation" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone0Resource"
 Width="100%" SkinID="RoundedBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='8'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:12.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='RiskInformationEffectiveDateLabel' EncodeHtml='false' ClientInstanceName='RiskInformationEffectiveDateLabel' runat='server' ClientIDMode='Static' meta:resourcekey="RiskInformationEffectiveDateLabelResource"  Text="Fecha de efecto"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='RiskInformationEffectiveDate'       ></dxe:ASPxLabel></td>    <td style='width:12.5%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='RiskInformationEffectiveDate' ToolTip="Fecha de efecto (inicio de vigencia) de la póliza o certificado." ClientIDMode='Static' ClientVisible='True' meta:resourcekey="RiskInformationEffectiveDateResource"  Width='100px' ClientEnabled='False'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
       </dxe:ASPxDateEdit>
    </td>

<td style='width:12.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='RiskInformationProductCodeLabel' EncodeHtml='false' ClientInstanceName='RiskInformationProductCodeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="RiskInformationProductCodeLabelResource"  Text="Plan"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='RiskInformationProductCode'       ></dxe:ASPxLabel></td>    <td style='width:12.5%;' align='left'>


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

<td style='width:12.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='uwcaseidLabel' EncodeHtml='false' ClientInstanceName='uwcaseidLabel' runat='server' ClientIDMode='Static' meta:resourcekey="uwcaseidLabelResource"  Text="Caso"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='uwcaseid'       ></dxe:ASPxLabel></td>    <td style='width:12.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='uwcaseid' ClientInstanceName='uwcaseid' ToolTip="text0" Size='10' NullText="" ClientVisible='True' MaxLength='10' ClientIDMode='Static' meta:resourcekey="uwcaseidResource" Width='90px'  ClientEnabled='False'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:12.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LineOfBusinessLabel' EncodeHtml='false' ClientInstanceName='LineOfBusinessLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LineOfBusinessLabelResource"  Text="Ramo"  ClientEnabled='true'  ClientVisible='false'  AssociatedControlID='LineOfBusiness'       ></dxe:ASPxLabel></td>    <td style='width:12.5%;' align='left'>


<dxe:ASPxComboBox ID='LineOfBusiness' runat='server' ClientInstanceName='LineOfBusiness' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Código del ramo comercial." ClientVisible='false' ClientEnabled='True' meta:resourcekey="LineOfBusinessResource"  ValueType='System.Int32'    TextField='SDESCRIPT' ValueField='NBRANCH'>         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone0" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
<ClientSideEvents  SelectedIndexChanged="LineOfBusinessSelectedIndexChanged" />
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
    <td style='width:100%' colspan='2'>
    <dxtc:ASPxPageControl ID="tabcontainer0" runat="server" ClientIDMode='Static' ClientVisible="True" ActiveTabIndex="0" EnableHierarchyRecreation="True" Width="100%" TabAlign="Left" TabPosition="Top"  >
          <TabPages>

              <dxtc:TabPage Name="Relationship" Text="Información básica" ClientVisible="True" ClientEnabled="True" ToolTip="" meta:resourcekey="RelationshipResource">
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
                    <dxrp:ASPxRoundPanel ID="zone12" ClientInstanceName="zone12" runat="server" HeaderText="zone" ToolTip="zone" Enabled="False" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone12Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label26' EncodeHtml='false' ClientInstanceName='label26' runat='server' ClientIDMode='Static' meta:resourcekey="label26Resource"  Text="Información del asegurado"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="10"  ForeColor="#000000"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



<td style='width:35%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label0' EncodeHtml='false' ClientInstanceName='label0' runat='server' ClientIDMode='Static' meta:resourcekey="label0Resource"  Text="Información adicional del asegurado"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="10"  ForeColor="#000000"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



<td style='width:40%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label27' EncodeHtml='false' ClientInstanceName='label27' runat='server' ClientIDMode='Static' meta:resourcekey="label27Resource"  Text="Suma asegurada por la que desea estar asegurado"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="10"  ForeColor="#000000"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style='width:25%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone6" ClientInstanceName="zone6" runat="server" HeaderText="zone" ToolTip="zone" Enabled="False" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone6Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='ClientIDLabel' EncodeHtml='false' ClientInstanceName='ClientIDLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ClientIDLabelResource"  Text="Identificación"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='ClientID'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='ClientID' ClientInstanceName='ClientID' ToolTip="Código de identificación del cliente" Size='14' NullText="" ClientVisible='True' MaxLength='14' ClientIDMode='Static' meta:resourcekey="ClientIDResource" Width='126px'  ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone6" >
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
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='GenderLabel' EncodeHtml='false' ClientInstanceName='GenderLabel' runat='server' ClientIDMode='Static' meta:resourcekey="GenderLabelResource"  Text="Género"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Gender'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='Gender' ClientInstanceName='Gender' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Vertical' ClientIDMode='Static' ToolTip="Género del cliente." ClientVisible='true' ClientEnabled='True'  meta:resourcekey="GenderResource"  ValueType='System.String'   >
            <Items>
                <dxe:ListEditItem Value='1' Text='Femenino' meta:resourcekey="GenderListItemValue1Resource"/>
                <dxe:ListEditItem Value='2' Text='Masculino' meta:resourcekey="GenderListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="8px" PaddingRight="0px" PaddingTop="0px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone6" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
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
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='SmokerIndicatorLabel' EncodeHtml='false' ClientInstanceName='SmokerIndicatorLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SmokerIndicatorLabelResource"  Text="¿Fumador?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='SmokerIndicator'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='SmokerIndicator' ClientInstanceName='SmokerIndicator' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="Indicador de cliente fumador." ClientVisible='true' ClientEnabled='True'  meta:resourcekey="SmokerIndicatorResource"  ValueType='System.String'   >
            <Items>
                <dxe:ListEditItem Value='1' Text='Si' meta:resourcekey="SmokerIndicatorListItemValue1Resource"/>
                <dxe:ListEditItem Value='2' Text='No' meta:resourcekey="SmokerIndicatorListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="8px" PaddingRight="0px" PaddingTop="0px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone6" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
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
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='BirthDateLabel' EncodeHtml='false' ClientInstanceName='BirthDateLabel' runat='server' ClientIDMode='Static' meta:resourcekey="BirthDateLabelResource"  Text="Fecha de nacimiento"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='BirthDate'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='BirthDate' ToolTip="Fecha de nacimiento del cliente." ClientIDMode='Static' ClientVisible='True' meta:resourcekey="BirthDateResource"  Width='100px' ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone6" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxDateEdit>
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
                    <dxrp:ASPxRoundPanel ID="zoneAS2" ClientInstanceName="zoneAS2" runat="server" HeaderText="zone" ToolTip="Información adicional del asegurado" Enabled="False" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zoneAS2Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HeightLabel' EncodeHtml='false' ClientInstanceName='HeightLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HeightLabelResource"  Text="Altura"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Height'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

        <table>
            <tr>
                <td>
       <dxe:ASPxTextBox runat='server' ID='Height' ClientInstanceName='Height' ToolTip="Altura del cliente." Size='4' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="HeightResource"  Width='60px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..9g>.<00..99>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="zoneAS2" >
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
       <dxe:ASPxTextBox runat='server' ID='Weight' ClientInstanceName='Weight' ToolTip="Peso del cliente." Size='6' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="WeightResource"  Width='60px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999g>.<00..99>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="zoneAS2" >
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
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='FirstNameLabel' EncodeHtml='false' ClientInstanceName='FirstNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="FirstNameLabelResource"  Text="Nombre(s)"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='FirstName'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='FirstName' ClientInstanceName='FirstName' ToolTip="Nombre(s) del cliente." Size='19' NullText="" ClientVisible='True' MaxLength='19' ClientIDMode='Static' meta:resourcekey="FirstNameResource" Width='200px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zoneAS2" >
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
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LastNameLabel' EncodeHtml='false' ClientInstanceName='LastNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LastNameLabelResource"  Text="Apellido paterno"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='LastName'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='LastName' ClientInstanceName='LastName' ToolTip="Apellido paterno del cliente." Size='19' NullText="" ClientVisible='True' MaxLength='19' ClientIDMode='Static' meta:resourcekey="LastNameResource" Width='200px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zoneAS2" >
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
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LastName2Label' EncodeHtml='false' ClientInstanceName='LastName2Label' runat='server' ClientIDMode='Static' meta:resourcekey="LastName2LabelResource"  Text="Apellido materno"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='LastName2'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='LastName2' ClientInstanceName='LastName2' ToolTip="Apellido materno del cliente." Size='19' NullText="" ClientVisible='True' MaxLength='19' ClientIDMode='Static' meta:resourcekey="LastName2Resource" Width='200px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zoneAS2" >
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
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='eMailLabel' EncodeHtml='false' ClientInstanceName='eMailLabel' runat='server' ClientIDMode='Static' meta:resourcekey="eMailLabelResource"  Text="Correo electrónico"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='eMail'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='eMail' ClientInstanceName='eMail' ToolTip="eMail" Size='50' NullText="usuario@proveedor.com" ClientVisible='True' MaxLength='50' ClientIDMode='Static' meta:resourcekey="eMailResource" Width='200px'  ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zoneAS2" >
     <RequiredField IsRequired='True' ErrorText="El campo es requerido." />
     <RegularExpression ValidationExpression="^\s*[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]\s*$" ErrorText="Correo electrónico inválido" />
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
    <td style='width:40%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone5" ClientInstanceName="zone5" runat="server" HeaderText="Include the sum insured you want to be covered with" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone5Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
    <td style="width:0%">
      &nbsp;
    </td>
    <td style="width:0%">
      &nbsp;
    </td>
    <td style="width:0%">
      &nbsp;
    </td>
    <td style="width:0%">
      &nbsp;
    </td>
    <td style="width:0%">
      &nbsp;
    </td>
    <td style="width:0%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='InsuredAmountLabel' EncodeHtml='false' ClientInstanceName='InsuredAmountLabel' runat='server' ClientIDMode='Static' meta:resourcekey="InsuredAmountLabelResource"  Text="Capital Asegurado"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='InsuredAmount'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='InsuredAmount' ClientInstanceName='InsuredAmount' ToolTip="Capital Asegurado" Size='21' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="InsuredAmountResource"  Width='130px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999999999g>.<00..99>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="RiskInformation" >
        <RequiredField IsRequired='true' ErrorText="Información requerida" />
        <RegularExpression ValidationExpression="^[-0-9]\d*([,\.]\d+)?$" ErrorText="Información requerida" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:25%;' colspan='2' align='left'>


<dxe:ASPxComboBox ID='Currency' runat='server' ClientInstanceName='Currency' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Moneda en que está expresada la suma asegurada" ClientVisible='true' ClientEnabled='True' meta:resourcekey="CurrencyResource"  Width='50px'  ValueType='System.Int32'    TextField='SSHORT_DES' ValueField='NCODIGINT'><ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="zone5" >
</ValidationSettings>
</dxe:ASPxComboBox>
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
          </dxw:ContentControl>
       </ContentCollection>
           </dxtc:TabPage>
              <dxtc:TabPage Name="tab6" Text="Enfermedades" ClientVisible="True" ClientEnabled="True" ToolTip="" meta:resourcekey="tab6Resource">
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
                    <dxrp:ASPxRoundPanel ID="zone17Enfermedad" ClientInstanceName="zone17Enfermedad" runat="server" HeaderText="zone" ToolTip="zone" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone17EnfermedadResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='ImpairmentExclusionForAmendment' EnableRowsCache='False' EnableViewState='True' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='ImpairmentExclusionForAmendment' runat='server' Width='100%' KeyFieldName='Consecutive' Caption="Exclusión enfermedades" meta:resourcekey="ImpairmentExclusionForAmendmentResource"

>
               <SettingsPager Visible="True" PageSize="10"/>
 <SettingsBehavior AllowFocusedRow="True" AllowSort="False"/>
<Settings ShowGroupPanel='True' />
        <Columns>
<dxwgv:GridViewDataTextColumn FieldName='Consecutive' Visible='false'>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Impairment' FieldName='Impairment' Caption="Enfermedad" ToolTip="Enfermedad" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="ImpairmentFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="##,###,###,##0">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <-9999999999..9999999999g>' />
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Description' FieldName='Description' Caption="Descripción" ToolTip="Descripción" GroupIndex="-1" VisibleIndex="1" meta:resourcekey="DescriptionFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='100' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='ClientIDEF' FieldName='ClientID' Caption="Cliente" ToolTip="Código de Cliente"  GroupIndex="-1" VisibleIndex="2" meta:resourcekey="ClientIDEFFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
<PropertiesComboBox EnableCallbackMode='false' IncrementalFilteringMode='StartsWith'
 TextField='COMPLETECLIENTNAME' ValueField='CLIENTID'>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='ExclusionCauseEF' FieldName='ExclusionCause' Caption="Causa de exclusión" ToolTip="Causa de exclusión"  GroupIndex="-1" VisibleIndex="3" meta:resourcekey="ExclusionCauseEFFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="3" Visible="True" />
<PropertiesComboBox EnableCallbackMode='false' IncrementalFilteringMode='StartsWith'
 TextField='SDESCRIPT' ValueField='NEXC_CODE'>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataDateColumn Name='ExclusionDate' FieldName='ExclusionDate' Caption="Fecha de exclusión" ToolTip="Fecha de exclusión" GroupIndex="-1" VisibleIndex="4" meta:resourcekey="ExclusionDateFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="4" Visible="True" />
           <PropertiesDateEdit>
           </PropertiesDateEdit>
</dxwgv:GridViewDataDateColumn>
<dxwgv:GridViewDataDateColumn Name='EndingDate' FieldName='EndingDate' Caption="Término de exclusión" ToolTip="Término de exclusión" GroupIndex="-1" VisibleIndex="5" meta:resourcekey="EndingDateFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="5" Visible="True" />
           <PropertiesDateEdit>
           </PropertiesDateEdit>
</dxwgv:GridViewDataDateColumn>
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
          </dxw:ContentControl>
       </ContentCollection>
           </dxtc:TabPage>
              <dxtc:TabPage Name="tab8" Text="Cláusulas" ClientVisible="True" ClientEnabled="True" ToolTip="" meta:resourcekey="tab8Resource">
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
                    <dxrp:ASPxRoundPanel ID="zone4" ClientInstanceName="zone4" runat="server" HeaderText="zona" ToolTip="zona" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone4Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='ClauseForAmendment' EnableRowsCache='False' EnableViewState='True' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='ClauseForAmendment' runat='server' Width='100%' KeyFieldName='Consecutive' Caption="Cláusulas" meta:resourcekey="ClauseForAmendmentResource"

>
               <SettingsPager Visible="True" PageSize="10"/>
 <SettingsBehavior AllowFocusedRow="True" AllowSort="False"/>
        <Columns>
<dxwgv:GridViewDataTextColumn FieldName='Consecutive' Visible='false'>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='ClauseCode' FieldName='ClauseCode' Caption="Cláusula" ToolTip="Código de la Cláusula" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="ClauseCodeFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <-99999..99999g>' />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="ClauseForAmendment" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Descriptionc' FieldName='Description' Caption="Descripción" ToolTip="Descripción" GroupIndex="-1" VisibleIndex="1" meta:resourcekey="DescriptioncFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='100' >
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
          </dxw:ContentControl>
       </ContentCollection>
           </dxtc:TabPage>
              <dxtc:TabPage Name="tab0" Text="Recargos y descuentos por asegurado" ClientVisible="True" ClientEnabled="True" ToolTip="" meta:resourcekey="tab0Resource">
  <TabStyle  Font-Bold="True"  Font-Size="12"  />
                 <ContentCollection>
                    <dxw:ContentControl runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='CompleteClientNameLabel' EncodeHtml='false' ClientInstanceName='CompleteClientNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CompleteClientNameLabelResource"  Text="Asegurado"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='CompleteClientName'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxLabel ID='CompleteClientName' EncodeHtml='false' ClientInstanceName='CompleteClientName' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

    </td>

  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone2" ClientInstanceName="zone2" runat="server" HeaderText="zona" ToolTip="zona" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone2Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='ExtraPremiumDiscountForAmendment' EnableRowsCache='False' EnableViewState='True' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='ExtraPremiumDiscountForAmendment' runat='server' Width='100%' KeyFieldName='Consecutive' Caption="Recargos y descuentos por asegurado" meta:resourcekey="ExtraPremiumDiscountForAmendmentResource"

>
               <SettingsPager Visible="True" PageSize="10"/>
               <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AllowSort="True"/>
        <Columns>
<dxwgv:GridViewDataTextColumn FieldName='Consecutive' Visible='false'>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Descriptionra' FieldName='Description' Caption="Descripción" ToolTip="Descripción" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="DescriptionraFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='100' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Amountra' FieldName='Amount' Caption="Monto fijo" ToolTip="Monto fijo" GroupIndex="-1" VisibleIndex="1" meta:resourcekey="AmountraFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,###,###,###,###,##0.00">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <-999999999999999999..999999999999999999g>.<00..99>' />
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Ratera' FieldName='Rate' Caption="Porcentaje" ToolTip="Tasa" GroupIndex="-1" VisibleIndex="2" meta:resourcekey="RateraFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,###,##0.00">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <-999999999..999999999g>.<00..99>' />
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='ExtraPremiumPermanentOrTemporaryra' FieldName='ExtraPremiumPermanentOrTemporary' Caption="Periodo" ToolTip="Permanente/temporal"  GroupIndex="-1" VisibleIndex="3" meta:resourcekey="ExtraPremiumPermanentOrTemporaryraFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="3" Visible="True" />
<PropertiesComboBox>
            <Items>
                <dxe:ListEditItem Value='1' Text='Permanente' meta:resourcekey="ExtraPremiumPermanentOrTemporaryraListItemValue1Resource"/>
                <dxe:ListEditItem Value='2' Text='Temporal' meta:resourcekey="ExtraPremiumPermanentOrTemporaryraListItemValue2Resource"/>
            </Items>

</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataDateColumn Name='ExtraPremiumOrDiscountEffectiveDatera' FieldName='ExtraPremiumOrDiscountEffectiveDate' Caption="Fecha Inicio Efecto" ToolTip="Fecha Inicio Efecto" GroupIndex="-1" VisibleIndex="4" meta:resourcekey="ExtraPremiumOrDiscountEffectiveDateraFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="4" Visible="True" />
           <PropertiesDateEdit>
           </PropertiesDateEdit>
</dxwgv:GridViewDataDateColumn>
<dxwgv:GridViewDataDateColumn Name='EndingDateOfExtraPremiumOrDiscountra' FieldName='EndingDateOfExtraPremiumOrDiscount' Caption="Fecha Fin Efecto" ToolTip="Fecha Fin Efecto" GroupIndex="-1" VisibleIndex="5" meta:resourcekey="EndingDateOfExtraPremiumOrDiscountraFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="5" Visible="True" />
           <PropertiesDateEdit>
           </PropertiesDateEdit>
</dxwgv:GridViewDataDateColumn>
                            <dxwgv:GridViewCommandColumn Caption=' ' meta:resourcekey='ExtraPremiumDiscountForAmendmentCommandColumsResource'>
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
          </dxw:ContentControl>
       </ContentCollection>
           </dxtc:TabPage>
              <dxtc:TabPage Name="tab0Prima" Text="Prima" ClientVisible="True" ClientEnabled="True" ToolTip="" meta:resourcekey="tab0PrimaResource">
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
                    <dxrp:ASPxRoundPanel ID="zone1" ClientInstanceName="zone1" runat="server" HeaderText="zone" ToolTip="zone" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone1Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zoneCoberturas" ClientInstanceName="zoneCoberturas" runat="server" HeaderText="Basic health questionnaire" ToolTip="Cuestionario básico de salud" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zoneCoberturasResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='CoverageForAmendment' EnableRowsCache='False' EnableViewState='True' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='CoverageForAmendment' runat='server' Width='100%' KeyFieldName='Consecutive' Caption="Coberturas" meta:resourcekey="CoverageForAmendmentResource"

>
               <SettingsPager Visible="True" PageSize="10"/>
 <SettingsBehavior AllowFocusedRow="True" AllowSort="False"/>
        <Columns>
<dxwgv:GridViewDataTextColumn FieldName='Consecutive' Visible='false'>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='DescriptionOfModuleg' FieldName='DescriptionOfModule' Caption="Módulo" ToolTip="Descripción del módulo" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="DescriptionOfModulegFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='100' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='DescriptionOfCoverageg' FieldName='DescriptionOfCoverage' Caption="Cobertura" ToolTip="Descripción de la cobertura" GroupIndex="-1" VisibleIndex="1" meta:resourcekey="DescriptionOfCoveragegFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='100' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='InsuredAmountg' FieldName='InsuredAmount' Caption="Suma asegurada" ToolTip="Capital Asegurado" GroupIndex="-1" VisibleIndex="2" meta:resourcekey="InsuredAmountgFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,###,###,###,###,##0.00">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <-999999999999999999..999999999999999999g>.<00..99>' />
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='AnnualPremiumg' FieldName='AnnualPremium' Caption="Prima Anual" ToolTip="Prima Anual" GroupIndex="-1" VisibleIndex="3" meta:resourcekey="AnnualPremiumgFieldResource"
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
  <tr valign='top'>
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zoneRecargos" ClientInstanceName="zoneRecargos" runat="server" HeaderText="Beneficiaries" ToolTip="Beneficiarios" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zoneRecargosResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:20%' colspan='2'>
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='DiscountSurchargeAmendment' EnableRowsCache='False' EnableViewState='True' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='DiscountSurchargeAmendment' runat='server' Width='100%' KeyFieldName='Description' Caption="Recargos y Descuentos" meta:resourcekey="DiscountSurchargeAmendmentResource"

>
               <SettingsPager Visible="True" PageSize="10"/>
               <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AllowSort="True"/>
        <Columns>
<dxwgv:GridViewDataCheckColumn Name='AcceptedSurcharge' FieldName='AcceptedSurcharge' Caption="Recargo Aceptado" ToolTip="Recargo Aceptado" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="AcceptedSurchargeFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
<PropertiesCheckEdit>
</PropertiesCheckEdit>
</dxwgv:GridViewDataCheckColumn>
<dxwgv:GridViewDataTextColumn Name='DescriptionDiscount' FieldName='Description' Caption="Descripción" ToolTip="Descripción" GroupIndex="-1" VisibleIndex="1" meta:resourcekey="DescriptionDiscountFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='30' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Amount' FieldName='Amount' Caption="Monto" ToolTip="Monto" GroupIndex="-1" VisibleIndex="2" meta:resourcekey="AmountFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,###,###,###,###,##0.00">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <-999999999999999999..999999999999999999g>.<00..99>' />
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Percentage' FieldName='Percentage' Caption="Porcentaje" ToolTip="Porcentaje" GroupIndex="-1" VisibleIndex="3" meta:resourcekey="PercentageFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="3" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="#,###,###,###,###,###,##0.00">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <-999999999999999999..999999999999999999g>.<00..99>' />
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn Caption=' ' meta:resourcekey='DiscountSurchargeAmendmentCommandColumsResource'>
                                <CancelButton Visible='True' Text='Cancelar' />
                                <UpdateButton Visible='True' Text='Actualizar' />
                            </dxwgv:GridViewCommandColumn>
            </Columns>
        </dxwgv:ASPxGridView>
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
          </dxw:ContentControl>
       </ContentCollection>
           </dxtc:TabPage>
          </TabPages>
      </dxtc:ASPxPageControl>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone0FinConsulta" ClientInstanceName="zone0FinConsulta" runat="server" HeaderText="zone" ToolTip="zone" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone0FinConsultaResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%'  colspan='2' align='Center'>

       <dxe:ASPxButton ID='button0Finaliza' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Finalizar la consulta y regresar al panel de suscripción" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button0FinalizaResource" Text="Finalizar consulta" ImagePosition='Left' Image-Url="/images/Library/16x16_ASPNetIcons/last_16x16.gif"  OnClick='button0Finaliza_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button0FinalizaClick" />
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