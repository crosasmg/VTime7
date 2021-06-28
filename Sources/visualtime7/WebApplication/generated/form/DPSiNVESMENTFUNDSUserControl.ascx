<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DPSiNVESMENTFUNDSUserControl.ascx.vb" Inherits="DPSiNVESMENTFUNDSUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">



</script>

<script src="/generated/form/DPSiNVESMENTFUNDS.js" type="text/javascript"></script>      
<asp:UpdatePanel runat="server">


  
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='DPSiNVESMENTFUNDSTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='AbnormalResultsTestsLabel' EncodeHtml='false' ClientInstanceName='AbnormalResultsTestsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AbnormalResultsTestsLabelResource"  Text="32.-&#191; Resultados anormales en los ex&#225;menes de: radiograf&#237;a de t&#243;rax, glicemia o electrocardiograma?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='AbnormalResultsTests'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='AbnormalResultsTests' runat='server' Text="32.-¿ Resultados anormales en los exámenes de: radiografía de tórax, glicemia o electrocardiograma?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="AbnormalResultsTestsResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='AccentuatedDeafnessLabel' EncodeHtml='false' ClientInstanceName='AccentuatedDeafnessLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AccentuatedDeafnessLabelResource"  Text="9.-&#191; Sordera acentuada?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='AccentuatedDeafness'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='AccentuatedDeafness' runat='server' Text="9.-¿ Sordera acentuada?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="AccentuatedDeafnessResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='ActivityDangerousProfessionLabel' EncodeHtml='false' ClientInstanceName='ActivityDangerousProfessionLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ActivityDangerousProfessionLabelResource"  Text="30.-&#191; Practica alguna actividad, oficio o profesi&#243;n que sea riesgosa o peligrosa?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='ActivityDangerousProfession'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='ActivityDangerousProfession' runat='server' Text="30.-¿ Practica alguna actividad, oficio o profesión que sea riesgosa o peligrosa?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="ActivityDangerousProfessionResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='AlcoholConsumptionLabel' EncodeHtml='false' ClientInstanceName='AlcoholConsumptionLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AlcoholConsumptionLabelResource"  Text="Per&#237;odos de consumo de bebidas alcoh&#243;licas: Diariamente, Semanalmente u Ocasionalmente"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='AlcoholConsumption'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='AlcoholConsumption' ClientInstanceName='AlcoholConsumption' ToolTip="Períodos de consumo de bebidas alcohólicas: Diariamente, Semanalmente u Ocasionalmente" Size='1' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="AlcoholConsumptionResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-9..9g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="Form1" >
        <RequiredField IsRequired='true' ErrorText="El campo es requerido." />
        <RegularExpression ValidationExpression="^[1-9]\d*([,\.]\d+)?$" ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='AnotherSeriousIllnessLabel' EncodeHtml='false' ClientInstanceName='AnotherSeriousIllnessLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AnotherSeriousIllnessLabelResource"  Text="19.-&#191; Alguna otra enfermedad grave no mencionada m&#225;s arriba?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='AnotherSeriousIllness'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='AnotherSeriousIllness' runat='server' Text="19.-¿ Alguna otra enfermedad grave no mencionada más arriba?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="AnotherSeriousIllnessResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='AreYouPregnantLabel' EncodeHtml='false' ClientInstanceName='AreYouPregnantLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AreYouPregnantLabelResource"  Text="35.- En caso de sexo femenino: &#191;S e encuentra embarazada?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='AreYouPregnant'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='AreYouPregnant' runat='server' Text="35.- En caso de sexo femenino: ¿S e encuentra embarazada?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="AreYouPregnantResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='BenignMalignantTumorsLabel' EncodeHtml='false' ClientInstanceName='BenignMalignantTumorsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="BenignMalignantTumorsLabelResource"  Text="6.-&#191; Ha tenido o se encuentra en tratamiento por c&#225;ncer, tumores u otro tipo de alteraciones benignas o malignas?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='BenignMalignantTumors'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='BenignMalignantTumors' runat='server' Text="6.-¿ Ha tenido o se encuentra en tratamiento por cáncer, tumores u otro tipo de alteraciones benignas o malignas?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="BenignMalignantTumorsResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='CirrhosisOrStomachUlcerLabel' EncodeHtml='false' ClientInstanceName='CirrhosisOrStomachUlcerLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CirrhosisOrStomachUlcerLabelResource"  Text="15.-&#191; Cirrosis, &#250;lcera al est&#243;mago, diarrea por m&#225;s de un mes?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='CirrhosisOrStomachUlcer'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='CirrhosisOrStomachUlcer' runat='server' Text="15.-¿ Cirrosis, úlcera al estómago, diarrea por más de un mes?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="CirrhosisOrStomachUlcerResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DangerousActivityPracticedLabel' EncodeHtml='false' ClientInstanceName='DangerousActivityPracticedLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DangerousActivityPracticedLabelResource"  Text="Actividad, oficio o profesi&#243;n peligrosa que realiza la persona"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DangerousActivityPracticed'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DangerousActivityPracticed' ClientInstanceName='DangerousActivityPracticed' ToolTip="Actividad, oficio o profesión peligrosa que realiza la persona" Size='2' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="DangerousActivityPracticedResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-99..99g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="Form1" >
        <RequiredField IsRequired='true' ErrorText="El campo es requerido." />
        <RegularExpression ValidationExpression="^[1-9]\d*([,\.]\d+)?$" ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DangerousSportLabel' EncodeHtml='false' ClientInstanceName='DangerousSportLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DangerousSportLabelResource"  Text="31.-&#191; Practica alguna actividad deportiva que sea riesgosa o peligrosa?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DangerousSport'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='DangerousSport' runat='server' Text="31.-¿ Practica alguna actividad deportiva que sea riesgosa o peligrosa?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="DangerousSportResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DangerousSportPracticedLabel' EncodeHtml='false' ClientInstanceName='DangerousSportPracticedLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DangerousSportPracticedLabelResource"  Text="Deporte peligroso que realiza la persona"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DangerousSportPracticed'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='DangerousSportPracticed' ClientInstanceName='DangerousSportPracticed' ToolTip="Deporte peligroso que realiza la persona" Size='2' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="DangerousSportPracticedResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-99..99g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="Form1" >
        <RequiredField IsRequired='true' ErrorText="El campo es requerido." />
        <RegularExpression ValidationExpression="^[1-9]\d*([,\.]\d+)?$" ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DiseasesMusculoskeletalSystemLabel' EncodeHtml='false' ClientInstanceName='DiseasesMusculoskeletalSystemLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DiseasesMusculoskeletalSystemLabelResource"  Text="12.-&#191; Lesiones traum&#225;ticas, enfermedades relacionadas con las articulaciones o del sistema m&#250;sculo esquel&#233;tico?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DiseasesMusculoskeletalSystem'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='DiseasesMusculoskeletalSystem' runat='server' Text="12.-¿ Lesiones traumáticas, enfermedades relacionadas con las articulaciones o del sistema músculo esquelético?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="DiseasesMusculoskeletalSystemResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DrinkAlcoholicBeveragesLabel' EncodeHtml='false' ClientInstanceName='DrinkAlcoholicBeveragesLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DrinkAlcoholicBeveragesLabelResource"  Text="24.-&#191; Ingiere o ha ingerido bebidas alcoh&#243;licas?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DrinkAlcoholicBeverages'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='DrinkAlcoholicBeverages' runat='server' Text="24.-¿ Ingiere o ha ingerido bebidas alcohólicas?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="DrinkAlcoholicBeveragesResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DrugsOrNarcoticsLabel' EncodeHtml='false' ClientInstanceName='DrugsOrNarcoticsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DrugsOrNarcoticsLabelResource"  Text="20.-&#191; Consume o ha consumido drogas u estupefacientes de manera ocacional o permanente?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DrugsOrNarcotics'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='DrugsOrNarcotics' runat='server' Text="20.-¿ Consume o ha consumido drogas u estupefacientes de manera ocacional o permanente?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="DrugsOrNarcoticsResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='EyeDiseaseLabel' EncodeHtml='false' ClientInstanceName='EyeDiseaseLabel' runat='server' ClientIDMode='Static' meta:resourcekey="EyeDiseaseLabelResource"  Text="8.-&#191; Ceguera total o parcial, p&#233;rdida de un ojo, catarata o alguna enfermedad de los ojos?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='EyeDisease'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='EyeDisease' runat='server' Text="8.-¿ Ceguera total o parcial, pérdida de un ojo, catarata o alguna enfermedad de los ojos?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="EyeDiseaseResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HealthStatusCurrentlyLabel' EncodeHtml='false' ClientInstanceName='HealthStatusCurrentlyLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HealthStatusCurrentlyLabelResource"  Text="28.-&#191; C&#243;mo considera su estado de salud actual: Bueno, Regular o Deficiente?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HealthStatusCurrently'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='HealthStatusCurrently' ClientInstanceName='HealthStatusCurrently' ToolTip="28.-¿Cómo considera su estado de salud actual: Bueno, Regular o Deficiente?" Size='1' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="HealthStatusCurrentlyResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-9..9g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="Form1" >
        <RequiredField IsRequired='true' ErrorText="El campo es requerido." />
        <RegularExpression ValidationExpression="^[1-9]\d*([,\.]\d+)?$" ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HeartDiseaseLabel' EncodeHtml='false' ClientInstanceName='HeartDiseaseLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HeartDiseaseLabelResource"  Text="11.-&#191; Hipertensi&#243;n arterial o alguna enfermedad al coraz&#243;n?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HeartDisease'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='HeartDisease' runat='server' Text="11.-¿ Hipertensión arterial o alguna enfermedad al corazón?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="HeartDiseaseResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HeightInsuredLabel' EncodeHtml='false' ClientInstanceName='HeightInsuredLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HeightInsuredLabelResource"  Text="Estatura del Asegurado"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HeightInsured'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='HeightInsured' ClientInstanceName='HeightInsured' ToolTip="Estatura del Asegurado" Size='8' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="HeightInsuredResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-99999..99999g>.<00..99>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="Form1" >
        <RequiredField IsRequired='true' ErrorText="El campo es requerido." />
        <RegularExpression ValidationExpression="^[1-9]\d*([,\.]\d+)?$" ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HeRetiredLabel' EncodeHtml='false' ClientInstanceName='HeRetiredLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HeRetiredLabelResource"  Text="29.-&#191; Es jubilado?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HeRetired'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='HeRetired' runat='server' Text="29.-¿ Es jubilado?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="HeRetiredResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HeRetiredMedicalReasonsLabel' EncodeHtml='false' ClientInstanceName='HeRetiredMedicalReasonsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HeRetiredMedicalReasonsLabelResource"  Text="&#191;J ubil&#243; por razones m&#233;dicas?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HeRetiredMedicalReasons'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='HeRetiredMedicalReasons' runat='server' Text="¿J ubiló por razones médicas?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="HeRetiredMedicalReasonsResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HIVDiseaseOrHepatitisBOrCLabel' EncodeHtml='false' ClientInstanceName='HIVDiseaseOrHepatitisBOrCLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HIVDiseaseOrHepatitisBOrCLabelResource"  Text="18.-&#191; Es portador del Virus de Inmunodeficiencia Humana (VIH ), ha tenido Hepatitis B o C?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HIVDiseaseOrHepatitisBOrC'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='HIVDiseaseOrHepatitisBOrC' runat='server' Text="18.-¿ Es portador del Virus de Inmunodeficiencia Humana (VIH ), ha tenido Hepatitis B o C?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="HIVDiseaseOrHepatitisBOrCResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HospitalizedOnceLabel' EncodeHtml='false' ClientInstanceName='HospitalizedOnceLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HospitalizedOnceLabelResource"  Text="3.-&#191; Ha sido hospitalizado alguna vez?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HospitalizedOnce'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='HospitalizedOnce' runat='server' Text="3.-¿ Ha sido hospitalizado alguna vez?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="HospitalizedOnceResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HowLongNonsmokerLabel' EncodeHtml='false' ClientInstanceName='HowLongNonsmokerLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HowLongNonsmokerLabelResource"  Text="&#191;C u&#225;nto hace que no fuma?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HowLongNonsmoker'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='HowLongNonsmoker' ClientInstanceName='HowLongNonsmoker' ToolTip="¿Cuánto hace que no fuma?" Size='2' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="HowLongNonsmokerResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-99..99g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="Form1" >
        <RequiredField IsRequired='true' ErrorText="El campo es requerido." />
        <RegularExpression ValidationExpression="^[1-9]\d*([,\.]\d+)?$" ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HowManyCigarettesDayLabel' EncodeHtml='false' ClientInstanceName='HowManyCigarettesDayLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HowManyCigarettesDayLabelResource"  Text="&#191;C u&#225;ntos cigarrillos al d&#237;a?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HowManyCigarettesDay'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='HowManyCigarettesDay' ClientInstanceName='HowManyCigarettesDay' ToolTip="¿Cuántos cigarrillos al día?" Size='2' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="HowManyCigarettesDayResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-99..99g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="Form1" >
        <RequiredField IsRequired='true' ErrorText="El campo es requerido." />
        <RegularExpression ValidationExpression="^[1-9]\d*([,\.]\d+)?$" ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HowManyCigarettesSmokedDayLabel' EncodeHtml='false' ClientInstanceName='HowManyCigarettesSmokedDayLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HowManyCigarettesSmokedDayLabelResource"  Text="&#191;C u&#225;ntos cigarrillos al d&#237;a?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HowManyCigarettesSmokedDay'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='HowManyCigarettesSmokedDay' ClientInstanceName='HowManyCigarettesSmokedDay' ToolTip="¿Cuántos cigarrillos al día?" Size='2' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="HowManyCigarettesSmokedDayResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-99..99g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="Form1" >
        <RequiredField IsRequired='true' ErrorText="El campo es requerido." />
        <RegularExpression ValidationExpression="^[1-9]\d*([,\.]\d+)?$" ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HowManyWeeksPregnantLabel' EncodeHtml='false' ClientInstanceName='HowManyWeeksPregnantLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HowManyWeeksPregnantLabelResource"  Text="&#191;C u&#225;ntas semanas tiene?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HowManyWeeksPregnant'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='HowManyWeeksPregnant' ClientInstanceName='HowManyWeeksPregnant' ToolTip="¿Cuántas semanas tiene?" Size='2' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="HowManyWeeksPregnantResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-99..99g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="Form1" >
        <RequiredField IsRequired='true' ErrorText="El campo es requerido." />
        <RegularExpression ValidationExpression="^[1-9]\d*([,\.]\d+)?$" ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HowManyYearsSmokesLabel' EncodeHtml='false' ClientInstanceName='HowManyYearsSmokesLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HowManyYearsSmokesLabelResource"  Text="&#191;H ace cu&#225;ntos a&#241;os fuma?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HowManyYearsSmokes'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='HowManyYearsSmokes' ClientInstanceName='HowManyYearsSmokes' ToolTip="¿Hace cuántos años fuma?" Size='2' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="HowManyYearsSmokesResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-99..99g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="Form1" >
        <RequiredField IsRequired='true' ErrorText="El campo es requerido." />
        <RegularExpression ValidationExpression="^[1-9]\d*([,\.]\d+)?$" ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HowOldSmokedLabel' EncodeHtml='false' ClientInstanceName='HowOldSmokedLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HowOldSmokedLabelResource"  Text="&#191;C u&#225;ntos a&#241;os fum&#243;?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HowOldSmoked'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='HowOldSmoked' ClientInstanceName='HowOldSmoked' ToolTip="¿Cuántos años fumó?" Size='2' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="HowOldSmokedResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-99..99g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="Form1" >
        <RequiredField IsRequired='true' ErrorText="El campo es requerido." />
        <RegularExpression ValidationExpression="^[1-9]\d*([,\.]\d+)?$" ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LifeInsuranceAcceptedWithSurchargeLabel' EncodeHtml='false' ClientInstanceName='LifeInsuranceAcceptedWithSurchargeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LifeInsuranceAcceptedWithSurchargeLabelResource"  Text="&#191;F ue aplazado, rechazado o aceptado con alg&#250;n tipo de recargo?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LifeInsuranceAcceptedWithSurcharge'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='LifeInsuranceAcceptedWithSurcharge' runat='server' Text="¿F ue aplazado, rechazado o aceptado con algún tipo de recargo?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="LifeInsuranceAcceptedWithSurchargeResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LifeInsuranceCompanyLabel' EncodeHtml='false' ClientInstanceName='LifeInsuranceCompanyLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LifeInsuranceCompanyLabelResource"  Text="&#191;E n qu&#233; compa&#241;&#237;a?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LifeInsuranceCompany'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='LifeInsuranceCompany' ClientInstanceName='LifeInsuranceCompany' ToolTip="¿En qué compañía?" Size='30' NullText="" ClientVisible='True' MaxLength='30' ClientIDMode='Static' meta:resourcekey="LifeInsuranceCompanyResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Form1" >
     <RequiredField IsRequired='True' ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LifeInsuranceInForceLabel' EncodeHtml='false' ClientInstanceName='LifeInsuranceInForceLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LifeInsuranceInForceLabelResource"  Text="33.-&#191; Tiene alg&#250;n seguro de vida vigente?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LifeInsuranceInForce'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='LifeInsuranceInForce' runat='server' Text="33.-¿ Tiene algún seguro de vida vigente?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="LifeInsuranceInForceResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LungDiseasesLabel' EncodeHtml='false' ClientInstanceName='LungDiseasesLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LungDiseasesLabelResource"  Text="13.-&#191; Asma bronquial, bronquitis cr&#243;nica o enfermedades del pulm&#243;n?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LungDiseases'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='LungDiseases' runat='server' Text="13.-¿ Asma bronquial, bronquitis crónica o enfermedades del pulmón?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="LungDiseasesResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='MedicalTreatmentDrugsLabel' EncodeHtml='false' ClientInstanceName='MedicalTreatmentDrugsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="MedicalTreatmentDrugsLabelResource"  Text="21.-&#191; Ha sido sometido a tratamiento m&#233;dico debido al consumo de drogas o estupefacientes?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='MedicalTreatmentDrugs'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='MedicalTreatmentDrugs' runat='server' Text="21.-¿ Ha sido sometido a tratamiento médico debido al consumo de drogas o estupefacientes?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="MedicalTreatmentDrugsResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='MetabolicDiseaseEndocrineSystemLabel' EncodeHtml='false' ClientInstanceName='MetabolicDiseaseEndocrineSystemLabel' runat='server' ClientIDMode='Static' meta:resourcekey="MetabolicDiseaseEndocrineSystemLabelResource"  Text="10.-&#191; Di&#225;betes, bocio, hipertiroidismo u otra enfermedad del sistema endocrino metab&#243;lico?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='MetabolicDiseaseEndocrineSystem'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='MetabolicDiseaseEndocrineSystem' runat='server' Text="10.-¿ Diábetes, bocio, hipertiroidismo u otra enfermedad del sistema endocrino metabólico?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="MetabolicDiseaseEndocrineSystemResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='NephritisOrBladderDiseasesLabel' EncodeHtml='false' ClientInstanceName='NephritisOrBladderDiseasesLabel' runat='server' ClientIDMode='Static' meta:resourcekey="NephritisOrBladderDiseasesLabelResource"  Text="16.-&#191; Nefritis o cualquier enfermedad de la vejiga, pr&#243;stata, ri&#241;&#243;n o v&#237;as urinarias?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='NephritisOrBladderDiseases'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='NephritisOrBladderDiseases' runat='server' Text="16.-¿ Nefritis o cualquier enfermedad de la vejiga, próstata, riñón o vías urinarias?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="NephritisOrBladderDiseasesResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='NerveDiseasesLabel' EncodeHtml='false' ClientInstanceName='NerveDiseasesLabel' runat='server' ClientIDMode='Static' meta:resourcekey="NerveDiseasesLabelResource"  Text="7.-&#191; Tratamiento en trastornos mentales, convulsiones, par&#225;lisis u otra enfermedad nerviosa?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='NerveDiseases'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='NerveDiseases' runat='server' Text="7.-¿ Tratamiento en trastornos mentales, convulsiones, parálisis u otra enfermedad nerviosa?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="NerveDiseasesResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='ObservationsLabel' EncodeHtml='false' ClientInstanceName='ObservationsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ObservationsLabelResource"  Text="Observaciones"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Observations'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxMemo runat='server' ClientIDMode='Static' ID='Observations' ToolTip="Observaciones" meta:resourcekey="ObservationsResource" Columns='100' Rows='2' Size='250' NullText="" ClientVisible='True' ClientEnabled='True'  >
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="Form1" >
     <RequiredField IsRequired='True' ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxMemo>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='OperationGenitalsLabel' EncodeHtml='false' ClientInstanceName='OperationGenitalsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="OperationGenitalsLabelResource"  Text="14.-&#191; Enfermedad a los senos o alguna operaci&#243;n a los genitales?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='OperationGenitals'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='OperationGenitals' runat='server' Text="14.-¿ Enfermedad a los senos o alguna operación a los genitales?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="OperationGenitalsResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='PhysicalAbnormalitiesLabel' EncodeHtml='false' ClientInstanceName='PhysicalAbnormalitiesLabel' runat='server' ClientIDMode='Static' meta:resourcekey="PhysicalAbnormalitiesLabelResource"  Text="5.-&#191; Presenta alguna anomal&#237;a de constituci&#243;n, deformaci&#243;n, amputaci&#243;n o una discapacidad f&#237;sica?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='PhysicalAbnormalities'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='PhysicalAbnormalities' runat='server' Text="5.-¿ Presenta alguna anomalía de constitución, deformación, amputación o una discapacidad física?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="PhysicalAbnormalitiesResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='RejectedByHiringLifeInsuranceLabel' EncodeHtml='false' ClientInstanceName='RejectedByHiringLifeInsuranceLabel' runat='server' ClientIDMode='Static' meta:resourcekey="RejectedByHiringLifeInsuranceLabelResource"  Text="34.-&#191; Ha sido rechazado al contratar alg&#250;n seguro de vida?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='RejectedByHiringLifeInsurance'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='RejectedByHiringLifeInsurance' runat='server' Text="34.-¿ Ha sido rechazado al contratar algún seguro de vida?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="RejectedByHiringLifeInsuranceResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='SeriousAccidentLabel' EncodeHtml='false' ClientInstanceName='SeriousAccidentLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SeriousAccidentLabelResource"  Text="4.-&#191; Ha tenido alg&#250;n accidente que le haya dejado secuelas f&#237;sicas?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='SeriousAccident'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='SeriousAccident' runat='server' Text="4.-¿ Ha tenido algún accidente que le haya dejado secuelas físicas?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="SeriousAccidentResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='SexuallyTransmittedDiseaseLabel' EncodeHtml='false' ClientInstanceName='SexuallyTransmittedDiseaseLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SexuallyTransmittedDiseaseLabelResource"  Text="17.-&#191; Enfermedad de transmisi&#243;n sexual?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='SexuallyTransmittedDisease'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='SexuallyTransmittedDisease' runat='server' Text="17.-¿ Enfermedad de transmisión sexual?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="SexuallyTransmittedDiseaseResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='SickConsumptionAlcoholicBeveragesLabel' EncodeHtml='false' ClientInstanceName='SickConsumptionAlcoholicBeveragesLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SickConsumptionAlcoholicBeveragesLabelResource"  Text="25.-&#191; Ha estado enfermo o en tratamiento m&#233;dico por la ingesta de bebidas alcoh&#243;licas?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='SickConsumptionAlcoholicBeverages'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='SickConsumptionAlcoholicBeverages' runat='server' Text="25.-¿ Ha estado enfermo o en tratamiento médico por la ingesta de bebidas alcohólicas?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="SickConsumptionAlcoholicBeveragesResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='SurgicallyOperatedLabel' EncodeHtml='false' ClientInstanceName='SurgicallyOperatedLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SurgicallyOperatedLabelResource"  Text="2.-&#191; Ha sido operado quir&#250;rgicamente?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='SurgicallyOperated'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='SurgicallyOperated' runat='server' Text="2.-¿ Ha sido operado quirúrgicamente?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="SurgicallyOperatedResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='TimeInLifeInsuranceLabel' EncodeHtml='false' ClientInstanceName='TimeInLifeInsuranceLabel' runat='server' ClientIDMode='Static' meta:resourcekey="TimeInLifeInsuranceLabelResource"  Text="&#191;H ace cu&#225;nto tiempo lo posee?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='TimeInLifeInsurance'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='TimeInLifeInsurance' ClientInstanceName='TimeInLifeInsurance' ToolTip="¿Hace cuánto tiempo lo posee?" Size='3' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="TimeInLifeInsuranceResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-999..999g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="Form1" >
        <RequiredField IsRequired='true' ErrorText="El campo es requerido." />
        <RegularExpression ValidationExpression="^[1-9]\d*([,\.]\d+)?$" ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='UsedToSmokeLabel' EncodeHtml='false' ClientInstanceName='UsedToSmokeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="UsedToSmokeLabelResource"  Text="23.-&#191; Fumaba?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='UsedToSmoke'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='UsedToSmoke' runat='server' Text="23.-¿ Fumaba?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="UsedToSmokeResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='WeightInsuredLabel' EncodeHtml='false' ClientInstanceName='WeightInsuredLabel' runat='server' ClientIDMode='Static' meta:resourcekey="WeightInsuredLabelResource"  Text="Peso del Asegurado"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='WeightInsured'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='WeightInsured' ClientInstanceName='WeightInsured' ToolTip="Peso del Asegurado" Size='8' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="WeightInsuredResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-99999..99999g>.<00..99>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="Form1" >
        <RequiredField IsRequired='true' ErrorText="El campo es requerido." />
        <RegularExpression ValidationExpression="^[1-9]\d*([,\.]\d+)?$" ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='WeightVariationLabel' EncodeHtml='false' ClientInstanceName='WeightVariationLabel' runat='server' ClientIDMode='Static' meta:resourcekey="WeightVariationLabelResource"  Text="1.-&#191; Ha variado su peso por m&#225;s de 5 Kg. en los &#250;ltimos seis meses?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='WeightVariation'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='WeightVariation' runat='server' Text="1.-¿ Ha variado su peso por más de 5 Kg. en los últimos seis meses?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="WeightVariationResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='YouIngestedMedicationIllnessDiseaseLabel' EncodeHtml='false' ClientInstanceName='YouIngestedMedicationIllnessDiseaseLabel' runat='server' ClientIDMode='Static' meta:resourcekey="YouIngestedMedicationIllnessDiseaseLabelResource"  Text="26.-&#191; Ingiere usted alg&#250;n medicamento por alguna enfermedad o dolencia?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='YouIngestedMedicationIllnessDisease'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='YouIngestedMedicationIllnessDisease' runat='server' Text="26.-¿ Ingiere usted algún medicamento por alguna enfermedad o dolencia?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="YouIngestedMedicationIllnessDiseaseResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='YouIngestedMedicationRegularLabel' EncodeHtml='false' ClientInstanceName='YouIngestedMedicationRegularLabel' runat='server' ClientIDMode='Static' meta:resourcekey="YouIngestedMedicationRegularLabelResource"  Text="27.-&#191; Ingiere usted alg&#250;n medicamento en forma peri&#243;dica?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='YouIngestedMedicationRegular'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='YouIngestedMedicationRegular' runat='server' Text="27.-¿ Ingiere usted algún medicamento en forma periódica?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="YouIngestedMedicationRegularResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='YouSmokeLabel' EncodeHtml='false' ClientInstanceName='YouSmokeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="YouSmokeLabelResource"  Text="22.-&#191; Usted fuma?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='YouSmoke'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxCheckBox ID='YouSmoke' runat='server' Text="22.-¿ Usted fuma?" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="YouSmokeResource"  EncodeHtml='false' > 
       </dxe:ASPxCheckBox>
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
                           
                           if (''!=''){
                            document.getElementById(btnCancel.name).style.visibility = 'hidden';
                            document.getElementById(btnConfirm.name).style.visibility = 'hidden';
                            document.getElementById(lblMessage.name).innerHTML = '';                     
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