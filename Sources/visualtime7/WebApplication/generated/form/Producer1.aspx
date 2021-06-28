<%@ Page Title="Form1" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="Producer1.aspx.vb" Inherits="Producer1WebForm" UICulture="auto" Culture="auto" meta:resourcekey="Producer1PageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="Producer1UserControl.ascx" tagname="Producer1UserControl" tagprefix="Producer1UC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="Producer1.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="Producer1UpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <Producer1UC:Producer1UserControl ID="Producer1UserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>