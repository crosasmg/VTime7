<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NavigationDirectory.ascx.cs" Inherits="Dropthings.Widgets.NavigationDirectory" %>

<asp:Panel ID="pnlEdit" runat="server" Width="336px" Visible="False"
    meta:resourcekey="pnlEditResource1">
    <asp:HiddenField ID="hdnLang" runat="server" />
    <table style="width: 208px">
        <tr>
            <td>Category: </td>
            <td class="style1">
                <asp:DropDownList ID="ddCategories" runat="server" AutoPostBack="True"
                    DataTextField="DESCRIPTION" DataValueField="CODE" meta:resourcekey="ddCategoriesResource1">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td class="style1">&nbsp;</td>
        </tr>
    </table>
</asp:Panel>

<dxtv:ASPxTreeView ID="MenuTreeView" runat="server" EnableCallBacks="True" ClientIDMode="AutoID"
    meta:resourcekey="TreeViewMenuResource1">
</dxtv:ASPxTreeView>