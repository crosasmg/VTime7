<%@ Page Title="Mi Vida Vale - Consulta del riesgo" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="NNCotizacionMiVidaValeView.aspx.vb" Inherits="NNCotizacionMiVidaValeViewWebForm" UICulture="auto" Culture="auto" meta:resourcekey="NNCotizacionMiVidaValeViewPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="NNCotizacionMiVidaValeViewUserControl.ascx" tagname="NNCotizacionMiVidaValeViewUserControl" tagprefix="NNCotizacionMiVidaValeViewUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="NNCotizacionMiVidaValeView.js" type="text/javascript"></script>
<link rel='stylesheet' href='/Controls/PhysicalAddressControl.css' type='text/css' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="NNCotizacionMiVidaValeViewUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <NNCotizacionMiVidaValeViewUC:NNCotizacionMiVidaValeViewUserControl ID="NNCotizacionMiVidaValeViewUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>