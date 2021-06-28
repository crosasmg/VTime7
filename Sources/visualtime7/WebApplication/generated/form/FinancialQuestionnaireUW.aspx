<%@ Page Title="Cuestionario financiero" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="FinancialQuestionnaireUW.aspx.vb" Inherits="FinancialQuestionnaireUWWebForm" UICulture="auto" Culture="auto" meta:resourcekey="FinancialQuestionnaireUWPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="FinancialQuestionnaireUWUserControl.ascx" tagname="FinancialQuestionnaireUWUserControl" tagprefix="FinancialQuestionnaireUWUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="FinancialQuestionnaireUW.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="FinancialQuestionnaireUWUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <FinancialQuestionnaireUWUC:FinancialQuestionnaireUWUserControl ID="FinancialQuestionnaireUWUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>