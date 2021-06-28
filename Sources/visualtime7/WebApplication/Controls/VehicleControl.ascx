<%@ Control Language="VB" AutoEventWireup="false" CodeFile="VehicleControl.ascx.vb"
    Inherits="Controls_VehicleControl" %>
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
                                        <dxe:ASPxButtonEdit ID="ButtonEditAuto" runat="server" ClientInstanceName="ButtonEditAuto"
                                            AutoPostBack="False" MaxLength="10" Width="130px">
                                            <Buttons>
                                                <dxe:EditButton>
                                                </dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) { PopUpAuto.Show(); }" TextChanged="function(s, e) { GridViewAuto.PerformCallback('CustomSearch'); }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td align="right">
                                        <dxe:ASPxLabel ID="LabelAutoDescript" runat="server" ClientInstanceName="LabelAutoDescript">
                                        </dxe:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<dxpc:ASPxPopupControl ID="PopUpAuto" runat="server" PopupHorizontalAlign="LeftSides"
    PopupVerticalAlign="Below" ShowCloseButton="False" ShowHeader="False" Height="0px"
    Width="0px" ClientInstanceName="PopUpAuto" AllowResize="True" PopupAction="None"
    PopupElementID="ButtonEditAuto">
    <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <dxwgv:ASPxGridView ID="GridViewAuto" runat="server" AutoGenerateColumns="False"
                KeyFieldName="SREGIST" OnHtmlRowCreated="GridViewAuto_HtmlRowCreated" Width="500px"
                ClientInstanceName="GridViewAuto" DataSourceID="XpoAutoControl">
                <ClientSideEvents RowClick="function(s, e) {var row = s.GetRow(e.visibleIndex);
	                                                                ButtonEditAuto.SetValue(row.attributes[&quot;sRegist&quot;].nodeValue);
	                                                                LabelAutoDescript.SetText(row.attributes[&quot;AutoDescript&quot;].nodeValue);
	                                                                }" RowDblClick="function(s, e) {	var row = s.GetRow(e.visibleIndex);
	                                                                    ButtonEditAuto.SetValue(row.attributes[&quot;sRegist&quot;].nodeValue);
	                                                                    LabelAutoDescript.SetText(row.attributes[&quot;AutoDescript&quot;].nodeValue);
	                                                                    PopUpAuto.Hide();}" BeginCallback="function(s, e) { GridViewAuto.cp_ExistsAuto = true; }"
                    EndCallback="function(s, e) { if (GridViewAuto.cp_ExistsAuto) {
            if (typeof (GridViewAuto.cp_Auto) != 'undefined') {
                LabelAutoDescript.SetText(GridViewAuto.cp_Auto);
            }
        }
        else {
            LabelAutoDescript.SetText('');
            popupMessageAuto.Show();
        } }"></ClientSideEvents>
                <Settings ShowFooter="True" ShowGroupButtons="True" ShowStatusBar="Hidden" ShowHorizontalScrollBar="true"
                    ShowFilterBar="Visible" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="NVEHBRAND" Caption="Código de Marca" VisibleIndex="0"
                        Width="100px" Visible="False" meta:resourcekey="NVEHBRANDColumnResource">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="SVEHBRAND" Caption="Marca" VisibleIndex="1"
                        Width="100px" meta:resourcekey="SVEHBRANDColumnResource">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="SVEHMODEL" Caption="Modelo" VisibleIndex="2"
                        Width="100px" meta:resourcekey="SVEHMODELColumnResource">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="NYEAR" Caption="Año" VisibleIndex="3" Width="100px"
                        meta:resourcekey="NYEARColumnResource">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="SREGIST" Caption="Placa" VisibleIndex="4"
                        Width="100px" meta:resourcekey="SREGISTColumnResource">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="NVEHTYPE" Caption="Código de Tipo" VisibleIndex="5"
                        Width="150px" Visible="False" meta:resourcekey="NVEHTYPEColumnResource">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="SVEHTYPE" Caption="Tipo" VisibleIndex="6"
                        Width="150px" meta:resourcekey="SVEHTYPEColumnResource">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="NGROUPVEH" Caption="Código de Clase" VisibleIndex="7"
                        Width="100px" Visible="False" meta:resourcekey="NGROUPVEHColumnResource">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="SGROUPVEH" Caption="Clase" VisibleIndex="8"
                        Width="100px" meta:resourcekey="SGROUPVEHColumnResource">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="SMOTOR" Caption="Motor" VisibleIndex="9"
                        Width="150px" meta:resourcekey="SMOTORColumnResource">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="SCHASSIS" Caption="Carrocería" VisibleIndex="10"
                        Width="150px" meta:resourcekey="SCHASSISColumnResource">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="SCOLOR" Caption="Color" VisibleIndex="11"
                        Width="100px" meta:resourcekey="SCOLORColumnResource">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="AUTODESCRIPT" Caption="Descripcion" VisibleIndex="12"
                        Width="100px" Visible="False" meta:resourcekey="AUTODESCRIPTColumnResource">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior AllowFocusedRow="true" ColumnResizeMode="Control" />
            </dxwgv:ASPxGridView>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>
<dxpc:ASPxPopupControl AllowDragging="True" ShowCloseButton="False" PopupHorizontalAlign="LeftSides"
    PopupVerticalAlign="Below" ID="popupMessageAuto" runat="server" ClientInstanceName="popupMessageAuto"
    Modal="true" meta:resourcekey="popupMessageAutoResource" EnableHotTrack="False"
    ShowPageScrollbarWhenModal="True" PopupAction="None" HeaderText="" AllowResize="True"
    PopupElementID="ButtonEditAuto">
    <HeaderTemplate>
        <div>
            Mensaje</div>
    </HeaderTemplate>
    <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControlAuto" runat="server">
            <div style="width: 250px">
                <table>
                    <tr>
                        <td>
                            <dxe:ASPxImage ID="popupASPxImageAuto" runat="server" ImageUrl="~/images/generaluse/exclamation.png">
                            </dxe:ASPxImage>
                        </td>
                        <td>
                            <dxe:ASPxLabel ID="popuplblMessageAuto" runat="server" ClientInstanceName="popuplblMessageAuto"
                                Text="El auto no está registrado" meta:resourcekey="popuplblMessageAutoResource">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr align="center">
                        <td align="center">
                            <dxe:ASPxButton ID="popupbtnAcceptMessageAuto" runat="server" Width="50px" AutoPostBack="False"
                                Text="Cerrar" Enabled="true" ClientInstanceName="popupbtnAcceptMessageAuto" meta:resourcekey="popupbtnAcceptMessageAutoResource">
                                <ClientSideEvents Click="function(s, e) {popupMessageAuto.Hide();}" />
                            </dxe:ASPxButton>
                        </td>
                        x
                    </tr>
                </table>
            </div>
        </dxpc:PopupControlContentControl>
    </ContentCollection>
</dxpc:ASPxPopupControl>
<asp:SqlDataSource ID="DsAutoSearch" runat="server" ConnectionString="" ProviderName=""
    SelectCommand=" SELECT AutoDescript FROM INSUDB.GCV_AUTOCONTROL WHERE RTRIM(SREGIST) = :SREGIST ">
    <SelectParameters>
        <asp:Parameter Name="SREGIST" />
    </SelectParameters>
</asp:SqlDataSource>
<dxpo:XpoDataSource ID="XpoAutoControl" runat="server" ServerMode="True" TypeName="GIT.EDW.Query.Model.BackOffice.Auto">
</dxpo:XpoDataSource>
