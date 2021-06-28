<%@ Control Language="VB" AutoEventWireup="false" CodeFile="GeneralInformation.ascx.vb" Inherits="Underwriting_Controls_GeneralInformation" %>

<link href="/Styles/fasi.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Styles\fasi.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />

<div>
    <div class="role-alert-success alert alert-success" style="display: none">
        <a href="#" data-hide="alert" class="close">&times;</a><% Response.Write(GetGlobalResourceObject("Resource", "AlertSuccessMessage"))%>
    </div>
    <div class="role-alert-fail alert alert-danger" style="display: none">
        <a href="#" data-hide="alert" class="close">&times;</a><strong><% Response.Write(GetGlobalResourceObject("Resource", "AlertDangerMessage"))%></strong>
    </div>
    <div class="role-container">
        <div class="role-controls">
        </div>
        <div class="grid-role-wrapper">
            <table id="grid-role"></table>
            <div id="pager-role"></div>
        </div>
    </div>
</div>