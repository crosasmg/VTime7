<%@ Page Language="VB" AutoEventWireup="false" CodeFile="_ruleDetail.aspx.vb" Inherits="Underwriting_Controls_Partials_ruleDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title><% Response.Write(GetLocalResourceObject("title"))%></title>
</head>
<body>
	<form id="form-add-attachment" name="form-add-attachment" action="" class="form-horizontal">
		<div class="col-md-6">
			<div class="form-group">
				<label for="ddlPregunta" class="col-sm-4 control-label">
					<% Response.Write(GetLocalResourceObject("ddlPregunta"))%>:</label>
				<div class="col-sm-8">
					<select class="form-control" id="ddlPregunta" name="ddlPregunta" required="required" title="<% Response.Write(GetLocalResourceObject("ddlPregunta"))%>">
						<option />
					</select>
				</div>
			</div>
		</div>

		<div class="col-md-6">
			<div class="form-group">
				<label for="ddlRegla" class="col-sm-4 control-label">
					<% Response.Write(GetLocalResourceObject("ddlRegla"))%>:</label>
				<div class="col-sm-8">
					<select class="form-control" id="ddlRegla" name="ddlRegla" required="required" title="<% Response.Write(GetLocalResourceObject("ddlRegla"))%>">
						<option />
					</select>
				</div>
			</div>
		</div>

		<div class="col-md-6">
			<div class="form-group">
				<label for="ddlRegla" class="col-sm-4 control-label">
					<% Response.Write(GetLocalResourceObject("ddlEnfermedad"))%>:</label>
				<div class="col-sm-8">
					<select class="form-control" id="ddlEnfermedad" name="ddlEnfermedad" required="required" title="<% Response.Write(GetLocalResourceObject("ddlEnfermedad"))%>">
						<option />
					</select>
				</div>
			</div>
		</div>

		<div class="col-md-6">
			<div class="form-group">
				<label for="txtReglaManual" class="col-sm-4 control-label">
					<% Response.Write(GetLocalResourceObject("txtReglaManual"))%>:</label>
				<div class="col-sm-8">
					<input type="text" class="form-control" id="txtReglaManual" name="txtReglaManual"
						placeholder="<% Response.Write(GetLocalResourceObject("txtReglaManual"))%>" maxlength="30" required="required"
						title="<% Response.Write(GetLocalResourceObject("txtReglaManual"))%>" />
				</div>
			</div>
		</div>

		<div class="col-md-6">
			<div class="form-group">
				<label for="ddlReqAreaDeSuscripcion" class="col-sm-4 control-label">
					<% Response.Write(GetLocalResourceObject("ddlReqAreaDeSuscripcion"))%>:</label>
				<div class="col-sm-8">
					<select class="form-control" id="ddlReqAreaDeSuscripcion" name="ddlReqAreaDeSuscripcion" required="required" title="<% Response.Write(GetLocalResourceObject("ddlReqAreaDeSuscripcion"))%>">
						<option />
					</select>
				</div>
			</div>
		</div>

		<div class="col-md-6">
			<div class="form-group">
				<label for="txtPuntosAutomaticos" class="col-sm-4 control-label">
					<% Response.Write(GetLocalResourceObject("txtPuntosAutomaticos"))%>:</label>
				<div class="col-sm-8">
					<input type="number" class="form-control" id="txtPuntosAutomaticos" name="txtPuntosAutomaticos"
						max="100" min="-100" required="required" title="<% Response.Write(GetLocalResourceObject("txtPuntosAutomaticos"))%>" />
				</div>
			</div>
		</div>

		<div class="col-md-6">
			<div class="form-group">
				<label for="txtPuntosManuales" class="col-sm-4 control-label">
					<% Response.Write(GetLocalResourceObject("txtPuntosManuales"))%>:</label>
				<div class="col-sm-8">
					<input type="number" class="form-control" id="txtPuntosManuales" name="txtPuntosManuales"
						max="100" min="-100" required="required" title="<% Response.Write(GetLocalResourceObject("txtPuntosManuales"))%>" />
				</div>
			</div>
		</div>

		<div class="col-md-6">
			<div class="form-group">
				<label for="txtPuntuacionFinal" class="col-sm-4 control-label">
					<% Response.Write(GetLocalResourceObject("txtPuntuacionFinal"))%>:</label>
				<div class="col-sm-8">
					<input type="number" class="form-control" id="txtPuntuacionFinal" name="txtPuntuacionFinal"
						max="100" min="-100" required="required" title="<% Response.Write(GetLocalResourceObject("txtPuntuacionFinal"))%>" />
				</div>
			</div>
		</div>

		<div class="col-md-6">
			<div class="form-group">
				<label for="ddlTipoDeAlarma" class="col-sm-4 control-label">
					<% Response.Write(GetLocalResourceObject("ddlTipoDeAlarma"))%>:</label>
				<div class="col-sm-8">
					<select class="form-control" id="ddlTipoDeAlarma" name="ddlTipoDeAlarma" required="required" title="<% Response.Write(GetLocalResourceObject("ddlTipoDeAlarma"))%>">
						<option />
					</select>
				</div>
			</div>
		</div>

		<div class="col-md-6">
			<div class="form-group">
				<label for="ddlEstado" class="col-sm-4 control-label">
					<% Response.Write(GetLocalResourceObject("ddlEstado"))%>:</label>
				<div class="col-sm-8">
					<select class="form-control" id="ddlEstado" name="ddlEstado" required="required" title="<% Response.Write(GetLocalResourceObject("ddlEstado"))%>">
						<option />
					</select>
				</div>
			</div>
		</div>

		<div class="col-md-6">
			<div class="form-group">
				<label for="txtCreadoPor" class="col-sm-4 control-label">
					<% Response.Write(GetLocalResourceObject("txtCreadoPor"))%>:</label>
				<div class="col-sm-8">
					<input type="text" class="form-control" id="txtCreadoPor" name="txtCreadoPor"
						disabled="disabled" required="required" title="<% Response.Write(GetLocalResourceObject("txtCreadoPor"))%>" />
				</div>
			</div>
		</div>

	</form>

	<div class="col-sm-12 text-right">
		<button id="btn-actualizar-anexo" type="button" class="btn btn-default" title="<% Response.Write(GetGlobalResourceObject("Resource", "Save"))%>">
			<i class="fa fa-pencil-square-o fa-lg"></i><% Response.Write(GetGlobalResourceObject("Resource", "Save"))%>
		</button>
	</div>

    <link href="../Styles/fasi.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Styles\fasi.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />
	<script type="text/javascript" src="scripts/_ruleDetail.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\_ruleDetail.js").ToString("yyyyMMddHHmmss")%>"></script>
</body>
</html>
