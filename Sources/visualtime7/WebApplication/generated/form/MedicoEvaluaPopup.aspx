<%@ Page Title="Recargos Médicos" Language="VB" AutoEventWireup="false" CodeFile="MedicoEvaluaPopup.aspx.vb" Inherits="MedicoEvaluaPopup" UICulture="auto" Culture="auto" meta:resourcekey="MedicoEvaluaPageTitleResource"%>

<%@ Register src="MedicoEvaluaUserControl.ascx" tagname="MedicoEvaluaUserControl" tagprefix="MedicoEvaluaUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="MedicoEvalua.js" type="text/javascript"></script>
</head>
<body id="MedicoEvaluaBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="MedicoEvaluaUpdatePanel" runat="server" updatemode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
             <MedicoEvaluaUC:MedicoEvaluaUserControl ID="MedicoEvaluaUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>