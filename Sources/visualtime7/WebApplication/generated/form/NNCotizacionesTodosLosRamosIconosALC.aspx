<%@ Page Title="Cotizaciones" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="NNCotizacionesTodosLosRamosIconosALC.aspx.vb" Inherits="NNCotizacionesTodosLosRamosIconosALCWebForm" UICulture="auto" Culture="auto" meta:resourcekey="NNCotizacionesTodosLosRamosIconosALCPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="NNCotizacionesTodosLosRamosIconosALCUserControl.ascx" tagname="NNCotizacionesTodosLosRamosIconosALCUserControl" tagprefix="NNCotizacionesTodosLosRamosIconosALCUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="NNCotizacionesTodosLosRamosIconosALC.js" type="text/javascript"></script>
<link rel='stylesheet' href='/Controls/PhysicalAddressControl.css' type='text/css' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="NNCotizacionesTodosLosRamosIconosALCUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <NNCotizacionesTodosLosRamosIconosALCUC:NNCotizacionesTodosLosRamosIconosALCUserControl ID="NNCotizacionesTodosLosRamosIconosALCUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>