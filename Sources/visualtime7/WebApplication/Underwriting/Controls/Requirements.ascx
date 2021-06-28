<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Requirements.ascx.vb" Inherits="Underwriting_Controls_Requirements" %>
<div>
    <div class="requirement-alert-success alert alert-success" style="display: none">
        <a href="#" data-hide="alert" class="close">&times;</a><% Response.Write(GetGlobalResourceObject("Resource", "AlertSuccessMessage"))%>
    </div>
    <div class="requirement-alert-fail alert alert-danger" style="display: none">
        <a href="#" data-hide="alert" class="close">&times;</a><strong><% Response.Write(GetGlobalResourceObject("Resource", "AlertDangerMessage"))%></strong>
    </div>
    
    <div class="requirement-container">
        <div class="requirement-controls">
        </div>
        <div class="grid-requirement-wrapper">
            <table id="grid-requirement"></table>
            <div id="pager-requirement"></div>
        </div>
    </div>
</div>