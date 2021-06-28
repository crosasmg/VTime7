<%@ Page Title="Form1" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="DPSiNVESMENTFUNDS.aspx.vb" Inherits="DPSiNVESMENTFUNDSWebForm" UICulture="auto" Culture="auto" meta:resourcekey="DPSiNVESMENTFUNDSPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="DPSiNVESMENTFUNDSUserControl.ascx" tagname="DPSiNVESMENTFUNDSUserControl" tagprefix="DPSiNVESMENTFUNDSUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="DPSiNVESMENTFUNDS.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <DPSiNVESMENTFUNDSUC:DPSiNVESMENTFUNDSUserControl ID="DPSiNVESMENTFUNDSUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>        
</asp:Content>