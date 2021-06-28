<%@ Page Title="Cuestionario financiero" Language="VB" AutoEventWireup="false" CodeFile="FinancialQuestionnaireUWPopup.aspx.vb" Inherits="FinancialQuestionnaireUWPopup" UICulture="auto" Culture="auto" meta:resourcekey="FinancialQuestionnaireUWPageTitleResource"%>

<%@ Register src="FinancialQuestionnaireUWUserControl.ascx" tagname="FinancialQuestionnaireUWUserControl" tagprefix="FinancialQuestionnaireUWUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="FinancialQuestionnaireUW.js" type="text/javascript"></script>
</head>
<body id="FinancialQuestionnaireUWBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="FinancialQuestionnaireUWUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <FinancialQuestionnaireUWUC:FinancialQuestionnaireUWUserControl ID="FinancialQuestionnaireUWUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>