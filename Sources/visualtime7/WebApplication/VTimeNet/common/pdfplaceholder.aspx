<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>

<% Response.Expires = -1441
%>
<HTML>
<HEAD>
<TITLE>Descargar archivo</TITLE>

<Script>
    function HostFile() {
        document.location.href ="PDFFile.aspx?file=<%=Request.QueryString("file")%>";
    }

</SCRIPT>
</HEAD>
<BODY ONLOAD="HostFile();">
<center><h3>Descargar archivo</h3></center>
<center>Si recibe advertencias de seguridad de su browser, elija <b>descargar archivo</b></center>
<br/>
<center><img height=80 strech src="/VTimeNet/images/downloadprompt.jpg" /></center>
<br/>
<center><a  href="javascript:window.close()">Cerrar</a></center>

</BODY>
</HTML>
