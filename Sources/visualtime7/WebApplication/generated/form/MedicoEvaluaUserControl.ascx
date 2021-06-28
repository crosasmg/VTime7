<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MedicoEvaluaUserControl.ascx.vb" Inherits="MedicoEvaluaUserControl" %>

<script src="/dropthings/ConfirmDeleteWindow.js" type="text/javascript"></script>
<script type="text/javascript">
    var msgControlsDependencyResource='<asp:Localize runat="server" Text="Espere..." meta:resourcekey="ControlsDependencyResource"></asp:Localize>';
    var msgControlsDependencyResource='<asp:Localize runat="server" Text="Espere..." meta:resourcekey="ControlsDependencyResource"></asp:Localize>';
    var msgbtnAgregarInformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="btnAgregarInformationMessageResource"></asp:Localize>';
    var titlebtnAgregarInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebtnAgregarInformationMessageResource"></asp:Localize>';
    var msgbtnEditarInformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="btnEditarInformationMessageResource"></asp:Localize>';
    var titlebtnEditarInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebtnEditarInformationMessageResource"></asp:Localize>';
    var msgbtnEliminarInformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="btnEliminarInformationMessageResource"></asp:Localize>';
    var titlebtnEliminarInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebtnEliminarInformationMessageResource"></asp:Localize>';
    var msgbutton2InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button2InformationMessageResource"></asp:Localize>';
    var titlebutton2InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton2InformationMessageResource"></asp:Localize>';
    var msgbutton3InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button3InformationMessageResource"></asp:Localize>';
    var titlebutton3InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton3InformationMessageResource"></asp:Localize>';
    var msgbtnAplicarInformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="btnAplicarInformationMessageResource"></asp:Localize>';
    var titlebtnAplicarInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebtnAplicarInformationMessageResource"></asp:Localize>';
    var msgbutton13InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button13InformationMessageResource"></asp:Localize>';
    var titlebutton13InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton13InformationMessageResource"></asp:Localize>';
    var msgbutton14InformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="button14InformationMessageResource"></asp:Localize>';
    var titlebutton14InformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlebutton14InformationMessageResource"></asp:Localize>';
    var msgcerrarInformationMessageResource='<asp:Localize runat="server" Text="Por favor espere..." meta:resourcekey="cerrarInformationMessageResource"></asp:Localize>';
    var titlecerrarInformationMessageResource='<asp:Localize runat="server" Text="Información" meta:resourcekey="titlecerrarInformationMessageResource"></asp:Localize>';



</script>

<script src="/generated/form/MedicoEvalua.js" type="text/javascript"></script>      
<asp:UpdatePanel ID="MedicoEvaluaUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
<ContentTemplate>
<dx:ASPxHiddenField runat="server" ClientInstanceName="FormResources" ID="FormResources"/>
<table id='MedicoEvaluaTablePage' runat='server' style='width: 100%;margin: auto;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="RiskInformation" ClientInstanceName="RiskInformation" runat="server" HeaderText="Datos de la Solicitud" ToolTip="Datos de la Solicitud" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="RiskInformationResource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:50%;' align='left'>       <dxe:ASPxLabel ID='ProductCodeLabel' EncodeHtml='false' ClientInstanceName='ProductCodeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ProductCodeLabelResource"  Text="Producto"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='ProductCode'       ></dxe:ASPxLabel><br />


<dxe:ASPxComboBox ID='ProductCode' runat='server' ClientInstanceName='ProductCode' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Producto" ClientVisible='true' ClientEnabled='False' meta:resourcekey="ProductCodeResource"  Width='200px'  ValueType='System.Int32'    TextField='SDESCRIPT' ValueField='NPRODUCT'>         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="RiskInformation" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
<ClientSideEvents  BeginCallback="ProductCodeBeginCallback"  EndCallback="ProductCodeEndCallback" />
</dxe:ASPxComboBox>
    </td>

    <td style='width:50%;' align='left'>       <dxe:ASPxLabel ID='FullProposalIdLabel' EncodeHtml='false' ClientInstanceName='FullProposalIdLabel' runat='server' ClientIDMode='Static' meta:resourcekey="FullProposalIdLabelResource"  Text="N° de Solicitud"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='FullProposalId'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='FullProposalId' ClientInstanceName='FullProposalId' ToolTip="Solicitud" Size='20' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="FullProposalIdResource"  Width='200px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <-99999999999999999999..99999999999999999999>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="RiskInformation" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

  </tr>
  <tr valign='top'>
<td style='width:25%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='LineOfBusinessLabel' EncodeHtml='false' ClientInstanceName='LineOfBusinessLabel' runat='server' ClientIDMode='Static' meta:resourcekey="LineOfBusinessLabelResource"  Text="Ramo"  ClientEnabled='false'  ClientVisible='false'  AssociatedControlID='LineOfBusiness'       ></dxe:ASPxLabel></td>    <td style='width:25%;' align='left'>


<dxe:ASPxComboBox ID='LineOfBusiness' runat='server' ClientInstanceName='LineOfBusiness' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Ramo" ClientVisible='false' ClientEnabled='False' meta:resourcekey="LineOfBusinessResource"  Width='200px'  ValueType='System.Int32'    TextField='SDESCRIPT' ValueField='NBRANCH'>         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="RiskInformation" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
<ClientSideEvents  SelectedIndexChanged="LineOfBusinessSelectedIndexChanged" />
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
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="UnderwritingCase" ClientInstanceName="UnderwritingCase" runat="server" HeaderText="Datos del Asegurado" ToolTip="Datos del Asegurado" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="UnderwritingCaseResource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='10'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:20%;' align='left'>       <dxe:ASPxLabel ID='CompleteClientNameLabel' EncodeHtml='false' ClientInstanceName='CompleteClientNameLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CompleteClientNameLabelResource"  Text="Nombre"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='CompleteClientName'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='CompleteClientName' ClientInstanceName='CompleteClientName' ToolTip="Nombre" Size='63' NullText="" ClientVisible='True' MaxLength='63' ClientIDMode='Static' meta:resourcekey="CompleteClientNameResource" Width='320px'  ClientEnabled='False'  >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="UnderwritingCase" >
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:20%;' align='center'>       <dxe:ASPxLabel ID='GenderLabel' EncodeHtml='false' ClientInstanceName='GenderLabel' runat='server' ClientIDMode='Static' meta:resourcekey="GenderLabelResource"  Text="Género"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='Gender'       ></dxe:ASPxLabel><br />

       <dxe:ASPxRadioButtonList ID='Gender' ClientInstanceName='Gender' Border-BorderStyle='None' Native='True' runat='server' RepeatDirection='Vertical' ClientIDMode='Static' ToolTip="Sexo del cliente" ClientVisible='true' ClientEnabled='False'  meta:resourcekey="GenderResource"  ValueType='System.String'    TextField='SDESCRIPT' ValueField='SSEXCLIEN'>         <Paddings PaddingBottom="0px" PaddingLeft="0px" PaddingRight="0px" PaddingTop="0px" />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="UnderwritingCase" >
</ValidationSettings>
       </dxe:ASPxRadioButtonList>
    </td>

    <td style='width:20%;' align='left'>       <dxe:ASPxLabel ID='AgeLabel' EncodeHtml='false' ClientInstanceName='AgeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="AgeLabelResource"  Text="Edad"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='Age'       ></dxe:ASPxLabel><br />

        <table>
            <tr>
                <td>
       <dxe:ASPxTextBox runat='server' ID='Age' ClientInstanceName='Age' ToolTip="Edad" Size='5' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="AgeResource"  Width='70px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <-99999..99999g>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="UnderwritingCase" >
</ValidationSettings>
       </dxe:ASPxTextBox>
                </td>
                <td align='left'>
       <dxe:ASPxLabel ID='AgeMeasureLabel' ClientInstanceName='AgeMeasureLabel' runat='server' ClientEnabled='False' Text="Años" meta:resourcekey="AgeMeasureLabelResource"  ClientVisible='true'        ></dxe:ASPxLabel>
               </td>
            </tr>
        </table>
    </td>

    <td style='width:20%;' align='left'>       <dxe:ASPxLabel ID='HeightLabel' EncodeHtml='false' ClientInstanceName='HeightLabel' runat='server' ClientIDMode='Static' meta:resourcekey="HeightLabelResource"  Text="Estatura"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='Height'       ></dxe:ASPxLabel><br />

        <table>
            <tr>
                <td>
       <dxe:ASPxTextBox runat='server' ID='Height' ClientInstanceName='Height' ToolTip="Estatura" Size='7' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="HeightResource"  Width='70px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <-9999..9999g>.<00..99>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="UnderwritingCase" >
</ValidationSettings>
       </dxe:ASPxTextBox>
                </td>
                <td align='left'>
       <dxe:ASPxLabel ID='HeightMeasureLabel' ClientInstanceName='HeightMeasureLabel' runat='server' ClientEnabled='False' Text="Mts" meta:resourcekey="HeightMeasureLabelResource"  ClientVisible='true'        ></dxe:ASPxLabel>
               </td>
            </tr>
        </table>
    </td>

    <td style='width:20%;' align='left'>       <dxe:ASPxLabel ID='WeightLabel' EncodeHtml='false' ClientInstanceName='WeightLabel' runat='server' ClientIDMode='Static' meta:resourcekey="WeightLabelResource"  Text="Peso"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='Weight'       ></dxe:ASPxLabel><br />

        <table>
            <tr>
                <td>
       <dxe:ASPxTextBox runat='server' ID='Weight' ClientInstanceName='Weight' ToolTip="Peso" Size='8' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="WeightResource"  Width='70px'  Text='0' ClientEnabled='false' ClientVisible='true'  MaskSettings-Mask=' <-99999..99999g>.<00..99>' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="UnderwritingCase" >
</ValidationSettings>
       </dxe:ASPxTextBox>
                </td>
                <td align='left'>
       <dxe:ASPxLabel ID='WeightMeasureLabel' ClientInstanceName='WeightMeasureLabel' runat='server' ClientEnabled='False' Text="Kgs" meta:resourcekey="WeightMeasureLabelResource"  ClientVisible='true'        ></dxe:ASPxLabel>
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
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone0" ClientInstanceName="zone0" runat="server" HeaderText="Domicilio" ToolTip="Domicilio" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone0Resource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='8'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:20%;' align='left'>       <dxe:ASPxLabel ID='CountryLabel' EncodeHtml='false' ClientInstanceName='CountryLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CountryLabelResource"  Text="País"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='Country'       ></dxe:ASPxLabel><br />


<dxe:ASPxComboBox ID='Country' runat='server' ClientInstanceName='Country' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="País" ClientVisible='true' ClientEnabled='False' meta:resourcekey="CountryResource"  Width='180px'  ValueType='System.Int32'    TextField='SDESCRIPT' ValueField='NCOUNTRY'>         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
</dxe:ASPxComboBox>
    </td>

    <td style='width:25%;' align='left'>       <dxe:ASPxLabel ID='StateOrProvinceLabel' EncodeHtml='false' ClientInstanceName='StateOrProvinceLabel' runat='server' ClientIDMode='Static' meta:resourcekey="StateOrProvinceLabelResource"  Text="Región"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='StateOrProvince'       ></dxe:ASPxLabel><br />


<dxe:ASPxComboBox ID='StateOrProvince' runat='server' ClientInstanceName='StateOrProvince' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Región" ClientVisible='true' ClientEnabled='False' meta:resourcekey="StateOrProvinceResource"  Width='180px'  ValueType='System.Int32'    TextField='SDESCRIPT' ValueField='NPROVINCE'>         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
<ClientSideEvents  SelectedIndexChanged="StateOrProvinceSelectedIndexChanged" />
</dxe:ASPxComboBox>
    </td>

    <td style='width:25%;' align='left'>       <dxe:ASPxLabel ID='CityCodeLabel' EncodeHtml='false' ClientInstanceName='CityCodeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="CityCodeLabelResource"  Text="Ciudad"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='CityCode'       ></dxe:ASPxLabel><br />


<dxe:ASPxComboBox ID='CityCode' runat='server' ClientInstanceName='CityCode' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Ciudad" ClientVisible='true' ClientEnabled='False' meta:resourcekey="CityCodeResource"  Width='180px'  ValueType='System.Int32'    TextField='SDESCRIPT' ValueField='NLOCAL'><ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
<ClientSideEvents  EndCallback="CityCodeEndCallback"  SelectedIndexChanged="CityCodeSelectedIndexChanged"  BeginCallback="CityCodeBeginCallback" />
</dxe:ASPxComboBox>
    </td>

    <td style='width:30%;' align='left'>       <dxe:ASPxLabel ID='MunicipalityCodeLabel' EncodeHtml='false' ClientInstanceName='MunicipalityCodeLabel' runat='server' ClientIDMode='Static' meta:resourcekey="MunicipalityCodeLabelResource"  Text="Comuna"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='MunicipalityCode'       ></dxe:ASPxLabel><br />


<dxe:ASPxComboBox ID='MunicipalityCode' runat='server' ClientInstanceName='MunicipalityCode' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Comuna" ClientVisible='true' ClientEnabled='False' meta:resourcekey="MunicipalityCodeResource"  Width='180px'  ValueType='System.Int32'    TextField='SDESCRIPT' ValueField='NMUNICIPALITY'><ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone0" >
</ValidationSettings>
<ClientSideEvents  BeginCallback="MunicipalityCodeBeginCallback"  EndCallback="MunicipalityCodeEndCallback" />
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
                    <dxrp:ASPxRoundPanel ID="zone6" ClientInstanceName="zone6" runat="server" HeaderText="Datos de Salud" ToolTip="Datos de Salud" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone6Resource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='QuestionAndAnswer' EnableRowsCache='False' EnableViewState='False' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='QuestionAndAnswer' runat='server' Width='100%' KeyFieldName='Id' Caption="Declaración Personal de Salud" meta:resourcekey="QuestionAndAnswerResource"
>
               <SettingsPager Visible="True" PageSize="10"/>
 <SettingsBehavior AllowFocusedRow="True" AllowSort="False"/>
        <Columns>
<dxwgv:GridViewDataTextColumn FieldName='Id' Visible='false'>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Question' FieldName='Question' ToolTip="Pregunta" Caption="Pregunta" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="QuestionFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='100' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="QuestionAndAnswer" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn Name='Answer' FieldName='Answer' ToolTip="Respuesta" Caption="Respuesta" GroupIndex="-1" VisibleIndex="1" meta:resourcekey="AnswerFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='50' >
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="QuestionAndAnswer" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
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
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="Observaciones" ClientInstanceName="Observaciones" runat="server" HeaderText="Observaciones" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="ObservacionesResource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='Note' EnableRowsCache='False' EnableViewState='False' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='Note' runat='server' Width='100%' KeyFieldName='NoteID' Caption="Observaciones" meta:resourcekey="NoteResource"
>
               <SettingsPager Visible="True" PageSize="10"/>
 <SettingsBehavior AllowFocusedRow="True" AllowSort="False"/>
        <Columns>
<dxwgv:GridViewDataTextColumn FieldName='NoteID' Visible='false'>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataComboBoxColumn Name='RecordType' FieldName='RecordType' ToolTip="Tipo de observación" Caption="Tipo" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="RecordTypeFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
<PropertiesComboBox EnableCallbackMode='false' IncrementalFilteringMode='StartsWith'
 TextField='SDESCRIPT' ValueField='NRECTYPE'>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='UserCode2' FieldName='UserCode' ToolTip="Emisor que crea/actualiza la nota" Caption="Emisor" GroupIndex="-1" VisibleIndex="1" meta:resourcekey="UserCode2FieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
<PropertiesComboBox EnableCallbackMode='false' IncrementalFilteringMode='StartsWith'
 TextField='SCLIENAME' ValueField='NUSERCODE'>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='Description' FieldName='Description' ToolTip="Descripción que crea/actualiza la observación" Caption="Descripción" GroupIndex="-1" VisibleIndex="2" meta:resourcekey="DescriptionFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
           <PropertiesTextEdit NullText="" MaxLength='60' >
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataDateColumn Name='UpdateTimeStamp1' FieldName='UpdateTimeStamp' ToolTip="Fecha de creación/actualización de la observación" Caption="Fecha" GroupIndex="-1" VisibleIndex="3" meta:resourcekey="UpdateTimeStamp1FieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="3" Visible="True" />
           <PropertiesDateEdit>
           </PropertiesDateEdit>
</dxwgv:GridViewDataDateColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zoneNote" ClientInstanceName="zoneNote" runat="server" HeaderText="" ToolTip="zo" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zoneNoteResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:100%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone8" ClientInstanceName="zone8" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone8Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='8'>
    </td>
  </tr>
  <tr valign='top'>
<td style='width:12.5%; padding-top:3px;' colspan='1' align='Left'>       <dxe:ASPxLabel ID='NoteIDLabel' EncodeHtml='false' ClientInstanceName='NoteIDLabel' runat='server' ClientIDMode='Static' meta:resourcekey="NoteIDLabelResource"  Text="Observación"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='NoteID'       ></dxe:ASPxLabel></td>    <td style='width:12.5%;' align='left'>


<dxe:ASPxComboBox ID='NoteID' runat='server' ClientInstanceName='NoteID' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Observación a editar/eliminar" ClientVisible='true' ClientEnabled='True' meta:resourcekey="NoteIDResource"  ValueType='System.Int64'  AutoPostBack='false' OnSelectedIndexChanged='NoteID_SelectedIndexChanged'  TextField='DESCRIPTION' ValueField='NOTEID'>         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="zone8" >
<RequiredField IsRequired='true' ErrorText="El campo es requerido." />
</ValidationSettings>
<ClientSideEvents  ValueChanged="function(s, e) {AsyncPostBack()}" />
</dxe:ASPxComboBox>
    </td>

    <td style='width:25%'  colspan='2' align='Left'><div style='float: left;'>

       <dxe:ASPxButton ID='btnAgregar' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Agregar una nueva observación" ClientVisible='True' ClientEnabled='True' meta:resourcekey="btnAgregarResource" Text="Agregar"  OnClick='btnAgregar_Click' AutoPostBack='false'>
<ClientSideEvents  Click="btnAgregarClick" />
       </dxe:ASPxButton>
</div>

<div style='float: left;'>

       <dxe:ASPxButton ID='btnEditar' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Editar una observación" ClientVisible='True' ClientEnabled='True' meta:resourcekey="btnEditarResource" Text="Editar"  OnClick='btnEditar_Click' AutoPostBack='false'>
<ClientSideEvents  Click="btnEditarClick" />
       </dxe:ASPxButton>
</div>

<div style='float: left;'>

       <dxe:ASPxButton ID='btnEliminar' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Eliminar la observación" ClientVisible='True' ClientEnabled='True' meta:resourcekey="btnEliminarResource" Text="Eliminar"  OnClick='btnEliminar_Click' AutoPostBack='false'>
<ClientSideEvents  Click="btnEliminarClick" />
       </dxe:ASPxButton>
</div>
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
                    <dxrp:ASPxRoundPanel ID="NoteAddEdit" ClientInstanceName="NoteAddEdit" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="NoteAddEditResource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:50%;' align='left'>       <dxe:ASPxLabel ID='DescriptionNoteLabel' EncodeHtml='false' ClientInstanceName='DescriptionNoteLabel' runat='server' ClientIDMode='Static' meta:resourcekey="DescriptionNoteLabelResource"  Text="Descripción corta"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='DescriptionNote'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='DescriptionNote' ClientInstanceName='DescriptionNote' ToolTip="Descripción de la observación" Size='60' NullText="" ClientVisible='True' MaxLength='60' ClientIDMode='Static' meta:resourcekey="DescriptionNoteResource" ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="NoteAddEdit" >
     <RequiredField IsRequired='True' ErrorText="" />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:50%;' align='left'>       <dxe:ASPxLabel ID='UpdateTimeStampLabel' EncodeHtml='false' ClientInstanceName='UpdateTimeStampLabel' runat='server' ClientIDMode='Static' meta:resourcekey="UpdateTimeStampLabelResource"  Text="Fecha"  ClientEnabled='false'  ClientVisible='true'  AssociatedControlID='UpdateTimeStamp'       ></dxe:ASPxLabel><br />

       <dxe:ASPxDateEdit runat='server' ID='UpdateTimeStamp' ToolTip="Fecha de la observación" ClientIDMode='Static' ClientVisible='True' meta:resourcekey="UpdateTimeStampResource" ClientEnabled='False'  >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="NoteAddEdit" >
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
    <td style='width:100%;' colspan='6' align='center'>       <dxe:ASPxLabel ID='FreeTextLabel' EncodeHtml='false' ClientInstanceName='FreeTextLabel' runat='server' ClientIDMode='Static' meta:resourcekey="FreeTextLabelResource"  Text="Observaciones"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='FreeText'       ></dxe:ASPxLabel><br />

       <dxe:ASPxMemo runat='server' ClientIDMode='Static' ID='FreeText' ToolTip="Detalle completo de la observación" meta:resourcekey="FreeTextResource" Columns='120' Rows='10' Size='0' NullText="" ClientVisible='True' ClientEnabled='True'  >
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="NoteAddEdit" >
     <RequiredField IsRequired='True' ErrorText="" />
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
    <td style='width:300%' colspan='6'>
                    <dxrp:ASPxRoundPanel ID="zone1" ClientInstanceName="zone1" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="False" meta:resourcekey="zone1Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:50%' colspan='2'>
                    <dxrp:ASPxRoundPanel ID="zone7" ClientInstanceName="zone7" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone7Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='2'>
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
                    <dxrp:ASPxRoundPanel ID="zone9" ClientInstanceName="zone9" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone9Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='4'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:50%'  colspan='2' align='Left'>

       <dxe:ASPxButton ID='button2' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="button2" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button2Resource" Text="Aplicar"  OnClick='button2_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button2Click" />
       </dxe:ASPxButton>
    </td>

    <td style='width:50%'  colspan='2' align='Left'>

       <dxe:ASPxButton ID='button3' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="button3" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button3Resource" Text="Cancelar"  OnClick='button3_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button3Click" />
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
                    <dxrp:ASPxRoundPanel ID="zone10" ClientInstanceName="zone10" runat="server" HeaderText="Antecedentes de Recargos Médicos" ToolTip="Antecedentes de Recargos Médicos" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone10Resource"
 Width="100%" SkinID="CaptionAndRoundedBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='10'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:20%;' align='left'>       <dxe:ASPxLabel ID='IdAddEditLabel' EncodeHtml='false' ClientInstanceName='IdAddEditLabel' runat='server' ClientIDMode='Static' meta:resourcekey="IdAddEditLabelResource"  Text="Componente"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='IdAddEdit'       ></dxe:ASPxLabel><br />


<dxe:ASPxComboBox ID='IdAddEdit' runat='server' ClientInstanceName='IdAddEdit' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="ID" ClientVisible='true' ClientEnabled='True' meta:resourcekey="IdAddEditResource"  ValueType='System.String'    TextField='DESCRIPTION' ValueField='CODE'>         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="RecargosMedicos" >
<RequiredField IsRequired='true' ErrorText="El componente debe de estar lleno." />
</ValidationSettings>
</dxe:ASPxComboBox>
    </td>

    <td style='width:20%;' align='left'>       <dxe:ASPxLabel ID='ComponentAddEditLabel' EncodeHtml='false' ClientInstanceName='ComponentAddEditLabel' runat='server' ClientIDMode='Static' meta:resourcekey="ComponentAddEditLabelResource"  Text="Concepto médico"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='ComponentAddEdit'       ></dxe:ASPxLabel><br />


<dxe:ASPxComboBox ID='ComponentAddEdit' runat='server' ClientInstanceName='ComponentAddEdit' ShowImageInEditBox='True' ClientIDMode='Static' EnableIncrementalFiltering='True' ToolTip="Componente" ClientVisible='true' ClientEnabled='True' meta:resourcekey="ComponentAddEditResource"  ValueType='System.Int32'    TextField='SDESCRIPT' ValueField='NCAUSE'>         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="RecargosMedicos" >
<RequiredField IsRequired='true' ErrorText="El concepto médico debe estar lleno." />
</ValidationSettings>
</dxe:ASPxComboBox>
    </td>

    <td style='width:20%;' align='left'>       <dxe:ASPxLabel ID='PercentageSingleLabel' EncodeHtml='false' ClientInstanceName='PercentageSingleLabel' runat='server' ClientIDMode='Static' meta:resourcekey="PercentageSingleLabelResource"  Text="% Recargo"  ClientEnabled='true'  ClientVisible='true'  AssociatedControlID='PercentageSingle'       ></dxe:ASPxLabel><br />

       <dxe:ASPxTextBox runat='server' ID='PercentageSingle' ClientInstanceName='PercentageSingle' ToolTip="% Recargo" Size='8' HorizontalAlign='Right' ValidationSettings-Display='Dynamic' MaskSettings-IncludeLiterals='DecimalSymbol' ClientIDMode='Static' meta:resourcekey="PercentageSingleResource"  Text='0' ClientEnabled='true' ClientVisible='true'  MaskSettings-Mask=' <0..99999g>.<00..99>' >
         <Paddings PaddingLeft="8px" />
         <BackgroundImage HorizontalPosition="left" ImageUrl="/images/generaluse/required.PNG" Repeat="NoRepeat" VerticalPosition="center"/>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="RecargosMedicos" >
        <RequiredField IsRequired='true' ErrorText="Debe llenar el % del recargo." />
        <RegularExpression ValidationExpression="^[-0-9]\d*([,\.]\d+)?$" ErrorText="Debe llenar el % del recargo." />
</ValidationSettings>
       </dxe:ASPxTextBox>
    </td>

    <td style='width:20%'  colspan='2' align='Left'>

       <dxe:ASPxButton ID='btnAplicar' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Aplicar recargo" ClientVisible='True' ClientEnabled='True' meta:resourcekey="btnAplicarResource" Text="Aplicar Recargo"  OnClick='btnAplicar_Click' AutoPostBack='false'>
<ClientSideEvents  Click="btnAplicarClick" />
       </dxe:ASPxButton>
    </td>

    <td style="width:10%">
      &nbsp;
    </td>
    <td style="width:10%">
      &nbsp;
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:120%' colspan='12'>
      <dxwgv:ASPxGridView AutoGenerateColumns='False' ClientIDMode='Static' ClientInstanceName='SurchargeDiscountByInsured' EnableRowsCache='False' EnableViewState='False' EnableCallBacks='True' KeyboardSupport='False' EnableCallbackCompression='True' ID='SurchargeDiscountByInsured' runat='server' Width='100%' KeyFieldName='Id' Caption="" meta:resourcekey="SurchargeDiscountByInsuredResource"
>
               <SettingsPager Visible="True" PageSize="10"/>
               <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AllowSort="True"/>
                     <SettingsEditing EditFormColumnCount="5"/>
        <Columns>
<dxwgv:GridViewDataComboBoxColumn Name='Id' FieldName='Id' ToolTip="ID" Caption="Componente" GroupIndex="-1" VisibleIndex="0" meta:resourcekey="IdFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="0" Visible="True" />
<PropertiesComboBox EnableCallbackMode='false' IncrementalFilteringMode='StartsWith'
 TextField='DESCRIPTION' ValueField='CODE'>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="SurchargeDiscountByInsured" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataComboBoxColumn Name='Component' FieldName='Component' ToolTip="Componente" Caption="Concepto médico" GroupIndex="-1" VisibleIndex="1" meta:resourcekey="ComponentFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="1" Visible="True" />
<PropertiesComboBox EnableCallbackMode='false' IncrementalFilteringMode='StartsWith'
 TextField='SDESCRIPT' ValueField='NCAUSE'>
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" Display="Dynamic" ValidationGroup="SurchargeDiscountByInsured" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
</PropertiesComboBox>
</dxwgv:GridViewDataComboBoxColumn>
<dxwgv:GridViewDataTextColumn Name='Percentage' FieldName='Percentage' ToolTip="% Recargo" Caption="% Recargo" GroupIndex="-1" VisibleIndex="2" meta:resourcekey="PercentageFieldResource"
 Visible='True'
><EditFormSettings VisibleIndex="2" Visible="True" />
           <PropertiesTextEdit Style-HorizontalAlign="Right" DisplayFormatString="###,##0.00">
   <MaskSettings IncludeLiterals="DecimalSymbol" Mask=' <0..99999g>.<00..99>' />
<ValidationSettings EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" ValidationGroup="SurchargeDiscountByInsured" >

                   <RequiredField IsRequired="True" ErrorText="El campo es requerido."/>
               </ValidationSettings>
           </PropertiesTextEdit>
</dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewCommandColumn Caption=" " >
                                    <DeleteButton Visible="True" Text="Eliminar" />
<CancelButton Visible="True" Text="Cancel" />
                                    <UpdateButton Visible="True" Text="Actualizar" />
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
                    <dxrp:ASPxRoundPanel ID="zone12" ClientInstanceName="zone12" runat="server" HeaderText="" ToolTip="" Enabled="True" ClientIDMode='Static' ClientVisible="True" meta:resourcekey="zone12Resource"
 Width="100%" SkinID="NotBorder">                        <PanelCollection>
                            <dxp:PanelContent runat="server">
<table style='width: 100%;'>
  <tr valign='top'>
    <td colspan='8'>
    </td>
  </tr>
  <tr valign='top'>
    <td style='width:85%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='button13' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Pendiente" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button13Resource" Text="Pendiente"  OnClick='button13_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button13Click" />
       </dxe:ASPxButton>
    </td>

    <td style='width:5%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='button14' runat='server' ClientIDMode='Static' CausesValidation='True' ToolTip="Aprobar" ClientVisible='True' ClientEnabled='True' meta:resourcekey="button14Resource" Text="Aprobar"  OnClick='button14_Click' AutoPostBack='false'>
<ClientSideEvents  Click="button14Click" />
       </dxe:ASPxButton>
    </td>

    <td style='width:5%'  colspan='2' align='Right'>

       <dxe:ASPxButton ID='cerrar' runat='server' ClientIDMode='Static' CausesValidation='False' ToolTip="Cierra la ventana" ClientVisible='True' ClientEnabled='True' meta:resourcekey="cerrarResource" Text="Cerrar"  AutoPostBack='false'>
<ClientSideEvents  Click="cerrarClick" />
       </dxe:ASPxButton>
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

    <dxlp:ASPxLoadingPanel ID="LoadingPanelGridView" runat="server" ClientInstanceName="LoadingPanelGridView"  Modal="True" Text="<%$ Resources:Resource, Working %>" />
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
                        <ClientSideEvents Click="function(s,e){Confirmation_Actions();}" />
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