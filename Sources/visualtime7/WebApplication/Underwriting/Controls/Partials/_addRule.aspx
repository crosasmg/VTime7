<%@ Page Language="VB" AutoEventWireup="false" CodeFile="_addRule.aspx.vb" Inherits="Underwriting_Controls_Partials_addRule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title></title>
	<script type="text/javascript" src="scripts/_addRule.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\_addRule.js").ToString("yyyyMMddHHmmss")%>"></script>
</head>
<body>
	<form id="form-add-attachment" name="form-add-attachment" action="" class="form-horizontal">
		<div class="col-md-12 FormError bg-danger divError" style="display: none;"></div>
		<div class="form-group">
			<div class="col-md-4">
				<label for="txtReglaManual" class="control-label"><% Response.Write(GetLocalResourceObject("txtReglaManual"))%>:</label>
				<input type="text" class="form-control" id="txtReglaManual" name="txtReglaManual" placeholder="<% Response.Write(GetLocalResourceObject("txtReglaManual"))%>"
					maxlength="30" required="required" title="<% Response.Write(GetLocalResourceObject("txtReglaManual"))%>" />
			</div>
			<div class="col-md-4">
				<label for="txtEnfermedad" class="control-label"><% Response.Write(GetLocalResourceObject("ddlEnfermedad"))%>:</label>
				<input type="text" id="txtEnfermedad" name="txtEnfermedad" class="form-control" title="<% Response.Write(GetLocalResourceObject("ddlEnfermedad"))%>" placeholder="<% Response.Write(GetLocalResourceObject("ddlEnfermedad"))%>" />
			</div>
			<div class="col-md-4">
				<label for="ddlNivelEnfermedad" class="control-label"><% Response.Write(GetLocalResourceObject("ddlNivelEnfermedad"))%>:</label>
				<select class="form-control" disabled="disabled" id="ddlNivelEnfermedad" name="ddlNivelEnfermedad" title="<% Response.Write(GetLocalResourceObject("ddlNivelEnfermedad"))%>">
					<option />
				</select>
			</div>
		</div>
		<div class="form-group">
			<div class="col-md-4">
				<label for="txtExplicacion" class="control-label"><% Response.Write(GetLocalResourceObject("txtExplicacion"))%>:</label>
				<textarea class="form-control" id="txtExplicacion" name="txtExplicacion" title="<% Response.Write(GetLocalResourceObject("txtExplicacion"))%>" rows="6"></textarea>
			</div>
			<div class="col-md-8">
				<label for="ddlClientId" class="control-label"><% Response.Write(GetLocalResourceObject("ddlClientId"))%>:</label>
				<select class="form-control" id="ddlClientId" name="ddlClientId" title="<% Response.Write(GetLocalResourceObject("ddlClientId"))%>"></select>
			</div>
			<div class="col-md-4">
				<label for="ddlReqAreaDeSuscripcion" class="control-label"><% Response.Write(GetLocalResourceObject("ddlReqAreaDeSuscripcion"))%>:</label>
				<select class="form-control" id="ddlReqAreaDeSuscripcion" name="ddlReqAreaDeSuscripcion" required="required" title="<% Response.Write(GetLocalResourceObject("ddlReqAreaDeSuscripcion"))%>">
					<option />
				</select>
				<label for="txtPregunta" class="control-label"><% Response.Write(GetLocalResourceObject("ddlPregunta"))%>:</label>
				<input type="text" id="txtPregunta" name="txtPregunta" class="form-control" title="<% Response.Write(GetLocalResourceObject("ddlPregunta"))%>" placeholder="<% Response.Write(GetLocalResourceObject("ddlPregunta"))%>" />
			</div>
			<div class="col-md-4">
				<label for="txtCreadoPor" class="control-label"><% Response.Write(GetLocalResourceObject("txtCreadoPor"))%>:</label>
				<input type="text" class="form-control" id="txtCreadoPor" name="txtCreadoPor" required="required" disabled="disabled" title="<% Response.Write(GetLocalResourceObject("txtCreadoPor"))%>" />
			</div>
            <div class="col-md-4">
				<input type="hidden" class="form-control" id="txtRespuesta" name="txtRespuesta" disabled="disabled" title="<% Response.Write(GetLocalResourceObject("txtCreadoPor"))%>" />
			</div>
			<div class="col-md-4">
				<label for="rangoPuntos" class="control-label"><% Response.Write(GetLocalResourceObject("txtPuntos"))%>:</label>
				<label id="lblPuntos" class="control-label"></label>
				<input type="range" id="rangoPuntos" name="rangoPuntos" min="-99" max="99" />
			</div>
		</div>
		<div class="form-group">
			<div class="col-md-4"></div>
			<div class="col-md-4"></div>
			<div class="col-md-4">
				<label for="chkIsManual" class="control-label"><% Response.Write(GetLocalResourceObject("chkIsManual"))%>:</label>
				<input type="checkbox" id="chkIsManual" name="chkIsManual" checked="checked" disabled="disabled" />
			</div>
		</div>
		<div class="form-group" style="padding-left: 15px; padding-right: 15px">
			<div id="tabsAlarmas">
				<ul class="nav nav-tabs" role="tablist">
					<li role="presentation" class="active"><a href="#AlarmTab" onclick="javascript:sessionStorage.setItem('SelectedTab','AlarmTab')" aria-controls="AlarmTab" role="tab" data-toggle="tab" title="<% Response.Write(GetLocalResourceObject("title-alarma"))%>"><i class="fa fa-bell fa-lg" aria-hidden="true"></i><% Response.Write(GetLocalResourceObject("title-alarma"))%></a></li>
				</ul>
				<div class="alarm-container">
					<div class="alarm-controls"></div>
					<div class="grid-alarm-wrapper">
						<table id="grid-alarm"></table>
						<div id="pager-alarm"></div>
					</div>
				</div>
			</div>
		</div>
		<div class="form-group" style="padding-left: 15px; padding-right: 15px">
			<div id="tabsRestriccion">
				<ul class="nav nav-tabs" role="tablist">
					<li role="presentation" id="tabRecargos" class="active hide"><a href="#Recargos" aria-controls="RecargosTab" role="tab" data-toggle="tab" title="<% Response.Write(GetLocalResourceObject("title-recargos-descuentos"))%>"><i class="fa fa-exchange fa-lg" aria-hidden="true"></i><% Response.Write(GetLocalResourceObject("title-recargos-descuentos"))%></a></li>
					<li role="presentation" id="tabExclusion" class="active hide"><a href="#Exclusion" aria-controls="ExclusionTab" role="tab" data-toggle="tab" title="<% Response.Write(GetLocalResourceObject("title-exclusion"))%>"><i class="fa fa-exclamation-circle fa-lg" aria-hidden="true"></i><% Response.Write(GetLocalResourceObject("title-exclusion"))%></a></li>
					<li role="presentation" id="tabLimites" class="active hide"><a href="#Limites" aria-controls="LimitesTab" role="tab" data-toggle="tab" title="<% Response.Write(GetLocalResourceObject("title-limit"))%>"><i class="fa fa-line-chart fa-lg" aria-hidden="true"></i><% Response.Write(GetLocalResourceObject("title-limit"))%></a></li>
				</ul>
				<div class="restriction-container">
					<div class="restriction-controls"></div>
					<div class="grid-restriction-wrapper">
						<table id="grid-restriction"></table>
						<div id="pager-restriction"></div>
					</div>
				</div>
			</div>
		</div>
	</form>
	<link href="../Styles/fasi.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Styles\fasi.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />
</body>
</html>
