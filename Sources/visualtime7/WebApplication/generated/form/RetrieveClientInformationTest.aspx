<%@ Page Title="Retrieve Client Information" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="RetrieveClientInformationTest.aspx.vb" Inherits="RetrieveClientInformationTestWebForm" UICulture="auto" Culture="auto" meta:resourcekey="RetrieveClientInformationTestPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="RetrieveClientInformationTestUserControl.ascx" tagname="RetrieveClientInformationTestUserControl" tagprefix="RetrieveClientInformationTestUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="RetrieveClientInformationTest.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="RetrieveClientInformationTestUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <RetrieveClientInformationTestUC:RetrieveClientInformationTestUserControl ID="RetrieveClientInformationTestUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>