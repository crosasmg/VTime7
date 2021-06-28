<%@ Control Language="VB" AutoEventWireup="false" CodeFile="IntermedControl.ascx.vb"
    Inherits="Controls_IntermedControl" %>
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
                                        <dxe:ASPxButtonEdit ID="ButtonEditIntermed" runat="server" ClientInstanceName="ButtonEditIntermed"
                                            MaxLength="10" Width="130px">
                                            <Buttons>
                                                <dxe:EditButton>
                                                </dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) { 
        PopUpIntermed.Show();
 }" TextChanged="function(s, e) { 
	GridViewIntermed.PerformCallback('CustomSearch');  
 }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td align="right">
                                        <dxe:ASPxLabel ID="LabelIntermed" runat="server" ClientInstanceName="LabelIntermed">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <dxpc:ASPxPopupControl ID="PopUpIntermed" runat="server" PopupHorizontalAlign="LeftSides"
                    PopupVerticalAlign="Below" ShowCloseButton="False" ShowHeader="False" Height="0px"
                    Width="0px" ClientInstanceName="PopUpIntermed" AllowResize="True" PopupAction="None"
                    PopupElementID="ButtonEditIntermed">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <dxwgv:ASPxGridView ID="GridViewIntermed" runat="server" AutoGenerateColumns="False"
                                OnHtmlRowCreated="GridViewIntermed_HtmlRowCreated" Width="500px" ClientInstanceName="GridViewIntermed"
                                KeyFieldName="NINTERMED" DataSourceID="XpoProducerControl">
                                <ClientSideEvents RowClick="function(s, e) {var row = s.GetRow(e.visibleIndex);
	                                                                ButtonEditIntermed.SetValue(row.attributes[&quot;nIntermed&quot;].nodeValue);
	                                                                LabelIntermed.SetText(row.attributes[&quot;sCliename&quot;].nodeValue);}"
                                    RowDblClick="function(s, e) {	var row = s.GetRow(e.visibleIndex);
	                                                                    ButtonEditIntermed.SetValue(row.attributes[&quot;nIntermed&quot;].nodeValue);
	                                                                    LabelIntermed.SetText(row.attributes[&quot;sCliename&quot;].nodeValue);
	                                                                    PopUpIntermed.Hide();}" BeginCallback="function(s, e) { GridViewIntermed.cp_ExistsIntermed = true; }"
                                    EndCallback="function(s, e) { 
        if (GridViewIntermed.cp_ExistsIntermed) {
            if (typeof (GridViewIntermed.cp_IntermedName) != 'undefined') {
                LabelIntermed.SetText(GridViewIntermed.cp_IntermedName);
            }
        }
        else {
            LabelIntermed.SetText('');
            popupMessageIntermed.Show();
        }                                           
                                           }"></ClientSideEvents>
                                <Settings ShowFooter="True" ShowGroupButtons="True" ShowStatusBar="Hidden" ShowHorizontalScrollBar="true"
                                    ShowFilterBar="Visible" />
                                <SettingsBehavior AllowFocusedRow="true" ColumnResizeMode="Control" />
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn FieldName="NINTERMED" VisibleIndex="0" Caption="Código"
                                        Width="100px" meta:resourcekey="NINTERMEDColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SCLIENT" VisibleIndex="1" Caption="Número de Cliente"
                                        Width="150px" meta:resourcekey="SCLIENTColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SCLIENAME" VisibleIndex="2" Caption="Nombre"
                                        Width="200px" meta:resourcekey="SCLIENAMEColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NINTERTYP" VisibleIndex="3" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NINTERTYPDESC" VisibleIndex="4" Caption="Tipo"
                                        Width="150px" meta:resourcekey="NINTERTYPDESCColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NOFFICE" VisibleIndex="5" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NOFFICEDESC" VisibleIndex="6" Caption="Sucursal"
                                        Width="150px" meta:resourcekey="NOFFICEDESCColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NOFFICEAGEN" VisibleIndex="7" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NOFFICEAGENDESC" VisibleIndex="8" Caption="Oficina"
                                        Width="150px" meta:resourcekey="NOFFICEAGENDESCColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NAGENCY" VisibleIndex="9" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NAGENCYDESC" VisibleIndex="10" Caption="Agencia"
                                        Width="150px" meta:resourcekey="NAGENCYDESCColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NINT_STATUS" VisibleIndex="11" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NINT_STATUSDESC" VisibleIndex="12" Caption="Estatus"
                                        Width="150px" meta:resourcekey="NINT_STATUSDESCColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </dxwgv:ASPxGridView>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
                <dxpc:ASPxPopupControl AllowDragging="True" ShowCloseButton="False" PopupHorizontalAlign="LeftSides"
                    PopupVerticalAlign="Below" ID="popupMessageIntermed" runat="server" ClientInstanceName="popupMessageIntermed"
                    Modal="true" EnableHotTrack="False" ShowPageScrollbarWhenModal="True" PopupAction="None"
                    AllowResize="True" PopupElementID="ButtonEditIntermed" meta:resourcekey="popupMessageIntermedResource"
                    HeaderText="">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControlIntermed" runat="server">
                            <div style="width: 250px">
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxImage ID="popupImageMessageIntermed" runat="server" ImageUrl="~/images/generaluse/exclamation.png">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td>
                                            <dxe:ASPxLabel ID="popuplblMessageIntermed" runat="server" ClientInstanceName="popuplblMessageIntermed"
                                                Text="El Intermediario no está registrado" meta:resourcekey="popuplblMessageIntermedResource">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr align="center">
                                        <td align="center">
                                            <dxe:ASPxButton ID="popupbtnAcceptMessageIntermed" runat="server" Width="50px" AutoPostBack="False"
                                                Text="Cerrar" Enabled="true" ClientInstanceName="popupbtnAcceptMessageIntermed"
                                                meta:resourcekey="popupbtnAcceptMessageIntermedResource">
                                                <ClientSideEvents Click="function(s, e) {popupMessageIntermed.Hide();}" />
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
<asp:SqlDataSource ID="DsIntermedSeach" runat="server" ConnectionString="" ProviderName=""
    SelectCommand="SELECT SCLIENAME FROM INSUDB.GCV_INTERMEDCONTROL WHERE NINTERMED = :NINTERMED">
    <SelectParameters>
        <asp:Parameter Name="NINTERMED" />
    </SelectParameters>
</asp:SqlDataSource>
<dxpo:XpoDataSource ID="XpoProducerControl" runat="server" ServerMode="True" TypeName="GIT.EDW.Query.Model.BackOffice.Producer">
</dxpo:XpoDataSource>
