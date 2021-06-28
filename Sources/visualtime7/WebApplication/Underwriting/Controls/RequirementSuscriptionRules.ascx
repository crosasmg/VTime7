<%@ Control Language="VB" AutoEventWireup="false" CodeFile="RequirementSuscriptionRules.ascx.vb"
    Inherits="Underwriting_Controls_RequirementSuscriptionRules" %>
<%@ Register Src="SuscriptionRulesInformation.ascx" TagName="SuscriptionRulesInformation"
    TagPrefix="uc1" %>
<script type="text/javascript">
    function OnAddNewRowOnRules(s, e) {

    }
</script>
<dxwgv:ASPxGridView runat="server" AutoGenerateColumns="False" Caption="Reglas de suscripci&#243;n"
    ToolTip="Reglas de suscripci&#243;n" ID="UnderwritingGridView" Width="100%" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
    CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
    DataSourceID="dsUnderwritingRules" KeyFieldName="UnderRuleId" ClientInstanceName="rulesGrid"
    EnableViewState="False" meta:resourcekey="UnderwritingGridViewResource2">
    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
    <Styles CssPostfix="SoftOrange" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css">
    </Styles>
    <Templates>
        <DetailRow>
            <uc1:SuscriptionRulesInformation ID="SuscriptionRulesInformation1" runat="server" />
            <div style="padding: 2px 2px 2px 2px">
                <table width="100%">
                    <tr>
                        <td width="97%" align="right">
                        </td>
                        <td width="3%">
                            <dxe:ASPxButton ID="btnSave" runat="server" Cursor="pointer" EnableDefaultAppearance="False"
                                Font-Size="Small" Font-Underline="True" AutoPostBack="False" ForeColor="Blue"
                                Height="26px" Text="Editar" Width="30px" meta:resourcekey="btnSaveResource1"
                                EnableClientSideAPI="True" OnLoad="btnSave_Load">
                                <ClientSideEvents Click="function(s,e){ 
                                                                    rulesGrid.StartEditRow(rulesGrid.GetFocusedRowIndex());
                                }" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
        </DetailRow>
        <EditForm>
            <div style="padding: 4px 4px 3px 4px; width: 100%">
                <script type="text/javascript">
                    //The controls javascript from this usercontrol are on GeneralInformation.ascx user control
                </script>
                <table width="100%">
                    <tr>
                        <td width="10%">
                            <dxe:ASPxLabel ID="lblQuestion" runat="server" Text="Pregunta:" meta:resourcekey="lblQuestionResource1">
                            </dxe:ASPxLabel>
                        </td>
                        <td width="10%">
                            <dxe:ASPxDropDownEdit ID="ddeQuestion" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                CssPostfix="SoftOrange" ClientInstanceName="ddeQuestion" EnableClientSideAPI="True"
                                SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" Width="100%" DropDownWindowWidth="300px"
                                OnValidation="ddeQuestion_Validation" Value='<%# Bind("QuestionId") %>' meta:resourcekey="ddeQuestionResource1">
                                <Paddings PaddingLeft="8px" />
                                <BackgroundImage HorizontalPosition="left" ImageUrl="/Underwriting/Images/required.png"
                                    VerticalPosition="center" Repeat="NoRepeat" />
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="RulesEditForm" />
                                <DropDownWindowTemplate>
                                    <div style="width: 100%">
                                        <dxwgv:ASPxGridView ID="gQuestionForRequirement" runat="server" AutoGenerateColumns="False"
                                            Width="100%" ClientInstanceName="gQuestionForRequirement"
                                            OnAfterPerformCallback="gQuestionForRequirement_AfterPerformCallback" OnCustomJSProperties="gQuestionForRequirement_CustomJSProperties"
                                            CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange" OnDataBinding="gQuestionForRequirement_DataBinding"
                                            KeyFieldName="Code" meta:resourcekey="gQuestionForRequirementResource1">
                                            <SettingsBehavior AllowFocusedRow="True" />
                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFooter="True" ShowStatusBar="Hidden"
                                                ShowHorizontalScrollBar="True" />
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="Code" VisibleIndex="0" Caption="Id de pregunta"
                                                    meta:resourcekey="GridViewDataTextColumnResource6">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="Description" VisibleIndex="1" Width="300px"
                                                    Caption="Descripción" meta:resourcekey="GridViewDataTextColumnResource11">
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                            <Styles CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange">
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                                <LoadingPanel ImageSpacing="10px">
                                                </LoadingPanel>
                                            </Styles>
                                            <ImagesFilterControl>
                                                <LoadingPanel Url="~/App_Themes/SoftOrange/Editors/Loading.gif">
                                                </LoadingPanel>
                                            </ImagesFilterControl>
                                            <Images SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css">
                                                <LoadingPanelOnStatusBar Url="~/App_Themes/SoftOrange/GridView/gvLoadingOnStatusBar.gif">
                                                </LoadingPanelOnStatusBar>
                                                <LoadingPanel Url="~/App_Themes/SoftOrange/GridView/Loading.gif">
                                                </LoadingPanel>
                                            </Images>
                                            <ClientSideEvents EndCallback="QuestionsEndCallbackHandler" Init="QuestionsGridViewInitHandler"
                                                RowClick="QuestionsRowClickHandler" />
                                            <StylesEditors>
                                                <ProgressBar Height="25px">
                                                </ProgressBar>
                                            </StylesEditors>
                                        </dxwgv:ASPxGridView>
                                    </div>
                                </DropDownWindowTemplate>
                                <ClientSideEvents DropDown="QuestionsDropDownHandler" />
                                <ButtonStyle Width="13px">
                                </ButtonStyle>
                            </dxe:ASPxDropDownEdit>
                        </td>
                        <td width="30%">
                            <dxe:ASPxTextBox ID="txtQuestionDescription" runat="server" Width="100%" Text='<%# Bind("QuestionDescription") %>'
                                ClientInstanceName="lblQuestionDescription" EnableClientSideAPI="True" ReadOnly="True"
                                meta:resourcekey="txtQuestionDescriptionResource1">
                                <ValidationSettings ValidationGroup="RulesEditForm" ErrorDisplayMode="ImageWithTooltip"
                                    CausesValidation="True">
                                    <RequiredField ErrorText="Este valor es obligatorio" IsRequired="True" />
                                </ValidationSettings>
                            </dxe:ASPxTextBox>
                        </td>
                        <td width="10%">
                            <dxe:ASPxLabel ID="lblRule" runat="server" Text="Regla:" meta:resourcekey="lblRuleResource1"></dxe:ASPxLabel>
                        </td>
                        <td width="10%">
                            <dxe:ASPxDropDownEdit ID="ddeRule" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                ClientInstanceName="ddeRules" Width="100%" EnableClientSideAPI="True" Value='<%# Bind("UnderwritingRuleId") %>'
                                DropDownWindowWidth="300px" OnValidation="ddeRule_Validation" meta:resourcekey="ddeRuleResource1">
                                <Paddings PaddingLeft="8px" />
                                <BackgroundImage HorizontalPosition="left" ImageUrl="/Underwriting/Images/required.png"
                                    VerticalPosition="center" Repeat="NoRepeat" />
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="RulesEditForm" />
                                <DropDownWindowTemplate>
                                    <div style="width: 100%">
                                        <dxwgv:ASPxGridView ID="gRules" runat="server" AutoGenerateColumns="False" ClientInstanceName="gRules"
                                            CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange"
                                            OnDataBinding="gRules_DataBinding" KeyFieldName="Code"
                                            Width="100%" OnAfterPerformCallback="gRules_AfterPerformCallback" OnCustomJSProperties="gRules_CustomJSProperties"
                                            meta:resourcekey="gRulesResource1">
                                            <SettingsBehavior AllowFocusedRow="True" />
                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFooter="True" ShowStatusBar="Hidden" ShowHorizontalScrollBar="True" />
                                            <Styles CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange">
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px"></Header>
                                                <LoadingPanel ImageSpacing="10px"></LoadingPanel>
                                            </Styles>
                                            <ImagesFilterControl>
                                                <LoadingPanel Url="~/App_Themes/SoftOrange/Editors/Loading.gif"></LoadingPanel>
                                            </ImagesFilterControl>
                                            <Images SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css">
                                                <LoadingPanelOnStatusBar Url="~/App_Themes/SoftOrange/GridView/gvLoadingOnStatusBar.gif"></LoadingPanelOnStatusBar>
                                                <LoadingPanel Url="~/App_Themes/SoftOrange/GridView/Loading.gif"></LoadingPanel>
                                            </Images>
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn Caption="Id regla" FieldName="Code" VisibleIndex="0" meta:resourcekey="GridViewDataTextColumnResource8"></dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Descripción" FieldName="Description" VisibleIndex="1" Width="300px" meta:resourcekey="GridViewDataTextColumnResource9"></dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                            <ClientSideEvents EndCallback="RulesDetailEndCallbackHandler" Init="RulesDetailGridViewInitHandler" RowClick="RulesDetailRowClickHandler" />
                                            <StylesEditors>
                                                <ProgressBar Height="25px"></ProgressBar>
                                            </StylesEditors>
                                        </dxwgv:ASPxGridView>
                                    </div>
                                </DropDownWindowTemplate>
                                <ClientSideEvents DropDown="RulesDetailDropDownHandler" />
                            </dxe:ASPxDropDownEdit>
                        </td>
                        <td width="30%">
                            <dxe:ASPxTextBox ID="txtRuleDescription" runat="server" Width="100%" Text='<%# Bind("RuleDescription") %>'
                                ClientInstanceName="lblRuleDescription" EnableClientSideAPI="True" ReadOnly="True"
                                meta:resourcekey="txtRuleDescriptionResource1">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidationGroup="RulesEditForm" CausesValidation="True">
                                    <RequiredField ErrorText="Este valor es obligatorio" IsRequired="True" />
                                </ValidationSettings>
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%">
                            <dxe:ASPxCheckBox ID="chkManualRule" runat="server" Text="Regla Manual" ClientInstanceName="chkManualRule" Value='<%# Bind("IsManualRule") %>' meta:resourcekey="chkManualRuleResource1">
                            <ClientSideEvents CheckedChanged="function(s, e) {
                                txtManualRule.SetEnabled(s.GetChecked());
                                ddeRules.SetEnabled(!s.GetChecked());
                                if(s.GetChecked()){
                                    ddeRules.SetValue(null);
                                    lblRuleDescription.SetText('');
                                }
                            }" Init="function(s, e) {
	                            if (txtManualRule.GetText() != ''){
		                            chkManualRule.SetChecked(true);
                                    txtManualRule.SetEnabled(s.GetChecked());
                                    ddeRules.SetValue(null);
                                    lblRuleDescription.SetText('');
                                    ddeRules.SetEnabled(!s.GetChecked());
                                }
                            }" />
                            </dxe:ASPxCheckBox>
                        </td>
                        <td width="40%" colspan="2">
                            <dxe:ASPxTextBox ID="txtManualRule" runat="server" Width="100%" ClientInstanceName="txtManualRule"
                                CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange"
                                SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" EnableClientSideAPI="True"
                                OnValidation="txtManualRule_Validation" Value='<%# Bind("TemporalRuleDescription") %>'
                                meta:resourcekey="txtManualRuleResource1">
                            </dxe:ASPxTextBox>
                        </td>
                        <td width="10%">
                            <dxe:ASPxLabel ID="lblUnderwritingRule" runat="server" Text="Área de Suscripción:"
                                meta:resourcekey="lblUnderwritingRuleResource1">
                            </dxe:ASPxLabel>
                        </td>
                        <td colspan="2" width="40%">
                            <dxe:ASPxComboBox ID="cmbUnderwritingArea" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                Value='<%# Bind("UnderwritingArea") %>' Width="300px" meta:resourcekey="cmbUnderwritingAreaResource1"
                                TextField="Description" ValueField="Code" ValueType="System.Int32" OnDataBinding="cmbUnderwritingArea_DataBinding">
                                <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                                </LoadingPanelImage>
                                <Paddings PaddingLeft="8px" />
                                <BackgroundImage HorizontalPosition="left" ImageUrl="/Underwriting/Images/required.png" VerticalPosition="center" Repeat="NoRepeat" />
                                <ValidationSettings ValidationGroup="RulesEditForm" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="El valor es obligatorio" />
                                </ValidationSettings>
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td width="10%">
                            <dxe:ASPxLabel ID="lblRuleExplain" runat="server" Text="Explicación de Regla:" meta:resourcekey="lblRuleExplainResource1"></dxe:ASPxLabel>
                        </td>
                        <td width="16px">
                            <dxe:ASPxButton ID="btnRuleExplain" runat="server" EnableDefaultAppearance="False"
                                Height="16px" Width="16px" AutoPostBack="False" Cursor="pointer" meta:resourcekey="btnRuleExplainResource1">
                                <ClientSideEvents Click="function(s,e){ ppEditExplainRule.Show(); }" />
                                <Image Url="/Underwriting/Images/text_signature.png">
                                </Image>
                            </dxe:ASPxButton>
                        </td>
                        <td width="120px">
                            <dxe:ASPxLabel ID="lblAtumaticPoints" runat="server" Text="Puntos Automáticos:" meta:resourcekey="lblAtumaticPointsResource1"></dxe:ASPxLabel>
                        </td>
                        <td class="style7">
                            <dxe:ASPxSpinEdit ID="seAutomaticPoints" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                CssPostfix="SoftOrange" Enabled="False" Height="21px" Number='<%# Eval("AutomaticPoints") %>'
                                SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" Width="70px" ClientInstanceName="seAutomaticPoints"
                                DisplayFormatString="+#;-#;0" meta:resourcekey="seAutomaticPointsResource1">
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td class="style7">
                            <dxe:ASPxLabel ID="lblManualPoints0" runat="server" Text="Puntos Manuales:" meta:resourcekey="lblManualPoints0Resource1"></dxe:ASPxLabel>
                        </td>
                        <td class="style7">
                            <dxe:ASPxSpinEdit ID="seManualPoints" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                CssPostfix="SoftOrange" Height="21px" Number='<%# Bind("ManualPoints") %>' SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                Width="70px" ClientInstanceName="seManualPoints" DisplayFormatString="+#;-#;0"
                                meta:resourcekey="seManualPointsResource1">
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td class="style7">
                            <dxe:ASPxLabel ID="lblFinalPoints" runat="server" Text="Puntuación Final:" meta:resourcekey="lblFinalPointsResource1"></dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxSpinEdit ID="seFinalPoints" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                CssPostfix="SoftOrange" Height="21px" Number='<%# Eval("FinalPoints") %>' SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                Width="70px" ClientInstanceName="seFinalPoints" Enabled="False" DisplayFormatString="+#;-#;0"
                                meta:resourcekey="seFinalPointsResource1">
                            </dxe:ASPxSpinEdit>
                        </td>
                        <td>
                            <dxe:ASPxLabel ID="lblAlarmType" runat="server" Text="Tipo de Alarma:" meta:resourcekey="lblAlarmTypeResource1"></dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxComboBox ID="cmbAlarmType" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                Value='<%# Bind("AlarmType") %>' meta:resourcekey="cmbAlarmTypeResource1" TextField="Description"
                                ValueField="Code" ValueType="System.Int32" OnDataBinding="cmbAlarmType_DataBinding">
                                <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                                </LoadingPanelImage>
                                <Paddings PaddingLeft="8px" />
                                <ClientSideEvents Init="function(s,e){ if(s.GetSelectedItem() != null){ if(s.GetSelectedItem().value == 4){ SetFlatExtraPremiumRoundPanelVisible(); } else if(s.GetSelectedItem().value == 5){ SetExclusionRoundPanelVisible(); } else { SetRoundBoxesVisible(false, false); } }else { SetRoundBoxesVisible(false, false); } }"
                                    ValueChanged="function(s,e){ if(s.GetSelectedItem() != null){ if(s.GetSelectedItem().value == 4){ SetFlatExtraPremiumRoundPanelVisible(); } else if(s.GetSelectedItem().value == 5){ SetExclusionRoundPanelVisible(); } else { SetRoundBoxesVisible(false, false); } }else { SetRoundBoxesVisible(false, false); } }" />
                                <BackgroundImage HorizontalPosition="left" ImageUrl="/Underwriting/Images/required.png"
                                    VerticalPosition="center" Repeat="NoRepeat" />
                                <ValidationSettings ValidationGroup="RulesEditForm" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="El valor es obligatorio" />
                                </ValidationSettings>
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding: 10px 4px 3px 4px">
                <dxrp:ASPxRoundPanel ID="rpFlatExtraPremium" runat="server" Width="100%" BackColor="#E5EECF"
                    ClientInstanceName="rpFlatExtraPremium" EnableClientSideAPI="True" CssFilePath="~/App_Themes/Youthful/{0}/styles.css"
                    CssPostfix="Youthful" HeaderText="Información de flat extra prima" SpriteCssFilePath="~/App_Themes/Youthful/{0}/sprite.css"
                    meta:resourcekey="rpFlatExtraPremiumResource1">
                    <ContentPaddings Padding="6px" PaddingBottom="6px" PaddingTop="4px" />
                    <HeaderRightEdge>
                        <BackgroundImage ImageUrl="~/App_Themes/Youthful/Web/rpHeaderLeftEdge.gif" VerticalPosition="bottom" />
                    </HeaderRightEdge>
                    <Border BorderStyle="None" />
                    <HeaderLeftEdge>
                        <BackgroundImage ImageUrl="~/App_Themes/Youthful/Web/rpHeaderLeftEdge.gif" VerticalPosition="bottom" />
                    </HeaderLeftEdge>
                    <HeaderStyle BackColor="#D3E4A6">
                        <Paddings PaddingBottom="5px" PaddingTop="0px" />
                        <BorderBottom BorderStyle="None" />
                    </HeaderStyle>
                    <HeaderContent>
                        <BackgroundImage ImageUrl="~/App_Themes/Youthful/Web/rpHeaderSeparator.gif" Repeat="RepeatX"
                            VerticalPosition="bottom" />
                    </HeaderContent>
                    <PanelCollection>
                        <dxp:PanelContent runat="server" Width="100%" meta:resourcekey="PanelContent1Resource1">
                            <div style="padding: 4px 4px 4px 4px; text-align: center;">
                                <table width="500px">
                                    <tr>
                                        <td width="175px">
                                        </td>
                                        <td width="175px">
                                        </td>
                                        <td align="center" colspan="3" width="150px">
                                            <dxe:ASPxLabel ID="lblExtraPremiumDuration" runat="server" Text="Duración Extra Prima"
                                                meta:resourcekey="lblExtraPremiumDurationResource1">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="175px" align="left">
                                            <dxe:ASPxLabel ID="lblFlatExtraPremium" runat="server" Text="Flat Extra Prima" meta:resourcekey="lblFlatExtraPremiumResource1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="left" width="175px">
                                            <dxe:ASPxLabel ID="lblExclusionPeriodType" runat="server" Text="Período" meta:resourcekey="lblExclusionPeriodTypeResource1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="left" width="50px">
                                            <dxe:ASPxLabel ID="lblExtraPremiumDurationYears" runat="server" Text="Años" meta:resourcekey="lblExtraPremiumDurationYearsResource1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="left" width="50px">
                                            <dxe:ASPxLabel ID="lblExtraPremiumDurationMonths" runat="server" Text="Meses" meta:resourcekey="lblExtraPremiumDurationMonthsResource1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="left" width="50px">
                                            <dxe:ASPxLabel ID="lblExtraPremiumDurationDays" runat="server" Text="Días" meta:resourcekey="lblExtraPremiumDurationDaysResource1">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" width="175px">
                                            <dxe:ASPxTextBox ID="txtExtraFlatPremium" runat="server" Width="100px" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                                CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                                Value='<%# Bind("FlatExtraPremium") %>' HorizontalAlign="Right" meta:resourcekey="txtExtraFlatPremiumResource1">
                                                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999999999999999g&gt;.&lt;00..99&gt;" />
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                </ValidationSettings>
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td align="left" width="175px">
                                            <dxe:ASPxComboBox ID="cmbFlatPeriodType" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                                CssPostfix="SoftOrange" OnDataBinding="cmbFlatPeriodType_DataBinding" 
                                                SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                                Value='<%# Bind("ExclusionPeriodType") %>' ClientInstanceName="cmbFlatPeriodType"
                                                Width="100%" meta:resourcekey="cmbFlatPeriodTypeResource1" TextField="Description"
                                                ValueField="Code" ValueType="System.Int32">
                                                <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                                                </LoadingPanelImage>
                                                <ValidationSettings ErrorDisplayMode="None">
                                                </ValidationSettings>
                                                <ClientSideEvents ValueChanged="function(s,e){ 
                                                                        if(s.GetSelectedItem() != null)
                                                                        {
                                                                                  if(s.GetSelectedItem().value == 1){ 
                                                                                      txtDurationOfExtraPremiumYears.SetEnabled(false);
                                                                                      txtDurationOfExtraPremiumMonths.SetEnabled(false);
                                                                                      txtDurationOfExtraPremiumDays.SetEnabled(false); 
                                                                                  }
                                                                                  else{
                                                                                      txtDurationOfExtraPremiumYears.SetEnabled(true);
                                                                                      txtDurationOfExtraPremiumMonths.SetEnabled(true);
                                                                                      txtDurationOfExtraPremiumDays.SetEnabled(true);
                                                                                  }
                                                                         }
                                                                          else{
                                                                              txtDurationOfExtraPremiumYears.SetEnabled(false);
                                                                              txtDurationOfExtraPremiumMonths.SetEnabled(false);
                                                                              txtDurationOfExtraPremiumDays.SetEnabled(false);
                                                                          }                                                                         
                                                                   }" Init="function(s,e){ 
                                                                        if(s.GetSelectedItem() != null)
                                                                        {
                                                                                  if(s.GetSelectedItem().value == 1){ 
                                                                                      txtDurationOfExtraPremiumYears.SetEnabled(false);
                                                                                      txtDurationOfExtraPremiumMonths.SetEnabled(false);
                                                                                      txtDurationOfExtraPremiumDays.SetEnabled(false); 
                                                                                  }
                                                                                  else{
                                                                                      txtDurationOfExtraPremiumYears.SetEnabled(true);
                                                                                      txtDurationOfExtraPremiumMonths.SetEnabled(true);
                                                                                      txtDurationOfExtraPremiumDays.SetEnabled(true);
                                                                                  }
                                                                         }
                                                                          else{
                                                                              txtDurationOfExtraPremiumYears.SetEnabled(false);
                                                                              txtDurationOfExtraPremiumMonths.SetEnabled(false);
                                                                              txtDurationOfExtraPremiumDays.SetEnabled(false);
                                                                          }                                                                         
                                                                   }" />
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td align="left" width="50px">
                                            <dxe:ASPxTextBox ID="txtDurationOfExtraPremiumYears" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                                CssPostfix="SoftOrange" HorizontalAlign="Right" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                                Text='<%# Bind("DurationOfFlatExtraPremiumYears") %>' Width="50px" ClientInstanceName="txtDurationOfExtraPremiumYears"
                                                EnableClientSideAPI="True" Height="19px" meta:resourcekey="txtDurationOfExtraPremiumYearsResource1">
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="False">
                                                </ValidationSettings>
                                                <MaskSettings Mask="<0..1000>" />
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td align="left" width="50px">
                                            <dxe:ASPxTextBox ID="txtDurationOfExtraPremiumMonths" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                                CssPostfix="SoftOrange" HorizontalAlign="Right" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                                Text='<%# Bind("DurationOfFlatExtraPremiumMonths") %>' Width="50px" ClientInstanceName="txtDurationOfExtraPremiumMonths"
                                                EnableClientSideAPI="True" meta:resourcekey="txtDurationOfExtraPremiumMonthsResource1">
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="False">
                                                </ValidationSettings>
                                                <MaskSettings Mask="<0..1000>" />
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td align="left" width="50px">
                                            <dxe:ASPxTextBox ID="txtDurationOfExtraPremiumDays" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                                CssPostfix="SoftOrange" HorizontalAlign="Right" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                                Text='<%# Bind("DurationOfFlatExtraPremiumDays") %>' Width="50px" ClientInstanceName="txtDurationOfExtraPremiumDays"
                                                EnableClientSideAPI="True" meta:resourcekey="txtDurationOfExtraPremiumDaysResource1">
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="False">
                                                </ValidationSettings>
                                                <MaskSettings Mask="<0..1000>" />
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxrp:ASPxRoundPanel>
            </div>
            <div style="padding: 4px 4px 4px 4px; text-align: center" id="divExclusions">
                <dxrp:ASPxRoundPanel ID="rpExclusion" runat="server" Width="100%" BackColor="#E5EECF"
                    ClientInstanceName="rpExclusion" EnableClientSideAPI="True" CssFilePath="~/App_Themes/Youthful/{0}/styles.css"
                    CssPostfix="Youthful" HeaderText="Exclusiones" SpriteCssFilePath="~/App_Themes/Youthful/{0}/sprite.css"
                    meta:resourcekey="rpExclusionResource1">
                    <ContentPaddings Padding="6px" PaddingBottom="6px" PaddingTop="4px" />
                    <HeaderRightEdge>
                        <BackgroundImage ImageUrl="~/App_Themes/Youthful/Web/rpHeaderLeftEdge.gif" VerticalPosition="bottom" />
                    </HeaderRightEdge>
                    <Border BorderStyle="None" />
                    <HeaderLeftEdge>
                        <BackgroundImage ImageUrl="~/App_Themes/Youthful/Web/rpHeaderLeftEdge.gif" VerticalPosition="bottom" />
                    </HeaderLeftEdge>
                    <HeaderStyle BackColor="#D3E4A6">
                        <Paddings PaddingBottom="5px" PaddingTop="0px" />
                        <BorderBottom BorderStyle="None" />
                    </HeaderStyle>
                    <HeaderContent>
                        <BackgroundImage ImageUrl="~/App_Themes/Youthful/Web/rpHeaderSeparator.gif" Repeat="RepeatX"
                            VerticalPosition="bottom" />
                    </HeaderContent>
                    <PanelCollection>
                        <dxp:PanelContent runat="server" meta:resourcekey="PanelContent2Resource1">
                            <div style="padding: 4px 4px 4px 4px; text-align: center;">
                                <table width="600px">
                                    <tr>
                                        <td align="center" width="20%">
                                        </td>
                                        <td align="center" width="20%">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="text-align: center" colspan="3">
                                            <dxe:ASPxLabel ID="lblWaitingPeriod" runat="server" Text="Duración Plazo" meta:resourcekey="lblWaitingPeriodResource1">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <dxe:ASPxLabel ID="lblExclusionType" runat="server" Text="Tipo de Exclusión" meta:resourcekey="lblExclusionTypeResource1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="left">
                                            <dxe:ASPxLabel ID="lblPeriod" runat="server" Text="Período" meta:resourcekey="lblPeriodResource1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="left">
                                            <dxe:ASPxLabel ID="lblCover" runat="server" Text="Cobertura" meta:resourcekey="lblCoverResource1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td align="left">
                                            <dxe:ASPxLabel ID="lblIllness" runat="server" Text="Enfermedad" meta:resourcekey="lblIllnessResource1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td width="9%">
                                            <dxe:ASPxLabel ID="lblWaitingPeriodYears" runat="server" Text="Años" meta:resourcekey="lblWaitingPeriodYearsResource1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td width="9%">
                                            <dxe:ASPxLabel ID="lblWaitingPeriodMonths" runat="server" Text="Meses" meta:resourcekey="lblWaitingPeriodMonthsResource1">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td width="9%">
                                            <dxe:ASPxLabel ID="lblWaitingPeriodDays" runat="server" Text="Días" meta:resourcekey="lblWaitingPeriodDaysResource1">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <dxe:ASPxComboBox ID="cmbExclusionType" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                                CssPostfix="SoftOrange" OnDataBinding="cmbExclusionType_DataBinding" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                                Value='<%# Bind("ExclusionType") %>' meta:resourcekey="cmbExclusionTypeResource1"
                                                TextField="Description" ValueField="Code" ValueType="System.Int32">
                                                <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                                                </LoadingPanelImage>
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td align="left">
                                            <dxe:ASPxComboBox ID="cmbExclusionPeriodType" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                                CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                                Value='<%# Bind("ExclusionPeriodType") %>' ClientInstanceName="cmbExclusionPeriodType"
                                                meta:resourcekey="cmbExclusionPeriodTypeResource1" TextField="Description" ValueField="Code"
                                                ValueType="System.Int32" OnDataBinding="cmbExclusionPeriodType_DataBinding">
                                                <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                                                </LoadingPanelImage>
                                                <ClientSideEvents ValueChanged="function(s,e){ 
                                                                       if(s.GetSelectedItem() != null)
                                                                       {     
                                                                            if(s.GetSelectedItem().value != 1){ 
                                                                                      txtWaitingPeriodYears.SetEnabled(true);
                                                                                      txtWaitingPeriodMonths.SetEnabled(true);
                                                                                      txtWaitingPeriodDays.SetEnabled(true); 
                                                                                      }
                                                                              else{
                                                                                  txtWaitingPeriodYears.SetEnabled(false);
                                                                                  txtWaitingPeriodMonths.SetEnabled(false);
                                                                                  txtWaitingPeriodDays.SetEnabled(false);
                                                                              }
                                                                       }
                                                                       else{
                                                                           txtWaitingPeriodYears.SetEnabled(false);
                                                                           txtWaitingPeriodMonths.SetEnabled(false);
                                                                           txtWaitingPeriodDays.SetEnabled(false);
                                                                       }
                                                                   }" Init="function(s,e){ 
                                                                   if(s.GetSelectedItem() != null)
                                                                       {     
                                                                            if(s.GetSelectedItem().value != 1){ 
                                                                                  txtWaitingPeriodYears.SetEnabled(true);
                                                                                  txtWaitingPeriodMonths.SetEnabled(true);
                                                                                  txtWaitingPeriodDays.SetEnabled(true); 
                                                                            }
                                                                            else{
                                                                                  txtWaitingPeriodYears.SetEnabled(false);
                                                                                  txtWaitingPeriodMonths.SetEnabled(false);
                                                                                  txtWaitingPeriodDays.SetEnabled(false);
                                                                            }
                                                                       }
                                                                       else{
                                                                           txtWaitingPeriodYears.SetEnabled(false);
                                                                           txtWaitingPeriodMonths.SetEnabled(false);
                                                                           txtWaitingPeriodDays.SetEnabled(false);
                                                                       }
                                                                   }" />
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td>
                                            <dxe:ASPxComboBox ID="cmbCover" runat="server" ValueType="System.Int32" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                                CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                                Value='<%# Bind("Coverage") %>' meta:resourcekey="cmbCoverResource1">
                                                <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                                                </LoadingPanelImage>
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td>
                                            <dxe:ASPxComboBox ID="cmbIllness" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                                CssPostfix="SoftOrange" OnDataBinding="cmbIllness_DataBinding" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                                TextField="Description" ValueField="Code" ValueType="System.Int32" meta:resourcekey="cmbIllnessResource2">
                                                <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                                                </LoadingPanelImage>
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td width="9%">
                                            <dxe:ASPxTextBox ID="txtWaitingPeriodYears" runat="server" Width="50px" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                                CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                                Text='<%# Bind("WaitingPeriodYears") %>' HorizontalAlign="Right" ClientInstanceName="txtWaitingPeriodYears"
                                                EnableClientSideAPI="True" meta:resourcekey="txtWaitingPeriodYearsResource1">
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="False">
                                                </ValidationSettings>
                                                <MaskSettings Mask="<0..1000>" />
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td width="9%">
                                            <dxe:ASPxTextBox ID="txtWaitingPeriodMonths" runat="server" Width="50px" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                                CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                                Text='<%# Bind("WaitingPeriodMonths") %>' HorizontalAlign="Right" ClientInstanceName="txtWaitingPeriodMonths"
                                                EnableClientSideAPI="True" meta:resourcekey="txtWaitingPeriodMonthsResource1">
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="False">
                                                </ValidationSettings>
                                                <MaskSettings Mask="<0..1000>" />
                                            </dxe:ASPxTextBox>
                                        </td>
                                        <td width="9%">
                                            <dxe:ASPxTextBox ID="txtWaitingPeriodDays" runat="server" Width="50px" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                                CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                                Text='<%# Bind("WaitingPeriodDays") %>' HorizontalAlign="Right" ClientInstanceName="txtWaitingPeriodDays"
                                                EnableClientSideAPI="True" meta:resourcekey="txtWaitingPeriodDaysResource1">
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidateOnLeave="False">
                                                </ValidationSettings>
                                                <MaskSettings Mask="<0..1000>" />
                                            </dxe:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </dxp:PanelContent>
                    </PanelCollection>
                </dxrp:ASPxRoundPanel>
            </div>
            <div style="padding: 4px 4px 4px 4px">
                <table>
                    <tr>
                        <td width="100px">
                            <dxe:ASPxLabel ID="lblStatus" runat="server" Text="Estado:" meta:resourcekey="lblStatusResource1">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxComboBox ID="cmbStatus" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                Value='<%# Bind("Status") %>' meta:resourcekey="cmbStatusResource1" TextField="Description"
                                ValueField="Code" ValueType="System.Int32" OnDataBinding="cmbStatus_DataBinding">
                                <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                                </LoadingPanelImage>
                            </dxe:ASPxComboBox>
                        </td>
                        <td width="100px">
                            <dxe:ASPxLabel ID="lblCreatedBy" runat="server" Text="Creado por:" meta:resourcekey="lblCreatedByResource1">
                            </dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txtCreatedBy" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                CssPostfix="SoftOrange" Enabled="False" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                Width="170px" Text='<%# Eval("CreatedBy") %>' meta:resourcekey="txtCreatedByResource1">
                            </dxe:ASPxTextBox>
                        </td>
                        <td>
                            <dxe:ASPxLabel ID="lblUnderRuleId" runat="server" Text='<%# Bind("UnderRuleId") %>'
                                Visible="False" meta:resourcekey="lblUnderRuleIdResource1">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="text-align: right; padding: 2px 2px 2px 2px">
                <dxe:ASPxHyperLink ID="hlUpdate" runat="server" Text="Salvar" Cursor="pointer" Font-Underline="True"
                    meta:resourcekey="hlUpdateResource1">
                    <ClientSideEvents Click="function(s, e) {        
        if(chkManualRule.GetChecked())
            lblRuleDescription.SetEnabled(false);
        else
            lblRuleDescription.SetEnabled(true);
        
        if(ASPxClientEdit.ValidateGroup(&quot;RulesEditForm&quot;))    
            rulesGrid.UpdateEdit();
}" />
                </dxe:ASPxHyperLink>
                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                    runat="server" ColumnID=""></dxwgv:ASPxGridViewTemplateReplacement>
            </div>
            <dxpc:ASPxPopupControl ID="ppEditExplainRule" runat="server" ClientInstanceName="ppEditExplainRule"
                CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange"
                EnableHotTrack="False" HeaderText="Explicación de la regla" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" AllowDragging="True"
                meta:resourcekey="ppEditExplainRuleResource1">
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server" meta:resourcekey="PopupControlContentControl1Resource1">
                        <dxe:ASPxMemo ID="mmExplainRule" runat="server" Height="110px" Width="500px" Text='<%# Bind("Explanation") %>' meta:resourcekey="mmExplainRuleResource2">
                        </dxe:ASPxMemo>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
                <HeaderStyle>
                    <Paddings PaddingRight="6px" />
                </HeaderStyle>
            </dxpc:ASPxPopupControl>
        </EditForm>
    </Templates>
    <SettingsEditing PopupEditFormAllowResize="True" />
    <ClientSideEvents DetailRowExpanding="UnderwritingRuleExpandRow" />
    <Columns>
        <dxwgv:GridViewDataTextColumn FieldName="UnderRuleId" Visible="False" VisibleIndex="7"
            meta:resourcekey="GridViewDataTextColumnResource7">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn FieldName="QuestionId" VisibleIndex="0" Caption="Id pregunta"
            Width="50px" meta:resourcekey="GridViewDataTextColumnResource10">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataComboBoxColumn Caption="Descripción de la pregunta" FieldName="QuestionId"
            VisibleIndex="1" Width="300px" meta:resourcekey="GridViewDataComboBoxColumnResource2">
            <PropertiesComboBox TextField="Description" ValueField="Code" ValueType="System.Int32">
            </PropertiesComboBox>
        </dxwgv:GridViewDataComboBoxColumn>
        <dxwgv:GridViewDataTextColumn FieldName="UnderwritingRuleId" Caption="Regla" VisibleIndex="2"
            Width="50px" meta:resourcekey="GridViewDataTextColumnResource16">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataTextColumn Caption="Descripción" FieldName="RuleDescription" VisibleIndex="3"
            Width="300px">
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewDataComboBoxColumn Caption="Área de suscripción" FieldName="UnderwritingArea"
            VisibleIndex="4" Width="70px" meta:resourcekey="GridViewDataComboBoxColumnResource4">
            <PropertiesComboBox TextField="Description" ValueField="Code" ValueType="System.Int32">
            </PropertiesComboBox>
        </dxwgv:GridViewDataComboBoxColumn>
        <dxwgv:GridViewDataTextColumn VisibleIndex="5" Caption="Explicación de la regla"
            Width="50px" meta:resourcekey="GridViewDataTextColumnResource15" FieldName="Explanation"
            Name="Explanation">
            <DataItemTemplate>
                <dxe:ASPxButton ID="btnLinkToWorkflow" runat="server" EnableDefaultAppearance="False"
                    EnableTheming="True" Height="16px" Width="16px" meta:resourcekey="btnLinkToWorkflowResource1"
                    AutoPostBack="False" Cursor="pointer">
                    <Image Url="/Underwriting/Images/question_before_pressed.png" UrlPressed="/Underwriting/Images/question_after_pressed.png">
                    </Image>
                </dxe:ASPxButton>
            </DataItemTemplate>
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dxwgv:GridViewDataTextColumn>
        <dxwgv:GridViewCommandColumn VisibleIndex="6" Width="50px" Caption=" " meta:resourcekey="GridViewCommandColumnResource2">
            <DeleteButton Text="Anular" Visible="True">
            </DeleteButton>
            <NewButton Text="Insertar" Visible="true">
            </NewButton>
            <CancelButton Text="Cancelar" Visible="true">
            </CancelButton>
        </dxwgv:GridViewCommandColumn>
    </Columns>
    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" IsDetailGrid="True" />
</dxwgv:ASPxGridView>
<asp:ObjectDataSource 
    ID="dsUnderwritingRules" 
    runat="server" 
    OldValuesParameterFormatString="original_{0}"
    SelectMethod="SelectAll" 
    InsertMethod="InsertOnCache" 
    DeleteMethod="DeleteOnCache" 
    UpdateMethod="UpdateOnCache"
    ConflictDetection="CompareAllValues"
    TypeName="InMotionGIT.Underwriting.Proxy.Helpers.UnderwritingRule"
    DataObjectTypeName="InMotionGIT.Underwriting.Contracts.UnderwritingRule">
    <DeleteParameters>
        <asp:Parameter Name="original_RuleDescription" Type="String" />
        <asp:Parameter Name="original_UnderwritingArea" Type="Int32" />
        <asp:Parameter Name="original_ManualPoints" Type="Int32" />
        <asp:Parameter Name="original_AlarmType" Type="Int32" />
        <asp:Parameter Name="original_FlatExtraPremium" Type="Decimal" />
        <asp:Parameter Name="original_DurationOfFlatExtraPremiumYears" Type="Int32" />
        <asp:Parameter Name="original_DurationOfFlatExtraPremiumMonths" Type="Int32" />
        <asp:Parameter Name="original_DurationOfFlatExtraPremiumDays" Type="Int32" />
        <asp:Parameter Name="original_ExclusionType" Type="Int32" />
        <asp:Parameter Name="original_ExclusionPeriodType" Type="Int32" />
        <asp:Parameter Name="original_Coverage" Type="Int32" />
        <asp:Parameter Name="original_ImpairmentCode" Type="Int32" />
        <asp:Parameter Name="original_WaitingPeriodYears" Type="Int32" />
        <asp:Parameter Name="original_WaitingPeriodMonths" Type="Int32" />
        <asp:Parameter Name="original_WaitingPeriodDays" Type="Int32" />
        <asp:Parameter Name="original_Status" Type="Int32" />
        <asp:Parameter Name="original_UnderwritingRuleId" Type="String" />
        <asp:Parameter Name="original_UnderRuleId" Type="String" />
        <asp:Parameter Name="original_QuestionId" Type="String" />
        <asp:Parameter Name="original_QuestionDescription" Type="String" />
        <asp:Parameter Name="original_IsManualRule" Type="Boolean" />
        <asp:Parameter Name="original_TemporalRuleDescription" Type="String" />
        <asp:Parameter Name="original_Explanation" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="QuestionId" Type="String" />
        <asp:Parameter Name="QuestionDescription" Type="String" />
        <asp:Parameter Name="RuleDescription" Type="String" />
        <asp:Parameter Name="UnderwritingArea" Type="Int32" />
        <asp:Parameter Name="ManualPoints" Type="Int32" />
        <asp:Parameter Name="AlarmType" Type="Int32" />
        <asp:Parameter Name="FlatExtraPremium" Type="Decimal" />
        <asp:Parameter Name="DurationOfFlatExtraPremiumYears" Type="Int32" />
        <asp:Parameter Name="DurationOfFlatExtraPremiumMonths" Type="Int32" />
        <asp:Parameter Name="DurationOfFlatExtraPremiumDays" Type="Int32" />
        <asp:Parameter Name="ExclusionType" Type="Int32" />
        <asp:Parameter Name="ExclusionPeriodType" Type="Int32" />
        <asp:Parameter Name="Coverage" Type="Int32" />
        <asp:Parameter Name="ImpairmentCode" Type="Int32" />
        <asp:Parameter Name="WaitingPeriodYears" Type="Int32" />
        <asp:Parameter Name="WaitingPeriodMonths" Type="Int32" />
        <asp:Parameter Name="WaitingPeriodDays" Type="Int32" />
        <asp:Parameter Name="Status" Type="Int32" />
        <asp:Parameter Name="UnderwritingRuleId" Type="String" />
        <asp:Parameter Name="UnderRuleId" Type="String" />
        <asp:Parameter Name="Explanation" Type="String" />
        <asp:Parameter Name="IsManualRule" Type="Boolean" />
        <asp:Parameter Name="TemporalRuleDescription" Type="String" />
        <asp:Parameter Name="original_QuestionId" Type="String" />
        <asp:Parameter Name="original_QuestionDescription" Type="String" />
        <asp:Parameter Name="original_RuleDescription" Type="String" />
        <asp:Parameter Name="original_UnderwritingArea" Type="Int32" />
        <asp:Parameter Name="original_ManualPoints" Type="Int32" />
        <asp:Parameter Name="original_AlarmType" Type="Int32" />
        <asp:Parameter Name="original_FlatExtraPremium" Type="Decimal" />
        <asp:Parameter Name="original_DurationOfFlatExtraPremiumYears" Type="Int32" />
        <asp:Parameter Name="original_DurationOfFlatExtraPremiumMonths" Type="Int32" />
        <asp:Parameter Name="original_DurationOfFlatExtraPremiumDays" Type="Int32" />
        <asp:Parameter Name="original_ExclusionType" Type="Int32" />
        <asp:Parameter Name="original_ExclusionPeriodType" Type="Int32" />
        <asp:Parameter Name="original_Coverage" Type="Int32" />
        <asp:Parameter Name="original_ImpairmentCode" Type="Int32" />
        <asp:Parameter Name="original_WaitingPeriodYears" Type="Int32" />
        <asp:Parameter Name="original_WaitingPeriodMonths" Type="Int32" />
        <asp:Parameter Name="original_WaitingPeriodDays" Type="Int32" />
        <asp:Parameter Name="original_Status" Type="Int32" />
        <asp:Parameter Name="original_UnderwritingRuleId" Type="String" />
        <asp:Parameter Name="original_UnderRuleId" Type="String" />
        <asp:Parameter Name="original_IsManualRule" Type="Boolean" />
        <asp:Parameter Name="original_TemporalRuleDescription" Type="String" />
        <asp:Parameter Name="original_Explanation" Type="String" />
    </UpdateParameters>
    <SelectParameters>
        <asp:Parameter Name="languageId" Type="Int32" />
    </SelectParameters>
    <InsertParameters>
        <asp:Parameter Name="QuestionId" Type="String" />
        <asp:Parameter Name="QuestionDescription" Type="String" />
        <asp:Parameter Name="UnderwritingRuleId" Type="String" />
        <asp:Parameter Name="RuleDescription" Type="String" />
        <asp:Parameter Name="UnderwritingArea" Type="Int32" />
        <asp:Parameter Name="ManualPoints" Type="Int32" />
        <asp:Parameter Name="AlarmType" Type="Int32" />
        <asp:Parameter Name="FlatExtraPremium" Type="Decimal" />
        <asp:Parameter Name="DurationOfFlatExtraPremiumYears" Type="Int32" />
        <asp:Parameter Name="DurationOfFlatExtraPremiumMonths" Type="Int32" />
        <asp:Parameter Name="DurationOfFlatExtraPremiumDays" Type="Int32" />
        <asp:Parameter Name="ExclusionType" Type="Int32" />
        <asp:Parameter Name="ExclusionPeriodType" Type="Int32" />
        <asp:Parameter Name="Coverage" Type="Int32" />
        <asp:Parameter Name="ImpairmentCode" Type="Int32" />
        <asp:Parameter Name="WaitingPeriodYears" Type="Int32" />
        <asp:Parameter Name="WaitingPeriodMonths" Type="Int32" />
        <asp:Parameter Name="WaitingPeriodDays" Type="Int32" />
        <asp:Parameter Name="Status" Type="Int32" />
        <asp:Parameter Name="UnderRuleId" Type="String" />
        <asp:Parameter Name="isManualRule" Type="Boolean" />
        <asp:Parameter Name="TemporalRuleDescription" Type="String" />
        <asp:Parameter Name="Explanation" Type="String" />
        <asp:Parameter Name="languageId" Type="Int32" />
    </InsertParameters>
</asp:ObjectDataSource>
<dxpc:ASPxPopupControl ID="ppGridEditExplainRule" runat="server" ClientInstanceName="ppGridEditExplainRule"
    CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange"
    EnableHotTrack="False" HeaderText="Explicación de la regla" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" AllowDragging="True"
    meta:resourcekey="ppEditExplainRuleResource1">
    <ContentCollection>
        <dxpc:PopupControlContentControl runat="server" meta:resourcekey="PopupControlContentControl1Resource1">
            <dxe:ASPxMemo ID="mmGridExplainRule" runat="server" Height="110px" Width="500px" ClientInstanceName="mmGridExplainRule" EnableClientSideAPI="True" meta:resourcekey="mmGridExplainRuleResource1">
            </dxe:ASPxMemo>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
    <HeaderStyle>
        <Paddings PaddingRight="6px" />
    </HeaderStyle>
</dxpc:ASPxPopupControl>
<dx:ASPxHiddenField ID="hdnIsEditingMode" runat="server" ClientInstanceName="hdnIsEditingModeOnRules">
</dx:ASPxHiddenField>
