<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SMFDIDPSUserControl.ascx.vb" Inherits="SMFDIDPSUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">



</script>

<script src="/generated/form/SMFDIDPS.js" type="text/javascript"></script>      
<asp:UpdatePanel runat="server">


  
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='SMFDIDPSTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="DatosBase" ClientInstanceName="DatosBase" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="DatosBaseResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='8'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:12.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='WeightInsuredLabel' EncodeHtml='false' ClientInstanceName='WeightInsuredLabel' runat='server' ClientIDMode='Static' meta:resourcekey="WeightInsuredLabelResource"  Text="Peso"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='WeightInsured'       ></dxe:ASPxLabel></td>    <td style='width:12.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='WeightInsured' ClientInstanceName='WeightInsured' ToolTip="Peso del Asegurado" Size='8' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="WeightInsuredResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-99999..99999g>.<00..99>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="DatosBase" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='kilogramos' EncodeHtml='false' ClientInstanceName='kilogramos' runat='server' ClientIDMode='Static' meta:resourcekey="kilogramosResource"  Text="Kg."  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



<td style='width:12.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HeightInsuredLabel' EncodeHtml='false' ClientInstanceName='HeightInsuredLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HeightInsuredLabelResource"  Text="Estatura"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HeightInsured'       ></dxe:ASPxLabel></td>    <td style='width:12.5%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='HeightInsured' ClientInstanceName='HeightInsured' ToolTip="Estatura del Asegurado" Size='8' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="HeightInsuredResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-99999..99999g>.<00..99>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="DatosBase" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='metros' EncodeHtml='false' ClientInstanceName='metros' runat='server' ClientIDMode='Static' meta:resourcekey="metrosResource"  Text="mts."  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



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
                    <dxrp:ASPxRoundPanel ID="zone2" ClientInstanceName="zone2" runat="server" HeaderText="RESPONDER &quot;SI&quot; O &quot;NO&quot;" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone2Resource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='WeightVariationLabel' EncodeHtml='false' ClientInstanceName='WeightVariationLabel' runat='server' ClientIDMode='Static' meta:resourcekey="WeightVariationLabelResource"  Text="1.-&#191; Ha variado su peso por m&#225;s de 5 Kg. en los &#250;ltimos seis meses?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='WeightVariation'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='WeightVariation' ClientInstanceName='WeightVariation' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="1.-¿Ha variado su peso por más de 5 Kg. en los últimos seis meses?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="WeightVariationResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone2" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='SurgicallyOperatedLabel' EncodeHtml='false' ClientInstanceName='SurgicallyOperatedLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SurgicallyOperatedLabelResource"  Text="2.-&#191; Ha sido operado quir&#250;rgicamente?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='SurgicallyOperated'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='SurgicallyOperated' ClientInstanceName='SurgicallyOperated' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="2.-¿Ha sido operado quirúrgicamente?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="SurgicallyOperatedResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone2" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HospitalizedOnceLabel' EncodeHtml='false' ClientInstanceName='HospitalizedOnceLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HospitalizedOnceLabelResource"  Text="3.-&#191; Ha sido hospitalizado alguna vez?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HospitalizedOnce'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='HospitalizedOnce' ClientInstanceName='HospitalizedOnce' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="3.-¿Ha sido hospitalizado alguna vez?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="HospitalizedOnceResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone2" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='SeriousAccidentLabel' EncodeHtml='false' ClientInstanceName='SeriousAccidentLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SeriousAccidentLabelResource"  Text="4.-&#191; Ha tenido alg&#250;n accidente que le haya dejado secuelas f&#237;sicas?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='SeriousAccident'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='SeriousAccident' ClientInstanceName='SeriousAccident' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="4.-¿Ha tenido algún accidente que le haya dejado secuelas físicas?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="SeriousAccidentResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone2" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='PhysicalAbnormalitiesLabel' EncodeHtml='false' ClientInstanceName='PhysicalAbnormalitiesLabel' runat='server' ClientIDMode='Static' meta:resourcekey="PhysicalAbnormalitiesLabelResource"  Text="5.-&#191; Presenta alguna anomal&#237;a de constituci&#243;n, deformaci&#243;n, amputaci&#243;n o una discapacidad f&#237;sica?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='PhysicalAbnormalities'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='PhysicalAbnormalities' ClientInstanceName='PhysicalAbnormalities' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="5.-¿Presenta alguna anomalía de constitución, deformación, amputación o una discapacidad física?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="PhysicalAbnormalitiesResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone2" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='BenignMalignantTumorsLabel' EncodeHtml='false' ClientInstanceName='BenignMalignantTumorsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="BenignMalignantTumorsLabelResource"  Text="6.-&#191; Ha tenido o se encuentra en tratamiento por c&#225;ncer, tumores u otro tipo de alteraciones benignas o malignas?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='BenignMalignantTumors'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='BenignMalignantTumors' ClientInstanceName='BenignMalignantTumors' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="6.-¿Ha tenido o se encuentra en tratamiento por cáncer, tumores u otro tipo de alteraciones benignas o malignas?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="BenignMalignantTumorsResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone2" >
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
                    <dxrp:ASPxRoundPanel ID="DPS" ClientInstanceName="DPS" runat="server" HeaderText="PADECE EN LA ACTUALIDAD, HA SIDO TRATADO O ESTÁ EN TRATAMIENTO DE " ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="DPSResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='NerveDiseasesLabel' EncodeHtml='false' ClientInstanceName='NerveDiseasesLabel' runat='server' ClientIDMode='Static' meta:resourcekey="NerveDiseasesLabelResource"  Text="7.-&#191; Tratamiento en trastornos mentales, convulsiones, par&#225;lisis u otra enfermedad nerviosa?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='NerveDiseases'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='NerveDiseases' ClientInstanceName='NerveDiseases' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="7.-¿Tratamiento en trastornos mentales, convulsiones, parálisis u otra enfermedad nerviosa?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="NerveDiseasesResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='EyeDiseaseLabel' EncodeHtml='false' ClientInstanceName='EyeDiseaseLabel' runat='server' ClientIDMode='Static' meta:resourcekey="EyeDiseaseLabelResource"  Text="8.-&#191; Ceguera total o parcial, p&#233;rdida de un ojo, catarata o alguna enfermedad de los ojos?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='EyeDisease'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='EyeDisease' ClientInstanceName='EyeDisease' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="8.-¿Ceguera total o parcial, pérdida de un ojo, catarata o alguna enfermedad de los ojos?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="EyeDiseaseResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='AccentuatedDeafnessLabel' EncodeHtml='false' ClientInstanceName='AccentuatedDeafnessLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AccentuatedDeafnessLabelResource"  Text="9.-&#191; Sordera acentuada?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='AccentuatedDeafness'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='AccentuatedDeafness' ClientInstanceName='AccentuatedDeafness' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="9.-¿Sordera acentuada?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="AccentuatedDeafnessResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='MetabolicDiseaseEndocrineSystemLabel' EncodeHtml='false' ClientInstanceName='MetabolicDiseaseEndocrineSystemLabel' runat='server' ClientIDMode='Static' meta:resourcekey="MetabolicDiseaseEndocrineSystemLabelResource"  Text="10.-&#191; Di&#225;betes, bocio, hipertiroidismo u otra enfermedad del sistema endocrino metab&#243;lico?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='MetabolicDiseaseEndocrineSystem'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='MetabolicDiseaseEndocrineSystem' ClientInstanceName='MetabolicDiseaseEndocrineSystem' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="10.-¿Diábetes, bocio, hipertiroidismo u otra enfermedad del sistema endocrino metabólico?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="MetabolicDiseaseEndocrineSystemResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HeartDiseaseLabel' EncodeHtml='false' ClientInstanceName='HeartDiseaseLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HeartDiseaseLabelResource"  Text="11.-&#191; Hipertensi&#243;n arterial o alguna enfermedad al coraz&#243;n?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HeartDisease'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='HeartDisease' ClientInstanceName='HeartDisease' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="11.-¿Hipertensión arterial o alguna enfermedad al corazón?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="HeartDiseaseResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DiseasesMusculoSkeletalSystemLabel' EncodeHtml='false' ClientInstanceName='DiseasesMusculoSkeletalSystemLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DiseasesMusculoSkeletalSystemLabelResource"  Text="12.-&#191; Lesiones traum&#225;ticas, enfermedades relacionadas con las articulaciones o del sistema m&#250;sculo esquel&#233;tico?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DiseasesMusculoSkeletalSystem'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='DiseasesMusculoSkeletalSystem' ClientInstanceName='DiseasesMusculoSkeletalSystem' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="12.-¿Lesiones traumáticas, enfermedades relacionadas con las articulaciones o del sistema músculo esquelético?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="DiseasesMusculoSkeletalSystemResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LungDiseasesLabel' EncodeHtml='false' ClientInstanceName='LungDiseasesLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LungDiseasesLabelResource"  Text="13.-&#191; Asma bronquial, bronquitis cr&#243;nica o enfermedades del pulm&#243;n?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LungDiseases'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='LungDiseases' ClientInstanceName='LungDiseases' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="13.-¿Asma bronquial, bronquitis crónica o enfermedades del pulmón?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="LungDiseasesResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='OperationGenitalsLabel' EncodeHtml='false' ClientInstanceName='OperationGenitalsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="OperationGenitalsLabelResource"  Text="14.-&#191; Enfermedad a los senos o alguna operaci&#243;n a los genitales?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='OperationGenitals'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='OperationGenitals' ClientInstanceName='OperationGenitals' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="14.-¿Enfermedad a los senos o alguna operación a los genitales?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="OperationGenitalsResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='CirrhosisOrStomachUlcerLabel' EncodeHtml='false' ClientInstanceName='CirrhosisOrStomachUlcerLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CirrhosisOrStomachUlcerLabelResource"  Text="15.-&#191; Cirrosis, &#250;lcera al est&#243;mago, diarrea por m&#225;s de un mes?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='CirrhosisOrStomachUlcer'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='CirrhosisOrStomachUlcer' ClientInstanceName='CirrhosisOrStomachUlcer' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="15.-¿Cirrosis, úlcera al estómago, diarrea por más de un mes?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="CirrhosisOrStomachUlcerResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='NephritisOrBladderDiseasesLabel' EncodeHtml='false' ClientInstanceName='NephritisOrBladderDiseasesLabel' runat='server' ClientIDMode='Static' meta:resourcekey="NephritisOrBladderDiseasesLabelResource"  Text="16.-&#191; Nefritis o cualquier enfermedad de la vejiga, pr&#243;stata, ri&#241;&#243;n o v&#237;as urinarias?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='NephritisOrBladderDiseases'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='NephritisOrBladderDiseases' ClientInstanceName='NephritisOrBladderDiseases' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="16.-¿Nefritis o cualquier enfermedad de la vejiga, próstata, riñón o vías urinarias?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="NephritisOrBladderDiseasesResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='SexuallyTransmittedDiseaseLabel' EncodeHtml='false' ClientInstanceName='SexuallyTransmittedDiseaseLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SexuallyTransmittedDiseaseLabelResource"  Text="17.-&#191; Enfermedad de transmisi&#243;n sexual?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='SexuallyTransmittedDisease'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='SexuallyTransmittedDisease' ClientInstanceName='SexuallyTransmittedDisease' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="17.-¿Enfermedad de transmisión sexual?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="SexuallyTransmittedDiseaseResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HIVDiseaseOrHepatitisBOrCLabel' EncodeHtml='false' ClientInstanceName='HIVDiseaseOrHepatitisBOrCLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HIVDiseaseOrHepatitisBOrCLabelResource"  Text="18.-&#191; Es portador del Virus de Inmunodeficiencia Humana (VIH ), ha tenido Hepatitis B o C?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HIVDiseaseOrHepatitisBOrC'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='HIVDiseaseOrHepatitisBOrC' ClientInstanceName='HIVDiseaseOrHepatitisBOrC' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="18.-¿Es portador del Virus de Inmunodeficiencia Humana (VIH), ha tenido Hepatitis B o C?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="HIVDiseaseOrHepatitisBOrCResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='AnotherSeriousIllnessLabel' EncodeHtml='false' ClientInstanceName='AnotherSeriousIllnessLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AnotherSeriousIllnessLabelResource"  Text="19.-&#191; Alguna otra enfermedad grave no mencionada m&#225;s arriba?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='AnotherSeriousIllness'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='AnotherSeriousIllness' ClientInstanceName='AnotherSeriousIllness' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="19.-¿Alguna otra enfermedad grave no mencionada más arriba?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="AnotherSeriousIllnessResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DrugsOrNarcoticsLabel' EncodeHtml='false' ClientInstanceName='DrugsOrNarcoticsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DrugsOrNarcoticsLabelResource"  Text="20.-&#191; Consume o ha consumido drogas u estupefacientes de manera ocacional o permanente?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DrugsOrNarcotics'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='DrugsOrNarcotics' ClientInstanceName='DrugsOrNarcotics' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="20.-¿Consume o ha consumido drogas u estupefacientes de manera ocacional o permanente?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="DrugsOrNarcoticsResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='MedicalTreatmentDrugsLabel' EncodeHtml='false' ClientInstanceName='MedicalTreatmentDrugsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="MedicalTreatmentDrugsLabelResource"  Text="21.-&#191; Ha sido sometido a tratamiento m&#233;dico debido al consumo de drogas o estupefacientes?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='MedicalTreatmentDrugs'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='MedicalTreatmentDrugs' ClientInstanceName='MedicalTreatmentDrugs' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="21.-¿Ha sido sometido a tratamiento médico debido al consumo de drogas o estupefacientes?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="MedicalTreatmentDrugsResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='YouSmokeLabel' EncodeHtml='false' ClientInstanceName='YouSmokeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="YouSmokeLabelResource"  Text="22.-&#191;Usted fuma?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='YouSmoke'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='YouSmoke' ClientInstanceName='YouSmoke' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="22.-¿Usted fuma?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="YouSmokeResource"  ValueType='System.Int64'  AutoPostBack='true' OnSelectedIndexChanged='YouSmoke_SelectedIndexChanged'  TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HowManyYearsSmokesLabel' EncodeHtml='false' ClientInstanceName='HowManyYearsSmokesLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HowManyYearsSmokesLabelResource"  Text="       &#191;Hace cu&#225;ntos a&#241;os fuma?"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='HowManyYearsSmokes'       ></dxe:ASPxLabel></td>    <td style='width:150%;' colspan='15' align='left'><div style='float: left;'>

       <dxe:ASPxLabel ID='HowManyYearsSmokes' EncodeHtml='false' ClientInstanceName='HowManyYearsSmokes' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

</div>

       <dxe:ASPxLabel ID='AñosFuma' EncodeHtml='false' ClientInstanceName='AñosFuma' runat='server' ClientIDMode='Static' meta:resourcekey="AñosFumaResource"  Text="a&#241;os"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel>


</div>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HowManyCigarettesDayLabel' EncodeHtml='false' ClientInstanceName='HowManyCigarettesDayLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HowManyCigarettesDayLabelResource"  Text="       &#191;Cu&#225;ntos cigarrillos al d&#237;a?"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='HowManyCigarettesDay'       ></dxe:ASPxLabel></td>    <td style='width:150%;' colspan='13' align='left'><div style='float: left;'>

       <dxe:ASPxLabel ID='HowManyCigarettesDay' EncodeHtml='false' ClientInstanceName='HowManyCigarettesDay' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

</div>

       <dxe:ASPxLabel ID='CigarrillosFuma' EncodeHtml='false' ClientInstanceName='CigarrillosFuma' runat='server' ClientIDMode='Static' meta:resourcekey="CigarrillosFumaResource"  Text="cigarrillos"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel>


</div>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='UsedToSmokeLabel' EncodeHtml='false' ClientInstanceName='UsedToSmokeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="UsedToSmokeLabelResource"  Text="23.-&#191;Fumaba?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='UsedToSmoke'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='UsedToSmoke' ClientInstanceName='UsedToSmoke' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="23.-¿Fumaba?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="UsedToSmokeResource"  ValueType='System.Int64'  AutoPostBack='true' OnSelectedIndexChanged='UsedToSmoke_SelectedIndexChanged'  TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HowOldSmokedLabel' EncodeHtml='false' ClientInstanceName='HowOldSmokedLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HowOldSmokedLabelResource"  Text="       &#191;Cu&#225;ntos a&#241;os fum&#243;?"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='HowOldSmoked'       ></dxe:ASPxLabel></td>    <td style='width:150%;' colspan='11' align='left'><div style='float: left;'>

       <dxe:ASPxLabel ID='HowOldSmoked' EncodeHtml='false' ClientInstanceName='HowOldSmoked' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

</div>

       <dxe:ASPxLabel ID='AñosFumo' EncodeHtml='false' ClientInstanceName='AñosFumo' runat='server' ClientIDMode='Static' meta:resourcekey="AñosFumoResource"  Text="a&#241;os"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel>


</div>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HowManyCigarettesSmokedDayLabel' EncodeHtml='false' ClientInstanceName='HowManyCigarettesSmokedDayLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HowManyCigarettesSmokedDayLabelResource"  Text="       &#191;Cu&#225;ntos cigarrillos al d&#237;a?"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='HowManyCigarettesSmokedDay'       ></dxe:ASPxLabel></td>    <td style='width:150%;' colspan='9' align='left'><div style='float: left;'>

       <dxe:ASPxLabel ID='HowManyCigarettesSmokedDay' EncodeHtml='false' ClientInstanceName='HowManyCigarettesSmokedDay' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

</div>

       <dxe:ASPxLabel ID='CigarrillosFumo' EncodeHtml='false' ClientInstanceName='CigarrillosFumo' runat='server' ClientIDMode='Static' meta:resourcekey="CigarrillosFumoResource"  Text="cigarrillos"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel>


</div>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HowLongNonsmokerLabel' EncodeHtml='false' ClientInstanceName='HowLongNonsmokerLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HowLongNonsmokerLabelResource"  Text="       &#191;Cu&#225;nto hace que no fuma?"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='HowLongNonsmoker'       ></dxe:ASPxLabel></td>    <td style='width:150%;' colspan='7' align='left'><div style='float: left;'>

       <dxe:ASPxLabel ID='HowLongNonsmoker' EncodeHtml='false' ClientInstanceName='HowLongNonsmoker' runat='server' ClientIDMode='Static' >
       </dxe:ASPxLabel>

</div>

       <dxe:ASPxLabel ID='AñosNoFuma' EncodeHtml='false' ClientInstanceName='AñosNoFuma' runat='server' ClientIDMode='Static' meta:resourcekey="AñosNoFumaResource"  Text="a&#241;os"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel>


</div>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DrinkAlcoholicBeveragesLabel' EncodeHtml='false' ClientInstanceName='DrinkAlcoholicBeveragesLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DrinkAlcoholicBeveragesLabelResource"  Text="24.-&#191; Ingiere o ha ingerido bebidas alcoh&#243;licas?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DrinkAlcoholicBeverages'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='DrinkAlcoholicBeverages' ClientInstanceName='DrinkAlcoholicBeverages' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="24.-¿Ingiere o ha ingerido bebidas alcohólicas?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="DrinkAlcoholicBeveragesResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='SickConsumptionAlcoholicBeveragesLabel' EncodeHtml='false' ClientInstanceName='SickConsumptionAlcoholicBeveragesLabel' runat='server' ClientIDMode='Static' meta:resourcekey="SickConsumptionAlcoholicBeveragesLabelResource"  Text="25.-&#191; Ha estado enfermo o en tratamiento m&#233;dico por la ingesta de bebidas alcoh&#243;licas?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='SickConsumptionAlcoholicBeverages'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='SickConsumptionAlcoholicBeverages' ClientInstanceName='SickConsumptionAlcoholicBeverages' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="25.-¿Ha estado enfermo o en tratamiento médico por la ingesta de bebidas alcohólicas?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="SickConsumptionAlcoholicBeveragesResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='YouIngestedMedicationIllnessDiseaseLabel' EncodeHtml='false' ClientInstanceName='YouIngestedMedicationIllnessDiseaseLabel' runat='server' ClientIDMode='Static' meta:resourcekey="YouIngestedMedicationIllnessDiseaseLabelResource"  Text="26.-&#191; Ingiere usted alg&#250;n medicamento por alguna enfermedad o dolencia?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='YouIngestedMedicationIllnessDisease'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='YouIngestedMedicationIllnessDisease' ClientInstanceName='YouIngestedMedicationIllnessDisease' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="26.-¿Ingiere usted algún medicamento por alguna enfermedad o dolencia?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="YouIngestedMedicationIllnessDiseaseResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='YouIngestedMedicationRegularLabel' EncodeHtml='false' ClientInstanceName='YouIngestedMedicationRegularLabel' runat='server' ClientIDMode='Static' meta:resourcekey="YouIngestedMedicationRegularLabelResource"  Text="27.-&#191; Ingiere usted alg&#250;n medicamento en forma peri&#243;dica?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='YouIngestedMedicationRegular'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='YouIngestedMedicationRegular' ClientInstanceName='YouIngestedMedicationRegular' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="27.-¿Ingiere usted algún medicamento en forma periódica?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="YouIngestedMedicationRegularResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HealthStatusCurrentlyLabel' EncodeHtml='false' ClientInstanceName='HealthStatusCurrentlyLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HealthStatusCurrentlyLabelResource"  Text="28.-&#191; C&#243;mo considera su estado de salud actual?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HealthStatusCurrently'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='HealthStatusCurrently' ClientInstanceName='HealthStatusCurrently' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="28.-¿Cómo considera su estado de salud actual?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="HealthStatusCurrentlyResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HeRetiredLabel' EncodeHtml='false' ClientInstanceName='HeRetiredLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HeRetiredLabelResource"  Text="29.-&#191; Es jubilado?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HeRetired'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='HeRetired' ClientInstanceName='HeRetired' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="29.-¿Es jubilado?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="HeRetiredResource"  ValueType='System.Int64'  AutoPostBack='true' OnSelectedIndexChanged='HeRetired_SelectedIndexChanged'  TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HeRetiredMedicalReasonsLabel' EncodeHtml='false' ClientInstanceName='HeRetiredMedicalReasonsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HeRetiredMedicalReasonsLabelResource"  Text="       &#191;Jubil&#243; por razones m&#233;dicas?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HeRetiredMedicalReasons'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='HeRetiredMedicalReasons' ClientInstanceName='HeRetiredMedicalReasons' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="¿Jubiló por razones médicas?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="HeRetiredMedicalReasonsResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='ActivityDangerousProfessionLabel' EncodeHtml='false' ClientInstanceName='ActivityDangerousProfessionLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ActivityDangerousProfessionLabelResource"  Text="30.-&#191; Practica alguna actividad, oficio o profesi&#243;n que sea riesgosa o peligrosa?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='ActivityDangerousProfession'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='ActivityDangerousProfession' ClientInstanceName='ActivityDangerousProfession' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="30.-¿Practica alguna actividad, oficio o profesión que sea riesgosa o peligrosa?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="ActivityDangerousProfessionResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='DangerousSportLabel' EncodeHtml='false' ClientInstanceName='DangerousSportLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DangerousSportLabelResource"  Text="31.-&#191; Practica alguna actividad deportiva que sea riesgosa o peligrosa?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='DangerousSport'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='DangerousSport' ClientInstanceName='DangerousSport' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="31.-¿Practica alguna actividad deportiva que sea riesgosa o peligrosa?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="DangerousSportResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='AbnormalResultsTestsLabel' EncodeHtml='false' ClientInstanceName='AbnormalResultsTestsLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AbnormalResultsTestsLabelResource"  Text="32.-&#191; Resultados anormales en los ex&#225;menes de: radiograf&#237;a de t&#243;rax, glicemia o electrocardiograma?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='AbnormalResultsTests'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='AbnormalResultsTests' ClientInstanceName='AbnormalResultsTests' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="32.-¿Resultados anormales en los exámenes de: radiografía de tórax, glicemia o electrocardiograma?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="AbnormalResultsTestsResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LifeInsuranceInForceLabel' EncodeHtml='false' ClientInstanceName='LifeInsuranceInForceLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LifeInsuranceInForceLabelResource"  Text="33.-&#191; Tiene alg&#250;n seguro de vida vigente?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LifeInsuranceInForce'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='LifeInsuranceInForce' ClientInstanceName='LifeInsuranceInForce' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="33.-¿Tiene algún seguro de vida vigente?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="LifeInsuranceInForceResource"  ValueType='System.Int64'  AutoPostBack='true' OnSelectedIndexChanged='LifeInsuranceInForce_SelectedIndexChanged'  TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='TimeInLifeInsuranceLabel' EncodeHtml='false' ClientInstanceName='TimeInLifeInsuranceLabel' runat='server' ClientIDMode='Static' meta:resourcekey="TimeInLifeInsuranceLabelResource"  Text="       &#191;Hace cu&#225;nto tiempo lo posee?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='TimeInLifeInsurance'       ></dxe:ASPxLabel></td>    <td style='width:150%;' colspan='5' align='left'><div style='float: left;'>

       <dxe:ASPxTextBox runat='server' ID='TimeInLifeInsurance' ClientInstanceName='TimeInLifeInsurance' ToolTip="¿Hace cuánto tiempo lo posee?" Size='5' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="TimeInLifeInsuranceResource"  Width='54px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-99999..99999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxTextBox>
</div>

       <dxe:ASPxLabel ID='meses' EncodeHtml='false' ClientInstanceName='meses' runat='server' ClientIDMode='Static' meta:resourcekey="mesesResource"  Text="meses"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel>


</div>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LifeInsuranceCompanyLabel' EncodeHtml='false' ClientInstanceName='LifeInsuranceCompanyLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LifeInsuranceCompanyLabelResource"  Text="       &#191;En qu&#233; compa&#241;&#237;a?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LifeInsuranceCompany'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='LifeInsuranceCompany' ClientInstanceName='LifeInsuranceCompany' ToolTip="¿En qué compañía?" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="LifeInsuranceCompanyResource" ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LifeInsuranceAcceptedWithSurchargeLabel' EncodeHtml='false' ClientInstanceName='LifeInsuranceAcceptedWithSurchargeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LifeInsuranceAcceptedWithSurchargeLabelResource"  Text="       &#191;Fue aplazado, rechazado o aceptado con alg&#250;n tipo de recargo?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='LifeInsuranceAcceptedWithSurcharge'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='LifeInsuranceAcceptedWithSurcharge' ClientInstanceName='LifeInsuranceAcceptedWithSurcharge' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="¿Fue aplazado, rechazado o aceptado con algún tipo de recargo?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="LifeInsuranceAcceptedWithSurchargeResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='RejectedByHiringLifeInsuranceLabel' EncodeHtml='false' ClientInstanceName='RejectedByHiringLifeInsuranceLabel' runat='server' ClientIDMode='Static' meta:resourcekey="RejectedByHiringLifeInsuranceLabelResource"  Text="34.-&#191; Ha sido rechazado al contratar alg&#250;n seguro de vida?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='RejectedByHiringLifeInsurance'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='RejectedByHiringLifeInsurance' ClientInstanceName='RejectedByHiringLifeInsurance' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="34.-¿Ha sido rechazado al contratar algún seguro de vida?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="RejectedByHiringLifeInsuranceResource"  ValueType='System.Int64'    TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='AreYouPregnantLabel' EncodeHtml='false' ClientInstanceName='AreYouPregnantLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AreYouPregnantLabelResource"  Text="35.- En caso de sexo femenino: &#191;S e encuentra embarazada?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='AreYouPregnant'       ></dxe:ASPxLabel></td>    <td style='width:50%;' align='left'>

       <dxe:ASPxRadioButtonList ID='AreYouPregnant' ClientInstanceName='AreYouPregnant' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="35.-En caso de sexo femenino: ¿Se encuentra embarazada?" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="AreYouPregnantResource"  ValueType='System.Int64'  AutoPostBack='true' OnSelectedIndexChanged='AreYouPregnant_SelectedIndexChanged'  TextField='SDESCRIPT' ValueField='NCODIGINT'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='HowManyWeeksPregnantLabel' EncodeHtml='false' ClientInstanceName='HowManyWeeksPregnantLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HowManyWeeksPregnantLabelResource"  Text="       &#191;Cu&#225;ntas semanas tiene?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='HowManyWeeksPregnant'       ></dxe:ASPxLabel></td>    <td style='width:150%;' colspan='3' align='left'><div style='float: left;'>

       <dxe:ASPxTextBox runat='server' ID='HowManyWeeksPregnant' ClientInstanceName='HowManyWeeksPregnant' ToolTip="¿Cuántas semanas tiene?" Size='5' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="HowManyWeeksPregnantResource"  Width='54px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <-99999..99999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="DPS" >
</ValidationSettings>
       </dxe:ASPxTextBox>
</div>

       <dxe:ASPxLabel ID='semanas' EncodeHtml='false' ClientInstanceName='semanas' runat='server' ClientIDMode='Static' meta:resourcekey="semanasResource"  Text="semanas"  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel>


</div>

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
                    <dxrp:ASPxRoundPanel ID="OBSERVACIONES" ClientInstanceName="OBSERVACIONES" runat="server" HeaderText="OBSERVACIONES" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="OBSERVACIONESResource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%;' colspan='2' align='left'>

       <dxe:ASPxMemo runat='server' ClientIDMode='Static' ID='Observations' ToolTip="Observaciones" meta:resourcekey="ObservationsResource" Columns='20' Rows='2' Size='0' NullText="" ClientVisible='True' ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="OBSERVACIONES" >
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