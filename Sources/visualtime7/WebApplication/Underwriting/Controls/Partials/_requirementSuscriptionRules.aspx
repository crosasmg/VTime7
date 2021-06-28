<%@ Page Language="VB" AutoEventWireup="false" CodeFile="_requirementSuscriptionRules.aspx.vb" Inherits="Underwriting_Controls_Partials_requirementSuscriptionRules" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><% Response.Write(GetLocalResourceObject("title"))%></title>
</head>
<body>
    <div>
        <div class="rules-alert-success alert alert-success" style="display: none">
            <a href="#" data-hide="alert" class="close">&times;</a><% Response.Write(GetGlobalResourceObject("Resource", "AlertSuccessMessage"))%>
        </div>
        <div class="rules-alert-fail alert alert-danger" style="display: none">
            <a href="#" data-hide="alert" class="close">&times;</a><strong><% Response.Write(GetGlobalResourceObject("Resource", "AlertDangerMessage"))%></strong>
        </div>
        
        <div class="rules-container">
            <div class="rules-controls">
            </div>
            <div class="grid-rules-wrapper">
                <table id="grid-rules"></table>
                <div id="pager-rules"></div>
            </div>
        </div>
    </div>

    <link href="../Styles/fasi.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Styles\fasi.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />
    <script src="scripts/_requirementSuscriptionRules.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\_requirementSuscriptionRules.js").ToString("yyyyMMddHHmmss")%>"></script>

    <style>
        #pager-rules_left {
            width: auto !important;
        }
    </style>
    
</body>
</html>