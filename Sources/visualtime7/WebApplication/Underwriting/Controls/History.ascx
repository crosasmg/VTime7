<%@ Control Language="VB" AutoEventWireup="false" CodeFile="History.ascx.vb" Inherits="Underwriting_Controls_History" %>
<div>
    <div class="history-alert-success alert alert-success" style="display: none">
        <a href="#" data-hide="alert" class="close">&times;</a><% Response.Write(GetGlobalResourceObject("Resource", "AlertSuccessMessage"))%>
    </div>
    <div class="history-alert-fail alert alert-danger" style="display: none">
        <a href="#" data-hide="alert" class="close">&times;</a><strong><% Response.Write(GetGlobalResourceObject("Resource", "AlertDangerMessage"))%></strong>
    </div>
    
    <div class="history-container">
        <div class="history-controls">
        </div>
        <div class="grid-history-wrapper">
            <table id="grid-history"></table>
            <div id="pager-history"></div>
        </div>
    </div>
</div>