<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_TablesControl.ascx.vb"
    Inherits="Controls_Tab_TablesControl" %>
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
                                        <dxe:ASPxButtonEdit ID="ButtonEditTab_TablesControl" runat="server" ClientInstanceName="ButtonEditTab_TablesControl"
                                            AutoPostBack="False" MaxLength="10" Width="100px">
                                            <Buttons>
                                                <dxe:EditButton>
                                                </dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) { PopUpTab_TablesControl.Show(); }"
                                                TextChanged="function(s, e) { GridViewTab_TablesControl.PerformCallback('CustomSearch'); }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td align="right">
                                        <dxe:ASPxLabel ID="LabelTab_TablesControl" runat="server" ClientInstanceName="LabelTab_TablesControl">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <dxpc:ASPxPopupControl ID="PopUpTab_TablesControl" runat="server" PopupHorizontalAlign="LeftSides"
                    PopupVerticalAlign="Below" ShowCloseButton="False" ShowHeader="False" Height="0px"
                    Width="0px" ClientInstanceName="PopUpTab_TablesControl" AllowResize="True" PopupAction="None"
                    PopupElementID="ButtonEditTab_TablesControl">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <dxwgv:ASPxGridView ID="GridViewTab_TablesControl" runat="server" AutoGenerateColumns="False"
                                DataSourceID="DsTab_TablesControl" OnHtmlRowCreated="GridViewTab_TablesControl_HtmlRowCreated"
                                Width="400px" ClientInstanceName="GridViewTab_TablesControl" KeyFieldName="NCODE">
                                <ClientSideEvents RowClick="function(s, e) {var row = s.GetRow(e.visibleIndex);
	                                                                ButtonEditTab_TablesControl.SetValue(row.attributes[&quot;nCode&quot;].nodeValue);
	                                                                LabelTab_TablesControl.SetText(row.attributes[&quot;sDescript&quot;].nodeValue);}"
                                    RowDblClick="function(s, e) {	var row = s.GetRow(e.visibleIndex);
	                                                                    ButtonEditTab_TablesControl.SetValue(row.attributes[&quot;nCode&quot;].nodeValue);
	                                                                    LabelTab_TablesControl.SetText(row.attributes[&quot;sDescript&quot;].nodeValue);
	                                                                    PopUpTab_TablesControl.Hide();}" BeginCallback="function(s, e) { GridViewTab_TablesControl.cp_ExistsTab_TablesControl = true; }"
                                    EndCallback="function(s, e) {  if (GridViewTab_TablesControl.cp_ExistsTab_TablesControl) {
            if (typeof (GridViewTab_TablesControl.cp_Tab_TablesControlName) != 'undefined') {
                LabelTab_TablesControl.SetText(GridViewTab_TablesControl.cp_Tab_TablesControlName);
            }
        }
        else {
            LabelTab_TablesControl.SetText('');
            popupMessageTab_TablesControl.Show();
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
                    PopupVerticalAlign="Below" ID="popupMessageTab_TablesControl" runat="server"
                    meta:resourcekey="popupMessageTab_TablesControlResource" ClientInstanceName="popupMessageTab_TablesControl"
                    Modal="true" HeaderText="" EnableHotTrack="False" ShowPageScrollbarWhenModal="True"
                    PopupAction="None" AllowResize="True" PopupElementID="ButtonEditTab_TablesControl">
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
                                            <dxe:ASPxLabel ID="lblMessageTab_TablesControl" runat="server" ClientInstanceName="lblMessageTab_TablesControl"
                                                Text="El registro no existe" meta:resourcekey="Resource">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr align="center">
                                        <td align="center">
                                            <dxe:ASPxButton ID="btnAcceptMessageTab_TablesControl" runat="server" Width="50px"
                                                AutoPostBack="False" Text="Cerrar" Enabled="true" ClientInstanceName="btnAcceptMessageTab_TablesControl"
                                                meta:resourcekey="btnAcceptMessageTab_TablesControlResource">
                                                <ClientSideEvents Click="function(s, e) {popupMessageTab_TablesControl.Hide();}" />
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
<asp:SqlDataSource ID="DsTab_TablesControl" runat="server" ConnectionString="<%$ ConnectionStrings:BackOfficeConnectionString %>"
    ProviderName="<%$ ConnectionStrings:BackOfficeConnectionString.ProviderName %>">
    <%--SelectCommand="SELECT NCODIGINT AS NCODE, SDESCRIPT FROM TABLE11 WHERE SSTATREGT = '1' ORDER BY SDESCRIPT">--%>
</asp:SqlDataSource>
<asp:SqlDataSource ID="DsTab_TablesControlSeach" runat="server" ConnectionString="<%$ ConnectionStrings:BackOfficeConnectionString %>"
    ProviderName="<%$ ConnectionStrings:BackOfficeConnectionString.ProviderName %>">
    <%--    SelectCommand="SELECT SDESCRIPT FROM TABLE11 WHERE NCODIGINT = :NCODIGINT">
        <SelectParameters>
            <asp:Parameter Name="NCODIGINT" />
        </SelectParameters>--%>
</asp:SqlDataSource>
