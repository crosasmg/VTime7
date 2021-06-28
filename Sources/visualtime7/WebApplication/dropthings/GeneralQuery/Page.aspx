<%@ Page Title="Query" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false"
    CodeFile="Page.aspx.vb" Inherits="dropthings_GeneralQuery_Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <ucqm:QueryManager ID="QueryManagerUC" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
