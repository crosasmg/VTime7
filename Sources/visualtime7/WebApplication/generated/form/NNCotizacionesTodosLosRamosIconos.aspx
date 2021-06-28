<%@ Page Title="Cotizaciones" Language="VB" MasterPageFile="~/fasi/FASI.master" AutoEventWireup="false" CodeFile="NNCotizacionesTodosLosRamosIconos.aspx.vb" Inherits="NNCotizacionesTodosLosRamosIconosWebForm" UICulture="auto" Culture="auto" meta:resourcekey="NNCotizacionesTodosLosRamosIconosPageTitleResource"%>
<%@ MasterType TypeName="FASI" %>
<%--<%@ Register src="NNCotizacionesTodosLosRamosIconosUserControl.ascx" tagname="NNCotizacionesTodosLosRamosIconosUserControl" tagprefix="NNCotizacionesTodosLosRamosIconosUC" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="NNCotizacionesTodosLosRamosIconos.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="NNCotizacionesTodosLosRamosIconosUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <%--<NNCotizacionesTodosLosRamosIconosUC:NNCotizacionesTodosLosRamosIconosUserControl ID="NNCotizacionesTodosLosRamosIconosUserControl" runat="server" />--%>       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>