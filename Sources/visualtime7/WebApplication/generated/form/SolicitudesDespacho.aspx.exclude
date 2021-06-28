<%@ Page Title="Recepción Guías de Despacho" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="SolicitudesDespacho.aspx.vb" Inherits="SolicitudesDespachoWebForm" UICulture="auto" Culture="auto" meta:resourcekey="SolicitudesDespachoPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="SolicitudesDespachoUserControl.ascx" tagname="SolicitudesDespachoUserControl" tagprefix="SolicitudesDespachoUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="SolicitudesDespacho.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="SolicitudesDespachoUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <SolicitudesDespachoUC:SolicitudesDespachoUserControl ID="SolicitudesDespachoUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>