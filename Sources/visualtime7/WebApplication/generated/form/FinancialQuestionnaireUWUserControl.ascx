<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FinancialQuestionnaireUWUserControl.ascx.vb" Inherits="FinancialQuestionnaireUWUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">
    var msgsubmitInformationMessageResource='<asp:Localize runat="server" Text="Procesando información.... Por favor espere." meta:resourcekey="submitInformationMessageResource"></asp:Localize>';
    var titlesubmitInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlesubmitInformationMessageResource"></asp:Localize>';



</script>

<script src="/generated/form/FinancialQuestionnaireUW.js" type="text/javascript"></script>      
<asp:UpdatePanel ID="FinancialQuestionnaireUWUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='FinancialQuestionnaireUWTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="part0" ClientInstanceName="part0" runat="server" HeaderText="" ToolTip="" Enabled="False" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="part0Resource"
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

       <dxe:ASPxTextBox runat='server' ID='ClientName' ClientInstanceName='ClientName' ToolTip="Nombre del solicitante." Size='30' NullText="" ClientVisible='True' MaxLength='30' ClientIDMode='Static' meta:resourcekey="ClientNameResource" Width='270px'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part0" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='uwcaseidLabel' EncodeHtml='false' ClientInstanceName='uwcaseidLabel' runat='server' ClientIDMode='Static' meta:resourcekey="uwcaseidLabelResource"  Text="Solicitud"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='uwcaseid'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='uwcaseid' ClientInstanceName='uwcaseid' ToolTip="" Size='5' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="uwcaseidResource"  Width='54px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..99999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part0" >
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
                    <dxrp:ASPxRoundPanel ID="part1" ClientInstanceName="part1" runat="server" HeaderText="El siguiente cuestionario debe ser completado por el solicitante del seguro. Por favor conteste todas las preguntas." ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="part1Resource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label0' EncodeHtml='false' ClientInstanceName='label0' runat='server' ClientIDMode='Static' meta:resourcekey="label0Resource"  Text="INGRESOS"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="10"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="part2" ClientInstanceName="part2" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="part2Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='Year1Label' EncodeHtml='false' ClientInstanceName='Year1Label' runat='server' ClientIDMode='Static' meta:resourcekey="Year1LabelResource"  Text="Año"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='Year1'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='Year1' ClientInstanceName='Year1' ToolTip="Primer año en el cálculo del patrimonio neto" Size='4' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="Year1Resource"  Width='45px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <0..9999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:33%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='Year2' ClientInstanceName='Year2' ToolTip="Segundo año en el cálculo del patrimonio neto" Size='4' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="Year2Resource"  Width='45px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <0..9999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:34%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='Year3' ClientInstanceName='Year3' ToolTip="Tercer año en el cálculo del patrimonio neto" Size='4' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="Year3Resource"  Width='45px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <0..9999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
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
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='SalaryYear1Label' EncodeHtml='false' ClientInstanceName='SalaryYear1Label' runat='server' ClientIDMode='Static' meta:resourcekey="SalaryYear1LabelResource"  Text="Salario"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='SalaryYear1'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='SalaryYear1' ClientInstanceName='SalaryYear1' ToolTip="Salario recibido para el primer año" Size='10' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="SalaryYear1Resource"  Width='117px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..9999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:33%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='SalaryYear2' ClientInstanceName='SalaryYear2' ToolTip="Salario recibido para el segundo año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="SalaryYear2Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:34%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='SalaryYear3' ClientInstanceName='SalaryYear3' ToolTip="Salario recibido para el tercer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="SalaryYear3Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='BusinesReported1Label' EncodeHtml='false' ClientInstanceName='BusinesReported1Label' runat='server' ClientIDMode='Static' meta:resourcekey="BusinesReported1LabelResource"  Text="Beneficios de la empresas y beneficios personales"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='BusinesReported1'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='BusinesReported1' ClientInstanceName='BusinesReported1' ToolTip="Indica los beneficios obtenidos el primer año en empresas y personales" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="BusinesReported1Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:33%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='BusinesReported2' ClientInstanceName='BusinesReported2' ToolTip="Indica los beneficios obtenidos el segundo año en empresas y personales" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="BusinesReported2Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:34%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='BusinesReported3' ClientInstanceName='BusinesReported3' ToolTip="Indica los beneficios obtenidos el segundo año en empresas y personales" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="BusinesReported3Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='BonusYear1Label' EncodeHtml='false' ClientInstanceName='BonusYear1Label' runat='server' ClientIDMode='Static' meta:resourcekey="BonusYear1LabelResource"  Text="Beneficios"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='BonusYear1'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='BonusYear1' ClientInstanceName='BonusYear1' ToolTip="Ingresos de bonificación primer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="BonusYear1Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:33%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='BonusYear2' ClientInstanceName='BonusYear2' ToolTip="Ingresos de bonificación segundo año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="BonusYear2Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:34%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='BonusYear3' ClientInstanceName='BonusYear3' ToolTip="Ingresos de bonificación tercer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="BonusYear3Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='CommissionYear1Label' EncodeHtml='false' ClientInstanceName='CommissionYear1Label' runat='server' ClientIDMode='Static' meta:resourcekey="CommissionYear1LabelResource"  Text="Comisión"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='CommissionYear1'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='CommissionYear1' ClientInstanceName='CommissionYear1' ToolTip="Ingresos por comisión del primer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="CommissionYear1Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:33%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='CommissionYear2' ClientInstanceName='CommissionYear2' ToolTip="Ingresos por comisión del segundo año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="CommissionYear2Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:34%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='CommissionYear3' ClientInstanceName='CommissionYear3' ToolTip="Ingresos por comisión del tercer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="CommissionYear3Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='PensionProfitSharingYear1Label' EncodeHtml='false' ClientInstanceName='PensionProfitSharingYear1Label' runat='server' ClientIDMode='Static' meta:resourcekey="PensionProfitSharingYear1LabelResource"  Text="Pension/Participación en beneficios"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='PensionProfitSharingYear1'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='PensionProfitSharingYear1' ClientInstanceName='PensionProfitSharingYear1' ToolTip="Pension/Participación en beneficios del primer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="PensionProfitSharingYear1Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:33%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='PensionProfitSharingYear2' ClientInstanceName='PensionProfitSharingYear2' ToolTip="Pension/Participación en beneficios del segundo año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="PensionProfitSharingYear2Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:34%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='PensionProfitSharingYear3' ClientInstanceName='PensionProfitSharingYear3' ToolTip="Pension/Participación en beneficios del tercer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="PensionProfitSharingYear3Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='OtherIncomeYear1Label' EncodeHtml='false' ClientInstanceName='OtherIncomeYear1Label' runat='server' ClientIDMode='Static' meta:resourcekey="OtherIncomeYear1LabelResource"  Text="Otros (Especifique)"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='OtherIncomeYear1'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='OtherIncomeYear1' ClientInstanceName='OtherIncomeYear1' ToolTip="Otros ingresos, primer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="OtherIncomeYear1Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true' AutoPostBack='true' OnTextChanged='OtherIncomeYear1_TextChanged' MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:33%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='OtherIncomeYear2' ClientInstanceName='OtherIncomeYear2' ToolTip="Otros ingresos, segundo año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="OtherIncomeYear2Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true' AutoPostBack='true' OnTextChanged='OtherIncomeYear2_TextChanged' MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:34%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='OtherIncomeYear3' ClientInstanceName='OtherIncomeYear3' ToolTip="Otros ingresos, tercer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="OtherIncomeYear3Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true' AutoPostBack='true' OnTextChanged='OtherIncomeYear3_TextChanged' MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DescriptionOtherIncomeLabel' EncodeHtml='false' ClientInstanceName='DescriptionOtherIncomeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DescriptionOtherIncomeLabelResource"  Text="Descripción:"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DescriptionOtherIncome'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DescriptionOtherIncome' ClientInstanceName='DescriptionOtherIncome' ToolTip="Descripción de otro ingreso" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="DescriptionOtherIncomeResource" Width='135px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part2" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style="width:16.5%">
      &nbsp;
    </td>
    <td style="width:16.5%">
      &nbsp;
    </td>
    <td style="width:16.5%">
      &nbsp;
    </td>
    <td style="width:16.5%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='TotalEarnedIncomeYear1Label' EncodeHtml='false' ClientInstanceName='TotalEarnedIncomeYear1Label' runat='server' ClientIDMode='Static' meta:resourcekey="TotalEarnedIncomeYear1LabelResource"  Text="Ingresos totales"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='TotalEarnedIncomeYear1'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='TotalEarnedIncomeYear1' ClientInstanceName='TotalEarnedIncomeYear1' ToolTip="Ingresos totales del primer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="TotalEarnedIncomeYear1Resource"  Width='144px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
        <RequiredField IsRequired='true' ErrorText="" />
        <RegularExpression ValidationExpression="^[-0-9]\d*([,\.]\d+)?$" ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:33%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='TotalEarnedIncomeYear2' ClientInstanceName='TotalEarnedIncomeYear2' ToolTip="Ingresos totales del segundo año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="TotalEarnedIncomeYear2Resource"  Width='144px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
        <RequiredField IsRequired='true' ErrorText="" />
        <RegularExpression ValidationExpression="^[-0-9]\d*([,\.]\d+)?$" ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:34%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='TotalEarnedIncomeYear3' ClientInstanceName='TotalEarnedIncomeYear3' ToolTip="Ingresos totales del tercer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="TotalEarnedIncomeYear3Resource"  Width='144px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part2" >
        <RequiredField IsRequired='true' ErrorText="" />
        <RegularExpression ValidationExpression="^[-0-9]\d*([,\.]\d+)?$" ErrorText="" />
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
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label1' EncodeHtml='false' ClientInstanceName='label1' runat='server' ClientIDMode='Static' meta:resourcekey="label1Resource"  Text="INGRESOS ANUALES DEVENGADOS"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="10"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="part3" ClientInstanceName="part3" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="part3Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='iaYear1Label' EncodeHtml='false' ClientInstanceName='iaYear1Label' runat='server' ClientIDMode='Static' meta:resourcekey="iaYear1LabelResource"  Text="Año"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='iaYear1'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='iaYear1' ClientInstanceName='iaYear1' ToolTip="" Size='4' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="iaYear1Resource"  Width='45px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <0..9999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:33%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='iaYear2' ClientInstanceName='iaYear2' ToolTip="" Size='4' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="iaYear2Resource"  Width='45px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <0..9999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:34%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='iaYear3' ClientInstanceName='iaYear3' ToolTip="" Size='4' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="iaYear3Resource"  Width='45px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <0..9999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DividendsYear1Label' EncodeHtml='false' ClientInstanceName='DividendsYear1Label' runat='server' ClientIDMode='Static' meta:resourcekey="DividendsYear1LabelResource"  Text="Dividendos"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DividendsYear1'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DividendsYear1' ClientInstanceName='DividendsYear1' ToolTip="Dividendos de los ingresos anuales obtenidos en el primer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="DividendsYear1Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:33%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DividendsYear2' ClientInstanceName='DividendsYear2' ToolTip="Dividendos de los ingresos anuales obtenidos en el segundo año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="DividendsYear2Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:34%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DividendsYear3' ClientInstanceName='DividendsYear3' ToolTip="Dividendos de los ingresos anuales obtenidos en el tercer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="DividendsYear3Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='InterestYear1Label' EncodeHtml='false' ClientInstanceName='InterestYear1Label' runat='server' ClientIDMode='Static' meta:resourcekey="InterestYear1LabelResource"  Text="Intereses"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='InterestYear1'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='InterestYear1' ClientInstanceName='InterestYear1' ToolTip="Interés de los ingresos anuales obtenidos en el primer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="InterestYear1Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:33%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='InterestYear2' ClientInstanceName='InterestYear2' ToolTip="Interés de los ingresos anuales obtenidos en el segundo año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="InterestYear2Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:34%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='InterestYear3' ClientInstanceName='InterestYear3' ToolTip="Interés de los ingresos anuales obtenidos en el tercer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="InterestYear3Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='NetRentalsYear1Label' EncodeHtml='false' ClientInstanceName='NetRentalsYear1Label' runat='server' ClientIDMode='Static' meta:resourcekey="NetRentalsYear1LabelResource"  Text="Alquileres netos"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='NetRentalsYear1'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='NetRentalsYear1' ClientInstanceName='NetRentalsYear1' ToolTip="Renta neta de los ingresos anuales obtenidos en el primer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="NetRentalsYear1Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:33%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='NetRentalsYear2' ClientInstanceName='NetRentalsYear2' ToolTip="Renta neta de los ingresos anuales obtenidos en el segundo año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="NetRentalsYear2Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:34%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='NetRentalsYear3' ClientInstanceName='NetRentalsYear3' ToolTip="Renta neta de los ingresos anuales obtenidos en el tercer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="NetRentalsYear3Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='CapitalGainsYear1Label' EncodeHtml='false' ClientInstanceName='CapitalGainsYear1Label' runat='server' ClientIDMode='Static' meta:resourcekey="CapitalGainsYear1LabelResource"  Text="Ganancias sobre capital"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='CapitalGainsYear1'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='CapitalGainsYear1' ClientInstanceName='CapitalGainsYear1' ToolTip="Las ganancias de capital de los ingresos anuales obtenidos en el primer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="CapitalGainsYear1Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:33%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='CapitalGainsYear2' ClientInstanceName='CapitalGainsYear2' ToolTip="Las ganancias de capital de los ingresos anuales obtenidos en el segundo año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="CapitalGainsYear2Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:34%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='CapitalGainsYear3' ClientInstanceName='CapitalGainsYear3' ToolTip="Las ganancias de capital de los ingresos anuales obtenidos en el tercer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="CapitalGainsYear3Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='OtherEarnedIncomeYear1Label' EncodeHtml='false' ClientInstanceName='OtherEarnedIncomeYear1Label' runat='server' ClientIDMode='Static' meta:resourcekey="OtherEarnedIncomeYear1LabelResource"  Text="Otros (Especifique)"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='OtherEarnedIncomeYear1'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='OtherEarnedIncomeYear1' ClientInstanceName='OtherEarnedIncomeYear1' ToolTip="Otros, por el ingreso anual obtenido en el primer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="OtherEarnedIncomeYear1Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true' AutoPostBack='true' OnTextChanged='OtherEarnedIncomeYear1_TextChanged' MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:33%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='OtherEarnedIncomeYear2' ClientInstanceName='OtherEarnedIncomeYear2' ToolTip="Otros, por el ingreso anual obtenido en el segundo año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="OtherEarnedIncomeYear2Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true' AutoPostBack='true' OnTextChanged='OtherEarnedIncomeYear2_TextChanged' MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:34%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='OtherEarnedIncomeYear3' ClientInstanceName='OtherEarnedIncomeYear3' ToolTip="Otros, por el ingreso anual obtenido en el tercer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="OtherEarnedIncomeYear3Resource"  Width='144px'  Text='0' ClientEnabled='true' ClientVisible='true' AutoPostBack='true' OnTextChanged='OtherEarnedIncomeYear3_TextChanged' MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DescriptionOtherEarnedIncomeLabel' EncodeHtml='false' ClientInstanceName='DescriptionOtherEarnedIncomeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DescriptionOtherEarnedIncomeLabelResource"  Text="Descripción"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DescriptionOtherEarnedIncome'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DescriptionOtherEarnedIncome' ClientInstanceName='DescriptionOtherEarnedIncome' ToolTip="Descripción de otros ingresos del trabajo" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="DescriptionOtherEarnedIncomeResource" Width='135px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part3" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style="width:16.5%">
      &nbsp;
    </td>
    <td style="width:16.5%">
      &nbsp;
    </td>
    <td style="width:16.5%">
      &nbsp;
    </td>
    <td style="width:16.5%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:16.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='TotalUnearnedIncomeYear1Label' EncodeHtml='false' ClientInstanceName='TotalUnearnedIncomeYear1Label' runat='server' ClientIDMode='Static' meta:resourcekey="TotalUnearnedIncomeYear1LabelResource"  Text="Total ingresos no derivados del trabajo"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='TotalUnearnedIncomeYear1'       ></dxe:ASPxLabel></td>    <td style='width:16.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='TotalUnearnedIncomeYear1' ClientInstanceName='TotalUnearnedIncomeYear1' ToolTip="Total ingresos no derivados del trabajo, primer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="TotalUnearnedIncomeYear1Resource"  Width='144px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:33%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='TotalUnearnedIncomeYear2' ClientInstanceName='TotalUnearnedIncomeYear2' ToolTip="Total ingresos no derivados del trabajo, segundo año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="TotalUnearnedIncomeYear2Resource"  Width='144px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:34%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='TotalUnearnedIncomeYear3' ClientInstanceName='TotalUnearnedIncomeYear3' ToolTip="Total ingresos no derivados del trabajo, tercer año" Size='12' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="TotalUnearnedIncomeYear3Resource"  Width='144px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part3" >
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
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label2' EncodeHtml='false' ClientInstanceName='label2' runat='server' ClientIDMode='Static' meta:resourcekey="label2Resource"  Text="ACTIVOS / PASIVOS"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="10"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="part4" ClientInstanceName="part4" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="part4Resource"
 Width="100%" SkinID="CaptionAndSquareBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Center'>       <dxe:ASPxLabel ID='label3' EncodeHtml='false' ClientInstanceName='label3' runat='server' ClientIDMode='Static' meta:resourcekey="label3Resource"  Text="Activos"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



<td style='width:50%; padding-top:3px;' colspan='2' align='Center'>       <dxe:ASPxLabel ID='label4' EncodeHtml='false' ClientInstanceName='label4' runat='server' ClientIDMode='Static' meta:resourcekey="label4Resource"  Text="Pasivos"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="subpart4" ClientInstanceName="subpart4" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="subpart4Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='CashLabel' EncodeHtml='false' ClientInstanceName='CashLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CashLabelResource"  Text="Efectivo"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Cash'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='Cash' ClientInstanceName='Cash' ToolTip="Activo, efectivo" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="CashResource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart4" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='RealEstateLabel' EncodeHtml='false' ClientInstanceName='RealEstateLabel' runat='server' ClientIDMode='Static' meta:resourcekey="RealEstateLabelResource"  Text="Bienes raíces"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='RealEstate'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='RealEstate' ClientInstanceName='RealEstate' ToolTip="Activo, Bienes raices" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="RealEstateResource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart4" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='BusinessEquityLabel' EncodeHtml='false' ClientInstanceName='BusinessEquityLabel' runat='server' ClientIDMode='Static' meta:resourcekey="BusinessEquityLabelResource"  Text="Negocios de equidad"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='BusinessEquity'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='BusinessEquity' ClientInstanceName='BusinessEquity' ToolTip="Activos de negocios de equidad" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="BusinessEquityResource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart4" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='StocksLabel' EncodeHtml='false' ClientInstanceName='StocksLabel' runat='server' ClientIDMode='Static' meta:resourcekey="StocksLabelResource"  Text="Inventario"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Stocks'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='Stocks' ClientInstanceName='Stocks' ToolTip="Activo, inventario" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="StocksResource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart4" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='OtherAssets1Label' EncodeHtml='false' ClientInstanceName='OtherAssets1Label' runat='server' ClientIDMode='Static' meta:resourcekey="OtherAssets1LabelResource"  Text="Otros activos"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='OtherAssets1'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='OtherAssets1' ClientInstanceName='OtherAssets1' ToolTip="Detalle de otros activos" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="OtherAssets1Resource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true' AutoPostBack='true' OnTextChanged='OtherAssets1_TextChanged' MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart4" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DescribeOtherAssets1Label' EncodeHtml='false' ClientInstanceName='DescribeOtherAssets1Label' runat='server' ClientIDMode='Static' meta:resourcekey="DescribeOtherAssets1LabelResource"  Text="Especifique"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DescribeOtherAssets1'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DescribeOtherAssets1' ClientInstanceName='DescribeOtherAssets1' ToolTip="Descripción de los otros activos" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="DescribeOtherAssets1Resource" Width='135px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="subpart4" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='OtherAssets2Label' EncodeHtml='false' ClientInstanceName='OtherAssets2Label' runat='server' ClientIDMode='Static' meta:resourcekey="OtherAssets2LabelResource"  Text="Otros activos"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='OtherAssets2'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='OtherAssets2' ClientInstanceName='OtherAssets2' ToolTip="Detalle de otros activos" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="OtherAssets2Resource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true' AutoPostBack='true' OnTextChanged='OtherAssets2_TextChanged' MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart4" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DescribeOtherAssets2Label' EncodeHtml='false' ClientInstanceName='DescribeOtherAssets2Label' runat='server' ClientIDMode='Static' meta:resourcekey="DescribeOtherAssets2LabelResource"  Text="Especifique"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DescribeOtherAssets2'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DescribeOtherAssets2' ClientInstanceName='DescribeOtherAssets2' ToolTip="Descripción de los otros activos" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="DescribeOtherAssets2Resource" Width='135px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="subpart4" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='OtherAssets3Label' EncodeHtml='false' ClientInstanceName='OtherAssets3Label' runat='server' ClientIDMode='Static' meta:resourcekey="OtherAssets3LabelResource"  Text="Otros activos"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='OtherAssets3'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='OtherAssets3' ClientInstanceName='OtherAssets3' ToolTip="Detalle de otros activos" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="OtherAssets3Resource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true' AutoPostBack='true' OnTextChanged='OtherAssets3_TextChanged' MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart4" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DescribeOtherAssets3Label' EncodeHtml='false' ClientInstanceName='DescribeOtherAssets3Label' runat='server' ClientIDMode='Static' meta:resourcekey="DescribeOtherAssets3LabelResource"  Text="Especifique"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DescribeOtherAssets3'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DescribeOtherAssets3' ClientInstanceName='DescribeOtherAssets3' ToolTip="Descripción de los otros activos" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="DescribeOtherAssets3Resource" Width='135px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="subpart4" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='OtherAssets4Label' EncodeHtml='false' ClientInstanceName='OtherAssets4Label' runat='server' ClientIDMode='Static' meta:resourcekey="OtherAssets4LabelResource"  Text="Otros activos"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='OtherAssets4'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='OtherAssets4' ClientInstanceName='OtherAssets4' ToolTip="Detalle de otros activos" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="OtherAssets4Resource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true' AutoPostBack='true' OnTextChanged='OtherAssets4_TextChanged' MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart4" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DescribeOtherAssets4Label' EncodeHtml='false' ClientInstanceName='DescribeOtherAssets4Label' runat='server' ClientIDMode='Static' meta:resourcekey="DescribeOtherAssets4LabelResource"  Text="Especifique"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DescribeOtherAssets4'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DescribeOtherAssets4' ClientInstanceName='DescribeOtherAssets4' ToolTip="Descripción de los otros activos" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="DescribeOtherAssets4Resource" Width='135px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="subpart4" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='TotalAssetsLabel' EncodeHtml='false' ClientInstanceName='TotalAssetsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="TotalAssetsLabelResource"  Text="Total activo"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='TotalAssets'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='TotalAssets' ClientInstanceName='TotalAssets' ToolTip="Total assets in dollar" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="TotalAssetsResource"  Width='180px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart4" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="subpart41" ClientInstanceName="subpart41" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="subpart41Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='MortgagesLabel' EncodeHtml='false' ClientInstanceName='MortgagesLabel' runat='server' ClientIDMode='Static' meta:resourcekey="MortgagesLabelResource"  Text="Hipoteca"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Mortgages'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='Mortgages' ClientInstanceName='Mortgages' ToolTip="Hipoteca" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="MortgagesResource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart41" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LoansLabel' EncodeHtml='false' ClientInstanceName='LoansLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LoansLabelResource"  Text="Prétamos"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Loans'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='Loans' ClientInstanceName='Loans' ToolTip="Préstamos" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="LoansResource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart41" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LiensLabel' EncodeHtml='false' ClientInstanceName='LiensLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LiensLabelResource"  Text="Gravámenes"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Liens'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='Liens' ClientInstanceName='Liens' ToolTip="Gravámenes" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="LiensResource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart41" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='BondsLabel' EncodeHtml='false' ClientInstanceName='BondsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="BondsLabelResource"  Text="Bonos"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Bonds'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='Bonds' ClientInstanceName='Bonds' ToolTip="Pasivos" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="BondsResource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart41" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='OtherLiabilities1Label' EncodeHtml='false' ClientInstanceName='OtherLiabilities1Label' runat='server' ClientIDMode='Static' meta:resourcekey="OtherLiabilities1LabelResource"  Text="Otros pasivos"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='OtherLiabilities1'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='OtherLiabilities1' ClientInstanceName='OtherLiabilities1' ToolTip="Otros pasivos" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="OtherLiabilities1Resource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true' AutoPostBack='true' OnTextChanged='OtherLiabilities1_TextChanged' MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart41" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DescribeOtherLiabilities1Label' EncodeHtml='false' ClientInstanceName='DescribeOtherLiabilities1Label' runat='server' ClientIDMode='Static' meta:resourcekey="DescribeOtherLiabilities1LabelResource"  Text="Especifique"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DescribeOtherLiabilities1'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DescribeOtherLiabilities1' ClientInstanceName='DescribeOtherLiabilities1' ToolTip="Descripción de los otros pasivos" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="DescribeOtherLiabilities1Resource" Width='135px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="subpart41" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='OtherLiabilities2Label' EncodeHtml='false' ClientInstanceName='OtherLiabilities2Label' runat='server' ClientIDMode='Static' meta:resourcekey="OtherLiabilities2LabelResource"  Text="Otros pasivos"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='OtherLiabilities2'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='OtherLiabilities2' ClientInstanceName='OtherLiabilities2' ToolTip="Otros pasivos" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="OtherLiabilities2Resource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true' AutoPostBack='true' OnTextChanged='OtherLiabilities2_TextChanged' MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart41" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DescribeOtherLiabilities2Label' EncodeHtml='false' ClientInstanceName='DescribeOtherLiabilities2Label' runat='server' ClientIDMode='Static' meta:resourcekey="DescribeOtherLiabilities2LabelResource"  Text="Especifique"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DescribeOtherLiabilities2'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DescribeOtherLiabilities2' ClientInstanceName='DescribeOtherLiabilities2' ToolTip="Descripción de los otros pasivos" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="DescribeOtherLiabilities2Resource" Width='135px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="subpart41" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='OtherLiabilities3Label' EncodeHtml='false' ClientInstanceName='OtherLiabilities3Label' runat='server' ClientIDMode='Static' meta:resourcekey="OtherLiabilities3LabelResource"  Text="Otros pasivos"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='OtherLiabilities3'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='OtherLiabilities3' ClientInstanceName='OtherLiabilities3' ToolTip="Otros pasivos" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="OtherLiabilities3Resource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true' AutoPostBack='true' OnTextChanged='OtherLiabilities3_TextChanged' MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart41" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DescribeOtherLiabilities3Label' EncodeHtml='false' ClientInstanceName='DescribeOtherLiabilities3Label' runat='server' ClientIDMode='Static' meta:resourcekey="DescribeOtherLiabilities3LabelResource"  Text="Especifique"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DescribeOtherLiabilities3'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DescribeOtherLiabilities3' ClientInstanceName='DescribeOtherLiabilities3' ToolTip="Descripción de los otros pasivos" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="DescribeOtherLiabilities3Resource" Width='135px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="subpart41" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='OtherLiabilities4Label' EncodeHtml='false' ClientInstanceName='OtherLiabilities4Label' runat='server' ClientIDMode='Static' meta:resourcekey="OtherLiabilities4LabelResource"  Text="Otros pasivos"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='OtherLiabilities4'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='OtherLiabilities4' ClientInstanceName='OtherLiabilities4' ToolTip="Otros pasivos" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="OtherLiabilities4Resource"  Width='180px'  Text='0' ClientEnabled='true' ClientVisible='true' AutoPostBack='true' OnTextChanged='OtherLiabilities4_TextChanged' MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart41" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DescribeOtherLiabilities4Label' EncodeHtml='false' ClientInstanceName='DescribeOtherLiabilities4Label' runat='server' ClientIDMode='Static' meta:resourcekey="DescribeOtherLiabilities4LabelResource"  Text="Especifique"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DescribeOtherLiabilities4'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DescribeOtherLiabilities4' ClientInstanceName='DescribeOtherLiabilities4' ToolTip="Descripción de los otros pasivos" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="DescribeOtherLiabilities4Resource" Width='135px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="subpart41" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='TotalLiabilitiesLabel' EncodeHtml='false' ClientInstanceName='TotalLiabilitiesLabel' runat='server' ClientIDMode='Static' meta:resourcekey="TotalLiabilitiesLabelResource"  Text="Total pasivo"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='TotalLiabilities'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='TotalLiabilities' ClientInstanceName='TotalLiabilities' ToolTip="Total pasivo" Size='15' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="TotalLiabilitiesResource"  Width='180px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <0..999999999999999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="subpart41" >
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
    <td style="width:50%">
      &nbsp;
    </td>
    <td style="width:50%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="part5" ClientInstanceName="part5" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="part5Resource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='AdditionalInformationLabel' EncodeHtml='false' ClientInstanceName='AdditionalInformationLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AdditionalInformationLabelResource"  Text="Información adicional"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='AdditionalInformation'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxMemo runat='server' ClientIDMode='Static' ID='AdditionalInformation' ToolTip="Additional information supplied for the questionnaire" meta:resourcekey="AdditionalInformationResource" Columns='80' Rows='2' Size='0' NullText="" ClientVisible='True'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part5" >
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
  </tr>
</table>
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dxrp:ASPxRoundPanel>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zoned" ClientInstanceName="zoned" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zonedResource"
 Width="100%" SkinID="RoundedBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="declaracion" ClientInstanceName="declaracion" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="declaracionResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='textd' EncodeHtml='false' ClientInstanceName='textd' runat='server' ClientIDMode='Static' meta:resourcekey="textdResource"  Text="Declaro que las respuestas que he dado son de lo mejor de mi conocimiento,  verdadera y completa,  que no he ocultado ninguna información material que pueda influir en la evaluación o la aceptación de mi solicitud. Reconozco que este cuestionario es parte de la solicitud de seguro de vida y que no revelar algún hecho material conocido para mí, puede invalidar el contrato."  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



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
                    <dxrp:ASPxRoundPanel ID="firma" ClientInstanceName="firma" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="firmaResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:33%;' align='left'>       <dxe:ASPxLabel ID='DateReceivedLabel' EncodeHtml='false' ClientInstanceName='DateReceivedLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DateReceivedLabelResource"  Text="Fecha"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DateReceived'       ></dxe:ASPxLabel><br />

       <dxe:ASPxDateEdit runat='server' ID='DateReceived' ToolTip="Fecha en la que se rellena el cuestionario" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="DateReceivedResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="firma" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxDateEdit>
    </td>

    <td style='width:33%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='save' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Se guarda la información del cuestionario sin procesarlo, a la espera de completar su contenido por parte del solicitante." ClientVisible='True' ClientEnabled='True' meta:resourcekey="saveResource" Text="Guardar temporalmente" ImagePosition='Left' Image-Url="/images/Library/16x16_ASPNetIcons/file_manager_16x16.gif"  OnClick='save_Click' AutoPostBack='true'>
       </dxe:ASPxButton>
    </td>

    <td style='width:34%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='submit' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Registra la información del cuestionario y actualiza los requisitos relacionados a la solciitud." ClientVisible='True' ClientEnabled='True' meta:resourcekey="submitResource" Text="Enviar" ImagePosition='Left' Image-Url="/images/Library/16x16_ASPNetIcons/mail2_(add)_16x16.gif"  OnClick='submit_Click' AutoPostBack='false'>
<ClientSideEvents  Click="submitClick" />
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