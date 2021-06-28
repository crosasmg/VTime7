<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Restrictions.ascx.vb" Inherits="Underwriting_Controls_Restrictions" %>
<div>
    <div class="restrictions-alert-success alert alert-success" style="display: none">
        <a href="#" data-hide="alert" class="close">&times;</a><% Response.Write(GetGlobalResourceObject("Resource", "AlertSuccessMessage"))%>
    </div>
    <div class="restrictions-alert-fail alert alert-danger" style="display: none">
        <a href="#" data-hide="alert" class="close">&times;</a><strong><% Response.Write(GetGlobalResourceObject("Resource", "AlertDangerMessage"))%></strong>
    </div>

    <div class="restrictions-container">
        <div class="restrictions-controls">
        </div>
        <div class="grid-restrictions-wrapper">
            <table id="grid-restrictions"></table>
            <div id="pager-restrictions"></div>
        </div>
    </div>
</div>