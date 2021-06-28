<%@ Page Title="Cuestionario de diábetes" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="DiabetesQuestionnaireUW.aspx.vb" Inherits="DiabetesQuestionnaireUWWebForm" UICulture="auto" Culture="auto" meta:resourcekey="DiabetesQuestionnaireUWPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="DiabetesQuestionnaireUWUserControl.ascx" tagname="DiabetesQuestionnaireUWUserControl" tagprefix="DiabetesQuestionnaireUWUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="DiabetesQuestionnaireUW.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="DiabetesQuestionnaireUWUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <DiabetesQuestionnaireUWUC:DiabetesQuestionnaireUWUserControl ID="DiabetesQuestionnaireUWUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>