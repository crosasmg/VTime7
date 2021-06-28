<%@ Page Title="User Registration." Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="UserRegistration.aspx.vb" Inherits="UserRegistrationWebForm" UICulture="auto" Culture="auto" meta:resourcekey="UserRegistrationPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="UserRegistrationUserControl.ascx" tagname="UserRegistrationUserControl" tagprefix="UserRegistrationUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="UserRegistration.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="UserRegistrationUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <UserRegistrationUC:UserRegistrationUserControl ID="UserRegistrationUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>