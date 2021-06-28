<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SuscriptionRulesInformation.ascx.vb"
    Inherits="Underwriting_Controls_SuscriptionRulesInformation" %>
<div style="padding: 4px 4px 3px 4px;">
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
                    SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" Width="100%" Value='<%# Eval("QuestionId") %>'
                    Enabled="False" meta:resourcekey="ddeQuestionResource2">
                    <ButtonStyle Width="13px">
                    </ButtonStyle>
                </dxe:ASPxDropDownEdit>
            </td>
            <td width="30%">
                <dxe:ASPxLabel ID="lblQuestionDescription" runat="server" Text='<%# Eval("QuestionDescription") %>'
                    ClientInstanceName="lblQuestionDescription" EnableClientSideAPI="True" meta:resourcekey="lblQuestionDescriptionResource1">
                </dxe:ASPxLabel>
            </td>
            <td width="10%">
                <dxe:ASPxLabel ID="lblRule" runat="server" Text="Regla:" meta:resourcekey="lblRuleResource1">
                </dxe:ASPxLabel>
            </td>
            <td width="10%">
                <dxe:ASPxDropDownEdit ID="ddeRule" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                    CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                    ClientInstanceName="ddeRules" Width="100%" EnableClientSideAPI="True" Value='<%# Eval("UnderwritingRuleId") %>'
                    Enabled="False" meta:resourcekey="ddeRuleResource2">
                </dxe:ASPxDropDownEdit>
            </td>
            <td width="30%">
                <dxe:ASPxLabel ID="lblRuleDescription" runat="server" ClientInstanceName="lblRuleDescription"
                    EnableClientSideAPI="True" Text='<%# Eval("RuleDescription") %>' meta:resourcekey="lblRuleDescriptionResource1">
                </dxe:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td width="10%">
                <dxe:ASPxCheckBox ID="chkManualRule" runat="server" Text="Regla Manual" ClientInstanceName="chkManualRule"
                    ReadOnly="True" meta:resourcekey="chkManualRuleResource1">
                </dxe:ASPxCheckBox>
            </td>
            <td width="10%" colspan="2" style="width: 20%">
                <dxe:ASPxTextBox ID="txtManualRule" runat="server" Width="100%" ClientInstanceName="txtManualRule"
                    CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange"
                    SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" EnableClientSideAPI="True"
                    Enabled="False" meta:resourcekey="txtManualRuleResource1">
                </dxe:ASPxTextBox>
            </td>
            <td width="10%">
                <dxe:ASPxLabel ID="lblUnderwritingRule" runat="server" Text="Área de Suscripción:"
                    meta:resourcekey="lblUnderwritingRuleResource1">
                </dxe:ASPxLabel>
            </td>
            <td colspan="2">
                <dxe:ASPxComboBox ID="cmbUnderwritingArea" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                    CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                    Value='<%# Eval("UnderwritingArea") %>' Width="300px"
                    Enabled="False" meta:resourcekey="cmbUnderwritingAreaResource1" TextField="Description" ValueField="Code" ValueType="System.Int32">
                    <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                    </LoadingPanelImage>
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td width="10%" colspan="2">
                <dxe:ASPxLabel ID="lblRuleExplain" runat="server" Text="Explicación de Regla:" meta:resourcekey="lblRuleExplainResource1">
                </dxe:ASPxLabel>
            </td>
            <td width="10%">
                <dxe:ASPxButton ID="btnRukeExplain" runat="server" EnableDefaultAppearance="False"
                    Height="16px" Width="16px" Cursor="pointer" AutoPostBack="False" meta:resourcekey="btnRukeExplainResource1">
                    <ClientSideEvents Click="function(s,e){ ppExplainRule.Show(); }" />
                    <Image Url="/Underwriting/Images/text_signature.png">
                    </Image>
                </dxe:ASPxButton>
            </td>
            <td width="10%">
                <dxe:ASPxLabel ID="lblAtumaticPoints" runat="server" Text="Puntos Automáticos:" meta:resourcekey="lblAtumaticPointsResource1">
                </dxe:ASPxLabel>
            </td>
            <td width="10%">
                <dxe:ASPxSpinEdit ID="seAutomaticPoints" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                    CssPostfix="SoftOrange" Height="21px" Number='<%# Eval("AutomaticPoints") %>'
                    SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" Width="70px" DisplayFormatString="+#;-#;0"
                    ClientInstanceName="seAutomaticPoints" meta:resourcekey="seAutomaticPointsResource1"
                    ClientEnabled="False">
                </dxe:ASPxSpinEdit>
            </td>
            <td width="10%">
                <dxe:ASPxLabel ID="lblManualPoints0" runat="server" Text="Puntos Manuales:" meta:resourcekey="lblManualPoints0Resource1">
                </dxe:ASPxLabel>
            </td>
            <td width="10%">
                <dxe:ASPxSpinEdit ID="seManualPoints" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                    CssPostfix="SoftOrange" Height="21px" Number='<%# Eval("ManualPoints") %>' SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                    Width="70px" ClientInstanceName="seManualPoints" DisplayFormatString="+#;-#;0"
                    meta:resourcekey="seManualPointsResource1" ClientEnabled="False">
                </dxe:ASPxSpinEdit>
            </td>
            <td width="10%">
                <dxe:ASPxLabel ID="lblFinalPoints" runat="server" Text="Puntuación Final:" meta:resourcekey="lblFinalPointsResource1">
                </dxe:ASPxLabel>
            </td>
            <td>
                <dxe:ASPxSpinEdit ID="seFinalPoints" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                    CssPostfix="SoftOrange" Height="21px" Number='<%# Eval("FinalPoints") %>' SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                    Width="70px" ClientInstanceName="seFinalPoints" DisplayFormatString="+#;-#;0"
                    meta:resourcekey="seFinalPointsResource1" ClientEnabled="False">
                </dxe:ASPxSpinEdit>
            </td>
            <td>
                <dxe:ASPxLabel ID="lblAlarmType" runat="server" Text="Tipo de Alarma:" meta:resourcekey="lblAlarmTypeResource1">
                </dxe:ASPxLabel>
            </td>
            <td>
                <dxe:ASPxComboBox ID="cmbAlarmType" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                    CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                      Value='<%# Eval("AlarmType") %>' meta:resourcekey="cmbAlarmTypeResource1"
                    TextField="Description" ValueField="Code" ValueType="System.Int32">
                    <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                    </LoadingPanelImage>
                    <ClientSideEvents Init="function(s,e){ if(s.GetSelectedItem().value == 4){ SetFlatExtraPremiumRoundPanelVisible(); } else if(s.GetSelectedItem().value == 5){ SetExclusionRoundPanelVisible(); } else { SetRoundBoxesVisible(false, false); }  }"
                        ValueChanged="function(s,e){ if(s.GetSelectedItem().value == 4){ SetFlatExtraPremiumRoundPanelVisible(); } else if(s.GetSelectedItem().value == 5){ SetExclusionRoundPanelVisible(); } else { SetRoundBoxesVisible(false, false); }  }" />
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
            <dxp:PanelContent runat="server" Width="100%" meta:resourcekey="PanelContentResource1">
                <div style="padding: 4px 4px 4px 4px; text-align: center;">
                    <table width="600px">
                        <tr>
                            <td width="100px">
                            </td>
                            <td width="200px">
                            </td>
                            <td align="center" colspan="3" width="20%">
                                <dxe:ASPxLabel ID="lblExtraPremiumDuration" runat="server" Text="Duración Extra Prima"
                                    meta:resourcekey="lblExtraPremiumDurationResource1">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td width="100px" align="left">
                                <dxe:ASPxLabel ID="lblFlatExtraPremium" runat="server" Text="Flat Extra Prima" meta:resourcekey="lblFlatExtraPremiumResource1">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" width="200px">
                                <dxe:ASPxLabel ID="lblExclusionPeriodType" runat="server" Text="Período" meta:resourcekey="lblExclusionPeriodTypeResource1">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" width="9%">
                                <dxe:ASPxLabel ID="lblExtraPremiumDurationYears" runat="server" Text="Años" meta:resourcekey="lblExtraPremiumDurationYearsResource1">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" width="9%">
                                <dxe:ASPxLabel ID="lblExtraPremiumDurationMonths" runat="server" Text="Meses" meta:resourcekey="lblExtraPremiumDurationMonthsResource1">
                                </dxe:ASPxLabel>
                            </td>
                            <td align="left" width="9%">
                                <dxe:ASPxLabel ID="lblExtraPremiumDurationDays" runat="server" Text="Días" meta:resourcekey="lblExtraPremiumDurationDaysResource1">
                                </dxe:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td width="100px">
                                <dxe:ASPxTextBox ID="txtExtraFlatPremium" runat="server" Width="100px" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                    CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                    Text='<%# Eval("FlatExtraPremium") %>' HorizontalAlign="Right" Enabled="False"
                                    meta:resourcekey="txtExtraFlatPremiumResource1">
                                    <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999999999999999g&gt;.&lt;00..99&gt;" />
                                </dxe:ASPxTextBox>
                            </td>
                            <td width="200px">
                                <dxe:ASPxComboBox ID="cmbFlatPeriodType" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                    CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                    Value='<%# Eval("ExclusionPeriodType") %>'  Width="100%"
                                    meta:resourcekey="cmbFlatPeriodTypeResource1" TextField="Description" ValueField="Code" ValueType="System.Int32">
                                    <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                                    </LoadingPanelImage>
                                </dxe:ASPxComboBox>
                            </td>
                            <td width="9%">
                                <dxe:ASPxTextBox ID="txtDurationOfExtraPremiumYears" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                    CssPostfix="SoftOrange" HorizontalAlign="Right" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                    Text='<%# Eval("DurationOfFlatExtraPremiumDays") %>' Width="50px" meta:resourcekey="txtDurationOfExtraPremiumYearsResource1">
                                </dxe:ASPxTextBox>
                            </td>
                            <td width="9%">
                                <dxe:ASPxTextBox ID="txtDurationOfExtraPremiumMonths" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                    CssPostfix="SoftOrange" HorizontalAlign="Right" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                    Text='<%# Eval("DurationOfFlatExtraPremiumMonths") %>' Width="50px" meta:resourcekey="txtDurationOfExtraPremiumMonthsResource1">
                                </dxe:ASPxTextBox>
                            </td>
                            <td width="9%">
                                <dxe:ASPxTextBox ID="txtDurationOfExtraPremiumDays" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                    CssPostfix="SoftOrange" HorizontalAlign="Right" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                    Text='<%# Eval("DurationOfFlatExtraPremiumYears") %>' Width="50px" meta:resourcekey="txtDurationOfExtraPremiumDaysResource1">
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
            <dxp:PanelContent ID="PanelContent1" runat="server" meta:resourcekey="PanelContent1Resource1">
                <div style="padding: 4px 4px 4px 4px; text-align: center; width: 100%">
                    <table>
                        <tr>
                            <td align="center" width="20%">
                            </td>
                            <td align="center" width="20%">
                            </td>
                            <td width="9%">
                            </td>
                            <td width="9%">
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
                            <td width="9%">
                                <dxe:ASPxComboBox ID="cmbExclusionType" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                    CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                    Value='<%# Eval("ExclusionType") %>' meta:resourcekey="cmbExclusionTypeResource1"
                                    TextField="Description" ValueField="Code" ValueType="System.Int32">
                                    <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                                    </LoadingPanelImage>
                                </dxe:ASPxComboBox>
                            </td>
                            <td width="9%">
                                <dxe:ASPxComboBox ID="cmbExclusionPeriodType" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                    CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                    Value='<%# Eval("ExclusionPeriodType") %>' meta:resourcekey="cmbExclusionPeriodTypeResource1"
                                    TextField="Description" ValueField="Code" ValueType="System.Int32">
                                    <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                                    </LoadingPanelImage>
                                </dxe:ASPxComboBox>
                            </td>
                            <td>
                                <dxe:ASPxComboBox ID="cmbCover" runat="server" ValueType="System.Int32" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                    CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                    Value='<%# Eval("Coverage") %>' meta:resourcekey="cmbCoverResource1">
                                    <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                                    </LoadingPanelImage>
                                </dxe:ASPxComboBox>
                            </td>
                            <td>
                                <dxe:ASPxComboBox ID="cmbIllness" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                    CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"  meta:resourcekey="cmbIllnessResource1" TextField="Description" ValueField="Code" ValueType="System.Int32">
                                    <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                                    </LoadingPanelImage>
                                </dxe:ASPxComboBox>
                            </td>
                            <td width="9%">
                                <dxe:ASPxTextBox ID="txtWaitingPeriodYears" runat="server" Width="50px" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                    CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                    Text='<%# Eval("WaitingPeriodYears") %>' HorizontalAlign="Right" meta:resourcekey="txtWaitingPeriodYearsResource1">
                                </dxe:ASPxTextBox>
                            </td>
                            <td width="9%">
                                <dxe:ASPxTextBox ID="txtWaitingPeriodMonths" runat="server" Width="50px" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                    CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                    Text='<%# Eval("WaitingPeriodMonths") %>' HorizontalAlign="Right" meta:resourcekey="txtWaitingPeriodMonthsResource1">
                                </dxe:ASPxTextBox>
                            </td>
                            <td width="9%">
                                <dxe:ASPxTextBox ID="txtWaitingPeriodDays" runat="server" Width="50px" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                                    CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                                    Text='<%# Eval("WaitingPeriodDays") %>' HorizontalAlign="Right" meta:resourcekey="txtWaitingPeriodDaysResource1">
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
                      Value='<%# Eval("Status") %>' Enabled="False" meta:resourcekey="cmbStatusResource1"
                    TextField="Description" ValueField="Code" ValueType="System.Int32">
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
        </tr>
    </table>
</div>
<dxpc:ASPxPopupControl ID="ppExplainRule" runat="server" ClientInstanceName="ppExplainRule"
    CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css" CssPostfix="SoftOrange"
    EnableHotTrack="False" HeaderText="Explicación de la regla" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" AllowDragging="True"
    meta:resourcekey="ppExplainRuleResource1">
    <ContentCollection>
        <dxpc:PopupControlContentControl runat="server" meta:resourcekey="PopupControlContentControlResource1">
            <dxe:ASPxMemo ID="mmExplainRule" runat="server" Height="110px" Width="500px" Text='<%# Eval("Explanation") %>'
                Enabled="false" meta:resourcekey="mmExplainRuleResource1">
            </dxe:ASPxMemo>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
    <HeaderStyle>
        <Paddings PaddingRight="6px" />
    </HeaderStyle>
</dxpc:ASPxPopupControl>
