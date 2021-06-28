<%@ Page Title="ClientControl" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="ClientControl.aspx.vb" Inherits="ClientControlWebForm" UICulture="auto" Culture="auto" meta:resourcekey="ClientControlPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="ClientControlUserControl.ascx" tagname="ClientControlUserControl" tagprefix="ClientControlUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="ClientControl.js" type="text/javascript"></script>
    <script src="/customscripts/clientcontrol_devexpress.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="ClientControlUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <ClientControlUC:ClientControlUserControl ID="ClientControlUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>