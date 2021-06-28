<%@ Page Title="Cuestionario de cáncer, tumor o quistes" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="CancerQuestionnaireUW.aspx.vb" Inherits="CancerQuestionnaireUWWebForm" UICulture="auto" Culture="auto" meta:resourcekey="CancerQuestionnaireUWPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="CancerQuestionnaireUWUserControl.ascx" tagname="CancerQuestionnaireUWUserControl" tagprefix="CancerQuestionnaireUWUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="CancerQuestionnaireUW.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="CancerQuestionnaireUWUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <CancerQuestionnaireUWUC:CancerQuestionnaireUWUserControl ID="CancerQuestionnaireUWUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>