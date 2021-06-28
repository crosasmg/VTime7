<%@ Page Title="Mi Vida vale" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="NNCotizacionMiVidaVale1Basico.aspx.vb" Inherits="NNCotizacionMiVidaVale1BasicoWebForm" UICulture="auto" Culture="auto" meta:resourcekey="NNCotizacionMiVidaVale1BasicoPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="NNCotizacionMiVidaVale1BasicoUserControl.ascx" tagname="NNCotizacionMiVidaVale1BasicoUserControl" tagprefix="NNCotizacionMiVidaVale1BasicoUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="NNCotizacionMiVidaVale1Basico.js" type="text/javascript"></script>
<link rel='stylesheet' href='/Controls/PhysicalAddressControl.css' type='text/css' />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="NNCotizacionMiVidaVale1BasicoUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <NNCotizacionMiVidaVale1BasicoUC:NNCotizacionMiVidaVale1BasicoUserControl ID="NNCotizacionMiVidaVale1BasicoUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>