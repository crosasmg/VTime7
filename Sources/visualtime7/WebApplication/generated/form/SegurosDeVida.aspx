<%@ Page Title="Selección Seguros de Vida" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="SegurosDeVida.aspx.vb" Inherits="SegurosDeVidaWebForm" UICulture="auto" Culture="auto" meta:resourcekey="SegurosDeVidaPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="SegurosDeVidaUserControl.ascx" tagname="SegurosDeVidaUserControl" tagprefix="SegurosDeVidaUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="SegurosDeVida.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <SegurosDeVidaUC:SegurosDeVidaUserControl ID="SegurosDeVidaUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>        
</asp:Content>