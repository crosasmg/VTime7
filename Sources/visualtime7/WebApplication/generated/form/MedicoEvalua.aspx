<%@ Page Title="Recargos Médicos" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="MedicoEvalua.aspx.vb" Inherits="MedicoEvaluaWebForm" UICulture="auto" Culture="auto" meta:resourcekey="MedicoEvaluaPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="MedicoEvaluaUserControl.ascx" tagname="MedicoEvaluaUserControl" tagprefix="MedicoEvaluaUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="MedicoEvalua.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="MedicoEvaluaUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <MedicoEvaluaUC:MedicoEvaluaUserControl ID="MedicoEvaluaUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>