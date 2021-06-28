<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Tables.ascx.vb" Inherits="Controls_Tab_Tables" %>
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
                                        <dxe:ASPxButtonEdit ID="ButtonEditTab_Tables" runat="server" ClientInstanceName="ButtonEditTab_Tables"
                                            AutoPostBack="False" MaxLength="10" Width="100px">
                                            <Buttons>
                                                <dxe:EditButton>
                                                </dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) { PopUpTab_Tables.Show(); }" TextChanged="function(s, e) { GridViewTab_Tables.PerformCallback('CustomSearch'); }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td align="right">
                                        <dxe:ASPxLabel ID="LabelTab_Tables" runat="server" ClientInstanceName="LabelTab_Tables">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <dxpc:ASPxPopupControl ID="PopUpTab_Tables" runat="server" PopupHorizontalAlign="LeftSides"
                    PopupVerticalAlign="Below" ShowCloseButton="False" ShowHeader="False" Height="0px"
                    Width="0px" ClientInstanceName="PopUpTab_Tables" AllowResize="True" PopupAction="None"
                    PopupElementID="ButtonEditTab_Tables">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <dxwgv:ASPxGridView ID="GridViewTab_Tables" runat="server" AutoGenerateColumns="False"
                                DataSourceID="DsTab_Tables" OnHtmlRowCreated="GridViewTab_Tables_HtmlRowCreated"
                                Width="400px" ClientInstanceName="GridViewTab_Tables" KeyFieldName="NCODE">
                                <ClientSideEvents RowClick="function(s, e) {var row = s.GetRow(e.visibleIndex);
	                                                                ButtonEditTab_Tables.SetValue(row.attributes[&quot;nCode&quot;].nodeValue);
	                                                                LabelTab_Tables.SetText(row.attributes[&quot;sDescript&quot;].nodeValue);}"
                                    RowDblClick="function(s, e) {	var row = s.GetRow(e.visibleIndex);
	                                                                    ButtonEditTab_Tables.SetValue(row.attributes[&quot;nCode&quot;].nodeValue);
	                                                                    LabelTab_Tables.SetText(row.attributes[&quot;sDescript&quot;].nodeValue);
	                                                                    PopUpTab_Tables.Hide();}" BeginCallback="function(s, e) { GridViewTab_Tables.cp_ExistsTab_Tables = true; }"
                                    EndCallback="function(s, e) {  if (GridViewTab_Tables.cp_ExistsTab_Tables) {
            if (typeof (GridViewTab_Tables.cp_Tab_TablesName) != 'undefined') {
                LabelTab_Tables.SetText(GridViewTab_Tables.cp_Tab_TablesName);
            }
        }
        else {
            LabelTab_Tables.SetText('');
            popupMessageTab_Tables.Show();
        } }"></ClientSideEvents>
                                <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFooter="True" ShowGroupButtons="True"
                                    ShowStatusBar="Hidden" ShowHorizontalScrollBar="true" />
                                <SettingsBehavior AllowFocusedRow="true" ColumnResizeMode="Control" />
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn FieldName="NCODE" VisibleIndex="0" Width="75px" Caption="Código"
                                        meta:resourcekey="NCODEColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SDESCRIPT" VisibleIndex="1" Width="150px"
                                        Caption="Descripcion" meta:resourcekey="SDESCRIPTColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </dxwgv:ASPxGridView>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
                <dxpc:ASPxPopupControl AllowDragging="True" ShowCloseButton="False" PopupHorizontalAlign="LeftSides"
                    PopupVerticalAlign="Below" ID="popupMessageTab_Tables" runat="server" HeaderText=""
                    ClientInstanceName="popupMessageTab_Tables" Modal="true" meta:resourcekey="popupMessageTab_TablesResource"
                    EnableHotTrack="False" ShowPageScrollbarWhenModal="True" PopupAction="None" AllowResize="True"
                    PopupElementID="ButtonEditTab_Tables">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                            <div style="width: 250px">
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxImage ID="ImageMessage" runat="server" ImageUrl="~/images/generaluse/exclamation.png">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td>
                                            <dxe:ASPxLabel ID="lblMessageTab_Tables" runat="server" ClientInstanceName="lblMessageTab_Tables"
                                                Text="El registro no existe" meta:resourcekey="lblMessageTab_TablesResource">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr align="center">
                                        <td align="center">
                                            <dxe:ASPxButton ID="btnAcceptMessageTab_Tables" runat="server" Width="50px" AutoPostBack="False"
                                                Text="Cerrar" Enabled="true" ClientInstanceName="btnAcceptMessageTab_Tables"
                                                meta:resourcekey="btnAcceptMessageTab_TablesResource">
                                                <ClientSideEvents Click="function(s, e) {popupMessageTab_Tables.Hide();}" />
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
<asp:SqlDataSource ID="DsTab_Tables" runat="server" ConnectionString="<%$ ConnectionStrings:BackOfficeConnectionString %>"
    ProviderName="<%$ ConnectionStrings:BackOfficeConnectionString.ProviderName %>">
    <%--SelectCommand="SELECT NCODIGINT AS NCODE, SDESCRIPT FROM TABLE11 WHERE SSTATREGT = '1' ORDER BY SDESCRIPT">--%>
</asp:SqlDataSource>
<asp:SqlDataSource ID="DsTab_TablesSeach" runat="server" ConnectionString="<%$ ConnectionStrings:BackOfficeConnectionString %>"
    ProviderName="<%$ ConnectionStrings:BackOfficeConnectionString.ProviderName %>">
    <%--    SelectCommand="SELECT SDESCRIPT FROM TABLE11 WHERE NCODIGINT = :NCODIGINT">
        <SelectParameters>
            <asp:Parameter Name="NCODIGINT" />
        </SelectParameters>--%>
</asp:SqlDataSource>
