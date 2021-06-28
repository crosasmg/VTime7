<%@ Page Title="Confirmación" Language="VB" MasterPageFile="~/DropthingsMasterPage.master" AutoEventWireup="false" CodeFile="Confirmación.aspx.vb" Inherits="ConfirmaciónWebForm" UICulture="auto" Culture="auto" meta:resourcekey="ConfirmaciónPageTitleResource"%>
<%@ MasterType TypeName="DropthingsMasterPage" %>
<%@ Register src="ConfirmaciónUserControl.ascx" tagname="ConfirmaciónUserControl" tagprefix="ConfirmaciónUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script src="Confirmación.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">   
      <asp:UpdatePanel ID="ConfirmaciónUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <ConfirmaciónUC:ConfirmaciónUserControl ID="ConfirmaciónUserControl" runat="server" />       
        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>