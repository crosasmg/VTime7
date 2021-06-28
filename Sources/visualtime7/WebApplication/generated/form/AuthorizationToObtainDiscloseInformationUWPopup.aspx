<%@ Page Title="Autorización para obtener información relacionada con su historial médico" Language="VB" AutoEventWireup="false" CodeFile="AuthorizationToObtainDiscloseInformationUWPopup.aspx.vb" Inherits="AuthorizationToObtainDiscloseInformationUWPopup" UICulture="auto" Culture="auto" meta:resourcekey="AuthorizationToObtainDiscloseInformationUWPageTitleResource"%>

<%@ Register src="AuthorizationToObtainDiscloseInformationUWUserControl.ascx" tagname="AuthorizationToObtainDiscloseInformationUWUserControl" tagprefix="AuthorizationToObtainDiscloseInformationUWUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="AuthorizationToObtainDiscloseInformationUW.js" type="text/javascript"></script>
</head>
<body id="AuthorizationToObtainDiscloseInformationUWBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="AuthorizationToObtainDiscloseInformationUWUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <AuthorizationToObtainDiscloseInformationUWUC:AuthorizationToObtainDiscloseInformationUWUserControl ID="AuthorizationToObtainDiscloseInformationUWUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>