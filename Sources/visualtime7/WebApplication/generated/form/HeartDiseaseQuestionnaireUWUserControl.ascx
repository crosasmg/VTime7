<%@ Control Language="VB" AutoEventWireup="false" CodeFile="HeartDiseaseQuestionnaireUWUserControl.ascx.vb" Inherits="HeartDiseaseQuestionnaireUWUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">
    var msgbutton8InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button8InformationMessageResource"></asp:Localize>';
    var titlebutton8InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton8InformationMessageResource"></asp:Localize>';
    var msgbutton7InformationMessageResource='<asp:Localize runat="server" Text="Procesando información.... Por favor espere." meta:resourcekey="button7InformationMessageResource"></asp:Localize>';
    var titlebutton7InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton7InformationMessageResource"></asp:Localize>';



</script>

<script src="/generated/form/HeartDiseaseQuestionnaireUW.js" type="text/javascript"></script>      
<asp:UpdatePanel ID="HeartDiseaseQuestionnaireUWUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='HeartDiseaseQuestionnaireUWTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone7" ClientInstanceName="zone7" runat="server" HeaderText="" ToolTip="" Enabled="False" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone7Resource"
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

       <dxe:ASPxTextBox runat='server' ID='ClientName' ClientInstanceName='ClientName' ToolTip="text8" Size='30' NullText="" ClientVisible='True' MaxLength='30' ClientIDMode='Static' meta:resourcekey="ClientNameResource" Width='270px'  ClientEnabled='False'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone7" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='uwcaseidLabel' EncodeHtml='false' ClientInstanceName='uwcaseidLabel' runat='server' ClientIDMode='Static' meta:resourcekey="uwcaseidLabelResource"  Text="Solicitud"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='uwcaseid'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='uwcaseid' ClientInstanceName='uwcaseid' ToolTip="Identificador del caso" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="uwcaseidResource" Width='135px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone7" >
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
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="Questions" ClientInstanceName="Questions" runat="server" HeaderText="El siguiente cuestionario debe ser completado por el solicitante del seguro. Por favor conteste todas las preguntas." ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="QuestionsResource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label2' EncodeHtml='false' ClientInstanceName='label2' runat='server' ClientIDMode='Static' meta:resourcekey="label2Resource"  Text="1. ¿Sabe usted el diagnóstico específico aplicado a esta enfermedad?"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='Cardiomyopathy' runat='server' Text="Cardiomiopatía" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="Cardiomyopathy"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='IschaemicHeartDisease' runat='server' Text="Isquémica enfermedad del corazón" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="IschaemicHeartDisease"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='MitralOrOtherValve' runat='server' Text="Válvula mitral u otro tipo de válvula estenosis/insuficiencia" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="MitralOrOtherValve"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='OtherDiagnosis' runat='server' Text="Otro diagnóstico" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="OtherDiagnosis"  AutoPostBack='false' EncodeHtml='false' > 
<ClientSideEvents  CheckedChanged="OtherDiagnosisCheckedChanged" />
       </dxe:ASPxCheckBox>


    </td>

    <td style='width:50%;' align='left'>       <dxe:ASPxLabel ID='DetailsSpecificDiagnosisLabel' EncodeHtml='false' ClientInstanceName='DetailsSpecificDiagnosisLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DetailsSpecificDiagnosisLabelResource"  Text="Si la respuesta es Sí a cualquier otra, por favor explique:"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DetailsSpecificDiagnosis'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='DetailsSpecificDiagnosis' ClientInstanceName='DetailsSpecificDiagnosis' ToolTip="Explicar el diagnóstico específico de la enfermedad" Size='35' NullText="" ClientVisible='True' MaxLength='35' ClientIDMode='Static' meta:resourcekey="DetailsSpecificDiagnosisResource" Width='315px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Questions" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='SymptomsAccompaniedByOtherLabel' EncodeHtml='false' ClientInstanceName='SymptomsAccompaniedByOtherLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SymptomsAccompaniedByOtherLabelResource"  Text="2. ¿Sus síntomas son acompañados por otros síntomas del organismo (por ejemplo, la sudoración, mareos, desmayos)?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='SymptomsAccompaniedByOther'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxRadioButtonList ID='SymptomsAccompaniedByOther' ClientInstanceName='SymptomsAccompaniedByOther' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="Indica si los síntomas van acompañados de otros síntomas" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="SymptomsAccompaniedByOtherResource"  ValueType='System.Boolean'  AutoPostBack='false' >
            <Items>
                <dxe:ListEditItem Value='True' Text='Si' meta:resourcekey="SymptomsAccompaniedByOtherListItemValue1Resource"/>
                <dxe:ListEditItem Value='False' Text='No' meta:resourcekey="SymptomsAccompaniedByOtherListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Questions" >
</ValidationSettings>
<ClientSideEvents  ValueChanged="SymptomsAccompaniedByOtherValueChanged" />
       </dxe:ASPxRadioButtonList>
    </td>

    <td style='width:50%;' align='left'>       <dxe:ASPxLabel ID='DescribeBodySymptomsLabel' EncodeHtml='false' ClientInstanceName='DescribeBodySymptomsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DescribeBodySymptomsLabelResource"  Text="Si la respuesta es Sí, por favor describa:"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DescribeBodySymptoms'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='DescribeBodySymptoms' ClientInstanceName='DescribeBodySymptoms' ToolTip="Describir los otros síntomas" Size='35' NullText="" ClientVisible='True' MaxLength='35' ClientIDMode='Static' meta:resourcekey="DescribeBodySymptomsResource" Width='315px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Questions" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DateSymptomsInitiallyOccurLabel' EncodeHtml='false' ClientInstanceName='DateSymptomsInitiallyOccurLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DateSymptomsInitiallyOccurLabelResource"  Text="3. ¿Cuándo se produjo estos síntomas por primera vez?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DateSymptomsInitiallyOccur'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='DateSymptomsInitiallyOccur' ToolTip="Indica cuando fue la primera vez que comenzaron los síntomas" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="DateSymptomsInitiallyOccurResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Questions" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='SymptomsRelatedWithEventLabel' EncodeHtml='false' ClientInstanceName='SymptomsRelatedWithEventLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SymptomsRelatedWithEventLabelResource"  Text="4. ¿Los síntomas estuvieron relacionados con un evento especial?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='SymptomsRelatedWithEvent'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxRadioButtonList ID='SymptomsRelatedWithEvent' ClientInstanceName='SymptomsRelatedWithEvent' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="Indica si los síntomas estuvieron relacionados con un evento en particular" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="SymptomsRelatedWithEventResource"  ValueType='System.Boolean'   >
            <Items>
                <dxe:ListEditItem Value='True' Text='Si' meta:resourcekey="SymptomsRelatedWithEventListItemValue1Resource"/>
                <dxe:ListEditItem Value='False' Text='No' meta:resourcekey="SymptomsRelatedWithEventListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Questions" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='FrequencyOfTheSymptomsLabel' EncodeHtml='false' ClientInstanceName='FrequencyOfTheSymptomsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="FrequencyOfTheSymptomsLabelResource"  Text="5. ¿Con qué frecuencia tiene estos síntomas u otros similares desde el episodio inicial?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='FrequencyOfTheSymptoms'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>


<dxe:ASPxComboBox ID='FrequencyOfTheSymptoms' runat='server' ClientInstanceName='FrequencyOfTheSymptoms' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="indicar la frecuencia de los síntomas como: siempre, regularmente, u otro" ClientVisible='true' ClientEnabled='True' meta:resourcekey="FrequencyOfTheSymptomsResource"  ValueType='System.Int32'   >
            <Items>
                <dxe:ListEditItem Value='0' Text='Rara vez' meta:resourcekey="FrequencyOfTheSymptomsListItemValue1Resource"/>
                <dxe:ListEditItem Value='1' Text='Pocos' meta:resourcekey="FrequencyOfTheSymptomsListItemValue2Resource"/>
                <dxe:ListEditItem Value='2' Text='Frecuentemente' meta:resourcekey="FrequencyOfTheSymptomsListItemValue3Resource"/>
                <dxe:ListEditItem Value='3' Text='En varias ocasiones' meta:resourcekey="FrequencyOfTheSymptomsListItemValue4Resource"/>
                <dxe:ListEditItem Value='4' Text='Nunca' meta:resourcekey="FrequencyOfTheSymptomsListItemValue5Resource"/>
                <dxe:ListEditItem Value='5' Text='Casi todo el tiempo' meta:resourcekey="FrequencyOfTheSymptomsListItemValue6Resource"/>
                <dxe:ListEditItem Value='6' Text='Muchas veces' meta:resourcekey="FrequencyOfTheSymptomsListItemValue7Resource"/>
            </Items>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Questions" >
</ValidationSettings>
</dxe:ASPxComboBox>
    </td>

    <td style='width:50%;' align='left'>       <dxe:ASPxLabel ID='DetailsEventRelatedLabel' EncodeHtml='false' ClientInstanceName='DetailsEventRelatedLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DetailsEventRelatedLabelResource"  Text="Detalles del evento"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DetailsEventRelated'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='DetailsEventRelated' ClientInstanceName='DetailsEventRelated' ToolTip="detalles del evento" Size='35' NullText="" ClientVisible='True' MaxLength='35' ClientIDMode='Static' meta:resourcekey="DetailsEventRelatedResource" Width='315px'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Questions" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DuringOfTheSymptomsLabel' EncodeHtml='false' ClientInstanceName='DuringOfTheSymptomsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DuringOfTheSymptomsLabelResource"  Text="6. ¿Cuánto tiempo duran estos síntomas?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DuringOfTheSymptoms'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>


<dxe:ASPxComboBox ID='DuringOfTheSymptoms' runat='server' ClientInstanceName='DuringOfTheSymptoms' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Duración de los síntomas: un año, semanas, días, otros" ClientVisible='true' ClientEnabled='True' meta:resourcekey="DuringOfTheSymptomsResource"  ValueType='System.String'   >
            <Items>
                <dxe:ListEditItem Value='0' Text='Diario' meta:resourcekey="DuringOfTheSymptomsListItemValue1Resource"/>
                <dxe:ListEditItem Value='1' Text='Semanal' meta:resourcekey="DuringOfTheSymptomsListItemValue2Resource"/>
                <dxe:ListEditItem Value='2' Text='Después de cada comida' meta:resourcekey="DuringOfTheSymptomsListItemValue3Resource"/>
                <dxe:ListEditItem Value='3' Text='Varias veces al dia' meta:resourcekey="DuringOfTheSymptomsListItemValue4Resource"/>
            </Items>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Questions" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DateOfLastOccurrenceLabel' EncodeHtml='false' ClientInstanceName='DateOfLastOccurrenceLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DateOfLastOccurrenceLabelResource"  Text="7. ¿Cuándo fue el último episodio?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DateOfLastOccurrence'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='DateOfLastOccurrence' ToolTip="indica la fecha en la cual se produjo el último episodio" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="DateOfLastOccurrenceResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Questions" >
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
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label3' EncodeHtml='false' ClientInstanceName='label3' runat='server' ClientIDMode='Static' meta:resourcekey="label3Resource"  Text="8. ¿Cómo comienzan generalmente los síntomas?"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='Suddenly' runat='server' Text="Inesperadamente" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="Suddenly"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='Gradually' runat='server' Text="Gradualmente" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="Gradually"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='AtRest' runat='server' Text="En reposo" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="AtRest"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='OnlyOnPhysicalActivity' runat='server' Text="Sólo con actividad física" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="OnlyOnPhysicalActivity"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


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
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label4' EncodeHtml='false' ClientInstanceName='label4' runat='server' ClientIDMode='Static' meta:resourcekey="label4Resource"  Text="9. ¿Sus síntomas empeoran o mejoran con el esfuerzo?"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='SymptomsBetter' runat='server' Text="Mejoró" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="SymptomsBetter"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='SymptomsWorse' runat='server' Text="Empeoró" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="SymptomsWorse"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='YesConsultedSpecialistLabel' EncodeHtml='false' ClientInstanceName='YesConsultedSpecialistLabel' runat='server' ClientIDMode='Static' meta:resourcekey="YesConsultedSpecialistLabelResource"  Text="10. ¿Ha consultado a un médico generalista o especialista en esta enfermedad?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='YesConsultedSpecialist'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxRadioButtonList ID='YesConsultedSpecialist' ClientInstanceName='YesConsultedSpecialist' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="indica que la persona consultó al especialista o médico general para esta enfermedad" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="YesConsultedSpecialistResource"  ValueType='System.Boolean'   >
            <Items>
                <dxe:ListEditItem Value='True' Text='Si' meta:resourcekey="YesConsultedSpecialistListItemValue1Resource"/>
                <dxe:ListEditItem Value='False' Text='No' meta:resourcekey="YesConsultedSpecialistListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Questions" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

    <td style='width:50%' colspan='2'>
<dxe:ASPxButton ID='btnADDDetailsOfMedicalPractitioners' ClientInstanceName='btnADDDetailsOfMedicalPractitioners' ClientVisible="True" ClientEnabled="True" Image-Url="~/images/generaluse/new.gif" Text="" meta:resourcekey="DetailsOfMedicalPractitionersGridBtnResource" runat="server" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
	                    DetailsOfMedicalPractitioners.AddNewRow();
                }" />
            </dxe:ASPxButton>            
   
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='DetailsOfMedicalPractitioners' EnableRowsCache='False' EnableViewState='True' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='DetailsOfMedicalPractitioners' runat='server' Width='100%' KeyFieldName='IdDetails' Caption="Si su respuesta es SI, por favor especifique" meta:resourcekey="DetailsOfMedicalPractitionersResource"

>
                 <ClientSideEvents 
 RowDblClick="function(s, e) { DetailsOfMedicalPractitioners.StartEditRow(e.visibleIndex); }" />
            <SettingsEditing Mode="Inline" />
               <SettingsPager Visible="True" PageSize="10"/>
               <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AllowSort="True"/>
        <Columns>
<dxwgv:GridViewDataTextColumn FieldName='IdDetails' Visible='false'>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='PractitionerName' FieldName='PractitionerName' Caption="Nombre" ToolTip="PractitionerName" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="PractitionerNameFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='15' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='PhonePractitioner' FieldName='PhonePractitioner' Caption="Teléfono" ToolTip="Teléfono del médico o especialista." GroupIndex="-1" VisibleIndex="1" meta:resourcekey="PhonePractitionerFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='15' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='eMailPractitioner' FieldName='eMailPractitioner' Caption="eMail" ToolTip="Correo electrónico del médico o especialista." GroupIndex="-1" VisibleIndex="2" meta:resourcekey="eMailPractitionerFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='50' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='AddresPractitioner' FieldName='AddresPractitioner' Caption="Dirección" ToolTip="AddresPractitioner" GroupIndex="-1" VisibleIndex="3" meta:resourcekey="AddresPractitionerFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="3" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='15' >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='TypeTreatmentHadLabel' EncodeHtml='false' ClientInstanceName='TypeTreatmentHadLabel' runat='server' ClientIDMode='Static' meta:resourcekey="TypeTreatmentHadLabelResource"  Text="11. ¿Qué tratamiento ha recibido usted (por ejemplo, cirugía o medicación)?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='TypeTreatmentHad'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>


<dxe:ASPxComboBox ID='TypeTreatmentHad' runat='server' ClientInstanceName='TypeTreatmentHad' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="tipo de tratamiento recibido" ClientVisible='true' ClientEnabled='True' meta:resourcekey="TypeTreatmentHadResource"  ValueType='System.Int32'   >
            <Items>
                <dxe:ListEditItem Value='0' Text='Cirugía' meta:resourcekey="TypeTreatmentHadListItemValue1Resource"/>
                <dxe:ListEditItem Value='1' Text='Medicación' meta:resourcekey="TypeTreatmentHadListItemValue2Resource"/>
                <dxe:ListEditItem Value='2' Text='Dieta' meta:resourcekey="TypeTreatmentHadListItemValue3Resource"/>
                <dxe:ListEditItem Value='3' Text='Cirugía-Medicación' meta:resourcekey="TypeTreatmentHadListItemValue4Resource"/>
                <dxe:ListEditItem Value='4' Text='Cirugía-Dieta' meta:resourcekey="TypeTreatmentHadListItemValue5Resource"/>
                <dxe:ListEditItem Value='5' Text='Medicación-Dieta' meta:resourcekey="TypeTreatmentHadListItemValue6Resource"/>
                <dxe:ListEditItem Value='6' Text='Otro' meta:resourcekey="TypeTreatmentHadListItemValue7Resource"/>
            </Items>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Questions" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='StillReceivingTreatmentLabel' EncodeHtml='false' ClientInstanceName='StillReceivingTreatmentLabel' runat='server' ClientIDMode='Static' meta:resourcekey="StillReceivingTreatmentLabelResource"  Text="12. ¿Sigue recibiendo tratamiento?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='StillReceivingTreatment'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxRadioButtonList ID='StillReceivingTreatment' ClientInstanceName='StillReceivingTreatment' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="indica que si aun esta recibiendo tratamiento" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="StillReceivingTreatmentResource"  ValueType='System.Boolean'  AutoPostBack='false' >
            <Items>
                <dxe:ListEditItem Value='True' Text='Si' meta:resourcekey="StillReceivingTreatmentListItemValue1Resource"/>
                <dxe:ListEditItem Value='False' Text='No' meta:resourcekey="StillReceivingTreatmentListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Questions" >
</ValidationSettings>
<ClientSideEvents  ValueChanged="StillReceivingTreatmentValueChanged" />
       </dxe:ASPxRadioButtonList>
    </td>

    <td style='width:50%;' align='left'>       <dxe:ASPxLabel ID='DetailsStillRecievingTreatmentLabel' EncodeHtml='false' ClientInstanceName='DetailsStillRecievingTreatmentLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DetailsStillRecievingTreatmentLabelResource"  Text="Si la respuesta es Sí, por favor dar detalles:"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DetailsStillRecievingTreatment'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='DetailsStillRecievingTreatment' ClientInstanceName='DetailsStillRecievingTreatment' ToolTip="detalles del tratamiento actual" Size='35' NullText="" ClientVisible='True' MaxLength='35' ClientIDMode='Static' meta:resourcekey="DetailsStillRecievingTreatmentResource" Width='315px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Questions" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='SurgeryOrInvestigationContemplatedLabel' EncodeHtml='false' ClientInstanceName='SurgeryOrInvestigationContemplatedLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SurgeryOrInvestigationContemplatedLabelResource"  Text="13. ¿Esta contemplada cualquier intervención quirúrgica o una investigación más a fondo para el futuro?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='SurgeryOrInvestigationContemplated'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxRadioButtonList ID='SurgeryOrInvestigationContemplated' ClientInstanceName='SurgeryOrInvestigationContemplated' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="indica si hay alguna investigacion, examen o cirugía planificada para el futuro" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="SurgeryOrInvestigationContemplatedResource"  ValueType='System.Boolean'   >
            <Items>
                <dxe:ListEditItem Value='True' Text='Si' meta:resourcekey="SurgeryOrInvestigationContemplatedListItemValue1Resource"/>
                <dxe:ListEditItem Value='False' Text='No' meta:resourcekey="SurgeryOrInvestigationContemplatedListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Questions" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

    <td style='width:50%' colspan='2'>
<dxe:ASPxButton ID='btnADDTreatmentPrescribed' ClientInstanceName='btnADDTreatmentPrescribed' ClientVisible="True" ClientEnabled="True" Image-Url="~/images/generaluse/new.gif" Text="" meta:resourcekey="TreatmentPrescribedGridBtnResource" runat="server" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
	                    TreatmentPrescribed.AddNewRow();
                }" />
            </dxe:ASPxButton>            
   
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='TreatmentPrescribed' EnableRowsCache='False' EnableViewState='True' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='TreatmentPrescribed' runat='server' Width='100%' KeyFieldName='id' Caption="14. Por favor escriba todos los medicamentos, no mencionados anteriormente en este cuestionario, que usted esta tomando con regulridad o de maneraa intermitente, ya sea para esta o cualquier otra condición o enfermedad" meta:resourcekey="TreatmentPrescribedResource"

>
                 <ClientSideEvents 
 RowDblClick="function(s, e) { TreatmentPrescribed.StartEditRow(e.visibleIndex); }" />
            <SettingsEditing Mode="Inline" />
               <SettingsPager Visible="True" PageSize="10"/>
               <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AllowSort="True"/>
        <Columns>
<dxwgv:GridViewDataTextColumn FieldName='id' Visible='false'>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='DatePeriod' FieldName='DatePeriod' Caption="Fecha" ToolTip="DatePeriod" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="DatePeriodFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='15' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Dosage' FieldName='Dosage' Caption="Dosis" ToolTip="Dosage" GroupIndex="-1" VisibleIndex="1" meta:resourcekey="DosageFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='15' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='NameOfMedication' FieldName='NameOfMedication' Caption="Medicamento" ToolTip="NameOfMedication" GroupIndex="-1" VisibleIndex="2" meta:resourcekey="NameOfMedicationFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='15' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn Caption=' ' meta:resourcekey='TreatmentPrescribedCommandColumsResource'>
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
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label5' EncodeHtml='false' ClientInstanceName='label5' runat='server' ClientIDMode='Static' meta:resourcekey="label5Resource"  Text="15. ¿Ha sido objeto de las investigaciones especiales, tales como: (marque todas las que correspondan)."  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='CoronaryAngiogram' runat='server' Text="Angiografía coronaria" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="CoronaryAngiogram"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='ThalliumPerfusionScan' runat='server' Text="Gammagrafía de perfusión con talio" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="ThalliumPerfusionScan"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='Resting' runat='server' Text="Descanso" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="Resting"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='Exercise' runat='server' Text="Prueba" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="Exercise"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='Endoscopy' runat='server' Text="Endoscopia" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="Endoscopy"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='Echocardiogram' runat='server' Text="Ecocardiograma" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="Echocardiogram"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='SestamibiStress' runat='server' Text="Sestamibi Estrés / Prueba ECG" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="SestamibiStress"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='Other' runat='server' Text="Otro" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="Other"  AutoPostBack='false' EncodeHtml='false' > 
<ClientSideEvents  CheckedChanged="OtherCheckedChanged" />
       </dxe:ASPxCheckBox>


    </td>

    <td style='width:50%;' align='left'>       <dxe:ASPxLabel ID='SpecifyOtherLabel' EncodeHtml='false' ClientInstanceName='SpecifyOtherLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SpecifyOtherLabelResource"  Text="Otro, por favor especifique:"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='SpecifyOther'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='SpecifyOther' ClientInstanceName='SpecifyOther' ToolTip="Especificar la otra investigacion o prueba especial" Size='35' NullText="" ClientVisible='True' MaxLength='35' ClientIDMode='Static' meta:resourcekey="SpecifyOtherResource" Width='315px'  ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Questions" >
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='YesRestrictedInLifeStyleLabel' EncodeHtml='false' ClientInstanceName='YesRestrictedInLifeStyleLabel' runat='server' ClientIDMode='Static' meta:resourcekey="YesRestrictedInLifeStyleLabelResource"  Text="16. ¿Usted ha tenido regularmente ausencias al trabajo, o esta actualmente ausente del trabajo o ha sido restringido en su estilo de vida durante más de un mes como consecuencia de esta enfermedad?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='YesRestrictedInLifeStyle'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxRadioButtonList ID='YesRestrictedInLifeStyle' ClientInstanceName='YesRestrictedInLifeStyle' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="indica si tiene restricciones en el estilo de vida o se ha ausentado al trabajo" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="YesRestrictedInLifeStyleResource"  ValueType='System.Boolean'   >
            <Items>
                <dxe:ListEditItem Value='True' Text='Si' meta:resourcekey="YesRestrictedInLifeStyleListItemValue1Resource"/>
                <dxe:ListEditItem Value='False' Text='No' meta:resourcekey="YesRestrictedInLifeStyleListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Questions" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

    <td style='width:50%' colspan='2'>
<dxe:ASPxButton ID='btnADDDetailsAbsensesFromWork' ClientInstanceName='btnADDDetailsAbsensesFromWork' ClientVisible="True" ClientEnabled="True" Image-Url="~/images/generaluse/new.gif" Text="" meta:resourcekey="DetailsAbsensesFromWorkGridBtnResource" runat="server" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
	                    DetailsAbsensesFromWork.AddNewRow();
                }" />
            </dxe:ASPxButton>            
   
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='DetailsAbsensesFromWork' EnableRowsCache='False' EnableViewState='True' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='DetailsAbsensesFromWork' runat='server' Width='100%' KeyFieldName='id' Caption="Si si respuesta es SI, por favor especifique" meta:resourcekey="DetailsAbsensesFromWorkResource"

>
                 <ClientSideEvents 
 RowDblClick="function(s, e) { DetailsAbsensesFromWork.StartEditRow(e.visibleIndex); }" />
            <SettingsEditing Mode="Inline" />
               <SettingsPager Visible="True" PageSize="10"/>
               <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AllowSort="True"/>
        <Columns>
<dxwgv:GridViewDataTextColumn FieldName='id' Visible='false'>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataDateColumn Name='DateFrom' FieldName='DateFrom' Caption="Desde" ToolTip="DateFrom" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="DateFromFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesDateEdit>
           </PropertiesDateEdit>
</dxwgv:GridViewDataDateColumn>
<dxwgv:GridViewDataDateColumn Name='DateTo' FieldName='DateTo' Caption="Hasta" ToolTip="DateTo" GroupIndex="-1" VisibleIndex="1" meta:resourcekey="DateToFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesDateEdit>
           </PropertiesDateEdit>
</dxwgv:GridViewDataDateColumn>
<dxwgv:GridViewDataTextColumn Name='Details' FieldName='Details' Caption="Detalles" ToolTip="Details" GroupIndex="-1" VisibleIndex="2" meta:resourcekey="DetailsFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='15' >
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
    <td style='width:50%;' align='left'>       <dxe:ASPxLabel ID='AdditionalInformationLabel' EncodeHtml='false' ClientInstanceName='AdditionalInformationLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AdditionalInformationLabelResource"  Text="17. Información adicional"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='AdditionalInformation'       ></dxe:ASPxLabel><br />

       <dxe:ASPxMemo runat='server' ClientIDMode='Static' ID='AdditionalInformation' ToolTip="Indica alguna informacion adicional al cuestionario relevante" meta:resourcekey="AdditionalInformationResource" Columns='20' Rows='2' Size='0' NullText="" ClientVisible='True'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Questions" >
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
                    <dxrp:ASPxRoundPanel ID="declaracion" ClientInstanceName="declaracion" runat="server" HeaderText="zone" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="declaracionResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label1' EncodeHtml='false' ClientInstanceName='label1' runat='server' ClientIDMode='Static' meta:resourcekey="label1Resource"  Text="Declaro que las respuestas que he dado son de lo mejor de mi conocimiento,  verdadera y completa,  que no he ocultado ninguna información material que pueda influir en la evaluación o la aceptación de mi solicitud. Reconozco que este cuestionario es parte de la solicitud de seguro de vida y que no revelar algún hecho material conocido para mí, puede invalidar el contrato."  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



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
                    <dxrp:ASPxRoundPanel ID="firma" ClientInstanceName="firma" runat="server" HeaderText="zone" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="firmaResource"
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
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="firma" >
</ValidationSettings>
       </dxe:ASPxDateEdit>
    </td>

    <td style='width:33%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='button8' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Se guarda la información del cuestionario sin procesarlo, a la espera de completar su contenido por parte del solicitante." ClientVisible='True' ClientEnabled='True' meta:resourcekey="button8Resource" Text="Guardar temporalmente" ImagePosition='Left' Image-Url="/images/Library/16x16_ASPNetIcons/file_manager_16x16.gif"  OnClick='button8_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button8Click" />
       </dxe:ASPxButton>
    </td>

    <td style='width:34%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='button7' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Registra la información del cuestionario y actualiza los requisitos relacionados a la solciitud." ClientVisible='True' ClientEnabled='True' meta:resourcekey="button7Resource" Text="Enviar" ImagePosition='Left' Image-Url="/images/Library/16x16_ASPNetIcons/mail2_(add)_16x16.gif"  OnClick='button7_Click' AutoPostBack='false'>
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