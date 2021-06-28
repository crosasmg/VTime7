<%@ Page Title="Cuestionario de diábetes" Language="VB" AutoEventWireup="false" CodeFile="DiabetesQuestionnaireUWPopup.aspx.vb" Inherits="DiabetesQuestionnaireUWPopup" UICulture="auto" Culture="auto" meta:resourcekey="DiabetesQuestionnaireUWPageTitleResource"%>

<%@ Register src="DiabetesQuestionnaireUWUserControl.ascx" tagname="DiabetesQuestionnaireUWUserControl" tagprefix="DiabetesQuestionnaireUWUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="DiabetesQuestionnaireUW.js" type="text/javascript"></script>
</head>
<body id="DiabetesQuestionnaireUWBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="DiabetesQuestionnaireUWUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <DiabetesQuestionnaireUWUC:DiabetesQuestionnaireUWUserControl ID="DiabetesQuestionnaireUWUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>