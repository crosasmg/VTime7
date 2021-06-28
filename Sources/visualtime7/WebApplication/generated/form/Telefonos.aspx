<%@ Page Title="Telefonos" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="Telefonos.aspx.vb" Inherits="TelefonosWebForm" UICulture="auto" Culture="auto" meta:resourcekey="TelefonosPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="TelefonosUserControl.ascx" tagname="TelefonosUserControl" tagprefix="TelefonosUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="Telefonos.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="TelefonosUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <TelefonosUC:TelefonosUserControl ID="TelefonosUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>