<%@ Page Title="Auto servicio" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="OnLineServiceUnderwriterII.aspx.vb" Inherits="OnLineServiceUnderwriterIIWebForm" UICulture="auto" Culture="auto" meta:resourcekey="OnLineServiceUnderwriterIIPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="OnLineServiceUnderwriterIIUserControl.ascx" tagname="OnLineServiceUnderwriterIIUserControl" tagprefix="OnLineServiceUnderwriterIIUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="OnLineServiceUnderwriterII.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="OnLineServiceUnderwriterIIUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <OnLineServiceUnderwriterIIUC:OnLineServiceUnderwriterIIUserControl ID="OnLineServiceUnderwriterIIUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>