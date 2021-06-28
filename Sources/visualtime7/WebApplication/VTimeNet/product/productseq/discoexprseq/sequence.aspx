<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence

'+ Objeto para el manejo de las páginas que forman la secuencia	
Dim mclsDisco_expr As eProduct.Disco_expr


</script>
<%Response.Expires = -1
mclsSequence = New eFunctions.Sequence
mclsDisco_expr = New eProduct.Disco_expr
%>
<HTML>
<HEAD>
	
	<META HTTP-EQUIV="Content-Language" CONTENT="es">
    <BASE TARGET="fraFolder">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Sequence.js"></SCRIPT>
	<%Response.Write("<SCRIPT>")
If Session("bQuery") Then
	Response.Write("var pblnQuery=true;")
Else
	Response.Write("var pblnQuery=false;")
End If
Response.Write("</SCRIPT>")
%>
</HEAD>
<BODY <%=mclsSequence.BODYParameters()%>>
<%
'+ Se carga la secuencia sólo si todas las variables necesarias tienen valor
Response.Write(mclsDisco_expr.LoadTabs(Session("bQuery"), Session("nBranch"), Session("nProduct"), Session("nDisexprc"), Session("dEffecdate")))

If Request.QueryString.Item("sGoToNext") = "Yes" Then
	Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
End If

mclsSequence = Nothing
mclsDisco_expr = Nothing
%>





