<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBatch" %>
<script language="VB" runat="Server">

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence

'+ Objecto para el manejo de la funciones generales
Dim mobjValues As eFunctions.Values

'+ Objeto para el manejo de las páginas que forman la secuencia	
Dim mclsPolicy_Win As eBatch.ValBatch


</script>
<%Response.Expires = 0
mclsSequence = New eFunctions.Sequence
mobjValues = New eFunctions.Values
mclsPolicy_Win = New eBatch.ValBatch
%>
<HTML>
<HEAD>
	
	<META http-equiv="Content-Language" content="es">


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
Response.Write(mclsPolicy_Win.LoadTabs(Session("bQuery"), Session("nContent")))
If Request.QueryString.Item("sGoToNext") = "Yes" Then
	Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
End If

mclsSequence = Nothing
mclsPolicy_Win = Nothing
mobjValues = Nothing
%>





