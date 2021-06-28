<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Decision.ascx.vb" Inherits="Underwriting_Controls_Decision" %>
<div>
    <div class="decision-alert-success alert alert-success" style="display: none">
        <a href="#" data-hide="alert" class="close">&times;</a><% Response.Write(GetGlobalResourceObject("Resource", "AlertSuccessMessage"))%>
    </div>
    <div class="decision-alert-fail alert alert-danger" style="display: none">
        <a href="#" data-hide="alert" class="close">&times;</a><strong><% Response.Write(GetGlobalResourceObject("Resource", "AlertDangerMessage"))%></strong>
    </div>

    <div class="decision-container">
        <div class="decision-controls">
        </div>
        <div class="grid-decision-wrapper">
            <table id="grid-decision"></table>
            <div id="pager-decision"></div>
        </div>
    </div>
</div>