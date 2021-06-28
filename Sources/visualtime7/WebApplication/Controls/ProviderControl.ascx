<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProviderControl.ascx.vb"
    Inherits="Controls_ProviderControl" %>
<script type="text/javascript">
</script>
<table border="0">
    <tr>
        <td>
            <div>
                <table>
                    <tr align="left">
                        <td align="left">
                            <table>
                                <tr align="left">
                                    <td align="left">
                                        <dxe:ASPxButtonEdit ID="ButtonEditProviderControl" runat="server" ClientInstanceName="ButtonEditProviderControl"
                                            AutoPostBack="False" MaxLength="5" Width="80px">
                                            <Buttons>
                                                <dxe:EditButton>
                                                </dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) { PopUpProvider.Show(); }" TextChanged="function(s, e) { GridViewProviderControl.PerformCallback('CustomSearch'); }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td align="right">
                                        <dxe:ASPxLabel ID="LabelProviderControl" runat="server" ClientInstanceName="LabelProviderControl">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <dxpc:ASPxPopupControl ID="PopUpProvider" runat="server" PopupHorizontalAlign="LeftSides"
                    PopupVerticalAlign="Below" ShowCloseButton="False" ShowHeader="False" Height="0px"
                    Width="0px" ClientInstanceName="PopUpProvider" AllowResize="True" PopupAction="None"
                    PopupElementID="ButtonEditProviderControl">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <dxwgv:ASPxGridView ID="GridViewProviderControl" runat="server" AutoGenerateColumns="False"
                                OnHtmlRowCreated="GridViewProviderControl_HtmlRowCreated" Width="500px" ClientInstanceName="GridViewProviderControl"
                                KeyFieldName="NPROVIDER" DataSourceID="XpoProviderControl">
                                <ClientSideEvents RowClick="function(s, e) {var row = s.GetRow(e.visibleIndex);
	                                                                ButtonEditProviderControl.SetValue(row.attributes[&quot;nProvider&quot;].nodeValue);
	                                                                LabelProviderControl.SetText(row.attributes[&quot;sCliename&quot;].nodeValue);}"
                                    RowDblClick="function(s, e) {	var row = s.GetRow(e.visibleIndex);
	                                                                    ButtonEditProviderControl.SetValue(row.attributes[&quot;nProvider&quot;].nodeValue);
	                                                                    LabelProviderControl.SetText(row.attributes[&quot;sCliename&quot;].nodeValue);
	                                                                    PopUpProvider.Hide();}" BeginCallback="function(s, e) { GridViewProviderControl.cp_ExistsProvider = true; }"
                                    EndCallback="function(s, e) {  if (GridViewProviderControl.cp_ExistsProvider) {
            if (typeof (GridViewProviderControl.cp_ProviderName) != 'undefined') {
                LabelProviderControl.SetText(GridViewProviderControl.cp_ProviderName);
            }
        }
        else {
            LabelProviderControl.SetText('');
            popupMessageProviderControl.Show();
        } }"></ClientSideEvents>
                                <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFooter="True" ShowGroupButtons="True"
                                    ShowStatusBar="Hidden" ShowHorizontalScrollBar="true" />
                                <SettingsBehavior AllowFocusedRow="true" ColumnResizeMode="Control" />
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn FieldName="NPROVIDER" VisibleIndex="0" Width="100px"
                                        Caption="Código" meta:resourcekey="NPROVIDERColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SCLIENT" VisibleIndex="1" Width="150px"
                                        Caption="Numero de Cliente" meta:resourcekey="SCLIENTColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SCLIENAME" VisibleIndex="2" Width="200px"
                                        Caption="Nombre" meta:resourcekey="SCLIENAMEColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NOFFICE" VisibleIndex="3" Width="200px"
                                        Caption="Oficina a la que pertenece" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NOFFICEDESC" VisibleIndex="4" Width="200px"
                                        Caption="Oficina a la que pertenece" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NTYPEPROV" VisibleIndex="5" Width="100px"
                                        Caption="Tipo de proveedor" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NTYPEPROVDESC" VisibleIndex="6" Width="200px"
                                        Caption="Tipo de proveedor" meta:resourcekey="NTYPEPROVDESCColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NTYPESUPPORT" VisibleIndex="7" Width="100px"
                                        Caption="Tipo de reespaldo" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="NTYPESUPPORTDESC" VisibleIndex="8" Width="200px"
                                        Caption="Tipo de reespaldo" Visible="False">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SDESCADD" VisibleIndex="9" Width="500px"
                                        Caption="Direccion" meta:resourcekey="SDESCADDColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SPHONES" VisibleIndex="10" Width="200px"
                                        Caption="Teléfonos" meta:resourcekey="SPHONESColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="SCLINUMDOCUS" VisibleIndex="11" Width="200px"
                                        Caption="Documentos" meta:resourcekey="SCLINUMDOCUSColumnResource">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                            </dxwgv:ASPxGridView>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
                <dxpc:ASPxPopupControl AllowDragging="True" ShowCloseButton="False" PopupHorizontalAlign="LeftSides"
                    PopupVerticalAlign="Below" ID="popupMessageProviderControl" runat="server" HeaderText=""
                    ClientInstanceName="popupMessageProviderControl" Modal="true" meta:resourcekey="popupMessageProviderControlResource"
                    EnableHotTrack="False" ShowPageScrollbarWhenModal="True" PopupAction="None" AllowResize="True"
                    PopupElementID="ButtonEditProviderControl">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl ID="PopupControlContentControlProvider" runat="server">
                            <div style="width: 250px">
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxImage ID="popupImageMessageProvider" runat="server" ImageUrl="~/images/generaluse/exclamation.png">
                                            </dxe:ASPxImage>
                                        </td>
                                        <td>
                                            <dxe:ASPxLabel ID="popuplblMessageProviderControl" runat="server" ClientInstanceName="popuplblMessageProviderControl"
                                                Text="El Proveedor no está registrado" meta:resourcekey="popuplblMessageProviderControlResource">
                                            </dxe:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr align="center">
                                        <td align="center">
                                            <dxe:ASPxButton ID="popupbtnAcceptMessageProviderControl" runat="server" Width="50px"
                                                AutoPostBack="False" Text="Cerrar" Enabled="true" ClientInstanceName="popupbtnAcceptMessageProviderControl"
                                                meta:resourcekey="popupbtnAcceptMessageProviderControlResource">
                                                <ClientSideEvents Click="function(s, e) {popupMessageProviderControl.Hide();}" />
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
<asp:SqlDataSource ID="DsProviderSeach" runat="server" ConnectionString="" ProviderName=""
    SelectCommand="SELECT SCLIENAME FROM INSUDB.GCV_PROVIDERCONTROL WHERE NProvider = :NProvider">
    <SelectParameters>
        <asp:Parameter Name="NProvider" />
    </SelectParameters>
</asp:SqlDataSource>
<dxpo:XpoDataSource ID="XpoProviderControl" runat="server" ServerMode="True" TypeName="GIT.EDW.Query.Model.BackOffice.Provider">
</dxpo:XpoDataSource>
