<%@ Page Title="Mi Vida vale" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="NNCotizacionMiVidaValeIIALC.aspx.vb" Inherits="NNCotizacionMiVidaValeIIALCWebForm" UICulture="auto" Culture="auto" meta:resourcekey="NNCotizacionMiVidaValeIIALCPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="NNCotizacionMiVidaValeIIALCUserControl.ascx" tagname="NNCotizacionMiVidaValeIIALCUserControl" tagprefix="NNCotizacionMiVidaValeIIALCUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="NNCotizacionMiVidaValeIIALC.js" type="text/javascript"></script>
<link rel='stylesheet' href='/Controls/PhysicalAddressControl.css' type='text/css' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="NNCotizacionMiVidaValeIIALCUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <NNCotizacionMiVidaValeIIALCUC:NNCotizacionMiVidaValeIIALCUserControl ID="NNCotizacionMiVidaValeIIALCUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>