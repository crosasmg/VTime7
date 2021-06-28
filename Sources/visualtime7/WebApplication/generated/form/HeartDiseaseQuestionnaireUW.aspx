<%@ Page Title="Cuestionario de enfermedades del corazón" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="HeartDiseaseQuestionnaireUW.aspx.vb" Inherits="HeartDiseaseQuestionnaireUWWebForm" UICulture="auto" Culture="auto" meta:resourcekey="HeartDiseaseQuestionnaireUWPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="HeartDiseaseQuestionnaireUWUserControl.ascx" tagname="HeartDiseaseQuestionnaireUWUserControl" tagprefix="HeartDiseaseQuestionnaireUWUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="HeartDiseaseQuestionnaireUW.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="HeartDiseaseQuestionnaireUWUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <HeartDiseaseQuestionnaireUWUC:HeartDiseaseQuestionnaireUWUserControl ID="HeartDiseaseQuestionnaireUWUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>