<%@ Page Title="DECLARACIÓN PERSONAL DE SALUD DEL PROPUESTO ASEGURADO" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="SMFDIDPS.aspx.vb" Inherits="SMFDIDPSWebForm" UICulture="auto" Culture="auto" meta:resourcekey="SMFDIDPSPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="SMFDIDPSUserControl.ascx" tagname="SMFDIDPSUserControl" tagprefix="SMFDIDPSUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="SMFDIDPS.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <SMFDIDPSUC:SMFDIDPSUserControl ID="SMFDIDPSUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>        
</asp:Content>