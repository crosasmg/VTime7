<%@ Control Language="VB" AutoEventWireup="false" CodeFile="RiskAddressControl.ascx.vb"
    Inherits="Controls_RiskAddressControl" %>
<script type="text/javascript">  
</script>
<table>
    <tr>
        <td>
            <div>
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButtonEdit ID="ButtonEditRiskAddress" runat="server" ClientInstanceName="ButtonEditRiskAddress"
                                            AutoPostBack="False" MaxLength="10" Width="130px">
                                            <Buttons>
                                                <dxe:EditButton>
                                                </dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) { popupRiskAddress.Show(); }" TextChanged="function(s, e) { GridViewRiskAddress.PerformCallback('CustomSearch'); }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td align="right">
                                        <dxe:ASPxLabel ID="LabelRiskAddress" runat="server" ClientInstanceName="LabelRiskAddress">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <dxpc:ASPxPopupControl ID="popupRiskAddress" runat="server" PopupHorizontalAlign="LeftSides"
                    PopupVerticalAlign="Below" ShowCloseButton="False" ShowHeader="False" Height="0px"
                    Width="0px" ClientInstanceName="popupRiskAddress" AllowResize="True" PopupAction="None"
                    PopupElementID="ButtonEditRiskAddress">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <dxwgv:ASPxGridView ID="GridViewRiskAddress" runat="server" AutoGenerateColumns="False"
                                OnHtmlRowCreated="GridViewRiskAddress_HtmlRowCreated" KeyFieldName="SKEY" Width="500px"
                                ClientInstanceName="GridViewRiskAddress" DataSourceID="XpoRiskAddressControl">
                                <ClientSideEvents RowClick="function(s, e) {var row = s.GetRow(e.visibleIndex);
	                                                                ButtonEditRiskAddress.SetValue(row.attributes[&quot;nPolicy&quot;].nodeValue);
	                                                                LabelRiskAddress.SetText(row.attributes[&quot;sDescAdd&quot;].nodeValue);}"
                                    RowDblClick="function(s, e) {	var row = s.GetRow(e.visibleIndex);
	                                                                    ButtonEditRiskAddress.SetValue(row.attributes[&quot;nPolicy&quot;].nodeValue);
	                                                                    LabelRiskAddress.SetText(row.attributes[&quot;sDescAdd&quot;].nodeValue);
	                                                                    popupRiskAddress.Hide();}" BeginCallback="function(s, e) { GridViewRiskAddress.cp_ExistsRiskAddress = true; }"
                                    EndCallback="function(s, e) {  if (GridViewRiskAddress.cp_ExistsRiskAddress) {
            if (typeof (GridViewRiskAddress.cp_RiskAddress) != 'undefined') {
                LabelRiskAddress.SetText(GridViewRiskAddress.cp_RiskAddress);
            }
        }
        else {
            LabelRiskAddress.SetText('');
            popupMessageRiskAddress.Show();
        } }"></ClientSideEvents>
                                <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFooter="True" ShowGroupButtons="True"
                                    ShowStatusBar="Hidden" ShowHorizontalScrollBar="true" />
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn FieldName="NBRANCH" VisibleIndex="0" Caption="Ramo"
                                        Width="100px" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SBRANCH" VisibleIndex="1" Caption="Ramo"
                                        Width="200px" meta:resourcekey="SBRANCHColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NPRODUCT" VisibleIndex="2" Caption="Producto"
                                        Width="100px" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SPRODUCT" VisibleIndex="3" Caption="Producto"
                                        Width="200px" meta:resourcekey="SPRODUCTColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NPOLICY" VisibleIndex="4" Caption="Póliza"
                                        Width="100px" meta:resourcekey="NPOLICYColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SSTREET" VisibleIndex="5" Caption="Calle/Av"
                                        Width="200px" meta:resourcekey="SSTREETColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NCERTIF" VisibleIndex="6" Caption="Certificado"
                                        Width="100px" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SBUILD" VisibleIndex="7" Caption="Edificio/ Estructura"
                                        Width="200px" meta:resourcekey="SBUILDColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SFLOOR" VisibleIndex="8" Caption="Piso"
                                        Width="50px" meta:resourcekey="SFLOORColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SDEPARTMENT" VisibleIndex="9" Caption="Apto/Casilla"
                                        Width="100px" meta:resourcekey="SDEPARTMENTColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SPOPULATION" VisibleIndex="10" Caption="Urbanización"
                                        Width="150px" meta:resourcekey="SPOPULATIONColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NCOUNTRY" VisibleIndex="11" Caption="" Visible="False"
                                        Width="100px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SCOUNTRY" VisibleIndex="12" Caption="País"
                                        Width="200px" meta:resourcekey="SCOUNTRYColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NGEOGRAPHICZONE1" VisibleIndex="13" Caption=""
                                        Visible="False" Width="100px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SGEOGRAPHICZONE1" VisibleIndex="14" Caption="Estado"
                                        Width="200px" meta:resourcekey="SGEOGRAPHICZONE1ColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NGEOGRAPHICZONE2" VisibleIndex="15" Caption=""
                                        Visible="False" Width="100px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SGEOGRAPHICZONE2" VisibleIndex="16" Caption="Municipio"
                                        Width="200px" meta:resourcekey="SGEOGRAPHICZONE2ColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NGEOGRAPHICZONE3" VisibleIndex="17" Caption=""
                                        Visible="False" Width="100px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SGEOGRAPHICZONE3" VisibleIndex="18" Caption="Ciudad"
                                        Width="200px" meta:resourcekey="SGEOGRAPHICZONE3ColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NZIP_CODE" VisibleIndex="19" Caption="Código Postal"
                                        Width="100px" meta:resourcekey="NZIP_CODEColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SDESCADD" VisibleIndex="20" Caption="Glosa de direcciones"
                                        Width="500px" meta:resourcekey="SDESCADDColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SKEY" VisibleIndex="20" Caption="Clave"
                                        Width="100px" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowFocusedRow="true" ColumnResizeMode="Control" />
                            </dxwgv:ASPxGridView>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
                <dxpc:ASPxPopupControl AllowDragging="True" ShowCloseButton="False" PopupHorizontalAlign="LeftSides"
                    PopupVerticalAlign="Below" ID="popupMessageRiskAddress" runat="server" HeaderText=""
                    ClientInstanceName="popupMessageRiskAddress" Modal="true" meta:resourcekey="popupMessageRiskAddressResource"
                    EnableHotTrack="False" ShowPageScrollbarWhenModal="True" PopupAction="None" AllowResize="True"
                    PopupElementID="ButtonEditRiskAddress">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControlRiskAddress" runat="server">
                            <div style="width: 250px">
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxImage ID="popupASPxImageRiskAddress" runat="server" ImageUrl="~/images/generaluse/exclamation.png">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td>
                                            <dxe:ASPxLabel ID="popuplblMessageRiskAddress" runat="server" ClientInstanceName="popuplblMessageRiskAddress"
                                                Text="La póliza no existe" meta:resourcekey="popuplblMessageRiskAddressResource">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr align="center">
                                        <td align="center">
                                            <dxe:ASPxButton ID="popupbtnAcceptMessageRiskAddress" runat="server" Width="50px"
                                                AutoPostBack="False" Text="Cerrar" Enabled="true" ClientInstanceName="popupbtnAcceptMessageRiskAddress"
                                                meta:resourcekey="popupbtnAcceptMessageRiskAddressResource">
                                                <ClientSideEvents Click="function(s, e) {popupMessageRiskAddress.Hide();}" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
            </div>
        </td>
    </tr>
</table>
<asp:SqlDataSource ID="DSRiskAddressSearch" runat="server" ConnectionString="" ProviderName=""
    SelectCommand="SELECT SDESCADD FROM INSUDB.ADDRESS WHERE NRECOWNER IN (1, 8, 15) AND NPOLICY = :NPOLICY
">
    <SelectParameters>
        <asp:Parameter Name="NPOLICY" />
    </SelectParameters>
</asp:SqlDataSource>
<dxpo:XpoDataSource ID="XpoRiskAddressControl" runat="server" ServerMode="True" TypeName="GIT.EDW.Query.Model.BackOffice.RiskAddress">
</dxpo:XpoDataSource>
