<%@ Page Title="Cuestionario de cáncer, tumor o quistes" Language="VB" AutoEventWireup="false" CodeFile="CancerQuestionnaireUWPopup.aspx.vb" Inherits="CancerQuestionnaireUWPopup" UICulture="auto" Culture="auto" meta:resourcekey="CancerQuestionnaireUWPageTitleResource"%>

<%@ Register src="CancerQuestionnaireUWUserControl.ascx" tagname="CancerQuestionnaireUWUserControl" tagprefix="CancerQuestionnaireUWUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="CancerQuestionnaireUW.js" type="text/javascript"></script>
</head>
<body id="CancerQuestionnaireUWBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="CancerQuestionnaireUWUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <CancerQuestionnaireUWUC:CancerQuestionnaireUWUserControl ID="CancerQuestionnaireUWUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>