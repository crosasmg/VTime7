<%@ Control Language="VB" AutoEventWireup="false" CodeFile="QueryManager.ascx.vb"
    Inherits="Dropthings.Widgets.QueryManager" %>

<script src="/customscripts/clientcontrol_devexpress.js" type="text/javascript"></script>
<script src='<%= ResolveClientUrl("QueryManager.js") %>' type="text/javascript"></script>
<style type="text/css">
    .gridcontainer {
        border: solid 1px #99BBE8;
        border-top-width: 0px;
        width: 75%;
    }
</style>
<asp:Panel ID="pnlSettings" runat="server" Visible="False" Width="100%">
    <table style="width: 100%;">
        <tr>
            <td align="right" width="50%">
                <asp:Label ID="EnviromentsLabel" runat="server" Text="" meta:resourcekey="EnviromentsLabelResource" />
            </td>
            <td width="50%">
                <%--                <dxe:ASPxComboBox ID="EnviromentsASPxComboBox" ClientInstanceName="EnviromentsASPxComboBox"
                    runat="server" DataSourceID="EnviromentDS" ValueField="EnviromentID" TextField="Description"
                    AutoPostBack="True" />
                <asp:SqlDataSource ID="EnviromentDS" runat="server" ConnectionString="<%$ ConnectionStrings:DesignerWorkbenchConnectionString %>"
                    SelectCommand="SELECT [EnviromentID], [Description] FROM [TabEnviroments]"></asp:SqlDataSource>--%>
            </td>
        </tr>
        <tr>
            <td align="right" width="50%">
                <asp:Label ID="ASPxLabelQuery" runat="server" Text="" meta:resourcekey="ASPxLabelQueryResource" />
            </td>
            <td width="50%">
                <%--                <dxe:ASPxComboBox ID="QueryComboBox" ClientInstanceName="QueryComboBox" runat="server"
                    AutoPostBack="True" DataSourceID="" ValueField="" TextField="" Enabled="false" />
                <asp:SqlDataSource ID="QueryDS" runat="server" ConnectionString="<%$ ConnectionStrings:DesignerWorkbenchConnectionString %>">
                </asp:SqlDataSource>--%>
            </td>
        </tr>
    </table>
    <hr />
</asp:Panel>
<table id="TableQuery" style="width: 100%;" runat="server">
    <tr>
        <td colspan="2">
            <asp:Panel ID="pnlUserInput" runat="server" HorizontalAlign="Left" />
        </td>
    </tr>
    <tr>
        <td id="TdTreeView" width="20%" valign="top" rowspan="2" runat="server">
            <asp:TreeView ID="TreeViewTables" SkinID="TreeViewSkin" runat="server">
            </asp:TreeView>
        </td>
        <td id="TdGridView" width="80%" valign="top" runat="server">
            <dxe:ASPxButton ID="CustomizeColumnsBtn" runat="server" Text="Add Columns" ClientInstanceName="CustomizeColumnsBtn"
                UseSubmitBehavior="false" AutoPostBack="false" EnableDefaultAppearance="False"
                Width="108px" Visible="False">
                <Image Url="~/images/generaluse/column.PNG" />
                <ClientSideEvents Click="CustomizeColumnsBtn_Click" />
            </dxe:ASPxButton>
            <dx:WebChartControl ID="ChartViewQueries" runat="server" Visible="False"
                Height="600px" Width="700px">
            </dx:WebChartControl>
            <dxwgv:ASPxGridView ID="GridViewQueries" ClientInstanceName="GridViewQueries" runat="server"
                AutoGenerateColumns="False" Width="100%" meta:resourcekey="GridViewQueriesResource1">
                <SettingsBehavior ColumnResizeMode="NextColumn" />
                <SettingsText PopupEditFormCaption="..." GroupPanel="..." EmptyDataRow="-" EmptyHeaders="" />
                <SettingsEditing PopupEditFormAllowResize="True" PopupEditFormHorizontalAlign="WindowCenter"
                    PopupEditFormVerticalAlign="WindowCenter" />
                <ClientSideEvents CustomizationWindowCloseUp="GridView_CustomizationWindowCloseUp"
                    RowClick="function(s, e) { setVisibleIndex(s, e); }" EndCallback="function(s, e) { GridViewQueries_EndCallback(s, e); }" />
                <Templates>
                    <EditForm>
                        <br />
                        <dx:ASPxGridViewTemplateReplacement ID="TemplateReplacementContent" ReplacementType="EditFormEditors" runat="server" />
                        <br />
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
            <dx:ASPxDataView ID='DataViewQueries' ClientInstanceName='DataViewQueries' runat='server' Visible='False' Width="100%"
                EnablePagingCallbackAnimation='True' EnableViewState='False' ViewStateMode='Disabled' EnableTheming='True' PagerAlign="Left">
                <PagerSettings ShowNumericButtons='False' Position="Bottom" />
                <SettingsTableLayout ColumnCount="5" RowsPerPage="2" />
                <ItemTemplate />
            </dx:ASPxDataView>
            <dxe:ASPxLabel ID="ErrorMsgASPxLabel" runat="server" Text="" Visible="false" ForeColor="Red" />
            <table id="TableExport" width="100%" runat="server" visible="False">
                <tr>
                    <td style="width: 10%" align="left">
                        <dxe:ASPxCheckBox ID="DataRowsCheckBox" runat="server" Text="<%$ Resources:Resource, CheckAll %>" ClientInstanceName="DataRowsCheckBox"
                            ClientIDMode='Static' AutoPostBack="false" EnableDefaultAppearance="False" ClientVisible="false"
                            ClientSideEvents-CheckedChanged="CheckUncheckRows" CheckState="Unchecked" />
                    </td>
                    <td style="width: 80%" align="right">
                        <dxe:ASPxLabel ID="ASPxLabelExport" runat="server" Text="Export to:" meta:resourcekey="ASPxLabelExportResource1" />
                    </td>
                    <td style="width: 10%" align="right">
                        <dxe:ASPxImage ID="xls_Export" runat="server" ImageUrl="~/images/16x16/FileFormat/xls.png" Cursor='pointer'
                            Height="16" Width="16">
                            <ClientSideEvents Click="function(s, e) {GridViewQueries.PerformCallback('Mod:Exp:xls');}" />
                        </dxe:ASPxImage>
                        <dxe:ASPxImage ID="pdf_Export" runat="server" ImageUrl="~/images/16x16/FileFormat/pdf.png" Cursor='pointer'
                            Height="16" Width="16">
                            <ClientSideEvents Click="function(s, e) {GridViewQueries.PerformCallback('Mod:Exp:pdf');}" />
                        </dxe:ASPxImage>
                        <dxe:ASPxImage ID="csv_Export" runat="server" ImageUrl="~/images/16x16/FileFormat/csv.png" Cursor='pointer'
                            Height="16" Width="16">
                            <ClientSideEvents Click="function(s, e) {GridViewQueries.PerformCallback('Mod:Exp:csv');}" />
                        </dxe:ASPxImage>
                        <dxe:ASPxImage ID="rtf_Export" runat="server" ImageUrl="~/images/16x16/FileFormat/rtf.png" Cursor='pointer'
                            Height="16" Width="16">
                            <ClientSideEvents Click="function(s, e) {GridViewQueries.PerformCallback('Mod:Exp:rtf');}" />
                        </dxe:ASPxImage>
                    </td>
                </tr>
            </table>
            <dxe:ASPxLabel ID="ActionMsgASPxLabel" runat="server" Text="" Visible="false" ForeColor="DarkGreen"
                meta:resourcekey="ActionMsgASPxLabelResource" />
            <br />
            <dxe:ASPxLabel ID="DetailASPxLabel" runat="server" Text="Double click on the row must get more detail" ClientVisible="false"
                meta:resourcekey="DetailASPxLabelResource" />
        </td>
    </tr>
</table>
<dxlp:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" />
<dxpc:ASPxPopupControl ID="PopUpImageDetail" runat="server" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False"
    Height="0px" Width="0px" ClientInstanceName="PopUpImageDetail" AllowResize="True"
    PopupAction="None" ShowPageScrollbarWhenModal="True">
    <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
            <dxwgv:ASPxGridView ID="GridViewImageDetail" runat="server" AutoGenerateColumns="False"
                KeyFieldName="NCONSEC" Visible="True" ClientInstanceName="GridViewImageDetail">
                <Settings ShowPreview="True" />
                <SettingsPager PageSize="1">
                </SettingsPager>
                <ClientSideEvents RowClick="function(s, e) { setVisibleIndexImage(s, e); }" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="NCONSEC" Caption="Consecutivo" VisibleIndex="0"
                        Width="50px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="SDESCRIPT" Caption="Descripción" VisibleIndex="1"
                        Width="200px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DCOMPDATE" Caption="Fecha de creación" VisibleIndex="2"
                        Width="200px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DNULLDATE" Caption="Fecha Hasta" VisibleIndex="3"
                        Width="200px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataHyperLinkColumn FieldName="NCONSEC" Caption="Vista Completa" VisibleIndex="4"
                        Width="100px">
                        <CellStyle HorizontalAlign="Center ">
                        </CellStyle>
                        <PropertiesHyperLinkEdit NavigateUrlFormatString="javascript:ShowImageFull('IIMAGE');"
                            ImageUrl="/images/16x16/General/imageLink.gif">
                        </PropertiesHyperLinkEdit>
                    </dxwgv:GridViewDataHyperLinkColumn>
                    <dxwgv:GridViewDataBinaryImageColumn FieldName="IIMAGE" VisibleIndex="5" Visible="false">
                    </dxwgv:GridViewDataBinaryImageColumn>
                </Columns>
                <Templates>
                    <PreviewRow>
                        <dxe:ASPxBinaryImage ID="ImageDetail" runat="server" Value='<%# Eval("IIMAGE") %>'
                            Width="400px" Height="300px">
                        </dxe:ASPxBinaryImage>
                    </PreviewRow>
                </Templates>
            </dxwgv:ASPxGridView>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>
<dxpc:ASPxPopupControl ID="PopupImagePreview" runat="server" AllowDragging="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ModalBackgroundStyle-BackgroundImage-HorizontalPosition="center"
    ClientInstanceName="PopupImagePreview" AllowResize="True" Modal="True" ShowPageScrollbarWhenModal="True">
    <ModalBackgroundStyle>
        <BackgroundImage HorizontalPosition="center"></BackgroundImage>
    </ModalBackgroundStyle>
    <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
            <dxcp:ASPxCallbackPanel ID="cpPopupImagePreview" ClientInstanceName="cpPopupImagePreview"
                runat="server" ShowLoadingPanel="True" ShowLoadingPanelImage="True">
                <PanelCollection>
                    <dxp:PanelContent runat="server">
                        <dxe:ASPxBinaryImage ID="BinaryImagePreview" runat="server" AlternateText="Loading...">
                        </dxe:ASPxBinaryImage>
                    </dxp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>
<dxpc:ASPxPopupControl ID="PopUpNoteDetail" runat="server" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" ShowCloseButton="False" ShowHeader="False"
    Height="0px" Width="0px" ClientInstanceName="PopUpNoteDetail" AllowResize="True"
    PopupAction="None">
    <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <dxwgv:ASPxGridView ID="GridViewNoteDetail" runat="server" AutoGenerateColumns="False"
                KeyFieldName="NCONSEC" Visible="True" ClientInstanceName="GridViewNoteDetail">
                <SettingsPager PageSize="1">
                </SettingsPager>
                <Settings ShowPreview="true" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="NCONSEC" VisibleIndex="0" Caption="Consecutivo"
                        Width="50px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="SDESCRIPT" VisibleIndex="1" Caption="Descripción"
                        Width="200px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="DCOMPDATE" VisibleIndex="2" Caption="Fecha de creación"
                        Width="200px">
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="DNULLDATE" VisibleIndex="3" Caption="Fecha limite"
                        Width="200px">
                    </dxwgv:GridViewDataDateColumn>
                </Columns>
                <Templates>
                    <PreviewRow>
                        <%  If Not notesformat.Contains("RTF") Then%>
                        <table style="border: none">
                            <tbody>
                                <tr>
                                    <td style="border: none;">
                                        <%#Eval("TDS_TEXT")%>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <% End If%>
                    </PreviewRow>
                </Templates>
            </dxwgv:ASPxGridView>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>
<dxpc:ASPxPopupControl ID="PopupNotePreview" runat="server" AllowDragging="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="PopupNotePreview"
    AllowResize="True" Modal="True" ShowPageScrollbarWhenModal="True" Width="600px">
    <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
            <dxcp:ASPxCallbackPanel ID="cpPopupNotePreview" ClientInstanceName="cpPopupNotePreview"
                runat="server" ShowLoadingPanel="True" ShowLoadingPanelImage="True" Width="600px"
                Height="400px">
                <PanelCollection>
                    <dxp:PanelContent ID="PanelContent1" runat="server">
                        <%--                                <table runat="server" style="border:none" id="TableNotePreview">
                                     <tbody>
                                     <tr>
                                         <td style="border:none;"></td>
                                     </tr>
                                     </tbody>
                                 </table>
                        --%>
                        <%--<dxe:ASPxLabel ID="LabelNotePreview" runat="server" AlternateText="Loading..."></dxe:ASPxLabel>--%>
                        <div runat="server" id="DivNotePreview">
                        </div>
                    </dxp:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>
<dxm:ASPxPopupMenu ID="PopupMenuCallTo" runat="server" PopupAction="None" PopupElementID="GridViewQueries"
    ClientInstanceName="PopupMenuCallTo">
    <%--        <Items>
            <dxm:MenuItem Text="Call To">--%>
    <Items>
        <dxm:MenuItem NavigateUrl="&quot;callto:+50622915302" Text="Skype" Name="ItemCallSkype">
        </dxm:MenuItem>
        <dxm:MenuItem NavigateUrl="&quot;livecall:+50622915302" Text="Msn" Name="ItemCallMsn">
        </dxm:MenuItem>
    </Items>
    <%--            </dxm:MenuItem>
        </Items>--%>
</dxm:ASPxPopupMenu>
<dxm:ASPxPopupMenu ID="PopupMenuMailTo" runat="server" PopupAction="None" PopupElementID="GridViewQueries"
    ClientInstanceName="PopupMenuMailTo">
    <%--        <Items>
            <dxm:MenuItem Text="Mail To">--%>
    <Items>
        <dxm:MenuItem NavigateUrl="mailto:soporte.gitcr@gmail.com" Text="soporte.gitcr@gmail.com"
            Name="ItemMailTo">
        </dxm:MenuItem>
    </Items>
    <%--            </dxm:MenuItem>
        </Items>--%>
</dxm:ASPxPopupMenu>
<dxm:ASPxPopupMenu ID="PopupMenuMessenger" runat="server" PopupAction="None" PopupElementID="GridViewQueries"
    ClientInstanceName="PopupMenuMessenger">
    <%--        <Items>
            <dxm:MenuItem Text="Instant Messenger">--%>
    <Items>
        <dxm:MenuItem NavigateUrl="skype:git.cr" Text="Skype" Name="ItemSkype">
        </dxm:MenuItem>
        <dxm:MenuItem NavigateUrl="msnim:chat?contact=soporte.gitcr@gmail.com" Text="Msn"
            Name="ItemMsn">
        </dxm:MenuItem>
        <dxm:MenuItem NavigateUrl="aim:goim?screenname=soporte.gitcr@gmail.com" Text="Aim"
            Visible="False" Name="ItemAim">
        </dxm:MenuItem>
        <dxm:MenuItem NavigateUrl="ymsgr:sendim?soporte.gitcr@gmail.com" Text="Ymsgr" Visible="False"
            Name="ItemYmsgr">
        </dxm:MenuItem>
    </Items>
    <%--           </dxm:MenuItem>
        </Items>--%>
</dxm:ASPxPopupMenu>
<dxm:ASPxPopupMenu ID="PopupMenuActions" runat="server" PopupAction="None" PopupElementID="GridViewQueries"
    ClientInstanceName="PopupMenuActions">
    <Items>
        <%--            <dxm:MenuItem Text="Actions">
                <Items>--%>
        <dxm:MenuItem NavigateUrl="#" Text="Action 0" Name="Item0">
        </dxm:MenuItem>
        <dxm:MenuItem NavigateUrl="#" Text="Action 1" Name="Item1">
        </dxm:MenuItem>
        <dxm:MenuItem NavigateUrl="#" Text="Action 2" Name="Item2">
        </dxm:MenuItem>
        <dxm:MenuItem NavigateUrl="#" Text="Action 3" Name="Item3">
        </dxm:MenuItem>
        <dxm:MenuItem NavigateUrl="#" Text="Action 4" Name="Item4">
        </dxm:MenuItem>
        <dxm:MenuItem NavigateUrl="#" Text="Action 5" Name="Item5">
        </dxm:MenuItem>
        <dxm:MenuItem NavigateUrl="#" Text="Action 6" Name="Item6">
        </dxm:MenuItem>
        <dxm:MenuItem NavigateUrl="#" Text="Action 7" Name="Item7">
        </dxm:MenuItem>
        <dxm:MenuItem NavigateUrl="#" Text="Action 8" Name="Item8">
        </dxm:MenuItem>
        <dxm:MenuItem NavigateUrl="#" Text="Action 9" Name="Item9">
        </dxm:MenuItem>
        <%--                </Items>
            </dxm:MenuItem>--%>
    </Items>
</dxm:ASPxPopupMenu>
