<%@ Control Language="VB" AutoEventWireup="false" CodeFile="RequirementsGeneralInformation.ascx.vb"
    Inherits="Underwriting_Controls_RequirementsGeneralInformation" %>
<table width="100%">
    <tr>
        <td width="10%">
            <dxe:ASPxLabel ID="lblRequirementType" runat="server" meta:resourcekey="RequirementTypeLabelResource1"
                Text="Tipo de Requerimiento:">
            </dxe:ASPxLabel>
        </td>
        <td width="40%">
            <table width="100%">
                <tr>
                    <td width="10%">
                        <dxe:ASPxLabel ID="lblReqID" runat="server" Text='<%# Eval("RequirementType") %>'
                            meta:resourcekey="lblReqIDResource1">
                        </dxe:ASPxLabel>
                    </td>
                    <td width="90%">
                        <dxe:ASPxComboBox ID="cmbRequirementType" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                            CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                             Width="100%" Value='<%# Eval("RequirementType") %>'
                            Enabled="False" meta:resourcekey="cmbRequirementTypeResource1" TextField="Description" ValueField="Code" ValueType="System.Int32">
                            <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                            </LoadingPanelImage>
                        </dxe:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </td>
        <td width="10%">
            <dxe:ASPxLabel ID="lblRequestedTo" runat="server" meta:resourcekey="RequestedToLabelResource1"
                Text="Solicitado a:">
            </dxe:ASPxLabel>
        </td>
        <td width="40%" colspan="3">
            <dxe:ASPxComboBox ID="cmbClientID" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                CssPostfix="SoftOrange" DataSourceID="Roles" meta:resourcekey="RequestedToResource1"
                SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" TextField="ClientName"
                ValueField="ClientID" ValueType="System.String" Width="100%" Value='<%# Eval("ClientID") %>'
                ClientInstanceName="requestedTo" EnableClientSideAPI="True" Enabled="False">
                <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                </LoadingPanelImage>
                <ValidationSettings>
                    <RequiredField IsRequired="true" ErrorText="El valor es obligatorio" />
                </ValidationSettings>
            </dxe:ASPxComboBox>
        </td>
    </tr>
    <tr>
        <td width="10%">
            <dxe:ASPxLabel ID="lblProcessType" runat="server" meta:resourcekey="ProcessTypeLabelResource1"
                Text="Tipo de Proceso:">
            </dxe:ASPxLabel>
        </td>
        <td width="40%">
            <dxe:ASPxComboBox ID="cmbProcessType" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                 Width="100%" Value='<%# Eval("ProcessType") %>' Enabled="False"
                meta:resourcekey="cmbProcessTypeResource1" TextField="Description" ValueField="Code" ValueType="System.Int32">
                <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                </LoadingPanelImage>
                <ValidationSettings>
                    <RequiredField IsRequired="true" ErrorText="El valor es obligatorio" />
                </ValidationSettings>
            </dxe:ASPxComboBox>
        </td>
        <td width="10%">
            <dxe:ASPxLabel ID="lblUnderwritingArea" runat="server" meta:resourcekey="UnderwritingAreaLabelResource1"
                Text="Área de Suscripción:" Width="100%">
            </dxe:ASPxLabel>
        </td>
        <td width="40%" colspan="3">
            <dxe:ASPxComboBox ID="cmbUnderwritingArea" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                CssPostfix="SoftOrange" meta:resourcekey="cmbUnderwritingAreaResource1" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                  Width="100%" Value='<%# Eval("UnderwritingArea") %>'
                Enabled="False" TextField="Description" ValueField="Code" ValueType="System.Int32">
                <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                </LoadingPanelImage>
                <ValidationSettings>
                    <RequiredField IsRequired="true" ErrorText="El valor es obligatorio" />
                </ValidationSettings>
            </dxe:ASPxComboBox>
        </td>
    </tr>
    <tr>
        <td width="10%">
            <dxe:ASPxLabel ID="lblDateRequested" runat="server" meta:resourcekey="DateRequestedLabelResource1"
                Text="Fecha de Solicitud:">
            </dxe:ASPxLabel>
        </td>
        <td width="40%">
            <dxe:ASPxDateEdit ID="dteRequirementDate" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                CssPostfix="SoftOrange" DateOnError="Today" meta:resourcekey="DateRequestedResource1"
                SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" Width="150px" Date='<%# Eval("RequirementDate") %>'
                Enabled="False">
            </dxe:ASPxDateEdit>
        </td>
        <td width="10%">
            <dxe:ASPxLabel ID="lblDateReceived" runat="server" meta:resourcekey="DateReceivedLabelResource1"
                Text="Fecha de Recepción:">
            </dxe:ASPxLabel>
        </td>
        <td width="40%" colspan="3">
            <dxe:ASPxDateEdit ID="dteReceptionDate" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                CssPostfix="SoftOrange" meta:resourcekey="ASPxDateEdit2Resource1" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                Width="150px" Date='<%# Eval("ReceptionDate") %>' Enabled="False">
                <ValidationSettings>
                    <RequiredField IsRequired="true" ErrorText="El valor es obligatorio" />
                </ValidationSettings>
            </dxe:ASPxDateEdit>
        </td>
    </tr>
    <tr>
        <td width="10%">
        </td>
        <td width="40%">
            <table>
                <tr>
                    <td>
                        <dxe:ASPxButton ID="btnCompleteInformation" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                            CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                            Text="Completar información manual" meta:resourcekey="btnCompleteInformationResource1">
                            <ClientSideEvents Click="function(s, e) {
    window.open(urlWebApplication+'/generated/'+UrlLink.GetValue() ,'mywindow','');
}" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnWatchDocument" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                            CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                            Text="Ver documento" meta:resourcekey="btnWatchDocumentResource1">
                            <ClientSideEvents Click="function(s, e) {
    window.open(urlWebApplication+'/generated/'+UrlLink.GetValue()+'&readonly=yes' ,'mywindow','');
}" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        </td>
        <td width="10%">
            <dxe:ASPxLabel ID="lblStatus" runat="server" meta:resourcekey="StatusLabelResource1"
                Text="Estado:">
            </dxe:ASPxLabel>
        </td>
        <td width="40%" colspan="3">
            <dxe:ASPxComboBox ID="cmbStatus" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                CssPostfix="SoftOrange" meta:resourcekey="cmbStatusResource1" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                 Width="250px" Value='<%# Eval("Status") %>' Enabled="False"
                 TextField="Description" ValueField="Code" ValueType="System.Int32">
                <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                </LoadingPanelImage>
                <ValidationSettings>
                    <RequiredField IsRequired="true" ErrorText="El valor es obligatorio" />
                </ValidationSettings>
            </dxe:ASPxComboBox>
        </td>
    </tr>
    <tr>
        <td width="10%">
            <dxe:ASPxLabel ID="lblAlarm" runat="server" meta:resourcekey="AlarmLabelResource1"
                Text="Alarma:">
            </dxe:ASPxLabel>
        </td>
        <td width="40%">
            <dxe:ASPxComboBox ID="cmbAlarmType" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                CssPostfix="SoftOrange" meta:resourcekey="cmbAlarmTypeResource1" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                 Width="300px" Value='<%# Eval("AlarmType") %>' Enabled="False"
                 TextField="Description" ValueField="Code" ValueType="System.Int32">
                <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                </LoadingPanelImage>
                <ValidationSettings>
                    <RequiredField IsRequired="true" ErrorText="El valor es obligatorio" />
                </ValidationSettings>
            </dxe:ASPxComboBox>
        </td>
        <td width="10%">
            <dxe:ASPxLabel ID="lblDebits" runat="server" Text="Débitos:" meta:resourcekey="lblDebitsResource1">
            </dxe:ASPxLabel>
        </td>
        <td width="40%" colspan="3">
            <dxe:ASPxTextBox ID="txtDebits" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                Width="50px" Value='<%# Eval("TotalDebits") %>' Enabled="False" meta:resourcekey="txtDebitsResource1">
            </dxe:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <td width="10%">
        </td>
        <td width="40%">
        </td>
        <td width="10%">
            <dxe:ASPxLabel ID="lblCredits" runat="server" meta:resourcekey="CreditsLabelResource1"
                Text="Créditos:">
            </dxe:ASPxLabel>
        </td>
        <td width="5%">
            <dxe:ASPxTextBox ID="txtCredits" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                Width="50px" Value='<%# Eval("TotalCredits") %>' Enabled="False" meta:resourcekey="txtCreditsResource1">
            </dxe:ASPxTextBox>
        </td>
        <td width="5%">
            <dxe:ASPxLabel ID="lblBlance" runat="server" Text="Balance:" meta:resourcekey="lblBlanceResource1">
            </dxe:ASPxLabel>
        </td>
        <td width="30%">
            <dxe:ASPxTextBox ID="txtBalance" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                Width="50px" Value='<%# Eval("Balance") %>' Enabled="False" meta:resourcekey="txtBalanceResource1">
            </dxe:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <td width="10%">
            <dxe:ASPxLabel ID="lblVendor" runat="server" meta:resourcekey="VendorLabelResource1"
                Text="Proveedor:">
            </dxe:ASPxLabel>
        </td>
        <td width="40%">
            <dxe:ASPxComboBox ID="cmbProviderID" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                CssPostfix="SoftOrange" DataSourceID="dsProviders" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                TextField="ClientName" ValueField="ClientID" ValueType="System.String" Width="100%"
                Value='<%# Eval("ProviderID") %>' Enabled="False" meta:resourcekey="cmbProviderIDResource1">
                <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                </LoadingPanelImage>
            </dxe:ASPxComboBox>
        </td>
        <td width="10%">
            <dxe:ASPxLabel ID="lblPayer" runat="server" Text="Pagador:" meta:resourcekey="lblPayerResource1">
            </dxe:ASPxLabel>
        </td>
        <td width="40%" colspan="3">
            <dxe:ASPxComboBox ID="cmbPayer" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                CssPostfix="SoftOrange" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                  Value='<%# Eval("Payer") %>' Width="350px" Enabled="False"
                meta:resourcekey="cmbPayerResource1"  TextField="Description" ValueField="Code" ValueType="System.Int32">
                <ButtonStyle Width="13px">
                </ButtonStyle>
                <LoadingPanelImage Url="~/App_Themes/SoftOrange/Web/Loading.gif">
                </LoadingPanelImage>
                <ValidationSettings>
                    <RequiredField IsRequired="true" ErrorText="El valor es obligatorio" />
                </ValidationSettings>
            </dxe:ASPxComboBox>
        </td>
    </tr>
    <tr>
        <td width="10%">
            <dxe:ASPxLabel ID="lblCost" runat="server" meta:resourcekey="CostLabelResource1"
                Text="Costo:">
            </dxe:ASPxLabel>
        </td>
        <td width="40%">
            <dxe:ASPxTextBox ID="txtCost" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                CssPostfix="SoftOrange" HorizontalAlign="Right" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                Width="150px" Value='<%# Eval("Cost") %>' Enabled="False" meta:resourcekey="txtCostResource1">
                <ValidationSettings>
                    <RequiredField IsRequired="true" ErrorText="El valor es obligatorio" />
                </ValidationSettings>
                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999999999999999999g&gt;.&lt;00..99&gt;" />
            </dxe:ASPxTextBox>
        </td>
        <td width="10%">
            <dxe:ASPxLabel ID="lblCostDueAmount" runat="server" Text="Falta por pagar:" meta:resourcekey="lblCostDueAmountResource1">
            </dxe:ASPxLabel>
        </td>
        <td width="40%" colspan="3">
            <dxe:ASPxTextBox ID="txtCostDueAmount" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                CssPostfix="SoftOrange" HorizontalAlign="Right" SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css"
                Width="150px" Value='<%# Eval("CostDueAmount") %>' Enabled="False" meta:resourcekey="txtCostDueAmountResource1">
                <ValidationSettings>
                    <RequiredField IsRequired="true" ErrorText="El valor es obligatorio" />
                </ValidationSettings>
                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999999999999999999g&gt;.&lt;00..99&gt;" />
            </dxe:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <td width="10%">
            <dxe:ASPxLabel ID="lblAccord" runat="server" Text="Código Acord:" meta:resourcekey="lblAccordResource1">
            </dxe:ASPxLabel>
        </td>
        <td width="40%">
            <dxe:ASPxTextBox ID="txtAccordCode" runat="server" CssFilePath="~/App_Themes/SoftOrange/{0}/styles.css"
                CssPostfix="SoftOrange" HorizontalAlign="Right" meta:resourcekey="CostResource1"
                SpriteCssFilePath="~/App_Themes/SoftOrange/{0}/sprite.css" Width="50px" Value='<%# Eval("AcordRequirementCode") %>'
                Enabled="False">
            </dxe:ASPxTextBox>
        </td>
        <td width="10%">
        </td>
        <td width="40%" colspan="3">
        </td>
    </tr>
</table>
<asp:ObjectDataSource 
    ID="Roles" 
    runat="server" 
    OldValuesParameterFormatString="original_{0}"
    SelectMethod="SelectAll" 
    DataObjectTypeName="InMotionGIT.Underwriting.Contracts.RoleInCase"
    TypeName="InMotionGIT.Underwriting.Proxy.Helpers.RoleInCase">
</asp:ObjectDataSource>
<asp:ObjectDataSource 
    ID="dsProviders" 
    runat="server" 
    OldValuesParameterFormatString="original_{0}"
    SelectMethod="SelectProviders" 
    DataObjectTypeName="InMotionGIT.Underwriting.Contracts.Requirement"
    TypeName="InMotionGIT.Underwriting.Proxy.Helpers.Requirement">
</asp:ObjectDataSource>
<dxe:ASPxTextBox ID="UrlLink" ClientInstanceName="UrlLink" runat="server" Value='<%# Eval("Link") %>'
    ClientVisible="False">
</dxe:ASPxTextBox>
