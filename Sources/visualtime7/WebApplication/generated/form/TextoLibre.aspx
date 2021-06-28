<%@ Page Title="TextoLibre" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="TextoLibre.aspx.vb" Inherits="TextoLibreWebForm" UICulture="auto" Culture="auto" meta:resourcekey="TextoLibrePageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="TextoLibreUserControl.ascx" tagname="TextoLibreUserControl" tagprefix="TextoLibreUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="TextoLibre.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="TextoLibreUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <TextoLibreUC:TextoLibreUserControl ID="TextoLibreUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>