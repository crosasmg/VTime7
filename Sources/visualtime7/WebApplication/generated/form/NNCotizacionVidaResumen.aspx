<%@ Page Title="Mi Vida Vale - Resumen" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="NNCotizacionVidaResumen.aspx.vb" Inherits="NNCotizacionVidaResumenWebForm" UICulture="auto" Culture="auto" meta:resourcekey="NNCotizacionVidaResumenPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="NNCotizacionVidaResumenUserControl.ascx" tagname="NNCotizacionVidaResumenUserControl" tagprefix="NNCotizacionVidaResumenUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="NNCotizacionVidaResumen.js" type="text/javascript"></script>
<link rel='stylesheet' href='/Controls/PhysicalAddressControl.css' type='text/css' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="NNCotizacionVidaResumenUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <NNCotizacionVidaResumenUC:NNCotizacionVidaResumenUserControl ID="NNCotizacionVidaResumenUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>