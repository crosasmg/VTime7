<%@ Page Title="SolicitudesAsociadasAGuiaDespacho" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="SolicitudesAsociadasAGuiaDespacho.aspx.vb" Inherits="SolicitudesAsociadasAGuiaDespachoWebForm" UICulture="auto" Culture="auto" meta:resourcekey="SolicitudesAsociadasAGuiaDespachoPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="SolicitudesAsociadasAGuiaDespachoUserControl.ascx" tagname="SolicitudesAsociadasAGuiaDespachoUserControl" tagprefix="SolicitudesAsociadasAGuiaDespachoUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="SolicitudesAsociadasAGuiaDespacho.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="SolicitudesAsociadasAGuiaDespachoUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <SolicitudesAsociadasAGuiaDespachoUC:SolicitudesAsociadasAGuiaDespachoUserControl ID="SolicitudesAsociadasAGuiaDespachoUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>