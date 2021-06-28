<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PolicyHistory.ascx.vb" Inherits="Underwriting_Controls_PolicyHistory" %>
<div>
    <div class="history-premium-alert-success alert alert-success" style="display: none">
        <a href="#" data-hide="alert" class="close">&times;</a><% Response.Write(GetGlobalResourceObject("Resource", "AlertSuccessMessage"))%>
    </div>
    <div class="history-premium-alert-fail alert alert-danger" style="display: none">
        <a href="#" data-hide="alert" class="close">&times;</a><strong><% Response.Write(GetGlobalResourceObject("Resource", "AlertDangerMessage"))%></strong>
    </div>
    <div class="history-premium-container">
        <div class="history-premium-controls">
        </div>
        <div class="grid-history-premium-wrapper">
            <table id="grid-history-premium"></table>
            <div id="pager-history-premium"></div>
        </div>
    </div>
</div>