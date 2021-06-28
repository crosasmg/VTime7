<%@ Control Language="VB" AutoEventWireup="true" CodeFile="PhysicalAddressControl.ascx.vb"
    Inherits="Controls_PhysicalAddressControl" %>
<script src='<%= ResolveClientUrl("~/Scripts/PhysicalAddressControl.js") %>' type="text/javascript"></script>
<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6v5-2uaq_wusHDktM9ILcqIrlPtnZgEk&sensor=false"></script>
<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&libraries=places"></script>
<link rel="Stylesheet" media="screen" type="text/css" href="<%=ResolveUrl("PhysicalAddressControl.css") %>" />
<style type="text/css">
    .style1
    {
        width: 81px;
    }
</style>
<dxcp:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" ClientSideEvents-BeginCallback="function(s, e) {
                                                                                                    lp.Show();
                                                                                                     }"
    ClientSideEvents-EndCallback="function(s, e) {
                                                                                                       lp.Hide();
                                                                                                     }">
    <ClientSideEvents BeginCallback="function(s, e) {
                                                                                                    lp.Show();
                                                                                                     }"
        EndCallback="function(s, e) {
                                                                                                       lp.Hide();
                                                                                                     }">
    </ClientSideEvents>
    <PanelCollection>
        <dxp:PanelContent ID="PanelContent3" runat="server">
            <dxlp:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp" Modal="True" />
            <asp:HiddenField ID="hfCountryCodeDefault" ClientIDMode="Static" Value="0" runat="server" />
            <asp:HiddenField ID="hfReload" ClientIDMode="Static" Value="0" runat="server" />
            <table>
                <tbody>
                    <tr>
                        <td valign="top" class="rowA">
                            <table style="width: 250px">
                                <tbody>
                                    <tr>
                                        <td class="cellA2">
                                            <dxe:ASPxLabel ID="lblTypePhyTicalAddress" Width="130px" runat="server" Text="Tipo de dirección física"  meta:resourcekey="lblTypePhyTicalAddressResource">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td class="cellA2">
                                            <dxe:ASPxComboBox ID="ddlTypePhyTicalAddress" ClientEnabled="True" EnableClientSideAPI="true"
                                                runat="server" ClientInstanceName="ddlTypePhyTicalAddress" ValueType='System.String'
                                                TextField='Description' ValueField='Code' IncrementalFilteringMode="StartsWith">
                                                <ClientSideEvents Validation="ValidationTypePhyTicalAddress" />
                                                <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                    Display="Dynamic">
                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="cellA2">
                                            <dxe:ASPxLabel ID="lblTypeRoute" Width="125px" runat="server" Text="Tipo de ruta" meta:resourcekey="lblTypeRouteResource">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td class="cellA2">
                                            <dxe:ASPxComboBox ID="ddlTypeRoute" ClientEnabled="True" EnableClientSideAPI="true"
                                                ClientInstanceName="ddlTypeRoute" runat="server" ValueType='System.String' TextField='Description'
                                                ValueField='Code' IncrementalFilteringMode="StartsWith">
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { ddlTypeRoute_SelectedIndex(s,e); }"
                                                    Validation="ValidationTypeRoute" />
                                                <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                    Display="Dynamic">
                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="cellA">
                                            <dxp:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
                                                <PanelCollection>
                                                    <dxp:PanelContent ID="PanelContent1" runat="server" Height="100%" SupportsDisabledAttribute="True">
                                                        <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="CallbackPanelTypeRoutePart"
                                                            RenderMode="Table" Width="100%" ID="CallbackPanelTypeRoutePart">
                                                            <ClientSideEvents EndCallback="function(s, e) { lp.Hide(); }" BeginCallback="function(s, e) { lp.Show(); }" />
                                                            <PanelCollection>
                                                                <dxp:PanelContent ID="PanelTypeRoutePart" Width="100%" runat="server" SupportsDisabledAttribute="True">
                                                                    <table style="width: 300px">
                                                                        <tbody>
                                                                            <tr id="tdPartRoute1" visible="false" runat="server">
                                                                                <%-- Part Route 1 --%>
                                                                                <td style="width: 130px; padding-bottom: 4px; padding-left: 4px;">
                                                                                    <dxe:ASPxComboBox ID="cbxPartRoute1" ClientEnabled="true" Visible="false" EnableClientSideAPI="true"
                                                                                        ClientInstanceName="cbxPartRoute1" runat="server" ValueType='System.String' TextField='Description'
                                                                                        ValueField='Code' Width="130px" IncrementalFilteringMode="StartsWith">
                                                                                        <ClientSideEvents Validation="ValidationcbxPartRoute" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                                <td style="width: 100%; padding-bottom: 4px; padding-left: 4px">
                                                                                    <dxe:ASPxTextBox Width="170px" ClientEnabled="true" ClientIDMode="Static" Visible="false"
                                                                                        ID="txtPartRoute1" ClientInstanceName="txtPartRoute1" runat="server">
                                                                                        <ClientSideEvents Validation="ValidationtxtPartRoute" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tdPartRoute2" visible="false" runat="server">
                                                                                <%-- Part Route 2 --%>
                                                                                <td style="width: 130px; padding-bottom: 4px; padding-left: 4px;">
                                                                                    <dxe:ASPxComboBox ID="cbxPartRoute2" ClientEnabled="true" ClientIDMode="Static" Visible="false"
                                                                                        EnableClientSideAPI="true" ClientInstanceName="cbxPartRoute2" runat="server"
                                                                                        ValueType='System.String' TextField='Description' ValueField='Code' Width="130px"
                                                                                        IncrementalFilteringMode="StartsWith">
                                                                                        <ClientSideEvents Validation="ValidationcbxPartRoute" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                                <td style="width: 100%; padding-bottom: 4px; padding-left: 4px">
                                                                                    <dxe:ASPxTextBox Width="170px" ID="txtPartRoute2" ClientEnabled="true" ClientIDMode="Static"
                                                                                        Visible="false" ClientInstanceName="txtPartRoute2" runat="server">
                                                                                        <ClientSideEvents Validation="ValidationtxtPartRoute" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tdPartRoute3" visible="false" runat="server">
                                                                                <%-- Part Route 3 --%>
                                                                                <td style="width: 130px; padding-bottom: 4px; padding-left: 4px;">
                                                                                    <dxe:ASPxComboBox ID="cbxPartRoute3" ClientEnabled="true" ClientIDMode="Static" Visible="false"
                                                                                        EnableClientSideAPI="true" ClientInstanceName="cbxPartRoute3" runat="server"
                                                                                        ValueType='System.String' TextField='Description' ValueField='Code' Width="130px"
                                                                                        IncrementalFilteringMode="StartsWith">
                                                                                        <ClientSideEvents Validation="ValidationcbxPartRoute" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                                <td style="width: 100%; padding-bottom: 4px; padding-left: 4px">
                                                                                    <dxe:ASPxTextBox Width="170px" ID="txtPartRoute3" ClientEnabled="true" ClientIDMode="Static"
                                                                                        Visible="false" ClientInstanceName="txtPartRoute3" runat="server">
                                                                                        <ClientSideEvents Validation="ValidationtxtPartRoute" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tdPartRoute4" visible="false" runat="server">
                                                                                <%-- Part Route 4 --%>
                                                                                <td style="width: 130px; padding-bottom: 4px; padding-left: 4px;">
                                                                                    <dxe:ASPxComboBox ID="cbxPartRoute4" ClientEnabled="true" ClientIDMode="Static" Visible="false"
                                                                                        EnableClientSideAPI="true" ClientInstanceName="cbxPartRoute4" runat="server"
                                                                                        ValueType='System.String' TextField='Description' ValueField='Code' Width="130px"
                                                                                        IncrementalFilteringMode="StartsWith">
                                                                                        <ClientSideEvents Validation="ValidationcbxPartRoute" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                                <td style="width: 100%; padding-bottom: 4px; padding-left: 4px">
                                                                                    <dxe:ASPxTextBox Width="170px" ID="txtPartRoute4" ClientEnabled="true" ClientIDMode="Static"
                                                                                        Visible="false" ClientInstanceName="txtPartRoute4" runat="server">
                                                                                        <ClientSideEvents Validation="ValidationtxtPartRoute" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tdPartRoute5" visible="false" runat="server">
                                                                                <%-- Part Route 5 --%>
                                                                                <td style="width: 130px; padding-bottom: 4px; padding-left: 4px;">
                                                                                    <dxe:ASPxComboBox ID="cbxPartRoute5" ClientEnabled="true" ClientIDMode="Static" Visible="false"
                                                                                        EnableClientSideAPI="true" ClientInstanceName="cbxPartRoute5" runat="server"
                                                                                        ValueType='System.String' TextField='Description' ValueField='Code' Width="130px"
                                                                                        IncrementalFilteringMode="StartsWith">
                                                                                        <ClientSideEvents Validation="ValidationcbxPartRoute" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                                <td style="width: 100%; padding-bottom: 4px; padding-left: 4px">
                                                                                    <dxe:ASPxTextBox Width="170px" ID="txtPartRoute5" ClientEnabled="true" ClientIDMode="Static"
                                                                                        Visible="false" ClientInstanceName="txtPartRoute5" runat="server">
                                                                                        <ClientSideEvents Validation="ValidationtxtPartRoute" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tdPartRoute6" visible="false" runat="server">
                                                                                <%-- Part Route 6 --%>
                                                                                <td style="width: 130px; padding-bottom: 4px; padding-left: 4px;">
                                                                                    <dxe:ASPxComboBox ID="cbxPartRoute6" ClientEnabled="true" ClientIDMode="Static" Visible="false"
                                                                                        EnableClientSideAPI="true" ClientInstanceName="cbxPartRoute6" runat="server"
                                                                                        ValueType='System.String' TextField='Description' ValueField='Code' Width="130px"
                                                                                        IncrementalFilteringMode="StartsWith">
                                                                                        <ClientSideEvents Validation="ValidationcbxPartRoute" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                                <td style="width: 100%; padding-bottom: 4px; padding-left: 4px">
                                                                                    <dxe:ASPxTextBox Width="170px" ID="txtPartRoute6" ClientEnabled="true" ClientIDMode="Static"
                                                                                        Visible="false" ClientInstanceName="txtPartRoute6" runat="server">
                                                                                        <ClientSideEvents Validation="ValidationtxtPartRoute" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tdPartRoute7" visible="false" runat="server">
                                                                                <%-- Part Route 7 --%>
                                                                                <td style="width: 130px; padding-bottom: 4px; padding-left: 4px;">
                                                                                    <dxe:ASPxComboBox ID="cbxPartRoute7" Visible="false" ClientEnabled="true" ClientIDMode="Static"
                                                                                        EnableClientSideAPI="true" ClientInstanceName="cbxPartRoute7" runat="server"
                                                                                        ValueType='System.String' TextField='Description' ValueField='Code' Width="130px"
                                                                                        IncrementalFilteringMode="StartsWith">
                                                                                        <ClientSideEvents Validation="ValidationcbxPartRoute" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                                <td style="width: 100%; padding-bottom: 4px; padding-left: 4px">
                                                                                    <dxe:ASPxTextBox Width="170px" ID="txtPartRoute7" ClientEnabled="true" ClientIDMode="Static"
                                                                                        Visible="false" ClientInstanceName="txtPartRoute7" runat="server">
                                                                                        <ClientSideEvents Validation="ValidationtxtPartRoute" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tdPartRoute8" visible="false" runat="server">
                                                                                <%-- Part Route 8 --%>
                                                                                <td style="width: 130px; padding-bottom: 4px; padding-left: 4px;">
                                                                                    <dxe:ASPxComboBox ID="cbxPartRoute8" Visible="false" ClientEnabled="true" ClientIDMode="Static"
                                                                                        EnableClientSideAPI="true" ClientInstanceName="cbxPartRoute8" runat="server"
                                                                                        ValueType='System.String' TextField='Description' ValueField='Code' Width="130px"
                                                                                        IncrementalFilteringMode="StartsWith">
                                                                                        <ClientSideEvents Validation="ValidationcbxPartRoute" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                                <td style="width: 100%; padding-bottom: 4px; padding-left: 4px">
                                                                                    <dxe:ASPxTextBox Width="170px" ID="txtPartRoute8" ClientEnabled="true" Visible="false"
                                                                                        ClientInstanceName="txtPartRoute8" runat="server">
                                                                                        <ClientSideEvents Validation="ValidationtxtPartRoute" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxcp:ASPxCallbackPanel>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dxp:ASPxPanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <dxe:ASPxCheckBox ID="chbResidentialAddress" ClientInstanceName="chbResidentialAddress"
                                                Text="Residencial" runat="server"  meta:resourcekey="chbResidentialAddressResource">
                                            </dxe:ASPxCheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="cellA">
                                            <dxe:ASPxCheckBox ID="chbCorrespondenceAddress" ClientInstanceName="chbCorrespondenceAddress"
                                                Text="Dirección de corespondencia" runat="server"  meta:resourcekey="chbCorrespondenceAddressResource">
                                            </dxe:ASPxCheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="cellA">
                                            <dxe:ASPxCheckBox ID="chbCollectionAddress" ClientInstanceName="chbCollectionAddress"
                                                Text="Dirección de cobro" runat="server"  meta:resourcekey="chbCollectionAddressResource">
                                            </dxe:ASPxCheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="cellA">
                                            <dxe:ASPxCheckBox ID="chbSendProblemAddress" ClientInstanceName="chbSendProblemAddress"
                                                Text="Dirección con problemas de envío" runat="server"  meta:resourcekey="chbSendProblemAddressResource">
                                            </dxe:ASPxCheckBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <td valign="top" class="rowA">
                            <table style="width: 250px">
                                <tbody>
                                    <tr>
                                        <td style="width: 130px">
                                            <dxe:ASPxLabel Width="130px" ID="lblCountry" ClientInstanceName="lblCountry" runat="server"
                                                Text="País"  meta:resourcekey="lblCountryResource">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td class="cellA">
                                            <dxe:ASPxComboBox ID="ddlCountry" EnableClientSideAPI="true" ClientInstanceName="ddlCountry"
                                                runat="server" ValueType='System.String' IncrementalFilteringMode="StartsWith"
                                                TextField='Description' ValueField='Code'>
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                                                                                     ddlCountry_SelectedIndexChanged(s,e);
                                                }" />
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <dxp:ASPxPanel ID="ASPxPanel2" ClientInstanceName="ASPxPanel2" runat="server" Width="100%">
                                                <PanelCollection>
                                                    <dxp:PanelContent ID="panelZoneDinamic2" runat="server" Height="100%" SupportsDisabledAttribute="True">
                                                        <dxcp:ASPxCallbackPanel runat="server" ClientInstanceName="CallbackPanel" RenderMode="Table"
                                                            Height="100%" Width="100%" ID="ASPxCallbackPanel2">
                                                            <ClientSideEvents EndCallback="function(s, e) { lp.Hide(); }" BeginCallback="function(s, e) { lp.Show(); }" />
                                                            <PanelCollection>
                                                                <dxp:PanelContent ID="panelCountry" runat="server" SupportsDisabledAttribute="True">
                                                                    <table style="width: 250px">
                                                                        <tbody>
                                                                            <tr id="tdGeographicZone1" visible="false" runat="server">
                                                                                <%-- Part GeographicZone 1 --%>
                                                                                <td style="padding-bottom: 4px;">
                                                                                    <dxe:ASPxLabel Width="130px" Visible="false" ID="lblGeographicZone1" runat="server">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-bottom: 4px;">
                                                                                    <dxe:ASPxComboBox Visible="false" ID="cbxGeographicZone1" EnableClientSideAPI="true"
                                                                                        ClientInstanceName="cbxGeographicZone1" runat="server" ValueType='System.String'
                                                                                        TextField='Description' ValueField='Code' IncrementalFilteringMode="StartsWith">
                                                                                        <ClientSideEvents Validation="ValidationcbxGeographicZone" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tdGeographicZone2" visible="false" runat="server">
                                                                                <%-- Part GeographicZone 2 --%>
                                                                                <td style="padding-bottom: 4px;">
                                                                                    <dxe:ASPxLabel Width="130px" Visible="false" ID="lblGeographicZone2" runat="server">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-bottom: 4px;">
                                                                                    <dxe:ASPxComboBox Visible="false" ID="cbxGeographicZone2" EnableClientSideAPI="true"
                                                                                        ClientInstanceName="cbxGeographicZone2" runat="server" ValueType='System.String'
                                                                                        TextField='Description' ValueField='Code' IncrementalFilteringMode="StartsWith">
                                                                                        <ClientSideEvents Validation="ValidationcbxGeographicZone" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tdGeographicZone3" visible="false" runat="server">
                                                                                <%-- Part GeographicZone 3 --%>
                                                                                <td style="padding-bottom: 4px;">
                                                                                    <dxe:ASPxLabel Width="130px" Visible="false" ID="lblGeographicZone3" runat="server">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-bottom: 4px;">
                                                                                    <dxe:ASPxComboBox Visible="false" ID="cbxGeographicZone3" EnableClientSideAPI="true"
                                                                                        ClientInstanceName="cbxGeographicZone3" runat="server" ValueType='System.String'
                                                                                        TextField='Description' ValueField='Code' IncrementalFilteringMode="StartsWith">
                                                                                        <ClientSideEvents Validation="ValidationcbxGeographicZone" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tdGeographicZone4" visible="false" runat="server">
                                                                                <%-- Part GeographicZone 4 --%>
                                                                                <td style="padding-bottom: 4px;">
                                                                                    <dxe:ASPxLabel Width="130px" Visible="false" ID="lblGeographicZone4" runat="server">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-bottom: 4px;">
                                                                                    <dxe:ASPxComboBox Visible="false" ID="cbxGeographicZone4" EnableClientSideAPI="true"
                                                                                        ClientInstanceName="cbxGeographicZone4" runat="server" ValueType='System.String'
                                                                                        TextField='Description' ValueField='Code' IncrementalFilteringMode="StartsWith">
                                                                                        <ClientSideEvents Validation="ValidationcbxGeographicZone" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tdGeographicZone5" visible="false" runat="server">
                                                                                <%-- Part GeographicZone 5 --%>
                                                                                <td style="padding-bottom: 4px;">
                                                                                    <dxe:ASPxLabel Width="130px" Visible="false" ID="lblGeographicZone5" runat="server">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-bottom: 4px;">
                                                                                    <dxe:ASPxComboBox Visible="false" ID="cbxGeographicZone5" EnableClientSideAPI="true"
                                                                                        ClientInstanceName="cbxGeographicZone5" runat="server" ValueType='System.String'
                                                                                        TextField='Description' ValueField='Code' IncrementalFilteringMode="StartsWith">
                                                                                        <ClientSideEvents Validation="ValidationcbxGeographicZone" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tdGeographicZone6" visible="false" runat="server">
                                                                                <%-- Part GeographicZone 6 --%>
                                                                                <td style="padding-bottom: 4px;">
                                                                                    <dxe:ASPxLabel Width="130px" Visible="false" ID="lblGeographicZone6" runat="server">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-bottom: 4px;">
                                                                                    <dxe:ASPxComboBox Visible="false" ID="cbxGeographicZone6" EnableClientSideAPI="true"
                                                                                        ClientInstanceName="cbxGeographicZone6" runat="server" ValueType='System.String'
                                                                                        TextField='Description' ValueField='Code' IncrementalFilteringMode="StartsWith">
                                                                                        <ClientSideEvents Validation="ValidationcbxGeographicZone" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tdGeographicZone7" visible="false" runat="server">
                                                                                <%-- Part GeographicZone 7 --%>
                                                                                <td style="padding-bottom: 4px;">
                                                                                    <dxe:ASPxLabel Width="130px" Visible="false" ID="lblGeographicZone7" runat="server">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-bottom: 4px;">
                                                                                    <dxe:ASPxComboBox Visible="false" ID="cbxGeographicZone7" EnableClientSideAPI="true"
                                                                                        ClientInstanceName="cbxGeographicZone7" runat="server" ValueType='System.String'
                                                                                        TextField='Description' ValueField='Code' IncrementalFilteringMode="StartsWith">
                                                                                        <ClientSideEvents Validation="ValidationcbxGeographicZone" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="tdGeographicZone8" visible="false" runat="server">
                                                                                <%-- Part GeographicZone 8 --%>
                                                                                <td style="padding-bottom: 4px;">
                                                                                    <dxe:ASPxLabel Width="130px" Visible="false" ID="lblGeographicZone8" runat="server">
                                                                                    </dxe:ASPxLabel>
                                                                                </td>
                                                                                <td style="padding-bottom: 4px;">
                                                                                    <dxe:ASPxComboBox Visible="false" ID="cbxGeographicZone8" EnableClientSideAPI="true"
                                                                                        ClientInstanceName="cbxGeographicZone8" runat="server" ValueType='System.String'
                                                                                        TextField='Description' ValueField='Code' IncrementalFilteringMode="StartsWith">
                                                                                        <ClientSideEvents Validation="ValidationcbxGeographicZone" />
                                                                                        <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                                                            Display="Dynamic">
                                                                                            <RequiredField IsRequired="true" />
                                                                                        </ValidationSettings>
                                                                                    </dxe:ASPxComboBox>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </dxp:PanelContent>
                                                            </PanelCollection>
                                                        </dxcp:ASPxCallbackPanel>
                                                    </dxp:PanelContent>
                                                </PanelCollection>
                                            </dxp:ASPxPanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="cellA">
                                            <dxe:ASPxLabel ID="lblPostalCode" ClientInstanceName="lblPostalCode" runat="server"
                                                Text="Código postal" meta:resourcekey="lblPostalCodeResource">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td class="cellA">
                                            <dxe:ASPxComboBox ID='ddlPostalCode' runat='server' ClientInstanceName='ddlPostalCode'
                                                ClientIDMode='Static' EnableIncrementalFiltering='True' ClientVisible='true'
                                                ClientEnabled='True' ValueType='System.String' TextFormatString="{0}" DropDownStyle="DropDown"
                                                EnableCallbackMode="true" IncrementalFilteringMode="Contains" CallbackPageSize="10"
                                                DropDownRows="10" AutoResizeWithContainer="false" IncrementalFilteringDelay="500"
                                                FilterMinLength="0" OnItemsRequestedByFilterCondition="ddlPostalCode_OnItemsRequestedByFilterCondition"
                                                OnItemRequestedByValue="ddlPostalCode_OnItemRequestedByValue" TextField='Code'
                                                ValueField='Code'>
                                                <Columns>
                                                    <dxe:ListBoxColumn FieldName="Code" Visible="True" Caption="Zipcode" meta:resourcekey="ddlPostalCodeResource"/>
                                                </Columns>
                                                <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                    Display="Dynamic">
                                                </ValidationSettings>
                                                <ClientSideEvents Validation="ValidationZipCode" />
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="cellA">
                                            <dxe:ASPxLabel ID="lblTimeZone" ClientInstanceName="lblTimeZone" runat="server" 
                                                 Text="Zona horaria" meta:resourcekey="lblTimeZoneResource">
                                            </dxe:ASPxLabel>
                                        </td>
                                        <td class="cellA">
                                            <dxe:ASPxComboBox ID="ddlTimeZone" ClientInstanceName="ddlTimeZone" TextField='Description'
                                                ValueField='Code' runat="server" IncrementalFilteringMode="StartsWith">
                                                <ClientSideEvents Validation="ValidationTimeZone" />
                                                <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                    Display="Dynamic">
                                                </ValidationSettings>
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="cellA">
                                            <dxe:ASPxLabel ID="lblInitialYear" ClientInstanceName="lblInitialYear" runat="server"
                                                Text="Año inicial" meta:resourcekey="lblInitialYearResource"/>
                                        </td>
                                        <td class="cellA">
                                            <dxe:ASPxComboBox ID="ddlInitialYear" ClientInstanceName="ddlInitialYear" ValueType='System.String'
                                                TextField='Description' ValueField='Code' runat="server" IncrementalFilteringMode="StartsWith">
                                                <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                    Display="Dynamic">
                                                </ValidationSettings>
                                                <ClientSideEvents Validation="ValidationInitialYear" />
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="cellA">
                                            <dxe:ASPxLabel ID="lblLastContact" ClientInstanceName="lblLastContact" runat="server" 
                                                 Text="Último contacto" meta:resourcekey="lblLastContactResource"/>
                                        </td>
                                        <td class="cellA">
                                            <dxe:ASPxDateEdit ID="ddlLastContact" ClientInstanceName="ddlLastContact" runat="server">
                                                <ValidationSettings EnableCustomValidation="True" ErrorTextPosition="Bottom" ErrorDisplayMode="Text"
                                                    Display="Dynamic">
                                                </ValidationSettings>
                                                <ClientSideEvents Validation="ValidationLastContact" />
                                            </dxe:ASPxDateEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="cellA">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxLabel ID="lblRiskZone" ClientInstanceName="lblRiskZone" runat="server" 
                                                                Text="Zona(s) de riesgo"  meta:resourcekey="lblRiskZoneResource">
                                                            </dxe:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td>
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="btnRiskZoneAdd" ClientInstanceName="btnRiskZoneAdd" Text="+"
                                                                runat="server">
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="btnRiskZoneRemove" ClientInstanceName="btnRiskZoneRemove" Text="-"
                                                                runat="server">
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <td valign="Top" class="rowA">
                            <table style="width: 250px">
                                <tbody>
                                    <tr>
                                        <td>
                                            <div id="mapa" runat="server" style="width: 300px; height: 300px;">
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </dxp:PanelContent>
    </PanelCollection>
</dxcp:ASPxCallbackPanel>