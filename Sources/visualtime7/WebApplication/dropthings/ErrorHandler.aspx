<%@ Page Title="Error" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="ErrorHandler.aspx.vb" MetaKeywords="PageResource1" Inherits="dropthings_ErrorHandler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" runat="Server">
    <div align="center" style="text-align: center; padding-top: 80px;">
        <dxe:ASPxImage ID="ASPxImage1" runat="server" ShowLoadingImage="True" ImageUrl="~/images/dropthings/error.png" meta:resourcekey="ASPxImage1Resource1" />
    </div>
    <div align="center" style="text-align: center; padding: 10px">
        <asp:Label ID="ErrorLabel" ForeColor="Red" Style="text-align: center; vertical-align: middle"
            Font-Size="Large" runat="server" Text="Error." meta:resourcekey="ErrorLabelResource1"></asp:Label>
        <br />
        <asp:Label ID="lblErrorDetail" ForeColor="Red" Style="text-align: center; vertical-align: middle"
            Font-Size="Small" runat="server" Text="Error Detail." meta:resourcekey="lblErrorDetailResource1"></asp:Label>
        <br />
    </div>
</asp:Content>