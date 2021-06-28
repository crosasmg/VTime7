<%@ Control Language="VB" AutoEventWireup="false" CodeFile="GoogleMapWidget.ascx.vb"
    Inherits="Dropthings.Widgets.GoogleMapWidgetUserControl" EnableViewState="false" %>

<asp:Panel ID="SettingsPanel" runat="Server" Visible="False">
    Latitude:
    <asp:TextBox ID="txtLatitude" Text="" runat="server" MaxLength="12" /><br />
    Longitude:
    <asp:TextBox ID="txtLongitude" Text="" runat="server" MaxLength="12" /><br />
    Zoom:
    <asp:TextBox ID="txtZoom" Text="" runat="server" MaxLength="1" size="2" /><br />
    <asp:Button ID="SaveSettingsButton" runat="Server" Text="Save" OnClick="SaveSettingsButton_Click" />
</asp:Panel>
<table width="100%">
    <tr>
        <td style="width: 1%">
            Address&nbsp;
        </td>
        <td style="width: 98%">
            <asp:TextBox ID="_txtAddress" runat="server" Width="96%" BorderStyle="Solid" BorderWidth="1"
                BackColor="White"></asp:TextBox>
        </td>
        <td style="width: 1%" align="center">
            <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/images/generaluse/search.gif" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <artem:GoogleMap ID="GoogleMap1" runat="server" DefaultMapView="Satellite" EnableDragging="true"
                EnableScrollWheelZoom="true" ShowTraffic="false" Width="100%" Height="350px"
                ShowScaleControl="true"
                Key="ABQIAAAAXNnB0D5_S0wVZO-QH5CtHBQQwiCnC_HiE2lTLt0vusMqO86tBxQr9ojEtmQGDIWvSulc0bpMyUaoXw" Latitude="9.944577646031238" Longitude="-84.11788076162338" Zoom="18">
            </artem:GoogleMap>
        </td>
    </tr>
</table>
