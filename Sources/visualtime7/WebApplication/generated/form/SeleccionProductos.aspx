<%@ Page Title="Seleccionar Producto" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="SeleccionProductos.aspx.vb" Inherits="SeleccionProductosWebForm" UICulture="auto" Culture="auto" meta:resourcekey="SeleccionProductosPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="SeleccionProductosUserControl.ascx" tagname="SeleccionProductosUserControl" tagprefix="SeleccionProductosUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="SeleccionProductos.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <SeleccionProductosUC:SeleccionProductosUserControl ID="SeleccionProductosUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>        
</asp:Content>