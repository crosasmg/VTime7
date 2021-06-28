<%@ Page Title="Proyectar" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="Proyectar.aspx.vb" Inherits="ProyectarWebForm" UICulture="auto" Culture="auto" meta:resourcekey="ProyectarPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="ProyectarUserControl.ascx" tagname="ProyectarUserControl" tagprefix="ProyectarUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="Proyectar.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <ProyectarUC:ProyectarUserControl ID="ProyectarUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>        
</asp:Content>