<%@ Page Language="VB" AutoEventWireup="false" CodeFile="_restrictionsDetail_Recharge.aspx.vb" Inherits="Underwriting_Controls_Partials_restrictionsDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><% Response.Write(GetLocalResourceObject("title"))%></title>
</head>
<body>
    <form id="form-restrictions-detail-recharge" name="form-restrictions-detail-recharge" action="" class="form-horizontal">
         <div class="col-md-6">
            <div class="form-group">
                <label for="txtDescription" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtDescription"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtDescription" name="txtDescription"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtDescriptionToolTip"))%>" />
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label for="txtTipo" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtTipo"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtTipo" name="txtTipo"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtTipoToolTip"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtMoneda" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtMoneda"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtMoneda" name="txtMoneda"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtMonedaToolTip"))%>" />    
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtFactor" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtFactor"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtFactor" name="txtFactor"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtFactorToolTip"))%>" /> 
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtMontoFijoAgregar" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtMontoFijoAgregar"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtMontoFijoAgregar" name="txtMontoFijoAgregar"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtMontoFijoAgregarToolTip"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtPeriodo" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtPeriodo"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtPeriodo" name="txtPeriodo"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtPeriodoToolTip"))%>" />
                </div>
            </div>
        </div>        

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtDias" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtDias"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtDias" name="txtDias"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtDiasToolTip"))%>" />              

                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtMeses" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtMeses"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtMeses" name="txtMeses"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtMesesToolTip"))%>" />     
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtYear" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtYear"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtYear" name="txtYear"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtYearToolTip"))%>" />   

                </div>
            </div>
        </div>
    </form> 
    <link href="../Styles/fasi.css" rel="stylesheet" />
    <script type="text/javascript" src="scripts/_informacionGeneral.js"></script>
</body>
</html>
