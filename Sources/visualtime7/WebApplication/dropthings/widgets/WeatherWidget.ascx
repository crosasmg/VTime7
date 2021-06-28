<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WeatherWidget.ascx.cs" Inherits="Dropthings.Widgets.WeatherWidgetUserControl" %>
<asp:Panel ID="SettingsPanel" runat="server" Visible="false">
<table width="100%">
<tr><td>Enter Zip:</td><td><asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox></td></tr>
<tr><td></td><td>
    <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="SaveButton_Click" Visible="false" />
    <asp:Button ID="CancelButton" runat="server" Text="Cancel" Visible="false"/>
</td></tr>
</table>
</asp:Panel>
<asp:MultiView ID="Multiview" runat="server" ActiveViewIndex="0">

<asp:View runat="server" ID="ProgressView">
    <asp:Label runat="Server" ID="label1" Text="Loading..." Font-Size="smaller" ForeColor="DimGray" />
</asp:View>

<asp:View runat="server" ID="ContentView">

<asp:Panel ID="FlakeBodyPanel" runat="server" EnableViewState="false">
    <asp:Label ID="WeatherLabel" runat="server" Text="" EnableViewState="false"></asp:Label>
</asp:Panel>

</asp:View>

</asp:MultiView>

<asp:Timer ID="MultiviewTimer" Interval="100" OnTick="LoadContentView" runat="server" /> 