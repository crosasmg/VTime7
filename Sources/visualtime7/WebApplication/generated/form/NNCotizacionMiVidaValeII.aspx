<%@ Page Title="Mi Vida vale" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="NNCotizacionMiVidaValeII.aspx.vb" Inherits="NNCotizacionMiVidaValeIIWebForm" UICulture="auto" Culture="auto" meta:resourcekey="NNCotizacionMiVidaValeIIPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="NNCotizacionMiVidaValeIIUserControl.ascx" tagname="NNCotizacionMiVidaValeIIUserControl" tagprefix="NNCotizacionMiVidaValeIIUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="NNCotizacionMiVidaValeII.js" type="text/javascript"></script>
<link rel='stylesheet' href='/Controls/PhysicalAddressControl.css' type='text/css' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="NNCotizacionMiVidaValeIIUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <NNCotizacionMiVidaValeIIUC:NNCotizacionMiVidaValeIIUserControl ID="NNCotizacionMiVidaValeIIUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>