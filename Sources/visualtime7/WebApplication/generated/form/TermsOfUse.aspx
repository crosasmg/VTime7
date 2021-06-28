<%@ Page Title="Términos de Uso" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="TermsOfUse.aspx.vb" Inherits="TermsOfUseWebForm" UICulture="auto" Culture="auto" meta:resourcekey="TermsOfUsePageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="TermsOfUseUserControl.ascx" tagname="TermsOfUseUserControl" tagprefix="TermsOfUseUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="TermsOfUse.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="TermsOfUseUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <TermsOfUseUC:TermsOfUseUserControl ID="TermsOfUseUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>