<%@ Page Language="VB" AutoEventWireup="false" CodeFile="_attachmentDetail.aspx.vb" Inherits="Underwriting_Controls_Partials_attachmentDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><% Response.Write(GetLocalResourceObject("title"))%></title>
</head>
<body>
    <form id="form-add-attachment" name="form-add-attachment" action="" class="form-horizontal">
        <div class="col-md-6">
            <div class="form-group">
                <label for="txtNombreArchivo" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtNombreArchivo"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtNombreArchivo" name="txtNombreArchivo"
                        placeholder="<% Response.Write(GetLocalResourceObject("txtNombreArchivo"))%>" maxlength="75" required="required" title="<% Response.Write(GetLocalResourceObject("txtNombreArchivo"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtDescripcionArchivo" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtDescripcionArchivo"))%>:</label>
                <div class="col-sm-8">
                    <input type="text" class="form-control" id="txtDescripcionArchivo" name="txtDescripcionArchivo"
                        placeholder="<% Response.Write(GetLocalResourceObject("txtDescripcionArchivo"))%>" maxlength="75" required="required" title="<% Response.Write(GetLocalResourceObject("txtDescripcionArchivo"))%>" />
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label for="txtArchivo" class="col-sm-4 control-label">
                    <% Response.Write(GetLocalResourceObject("txtArchivo"))%>:</label>
                <div class="col-sm-8">
                    <input type="file" class="form-control" id="txtArchivo" name="txtArchivo" required="required" title="<% Response.Write(GetLocalResourceObject("txtArchivo"))%>" />
                </div>
            </div>
        </div>
    </form>

    <div class="col-sm-12 text-right">
        <button id="btn-actualizar-anexo" type="button" class="btn btn-default" style="display:none;" title="<% Response.Write(GetGlobalResourceObject("Resource", "Save"))%>">
            <i class="fa fa-pencil-square-o fa-lg"></i> <% Response.Write(GetGlobalResourceObject("Resource", "Save"))%>
        </button>
    </div>

    <link href="../Styles/fasi.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Styles\fasi.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />
    <script type="text/javascript" src="scripts/_attachmentDetail.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\_attachmentDetail.js").ToString("yyyyMMddHHmmss")%>"></script>
</body>
</html>