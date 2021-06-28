<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CancerQuestionnaireUWUserControl.ascx.vb" Inherits="CancerQuestionnaireUWUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">
    var msgsubmitInformationMessageResource='<asp:Localize runat="server" Text="Procesando información.... Por favor espere." meta:resourcekey="submitInformationMessageResource"></asp:Localize>';
    var titlesubmitInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlesubmitInformationMessageResource"></asp:Localize>';



</script>

<script src="/generated/form/CancerQuestionnaireUW.js" type="text/javascript"></script>      
<asp:UpdatePanel ID="CancerQuestionnaireUWUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='CancerQuestionnaireUWTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="part0ca" ClientInstanceName="part0ca" runat="server" HeaderText="" ToolTip="" Enabled="False" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="part0caResource"
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='ClientNameLabel' EncodeHtml='false' ClientInstanceName='ClientNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ClientNameLabelResource"  Text="Solicitante del seguro"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='ClientName'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='ClientName' ClientInstanceName='ClientName' ToolTip="Solicitante del seguro" Size='30' NullText="" ClientVisible='True' MaxLength='30' ClientIDMode='Static' meta:resourcekey="ClientNameResource" Width='270px'  ClientEnabled='False'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part0ca" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='uwcaseidLabel' EncodeHtml='false' ClientInstanceName='uwcaseidLabel' runat='server' ClientIDMode='Static' meta:resourcekey="uwcaseidLabelResource"  Text="Solicitud"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='uwcaseid'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='uwcaseid' ClientInstanceName='uwcaseid' ToolTip="" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="uwcaseidResource" Width='135px'  ClientEnabled='False'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part0ca" >
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
                    <dxrp:ASPxRoundPanel ID="part1ca" ClientInstanceName="part1ca" runat="server" HeaderText="El siguiente cuestionario debe ser completado por el solicitante del seguro. Por favor conteste todas las preguntas." ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="part1caResource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
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
    <td style="width:0%">
      &nbsp;
    </td>
    <td style="width:0%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='TypeOfCancerLabel' EncodeHtml='false' ClientInstanceName='TypeOfCancerLabel' runat='server' ClientIDMode='Static' meta:resourcekey="TypeOfCancerLabelResource"  Text="1. Tipo de cáncer o tumor"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='TypeOfCancer'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>


<dxe:ASPxComboBox ID='TypeOfCancer' runat='server' ClientInstanceName='TypeOfCancer' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Tipo de cáncer" ClientVisible='true' ClientEnabled='True' meta:resourcekey="TypeOfCancerResource"  ValueType='System.Int32'   >
            <Items>
                <dxe:ListEditItem Value='1' Text='Cancer' meta:resourcekey="TypeOfCancerListItemValue1Resource"/>
                <dxe:ListEditItem Value='2' Text='Tumor' meta:resourcekey="TypeOfCancerListItemValue2Resource"/>
                <dxe:ListEditItem Value='3' Text='Quistes' meta:resourcekey="TypeOfCancerListItemValue3Resource"/>
            </Items>
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DateDiagnosedLabel' EncodeHtml='false' ClientInstanceName='DateDiagnosedLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DateDiagnosedLabelResource"  Text="2. Fecha del diagnóstico"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DateDiagnosed'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='DateDiagnosed' ToolTip="" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="DateDiagnosedResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxDateEdit>
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='StageOfCancerLabel' EncodeHtml='false' ClientInstanceName='StageOfCancerLabel' runat='server' ClientIDMode='Static' meta:resourcekey="StageOfCancerLabelResource"  Text="3. Estado del cáncer"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='StageOfCancer'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>


<dxe:ASPxComboBox ID='StageOfCancer' runat='server' ClientInstanceName='StageOfCancer' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Indica el estado del cáncer" ClientVisible='true' ClientEnabled='True' meta:resourcekey="StageOfCancerResource"  ValueType='System.Int32'   >
            <Items>
                <dxe:ListEditItem Value='0' Text='1' meta:resourcekey="StageOfCancerListItemValue1Resource"/>
                <dxe:ListEditItem Value='1' Text='2' meta:resourcekey="StageOfCancerListItemValue2Resource"/>
                <dxe:ListEditItem Value='2' Text='2a' meta:resourcekey="StageOfCancerListItemValue3Resource"/>
                <dxe:ListEditItem Value='3' Text='2b' meta:resourcekey="StageOfCancerListItemValue4Resource"/>
                <dxe:ListEditItem Value='4' Text='2c' meta:resourcekey="StageOfCancerListItemValue5Resource"/>
                <dxe:ListEditItem Value='5' Text='3' meta:resourcekey="StageOfCancerListItemValue6Resource"/>
                <dxe:ListEditItem Value='6' Text='3a' meta:resourcekey="StageOfCancerListItemValue7Resource"/>
                <dxe:ListEditItem Value='7' Text='3b' meta:resourcekey="StageOfCancerListItemValue8Resource"/>
                <dxe:ListEditItem Value='8' Text='4' meta:resourcekey="StageOfCancerListItemValue9Resource"/>
            </Items>
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
</dxe:ASPxComboBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='ScaleColonRectalCancerLabel' EncodeHtml='false' ClientInstanceName='ScaleColonRectalCancerLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ScaleColonRectalCancerLabelResource"  Text="Si el cáncer es de colon o rectal: Escala Dukes"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='ScaleColonRectalCancer'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>


<dxe:ASPxComboBox ID='ScaleColonRectalCancer' runat='server' ClientInstanceName='ScaleColonRectalCancer' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Si el cáncer es de colon o rectal indique la Escala Dukes" ClientVisible='true' ClientEnabled='True' meta:resourcekey="ScaleColonRectalCancerResource"  ValueType='System.Int32'   >
            <Items>
                <dxe:ListEditItem Value='0' Text='A' meta:resourcekey="ScaleColonRectalCancerListItemValue1Resource"/>
                <dxe:ListEditItem Value='1' Text='B1' meta:resourcekey="ScaleColonRectalCancerListItemValue2Resource"/>
                <dxe:ListEditItem Value='2' Text='B2-3' meta:resourcekey="ScaleColonRectalCancerListItemValue3Resource"/>
                <dxe:ListEditItem Value='3' Text='C1' meta:resourcekey="ScaleColonRectalCancerListItemValue4Resource"/>
                <dxe:ListEditItem Value='4' Text='C2' meta:resourcekey="ScaleColonRectalCancerListItemValue5Resource"/>
                <dxe:ListEditItem Value='5' Text='D' meta:resourcekey="ScaleColonRectalCancerListItemValue6Resource"/>
            </Items>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
</ValidationSettings>
</dxe:ASPxComboBox>
    </td>

  </tr>
  <tr valign='top'>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LevelMelanomaLabel' EncodeHtml='false' ClientInstanceName='LevelMelanomaLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LevelMelanomaLabelResource"  Text="Si es un melanoma: Nivel Clark"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LevelMelanoma'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>


<dxe:ASPxComboBox ID='LevelMelanoma' runat='server' ClientInstanceName='LevelMelanoma' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Nivel Clark" ClientVisible='true' ClientEnabled='True' meta:resourcekey="LevelMelanomaResource"  ValueType='System.Int32'   >
            <Items>
                <dxe:ListEditItem Value='0' Text='I' meta:resourcekey="LevelMelanomaListItemValue1Resource"/>
                <dxe:ListEditItem Value='1' Text='II' meta:resourcekey="LevelMelanomaListItemValue2Resource"/>
                <dxe:ListEditItem Value='2' Text='III' meta:resourcekey="LevelMelanomaListItemValue3Resource"/>
                <dxe:ListEditItem Value='3' Text='IV' meta:resourcekey="LevelMelanomaListItemValue4Resource"/>
                <dxe:ListEditItem Value='4' Text='V' meta:resourcekey="LevelMelanomaListItemValue5Resource"/>
            </Items>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
</ValidationSettings>
</dxe:ASPxComboBox>
    </td>

  </tr>
  <tr valign='top'>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='GradeProstateCancerLabel' EncodeHtml='false' ClientInstanceName='GradeProstateCancerLabel' runat='server' ClientIDMode='Static' meta:resourcekey="GradeProstateCancerLabelResource"  Text="Si es un cáncer de próstata: Grado Gleason"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='GradeProstateCancer'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>


<dxe:ASPxComboBox ID='GradeProstateCancer' runat='server' ClientInstanceName='GradeProstateCancer' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="If is a prostate cancer, indicate de gleasons grade" ClientVisible='true' ClientEnabled='True' meta:resourcekey="GradeProstateCancerResource"  ValueType='System.Int32'   >
            <Items>
                <dxe:ListEditItem Value='0' Text='2-5' meta:resourcekey="GradeProstateCancerListItemValue1Resource"/>
                <dxe:ListEditItem Value='1' Text='6' meta:resourcekey="GradeProstateCancerListItemValue2Resource"/>
                <dxe:ListEditItem Value='2' Text='7' meta:resourcekey="GradeProstateCancerListItemValue3Resource"/>
                <dxe:ListEditItem Value='3' Text='8-10' meta:resourcekey="GradeProstateCancerListItemValue4Resource"/>
            </Items>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
</ValidationSettings>
</dxe:ASPxComboBox>
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
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='EvidenceOfRecurringCancerLabel' EncodeHtml='false' ClientInstanceName='EvidenceOfRecurringCancerLabel' runat='server' ClientIDMode='Static' meta:resourcekey="EvidenceOfRecurringCancerLabelResource"  Text="4. ¿Ha habido alguna evidencia de cáncer recurrente?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='EvidenceOfRecurringCancer'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxRadioButtonList ID='EvidenceOfRecurringCancer' ClientInstanceName='EvidenceOfRecurringCancer' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="Indique si tiene alguna evidencia de cáncer recurrente" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="EvidenceOfRecurringCancerResource"  ValueType='System.Boolean'  AutoPostBack='false' OnSelectedIndexChanged='EvidenceOfRecurringCancer_SelectedIndexChanged' >
            <Items>
                <dxe:ListEditItem Value='True' Text='Si' meta:resourcekey="EvidenceOfRecurringCancerListItemValue1Resource"/>
                <dxe:ListEditItem Value='False' Text='No' meta:resourcekey="EvidenceOfRecurringCancerListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="8px" PaddingRight="0px" PaddingTop="0px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
<ClientSideEvents SelectedIndexChanged="AsyncPostBack"/>
       </dxe:ASPxRadioButtonList>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DateEvidenceLabel' EncodeHtml='false' ClientInstanceName='DateEvidenceLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DateEvidenceLabelResource"  Text="Si la respuesta es Si, indique la fecha"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DateEvidence'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='DateEvidence' ToolTip="" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="DateEvidenceResource" ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxDateEdit>
    </td>

  </tr>
  <tr valign='top'>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LocationRecurringCancerLabel' EncodeHtml='false' ClientInstanceName='LocationRecurringCancerLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LocationRecurringCancerLabelResource"  Text="Lozalización"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='LocationRecurringCancer'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='LocationRecurringCancer' ClientInstanceName='LocationRecurringCancer' ToolTip="Localización del cáncer" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="LocationRecurringCancerResource" Width='135px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
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
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DateInitiallyTreatedLabel' EncodeHtml='false' ClientInstanceName='DateInitiallyTreatedLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DateInitiallyTreatedLabelResource"  Text="5. Fecha inicialmente tratada"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DateInitiallyTreated'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='DateInitiallyTreated' ToolTip="" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="DateInitiallyTreatedResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxDateEdit>
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DateLastTreatedLabel' EncodeHtml='false' ClientInstanceName='DateLastTreatedLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DateLastTreatedLabelResource"  Text="6. Fecha del último tratamiento"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DateLastTreated'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='DateLastTreated' ToolTip="" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="DateLastTreatedResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxDateEdit>
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DateLastSeenByDoctorLabel' EncodeHtml='false' ClientInstanceName='DateLastSeenByDoctorLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DateLastSeenByDoctorLabelResource"  Text="7. Fecha de última visita al doctor"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DateLastSeenByDoctor'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='DateLastSeenByDoctor' ToolTip="" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="DateLastSeenByDoctorResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxDateEdit>
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='GrowthMayBeMalignantLabel' EncodeHtml='false' ClientInstanceName='GrowthMayBeMalignantLabel' runat='server' ClientIDMode='Static' meta:resourcekey="GrowthMayBeMalignantLabelResource"  Text="8. ¿Ha habido alguna evidencia o indicio de que el crecimiento puede ser maligno?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='GrowthMayBeMalignant'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxRadioButtonList ID='GrowthMayBeMalignant' ClientInstanceName='GrowthMayBeMalignant' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="GrowthMayBeMalignantResource"  ValueType='System.Boolean'  AutoPostBack='false' OnSelectedIndexChanged='GrowthMayBeMalignant_SelectedIndexChanged' >
            <Items>
                <dxe:ListEditItem Value='True' Text='Si' meta:resourcekey="GrowthMayBeMalignantListItemValue1Resource"/>
                <dxe:ListEditItem Value='False' Text='No' meta:resourcekey="GrowthMayBeMalignantListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="8px" PaddingRight="0px" PaddingTop="0px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
<ClientSideEvents SelectedIndexChanged="AsyncPostBack"/>
       </dxe:ASPxRadioButtonList>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DetailsEvidenceLabel' EncodeHtml='false' ClientInstanceName='DetailsEvidenceLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DetailsEvidenceLabelResource"  Text="En caso afirmativo, indique los detalles completos, incluyendo el informe histológico (si lo tiene su médico de cabecera):"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DetailsEvidence'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DetailsEvidence' ClientInstanceName='DetailsEvidence' ToolTip="Detalles completos, incluyendo histología informe del crecimiento" Size='35' NullText="" ClientVisible='True' MaxLength='35' ClientIDMode='Static' meta:resourcekey="DetailsEvidenceResource" Width='315px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
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
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='RemovedGrowthLabel' EncodeHtml='false' ClientInstanceName='RemovedGrowthLabel' runat='server' ClientIDMode='Static' meta:resourcekey="RemovedGrowthLabelResource"  Text="9. ¿El crecimiento ha sido eliminado?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='RemovedGrowth'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxRadioButtonList ID='RemovedGrowth' ClientInstanceName='RemovedGrowth' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="RemovedGrowthResource"  ValueType='System.Boolean'  AutoPostBack='false' OnSelectedIndexChanged='RemovedGrowth_SelectedIndexChanged' >
            <Items>
                <dxe:ListEditItem Value='True' Text='Si' meta:resourcekey="RemovedGrowthListItemValue1Resource"/>
                <dxe:ListEditItem Value='False' Text='No' meta:resourcekey="RemovedGrowthListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="8px" PaddingRight="0px" PaddingTop="0px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
<ClientSideEvents SelectedIndexChanged="AsyncPostBack"/>
       </dxe:ASPxRadioButtonList>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DetailsInvestigationsLabel' EncodeHtml='false' ClientInstanceName='DetailsInvestigationsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DetailsInvestigationsLabelResource"  Text="Si no, por favor proporcione detalles de las investigaciones que se han llevado a cabo (como biopsia). Por favor, incluya la fecha(s)"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DetailsInvestigations'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DetailsInvestigations' ClientInstanceName='DetailsInvestigations' ToolTip="Proporcione detalles de las investigaciones cuando el crecimiento no se ha eliminado" Size='35' NullText="" ClientVisible='True' MaxLength='35' ClientIDMode='Static' meta:resourcekey="DetailsInvestigationsResource" Width='315px'  ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
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
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='labelca' EncodeHtml='false' ClientInstanceName='labelca' runat='server' ClientIDMode='Static' meta:resourcekey="labelcaResource"  Text="10. ¿Qué tratamiento ha tenido para después de la eliminación?"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='ChemotherapyTreatmentFollowing' runat='server' Text="Quimioterapia" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="ChemotherapyTreatmentFollowing"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='RadiotherapyTreatmentFollowing' runat='server' Text="Radioterapia" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="RadiotherapyTreatmentFollowing"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='MedicationTreatmentFollowing' runat='server' Text="Medicación" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="MedicationTreatmentFollowing"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='OtherTreatmentFollowingLabel' EncodeHtml='false' ClientInstanceName='OtherTreatmentFollowingLabel' runat='server' ClientIDMode='Static' meta:resourcekey="OtherTreatmentFollowingLabelResource"  Text="Otro tratamiento"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='OtherTreatmentFollowing'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='OtherTreatmentFollowing' ClientInstanceName='OtherTreatmentFollowing' ToolTip="Tipo de tratamiento después de eliminado el crecimiento." Size='35' NullText="" ClientVisible='True' MaxLength='35' ClientIDMode='Static' meta:resourcekey="OtherTreatmentFollowingResource" Width='315px'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
</ValidationSettings>
       </dxe:ASPxTextBox>
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
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='StillFollowedUpLabel' EncodeHtml='false' ClientInstanceName='StillFollowedUpLabel' runat='server' ClientIDMode='Static' meta:resourcekey="StillFollowedUpLabelResource"  Text="11. ¿Sigue siendo objeto de seguimiento del tratamiento después de la eliminación?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='StillFollowedUp'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxRadioButtonList ID='StillFollowedUp' ClientInstanceName='StillFollowedUp' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="StillFollowedUpResource"  ValueType='System.Boolean'  AutoPostBack='false' OnSelectedIndexChanged='StillFollowedUp_SelectedIndexChanged' >
            <Items>
                <dxe:ListEditItem Value='True' Text='Si' meta:resourcekey="StillFollowedUpListItemValue1Resource"/>
                <dxe:ListEditItem Value='False' Text='No' meta:resourcekey="StillFollowedUpListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="8px" PaddingRight="0px" PaddingTop="0px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
<ClientSideEvents SelectedIndexChanged="AsyncPostBack"/>
       </dxe:ASPxRadioButtonList>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HowOftenLabel' EncodeHtml='false' ClientInstanceName='HowOftenLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HowOftenLabelResource"  Text="En caso afirmativo, indique con qué frecuencia"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='HowOften'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='HowOften' ClientInstanceName='HowOften' ToolTip="Frecuencia de aplicación del tratamiento" Size='35' NullText="" ClientVisible='True' MaxLength='35' ClientIDMode='Static' meta:resourcekey="HowOftenResource" Width='315px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DateOfDischargedFollowUpLabel' EncodeHtml='false' ClientInstanceName='DateOfDischargedFollowUpLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DateOfDischargedFollowUpLabelResource"  Text="Si no, cuando le dieron de alta en el seguimiento?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DateOfDischargedFollowUp'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='DateOfDischargedFollowUp' ToolTip="" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="DateOfDischargedFollowUpResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxDateEdit>
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
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label2ca' EncodeHtml='false' ClientInstanceName='label2ca' runat='server' ClientIDMode='Static' meta:resourcekey="label2caResource"  Text="12. Por favor, proporcione información detallada de todos los tratamientos que ha recibido en el pasado"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



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
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='SurgeryTreatment' runat='server' Text="Cirugía" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="SurgeryTreatment"  AutoPostBack='false' OnCheckedChanged='SurgeryTreatment_CheckedChanged' EncodeHtml='false' > 
<ClientSideEvents CheckedChanged="AsyncPostBack"/>
       </dxe:ASPxCheckBox>


    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DateSurgeryTreatmentLabel' EncodeHtml='false' ClientInstanceName='DateSurgeryTreatmentLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DateSurgeryTreatmentLabelResource"  Text="Fecha"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DateSurgeryTreatment'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='DateSurgeryTreatment' ToolTip="" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="DateSurgeryTreatmentResource" ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxDateEdit>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='ChemotherapyTreatment' runat='server' Text="Quimioterapia" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="ChemotherapyTreatment"  AutoPostBack='false' OnCheckedChanged='ChemotherapyTreatment_CheckedChanged' EncodeHtml='false' > 
<ClientSideEvents CheckedChanged="AsyncPostBack"/>
       </dxe:ASPxCheckBox>


    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DateChemotherapyTreatmentLabel' EncodeHtml='false' ClientInstanceName='DateChemotherapyTreatmentLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DateChemotherapyTreatmentLabelResource"  Text="Fecha"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DateChemotherapyTreatment'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='DateChemotherapyTreatment' ToolTip="" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="DateChemotherapyTreatmentResource" ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxDateEdit>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='RadiationTreatment' runat='server' Text="Radiación" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="RadiationTreatment"  AutoPostBack='false' OnCheckedChanged='RadiationTreatment_CheckedChanged' EncodeHtml='false' > 
<ClientSideEvents CheckedChanged="AsyncPostBack"/>
       </dxe:ASPxCheckBox>


    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DateRadiationTreatmentLabel' EncodeHtml='false' ClientInstanceName='DateRadiationTreatmentLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DateRadiationTreatmentLabelResource"  Text="Fecha"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DateRadiationTreatment'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='DateRadiationTreatment' ToolTip="" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="DateRadiationTreatmentResource" ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxDateEdit>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='HormoneTreatment' runat='server' Text="Hormonas" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="HormoneTreatment"  AutoPostBack='false' OnCheckedChanged='HormoneTreatment_CheckedChanged' EncodeHtml='false' > 
<ClientSideEvents CheckedChanged="AsyncPostBack"/>
       </dxe:ASPxCheckBox>


    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DateHormoneTreatmentLabel' EncodeHtml='false' ClientInstanceName='DateHormoneTreatmentLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DateHormoneTreatmentLabelResource"  Text="Fecha"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DateHormoneTreatment'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='DateHormoneTreatment' ToolTip="" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="DateHormoneTreatmentResource" ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxDateEdit>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='OtherTreatment' runat='server' Text="Otro tratamiento" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="OtherTreatment"  AutoPostBack='false' OnCheckedChanged='OtherTreatment_CheckedChanged' EncodeHtml='false' > 
<ClientSideEvents CheckedChanged="AsyncPostBack"/>
       </dxe:ASPxCheckBox>


    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DateOtherTreatmentLabel' EncodeHtml='false' ClientInstanceName='DateOtherTreatmentLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DateOtherTreatmentLabelResource"  Text="Fecha"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DateOtherTreatment'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='DateOtherTreatment' ToolTip="" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="DateOtherTreatmentResource" ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
<RequiredField IsRequired='true' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxDateEdit>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='NameTreatmentLabel' EncodeHtml='false' ClientInstanceName='NameTreatmentLabel' runat='server' ClientIDMode='Static' meta:resourcekey="NameTreatmentLabelResource"  Text="Por favor, proporcione detalles de otros tratamientos (si no ya se ha mencionado)"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='NameTreatment'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='NameTreatment' ClientInstanceName='NameTreatment' ToolTip="Name of the treatment recieved not specify in the list" Size='35' NullText="" ClientVisible='True' MaxLength='35' ClientIDMode='Static' meta:resourcekey="NameTreatmentResource" Width='315px'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='PrognosisLabel' EncodeHtml='false' ClientInstanceName='PrognosisLabel' runat='server' ClientIDMode='Static' meta:resourcekey="PrognosisLabelResource"  Text="13. ¿Le han dado ninguna información sobre el pronóstico?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Prognosis'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxRadioButtonList ID='Prognosis' ClientInstanceName='Prognosis' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="PrognosisResource"  ValueType='System.Boolean'  AutoPostBack='false' OnSelectedIndexChanged='Prognosis_SelectedIndexChanged' >
            <Items>
                <dxe:ListEditItem Value='True' Text='Si' meta:resourcekey="PrognosisListItemValue1Resource"/>
                <dxe:ListEditItem Value='False' Text='No' meta:resourcekey="PrognosisListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
</ValidationSettings>
<ClientSideEvents SelectedIndexChanged="AsyncPostBack"/>
       </dxe:ASPxRadioButtonList>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DetailsPrognosisLabel' EncodeHtml='false' ClientInstanceName='DetailsPrognosisLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DetailsPrognosisLabelResource"  Text="En caso afirmativo, indique los detalles completos"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DetailsPrognosis'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DetailsPrognosis' ClientInstanceName='DetailsPrognosis' ToolTip="Details of the prognosis" Size='35' NullText="" ClientVisible='True' MaxLength='35' ClientIDMode='Static' meta:resourcekey="DetailsPrognosisResource" Width='315px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
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
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label3ca' EncodeHtml='false' ClientInstanceName='label3ca' runat='server' ClientIDMode='Static' meta:resourcekey="label3caResource"  Text="14. Por favor, indique todos los tratamientos que actualmente recibe"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



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
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='ChemotherapyCurrentlyTreatment' runat='server' Text="Quimioterapia" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="ChemotherapyCurrentlyTreatment"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='RadiationCurrentlyTreatment' runat='server' Text="Radiación" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="RadiationCurrentlyTreatment"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='HormoneCurrentlyTreatment' runat='server' Text="Hormonas" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="HormoneCurrentlyTreatment"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='MedicationCurrentlyTreatment' runat='server' Text="Medicacion" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="MedicationCurrentlyTreatment"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='OtherCurrentlyTreatmentLabel' EncodeHtml='false' ClientInstanceName='OtherCurrentlyTreatmentLabel' runat='server' ClientIDMode='Static' meta:resourcekey="OtherCurrentlyTreatmentLabelResource"  Text="Otro"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='OtherCurrentlyTreatment'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='OtherCurrentlyTreatment' ClientInstanceName='OtherCurrentlyTreatment' ToolTip="Tipo de tratamiento actual" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="OtherCurrentlyTreatmentResource" Width='135px'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DetailsCurrentlyTreatmentLabel' EncodeHtml='false' ClientInstanceName='DetailsCurrentlyTreatmentLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DetailsCurrentlyTreatmentLabelResource"  Text="Por favor proporcione todos los detalles del tratamiento"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DetailsCurrentlyTreatment'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DetailsCurrentlyTreatment' ClientInstanceName='DetailsCurrentlyTreatment' ToolTip="Detalles del actual tratamiento" Size='35' NullText="" ClientVisible='True' MaxLength='35' ClientIDMode='Static' meta:resourcekey="DetailsCurrentlyTreatmentResource" Width='315px'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
</ValidationSettings>
       </dxe:ASPxTextBox>
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
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='YesRestrictedLifeStyleLabel' EncodeHtml='false' ClientInstanceName='YesRestrictedLifeStyleLabel' runat='server' ClientIDMode='Static' meta:resourcekey="YesRestrictedLifeStyleLabelResource"  Text="15. ¿Alguna vez ha tenido ausencias regulares de trabajo, o ha estado ausente del trabajo o restringido en su estilo de vida por más de un mes en el tiempo como resultado de lesión o enfermedad?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='YesRestrictedLifeStyle'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxRadioButtonList ID='YesRestrictedLifeStyle' ClientInstanceName='YesRestrictedLifeStyle' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="YesRestrictedLifeStyleResource"  ValueType='System.Boolean'   >
            <Items>
                <dxe:ListEditItem Value='True' Text='Si' meta:resourcekey="YesRestrictedLifeStyleListItemValue1Resource"/>
                <dxe:ListEditItem Value='False' Text='No' meta:resourcekey="YesRestrictedLifeStyleListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

    <td style='width:50%' colspan='2'>
<dxe:ASPxButton ID='btnADDDetailsAbsensesFromWork' ClientInstanceName='btnADDDetailsAbsensesFromWork' ClientVisible="True" ClientEnabled="True" Image-Url="~/images/generaluse/new.gif" Text="" meta:resourcekey="DetailsAbsensesFromWorkGridBtnResource" runat="server" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
	                    DetailsAbsensesFromWork.AddNewRow();
                }" />
            </dxe:ASPxButton>            
   
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='DetailsAbsensesFromWork' EnableRowsCache='False' EnableViewState='True' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='DetailsAbsensesFromWork' runat='server' Width='100%' KeyFieldName='id' Caption="Detalles de las ausencias del trabajo" meta:resourcekey="DetailsAbsensesFromWorkResource"

>
                 <ClientSideEvents 
 RowDblClick="function(s, e) { DetailsAbsensesFromWork.StartEditRow(e.visibleIndex); }" />
           <SettingsEditing Mode="PopupEditForm" PopupEditFormModal="true" PopupEditFormHorizontalAlign="WindowCenter" PopupEditFormVerticalAlign="WindowCenter"/>
               <SettingsPager Visible="True" PageSize="10"/>
               <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AllowSort="True"/>
                     <SettingsEditing EditFormColumnCount="1"/>
        <Columns>
<dxwgv:GridViewDataTextColumn FieldName='id' Visible='false'>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataDateColumn Name='DateFrom' FieldName='DateFrom' Caption="Desde" ToolTip="" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="DateFromFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesDateEdit>
           </PropertiesDateEdit>
</dxwgv:GridViewDataDateColumn>
<dxwgv:GridViewDataDateColumn Name='DateTo' FieldName='DateTo' Caption="Hasta" ToolTip="" GroupIndex="-1" VisibleIndex="1" meta:resourcekey="DateToFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesDateEdit>
           </PropertiesDateEdit>
</dxwgv:GridViewDataDateColumn>
<dxwgv:GridViewDataTextColumn Name='Details' FieldName='Details' Caption="Detalles" ToolTip="" GroupIndex="-1" VisibleIndex="2" meta:resourcekey="DetailsFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='25' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn Caption=' ' meta:resourcekey='DetailsAbsensesFromWorkCommandColumsResource'>
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
    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:50%' colspan='2'>
<dxe:ASPxButton ID='btnADDDetailsOfMedicalPractitioners' ClientInstanceName='btnADDDetailsOfMedicalPractitioners' ClientVisible="True" ClientEnabled="True" Image-Url="~/images/generaluse/new.gif" Text="" meta:resourcekey="DetailsOfMedicalPractitionersGridBtnResource" runat="server" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
	                    DetailsOfMedicalPractitioners.AddNewRow();
                }" />
            </dxe:ASPxButton>            
   
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='DetailsOfMedicalPractitioners' EnableRowsCache='False' EnableViewState='True' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='DetailsOfMedicalPractitioners' runat='server' Width='100%' KeyFieldName='IdDetails' Caption="16. Por favor, proporcione el nombre completo y dirección de todos los médicos generales y especialistas que le han tratado o le atiende actualmente para esta enfermedad y otras condiciones." meta:resourcekey="DetailsOfMedicalPractitionersResource"

>
                 <ClientSideEvents 
 RowDblClick="function(s, e) { DetailsOfMedicalPractitioners.StartEditRow(e.visibleIndex); }" />
           <SettingsEditing Mode="PopupEditForm" PopupEditFormModal="true" PopupEditFormHorizontalAlign="WindowCenter" PopupEditFormVerticalAlign="WindowCenter"/>
               <SettingsPager Visible="True" PageSize="10"/>
               <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AllowSort="True"/>
                     <SettingsEditing EditFormColumnCount="1"/>
        <Columns>
<dxwgv:GridViewDataTextColumn FieldName='IdDetails' Visible='false'>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='PractitionerName' FieldName='PractitionerName' Caption="Nombre" ToolTip="" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="PractitionerNameFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='30' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='PhonePractitioner' FieldName='PhonePractitioner' Caption="Teléfono" ToolTip="Teléfono del médico o especialista." GroupIndex="-1" VisibleIndex="1" meta:resourcekey="PhonePractitionerFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='15' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='eMailPractitioner' FieldName='eMailPractitioner' Caption="Correo" ToolTip="Correo electrónico del médico o especialista." GroupIndex="-1" VisibleIndex="2" meta:resourcekey="eMailPractitionerFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='50' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='AddresPractitioner' FieldName='AddresPractitioner' Caption="Dirección" ToolTip="" GroupIndex="-1" VisibleIndex="3" meta:resourcekey="AddresPractitionerFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="3" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='45' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn Caption=' ' meta:resourcekey='DetailsOfMedicalPractitionersCommandColumsResource'>
                                <EditButton Visible='True' Text='Editar' />
                                <DeleteButton Visible='True' Text='Eliminar' />
                                <CancelButton Visible='True' Text='Cancelar' />
                                <UpdateButton Visible='True' Text='Actualizar' />
                            </dxwgv:GridViewCommandColumn>
            </Columns>
        </dxwgv:ASPxGridView>
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='AdditionalInformationLabel' EncodeHtml='false' ClientInstanceName='AdditionalInformationLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AdditionalInformationLabelResource"  Text="17. Información adicional"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='AdditionalInformation'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxMemo runat='server' ClientIDMode='Static' ID='AdditionalInformation' ToolTip="Información adicional para el cuestionario" meta:resourcekey="AdditionalInformationResource" Columns='20' Rows='2' Size='0' NullText="" ClientVisible='True'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1ca" >
</ValidationSettings>
       </dxe:ASPxMemo>
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
                    <dxrp:ASPxRoundPanel ID="Zonadca" ClientInstanceName="Zonadca" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="ZonadcaResource"
 Width="100%" SkinID="RoundedBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="declaracionca" ClientInstanceName="declaracionca" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="declaracioncaResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label5ca' EncodeHtml='false' ClientInstanceName='label5ca' runat='server' ClientIDMode='Static' meta:resourcekey="label5caResource"  Text="Declaro que las respuestas que he dado son de lo mejor de mi conocimiento,  verdadera y completa,  que no he ocultado ninguna información material que pueda influir en la evaluación o la aceptación de mi solicitud. Reconozco que este cuestionario es parte de la solicitud de seguro de vida y que no revelar algún hecho material conocido para mí, puede invalidar el contrato."  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



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
                    <dxrp:ASPxRoundPanel ID="Firmaca" ClientInstanceName="Firmaca" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="FirmacaResource"
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
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Firmaca" >
</ValidationSettings>
       </dxe:ASPxDateEdit>
    </td>

    <td style='width:33%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='save' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Se guarda la información del cuestionario sin procesarlo, a la espera de completar su contenido por parte del solicitante." ClientVisible='True' ClientEnabled='True' meta:resourcekey="saveResource" Text="Guardar temporalmente" ImagePosition='Left' Image-Url="/images/Library/16x16_ASPNetIcons/file_manager_16x16.gif"  OnClick='save_Click' AutoPostBack='false'>
<ClientSideEvents  Click="saveClick" />
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