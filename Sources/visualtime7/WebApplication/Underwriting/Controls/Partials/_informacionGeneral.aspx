<%@ Page Language="VB" AutoEventWireup="false" CodeFile="_informacionGeneral.aspx.vb" Inherits="Underwriting_Controls_Partials_informacionGeneral" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><% Response.Write(GetLocalResourceObject("title"))%></title>
</head>
<body>
    <form id="form-informacion-general" name="form-informacion-general" action="" class="form-horizontal">
         <div class="col-md-6">
            <div class="form-group">
                <label for="txtRequirementID" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtRequirementID"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtRequirementID" name="txtRequirementID"
                         maxlength="30" readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtRequirementIDToolTip"))%>" />
					<input type="hidden" id="ClientId" name="ClientId" />
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label for="ddlTipoDeRequerimiento" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("ddlTipoDeRequerimiento"))%>:</label>
                <div class="col-sm-8">
                    <select class="form-control" id="ddlTipoDeRequerimiento" name="ddlTipoDeRequerimiento"
                        required="required" disabled="disabled" title="<% Response.Write(GetLocalResourceObject("ddlTipoDeRequerimientoToolTip"))%>">
                        <option />
                    </select>
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="ddlSolicitadoA" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("ddlSolicitadoA"))%>:</label>
                <div class="col-sm-8">
                    <select class="form-control" id="ddlSolicitadoA" name="ddlSolicitadoA" required="required" title="<% Response.Write(GetLocalResourceObject("ddlSolicitadoAToolTip"))%>" disabled="disabled" >
                    </select>
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="ddlTipoDeProceso" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("ddlTipoDeProceso"))%>:</label>
                <div class="col-sm-8">
                    <select class="form-control" id="ddlTipoDeProceso" name="ddlTipoDeProceso" required="required" title="<% Response.Write(GetLocalResourceObject("ddlTipoDeProcesoToolTip"))%>" disabled="disabled" >
                    </select>
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="ddlAreaDeSuscripcion" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("ddlAreaDeSuscripcion"))%>:</label>
                <div class="col-sm-8">
                    <select class="form-control" id="ddlAreaDeSuscripcion" name="ddlAreaDeSuscripcion" required="required" title="<% Response.Write(GetLocalResourceObject("ddlAreaDeSuscripcionToolTip"))%>">
                    </select>
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtFechaDeSolicitud" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtFechaDeSolicitud"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtFechaDeSolicitud" name="txtFechaDeSolicitud"
                        placeholder="<% Response.Write(GetLocalResourceObject("txtFechaDeSolicitud"))%>" maxlength="19" dateITA="true" required="required" readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtFechaDeSolicitudToolTip"))%>" />
                </div>
            </div>
        </div>        

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtFechaDeRecepcion" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtFechaDeRecepcion"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtFechaDeRecepcion" name="txtFechaDeRecepcion"
                        placeholder="<% Response.Write(GetLocalResourceObject("txtFechaDeRecepcion"))%>" dateITA="true" maxlength="19" title="<% Response.Write(GetLocalResourceObject("txtFechaDeRecepcionToolTip"))%>" />
                </div>
            </div>
        </div>

       <%-- <div class="col-md-6">
            <div class="form-group">
                <label for="ddlAlarma" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("ddlAlarma"))%>:</label>
                <div class="col-sm-8">
                    <select class="form-control" id="ddlAlarma" name="ddlAlarma" required="required" title="<% Response.Write(GetLocalResourceObject("ddlAlarmaToolTip"))%>">
                    </select>
                </div>
            </div>
        </div>--%>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtDebitos" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtDebitos"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtDebitos" name="txtDebitos"
                        placeholder="<% Response.Write(GetLocalResourceObject("txtDebitos"))%>" maxlength="30" required="required" readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtDebitosToolTip"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtCreditos" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtCreditos"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtCreditos" name="txtCreditos"
                        placeholder="<% Response.Write(GetLocalResourceObject("txtCreditos"))%>" maxlength="30" required="required" readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtCreditosToopTip"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtBalance" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtBalance"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtBalance" name="txtBalance"
                        placeholder="<% Response.Write(GetLocalResourceObject("txtBalance"))%>" maxlength="30" required="required" readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtBalanceToolTip"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="ddlProveedor" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("ddlProveedor"))%>:</label>
                <div class="col-sm-8">
                    <select class="form-control" id="ddlProveedor" name="ddlProveedor" title="<% Response.Write(GetLocalResourceObject("ddlProveedorToolTip"))%>">
                    </select>
                </div>
            </div>
        </div>

       <div class="col-md-6">
            <div class="form-group">
                <label for="ddlStatus" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("ddlStatus"))%>:</label>
                <div class="col-sm-8">
                    <select class="form-control" id="ddlStatus" name="ddlStatus"  required="required" title="<% Response.Write(GetLocalResourceObject("ddlStatusToolTip"))%>"  >
                    </select>
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="ddlPagador" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("ddlPagador"))%>:</label>
                <div class="col-sm-8">
                    <select class="form-control" id="ddlPagador" name="ddlPagador" required="required" title="<% Response.Write(GetLocalResourceObject("ddlPagadorToolTip"))%>">
                    </select>
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtCosto" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtCosto"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtCosto" name="txtCosto"
                        placeholder="<% Response.Write(GetLocalResourceObject("txtCosto"))%>" maxlength="30" required="required" title="<% Response.Write(GetLocalResourceObject("txtCostoToolTip"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtFaltaPorPagar" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtFaltaPorPagar"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtFaltaPorPagar" name="txtFaltaPorPagar"
                        placeholder="<% Response.Write(GetLocalResourceObject("txtFaltaPorPagar"))%>" maxlength="30" required="required" readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtFaltaPorPagarToolTip"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtCodigoAcord" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtCodigoAcord"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtCodigoAcord" name="txtCodigoAcord"
                        placeholder="<% Response.Write(GetLocalResourceObject("txtCodigoAcord"))%>" maxlength="30" required="required" readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtCodigoAcordToolTip"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-12" id="divEnlaces">
            <div class="form-group">
                <label class="col-sm-4 control-label" id="txtEnlaces">
                    <% Response.Write(GetLocalResourceObject("txtEnlaces"))%>:</label>
                <div class="col-sm-8">
                    <a id="aCargarInformacionManual" target="_blank" class="btn btn-default" href="#">
                        <i class="fa fa-upload" aria-hidden="true"></i> <% Response.Write(GetLocalResourceObject("anchorCargarInformacionManual"))%> </a>
                    <a id="aVerDocumento" target="_blank" class="btn btn-default" href="#">
                        <i class="fa fa-file-text-o" aria-hidden="true"></i> <% Response.Write(GetLocalResourceObject("anchorVerDocumento"))%> </a>
                </div>
            </div>
        </div>
    </form>

    <div class="col-sm-2 pull-right">
        <button id="btn-actualizar-informacion-general" type="button" class="btn btn-default" title="<% Response.Write(GetGlobalResourceObject("Resource", "Save"))%>">
            <i class="fa fa-pencil-square-o fa-lg"></i><% Response.Write(GetGlobalResourceObject("Resource", "Save"))%>
        </button>
    </div>

    <link href="../Styles/fasi.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Styles\fasi.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />
    <script type="text/javascript" src="scripts/_informacionGeneral.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\_informacionGeneral.js").ToString("yyyyMMddHHmmss")%>"></script>
</body>
</html>
