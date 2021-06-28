<%@ Page Title="Mi Vida vale - Secuencia" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="NNCotizacionMiVidaValeSecuencia.aspx.vb" Inherits="NNCotizacionMiVidaValeSecuenciaWebForm" UICulture="auto" Culture="auto" meta:resourcekey="NNCotizacionMiVidaValeSecuenciaPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="NNCotizacionMiVidaValeSecuenciaUserControl.ascx" tagname="NNCotizacionMiVidaValeSecuenciaUserControl" tagprefix="NNCotizacionMiVidaValeSecuenciaUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="NNCotizacionMiVidaValeSecuencia.js" type="text/javascript"></script>
<link rel='stylesheet' href='/Controls/PhysicalAddressControl.css' type='text/css' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="NNCotizacionMiVidaValeSecuenciaUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <NNCotizacionMiVidaValeSecuenciaUC:NNCotizacionMiVidaValeSecuenciaUserControl ID="NNCotizacionMiVidaValeSecuenciaUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>