<%@ Page Title="Error" Language="VB" MasterPageFile="~/DropthingsMasterPage.master"
    AutoEventWireup="false" CodeFile="Error.aspx.vb" Inherits="ErrorWebForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <div align="center" style="text-align: center; padding-top: 80px;">
        <dxe:ASPxImage ID="ASPxImage1" runat="server" ShowLoadingImage="true" ImageUrl="~/images/dropthings/warning.png" />
    </div>
    <div align="center" style="text-align: center; padding:10px">
        <asp:Label ID="ErrorLabel" ForeColor="Red" Style="text-align: center; vertical-align: middle"
            Font-Size="Large" runat="server" Text="Error."></asp:Label>
    </div>
</asp:Content>