<%@ Page Language="VB" AutoEventWireup="false" CodeFile="_roleDetail.aspx.vb" Inherits="Underwriting_Controls_Partials_roleDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><% Response.Write(GetLocalResourceObject("title"))%></title>
</head>
<body>
    <form id="form-add-role" name="form-add-role" action="" class="form-horizontal">
        <div class="col-md-6">
            <div class="form-group">
                <label for="ddlRole" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("ddlRole"))%>:</label>
                <div class="col-sm-8">
                    <select class="form-control" id="ddlRole" name="ddlRole" required="required" title="<% Response.Write(GetLocalResourceObject("ddlRole"))%>">
                        <option />
                    </select>
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtCodigoDelCliente" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtCodigoDelCliente"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtCodigoDelCliente" name="txtCodigoDelCliente"
                        placeholder="<% Response.Write(GetLocalResourceObject("txtCodigoDelCliente"))%>" maxlength="30" required="required" onkeypress="return validateNumber(event, false);"
                        title="<% Response.Write(GetLocalResourceObject("txtCodigoDelCliente"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtNombre" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtNombre"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtNombre" name="txtNombre"
                        placeholder="<% Response.Write(GetLocalResourceObject("txtNombre"))%>" maxlength="30" required="required"
                        title="<% Response.Write(GetLocalResourceObject("txtNombre"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtDireccionCompleta" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtDireccionCompleta"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtDireccionCompleta" name="txtDireccionCompleta"
                        placeholder="<% Response.Write(GetLocalResourceObject("txtDireccionCompleta"))%>" maxlength="100" required="required"
                        title="<% Response.Write(GetLocalResourceObject("txtDireccionCompleta"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtNumeroDeTelefono" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtNumeroDeTelefono"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtNumeroDeTelefono" name="txtNumeroDeTelefono"
                        placeholder="<% Response.Write(GetLocalResourceObject("txtNumeroDeTelefono"))%>" maxlength="30" required="required" onkeypress="return validateNumber(event, false);"
                        title="<% Response.Write(GetLocalResourceObject("txtNumeroDeTelefono"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtEdadActuarial" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtEdadActuarial"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtEdadActuarial" name="txtEdadActuarial"
                        placeholder="<% Response.Write(GetLocalResourceObject("txtEdadActuarial"))%>" maxlength="3" required="required" onkeypress="return validateNumber(event, false);"
                        title="<% Response.Write(GetLocalResourceObject("txtEdadActuarial"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="ddlSexo" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("ddlSexo"))%>:</label>
                <div class="col-sm-8">
                    <select class="form-control" id="ddlSexo" name="ddlSexo" required="required" title="<% Response.Write(GetLocalResourceObject("ddlSexo"))%>">
                        <option />
                    </select>
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtAltura" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtAltura"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtAltura" name="txtAltura"
                        placeholder="<% Response.Write(GetLocalResourceObject("txtAltura"))%>" maxlength="4" required="required" onkeypress="return validateNumber(event, true);"
                        title="<% Response.Write(GetLocalResourceObject("txtAltura"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtPeso" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtPeso"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtPeso" name="txtPeso"
                        placeholder="<% Response.Write(GetLocalResourceObject("txtPeso"))%>" maxlength="3" required="required" onkeypress="return validateNumber(event, false);"
                        title="<% Response.Write(GetLocalResourceObject("txtPeso"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="chkFumador" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("chkFumador"))%></label>
                <div class="col-sm-8">
                    <input id="chkFumador" type="checkbox" value="" title="<% Response.Write(GetLocalResourceObject("chkFumador"))%>"/>
                </div>
            </div>
        </div>
    </form>

    <div class="col-sm-12 text-right">
        <button id="btn-actualizar-rol" type="button" class="btn btn-default" title="<% Response.Write(GetGlobalResourceObject("Resource", "Save"))%>">
            <i class="fa fa-pencil-square-o fa-lg"></i> <% Response.Write(GetGlobalResourceObject("Resource", "Save"))%>
        </button>
    </div>

    <link href="../Styles/fasi.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Styles\fasi.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />
    <script type="text/javascript" src="scripts/_roleDetail.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\_roleDetail.js").ToString("yyyyMMddHHmmss")%>"></script>
</body>
</html>
