<%@ Page Title="Autorización para obtener información relacionada con su historial médico" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="AuthorizationToObtainDiscloseInformationUW.aspx.vb" Inherits="AuthorizationToObtainDiscloseInformationUWWebForm" UICulture="auto" Culture="auto" meta:resourcekey="AuthorizationToObtainDiscloseInformationUWPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="AuthorizationToObtainDiscloseInformationUWUserControl.ascx" tagname="AuthorizationToObtainDiscloseInformationUWUserControl" tagprefix="AuthorizationToObtainDiscloseInformationUWUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="AuthorizationToObtainDiscloseInformationUW.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="AuthorizationToObtainDiscloseInformationUWUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <AuthorizationToObtainDiscloseInformationUWUC:AuthorizationToObtainDiscloseInformationUWUserControl ID="AuthorizationToObtainDiscloseInformationUWUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>