<%@ Page Title="Form1" Language="VB" AutoEventWireup="false" CodeFile="GuideFormPopup.aspx.vb" Inherits="GuideFormPopup" UICulture="auto" Culture="auto" meta:resourcekey="GuideFormPageTitleResource"%>

<%@ Register src="GuideFormUserControl.ascx" tagname="GuideFormUserControl" tagprefix="GuideFormUC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Popup" runat="server">
    <title></title>
    <meta charset="iso-8859-1"> 
    <script src="/scripts/jquery.min.js" type="text/javascript"></script>
    <script src="GuideForm.js" type="text/javascript"></script>
</head>
<body id="GuideFormBody">
    <form id="popupForm" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <GuideFormUC:GuideFormUserControl ID="GuideFormUserControl" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>