<%@ Page Language="VB" AutoEventWireup="false" CodeFile="_requirementsDetail.aspx.vb" Inherits="Underwriting_Controls_Partials_addRequirement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><% Response.Write(GetLocalResourceObject("title"))%></title>
</head>
<body>
    <div>
        <!-- Nav tabs -->
        <ul class="nav nav-tabs" role="tablist">
            <li role="presentation" class="active"><a href="#informacion-general" aria-controls="informacion-general" role="tab" data-toggle="tab" title="<% Response.Write(GetLocalResourceObject("informacion-general"))%>"><i class="fa fa-pencil-square-o fa-lg" aria-hidden="true"></i> <% Response.Write(GetLocalResourceObject("informacion-general"))%></a></li>
            <li role="presentation"><a href="#reglas-de-suscripcion" aria-controls="reglas-de-suscripcion" role="tab" data-toggle="tab" title="<% Response.Write(GetLocalResourceObject("reglas-de-suscripcion"))%>"><i class="fa fa-list-ol fa-lg" aria-hidden="true"></i> <% Response.Write(GetLocalResourceObject("reglas-de-suscripcion"))%></a></li>
            <li role="presentation"><a href="#anexos" aria-controls="anexos" role="tab" data-toggle="tab" title="<% Response.Write(GetLocalResourceObject("anexos"))%>"><i class="fa fa-folder-o fa-lg" aria-hidden="true"></i> <% Response.Write(GetLocalResourceObject("anexos"))%></a></li>
        </ul>

        <!-- Tab panes -->
        <div class="tab-content">
            <div role="tabpanel" class="tab-pane active" id="informacion-general">
                <div id="informacion-general-content"></div>
            </div>
            <div role="tabpanel" class="tab-pane" id="reglas-de-suscripcion">
                <div id="reglas-de-suscripcion-content"></div>
            </div>
            <div role="tabpanel" class="tab-pane" id="anexos">
                <div id="anexos-content">
                </div>
            </div>
        </div>
    </div>

    <link href="../Styles/fasi.css?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Styles\fasi.css").ToString("yyyyMMddHHmmss")%>" rel="stylesheet" />
    <script type="text/javascript" src="scripts/_requirementsDetail.js?v=<%=System.IO.File.GetLastWriteTime(Server.MapPath("~") + "\Underwriting\scripts\_requirementsDetail.js").ToString("yyyyMMddHHmmss")%>"></script>
</body>
</html>
