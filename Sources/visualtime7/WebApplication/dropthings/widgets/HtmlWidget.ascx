<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HtmlWidget.ascx.cs" Inherits="Dropthings.Widgets.HtmlWidgetUserControl" %>
<asp:Panel ID="SettingsPanel" runat="server" Visible="false">
HTML: <br />
<asp:TextBox ID="HtmlTextBox" runat="server" Width="300" Height="200" MaxLength="2000" TextMode="MultiLine" />
<asp:Button ID="SaveSettings" runat="server" OnClick="SaveSettings_Clicked" Text="Save" />
</asp:Panel>
<asp:Literal ID="Output" runat="server" />