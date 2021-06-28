<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DocumentPending.ascx.vb"
    Inherits="Dropthings.Widgets.DocumentPendingUserControl" %>
<asp:GridView ID="ListGridView" runat="server" AutoGenerateColumns="False" SkinID="Main">
    <Columns>
        <asp:BoundField DataField="CREATIONDATE" HeaderText="Created" />
        <asp:TemplateField HeaderText="Document">
            <ItemTemplate>
                <a href="../<%#Eval("PAGE")%>?id=<%#Eval("FORMID")%>">
                    <%#Eval("TITLE")%></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="UPDATEDATE" HeaderText="Last Updated" />
    </Columns>
</asp:GridView>