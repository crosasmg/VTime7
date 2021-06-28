<%@ Page Title="Selección Seguros de Vida" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="SeleccionVidaSeguros.aspx.vb" Inherits="SeleccionVidaSegurosWebForm" UICulture="auto" Culture="auto" meta:resourcekey="SeleccionVidaSegurosPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="SeleccionVidaSegurosUserControl.ascx" tagname="SeleccionVidaSegurosUserControl" tagprefix="SeleccionVidaSegurosUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="SeleccionVidaSeguros.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="SeleccionVidaSegurosUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <SeleccionVidaSegurosUC:SeleccionVidaSegurosUserControl ID="SeleccionVidaSegurosUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>