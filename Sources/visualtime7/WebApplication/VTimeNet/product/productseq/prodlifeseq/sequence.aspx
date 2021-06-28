<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia
Dim mclsSequence As eFunctions.Sequence

'+ Objeto para el manejo de las páginas que forman la secuencia	
Dim mclsProduct_li As eProduct.ProdLifeSeq


</script>
<%Response.Expires = 0
mclsSequence = New eFunctions.Sequence
mclsProduct_li = New eProduct.ProdLifeSeq

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
Response.Write(mclsProduct_li.LoadTabsProdLifeSeq(Session("bQuery"), Session("nBranch"), Session("nProduct"), Session("dEffecdate"), Session("sBrancht"),  ,  , Session("nModulec")))

If Request.QueryString.Item("sGoToNext") = "Yes" Then
	Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
End If

mclsSequence = Nothing
mclsProduct_li = Nothing
%>





