<%@ Page Language="VB" AutoEventWireup="false" CodeFile="_restrictionsDetailInsuranceLimit.aspx.vb" Inherits="Underwriting_Controls_Partials_restrictionsDetail_InsuranceLimit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><% Response.Write(GetLocalResourceObject("title"))%></title>
</head>
<body>
    <form id="form-restrictions-detail-insurance-limit" name="form-restrictions-detail-insurance-limit" action="" class="form-horizontal">
        
        <div class="col-md-6" id="divModulo">
            <div class="form-group">
                <label for="txtModulo" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtModulo"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtModulo" name="txtModulo"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtModuloToolTip"))%>" />    
                </div>
            </div>
        </div>

        <div class="col-md-6" id="divCobertura">
            <div class="form-group">
                <label for="txtCobertura" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtCobertura"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtCobertura" name="txtCobertura"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtCoberturaToolTip"))%>" /> 
                </div>
            </div>
        </div>

        <div class="col-md-6" id="divMontoFijoAgregar">
            <div class="form-group">
                <label for="txtMontoFijoAgregar" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtMontoFijoAgregar"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtMontoFijoAgregar" name="txtMontoFijoAgregar"
                        readonly="readonly" title="<% Response.Write(GetLocalResourceObject("txtMontoFijoAgregarToolTip"))%>" />
                </div>
            </div>
        </div>
    </form> 
    <link href="../Styles/fasi.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Styles\fasi.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />
    <script type="text/javascript" src="scripts/_informacionGeneral.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\_informacionGeneral.js").ToString("yyyyMMddHHmmss")%>"></script>
</body>
</html>
