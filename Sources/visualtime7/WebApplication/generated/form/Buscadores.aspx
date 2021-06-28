<%@ Page Title="Buscadores" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="Buscadores.aspx.vb" Inherits="BuscadoresWebForm" UICulture="auto" Culture="auto" meta:resourcekey="BuscadoresPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="BuscadoresUserControl.ascx" tagname="BuscadoresUserControl" tagprefix="BuscadoresUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="Buscadores.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="BuscadoresUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <BuscadoresUC:BuscadoresUserControl ID="BuscadoresUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>