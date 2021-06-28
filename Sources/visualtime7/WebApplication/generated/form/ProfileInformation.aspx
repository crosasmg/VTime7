<%@ Page Title="Profile Information" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="ProfileInformation.aspx.vb" Inherits="ProfileInformationWebForm" UICulture="auto" Culture="auto" meta:resourcekey="ProfileInformationPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="ProfileInformationUserControl.ascx" tagname="ProfileInformationUserControl" tagprefix="ProfileInformationUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="ProfileInformation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <ProfileInformationUC:ProfileInformationUserControl ID="ProfileInformationUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>        
</asp:Content>