<%@ Page Title="Cuestionario de enfermedades del corazón" Language="VB" AutoEventWireup="false" CodeFile="HeartDiseaseQuestionnaireUWPopup.aspx.vb" Inherits="HeartDiseaseQuestionnaireUWPopup" UICulture="auto" Culture="auto" meta:resourcekey="HeartDiseaseQuestionnaireUWPageTitleResource"%>

<%@ Register src="HeartDiseaseQuestionnaireUWUserControl.ascx" tagname="HeartDiseaseQuestionnaireUWUserControl" tagprefix="HeartDiseaseQuestionnaireUWUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="HeartDiseaseQuestionnaireUW.js" type="text/javascript"></script>
</head>
<body id="HeartDiseaseQuestionnaireUWBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="HeartDiseaseQuestionnaireUWUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <HeartDiseaseQuestionnaireUWUC:HeartDiseaseQuestionnaireUWUserControl ID="HeartDiseaseQuestionnaireUWUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>