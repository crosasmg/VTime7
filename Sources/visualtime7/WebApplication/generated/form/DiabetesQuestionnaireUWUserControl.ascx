<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DiabetesQuestionnaireUWUserControl.ascx.vb" Inherits="DiabetesQuestionnaireUWUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">
    var msgsubmitInformationMessageResource='<asp:Localize runat="server" Text="Procesando información.... Por favor espere." meta:resourcekey="submitInformationMessageResource"></asp:Localize>';
    var titlesubmitInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlesubmitInformationMessageResource"></asp:Localize>';



</script>

<script src="/generated/form/DiabetesQuestionnaireUW.js" type="text/javascript"></script>      
<asp:UpdatePanel ID="DiabetesQuestionnaireUWUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='DiabetesQuestionnaireUWTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="part0d" ClientInstanceName="part0d" runat="server" HeaderText="" ToolTip="" Enabled="False" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="part0dResource"
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
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part0d" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='uwcaseidLabel' EncodeHtml='false' ClientInstanceName='uwcaseidLabel' runat='server' ClientIDMode='Static' meta:resourcekey="uwcaseidLabelResource"  Text="Solicitud"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='uwcaseid'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='uwcaseid' ClientInstanceName='uwcaseid' ToolTip="" Size='5' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="uwcaseidResource"  Width='54px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..99999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="part0d" >
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
                    <dxrp:ASPxRoundPanel ID="part1d" ClientInstanceName="part1d" runat="server" HeaderText="El siguiente cuestionario debe ser completado por el solicitante del seguro. Por favor conteste todas las preguntas." ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="part1dResource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label3d' EncodeHtml='false' ClientInstanceName='label3d' runat='server' ClientIDMode='Static' meta:resourcekey="label3dResource"  Text="1. Edad de aparición de la diábetes"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="10"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label1d' EncodeHtml='false' ClientInstanceName='label1d' runat='server' ClientIDMode='Static' meta:resourcekey="label1dResource"  Text="2. Método utilizado para el control de la diábetes:"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="10"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone0d" ClientInstanceName="zone0d" runat="server" HeaderText="" ToolTip="zona" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone0dResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:33%;' colspan='2' align='left'>

       <dxe:ASPxTextBox runat='server' ID='AgeOnSetDiabetes' ClientInstanceName='AgeOnSetDiabetes' ToolTip="Indicate the age at onset diabetes" Size='3' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="AgeOnSetDiabetesResource"  Width='36px'  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..999g>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="zone0d" >
        <RequiredField IsRequired='true' ErrorText="El campo es requerido." />
        <RegularExpression ValidationExpression="^[-0-9]\d*([,\.]\d+)?$" ErrorText="El campo es requerido." />
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
                    <dxrp:ASPxRoundPanel ID="zone1d" ClientInstanceName="zone1d" runat="server" HeaderText="" ToolTip="zona" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone1dResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='6'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:33%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='DietMethod' runat='server' Text="Dieta" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="DietMethod"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:33%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='ExerciseMethod' runat='server' Text="Ejercicios" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="ExerciseMethod"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

    <td style="width:16.5%">
      &nbsp;
    </td>
    <td style="width:16.5%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:33%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='InsulinInjections' runat='server' Text="Inyecciones de insulina" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="InsulinInjections"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:33%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='InsulinPump' runat='server' Text="Bomba de insulina" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="InsulinPump"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:17%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='OtherMethodLabel' EncodeHtml='false' ClientInstanceName='OtherMethodLabel' runat='server' ClientIDMode='Static' meta:resourcekey="OtherMethodLabelResource"  Text="Otro método"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='OtherMethod'       ></dxe:ASPxLabel></td>    <td style='width:17%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='OtherMethod' ClientInstanceName='OtherMethod' ToolTip="Indique el nombre del método que utiliza para controlar la diábetes." Size='30' NullText="" ClientVisible='True' MaxLength='30' ClientIDMode='Static' meta:resourcekey="OtherMethodResource" Width='270px'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone1d" >
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
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label2d' EncodeHtml='false' ClientInstanceName='label2d' runat='server' ClientIDMode='Static' meta:resourcekey="label2dResource"  Text="3. Por favor, indique si ha tenido alguna de las siguientes:"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="10"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label5d' EncodeHtml='false' ClientInstanceName='label5d' runat='server' ClientIDMode='Static' meta:resourcekey="label5dResource"  Text="4. Frecuencia de monitoreo del nivel de azucar en la sangre"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="10"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone4d" ClientInstanceName="zone4d" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone4dResource"
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
<td style='width:33%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='EKGAbnormality' runat='server' Text="ECG Anomalía" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="EKGAbnormality"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:33%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='DiabeticComa' runat='server' Text="Coma diabético" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="DiabeticComa"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

    <td style="width:16.5%">
      &nbsp;
    </td>
    <td style="width:16.5%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:33%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='EyeTrouble' runat='server' Text="Problemas de ojos" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="EyeTrouble"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:33%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='ProteinInUrine' runat='server' Text="Proteina en la orina" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="ProteinInUrine"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

    <td style="width:16.5%">
      &nbsp;
    </td>
    <td style="width:16.5%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:33%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='SkinUlceration' runat='server' Text="Ulceración en la piel" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="SkinUlceration"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:33%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='Amputation' runat='server' Text="Amputación" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="Amputation"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

    <td style="width:16.5%">
      &nbsp;
    </td>
    <td style="width:16.5%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
<td style='width:33%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='Neuropathy' runat='server' Text="Neuropatía" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="Neuropathy"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:33%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxCheckBox ID='InsulinReaction' runat='server' Text="Reacción a la insulina" ClientIDMode='Static' ClientVisible='true' ClientEnabled='True' meta:resourcekey="InsulinReaction"   EncodeHtml='false' > 
       </dxe:ASPxCheckBox>


    </td>

<td style='width:17%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='OtherLabel' EncodeHtml='false' ClientInstanceName='OtherLabel' runat='server' ClientIDMode='Static' meta:resourcekey="OtherLabelResource"  Text="Otro:"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='Other'       ></dxe:ASPxLabel></td>    <td style='width:17%;' align='left'>

       <dxe:ASPxTextBox runat='server' ID='Other' ClientInstanceName='Other' ToolTip="Indicate the name of other disease" Size='30' NullText="" ClientVisible='True' MaxLength='30' ClientIDMode='Static' meta:resourcekey="OtherResource" Width='270px'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone4d" >
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
                    <dxrp:ASPxRoundPanel ID="zone6d" ClientInstanceName="zone6d" runat="server" HeaderText="" ToolTip="zona" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone6dResource"
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
    <td style='width:33%;' colspan='2' align='left'>


<dxe:ASPxComboBox ID='FrenquencyMonitorBloodSugerLevel' runat='server' ClientInstanceName='FrenquencyMonitorBloodSugerLevel' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Frequency of monitoring" ClientVisible='true' ClientEnabled='True' meta:resourcekey="FrenquencyMonitorBloodSugerLevelResource"  Width='315px'  ValueType='System.Int32'   >
            <Items>
                <dxe:ListEditItem Value='0' Text='Diario' meta:resourcekey="FrenquencyMonitorBloodSugerLevelListItemValue1Resource"/>
                <dxe:ListEditItem Value='1' Text='Semanal' meta:resourcekey="FrenquencyMonitorBloodSugerLevelListItemValue2Resource"/>
                <dxe:ListEditItem Value='2' Text='Despues de cada comida' meta:resourcekey="FrenquencyMonitorBloodSugerLevelListItemValue3Resource"/>
                <dxe:ListEditItem Value='3' Text='Varias veces al dia' meta:resourcekey="FrenquencyMonitorBloodSugerLevelListItemValue4Resource"/>
            </Items>
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone6d" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
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
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label7d' EncodeHtml='false' ClientInstanceName='label7d' runat='server' ClientIDMode='Static' meta:resourcekey="label7dResource"  Text="5. Niveles de azúcar en la sangre más reciente (el mejor nivel que conozca)"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="10"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label8d' EncodeHtml='false' ClientInstanceName='label8d' runat='server' ClientIDMode='Static' meta:resourcekey="label8dResource"  Text="6. Presión arterial más reciente"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="9"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



  </tr>
  <tr valign='top'>
    <td style='width:50%;' colspan='2' align='left'>

        <table>
            <tr>
                <td>
       <dxe:ASPxTextBox runat='server' ID='MostRecentReadingSugarLevel' ClientInstanceName='MostRecentReadingSugarLevel' ToolTip="Indicate the level of blood sugar" Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="MostRecentReadingSugarLevelResource" Width='135px'  ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1d" >
     <RequiredField IsRequired='True' ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
                </td>
                <td align='left'>
       <dxe:ASPxLabel ID='MostRecentReadingSugarLevelMeasureLabel' ClientInstanceName='MostRecentReadingSugarLevelMeasureLabel' runat='server' ClientEnabled='False' Text="(70-100) mg/dl" meta:resourcekey="MostRecentReadingSugarLevelMeasureLabelResource"  ClientVisible='true'        ></dxe:ASPxLabel>
               </td>
            </tr>
        </table>
    </td>

    <td style='width:50%;' colspan='2' align='left'>

        <table>
            <tr>
                <td>
       <dxe:ASPxTextBox runat='server' ID='MostRecentReadingBloodPressure' ClientInstanceName='MostRecentReadingBloodPressure' ToolTip="Indique el nivel de la presión arterial más reciente." Size='15' NullText="" ClientVisible='True' MaxLength='15' ClientIDMode='Static' meta:resourcekey="MostRecentReadingBloodPressureResource" Width='135px'  ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1d" >
     <RequiredField IsRequired='True' ErrorText="El campo es requerido." />
</ValidationSettings>
       </dxe:ASPxTextBox>
                </td>
                <td align='left'>
       <dxe:ASPxLabel ID='MostRecentReadingBloodPressureMeasureLabel' ClientInstanceName='MostRecentReadingBloodPressureMeasureLabel' runat='server' ClientEnabled='False' Text="(<90) mmHg" meta:resourcekey="MostRecentReadingBloodPressureMeasureLabelResource"  ClientVisible='true'        ></dxe:ASPxLabel>
               </td>
            </tr>
        </table>
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
<td style='width:50%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='label10d' EncodeHtml='false' ClientInstanceName='label10d' runat='server' ClientIDMode='Static' meta:resourcekey="label10dResource"  Text="7. Última vez que visitó a un médico"  ClientEnabled='true'  ClientVisible='true'  Font-Bold="True"  Font-Size="10"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>



    <td style="width:25%">
      &nbsp;
    </td>
    <td style="width:25%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone9d" ClientInstanceName="zone9d" runat="server" HeaderText="" ToolTip="zona" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone9dResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%;' colspan='2' align='left'>

       <dxe:ASPxDateEdit runat='server' ID='DateLastVisitPhysician' ToolTip="Fecha de la última vez que ha visitado a un médico." ClientIDMode='Static' ClientVisible='True' meta:resourcekey="DateLastVisitPhysicianResource" ClientEnabled='True'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone9d" >
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
    <td style='width:50%' colspan='2'>
<dxe:ASPxButton ID='btnADDDetailsOfMedicalPractitioners' ClientInstanceName='btnADDDetailsOfMedicalPractitioners' ClientVisible="True" ClientEnabled="True" Image-Url="~/images/generaluse/new.gif" Text="" meta:resourcekey="DetailsOfMedicalPractitionersGridBtnResource" runat="server" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
	                    DetailsOfMedicalPractitioners.AddNewRow();
                }" />
            </dxe:ASPxButton>            
   
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='DetailsOfMedicalPractitioners' EnableRowsCache='False' EnableViewState='True' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='DetailsOfMedicalPractitioners' runat='server' Width='100%' KeyFieldName='IdDetails' Caption="Detalles de los médicos" meta:resourcekey="DetailsOfMedicalPractitionersResource"

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
           <PropertiesTextEdit NullText="" MaxLength='35' >
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
<dxwgv:GridViewDataTextColumn Name='AddresPractitioner' FieldName='AddresPractitioner' Caption="Dirección" ToolTip="" GroupIndex="-1" VisibleIndex="3" meta:resourcekey="AddresPractitionerFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="3" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <0..99999g>' />
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
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='CholesterolBelow200Label' EncodeHtml='false' ClientInstanceName='CholesterolBelow200Label' runat='server' ClientIDMode='Static' meta:resourcekey="CholesterolBelow200LabelResource"  Text="8. ¿Su colesterol está por debajo de 200 mg?"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='CholesterolBelow200' Font-Bold="True"  Font-Size="10"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxRadioButtonList ID='CholesterolBelow200' ClientInstanceName='CholesterolBelow200' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Horizontal' ClientIDMode='Static' ToolTip="Nivel de colesterol" ClientVisible='true' ClientEnabled='True'  meta:resourcekey="CholesterolBelow200Resource"  ValueType='System.Boolean'   >
            <Items>
                <dxe:ListEditItem Value='True' Text='Si' meta:resourcekey="CholesterolBelow200ListItemValue1Resource"/>
                <dxe:ListEditItem Value='False' Text='No' meta:resourcekey="CholesterolBelow200ListItemValue2Resource"/>
            </Items>
         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1d" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='AdditionalInformationLabel' EncodeHtml='false' ClientInstanceName='AdditionalInformationLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AdditionalInformationLabelResource"  Text="9. Información adicional"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='AdditionalInformation' Font-Bold="True"  Font-Size="10"  Theme="Office2003Olive"        ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>

       <dxe:ASPxMemo runat='server' ClientIDMode='Static' ID='AdditionalInformation' ToolTip="Additional information" meta:resourcekey="AdditionalInformationResource" Columns='40' Rows='2' Size='0' NullText="" ClientVisible='True'  ClientEnabled='True'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="part1d" >
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
                    <dxrp:ASPxRoundPanel ID="zonedia" ClientInstanceName="zonedia" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zonediaResource"
 Width="100%" SkinID="RoundedBorderAndNotCaption">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="declaraciondia" ClientInstanceName="declaraciondia" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="declaraciondiaResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:100%; padding-top:3px;' colspan='2' align='Left'>       <dxe:ASPxLabel ID='textdia' EncodeHtml='false' ClientInstanceName='textdia' runat='server' ClientIDMode='Static' meta:resourcekey="textdiaResource"  Text="Declaro que las respuestas que he dado son de lo mejor de mi conocimiento,  verdadera y completa,  que no he ocultado ninguna información material que pueda influir en la evaluación o la aceptación de mi solicitud. Reconozco que este cuestionario es parte de la solicitud de seguro de vida y que no revelar algún hecho material conocido para mí, puede invalidar el contrato."  ClientEnabled='true'  ClientVisible='true'        ></dxe:ASPxLabel></td>



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
                    <dxrp:ASPxRoundPanel ID="firmadia" ClientInstanceName="firmadia" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="firmadiaResource"
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
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="firmadia" >
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