<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PremiumControl.ascx.vb"
    Inherits="Controls_PremiumControl" %>
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
                                        <dxe:ASPxButtonEdit ID="ButtonEditPremium" runat="server" ClientInstanceName="ButtonEditPremium"
                                            AutoPostBack="False" MaxLength="14" Width="130px">
                                            <Buttons>
                                                <dxe:EditButton>
                                                </dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) { PopUpPremium.Show(); }" TextChanged="function(s, e) { GridViewPremium.PerformCallback('CustomSearch'); }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td align="right">
                                        <dxe:ASPxLabel ID="LabelPremium" runat="server" ClientInstanceName="LabelPremium">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <dxpc:ASPxPopupControl ID="PopUpPremium" runat="server" PopupHorizontalAlign="LeftSides"
                    PopupVerticalAlign="Below" ShowCloseButton="False" ShowHeader="False" Height="0px"
                    Width="0px" ClientInstanceName="PopUpPremium" AllowResize="True" PopupAction="None"
                    PopupElementID="ButtonEditPremium">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <dxwgv:ASPxGridView ID="GridViewPremium" runat="server" AutoGenerateColumns="False"
                                OnHtmlRowCreated="GridViewClient_HtmlRowCreated" Width="500px" ClientInstanceName="GridViewPremium"
                                OnCustomCallback="GridViewPremium_CustomCallback" KeyFieldName="NRECEIPT" DataSourceID="XpoPremiumControl">
                                <ClientSideEvents RowClick="function(s, e) {var row = s.GetRow(e.visibleIndex);
	                                                                ButtonEditPremium.SetValue(row.attributes[&quot;sReceipt&quot;].nodeValue);
	                                                                //LabelPremium.SetText(row.attributes[&quot;sCliename&quot;].nodeValue);
	                                                                }" RowDblClick="function(s, e) {	var row = s.GetRow(e.visibleIndex);
	                                                                    ButtonEditPremium.SetValue(row.attributes[&quot;sReceipt&quot;].nodeValue);
	                                                                    //LabelPremium.SetText(row.attributes[&quot;sCliename&quot;].nodeValue);
	                                                                    PopUpPremium.Hide();}" BeginCallback="function(s, e) { GridViewPremium.cp_ExistsClient = true; }"
                                    EndCallback="function(s, e) { if (GridViewPremium.cp_Receipt != '')
            ButtonEditPremium.SetText(GridViewPremium.cp_Receipt);

        if (!GridViewPremium.cp_ExistsClient) {
            ButtonEditPremium.SetText('');
            popupMessagePremium.Show();
        } }"></ClientSideEvents>
                                <Settings ShowFooter="True" ShowGroupButtons="True" ShowStatusBar="Hidden" ShowHorizontalScrollBar="true"
                                    ShowFilterBar="Visible" />
                                <SettingsBehavior AllowFocusedRow="true" ColumnResizeMode="Control" />
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn FieldName="NRECEIPT" VisibleIndex="0" Caption="Factura"
                                        meta:resourcekey="NRECEIPTColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NDIGIT" VisibleIndex="1" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NPAYNUMBE" VisibleIndex="2" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NBRANCH" VisibleIndex="3" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SCLIENT" VisibleIndex="4" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SCLIENAME" VisibleIndex="5" Caption="Cliente"
                                        Width="200px" meta:resourcekey="SCLIENAMEColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="BRANCHDESCRIPT" VisibleIndex="6" Caption="Descripción ramo"
                                        Width="200px" meta:resourcekey="BRANCHDESCRIPTColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NPRODUCT" VisibleIndex="7" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="PRODUCTDESCRIPT" VisibleIndex="8" Caption="Descripción producto"
                                        Width="200px" meta:resourcekey="PRODUCTDESCRIPTColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SCERTYPEDESC" VisibleIndex="9" Caption="Tipo de Registro"
                                        Width="100px" meta:resourcekey="SCERTYPEDESCColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NPOLICY" VisibleIndex="10" Caption="Póliza/cotización"
                                        meta:resourcekey="NPOLICYColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NCERTIF" VisibleIndex="11" Caption="Certificado"
                                        Width="100px" meta:resourcekey="NCERTIFColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SCERTYPE" VisibleIndex="12" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NPREMIUM" VisibleIndex="13" Caption="Monto de prima"
                                        meta:resourcekey="NPREMIUMColumnResource">
                                        <PropertiesTextEdit DisplayFormatString="#,##0.00">
                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..999g>.<00..99>" />
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </dxwgv:ASPxGridView>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
                <dxpc:ASPxPopupControl AllowDragging="True" ShowCloseButton="False" PopupHorizontalAlign="LeftSides"
                    PopupVerticalAlign="Below" ID="popupMessagePremium" runat="server" ClientInstanceName="popupMessagePremium"
                    Modal="true" EnableHotTrack="False" ShowPageScrollbarWhenModal="True" HeaderText=""
                    meta:resourcekey="popupMessagePremiumResource" PopupAction="None" AllowResize="True"
                    PopupElementID="ButtonEditPremium">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControlPremium" runat="server">
                            <div style="width: 250px">
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxImage ID="popupASPxImagePremium" runat="server" ImageUrl="~/images/generaluse/exclamation.png">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td>
                                            <dxe:ASPxLabel ID="popuplblMessagePremium" runat="server" ClientInstanceName="popuplblMessagePremium"
                                                Text="El recibo no está registrado" meta:resourcekey="popuplblMessagePremiumResource">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr align="center">
                                        <td align="center">
                                            <dxe:ASPxButton ID="popupbtnAcceptMessagePremium" runat="server" Width="50px" AutoPostBack="False"
                                                Text="Cerrar" Enabled="true" ClientInstanceName="popupbtnAcceptMessagePremium"
                                                meta:resourcekey="popupbtnAcceptMessagePremiumResource">
                                                <ClientSideEvents Click="function(s, e) {popupMessagePremium.Hide();}" />
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
<asp:SqlDataSource ID="DsPremiumSeach" runat="server" ConnectionString="" ProviderName=""
    SelectCommand="Select * from INSUDB.GCV_PREMIUMCONTROL prem
                        where PREM.SCERTYPE = :SCERTYPE
                        and PREM.NRECEIPT = :NRECEIPT
                        and PREM.NBRANCH = :NBRANCH
                        and PREM.NPRODUCT = :NPRODUCT
                        and PREM.NDIGIT = :NDIGIT
                        and PREM.NPAYNUMBE = :NPAYNUMBE">
    <SelectParameters>
        <asp:Parameter Name="SCLIENT" />
    </SelectParameters>
</asp:SqlDataSource>
<dxpo:XpoDataSource ID="XpoPremiumControl" runat="server" ServerMode="True" TypeName="GIT.EDW.Query.Model.BackOffice.Premium">
</dxpo:XpoDataSource>

