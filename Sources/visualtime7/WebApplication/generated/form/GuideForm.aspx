<%@ Page Title="Form1" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="GuideForm.aspx.vb" Inherits="GuideFormWebForm" UICulture="auto" Culture="auto" meta:resourcekey="GuideFormPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="GuideFormUserControl.ascx" tagname="GuideFormUserControl" tagprefix="GuideFormUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="GuideForm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <GuideFormUC:GuideFormUserControl ID="GuideFormUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>        
</asp:Content>