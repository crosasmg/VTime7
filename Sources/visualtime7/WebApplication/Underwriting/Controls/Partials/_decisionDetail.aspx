<%@ Page Language="VB" AutoEventWireup="false" CodeFile="_decisionDetail.aspx.vb" Inherits="Underwriting_Controls_Partials_restrictionsDetail_Exclusion" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><% Response.Write(GetLocalResourceObject("title"))%></title>
     <style>
        textarea {
          resize: none;
          max-width:100%; 
        } 
    </style>
</head>
<body>
    <form id="form-restrictions-detail-exclusion" name="form-decisions" action="" class="form-horizontal">
         <div class="col-md-6">
            <div class="form-group">
                <label for="txtTipo" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtRequerimiento"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtRequerimiento" name="txtRequerimiento"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtRequerimientoToolTip"))%>" />
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label for="txtTarifa" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtFechaHora"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtFechaHora" name="txtFechaHora"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtFechaHoraToolTip"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtModulo" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtPregunta"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtPregunta" name="txtPregunta"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtPreguntaToolTip"))%>" />    
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtCobertura" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtRespuesta"))%>:</label>
                <div class="col-sm-8">
                    <textarea disabled="disabled" style:"resize: none; max-width: 100%; max-height: 100%;" rows="3" cols="60" class="form-control" id="txtRespuesta" name="txtRespuesta" title="<% Response.Write(GetLocalResourceObject("txtRespuestaToolTip"))%>"/>
                </div>
            </div>
        </div>


        <div class="col-md-6">
            <div class="form-group">
                <label for="txtEnfermedad" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtCliente"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtCliente" name="txtCliente"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtClienteToolTip"))%>" />
                </div>
            </div>
        </div>        

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtDias" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtAreaSuscripcion"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtAreaSuscripcion" name="txtAreaSuscripcion"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtAreaSuscripcionToolTip"))%>" />              

                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtPeriodo" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtTotalDebitos"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtTotalDebitos" name="txtTotalDebitos"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtTotalDebitosToolTip"))%>" />     
                </div>
            </div>
        </div>

                <div class="col-md-6">
            <div class="form-group">
                <label for="txtDias" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtEstadoCaso"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtEstadoCaso" name="txtEstadoCaso"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtEstadoCasoToolTip"))%>" />              

                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtMeses" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtTotalCreditos"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtTotalCreditos" name="txtTotalCreditos"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtTotalCreditosToolTip"))%>" />     
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtYear" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("checkManual"))%>:</label>
                <div class="col-sm-8" style="padding:  11px;margin: auto;">
                    <input type="checkbox" style="top: 5px" id="checkManual" name="checkManual" disabled="disabled" 
                         title="<% Response.Write(GetLocalResourceObject("checkManualToolTip"))%>" />   

                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtMeses" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtBalance"))%>:</label>
                <div class="col-sm-8" >
                    <input type="text" class="form-control" id="txtBalance" name="txtBalance"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtBalanceToolTip"))%>" />     
                </div>
            </div>
        </div>
        
        <div class="col-md-6">
            <div class="form-group">
                <label for="txtMeses" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtCreadaPor"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtCreadaPor" name="txtCreadaPor"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtCreadaPorToolTip"))%>" />     
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtMeses" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtComentario"))%>:</label>
                <div class="col-sm-8">
                    <textarea disabled="disabled"  rows="5" cols="60" class="form-control" id="txtComentario" name="txtComentario" title="<% Response.Write(GetLocalResourceObject("txtComentarioToolTip"))%>"/>                      
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtMeses" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtExplicacion"))%>:</label>
                <div class="col-sm-8">
                    <textarea disabled="disabled"  rows="5" cols="60" class="form-control" id="txtExplicacion" name="txtExplicacion" title="<% Response.Write(GetLocalResourceObject("txtExplicacionToolTip"))%>"/>
                </div>
            </div>
        </div>

                <div class="col-md-6">
            <div class="form-group">
                <label for="txtMeses" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtTipoAlarma"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtTipoAlarma" name="txtTipoAlarma"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtTipoAlarmaToolTip"))%>" />     
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="" class="col-sm-4 control-label">
                    </label>
                <div class="col-sm-8">
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="" class="col-sm-4 control-label">
                    </label>
                <div class="col-sm-8">
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="" class="col-sm-4 control-label">
                    </label>
                <div class="col-sm-8">
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="" class="col-sm-4 control-label">
                    </label>
                <div class="col-sm-8">
                </div>
            </div>
        </div>

    </form> 
    
    <link href="../Styles/fasi.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Styles\fasi.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />
    <script type="text/javascript" src="scripts/_informacionGeneral.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\_informacionGeneral.js").ToString("yyyyMMddHHmmss")%>"></script>
    </body>

</html>