<%@ Page Title="Cambio de la dirección de un cliente (New Address)" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="ChangeAddressOfClientNewAddress.aspx.vb" Inherits="ChangeAddressOfClientNewAddressWebForm" UICulture="auto" Culture="auto" meta:resourcekey="ChangeAddressOfClientNewAddressPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="ChangeAddressOfClientNewAddressUserControl.ascx" tagname="ChangeAddressOfClientNewAddressUserControl" tagprefix="ChangeAddressOfClientNewAddressUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="ChangeAddressOfClientNewAddress.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <ChangeAddressOfClientNewAddressUC:ChangeAddressOfClientNewAddressUserControl ID="ChangeAddressOfClientNewAddressUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>        
</asp:Content>