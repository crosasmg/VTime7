<%@ Page Title="Bienvenido" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="BienvenidaPortal.aspx.vb" Inherits="BienvenidaPortalWebForm" UICulture="auto" Culture="auto" meta:resourcekey="BienvenidaPortalPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="BienvenidaPortalUserControl.ascx" tagname="BienvenidaPortalUserControl" tagprefix="BienvenidaPortalUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="BienvenidaPortal.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="BienvenidaPortalUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <BienvenidaPortalUC:BienvenidaPortalUserControl ID="BienvenidaPortalUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>